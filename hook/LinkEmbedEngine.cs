using System.Reflection;
using System.Text.RegularExpressions;

namespace Uprooted;

/// <summary>
/// Avalonia-native link embed unfurling engine.
/// Scans the chat visual tree for TextBlocks containing URLs, fetches OpenGraph metadata
/// via HttpClient, and injects Discord-style embed cards as native Avalonia controls.
///
/// Replaces the dead-end DotNetBrowser JS injection approach (LinkEmbedInjector.cs).
/// Root's chat UI is rendered entirely in Avalonia controls — DotNetBrowser's iframe is
/// permanently at about:blank and never renders chat content.
/// </summary>
internal class LinkEmbedEngine : IDisposable
{
    private const int ScanIntervalMs = 500;
    private const int MaxUrlsPerScan = 20;
    private const int HttpTimeoutSeconds = 10;
    private const int MaxHtmlBytes = 50 * 1024; // 50KB HTML read limit
    private const double MaxCardWidth = 400.0;
    private const int MaxImageBytes = 5 * 1024 * 1024;   // 5MB
    private const double MaxImageHeight = 200.0;
    private const double MaxImageWidth = 380.0;
    private const string DefaultAccentColor = "#5865F2"; // Discord-like blue
    private static readonly bool Verbose = Environment.GetEnvironmentVariable("UPROOTED_VERBOSE") == "1";

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;

    private Timer? _scanTimer;
    private int _scanning; // Interlocked reentrancy guard
    private readonly TailSampler _sampler = new(heartbeatTicks: 60, slowThresholdMs: 50);

    // Chat area discovery — no caching, always scan from mainWindow on each tick
    // to handle room navigation instantly

    // URL tracking — metadata cache avoids re-fetching, injectedCards tracks live cards
    // Per-node dedup is via "uprooted-embed-scanned" tag on each CTextBlock
    // Image byte cache survives VirtualizingStackPanel recycling for instant re-injection
    // All three caches are size-capped to prevent unbounded memory growth in long sessions.
    private const int MaxMetadataCache = 200;
    private const int MaxImageBytesCache = 50;  // each entry can be up to 5MB
    private const int MaxInjectedCards = 200;
    private readonly Dictionary<string, EmbedData?> _metadataCache = new();
    private readonly Dictionary<string, byte[]> _imageBytesCache = new();
    private readonly List<object> _injectedCards = new();
    private readonly Dictionary<string, Action> _animatedDisposables = new(); // dispose timers on card removal

    // Video thumbnail extraction via DotNetBrowser Chromium (JS → <video> → <canvas> → base64)
    private static readonly SemaphoreSlim s_videoThumbSemaphore = new(1, 1);
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, byte> s_videoThumbFailed = new();
    private const int VideoThumbTimeoutMs = 12_000;
    private const int VideoThumbPollMs = 200;
    private const string VideoThumbPrefix = "UPROOTED_THUMB:";
    private const string VideoThumbError = "UPROOTED_THUMB_ERR";

    // HTTP via reflection — Root's single-file trimming removes HttpClient methods at JIT time.
    // Using reflection avoids MissingMethodException during JIT compilation.
    private static object? s_httpClient;
    private static MethodInfo? s_getStringAsync;     // HttpClient.GetStringAsync(string)
    private static MethodInfo? s_getByteArrayAsync;  // HttpClient.GetByteArrayAsync(string)
    private static MethodInfo? s_getAsync;           // HttpClient.GetAsync(string) — fallback for bytes
    private static MethodInfo? s_sendAsync;          // HttpClient.SendAsync — core method
    private static MethodInfo? s_sendAsyncHeadersRead; // SendAsync(HttpRequestMessage, HttpCompletionOption) — for streaming
    private static object? s_httpCompletionOptionHeadersRead; // HttpCompletionOption.ResponseHeadersRead = 1
    private static Type? s_httpRequestMessageType;   // System.Net.Http.HttpRequestMessage
    private static object? s_httpMethodGet;          // HttpMethod.Get
    private static bool s_httpResolved;

    // ===== OG metadata record =====

    private record EmbedData(
        string Url,
        string? Provider,
        string? Author,
        string? Title,
        string? Description,
        string? Image,
        string? Color,
        string? VideoId);

    // ===== Regex patterns =====

