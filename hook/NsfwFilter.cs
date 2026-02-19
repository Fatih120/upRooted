using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace Uprooted;

/// <summary>
/// Avalonia-native NSFW content filter.
/// Scans the visual tree every 500ms for Image controls, classifies them via the
/// Google Vision SAFE_SEARCH_DETECTION API in C#, and injects a native Avalonia
/// overlay to block NSFW content. No DotNetBrowser dependency.
/// </summary>
internal class NsfwFilter : IDisposable
{
    private const int ScanIntervalMs = 500;
    private const int MinImagePixels = 100;       // skip avatars below 100×100
    private const int MaxConcurrentApiCalls = 3;

    private readonly AvaloniaReflection _r;
    private readonly UprootedSettings _settings;
    private readonly object _mainWindow;

    private readonly SemaphoreSlim _apiSemaphore = new(MaxConcurrentApiCalls, MaxConcurrentApiCalls);

    // imageId (object identity) → bitmapId — deduplicates per-bitmap API calls
    private readonly Dictionary<int, int> _seenImages = new();

    // overlay control → (parent panel, hidden image) — for reveal-all cleanup
    private readonly Dictionary<object, (object panel, object imageControl)> _overlayControls = new();

    private readonly object _lock = new();
    private Timer? _scanTimer;
    private int _scanPosted;  // Interlocked: 1 if a scan is already queued on the UI thread
    private bool _disposed;

    internal NsfwFilter(AvaloniaReflection r, UprootedSettings settings, object mainWindow)
    {
        _r = r;
        _settings = settings;
        _mainWindow = mainWindow;
    }

    /// <summary>
    /// Start the scan timer. Call from a background thread.
    /// </summary>
    internal void Initialize()
    {
        _scanTimer = new Timer(OnScanTick, null, ScanIntervalMs, ScanIntervalMs);
        Logger.Log("NsfwFilter", "Scan timer started (500ms interval)");
    }

    private void OnScanTick(object? state)
    {
        if (_disposed) return;
        if (!_settings.NsfwFilterEnabled || string.IsNullOrEmpty(_settings.NsfwApiKey)) return;

        // Only queue one scan at a time — avoids UI thread backlog if scan is slow
        if (Interlocked.CompareExchange(ref _scanPosted, 1, 0) != 0) return;

        try
        {
            _r.RunOnUIThread(() =>
            {
                try { ScanForImages(); }
                finally { Interlocked.Exchange(ref _scanPosted, 0); }
            });
        }
        catch (Exception ex)
        {
            Interlocked.Exchange(ref _scanPosted, 0);
            Logger.Log("NsfwFilter", $"OnScanTick error: {ex.Message}");
        }
    }

