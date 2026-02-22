using System.Collections;
using System.Runtime.InteropServices;

namespace Uprooted;

/// <summary>
/// Theme engine v2: resource-first approach targeting Root's actual 32 DynamicResource keys
/// in Application.Resources.ThemeDictionaries[variant], plus SimpleTheme keys in Styles[0].Resources.
///
/// Three override layers:
///   1. ThemeDictionaries[activeVariant] — Root's 32 custom keys (DynamicResource auto-propagates)
///   2. Styles[0].Resources — SimpleTheme keys (sidebar/pane + standard Avalonia controls)
///   3. Single delayed tree walk — catches hardcoded code-behind colors (one-shot, not continuous)
///
/// OKLCH palette generation for perceptually uniform color derivation.
/// </summary>
internal class ThemeEngine
{
    private readonly AvaloniaReflection _r;
    private string? _activeThemeName;

    // Custom theme state
    private Dictionary<string, string>? _customPalette;
    private string? _customAccent;
    private string? _customBg;
    private string _customSvgMode = "auto";

    // Custom ping/reply color (persists across theme switches)
    private string? _customPingColor;

    // Track what we've overridden for revert
    private readonly HashSet<string> _overriddenThemeDictKeys = new();
    private readonly Dictionary<string, object?> _savedStyleOriginals = new();
    private readonly HashSet<string> _addedStyleKeys = new();

    // Track controls recolored by the walker, so we can ClearValue on revert
    // to let DynamicResource reassert. Set of (control, propertyFieldName) pairs.
    private readonly HashSet<(object control, string propertyField)> _walkerRecolored = new();

    // Cache of types that do NOT have a Foreground property, so WalkAndRecolor skips
    // GetProperty("Foreground") for them on every subsequent node (reflection is slow).
    private readonly HashSet<Type> _noForegroundTypes = new();

    // Cached MethodInfo for Application.TryGetResource(object, ThemeVariant?, out object?)
    private System.Reflection.MethodInfo? _tryGetResourceMethod;

    // Single-shot walk timer
    private System.Threading.Timer? _walkTimer;

    // Throttle for live preview
    private long _lastLiveUpdateTick;
    private long _lastLiveWalkTick;

    // Track what colors the walker LAST painted onto injected controls.
    // Used by BuildTreeColorMap as the map SOURCE (what's actually on screen),
    // instead of ContentPages statics which drift between walks.
    private string _walkedTextPri = DefaultTextWhite;
    private string _walkedTextSec = DefaultTextMuted;
    private string _walkedTextTer = DefaultTextDim;
    private string _walkedCardBg = DefaultCardBg;
    private string _walkedBorder = DefaultCardBorder;
    private string _walkedAccent = DefaultAccentGreen;

    private const string DefaultTextWhite = "#FFF2F2F2";
    private const string DefaultTextMuted = "#A3F2F2F2";
    private const string DefaultTextDim = "#66F2F2F2";
    private const string DefaultCardBg = "#FF0F1923";
    private const string DefaultCardBorder = "#19F2F2F2";
    private const string DefaultAccentGreen = "#FF3B6AF8";

    // Cached reference to the variant dict we're overriding
    private IDictionary? _themeDicts;
    private object? _activeVariantKey;
    private object? _activeVariantDict;

    // Whether we've subscribed to variant changes
    private bool _variantChangeSubscribed;

    // Guard: true while we're programmatically switching the variant for theme application.
    // Prevents the variant change handler from reverting our theme or re-injecting sidebar.
    private bool _switchingVariantForTheme;

    // Requested variant before we switched for theme brightness.
    // Value can be null (System/Default), so _hasOriginalVariantKey tracks whether
    // we captured a value for restoration.
    private object? _originalVariantKey;
    private bool _hasOriginalVariantKey;
    private string? _originalActualVariantName;

    // Callback for SidebarInjector to re-inject on Root variant change
    private Action? _onVariantChanged;

    public string? ActiveThemeName => _activeThemeName;

    public ThemeEngine(AvaloniaReflection r)
    {
        _r = r;
        Logger.Log("Theme", "ThemeEngine v2 initialized (resource-first + OKLCH)");
    }

