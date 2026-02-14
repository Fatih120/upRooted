using System.Collections;
using System.Linq;

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

    public string? ActiveThemeName => _activeThemeName;

    public ThemeEngine(AvaloniaReflection r)
    {
        _r = r;
    }

    /// <summary>
    /// Apply a theme by name. Directly overrides resources in Styles[0].Resources
    /// for Root-specific keys, and adds a MergedDictionary for FluentTheme keys.
    /// </summary>
    public bool ApplyTheme(string name)
    {
        var themeName = name.ToLower().Trim();
        if (!Themes.TryGetValue(themeName, out var palette))
        {
            Logger.Log("Theme", "Unknown theme: " + name);
            return false;
        }

        Logger.Log("Theme", "Applying theme: " + themeName + " (" + palette.Count + " resource overrides)");

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
                    // Save original value before overriding
                    if (!_savedOriginals.ContainsKey(key))
                    {
                        try
                        {
                            var original = _r.GetResource(styleRes, key);
                            if (original != null)
                                _savedOriginals[key] = original;
                        }
                        catch { }
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
        if (TreeColorMaps.TryGetValue(themeName, out var colorMap))
        {
            _activeColorMap = colorMap;
            _reverseColorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var (orig, repl) in colorMap)
                _reverseColorMap[repl] = orig;
            Logger.Log("Theme", "Color map loaded: " + colorMap.Count + " mappings for visual tree walk");
        }

        // === Phase 4: Schedule delayed visual tree walks ===
        // The main UI loads asynchronously after auth, so walk multiple times.
        ScheduleVisualTreeWalks();

        // Install layout interceptor for near-instant recoloring on navigation
        InstallLayoutInterceptor();

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
        }, null, 1000, 500); // First walk at 1s, then every 500ms
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
        ["crimson"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Blue accents -> crimson
            ["#ff3b6af8"] = "#ffc42b1c",
            ["#ff4a78f9"] = "#ffd94a3d",
            ["#ff2e59d1"] = "#ffa32417",
            ["#ff2148af"] = "#ff821d12",
            ["#ff5b88ff"] = "#ffe06b60",
            ["#ff3366ff"] = "#ffd94a3d",
            ["#663b6af8"] = "#66c42b1c",
            ["#333b6af8"] = "#33c42b1c",
            ["#193b6af8"] = "#19c42b1c",

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
            ["#ff3366ff"] = "#ff3d7050",    // lighter moss
            ["#663b6af8"] = "#662a5a40",
            ["#333b6af8"] = "#332a5a40",
            ["#193b6af8"] = "#192a5a40",

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
            ["#ff505050"] = "#ff3d4a35",    // Gray border -> trestle border

            // Text: semi-transparent variants (warm tint)
            ["#a3f2f2f2"] = "#a3f0ece0",
            ["#66f2f2f2"] = "#66f0ece0",
            // NOTE: #19ffffff/#0affffff (hover overlays) intentionally excluded -
            // theming them makes hover effects persist permanently

            // Text: warm earthy tint
            ["#ffdedede"] = "#ffe0d8c8",
            ["#fff2f2f2"] = "#fff0ece0",
        },

        ["onyx"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Blue accents -> cool silver
            ["#ff3b6af8"] = "#ffa0a8b0",
            ["#ff4a78f9"] = "#ffb0b8c0",
            ["#ff2e59d1"] = "#ff8890a0",
            ["#ff2148af"] = "#ff707888",
            ["#ff5b88ff"] = "#ffc0c8d0",
            ["#ff3366ff"] = "#ffb0b8c0",
            ["#663b6af8"] = "#66a0a8b0",
            ["#333b6af8"] = "#33a0a8b0",
            ["#193b6af8"] = "#19a0a8b0",

            // Structural dark backgrounds -> pure OLED black
            ["#ff0d1521"] = "#ff000000",    // Main dark bg -> pure black
            ["#ff07101b"] = "#ff000000",    // Darker bg -> pure black
            ["#ff090e13"] = "#ff000000",    // Near-black bg -> pure black
            ["#ff0a1a2e"] = "#ff050505",    // Another dark bg
            ["#ff101c2e"] = "#ff0a0a0a",    // Slightly lighter
            ["#ff121a26"] = "#ff080808",    // DM/chat panel bg
            ["#ff141e2b"] = "#ff0e0e0e",    // Panel bg
            ["#ff282828"] = "#ff141414",    // Neutral gray -> dark gray
            ["#ff4f5c6f"] = "#ff484848",    // Gray-blue metadata -> neutral gray

            // Dark borders -> very dark gray
            ["#ff242c36"] = "#ff222222",
            ["#ff1a2230"] = "#ff1a1a1a",
            ["#ff505050"] = "#ff2a2a2a",

            // Text: slightly dimmer for OLED comfort
            ["#a3f2f2f2"] = "#a3e0e0e0",
            ["#66f2f2f2"] = "#66e0e0e0",
            // NOTE: #19ffffff/#0affffff (hover overlays) intentionally excluded -
            // theming them makes hover effects persist permanently

            // Text: slightly muted
            ["#ffdedede"] = "#ffd0d0d0",
            ["#fff2f2f2"] = "#ffe8e8e8",
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
    /// Walk the tree and restore all theme colors back to originals using the reverse map.
    /// </summary>
    private int WalkAndRestore(object visual, int depth)
    {
        if (depth > 50 || _reverseColorMap == null) return 0;
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
                        // On revert, ClearValue to remove our Style-priority override
                        // and let the original style/template value reassert
                        var fieldName = AvaloniaReflection.PropertyToFieldName(propName);
                        _r.SetValueStylePriority(visual, fieldName, _r.CreateBrush(original));
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

        // Walk all windows and restore original colors using reverse map
        if (_activeThemeName != null && _reverseColorMap != null)
        {
            try
            {
                int restored = 0;
                foreach (var window in _r.GetAllWindows())
                {
                    try { restored += WalkAndRestore(window, 0); }
                    catch { }
                }
                Logger.Log("Theme", "Revert walk: " + restored + " controls restored to originals");
            }
            catch (Exception ex)
            {
                Logger.Log("Theme", "Revert walk error: " + ex.Message);
            }
        }

        // Restore Styles[0].Resources originals
        if (_savedOriginals.Count > 0)
        {
            var styleRes = _r.GetStyleResources(0);
            if (styleRes != null)
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
            _savedOriginals.Clear();
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

        _activeThemeName = null;
        _activeColorMap = null;
        _reverseColorMap = null;
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
        if (_activeThemeName != null && Themes.TryGetValue(_activeThemeName, out var p))
        {
            if (p.TryGetValue("ThemeAccentColor", out var hex)) return hex;
            if (p.TryGetValue("SystemAccentColor", out hex)) return hex;
        }
        return "#3B6AF8"; // Root's default blue accent
    }

    public string GetBgPrimary()
    {
        if (_activeThemeName != null && Themes.TryGetValue(_activeThemeName, out var p))
        {
            if (p.TryGetValue("SolidBackgroundFillColorBase", out var hex)) return hex;
        }
        return "#0D1521"; // Root's default dark bg
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

        ["onyx"] = new Dictionary<string, string>
        {
            // === Root's custom theme keys (Styles[0].Resources) ===
            // OLED Onyx: pure black backgrounds, cool silver accent
            ["ThemeAccentColor"]     = "#A0A8B0",
            ["ThemeAccentColor2"]    = "#B0B8C0",
            ["ThemeAccentColor3"]    = "#8890A0",
            ["ThemeAccentColor4"]    = "#707888",
            ["ThemeAccentBrush"]     = "#A0A8B0",
            ["ThemeAccentBrush2"]    = "#B0B8C0",
            ["ThemeAccentBrush3"]    = "#8890A0",
            ["ThemeAccentBrush4"]    = "#707888",
            ["ThemeForegroundLowColor"]  = "#8CE0E0E0",
            ["ThemeForegroundLowBrush"]  = "#8CE0E0E0",
            ["HighlightForegroundColor"] = "#A0A8B0",
            ["HighlightForegroundBrush"] = "#A0A8B0",
            ["ErrorColor"]               = "#FF4444",
            ["ErrorLowColor"]            = "#80FF4444",
            ["ErrorBrush"]               = "#FF4444",
            ["ErrorLowBrush"]            = "#80FF4444",
            ["DatePickerFlyoutPresenterHighlightFill"]  = "#A0A8B0",
            ["TimePickerFlyoutPresenterHighlightFill"]  = "#A0A8B0",

            // === Standard FluentTheme keys ===
            ["SystemAccentColor"]       = "#A0A8B0",
            ["SystemAccentColorDark1"]  = "#8890A0",
            ["SystemAccentColorDark2"]  = "#707888",
            ["SystemAccentColorDark3"]  = "#585F70",
            ["SystemAccentColorLight1"] = "#B0B8C0",
            ["SystemAccentColorLight2"] = "#C0C8D0",
            ["SystemAccentColorLight3"] = "#D0D8E0",

            ["TextFillColorPrimary"]    = "#FFE0E0E0",
            ["TextFillColorSecondary"]  = "#C8E0E0E0",
            ["TextFillColorTertiary"]   = "#8CE0E0E0",
            ["TextFillColorDisabled"]   = "#5CE0E0E0",

            ["ControlFillColorDefault"]   = "#15FFFFFF",
            ["ControlFillColorSecondary"] = "#10FFFFFF",
            ["ControlFillColorTertiary"]  = "#08FFFFFF",
            ["ControlFillColorDisabled"]  = "#06FFFFFF",

            ["SolidBackgroundFillColorBase"]        = "#000000",
            ["SolidBackgroundFillColorSecondary"]   = "#0A0A0A",
            ["SolidBackgroundFillColorTertiary"]    = "#111111",
            ["SolidBackgroundFillColorQuarternary"] = "#181818",

            ["CardBackgroundFillColorDefault"]       = "#20A0A8B0",
            ["CardBackgroundFillColorDefaultBrush"]  = "#20A0A8B0",
            ["LayerFillColorDefault"]                = "#08FFFFFF",
            ["LayerFillColorAlt"]                    = "#0AFFFFFF",

            ["AccentFillColorDefaultBrush"]    = "#A0A8B0",
            ["AccentFillColorSecondaryBrush"]  = "#B0B8C0",
            ["AccentFillColorTertiaryBrush"]   = "#8890A0",
            ["AccentFillColorDisabledBrush"]   = "#5CA0A8B0",

            ["ControlStrokeColorDefault"]   = "#3DE0E0E0",
            ["ControlStrokeColorSecondary"] = "#25E0E0E0",
            ["CardStrokeColorDefault"]      = "#30E0E0E0",
            ["SurfaceStrokeColorDefault"]   = "#40A0A8B0",

            ["ButtonBackground"]                   = "#15A0A8B0",
            ["ButtonBackgroundPointerOver"]         = "#25A0A8B0",
            ["ButtonBackgroundPressed"]             = "#10A0A8B0",
            ["ButtonBackgroundDisabled"]            = "#08FFFFFF",

            ["ListBoxItemBackgroundPointerOver"]    = "#15A0A8B0",
            ["ListBoxItemBackgroundPressed"]        = "#20A0A8B0",
            ["ListBoxItemBackgroundSelected"]       = "#25A0A8B0",
            ["ListBoxItemBackgroundSelectedPointerOver"]  = "#30A0A8B0",
            ["ListBoxItemBackgroundSelectedPressed"]      = "#20A0A8B0",

            ["ToggleSwitchFillOn"]               = "#A0A8B0",
            ["ToggleSwitchFillOnPointerOver"]    = "#B0B8C0",
            ["ToggleSwitchFillOnPressed"]        = "#8890A0",

            ["ScrollBarThumbFill"]               = "#50A0A8B0",
            ["ScrollBarThumbFillPointerOver"]    = "#80A0A8B0",
            ["ScrollBarThumbFillPressed"]        = "#A0A8B0",

            ["TextControlBackgroundFocused"]     = "#20A0A8B0",
            ["TextControlBorderBrushFocused"]    = "#A0A8B0",

            ["TextSelectionHighlightColor"]      = "#60A0A8B0",
        },
    };
}
