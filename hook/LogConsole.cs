namespace Uprooted;

/// <summary>
/// Dev-only plugin: floating overlay console that shows live log output in real-time.
/// Subscribes to Logger.OnLine for zero-latency streaming. Backfills last 100 lines
/// from the log file on Enable. Auto-scrolls to bottom unless user has scrolled up.
///
/// Toggle: Enable/Disable from plugin settings (dev channel only, live toggle).
/// </summary>
internal static class LogConsole
{
    private const int MaxLines = 500;
    private const int BackfillLines = 100;
    private const double PanelW = 800;
    private const double PanelH = 500;
    private const double Margin = 16;
    private const string BgColor = "#E61A1A2E";    // dark navy, slight transparency
    private const string HeaderBg = "#FF12122A";
    private const string TextNormal = "#B0B0B0";
    private const string TextWide = "#7289DA";      // accent blue for wide events
    private const string TextError = "#ED4245";     // red for errors
    private const string TextMuted = "#666680";
    private const string AccentBlue = "#5865F2";

    private static AvaloniaReflection? _r;
    private static object? _mainWindow;
    private static bool _initialized;
    private static bool _enabled;

    // Overlay UI references
    private static object? _overlay;
    private static object? _panel;
    private static object? _scrollViewer;
    private static object? _linesPanel;      // StackPanel holding TextBlock per line
    private static int _lineCount;

    internal static void Init(AvaloniaReflection r, object mainWindow)
    {
        if (_initialized) return;
        _r = r;
        _mainWindow = mainWindow;
        _initialized = true;
    }

    internal static void Enable()
    {
        if (!_initialized || _enabled || _r == null || _mainWindow == null) return;
        _enabled = true;

        _r.RunOnUIThread(() =>
        {
            try
            {
                ShowPanel();
                BackfillFromFile();
                Logger.OnLine = OnLogLine;
            }
            catch (Exception ex)
            {
                Logger.Log("LogConsole", $"Enable error: {ex.Message}");
            }
        });
    }

    internal static void Disable()
    {
        if (!_enabled) return;
        _enabled = false;
        Logger.OnLine = null;

        _r?.RunOnUIThread(() =>
        {
            try { HidePanel(); }
            catch (Exception ex) { Logger.Log("LogConsole", $"Disable error: {ex.Message}"); }
        });
    }

    internal static void Toggle()
    {
        if (_enabled) Disable();
        else Enable();
    }

    // ===== Logger callback =====

    private static void OnLogLine(string line)
    {
        if (!_enabled || _r == null) return;

        // Logger fires from any thread — dispatch to UI thread
        _r.RunOnUIThread(() =>
        {
            try { AppendLine(line); }
            catch { }
        });
    }

    // ===== UI construction =====

    private static void ShowPanel()
    {
        if (_r == null || _mainWindow == null) return;

        var overlay = _r.GetOverlayLayer(_mainWindow);
        if (overlay == null) return;
        _overlay = overlay;

        // Get window bounds for positioning
        var windowBounds = _r.GetBounds(_mainWindow);
        double windowW = windowBounds?.W ?? 1280;
        double windowH = windowBounds?.H ?? 800;

        // Main panel container
        _panel = _r.CreateBorder(BgColor, 8);
        if (_panel == null) return;
        _r.SetWidth(_panel, PanelW);
        _r.SetHeight(_panel, PanelH);
        _r.SetClipToBounds(_panel, true);

        // Position: bottom-right with margin
        double panelX = windowW - PanelW - Margin;
        double panelY = windowH - PanelH - Margin;
        _r.SetCanvasPosition(_panel, Math.Max(Margin, panelX), Math.Max(Margin, panelY));

        // Build content: header + scrollable log area
        var outerStack = _r.CreateStackPanel(vertical: true, spacing: 0);
        if (outerStack == null) return;

        // Header row
        var header = BuildHeader();
        if (header != null) _r.AddChild(outerStack, header);

        // Log lines area: ScrollViewer wrapping a StackPanel
        _linesPanel = _r.CreateStackPanel(vertical: true, spacing: 0);
        _lineCount = 0;

        _scrollViewer = _r.CreateScrollViewer(_linesPanel);
        if (_scrollViewer != null)
        {
            _r.SetHeight(_scrollViewer, PanelH - 36); // minus header height
            // Enable vertical scrollbar
            try
            {
                var visType = _scrollViewer.GetType().Assembly
                    .GetType("Avalonia.Controls.Primitives.ScrollBarVisibility");
                if (visType != null)
                    _scrollViewer.GetType().GetProperty("VerticalScrollBarVisibility")
                        ?.SetValue(_scrollViewer, Enum.Parse(visType, "Auto"));
            }
            catch { }
            _r.AddChild(outerStack, _scrollViewer);
        }

        _r.SetBorderChild(_panel, outerStack);
        _r.AddToOverlay(overlay, _panel);

        Logger.Log("LogConsole", "Console panel opened");
    }

    private static object? BuildHeader()
    {
        if (_r == null) return null;

        var headerBorder = _r.CreateBorder(HeaderBg, 0);
        if (headerBorder == null) return null;
        _r.SetHeight(headerBorder, 36);

        var row = _r.CreateStackPanel(vertical: false, spacing: 12);
        if (row == null) return headerBorder;
        _r.SetMargin(row, 12, 8, 12, 8);
        _r.SetVerticalAlignment(row, "Center");

