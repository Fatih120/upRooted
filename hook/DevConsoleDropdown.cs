using System.Linq.Expressions;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Dev Console dropdown: titlebar gear button + overlay popup with all dev actions.
///
/// Injection target: WindowsTitleBarView StackPanel (col 2), inserted before SupportButton.
/// Uses RootSvgButton with "SettingsSVG" DynamicResource (gear icon, auto-flips with theme).
///
/// Popup structure:
///   OverlayLayer
///     Backdrop (full-window transparent click-to-dismiss)
///     Popup (Border #2B2D31, cornerRadius=10, border=1px #3F4147)
///       StackPanel
///         Header row: "Dev Console" title + x dismiss
///         Separator
///         SPOOFS section: Update Popup, Update Installed, Reset Spoofs
///         Separator
///         DIAGNOSTICS section: Dump Visual Tree, Dump Theme Colors, Dump Resource Keys
///         Separator
///         ENGINES section: Force Theme Walk, Revert Theme
///         Separator
///         TOOLS section: Live Console, Open Logs, Recon Logger [toggle]
///
/// Developer channel only. Cleaned up by ApplyChannelRuntimeState when switching away.
/// </summary>
internal static class DevConsoleDropdown
{
    private const string Tag = "DevConsole";

    // Titlebar injection state
    private static object? _button;
    private static object? _titleBarPanel;
    private static bool _retryScheduled;

    // Overlay popup state
    private static object? _currentOverlay;
    private static object? _currentBackdrop;
    private static object? _currentPopup;

    // Cached references
    private static AvaloniaReflection? _r;
    private static object? _mainWindow;
    private static ThemeEngine? _themeEngine;

    // Popup dimensions
    private const double POPUP_W = 260;

    // Fallback colors (rendered before theme applies, overridden by DynamicResource bindings)
    private const string POPUP_BG       = "#2B2D31";  // → BackgroundSecondary
    private const string POPUP_BORDER   = "#3F4147";  // → Border
    private const string TEXT_BRIGHT    = "#F2F3F5";  // → TextPrimary
    private const string TEXT_MUTED     = "#B5BAC1";  // → TextSecondary
    private const string SECTION_HEADER = "#949BA4";  // → TextTertiary
    private const string SEPARATOR_CLR  = "#3F4147";  // → Border
    private const string ROW_HOVER_BG   = "#36373D";  // → HighlightLight
    private const string TOGGLE_ON_BG   = "#248046";  // semantic green, not themed
    private const string TOGGLE_OFF_BG  = "#80848E";  // → Muted

    /// <summary>Initialize the dev console dropdown: inject titlebar button and store refs.</summary>
    public static void Init(AvaloniaReflection r, object mainWindow, ThemeEngine? themeEngine)
    {
        _r = r;
        _mainWindow = mainWindow;
        _themeEngine = themeEngine;

        EnsureTitleBarButtonInjected();
        Logger.Log(Tag, "Init complete");
    }

    /// <summary>Remove the titlebar button and dismiss any open popup.</summary>
    public static void Remove()
    {
        Dismiss();

        if (_button != null && _titleBarPanel != null && _r != null)
        {
            try
            {
                int count = _r.GetChildCount(_titleBarPanel);
                for (int i = 0; i < count; i++)
                {
                    if (_r.GetChild(_titleBarPanel, i) == _button)
                    {
                        // Remove by setting the child list
                        var children = _titleBarPanel.GetType()
                            .GetProperty("Children", BindingFlags.Public | BindingFlags.Instance)?
                            .GetValue(_titleBarPanel);
                        children?.GetType().GetMethod("RemoveAt")?.Invoke(children, new object[] { i });
                        Logger.Log(Tag, "Removed titlebar button");
                        break;
                    }
                }
            }
            catch (Exception ex) { Logger.Log(Tag, $"Remove button error: {ex.Message}"); }
        }

        _button = null;
        _titleBarPanel = null;
        _retryScheduled = false;
        _r = null;
        _mainWindow = null;
        _themeEngine = null;
    }

    // ===== Titlebar Button Injection =====