    /// <summary>
    /// DFS walk from mainWindow; classify each unprocessed Image control.
    /// Must run on the UI thread.
    /// </summary>
    private void ScanForImages()
    {
        try
        {
            if (_r.ImageType == null) return;
            var walker = new VisualTreeWalker(_r);

            foreach (var node in walker.DescendantsDepthFirst(_mainWindow))
            {
                if (node.GetType() != _r.ImageType) continue;

                // Skip controls already tagged by us
                var tag = _r.GetTag(node);
                if (tag != null && tag.StartsWith("uprooted-nsfw-")) continue;

                // Get Image.Source (the Bitmap)
                var source = node.GetType().GetProperty("Source")?.GetValue(node);
                if (source == null) continue;

                // Skip tiny images (avatars, icons)
                if (!IsLargeEnough(source)) continue;

                int imageId  = RuntimeHelpers.GetHashCode(node);
                int bitmapId = RuntimeHelpers.GetHashCode(source);

                // Skip if we already processed this exact bitmap on this control
                lock (_lock)
                {
                    if (_seenImages.TryGetValue(imageId, out int lastBitmapId) && lastBitmapId == bitmapId)
                        continue;
                    _seenImages[imageId] = bitmapId;
                }

                // Mark as in-progress so the next scan tick skips it
                _r.SetTag(node, "uprooted-nsfw-checking");

                var capturedNode   = node;
                var capturedSource = source;
                ThreadPool.QueueUserWorkItem(_ => ClassifyAndAct(capturedNode, capturedSource));
            }
        }
        catch (Exception ex)
        {
            Logger.Log("NsfwFilter", $"ScanForImages error: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns true if the bitmap's PixelSize is at least MinImagePixels × MinImagePixels.
    /// Defaults to true if PixelSize cannot be read (conservative — classify unknown sizes).
    /// </summary>
    private static bool IsLargeEnough(object bitmap)
    {
        try
        {
            var psVal = bitmap.GetType().GetProperty("PixelSize")?.GetValue(bitmap);
            if (psVal == null) return true;
            var w = psVal.GetType().GetProperty("Width")?.GetValue(psVal)  as int? ?? 0;
            var h = psVal.GetType().GetProperty("Height")?.GetValue(psVal) as int? ?? 0;
            return w >= MinImagePixels && h >= MinImagePixels;
        }
        catch { return true; }
    }

    /// <summary>
    /// Background thread: encode bitmap → call Vision API → inject overlay or tag clean.
    /// </summary>
    private void ClassifyAndAct(object imageControl, object bitmap)
    {
        _apiSemaphore.Wait();
        try
        {
            byte[]? png = GetBitmapBytes(bitmap);
            if (png == null || png.Length == 0)
            {
                // Encoding failed — untag so it can be retried
                _r.RunOnUIThread(() => _r.SetTag(imageControl, ""));
                lock (_lock) { _seenImages.Remove(RuntimeHelpers.GetHashCode(imageControl)); }
                return;
            }

            string requestJson = BuildVisionRequest(png);
            string? response   = PostVisionApi(requestJson);

            if (response == null)
            {
                // API error — untag and remove from seen so it's retried next scan
                _r.RunOnUIThread(() => _r.SetTag(imageControl, ""));
                lock (_lock) { _seenImages.Remove(RuntimeHelpers.GetHashCode(imageControl)); }
                return;
            }

            bool nsfw = IsNsfw(response, _settings.NsfwThreshold);
            Logger.Log("NsfwFilter", $"Classified image (id={RuntimeHelpers.GetHashCode(imageControl)}): nsfw={nsfw}");

            _r.RunOnUIThread(() =>
            {
                if (nsfw)
                    InjectOverlay(imageControl);
                else
                    _r.SetTag(imageControl, "uprooted-nsfw-clean");
            });
        }
        catch (Exception ex)
        {
            Logger.Log("NsfwFilter", $"ClassifyAndAct error: {ex.Message}");
            _r.RunOnUIThread(() => _r.SetTag(imageControl, ""));
        }
        finally
        {
            _apiSemaphore.Release();
        }
    }

    /// <summary>
    /// Encode an Avalonia Bitmap to PNG bytes via Bitmap.Save(Stream).
    /// </summary>
    private static byte[]? GetBitmapBytes(object bitmap)
    {
        try
        {
            var saveMethod = bitmap.GetType().GetMethod("Save", new[] { typeof(Stream) });
            if (saveMethod == null)
            {
                Logger.Log("NsfwFilter", "Bitmap.Save(Stream) not found");
                return null;
            }
            using var ms = new MemoryStream();
            saveMethod.Invoke(bitmap, new object[] { ms });
            return ms.ToArray();
        }
        catch (Exception ex)
        {
            Logger.Log("NsfwFilter", $"GetBitmapBytes error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Build the Google Vision API SAFE_SEARCH_DETECTION request JSON.
    /// Manual construction — no System.Text.Json.
    /// </summary>
    private static string BuildVisionRequest(byte[] png)
    {
        var b64 = Convert.ToBase64String(png);
        return $"{{\"requests\":[{{\"image\":{{\"content\":\"{b64}\"}},\"features\":[{{\"type\":\"SAFE_SEARCH_DETECTION\"}}]}}]}}";
    }

    /// <summary>
    /// POST to Vision API and return the raw response body, or null on error.
    /// </summary>
    private string? PostVisionApi(string requestJson)
    {
        try
        {
            // Strip quotes from EscapeJsonString output to get the raw key
            var rawKey = EscapeJsonString(_settings.NsfwApiKey).Trim('"');
            var url    = $"https://vision.googleapis.com/v1/images:annotate?key={rawKey}";

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method      = "POST";
            req.ContentType = "application/json";
            req.Timeout     = 30_000;

            var body = Encoding.UTF8.GetBytes(requestJson);
            req.ContentLength = body.Length;

            using (var stream = req.GetRequestStream())
                stream.Write(body, 0, body.Length);

            using var resp   = (HttpWebResponse)req.GetResponse();
            using var reader = new System.IO.StreamReader(resp.GetResponseStream());
            return reader.ReadToEnd();
        }
        catch (WebException ex)
        {
            Logger.Log("NsfwFilter", $"Vision API error: {ex.Status} — {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Logger.Log("NsfwFilter", $"PostVisionApi error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Map threshold (0.0–1.0) to Vision API likelihood scale (1–5) and test adult/racy scores.
    /// </summary>
    private static bool IsNsfw(string response, double threshold)
    {
        int threshInt = (int)Math.Round(threshold * 5);
        if (threshInt < 1) threshInt = 1;
        if (threshInt > 5) threshInt = 5;

        int adult = ParseLikelihood(response, "adult");
        int racy  = ParseLikelihood(response, "racy");

        return adult >= threshInt || racy >= threshInt;
    }

    /// <summary>
    /// Extract a Vision API likelihood string from the response JSON without a JSON parser.
    /// Handles both "field":"VALUE" and "field": "VALUE" formatting.
    /// Returns 0 if the field is not found or unrecognised.
    /// </summary>
    private static int ParseLikelihood(string response, string field)
    {
        string? value = null;

        // Try compact form first, then with a space after the colon
        foreach (var key in new[] { $"\"{field}\":\"", $"\"{field}\": \"" })
        {
            int idx = response.IndexOf(key, StringComparison.OrdinalIgnoreCase);
            if (idx < 0) continue;

            idx += key.Length;
            int end = response.IndexOf('"', idx);
            if (end > idx)
            {
                value = response.Substring(idx, end - idx);
                break;
            }
        }

        return value?.ToUpperInvariant() switch
        {
            "VERY_UNLIKELY" => 1,
            "UNLIKELY"      => 2,
            "POSSIBLE"      => 3,
            "LIKELY"        => 4,
            "VERY_LIKELY"   => 5,
            _               => 0
        };
    }

    /// <summary>
    /// Hide imageControl and inject a "⚠ NSFW — Click to reveal" overlay into the nearest Panel ancestor.
    /// Must run on the UI thread.
    /// </summary>
    private void InjectOverlay(object imageControl)
    {
        // Walk up max 5 levels to find a Panel ancestor we can inject into
        object? panel   = null;
        var     current = _r.GetParent(imageControl);
        for (int i = 0; i < 5 && current != null; i++)
        {
            if (_r.IsPanel(current))
            {
                panel = current;
                break;
            }
            current = _r.GetParent(current);
        }

        // Hide the original image regardless of whether we find a panel
        _r.SetIsVisible(imageControl, false);
        _r.SetTag(imageControl, "uprooted-nsfw-blocked");

        if (panel == null)
        {
            Logger.Log("NsfwFilter", "No Panel ancestor found — image hidden without overlay");
            return;
        }

        // Build overlay UI: Border > StackPanel > [⚠ NSFW label + hint]
        var label = _r.CreateTextBlock("⚠ NSFW", 16, "#FFFFFF", "Bold");
        var hint  = _r.CreateTextBlock("Click to reveal", 12, "#CCCCCC");

        var stack = _r.CreateStackPanel(vertical: true, spacing: 4);
        if (label != null) _r.AddChild(stack, label);
        if (hint  != null) _r.AddChild(stack, hint);
        _r.SetHorizontalAlignment(stack, "Center");
        _r.SetVerticalAlignment(stack, "Center");

        var overlay = _r.CreateBorder("#CC000000", cornerRadius: 4, child: stack);
        if (overlay == null)
        {
            Logger.Log("NsfwFilter", "Failed to create overlay Border");
            return;
        }

        _r.SetTag(overlay, "uprooted-nsfw-overlay");
        _r.SetPadding(overlay, 12, 8, 12, 8);
        _r.SetHorizontalAlignment(overlay, "Center");
        _r.SetVerticalAlignment(overlay, "Center");
        _r.SetCursorHand(overlay);

        // Mirror Grid.Row / Grid.Column from the image so the overlay lands in the same cell
        if (_r.IsGrid(panel))
        {
            _r.SetGridColumn(overlay, _r.GetGridColumn(imageControl));
            _r.SetGridRow(overlay,    _r.GetGridRow(imageControl));
        }

        _r.AddChild(panel, overlay);

        // Click-to-reveal: restore image and remove the overlay
        var capturedImage   = imageControl;
        var capturedPanel   = panel;
        var capturedOverlay = overlay;
        _r.SubscribePointerEvent(overlay, "PointerPressed", (_, _) =>
        {
            _r.SetIsVisible(capturedImage, true);
            _r.SetTag(capturedImage, "uprooted-nsfw-revealed");
            _r.RemoveChild(capturedPanel, capturedOverlay);
            lock (_lock) { _overlayControls.Remove(capturedOverlay); }
            Logger.Log("NsfwFilter", "NSFW overlay dismissed by user click");
        });

        lock (_lock)
        {
            _overlayControls[overlay] = (panel, imageControl);
        }

        Logger.Log("NsfwFilter", "NSFW overlay injected");
    }

    /// <summary>
    /// Called by ContentPages when NSFW settings change.
    /// If the filter has been disabled, all current overlays are removed immediately.
    /// If re-enabled, the scan timer auto-resumes on the next tick.
    /// </summary>
    internal void UpdateConfig()
    {
        if (!_settings.NsfwFilterEnabled)
            _r.RunOnUIThread(RevealAllBlocked);
    }

    /// <summary>
    /// Remove all active overlays and restore the hidden images. Runs on UI thread.
    /// Clears _seenImages so images are re-classified if the filter is re-enabled.
    /// </summary>
    private void RevealAllBlocked()
    {
        List<(object overlay, object panel, object imageControl)> toReveal;

        lock (_lock)
        {
            toReveal = _overlayControls
                .Select(kv => (kv.Key, kv.Value.panel, kv.Value.imageControl))
                .ToList();
            _overlayControls.Clear();
            _seenImages.Clear();
        }

        foreach (var (overlay, panel, imageControl) in toReveal)
        {
            try
            {
                _r.RemoveChild(panel, overlay);
                _r.SetIsVisible(imageControl, true);
                _r.SetTag(imageControl, "");
            }
            catch (Exception ex)
            {
                Logger.Log("NsfwFilter", $"RevealAllBlocked error: {ex.Message}");
            }
        }

        Logger.Log("NsfwFilter", $"Revealed {toReveal.Count} blocked image(s)");
    }

    /// <summary>
    /// Escape a string for inclusion in a JSON string literal.
    /// No System.Text.Json allowed in profiler context.
    /// </summary>
    private static string EscapeJsonString(string s)
    {
        if (string.IsNullOrEmpty(s)) return "\"\"";
        var sb = new StringBuilder("\"", s.Length + 2);
        foreach (var c in s)
        {
            switch (c)
            {
                case '"':  sb.Append("\\\""); break;
                case '\\': sb.Append("\\\\"); break;
                case '\n': sb.Append("\\n");  break;
                case '\r': sb.Append("\\r");  break;
                case '\t': sb.Append("\\t");  break;
                default:   sb.Append(c);      break;
            }
        }
        sb.Append('"');
        return sb.ToString();
    }

    public void Dispose()
    {
        _disposed = true;
        _scanTimer?.Dispose();
        _scanTimer = null;
        _apiSemaphore.Dispose();
    }
}