    private static readonly Regex UrlRegex = new(
        @"https?://[^\s<>""')\]]+",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // OG meta tags: <meta property="og:title" content="...">
    // Bridge pattern explicitly matches complete HTML attributes (key="value" or key='value')
    // to handle tags with extra attributes like itemProp="image" between property and content.
    private const string AttrBridge = @"(?:\s+[a-zA-Z_][\w.:-]*\s*=\s*(?:""[^""]*""|'[^']*'))*";
    private static readonly Regex OgRegex = new(
        @"<meta\s+(?:\s*[a-zA-Z_][\w.:-]*\s*=\s*(?:""[^""]*""|'[^']*'))*\s*(?:property|name)\s*=\s*[""']([^""']+)[""']" + AttrBridge + @"\s+content\s*=\s*[""']([^""']*)[""'][^>]*/?>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Reverse order: <meta content="..." property="og:title">
    private static readonly Regex OgRegexReverse = new(
        @"<meta\s+(?:\s*[a-zA-Z_][\w.:-]*\s*=\s*(?:""[^""]*""|'[^']*'))*\s*content\s*=\s*[""']([^""']*)[""']" + AttrBridge + @"\s+(?:property|name)\s*=\s*[""']([^""']+)[""'][^>]*/?>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Fallback <title> tag
    private static readonly Regex TitleRegex = new(
        @"<title[^>]*>([^<]+)</title>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // YouTube video ID extraction
    private static readonly Regex YoutubeIdRegex = new(
        @"(?:youtube\.com/watch\?.*?v=|youtu\.be/|youtube\.com/embed/|youtube\.com/shorts/)([a-zA-Z0-9_-]{11})",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Direct image URL detection (fast path — zero network)
    private static readonly Regex ImageUrlRegex = new(
        @"\.(?:jpe?g|png|gif|webp|bmp|svg)(?:\?[^\s]*)?$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Direct video URL detection (play button placeholder + open in browser)
    private static readonly Regex VideoUrlRegex = new(
        @"\.(?:mp4|webm|mov)(?:\?[^\s]*)?$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // JSON "title":"value" for oEmbed parsing (no System.Text.Json)
    private static readonly Regex JsonTitleRegex = new(
        @"""title""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    // JSON "author_name":"value" for oEmbed author parsing
    private static readonly Regex JsonAuthorRegex = new(
        @"""author_name""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    // Additional JSON regex patterns for oEmbed fields
    private static readonly Regex JsonThumbnailUrlRegex = new(
        @"""thumbnail_url""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    private static readonly Regex JsonProviderNameRegex = new(
        @"""provider_name""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    private static readonly Regex JsonDescriptionRegex = new(
        @"""description""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    // oEmbed "type" field — "photo" type uses "url" for the image, others use "thumbnail_url"
    private static readonly Regex JsonTypeRegex = new(
        @"""type""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    // oEmbed "url" field — the primary resource URL (image URL for "photo" type)
    private static readonly Regex JsonUrlRegex = new(
        @"""url""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    // JSON "html":"value" — some oEmbed providers (Twitter) return content in html field
    private static readonly Regex JsonHtmlRegex = new(
        @"""html""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    // Extract text from <p> tags in oEmbed HTML (Twitter embeds tweet text in <p>)
    private static readonly Regex HtmlParagraphRegex = new(
        @"<p[^>]*>(.*?)</p>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    // Strip HTML tags for plain text extraction
    private static readonly Regex HtmlTagRegex = new(
        @"<[^>]+>",
        RegexOptions.Compiled);

    // oEmbed discovery: <link rel="alternate" type="application/json+oembed" href="...">
    // Standard mechanism — any site that supports oEmbed advertises the endpoint in its HTML.
    private static readonly Regex OEmbedDiscoveryRegex = new(
        @"<link\s+[^>]*?type\s*=\s*[""']application/json\+oembed[""'][^>]*?href\s*=\s*[""']([^""']+)[""'][^>]*/?>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Reverse attribute order: href before type
    private static readonly Regex OEmbedDiscoveryReverseRegex = new(
        @"<link\s+[^>]*?href\s*=\s*[""']([^""']+)[""'][^>]*?type\s*=\s*[""']application/json\+oembed[""'][^>]*/?>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Embed-fixer domains that serve OG only to bots
    private static readonly Regex EmbedFixerDomainRegex = new(
        @"^https?://(?:vxtwitter\.com|fixvx\.com|fxtwitter\.com|fixupx\.com|twittpr\.com)/",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Twitter/X domains — also serve OG only to bots
    private static readonly Regex TwitterDomainRegex = new(
        @"^https?://(?:twitter\.com|x\.com)/",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Reddit domains — dedicated handler for subreddit labels + orange accent
    private static readonly Regex RedditDomainRegex = new(
        @"^https?://(?:www\.|old\.|np\.|new\.)?reddit\.com/",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Extract subreddit name from /r/<name> path segment
    private static readonly Regex RedditSubredditRegex = new(
        @"/r/([a-zA-Z0-9_]+)",
        RegexOptions.Compiled);

    private const string RedditAccentColor = "#FF4500"; // Reddit orange

    // Domains where Root renders embeds natively — skip to avoid double-embedding
    // Note: only media.tenor.com is skipped (direct GIF CDN); tenor.com/view/ pages
    // go through our OG pipeline to extract and render the animated GIF inline.
    private static readonly Regex NativeEmbedDomainRegex = new(
        @"^https?://(?:media\.tenor\.com|rootapp\.gg)/",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    internal static LinkEmbedEngine? Instance { get; set; }

    private readonly ThemeEngine? _themeEngine;

    internal LinkEmbedEngine(AvaloniaReflection resolver, object mainWindow, ThemeEngine? themeEngine = null)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _mainWindow = mainWindow;
        _themeEngine = themeEngine;
    }

    /// <summary>
    /// Update accent strip and background of all live embed cards to reflect a theme change.
    /// Must be called on the UI thread (e.g. from ContentPages.ApplyThemedColors or UpdateLiveColors).
    /// Site-specific colors (OG theme-color) are preserved; only cards using the default accent update.
    /// </summary>
    internal void NotifyThemeChanged()
    {
        var themeAccent = _themeEngine?.GetAccentColor() ?? ContentPages.AccentGreen;
        var newCardBg = ContentPages.CardBg;
        foreach (var card in _injectedCards)
        {
            try
            {
                _r.SetBackground(card, newCardBg);
                var embedTag = _r.GetTag(card) as string;
                const string prefix = "uprooted-embed-";
                if (embedTag?.StartsWith(prefix) == true &&
                    _metadataCache.TryGetValue(embedTag.Substring(prefix.Length), out var cached) &&
                    cached?.Color != null)
                {
                    // Site has its own brand color — keep it
                    SetBorderBrush(card, cached.Color);
                }
                else
                {
                    SetBorderBrush(card, themeAccent);
                }
            }
            catch { }
        }
    }

    internal void Initialize()
    {
        using var ev = WideEvent.Begin("LinkEmbed", "initialize");

        // Pre-warm HttpClient reflection so first fetch doesn't pay resolution cost
        EnsureHttpResolved();
        ev.Set("http_resolved", s_httpClient != null);

        // Start polling timer
        _scanTimer = new Timer(OnScanTick, null, 0, ScanIntervalMs);
        ev.Set("scan_interval_ms", ScanIntervalMs);
    }

    public void Dispose()
    {
        var t = _scanTimer;
        _scanTimer = null;
        t?.Dispose();
    }

    // ===== Timer callback =====

    private void OnScanTick(object? state)
    {
        // Reentrancy guard (same pattern as SidebarInjector)
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        var ev = WideEvent.BeginSampled("LinkEmbed", "scan_tick", _sampler);
        try
        {
            // Check settings — stop if plugin disabled
            var settings = UprootedSettings.Load();
            if (settings.Plugins.TryGetValue("link-embeds", out var enabled) && !enabled)
            {
                ev.Set("skipped", "disabled");
                ev.Dispose();
                Interlocked.Exchange(ref _scanning, 0);
                return;
            }

            _r.RunOnUIThread(() =>
            {
                try
                {
                    ScanForUrls(ev);
                }
                catch (Exception ex)
                {
                    ev.SetError(ex);
                }
                finally
                {
                    ev.Dispose();
                    Interlocked.Exchange(ref _scanning, 0);
                }
            });

            // Fallback release in case UI thread doesn't run
            Task.Delay(ScanIntervalMs * 2).ContinueWith(_ =>
            {
                Interlocked.CompareExchange(ref _scanning, 0, 1);
            });
        }
        catch (Exception ex)
        {
            ev.SetError(ex);
            ev.Dispose();
            Interlocked.Exchange(ref _scanning, 0);
        }
    }

    // ===== Component 1: Chat Area Discovery =====

    /// <summary>
    /// Always scan from mainWindow — no caching.
    /// Room navigation in Root replaces the visual tree, so a cached container goes stale instantly.
    /// Full tree walk is &lt;10ms for ~2000 nodes, so 2Hz polling is fine.
    /// </summary>
    private object DiscoverChatArea() => _mainWindow;

    // ===== Component 2: URL Scanner =====

    // Root renders message bodies via CTextBlock (RootApp.Client.Avalonia.Markdown.Components.CTextBlock)
    // which extends Control (not TextBlock). URL text is in the Text property.
    // Message structure: Grid > ... > RootMarkdownTextBlock > ... > CTextBlock
    private static readonly HashSet<string> s_messageTextTypes = new(StringComparer.Ordinal)
    {
        "CTextBlock",
        "RootMarkdownTextBlock"
    };

    private void ScanForUrls(WideEvent ev)
    {
        var container = DiscoverChatArea();
        if (container == null) return;

        int queued = 0;

        foreach (var node in _walker.DescendantsDepthFirst(container))
        {
            if (queued >= MaxUrlsPerScan) break;

            var typeName = node.GetType().Name;

            // Skip controls that aren't message text renderers
            if (!s_messageTextTypes.Contains(typeName)) continue;

            // Dedup via Tag on the control (CTextBlock extends Control, has Tag property)
            string? tag = null;
            try { tag = node.GetType().GetProperty("Tag")?.GetValue(node) as string; }
            catch { }
            if (tag == "uprooted-embed-scanned") continue;

            // Read Text property
            string? text = null;
            try
            {
                text = node.GetType().GetProperty("Text",
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(node) as string;
            }
            catch { }
            if (text == null) continue;

            var matches = UrlRegex.Matches(text);
            if (matches.Count == 0) continue;

            // Tag as scanned
            try { node.GetType().GetProperty("Tag")?.SetValue(node, "uprooted-embed-scanned"); }
            catch { }

            foreach (Match m in matches)
            {
                if (queued >= MaxUrlsPerScan) break;

                var url = m.Value;
                url = url.TrimEnd('.', ',', ';', ':', '!', '?');

                queued++;
                var capturedNode = node;
                ThreadPool.QueueUserWorkItem(_ => FetchAndInject(url, capturedNode));
            }
        }

        ev.Set("urls_queued", queued);
        if (queued > 0) ev.MarkNotable();
    }

    // ===== Component 3: OG Metadata Fetcher =====

    private void FetchAndInject(string url, object textBlock)
    {
        using var ev = WideEvent.Begin("LinkEmbed", "fetch_and_inject");
        ev.Set("url", url);
        try
        {
            // Skip domains where Root renders embeds natively (avoids double-embedding)
            if (NativeEmbedDomainRegex.IsMatch(url))
            {
                ev.Set("skipped", "native_embed_domain");
                return;
            }

            var data = FetchMetadata(url);

            // Fallback: if no metadata or no title, show minimal domain-only card
            if (data == null || string.IsNullOrEmpty(data.Title))
            {
                try
                {
                    var host = new Uri(url).Host;
                    data = new EmbedData(url, null, null, host, null, null, null, null);
                    ev.Set("fallback", "domain_only");
                }
                catch { return; }
            }

            ev.Set("title", data.Title);
            ev.Set("has_image", data.Image != null);
            ev.Set("has_video", data.VideoId != null);

            // Fast path: if we already have cached image bytes (re-injection after
            // VirtualizingStackPanel recycle), build the full card with image in one shot
            if (_imageBytesCache.TryGetValue(url, out var cachedBytes))
            {
                ev.Set("image_cache_hit", true);
                InjectEmbedWithCachedImage(textBlock, data, cachedBytes);
                return;
            }

            // First time: Phase A — inject text-only card immediately
            InjectEmbed(textBlock, data, imageBytes: null);

            // Phase B: Fetch image asynchronously, then update existing card
            if (!string.IsNullOrEmpty(data.Image))
            {
                var imageUrl = ResolveImageUrl(data.Image!, url);
                if (imageUrl != null)
                {
                    ev.Set("phase_b", "image");
                    var capturedData = data;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        using var imgEv = WideEvent.Begin("LinkEmbed", "phase_b_image");
                        imgEv.Set("url", url);
                        imgEv.Set("image_url", imageUrl);
                        try
                        {
                            var imgBytes = HttpGetBytes(imageUrl);

                            // OG fallback: if image fast-path URL served HTML instead of image bytes,
                            // fetch the page as HTML, parse OG tags, and try the real og:image URL.
                            // Common for Zipline /view/ and /u/ paths that have .png extension but serve viewer pages.
                            if (imgBytes == null && capturedData.Image == capturedData.Url)
                            {
                                imgEv.Set("og_fallback", true);
                                var ogData = FetchOgMetadata(capturedData.Url);
                                if (ogData?.Image != null && ogData.Image != capturedData.Url)
                                {
                                    var realImageUrl = ResolveImageUrl(ogData.Image, capturedData.Url);
                                    if (realImageUrl != null)
                                    {
                                        imgBytes = HttpGetBytes(realImageUrl);
                                        if (imgBytes != null)
                                            imgEv.Set("og_fallback_image", realImageUrl);
                                    }
                                }
                            }

                            if (imgBytes == null)
                            {
                                imgEv.Set("result", "no_bytes");
                                return;
                            }
                            if (imgBytes.Length > MaxImageBytes || imgBytes.Length < 100)
                            {
                                imgEv.Set("result", "size_rejected");
                                imgEv.Set("image_size", imgBytes.Length);
                                return;
                            }
                            // Cache image bytes for future re-injections (evict if full)
                            if (_imageBytesCache.Count >= MaxImageBytesCache) _imageBytesCache.Clear();
                            _imageBytesCache[capturedData.Url] = imgBytes;
                            AddImageToExistingCard(capturedData, imgBytes);
                            imgEv.Set("result", "injected");
                            imgEv.Set("image_size", imgBytes.Length);
                        }
                        catch (Exception ex)
                        {
                            imgEv.SetError(ex);
                        }
                    });
                }
            }

            // Phase B for video URLs: extract first-frame thumbnail via DotNetBrowser Chromium
            if (data.VideoId != null && string.IsNullOrEmpty(data.Image))
            {
                ev.Set("phase_b", "video_thumb");
                var capturedData = data;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    using var vidEv = WideEvent.Begin("LinkEmbed", "phase_b_video_thumb");
                    vidEv.Set("url", url);
                    try
                    {
                        var thumbBytes = ExtractVideoThumbnail(capturedData.Url);
                        if (thumbBytes != null && thumbBytes.Length >= 100)
                        {
                            if (_imageBytesCache.Count >= MaxImageBytesCache) _imageBytesCache.Clear();
                            _imageBytesCache[capturedData.Url] = thumbBytes;
                            ReplaceVideoPlaceholderWithThumbnail(capturedData, thumbBytes);
                            vidEv.Set("result", "injected");
                            vidEv.Set("thumb_size", thumbBytes.Length);
                        }
                        else
                        {
                            vidEv.Set("result", "no_thumb");
                        }
                    }
                    catch (Exception ex)
                    {
                        vidEv.SetError(ex);
                    }
                });
            }
        }
        catch (Exception ex)
        {
            ev.SetError(ex);
        }
    }

    /// <summary>
    /// Fast re-injection path: build full card with cached image bytes in one UI thread dispatch.
    /// Used when VirtualizingStackPanel recycles and we need to re-create the embed.
    /// </summary>
    private void InjectEmbedWithCachedImage(object textControl, EmbedData data, byte[] imageBytes)
    {
        _r.RunOnUIThread(() =>
        {
            try
            {
                var (grid, col, colSpan) = FindInjectionGrid(textControl);
                if (grid == null) return;

                var embedTag = "uprooted-link-embed:" + data.Url;
                var children = _r.GetChildren(grid);
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        if (child != null && _r.GetTag(child) == embedTag) return;
                    }
                }

                // Dispose any existing animated timer for this URL before re-creating
                if (_animatedDisposables.TryGetValue(data.Url, out var oldDispose))
                {
                    oldDispose();
                    _animatedDisposables.Remove(data.Url);
                }

                var card = BuildEmbedCard(data, bitmap: null, imageBytes: imageBytes);
                if (card == null) return;

                _r.SetTag(card, embedTag);
                _r.SetMargin(card, 0, 4, 0, 4);

                int newRow = AddAutoRowToGrid(grid);
                _r.SetGridRow(card, newRow);
                _r.SetGridColumn(card, col);
                SetGridColumnSpan(card, colSpan);

                _r.AddChild(grid, card);
                if (_injectedCards.Count >= MaxInjectedCards) _injectedCards.RemoveRange(0, _injectedCards.Count / 2);
                _injectedCards.Add(card);
            }
            catch (Exception ex)
            {
                Logger.Log("LinkEmbed", $"Cached inject error: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Phase B: Add image into an already-injected text-only embed card.
    /// Finds the card by its tag, creates bitmap on UI thread, appends Image control.
    /// </summary>
    private void AddImageToExistingCard(EmbedData data, byte[] imageBytes)
    {
        _r.RunOnUIThread(() =>
        {
            try
            {
                var embedTag = "uprooted-link-embed:" + data.Url;

                // Find the existing card in our injected list
                object? card = null;
                foreach (var c in _injectedCards)
                {
                    if (_r.GetTag(c) == embedTag)
                    {
                        card = c;
                        break;
                    }
                }
                if (card == null) return;

                // Get the StackPanel (child of the Border card)
                var content = _r.GetBorderChild(card);
                if (content == null) return;

                object? imageElement = null;

                // Try animated path first
                if (data.VideoId == null && AnimatedImage.IsAnimated(imageBytes))
                {
                    var frames = AnimatedImage.DecodeFrames(imageBytes, _r);
                    if (frames != null)
                    {
                        var animated = AnimatedImage.CreateAnimatedControl(frames, _r);
                        if (animated != null)
                        {
                            var img = animated.Value.control;
                            _r.SetMaxHeight(img, MaxImageHeight);
                            _r.SetMaxWidth(img, MaxImageWidth);
                            _r.SetHorizontalAlignment(img, "Left");

                            var imgBorder = _r.CreateBorder(null, cornerRadius: 6);
                            if (imgBorder != null)
                            {
                                _r.SetClipToBounds(imgBorder, true);
                                _r.SetHorizontalAlignment(imgBorder, "Left");
                                _r.SetBorderChild(imgBorder, img);
                                imageElement = imgBorder;
                            }
                            else
                            {
                                imageElement = img;
                            }

                            _animatedDisposables[data.Url] = animated.Value.dispose;
                        }
                    }
                }

                // Fallback: static bitmap
                if (imageElement == null)
                {
                    object? bitmap;
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        bitmap = _r.CreateBitmapFromStream(ms);
                    }
                    if (bitmap == null) return;

                    if (data.VideoId != null)
                    {
                        imageElement = BuildThumbnailWithPlayButton(bitmap, data.Url);
                    }
                    else
                    {
                        var img = _r.CreateImage("Uniform");
                        if (img == null) return;
                        _r.SetImageSource(img, bitmap);
                        _r.SetMaxHeight(img, MaxImageHeight);
                        _r.SetMaxWidth(img, MaxImageWidth);
                        _r.SetHorizontalAlignment(img, "Left");

                        var imgBorder = _r.CreateBorder(null, cornerRadius: 6);
                        if (imgBorder != null)
                        {
                            _r.SetClipToBounds(imgBorder, true);
                            _r.SetHorizontalAlignment(imgBorder, "Left");
                            _r.SetBorderChild(imgBorder, img);
                            imageElement = imgBorder;
                        }
                        else
                        {
                            imageElement = img;
                        }
                    }
                }

                if (imageElement != null)
                {
                    _r.AddChild(content, imageElement);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("LinkEmbed", $"AddImageToExistingCard error: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Extract the first frame of a video as JPEG bytes using DotNetBrowser's Chromium.
    /// Injects a hidden &lt;video&gt; element, waits for loadeddata, draws to &lt;canvas&gt;,
    /// then writes the base64 data URL to document.title for retrieval via IBrowser.Title.
    /// Serialized via semaphore since document.title is a global channel.
    /// </summary>
    private static byte[]? ExtractVideoThumbnail(string videoUrl)
    {
        using var ev = WideEvent.Begin("LinkEmbed", "extract_video_thumb");
        ev.Set("url", videoUrl);

        // Skip known failures
        if (s_videoThumbFailed.ContainsKey(videoUrl))
        {
            ev.Set("result", "known_failure");
            return null;
        }

        // Wait for DotNetBrowser shared references to become available
        if (!DotNetBrowserReflection.IsReady)
            ev.Set("waiting_for_browser", true);
        var waitStart = Environment.TickCount64;
        while (!DotNetBrowserReflection.IsReady)
        {
            if (Environment.TickCount64 - waitStart > 30_000)
            {
                ev.Set("result", "browser_timeout");
                s_videoThumbFailed[videoUrl] = 1;
                return null;
            }
            Thread.Sleep(500);
        }

        var instance = DotNetBrowserReflection.SharedInstance;
        var browser = DotNetBrowserReflection.SharedBrowser;
        var frame = DotNetBrowserReflection.SharedFrame;
        if (instance == null || browser == null || frame == null)
        {
            ev.SetError("shared references null despite IsReady");
            s_videoThumbFailed[videoUrl] = 1;
            return null;
        }

        // Serialize — document.title is a single global channel
        if (!s_videoThumbSemaphore.Wait(VideoThumbTimeoutMs))
        {
            ev.Set("result", "semaphore_timeout");
            return null;
        }

        string? originalTitle = null;
        try
        {
            originalTitle = instance.GetBrowserTitle(browser);

            // JS: create hidden video, on loadeddata draw first frame to canvas, write base64 to document.title
            var js = $@"(function(){{
  try {{
    var v = document.createElement('video');
    v.crossOrigin = 'anonymous';
    v.muted = true;
    v.style.display = 'none';
    v.addEventListener('loadeddata', function() {{
      try {{
        var c = document.createElement('canvas');
        c.width = v.videoWidth;
        c.height = v.videoHeight;
        c.getContext('2d').drawImage(v, 0, 0);
        document.title = '{VideoThumbPrefix}' + c.toDataURL('image/jpeg', 0.7);
      }} catch(e) {{
        document.title = '{VideoThumbError}';
      }} finally {{
        v.remove();
      }}
    }});
    v.addEventListener('error', function() {{
      document.title = '{VideoThumbError}';
      v.remove();
    }});
    v.src = '{EscapeJsString(videoUrl)}';
    document.body.appendChild(v);
  }} catch(e) {{
    document.title = '{VideoThumbError}';
  }}
}})();";

            if (!instance.ExecuteJavaScript(frame, js))
            {
                ev.Set("result", "js_execution_failed");
                s_videoThumbFailed[videoUrl] = 1;
                return null;
            }

            // Poll IBrowser.Title for result
            var pollStart = Environment.TickCount64;
            while (Environment.TickCount64 - pollStart < VideoThumbTimeoutMs)
            {
                Thread.Sleep(VideoThumbPollMs);
                var title = instance.GetBrowserTitle(browser);
                if (title == null) continue;

                if (title == VideoThumbError)
                {
                    ev.Set("result", "js_error");
                    s_videoThumbFailed[videoUrl] = 1;
                    return null;
                }

                if (title.StartsWith(VideoThumbPrefix, StringComparison.Ordinal))
                {
                    var dataUrl = title.Substring(VideoThumbPrefix.Length);
                    // Strip data URL header: "data:image/jpeg;base64,"
                    var commaIdx = dataUrl.IndexOf(',');
                    if (commaIdx < 0)
                    {
                        ev.Set("result", "malformed_data_url");
                        s_videoThumbFailed[videoUrl] = 1;
                        return null;
                    }
                    var base64 = dataUrl.Substring(commaIdx + 1);
                    try
                    {
                        var bytes = Convert.FromBase64String(base64);
                        ev.Set("result", "success");
                        ev.Set("thumb_bytes", bytes.Length);
                        return bytes;
                    }
                    catch (Exception ex)
                    {
                        ev.SetError(ex);
                        ev.Set("result", "base64_decode_failed");
                        s_videoThumbFailed[videoUrl] = 1;
                        return null;
                    }
                }
            }

            // Timeout
            ev.Set("result", "poll_timeout");
            ev.Set("timeout_ms", VideoThumbTimeoutMs);
            s_videoThumbFailed[videoUrl] = 1;
            return null;
        }
        catch (Exception ex)
        {
            ev.SetError(ex);
            s_videoThumbFailed[videoUrl] = 1;
            return null;
        }
        finally
        {
            // Restore original title
            if (originalTitle != null && instance != null && frame != null)
            {
                try
                {
                    instance.ExecuteJavaScript(frame,
                        $"document.title = '{EscapeJsString(originalTitle)}';");
                }
                catch { }
            }
            s_videoThumbSemaphore.Release();
        }
    }

    /// <summary>
    /// Escape a string for use inside a JS single-quoted string literal.
    /// </summary>
    private static string EscapeJsString(string s)
    {
        return s.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "\\r");
    }

    /// <summary>
    /// Replace the dark video placeholder in an existing card with a real thumbnail + play button.
    /// Called on a background thread after ExtractVideoThumbnail succeeds.
    /// </summary>
    private void ReplaceVideoPlaceholderWithThumbnail(EmbedData data, byte[] thumbBytes)
    {
        _r.RunOnUIThread(() =>
        {
            try
            {
                var embedTag = "uprooted-link-embed:" + data.Url;

                // Find the existing card
                object? card = null;
                foreach (var c in _injectedCards)
                {
                    if (_r.GetTag(c) == embedTag)
                    {
                        card = c;
                        break;
                    }
                }
                if (card == null) return;

                // Get the StackPanel content
                var content = _r.GetBorderChild(card);
                if (content == null) return;

                var children = _r.GetChildren(content);
                if (children == null || children.Count == 0) return;

                // The last child should be the dark placeholder — remove it
                var lastChild = children[children.Count - 1];
                if (lastChild != null)
                    _r.RemoveChild(content, lastChild);

                // Create bitmap from thumbnail bytes
                object? bitmap;
                using (var ms = new System.IO.MemoryStream(thumbBytes))
                {
                    bitmap = _r.CreateBitmapFromStream(ms);
                }
                if (bitmap == null) return;

                // Build thumbnail with play button overlay
                var imageElement = BuildThumbnailWithPlayButton(bitmap, data.Url);
                if (imageElement != null)
                {
                    _r.AddChild(content, imageElement);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("LinkEmbed", $"ReplaceVideoPlaceholder error: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Toggle visibility of image-only embed titles on all live cards.
    /// Called when the LinkEmbedsShowFilenames setting changes.
    /// </summary>
    internal void RefreshTitleVisibility()
    {
        _r.RunOnUIThread(() =>
        {
            try
            {
                bool show = UprootedSettings.Load().LinkEmbedsShowFilenames;
                foreach (var card in _injectedCards)
                {
                    var content = _r.GetBorderChild(card);
                    if (content == null) continue;
                    var children = _r.GetChildren(content);
                    if (children == null) continue;
                    foreach (var child in children)
                    {
                        if (child != null && _r.GetTag(child) == "uprooted-embed-img-title")
                            _r.SetIsVisible(child, show);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("LinkEmbed", $"RefreshTitleVisibility error: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Resolve relative image URLs against the page URL.
    /// Handles protocol-relative (//), root-relative (/path), and absolute URLs.
    /// </summary>
    private static string? ResolveImageUrl(string imageUrl, string pageUrl)
    {
        if (imageUrl.StartsWith("http://") || imageUrl.StartsWith("https://"))
            return imageUrl;

        try
        {
            var baseUri = new Uri(pageUrl);
            if (imageUrl.StartsWith("//"))
                return baseUri.Scheme + ":" + imageUrl;
            if (imageUrl.StartsWith("/"))
                return baseUri.Scheme + "://" + baseUri.Host + imageUrl;
            // Relative path
            return new Uri(baseUri, imageUrl).ToString();
        }
        catch
        {
            return null;
        }
    }

    // ===== Reflection-based HTTP =====
    // Root's single-file trimming removes HttpClient methods at JIT time.
    // MissingMethodException fires during JIT, bypassing try/catch.
    // All HTTP calls must use reflection to avoid JIT-time resolution.

    private static void EnsureHttpResolved()
    {
        if (s_httpResolved) return;
        s_httpResolved = true;

        using var ev = WideEvent.Begin("LinkEmbed", "http_resolve");
        try
        {
            // Find HttpClient type from loaded assemblies
            Type? httpClientType = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.GetName().Name != "System.Net.Http") continue;
                httpClientType = asm.GetType("System.Net.Http.HttpClient");
                break;
            }

            if (httpClientType == null)
            {
                ev.SetError("HttpClient type not found");
                return;
            }

            // Create instance via reflection
            s_httpClient = Activator.CreateInstance(httpClientType);
            if (s_httpClient == null) return;

            // Set Timeout
            var timeoutProp = httpClientType.GetProperty("Timeout");
            timeoutProp?.SetValue(s_httpClient, TimeSpan.FromSeconds(HttpTimeoutSeconds));

            // Set Accept-Encoding: identity to prevent gzip/brotli responses.
            // Decompression methods are often trimmed in Root's single-file binary,
            // causing MissingMethodException when YouTube (etc.) returns compressed content.
            // Also set User-Agent since some servers reject bare requests.
            try
            {
                var defaultHeaders = httpClientType.GetProperty("DefaultRequestHeaders")?.GetValue(s_httpClient);
                if (defaultHeaders != null)
                {
                    var tryAdd = defaultHeaders.GetType().GetMethod("TryAddWithoutValidation",
                        new[] { typeof(string), typeof(string) });
                    tryAdd?.Invoke(defaultHeaders, new object[] { "Accept-Encoding", "identity" });
                    tryAdd?.Invoke(defaultHeaders, new object[] { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36" });
                }
            }
            catch (Exception ex)
            {
                ev.Set("headers_error", ex.Message);
            }

            // Find GetStringAsync(string)
            s_getStringAsync = httpClientType.GetMethod("GetStringAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(string) }, null);

            // Find GetByteArrayAsync(string) — may be trimmed
            s_getByteArrayAsync = httpClientType.GetMethod("GetByteArrayAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(string) }, null);

            // Find GetAsync — try (string) first, then (Uri) since trimmed builds may only have Uri overload
            s_getAsync = httpClientType.GetMethod("GetAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(string) }, null);
            s_getAsync ??= httpClientType.GetMethod("GetAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(Uri) }, null);

            // Resolve SendAsync + HttpRequestMessage + HttpMethod.Get for deep fallback.
            // SendAsync is the core method that GetStringAsync delegates to — always survives trimming.
            try
            {
                var httpAsm = httpClientType.Assembly;
                s_httpRequestMessageType = httpAsm.GetType("System.Net.Http.HttpRequestMessage");
                var httpMethodType = httpAsm.GetType("System.Net.Http.HttpMethod");
                s_httpMethodGet = httpMethodType?.GetProperty("Get",
                    BindingFlags.Public | BindingFlags.Static)?.GetValue(null);

                // Find SendAsync(HttpRequestMessage) — 1-param overload
                if (s_httpRequestMessageType != null)
                {
                    s_sendAsync = httpClientType.GetMethod("SendAsync",
                        BindingFlags.Public | BindingFlags.Instance,
                        null, new[] { s_httpRequestMessageType }, null);
                    // Fallback: any SendAsync with compatible first param
                    s_sendAsync ??= httpClientType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => m.Name == "SendAsync" && !m.IsGenericMethod)
                        .FirstOrDefault(m =>
                        {
                            var p = m.GetParameters();
                            return p.Length >= 1 && p[0].ParameterType.IsAssignableFrom(s_httpRequestMessageType);
                        });

                    // Find SendAsync(HttpRequestMessage, HttpCompletionOption) for ResponseHeadersRead.
                    // This lets us return as soon as headers arrive without buffering the body —
                    // critical for extensionless video/binary URLs that would otherwise download
                    // hundreds of MB before the Content-Type check can bail out.
                    var httpCompletionOptionType = httpAsm.GetType("System.Net.Http.HttpCompletionOption");
                    if (httpCompletionOptionType != null)
                    {
                        s_sendAsyncHeadersRead = httpClientType.GetMethod("SendAsync",
                            BindingFlags.Public | BindingFlags.Instance,
                            null, new[] { s_httpRequestMessageType, httpCompletionOptionType }, null);
                        if (s_sendAsyncHeadersRead != null)
                            s_httpCompletionOptionHeadersRead = Enum.ToObject(httpCompletionOptionType, 1); // ResponseHeadersRead = 1
                    }
                }
            }
            catch (Exception ex)
            {
                ev.Set("send_async_error", ex.Message);
            }

            ev.Set("client", s_httpClient != null);
            ev.Set("GetStringAsync", s_getStringAsync != null);
            ev.Set("GetByteArrayAsync", s_getByteArrayAsync != null);
            ev.Set("GetAsync", s_getAsync != null);
            ev.Set("SendAsync", s_sendAsync != null);
            ev.Set("SendAsyncHR", s_sendAsyncHeadersRead != null);
            ev.Set("HttpRequestMessage", s_httpRequestMessageType != null);
            ev.Set("HttpMethod_Get", s_httpMethodGet != null);
        }
        catch (Exception ex)
        {
            ev.SetError(ex);
        }
    }

    /// <summary>
    /// Fetch URL content as string via reflection-based HttpClient.GetStringAsync.
    /// </summary>
    private static string? HttpGetString(string url)
    {
        EnsureHttpResolved();
        if (s_httpClient == null || s_getStringAsync == null) return null;

        try
        {
            var task = s_getStringAsync.Invoke(s_httpClient, new object[] { url });
            if (task == null) return null;

            // task is Task<string> — get .Result via reflection
            var resultProp = task.GetType().GetProperty("Result");
            return resultProp?.GetValue(task) as string;
        }
        catch (Exception ex)
        {
            // Unwrap TargetInvocationException → AggregateException → inner
            var inner = ex;
            if (inner is TargetInvocationException tie && tie.InnerException != null)
                inner = tie.InnerException;
            if (inner is AggregateException ae && ae.InnerException != null)
                inner = ae.InnerException;
            Logger.Log("LinkEmbed", $"HTTP GET error for {url}: {inner.Message}");
            return null;
        }
    }

    /// <summary>
    /// Fetch URL content as byte[] via reflection-based HTTP.
    /// Uses SendAsync as primary path (GetByteArrayAsync is always trimmed in Root).
    /// Validates HTTP status code (2xx) and Content-Type (image/*) to reject
    /// Cloudflare challenge pages and other non-image responses.
    /// </summary>
    private static byte[]? HttpGetBytes(string url)
    {
        EnsureHttpResolved();
        if (s_httpClient == null) return null;

        object? response = null;
        try
        {
            // Use SendAsync as primary path — always survives trimming
            if (s_sendAsync != null && s_httpRequestMessageType != null && s_httpMethodGet != null)
            {
                var request = Activator.CreateInstance(s_httpRequestMessageType, s_httpMethodGet, new Uri(url));
                var sendParams = s_sendAsync.GetParameters();
                object?[] args;
                if (sendParams.Length == 1)
                    args = new[] { request };
                else if (sendParams.Length == 2)
                    args = new object?[] { request, System.Threading.CancellationToken.None };
                else
                    args = new[] { request };

                var task = s_sendAsync.Invoke(s_httpClient, args);
                response = task?.GetType().GetProperty("Result")?.GetValue(task);
            }

            // Fallback: GetAsync (may take string or Uri depending on what survived trimming)
            if (response == null && s_getAsync != null)
            {
                var paramType = s_getAsync.GetParameters()[0].ParameterType;
                object arg = paramType == typeof(Uri) ? new Uri(url) : (object)url;
                var task = s_getAsync.Invoke(s_httpClient, new[] { arg });
                response = task?.GetType().GetProperty("Result")?.GetValue(task);
            }

            if (response == null)
            {
                Logger.Log("LinkEmbed", $"HTTP GET bytes: no response for {url}");
                return null;
            }

            // Check HTTP status code — reject non-2xx (Cloudflare 403/503, redirects to login, etc.)
            try
            {
                var statusCode = response.GetType().GetProperty("StatusCode")?.GetValue(response);
                if (statusCode != null)
                {
                    var statusInt = (int)statusCode;
                    if (statusInt < 200 || statusInt >= 300)
                    {
                        Logger.Log("LinkEmbed", $"HTTP GET bytes: status {statusInt} for {url}");
                        return null;
                    }
                }
            }
            catch { } // StatusCode reflection failed — proceed anyway

            // Check Content-Type — reject non-image responses (catches Cloudflare 200-with-HTML-challenge)
            try
            {
                var content = response.GetType().GetProperty("Content")?.GetValue(response);
                if (content != null)
                {
                    var headers = content.GetType().GetProperty("Headers")?.GetValue(content);
                    if (headers != null)
                    {
                        var ctProp = headers.GetType().GetProperty("ContentType");
                        var ctValue = ctProp?.GetValue(headers)?.ToString();
                        if (ctValue != null)
                        {
                            var ctLower = ctValue.ToLowerInvariant();
                            if (!ctLower.StartsWith("image/"))
                            {
                                Logger.Log("LinkEmbed", $"HTTP GET bytes: non-image Content-Type '{ctValue}' for {url}");
                                return null;
                            }
                        }
                        // Content-Type absent — proceed (some CDNs omit it)
                    }
                }
            }
            catch { } // Content-Type check failed — proceed anyway

            // Read response body as bytes
            var responseContent = response.GetType().GetProperty("Content")?.GetValue(response);
            if (responseContent == null) return null;

            var readTask = responseContent.GetType().GetMethod("ReadAsStreamAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, Type.EmptyTypes, null)?.Invoke(responseContent, null);
            if (readTask == null) return null;

            var stream = readTask.GetType().GetProperty("Result")?.GetValue(readTask) as System.IO.Stream;
            if (stream == null) return null;

            using var ms = new System.IO.MemoryStream();
            using (stream) { stream.CopyTo(ms); }
            return ms.ToArray();
        }
        catch (Exception ex)
        {
            var inner = ex;
            if (inner is TargetInvocationException tie && tie.InnerException != null)
                inner = tie.InnerException;
            if (inner is AggregateException ae && ae.InnerException != null)
                inner = ae.InnerException;
            Logger.Log("LinkEmbed", $"HTTP GET bytes error for {url}: {inner.Message}");
            return null;
        }
        finally
        {
            (response as IDisposable)?.Dispose();
        }
    }

    /// <summary>
    /// Fetch URL via SendAsync and return (contentType, body) tuple.
    /// Returns null body on failure. Content-Type may be null if header is absent.
    /// Always uses SendAsync (not GetAsync) because GetAsync is often trimmed in Root's
    /// single-file binary and fails with MissingMethodException on some response types.
    /// Optionally sets a per-request User-Agent override on the HttpRequestMessage.
    /// </summary>
    private static (string? contentType, string? body) HttpGetWithContentType(string url, string? userAgent = null)
    {
        EnsureHttpResolved();
        if (s_httpClient == null) return (null, null);

        object? response = null;
        try
        {
            if ((s_sendAsync != null || s_sendAsyncHeadersRead != null) && s_httpRequestMessageType != null && s_httpMethodGet != null)
            {
                if (Verbose) Logger.Log("LinkEmbed", $"HGWCT[1] creating request for {url}");
                var request = Activator.CreateInstance(s_httpRequestMessageType, s_httpMethodGet, new Uri(url));

                // Set per-request User-Agent if provided (overrides default header)
                if (userAgent != null && request != null)
                {
                    try
                    {
                        var reqHeaders = request.GetType().GetProperty("Headers")?.GetValue(request);
                        var tryAdd = reqHeaders?.GetType().GetMethod("TryAddWithoutValidation",
                            new[] { typeof(string), typeof(string) });
                        tryAdd?.Invoke(reqHeaders, new object[] { "User-Agent", userAgent });
                    }
                    catch { }
                }

                if (Verbose) Logger.Log("LinkEmbed", $"HGWCT[2] sending");
                object? task;
                // Prefer ResponseHeadersRead: returns as soon as headers arrive without buffering the body.
                // This prevents downloading large video/binary files before the Content-Type check.
                if (s_sendAsyncHeadersRead != null && s_httpCompletionOptionHeadersRead != null)
                {
                    task = s_sendAsyncHeadersRead.Invoke(s_httpClient, new[] { request, s_httpCompletionOptionHeadersRead });
                }
                else if (s_sendAsync != null)
                {
                    var sendParams = s_sendAsync.GetParameters();
                    object?[] args;
                    if (sendParams.Length == 1)
                        args = new[] { request };
                    else if (sendParams.Length == 2)
                        args = new object?[] { request, System.Threading.CancellationToken.None };
                    else
                        args = new[] { request };
                    task = s_sendAsync.Invoke(s_httpClient, args);
                }
                else
                {
                    task = null;
                }
                if (Verbose) Logger.Log("LinkEmbed", $"HGWCT[3] awaiting result");
                response = task?.GetType().GetProperty("Result")?.GetValue(task);
                if (Verbose) Logger.Log("LinkEmbed", $"HGWCT[4] got response: {response != null}");
            }

            if (response == null) return (null, null);

            // Check HTTP status code — reject non-2xx
            try
            {
                var statusCode = response.GetType().GetProperty("StatusCode")?.GetValue(response);
                if (statusCode != null)
                {
                    var statusInt = (int)statusCode;
                    if (statusInt < 200 || statusInt >= 300)
                    {
                        Logger.Log("LinkEmbed", $"HGWCT: status {statusInt} for {url}");
                        return (null, null);
                    }
                }
            }
            catch { } // StatusCode reflection failed — proceed anyway

            // Read Content-Type header
            string? contentType = null;
            try
            {
                var content = response.GetType().GetProperty("Content")?.GetValue(response);
                if (content != null)
                {
                    var headers = content.GetType().GetProperty("Headers")?.GetValue(content);
                    if (headers != null)
                    {
                        var ctProp = headers.GetType().GetProperty("ContentType");
                        var ctValue = ctProp?.GetValue(headers);
                        contentType = ctValue?.ToString();
                    }
                }
            }
            catch { }
            if (Verbose) Logger.Log("LinkEmbed", $"HGWCT[5] content-type: {contentType}");

            // Content-Type early bail: don't read the body at all for non-HTML responses.
            // With ResponseHeadersRead this is a zero-byte read for video/binary URLs.
            // With ResponseContentRead the body is already buffered, but we still skip the allocation.
            if (contentType != null)
            {
                var ct = contentType.ToLowerInvariant();
                bool isHtml = ct.Contains("text/html") || ct.Contains("text/xhtml") || ct.Contains("application/xhtml");
                if (!isHtml)
                {
                    if (Verbose) Logger.Log("LinkEmbed", $"HGWCT: non-HTML content-type bail: {contentType}");
                    return (contentType, null);
                }
            }

            // Read body as string via ReadAsStreamAsync + StreamReader
            // (ReadAsStringAsync triggers trimmed charset/encoding methods on some responses)
            string? body = null;
            try
            {
                var content = response.GetType().GetProperty("Content")?.GetValue(response);
                if (content != null)
                {
                    if (Verbose) Logger.Log("LinkEmbed", $"HGWCT[6] reading stream");
                    var readTask = content.GetType().GetMethod("ReadAsStreamAsync",
                        BindingFlags.Public | BindingFlags.Instance,
                        null, Type.EmptyTypes, null)?.Invoke(content, null);
                    var stream = readTask?.GetType().GetProperty("Result")?.GetValue(readTask) as System.IO.Stream;
                    if (stream != null)
                    {
                        using var reader = new System.IO.StreamReader(stream);
                        // Cap body read at MaxHtmlBytes — prevents OOM for oversized HTML responses
                        var buf = new char[MaxHtmlBytes];
                        int total = 0, n;
                        while (total < MaxHtmlBytes && (n = reader.Read(buf, total, MaxHtmlBytes - total)) > 0)
                            total += n;
                        body = new string(buf, 0, total);
                        if (Verbose) Logger.Log("LinkEmbed", $"HGWCT[7] body length: {body.Length}");
                    }
                }
            }
            catch (Exception bodyEx)
            {
                Logger.Log("LinkEmbed", $"HGWCT body read error: {bodyEx.GetType().Name}: {bodyEx.Message}");
            }

            return (contentType, body);
        }
        catch (Exception ex)
        {
            var inner = ex;
            if (inner is TargetInvocationException tie && tie.InnerException != null)
                inner = tie.InnerException;
            if (inner is AggregateException ae && ae.InnerException != null)
                inner = ae.InnerException;
            Logger.Log("LinkEmbed", $"HGWCT outer error [{inner.GetType().Name}]: {inner.Message}");
            return (null, null);
        }
        finally
        {
            (response as IDisposable)?.Dispose();
        }
    }

    // ===== Metadata fetching =====

    private EmbedData? FetchMetadata(string url)
    {
        if (_metadataCache.TryGetValue(url, out var cached))
            return cached;

        try
        {
            // Direct image URL — zero network, instant
            if (ImageUrlRegex.IsMatch(url))
            {
                var data = SynthesizeImageEmbed(url);
                if (_metadataCache.Count >= MaxMetadataCache) _metadataCache.Clear();
                _metadataCache[url] = data;
                return data;
            }

            // Direct video URL — play button placeholder + open in browser
            if (VideoUrlRegex.IsMatch(url))
            {
                var data = SynthesizeVideoEmbed(url);
                if (_metadataCache.Count >= MaxMetadataCache) _metadataCache.Clear();
                _metadataCache[url] = data;
                return data;
            }

            // YouTube — dedicated oEmbed path
            var ytMatch = YoutubeIdRegex.Match(url);
            if (ytMatch.Success)
            {
                var data = FetchYouTubeMetadata(url, ytMatch.Groups[1].Value);
                if (_metadataCache.Count >= MaxMetadataCache) _metadataCache.Clear();
                _metadataCache[url] = data;
                return data;
            }

            // Reddit — dedicated handler for subreddit label + orange accent
            if (RedditDomainRegex.IsMatch(url))
            {
                var redditData = FetchRedditMetadata(url);
                if (redditData != null)
                {
                    if (_metadataCache.Count >= MaxMetadataCache) _metadataCache.Clear();
                    _metadataCache[url] = redditData;
                    return redditData;
                }
                // Fall through to generic if Reddit-specific fetch failed
            }

            // Generic OG + oEmbed discovery (handles Twitter, any oEmbed site)
            var data2 = FetchOgMetadata(url);
            if (_metadataCache.Count >= MaxMetadataCache) _metadataCache.Clear();
            _metadataCache[url] = data2;
            return data2;
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbed", $"FetchMetadata error [{ex.GetType().Name}] for {url}: {ex.Message}");
            if (_metadataCache.Count >= MaxMetadataCache) _metadataCache.Clear();
            _metadataCache[url] = null;
            return null;
        }
    }

    /// <summary>
    /// Synthesize embed data for a direct image URL — zero network cost.
    /// The existing Phase B image download flow handles actual rendering.
    /// </summary>
    private static EmbedData SynthesizeImageEmbed(string url)
    {
        string title;
        string? provider = null;
        try
        {
            var uri = new Uri(url);
            title = System.IO.Path.GetFileName(uri.AbsolutePath);
            if (string.IsNullOrEmpty(title)) title = "Image";
            provider = uri.Host;
        }
        catch
        {
            title = "Image";
        }

        return new EmbedData(url, provider, null, title, null, url, null, null);
    }

    /// <summary>
    /// Synthesize embed data for a direct video URL (.mp4, .webm, .mov).
    /// No thumbnail to fetch — the embed card will show a dark placeholder with play button.
    /// VideoId is set to "direct" to trigger play button behavior in BuildEmbedCard.
    /// </summary>
    private static EmbedData SynthesizeVideoEmbed(string url)
    {
        string title;
        string? provider = null;
        try
        {
            var uri = new Uri(url);
            title = System.IO.Path.GetFileName(uri.AbsolutePath);
            if (string.IsNullOrEmpty(title)) title = "Video";
            provider = uri.Host;
        }
        catch
        {
            title = "Video";
        }

        return new EmbedData(url, provider, null, title, null, null, null, "direct");
    }

    /// <summary>
    /// oEmbed discovery: look for &lt;link rel="alternate" type="application/json+oembed" href="..."&gt;
    /// in already-fetched HTML, fetch the oEmbed endpoint, and parse the JSON response.
    /// Works for any site that advertises oEmbed (Twitter/X, Reddit, Flickr, etc.).
    /// Returns null if no oEmbed link found or fetch fails — caller falls back to OG tags.
    /// </summary>
    private EmbedData? TryOEmbedDiscovery(string html, string pageUrl, string? userAgent = null)
    {
        // Find the oEmbed discovery link (try both attribute orderings)
        string? oembedEndpoint = null;
        var discoveryMatch = OEmbedDiscoveryRegex.Match(html);
        if (discoveryMatch.Success)
            oembedEndpoint = System.Net.WebUtility.HtmlDecode(discoveryMatch.Groups[1].Value);
        if (oembedEndpoint == null)
        {
            var reverseMatch = OEmbedDiscoveryReverseRegex.Match(html);
            if (reverseMatch.Success)
                oembedEndpoint = System.Net.WebUtility.HtmlDecode(reverseMatch.Groups[1].Value);
        }

        if (oembedEndpoint == null) return null;

        try
        {
            // Pass through the same UA used for the page fetch (bot UA for embed-fixer domains)
            var (_, json) = HttpGetWithContentType(oembedEndpoint, userAgent);
            if (json == null) return null;

            if (Verbose) Logger.Log("LinkEmbed", $"oEmbed JSON ({json.Length}b): {(json.Length > 300 ? json[..300] : json)}");

            string? title = null;
            string? author = null;
            string? thumbnail = null;
            string? providerName = null;
            string? description = null;

            var titleMatch = JsonTitleRegex.Match(json);
            if (titleMatch.Success)
                title = DecodeJsonString(titleMatch.Groups[1].Value);

            var authorMatch = JsonAuthorRegex.Match(json);
            if (authorMatch.Success)
                author = DecodeJsonString(authorMatch.Groups[1].Value);

            var thumbMatch = JsonThumbnailUrlRegex.Match(json);
            if (thumbMatch.Success)
                thumbnail = DecodeJsonString(thumbMatch.Groups[1].Value);

            // For "photo" type oEmbed, the image is in "url" not "thumbnail_url"
            if (thumbnail == null)
            {
                var typeMatch = JsonTypeRegex.Match(json);
                var urlMatch = JsonUrlRegex.Match(json);
                if (urlMatch.Success)
                {
                    var oembedType = typeMatch.Success ? DecodeJsonString(typeMatch.Groups[1].Value) : null;
                    var oembedUrl = DecodeJsonString(urlMatch.Groups[1].Value);
                    // Use "url" as thumbnail for photo type, or if it looks like an image URL
                    if (oembedType == "photo" || ImageUrlRegex.IsMatch(oembedUrl ?? ""))
                        thumbnail = oembedUrl;
                }
            }

            var provMatch = JsonProviderNameRegex.Match(json);
            if (provMatch.Success)
                providerName = DecodeJsonString(provMatch.Groups[1].Value);

            var descMatch = JsonDescriptionRegex.Match(json);
            if (descMatch.Success)
                description = DecodeJsonString(descMatch.Groups[1].Value);

            // Some providers (Twitter) put content in the html field instead of title/description.
            // Extract text from <p> tags inside the html field as fallback.
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(description))
            {
                var htmlFieldMatch = JsonHtmlRegex.Match(json);
                if (htmlFieldMatch.Success)
                {
                    var htmlContent = DecodeJsonString(htmlFieldMatch.Groups[1].Value);
                    var pMatch = HtmlParagraphRegex.Match(htmlContent);
                    if (pMatch.Success)
                    {
                        var text = HtmlTagRegex.Replace(pMatch.Groups[1].Value, "");
                        text = System.Net.WebUtility.HtmlDecode(text).Trim();
                        if (!string.IsNullOrEmpty(text))
                            description = text;
                    }
                }
            }

            if (Verbose) Logger.Log("LinkEmbed", $"oEmbed parsed: title={title != null}, author={author != null}, thumb={thumbnail != null}, prov={providerName}, desc={description != null}");

            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(description))
                return null;

            if (string.IsNullOrEmpty(title))
                title = description?.Length > 80 ? description[..77] + "..." : description;

            if (description != null && description.Length > 200)
                description = description[..197] + "...";

            return new EmbedData(pageUrl, providerName, author, title, description, thumbnail, null, null);
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbed", $"oEmbed discovery fetch error [{ex.GetType().Name}] for {pageUrl}: {ex.Message}");
            return null;
        }
    }

    // Non-vxtwitter embed-fixer domains to normalize to vxtwitter (best OG tags with images)
    private static readonly Regex NonVxEmbedFixerRegex = new(
        @"^(https?://)(?:fixvx\.com|fxtwitter\.com|fixupx\.com|twittpr\.com)/",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private EmbedData? FetchOgMetadata(string url)
    {
        var originalUrl = url;

        // Normalize non-vxtwitter embed-fixer domains to vxtwitter.com
        // vxtwitter has the richest OG tags (including twitter:image)
        var fixerMatch = NonVxEmbedFixerRegex.Match(url);
        if (fixerMatch.Success)
        {
            url = fixerMatch.Groups[1].Value + "vxtwitter.com/" + url[(fixerMatch.Length)..];
        }

        // Twitter-related domains (x.com, twitter.com, vxtwitter, fxtwitter, fixupx, etc.)
        // serve OG tags only to bot UAs. Use a bot-like UA for these via per-request override.
        string? botUA = null;
        if (EmbedFixerDomainRegex.IsMatch(url) || TwitterDomainRegex.IsMatch(url))
        {
            botUA = "Mozilla/5.0 (compatible; Discordbot/2.0; +https://discordapp.com)";
        }

        var (contentType, html) = HttpGetWithContentType(url, botUA);
        if (html == null)
        {
            // HttpGetWithContentType bailed early (non-HTML Content-Type or fetch error).
            // Still synthesize embed if we know what type it is.
            if (contentType != null)
            {
                var ct = contentType.ToLowerInvariant();
                if (ct.StartsWith("image/"))
                    return SynthesizeImageEmbed(originalUrl);
                if (ct.StartsWith("video/"))
                    return SynthesizeVideoEmbed(originalUrl);
            }
            return null;
        }

        // Content-Type gate: only parse HTML-like responses
        if (contentType != null)
        {
            var ct = contentType.ToLowerInvariant();
            if (ct.Contains("text/html") || ct.Contains("text/xhtml") || ct.Contains("application/xhtml"))
            {
                // HTML — proceed to OG parsing
            }
            else if (ct.StartsWith("image/"))
            {
                // Extensionless image URL (e.g. pbs.twimg.com/media/abc) — synthesize image embed
                return SynthesizeImageEmbed(originalUrl);
            }
            else if (ct.StartsWith("video/"))
            {
                // Direct video URL without extension — synthesize video embed
                return SynthesizeVideoEmbed(originalUrl);
            }
            else
            {
                // PDF, binary, JSON, etc. — bail
                return null;
            }
        }
        // Content-Type absent — allow through (safe default)

        // Limit to first 50KB for parsing
        if (html.Length > MaxHtmlBytes)
            html = html[..MaxHtmlBytes];

        // oEmbed discovery: check for <link rel="alternate" type="application/json+oembed">
        // If found, use oEmbed data (richer than OG for Twitter, Reddit, etc.)
        var oembedResult = TryOEmbedDiscovery(html, originalUrl, botUA);
        if (oembedResult != null) return oembedResult;

        // Parse OG tags
        var ogTags = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (Match m in OgRegex.Matches(html))
        {
            var prop = m.Groups[1].Value;
            var content = m.Groups[2].Value;
            if (!ogTags.ContainsKey(prop))
                ogTags[prop] = System.Net.WebUtility.HtmlDecode(content);
        }

        foreach (Match m in OgRegexReverse.Matches(html))
        {
            var content = m.Groups[1].Value;
            var prop = m.Groups[2].Value;
            if (!ogTags.ContainsKey(prop))
                ogTags[prop] = System.Net.WebUtility.HtmlDecode(content);
        }

        ogTags.TryGetValue("og:title", out var title);
        ogTags.TryGetValue("og:description", out var description);
        ogTags.TryGetValue("og:image", out var image);
        ogTags.TryGetValue("og:site_name", out var siteName);
        ogTags.TryGetValue("theme-color", out var themeColor);

        // Fallback: twitter:* meta tags (vxtwitter, fxtwitter use these instead of og:*)
        if (string.IsNullOrEmpty(image))
            ogTags.TryGetValue("twitter:image", out image);
        if (string.IsNullOrEmpty(title))
            ogTags.TryGetValue("twitter:title", out title);
        if (string.IsNullOrEmpty(description))
            ogTags.TryGetValue("twitter:description", out description);

        if (string.IsNullOrEmpty(title))
        {
            var titleMatch = TitleRegex.Match(html);
            if (titleMatch.Success)
                title = System.Net.WebUtility.HtmlDecode(titleMatch.Groups[1].Value.Trim());
        }

        if (string.IsNullOrEmpty(siteName))
        {
            try { siteName = new Uri(url).Host; }
            catch { }
        }

        if (string.IsNullOrEmpty(title)) return null;

        if (description != null && description.Length > 200)
            description = description[..197] + "...";

        return new EmbedData(originalUrl, siteName, null, title, description, image, themeColor, null);
    }

    private EmbedData? FetchYouTubeMetadata(string url, string videoId)
    {
        string? title = null;
        string? author = null;

        // Try oEmbed API first
        try
        {
            var oembedUrl = $"https://www.youtube.com/oembed?url=https://www.youtube.com/watch?v={videoId}&format=json";
            var json = HttpGetString(oembedUrl);

            if (json != null)
            {
                var titleMatch = JsonTitleRegex.Match(json);
                if (titleMatch.Success)
                    title = DecodeJsonString(titleMatch.Groups[1].Value);

                var authorMatch = JsonAuthorRegex.Match(json);
                if (authorMatch.Success)
                    author = DecodeJsonString(authorMatch.Groups[1].Value);
            }
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbed", $"YouTube oEmbed error: {ex.Message}");
        }

        // Fallback: scrape watch page <title> if oEmbed failed (trimmed decompression methods)
        if (string.IsNullOrEmpty(title))
        {
            try
            {
                var pageHtml = HttpGetString($"https://www.youtube.com/watch?v={videoId}");
                if (pageHtml != null)
                {
                    var pageTitleMatch = TitleRegex.Match(pageHtml);
                    if (pageTitleMatch.Success)
                    {
                        title = System.Net.WebUtility.HtmlDecode(pageTitleMatch.Groups[1].Value.Trim());
                        if (title.EndsWith(" - YouTube"))
                            title = title[..^10];
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("LinkEmbed", $"YouTube page scrape error: {ex.Message}");
            }
        }

        if (string.IsNullOrEmpty(title))
            title = "YouTube Video";

        // Truncate long YouTube titles
        if (title!.Length > 100)
            title = title[..97] + "...";

        var thumbnail = $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";
        return new EmbedData(url, "YouTube", author, title, null, thumbnail, "#FF0000", videoId);
    }

    /// <summary>
    /// Fetch Reddit metadata via old.reddit.com OG tags.
    /// old.reddit.com serves lighter HTML with reliable OG tags in the first few KB.
    /// Returns null if no title found — caller falls through to generic OG path.
    /// </summary>
    private EmbedData? FetchRedditMetadata(string url)
    {
        // Extract subreddit from URL for provider label
        string provider = "Reddit";
        var subMatch = RedditSubredditRegex.Match(url);
        if (subMatch.Success)
            provider = "r/" + subMatch.Groups[1].Value;

        // Normalize fetch URL to old.reddit.com (lighter HTML, reliable OG tags)
        // Only rewrite www/new/np prefixes — leave old.reddit.com as-is
        var fetchUrl = Regex.Replace(url,
            @"^(https?://)(?:www\.|new\.|np\.)?reddit\.com/",
            "$1old.reddit.com/",
            RegexOptions.IgnoreCase);

        if (Verbose) Logger.Log("LinkEmbed", $"Reddit fetch: {url} -> {fetchUrl}");

        var (contentType, html) = HttpGetWithContentType(fetchUrl);
        if (html == null) return null;

        // Content-Type gate: only parse HTML
        if (contentType != null)
        {
            var ct = contentType.ToLowerInvariant();
            if (!ct.Contains("text/html") && !ct.Contains("text/xhtml") && !ct.Contains("application/xhtml"))
                return null;
        }

        // Limit to first 50KB for parsing
        if (html.Length > MaxHtmlBytes)
            html = html[..MaxHtmlBytes];

        // Parse OG tags
        var ogTags = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (Match m in OgRegex.Matches(html))
        {
            var prop = m.Groups[1].Value;
            var content = m.Groups[2].Value;
            if (!ogTags.ContainsKey(prop))
                ogTags[prop] = System.Net.WebUtility.HtmlDecode(content);
        }

        foreach (Match m in OgRegexReverse.Matches(html))
        {
            var content = m.Groups[1].Value;
            var prop = m.Groups[2].Value;
            if (!ogTags.ContainsKey(prop))
                ogTags[prop] = System.Net.WebUtility.HtmlDecode(content);
        }

        ogTags.TryGetValue("og:title", out var title);
        ogTags.TryGetValue("og:description", out var description);
        ogTags.TryGetValue("og:image", out var image);

        // Fallback to <title> tag
        if (string.IsNullOrEmpty(title))
        {
            var titleMatch = TitleRegex.Match(html);
            if (titleMatch.Success)
                title = System.Net.WebUtility.HtmlDecode(titleMatch.Groups[1].Value.Trim());
        }

        if (string.IsNullOrEmpty(title)) return null;

        if (description != null && description.Length > 200)
            description = description[..197] + "...";

        return new EmbedData(url, provider, null, title, description, image, RedditAccentColor, null);
    }

    /// <summary>
    /// Decode JSON string escapes: \", \\, \/, \uXXXX
    /// </summary>
    private static string DecodeJsonString(string raw)
    {
        var decoded = raw
            .Replace("\\\"", "\"")
            .Replace("\\/", "/");

        // Decode \uXXXX sequences without Regex.Replace + lambda (trimmed in Root)
        int idx = 0;
        while ((idx = decoded.IndexOf("\\u", idx)) >= 0 && idx + 5 < decoded.Length)
        {
            var hex = decoded.Substring(idx + 2, 4);
            if (int.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out int code))
            {
                decoded = string.Concat(decoded.AsSpan(0, idx), ((char)code).ToString(), decoded.AsSpan(idx + 6));
            }
            else
            {
                idx += 2; // Skip this \u, not valid hex
            }
        }

        return decoded.Replace("\\\\", "\\");
    }

    // ===== Component 4: Embed Card Builder =====

    /// <summary>
    /// Ensure a text color has sufficient contrast against the card background.
    /// Falls back to the theme's primary text color if WCAG contrast ratio is below 3:1.
    /// </summary>
    private static string EnsureReadable(string textHex)
    {
        try
        {
            var bgHex = ContentPages.CardBg;
            double bgLum = ColorUtils.Luminance(bgHex);
            double fgLum = ColorUtils.Luminance(textHex);
            double lighter = Math.Max(bgLum, fgLum);
            double darker = Math.Min(bgLum, fgLum);
            double ratio = (lighter + 0.05) / (darker + 0.05);
            if (ratio >= 3.0) return textHex;
        }
        catch { }
        return ContentPages.TextWhite;
    }

    private object? BuildEmbedCard(EmbedData data, object? bitmap = null, byte[]? imageBytes = null)
    {
        try
        {
            // Card structure:
            // Border (CardBg, CornerRadius=8, left accent border 3px)
            //   └── StackPanel (vertical, spacing=6, padding=12)
            //       ├── TextBlock (provider, FontSize=11, TextDim)           [if provider]
            //       ├── TextBlock (author, FontSize=12, Bold, TextWhite)     [if author]
            //       ├── TextBlock (title, FontSize=13, SemiBold, accent)     [required]
            //       ├── TextBlock (description, FontSize=12, TextMuted)      [if desc]
            //       └── Border/Grid (image + optional play overlay)          [if image]

            var themeAccent = _themeEngine?.GetAccentColor() ?? ContentPages.AccentGreen;
            var accentHex = data.Color ?? themeAccent;
            // For generic embeds, use theme-color for title if available; YouTube/default uses theme accent.
            // EnsureReadable checks WCAG contrast against CardBg and falls back to TextWhite.
            var titleColorHex = EnsureReadable(
                (data.VideoId == null && data.Color != null) ? data.Color : themeAccent);

            // Build inner content
            var content = _r.CreateStackPanel(vertical: true, spacing: 6);
            if (content == null) return null;

            // Provider name
            if (!string.IsNullOrEmpty(data.Provider))
            {
                var providerText = _r.CreateTextBlock(data.Provider!, 11, ContentPages.TextDim);
                if (providerText != null)
                    _r.AddChild(content, providerText);
            }

            // Author (YouTube channel name, etc.)
            if (!string.IsNullOrEmpty(data.Author))
            {
                var authorText = _r.CreateTextBlock(data.Author!, 12, ContentPages.TextWhite, "Bold");
                if (authorText != null)
                    _r.AddChild(content, authorText);
            }

            // Title — hide for image/video-only embeds when filenames setting is off
            bool isFileOnlyEmbed = (data.Image == data.Url && data.VideoId == null)
                                || data.VideoId == "direct";
            var titleText = _r.CreateTextBlock(data.Title!, 13, titleColorHex, "SemiBold");
            if (titleText != null)
            {
                _r.SetTextWrapping(titleText, "Wrap");
                if (isFileOnlyEmbed)
                {
                    _r.SetTag(titleText, "uprooted-embed-img-title");
                    _r.SetIsVisible(titleText, UprootedSettings.Load().LinkEmbedsShowFilenames);
                }
                _r.AddChild(content, titleText);
            }

            // Description
            if (!string.IsNullOrEmpty(data.Description))
            {
                var descText = _r.CreateTextBlock(data.Description!, 12, ContentPages.TextMuted);
                if (descText != null)
                {
                    _r.SetTextWrapping(descText, "Wrap");
                    _r.AddChild(content, descText);
                }
            }

            // Image preview (only used for initial build with bitmap; Phase B adds image later)
            if (bitmap != null || imageBytes != null)
            {
                object? imageElement = null;

                // Try animated path first if we have raw bytes
                if (imageBytes != null && data.VideoId == null && AnimatedImage.IsAnimated(imageBytes))
                {
                    var frames = AnimatedImage.DecodeFrames(imageBytes, _r);
                    if (frames != null)
                    {
                        var animated = AnimatedImage.CreateAnimatedControl(frames, _r);
                        if (animated != null)
                        {
                            var img = animated.Value.control;
                            _r.SetMaxHeight(img, MaxImageHeight);
                            _r.SetMaxWidth(img, MaxImageWidth);
                            _r.SetHorizontalAlignment(img, "Left");

                            var imgBorder = _r.CreateBorder(null, cornerRadius: 6);
                            if (imgBorder != null)
                            {
                                _r.SetClipToBounds(imgBorder, true);
                                _r.SetHorizontalAlignment(imgBorder, "Left");
                                _r.SetBorderChild(imgBorder, img);
                                imageElement = imgBorder;
                            }
                            else
                            {
                                imageElement = img;
                            }

                            // Track dispose action for cleanup
                            _animatedDisposables[data.Url] = animated.Value.dispose;
                        }
                    }
                }

                // Fallback: static bitmap (non-animated, or animated decode failed)
                if (imageElement == null && (bitmap != null || imageBytes != null))
                {
                    // Create static bitmap from bytes if we don't have one
                    if (bitmap == null && imageBytes != null)
                    {
                        try
                        {
                            using var bmpMs = new System.IO.MemoryStream(imageBytes);
                            bitmap = _r.CreateBitmapFromStream(bmpMs);
                        }
                        catch { }
                    }

                    if (bitmap != null)
                    {
                        if (data.VideoId != null)
                        {
                            imageElement = BuildThumbnailWithPlayButton(bitmap, data.Url);
                        }
                        else
                        {
                            var img = _r.CreateImage("Uniform");
                            if (img != null)
                            {
                                _r.SetImageSource(img, bitmap);
                                _r.SetMaxHeight(img, MaxImageHeight);
                                _r.SetMaxWidth(img, MaxImageWidth);
                                _r.SetHorizontalAlignment(img, "Left");

                                var imgBorder = _r.CreateBorder(null, cornerRadius: 6);
                                if (imgBorder != null)
                                {
                                    _r.SetClipToBounds(imgBorder, true);
                                    _r.SetHorizontalAlignment(imgBorder, "Left");
                                    _r.SetBorderChild(imgBorder, img);
                                    imageElement = imgBorder;
                                }
                                else
                                {
                                    imageElement = img;
                                }
                            }
                        }
                    }
                }

                if (imageElement != null)
                    _r.AddChild(content, imageElement);
            }

            // Video placeholder: dark rectangle with centered play button (direct .mp4/.webm/.mov URLs)
            // Only shown when VideoId is set but no thumbnail image is available.
            // YouTube embeds have data.Image set (thumbnail URL), so this won't trigger for them.
            if (data.VideoId != null && string.IsNullOrEmpty(data.Image) && bitmap == null && imageBytes == null)
            {
                var placeholder = BuildVideoPlaceholder(data.Url);
                if (placeholder != null)
                    _r.AddChild(content, placeholder);
            }

            // Outer border: background + left accent strip via BorderThickness
            var card = _r.CreateBorder(ContentPages.CardBg, cornerRadius: 8, child: content);
            if (card == null) return null;

            _r.SetPadding(card, 12, 10, 12, 10);
            _r.SetMaxWidth(card, MaxCardWidth);
            _r.SetHorizontalAlignment(card, "Left");

            // Left accent border (3px colored left edge)
            SetBorderBrush(card, accentHex);
            SetBorderThickness(card, 3, 0, 0, 0);

            return card;
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbed", $"BuildEmbedCard error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Build a thumbnail image with a centered play button overlay for YouTube embeds.
    /// Clicking the thumbnail opens the YouTube URL in the default browser.
    /// Structure: Border (CornerRadius=6, ClipToBounds) → Grid → [Image, Play Button]
    /// </summary>
    private object? BuildThumbnailWithPlayButton(object bitmap, string? url = null)
    {
        var img = _r.CreateImage("UniformToFill");
        if (img == null) return null;
        _r.SetImageSource(img, bitmap);
        _r.SetMaxHeight(img, MaxImageHeight);
        _r.SetMaxWidth(img, MaxImageWidth);

        // Play button: semi-transparent circle with triangle
        var playIcon = _r.CreateTextBlock("\u25B6", 20, "#FFFFFF"); // ▶
        if (playIcon != null)
        {
            _r.SetHorizontalAlignment(playIcon, "Center");
            _r.SetVerticalAlignment(playIcon, "Center");
        }

        var playCircle = _r.CreateBorder("#CC000000", cornerRadius: 24, child: playIcon);
        if (playCircle != null)
        {
            _r.SetWidth(playCircle, 48);
            _r.SetHeight(playCircle, 48);
            _r.SetHorizontalAlignment(playCircle, "Center");
            _r.SetVerticalAlignment(playCircle, "Center");
        }

        // Overlay grid: image + play button stacked
        var grid = _r.CreateGrid();
        if (grid == null) return null;
        _r.AddChild(grid, img);
        if (playCircle != null)
            _r.AddChild(grid, playCircle);

        // Clip container
        var container = _r.CreateBorder(null, cornerRadius: 6);
        if (container != null)
        {
            _r.SetClipToBounds(container, true);
            _r.SetBorderChild(container, grid);
            _r.SetMaxHeight(container, MaxImageHeight);
            _r.SetMaxWidth(container, MaxImageWidth);
            _r.SetHorizontalAlignment(container, "Left");
            _r.SetCursorHand(container);

            // Click to open YouTube URL in default browser
            if (url != null)
            {
                var capturedUrl = url;
                _r.SubscribeEvent(container, "PointerPressed", () =>
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = capturedUrl,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("LinkEmbed", $"Open URL error: {ex.Message}");
                    }
                });
            }

            return container;
        }

        return grid;
    }

    /// <summary>
    /// Build a dark video placeholder with centered play button for direct video URLs (.mp4, .webm, .mov).
    /// Used when no thumbnail image is available (SkiaSharp cannot decode video formats).
    /// Clicking opens the video URL in the default browser (same behavior as YouTube embeds).
    /// Structure: Border (corner radius 6, clip) → Grid → [Border (dark bg), Play Button Circle]
    /// </summary>
    private object? BuildVideoPlaceholder(string url)
    {
        // Dark background rectangle (16:9 aspect ratio)
        var bgBorder = _r.CreateBorder("#2d2d44", cornerRadius: 0);
        if (bgBorder == null) return null;
        _r.SetWidth(bgBorder, MaxImageWidth);
        _r.SetHeight(bgBorder, Math.Round(MaxImageWidth * 9.0 / 16.0)); // 16:9

        // Play button: red circle with white triangle (YouTube-style)
        var playIcon = _r.CreateTextBlock("\u25B6", 24, "#FFFFFF"); // ▶
        if (playIcon != null)
        {
            _r.SetHorizontalAlignment(playIcon, "Center");
            _r.SetVerticalAlignment(playIcon, "Center");
        }

        var playCircle = _r.CreateBorder("#CC000000", cornerRadius: 28, child: playIcon);
        if (playCircle != null)
        {
            _r.SetWidth(playCircle, 56);
            _r.SetHeight(playCircle, 56);
            _r.SetHorizontalAlignment(playCircle, "Center");
            _r.SetVerticalAlignment(playCircle, "Center");
        }

        // Overlay grid: dark bg + play button stacked
        var grid = _r.CreateGrid();
        if (grid == null) return null;
        _r.AddChild(grid, bgBorder);
        if (playCircle != null)
            _r.AddChild(grid, playCircle);

        // Clip container
        var container = _r.CreateBorder(null, cornerRadius: 6);
        if (container != null)
        {
            _r.SetClipToBounds(container, true);
            _r.SetBorderChild(container, grid);
            _r.SetMaxHeight(container, MaxImageHeight);
            _r.SetMaxWidth(container, MaxImageWidth);
            _r.SetHorizontalAlignment(container, "Left");
            _r.SetCursorHand(container);

            // Click to open video URL in default browser
            var capturedUrl = url;
            _r.SubscribeEvent(container, "PointerPressed", () =>
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = capturedUrl,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    Logger.Log("LinkEmbed", $"Open video URL error: {ex.Message}");
                }
            });

            return container;
        }

        return grid;
    }

    /// <summary>
    /// Set BorderBrush on a Border control via reflection.
    /// </summary>
    private void SetBorderBrush(object? border, string hex)
    {
        if (border == null) return;
        var brush = _r.CreateBrush(hex);
        if (brush == null) return;
        border.GetType().GetProperty("BorderBrush")?.SetValue(border, brush);
    }

    /// <summary>
    /// Set Grid.ColumnSpan attached property via reflection.
    /// </summary>
    private void SetGridColumnSpan(object control, int span)
    {
        if (_r.GridType == null) return;
        var setColumnSpan = _r.GridType.GetMethod("SetColumnSpan",
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        setColumnSpan?.Invoke(null, new object[] { control, span });
    }

    /// <summary>
    /// Set BorderThickness on a Border control via reflection.
    /// </summary>
    private void SetBorderThickness(object border, double left, double top, double right, double bottom)
    {
        if (_r.ThicknessType == null) return;
        var thickness = Activator.CreateInstance(_r.ThicknessType, left, top, right, bottom);
        border.GetType().GetProperty("BorderThickness")?.SetValue(border, thickness);
    }

    // ===== Component 5: Visual Tree Injection =====

    private void InjectEmbed(object textControl, EmbedData data, byte[]? imageBytes)
    {
        _r.RunOnUIThread(() =>
        {
            try
            {
                var (grid, col, colSpan) = FindInjectionGrid(textControl);
                if (grid == null) return;

                // URL-specific dedup tag
                var embedTag = "uprooted-link-embed:" + data.Url;

                // Check if already injected
                var children = _r.GetChildren(grid);
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        if (child != null && _r.GetTag(child) == embedTag) return;
                    }
                }

                var card = BuildEmbedCard(data);
                if (card == null) return;

                _r.SetTag(card, embedTag);
                _r.SetMargin(card, 0, 4, 0, 4);

                // Add an Auto RowDefinition to the Grid so the embed gets its own row
                int newRow = AddAutoRowToGrid(grid);

                // Place embed in the new row, same column as message text
                _r.SetGridRow(card, newRow);
                _r.SetGridColumn(card, col);
                SetGridColumnSpan(card, colSpan);

                _r.AddChild(grid, card);
                if (_injectedCards.Count >= MaxInjectedCards) _injectedCards.RemoveRange(0, _injectedCards.Count / 2);
                _injectedCards.Add(card);
            }
            catch (Exception ex)
            {
                Logger.Log("LinkEmbed", $"Inject error: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Add a new Auto-height RowDefinition to a Grid and return its row index.
    /// </summary>
    private int AddAutoRowToGrid(object grid)
    {
        try
        {
            var rowDefsProp = grid.GetType().GetProperty("RowDefinitions");
            var rowDefs = rowDefsProp?.GetValue(grid);
            if (rowDefs == null) return 99; // fallback: high row number

            // Get current count
            int count = 0;
            var countProp = rowDefs.GetType().GetProperty("Count");
            if (countProp != null)
                count = (int)countProp.GetValue(rowDefs)!;

            // Create RowDefinition with Auto height
            // GridUnitType.Auto = 0 in Avalonia
            if (_r.GridLengthType != null && _r.GridUnitTypeEnum != null)
            {
                var autoUnit = Enum.Parse(_r.GridUnitTypeEnum, "Auto");
                var autoLength = Activator.CreateInstance(_r.GridLengthType, 1.0, autoUnit);

                // Find RowDefinition type from same assembly as Grid
                var rowDefType = _r.GridType?.Assembly.GetType("Avalonia.Controls.RowDefinition");
                if (rowDefType != null)
                {
                    var rowDef = Activator.CreateInstance(rowDefType);
                    rowDefType.GetProperty("Height")?.SetValue(rowDef, autoLength);

                    // Add to RowDefinitions collection
                    var addMethod = rowDefs.GetType().GetMethod("Add");
                    addMethod?.Invoke(rowDefs, new[] { rowDef });

                    return count; // the new row is at index = old count
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbed", $"AddAutoRowToGrid error: {ex.Message}");
        }

        return 99; // fallback
    }

    /// <summary>
    /// Find the message content Grid that contains the CTextBlock's ancestor RootMarkdownTextBlock.
    /// Tree structure (confirmed via diagnostics):
    ///   CTextBlock → StackPanel(6) → RootMarkdownTextBlock → Grid(12) → Grid(7) → Panel → Border → Grid → MessageView → ...
    ///
    /// We inject into Grid(12) at depth 2 — the message layout grid.
    /// Returns a tuple: (grid, column to use, columnSpan).
    /// </summary>
    private (object? grid, int column, int columnSpan) FindInjectionGrid(object control)
    {
        // Walk up: CTextBlock → StackPanel → RootMarkdownTextBlock → Grid (target)
        var current = control;
        object? markdownTextBlock = null;

        for (int i = 0; i < 6; i++)
        {
            current = _r.GetParent(current);
            if (current == null) break;

            var typeName = current.GetType().Name;
            if (typeName == "RootMarkdownTextBlock")
            {
                markdownTextBlock = current;
                break;
            }
        }

        if (markdownTextBlock == null)
            return (null, 0, 1);

        // The Grid is the direct parent of RootMarkdownTextBlock
        var grid = _r.GetParent(markdownTextBlock);
        if (grid == null || !_r.IsGrid(grid))
        {
            // Fallback: try one more level up
            var above = _r.GetParent(grid!);
            if (above != null && _r.IsGrid(above))
                grid = above;
            else
                return (null, 0, 1);
        }

        // Get the column the markdown text is in (usually column 1, column 0 is avatar)
        int col = _r.GetGridColumn(markdownTextBlock);
        // Span all remaining columns
        int colSpan = 99;

        return (grid, col, colSpan);
    }
}
