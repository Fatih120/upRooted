using System.Reflection;
using System.Text;

namespace Uprooted;

/// <summary>
/// User Bio engine: lets Uprooted users set a short bio (max 190 chars)
/// visible on profile popups to other Uprooted users.
///
/// Bio text is stored on api.uprooted.sh using the same HMAC authentication
/// as UprootedPresenceBeacon. The engine handles:
///   - Registering (uploading) the local user's bio on startup
///   - Querying another user's bio when their profile popup opens
///   - Caching bio results with TTL
///   - Injecting the bio TextBlock into the profile popup visual tree
///
/// "View Only" mode allows seeing others' bios without broadcasting your own.
/// </summary>
internal class UserBioEngine
{
    private const string Tag = "UserBio";
    private const string ApiBase = "https://api.uprooted.sh";
    private const string Version = "0.4.3";
    private const int MaxBioLength = 190;
    private const string BioTag = "uprooted-user-bio";

    private readonly AvaloniaReflection _r;
    private readonly object _mainWindow;

    // Bio cache: uuid -> (bio text or null, expiry)
    private readonly Dictionary<string, (string? bio, DateTime expiry)> _cache = new();
    private readonly object _cacheLock = new();

    // Twemoji bitmap cache: hex codepoint key -> Avalonia Bitmap (null = download failed)
    private static readonly Dictionary<string, object?> _emojiCache = new();
    private static readonly object _emojiCacheLock = new();

    private UprootedPresenceBeacon? _beacon;

    /// <summary>
    /// Static instance for access from ContentPages (settings lightbox bio sync).
    /// </summary>
    internal static UserBioEngine? Instance { get; set; }

    internal UserBioEngine(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _mainWindow = mainWindow;
    }

