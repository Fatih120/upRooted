using System.Reflection;

namespace Uprooted;

/// <summary>
/// Cached reflection handles for DotNetBrowser types, properties, and methods.
/// Follows AvaloniaReflection patterns: assembly scanning, type caching, nullable returns.
/// Used by NsfwFilter and LinkEmbedInjector to execute JavaScript in browser frames.
///
/// Supports deferred resolution: if ExecuteJavaScript method can't be found at type-scan time
/// (e.g. because it lives on a concrete class rather than the interface), it will be resolved
/// on first use from the live frame object's runtime type.
/// </summary>
internal class DotNetBrowserReflection
{
    // DotNetBrowser types
    public Type? BrowserViewType { get; private set; }
    public Type? IBrowserType { get; private set; }
    public Type? IFrameType { get; private set; }

    // Cached property/method handles
    private PropertyInfo? _browserViewBrowser;   // BrowserView.Browser -> IBrowser
    private PropertyInfo? _browserMainFrame;     // IBrowser.MainFrame -> IFrame
    private MethodInfo? _frameExecuteJavaScript;  // IFrame.ExecuteJavaScript(string) -> object
    private readonly object _deferredLock = new();
    private bool _deferredResolutionAttempted;

    public bool IsResolved { get; private set; }

    public bool Resolve()
    {
        try
        {
            ResolveTypes();
            ResolveMembers();

            // Partial resolution: we can proceed if we have IBrowser (BrowserView can be found
            // via visual tree walk, and ExecuteJavaScript can be resolved at runtime from the
            // concrete frame object type)
            IsResolved = IBrowserType != null && _browserMainFrame != null;

            Logger.Log("DotNetBrowser", $"Resolved: {IsResolved} " +
                $"(BrowserView={BrowserViewType != null}, IBrowser={IBrowserType != null}, " +
                $"IFrame={IFrameType != null}, ExecJS={_frameExecuteJavaScript != null})");

            if (!IsResolved)
                DumpDiagnostics();

            return IsResolved;
        }
        catch (Exception ex)
        {
            Logger.Log("DotNetBrowser", $"Resolve failed: {ex}");
            DumpDiagnostics();
            return false;
        }
    }

    private void ResolveTypes()
    {
        var typeMap = new Dictionary<string, Type>();

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var name = asm.GetName().Name ?? "";
                if (!name.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase)) continue;

