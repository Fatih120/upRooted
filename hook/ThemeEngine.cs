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

    // Custom ping/reply color (persists across theme switches)
    private string? _customPingColor;

    // Track what we've overridden for revert
    private readonly HashSet<string> _overriddenThemeDictKeys = new();
    private readonly Dictionary<string, object?> _savedStyleOriginals = new();
    private readonly HashSet<string> _addedStyleKeys = new();

    // Single-shot walk timer
    private System.Threading.Timer? _walkTimer;

    // Throttle for live preview
    private long _lastLiveUpdateTick;

    // Cached reference to the variant dict we're overriding
    private IDictionary? _themeDicts;
    private object? _activeVariantKey;
    private object? _activeVariantDict;

    // Whether we've subscribed to variant changes
    private bool _variantChangeSubscribed;

    // Callback for SidebarInjector to re-inject on Root variant change
    private Action? _onVariantChanged;

    public string? ActiveThemeName => _activeThemeName;

    public ThemeEngine(AvaloniaReflection r)
    {
        _r = r;
        Logger.Log("Theme", "ThemeEngine v2 initialized (resource-first + OKLCH)");
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

            // Find Application.TryGetResource(object, ThemeVariant?, out object?)
            System.Reflection.MethodInfo? tryGet = null;
            foreach (var m in app.GetType().GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (m.Name != "TryGetResource") continue;
                var ps = m.GetParameters();
                if (ps.Length == 3 && ps[2].IsOut)
                {
                    tryGet = m;
                    break;
                }
            }

            if (tryGet == null)
            {
                Logger.Log("Theme", "ReadLiveRootColors: TryGetResource not found on Application");
                return null;
            }


            var result = new Dictionary<string, string>();
            string[] colorKeys = { "BrandPrimary", "BrandSecondary", "BrandTertiary",
                "TextPrimary", "TextSecondary", "TextTertiary", "TextWhite",
                "BackgroundPrimary", "BackgroundSecondary", "BackgroundTertiary", "Input",
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

            System.Reflection.MethodInfo? tryGet = null;
            foreach (var m in app.GetType().GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (m.Name != "TryGetResource") continue;
                var ps = m.GetParameters();
                if (ps.Length == 3 && ps[2].IsOut) { tryGet = m; break; }
            }
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
        var themeName = name.ToLower().Trim();
        if (!PresetThemes.TryGetValue(themeName, out var preset))
        {
            Logger.Log("Theme", "Unknown theme: " + name);
            return false;
        }

        return ApplyThemeInternal(themeName, preset.RootKeys, preset.SimpleThemeKeys);
    }

    /// <summary>Apply a custom theme from user-chosen accent + background.</summary>
    public bool ApplyCustomTheme(string accentHex, string bgHex)
    {
        if (!ColorUtils.IsValidHex(accentHex) || !ColorUtils.IsValidHex(bgHex))
        {
            Logger.Log("Theme", "Invalid custom colors: accent=" + accentHex + " bg=" + bgHex);
            return false;
        }

        var rootKeys = GenerateV2Palette(accentHex, bgHex);
        var simpleKeys = DeriveSimpleThemeKeys(accentHex, bgHex);
        _customPalette = MergePalettes(rootKeys, simpleKeys);
        _customAccent = accentHex;
        _customBg = bgHex;
        return ApplyThemeInternal("custom", rootKeys, simpleKeys);
    }

    /// <summary>
    /// Lightweight custom theme update for live preview during color picker drag.
    /// Only updates resource dictionaries — no revert, no tree walk.
    /// Throttled to ~60fps.
    /// </summary>
    public void UpdateCustomThemeLive(string accentHex, string bgHex)
    {
        if (!ColorUtils.IsValidHex(accentHex) || !ColorUtils.IsValidHex(bgHex))
            return;

        // Bootstrap with full apply if not yet active
        if (_activeThemeName != "custom" || _activeVariantDict == null)
        {
            Logger.Log("Theme", "Live preview: bootstrapping full custom apply");
            ApplyCustomTheme(accentHex, bgHex);
            _lastLiveUpdateTick = Environment.TickCount64;
            return;
        }

        // Throttle: ~60fps
        long now = Environment.TickCount64;
        if (now - _lastLiveUpdateTick < 16) return;
        _lastLiveUpdateTick = now;

        _customAccent = accentHex;
        _customBg = bgHex;

        var rootKeys = GenerateV2Palette(accentHex, bgHex);
        var simpleKeys = DeriveSimpleThemeKeys(accentHex, bgHex);
        _customPalette = MergePalettes(rootKeys, simpleKeys);

        // Update ThemeDictionaries
        OverrideThemeDictKeys(rootKeys);

        // Update Styles[0].Resources
        OverrideSimpleThemeKeys(simpleKeys);

        // Update ContentPages statics
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
        // Cancel walk timer
        _walkTimer?.Dispose();
        _walkTimer = null;

        // Phase 1: Remove our direct entries from ThemeDictionaries
        RemoveThemeDictOverrides();

        // Phase 2: Restore Styles[0].Resources
        var styleRes = _r.GetStyleResources(0);
        if (styleRes != null)
        {
            int restored = 0;
            foreach (var (key, original) in _savedStyleOriginals)
            {
                try
                {
                    _r.AddResource(styleRes, key, original);
                    restored++;
                }
                catch (Exception ex)
                {
                    Logger.Log("Theme", "  Restore failed for " + key + ": " + ex.Message);
                }
            }
            if (restored > 0)
                Logger.Log("Theme", "Restored " + restored + " originals in Styles[0]");

            int removed = 0;
            foreach (var key in _addedStyleKeys)
            {
                try { if (_r.RemoveResource(styleRes, key)) removed++; }
                catch { }
            }
            if (removed > 0)
                Logger.Log("Theme", "Removed " + removed + " added keys from Styles[0]");
        }

        _savedStyleOriginals.Clear();
        _addedStyleKeys.Clear();

        // Phase 3: Restore default DWM title bar
        UpdateTitleBarColor(DefaultDarkBg);

        _activeThemeName = null;
        _customPalette = null;
        Logger.Log("Theme", "Theme reverted — DynamicResource bindings auto-propagate");
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
        Dictionary<string, string> simpleThemeKeys)
    {
        Logger.Log("Theme", "Applying theme: " + themeName +
            " (" + rootKeys.Count + " root keys, " + simpleThemeKeys.Count + " simple keys)");

        // Revert any existing theme first
        RevertTheme();

        // Subscribe to variant changes (once)
        SubscribeToVariantChanges();

        // Phase 1: Override ThemeDictionaries[activeVariant] with Root's 32 keys
        EnsureVariantDictResolved();
        OverrideThemeDictKeys(rootKeys);

        // Phase 1b: Swap SVG asset paths if theme brightness doesn't match variant's SVG set
        // E.g., Uprooted dark theme on Light variant → swap Light SVGs to Dark SVGs
        SwapSvgPathsIfNeeded(rootKeys);

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
                    Logger.Log("Theme", "  Style override failed for " + key + ": " + ex.Message);
                }
            }
            Logger.Log("Theme", "Styles[0]: " + styleOverrides + " overrides applied");
        }

        // Phase 3: Update ContentPages statics
        var merged = MergePalettes(rootKeys, simpleThemeKeys);
        ContentPages.UpdateLiveColors(
            rootKeys.GetValueOrDefault("BrandPrimary", GetAccentColor()),
            rootKeys.GetValueOrDefault("BackgroundPrimary", DefaultDarkBg),
            merged);

        _activeThemeName = themeName;

        // Phase 4: Schedule single delayed walk (500ms one-shot)
        ScheduleDelayedWalk(500);

        // Phase 5: Update DWM title bar color
        if (rootKeys.TryGetValue("BackgroundPrimary", out var titleBarBg))
            UpdateTitleBarColor(titleBarBg);

        // Phase 6: Apply custom ping color if set
        if (!string.IsNullOrEmpty(_customPingColor))
            ApplyPingColorToThemeDicts();

        Logger.Log("Theme", "Theme applied: " + themeName +
            " (" + _overriddenThemeDictKeys.Count + " ThemeDict + " + styleOverrides + " Style)");
        return true;
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
    private void SwapSvgPathsIfNeeded(Dictionary<string, string> rootKeys)
    {
        if (_activeVariantDict == null) return;

        // Determine what SVG set Root is currently providing vs what our theme needs
        var currentFolder = GetCurrentVariantSvgFolder();
        bool needsDark = ThemeNeedsDarkSvgs(rootKeys);
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
    private static Dictionary<string, string> GenerateV2Palette(string accentHex, string bgHex)
    {
        var (aL, aC, aH) = ColorUtils.HexToOklch(accentHex);
        var (bL, bC, bH) = ColorUtils.HexToOklch(bgHex);

        // Clamp bg lightness to dark range
        bL = Math.Clamp(bL, 0.05, 0.25);

        var palette = new Dictionary<string, string>();

        // --- Brand colors ---
        palette["BrandPrimary"]   = accentHex;
        palette["BrandSecondary"] = ColorUtils.OklchToHex(
            Math.Clamp(aL + 0.15, 0, 1), aC * 0.7, (aH + 140) % 360);
        palette["BrandTertiary"]  = ColorUtils.OklchToHex(
            Math.Clamp(aL + 0.08, 0, 1), aC * 0.5, (aH + 210) % 360);

        // --- Background hierarchy (perceptually even L steps) ---
        palette["BackgroundPrimary"]   = ColorUtils.OklchToHex(bL, bC, bH);
        palette["BackgroundSecondary"] = ColorUtils.OklchToHex(bL + 0.02, bC, bH);
        palette["BackgroundTertiary"]  = ColorUtils.OklchToHex(Math.Max(0.03, bL - 0.015), bC, bH);
        palette["Input"]               = ColorUtils.OklchToHex(Math.Max(0.03, bL - 0.01), bC * 0.8, bH);

        // --- Text colors ---
        palette["TextPrimary"]   = ColorUtils.OklchToHex(0.93, 0.005, aH);
        palette["TextSecondary"] = ColorUtils.WithAlpha(palette["TextPrimary"], 0xA3);
        palette["TextTertiary"]  = ColorUtils.WithAlpha(palette["TextPrimary"], 0x66);
        palette["TextWhite"]     = "#F2F2F2";

        // --- UI elements ---
        palette["Border"] = ColorUtils.OklchToHex(
            bL + 0.08, bC * 0.4, ((aH + bH) / 2) % 360);
        palette["HighlightLight"]  = "#0AFFFFFF";
        palette["HighlightNormal"] = "#19FFFFFF";
        palette["HighlightStrong"] = "#30FFFFFF";

        // --- Semantic (kept from Root) ---
        palette["Info"]    = "#5BC0DE";
        palette["Warning"] = "#F0AD4E";
        palette["Error"]   = "#F03F36";

        // --- Derived ---
        palette["Muted"] = ColorUtils.OklchToHex(bL + 0.18, 0.015, bH);
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

        // --- Drop shadow (constant) ---
        palette["DropShadow"] = "#80000000";

        return palette;
    }

    /// <summary>
    /// Derive SimpleTheme keys for Styles[0].Resources (sidebar, standard Avalonia controls).
    /// </summary>
    private static Dictionary<string, string> DeriveSimpleThemeKeys(string accentHex, string bgHex)
    {
        var (bL, bC, bH) = ColorUtils.HexToOklch(bgHex);
        var bgSecondary = ColorUtils.OklchToHex(
            Math.Clamp(bL + 0.02, 0.05, 0.25), bC, bH);

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
        return total;
    }

    /// <summary>
    /// Build a map from Root's original ARGB colors to our themed colors.
    /// Only needed for hardcoded code-behind values that DynamicResource doesn't reach.
    /// </summary>
    private static Dictionary<string, string> BuildTreeColorMap(Dictionary<string, string> palette)
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // ContentPages card colors (hardcoded by our own UI code)
        // Map the defaults to our themed values
        if (palette.TryGetValue("BackgroundSecondary", out var bgSec))
            map["#FF0F1923"] = NormalizeArgb(bgSec);
        if (palette.TryGetValue("TextPrimary", out var textPri))
        {
            var (tr, tg, tb) = ColorUtils.ParseHex(textPri);
            map["#FFF2F2F2"] = $"#FF{tr:X2}{tg:X2}{tb:X2}";
            map["#A3F2F2F2"] = $"#A3{tr:X2}{tg:X2}{tb:X2}";
            map["#66F2F2F2"] = $"#66{tr:X2}{tg:X2}{tb:X2}";
        }

        return map;
    }

    private int WalkAndRecolor(object visual, Dictionary<string, string> colorMap, int depth)
    {
        if (depth > 50) return 0;

        var tag = _r.GetTag(visual);
        if (tag == "uprooted-no-recolor") return 0;

        int count = 0;
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

                    if (colorMap.TryGetValue(colorStr, out var replacement))
                    {
                        var newBrush = _r.CreateBrush(replacement);
                        if (newBrush != null)
                        {
                            prop.SetValue(visual, newBrush);
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