    /// <summary>
    /// Resolve and cache Application.TryGetResource(object, ThemeVariant?, out object?).
    /// Avoids scanning all methods on every call during live preview.
    /// </summary>
    private System.Reflection.MethodInfo? ResolveTryGetResource(object app)
    {
        if (_tryGetResourceMethod != null) return _tryGetResourceMethod;
        foreach (var m in app.GetType().GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
        {
            if (m.Name != "TryGetResource") continue;
            var ps = m.GetParameters();
            if (ps.Length == 3 && ps[2].IsOut)
            {
                _tryGetResourceMethod = m;
                return m;
            }
        }
        return null;
    }

    /// <summary>
    /// Set a callback that fires when Root's theme variant changes (Dark/Light/PureDark).
    /// SidebarInjector uses this to re-inject with fresh colors.
    /// </summary>
    public void SetVariantChangedCallback(Action? callback) => _onVariantChanged = callback;

    /// <summary>
    /// Subscribe to variant changes unconditionally — needed even when no Uprooted theme
    /// is active so the sidebar re-injects with correct colors on Root variant change.
    /// Call from StartupHook after theme engine init.
    /// </summary>
    public void EnsureVariantChangeSubscribed()
    {
        SubscribeToVariantChanges();
    }

    /// <summary>
    /// Read the current resolved values for Root's theme keys from ThemeDictionaries.
    /// If an Uprooted theme is active, returns our overridden values.
    /// If no Uprooted theme, returns Root's native values.
    /// Returns null on reflection failure.
    /// </summary>
    public Dictionary<string, string>? ReadLiveRootColors()
    {
        try
        {
            // Use Application-level TryGetResource — this is exactly how DynamicResource
            // resolves at runtime. It checks ThemeDictionaries, MergedDictionaries,
            // deferred factories, and respects the active variant. No manual variant
            // lookup needed.
            var app = _r.GetAppCurrent();
            if (app == null) return null;

            var variantKey = _r.GetActiveThemeVariant();

            var tryGet = ResolveTryGetResource(app);
            if (tryGet == null)
            {
                Logger.Log("Theme", "ReadLiveRootColors: TryGetResource not found on Application");
                return null;
            }


            var result = new Dictionary<string, string>();
            string[] colorKeys = { "BrandPrimary", "BrandSecondary", "BrandTertiary",
                "TextPrimary", "TextSecondary", "TextTertiary", "TextWhite",
                "BackgroundPrimary", "BackgroundSecondary", "BackgroundTertiary", "BackgroundElevated", "BackgroundButtonSurface", "Input",
                "Border", "HighlightLight", "HighlightNormal", "HighlightStrong",
                "Info", "Warning", "Error", "Muted", "Link" };

            foreach (var k in colorKeys)
            {
                try
                {
                    var args = new object?[] { k, variantKey, null };
                    var found = tryGet.Invoke(app, args);
                    if (found is not true || args[2] == null) continue;

                    var resource = args[2];
                    // Use A/R/G/B properties — ToString() can return named colors like "White"
                    var colorProp = resource.GetType().GetProperty("Color");
                    if (colorProp != null)
                    {
                        var color = colorProp.GetValue(resource);
                        if (color != null)
                        {
                            var ct = color.GetType();
                            var a = ct.GetProperty("A")?.GetValue(color) as byte?;
                            var rv = ct.GetProperty("R")?.GetValue(color) as byte?;
                            var gv = ct.GetProperty("G")?.GetValue(color) as byte?;
                            var bv = ct.GetProperty("B")?.GetValue(color) as byte?;
                            if (a.HasValue && rv.HasValue && gv.HasValue && bv.HasValue)
                                result[k] = $"#{a.Value:X2}{rv.Value:X2}{gv.Value:X2}{bv.Value:X2}";
                        }
                    }
                }
                catch { }
            }

            if (result.Count > 0)
                Logger.Log("Theme", $"ReadLiveRootColors: {result.Count} keys read (variant={variantKey})");

            return result.Count > 0 ? result : null;
        }
        catch (Exception ex)
        {
            Logger.Log("Theme", $"ReadLiveRootColors error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Returns the current Root theme variant name (e.g. "Dark", "Light", "PureDark").
    /// </summary>
    /// <summary>
    /// Read a single Root theme key from the live Application resources.
    /// Returns #AARRGGBB hex or null.
    /// </summary>
    public string? ReadLiveRootColor(string key)
    {
        try
        {
            var app = _r.GetAppCurrent();
            if (app == null) return null;
            var variantKey = _r.GetActiveThemeVariant();

            var tryGet = ResolveTryGetResource(app);
            if (tryGet == null) return null;

            var args = new object?[] { key, variantKey, null };
            var found = tryGet.Invoke(app, args);
            if (found is not true || args[2] == null) return null;

            var colorProp = args[2].GetType().GetProperty("Color");
            if (colorProp == null) return null;
            var color = colorProp.GetValue(args[2]);
            if (color == null) return null;

            var ct = color.GetType();
            var a = ct.GetProperty("A")?.GetValue(color) as byte?;
            var r = ct.GetProperty("R")?.GetValue(color) as byte?;
            var g = ct.GetProperty("G")?.GetValue(color) as byte?;
            var b = ct.GetProperty("B")?.GetValue(color) as byte?;
            if (a.HasValue && r.HasValue && g.HasValue && b.HasValue)
                return $"#{a.Value:X2}{r.Value:X2}{g.Value:X2}{b.Value:X2}";
        }
        catch { }
        return null;
    }

    public string GetCurrentRootVariant()
    {
        var v = _r.GetActiveThemeVariant();
        return v?.ToString() ?? "Dark";
    }

    public string GetCurrentRootRequestedVariant()
    {
        var requested = _r.GetRequestedThemeVariant();
        var name = requested?.ToString();
        if (string.IsNullOrWhiteSpace(name) || name.Equals("Default", StringComparison.OrdinalIgnoreCase))
            return "System";
        return name;
    }

    /// <summary>
    /// Returns Root's native requested variant (pre-theme) while an Uprooted theme is active.
    /// Falls back to current requested variant when no snapshot is available.
    /// </summary>
    public string GetNativeRootRequestedVariant()
    {
        if (_activeThemeName != null && _hasOriginalVariantKey)
        {
            var name = _originalVariantKey?.ToString();
            if (string.IsNullOrWhiteSpace(name) || name.Equals("Default", StringComparison.OrdinalIgnoreCase))
                return "System";
            return name;
        }
        return GetCurrentRootRequestedVariant();
    }

    /// <summary>
    /// Returns Root's native effective variant (pre-theme) while an Uprooted theme is active.
    /// Falls back to current effective variant when no snapshot is available.
    /// </summary>
    public string GetNativeRootVariant()
    {
        if (_activeThemeName != null && _hasOriginalVariantKey && !string.IsNullOrWhiteSpace(_originalActualVariantName))
            return _originalActualVariantName!;
        return GetCurrentRootVariant();
    }

    // ===== DWM title bar color (Windows 11) =====

    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref uint value, int size);

    private const int DWMWA_CAPTION_COLOR = 35;
    private const string DefaultDarkBg = "#0D1521";

    private void UpdateTitleBarColor(string hexColor)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        try
        {
            var hwnd = GetMainWindowHandle();
            if (hwnd == IntPtr.Zero) return;

            var hex = hexColor.TrimStart('#');
            if (hex.Length == 8) hex = hex[2..]; // Strip alpha prefix
            if (hex.Length != 6) return;
            byte r = Convert.ToByte(hex[0..2], 16);
            byte g = Convert.ToByte(hex[2..4], 16);
            byte b = Convert.ToByte(hex[4..6], 16);
            uint colorRef = (uint)(r | (g << 8) | (b << 16));

            DwmSetWindowAttribute(hwnd, DWMWA_CAPTION_COLOR, ref colorRef, sizeof(uint));
            Logger.Log("Theme", "Title bar color set to " + hexColor);
        }
        catch (Exception ex)
        {
            Logger.Log("Theme", "Title bar color error: " + ex.Message);
        }
    }

    private IntPtr GetMainWindowHandle()
    {
        var mainWindow = _r.GetMainWindow();
        if (mainWindow == null) return IntPtr.Zero;
        try
        {
            var method = mainWindow.GetType().GetMethod("TryGetPlatformHandle");
            if (method != null)
            {
                var platformHandle = method.Invoke(mainWindow, null);
                if (platformHandle != null)
                {
                    var handleProp = platformHandle.GetType().GetProperty("Handle");
                    if (handleProp != null)
                        return (IntPtr)handleProp.GetValue(platformHandle)!;
                }
            }
        }
        catch { }
        return IntPtr.Zero;
    }

    // ===== Public API =====

    /// <summary>Apply a named preset theme.</summary>
    public bool ApplyTheme(string name)
    {
        using var ev = WideEvent.Begin("Theme", "apply");
        ev.Set("theme", name);

        var themeName = name.ToLower().Trim();
        if (!PresetThemes.TryGetValue(themeName, out var preset))
        {
            ev.SetError("unknown theme");
            return false;
        }

        ev.Set("root_keys", preset.RootKeys.Count);
        ev.Set("simple_keys", preset.SimpleThemeKeys.Count);
        var result = ApplyThemeInternal(themeName, preset.RootKeys, preset.SimpleThemeKeys, ev);
        ev.Set("result", result ? "ok" : "failed");
        return result;
    }

    /// <summary>Apply a custom theme from user-chosen accent + background.</summary>
    public bool ApplyCustomTheme(string accentHex, string bgHex, string? textHex = null, string svgMode = "auto")
    {
        using var ev = WideEvent.Begin("Theme", "apply_custom");
        ev.Set("accent", accentHex);
        ev.Set("bg", bgHex);
        ev.Set("text", textHex);
        ev.Set("svg_mode", svgMode);

        if (!ColorUtils.IsValidHex(accentHex) || !ColorUtils.IsValidHex(bgHex))
        {
            ev.SetError("invalid custom colors");
            return false;
        }

        var rootKeys = GenerateV2Palette(accentHex, bgHex, textHex);
        var simpleKeys = DeriveSimpleThemeKeys(accentHex, bgHex);
        _customPalette = MergePalettes(rootKeys, simpleKeys);
        _customAccent = accentHex;
        _customBg = bgHex;
        _customSvgMode = svgMode;
        ev.Set("palette_fields", _customPalette.Count);
        var result = ApplyThemeInternal("custom", rootKeys, simpleKeys, ev);
        ev.Set("result", result ? "ok" : "failed");
        return result;
    }

    /// <summary>
    /// Lightweight custom theme update for live preview during color picker drag.
    /// Only updates resource dictionaries — no revert, no tree walk.
    /// Throttled to ~60fps.
    /// </summary>
    public void UpdateCustomThemeLive(string accentHex, string bgHex, string? textHex = null, string svgMode = "auto")
    {
        if (!ColorUtils.IsValidHex(accentHex) || !ColorUtils.IsValidHex(bgHex))
            return;

        // Bootstrap with full apply if not yet active
        if (_activeThemeName != "custom" || _activeVariantDict == null)
        {
            using var bootEv = WideEvent.Begin("Theme", "live_preview");
            bootEv.Set("accent", accentHex);
            bootEv.Set("bg", bgHex);
            bootEv.Set("action", "bootstrap");
            ApplyCustomTheme(accentHex, bgHex, textHex, svgMode);
            _lastLiveUpdateTick = Environment.TickCount64;
            return;
        }

        // Throttle: ~60fps
        long now = Environment.TickCount64;
        if (now - _lastLiveUpdateTick < 16) return;
        _lastLiveUpdateTick = now;

        using var ev = WideEvent.Begin("Theme", "live_preview");
        ev.Set("accent", accentHex);
        ev.Set("bg", bgHex);
        ev.Set("svg_mode", svgMode);

        // Check if brightness crossed the dark/light threshold — if so, need a full
        // re-apply to switch Root's variant (for correct SVGs).
        var rootKeys = GenerateV2Palette(accentHex, bgHex, textHex);
        bool themeNeedsDark = ThemeNeedsDarkSvgs(rootKeys);
        var currentVariantName = _r.GetActiveThemeVariant()?.ToString() ?? "Dark";
        bool currentIsDark = currentVariantName != "Light";
        if (themeNeedsDark != currentIsDark)
        {
            ev.Set("action", "threshold_reapply");
            ev.Set("needs_dark", themeNeedsDark);
            ev.Set("variant", currentVariantName);
            ApplyCustomTheme(accentHex, bgHex, textHex, svgMode);
            _lastLiveUpdateTick = Environment.TickCount64;
            return;
        }

        ev.Set("action", "incremental");
        _customAccent = accentHex;
        _customBg = bgHex;
        _customSvgMode = svgMode;
        var simpleKeys = DeriveSimpleThemeKeys(accentHex, bgHex);
        _customPalette = MergePalettes(rootKeys, simpleKeys);

        // Update ThemeDictionaries
        OverrideThemeDictKeys(rootKeys);

        // Update Styles[0].Resources
        OverrideSimpleThemeKeys(simpleKeys);

        // Walk injected controls to recolor with new palette values.
        // BuildTreeColorMap reads ContentPages statics (current on-screen colors)
        // and maps them to the new palette. Walk BEFORE updating statics.
        // Throttled: walk every 100ms max (vs 16ms dict updates).
        long walkNow = Environment.TickCount64;
        bool walked = false;
        if (walkNow - _lastLiveWalkTick >= 100)
        {
            _lastLiveWalkTick = walkNow;
            WalkAllWindows();
            walked = true;
        }
        ev.Set("walked", walked);

        // NOW update ContentPages statics to match new palette
        // (so next walk maps from current→new correctly)
        ContentPages.UpdateLiveColors(accentHex, bgHex, _customPalette);

        // Update DWM title bar
        if (rootKeys.TryGetValue("BackgroundPrimary", out var bgPrimary))
            UpdateTitleBarColor(bgPrimary);

        // Re-apply ping color if set
        if (!string.IsNullOrEmpty(_customPingColor))
            ApplyPingColorToThemeDicts();
    }

    /// <summary>Remove all theme overrides and restore Root defaults.</summary>
    public void RevertTheme()
    {
        using var ev = WideEvent.Begin("Theme", "revert");

        // Cancel walk timer
        _walkTimer?.Dispose();
        _walkTimer = null;

        // Phase 1: Remove our direct entries from ThemeDictionaries
        RemoveThemeDictOverrides();

        // Phase 2: Restore Styles[0].Resources
        var styleRes = _r.GetStyleResources(0);
        int stylesRestored = 0;
        int stylesRemoved = 0;
        if (styleRes != null)
        {
            foreach (var (key, original) in _savedStyleOriginals)
            {
                try
                {
                    _r.AddResource(styleRes, key, original);
                    stylesRestored++;
                }
                catch (Exception ex)
                {
                    ev.Set("restore_fail_" + key, ex.Message);
                }
            }

            foreach (var key in _addedStyleKeys)
            {
                try { if (_r.RemoveResource(styleRes, key)) stylesRemoved++; }
                catch { }
            }
        }
        ev.Set("styles_restored", stylesRestored);
        ev.Set("styles_removed", stylesRemoved);

        _savedStyleOriginals.Clear();
        _addedStyleKeys.Clear();

        // Phase 2b: ClearValue on ALL walker-recolored controls to remove LocalValue overrides.
        int walkerCleared = _walkerRecolored.Count;
        foreach (var (ctrl, propField) in _walkerRecolored)
        {
            try { _r.ClearValue(ctrl, propField); }
            catch { }
        }
        _walkerRecolored.Clear();
        ev.Set("walker_cleared", walkerCleared);

        // Phase 3: Restore default DWM title bar
        UpdateTitleBarColor(DefaultDarkBg);

        _activeThemeName = null;
        _customPalette = null;
        _customSvgMode = "auto";

        // Phase 4: Restore original requested variant if we switched it
        if (_hasOriginalVariantKey)
        {
            ev.Set("restore_variant", _originalVariantKey?.ToString() ?? "<null/default>");
            _switchingVariantForTheme = true;
            try
            {
                _r.SetRequestedThemeVariant(_originalVariantKey);
            }
            finally
            {
                _switchingVariantForTheme = false;
                _originalVariantKey = null;
                _hasOriginalVariantKey = false;
                _originalActualVariantName = null;
            }
            _activeVariantDict = null;
            _activeVariantKey = null;
        }

        // Phase 5: Force Root's native theme refresh by toggling the variant.
        // This makes Avalonia re-resolve ALL DynamicResource bindings across the entire tree.
        // Our ClearValue'd controls pick up Root's native colors via inheritance/resolution.
        {
            var requestedBefore = _r.GetRequestedThemeVariant();
            var current = _r.GetActiveThemeVariant();
            var opposite = _r.GetThemeVariantByName(
                current?.ToString() == "Light" ? "Dark" : "Light");
            if (current != null && opposite != null)
            {
                _switchingVariantForTheme = true;
                try
                {
                    _r.SetRequestedThemeVariant(opposite);
                    // Preserve true requested intent, including null/default ("System").
                    _r.SetRequestedThemeVariant(requestedBefore);
                }
                finally
                {
                    _switchingVariantForTheme = false;
                }
                ev.Set("variant_toggled", true);
            }

            // Restore our injected controls (tagged dyn-*) from Root's now-correct live colors.
            // These don't have DynamicResource bindings, so the variant toggle alone won't fix them.
            var liveColors = ReadLiveRootColors();
            if (liveColors != null)
            {
                int restored = 0;
                var topLevels = _r.GetAllTopLevels();
                foreach (var tl in topLevels)
                    restored += RestoreTaggedControls(tl, liveColors, 0);
                ev.Set("tagged_restored", restored);
            }
        }

        // Reset walked colors to defaults for next theme application
        _walkedTextPri = DefaultTextWhite;
        _walkedTextSec = DefaultTextMuted;
        _walkedTextTer = DefaultTextDim;
        _walkedCardBg = DefaultCardBg;
        _walkedBorder = DefaultCardBorder;
        _walkedAccent = DefaultAccentGreen;
    }

    /// <summary>Set a custom ping/reply highlight color.</summary>
    public void SetCustomPingColor(string hex)
    {
        _customPingColor = hex;
        Logger.Log("Theme", "Custom ping color set: " + hex);
        _r.RunOnUIThread(() =>
        {
            try { ApplyPingColorToThemeDicts(); }
            catch (Exception ex) { Logger.Log("Theme", "Ping color apply error: " + ex.Message); }
        });
    }

    /// <summary>Clear the custom ping color override.</summary>
    public void ClearCustomPingColor()
    {
        _customPingColor = null;
        Logger.Log("Theme", "Custom ping color cleared");

        // Restore SelfMention keys from active palette or remove overrides
        _r.RunOnUIThread(() =>
        {
            try
            {
                // Keys to restore: mention colors + Error (left border was overridden with neon)
                var pingKeys = new[] { "SelfMention", "SelfMentionBackground", "SelfMentionBorder", "Error" };

                if (_activeThemeName != null)
                {
                    var palette = GetPalette();
                    if (palette != null)
                    {
                        foreach (var key in pingKeys)
                        {
                            if (palette.TryGetValue(key, out var hex))
                                SetThemeDictValue(key, hex);
                        }
                    }
                }
                else
                {
                    // No theme active — remove our overrides so Root's defaults reassert
                    EnsureVariantDictResolved();
                    foreach (var key in pingKeys)
                        RemoveThemeDictKey(key);
                }

                // Restore SimpleTheme ErrorColor/ErrorBrush
                var styleRes = _r.GetStyleResources(0);
                if (styleRes != null)
                {
                    try
                    {
                        var errColor = _r.ParseColor("#F03F36");
                        if (errColor != null) _r.AddResource(styleRes, "ErrorColor", errColor);
                        var errBrush = _r.CreateBrush("#F03F36");
                        if (errBrush != null) _r.AddResource(styleRes, "ErrorBrush", errBrush);
                    }
                    catch { }
                }
            }
            catch (Exception ex) { Logger.Log("Theme", "Ping color clear error: " + ex.Message); }
        });
    }

    public string? GetCustomPingColor() => _customPingColor;

    /// <summary>Get the full palette (Root keys + SimpleTheme keys merged).</summary>
    public Dictionary<string, string>? GetPalette()
    {
        if (_activeThemeName == "custom" && _customPalette != null)
            return _customPalette;
        if (_activeThemeName != null && PresetThemes.TryGetValue(_activeThemeName, out var preset))
            return MergePalettes(preset.RootKeys, preset.SimpleThemeKeys);
        return null;
    }

    public string GetAccentColor()
    {
        if (_activeThemeName == "custom" && _customAccent != null)
            return _customAccent;
        if (_activeThemeName != null && PresetThemes.TryGetValue(_activeThemeName, out var preset))
        {
            if (preset.RootKeys.TryGetValue("BrandPrimary", out var hex)) return hex;
        }
        return "#3B6AF8"; // Root's default blue accent
    }

    public string GetBgPrimary()
    {
        if (_activeThemeName == "custom" && _customPalette != null)
        {
            if (_customPalette.TryGetValue("BackgroundPrimary", out var hex)) return hex;
            if (_customBg != null) return _customBg;
        }
        if (_activeThemeName != null && PresetThemes.TryGetValue(_activeThemeName, out var preset))
        {
            if (preset.RootKeys.TryGetValue("BackgroundPrimary", out var hex)) return hex;
        }
        return DefaultDarkBg;
    }

    /// <summary>Run a visual tree walk immediately (for SidebarInjector).</summary>
    public void WalkVisualTreeNow()
    {
        if (_activeThemeName == null) return;
        try
        {
            int recolored = WalkAllWindows();
            if (recolored > 0)
                Logger.Log("Theme", "Manual walk: " + recolored + " recolored");
        }
        catch { }
    }

    /// <summary>Schedule a single deferred walk (for SidebarInjector).</summary>
    public void ScheduleWalkBurst()
    {
        if (_activeThemeName == null) return;

        // Debounce: cancel any pending walk and schedule one 150ms out.
        // Running immediately on every tab switch stacks UI-thread walks during rapid navigation.
        ScheduleDelayedWalk(150);
    }

    // ===== Core Engine =====

    private bool ApplyThemeInternal(string themeName,
        Dictionary<string, string> rootKeys,
        Dictionary<string, string> simpleThemeKeys,
        WideEvent? parentEv = null)
    {
        using var ev = parentEv != null
            ? WideEvent.Begin("Theme", "apply_internal", parentEv)
            : WideEvent.Begin("Theme", "apply_internal");
        ev.Set("theme", themeName);
        ev.Set("root_keys", rootKeys.Count);
        ev.Set("simple_keys", simpleThemeKeys.Count);

        // Revert any existing theme first
        RevertTheme();

        // Subscribe to variant changes (once)
        SubscribeToVariantChanges();

        // Phase 0: Switch Root's variant if theme brightness requires different SVGs.
        // Dark backgrounds need Dark variant (light-colored SVGs).
        // Light backgrounds need Light variant (dark-colored SVGs).
        SwitchVariantIfNeeded(rootKeys, ev);

        // Phase 1: Override ThemeDictionaries[activeVariant] with Root's 32 keys
        EnsureVariantDictResolved();
        OverrideThemeDictKeys(rootKeys);

        // Phase 2: Override Styles[0].Resources with SimpleTheme keys
        var styleRes = _r.GetStyleResources(0);
        int styleOverrides = 0;
        if (styleRes != null)
        {
            foreach (var (key, hex) in simpleThemeKeys)
            {
                try
                {
                    // Save original before overriding
                    if (!_savedStyleOriginals.ContainsKey(key) && !_addedStyleKeys.Contains(key))
                    {
                        try
                        {
                            var original = _r.GetResource(styleRes, key);
                            if (original != null)
                                _savedStyleOriginals[key] = original;
                            else
                                _addedStyleKeys.Add(key);
                        }
                        catch { _addedStyleKeys.Add(key); }
                    }

                    bool isBrush = key.Contains("Brush") || key.EndsWith("Fill");
                    if (isBrush)
                    {
                        var brush = _r.CreateBrush(hex);
                        if (brush != null) { _r.AddResource(styleRes, key, brush); styleOverrides++; }
                    }
                    else
                    {
                        var color = _r.ParseColor(hex);
                        if (color != null) { _r.AddResource(styleRes, key, color); styleOverrides++; }
                    }
                }
                catch (Exception ex)
                {
                    ev.Set("style_fail_" + key, ex.Message);
                }
            }
        }
        ev.Set("style_overrides", styleOverrides);

        _activeThemeName = themeName;

        // Phase 3: Merge palette (pure computation — no UI-thread work).
        // _walked* fields still hold the PREVIOUS colors so the upcoming walk can
        // map old→new correctly. ContentPages statics are updated in Phase 3b.
        _customPalette = MergePalettes(rootKeys, simpleThemeKeys);

        // Phase 3b: Update ContentPages statics to match new palette.
        // Safe to do before the walk: _walkedText* is updated only inside
        // UpdateWalkedColors(), which runs AFTER a successful WalkAllWindows(),
        // so Source 1 in BuildTreeColorMap still correctly maps previous→new.
        ContentPages.UpdateLiveColors(
            rootKeys.GetValueOrDefault("BrandPrimary", GetAccentColor()),
            rootKeys.GetValueOrDefault("BackgroundPrimary", DefaultDarkBg),
            _customPalette);

        // Phase 4: Deferred walk — runs off the critical UI-thread path.
        // ThemeDictionary overrides (DynamicResource) auto-propagate immediately,
        // so only hardcoded-color controls need the walk. 150ms is enough time for
        // the initial render to settle; ScheduleWalkBurst handles any later navigation.
        // Using a single schedule replaces the previous pattern of an immediate sync
        // walk + a 500ms follow-up, saving ~30ms of UI-thread block at startup.
        ScheduleDelayedWalk(150);

        // Phase 5: Update DWM title bar color
        if (rootKeys.TryGetValue("BackgroundPrimary", out var titleBarBg))
            UpdateTitleBarColor(titleBarBg);

        // Phase 6: Apply custom ping color if set
        if (!string.IsNullOrEmpty(_customPingColor))
            ApplyPingColorToThemeDicts();

        ev.Set("theme_dict_keys", _overriddenThemeDictKeys.Count);
        return true;
    }

    /// <summary>
    /// Switch Root's variant to match theme brightness so SVGs load correctly.
    /// Dark themes need Dark variant (light-colored SVG icons).
    /// Light themes need Light variant (dark-colored SVG icons).
    /// Sets _originalVariantKey so RevertTheme can restore it.
    /// </summary>
    private void SwitchVariantIfNeeded(Dictionary<string, string> rootKeys, WideEvent? ev = null)
    {
        // Determine what variant the theme needs based on background luminance
        bool themeIsDark = ThemeNeedsDarkSvgs(rootKeys); // luminance <= 0.3 → dark SVGs → Dark variant
        string targetVariant = themeIsDark ? "Dark" : "Light";

        var currentVariant = _r.GetActiveThemeVariant();
        var currentName = currentVariant?.ToString() ?? "Dark";

        // PureDark uses Dark SVGs, so treat it as Dark for comparison
        bool currentIsDark = currentName != "Light";

        if (themeIsDark == currentIsDark)
        {
            // Already on the right variant family — no switch needed
            ev?.Set("variant_switch", "not_needed");
            ev?.Set("variant", currentName);
            _originalVariantKey = null;
            _hasOriginalVariantKey = false;
            _originalActualVariantName = null;
            return;
        }

        // Need to switch. Get the target ThemeVariant object.
        var targetKey = _r.GetThemeVariantByName(targetVariant);
        if (targetKey == null)
        {
            ev?.Set("variant_switch", "target_not_found");
            ev?.Set("target_variant", targetVariant);
            _originalVariantKey = null;
            _hasOriginalVariantKey = false;
            _originalActualVariantName = null;
            return;
        }

        ev?.Set("variant_switch", currentName + "_to_" + targetVariant);
        _originalVariantKey = _r.GetRequestedThemeVariant();
        _hasOriginalVariantKey = true;
        _originalActualVariantName = currentName;

        // Guard so our variant change handler skips this switch
        _switchingVariantForTheme = true;
        try
        {
            _r.SetRequestedThemeVariant(targetKey);
        }
        finally
        {
            _switchingVariantForTheme = false;
        }

        // Clear cached variant dict so EnsureVariantDictResolved picks up the new one
        _activeVariantDict = null;
        _activeVariantKey = null;
    }

    // ===== ThemeDictionaries Override =====

    private void EnsureVariantDictResolved()
    {
        if (_activeVariantDict != null) return;

        var appResources = _r.GetAppResources();
        if (appResources == null)
        {
            Logger.Log("Theme", "WARNING: App.Resources is null");
            return;
        }

        _themeDicts = _r.GetThemeDictionaries(appResources);
        if (_themeDicts == null)
        {
            Logger.Log("Theme", "WARNING: ThemeDictionaries is null — falling back to Styles[0] only");
            return;
        }

        // Get the active variant
        _activeVariantKey = _r.GetActiveThemeVariant();
        if (_activeVariantKey == null)
        {
            Logger.Log("Theme", "WARNING: ActualThemeVariant is null");
            return;
        }

        Logger.Log("Theme", "Active variant: " + _activeVariantKey);

        // Try to get the dict for the active variant directly
        try
        {
            if (_themeDicts.Contains(_activeVariantKey))
            {
                _activeVariantDict = _themeDicts[_activeVariantKey];
                Logger.Log("Theme", "ThemeDictionaries[" + _activeVariantKey + "] resolved directly");
                return;
            }
        }
        catch { }

        // Fallback: find variant by string name
        var variantName = _activeVariantKey.ToString();
        if (variantName != null)
        {
            var key = _r.FindVariantByName(_themeDicts, variantName);
            if (key != null)
            {
                _activeVariantKey = key;
                try
                {
                    _activeVariantDict = _themeDicts[key];
                    Logger.Log("Theme", "ThemeDictionaries resolved by name: " + variantName);
                    return;
                }
                catch { }
            }
        }

        // Last resort: try known variant names
        foreach (var name in new[] { "PureDark", "Dark", "Default" })
        {
            var key = _r.FindVariantByName(_themeDicts, name);
            if (key != null)
            {
                _activeVariantKey = key;
                try
                {
                    _activeVariantDict = _themeDicts[key];
                    Logger.Log("Theme", "ThemeDictionaries fallback: " + name);
                    return;
                }
                catch { }
            }
        }

        Logger.Log("Theme", "WARNING: Could not resolve any ThemeDictionary variant");
    }

    /// <summary>
    /// Override Root's 32 keys in ThemeDictionaries[activeVariant].
    /// Direct entries in a ResourceDictionary beat MergedDictionaries[0] entries,
    /// so DynamicResource bindings auto-propagate our values.
    /// </summary>
    private void OverrideThemeDictKeys(Dictionary<string, string> rootKeys)
    {
        if (_activeVariantDict == null) return;

        int count = 0;
        int skipped = 0;
        foreach (var (key, hex) in rootKeys)
        {
            if (PreservedSemanticKeys.Contains(key))
            {
                skipped++;
                continue;
            }
            try
            {
                SetThemeDictValue(key, hex);
                _overriddenThemeDictKeys.Add(key);
                count++;
            }
            catch (Exception ex)
            {
                Logger.Log("Theme", "  ThemeDict override failed for " + key + ": " + ex.Message);
            }
        }
        if (skipped > 0)
            Logger.Log("Theme", "ThemeDictionaries: " + skipped + " semantic keys preserved");
        Logger.Log("Theme", "ThemeDictionaries: " + count + " keys overridden");
    }

    // Keys stored as Color (not brush) in Root's ThemeDictionaries
    private static readonly HashSet<string> ColorTypeKeys = new(StringComparer.OrdinalIgnoreCase) { "DropShadow" };

    // Keys that must NOT be overridden in ThemeDictionaries because Root uses them
    // for semantic purposes unrelated to branding:
    //   BrandSecondary → online status indicator color (green #A8FF5D)
    //                     Used by MemberView, MemberProfileView, CommunityTabView, InviteMembersView
    //                     Overriding it recolors all online dots to the theme accent
    private static readonly HashSet<string> PreservedSemanticKeys = new(StringComparer.OrdinalIgnoreCase)
    {
        "BrandSecondary"
    };

    private void SetThemeDictValue(string key, string hex)
    {
        if (_activeVariantDict == null) return;

        if (key == "ScrollShadow")
        {
            // ScrollShadow is a LinearGradientBrush
            var brush = _r.CreateLinearGradientBrush(0, 0, 0, 1, new[]
            {
                (hex, 0.0),
                (ColorUtils.WithAlpha(hex, 0x00), 1.0)
            });
            if (brush != null)
                _r.AddResource(_activeVariantDict, key, brush);
        }
        else if (ColorTypeKeys.Contains(key))
        {
            // DropShadow is stored as Color in Root's ThemeDictionaries
            var color = _r.ParseColor(hex);
            if (color != null)
                _r.AddResource(_activeVariantDict, key, color);
        }
        else
        {
            // All other Root keys are ImmutableSolidColorBrush — store as SolidColorBrush
            var brush = _r.CreateBrush(hex);
            if (brush != null)
                _r.AddResource(_activeVariantDict, key, brush);
        }
    }

    /// <summary>
    /// Remove our direct entries from ThemeDictionaries[variant].
    /// MergedDictionaries[0] values (Root's originals) reassert via DynamicResource.
    /// </summary>
    private void RemoveThemeDictOverrides()
    {
        if (_activeVariantDict == null || _overriddenThemeDictKeys.Count == 0) return;

        int removed = 0;
        foreach (var key in _overriddenThemeDictKeys)
        {
            try
            {
                if (_r.RemoveResource(_activeVariantDict, key))
                    removed++;
            }
            catch { }
        }
        Logger.Log("Theme", "ThemeDictionaries: removed " + removed + "/" + _overriddenThemeDictKeys.Count + " overrides");
        _overriddenThemeDictKeys.Clear();

        // Clear cached variant refs so they re-resolve next apply
        _activeVariantDict = null;
        _activeVariantKey = null;
        _themeDicts = null;
    }

    private void RemoveThemeDictKey(string key)
    {
        if (_activeVariantDict == null) return;
        try { _r.RemoveResource(_activeVariantDict, key); }
        catch { }
        _overriddenThemeDictKeys.Remove(key);
    }

    // ===== SVG Asset Path Swapping =====
    // Root's ThemeDictionaries contain ~220 SVG path resources (keys ending in "SVG").
    // Light variant uses "Light Theme/" folder (light-colored icons for light backgrounds).
    // Dark/PureDark use "Dark Theme/" folder (light-colored icons for dark backgrounds).
    // When an Uprooted theme overrides colors, we may need to swap SVG sets to match
    // the theme's background brightness.

    private const string DarkSvgFolder = "/Resources/Assets/SVGs/Dark Theme/";
    private const string LightSvgFolder = "/Resources/Assets/SVGs/Light Theme/";

    /// <summary>
    /// Determines whether the theme needs Dark or Light SVG assets based on
    /// BackgroundPrimary luminance. Dark backgrounds need Dark SVGs (light icons),
    /// light backgrounds need Light SVGs (dark icons).
    /// </summary>
    private static bool ThemeNeedsDarkSvgs(Dictionary<string, string> rootKeys)
    {
        if (rootKeys.TryGetValue("BackgroundPrimary", out var bg) && ColorUtils.IsValidHex(bg))
            return ColorUtils.Luminance(bg) <= 0.3;
        return true; // Default to Dark SVGs
    }

    /// <summary>
    /// Get the current SVG folder used by the active variant ("Dark Theme" or "Light Theme").
    /// </summary>
    private string GetCurrentVariantSvgFolder()
    {
        var variantName = _activeVariantKey?.ToString() ?? "Dark";
        return variantName == "Light" ? LightSvgFolder : DarkSvgFolder;
    }

    /// <summary>
    /// Swap SVG asset paths in ThemeDictionaries if the theme's required SVG set
    /// differs from what the active Root variant provides.
    /// E.g., Uprooted dark theme on Light variant → swap Light SVGs to Dark SVGs.
    /// </summary>
    private void SwapSvgPathsIfNeeded(Dictionary<string, string> rootKeys, string svgMode = "auto")
    {
        if (_activeVariantDict == null) return;

        // Determine what SVG set Root is currently providing vs what our theme needs
        var currentFolder = GetCurrentVariantSvgFolder();
        bool needsDark;
        if (svgMode == "light") needsDark = false;
        else if (svgMode == "dark") needsDark = true;
        else needsDark = ThemeNeedsDarkSvgs(rootKeys);  // auto
        var neededFolder = needsDark ? DarkSvgFolder : LightSvgFolder;

        // If current variant already uses the right SVG set, nothing to do
        if (currentFolder == neededFolder) return;

        Logger.Log("Theme", $"SVG swap: {currentFolder} → {neededFolder} (variant={_activeVariantKey}, needsDark={needsDark})");

        // SVG paths are direct entries on the variant dict (not in MergedDictionaries).
        // Root adds them via IDictionary.Add, not AddDeferred.
        // Enumerate the variant dict itself to find SVG keys and swap their paths.
        int swapped = 0;

        // Collect SVG entries first to avoid modifying dict during enumeration
        var svgEntries = new List<(string key, string path)>();
        _r.EnumerateResources(_activeVariantDict, (key, value) =>
        {
            if (!key.EndsWith("SVG")) return;
            var path = value?.ToString();
            if (path != null && path.Contains(currentFolder))
                svgEntries.Add((key, path));
        });

        foreach (var (key, path) in svgEntries)
        {
            var newPath = path.Replace(currentFolder, neededFolder);
            try
            {
                _r.AddResource(_activeVariantDict, key, newPath);
                _overriddenThemeDictKeys.Add(key);
                swapped++;
            }
            catch { }
        }

        Logger.Log("Theme", $"SVG swap: {swapped} paths overridden");
    }


    /// <summary>Override SimpleTheme keys in Styles[0].Resources (for live updates).</summary>
    private void OverrideSimpleThemeKeys(Dictionary<string, string> simpleKeys)
    {
        var styleRes = _r.GetStyleResources(0);
        if (styleRes == null) return;

        foreach (var (key, hex) in simpleKeys)
        {
            try
            {
                bool isBrush = key.Contains("Brush") || key.EndsWith("Fill");
                if (isBrush)
                {
                    var brush = _r.CreateBrush(hex);
                    if (brush != null) _r.AddResource(styleRes, key, brush);
                }
                else
                {
                    var color = _r.ParseColor(hex);
                    if (color != null) _r.AddResource(styleRes, key, color);
                }
            }
            catch { }
        }
    }

    // ===== Variant Change Subscription =====

    private void SubscribeToVariantChanges()
    {
        if (_variantChangeSubscribed) return;
        _variantChangeSubscribed = true;

        _r.SubscribeActualThemeVariantChanged(() =>
        {
            // Skip if WE triggered the variant change (for SVG/theme purposes)
            if (_switchingVariantForTheme)
            {
                Logger.Log("Theme", "Variant change from our own switch — skipping handler");
                return;
            }

            var newVariant = GetCurrentRootVariant();
            Logger.Log("Theme", $"ActualThemeVariant changed -> {newVariant}");

            if (_activeThemeName != null)
            {
                // User switched Root's native variant while Uprooted theme was active.
                // Auto-revert: the user chose a Root theme, so respect that choice.
                Logger.Log("Theme", $"Root variant changed while Uprooted theme '{_activeThemeName}' active — auto-reverting");
                RevertTheme();
            }

            // Always notify sidebar — it needs to re-inject with correct colors
            // regardless of whether an Uprooted theme is active
            _onVariantChanged?.Invoke();
        });
        Logger.Log("Theme", "Subscribed to ActualThemeVariantChanged");
    }

    // ===== Ping Color =====

    private void ApplyPingColorToThemeDicts()
    {
        if (string.IsNullOrEmpty(_customPingColor)) return;

        // Ping color works standalone (no theme required) — resolve variant dict if needed
        EnsureVariantDictResolved();
        if (_activeVariantDict == null)
        {
            Logger.Log("Theme", "Ping color: cannot resolve ThemeDictionaries variant — skipping");
            return;
        }

        // Subscribe to variant changes so ping color survives Dark/PureDark switches
        SubscribeToVariantChanges();

        var hex = _customPingColor;

        // SelfMention keys — background wash and inline mention pills
        SetThemeDictValue("SelfMention", ColorUtils.WithAlpha(hex, 0x66));
        _overriddenThemeDictKeys.Add("SelfMention");
        SetThemeDictValue("SelfMentionBackground", ColorUtils.WithAlpha(hex, 0x26));
        _overriddenThemeDictKeys.Add("SelfMentionBackground");
        SetThemeDictValue("SelfMentionBorder", ColorUtils.WithAlpha(hex, 0x4D));
        _overriddenThemeDictKeys.Add("SelfMentionBorder");

        // Left border stripe — Root uses DynamicResourceExtension("Error") for the
        // MessageBackgroundBorder.BorderBrush on mention messages. Override Error with
        // a neon version of the ping color (OKLCH: boost lightness to 0.75, max chroma).
        var neonHex = MakeNeon(hex);
        SetThemeDictValue("Error", neonHex);
        _overriddenThemeDictKeys.Add("Error");

        // Also override SimpleTheme highlight keys for AvaloniaEdit caret/selection
        var styleRes = _r.GetStyleResources(0);
        if (styleRes != null)
        {
            try
            {
                var color = _r.ParseColor(hex);
                if (color != null) _r.AddResource(styleRes, "HighlightForegroundColor", color);
                var brush = _r.CreateBrush(hex);
                if (brush != null) _r.AddResource(styleRes, "HighlightForegroundBrush", brush);
                var selColor = _r.ParseColor(ColorUtils.WithAlpha(hex, 0x60));
                if (selColor != null) _r.AddResource(styleRes, "TextSelectionHighlightColor", selColor);
                // Keep ErrorColor/ErrorBrush in SimpleTheme at neon too for consistency
                var neonColor = _r.ParseColor(neonHex);
                if (neonColor != null) _r.AddResource(styleRes, "ErrorColor", neonColor);
                var neonBrush = _r.CreateBrush(neonHex);
                if (neonBrush != null) _r.AddResource(styleRes, "ErrorBrush", neonBrush);
            }
            catch { }
        }

        Logger.Log("Theme", "Ping color applied to ThemeDicts: " + hex + " (neon border: " + neonHex + ")");
    }

    /// <summary>
    /// Create a "neon" version of a color for the mention left border stripe.
    /// Boosts lightness to 0.75 and maximizes chroma in OKLCH while preserving hue.
    /// </summary>
    private static string MakeNeon(string hex)
    {
        var (l, c, h) = ColorUtils.HexToOklch(hex);
        // Neon: bright (L=0.75) with maximum chroma (0.35 is near-max for most hues;
        // OklchToRgb gamut-maps it down to the sRGB boundary automatically)
        return ColorUtils.OklchToHex(0.75, Math.Max(c, 0.35), h);
    }

    // ===== OKLCH Palette Generation =====

    /// <summary>
    /// Generate Root's 32 custom resource keys from accent + background using OKLCH.
    /// </summary>
    private static Dictionary<string, string> GenerateV2Palette(
        string accentHex, string bgHex, string? textHex = null)
    {
        var (aL, aC, aH) = ColorUtils.HexToOklch(accentHex);
        var (bL, bC, bH) = ColorUtils.HexToOklch(bgHex);

        // Allow full lightness range, just prevent pure extremes
        bL = Math.Clamp(bL, 0.05, 0.93);

        var palette = new Dictionary<string, string>();

        // --- Brand colors ---
        palette["BrandPrimary"]   = accentHex;
        palette["BrandSecondary"] = ColorUtils.OklchToHex(
            Math.Clamp(aL + 0.15, 0, 1), aC * 0.7, (aH + 140) % 360);
        palette["BrandTertiary"]  = ColorUtils.OklchToHex(
            Math.Clamp(aL + 0.08, 0, 1), aC * 0.5, (aH + 210) % 360);

        // --- Background hierarchy (smooth direction-aware L steps) ---
        // Smoothly interpolate step direction: at bL=0.5 steps are 0 (no offset).
        // Dark bg: secondary lighter, tertiary/input darker.
        // Light bg: secondary darker, tertiary/input lighter.
        // dir: -1.0 at bL=0.05, 0.0 at bL=0.50, +1.0 at bL=0.93
        double dir = (bL - 0.05) / (0.93 - 0.05) * 2.0 - 1.0; // linear -1..+1
        double step1 = 0.02 * (1.0 - dir);   // Secondary: +0.04 dark, 0 mid, -0.04 light
        double step2 = -0.015 * (1.0 - dir);  // Tertiary: -0.03 dark, 0 mid, +0.03 light
        double step3 = -0.01 * (1.0 - dir);   // Input: -0.02 dark, 0 mid, +0.02 light
        palette["BackgroundPrimary"]   = ColorUtils.OklchToHex(bL, bC, bH);
        palette["BackgroundSecondary"] = ColorUtils.OklchToHex(Math.Clamp(bL + step1, 0.05, 0.95), bC, bH);
        palette["BackgroundTertiary"]  = ColorUtils.OklchToHex(Math.Clamp(bL + step2, 0.05, 0.95), bC, bH);
        palette["BackgroundElevated"]  = DeriveElevatedSurface(palette["BackgroundSecondary"]);
        palette["BackgroundButtonSurface"] = DeriveButtonSurface(palette["BackgroundSecondary"]);
        palette["Input"]               = ColorUtils.OklchToHex(Math.Clamp(bL + step3, 0.05, 0.95), bC * 0.8, bH);

        // --- Text colors (custom or auto-derived) ---
        // TextPrimary: user-specified or smooth inverse of background lightness.
        // No hard threshold — smoothly transitions from light text to dark text
        // as background lightness increases.
        if (!string.IsNullOrEmpty(textHex) && ColorUtils.IsValidHex(textHex))
        {
            palette["TextPrimary"] = textHex;
        }
        else
        {
            // Smooth inverse: bL=0.05→textL=0.93, bL=0.93→textL=0.15
            // Linear map ensures no snap at any lightness value
            double textL = 0.93 - (bL - 0.05) / (0.93 - 0.05) * (0.93 - 0.15);
            textL = Math.Clamp(textL, 0.15, 0.95);
            palette["TextPrimary"] = ColorUtils.OklchToHex(textL, 0.005, aH);
        }

        // Secondary/Tertiary:
        // - Dark themes: match Root's native behavior (alpha variants of TextPrimary).
        //   This avoids overly bright "muted" text on very dark custom backgrounds.
        // - Light themes: keep opaque derived shades for readability on light surfaces.
        {
            if (bL < 0.5)
            {
                palette["TextSecondary"] = ColorUtils.WithAlpha(palette["TextPrimary"], 0xA3);
                palette["TextTertiary"]  = ColorUtils.WithAlpha(palette["TextPrimary"], 0x66);
            }
            else
            {
                var (tL, tC, tH) = ColorUtils.HexToOklch(palette["TextPrimary"]);
                double secL = tL + (0.50 - tL) * 0.35;
                double terL = tL + (0.50 - tL) * 0.60;
                palette["TextSecondary"] = ColorUtils.OklchToHex(Math.Clamp(secL, 0.10, 0.90), tC, tH);
                palette["TextTertiary"]  = ColorUtils.OklchToHex(Math.Clamp(terL, 0.10, 0.90), tC, tH);
            }
        }
        palette["TextWhite"] = "#F2F2F2";

        // --- UI elements (smooth direction-aware) ---
        // Border: lighter than bg on dark, darker on light. Smooth via dir.
        palette["Border"] = ColorUtils.OklchToHex(
            Math.Clamp(bL + 0.08 * (1.0 - dir), 0.05, 0.95),
            bC * 0.4, ((aH + bH) / 2) % 360);

        // Highlights: smooth blend from white-alpha (dark bg) to black-alpha (light bg).
        // At mid-range, both blend equally. Uses dir to interpolate alpha channels.
        // dir: -1=dark (white overlay), +1=light (black overlay)
        double whiteWeight = Math.Clamp((1.0 - dir) / 2.0, 0.0, 1.0);  // 1.0 at dark, 0.0 at light
        double blackWeight = 1.0 - whiteWeight;
        int hlA = (int)(0x0A * whiteWeight + 0x0A * blackWeight);
        int hnA = (int)(0x19 * whiteWeight + 0x19 * blackWeight);
        int hsA = (int)(0x30 * whiteWeight + 0x30 * blackWeight);
        palette["HighlightLight"]  = whiteWeight >= 0.5
            ? $"#{hlA:X2}FFFFFF" : $"#{hlA:X2}000000";
        palette["HighlightNormal"] = whiteWeight >= 0.5
            ? $"#{hnA:X2}FFFFFF" : $"#{hnA:X2}000000";
        palette["HighlightStrong"] = whiteWeight >= 0.5
            ? $"#{hsA:X2}FFFFFF" : $"#{hsA:X2}000000";

        // --- Semantic (kept from Root) ---
        palette["Info"]    = "#5BC0DE";
        palette["Warning"] = "#F0AD4E";
        palette["Error"]   = "#F03F36";

        // --- Derived (smooth direction-aware) ---
        palette["Muted"] = ColorUtils.OklchToHex(
            Math.Clamp(bL + 0.18 * (1.0 - dir), 0.05, 0.95), 0.015, bH);
        palette["Link"]  = ColorUtils.OklchToHex(
            Math.Clamp(aL + 0.12, 0, 1), aC * 0.55, ((aH - 15) + 360) % 360);

        // --- Mentions ---
        palette["SelfMention"]           = ColorUtils.WithAlpha(accentHex, 0x66);
        palette["SelfMentionBackground"] = ColorUtils.WithAlpha(accentHex, 0x26);
        palette["SelfMentionBorder"]     = ColorUtils.WithAlpha(accentHex, 0x4D);
        palette["OtherMention"]           = ColorUtils.WithAlpha(palette["Link"], 0x66);
        palette["OtherMentionBackground"] = ColorUtils.WithAlpha(palette["Link"], 0x26);
        palette["OtherMentionBorder"]     = ColorUtils.WithAlpha(palette["Link"], 0x4D);
        palette["RoleMention"]            = "#669B59B6";
        palette["RoleMentionBackground"]  = "#269B59B6";
        palette["RoleMentionBorder"]      = "#4D9B59B6";
        palette["RoleMentionText"]        = "#9B59B6";
        palette["ChannelMention"]           = ColorUtils.WithAlpha(palette["Warning"], 0x66);
        palette["ChannelMentionBackground"] = ColorUtils.WithAlpha(palette["Warning"], 0x26);
        palette["ChannelMentionBorder"]     = ColorUtils.WithAlpha(palette["Warning"], 0x4D);
        palette["ChannelMentionText"]       = palette["Warning"];

        // --- ScrollShadow (bg-colored gradient) ---
        palette["ScrollShadow"] = palette["BackgroundPrimary"];

        // --- Drop shadow (smooth: heavier on dark, lighter on light) ---
        int shadowAlpha = (int)(0x80 * whiteWeight + 0x30 * blackWeight);
        palette["DropShadow"] = $"#{Math.Clamp(shadowAlpha, 0x20, 0x80):X2}000000";

        return palette;
    }

    /// <summary>
    /// Derive SimpleTheme keys for Styles[0].Resources (sidebar, standard Avalonia controls).
    /// </summary>
    private static Dictionary<string, string> DeriveSimpleThemeKeys(string accentHex, string bgHex)
    {
        var (bL, bC, bH) = ColorUtils.HexToOklch(bgHex);
        // Smooth direction: matches GenerateV2Palette's dir calculation
        double dir = (Math.Clamp(bL, 0.05, 0.93) - 0.05) / (0.93 - 0.05) * 2.0 - 1.0;
        double step = 0.02 * (1.0 - dir);
        var bgSecondary = ColorUtils.OklchToHex(
            Math.Clamp(bL + step, 0.05, 0.95), bC, bH);

        return new Dictionary<string, string>
        {
            ["ThemeAccentColor"]  = accentHex,
            ["ThemeAccentBrush"]  = accentHex,
            ["ThemeControlHighlightLowColor"] = bgSecondary,
            ["ThemeControlHighlightLowBrush"] = bgSecondary,
            ["HighlightForegroundColor"] = accentHex,
            ["HighlightForegroundBrush"] = accentHex,
            ["ErrorColor"] = "#F03F36",
            ["ErrorBrush"] = "#F03F36",
        };
    }

    /// <summary>
    /// Elevation step for nested card surfaces:
    /// lighter on dark themes, darker on light themes.
    /// </summary>
    private static string DeriveElevatedSurface(string secondaryHex)
    {
        try
        {
            return ColorUtils.Luminance(secondaryHex) > 0.5
                ? ColorUtils.Darken(secondaryHex, 4.5)
                : ColorUtils.Lighten(secondaryHex, 4.5);
        }
        catch
        {
            return secondaryHex;
        }
    }

    /// <summary>
    /// Subtle button surface used by secondary actions (e.g. Open Logs/Refresh).
    /// Mirrors ContentPages: AdjustForHighlight(bg, 2).
    /// </summary>
    private static string DeriveButtonSurface(string secondaryHex)
    {
        try
        {
            return ColorUtils.Luminance(secondaryHex) > 0.5
                ? ColorUtils.Darken(secondaryHex, 2)
                : ColorUtils.Lighten(secondaryHex, 2);
        }
        catch
        {
            return secondaryHex;
        }
    }

    private static Dictionary<string, string> MergePalettes(
        Dictionary<string, string> rootKeys, Dictionary<string, string> simpleKeys)
    {
        var merged = new Dictionary<string, string>(rootKeys);
        foreach (var (k, v) in simpleKeys)
            merged[k] = v;
        return merged;
    }

    // ===== Minimal Tree Walk =====

    private void ScheduleDelayedWalk(int delayMs)
    {
        _walkTimer?.Dispose();
        _walkTimer = new System.Threading.Timer(_ =>
        {
            _r.RunOnUIThread(() =>
            {
                try
                {
                    int recolored = WalkAllWindows();
                    if (recolored > 0)
                        Logger.Log("Theme", "Delayed walk: " + recolored + " recolored");
                }
                catch (Exception ex)
                {
                    Logger.Log("Theme", "Delayed walk error: " + ex.Message);
                }
            });
        }, null, delayMs, System.Threading.Timeout.Infinite); // one-shot
    }

    private int WalkAllWindows()
    {
        if (_activeThemeName == null) return 0;
        var palette = GetPalette();
        if (palette == null) return 0;

        // Build a color map from Root's known original ARGB colors → our palette
        var colorMap = BuildTreeColorMap(palette);
        if (colorMap.Count == 0) return 0;

        int total = 0;
        var topLevels = _r.GetAllTopLevels();
        foreach (var topLevel in topLevels)
        {
            try { total += WalkAndRecolor(topLevel, colorMap, 0); }
            catch { }
        }

        // Record what we just painted so next walk maps from correct source
        if (total > 0) UpdateWalkedColors(palette);

        return total;
    }

    /// <summary>
    /// Build a map from CURRENT on-screen ARGB colors to new themed colors.
    /// Maps both hardcoded defaults AND previous theme colors to the new palette.
    /// </summary>
    /// <summary>
    /// Build color map for TEXT colors only (Foreground matching on untagged Root controls).
    /// Maps from ALL known text color sources → new palette targets.
    /// Includes: walked colors, hardcoded defaults, AND Root's native variant colors.
    /// This ensures the walker can find and recolor text regardless of variant state.
    /// </summary>
    private Dictionary<string, string> BuildTreeColorMap(Dictionary<string, string> palette)
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!palette.TryGetValue("TextPrimary", out var textPri)) return map;

        var newPri = NormalizeArgb(textPri);
        var newSec = palette.TryGetValue("TextSecondary", out var ts) ? NormalizeArgb(ts) : newPri;
        var newTer = palette.TryGetValue("TextTertiary", out var tt) ? NormalizeArgb(tt) : newPri;

        // Source 1: What the walker last painted
        map[NormalizeArgb(_walkedTextPri)] = newPri;
        map[NormalizeArgb(_walkedTextSec)] = newSec;
        map[NormalizeArgb(_walkedTextTer)] = newTer;

        // Source 2: Root's native Dark/PureDark variant text (from ROOT_THEME_SYSTEM_FINDINGS)
        map["#FFF2F2F2"] = newPri;  // Dark TextPrimary + TextWhite
        map["#A3F2F2F2"] = newSec;  // Dark TextSecondary (alpha)
        map["#66F2F2F2"] = newTer;  // Dark TextTertiary (alpha)
        map["#FFFFFFFF"] = newPri;  // Pure white (some controls)
        map["#FFE8E8E8"] = newPri;  // Near-white variant
        map["#FFEAEAEA"] = newPri;  // Near-white variant
        map["#FFE0E0E0"] = newPri;  // Light gray

        // Source 3: Root's native Light variant text (solid, NOT alpha)
        map["#FF131313"] = newPri;  // Light TextPrimary (confirmed from Root findings)
        map["#FF282828"] = newSec;  // Light TextSecondary
        map["#FF5E5E5E"] = newTer;  // Light TextTertiary
        map["#FF1A1A1A"] = newPri;  // Near-black variant
        map["#FF333333"] = newSec;  // Dark gray variant
        map["#FF000000"] = newPri;  // Pure black

        // Source 5: ContentPages statics (may differ from walked if statics updated between walks)
        var cpPri = NormalizeArgb(ContentPages.TextWhite);
        var cpSec = NormalizeArgb(ContentPages.TextMuted);
        var cpTer = NormalizeArgb(ContentPages.TextDim);
        if (!string.IsNullOrEmpty(cpPri)) map[cpPri] = newPri;
        if (!string.IsNullOrEmpty(cpSec)) map[cpSec] = newSec;
        if (!string.IsNullOrEmpty(cpTer)) map[cpTer] = newTer;

        return map;
    }

