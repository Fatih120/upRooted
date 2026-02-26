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
///         Service selector ComboBox (Google / DeepL Free / DeepL Pro)
///         [Conditional] DeepL API key TextBox
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

    // Stored for dismiss+re-show on service change
    private static AvaloniaReflection? _cachedR;
    private static object? _cachedButton;
    private static ThemeEngine? _cachedTheme;

    // DeepL API key TextBox reference (saved on dismiss)
    private static object? _apiKeyTextBox;

    // Cached Avalonia types (resolved once, reused across popup rebuilds)
    private static Type? _comboBoxType;
    private static Type? _comboBoxItemType;
    private static Type? _textBoxType;
    private static bool _typesResolved;

    private static void ResolveControlTypes()
    {
        if (_typesResolved) return;
        _typesResolved = true;
        try
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase)) continue;
                foreach (var t in asm.GetTypes())
                {
                    if (t.FullName == "Avalonia.Controls.ComboBox") _comboBoxType = t;
                    else if (t.FullName == "Avalonia.Controls.ComboBoxItem") _comboBoxItemType = t;
                    else if (t.FullName == "Avalonia.Controls.TextBox") _textBoxType = t;
                }
                if (_comboBoxType != null && _textBoxType != null) break;
            }
        }
        catch { }
    }

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

    // Service options for the ComboBox
    private static readonly (string Value, string Label)[] ServiceOptions =
    {
        ("google",    "Google Translate"),
        ("deepl",     "DeepL Free"),
        ("deepl-pro", "DeepL Pro"),
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
        _cachedR        = r;
        _cachedButton   = button;
        _cachedTheme    = themeEngine;

        var settings = UprootedSettings.Load();
        var service = settings.TranslateService ?? "google";
        var languages = TranslateEngine.GetLanguagesForService(service);
        bool isDeepL = service == "deepl" || service == "deepl-pro";

        // ── Positioning: centered on button, above the compose bar ──
        var buttonBounds   = r.GetBounds(button);
        var buttonPos      = r.TranslatePoint(button, 0, 0, overlay);
        var windowBounds   = r.GetBounds(mainWindow);

        double windowW = windowBounds?.W ?? 1200;
        double windowH = windowBounds?.H ?? 800;

        // Estimate popup height (accounts for optional DeepL API key row)
        double svcHeight  = 20 + COMBO_H + SECTION_GAP; // service selector
        double apiHeight  = isDeepL ? 20 + COMBO_H + SECTION_GAP : 0; // API key (conditional)
        double langHeight = 4 * (20 + COMBO_H + SECTION_GAP / 2); // 4 language combos
        double popupH = 40 + 1 + svcHeight + apiHeight + 1 + langHeight + 1 + 52
                       + POPUP_PADDING * 2 + SECTION_GAP * 3;

        double buttonX = buttonPos?.X ?? (windowW / 2);
        double buttonY = buttonPos?.Y ?? (windowH - 60);
        double buttonW = buttonBounds?.W ?? 32;

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
            r.SubscribeClickReleased(_currentBackdrop, () => Dismiss(r));
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

        // ── Service selector ──
        var svcLabel = r.CreateTextBlock("Translation service", 12, TEXT_COLOR, "Normal");
        if (svcLabel != null) { r.SetTag(svcLabel, "uprooted-no-recolor"); r.AddChild(content, svcLabel); }

        var svcCombo = BuildServiceCombo(r, service, engine, button, themeEngine);
        if (svcCombo != null) r.AddChild(content, svcCombo);

        // ── DeepL API key (conditional) ──
        if (isDeepL)
        {
            var keyLabel = r.CreateTextBlock("DeepL API key", 12, TEXT_COLOR, "Normal");
            if (keyLabel != null) { r.SetTag(keyLabel, "uprooted-no-recolor"); r.AddChild(content, keyLabel); }

            var keyBox = BuildApiKeyTextBox(r, settings.TranslateDeeplApiKey);
            if (keyBox != null) r.AddChild(content, keyBox);
        }

        // Separator
        var sep2 = BuildSeparator(r);
        if (sep2 != null) r.AddChild(content, sep2);

        // ── Language selectors ──
        // Received from
        var rxFromLabel = r.CreateTextBlock("Language received messages translated from", 12, TEXT_COLOR, "Normal");
        if (rxFromLabel != null) { r.SetTag(rxFromLabel, "uprooted-no-recolor"); r.AddChild(content, rxFromLabel); }
        var rxFromCombo = BuildLanguageCombo(r, languages, settings.TranslateFromLang, (code) =>
        {
            settings.TranslateFromLang = code;
            settings.Save();
        });
        if (rxFromCombo != null) r.AddChild(content, rxFromCombo);

        // Received to
        var rxToLabel = r.CreateTextBlock("Language received messages translated to", 12, TEXT_COLOR, "Normal");
        if (rxToLabel != null) { r.SetTag(rxToLabel, "uprooted-no-recolor"); r.AddChild(content, rxToLabel); }
        var rxToCombo = BuildLanguageCombo(r, languages, settings.TranslateToLang, (code) =>
        {
            settings.TranslateToLang = code;
            settings.Save();
        });
        if (rxToCombo != null) r.AddChild(content, rxToCombo);

        // Sent from
        var txFromLabel = r.CreateTextBlock("Language your own messages translated from", 12, TEXT_COLOR, "Normal");
        if (txFromLabel != null) { r.SetTag(txFromLabel, "uprooted-no-recolor"); r.AddChild(content, txFromLabel); }
        var txFromCombo = BuildLanguageCombo(r, languages, settings.TranslateSendFromLang, (code) =>
        {
            settings.TranslateSendFromLang = code;
            settings.Save();
        });
        if (txFromCombo != null) r.AddChild(content, txFromCombo);

        // Sent to
        var txToLabel = r.CreateTextBlock("Language your own messages translated to", 12, TEXT_COLOR, "Normal");
        if (txToLabel != null) { r.SetTag(txToLabel, "uprooted-no-recolor"); r.AddChild(content, txToLabel); }
        var txToCombo = BuildLanguageCombo(r, languages, settings.TranslateSendToLang, (code) =>
        {
            settings.TranslateSendToLang = code;
            settings.Save();
        });
        if (txToCombo != null) r.AddChild(content, txToCombo);

        // Separator
        var sep3 = BuildSeparator(r);
        if (sep3 != null) r.AddChild(content, sep3);

        // AutoTranslate toggle row
        var toggleRow = BuildAutoTranslateRow(r, settings, engine, content);
        if (toggleRow != null) r.AddChild(content, toggleRow);

        r.SetBorderChild(_currentPopup, content);
    }

    public static void Dismiss(AvaloniaReflection r)
    {
        // Save API key from TextBox before dismissing
        if (_apiKeyTextBox != null)
        {
            try
            {
                var textProp = _apiKeyTextBox.GetType().GetProperty("Text",
                    BindingFlags.Public | BindingFlags.Instance);
                var text = textProp?.GetValue(_apiKeyTextBox) as string ?? "";
                var settings = UprootedSettings.Load();
                if (settings.TranslateDeeplApiKey != text)
                {
                    settings.TranslateDeeplApiKey = text;
                    settings.Save();
                    Logger.Log("Translate", "Saved DeepL API key on popup dismiss");
                }
            }
            catch { }
            _apiKeyTextBox = null;
        }

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
        _cachedR        = null;
        _cachedButton   = null;
        _cachedTheme    = null;
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Builder helpers
    // ──────────────────────────────────────────────────────────────────────────

    private static object? BuildHeaderRow(AvaloniaReflection r)
    {
        var row = r.CreateGrid();
        if (row == null) return null;
        r.SetTag(row, "uprooted-no-recolor");

        var title = r.CreateTextBlock("Translate", 15, TEXT_BRIGHT, "SemiBold");
        if (title != null)
        {
            r.SetHorizontalAlignment(title, "Left");
            r.SetVerticalAlignment(title, "Center");
            r.SetTag(title, "uprooted-no-recolor");
            r.AddChild(row, title);
        }

        var closeBtn = r.CreateBorder(null, 4);
        if (closeBtn != null)
        {
            r.SetWidth(closeBtn, 24);
            r.SetHeight(closeBtn, 24);
            r.SetHorizontalAlignment(closeBtn, "Right");
            r.SetTag(closeBtn, "uprooted-no-recolor");

            var xLabel = r.CreateTextBlock("\u00d7", 16, TEXT_COLOR, "Normal");
            if (xLabel != null)
            {
                r.SetHorizontalAlignment(xLabel, "Center");
                r.SetVerticalAlignment(xLabel, "Center");
                r.SetTag(xLabel, "uprooted-no-recolor");
                r.SetBorderChild(closeBtn, xLabel);
            }

            var capturedR = r;
            r.SubscribeEvent(closeBtn, "PointerPressed", () =>
            {
                capturedR.SetRenderScale(closeBtn, 0.985);
                capturedR.SetBackground(closeBtn, "#20FFFFFF");
            });
            r.SubscribeClickReleased(closeBtn, () => Dismiss(capturedR));
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

    // ──────────────────────────────────────────────────────────────────────────
    // Service selector
    // ──────────────────────────────────────────────────────────────────────────

    private static object? BuildServiceCombo(AvaloniaReflection r, string currentService,
        TranslateEngine? engine, object button, ThemeEngine? themeEngine)
    {
        try
        {
            ResolveControlTypes();
            var comboType = _comboBoxType;
            if (comboType == null) return null;

            var combo = Activator.CreateInstance(comboType);
            if (combo == null) return null;

            r.SetHeight(combo, COMBO_H);
            r.SetBackground(combo, INPUT_BG);
            r.SetTag(combo, "uprooted-no-recolor");

            var displayNames = ServiceOptions.Select(s => s.Label).ToList();
            var itemsSourceProp = comboType.GetProperty("ItemsSource",
                BindingFlags.Public | BindingFlags.Instance);
            itemsSourceProp?.SetValue(combo, displayNames);

            int selectedIdx = Array.FindIndex(ServiceOptions, s => s.Value == currentService);
            if (selectedIdx < 0) selectedIdx = 0;
            var selectedIndexProp = comboType.GetProperty("SelectedIndex",
                BindingFlags.Public | BindingFlags.Instance);
            selectedIndexProp?.SetValue(combo, selectedIdx);

            var selChangedEvent = comboType.GetEvent("SelectionChanged");
            if (selChangedEvent != null)
            {
                var handlerType = selChangedEvent.EventHandlerType!;
                var paramTypes = handlerType.GetMethod("Invoke")!.GetParameters()
                    .Select(p => p.ParameterType).ToArray();

                var capturedCombo = combo;
                var capturedIdxProp = selectedIndexProp;
                var capturedR = r;
                var capturedBtn = button;
                var capturedTheme = themeEngine;
                var capturedEngine = engine;

                Action<object> onChanged = (_) =>
                {
                    try
                    {
                        var idx = capturedIdxProp?.GetValue(capturedCombo) as int? ?? 0;
                        if (idx >= 0 && idx < ServiceOptions.Length)
                        {
                            var newService = ServiceOptions[idx].Value;
                            var s = UprootedSettings.Load();
                            if (s.TranslateService != newService)
                            {
                                s.TranslateService = newService;
                                s.Save();
                                Logger.Log("Translate", $"Service changed to {newService}");

                                // Re-show popup with updated language lists
                                Dismiss(capturedR);
                                Show(capturedR, capturedBtn, capturedTheme, capturedEngine);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Translate", $"Service combo error: {ex.Message}");
                    }
                };

                var p0 = System.Linq.Expressions.Expression.Parameter(paramTypes[0], "sender");
                var p1 = System.Linq.Expressions.Expression.Parameter(paramTypes[1], "e");
                var cbExpr = System.Linq.Expressions.Expression.Constant(onChanged);
                var castE = System.Linq.Expressions.Expression.Convert(p1, typeof(object));
                var invExpr = System.Linq.Expressions.Expression.Invoke(cbExpr, castE);
                var lambda = System.Linq.Expressions.Expression.Lambda(handlerType, invExpr, p0, p1);
                selChangedEvent.AddEventHandler(combo, lambda.Compile());
            }

            return combo;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"BuildServiceCombo error: {ex.Message}");
            return null;
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // DeepL API key TextBox
    // ──────────────────────────────────────────────────────────────────────────

    private static object? BuildApiKeyTextBox(AvaloniaReflection r, string currentKey)
    {
        try
        {
            ResolveControlTypes();
            var textBoxType = _textBoxType;
            if (textBoxType == null) return null;

            var textBox = Activator.CreateInstance(textBoxType);
            if (textBox == null) return null;

            r.SetHeight(textBox, COMBO_H);
            r.SetBackground(textBox, INPUT_BG);
            r.SetTag(textBox, "uprooted-no-recolor");

            textBoxType.GetProperty("Text")?.SetValue(textBox, currentKey);
            textBoxType.GetProperty("Watermark")?.SetValue(textBox, "Enter DeepL API key...");

            // Set PasswordChar to mask the key
            var pwCharProp = textBoxType.GetProperty("PasswordChar");
            if (pwCharProp != null && pwCharProp.PropertyType == typeof(char))
                pwCharProp.SetValue(textBox, '\u2022'); // bullet character

            // Store reference for saving on dismiss
            _apiKeyTextBox = textBox;

            return textBox;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"BuildApiKeyTextBox error: {ex.Message}");
            return null;
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Language ComboBox
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Build a language selection ComboBox using Avalonia reflection.
    /// Falls back to a styled TextBlock label if ComboBox can't be instantiated.
    /// </summary>
    private static object? BuildLanguageCombo(AvaloniaReflection r,
        (string Code, string Name)[] languages, string currentCode, Action<string> onChange)
    {
        try
        {
            ResolveControlTypes();
            var comboType = _comboBoxType;

            if (comboType != null)
                return BuildAvaloniaCombBox(r, comboType, languages, currentCode, onChange);
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"BuildLanguageCombo ComboBox attempt failed: {ex.Message}");
        }

        // Fallback: plain display TextBlock (non-interactive)
        var fb = r.CreateTextBlock(
            TranslateEngine.GetLanguageDisplayName(currentCode), 13, TEXT_BRIGHT, "Normal");
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
        (string Code, string Name)[] languages, string currentCode, Action<string> onChange)
    {
        var combo = Activator.CreateInstance(comboType);
        if (combo == null) return null;

        r.SetHeight(combo, COMBO_H);
        r.SetBackground(combo, INPUT_BG);
        r.SetTag(combo, "uprooted-no-recolor");

        var displayNames = languages.Select(l => l.Name).ToList();

        var itemsSourceProp = comboType.GetProperty("ItemsSource",
            BindingFlags.Public | BindingFlags.Instance);
        itemsSourceProp?.SetValue(combo, displayNames);

        // Find matching index — try exact match, then case-insensitive prefix
        int selectedIdx = Array.FindIndex(languages, l => l.Code == currentCode);
        if (selectedIdx < 0)
        {
            // Try mapping between services (e.g., "auto" ↔ "", "en" ↔ "en-us")
            if (currentCode == "auto" || currentCode == "")
                selectedIdx = 0; // first entry is always auto-detect
            else
                selectedIdx = Array.FindIndex(languages,
                    l => l.Code.StartsWith(currentCode, StringComparison.OrdinalIgnoreCase)
                      || currentCode.StartsWith(l.Code, StringComparison.OrdinalIgnoreCase));
        }
        if (selectedIdx < 0) selectedIdx = 0;

        var selectedIndexProp = comboType.GetProperty("SelectedIndex",
            BindingFlags.Public | BindingFlags.Instance);
        selectedIndexProp?.SetValue(combo, selectedIdx);

        var selChangedEvent = comboType.GetEvent("SelectionChanged");
        if (selChangedEvent != null)
        {
            var handlerType = selChangedEvent.EventHandlerType!;
            var paramTypes = handlerType.GetMethod("Invoke")!.GetParameters()
                .Select(p => p.ParameterType).ToArray();

            var capturedCombo = combo;
            var capturedIdxProp = selectedIndexProp;
            var capturedLangs = languages;

            Action<object> onSelChanged = (_) =>
            {
                try
                {
                    var idx = capturedIdxProp?.GetValue(capturedCombo) as int? ?? 0;
                    if (idx >= 0 && idx < capturedLangs.Length)
                    {
                        var code = capturedLangs[idx].Code;
                        onChange(code);
                        Logger.Log("Translate", $"Language changed to {code}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"SelectionChanged error: {ex.Message}");
                }
            };

            var p0 = System.Linq.Expressions.Expression.Parameter(paramTypes[0], "sender");
            var p1 = System.Linq.Expressions.Expression.Parameter(paramTypes[1], "e");
            var cbExpr = System.Linq.Expressions.Expression.Constant(onSelChanged);
            var castE = System.Linq.Expressions.Expression.Convert(p1, typeof(object));
            var invExpr = System.Linq.Expressions.Expression.Invoke(cbExpr, castE);
            var lambda = System.Linq.Expressions.Expression.Lambda(handlerType, invExpr, p0, p1);
            selChangedEvent.AddEventHandler(combo, lambda.Compile());
        }

        return combo;
    }

    // ──────────────────────────────────────────────────────────────────────────
    // AutoTranslate toggle row
    // ──────────────────────────────────────────────────────────────────────────

    private static object? BuildAutoTranslateRow(AvaloniaReflection r,
        UprootedSettings settings, TranslateEngine? engine, object contentPanel)
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

            var capturedPill    = pill;
            var capturedKnob    = knob;
            var capturedEngine  = engine;
            var capturedR       = r;
            var capturedContent = contentPanel;
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

            r.SubscribeClickReleased(pill, () =>
            {
                try
                {
                    var s = UprootedSettings.Load();
                    bool turningOn = !s.TranslateAutoTranslate;

                    // First-enable warning
                    if (turningOn && s.TranslateShowAutoTranslateAlert)
                    {
                        ShowAutoTranslateWarning(capturedR, capturedContent,
                            capturedPill, capturedKnob, capturedEngine, hovered);
                        return;
                    }

                    ApplyAutoTranslateToggle(capturedR, s, turningOn,
                        capturedPill, capturedKnob, capturedEngine, hovered);
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

    private static void ApplyAutoTranslateToggle(AvaloniaReflection r, UprootedSettings s,
        bool turnOn, object pill, object? knob, TranslateEngine? engine, bool[] hovered)
    {
        s.TranslateAutoTranslate = turnOn;
        s.Save();

        var newBg    = turnOn ? ContentPages.AccentGreen : "#4A4D55";
        var newKnobX = turnOn ? 22.0 : 2.0;
        r.SetRenderScale(pill, 1.0);
        var hoverBg = turnOn
            ? ColorUtils.Lighten(ContentPages.AccentGreen, 10)
            : ColorUtils.Lighten("#4A4D55", 8);
        r.SetBackground(pill, hovered[0] ? hoverBg : newBg);
        if (knob != null)
            r.SetMargin(knob, newKnobX, 3, 0, 0);

        engine?.RefreshButtonColors(turnOn);
        Logger.Log("Translate", $"AutoTranslate toggled via popup: {turnOn}");
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Auto-translate first-enable warning
    // ──────────────────────────────────────────────────────────────────────────

    private static void ShowAutoTranslateWarning(AvaloniaReflection r, object contentPanel,
        object pill, object? knob, TranslateEngine? engine, bool[] hovered)
    {
        try
        {
            // Build warning panel
            var warningPanel = r.CreateStackPanel(vertical: true, spacing: 8);
            if (warningPanel == null) return;
            r.SetTag(warningPanel, "uprooted-translate-warning");

            var warningBorder = r.CreateBorder("#30FFAA00", 6);
            if (warningBorder != null)
            {
                r.SetTag(warningBorder, "uprooted-no-recolor");
                r.SetMargin(warningBorder, 0, 4, 0, 0);

                var innerStack = r.CreateStackPanel(vertical: true, spacing: 8);
                if (innerStack != null)
                {
                    r.SetMargin(innerStack, 12, 12, 12, 12);
                    r.SetTag(innerStack, "uprooted-no-recolor");

                    var warningText = r.CreateTextBlock(
                        "AutoTranslate will automatically translate all incoming messages and outgoing messages. " +
                        "This uses an external translation service which means message content is sent to Google or DeepL servers.",
                        12, TEXT_BRIGHT, "Normal");
                    if (warningText != null)
                    {
                        r.SetTag(warningText, "uprooted-no-recolor");
                        // Set word wrap
                        var wrapProp = warningText.GetType().GetProperty("TextWrapping");
                        if (wrapProp != null)
                        {
                            var wrapEnum = Enum.Parse(wrapProp.PropertyType, "Wrap");
                            wrapProp.SetValue(warningText, wrapEnum);
                        }
                        r.AddChild(innerStack, warningText);
                    }

                    // Button row
                    var buttonRow = r.CreateGrid();
                    if (buttonRow != null)
                    {
                        r.SetTag(buttonRow, "uprooted-no-recolor");

                        // "OK" button
                        var okBtn = r.CreateBorder(ContentPages.AccentGreen, 4);
                        if (okBtn != null)
                        {
                            r.SetHorizontalAlignment(okBtn, "Left");
                            r.SetTag(okBtn, "uprooted-no-recolor");
                            r.SetIsHitTestVisible(okBtn, true);

                            var okLabel = r.CreateTextBlock("  OK  ", 12, "#FFFFFF", "SemiBold");
                            if (okLabel != null)
                            {
                                r.SetMargin(okLabel, 12, 6, 12, 6);
                                r.SetTag(okLabel, "uprooted-no-recolor");
                                r.SetBorderChild(okBtn, okLabel);
                            }

                            var capturedWarningPanel = warningPanel;
                            var capturedContent = contentPanel;
                            r.SubscribeClickReleased(okBtn, () =>
                            {
                                RemoveChildFromPanel(r, capturedContent, capturedWarningPanel);
                                var s = UprootedSettings.Load();
                                ApplyAutoTranslateToggle(r, s, true, pill, knob, engine, hovered);
                            });
                            r.AddChild(buttonRow, okBtn);
                        }

                        // "Don't show again" button
                        var dontShowBtn = r.CreateBorder(null, 4);
                        if (dontShowBtn != null)
                        {
                            r.SetHorizontalAlignment(dontShowBtn, "Right");
                            r.SetTag(dontShowBtn, "uprooted-no-recolor");
                            r.SetIsHitTestVisible(dontShowBtn, true);

                            var dontShowLabel = r.CreateTextBlock("Don't show again", 12, TEXT_COLOR, "Normal");
                            if (dontShowLabel != null)
                            {
                                r.SetMargin(dontShowLabel, 8, 6, 8, 6);
                                r.SetTag(dontShowLabel, "uprooted-no-recolor");
                                r.SetBorderChild(dontShowBtn, dontShowLabel);
                            }

                            var capturedWarningPanel2 = warningPanel;
                            var capturedContent2 = contentPanel;
                            r.SubscribeClickReleased(dontShowBtn, () =>
                            {
                                RemoveChildFromPanel(r, capturedContent2, capturedWarningPanel2);
                                var s = UprootedSettings.Load();
                                s.TranslateShowAutoTranslateAlert = false;
                                ApplyAutoTranslateToggle(r, s, true, pill, knob, engine, hovered);
                            });
                            r.AddChild(buttonRow, dontShowBtn);
                        }

                        r.AddChild(innerStack, buttonRow);
                    }

                    r.SetBorderChild(warningBorder, innerStack);
                }

                r.AddChild(warningPanel, warningBorder);
            }

            r.AddChild(contentPanel, warningPanel);
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"ShowAutoTranslateWarning error: {ex.Message}");
        }
    }

    private static void RemoveChildFromPanel(AvaloniaReflection r, object parent, object child)
    {
        try
        {
            var childrenProp = parent.GetType().GetProperty("Children",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            if (childrenProp == null) return;
            var coll = childrenProp.GetValue(parent);
            if (coll == null) return;
            var removeMethod = coll.GetType().GetMethod("Remove",
                BindingFlags.Public | BindingFlags.Instance);
            removeMethod?.Invoke(coll, new[] { child });
        }
        catch { }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Utilities
    // ──────────────────────────────────────────────────────────────────────────

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