                foreach (var type in asm.GetTypes())
                {
                    var fn = type.FullName;
                    if (fn != null) typeMap[fn] = type;
                }
            }
            catch { }
        }

        Logger.Log("DotNetBrowser", $"Scanned DotNetBrowser assemblies, found {typeMap.Count} types");

        Type? Find(string fullName) => typeMap.TryGetValue(fullName, out var t) ? t : null;

        // BrowserView -- the Avalonia control
        BrowserViewType = Find("DotNetBrowser.AvaloniaUi.BrowserView");
        // Fallback: search by name suffix in DotNetBrowser assemblies
        BrowserViewType ??= typeMap.Values.FirstOrDefault(t =>
            t.Name == "BrowserView" && !t.IsAbstract && !t.IsInterface);

        // Fallback: search ALL loaded assemblies for BrowserView (Root may wrap it)
        if (BrowserViewType == null)
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (var type in asm.GetTypes())
                    {
                        if (type.Name == "BrowserView" && !type.IsAbstract && !type.IsInterface)
                        {
                            BrowserViewType = type;
                            Logger.Log("DotNetBrowser", $"  BrowserView found in non-DotNetBrowser assembly: {asm.GetName().Name}");
                            break;
                        }
                    }
                    if (BrowserViewType != null) break;
                }
                catch { }
            }
        }

        // IBrowser -- browser instance
        IBrowserType = Find("DotNetBrowser.Browser.IBrowser");
        IBrowserType ??= typeMap.Values.FirstOrDefault(t =>
            t.Name == "IBrowser" && t.IsInterface);

        // IFrame -- frame for JS execution
        IFrameType = Find("DotNetBrowser.Frame.IFrame");
        IFrameType ??= Find("DotNetBrowser.Frames.IFrame");
        IFrameType ??= typeMap.Values.FirstOrDefault(t =>
            t.Name == "IFrame" && t.IsInterface);

        Logger.Log("DotNetBrowser", $"  BrowserView: {(BrowserViewType != null ? BrowserViewType.FullName : "MISSING")}");
        Logger.Log("DotNetBrowser", $"  IBrowser: {(IBrowserType != null ? IBrowserType.FullName : "MISSING")}");
        Logger.Log("DotNetBrowser", $"  IFrame: {(IFrameType != null ? IFrameType.FullName : "MISSING")}");
    }

    private void ResolveMembers()
    {
        var pub = BindingFlags.Public | BindingFlags.Instance;

        // BrowserView.Browser property
        _browserViewBrowser = BrowserViewType?.GetProperty("Browser", pub);

        // IBrowser.MainFrame property
        _browserMainFrame = IBrowserType?.GetProperty("MainFrame", pub);

        // Log the MainFrame property return type -- may not be IFrame
        if (_browserMainFrame != null)
        {
            var mainFrameReturnType = _browserMainFrame.PropertyType;
            Logger.Log("DotNetBrowser", $"  IBrowser.MainFrame returns: {mainFrameReturnType.FullName}");

            // If the return type is different from IFrameType, use it for method search
            if (IFrameType == null || (mainFrameReturnType != IFrameType && mainFrameReturnType.IsInterface))
            {
                Logger.Log("DotNetBrowser", $"  Using MainFrame return type as frame type: {mainFrameReturnType.FullName}");
                IFrameType ??= mainFrameReturnType;
            }
        }

        // Search for ExecuteJavaScript across multiple sources
        _frameExecuteJavaScript = FindExecuteJavaScriptMethod(pub);

        Logger.Log("DotNetBrowser", $"  BrowserView.Browser: {(_browserViewBrowser != null ? "OK" : "MISSING")}");
        Logger.Log("DotNetBrowser", $"  IBrowser.MainFrame: {(_browserMainFrame != null ? "OK" : "MISSING")}");
        Logger.Log("DotNetBrowser", $"  ExecuteJavaScript: {(_frameExecuteJavaScript != null ? $"OK ({_frameExecuteJavaScript.DeclaringType?.Name}.{_frameExecuteJavaScript.Name})" : "MISSING (will attempt deferred resolution)")}");
    }

    private MethodInfo? FindExecuteJavaScriptMethod(BindingFlags pub)
    {
        MethodInfo? result = null;

        // Strategy 1: Search IFrame directly
        if (IFrameType != null)
        {
            result = SearchTypeForExecJS(IFrameType, pub, "IFrame");
            if (result != null) return result;

            // Search IFrame's interface hierarchy
            if (IFrameType.IsInterface)
            {
                foreach (var iface in IFrameType.GetInterfaces())
                {
                    result = SearchTypeForExecJS(iface, pub, iface.Name);
                    if (result != null) return result;
                }
            }
        }

        // Strategy 2: Search IBrowser directly (older DotNetBrowser versions)
        if (IBrowserType != null)
        {
            result = SearchTypeForExecJS(IBrowserType, pub, "IBrowser");
            if (result != null) return result;

            if (IBrowserType.IsInterface)
            {
                foreach (var iface in IBrowserType.GetInterfaces())
                {
                    result = SearchTypeForExecJS(iface, pub, iface.Name);
                    if (result != null) return result;
                }
            }
        }

        // Strategy 3: Search the MainFrame property return type (may be more specific than IFrame)
        if (_browserMainFrame != null)
        {
            var mainFrameReturnType = _browserMainFrame.PropertyType;
            if (mainFrameReturnType != IFrameType && mainFrameReturnType != IBrowserType)
            {
                result = SearchTypeForExecJS(mainFrameReturnType, pub, mainFrameReturnType.Name);
                if (result != null) return result;

                if (mainFrameReturnType.IsInterface)
                {
                    foreach (var iface in mainFrameReturnType.GetInterfaces())
                    {
                        result = SearchTypeForExecJS(iface, pub, iface.Name);
                        if (result != null) return result;
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Search a single type for an ExecuteJavaScript-like method. Handles generic methods.
    /// </summary>
    private static MethodInfo? SearchTypeForExecJS(Type type, BindingFlags pub, string logLabel)
    {
        MethodInfo[] allMethods;
        try { allMethods = type.GetMethods(pub); }
        catch { return null; }

        // Log methods related to script execution for diagnostics
        foreach (var m in allMethods)
        {
            if (m.Name.Contains("Java", StringComparison.OrdinalIgnoreCase) ||
                m.Name.Contains("Script", StringComparison.OrdinalIgnoreCase) ||
                m.Name.Contains("Evaluate", StringComparison.OrdinalIgnoreCase) ||
                m.Name.Contains("Execute", StringComparison.OrdinalIgnoreCase))
            {
                var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                var generic = m.IsGenericMethod ? $"<{m.GetGenericArguments().Length} generic>" : "";
                Logger.Log("DotNetBrowser", $"  {logLabel} method: {m.Name}{generic}({parms}) -> {m.ReturnType.Name}");
            }
        }

        // Exact match: non-generic, single string param
        var found = allMethods.FirstOrDefault(m =>
            m.Name == "ExecuteJavaScript" && !m.IsGenericMethod &&
            m.GetParameters().Length == 1 &&
            m.GetParameters()[0].ParameterType == typeof(string));
        if (found != null) return found;

        // Generic: ExecuteJavaScript<T>(string) — make concrete with T=object
        found = allMethods.FirstOrDefault(m =>
            m.Name == "ExecuteJavaScript" && m.IsGenericMethod &&
            m.GetParameters().Length == 1 &&
            m.GetParameters()[0].ParameterType == typeof(string));
        if (found != null)
        {
            try
            {
                var concrete = found.MakeGenericMethod(typeof(object));
                Logger.Log("DotNetBrowser", $"  Resolved generic ExecuteJavaScript<object> on {logLabel}");
                return concrete;
            }
            catch (Exception ex)
            {
                Logger.Log("DotNetBrowser", $"  MakeGenericMethod failed on {logLabel}: {ex.Message}");
            }
        }

        // Generic with any single param (param might not be typeof(string) at definition level)
        found = allMethods.FirstOrDefault(m =>
            m.Name == "ExecuteJavaScript" && m.IsGenericMethod &&
            m.GetParameters().Length == 1);
        if (found != null)
        {
            try
            {
                var concrete = found.MakeGenericMethod(typeof(object));
                Logger.Log("DotNetBrowser", $"  Resolved generic ExecuteJavaScript<object>(any) on {logLabel}");
                return concrete;
            }
            catch (Exception ex)
            {
                Logger.Log("DotNetBrowser", $"  MakeGenericMethod (any param) failed on {logLabel}: {ex.Message}");
            }
        }

        // Any single-param non-generic overload of ExecuteJavaScript
        found = allMethods.FirstOrDefault(m =>
            m.Name == "ExecuteJavaScript" && !m.IsGenericMethod &&
            m.GetParameters().Length == 1);
        if (found != null) return found;

        // Broader: ExecuteJavaScriptAndReturnValue (older API)
        found = allMethods.FirstOrDefault(m =>
            m.Name.Contains("ExecuteJavaScript", StringComparison.Ordinal) &&
            m.GetParameters().Length >= 1 &&
            m.GetParameters()[0].ParameterType == typeof(string));
        if (found != null) return found;

        // Broader name patterns: Evaluate, RunScript
        foreach (var name in new[] { "EvaluateJavaScript", "RunJavaScript", "EvalScript", "RunScript" })
        {
            found = allMethods.FirstOrDefault(m =>
                m.Name == name &&
                m.GetParameters().Length >= 1 &&
                m.GetParameters()[0].ParameterType == typeof(string));
            if (found != null) return found;
        }

        return null;
    }

    /// <summary>
    /// Attempt to resolve the ExecuteJavaScript method from a live frame object's runtime type.
    /// Called when static resolution failed but we have an actual frame instance.
    /// </summary>
    private MethodInfo? DeferredResolveExecJS(object frame)
    {
        lock (_deferredLock)
        {
            // Double-check: another thread may have resolved it
            if (_frameExecuteJavaScript != null) return _frameExecuteJavaScript;
            if (_deferredResolutionAttempted) return null;
            _deferredResolutionAttempted = true;
        }

        var pub = BindingFlags.Public | BindingFlags.Instance;
        var runtimeType = frame.GetType();
        Logger.Log("DotNetBrowser", $"Deferred resolution: searching runtime type {runtimeType.FullName}");

        // Search the concrete type
        var result = SearchTypeForExecJS(runtimeType, pub, runtimeType.Name);

        // Search all interfaces of the concrete type
        if (result == null)
        {
            foreach (var iface in runtimeType.GetInterfaces())
            {
                result = SearchTypeForExecJS(iface, pub, iface.Name);
                if (result != null) break;
            }
        }

        if (result != null)
        {
            lock (_deferredLock)
            {
                _frameExecuteJavaScript = result;
            }
            Logger.Log("DotNetBrowser", $"Deferred resolution succeeded: {result.DeclaringType?.Name}.{result.Name}");
        }
        else
        {
            Logger.Log("DotNetBrowser", "Deferred resolution failed: no ExecuteJavaScript-like method found on runtime type");
        }

        return result;
    }

    /// <summary>
    /// Comprehensive diagnostics dump when Resolve() fails.
    /// Logs everything we can find about DotNetBrowser's actual API surface.
    /// </summary>
    private void DumpDiagnostics()
    {
        Logger.Log("DotNetBrowser", "=== DIAGNOSTICS START ===");

        var pub = BindingFlags.Public | BindingFlags.Instance;

        // 1. All DotNetBrowser assembly names
        Logger.Log("DotNetBrowser", "--- Loaded DotNetBrowser assemblies ---");
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var name = asm.GetName().Name ?? "";
                if (name.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase))
                    Logger.Log("DotNetBrowser", $"  Assembly: {asm.GetName().FullName}");
            }
            catch { }
        }

        // 2. Key types — any type whose name contains these keywords
        Logger.Log("DotNetBrowser", "--- Key types matching View/Browser/Frame/Script/Execute/Engine ---");
        var keywords = new[] { "View", "Browser", "Frame", "Script", "Java", "Execute", "Engine" };
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase)) continue;

                foreach (var type in asm.GetTypes())
                {
                    var tn = type.Name;
                    foreach (var kw in keywords)
                    {
                        if (tn.Contains(kw, StringComparison.OrdinalIgnoreCase))
                        {
                            Logger.Log("DotNetBrowser", $"  {type.FullName} (interface={type.IsInterface}, abstract={type.IsAbstract})");
                            break;
                        }
                    }
                }
            }
            catch { }
        }

        // 3. All methods on IBrowser
        if (IBrowserType != null)
        {
            Logger.Log("DotNetBrowser", $"--- All IBrowser methods ({IBrowserType.FullName}) ---");
            try
            {
                foreach (var m in IBrowserType.GetMethods(pub))
                {
                    var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                    var generic = m.IsGenericMethod ? $"<{m.GetGenericArguments().Length}>" : "";
                    Logger.Log("DotNetBrowser", $"  {m.Name}{generic}({parms}) -> {m.ReturnType.Name}");
                }
            }
            catch (Exception ex) { Logger.Log("DotNetBrowser", $"  Error enumerating: {ex.Message}"); }

            // IBrowser properties
            Logger.Log("DotNetBrowser", "--- All IBrowser properties ---");
            try
            {
                foreach (var p in IBrowserType.GetProperties(pub))
                    Logger.Log("DotNetBrowser", $"  {p.Name} : {p.PropertyType.FullName}");
            }
            catch (Exception ex) { Logger.Log("DotNetBrowser", $"  Error enumerating: {ex.Message}"); }
        }

        // 4. All methods on IFrame
        if (IFrameType != null)
        {
            Logger.Log("DotNetBrowser", $"--- All IFrame methods ({IFrameType.FullName}) ---");
            try
            {
                foreach (var m in IFrameType.GetMethods(pub))
                {
                    var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                    var generic = m.IsGenericMethod ? $"<{m.GetGenericArguments().Length}>" : "";
                    Logger.Log("DotNetBrowser", $"  {m.Name}{generic}({parms}) -> {m.ReturnType.Name}");
                }
            }
            catch (Exception ex) { Logger.Log("DotNetBrowser", $"  Error enumerating: {ex.Message}"); }

            // 5. IFrame's parent interfaces and their methods
            Logger.Log("DotNetBrowser", "--- IFrame parent interfaces ---");
            try
            {
                foreach (var iface in IFrameType.GetInterfaces())
                {
                    Logger.Log("DotNetBrowser", $"  Interface: {iface.FullName}");
                    foreach (var m in iface.GetMethods(pub))
                    {
                        var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                        var generic = m.IsGenericMethod ? $"<{m.GetGenericArguments().Length}>" : "";
                        Logger.Log("DotNetBrowser", $"    {m.Name}{generic}({parms}) -> {m.ReturnType.Name}");
                    }
                }
            }
            catch (Exception ex) { Logger.Log("DotNetBrowser", $"  Error enumerating: {ex.Message}"); }
        }

        // 6. MainFrame property return type
        if (_browserMainFrame != null)
        {
            var retType = _browserMainFrame.PropertyType;
            Logger.Log("DotNetBrowser", $"--- MainFrame declared return type: {retType.FullName} ---");
            try
            {
                foreach (var m in retType.GetMethods(pub))
                {
                    var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                    var generic = m.IsGenericMethod ? $"<{m.GetGenericArguments().Length}>" : "";
                    Logger.Log("DotNetBrowser", $"  {m.Name}{generic}({parms}) -> {m.ReturnType.Name}");
                }
            }
            catch (Exception ex) { Logger.Log("DotNetBrowser", $"  Error enumerating: {ex.Message}"); }
        }

        Logger.Log("DotNetBrowser", "=== DIAGNOSTICS END ===");
    }

    /// <summary>
    /// Walk the Avalonia visual tree to find the first DotNetBrowser BrowserView control.
    /// Falls back to finding any control with a "Browser" property returning IBrowser.
    /// </summary>
    public object? FindBrowserView(AvaloniaReflection r, object mainWindow)
    {
        // Strategy 1: find by known BrowserView type
        if (BrowserViewType != null)
        {
            var found = FindInTree(r, mainWindow, t => BrowserViewType.IsAssignableFrom(t), 0);
            if (found != null) return found;
        }

        // Strategy 2: find any control with a "Browser" property returning IBrowser
        if (IBrowserType != null)
        {
            var found = FindInTree(r, mainWindow, t =>
            {
                var browserProp = t.GetProperty("Browser", BindingFlags.Public | BindingFlags.Instance);
                return browserProp != null && IBrowserType.IsAssignableFrom(browserProp.PropertyType);
            }, 0);

            if (found != null)
            {
                // Cache the BrowserView type and property for future lookups
                var foundType = found.GetType();
                if (BrowserViewType == null)
                {
                    BrowserViewType = foundType;
                    _browserViewBrowser = foundType.GetProperty("Browser", BindingFlags.Public | BindingFlags.Instance);
                    Logger.Log("DotNetBrowser", $"BrowserView discovered via visual tree: {foundType.FullName}");
                }
                return found;
            }
        }

        return null;
    }

    private object? FindInTree(AvaloniaReflection r, object visual, Func<Type, bool> predicate, int depth)
    {
        if (depth > 50) return null;

        if (predicate(visual.GetType()))
            return visual;

        foreach (var child in r.GetVisualChildren(visual))
        {
            var found = FindInTree(r, child, predicate, depth + 1);
            if (found != null) return found;
        }

        return null;
    }

    /// <summary>
    /// Get the IBrowser instance from a BrowserView control.
    /// </summary>
    public object? GetBrowser(object browserView)
    {
        // If we don't have a cached property, try to find it on the runtime type
        if (_browserViewBrowser == null)
        {
            var prop = browserView.GetType().GetProperty("Browser", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && (IBrowserType == null || IBrowserType.IsAssignableFrom(prop.PropertyType)))
            {
                _browserViewBrowser = prop;
                Logger.Log("DotNetBrowser", $"Browser property discovered on {browserView.GetType().FullName}");
            }
            else
            {
                return null;
            }
        }

        try { return _browserViewBrowser.GetValue(browserView); }
        catch (Exception ex)
        {
            Logger.Log("DotNetBrowser", $"GetBrowser error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Get the main IFrame from an IBrowser instance.
    /// </summary>
    public object? GetMainFrame(object browser)
    {
        if (_browserMainFrame == null) return null;
        try { return _browserMainFrame.GetValue(browser); }
        catch (Exception ex)
        {
            Logger.Log("DotNetBrowser", $"GetMainFrame error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Execute JavaScript in the given frame. Returns true if execution succeeded.
    /// Supports deferred resolution: if the method wasn't found at type-scan time,
    /// it will be resolved from the frame object's runtime type on first call.
    /// </summary>
    public bool ExecuteJavaScript(object frame, string script)
    {
        var method = _frameExecuteJavaScript;

        // Deferred resolution: try to find the method on the runtime type
        if (method == null)
            method = DeferredResolveExecJS(frame);

        if (method == null) return false;

        try
        {
            var result = method.Invoke(frame, new object[] { script });
            // If it returns a Task, we don't need to await it — fire-and-forget is fine
            return true;
        }
        catch (TargetInvocationException tie) when (tie.InnerException != null)
        {
            Logger.Log("DotNetBrowser", $"ExecuteJavaScript error: {tie.InnerException.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Logger.Log("DotNetBrowser", $"ExecuteJavaScript error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Check if DotNetBrowser assemblies (including AvaloniaUi) are loaded.
    /// We need at least DotNetBrowser + an AvaloniaUi assembly for BrowserView.
    /// </summary>
    public static bool AreDotNetBrowserAssembliesLoaded()
    {
        bool hasCore = false;
        bool hasUi = false;
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            var name = asm.GetName().Name ?? "";
            if (name.Equals("DotNetBrowser.Chromium", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("DotNetBrowser", StringComparison.OrdinalIgnoreCase))
                hasCore = true;
            if (name.StartsWith("DotNetBrowser.Avalonia", StringComparison.OrdinalIgnoreCase))
                hasUi = true;
        }
        return hasCore && hasUi;
    }
}