    /// <summary>
    /// Update _walked* fields to reflect what the walker just painted.
    /// Called AFTER a successful walk so the next walk knows what's on screen.
    /// </summary>
    private void UpdateWalkedColors(Dictionary<string, string> palette)
    {
        if (palette.TryGetValue("TextPrimary", out var tp)) _walkedTextPri = NormalizeArgb(tp);
        if (palette.TryGetValue("TextSecondary", out var ts)) _walkedTextSec = NormalizeArgb(ts);
        if (palette.TryGetValue("TextTertiary", out var tt)) _walkedTextTer = NormalizeArgb(tt);
        if (palette.TryGetValue("BackgroundSecondary", out var bg)) _walkedCardBg = NormalizeArgb(bg);
        if (palette.TryGetValue("Border", out var b)) _walkedBorder = NormalizeArgb(b);
        if (palette.TryGetValue("BrandPrimary", out var a)) _walkedAccent = NormalizeArgb(a);
    }

    /// <summary>
    /// Walk the tree and restore dyn-* tagged controls from live Root colors.
    /// Used after revert to set injected controls to Root's native values.
    /// </summary>
    private int RestoreTaggedControls(object visual, Dictionary<string, string> liveColors, int depth)
    {
        if (depth > 50) return 0;
        var tag = _r.GetTag(visual);
        if (tag == "uprooted-no-recolor") return 0;

        int count = 0;
        if (tag != null && tag.StartsWith("dyn-"))
        {
            foreach (var part in tag.Split(','))
            {
                var t = part.Trim();
                if (!t.StartsWith("dyn-")) continue;
                var ci = t.IndexOf(':', 4);
                if (ci < 0) continue;
                var mode = t[4..ci];
                var key = t[(ci + 1)..];
                var pn = mode switch { "fg" => "Foreground", "bg" => "Background", "bb" => "BorderBrush", _ => null };
                if (pn == null) continue;
                if (!liveColors.TryGetValue(key, out var hex)) continue;
                var brush = _r.CreateBrush(hex);
                if (brush == null) continue;
                var prop = visual.GetType().GetProperty(pn);
                if (prop != null) { prop.SetValue(visual, brush); count++; }
            }
        }

        foreach (var child in _r.GetVisualChildren(visual))
            count += RestoreTaggedControls(child, liveColors, depth + 1);
        return count;
    }

