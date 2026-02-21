using System.Reflection;

namespace Uprooted;

/// <summary>
/// Translate configuration popup — displayed above the compose bar when the
/// translate button is left-clicked.
///
/// Structure:
///   OverlayLayer
///     Backdrop (full-window transparent click-to-dismiss)
///     Popup (Border #2B2D31, cornerRadius=10, border=1px #3F4147)
///       StackPanel
///         Header row: "Translate" title + × dismiss
///         Separator
///         Label + ComboBox: received from language
///         Label + ComboBox: received to language
///         Label + ComboBox: sent from language
///         Label + ComboBox: sent to language
///         Separator
///         Auto-translate toggle row
///
/// Positioning: above the compose bar (popup floats up from button location).
/// Follows ColorPickerPopup.cs exactly for overlay/backdrop/dismiss patterns.
/// </summary>
internal static class TranslateConfigPopup
{
    private static object? _currentOverlay;
    private static object? _currentBackdrop;
    private static object? _currentPopup;
    private static TranslateEngine? _currentEngine;

    // Popup dimensions
    private const double POPUP_W       = 340;
    private const double POPUP_PADDING = 20;
    private const double SECTION_GAP   = 12;
    private const double COMBO_H       = 38;

    // Discord-dark popup colors (match ColorPickerPopup exactly)
    private const string POPUP_BG      = "#2B2D31";
    private const string POPUP_BORDER  = "#3F4147";
    private const string INPUT_BG      = "#1E1F22";
    private const string TEXT_COLOR    = "#B5BAC1";
    private const string TEXT_BRIGHT   = "#F2F3F5";
    private const string SEPARATOR_CLR = "#3F4147";

    /// <summary>True while the popup is open.</summary>
    public static bool IsOpen => _currentPopup != null;

    // Language list: (code, display name)
    private static readonly (string Code, string Name)[] Languages =
    {
        ("auto", "Detect language"),
        ("en",   "English"),
        ("es",   "Spanish"),
        ("fr",   "French"),
        ("de",   "German"),
        ("pt",   "Portuguese"),
        ("it",   "Italian"),
        ("ru",   "Russian"),
        ("zh-CN","Chinese (Simplified)"),
        ("zh-TW","Chinese (Traditional)"),
        ("ja",   "Japanese"),
        ("ko",   "Korean"),
        ("ar",   "Arabic"),
        ("hi",   "Hindi"),
        ("nl",   "Dutch"),
        ("pl",   "Polish"),
        ("tr",   "Turkish"),
        ("sv",   "Swedish"),
        ("da",   "Danish"),
        ("no",   "Norwegian"),
        ("fi",   "Finnish"),
        ("uk",   "Ukrainian"),
        ("vi",   "Vietnamese"),
        ("th",   "Thai"),
        ("id",   "Indonesian"),
    };