    private static void InjectTitleBarButton()
    {
        if (_button != null || _r == null || _mainWindow == null) return;

        try
        {
            // Find SupportButton by name in the visual tree
            object? supportBtn = null;
            var walker = new VisualTreeWalker(_r);
            foreach (var node in walker.DescendantsDepthFirst(_mainWindow))
            {
                var name = node.GetType().GetProperty("Name")?.GetValue(node) as string;
                if (name == "SupportButton")
                {
                    supportBtn = node;
                    break;
                }
            }
            if (supportBtn == null) { Logger.Log(Tag, "SupportButton not found"); return; }

            // Get parent StackPanel
            var parent = _r.GetParent(supportBtn);
            if (parent == null) { Logger.Log(Tag, "Parent StackPanel not found"); return; }
            _titleBarPanel = parent;

            // Find RootSvgButton type
            Type? svgBtnType = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try { svgBtnType = asm.GetType("RootApp.Client.Avalonia.Controls.RootSvgButton"); }
                catch { }
                if (svgBtnType != null) break;
            }
            if (svgBtnType == null) { Logger.Log(Tag, "RootSvgButton type not found"); return; }

            // Create RootSvgButton instance
            var btn = Activator.CreateInstance(svgBtnType);
            if (btn == null) return;

            // Mirror native SupportButton layout properties
            if (!ApplyNativeTitleBarButtonLayout(supportBtn, btn))
            {
                svgBtnType.GetProperty("Width")?.SetValue(btn, 20.0);
                svgBtnType.GetProperty("Height")?.SetValue(btn, 20.0);
                svgBtnType.GetProperty("Margin")?.SetValue(btn,
                    Activator.CreateInstance(_r.ThicknessType!, 0.0, 0.0, 8.0, 0.0));
            }

            // SvgWidth/SvgHeight are setter-only: always set explicitly
            svgBtnType.GetProperty("SvgWidth")?.SetValue(btn, 18.0);
            svgBtnType.GetProperty("SvgHeight")?.SetValue(btn, 18.0);

            _r.SetVerticalAlignment(btn, "Center");

            // Bind SvgPath to gear icon (auto-flips with theme)
            _r.BindToDynamicResource(btn, "SvgPath", "SettingsSVG");
            btn.GetType().GetProperty("SvgOpacity")?.SetValue(btn, 1.0);

            // Add tooltip
            try { AttachTooltip(btn, "Dev Console"); }
            catch (Exception ex) { Logger.Log(Tag, $"Tooltip error: {ex.Message}"); }

            // Wire Click event via Expression lambda
            var clickEvent = btn.GetType().GetEvent("Click");
            if (clickEvent != null)
            {
                var invokeParams = clickEvent.EventHandlerType!.GetMethod("Invoke")!.GetParameters();
                var p0 = Expression.Parameter(typeof(object), "s");
                var p1 = Expression.Parameter(invokeParams[1].ParameterType, "e");
                var body = Expression.Call(typeof(DevConsoleDropdown)
                    .GetMethod(nameof(ToggleDropdown), BindingFlags.NonPublic | BindingFlags.Static)!);
                var lambda = Expression.Lambda(clickEvent.EventHandlerType!, body, p0, p1);
                clickEvent.AddEventHandler(btn, (Delegate)lambda.Compile());
            }

            // Insert before SupportButton in the StackPanel
            int supportIdx = -1;
            int childCount = _r.GetChildCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                if (_r.GetChild(parent, i) == supportBtn) { supportIdx = i; break; }
            }
            if (supportIdx >= 0)
                _r.InsertChild(parent, supportIdx, btn);
            else
                _r.AddChild(parent, btn);