    private int WalkAndRecolor(object visual, Dictionary<string, string> colorMap, int depth)
    {
        if (depth > 50) return 0;

        var tag = _r.GetTag(visual);
        if (tag == "uprooted-no-recolor") return 0;

        int count = 0;
        try
        {
            // Strategy 1: Tag-based recoloring (deterministic, never desyncs)
            // Tags: "dyn-fg:TextPrimary", "dyn-bg:BackgroundSecondary"
            if (tag != null && tag.StartsWith("dyn-"))
            {
                var palette = GetPalette();
                if (palette != null)
                {
                    // Support multiple bindings: "dyn-fg:TextPrimary,dyn-bg:BackgroundSecondary"
                    foreach (var part in tag.Split(','))
                    {
                        var trimmed = part.Trim();
                        if (!trimmed.StartsWith("dyn-")) continue;
                        var colonIdx = trimmed.IndexOf(':', 4);
                        if (colonIdx < 0) continue;

                        var mode = trimmed[4..colonIdx];  // "fg" or "bg"
                        var key = trimmed[(colonIdx + 1)..]; // "TextPrimary"

                        if (!palette.TryGetValue(key, out var hex)) continue;
                        var newBrush = _r.CreateBrush(hex);
                        if (newBrush == null) continue;

                        var propName = mode switch
                        {
                            "fg" => "Foreground",
                            "bg" => "Background",
                            "bb" => "BorderBrush",
                            _ => null
                        };
                        if (propName != null)
                        {
                            var prop = visual.GetType().GetProperty(propName);
                            if (prop != null)
                            {
                                prop.SetValue(visual, newBrush);
                                _walkerRecolored.Add((visual, propName + "Property"));
                                count++;
                            }
                        }
                    }
                }
            }
            // Untagged controls: color-map matching for TEXT colors only.
            // Uses SetValueStylePriority so DynamicResource can reassert on revert.
            // This recolors Root's native text (settings tabs, profile labels, etc.)
            // _noForegroundTypes caches types where GetProperty("Foreground")==null to
            // avoid repeated reflection calls for the many non-text nodes in the tree.
            foreach (var propName in new[] { "Foreground" })
            {
                var vtype = visual.GetType();
                if (_noForegroundTypes.Contains(vtype)) continue;
                var prop = vtype.GetProperty(propName);
                if (prop == null) { _noForegroundTypes.Add(vtype); continue; }
                try
                {
                    var brush = prop.GetValue(visual);
                    if (brush == null) continue;
                    var colorStr = GetBrushColorString(brush);
                    if (colorStr == null) continue;

                    if (colorMap.TryGetValue(colorStr, out var replacement))
                    {
                        var newBrush = _r.CreateBrush(replacement);
                        if (newBrush != null)
                        {
                            prop.SetValue(visual, newBrush);
                            // Track for ClearValue on revert
                            _walkerRecolored.Add((visual, propName + "Property"));
                            count++;
                        }
                    }
                }
                catch { }
            }
        }
        catch { }

        foreach (var child in _r.GetVisualChildren(visual))
            count += WalkAndRecolor(child, colorMap, depth + 1);

        return count;
    }

