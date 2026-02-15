using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

namespace Uprooted;

/// <summary>
/// Manages runtime theme application by directly overriding resources in
/// Application.Styles[0].Resources (Root's SimpleTheme) and also injecting
/// a ResourceDictionary into Application.Resources.MergedDictionaries for
/// standard FluentTheme keys.
///
/// Root's theme colors (ThemeAccentColor, ThemeAccentBrush, etc.) live in
/// Styles[0].Resources and are NOT overridden by MergedDictionaries.
/// We must directly write into Styles[0].Resources to change them.
///
/// Revert: restore saved original values in Styles[0].Resources and remove
/// our MergedDictionary.
/// </summary>
internal class ThemeEngine
{
    private readonly AvaloniaReflection _r;
    private object? _injectedDict;      // Our ResourceDictionary in MergedDictionaries
    private string? _activeThemeName;

    // Saved original values from Styles[0].Resources for revert
    private readonly Dictionary<string, object?> _savedOriginals = new();
    // Keys that were ADDED to Styles[0] (had no original) - must be removed on revert
    private readonly HashSet<string> _addedKeys = new();

    public string? ActiveThemeName => _activeThemeName;

    public ThemeEngine(AvaloniaReflection r)
    {
        _r = r;
    }

    // ===== DWM title bar color (Windows 11) =====

    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref uint value, int size);

    private const int DWMWA_CAPTION_COLOR = 35;
    private const string DefaultDarkBg = "#0D1521"; // Root's default dark background

    /// <summary>
    /// Set the Windows title bar color to match the active theme's background.
    /// Uses DwmSetWindowAttribute(DWMWA_CAPTION_COLOR) on Windows 11.
    /// </summary>
    private void UpdateTitleBarColor(string hexColor)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        try
        {
            var hwnd = GetMainWindowHandle();
            if (hwnd == IntPtr.Zero) return;

            // Parse #RRGGBB or #AARRGGBB to COLORREF (0x00BBGGRR)
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
            // Avalonia 11+: TopLevel.TryGetPlatformHandle()
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

    /// <summary>
    /// Apply a named preset theme (crimson, loki).
    /// </summary>
    public bool ApplyTheme(string name)
    {
        var themeName = name.ToLower().Trim();
        if (!Themes.TryGetValue(themeName, out var palette))
        {
            Logger.Log("Theme", "Unknown theme: " + name);
            return false;
        }

        TreeColorMaps.TryGetValue(themeName, out var treeMap);
        return ApplyThemeInternal(themeName, palette, treeMap);
    }

    /// <summary>
    /// Apply a custom theme generated from user-chosen accent + background colors.
    /// Generates the full palette and tree color map from ColorUtils.
    /// </summary>
    public bool ApplyCustomTheme(string accentHex, string bgHex)
    {
        if (!ColorUtils.IsValidHex(accentHex) || !ColorUtils.IsValidHex(bgHex))
        {
            Logger.Log("Theme", "Invalid custom colors: accent=" + accentHex + " bg=" + bgHex);
            return false;
        }

        var palette = GenerateCustomTheme(accentHex, bgHex);
        var treeMap = GenerateCustomTreeColorMap(accentHex, bgHex);
        _customPalette = palette;
        _customAccent = accentHex;
        _customBg = bgHex;
        return ApplyThemeInternal("custom", palette, treeMap);
    }

    // Stored custom palette for GetAccentColor/GetBgPrimary when theme is "custom"
    private Dictionary<string, string>? _customPalette;
    private string? _customAccent;
    private string? _customBg;

    /// <summary>
    /// Core theme application. Overrides resources in Styles[0].Resources,
    /// adds a MergedDictionary for FluentTheme keys, sets up visual tree walks.
    /// </summary>
    private bool ApplyThemeInternal(string themeName,
        Dictionary<string, string> palette,
        Dictionary<string, string>? treeColorMap)
    {
        Logger.Log("Theme", "Applying theme: " + themeName + " (" + palette.Count + " resource overrides)");

        // Save previous theme's color map BEFORE reverting, so we can build
        // a combined map that catches controls still showing old theme colors.
        var previousColorMap = _activeColorMap;
        var previousThemeName = _activeThemeName;

        // Revert any existing theme first
        RevertTheme();

        // === Phase 1: Override Styles[0].Resources (Root's custom theme keys) ===
        var styleRes = _r.GetStyleResources(0);
        int styleOverrides = 0;
        if (styleRes != null)
        {
            Logger.Log("Theme", "Injecting into Styles[0].Resources...");
            foreach (var (key, hex) in palette)
            {
                try
                {
                    // Save original value before overriding (or track as added)
                    if (!_savedOriginals.ContainsKey(key) && !_addedKeys.Contains(key))
                    {
                        try
                        {
                            var original = _r.GetResource(styleRes, key);
                            if (original != null)
                                _savedOriginals[key] = original;
                            else
                                _addedKeys.Add(key); // Key didn't exist - remove on revert
                        }
                        catch
                        {
                            _addedKeys.Add(key); // Assume didn't exist
                        }
                    }

                    // Detect Brush vs Color: keys containing "Brush" or ending in "Fill"
                    // must be SolidColorBrush (not Color). E.g. ThemeAccentBrush2, ErrorBrush
                    bool isBrush = key.Contains("Brush") || key.EndsWith("Fill");

                    if (isBrush)
                    {
                        var brush = _r.CreateBrush(hex);
                        if (brush != null)
                        {
                            _r.AddResource(styleRes, key, brush);
                            styleOverrides++;
                        }
                    }
                    else
                    {
                        var color = _r.ParseColor(hex);
                        if (color != null)
                        {
                            _r.AddResource(styleRes, key, color);
                            styleOverrides++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Theme", "  Style override failed for " + key + ": " + ex.Message);
                }
            }
            Logger.Log("Theme", "Styles[0].Resources: " + styleOverrides + " overrides applied, " + _savedOriginals.Count + " originals saved");
        }
        else
        {
            Logger.Log("Theme", "WARNING: Could not get Styles[0].Resources");
        }

        // === Phase 2: Also add MergedDictionary for FluentTheme standard keys ===
        var resources = _r.GetAppResources();
        var mergedDicts = resources != null ? _r.GetMergedDictionaries(resources) : null;
        int mergedAdded = 0;

        if (mergedDicts != null)
        {
            var dict = _r.CreateResourceDictionary();
            if (dict != null)
            {
                foreach (var (key, hex) in palette)
                {
                    try
                    {
                        bool isBrush = key.Contains("Brush") || key.EndsWith("Fill");

                        if (isBrush)
                        {
                            var brush = _r.CreateBrush(hex);
                            if (brush != null)
                            {
                                _r.AddResource(dict, key, brush);
                                mergedAdded++;
                            }
                        }
                        else
                        {
                            var color = _r.ParseColor(hex);
                            if (color != null)
                            {
                                _r.AddResource(dict, key, color);
                                mergedAdded++;

                                // Auto-generate Brush variant for Color keys
                                var brush = _r.CreateBrush(hex);
                                if (brush != null)
                                {
                                    _r.AddResource(dict, key + "Brush", brush);
                                    mergedAdded++;
                                }
                            }
                        }
                    }
                    catch { }
                }

                mergedDicts.Add(dict);
                _injectedDict = dict;
            }
        }

        _activeThemeName = themeName;
        Logger.Log("Theme", "Theme applied: " + styleOverrides + " style overrides + " + mergedAdded + " merged dict entries");

        // === Phase 3: Set up visual tree color maps ===
        if (treeColorMap != null)
        {
            var combinedMap = new Dictionary<string, string>(treeColorMap, StringComparer.OrdinalIgnoreCase);
            int crossMapped = 0;

            if (previousColorMap != null && previousThemeName != null)
            {
                foreach (var (origColor, prevReplacement) in previousColorMap)
                {
                    if (treeColorMap.TryGetValue(origColor, out var newReplacement))
                    {
                        if (!combinedMap.ContainsKey(prevReplacement) &&
                            !string.Equals(prevReplacement, newReplacement, StringComparison.OrdinalIgnoreCase))
                        {
                            combinedMap[prevReplacement] = newReplacement;
                            crossMapped++;
                        }
                    }
                }
                if (crossMapped > 0)
                    Logger.Log("Theme", "Cross-mapped " + crossMapped + " colors from " + previousThemeName + " -> " + themeName);
            }

            _activeColorMap = combinedMap;
            _reverseColorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var (orig, repl) in combinedMap)
                _reverseColorMap[repl] = orig;
            Logger.Log("Theme", "Color map loaded: " + combinedMap.Count + " mappings (" + treeColorMap.Count + " base + " + crossMapped + " cross-mapped)");
        }

        // === Phase 4: Immediate full tree walk + schedule continuous walks ===
        try
        {
            int initial = WalkAllWindows();
            Logger.Log("Theme", "Immediate walk: " + initial + " recolored");
        }
        catch { }
        ScheduleVisualTreeWalks();
        InstallLayoutInterceptor();

        // === Phase 5: Update DWM title bar color ===
        if (palette.TryGetValue("SolidBackgroundFillColorBase", out var titleBarBg))
            UpdateTitleBarColor(titleBarBg);

        return true;
    }

    private System.Threading.Timer? _walkTimer;
    private int _walkCount;
    private bool _layoutInterceptorInstalled;
    private long _lastLayoutWalkTick;  // Debounce for layout interceptor

    /// <summary>
    /// Schedule continuous visual tree walks at 500ms intervals. Root rebuilds its
    /// visual tree on every navigation, creating new controls with original colors.
    /// We keep walking to catch them. Each walk is ~2ms for 500+ nodes (just
    /// reflection property reads + color comparison), so 500ms interval is fine.
    /// </summary>
    private void ScheduleVisualTreeWalks()
    {
        _walkCount = 0;

        // Cancel any existing timer
        _walkTimer?.Dispose();

        // Walk every 500ms continuously - fast enough that flashes are barely visible
        _walkTimer = new System.Threading.Timer(_ =>
        {
            _walkCount++;
            _r.RunOnUIThread(() =>
            {
                try
                {
                    int recolored = WalkAllWindows();
                    if (recolored > 0)
                        Logger.Log("Theme", "Walk #" + _walkCount + ": " + recolored + " recolored");
                }
                catch (Exception ex)
                {
                    Logger.Log("Theme", "Walk error: " + ex.Message);
                }
            });
        }, null, 200, 500); // First walk at 200ms, then every 500ms
    }

    /// <summary>
    /// Hook into MainWindow.LayoutUpdated to detect navigation instantly.
    /// When Root navigates (switches channels, communities, pages), the layout changes.
    /// We detect this and walk immediately - before the next render frame.
    /// </summary>
    private void InstallLayoutInterceptor()
    {
        if (_layoutInterceptorInstalled) return;

        var mainWindow = _r.GetMainWindow();
        if (mainWindow == null) return;

        try
        {
            _r.SubscribeEvent(mainWindow, "LayoutUpdated", () =>
            {
                if (_activeColorMap == null) return;

                // Debounce: skip if we walked less than 80ms ago
                long now = Environment.TickCount64;
                if (now - _lastLayoutWalkTick < 80) return;
                _lastLayoutWalkTick = now;

                try
                {
                    // Walk all windows on every layout update - catches popups/overlays too
                    int recolored = WalkAllWindows();
                    if (recolored > 0)
                        Logger.Log("Theme", "Layout intercept: " + recolored + " recolored");
                }
                catch { }
            });

            _layoutInterceptorInstalled = true;
            Logger.Log("Theme", "Layout interceptor installed on MainWindow");
        }
        catch (Exception ex)
        {
            Logger.Log("Theme", "Layout interceptor install failed: " + ex.Message);
        }
    }

    /// <summary>
    /// After detecting a view change, do rapid follow-up walks at 200ms, 500ms, 1000ms
    /// to catch controls that load after the initial navigation.
    /// </summary>
    private void ScheduleRapidFollowUp()
    {
        System.Threading.ThreadPool.QueueUserWorkItem(_ =>
        {
            foreach (var delayMs in new[] { 200, 500, 1000 })
            {
                Thread.Sleep(delayMs);
                if (_activeColorMap == null) return;
                _r.RunOnUIThread(() =>
                {
                    try
                    {
                        int recolored = WalkAllWindows();
                        if (recolored > 0)
                            Logger.Log("Theme", "Rapid follow-up (+" + delayMs + "ms): " + recolored + " recolored");
                    }
                    catch { }
                });
            }
        });
    }

    /// <summary>
    /// Compute a lightweight fingerprint of the visual tree structure.
    /// Changes when navigation occurs (new views loaded).
    /// </summary>
    private int ComputeTreeFingerprint(object mainWindow)
    {
        int hash = 0;
        int count = 0;
        try
        {
            // Walk 3 levels deep and hash child counts + type names
            foreach (var c1 in _r.GetVisualChildren(mainWindow))
            {
                hash = hash * 31 + c1.GetType().Name.GetHashCode();
                foreach (var c2 in _r.GetVisualChildren(c1))
                {
                    count++;
                    foreach (var c3 in _r.GetVisualChildren(c2))
                    {
                        count++;
                        foreach (var c4 in _r.GetVisualChildren(c3))
                            count++;
                    }
                }
            }
        }
        catch { }
        return hash ^ (count * 997);
    }

    /// <summary>
    /// Walk all open TopLevel instances (MainWindow + PopupRoot windows).
    /// Uses WindowImpl.s_instances to find popup windows for profile cards, context menus, etc.
    /// </summary>
    private int WalkAllWindows()
    {
        int total = 0;
        var topLevels = _r.GetAllTopLevels();
        foreach (var topLevel in topLevels)
        {
            try { total += WalkAndRecolor(topLevel, 0); }
            catch { }
        }
        return total;
    }

    /// <summary>
    /// Run a visual tree walk immediately (e.g., after page navigation).
    /// Call from UI thread.
    /// </summary>
    public void WalkVisualTreeNow()
    {
        if (_activeThemeName == null || _activeColorMap == null) return;

        int recolored = WalkAllWindows();
        if (recolored > 0)
            Logger.Log("Theme", "Manual walk: " + recolored + " recolored");
    }

    /// <summary>
    /// Trigger a burst of walks: immediate + 200ms + 500ms + 1000ms.
    /// Call when navigation or content changes are detected.
    /// </summary>
    public void ScheduleWalkBurst()
    {
        if (_activeThemeName == null || _activeColorMap == null) return;

        // Immediate walk on UI thread
        _r.RunOnUIThread(() =>
        {
            try { WalkVisualTreeNow(); }
            catch { }
        });

        // Follow-up walks
        ScheduleRapidFollowUp();
    }

    // ===== Visual tree color mapping =====
    // Maps original Root colors -> themed replacements for each theme.
    // Covers accents, backgrounds, and borders for a comprehensive visual change.
    // All colors in lowercase #aarrggbb format (Avalonia Color.ToString() format).

    private static readonly Dictionary<string, Dictionary<string, string>> TreeColorMaps = new()
    {
        // IMPORTANT: Every replacement value must be UNIQUE within its theme map.
        // The reverse map (for revert) maps replacement -> original. If two originals
        // share a replacement, only one survives and the other can't be reverted.
        // Use +-1 RGB values to make visually identical but unique replacements.

        ["crimson"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Blue accents -> crimson
            ["#ff3b6af8"] = "#ffc42b1c",
            ["#ff4a78f9"] = "#ffd94a3d",
            ["#ff2e59d1"] = "#ffa32417",
            ["#ff2148af"] = "#ff821d12",
            ["#ff5b88ff"] = "#ffe06b60",
            ["#ff3366ff"] = "#ffda4b3e",    // unique (not #ffd94a3d)
            ["#663b6af8"] = "#66c42b1c",
            ["#333b6af8"] = "#33c42b1c",
            ["#193b6af8"] = "#19c42b1c",

            // ContentPages card background
            ["#ff0f1923"] = "#ff2a1818",    // CardBg -> warm card

            // Structural dark backgrounds -> clearly warm/red tinted
            ["#ff0d1521"] = "#ff241414",    // Main dark bg -> warm dark
            ["#ff07101b"] = "#ff1a0e0e",    // Darker bg -> warm darker
            ["#ff090e13"] = "#ff1e1010",    // Near-black bg
            ["#ff0a1a2e"] = "#ff241212",    // Another dark bg
            ["#ff101c2e"] = "#ff2a1616",    // Slightly lighter dark bg
            ["#ff121a26"] = "#ff2c1818",    // DM/chat panel bg
            ["#ff141e2b"] = "#ff2e1a1a",    // Panel bg
            ["#ff282828"] = "#ff302020",    // Neutral gray -> warm gray
            ["#ff4f5c6f"] = "#ff6f5050",    // Gray-blue metadata/header -> warm gray

            // Dark borders -> warm red-tinted
            ["#ff242c36"] = "#ff402828",    // Border -> warm border
            ["#ff1a2230"] = "#ff341e1e",    // Darker border
            ["#ff505050"] = "#ff504040",    // Gray border -> warm gray

            // Text: semi-transparent variants
            ["#a3f2f2f2"] = "#a3f0dada",
            ["#66f2f2f2"] = "#66f0dada",
            // NOTE: #19ffffff/#0affffff (hover overlays) intentionally excluded -
            // theming them makes hover effects persist permanently

            // Text: warm tint
            ["#ffdedede"] = "#fff0dada",
            ["#fff2f2f2"] = "#fff8eaea",
        },

        ["loki"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Blue accents -> Moss green (trestle palette)
            ["#ff3b6af8"] = "#ff2a5a40",    // Moss
            ["#ff4a78f9"] = "#ff3d7050",    // lighter moss
            ["#ff2e59d1"] = "#ff1e402f",    // Pine
            ["#ff2148af"] = "#ff112318",    // Shadow
            ["#ff5b88ff"] = "#ff508a62",    // bright moss
            ["#ff3366ff"] = "#ff3e7151",    // unique (not #ff3d7050)
            ["#663b6af8"] = "#662a5a40",
            ["#333b6af8"] = "#332a5a40",
            ["#193b6af8"] = "#192a5a40",

            // ContentPages card background
            ["#ff0f1923"] = "#ff171c17",    // CardBg -> trestle card

            // Structural dark backgrounds -> trestle dark palette
            ["#ff0d1521"] = "#ff0f1210",    // Main dark bg -> trestle bg dark
            ["#ff07101b"] = "#ff0a0d0a",    // Darker bg
            ["#ff090e13"] = "#ff0c0f0c",    // Near-black bg
            ["#ff0a1a2e"] = "#ff0d100d",    // Another dark bg
            ["#ff101c2e"] = "#ff151a15",    // Slightly lighter -> trestle gradient dark
            ["#ff121a26"] = "#ff131813",    // DM/chat panel bg
            ["#ff141e2b"] = "#ff1a1f1a",    // Panel bg -> trestle gradient light
            ["#ff282828"] = "#ff1e231e",    // Neutral gray -> earthy gray
            ["#ff4f5c6f"] = "#ff4a5a42",    // Gray-blue metadata -> trestle muted

            // Dark borders -> trestle border colors
            ["#ff242c36"] = "#ff3d4a35",    // Border -> trestle primary border
            ["#ff1a2230"] = "#ff2a3528",    // Darker border -> trestle header
            ["#ff505050"] = "#ff3e4b36",    // Gray border -> unique (not #ff3d4a35)

            // Text: semi-transparent variants (warm tint)
            ["#a3f2f2f2"] = "#a3f0ece0",
            ["#66f2f2f2"] = "#66f0ece0",
            // NOTE: #19ffffff/#0affffff (hover overlays) intentionally excluded -
            // theming them makes hover effects persist permanently

            // Text: warm earthy tint
            ["#ffdedede"] = "#ffe0d8c8",
            ["#fff2f2f2"] = "#fff0ece0",
        },

    };

    // Active color map for the current theme (original -> replacement)
    private Dictionary<string, string>? _activeColorMap;
    // Reverse map for revert (replacement -> original)
    private Dictionary<string, string>? _reverseColorMap;

    /// <summary>
    /// Walk the visual tree and replace colors using the active theme color map.
    /// Two-pass: collect all nodes/colors first, then apply changes.
    /// This avoids tree modification during traversal which can cause missing nodes.
    /// </summary>
    private int WalkAndRecolor(object visual, int depth, Dictionary<string, int>? colorCounts = null)
    {
        if (_activeColorMap == null) return 0;

        // Phase 1: Collect all pending changes without modifying the tree
        var pending = new List<(object control, System.Reflection.PropertyInfo prop, string replacement)>();
        CollectColorChanges(visual, 0, pending, colorCounts);

        // Phase 2: Apply all changes at Style priority (not LocalValue).
        // Style priority lets hover/pressed triggers override us temporarily,
        // then our value reasserts when the trigger deactivates.
        int count = 0;
        foreach (var (control, prop, replacement) in pending)
        {
            try
            {
                var newBrush = _r.CreateBrush(replacement);
                if (newBrush == null) continue;

                // Try Style-priority SetValue first (preserves hover behavior)
                var fieldName = AvaloniaReflection.PropertyToFieldName(prop.Name);
                if (!_r.SetValueStylePriority(control, fieldName, newBrush))
                {
                    // Fallback: direct CLR set (LocalValue priority)
                    prop.SetValue(control, newBrush);
                }
                count++;
            }
            catch { }
        }

        return count;
    }

    private void CollectColorChanges(object visual, int depth,
        List<(object, System.Reflection.PropertyInfo, string)> pending,
        Dictionary<string, int>? colorCounts)
    {
        if (depth > 50 || _activeColorMap == null) return;

        // Skip subtrees tagged by Uprooted (e.g. theme preview swatches)
        var tag = _r.GetTag(visual);
        if (tag == "uprooted-no-recolor") return;

        try
        {
            foreach (var propName in new[] { "Background", "Foreground", "BorderBrush", "Fill" })
            {
                var prop = visual.GetType().GetProperty(propName);
                if (prop == null) continue;

                try
                {
                    var brush = prop.GetValue(visual);
                    if (brush == null) continue;

                    var colorStr = GetBrushColorString(brush);
                    if (colorStr == null) continue;

                    if (colorCounts != null)
                    {
                        var key = propName + ":" + colorStr;
                        colorCounts.TryGetValue(key, out int existing);
                        colorCounts[key] = existing + 1;
                    }

                    if (_activeColorMap.TryGetValue(colorStr, out var replacement))
                    {
                        pending.Add((visual, prop, replacement));
                    }
                }
                catch { }
            }
        }
        catch { }

        foreach (var child in _r.GetVisualChildren(visual))
        {
            CollectColorChanges(child, depth + 1, pending, colorCounts);
        }
    }

    /// <summary>
    /// Extract the color string from any brush type (SolidColorBrush, ImmutableSolidColorBrush, etc.)
    /// </summary>
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

    /// <summary>
    /// Walk the tree and restore themed controls to their original colors.
    /// Hybrid approach: ClearValue first (lets DynamicResource reassert from
    /// now-restored resources), then check the result. If ClearValue left the
    /// property null/transparent or still showing the themed color, fall back
    /// to explicit SetValue with the original color.
    /// IMPORTANT: Resources must be restored BEFORE calling this method.
    /// </summary>
    private int WalkAndRestore(object visual, int depth)
    {
        if (depth > 50 || _reverseColorMap == null) return 0;
        if (_r.GetTag(visual) == "uprooted-no-recolor") return 0;
        int count = 0;

        try
        {
            foreach (var propName in new[] { "Background", "Foreground", "BorderBrush", "Fill" })
            {
                var prop = visual.GetType().GetProperty(propName);
                if (prop == null) continue;
                try
                {
                    var brush = prop.GetValue(visual);
                    if (brush == null) continue;
                    var colorStr = GetBrushColorString(brush);
                    if (colorStr == null) continue;

                    if (_reverseColorMap.TryGetValue(colorStr, out var original))
                    {
                        var fieldName = AvaloniaReflection.PropertyToFieldName(propName);

                        // Step 1: ClearValue removes our LocalValue override.
                        // If the control uses DynamicResource, it re-resolves to
                        // the correct original from the now-restored resources.
                        _r.ClearValueSilent(visual, fieldName);

                        // Step 2: Verify. Read the property after ClearValue.
                        // If it's null or still shows the themed color, the control
                        // didn't have a DynamicResource binding - set explicitly.
                        var newBrush = prop.GetValue(visual);
                        var newColor = newBrush != null ? GetBrushColorString(newBrush) : null;

                        if (newColor == null ||
                            string.Equals(newColor, colorStr, StringComparison.OrdinalIgnoreCase))
                        {
                            // ClearValue didn't fix it - set the original explicitly
                            var originalBrush = _r.CreateBrush(original);
                            if (originalBrush != null)
                                _r.SetValueStylePriority(visual, fieldName, originalBrush);
                        }

                        count++;
                    }
                }
                catch { }
            }
        }
        catch { }

        foreach (var child in _r.GetVisualChildren(visual))
        {
            count += WalkAndRestore(child, depth + 1);
        }
        return count;
    }

    public void RevertTheme()
    {
        // Cancel any pending walks
        _walkTimer?.Dispose();
        _walkTimer = null;

        // Disable layout interceptor IMMEDIATELY to prevent re-applying colors during revert.
        // WalkAndRestore uses _reverseColorMap (not _activeColorMap), so this is safe.
        _activeColorMap = null;

        // === Phase 1: Restore all resources FIRST ===
        // This must happen BEFORE the visual tree walk so that ClearValue
        // causes DynamicResource bindings to resolve to the correct originals.

        var styleRes = _r.GetStyleResources(0);
        if (styleRes != null)
        {
            if (_savedOriginals.Count > 0)
            {
                int restored = 0;
                foreach (var (key, original) in _savedOriginals)
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
                Logger.Log("Theme", "Restored " + restored + " original resources in Styles[0]");
            }

            // Remove keys that were ADDED by us (had no original value)
            if (_addedKeys.Count > 0)
            {
                int removed = 0;
                foreach (var key in _addedKeys)
                {
                    try
                    {
                        if (_r.RemoveResource(styleRes, key))
                            removed++;
                    }
                    catch { }
                }
                Logger.Log("Theme", "Removed " + removed + "/" + _addedKeys.Count + " added keys from Styles[0]");
            }
        }

        // Remove MergedDictionary
        if (_injectedDict != null)
        {
            try
            {
                var resources = _r.GetAppResources();
                var mergedDicts = _r.GetMergedDictionaries(resources);
                if (mergedDicts != null)
                {
                    mergedDicts.Remove(_injectedDict);
                    Logger.Log("Theme", "Removed MergedDictionary, count now " + mergedDicts.Count);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Theme", "RevertTheme MergedDict error: " + ex.Message);
            }
            _injectedDict = null;
        }

        // === Phase 2: Targeted purge — ClearValue on all KNOWN theme colors ===
        // Build a set of every color we know about (both originals and replacements).
        // Only ClearValue on controls whose current color is in this set.
        // This avoids nuking Root's structural backgrounds that weren't theme-related.
        var purgeColors = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        if (_activeColorMap != null)
        {
            foreach (var (orig, repl) in _activeColorMap)
            {
                purgeColors.Add(orig);
                purgeColors.Add(repl);
            }
        }
        if (_reverseColorMap != null)
        {
            foreach (var (repl, orig) in _reverseColorMap)
            {
                purgeColors.Add(repl);
                purgeColors.Add(orig);
            }
        }

        try
        {
            int purged = 0;
            foreach (var topLevel in _r.GetAllTopLevels())
            {
                try { purged += PurgeKnownColors(topLevel, 0, purgeColors); }
                catch { }
            }
            Logger.Log("Theme", "Targeted purge: " + purged + " color properties cleared (" + purgeColors.Count + " known colors)");
        }
        catch (Exception ex)
        {
            Logger.Log("Theme", "Targeted purge error: " + ex.Message);
        }

        _savedOriginals.Clear();
        _addedKeys.Clear();

        // Restore default DWM title bar color
        UpdateTitleBarColor(DefaultDarkBg);

        _activeThemeName = null;
        _reverseColorMap = null;
    }

    /// <summary>
    /// Schedule delayed revert walks to catch controls loaded after the initial revert.
    /// Uses the saved reverse map to find and restore remaining themed controls.
    /// </summary>
    private void ScheduleRevertFollowUps(Dictionary<string, string> reverseMap)
    {
        System.Threading.ThreadPool.QueueUserWorkItem(_ =>
        {
            foreach (var delayMs in new[] { 500, 1500, 3000 })
            {
                Thread.Sleep(delayMs);
                // Stop if a new theme was applied while we were waiting
                if (_activeThemeName != null) return;

                _r.RunOnUIThread(() =>
                {
                    try
                    {
                        int restored = 0;
                        foreach (var topLevel in _r.GetAllTopLevels())
                        {
                            try { restored += WalkAndRestoreWithMap(topLevel, 0, reverseMap); }
                            catch { }
                        }
                        if (restored > 0)
                            Logger.Log("Theme", "Revert follow-up (+" + delayMs + "ms): " + restored + " restored");
                    }
                    catch { }
                });
            }
        });
    }

    /// <summary>
    /// Targeted purge: ClearValue on color properties only if the current color
    /// is in the known set (theme colors we applied or originals we mapped from).
    /// This avoids clearing Root's structural backgrounds.
    /// </summary>
    private int PurgeKnownColors(object visual, int depth, HashSet<string> knownColors)
    {
        if (depth > 50) return 0;
        if (_r.GetTag(visual) == "uprooted-no-recolor") return 0;
        int count = 0;

        try
        {
            foreach (var propName in new[] { "Background", "Foreground", "BorderBrush", "Fill" })
            {
                try
                {
                    var prop = visual.GetType().GetProperty(propName);
                    if (prop == null) continue;
                    var brush = prop.GetValue(visual);
                    if (brush == null) continue;
                    var colorStr = GetBrushColorString(brush);
                    if (colorStr == null) continue;

                    if (knownColors.Contains(colorStr))
                    {
                        var fieldName = AvaloniaReflection.PropertyToFieldName(propName);
                        if (_r.ClearValueSilent(visual, fieldName))
                        {
                            // Verify — if ClearValue left it null, restore a fallback
                            var newBrush = prop.GetValue(visual);
                            if (newBrush == null)
                            {
                                string restoreColor = colorStr;
                                if (_reverseColorMap != null && _reverseColorMap.TryGetValue(colorStr, out var orig))
                                    restoreColor = orig;

                                var restoreBrush = _r.CreateBrush(restoreColor);
                                if (restoreBrush != null)
                                    _r.SetValueStylePriority(visual, fieldName, restoreBrush);
                            }
                            count++;
                        }
                    }
                }
                catch { }
            }
        }
        catch { }

        foreach (var child in _r.GetVisualChildren(visual))
        {
            count += PurgeKnownColors(child, depth + 1, knownColors);
        }

        return count;
    }

    /// <summary>
    /// Walk and restore using a specific reverse map (for follow-up revert walks).
    /// Uses ClearValue + SetValue fallback, same as WalkAndRestore.
    /// </summary>
    private int WalkAndRestoreWithMap(object visual, int depth, Dictionary<string, string> reverseMap)
    {
        if (depth > 50) return 0;
        if (_r.GetTag(visual) == "uprooted-no-recolor") return 0;
        int count = 0;

        try
        {
            foreach (var propName in new[] { "Background", "Foreground", "BorderBrush", "Fill" })
            {
                var prop = visual.GetType().GetProperty(propName);
                if (prop == null) continue;
                try
                {
                    var brush = prop.GetValue(visual);
                    if (brush == null) continue;
                    var colorStr = GetBrushColorString(brush);
                    if (colorStr == null) continue;

                    if (reverseMap.TryGetValue(colorStr, out var original))
                    {
                        var fieldName = AvaloniaReflection.PropertyToFieldName(propName);
                        _r.ClearValueSilent(visual, fieldName);

                        var newBrush = prop.GetValue(visual);
                        var newColor = newBrush != null ? GetBrushColorString(newBrush) : null;

                        if (newColor == null ||
                            string.Equals(newColor, colorStr, StringComparison.OrdinalIgnoreCase))
                        {
                            var originalBrush = _r.CreateBrush(original);
                            if (originalBrush != null)
                                _r.SetValueStylePriority(visual, fieldName, originalBrush);
                        }
                        count++;
                    }
                }
                catch { }
            }
        }
        catch { }

        foreach (var child in _r.GetVisualChildren(visual))
        {
            count += WalkAndRestoreWithMap(child, depth + 1, reverseMap);
        }
        return count;
    }

    /// <summary>
    /// Diagnostic: dump all colors found in the visual tree by frequency.
    /// Helps identify which colors need to be targeted for theme overrides.
    /// </summary>
    public void DumpVisualTreeColors()
    {
        var mainWindow = _r.GetMainWindow();
        if (mainWindow == null)
        {
            Logger.Log("Theme", "DumpVisualTreeColors: no MainWindow");
            return;
        }

        var colorCounts = new Dictionary<string, int>();
        var typeCounts = new Dictionary<string, int>();
        var nodeCounter = new int[] { 0 };
        // Walk all TopLevel instances (MainWindow + PopupRoot + dialogs)
        var topLevels = _r.GetAllTopLevels();
        Logger.Log("Theme", "DumpVisualTreeColors: scanning " + topLevels.Count + " TopLevel instances");
        foreach (var tl in topLevels)
        {
            Logger.Log("Theme", "  TopLevel: " + tl.GetType().FullName);
            ScanColors(tl, colorCounts, 0, typeCounts, nodeCounter);
        }
        int totalNodes = nodeCounter[0];

        // Sort by frequency (descending) and log top entries
        var sorted = colorCounts.OrderByDescending(kv => kv.Value).ToList();
        Logger.Log("Theme", "=== VISUAL TREE COLOR DUMP (" + sorted.Count + " unique combos, " + totalNodes + " total nodes) ===");
        int logged = 0;
        foreach (var (key, freq) in sorted)
        {
            if (logged >= 80) break;
            Logger.Log("Theme", "  [" + freq + "x] " + key);
            logged++;
        }

        // Also dump top control types
        var sortedTypes = typeCounts.OrderByDescending(kv => kv.Value).Take(20).ToList();
        Logger.Log("Theme", "--- TOP CONTROL TYPES ---");
        foreach (var (typeName, count) in sortedTypes)
            Logger.Log("Theme", "  [" + count + "x] " + typeName);

        // Search for browser/web controls
        Logger.Log("Theme", "--- BROWSER/WEB CONTROLS ---");
        var webControls = new List<string>();
        FindWebControls(mainWindow, webControls, 0);
        if (webControls.Count == 0)
            Logger.Log("Theme", "  (none found)");
        else
            foreach (var wc in webControls)
                Logger.Log("Theme", "  " + wc);

        Logger.Log("Theme", "=== END COLOR DUMP ===");
    }

    private void FindWebControls(object visual, List<string> results, int depth)
    {
        if (depth > 50) return;
        var fullName = visual.GetType().FullName ?? "";
        var name = visual.GetType().Name;
        // Look for DotNetBrowser, WebView, Chromium, BrowserView, etc.
        if (name.Contains("Browser") || name.Contains("Web") || name.Contains("Chromium")
            || name.Contains("Cef") || fullName.Contains("DotNetBrowser"))
        {
            var size = "";
            try
            {
                var boundsProp = visual.GetType().GetProperty("Bounds");
                if (boundsProp != null)
                    size = " Bounds=" + boundsProp.GetValue(visual);
            }
            catch { }
            results.Add("depth=" + depth + " " + fullName + size);
        }
        foreach (var child in _r.GetVisualChildren(visual))
            FindWebControls(child, results, depth + 1);
    }

    /// <summary>
    /// Read-only scan of visual tree colors (no modifications).
    /// </summary>
    private void ScanColors(object visual, Dictionary<string, int> colorCounts, int depth,
        Dictionary<string, int>? typeCounts = null, int[]? nodeCounter = null)
    {
        if (depth > 50) return;
        if (nodeCounter != null) nodeCounter[0]++;

        // Track control type
        if (typeCounts != null)
        {
            var typeName = visual.GetType().Name;
            typeCounts.TryGetValue(typeName, out int tc);
            typeCounts[typeName] = tc + 1;
        }

        try
        {
            foreach (var propName in new[] { "Background", "Foreground", "BorderBrush", "Fill" })
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
            ScanColors(child, colorCounts, depth + 1, typeCounts, nodeCounter);
    }

    /// <summary>
    /// Diagnostic: dump the visual tree structure showing types and nesting.
    /// Truncated to significant branches.
    /// </summary>
    public void DumpVisualTreeStructure()
    {
        var mainWindow = _r.GetMainWindow();
        if (mainWindow == null) return;

        Logger.Log("Theme", "=== VISUAL TREE STRUCTURE ===");
        DumpNode(mainWindow, 0, 6); // 6 levels deep with full detail
        Logger.Log("Theme", "=== END STRUCTURE ===");
    }

    private void DumpNode(object visual, int depth, int maxDetailDepth)
    {
        if (depth > 20) return;
        var typeName = visual.GetType().Name;
        var fullTypeName = visual.GetType().FullName ?? typeName;
        var children = _r.GetVisualChildren(visual);
        var childCount = 0;
        foreach (var _ in children) childCount++;

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

        // Show full type name for DotNetBrowser/unknown types, short for Avalonia
        var displayName = fullTypeName.Contains("DotNet") || fullTypeName.Contains("Browser")
            || fullTypeName.Contains("Cef") || fullTypeName.Contains("Chromium")
            ? fullTypeName : typeName;

        Logger.Log("Theme", indent + displayName + " [" + childCount + "]" + bgStr);

        // Show all children up to a reasonable limit per level
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

    /// <summary>
    /// Diagnostic: dump all existing resource keys from Application.Resources and Styles.
    /// </summary>
    public void DumpResourceKeys()
    {
        Logger.Log("Theme", "=== RESOURCE KEY DUMP ===");

        // Dump Styles[0].Resources (Root's custom theme keys)
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

        // Dump Application.Resources
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

            // Dump MergedDictionaries
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

    // ===== Theme palette accessors for ContentPages =====

    public string GetAccentColor()
    {
        if (_activeThemeName == "custom" && _customAccent != null)
            return _customAccent;
        if (_activeThemeName != null && Themes.TryGetValue(_activeThemeName, out var p))
        {
            if (p.TryGetValue("ThemeAccentColor", out var hex)) return hex;
            if (p.TryGetValue("SystemAccentColor", out hex)) return hex;
        }
        return "#3B6AF8"; // Root's default blue accent
    }

    public string GetBgPrimary()
    {
        if (_activeThemeName == "custom" && _customBg != null)
            return _customBg;
        if (_activeThemeName != null && Themes.TryGetValue(_activeThemeName, out var p))
        {
            if (p.TryGetValue("SolidBackgroundFillColorBase", out var hex)) return hex;
        }
        return "#0D1521"; // Root's default dark bg
    }

    // ===== Custom Theme Generation =====

    /// <summary>
    /// Generate a full theme palette from accent + background colors using ColorUtils.
    /// Produces the same ~50 keys as the preset themes.
    /// </summary>
    private static Dictionary<string, string> GenerateCustomTheme(string accent, string bg)
    {
        var textColor = ColorUtils.DeriveTextColor(bg);
        var accentLight1 = ColorUtils.Lighten(accent, 15);
        var accentLight2 = ColorUtils.Lighten(accent, 30);
        var accentLight3 = ColorUtils.Lighten(accent, 45);
        var accentDark1 = ColorUtils.Darken(accent, 15);
        var accentDark2 = ColorUtils.Darken(accent, 30);
        var accentDark3 = ColorUtils.Darken(accent, 45);

        var bgSecondary = ColorUtils.Lighten(bg, 8);
        var bgTertiary = ColorUtils.Lighten(bg, 14);
        var bgQuarternary = ColorUtils.Lighten(bg, 20);

        var textFaded78 = ColorUtils.WithAlphaFraction(textColor, 0.78);
        var textFaded55 = ColorUtils.WithAlphaFraction(textColor, 0.55);
        var textFaded36 = ColorUtils.WithAlphaFraction(textColor, 0.36);

        return new Dictionary<string, string>
        {
            // Root custom theme keys
            ["ThemeAccentColor"]     = accent,
            ["ThemeAccentColor2"]    = accentLight1,
            ["ThemeAccentColor3"]    = accentDark1,
            ["ThemeAccentColor4"]    = accentDark2,
            ["ThemeAccentBrush"]     = accent,
            ["ThemeAccentBrush2"]    = accentLight1,
            ["ThemeAccentBrush3"]    = accentDark1,
            ["ThemeAccentBrush4"]    = accentDark2,
            ["ThemeForegroundLowColor"]  = textFaded55,
            ["ThemeForegroundLowBrush"]  = textFaded55,
            ["HighlightForegroundColor"] = accent,
            ["HighlightForegroundBrush"] = accent,
            ["ErrorColor"]               = "#FF4444",
            ["ErrorLowColor"]            = "#80FF4444",
            ["ErrorBrush"]               = "#FF4444",
            ["ErrorLowBrush"]            = "#80FF4444",
            ["DatePickerFlyoutPresenterHighlightFill"]  = accent,
            ["TimePickerFlyoutPresenterHighlightFill"]  = accent,

            // System accent colors
            ["SystemAccentColor"]       = accent,
            ["SystemAccentColorDark1"]  = accentDark1,
            ["SystemAccentColorDark2"]  = accentDark2,
            ["SystemAccentColorDark3"]  = accentDark3,
            ["SystemAccentColorLight1"] = accentLight1,
            ["SystemAccentColorLight2"] = accentLight2,
            ["SystemAccentColorLight3"] = accentLight3,

            // Text fill
            ["TextFillColorPrimary"]    = ColorUtils.WithAlphaFraction(textColor, 1.0),
            ["TextFillColorSecondary"]  = textFaded78,
            ["TextFillColorTertiary"]   = textFaded55,
            ["TextFillColorDisabled"]   = textFaded36,

            // Control fills
            ["ControlFillColorDefault"]   = "#15FFFFFF",
            ["ControlFillColorSecondary"] = "#10FFFFFF",
            ["ControlFillColorTertiary"]  = "#08FFFFFF",
            ["ControlFillColorDisabled"]  = "#06FFFFFF",

            // Solid backgrounds
            ["SolidBackgroundFillColorBase"]        = bg,
            ["SolidBackgroundFillColorSecondary"]   = bgSecondary,
            ["SolidBackgroundFillColorTertiary"]    = bgTertiary,
            ["SolidBackgroundFillColorQuarternary"] = bgQuarternary,

            // Card/layer
            ["CardBackgroundFillColorDefault"]       = ColorUtils.WithAlpha(accent, 0x20),
            ["CardBackgroundFillColorDefaultBrush"]  = ColorUtils.WithAlpha(accent, 0x20),
            ["LayerFillColorDefault"]                = "#08FFFFFF",
            ["LayerFillColorAlt"]                    = "#0AFFFFFF",

            // Accent fill brushes
            ["AccentFillColorDefaultBrush"]    = accent,
            ["AccentFillColorSecondaryBrush"]  = accentLight1,
            ["AccentFillColorTertiaryBrush"]   = accentDark1,
            ["AccentFillColorDisabledBrush"]   = ColorUtils.WithAlpha(accent, 0x5C),

            // Strokes
            ["ControlStrokeColorDefault"]   = ColorUtils.WithAlphaFraction(textColor, 0.24),
            ["ControlStrokeColorSecondary"] = ColorUtils.WithAlphaFraction(textColor, 0.15),
            ["CardStrokeColorDefault"]      = ColorUtils.WithAlphaFraction(textColor, 0.19),
            ["SurfaceStrokeColorDefault"]   = ColorUtils.WithAlpha(accent, 0x40),

            // Buttons
            ["ButtonBackground"]                   = ColorUtils.WithAlpha(accent, 0x15),
            ["ButtonBackgroundPointerOver"]         = ColorUtils.WithAlpha(accent, 0x25),
            ["ButtonBackgroundPressed"]             = ColorUtils.WithAlpha(accent, 0x10),
            ["ButtonBackgroundDisabled"]            = "#08FFFFFF",

            // ListBox
            ["ListBoxItemBackgroundPointerOver"]    = ColorUtils.WithAlpha(accent, 0x15),
            ["ListBoxItemBackgroundPressed"]        = ColorUtils.WithAlpha(accent, 0x20),
            ["ListBoxItemBackgroundSelected"]       = ColorUtils.WithAlpha(accent, 0x25),
            ["ListBoxItemBackgroundSelectedPointerOver"]  = ColorUtils.WithAlpha(accent, 0x30),
            ["ListBoxItemBackgroundSelectedPressed"]      = ColorUtils.WithAlpha(accent, 0x20),

            // ToggleSwitch
            ["ToggleSwitchFillOn"]               = accent,
            ["ToggleSwitchFillOnPointerOver"]    = accentLight1,
            ["ToggleSwitchFillOnPressed"]        = accentDark1,

            // ScrollBar
            ["ScrollBarThumbFill"]               = ColorUtils.WithAlpha(accent, 0x50),
            ["ScrollBarThumbFillPointerOver"]    = ColorUtils.WithAlpha(accent, 0x80),
            ["ScrollBarThumbFillPressed"]        = accent,

            // TextControl
            ["TextControlBackgroundFocused"]     = ColorUtils.WithAlpha(accent, 0x20),
            ["TextControlBorderBrushFocused"]    = accent,

            // Selection
            ["TextSelectionHighlightColor"]      = ColorUtils.WithAlpha(accent, 0x60),
        };
    }

    /// <summary>
    /// Generate a tree color map for custom themes. Maps Root's original ARGB colors
    /// to custom-derived equivalents. Each replacement value must be unique.
    /// </summary>
    private static Dictionary<string, string> GenerateCustomTreeColorMap(string accent, string bg)
    {
        // Root's original colors (ARGB format from Avalonia Color.ToString())
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Blue accent -> custom accent (with minor offsets for uniqueness)
        var (ar, ag, ab) = ColorUtils.ParseHex(accent);
        map["#ff3b6af8"] = $"#FF{ar:X2}{ag:X2}{ab:X2}";
        map["#ff4a78f9"] = "#FF" + ColorUtils.Lighten(accent, 15).TrimStart('#');
        map["#ff2e59d1"] = "#FF" + ColorUtils.Darken(accent, 15).TrimStart('#');
        map["#ff2148af"] = "#FF" + ColorUtils.Darken(accent, 30).TrimStart('#');
        map["#ff5b88ff"] = "#FF" + ColorUtils.Lighten(accent, 30).TrimStart('#');
        // Offset by 1 for uniqueness
        var accentL15 = ColorUtils.ParseHex(ColorUtils.Lighten(accent, 15));
        map["#ff3366ff"] = $"#FF{(byte)Math.Min(255, accentL15.R + 1):X2}{accentL15.G:X2}{accentL15.B:X2}";
        map["#663b6af8"] = $"#66{ar:X2}{ag:X2}{ab:X2}";
        map["#333b6af8"] = $"#33{ar:X2}{ag:X2}{ab:X2}";
        map["#193b6af8"] = $"#19{ar:X2}{ag:X2}{ab:X2}";

        // ContentPages card bg
        var cardBg = ColorUtils.Lighten(bg, 6);
        map["#ff0f1923"] = "#FF" + cardBg.TrimStart('#');

        // Structural backgrounds -> custom bg with incremental lightening for uniqueness
        var (br, bgr, bb) = ColorUtils.ParseHex(bg);
        map["#ff0d1521"] = $"#FF{br:X2}{bgr:X2}{bb:X2}";
        var bg2 = ColorUtils.Darken(bg, 8);
        map["#ff07101b"] = "#FF" + bg2.TrimStart('#');
        var bg3 = ColorUtils.Darken(bg, 5);
        map["#ff090e13"] = "#FF" + bg3.TrimStart('#');
        var bg4 = ColorUtils.Darken(bg, 3);
        map["#ff0a1a2e"] = "#FF" + bg4.TrimStart('#');
        map["#ff101c2e"] = "#FF" + ColorUtils.Lighten(bg, 5).TrimStart('#');
        map["#ff121a26"] = "#FF" + ColorUtils.Lighten(bg, 3).TrimStart('#');
        map["#ff141e2b"] = "#FF" + ColorUtils.Lighten(bg, 8).TrimStart('#');
        map["#ff282828"] = "#FF" + ColorUtils.Lighten(bg, 10).TrimStart('#');
        map["#ff4f5c6f"] = "#FF" + ColorUtils.Lighten(bg, 25).TrimStart('#');

        // Borders
        map["#ff242c36"] = "#FF" + ColorUtils.Lighten(bg, 18).TrimStart('#');
        map["#ff1a2230"] = "#FF" + ColorUtils.Lighten(bg, 12).TrimStart('#');
        map["#ff505050"] = "#FF" + ColorUtils.Lighten(bg, 20).TrimStart('#');

        // Text semi-transparent
        var textBase = ColorUtils.DeriveTextColor(bg);
        var (tr, tg, tb) = ColorUtils.ParseHex(textBase);
        map[$"#a3f2f2f2"] = $"#A3{tr:X2}{tg:X2}{tb:X2}";
        map[$"#66f2f2f2"] = $"#66{tr:X2}{tg:X2}{tb:X2}";

        // Text opaque
        map["#ffdedede"] = "#FF" + ColorUtils.Darken(textBase, 5).TrimStart('#');
        map["#fff2f2f2"] = $"#FF{tr:X2}{tg:X2}{tb:X2}";

        return map;
    }

    // ===== Theme Definitions =====
    // Keys ending in "Brush" or "Fill" are created as SolidColorBrush.
    // All other keys are created as Color values.
    // For Color keys, a "Brush" variant is auto-generated in the MergedDictionary.

    private static readonly Dictionary<string, Dictionary<string, string>> Themes = new()
    {
        ["crimson"] = new Dictionary<string, string>
        {
            // === Root's custom theme keys (Styles[0].Resources) - THESE ARE THE IMPORTANT ONES ===
            ["ThemeAccentColor"]     = "#C42B1C",
            ["ThemeAccentColor2"]    = "#D94A3D",
            ["ThemeAccentColor3"]    = "#A32417",
            ["ThemeAccentColor4"]    = "#821D12",
            ["ThemeAccentBrush"]     = "#C42B1C",
            ["ThemeAccentBrush2"]    = "#D94A3D",
            ["ThemeAccentBrush3"]    = "#A32417",
            ["ThemeAccentBrush4"]    = "#821D12",
            ["ThemeForegroundLowColor"]  = "#8CF0EAEA",
            ["ThemeForegroundLowBrush"]  = "#8CF0EAEA",
            ["HighlightForegroundColor"] = "#C42B1C",
            ["HighlightForegroundBrush"] = "#C42B1C",
            ["ErrorColor"]               = "#FF4444",
            ["ErrorLowColor"]            = "#80FF4444",
            ["ErrorBrush"]               = "#FF4444",
            ["ErrorLowBrush"]            = "#80FF4444",
            ["DatePickerFlyoutPresenterHighlightFill"]  = "#C42B1C",
            ["TimePickerFlyoutPresenterHighlightFill"]  = "#C42B1C",

            // === Standard FluentTheme keys (MergedDictionary) ===
            // System accent colors
            ["SystemAccentColor"]       = "#C42B1C",
            ["SystemAccentColorDark1"]  = "#A32417",
            ["SystemAccentColorDark2"]  = "#821D12",
            ["SystemAccentColorDark3"]  = "#61150E",
            ["SystemAccentColorLight1"] = "#D94A3D",
            ["SystemAccentColorLight2"] = "#E06B60",
            ["SystemAccentColorLight3"] = "#E88D84",

            // Text fill colors
            ["TextFillColorPrimary"]    = "#FFF0EAEA",
            ["TextFillColorSecondary"]  = "#C8F0EAEA",
            ["TextFillColorTertiary"]   = "#8CF0EAEA",
            ["TextFillColorDisabled"]   = "#5CF0EAEA",

            // Control fills
            ["ControlFillColorDefault"]   = "#15FFFFFF",
            ["ControlFillColorSecondary"] = "#10FFFFFF",
            ["ControlFillColorTertiary"]  = "#08FFFFFF",
            ["ControlFillColorDisabled"]  = "#06FFFFFF",

            // Solid backgrounds (more saturated for visible tint)
            ["SolidBackgroundFillColorBase"]        = "#241414",
            ["SolidBackgroundFillColorSecondary"]   = "#2C1818",
            ["SolidBackgroundFillColorTertiary"]    = "#341C1C",
            ["SolidBackgroundFillColorQuarternary"] = "#3C2020",

            // Card/layer
            ["CardBackgroundFillColorDefault"]       = "#20C42B1C",
            ["CardBackgroundFillColorDefaultBrush"]  = "#20C42B1C",
            ["LayerFillColorDefault"]                = "#08FFFFFF",
            ["LayerFillColorAlt"]                    = "#0AFFFFFF",

            // Accent fill brushes
            ["AccentFillColorDefaultBrush"]    = "#C42B1C",
            ["AccentFillColorSecondaryBrush"]  = "#D94A3D",
            ["AccentFillColorTertiaryBrush"]   = "#A32417",
            ["AccentFillColorDisabledBrush"]   = "#5CC42B1C",

            // Strokes
            ["ControlStrokeColorDefault"]   = "#3DF0EAEA",
            ["ControlStrokeColorSecondary"] = "#25F0EAEA",
            ["CardStrokeColorDefault"]      = "#30F0EAEA",
            ["SurfaceStrokeColorDefault"]   = "#40C42B1C",

            // Button backgrounds
            ["ButtonBackground"]                   = "#15C42B1C",
            ["ButtonBackgroundPointerOver"]         = "#25C42B1C",
            ["ButtonBackgroundPressed"]             = "#10C42B1C",
            ["ButtonBackgroundDisabled"]            = "#08FFFFFF",

            // ListBox selection
            ["ListBoxItemBackgroundPointerOver"]    = "#15C42B1C",
            ["ListBoxItemBackgroundPressed"]        = "#20C42B1C",
            ["ListBoxItemBackgroundSelected"]       = "#25C42B1C",
            ["ListBoxItemBackgroundSelectedPointerOver"]  = "#30C42B1C",
            ["ListBoxItemBackgroundSelectedPressed"]      = "#20C42B1C",

            // ToggleSwitch
            ["ToggleSwitchFillOn"]               = "#C42B1C",
            ["ToggleSwitchFillOnPointerOver"]    = "#D94A3D",
            ["ToggleSwitchFillOnPressed"]        = "#A32417",

            // ScrollBar
            ["ScrollBarThumbFill"]               = "#50C42B1C",
            ["ScrollBarThumbFillPointerOver"]    = "#80C42B1C",
            ["ScrollBarThumbFillPressed"]        = "#C42B1C",

            // TextControl
            ["TextControlBackgroundFocused"]     = "#20C42B1C",
            ["TextControlBorderBrushFocused"]    = "#C42B1C",

            // Selection highlight
            ["TextSelectionHighlightColor"]      = "#60C42B1C",
        },

        ["loki"] = new Dictionary<string, string>
        {
            // === Root's custom theme keys (Styles[0].Resources) ===
            // Trestle palette: Moss=#2a5a40, Pine=#1e402f, Shadow=#112318, Gold=#d4a847
            ["ThemeAccentColor"]     = "#2A5A40",
            ["ThemeAccentColor2"]    = "#3D7050",
            ["ThemeAccentColor3"]    = "#1E402F",
            ["ThemeAccentColor4"]    = "#112318",
            ["ThemeAccentBrush"]     = "#2A5A40",
            ["ThemeAccentBrush2"]    = "#3D7050",
            ["ThemeAccentBrush3"]    = "#1E402F",
            ["ThemeAccentBrush4"]    = "#112318",
            ["ThemeForegroundLowColor"]  = "#8CF0ECE0",
            ["ThemeForegroundLowBrush"]  = "#8CF0ECE0",
            ["HighlightForegroundColor"] = "#D4A847",  // Gold accent
            ["HighlightForegroundBrush"] = "#D4A847",  // Gold accent
            ["ErrorColor"]               = "#FF4444",
            ["ErrorLowColor"]            = "#80FF4444",
            ["ErrorBrush"]               = "#FF4444",
            ["ErrorLowBrush"]            = "#80FF4444",
            ["DatePickerFlyoutPresenterHighlightFill"]  = "#2A5A40",
            ["TimePickerFlyoutPresenterHighlightFill"]  = "#2A5A40",

            // === Standard FluentTheme keys ===
            ["SystemAccentColor"]       = "#2A5A40",
            ["SystemAccentColorDark1"]  = "#1E402F",
            ["SystemAccentColorDark2"]  = "#112318",
            ["SystemAccentColorDark3"]  = "#0D1A12",
            ["SystemAccentColorLight1"] = "#3D7050",
            ["SystemAccentColorLight2"] = "#508A62",
            ["SystemAccentColorLight3"] = "#6AA07A",

            ["TextFillColorPrimary"]    = "#FFF0ECE0",
            ["TextFillColorSecondary"]  = "#C8F0ECE0",
            ["TextFillColorTertiary"]   = "#8CF0ECE0",
            ["TextFillColorDisabled"]   = "#5CF0ECE0",

            ["ControlFillColorDefault"]   = "#15FFFFFF",
            ["ControlFillColorSecondary"] = "#10FFFFFF",
            ["ControlFillColorTertiary"]  = "#08FFFFFF",
            ["ControlFillColorDisabled"]  = "#06FFFFFF",

            ["SolidBackgroundFillColorBase"]        = "#0F1210",
            ["SolidBackgroundFillColorSecondary"]   = "#151A15",
            ["SolidBackgroundFillColorTertiary"]    = "#1A1F1A",
            ["SolidBackgroundFillColorQuarternary"] = "#202820",

            ["CardBackgroundFillColorDefault"]       = "#202A5A40",
            ["CardBackgroundFillColorDefaultBrush"]  = "#202A5A40",
            ["LayerFillColorDefault"]                = "#08FFFFFF",
            ["LayerFillColorAlt"]                    = "#0AFFFFFF",

            ["AccentFillColorDefaultBrush"]    = "#2A5A40",
            ["AccentFillColorSecondaryBrush"]  = "#3D7050",
            ["AccentFillColorTertiaryBrush"]   = "#1E402F",
            ["AccentFillColorDisabledBrush"]   = "#5C2A5A40",

            ["ControlStrokeColorDefault"]   = "#3DF0ECE0",
            ["ControlStrokeColorSecondary"] = "#25F0ECE0",
            ["CardStrokeColorDefault"]      = "#30F0ECE0",
            ["SurfaceStrokeColorDefault"]   = "#402A5A40",

            ["ButtonBackground"]                   = "#152A5A40",
            ["ButtonBackgroundPointerOver"]         = "#252A5A40",
            ["ButtonBackgroundPressed"]             = "#102A5A40",
            ["ButtonBackgroundDisabled"]            = "#08FFFFFF",

            ["ListBoxItemBackgroundPointerOver"]    = "#152A5A40",
            ["ListBoxItemBackgroundPressed"]        = "#202A5A40",
            ["ListBoxItemBackgroundSelected"]       = "#252A5A40",
            ["ListBoxItemBackgroundSelectedPointerOver"]  = "#302A5A40",
            ["ListBoxItemBackgroundSelectedPressed"]      = "#202A5A40",

            ["ToggleSwitchFillOn"]               = "#2A5A40",
            ["ToggleSwitchFillOnPointerOver"]    = "#3D7050",
            ["ToggleSwitchFillOnPressed"]        = "#1E402F",

            ["ScrollBarThumbFill"]               = "#502A5A40",
            ["ScrollBarThumbFillPointerOver"]    = "#802A5A40",
            ["ScrollBarThumbFillPressed"]        = "#2A5A40",

            ["TextControlBackgroundFocused"]     = "#202A5A40",
            ["TextControlBorderBrushFocused"]    = "#2A5A40",

            ["TextSelectionHighlightColor"]      = "#602A5A40",
        },

    };
}