            _button = btn;
            Logger.Log(Tag, "Injected gear button before SupportButton");
        }
        catch (Exception ex) { Logger.Log(Tag, $"InjectTitleBarButton error: {ex.Message}"); }
    }

    private static void EnsureTitleBarButtonInjected()
    {
        InjectTitleBarButton();
        if (_button != null || _retryScheduled || _r == null) return;

        _retryScheduled = true;
        var r = _r;
        _ = System.Threading.Tasks.Task.Run(async () =>
        {
            try
            {
                foreach (var delayMs in new[] { 120, 350, 700, 1200, 2200, 4000 })
                {
                    await System.Threading.Tasks.Task.Delay(delayMs);
                    if (_button != null) return;

                    var tcs = new TaskCompletionSource<bool>();
                    r.RunOnUIThread(() =>
                    {
                        try { InjectTitleBarButton(); }
                        catch { }
                        tcs.TrySetResult(_button != null);
                    });
                    if (await tcs.Task) return;
                }
            }
            catch { }
            finally { _retryScheduled = false; }
        });
    }

    private static void AttachTooltip(object btn, string text)
    {
        if (_r == null) return;

        var tooltipText = _r.CreateTextBlock(text, 14, null);
        if (tooltipText == null) return;

        // Create RootToolTip
        Type? rootToolTipType = null;
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try { rootToolTipType = asm.GetType("RootApp.Client.Avalonia.Controls.RootToolTip"); }
            catch { }
            if (rootToolTipType != null) break;
        }
        if (rootToolTipType == null) return;

        var tip = Activator.CreateInstance(rootToolTipType);
        if (tip == null) return;
        tip.GetType().GetProperty("Content")?.SetValue(tip, tooltipText);

        // ToolTip.SetTip(btn, tip)
        Type? toolTipType = null;
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try { toolTipType = asm.GetType("Avalonia.Controls.ToolTip"); }
            catch { }
            if (toolTipType != null) break;
        }
        var setTip = toolTipType?.GetMethod("SetTip", BindingFlags.Public | BindingFlags.Static);
        setTip?.Invoke(null, new[] { btn, tip });

        var setPlacement = toolTipType?.GetMethod("SetPlacement", BindingFlags.Public | BindingFlags.Static);
        if (setPlacement != null)
        {
            Type? placementType = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try { placementType = asm.GetType("Avalonia.Controls.PlacementMode"); }
                catch { }
                if (placementType != null) break;
            }
            if (placementType != null)
                setPlacement.Invoke(null, new[] { btn, Enum.Parse(placementType, "Bottom") });
        }

        var setDelay = toolTipType?.GetMethod("SetShowDelay", BindingFlags.Public | BindingFlags.Static);
        setDelay?.Invoke(null, new object[] { btn, 0 });
    }

    // ===== Dropdown Toggle / Show / Dismiss =====

    private static void ToggleDropdown()
    {
        if (_currentPopup != null)
            Dismiss();
        else
            ShowDropdown();
    }

    private static void ShowDropdown()
    {
        if (_r == null || _mainWindow == null || _button == null) return;
        var r = _r;

        Dismiss(); // ensure clean state

        var overlay = r.GetOverlayLayer(_mainWindow);
        if (overlay == null) return;
        _currentOverlay = overlay;

        // Position: below the button, right-aligned so it doesn't overflow right edge
        var buttonBounds = r.GetBounds(_button);
        var buttonPos = r.TranslatePoint(_button, 0, 0, overlay);
        var windowBounds = r.GetBounds(_mainWindow);

        double windowW = windowBounds?.W ?? 1200;
        double windowH = windowBounds?.H ?? 800;
        double buttonX = buttonPos?.X ?? (windowW / 2);
        double buttonY = buttonPos?.Y ?? 36;
        double buttonH = buttonBounds?.H ?? 20;
        double buttonW = buttonBounds?.W ?? 20;

        // Popup appears below the button, right-aligned to button right edge
        double popupX = buttonX + buttonW - POPUP_W;
        popupX = Math.Clamp(popupX, 8, windowW - POPUP_W - 8);
        double popupY = buttonY + buttonH + 4;
        popupY = Math.Min(popupY, windowH - 400); // rough clamp to avoid offscreen

        // ── Backdrop (click to dismiss) ──
        _currentBackdrop = r.CreateBorder("#01000000", 0);
        if (_currentBackdrop != null)
        {
            r.SetWidth(_currentBackdrop, windowW);
            r.SetHeight(_currentBackdrop, windowH);
            r.SetCanvasPosition(_currentBackdrop, 0, 0);
            r.SetTag(_currentBackdrop, "uprooted-no-recolor");
            r.SubscribeClickReleased(_currentBackdrop, () => Dismiss());
            r.AddToOverlay(overlay, _currentBackdrop);
        }

        // ── Popup container ──
        _currentPopup = r.CreateBorder(POPUP_BG, 10);
        if (_currentPopup == null) return;

        r.SetTag(_currentPopup, "dyn-bg:BackgroundSecondary,dyn-bb:Border");
        r.BindToDynamicResource(_currentPopup, "Background", "BackgroundSecondary");
        r.BindToDynamicResource(_currentPopup, "BorderBrush", "Border");
        SetBorderStroke(r, _currentPopup, POPUP_BORDER, 1.0);
        r.SetCanvasPosition(_currentPopup, popupX, popupY);
        r.SetWidth(_currentPopup, POPUP_W);
        r.AddToOverlay(overlay, _currentPopup);

        // ── Content ──
        var stack = r.CreateStackPanel(true, 2);
        if (stack == null) return;

        _currentPopup.GetType().GetProperty("Child")?.SetValue(_currentPopup, stack);
        r.SetPadding(_currentPopup, 12, 10, 12, 10);

        // Header row
        BuildHeaderRow(r, stack);
        BuildSeparator(r, stack);

        // SPOOFS
        BuildSectionHeader(r, stack, "SPOOFS");
        var settings = UprootedSettings.Load();
        BuildActionRow(r, stack, "Update Popup", () =>
        {
            ContentPages.ShowUpdateNotification(r, settings.Version);
        });
        BuildActionRow(r, stack, "Update Installed", () =>
        {
            AutoUpdater.Instance?.SpoofUpdateApplied(true);
        });
        BuildActionRow(r, stack, "Reset Spoofs", () =>
        {
            AutoUpdater.Instance?.SpoofUpdateApplied(false);
            ContentPages.DismissUpdateNotification(r);
        });
        BuildSeparator(r, stack);

        // DIAGNOSTICS
        BuildSectionHeader(r, stack, "DIAGNOSTICS");
        BuildActionRow(r, stack, "Dump Visual Tree", () =>
        {
            _themeEngine?.DumpVisualTreeStructure();
        });
        BuildActionRow(r, stack, "Dump Theme Colors", () =>
        {
            _themeEngine?.DumpVisualTreeColors();
        });
        BuildActionRow(r, stack, "Dump Resource Keys", () =>
        {
            _themeEngine?.DumpResourceKeys();
        });
        BuildSeparator(r, stack);

        // ENGINES
        BuildSectionHeader(r, stack, "ENGINES");
        BuildActionRow(r, stack, "Force Theme Walk", () =>
        {
            _themeEngine?.ScheduleWalkBurst();
        });
        BuildActionRow(r, stack, "Revert Theme", () =>
        {
            _themeEngine?.RevertTheme();
            var s = UprootedSettings.Load();
            s.ActiveTheme = "default-dark";
            try { s.Save(); }
            catch (Exception sx) { Logger.Log(Tag, $"Revert save error: {sx.Message}"); }
        });
        BuildSeparator(r, stack);

        // TOOLS
        BuildSectionHeader(r, stack, "TOOLS");
        BuildActionRow(r, stack, "Live Console", () =>
        {
            LogConsole.Enable();
        });
        BuildActionRow(r, stack, "Open Logs", () =>
        {
            ContentPages.OpenInExplorer(Logger.GetLogPath());
        });
        bool reconEnabled = settings.Plugins.TryGetValue("recon-logger", out var rlOn) && rlOn;
        BuildToggleRow(r, stack, "Recon Logger", reconEnabled, (newValue) =>
        {
            ContentPages.ToggleReconLogger(newValue);
            // Persist the toggle in settings
            try
            {
                var s = UprootedSettings.Load();
                s.Plugins["recon-logger"] = newValue;
                s.Save();
            }
            catch { }
        });

        // Kick a theme walk so DynamicResource bindings get resolved for current theme
        _themeEngine?.ScheduleWalkBurst();

        Logger.Log(Tag, "Dropdown opened");
    }

    private static void Dismiss()
    {
        if (_currentOverlay == null && _currentBackdrop == null && _currentPopup == null) return;

        var r = _r;
        var overlay = _currentOverlay;

        if (r != null && overlay != null)
        {
            if (_currentBackdrop != null)
            {
                try { r.RemoveFromOverlay(overlay, _currentBackdrop); } catch { }
            }
            if (_currentPopup != null)
            {
                try { r.RemoveFromOverlay(overlay, _currentPopup); } catch { }
            }
        }

        _currentBackdrop = null;
        _currentPopup = null;
        _currentOverlay = null;
    }

    // ===== UI Builder Helpers =====

    private static void BuildHeaderRow(AvaloniaReflection r, object parent)
    {
        // Horizontal stack: title on left, dismiss button on right
        // Use a grid with two columns so the X button aligns right
        var headerGrid = r.CreateGrid();
        if (headerGrid == null) return;
        r.SetTag(headerGrid, "uprooted-no-recolor");
        r.AddGridColumn(headerGrid, 1.0); // star column for title
        r.AddGridColumnAuto(headerGrid);   // auto column for dismiss button

        // Title
        var title = r.CreateTextBlock("Dev Console", 14, TEXT_BRIGHT, "SemiBold");
        if (title != null)
        {
            r.SetTag(title, "dyn-fg:TextPrimary");
            r.BindToDynamicResource(title, "Foreground", "TextPrimary");
            r.SetVerticalAlignment(title, "Center");
            r.SetGridColumn(title, 0);
            r.AddChild(headerGrid, title);
        }

        // Dismiss button (x)
        var dismissText = r.CreateTextBlock("\u2715", 14, TEXT_MUTED);
        if (dismissText != null)
        {
            r.SetTag(dismissText, "dyn-fg:TextSecondary");
            r.BindToDynamicResource(dismissText, "Foreground", "TextSecondary");
            var dismissBtn = r.CreateBorder(null, 4, dismissText);
            if (dismissBtn != null)
            {
                r.SetTag(dismissBtn, "dyn-bg:HighlightLight");
                r.SetPadding(dismissBtn, 6, 2, 6, 2);
                r.SetCursorHand(dismissBtn);
                r.SetGridColumn(dismissBtn, 1);
                r.SubscribeClickReleased(dismissBtn, () => Dismiss());
                r.SubscribeEvent(dismissBtn, "PointerEntered", () =>
                {
                    r.SetBackground(dismissBtn, ROW_HOVER_BG);
                    r.BindToDynamicResource(dismissBtn, "Background", "HighlightLight");
                });
                r.SubscribeEvent(dismissBtn, "PointerExited", () => r.SetBackground(dismissBtn, (string?)null));
                r.AddChild(headerGrid, dismissBtn);
            }
        }

        r.AddChild(parent, headerGrid);
    }

    private static void BuildSeparator(AvaloniaReflection r, object parent)
    {
        var sep = r.CreateBorder(SEPARATOR_CLR, 0);
        if (sep == null) return;
        r.SetTag(sep, "dyn-bg:Border");
        r.BindToDynamicResource(sep, "Background", "Border");
        r.SetHeight(sep, 1);
        r.SetMargin(sep, 0, 4, 0, 4);
        r.AddChild(parent, sep);
    }

    private static void BuildSectionHeader(AvaloniaReflection r, object parent, string title)
    {
        var text = r.CreateTextBlock(title, 11, SECTION_HEADER, "Bold");
        if (text == null) return;
        r.SetTag(text, "dyn-fg:TextTertiary");
        r.BindToDynamicResource(text, "Foreground", "TextTertiary");
        r.SetMargin(text, 4, 4, 0, 2);
        r.AddChild(parent, text);
    }

    private static void BuildActionRow(AvaloniaReflection r, object parent, string label, Action onClick)
    {
        var text = r.CreateTextBlock(label, 13, TEXT_BRIGHT);
        if (text == null) return;
        r.SetTag(text, "dyn-fg:TextPrimary");
        r.BindToDynamicResource(text, "Foreground", "TextPrimary");

        var row = r.CreateBorder(null, 4, text);
        if (row == null) return;
        r.SetPadding(row, 8, 5, 8, 5);
        r.SetCursorHand(row);

        r.SubscribeClickReleased(row, () =>
        {
            Dismiss();
            try { onClick(); }
            catch (Exception ex) { Logger.Log(Tag, $"Action '{label}' error: {ex.Message}"); }
        });

        r.SubscribeEvent(row, "PointerEntered", () =>
        {
            r.SetBackground(row, ROW_HOVER_BG);
            r.BindToDynamicResource(row, "Background", "HighlightLight");
        });
        r.SubscribeEvent(row, "PointerExited", () => r.SetBackground(row, (string?)null));

        r.AddChild(parent, row);
    }

    private static void BuildToggleRow(AvaloniaReflection r, object parent,
        string label, bool currentValue, Action<bool> onToggle)
    {
        // Grid: label | toggle indicator
        var grid = r.CreateGrid();
        if (grid == null) return;
        r.SetTag(grid, "uprooted-no-recolor");
        r.AddGridColumn(grid, 1.0); // star column for label
        r.AddGridColumnAuto(grid);   // auto column for toggle

        var text = r.CreateTextBlock(label, 13, TEXT_BRIGHT);
        if (text != null)
        {
            r.SetTag(text, "dyn-fg:TextPrimary");
            r.BindToDynamicResource(text, "Foreground", "TextPrimary");
            r.SetVerticalAlignment(text, "Center");
            r.SetGridColumn(text, 0);
            r.AddChild(grid, text);
        }

        // Toggle indicator: small colored pill
        bool isOn = currentValue;
        var toggleText = r.CreateTextBlock(isOn ? "ON" : "OFF", 10, "#FFFFFF", "Bold");
        var toggleBg = isOn ? TOGGLE_ON_BG : TOGGLE_OFF_BG;
        var togglePill = r.CreateBorder(toggleBg, 8, toggleText);
        if (togglePill != null && toggleText != null)
        {
            // Toggle pill keeps hardcoded colors (semantic green/grey, not theme-dependent)
            r.SetTag(togglePill, "uprooted-no-recolor");
            r.SetTag(toggleText, "uprooted-no-recolor");
            r.SetPadding(togglePill, 8, 2, 8, 2);
            r.SetGridColumn(togglePill, 1);
            r.SetVerticalAlignment(togglePill, "Center");
            r.AddChild(grid, togglePill);
        }

        // Wrap in clickable row
        var row = r.CreateBorder(null, 4, grid);
        if (row == null) return;
        r.SetPadding(row, 8, 5, 8, 5);
        r.SetCursorHand(row);

        r.SubscribeClickReleased(row, () =>
        {
            isOn = !isOn;
            try { onToggle(isOn); }
            catch (Exception ex) { Logger.Log(Tag, $"Toggle '{label}' error: {ex.Message}"); }

            // Update visual
            try
            {
                toggleText?.GetType().GetProperty("Text")?.SetValue(toggleText, isOn ? "ON" : "OFF");
                if (togglePill != null)
                    r.SetBackground(togglePill, isOn ? TOGGLE_ON_BG : TOGGLE_OFF_BG);
            }
            catch { }
        });

        r.SubscribeEvent(row, "PointerEntered", () =>
        {
            r.SetBackground(row, ROW_HOVER_BG);
            r.BindToDynamicResource(row, "Background", "HighlightLight");
        });
        r.SubscribeEvent(row, "PointerExited", () => r.SetBackground(row, (string?)null));

        r.AddChild(parent, row);
    }

    // ===== Utility Helpers =====

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

    private static bool ApplyNativeTitleBarButtonLayout(object supportBtn, object injectedBtn)
    {
        try
        {
            bool copiedAny = false;
            foreach (var prop in new[]
            {
                "SvgWidth", "SvgHeight", "SvgOpacity", "SvgBorderOpacity",
                "Width", "Height", "MinWidth", "MinHeight", "MaxWidth", "MaxHeight",
                "Margin", "HorizontalAlignment", "VerticalAlignment"
            })
            {
                copiedAny |= TryCopySharedProperty(supportBtn, injectedBtn, prop);
            }
            if (copiedAny)
                Logger.Log(Tag, "Mirrored native SupportButton layout properties");
            return copiedAny;
        }
        catch { return false; }
    }

    private static bool TryCopySharedProperty(object source, object target, string propertyName)
    {
        try
        {
            var srcProp = source.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            var dstProp = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (srcProp == null || dstProp == null || !srcProp.CanRead || !dstProp.CanWrite) return false;
            var value = srcProp.GetValue(source);
            if (value == null) return false;
            dstProp.SetValue(target, value);
            return true;
        }
        catch { return false; }
    }
}