    /// <summary>Show the translate configuration popup near the given button.</summary>
    public static void Show(AvaloniaReflection r, object button,
        ThemeEngine? themeEngine, TranslateEngine? engine)
    {
        Dismiss(r);

        var mainWindow = r.GetMainWindow();
        if (mainWindow == null) return;

        var overlay = r.GetOverlayLayer(mainWindow);
        if (overlay == null) return;

        _currentOverlay = overlay;
        _currentEngine  = engine;

        var settings = UprootedSettings.Load();

        // ── Positioning: centered on button, above the compose bar ──
        var buttonBounds   = r.GetBounds(button);
        var buttonPos      = r.TranslatePoint(button, 0, 0, overlay);
        var windowBounds   = r.GetBounds(mainWindow);

        double windowW = windowBounds?.W ?? 1200;
        double windowH = windowBounds?.H ?? 800;

        // Estimate popup height:
        //   header(40) + sep(1) + 4×(label(20)+combo(38)+gap(8)) + sep(1) + toggleRow(48) + padding(40)
        double popupH = 40 + 1 + 4 * (20 + COMBO_H + SECTION_GAP / 2) + 1 + 52 + POPUP_PADDING * 2 + SECTION_GAP * 6;

        double buttonX = buttonPos?.X ?? (windowW / 2);
        double buttonY = buttonPos?.Y ?? (windowH - 60);
        double buttonW = buttonBounds?.W ?? 32;

        // Center horizontally on button, float above it
        double popupX = buttonX + buttonW / 2 - POPUP_W / 2;
        popupX = Math.Clamp(popupX, 8, windowW - POPUP_W - 8);

        double popupY = buttonY - popupH - 8;
        popupY = Math.Max(8, popupY);

        // ── Backdrop (click to dismiss) ──
        _currentBackdrop = r.CreateBorder("#01000000", 0);
        if (_currentBackdrop != null)
        {
            r.SetWidth(_currentBackdrop, windowW);
            r.SetHeight(_currentBackdrop, windowH);
            r.SetCanvasPosition(_currentBackdrop, 0, 0);
            r.SetTag(_currentBackdrop, "uprooted-no-recolor");
            r.SubscribeEvent(_currentBackdrop, "PointerReleased", () => Dismiss(r));
            r.AddToOverlay(overlay, _currentBackdrop);
        }

        // ── Popup container ──
        _currentPopup = r.CreateBorder(POPUP_BG, 10);
        if (_currentPopup == null) return;

        r.SetTag(_currentPopup, "uprooted-no-recolor");
        SetBorderStroke(r, _currentPopup, POPUP_BORDER, 1.0);
        r.SetCanvasPosition(_currentPopup, popupX, popupY);
        r.SetWidth(_currentPopup, POPUP_W);
        r.AddToOverlay(overlay, _currentPopup);

        // ── Content StackPanel ──
        var content = r.CreateStackPanel(vertical: true, spacing: SECTION_GAP);
        if (content == null) return;
        r.SetMargin(content, POPUP_PADDING, POPUP_PADDING, POPUP_PADDING, POPUP_PADDING);
        r.SetTag(content, "uprooted-no-recolor");

        // Header row
        var header = BuildHeaderRow(r);
        if (header != null) r.AddChild(content, header);

        // Separator
        var sep1 = BuildSeparator(r);
        if (sep1 != null) r.AddChild(content, sep1);

        // Received from
        var rxFromLabel = r.CreateTextBlock("Language received messages translated from", 12, TEXT_COLOR, "Normal");
        if (rxFromLabel != null) { r.SetTag(rxFromLabel, "uprooted-no-recolor"); r.AddChild(content, rxFromLabel); }
        var rxFromCombo = BuildLanguageCombo(r, settings.TranslateFromLang, (code) =>
        {
            settings.TranslateFromLang = code;
            settings.Save();
        });
        if (rxFromCombo != null) r.AddChild(content, rxFromCombo);

        // Received to
        var rxToLabel = r.CreateTextBlock("Language received messages translated to", 12, TEXT_COLOR, "Normal");
        if (rxToLabel != null) { r.SetTag(rxToLabel, "uprooted-no-recolor"); r.AddChild(content, rxToLabel); }
        var rxToCombo = BuildLanguageCombo(r, settings.TranslateToLang, (code) =>
        {
            settings.TranslateToLang = code;
            settings.Save();
        });
        if (rxToCombo != null) r.AddChild(content, rxToCombo);

        // Sent from
        var txFromLabel = r.CreateTextBlock("Language your own messages translated from", 12, TEXT_COLOR, "Normal");
        if (txFromLabel != null) { r.SetTag(txFromLabel, "uprooted-no-recolor"); r.AddChild(content, txFromLabel); }
        var txFromCombo = BuildLanguageCombo(r, settings.TranslateSendFromLang, (code) =>
        {
            settings.TranslateSendFromLang = code;
            settings.Save();
        });
        if (txFromCombo != null) r.AddChild(content, txFromCombo);

        // Sent to
        var txToLabel = r.CreateTextBlock("Language your own messages translated to", 12, TEXT_COLOR, "Normal");
        if (txToLabel != null) { r.SetTag(txToLabel, "uprooted-no-recolor"); r.AddChild(content, txToLabel); }
        var txToCombo = BuildLanguageCombo(r, settings.TranslateSendToLang, (code) =>
        {
            settings.TranslateSendToLang = code;
            settings.Save();
        });
        if (txToCombo != null) r.AddChild(content, txToCombo);

        // Separator
        var sep2 = BuildSeparator(r);
        if (sep2 != null) r.AddChild(content, sep2);

        // AutoTranslate toggle row
        var toggleRow = BuildAutoTranslateRow(r, settings, engine);
        if (toggleRow != null) r.AddChild(content, toggleRow);

        r.SetBorderChild(_currentPopup, content);
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
        _currentEngine  = null;
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Builder helpers
    // ──────────────────────────────────────────────────────────────────────────

    private static object? BuildHeaderRow(AvaloniaReflection r)
    {
        var row = r.CreateGrid();
        if (row == null) return null;
        r.SetTag(row, "uprooted-no-recolor");

        // Title TextBlock (left)
        var title = r.CreateTextBlock("Translate", 15, TEXT_BRIGHT, "SemiBold");
        if (title != null)
        {
            r.SetHorizontalAlignment(title, "Left");
            r.SetVerticalAlignment(title, "Center");
            r.SetTag(title, "uprooted-no-recolor");
            r.AddChild(row, title);
        }

        // Dismiss "×" button (right)
        var closeBtn = r.CreateBorder(null, 4);
        if (closeBtn != null)
        {
            r.SetWidth(closeBtn, 24);
            r.SetHeight(closeBtn, 24);
            r.SetHorizontalAlignment(closeBtn, "Right");
            r.SetTag(closeBtn, "uprooted-no-recolor");

            var xLabel = r.CreateTextBlock("×", 16, TEXT_COLOR, "Normal");
            if (xLabel != null)
            {
                r.SetHorizontalAlignment(xLabel, "Center");
                r.SetVerticalAlignment(xLabel, "Center");
                r.SetTag(xLabel, "uprooted-no-recolor");
                r.SetBorderChild(closeBtn, xLabel);
            }

            // Capture for dismiss (need AvaloniaReflection in lambda context)
            var capturedR = r;
            r.SubscribeEvent(closeBtn, "PointerPressed", () =>
            {
                capturedR.SetRenderScale(closeBtn, 0.985);
                capturedR.SetBackground(closeBtn, "#20FFFFFF");
            });
            r.SubscribeEvent(closeBtn, "PointerReleased", () => Dismiss(capturedR));
            r.SubscribeEvent(closeBtn, "PointerExited", () =>
            {
                capturedR.SetRenderScale(closeBtn, 1.0);
                capturedR.SetBackground(closeBtn, (string?)null);
            });
            r.AddChild(row, closeBtn);
        }

        return row;
    }

    private static object? BuildSeparator(AvaloniaReflection r)
    {
        var sep = r.CreateBorder(SEPARATOR_CLR, 0);
        if (sep == null) return null;
        r.SetHeight(sep, 1);
        r.SetTag(sep, "uprooted-no-recolor");
        return sep;
    }

    /// <summary>
    /// Build a language selection ComboBox using Avalonia reflection.
    /// Falls back to a styled TextBlock label if ComboBox can't be instantiated.
    /// </summary>
    private static object? BuildLanguageCombo(AvaloniaReflection r,
        string currentCode, Action<string> onChange)
    {
        try
        {
            // Try Avalonia ComboBox first
            Type? comboType = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase)) continue;
                foreach (var t in asm.GetTypes())
                {
                    if (t.FullName == "Avalonia.Controls.ComboBox")
                    { comboType = t; break; }
                }
                if (comboType != null) break;
            }

