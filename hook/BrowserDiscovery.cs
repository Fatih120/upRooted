using System.Reflection;

namespace Uprooted;

/// <summary>
/// Read-only diagnostic scanner that runs after Phase 3 (MainWindow available).
/// Dumps everything we can find about browser engines, chat rendering, and
/// the visual tree structure to the log. Zero side effects.
///
/// Purpose: Root v0.9.92 removed DotNetBrowser entirely. This discovers what
/// (if anything) replaced it, and how chat messages are rendered.
/// </summary>
internal class BrowserDiscovery
{
    private const string Tag = "Discovery";

    private readonly AvaloniaReflection _r;
    private readonly object _mainWindow;
    private readonly VisualTreeWalker _walker;

    internal BrowserDiscovery(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _mainWindow = mainWindow;
        _walker = new VisualTreeWalker(resolver);
    }

    /// <summary>
    /// Main entry point. Call on UI thread.
    /// Logs all findings under [Discovery] category.
    /// </summary>
    internal void DumpAllFindings()
    {
        Logger.Log(Tag, "========================================");
        Logger.Log(Tag, "=== Browser Discovery Scan Start ===");
        Logger.Log(Tag, "========================================");

        try { DumpBrowserAssemblies(); } catch (Exception ex) { Logger.Log(Tag, $"Assembly scan error: {ex.Message}"); }
        try { DumpAllAssemblySummary(); } catch (Exception ex) { Logger.Log(Tag, $"Assembly summary error: {ex.Message}"); }
        try { DumpVisualTreeBrowserControls(); } catch (Exception ex) { Logger.Log(Tag, $"Visual tree scan error: {ex.Message}"); }
        try { DumpChatAreaStructure(); } catch (Exception ex) { Logger.Log(Tag, $"Chat area scan error: {ex.Message}"); }
        try { CheckPreloadBeacon(); } catch (Exception ex) { Logger.Log(Tag, $"Preload beacon error: {ex.Message}"); }

        Logger.Log(Tag, "========================================");
        Logger.Log(Tag, "=== Browser Discovery Scan Complete ===");
        Logger.Log(Tag, "========================================");
    }