    private string? GetBrushColorString(object brush)
    {
        try
        {
            var colorProp = brush.GetType().GetProperty("Color");
            if (colorProp == null) return null;
            var color = colorProp.GetValue(brush);
            return color?.ToString();
        }
        catch { return null; }
    }

    private static string NormalizeArgb(string hex)
    {
        var h = hex.TrimStart('#');
        if (h.Length == 6) h = "FF" + h;
        return "#" + h.ToUpperInvariant();
    }

    // ===== Diagnostics =====

    /// <summary>Dump all visual tree colors by frequency.</summary>
    public void DumpVisualTreeColors()
    {
        var topLevels = _r.GetAllTopLevels();
        var colorCounts = new Dictionary<string, int>();
        int totalNodes = 0;

        Logger.Log("Theme", "DumpVisualTreeColors: scanning " + topLevels.Count + " TopLevel instances");
        foreach (var tl in topLevels)
            ScanColors(tl, colorCounts, 0, ref totalNodes);

        var sorted = colorCounts.OrderByDescending(kv => kv.Value).ToList();
        Logger.Log("Theme", "=== VISUAL TREE COLOR DUMP (" + sorted.Count + " unique, " + totalNodes + " nodes) ===");
        int logged = 0;
        foreach (var (key, freq) in sorted)
        {
            if (logged >= 80) break;
            Logger.Log("Theme", "  [" + freq + "x] " + key);
            logged++;
        }
        Logger.Log("Theme", "=== END COLOR DUMP ===");
    }

