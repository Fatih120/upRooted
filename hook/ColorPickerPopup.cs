namespace Uprooted;

/// <summary>
/// Swatch-grid color picker popup displayed on the OverlayLayer.
/// 12 hue columns × 9 lightness rows + 1 grayscale row (~130 swatches).
/// Singleton: only one popup open at a time.
/// </summary>
internal static class ColorPickerPopup
{
    private static object? _currentOverlay;
    private static object? _currentBackdrop;
    private static object? _currentPopup;

    // 12 base hues at 30° intervals
    private static readonly int[] Hues = { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 };

    // 9 lightness steps from pastel (top) to dark (bottom)
    private static readonly double[] Lightnesses = { 0.85, 0.75, 0.65, 0.55, 0.45, 0.35, 0.25, 0.20, 0.15 };

    // 12 grayscale values from white to black
    private static readonly string[] Grayscale =
    {
        "#FFFFFF", "#E0E0E0", "#C0C0C0", "#A0A0A0",
        "#888888", "#707070", "#585858", "#404040",
        "#303030", "#202020", "#101010", "#000000"
    };

    /// <summary>
    /// Show the color picker popup near the given swatch control.
    /// When a color is picked, it sets the textBox text (which triggers
    /// the existing TextChanged handler to update the swatch preview).
    /// </summary>
    public static void Show(AvaloniaReflection r, object swatch, object? textBox)
    {
        // Dismiss any existing popup
        Dismiss(r);

        var mainWindow = r.GetMainWindow();
        if (mainWindow == null) return;

        var overlay = r.GetOverlayLayer(mainWindow);
        if (overlay == null) return;

        _currentOverlay = overlay;

        // Get swatch position relative to overlay
        var swatchBounds = r.GetBounds(swatch);
        var translated = r.TranslatePoint(swatch, 0, 0, overlay);
        if (swatchBounds == null || translated == null) return;

        double swatchX = translated.Value.X;
        double swatchY = translated.Value.Y;
        double swatchW = swatchBounds.Value.W;
        double swatchH = swatchBounds.Value.H;

        // Get window bounds for edge clamping
        var windowBounds = r.GetBounds(mainWindow);
        double windowW = windowBounds?.W ?? 800;
        double windowH = windowBounds?.H ?? 600;

        // Popup dimensions: 12 swatches × 21px + padding ≈ 272, 10 rows × 21px + padding ≈ 234
        double popupW = 272;
        double popupH = 234;

        // Position: right of swatch by default, left if near right edge
        double popupX = swatchX + swatchW + 8;
        if (popupX + popupW > windowW - 16)
            popupX = swatchX - popupW - 8;

        // Vertical: center on swatch, clamped to window
        double popupY = swatchY + (swatchH / 2) - (popupH / 2);
        popupY = Math.Max(8, Math.Min(popupY, windowH - popupH - 8));

        // Create backdrop (full-window, nearly transparent — catches clicks to dismiss)
        _currentBackdrop = r.CreateBorder("#01000000", 0);
        if (_currentBackdrop != null)
        {
            r.SetWidth(_currentBackdrop, windowW);
            r.SetHeight(_currentBackdrop, windowH);
            r.SetCanvasPosition(_currentBackdrop, 0, 0);
            r.SetTag(_currentBackdrop, "uprooted-no-recolor");

            r.SubscribeEvent(_currentBackdrop, "PointerPressed", () => Dismiss(r));
            r.AddToOverlay(overlay, _currentBackdrop);
        }

        // Create popup container
        _currentPopup = r.CreateBorder("#1A1A1A", 8);
        if (_currentPopup == null) return;
        r.SetTag(_currentPopup, "uprooted-no-recolor");
        SetBorderStroke(r, _currentPopup, "#40ffffff", 1.0);

        var content = r.CreateStackPanel(vertical: true, spacing: 1);
        if (content == null) return;
        r.SetMargin(content, 6, 6, 6, 6);

        // 9 hue rows
        for (int li = 0; li < Lightnesses.Length; li++)
        {
            var row = r.CreateStackPanel(vertical: false, spacing: 1);
            if (row == null) continue;

            double lightness = Lightnesses[li];
            for (int hi = 0; hi < Hues.Length; hi++)
            {
                string hex = HslToHex(Hues[hi], 1.0, lightness);
                var cell = CreateSwatchCell(r, hex, textBox);
                if (cell != null) r.AddChild(row, cell);
            }

            r.AddChild(content, row);
        }

        // 1 grayscale row
        var grayRow = r.CreateStackPanel(vertical: false, spacing: 1);
        if (grayRow != null)
        {
            foreach (var hex in Grayscale)
            {
                var cell = CreateSwatchCell(r, hex, textBox);
                if (cell != null) r.AddChild(grayRow, cell);
            }
            r.AddChild(content, grayRow);
        }

        r.SetBorderChild(_currentPopup, content);
        r.SetCanvasPosition(_currentPopup, popupX, popupY);
        r.AddToOverlay(overlay, _currentPopup);
    }