        // Title
        var title = _r.CreateTextBlock("Log Console", 13, "#FFFFFF");
        if (title != null)
        {
            _r.SetFontWeight(title, "Bold");
            _r.AddChild(row, title);
        }

        // Spacer — push buttons to right (use a wide empty TextBlock)
        var spacer = _r.CreateTextBlock("", 1, "#00000000");
        if (spacer != null)
        {
            spacer.GetType().GetProperty("Width")?.SetValue(spacer, PanelW - 200);
            _r.AddChild(row, spacer);
        }

        // Clear button
        var clearBtn = _r.CreateTextBlock("[Clear]", 11, AccentBlue);
        if (clearBtn != null)
        {
            _r.SetCursorHand(clearBtn);
            _r.SubscribeEvent(clearBtn, "PointerPressed", () => ClearLines());
            _r.AddChild(row, clearBtn);
        }

        // Close button
        var closeBtn = _r.CreateTextBlock("[X]", 11, TextError);
        if (closeBtn != null)
        {
            _r.SetCursorHand(closeBtn);
            _r.SubscribeEvent(closeBtn, "PointerPressed", () => Disable());
            _r.AddChild(row, closeBtn);
        }

        _r.SetBorderChild(headerBorder, row);
        return headerBorder;
    }

    // ===== Line management =====

    private static void AppendLine(string line)
    {
        if (_r == null || _linesPanel == null) return;

        // Determine color based on content
        string color;
        if (line.Contains("error=") || line.Contains("error_msg=") || line.Contains("FAILED"))
            color = TextError;
        else if (line.Contains('|') && line.Contains("dur_ms="))
            color = TextWide;
        else
            color = TextNormal;

        var tb = _r.CreateTextBlock(line, 11, color);
        if (tb == null) return;
        _r.SetMargin(tb, 8, 1, 8, 1);

        // Set monospace font
        try
        {
            var ffType = tb.GetType().Assembly.GetType("Avalonia.Media.FontFamily");
            if (ffType != null)
            {
                var ff = Activator.CreateInstance(ffType, "Cascadia Mono, Consolas, Courier New, monospace");
                tb.GetType().GetProperty("FontFamily")?.SetValue(tb, ff);
            }
        }
        catch { }

        // Text wrapping
        try
        {
            var wrapType = tb.GetType().Assembly.GetType("Avalonia.Media.TextWrapping");
            if (wrapType != null)
                tb.GetType().GetProperty("TextWrapping")?.SetValue(tb, Enum.Parse(wrapType, "Wrap"));
        }
        catch { }

        _r.AddChild(_linesPanel, tb);
        _lineCount++;

        // Cap lines: remove oldest when exceeded
        if (_lineCount > MaxLines)
        {
            try
            {
                var children = _r.GetChildren(_linesPanel);
                if (children != null && children.Count > 0)
                {
                    children.RemoveAt(0);
                    _lineCount--;
                }
            }
            catch { }
        }

        // Auto-scroll to bottom
        ScrollToBottom();
    }

    private static void ClearLines()
    {
        if (_r == null || _linesPanel == null) return;
        try
        {
            var children = _r.GetChildren(_linesPanel);
            if (children != null)
            {
                for (int i = children.Count - 1; i >= 0; i--)
                    children.RemoveAt(i);
            }
            _lineCount = 0;
        }
        catch { }
    }

    private static void ScrollToBottom()
    {
        if (_scrollViewer == null) return;
        try
        {
            // ScrollViewer.ScrollToEnd() or set Offset to (0, Extent.Height)
            var scrollToEnd = _scrollViewer.GetType().GetMethod("ScrollToEnd");
            if (scrollToEnd != null)
            {
                scrollToEnd.Invoke(_scrollViewer, null);
                return;
            }

            // Fallback: set Offset.Y to max
            var extentProp = _scrollViewer.GetType().GetProperty("Extent");
            var offsetProp = _scrollViewer.GetType().GetProperty("Offset");
            if (extentProp != null && offsetProp != null)
            {
                var extent = extentProp.GetValue(_scrollViewer);
                if (extent != null)
                {
                    var heightProp = extent.GetType().GetProperty("Height");
                    var height = heightProp?.GetValue(extent);
                    if (height is double h)
                    {
                        var vecType = offsetProp.PropertyType;
                        var vec = Activator.CreateInstance(vecType, 0.0, h);
                        offsetProp.SetValue(_scrollViewer, vec);
                    }
                }
            }
        }
        catch { }
    }

    // ===== Backfill =====

    private static void BackfillFromFile()
    {
        try
        {
            var logPath = Logger.GetLogPath();
            if (!File.Exists(logPath)) return;

            var allLines = File.ReadAllLines(logPath);
            int start = Math.Max(0, allLines.Length - BackfillLines);
            for (int i = start; i < allLines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(allLines[i]))
                    AppendLine(allLines[i]);
            }
        }
        catch (Exception ex)
        {
            Logger.Log("LogConsole", $"Backfill error: {ex.Message}");
        }
    }

    // ===== Teardown =====

    private static void HidePanel()
    {
        if (_overlay != null && _panel != null && _r != null)
        {
            _r.RemoveFromOverlay(_overlay, _panel);
        }
        _overlay = null;
        _panel = null;
        _scrollViewer = null;
        _linesPanel = null;
        _lineCount = 0;
    }
}