    private void ScanColors(object visual, Dictionary<string, int> colorCounts, int depth, ref int nodeCount)
    {
        if (depth > 50) return;
        nodeCount++;

        try
        {
            foreach (var propName in new[] { "Background", "Foreground", "BorderBrush" })
            {
                var prop = visual.GetType().GetProperty(propName);
                if (prop == null) continue;
                try
                {
                    var brush = prop.GetValue(visual);
                    if (brush == null) continue;
                    var colorStr = GetBrushColorString(brush);
                    if (colorStr == null) continue;
                    var key = propName + ":" + colorStr;
                    colorCounts.TryGetValue(key, out int existing);
                    colorCounts[key] = existing + 1;
                }
                catch { }
            }
        }
        catch { }

        foreach (var child in _r.GetVisualChildren(visual))
            ScanColors(child, colorCounts, depth + 1, ref nodeCount);
    }

    /// <summary>Dump visual tree structure showing types and nesting.</summary>
    public void DumpVisualTreeStructure()
    {
        var mainWindow = _r.GetMainWindow();
        if (mainWindow == null) return;

        Logger.Log("Theme", "=== VISUAL TREE STRUCTURE ===");
        DumpNode(mainWindow, 0, 6);
        Logger.Log("Theme", "=== END STRUCTURE ===");
    }