    public static void Dismiss(AvaloniaReflection r)
    {
        if (_currentOverlay == null) return;

        if (_currentBackdrop != null)
        {
            r.RemoveFromOverlay(_currentOverlay, _currentBackdrop);
            _currentBackdrop = null;
        }
        if (_currentPopup != null)
        {
            r.RemoveFromOverlay(_currentOverlay, _currentPopup);
            _currentPopup = null;
        }

        _currentOverlay = null;
    }

    private static object? CreateSwatchCell(AvaloniaReflection r, string hex, object? textBox)
    {
        var cell = r.CreateBorder(hex, 2);
        if (cell == null) return null;

        r.SetWidth(cell, 20);
        r.SetHeight(cell, 20);
        r.SetCursorHand(cell);

        // Click: set text and dismiss
        r.SubscribeEvent(cell, "PointerPressed", () =>
        {
            if (textBox != null)
                r.SetTextBoxText(textBox, hex);
            Dismiss(r);
        });

        // Hover: white border on enter, clear on exit
        r.SubscribeEvent(cell, "PointerEntered", () =>
            SetBorderStroke(r, cell, "#FFFFFF", 1.5));
        r.SubscribeEvent(cell, "PointerExited", () =>
            SetBorderStroke(r, cell, "#00000000", 0));

        return cell;
    }

    /// <summary>
    /// Convert HSL to hex color string. Standard algorithm.
    /// h: 0-360, s: 0-1, l: 0-1
    /// </summary>
    private static string HslToHex(double h, double s, double l)
    {
        double c = (1 - Math.Abs(2 * l - 1)) * s;
        double x = c * (1 - Math.Abs((h / 60.0) % 2 - 1));
        double m = l - c / 2;

        double r1, g1, b1;
        if (h < 60)       { r1 = c; g1 = x; b1 = 0; }
        else if (h < 120) { r1 = x; g1 = c; b1 = 0; }
        else if (h < 180) { r1 = 0; g1 = c; b1 = x; }
        else if (h < 240) { r1 = 0; g1 = x; b1 = c; }
        else if (h < 300) { r1 = x; g1 = 0; b1 = c; }
        else               { r1 = c; g1 = 0; b1 = x; }

        byte rb = (byte)Math.Round((r1 + m) * 255);
        byte gb = (byte)Math.Round((g1 + m) * 255);
        byte bb = (byte)Math.Round((b1 + m) * 255);

        return $"#{rb:X2}{gb:X2}{bb:X2}";
    }

    private static void SetBorderStroke(AvaloniaReflection r, object? border, string hex, double width)
    {
        if (border == null) return;
        var brush = r.CreateBrush(hex);
        border.GetType().GetProperty("BorderBrush")?.SetValue(border, brush);

        if (r.ThicknessType != null)
        {
            var thickness = Activator.CreateInstance(r.ThicknessType, width, width, width, width);
            border.GetType().GetProperty("BorderThickness")?.SetValue(border, thickness);
        }
    }
}