    /// <summary>
    /// Start the engine. If the user has a bio set and is not in View Only mode,
    /// registers (uploads) the bio to the server.
    /// </summary>
    internal void Initialize(UprootedPresenceBeacon beacon)
    {
        _beacon = beacon;

        ThreadPool.QueueUserWorkItem(_ =>
        {
            // Wait for beacon to have OwnUuidStr (it polls for up to 60s)
            // Wait for beacon to resolve OwnUuidStr (polls up to 5 min for login)
            for (int i = 0; i < 65; i++)
            {
                if (_beacon.OwnUuidStr != null) break;
                Thread.Sleep(5_000);
            }

            if (_beacon.OwnUuidStr == null)
            {
                Logger.Log(Tag, "Own UUID not available — bio registration skipped");
                return;
            }

            try
            {
                var settings = UprootedSettings.Load();
                if (settings.UserBioViewOnly)
                {
                    Logger.Log(Tag, "View Only mode — skipping bio registration");
                    return;
                }
                if (string.IsNullOrWhiteSpace(settings.UserBioText))
                {
                    Logger.Log(Tag, "No bio text set — skipping registration");
                    return;
                }

                RegisterBio(settings.UserBioText);
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"Initialize error: {ex.Message}");
            }
        });
    }

    // ===== Bio registration (upload own bio) =====

    /// <summary>
    /// Upload the user's bio to the server. Called on startup and when bio is saved.
    /// </summary>
    internal void RegisterBio(string bioText)
    {
        if (_beacon?.OwnUuidStr == null)
        {
            Logger.Log(Tag, "RegisterBio: Own UUID not available");
            return;
        }

        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                var uuid = _beacon.OwnUuidStr;
                var utcDayNumber = (int)((DateTime.UtcNow - DateTime.UnixEpoch).TotalDays);
                var message = $"{uuid}:{utcDayNumber}";
                var hmacHex = UprootedPresenceBeacon.ComputeHmacHex(message, UprootedPresenceBeacon.DecryptKey());

                var bioEscaped = EscapeJsonString(bioText);
                var body = $"{{\"uuid\":\"{uuid}\",\"token\":\"{hmacHex}\",\"ts\":{utcDayNumber},\"bio\":\"{bioEscaped}\",\"v\":\"{Version}\"}}";

                var statusCode = HttpSendWithBody($"{ApiBase}/presence/bio", body, "Post");

                if (statusCode is 200 or 204)
                    Logger.Log(Tag, "Bio registered successfully");
                else if (statusCode == 429)
                    Logger.Log(Tag, "Bio registration rate-limited (OK)");
                else
                    Logger.Log(Tag, $"Bio registration failed: HTTP {statusCode}");
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"RegisterBio error: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Delete the user's bio from the server (when switching to View Only mode).
    /// </summary>
    internal void DeleteBio()
    {
        if (_beacon?.OwnUuidStr == null) return;

        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                var uuid = _beacon.OwnUuidStr;
                var utcDayNumber = (int)((DateTime.UtcNow - DateTime.UnixEpoch).TotalDays);
                var message = $"{uuid}:{utcDayNumber}";
                var hmacHex = UprootedPresenceBeacon.ComputeHmacHex(message, UprootedPresenceBeacon.DecryptKey());

                var body = $"{{\"uuid\":\"{uuid}\",\"token\":\"{hmacHex}\",\"ts\":{utcDayNumber},\"v\":\"{Version}\"}}";
                var statusCode = HttpSendWithBody($"{ApiBase}/presence/bio", body, "Delete");

                Logger.Log(Tag, statusCode is 200 or 204
                    ? "Bio deleted from server"
                    : $"Bio delete returned HTTP {statusCode}");
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"DeleteBio error: {ex.Message}");
            }
        });
    }

    // ===== Bio query API (called by ProfileBadgeInjector) =====

    /// <summary>
    /// Return cached bio for a UUID. Returns a sentinel to distinguish states:
    ///   - non-null string (possibly empty) = cached result
    ///   - null = not cached (caller should query async)
    /// Empty string means "user has no bio" (still a valid cache entry).
    /// </summary>
    internal string? TryGetCachedBio(string uuid)
    {
        lock (_cacheLock)
        {
            if (_cache.TryGetValue(uuid, out var entry))
            {
                if (DateTime.UtcNow < entry.expiry)
                    return entry.bio ?? "";
                _cache.Remove(uuid);
            }
        }
        return null;
    }

    /// <summary>
    /// Query the server for a user's bio in the background.
    /// Calls onResult(bioText) on a thread pool thread when complete.
    /// bioText is null if no bio, or the bio string if found.
    /// </summary>
    internal void QueryBioAsync(string uuid, Action<string?> onResult)
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            string? bio = null;
            TimeSpan ttl = TimeSpan.FromSeconds(30); // error TTL
            try
            {
                bio = FetchBio(uuid);
                ttl = !string.IsNullOrEmpty(bio)
                    ? TimeSpan.FromMinutes(5)   // has bio: cache 5 min
                    : TimeSpan.FromSeconds(60); // no bio: cache 60s
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"QueryBioAsync error for {uuid[..8]}...: {ex.Message}");
            }

            lock (_cacheLock)
                _cache[uuid] = (bio, DateTime.UtcNow + ttl);

            try { onResult(bio); }
            catch { }
        });
    }

    /// <summary>
    /// Fire-and-forget bio query that warms the cache.
    /// Safe to call speculatively — skips if already cached.
    /// </summary>
    internal void PrefetchBio(string uuid)
    {
        if (TryGetCachedBio(uuid) != null) return;
        QueryBioAsync(uuid, _ => { });
    }

    /// <summary>
    /// Pre-download Twemoji bitmaps for all emoji in the given text on the current thread
    /// (expected to be called from a ThreadPool thread). This moves emoji HTTP latency
    /// off the UI thread so that InjectBioText/TryInjectFormattedBio only hits the cache.
    /// </summary>
    internal void PredownloadEmojiBitmaps(string text)
    {
        if (!ContainsEmoji(text)) return;
        foreach (var (_, codepoints) in SplitEmoji(text))
        {
            if (codepoints != null)
                GetOrDownloadEmojiBitmap(codepoints);
        }
    }

    /// <summary>
    /// Invalidate a cached bio entry.
    /// </summary>
    internal void InvalidateBioCache(string uuid)
    {
        lock (_cacheLock) { _cache.Remove(uuid); }
    }

    // ===== Profile popup injection =====

    /// <summary>
    /// Try to inject a bio TextBlock into a profile popup.
    /// Called by ProfileBadgeInjector after badges are injected.
    /// </summary>
    internal void TryInjectBio(object popup, object verticalPanel, int insertIndex, string userId)
    {
        bool isSelf = string.Equals(userId, _beacon?.OwnUuidStr, StringComparison.OrdinalIgnoreCase);

        // Dedup: check if bio already injected in this popup
        var walker = new VisualTreeWalker(_r);
        foreach (var node in walker.DescendantsDepthFirst(popup))
        {
            if (_r.GetTag(node) == BioTag)
                return; // already injected
        }

        // Check plugin enabled
        var settings = UprootedSettings.Load();
        var pluginEnabled = settings.Plugins.TryGetValue("user-bio", out var enabled) && enabled;
        if (!pluginEnabled) return;

        if (isSelf)
        {
            // Own profile: show editable bio section (read from local settings, not server)
            if (!settings.UserBioViewOnly)
                InjectOwnBioSection(verticalPanel, insertIndex, settings);
            return;
        }

        var cached = TryGetCachedBio(userId);
        if (cached != null)
        {
            // Cache hit
            if (!string.IsNullOrEmpty(cached))
                InjectBioText(verticalPanel, insertIndex, cached);
            return;
        }

        // Cache miss: query async
        QueryBioAsync(userId, bio =>
        {
            if (string.IsNullOrEmpty(bio)) return;
            // Pre-download emoji bitmaps on this ThreadPool thread so the UI thread
            // only hits the bitmap cache (no HTTP latency during injection).
            PredownloadEmojiBitmaps(bio);
            _r.RunOnUIThread(() =>
            {
                try
                {
                    // Verify popup still attached
                    if (_r.GetParent(popup) == null) return;

                    // Re-check dedup (another callback may have injected)
                    foreach (var node in walker.DescendantsDepthFirst(popup))
                    {
                        if (_r.GetTag(node) == BioTag)
                            return;
                    }

                    InjectBioText(verticalPanel, insertIndex, bio);
                }
                catch (Exception ex)
                {
                    Logger.Log(Tag, $"Bio inject (async) error: {ex.Message}");
                }
            });
        });
    }

    /// <summary>
    /// Create and insert a bio display into the vertical panel.
    /// Supports basic markdown: **bold** and *italic*.
    /// </summary>
    private void InjectBioText(object verticalPanel, int insertIndex, string bioText)
    {
        try
        {
            // Try markdown-formatted rendering first
            if (TryInjectFormattedBio(verticalPanel, insertIndex, bioText))
            {
                Logger.Log(Tag, $"Bio injected (markdown): {bioText.Length} chars");
                return;
            }

            // Fallback: plain TextBlock
            var textBlock = _r.CreateTextBlock(bioText, 12.0, "#9A9AAA");
            if (textBlock == null)
            {
                Logger.Log(Tag, "InjectBioText: CreateTextBlock returned null");
                return;
            }

            _r.SetTextWrapping(textBlock, "Wrap");
            _r.SetHorizontalAlignment(textBlock, "Center");
            CenterTextAlignment(textBlock);
            _r.SetMargin(textBlock, 0, 6, 0, 2);
            _r.SetMaxWidth(textBlock, 240);
            _r.SetTag(textBlock, BioTag);

            _r.InsertChild(verticalPanel, insertIndex, textBlock);
            Logger.Log(Tag, $"Bio injected: {bioText.Length} chars");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"InjectBioText error: {ex.Message}");
        }
    }

    /// <summary>
    /// Try to render bio text with basic markdown (bold/italic) and emoji images using TextBlock Inlines.
    /// Emoji characters are replaced with Twemoji PNG images via InlineUIContainer.
    /// Returns false if Inline types can't be resolved (fallback to plain text).
    /// </summary>
    private bool TryInjectFormattedBio(object verticalPanel, int insertIndex, string bioText)
    {
        var segments = ParseMarkdownSegments(bioText);
        bool hasFormatting = false;
        foreach (var seg in segments)
            if (seg.bold || seg.italic) { hasFormatting = true; break; }
        bool hasEmoji = ContainsEmoji(bioText);
        if (!hasFormatting && !hasEmoji) return false;

        try
        {
            // Create TextBlock with foreground + font size (empty text — Inlines will render)
            var textBlock = _r.CreateTextBlock("", 12.0, "#9A9AAA");
            if (textBlock == null) return false;

            // Resolve Inline types from same assembly as TextBlock
            var asm = textBlock.GetType().Assembly;
            var runType = asm.GetType("Avalonia.Controls.Documents.Run");
            if (runType == null) return false;

            // Resolve InlineUIContainer for emoji image rendering
            var containerType = hasEmoji
                ? asm.GetType("Avalonia.Controls.Documents.InlineUIContainer")
                : null;

            // If only emoji (no formatting) but InlineUIContainer unavailable, fall back to plain TextBlock
            if (!hasFormatting && containerType == null) return false;

            var inlinesProp = textBlock.GetType().GetProperty("Inlines");
            var inlines = inlinesProp?.GetValue(textBlock);
            if (inlines == null) return false;

            var addMethod = inlines.GetType().GetMethod("Add");
            if (addMethod == null) return false;

            foreach (var (text, bold, italic) in segments)
            {
                if (string.IsNullOrEmpty(text)) continue;

                // Split each markdown segment into text + emoji sub-segments
                var parts = hasEmoji ? SplitEmoji(text) : new List<(string, int[]?)> { (text, null) };

                foreach (var (partText, codepoints) in parts)
                {
                    if (codepoints != null)
                    {
                        // Emoji segment → InlineUIContainer with Twemoji Image
                        object? bitmap = containerType != null ? GetOrDownloadEmojiBitmap(codepoints) : null;
                        if (bitmap != null)
                        {
                            var img = _r.CreateImage("Uniform");
                            if (img != null)
                            {
                                _r.SetImageSource(img, bitmap);
                                _r.SetWidth(img, 14);
                                _r.SetHeight(img, 14);
                                var container = Activator.CreateInstance(containerType!);
                                if (container != null)
                                {
                                    container.GetType().GetProperty("Child")?.SetValue(container, img);
                                    try { addMethod.Invoke(inlines, new[] { container }); continue; }
                                    catch { }
                                }
                            }
                        }

                        // Fallback: render emoji as text Run (system font rendering)
                        var emojiSb = new StringBuilder();
                        foreach (var cp in codepoints) emojiSb.Append(char.ConvertFromUtf32(cp));
                        var fallbackRun = Activator.CreateInstance(runType);
                        if (fallbackRun != null)
                        {
                            fallbackRun.GetType().GetProperty("Text")?.SetValue(fallbackRun, emojiSb.ToString());
                            try { addMethod.Invoke(inlines, new[] { fallbackRun }); } catch { }
                        }
                        continue;
                    }

                    if (string.IsNullOrEmpty(partText)) continue;

                    // Text segment → Run with optional bold/italic formatting
                    var run = Activator.CreateInstance(runType);
                    if (run == null) continue;
                    run.GetType().GetProperty("Text")?.SetValue(run, partText);

                    if (bold && _r.FontWeightType != null)
                    {
                        try
                        {
                            var boldWeight = Activator.CreateInstance(_r.FontWeightType, 700);
                            run.GetType().GetProperty("FontWeight")?.SetValue(run, boldWeight);
                        }
                        catch { }
                    }

                    if (italic)
                    {
                        try
                        {
                            var fontStyleType = asm.GetType("Avalonia.Media.FontStyle");
                            if (fontStyleType != null)
                            {
                                var italicVal = Enum.Parse(fontStyleType, "Italic");
                                run.GetType().GetProperty("FontStyle")?.SetValue(run, italicVal);
                            }
                        }
                        catch { }
                    }

                    try { addMethod.Invoke(inlines, new[] { run }); }
                    catch { return false; }
                }
            }

            _r.SetTextWrapping(textBlock, "Wrap");
            _r.SetHorizontalAlignment(textBlock, "Center");
            CenterTextAlignment(textBlock);
            _r.SetMargin(textBlock, 0, 6, 0, 2);
            _r.SetMaxWidth(textBlock, 240);
            _r.SetTag(textBlock, BioTag);

            _r.InsertChild(verticalPanel, insertIndex, textBlock);
            return true;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"TryInjectFormattedBio error (falling back to plain): {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Parse basic markdown: **bold** and *italic* segments.
    /// Returns list of (text, bold, italic) tuples.
    /// </summary>
    private static List<(string text, bool bold, bool italic)> ParseMarkdownSegments(string input)
    {
        var segments = new List<(string text, bool bold, bool italic)>();
        int i = 0;
        var buf = new StringBuilder();

        while (i < input.Length)
        {
            if (i + 1 < input.Length && input[i] == '*' && input[i + 1] == '*')
            {
                if (buf.Length > 0) { segments.Add((buf.ToString(), false, false)); buf.Clear(); }
                int close = input.IndexOf("**", i + 2, StringComparison.Ordinal);
                if (close >= 0)
                {
                    segments.Add((input.Substring(i + 2, close - i - 2), true, false));
                    i = close + 2;
                }
                else { buf.Append("**"); i += 2; }
            }
            else if (input[i] == '*')
            {
                if (buf.Length > 0) { segments.Add((buf.ToString(), false, false)); buf.Clear(); }
                int close = input.IndexOf('*', i + 1);
                if (close >= 0)
                {
                    segments.Add((input.Substring(i + 1, close - i - 1), false, true));
                    i = close + 1;
                }
                else { buf.Append('*'); i++; }
            }
            else { buf.Append(input[i]); i++; }
        }

        if (buf.Length > 0) segments.Add((buf.ToString(), false, false));
        return segments;
    }

    /// <summary>
    /// Create and insert an editable bio section into the user's own profile popup.
    /// Features: live character counter, Save button, auto-save on LostFocus.
    /// </summary>
    private void InjectOwnBioSection(object verticalPanel, int insertIndex, UprootedSettings settings)
    {
        try
        {
            // Container for the editable bio section
            var container = _r.CreateStackPanel(vertical: true, spacing: 4);
            if (container == null) return;
            _r.SetTag(container, BioTag);
            _r.SetMargin(container, 0, 6, 0, 2);

            // Editable TextBox with current bio
            var bioBox = _r.CreateTextBox("Set your bio...", settings.UserBioText, MaxBioLength);
            if (bioBox == null) return;

            // Style to be compact and blend with the profile popup
            try { bioBox.GetType().GetProperty("FontSize")?.SetValue(bioBox, 12.0); } catch { }

            // Set TextWrapping directly on the TextBox (AvaloniaReflection.SetTextWrapping
            // uses TextBlock's PropertyInfo which doesn't apply to TextBox)
            if (_r.TextWrappingType != null)
            {
                try
                {
                    var wrapVal = Enum.Parse(_r.TextWrappingType, "Wrap");
                    bioBox.GetType().GetProperty("TextWrapping")?.SetValue(bioBox, wrapVal);
                }
                catch { }
            }

            _r.SetHeight(bioBox, 48);
            _r.SetPadding(bioBox, 8, 6, 8, 6);
            _r.SetBackground(bioBox, "#18ffffff");
            _r.SetForeground(bioBox, "#9A9AAA");

            // Subtle rounded corners, no border
            try
            {
                if (_r.CornerRadiusType != null)
                {
                    var cr = Activator.CreateInstance(_r.CornerRadiusType, 6.0, 6.0, 6.0, 6.0);
                    bioBox.GetType().GetProperty("CornerRadius")?.SetValue(bioBox, cr);
                }
                if (_r.ThicknessType != null)
                {
                    var zero = Activator.CreateInstance(_r.ThicknessType, 0.0, 0.0, 0.0, 0.0);
                    bioBox.GetType().GetProperty("BorderThickness")?.SetValue(bioBox, zero);
                }
            }
            catch { }

            // --- Bottom row: character counter + Save button ---
            var counterText = _r.CreateTextBlock(
                $"{settings.UserBioText.Length}/{MaxBioLength}",
                10.0, "#666680");

            // Save button: small pill
            var saveBtnText = _r.CreateTextBlock("Save", 10.0, "#B0B0C0");
            object? saveBtn = null;
            if (saveBtnText != null)
            {
                _r.SetVerticalAlignment(saveBtnText, "Center");
                saveBtn = _r.CreateBorder(bgHex: "#28ffffff", cornerRadius: 4, child: saveBtnText);
                if (saveBtn != null)
                {
                    _r.SetPadding(saveBtn, 10, 3, 10, 3);
                    _r.SetCursorHand(saveBtn);
                }
            }

            // Horizontal row: counter (left) + spacer + save button (right)
            var bottomRow = _r.CreateStackPanel(vertical: false, spacing: 6);
            if (bottomRow != null)
                _r.SetHorizontalAlignment(bottomRow, "Right");

            // Shared save logic
            var bioBoxRef = bioBox;
            var counterRef = counterText;
            Action doSave = () =>
            {
                try
                {
                    var text = _r.GetTextBoxText(bioBoxRef)?.Trim() ?? "";
                    if (text.Length > MaxBioLength) text = text[..MaxBioLength];
                    var s = UprootedSettings.Load();
                    s.UserBioText = text;
                    try { s.Save(); } catch { }
                    try { counterRef?.GetType().GetProperty("Text")?.SetValue(counterRef, $"{text.Length}/{MaxBioLength}"); } catch { }
                    if (!string.IsNullOrWhiteSpace(text))
                        RegisterBio(text);
                    else
                        DeleteBio();
                    Logger.Log(Tag, $"Own bio saved: {text.Length} chars");
                }
                catch (Exception ex)
                {
                    Logger.Log(Tag, $"Own bio save error: {ex.Message}");
                }
            };

            // Live character counter: update on every keystroke via TextChanged
            _r.SubscribeEvent(bioBox, "TextChanged", () =>
            {
                try
                {
                    var text = _r.GetTextBoxText(bioBoxRef) ?? "";
                    counterRef?.GetType().GetProperty("Text")?.SetValue(counterRef, $"{text.Length}/{MaxBioLength}");
                }
                catch { }
            });

            // Save on LostFocus
            _r.SubscribeEvent(bioBox, "LostFocus", () => doSave());

            // Save on button click
            if (saveBtn != null)
                _r.SubscribeClickReleased(saveBtn, () => doSave());

            // Assemble bottom row
            if (bottomRow != null)
            {
                if (counterText != null) _r.AddChild(bottomRow, counterText);
                if (saveBtn != null) _r.AddChild(bottomRow, saveBtn);
            }

            // Assemble container
            _r.AddChild(container, bioBox);
            if (bottomRow != null) _r.AddChild(container, bottomRow);
            else if (counterText != null) _r.AddChild(container, counterText);

            _r.InsertChild(verticalPanel, insertIndex, container);
            Logger.Log(Tag, $"Own bio section injected (current: {settings.UserBioText.Length} chars)");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"InjectOwnBioSection error: {ex.Message}");
        }
    }

    // ===== Emoji rendering (Twemoji) =====

    /// <summary>
    /// Quick scan: does the text contain any emoji that should be rendered as Twemoji images?
    /// Checks for supplementary-plane emoji and common BMP emoji ranges.
    /// </summary>
    private static bool ContainsEmoji(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsHighSurrogate(text[i]) && i + 1 < text.Length && char.IsLowSurrogate(text[i + 1]))
            {
                int cp = char.ConvertToUtf32(text[i], text[i + 1]);
                if (cp >= 0x1F000 && cp <= 0x1FAFF) return true;
            }
            char c = text[i];
            if (c >= '\u2600' && c <= '\u27BF') return true;
            if (c >= '\u2300' && c <= '\u23FF') return true;
        }
        return false;
    }

    /// <summary>
    /// Split text into alternating plain-text and emoji segments.
    /// Returns list of (text, codepoints) where codepoints != null means emoji.
    /// </summary>
    private static List<(string text, int[]? codepoints)> SplitEmoji(string input)
    {
        var result = new List<(string text, int[]? codepoints)>();
        var textBuf = new StringBuilder();
        int i = 0;

        while (i < input.Length)
        {
            int saved = i;
            var cps = TryReadEmojiSequence(input, ref i);
            if (cps != null)
            {
                if (textBuf.Length > 0) { result.Add((textBuf.ToString(), null)); textBuf.Clear(); }
                result.Add(("", cps));
            }
            else
            {
                // Regular character (or surrogate pair that isn't emoji)
                if (char.IsHighSurrogate(input[i]) && i + 1 < input.Length && char.IsLowSurrogate(input[i + 1]))
                { textBuf.Append(input[i]); textBuf.Append(input[i + 1]); i += 2; }
                else
                { textBuf.Append(input[i]); i++; }
            }
        }

        if (textBuf.Length > 0) result.Add((textBuf.ToString(), null));
        return result;
    }

    /// <summary>
    /// Try to consume an emoji sequence starting at position i.
    /// Handles regional indicator pairs, keycap sequences, ZWJ sequences, skin tone modifiers.
    /// Advances i past the sequence on success, leaves unchanged on failure.
    /// </summary>
    private static int[]? TryReadEmojiSequence(string s, ref int i)
    {
        int pos = i;
        int cp = ReadCp(s, pos, out int len);

        // Regional indicator pair (flags): two consecutive U+1F1E6..U+1F1FF
        if (cp >= 0x1F1E6 && cp <= 0x1F1FF)
        {
            if (pos + len < s.Length)
            {
                int next = ReadCp(s, pos + len, out int len2);
                if (next >= 0x1F1E6 && next <= 0x1F1FF)
                {
                    i = pos + len + len2;
                    return new[] { cp, next };
                }
            }
            return null; // lone regional indicator — not a valid flag
        }

        // Keycap sequence: [0-9#*] + FE0F? + 20E3
        if ((cp >= '0' && cp <= '9') || cp == '#' || cp == '*')
        {
            int p = pos + len;
            if (p < s.Length)
            {
                int n1 = ReadCp(s, p, out int l1);
                if (n1 == 0xFE0F && p + l1 < s.Length)
                {
                    int n2 = ReadCp(s, p + l1, out int l2);
                    if (n2 == 0x20E3) { i = p + l1 + l2; return new[] { cp, 0xFE0F, 0x20E3 }; }
                }
                if (n1 == 0x20E3) { i = p + l1; return new[] { cp, 0x20E3 }; }
            }
            return null; // plain digit/hash/asterisk
        }

        // Must be an emoji-starting codepoint
        if (!IsEmojiStart(cp)) return null;

        var cps = new List<int> { cp };
        pos += len;

        // Consume trailing modifiers, VS16, skin tones, ZWJ sequences
        while (pos < s.Length)
        {
            int next = ReadCp(s, pos, out int nLen);

            // VS16 (emoji presentation selector)
            if (next == 0xFE0F) { cps.Add(next); pos += nLen; continue; }

            // Skin tone modifier (U+1F3FB..U+1F3FF)
            if (next >= 0x1F3FB && next <= 0x1F3FF) { cps.Add(next); pos += nLen; continue; }

            // ZWJ sequence: U+200D followed by another emoji
            if (next == 0x200D && pos + nLen < s.Length)
            {
                int after = ReadCp(s, pos + nLen, out int aLen);
                if (IsEmojiStart(after))
                {
                    cps.Add(0x200D);
                    cps.Add(after);
                    pos += nLen + aLen;
                    continue;
                }
            }

            break;
        }

        i = pos;
        return cps.ToArray();
    }

    /// <summary>
    /// Is this codepoint a valid start of an emoji sequence?
    /// Covers supplementary-plane emoji blocks and common BMP pictographic ranges.
    /// </summary>
    private static bool IsEmojiStart(int cp)
    {
        if (cp >= 0x1F000 && cp <= 0x1FAFF) return true;  // Supplementary plane emoji
        if (cp >= 0x2600 && cp <= 0x27BF) return true;    // Misc symbols + dingbats
        if (cp >= 0x2300 && cp <= 0x23FF) return true;    // Misc technical
        if (cp >= 0x2B05 && cp <= 0x2B55) return true;    // Arrows & shapes
        if (cp >= 0x25A0 && cp <= 0x25FF) return true;    // Geometric shapes
        if (cp == 0x00A9 || cp == 0x00AE || cp == 0x2122) return true; // ©, ®, ™
        return false;
    }

    /// <summary>
    /// Read a single Unicode codepoint from a UTF-16 string, handling surrogate pairs.
    /// </summary>
    private static int ReadCp(string s, int index, out int charCount)
    {
        if (char.IsHighSurrogate(s[index]) && index + 1 < s.Length && char.IsLowSurrogate(s[index + 1]))
        { charCount = 2; return char.ConvertToUtf32(s[index], s[index + 1]); }
        charCount = 1;
        return s[index];
    }

    /// <summary>
    /// Build Twemoji CDN URL for a sequence of codepoints.
    /// Strips VS16 (U+FE0F), joins remaining codepoints as lowercase hex with dashes.
    /// </summary>
    private static string TwemojiUrl(int[] codepoints)
    {
        var sb = new StringBuilder();
        for (int j = 0; j < codepoints.Length; j++)
        {
            if (codepoints[j] == 0xFE0F) continue;
            if (sb.Length > 0) sb.Append('-');
            sb.Append(codepoints[j].ToString("x"));
        }
        return $"https://cdn.jsdelivr.net/gh/twitter/twemoji@14.0.2/assets/72x72/{sb}.png";
    }

    /// <summary>
    /// Get or download a Twemoji bitmap for the given codepoints.
    /// Returns cached Avalonia Bitmap, or downloads from CDN on first access. Null on failure.
    /// </summary>
    private object? GetOrDownloadEmojiBitmap(int[] codepoints)
    {
        var keySb = new StringBuilder();
        for (int j = 0; j < codepoints.Length; j++)
        {
            if (codepoints[j] == 0xFE0F) continue;
            if (keySb.Length > 0) keySb.Append('-');
            keySb.Append(codepoints[j].ToString("x"));
        }
        var key = keySb.ToString();

        lock (_emojiCacheLock)
        {
            if (_emojiCache.TryGetValue(key, out var cached))
                return cached;
        }

        // Download from Twemoji CDN
        object? bitmap = null;
        try
        {
            var url = TwemojiUrl(codepoints);
            var bytes = DownloadEmojiBytes(url);
            if (bytes != null && bytes.Length > 0)
            {
                using var ms = new MemoryStream(bytes);
                bitmap = _r.CreateBitmapFromStream(ms);
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Emoji download failed for {key}: {ex.Message}");
        }

        lock (_emojiCacheLock) { _emojiCache[key] = bitmap; }
        return bitmap;
    }

    /// <summary>
    /// Download emoji PNG bytes from Twemoji CDN via reflection-based HTTP.
    /// </summary>
    private static byte[]? DownloadEmojiBytes(string url)
    {
        try
        {
            AutoUpdater.EnsureHttpResolved();
            if (AutoUpdater.s_httpClient == null || AutoUpdater.s_sendAsync == null
                || AutoUpdater.s_httpRequestMessageType == null)
                return null;

            var httpAsm = AutoUpdater.s_httpRequestMessageType.Assembly;
            var httpMethodGet = httpAsm.GetType("System.Net.Http.HttpMethod")
                ?.GetProperty("Get", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
            if (httpMethodGet == null) return null;

            var request = Activator.CreateInstance(AutoUpdater.s_httpRequestMessageType, httpMethodGet, new Uri(url));
            if (request == null) return null;

            var sendParams = AutoUpdater.s_sendAsync.GetParameters();
            object?[] args = sendParams.Length == 1
                ? new[] { request }
                : new object?[] { request, CancellationToken.None };

            var task = AutoUpdater.s_sendAsync.Invoke(AutoUpdater.s_httpClient, args);
            var response = task?.GetType().GetProperty("Result")?.GetValue(task);
            if (response == null) return null;

            var statusCodeProp = response.GetType().GetProperty("StatusCode");
            var code = statusCodeProp != null ? Convert.ToInt32(statusCodeProp.GetValue(response)) : -1;
            if (code < 200 || code >= 300) return null;

            var content = response.GetType().GetProperty("Content")?.GetValue(response);
            if (content == null) return null;

            var readTask = content.GetType().GetMethod("ReadAsByteArrayAsync",
                BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)?.Invoke(content, null);
            return readTask?.GetType().GetProperty("Result")?.GetValue(readTask) as byte[];
        }
        catch
        {
            return null;
        }
    }

    // ===== Network =====

    private static string? FetchBio(string uuid)
    {
        var url = $"{ApiBase}/presence/{uuid}/bio";
        var json = HttpGetBio(url);
        if (json == null) return null;

        // Manual JSON parse: extract "bio" value
        return ExtractJsonStringField(json, "bio");
    }

    /// <summary>
    /// HTTP GET for bio data. Same pattern as PresenceBeacon.HttpGetPresence.
    /// </summary>
    private static string? HttpGetBio(string url)
    {
        try
        {
            AutoUpdater.EnsureHttpResolved();

            if (AutoUpdater.s_httpClient == null || AutoUpdater.s_sendAsync == null
                || AutoUpdater.s_httpRequestMessageType == null)
            {
                Logger.Log(Tag, "HttpGetBio: HTTP not resolved");
                return null;
            }

            var httpAsm = AutoUpdater.s_httpRequestMessageType.Assembly;
            var httpMethodType = httpAsm.GetType("System.Net.Http.HttpMethod");
            var httpMethodGet = httpMethodType?.GetProperty("Get",
                BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
            if (httpMethodGet == null) return null;

            var request = Activator.CreateInstance(AutoUpdater.s_httpRequestMessageType, httpMethodGet, new Uri(url));
            if (request == null) return null;

            try
            {
                var headers = request.GetType().GetProperty("Headers")?.GetValue(request);
                var tryAdd = headers?.GetType().GetMethod("TryAddWithoutValidation",
                    new[] { typeof(string), typeof(string) });
                tryAdd?.Invoke(headers, new object[] { "Accept", "application/json" });
            }
            catch { }

            var sendParams = AutoUpdater.s_sendAsync.GetParameters();
            object?[] args = sendParams.Length == 1
                ? new[] { request }
                : new object?[] { request, System.Threading.CancellationToken.None };

            var task = AutoUpdater.s_sendAsync.Invoke(AutoUpdater.s_httpClient, args);
            var response = task?.GetType().GetProperty("Result")?.GetValue(task);
            if (response == null) return null;

            var statusCodeProp = response.GetType().GetProperty("StatusCode");
            var statusCode = statusCodeProp != null ? Convert.ToInt32(statusCodeProp.GetValue(response)) : -1;

            // 404 = no bio set, return empty
            if (statusCode == 404) return "";

            if (statusCode >= 0 && (statusCode < 200 || statusCode >= 300))
            {
                Logger.Log(Tag, $"GET {url} returned HTTP {statusCode}");
                return null;
            }

            var content = response.GetType().GetProperty("Content")?.GetValue(response);
            if (content == null) return null;

            var readTask = content.GetType().GetMethod("ReadAsStringAsync",
                BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)?.Invoke(content, null);
            return readTask?.GetType().GetProperty("Result")?.GetValue(readTask) as string;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"HttpGetBio error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Send an HTTP request with a JSON body using a specific method (Post/Delete).
    /// </summary>
    private static int HttpSendWithBody(string url, string jsonBody, string methodName)
    {
        try
        {
            AutoUpdater.EnsureHttpResolved();
            UprootedPresenceBeacon.EnsurePostResolved();

            if (AutoUpdater.s_httpClient == null || AutoUpdater.s_sendAsync == null
                || AutoUpdater.s_httpRequestMessageType == null)
            {
                Logger.Log(Tag, $"HttpSend({methodName}): HTTP not resolved");
                return -1;
            }

            var httpAsm = AutoUpdater.s_httpRequestMessageType.Assembly;
            var httpMethodType = httpAsm.GetType("System.Net.Http.HttpMethod");
            var httpMethod = httpMethodType?.GetProperty(methodName,
                BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
            if (httpMethod == null)
            {
                Logger.Log(Tag, $"HttpSend: HttpMethod.{methodName} not found");
                return -1;
            }

            var request = Activator.CreateInstance(AutoUpdater.s_httpRequestMessageType, httpMethod, new Uri(url));
            if (request == null) return -1;

            // Create StringContent body
            object? bodyContent = null;
            var stringContentType = httpAsm.GetType("System.Net.Http.StringContent");
            if (stringContentType != null)
            {
                try { bodyContent = Activator.CreateInstance(stringContentType, jsonBody, Encoding.UTF8, "application/json"); }
                catch { }
            }

            if (bodyContent == null)
            {
                var byteArrayContentType = httpAsm.GetType("System.Net.Http.ByteArrayContent");
                if (byteArrayContentType != null)
                {
                    var bytes = Encoding.UTF8.GetBytes(jsonBody);
                    bodyContent = Activator.CreateInstance(byteArrayContentType, bytes);
                    if (bodyContent != null)
                    {
                        var contentHeaders = bodyContent.GetType().GetProperty("Headers")?.GetValue(bodyContent);
                        var contentTypeProp = contentHeaders?.GetType().GetProperty("ContentType");
                        if (contentTypeProp != null)
                        {
                            var mediaHeaderType = httpAsm.GetType("System.Net.Http.Headers.MediaTypeHeaderValue");
                            if (mediaHeaderType != null)
                            {
                                var mediaHeader = Activator.CreateInstance(mediaHeaderType, "application/json");
                                contentTypeProp.SetValue(contentHeaders, mediaHeader);
                            }
                        }
                    }
                }
            }

            if (bodyContent == null)
            {
                Logger.Log(Tag, "HttpSend: cannot create body content");
                return -1;
            }

            request.GetType().GetProperty("Content")?.SetValue(request, bodyContent);

            var sendParams = AutoUpdater.s_sendAsync.GetParameters();
            object?[] args = sendParams.Length == 1
                ? new[] { request }
                : new object?[] { request, System.Threading.CancellationToken.None };

            var task = AutoUpdater.s_sendAsync.Invoke(AutoUpdater.s_httpClient, args);
            var response = task?.GetType().GetProperty("Result")?.GetValue(task);
            if (response == null) return -1;

            var statusCodeProp = response.GetType().GetProperty("StatusCode");
            return statusCodeProp != null ? Convert.ToInt32(statusCodeProp.GetValue(response)) : -1;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"HttpSend({methodName}) error: {ex.Message}");
            return -1;
        }
    }

    // ===== UI helpers =====

    /// <summary>
    /// Set TextAlignment.Center on a TextBlock via reflection.
    /// </summary>
    private static void CenterTextAlignment(object textBlock)
    {
        try
        {
            var textAlignType = textBlock.GetType().Assembly.GetType("Avalonia.Media.TextAlignment");
            if (textAlignType != null)
            {
                var centerVal = Enum.Parse(textAlignType, "Center");
                textBlock.GetType().GetProperty("TextAlignment")?.SetValue(textBlock, centerVal);
            }
        }
        catch { }
    }

    // ===== JSON helpers (no System.Text.Json) =====

    /// <summary>
    /// Extract a string field value from a JSON object.
    /// Handles escaped quotes within the value.
    /// </summary>
    private static string? ExtractJsonStringField(string json, string fieldName)
    {
        var key = $"\"{fieldName}\"";
        var keyIdx = json.IndexOf(key, StringComparison.Ordinal);
        if (keyIdx < 0) return null;

        var colonIdx = json.IndexOf(':', keyIdx + key.Length);
        if (colonIdx < 0) return null;

        var openQuote = json.IndexOf('"', colonIdx + 1);
        if (openQuote < 0) return null;

        var sb = new StringBuilder();
        for (int i = openQuote + 1; i < json.Length; i++)
        {
            if (json[i] == '\\' && i + 1 < json.Length)
            {
                char next = json[i + 1];
                if (next == '"') { sb.Append('"'); i++; }
                else if (next == '\\') { sb.Append('\\'); i++; }
                else if (next == 'n') { sb.Append('\n'); i++; }
                else if (next == 'r') { sb.Append('\r'); i++; }
                else if (next == 't') { sb.Append('\t'); i++; }
                else { sb.Append(json[i]); }
            }
            else if (json[i] == '"')
            {
                break;
            }
            else
            {
                sb.Append(json[i]);
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Escape a string for inclusion in a JSON value (between quotes).
    /// </summary>
    private static string EscapeJsonString(string s)
    {
        var sb = new StringBuilder(s.Length + 8);
        foreach (char c in s)
        {
            switch (c)
            {
                case '"': sb.Append("\\\""); break;
                case '\\': sb.Append("\\\\"); break;
                case '\n': sb.Append("\\n"); break;
                case '\r': sb.Append("\\r"); break;
                case '\t': sb.Append("\\t"); break;
                default:
                    if (c < 0x20)
                        sb.Append($"\\u{(int)c:X4}");
                    else
                        sb.Append(c);
                    break;
            }
        }
        return sb.ToString();
    }
}