    private void DumpNode(object visual, int depth, int maxDetailDepth)
    {
        if (depth > 20) return;
        var typeName = visual.GetType().Name;
        var fullTypeName = visual.GetType().FullName ?? typeName;
        int childCount = 0;
        foreach (var _ in _r.GetVisualChildren(visual)) childCount++;

        var indent = new string(' ', depth * 2);
        var bgStr = "";
        try
        {
            var bgProp = visual.GetType().GetProperty("Background");
            if (bgProp != null)
            {
                var brush = bgProp.GetValue(visual);
                if (brush != null)
                {
                    var color = GetBrushColorString(brush);
                    if (color != null) bgStr = " bg=" + color;
                }
            }
        }
        catch { }

        var displayName = fullTypeName.Contains("DotNet") || fullTypeName.Contains("Browser")
            ? fullTypeName : typeName;
        Logger.Log("Theme", indent + displayName + " [" + childCount + "]" + bgStr);

        int shown = 0;
        foreach (var child in _r.GetVisualChildren(visual))
        {
            if (shown >= 8 && depth >= maxDetailDepth)
            {
                Logger.Log("Theme", indent + "  ... (" + (childCount - shown) + " more)");
                break;
            }
            DumpNode(child, depth + 1, maxDetailDepth);
            shown++;
        }
    }

    /// <summary>Dump resource keys from Application.Resources and Styles.</summary>
    public void DumpResourceKeys()
    {
        Logger.Log("Theme", "=== RESOURCE KEY DUMP ===");

        var styleRes = _r.GetStyleResources(0);
        if (styleRes != null)
        {
            Logger.Log("Theme", "Styles[0].Resources type: " + styleRes.GetType().FullName);
            int count = 0;
            _r.EnumerateResources(styleRes, (k, v) =>
            {
                if (count < 60)
                    Logger.Log("Theme", "  [S0] [" + (v?.GetType().Name ?? "null") + "] " + k + " = " + v);
                count++;
            });
            Logger.Log("Theme", "Styles[0].Resources total: " + count);
        }

        var appRes = _r.GetAppResources();
        if (appRes != null)
        {
            int count = 0;
            _r.EnumerateResources(appRes, (k, v) =>
            {
                if (count < 30)
                    Logger.Log("Theme", "  [AR] [" + (v?.GetType().Name ?? "null") + "] " + k + " = " + v);
                count++;
            });
            Logger.Log("Theme", "Application.Resources total: " + count);

            // Dump ThemeDictionaries
            var themeDicts = _r.GetThemeDictionaries(appRes);
            if (themeDicts != null)
            {
                Logger.Log("Theme", "ThemeDictionaries count: " + themeDicts.Count);
                foreach (var key in themeDicts.Keys)
                {
                    Logger.Log("Theme", "  ThemeDictionary variant: " + key);
                    var dict = themeDicts[key];
                    int dcount = 0;
                    _r.EnumerateResources(dict, (k, v) =>
                    {
                        if (dcount < 20)
                            Logger.Log("Theme", "    [" + (v?.GetType().Name ?? "null") + "] " + k + " = " + v);
                        dcount++;
                    });
                    Logger.Log("Theme", "    Total: " + dcount);
                }
            }

            var merged = _r.GetMergedDictionaries(appRes);
            if (merged != null)
            {
                Logger.Log("Theme", "MergedDictionaries count: " + merged.Count);
                for (int i = 0; i < merged.Count && i < 5; i++)
                {
                    var dict = merged[i];
                    int dcount = 0;
                    _r.EnumerateResources(dict, (k, v) =>
                    {
                        if (dcount < 15)
                            Logger.Log("Theme", "  [MD" + i + "] [" + (v?.GetType().Name ?? "null") + "] " + k + " = " + v);
                        dcount++;
                    });
                    Logger.Log("Theme", "  MergedDict[" + i + "] total: " + dcount);
                }
            }
        }

        Logger.Log("Theme", "=== END RESOURCE KEY DUMP ===");
    }