            if (comboType != null)
                return BuildAvaloniaCombBox(r, comboType, currentCode, onChange);
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"BuildLanguageCombo ComboBox attempt failed: {ex.Message}");
        }

        // Fallback: plain display TextBlock (non-interactive)
        var fb = r.CreateTextBlock(GetLanguageName(currentCode), 13, TEXT_BRIGHT, "Normal");
        if (fb != null)
        {
            r.SetBackground(fb, INPUT_BG);
            r.SetHeight(fb, COMBO_H);
            r.SetMargin(fb, 8, 0, 0, 0);
            r.SetTag(fb, "uprooted-no-recolor");
        }
        return fb;
    }

    private static object? BuildAvaloniaCombBox(AvaloniaReflection r, Type comboType,
        string currentCode, Action<string> onChange)
    {
        var combo = Activator.CreateInstance(comboType);
        if (combo == null) return null;

        r.SetHeight(combo, COMBO_H);
        r.SetBackground(combo, INPUT_BG);
        r.SetTag(combo, "uprooted-no-recolor");

        // Build display strings list
        var displayNames = Languages.Select(l => l.Name).ToList();

        // ItemsSource
        var itemsSourceProp = comboType.GetProperty("ItemsSource",
            BindingFlags.Public | BindingFlags.Instance);
        itemsSourceProp?.SetValue(combo, displayNames);

        // SelectedIndex
        int selectedIdx = Array.FindIndex(Languages, l => l.Code == currentCode);
        if (selectedIdx < 0) selectedIdx = 0;
        var selectedIndexProp = comboType.GetProperty("SelectedIndex",
            BindingFlags.Public | BindingFlags.Instance);
        selectedIndexProp?.SetValue(combo, selectedIdx);

        // SelectionChanged event
        var selChangedEvent = comboType.GetEvent("SelectionChanged");
        if (selChangedEvent != null)
        {
            var handlerType = selChangedEvent.EventHandlerType!;
            var invokeMethod = handlerType.GetMethod("Invoke")!;
            var paramTypes   = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();

            // Capture combo and onChange for the lambda
            var capturedCombo           = combo;
            var capturedSelectedIndexProp = selectedIndexProp;

            Action<object> onSelChanged = (_) =>
            {
                try
                {
                    var idx = capturedSelectedIndexProp?.GetValue(capturedCombo) as int? ?? 0;
                    if (idx >= 0 && idx < Languages.Length)
                    {
                        var code = Languages[idx].Code;
                        onChange(code);
                        Logger.Log("Translate", $"Language changed to {code}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"SelectionChanged error: {ex.Message}");
                }
            };

            // Build Expression lambda: (sender, e) => onSelChanged((object)e)
            var p0      = System.Linq.Expressions.Expression.Parameter(paramTypes[0], "sender");
            var p1      = System.Linq.Expressions.Expression.Parameter(paramTypes[1], "e");
            var cbExpr  = System.Linq.Expressions.Expression.Constant(onSelChanged);
            var castE   = System.Linq.Expressions.Expression.Convert(p1, typeof(object));
            var invExpr = System.Linq.Expressions.Expression.Invoke(cbExpr, castE);
            var lambda  = System.Linq.Expressions.Expression.Lambda(handlerType, invExpr, p0, p1);
            var handler = lambda.Compile();

            selChangedEvent.AddEventHandler(combo, handler);
        }

        return combo;
    }

    private static object? BuildAutoTranslateRow(AvaloniaReflection r,
        UprootedSettings settings, TranslateEngine? engine)
    {
        var row = r.CreateGrid();
        if (row == null) return null;
        r.SetTag(row, "uprooted-no-recolor");

        // Left: label + description column
        var labelCol = r.CreateStackPanel(vertical: true, spacing: 2);
        if (labelCol != null)
        {
            r.SetTag(labelCol, "uprooted-no-recolor");
            r.SetHorizontalAlignment(labelCol, "Left");
            r.SetVerticalAlignment(labelCol, "Center");

            var label = r.CreateTextBlock("AutoTranslate", 14, TEXT_BRIGHT, "SemiBold");
            if (label != null)
            {
                r.SetTag(label, "uprooted-no-recolor");
                r.AddChild(labelCol, label);
            }
            var desc = r.CreateTextBlock("Translate messages automatically on send/receive", 11, TEXT_COLOR, "Normal");
            if (desc != null)
            {
                r.SetTag(desc, "uprooted-no-recolor");
                r.AddChild(labelCol, desc);
            }
            r.AddChild(row, labelCol);
        }

        // Right: toggle pill
        var toggleBg  = settings.TranslateAutoTranslate ? ContentPages.AccentGreen : "#4A4D55";
        var pill      = r.CreateBorder(toggleBg, 12);
        if (pill != null)
        {
            r.SetWidth(pill, 44);
            r.SetHeight(pill, 24);
            r.SetHorizontalAlignment(pill, "Right");
            r.SetVerticalAlignment(pill, "Center");
            r.SetTag(pill, "uprooted-no-recolor");

            // Knob (white circle)
            var knobX  = settings.TranslateAutoTranslate ? 22.0 : 2.0;
            var knob   = r.CreateBorder("#FFFFFF", 9);
            if (knob != null)
            {
                r.SetWidth(knob, 18);
                r.SetHeight(knob, 18);
                r.SetMargin(knob, knobX, 3, 0, 0);
                r.SetHorizontalAlignment(knob, "Left");
                r.SetVerticalAlignment(knob, "Top");
                r.SetTag(knob, "uprooted-no-recolor");
                r.SetBorderChild(pill, knob);
            }

            // Click toggles AutoTranslate
            var capturedPill   = pill;
            var capturedKnob   = knob;
            var capturedEngine = engine;
            var capturedR      = r;
            bool[] hovered = { false };

            r.SubscribeEvent(pill, "PointerPressed", () =>
            {
                var rest = settings.TranslateAutoTranslate ? ContentPages.AccentGreen : "#4A4D55";
                var hover = settings.TranslateAutoTranslate
                    ? ColorUtils.Lighten(ContentPages.AccentGreen, 10)
                    : ColorUtils.Lighten("#4A4D55", 8);
                var basis = hovered[0] ? hover : rest;
                capturedR.SetRenderScale(capturedPill, 0.985);
                capturedR.SetBackground(capturedPill, ColorUtils.Luminance(basis) > 0.5
                    ? ColorUtils.Darken(basis, 8)
                    : ColorUtils.Lighten(basis, 8));
            });

            r.SubscribeEvent(pill, "PointerReleased", () =>
            {
                try
                {
                    var s = UprootedSettings.Load();
                    s.TranslateAutoTranslate = !s.TranslateAutoTranslate;
                    s.Save();

                    // Update pill color and knob position
                    var newBg    = s.TranslateAutoTranslate ? ContentPages.AccentGreen : "#4A4D55";
                    var newKnobX = s.TranslateAutoTranslate ? 22.0 : 2.0;
                    capturedR.SetRenderScale(capturedPill, 1.0);
                    var hoverBg = s.TranslateAutoTranslate
                        ? ColorUtils.Lighten(ContentPages.AccentGreen, 10)
                        : ColorUtils.Lighten("#4A4D55", 8);
                    capturedR.SetBackground(capturedPill, hovered[0] ? hoverBg : newBg);
                    if (capturedKnob != null)
                        capturedR.SetMargin(capturedKnob, newKnobX, 3, 0, 0);

                    // Refresh all toolbar buttons (without toggling the state again)
                    capturedEngine?.RefreshButtonColors(s.TranslateAutoTranslate);

                    Logger.Log("Translate", $"AutoTranslate toggled via popup: {s.TranslateAutoTranslate}");
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"Toggle click error: {ex.Message}");
                }
            });

            r.SubscribeEvent(pill, "PointerEntered", () =>
            {
                hovered[0] = true;
                var on = UprootedSettings.Load().TranslateAutoTranslate;
                capturedR.SetBackground(capturedPill, on
                    ? ColorUtils.Lighten(ContentPages.AccentGreen, 10)
                    : ColorUtils.Lighten("#4A4D55", 8));
            });
            r.SubscribeEvent(pill, "PointerExited", () =>
            {
                hovered[0] = false;
                capturedR.SetRenderScale(capturedPill, 1.0);
                var on = UprootedSettings.Load().TranslateAutoTranslate;
                capturedR.SetBackground(capturedPill, on ? ContentPages.AccentGreen : "#4A4D55");
            });

            r.AddChild(row, pill);
        }

        return row;
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Utilities
    // ──────────────────────────────────────────────────────────────────────────

    private static string GetLanguageName(string code)
    {
        foreach (var (c, n) in Languages)
            if (c == code) return n;
        return code;
    }

    private static void SetBorderStroke(AvaloniaReflection r, object? element,
        string hex, double width)
    {
        if (element == null) return;
        var brush = r.CreateBrush(hex);
        element.GetType().GetProperty("BorderBrush")?.SetValue(element, brush);
        if (r.ThicknessType != null)
        {
            var thickness = Activator.CreateInstance(r.ThicknessType, width, width, width, width);
            element.GetType().GetProperty("BorderThickness")?.SetValue(element, thickness);
        }
    }
}