    /// <summary>
    /// A. Scan all loaded assemblies for browser-related keywords.
    /// </summary>
    private void DumpBrowserAssemblies()
    {
        Logger.Log(Tag, "--- A. Browser-related assemblies ---");

        var keywords = new[] { "WebView", "WebKit", "Cef", "Chromium", "Browser", "Edge", "WebRtc", "Electron", "Web2", "DotNet" };
        int matchCount = 0;

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var name = asm.GetName().Name ?? "";
                var fullName = asm.GetName().FullName ?? "";

                bool matches = false;
                foreach (var kw in keywords)
                {
                    if (name.Contains(kw, StringComparison.OrdinalIgnoreCase))
                    {
                        matches = true;
                        break;
                    }
                }

                if (!matches) continue;
                matchCount++;

                Logger.Log(Tag, $"  MATCH: {fullName}");
                Logger.Log(Tag, $"    Location: {(string.IsNullOrEmpty(asm.Location) ? "(in-memory/single-file)" : asm.Location)}");

                // Dump public types in this assembly
                try
                {
                    var types = asm.GetTypes();
                    Logger.Log(Tag, $"    Types: {types.Length} total");
                    foreach (var type in types)
                    {
                        if (!type.IsPublic && !type.IsNestedPublic) continue;
                        Logger.Log(Tag, $"      {type.FullName} (interface={type.IsInterface}, abstract={type.IsAbstract})");
                    }
                }
                catch (ReflectionTypeLoadException rtle)
                {
                    Logger.Log(Tag, $"    TypeLoadException: {rtle.LoaderExceptions.Length} loader errors");
                    var loaded = rtle.Types.Where(t => t != null).ToList();
                    Logger.Log(Tag, $"    Partially loaded {loaded.Count} types");
                    foreach (var type in loaded)
                    {
                        if (type != null && (type.IsPublic || type.IsNestedPublic))
                            Logger.Log(Tag, $"      {type.FullName}");
                    }
                }
            }
            catch { }
        }

        Logger.Log(Tag, $"  Total browser-related assemblies: {matchCount}");
    }

    /// <summary>
    /// Compact summary of ALL loaded assemblies (not just browser ones).
    /// Helps identify what Root is using for its UI stack.
    /// </summary>
    private void DumpAllAssemblySummary()
    {
        Logger.Log(Tag, "--- A2. All loaded assemblies (name only) ---");

        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .OrderBy(a => a.GetName().Name ?? "")
            .ToList();

        Logger.Log(Tag, $"  Total loaded: {assemblies.Count}");

        foreach (var asm in assemblies)
        {
            try
            {
                var name = asm.GetName().Name ?? "(null)";
                var version = asm.GetName().Version?.ToString() ?? "?";
                Logger.Log(Tag, $"  {name} v{version}");
            }
            catch { }
        }
    }

    /// <summary>
    /// B. Walk the full visual tree from MainWindow looking for browser-like controls.
    /// </summary>
    private void DumpVisualTreeBrowserControls()
    {
        Logger.Log(Tag, "--- B. Visual tree scan for browser-like controls ---");

        var browserKeywords = new[] { "Web", "Browser", "Frame", "View2", "Cef", "Chromium" };
        int scanned = 0;
        int matches = 0;

        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            scanned++;
            var type = node.GetType();
            var typeName = type.Name;
            var fullName = type.FullName ?? typeName;

            // Check type name against keywords
            bool nameMatch = false;
            foreach (var kw in browserKeywords)
            {
                if (typeName.Contains(kw, StringComparison.OrdinalIgnoreCase))
                {
                    nameMatch = true;
                    break;
                }
            }

            // Check for browser-indicative properties
            var pub = BindingFlags.Public | BindingFlags.Instance;
            var sourceProperty = type.GetProperty("Source", pub);
            bool hasUriSource = sourceProperty != null &&
                (sourceProperty.PropertyType == typeof(Uri) ||
                 sourceProperty.PropertyType.Name.Contains("Uri"));

            var browserProperty = type.GetProperty("Browser", pub);
            bool hasBrowserProp = browserProperty != null;

            // Check for JS execution methods
            bool hasJsMethods = false;
            string? jsMethodName = null;
            try
            {
                foreach (var m in type.GetMethods(pub))
                {
                    if (m.Name.Contains("JavaScript", StringComparison.OrdinalIgnoreCase) ||
                        m.Name.Contains("Script", StringComparison.OrdinalIgnoreCase) ||
                        m.Name.Contains("Execute", StringComparison.OrdinalIgnoreCase))
                    {
                        hasJsMethods = true;
                        jsMethodName = m.Name;
                        break;
                    }
                }
            }
            catch { }

            if (!nameMatch && !hasUriSource && !hasBrowserProp && !hasJsMethods) continue;

            matches++;
            var path = GetVisualPath(node);
            Logger.Log(Tag, $"  BROWSER-LIKE: {fullName}");
            Logger.Log(Tag, $"    Path: {path}");
            Logger.Log(Tag, $"    NameMatch={nameMatch} UriSource={hasUriSource} BrowserProp={hasBrowserProp} JsMethods={hasJsMethods}");

            if (hasUriSource && sourceProperty != null)
            {
                try
                {
                    var sourceVal = sourceProperty.GetValue(node);
                    Logger.Log(Tag, $"    Source value: {sourceVal ?? "(null)"}");
                }
                catch { }
            }

            if (hasBrowserProp && browserProperty != null)
            {
                Logger.Log(Tag, $"    Browser property type: {browserProperty.PropertyType.FullName}");
                try
                {
                    var browserVal = browserProperty.GetValue(node);
                    Logger.Log(Tag, $"    Browser value: {(browserVal != null ? browserVal.GetType().FullName : "(null)")}");
                }
                catch { }
            }

            if (hasJsMethods)
            {
                Logger.Log(Tag, $"    JS-related method: {jsMethodName}");
                // Dump all JS-related methods
                try
                {
                    foreach (var m in type.GetMethods(pub))
                    {
                        if (m.Name.Contains("JavaScript", StringComparison.OrdinalIgnoreCase) ||
                            m.Name.Contains("Script", StringComparison.OrdinalIgnoreCase) ||
                            m.Name.Contains("Execute", StringComparison.OrdinalIgnoreCase))
                        {
                            var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                            Logger.Log(Tag, $"      {m.Name}({parms}) -> {m.ReturnType.Name}");
                        }
                    }
                }
                catch { }
            }
        }

        Logger.Log(Tag, $"  Scanned {scanned} visual tree nodes, found {matches} browser-like controls");
    }

    /// <summary>
    /// C. Analyze the chat area structure.
    /// Look for ItemsControl/ListBox (non-settings), TextBlocks with multi-word content,
    /// and log the parent hierarchy.
    /// </summary>
    private void DumpChatAreaStructure()
    {
        Logger.Log(Tag, "--- C. Chat area structure analysis ---");

        // Strategy 1: Find all ItemsControl/ListBox that are NOT in the settings area
        var interestingContainers = new List<(object node, string typeName, string path)>();
        var settingsTextBlock = _walker.FindFirstTextBlock(_mainWindow, "APP SETTINGS");
        object? settingsSubtree = null;

        // If settings is open, find the settings layout container to exclude it
        if (settingsTextBlock != null)
        {
            var current = _r.GetParent(settingsTextBlock);
            for (int i = 0; i < 20 && current != null; i++)
            {
                if (_r.IsGrid(current) && _r.GetChildCount(current) >= 3)
                {
                    settingsSubtree = current;
                    break;
                }
                current = _r.GetParent(current);
            }
        }

        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var typeName = node.GetType().Name;

            // Skip if inside settings subtree
            if (settingsSubtree != null && IsDescendantOf(node, settingsSubtree))
                continue;

            if (typeName == "ItemsControl" || typeName == "ListBox" ||
                typeName.Contains("VirtualizingStackPanel") || typeName.Contains("ItemsRepeater"))
            {
                var path = GetVisualPath(node);
                interestingContainers.Add((node, typeName, path));
            }
        }

        Logger.Log(Tag, $"  Found {interestingContainers.Count} list-like containers (outside settings)");

        foreach (var (container, typeName, path) in interestingContainers)
        {
            var childCount = _r.GetChildCount(container);
            Logger.Log(Tag, $"  Container: {typeName} ({childCount} children)");
            Logger.Log(Tag, $"    Path: {path}");

            // Log the first few children's types
            var children = _r.GetVisualChildren(container).Take(5).ToList();
            foreach (var child in children)
            {
                var childTypeName = child.GetType().Name;
                var text = _r.IsTextBlock(child) ? $" Text=\"{Truncate(_r.GetText(child), 60)}\"" : "";
                Logger.Log(Tag, $"    Child: {childTypeName}{text}");

                // One level deeper
                foreach (var grandchild in _r.GetVisualChildren(child).Take(3))
                {
                    var gcType = grandchild.GetType().Name;
                    var gcText = _r.IsTextBlock(grandchild) ? $" Text=\"{Truncate(_r.GetText(grandchild), 60)}\"" : "";
                    Logger.Log(Tag, $"      {gcType}{gcText}");
                }
            }
        }

        // Strategy 2: Find TextBlocks with multi-word content that look like chat messages
        Logger.Log(Tag, "  --- Multi-word TextBlocks (possible chat messages) ---");
        int messageCandidate = 0;

        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            if (settingsSubtree != null && IsDescendantOf(node, settingsSubtree))
                continue;

            if (!_r.IsTextBlock(node)) continue;

            var text = _r.GetText(node);
            if (text == null) continue;

            // Multi-word content that looks like a user message (not a label)
            var wordCount = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            if (wordCount < 4 || text.Length < 20) continue;

            // Skip common UI text
            if (text.StartsWith("APP ") || text.StartsWith("Version ") ||
                text.Contains("uprooted", StringComparison.OrdinalIgnoreCase)) continue;

            messageCandidate++;
            if (messageCandidate <= 10)
            {
                var path = GetVisualPath(node);
                Logger.Log(Tag, $"  Message candidate: \"{Truncate(text, 80)}\"");
                Logger.Log(Tag, $"    Path: {path}");

                // Log the parent hierarchy (5 levels up)
                var current = _r.GetParent(node);
                for (int i = 0; i < 5 && current != null; i++)
                {
                    var parentType = current.GetType().Name;
                    var parentChildCount = _r.GetChildCount(current);
                    Logger.Log(Tag, $"    Parent[{i}]: {parentType} ({parentChildCount} children)");
                    current = _r.GetParent(current);
                }
            }
        }

        Logger.Log(Tag, $"  Total message candidates: {messageCandidate}");

        // Strategy 3: Dump top-level structure (first 3 levels) of non-settings areas
        Logger.Log(Tag, "  --- Top-level visual tree structure (depth 4) ---");
        DumpSubtree(_mainWindow, 0, 4, settingsSubtree);
    }

    /// <summary>
    /// D. Check if the TypeScript preload beacon file exists.
    /// The HTML-injected preload should write a marker file on load.
    /// </summary>
    private void CheckPreloadBeacon()
    {
        Logger.Log(Tag, "--- D. Preload beacon check ---");

        var uprootedDir = PlatformPaths.GetUprootedDir();
        var beaconPath = Path.Combine(uprootedDir, "preload-beacon.txt");

        if (File.Exists(beaconPath))
        {
            try
            {
                var content = File.ReadAllText(beaconPath);
                Logger.Log(Tag, $"  Beacon EXISTS: {beaconPath}");
                Logger.Log(Tag, $"  Content: {Truncate(content, 200)}");

                // Check age
                var lastWrite = File.GetLastWriteTime(beaconPath);
                var age = DateTime.Now - lastWrite;
                Logger.Log(Tag, $"  Last written: {lastWrite:yyyy-MM-dd HH:mm:ss} ({age.TotalMinutes:F1} min ago)");
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"  Beacon read error: {ex.Message}");
            }
        }
        else
        {
            Logger.Log(Tag, $"  Beacon NOT FOUND: {beaconPath}");
            Logger.Log(Tag, "  (TypeScript preload may not be executing in any web context)");
        }

        // Also check if dist/uprooted.js exists (the TS bundle)
        var distJs = Path.Combine(uprootedDir, "dist", "uprooted.js");
        var altDistJs = Path.Combine(uprootedDir, "uprooted.js");
        Logger.Log(Tag, $"  dist/uprooted.js: {(File.Exists(distJs) ? "EXISTS" : "NOT FOUND")}");
        Logger.Log(Tag, $"  uprooted.js: {(File.Exists(altDistJs) ? "EXISTS" : "NOT FOUND")}");

        // Check HTML files for preload script tags
        var profileDir = PlatformPaths.GetProfileDir();
        var htmlFiles = new[] { "index.html", "main.html", "app.html" };
        foreach (var htmlFile in htmlFiles)
        {
            var htmlPath = Path.Combine(profileDir, htmlFile);
            if (!File.Exists(htmlPath)) continue;

            try
            {
                var html = File.ReadAllText(htmlPath);
                var hasUprooted = html.Contains("uprooted", StringComparison.OrdinalIgnoreCase);
                var hasPreload = html.Contains("preload", StringComparison.OrdinalIgnoreCase);
                Logger.Log(Tag, $"  {htmlFile}: exists, contains 'uprooted'={hasUprooted}, 'preload'={hasPreload}");
            }
            catch { }
        }
    }

    // ===== Helpers =====

    /// <summary>
    /// Build a visual tree path string from MainWindow to the given node.
    /// </summary>
    private string GetVisualPath(object node)
    {
        var parts = new List<string>();
        var current = node;
        int safety = 0;

        while (current != null && safety < 30)
        {
            parts.Add(current.GetType().Name);
            if (current == _mainWindow) break;
            current = _r.GetParent(current);
            safety++;
        }

        parts.Reverse();
        return string.Join(" > ", parts);
    }

    /// <summary>
    /// Check if a node is a descendant of a given ancestor.
    /// </summary>
    private bool IsDescendantOf(object node, object ancestor)
    {
        var current = _r.GetParent(node);
        int safety = 0;
        while (current != null && safety < 50)
        {
            if (current == ancestor) return true;
            current = _r.GetParent(current);
            safety++;
        }
        return false;
    }

    /// <summary>
    /// Dump subtree structure to log, skipping a subtree if provided.
    /// </summary>
    private void DumpSubtree(object node, int depth, int maxDepth, object? skipSubtree)
    {
        if (depth > maxDepth) return;
        if (skipSubtree != null && node == skipSubtree)
        {
            var indent = new string(' ', depth * 2);
            Logger.Log(Tag, $"  {indent}[SETTINGS SUBTREE - SKIPPED]");
            return;
        }

        var typeName = node.GetType().Name;
        var text = _r.IsTextBlock(node) ? $" \"{Truncate(_r.GetText(node), 40)}\"" : "";
        var childCount = _r.GetChildCount(node);
        var childInfo = childCount > 0 ? $" ({childCount}ch)" : "";
        var indent2 = new string(' ', depth * 2);

        Logger.Log(Tag, $"  {indent2}{typeName}{text}{childInfo}");

        foreach (var child in _r.GetVisualChildren(node))
        {
            DumpSubtree(child, depth + 1, maxDepth, skipSubtree);
        }
    }

    private static string Truncate(string? s, int maxLen)
    {
        if (s == null) return "(null)";
        return s.Length <= maxLen ? s : s[..maxLen] + "...";
    }
}