    // ===== Preset Themes =====

    private record PresetThemeData(
        Dictionary<string, string> RootKeys,
        Dictionary<string, string> SimpleThemeKeys);

    private static readonly Dictionary<string, PresetThemeData> PresetThemes = new()
    {
        ["crimson"] = new PresetThemeData(
            RootKeys: new Dictionary<string, string>
            {
                // Brand
                ["BrandPrimary"]   = "#C42B1C",
                ["BrandSecondary"] = "#E06B60",
                ["BrandTertiary"]  = "#E88D84",

                // Backgrounds
                ["BackgroundPrimary"]   = "#241414",
                ["BackgroundSecondary"] = "#2C1818",
                ["BackgroundTertiary"]  = "#1A0E0E",
                ["BackgroundElevated"]  = DeriveElevatedSurface("#2C1818"),
                ["BackgroundButtonSurface"] = DeriveButtonSurface("#2C1818"),
                ["Input"]               = "#1E1010",

                // Text
                ["TextPrimary"]   = "#F0DADA",
                ["TextSecondary"] = "#A3F0DADA",
                ["TextTertiary"]  = "#66F0DADA",
                ["TextWhite"]     = "#F2F2F2",

                // UI
                ["Border"]          = "#402828",
                ["HighlightLight"]  = "#0AFFFFFF",
                ["HighlightNormal"] = "#19FFFFFF",
                ["HighlightStrong"] = "#30FFFFFF",

                // Semantic
                ["Info"]    = "#5BC0DE",
                ["Warning"] = "#F0AD4E",
                ["Error"]   = "#F03F36",

                // Derived
                ["Muted"] = "#3C2020",
                ["Link"]  = "#E06B60",

                // Mentions
                ["SelfMention"]             = "#66C42B1C",
                ["SelfMentionBackground"]   = "#26C42B1C",
                ["SelfMentionBorder"]       = "#4DC42B1C",
                ["OtherMention"]            = "#66E06B60",
                ["OtherMentionBackground"]  = "#26E06B60",
                ["OtherMentionBorder"]      = "#4DE06B60",
                ["RoleMention"]             = "#669B59B6",
                ["RoleMentionBackground"]   = "#269B59B6",
                ["RoleMentionBorder"]       = "#4D9B59B6",
                ["RoleMentionText"]         = "#9B59B6",
                ["ChannelMention"]            = "#66F0AD4E",
                ["ChannelMentionBackground"]  = "#26F0AD4E",
                ["ChannelMentionBorder"]      = "#4DF0AD4E",
                ["ChannelMentionText"]        = "#F0AD4E",

                // Shadow
                ["ScrollShadow"] = "#241414",
                ["DropShadow"]   = "#80000000",
            },
            SimpleThemeKeys: new Dictionary<string, string>
            {
                ["ThemeAccentColor"]  = "#C42B1C",
                ["ThemeAccentBrush"]  = "#C42B1C",
                ["ThemeControlHighlightLowColor"] = "#2C1818",
                ["ThemeControlHighlightLowBrush"] = "#2C1818",
                ["HighlightForegroundColor"] = "#C42B1C",
                ["HighlightForegroundBrush"] = "#C42B1C",
                ["ErrorColor"] = "#F03F36",
                ["ErrorBrush"] = "#F03F36",
            }
        ),

        ["cosmic-smoothie"] = new PresetThemeData(
            RootKeys: new Dictionary<string, string>
            {
                // Brand
                ["BrandPrimary"]   = "#7328BA",
                ["BrandSecondary"] = "#A15DE6",
                ["BrandTertiary"]  = "#B87CEF",

                // Backgrounds
                ["BackgroundPrimary"]   = "#0A041E",
                ["BackgroundSecondary"] = "#100822",
                ["BackgroundTertiary"]  = "#060216",
                ["BackgroundElevated"]  = DeriveElevatedSurface("#100822"),
                ["BackgroundButtonSurface"] = DeriveButtonSurface("#100822"),
                ["Input"]               = "#080318",

                // Text
                ["TextPrimary"]   = "#F4ECF8",
                ["TextSecondary"] = "#A3F4ECF8",
                ["TextTertiary"]  = "#66F4ECF8",
                ["TextWhite"]     = "#F2F2F2",

                // UI
                ["Border"]          = "#302040",
                ["HighlightLight"]  = "#0AFFFFFF",
                ["HighlightNormal"] = "#19FFFFFF",
                ["HighlightStrong"] = "#30FFFFFF",

                // Semantic
                ["Info"]    = "#5BC0DE",
                ["Warning"] = "#F0AD4E",
                ["Error"]   = "#F03F36",

                // Derived
                ["Muted"] = "#1C102E",
                ["Link"]  = "#A15DE6",

                // Mentions
                ["SelfMention"]             = "#667328BA",
                ["SelfMentionBackground"]   = "#267328BA",
                ["SelfMentionBorder"]       = "#4D7328BA",
                ["OtherMention"]            = "#66A15DE6",
                ["OtherMentionBackground"]  = "#26A15DE6",
                ["OtherMentionBorder"]      = "#4DA15DE6",
                ["RoleMention"]             = "#669B59B6",
                ["RoleMentionBackground"]   = "#269B59B6",
                ["RoleMentionBorder"]       = "#4D9B59B6",
                ["RoleMentionText"]         = "#9B59B6",
                ["ChannelMention"]            = "#66F0AD4E",
                ["ChannelMentionBackground"]  = "#26F0AD4E",
                ["ChannelMentionBorder"]      = "#4DF0AD4E",
                ["ChannelMentionText"]        = "#F0AD4E",

                // Shadow
                ["ScrollShadow"] = "#0A041E",
                ["DropShadow"]   = "#80000000",
            },
            SimpleThemeKeys: new Dictionary<string, string>
            {
                ["ThemeAccentColor"]  = "#7328BA",
                ["ThemeAccentBrush"]  = "#7328BA",
                ["ThemeControlHighlightLowColor"] = "#100822",
                ["ThemeControlHighlightLowBrush"] = "#100822",
                ["HighlightForegroundColor"] = "#7328BA",
                ["HighlightForegroundBrush"] = "#7328BA",
                ["ErrorColor"] = "#F03F36",
                ["ErrorBrush"] = "#F03F36",
            }
        ),

        ["loki"] = new PresetThemeData(
            RootKeys: new Dictionary<string, string>
            {
                // Brand — Moss/Trestle palette
                ["BrandPrimary"]   = "#2A5A40",
                ["BrandSecondary"] = "#508A62",
                ["BrandTertiary"]  = "#6AA07A",

                // Backgrounds
                ["BackgroundPrimary"]   = "#0F1210",
                ["BackgroundSecondary"] = "#151A15",
                ["BackgroundTertiary"]  = "#0A0D0A",
                ["BackgroundElevated"]  = DeriveElevatedSurface("#151A15"),
                ["BackgroundButtonSurface"] = DeriveButtonSurface("#151A15"),
                ["Input"]               = "#0C0F0C",

                // Text
                ["TextPrimary"]   = "#F0ECE0",
                ["TextSecondary"] = "#A3F0ECE0",
                ["TextTertiary"]  = "#66F0ECE0",
                ["TextWhite"]     = "#F2F2F2",

                // UI
                ["Border"]          = "#3D4A35",
                ["HighlightLight"]  = "#0AFFFFFF",
                ["HighlightNormal"] = "#19FFFFFF",
                ["HighlightStrong"] = "#30FFFFFF",

                // Semantic
                ["Info"]    = "#5BC0DE",
                ["Warning"] = "#F0AD4E",
                ["Error"]   = "#F03F36",

                // Derived
                ["Muted"] = "#202820",
                ["Link"]  = "#D4A847",   // Gold accent for Loki

                // Mentions
                ["SelfMention"]             = "#662A5A40",
                ["SelfMentionBackground"]   = "#262A5A40",
                ["SelfMentionBorder"]       = "#4D2A5A40",
                ["OtherMention"]            = "#66D4A847",
                ["OtherMentionBackground"]  = "#26D4A847",
                ["OtherMentionBorder"]      = "#4DD4A847",
                ["RoleMention"]             = "#669B59B6",
                ["RoleMentionBackground"]   = "#269B59B6",
                ["RoleMentionBorder"]       = "#4D9B59B6",
                ["RoleMentionText"]         = "#9B59B6",
                ["ChannelMention"]            = "#66F0AD4E",
                ["ChannelMentionBackground"]  = "#26F0AD4E",
                ["ChannelMentionBorder"]      = "#4DF0AD4E",
                ["ChannelMentionText"]        = "#F0AD4E",

                // Shadow
                ["ScrollShadow"] = "#0F1210",
                ["DropShadow"]   = "#80000000",
            },
            SimpleThemeKeys: new Dictionary<string, string>
            {
                ["ThemeAccentColor"]  = "#2A5A40",
                ["ThemeAccentBrush"]  = "#2A5A40",
                ["ThemeControlHighlightLowColor"] = "#151A15",
                ["ThemeControlHighlightLowBrush"] = "#151A15",
                ["HighlightForegroundColor"] = "#D4A847",
                ["HighlightForegroundBrush"] = "#D4A847",
                ["ErrorColor"] = "#F03F36",
                ["ErrorBrush"] = "#F03F36",
            }
        ),
    };
}
