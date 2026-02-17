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
internal class LinkEmbedEngine
{
    private const int ScanIntervalMs = 500;
    private const int MaxUrlsPerScan = 20;
    private const int HttpTimeoutSeconds = 5;
    private const int MaxHtmlBytes = 50 * 1024; // 50KB HTML read limit
    private const double MaxCardWidth = 400.0;
    private const int MaxImageBytes = 5 * 1024 * 1024;   // 5MB
    private const double MaxImageHeight = 200.0;
    private const double MaxImageWidth = 380.0;
    private const string DefaultAccentColor = "#5865F2"; // Discord-like blue

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;

    private Timer? _scanTimer;
    private int _scanning; // Interlocked reentrancy guard

    // Chat area discovery — no caching, always scan from mainWindow on each tick
    // to handle room navigation instantly

    // URL tracking — metadata cache avoids re-fetching, injectedCards tracks live cards
    // Per-node dedup is via "uprooted-embed-scanned" tag on each CTextBlock
    // Image byte cache survives VirtualizingStackPanel recycling for instant re-injection
    private readonly Dictionary<string, EmbedData?> _metadataCache = new();
    private readonly Dictionary<string, byte[]> _imageBytesCache = new();
    private readonly List<object> _injectedCards = new();

    // HTTP via reflection — Root's single-file trimming removes HttpClient methods at JIT time.
    // Using reflection avoids MissingMethodException during JIT compilation.
    private static object? s_httpClient;
    private static MethodInfo? s_getStringAsync;     // HttpClient.GetStringAsync(string)
    private static MethodInfo? s_getByteArrayAsync;  // HttpClient.GetByteArrayAsync(string)
    private static MethodInfo? s_getAsync;           // HttpClient.GetAsync(string) — fallback for bytes
    private static MethodInfo? s_sendAsync;          // HttpClient.SendAsync — core method
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
    private static readonly Regex OgRegex = new(
        @"<meta\s+(?:[^>]*?\s)?(?:property|name)\s*=\s*[""']([^""']+)[""'][^>]*?\scontent\s*=\s*[""']([^""']*)[""'][^>]*?/?>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Reverse order: <meta content="..." property="og:title">
    private static readonly Regex OgRegexReverse = new(
        @"<meta\s+(?:[^>]*?\s)?content\s*=\s*[""']([^""']*)[""'][^>]*?\s(?:property|name)\s*=\s*[""']([^""']+)[""'][^>]*?/?>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // Fallback <title> tag
    private static readonly Regex TitleRegex = new(
        @"<title[^>]*>([^<]+)</title>",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // YouTube video ID extraction
    private static readonly Regex YoutubeIdRegex = new(
        @"(?:youtube\.com/watch\?.*?v=|youtu\.be/|youtube\.com/embed/|youtube\.com/shorts/)([a-zA-Z0-9_-]{11})",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    // JSON "title":"value" for oEmbed parsing (no System.Text.Json)
    private static readonly Regex JsonTitleRegex = new(
        @"""title""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    // JSON "author_name":"value" for oEmbed author parsing
    private static readonly Regex JsonAuthorRegex = new(
        @"""author_name""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""",
        RegexOptions.Compiled);

    internal LinkEmbedEngine(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _mainWindow = mainWindow;
    }

    internal void Initialize()
    {
        Logger.Log("LinkEmbed", "Starting native link embed engine");

        // Pre-warm HttpClient reflection so first fetch doesn't pay resolution cost
        EnsureHttpResolved();

        // Start polling timer
        _scanTimer = new Timer(OnScanTick, null, 0, ScanIntervalMs);
        Logger.Log("LinkEmbed", $"Scan timer started ({ScanIntervalMs}ms interval)");
    }

    // ===== Timer callback =====

    private void OnScanTick(object? state)
    {
        // Reentrancy guard (same pattern as SidebarInjector)
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        try
        {
            // Check settings — stop if plugin disabled
            var settings = UprootedSettings.Load();
            if (settings.Plugins.TryGetValue("link-embeds", out var enabled) && !enabled)
            {
                Interlocked.Exchange(ref _scanning, 0);
                return;
            }

            _r.RunOnUIThread(() =>
            {
                try
                {
                    ScanForUrls();
                }
                catch (Exception ex)
                {
                    Logger.Log("LinkEmbed", $"Scan error: {ex.Message}");
                }
                finally
                {
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
            Logger.Log("LinkEmbed", $"OnScanTick error: {ex.Message}");
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

    private void ScanForUrls()
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
    }

    // ===== Component 3: OG Metadata Fetcher =====

    private void FetchAndInject(string url, object textBlock)
    {
        try
        {
            var data = FetchMetadata(url);
            if (data == null) return;
            if (string.IsNullOrEmpty(data.Title)) return;

            // Fast path: if we already have cached image bytes (re-injection after
            // VirtualizingStackPanel recycle), build the full card with image in one shot
            if (_imageBytesCache.TryGetValue(url, out var cachedBytes))
            {
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
                    var capturedData = data;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        try
                        {
                            var imgBytes = HttpGetBytes(imageUrl);
                            if (imgBytes == null) return;
                            if (imgBytes.Length > MaxImageBytes || imgBytes.Length < 100)
                            {
                                Logger.Log("LinkEmbed", $"Image skipped (size={imgBytes.Length}): {imageUrl}");
                                return;
                            }
                            // Cache image bytes for future re-injections
                            _imageBytesCache[capturedData.Url] = imgBytes;
                            AddImageToExistingCard(capturedData, imgBytes);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log("LinkEmbed", $"Phase B image error for {url}: {ex.Message}");
                        }
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbed", $"FetchAndInject error for {url}: {ex.Message}");
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

                object? bitmap = null;
                try
                {
                    using var ms = new System.IO.MemoryStream(imageBytes);
                    bitmap = _r.CreateBitmapFromStream(ms);
                }
                catch { }

                var card = BuildEmbedCard(data, bitmap);
                if (card == null) return;

                _r.SetTag(card, embedTag);
                _r.SetMargin(card, 0, 4, 0, 4);

                int newRow = AddAutoRowToGrid(grid);
                _r.SetGridRow(card, newRow);
                _r.SetGridColumn(card, col);
                SetGridColumnSpan(card, colSpan);

                _r.AddChild(grid, card);
                _injectedCards.Add(card);
                Logger.Log("LinkEmbed", $"Embed re-injected (cached) for: {data.Url}");
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

                // Create bitmap on UI thread
                object? bitmap;
                using (var ms = new System.IO.MemoryStream(imageBytes))
                {
                    bitmap = _r.CreateBitmapFromStream(ms);
                }
                if (bitmap == null) return;

                // Get the StackPanel (child of the Border card)
                var content = _r.GetBorderChild(card);
                if (content == null) return;

                // Build image element (with play button overlay for YouTube)
                object? imageElement;
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
                        _r.SetBorderChild(imgBorder, img);
                        imageElement = imgBorder;
                    }
                    else
                    {
                        imageElement = img;
                    }
                }

                if (imageElement != null)
                {
                    _r.AddChild(content, imageElement);
                    Logger.Log("LinkEmbed", $"Image added to embed: {data.Url}");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("LinkEmbed", $"AddImageToExistingCard error: {ex.Message}");
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
                Logger.Log("LinkEmbed", "HttpClient type not found");
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
                    tryAdd?.Invoke(defaultHeaders, new object[] { "User-Agent", "Mozilla/5.0 (compatible; Uprooted/0.2)" });
                }
            }
            catch (Exception ex)
            {
                Logger.Log("LinkEmbed", $"HTTP headers error: {ex.Message}");
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
                }
            }
            catch (Exception ex)
            {
                Logger.Log("LinkEmbed", $"SendAsync resolve error: {ex.Message}");
            }

            // Log all available methods for diagnostics
            var methodNames = httpClientType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.DeclaringType == httpClientType)
                .Select(m => $"{m.Name}({m.GetParameters().Length})")
                .Distinct()
                .OrderBy(n => n);
            Logger.Log("LinkEmbed", $"HttpClient available methods: {string.Join(", ", methodNames)}");

            Logger.Log("LinkEmbed", $"HTTP resolved: client={s_httpClient != null}, " +
                $"GetStringAsync={s_getStringAsync != null}, " +
                $"GetByteArrayAsync={s_getByteArrayAsync != null}, " +
                $"GetAsync={s_getAsync != null}, " +
                $"SendAsync={s_sendAsync != null}, " +
                $"HttpRequestMessage={s_httpRequestMessageType != null}, " +
                $"HttpMethod.Get={s_httpMethodGet != null}");
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbed", $"HTTP resolve error: {ex.Message}");
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
    /// Tries GetByteArrayAsync first, falls back to GetAsync + stream copy.
    /// </summary>
    private static byte[]? HttpGetBytes(string url)
    {
        EnsureHttpResolved();
        if (s_httpClient == null) return null;

        // Try GetByteArrayAsync if available
        if (s_getByteArrayAsync != null)
        {
            try
            {
                var task = s_getByteArrayAsync.Invoke(s_httpClient, new object[] { url });
                if (task != null)
                {
                    var result = task.GetType().GetProperty("Result")?.GetValue(task) as byte[];
                    if (result != null) return result;
                }
            }
            catch { } // Fall through to GetAsync
        }

        // Fallback: GetAsync or SendAsync → response.Content stream → byte array
        try
        {
            object? response = null;

            // Try GetAsync first (may take string or Uri depending on what survived trimming)
            if (s_getAsync != null)
            {
                var paramType = s_getAsync.GetParameters()[0].ParameterType;
                object arg = paramType == typeof(Uri) ? new Uri(url) : (object)url;
                var task = s_getAsync.Invoke(s_httpClient, new[] { arg });
                response = task?.GetType().GetProperty("Result")?.GetValue(task);
            }

            // Deep fallback: SendAsync(new HttpRequestMessage(HttpMethod.Get, url))
            if (response == null && s_sendAsync != null && s_httpRequestMessageType != null && s_httpMethodGet != null)
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

            if (response == null) return null;

            var content = response.GetType().GetProperty("Content")?.GetValue(response);
            if (content == null) return null;

            // ReadAsStreamAsync() → Task<Stream>
            var readTask = content.GetType().GetMethod("ReadAsStreamAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, Type.EmptyTypes, null)?.Invoke(content, null);
            if (readTask == null) return null;

            var stream = readTask.GetType().GetProperty("Result")?.GetValue(readTask) as System.IO.Stream;
            if (stream == null) return null;

            using var ms = new System.IO.MemoryStream();
            stream.CopyTo(ms);
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
    }

    // ===== Metadata fetching =====

    private EmbedData? FetchMetadata(string url)
    {
        if (_metadataCache.TryGetValue(url, out var cached))
            return cached;

        try
        {
            var ytMatch = YoutubeIdRegex.Match(url);
            if (ytMatch.Success)
            {
                var data = FetchYouTubeMetadata(url, ytMatch.Groups[1].Value);
                _metadataCache[url] = data;
                return data;
            }

            var data2 = FetchOgMetadata(url);
            _metadataCache[url] = data2;
            return data2;
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbed", $"FetchMetadata error for {url}: {ex.Message}");
            _metadataCache[url] = null;
            return null;
        }
    }

    private EmbedData? FetchOgMetadata(string url)
    {
        var html = HttpGetString(url);
        if (html == null) return null;

        // Limit to first 50KB for OG parsing
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
        ogTags.TryGetValue("og:site_name", out var siteName);
        ogTags.TryGetValue("theme-color", out var themeColor);

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

        Logger.Log("LinkEmbed", $"OG fetched for {url}: title=\"{title}\"");
        return new EmbedData(url, siteName, null, title, description, image, themeColor, null);
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
                        Logger.Log("LinkEmbed", $"YouTube title from page scrape: \"{title}\"");
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
        Logger.Log("LinkEmbed", $"YouTube fetched: id={videoId}, author=\"{author}\", title=\"{title}\"");
        return new EmbedData(url, "YouTube", author, title, null, thumbnail, "#FF0000", videoId);
    }

    /// <summary>
    /// Decode JSON string escapes: \", \\, \/, \uXXXX
    /// </summary>
    private static string DecodeJsonString(string raw)
    {
        var decoded = raw
            .Replace("\\\"", "\"")
            .Replace("\\\\", "\\")
            .Replace("\\/", "/");
        return Regex.Replace(decoded, @"\\u([0-9a-fA-F]{4})",
            m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString());
    }

    // ===== Component 4: Embed Card Builder =====

    private object? BuildEmbedCard(EmbedData data, object? bitmap = null)
    {
        try
        {
            // Card structure:
            // Border (CardBg, CornerRadius=8, left accent border 3px)
            //   └── StackPanel (vertical, spacing=6, padding=12)
            //       ├── TextBlock (provider, FontSize=11, TextDim)           [if provider]
            //       ├── TextBlock (author, FontSize=12, Bold, White)         [if author]
            //       ├── TextBlock (title, FontSize=13, SemiBold, accent)     [required]
            //       ├── TextBlock (description, FontSize=12, TextMuted)      [if desc]
            //       └── Border/Grid (image + optional play overlay)          [if image]

            var accentHex = data.Color ?? DefaultAccentColor;
            // For generic embeds, use theme-color for title if available; YouTube always uses default blue
            var titleColorHex = (data.VideoId == null && data.Color != null) ? data.Color : DefaultAccentColor;

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
                var authorText = _r.CreateTextBlock(data.Author!, 12, "#FFFFFF", "Bold");
                if (authorText != null)
                    _r.AddChild(content, authorText);
            }

            // Title (required)
            var titleText = _r.CreateTextBlock(data.Title!, 13, titleColorHex, "SemiBold");
            if (titleText != null)
            {
                _r.SetTextWrapping(titleText, "Wrap");
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
            if (bitmap != null)
            {
                object? imageElement;
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
                            _r.SetBorderChild(imgBorder, img);
                            imageElement = imgBorder;
                        }
                        else
                        {
                            imageElement = img;
                        }
                    }
                    else
                    {
                        imageElement = null;
                    }
                }

                if (imageElement != null)
                    _r.AddChild(content, imageElement);
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
                if (grid == null)
                {
                    Logger.Log("LinkEmbed", $"No injection grid for: {data.Url}");
                    return;
                }

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
                _injectedCards.Add(card);

                Logger.Log("LinkEmbed", $"Embed injected for: {data.Url} " +
                    $"into Grid row={newRow} col={col}");
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
        {
            Logger.Log("LinkEmbed", "RootMarkdownTextBlock not found in parent chain");
            return (null, 0, 1);
        }

        // The Grid is the direct parent of RootMarkdownTextBlock
        var grid = _r.GetParent(markdownTextBlock);
        if (grid == null || !_r.IsGrid(grid))
        {
            // Fallback: try one more level up
            var above = _r.GetParent(grid!);
            if (above != null && _r.IsGrid(above))
                grid = above;
            else
            {
                Logger.Log("LinkEmbed", $"Grid not found above RootMarkdownTextBlock (found {grid?.GetType().Name})");
                return (null, 0, 1);
            }
        }

        // Get the column the markdown text is in (usually column 1, column 0 is avatar)
        int col = _r.GetGridColumn(markdownTextBlock);
        // Span all remaining columns
        int colSpan = 99;

        return (grid, col, colSpan);
    }
}
