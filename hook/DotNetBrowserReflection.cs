using System.Reflection;

namespace Uprooted;

/// <summary>
/// Cached reflection handles for DotNetBrowser types, properties, and methods.
/// Follows AvaloniaReflection patterns: assembly scanning, type caching, nullable returns.
/// Used by NsfwFilter to execute JavaScript in browser frames.
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

    // Cached engine for retry-based browser discovery
    private object? _cachedEngine;
    private Type? _cachedEngineType;

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

        // Strategy 4: Search for extension methods in DotNetBrowser assemblies
        result = SearchExtensionMethods();
        if (result != null) return result;

        return null;
    }

    /// <summary>
    /// Search all DotNetBrowser assemblies for static extension methods targeting IFrame.
    /// Extension methods are static methods where the first parameter is IFrame (or compatible).
    /// </summary>
    private MethodInfo? SearchExtensionMethods()
    {
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase)) continue;

                foreach (var type in asm.GetTypes())
                {
                    if (!type.IsAbstract || !type.IsSealed) continue; // static classes only

                    var staticMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
                    foreach (var m in staticMethods)
                    {
                        if (!m.Name.Contains("JavaScript", StringComparison.OrdinalIgnoreCase) &&
                            !m.Name.Contains("Execute", StringComparison.OrdinalIgnoreCase) &&
                            !m.Name.Contains("Script", StringComparison.OrdinalIgnoreCase))
                            continue;

                        var parms = m.GetParameters();
                        if (parms.Length < 1) continue;

                        // First param should be IFrame-compatible (extension method target)
                        var firstParamType = parms[0].ParameterType;
                        if (IFrameType != null && !firstParamType.IsAssignableFrom(IFrameType) &&
                            !IFrameType.IsAssignableFrom(firstParamType))
                            continue;

                        // Second param should accept string (the script)
                        if (parms.Length >= 2)
                        {
                            var secondParamType = parms[1].ParameterType;
                            if (secondParamType != typeof(string) && !secondParamType.IsGenericParameter)
                                continue;
                        }

                        var parmsStr = string.Join(", ", parms.Select(p => $"{p.ParameterType.Name} {p.Name}"));
                        Logger.Log("DotNetBrowser", $"  Extension method candidate: {type.FullName}.{m.Name}({parmsStr})");

                        // For extension methods, we'll need special invocation — store as-is
                        if (m.IsGenericMethod)
                        {
                            try
                            {
                                var concrete = m.MakeGenericMethod(typeof(object));
                                Logger.Log("DotNetBrowser", $"  Resolved generic extension {m.Name}<object>");
                                return concrete;
                            }
                            catch { }
                        }
                        return m;
                    }
                }
            }
            catch { }
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

        // Multi-param: ExecuteJavaScript(string, ...) with additional params
        found = allMethods.FirstOrDefault(m =>
            m.Name == "ExecuteJavaScript" && !m.IsGenericMethod &&
            m.GetParameters().Length >= 2 &&
            m.GetParameters()[0].ParameterType == typeof(string));
        if (found != null) return found;

        // Generic multi-param: ExecuteJavaScript<T>(string, ...) — first param may be generic
        found = allMethods.FirstOrDefault(m =>
            m.Name == "ExecuteJavaScript" && m.IsGenericMethod &&
            m.GetParameters().Length >= 2 &&
            (m.GetParameters()[0].ParameterType == typeof(string) || m.GetParameters()[0].ParameterType.IsGenericParameter));
        if (found != null)
        {
            try
            {
                var concrete = found.MakeGenericMethod(typeof(object));
                Logger.Log("DotNetBrowser", $"  Resolved generic multi-param ExecuteJavaScript<object> on {logLabel}");
                return concrete;
            }
            catch { }
        }

        // Broader: ExecuteJavaScriptAndReturnValue (older API)
        found = allMethods.FirstOrDefault(m =>
            m.Name.Contains("ExecuteJavaScript", StringComparison.Ordinal) &&
            m.GetParameters().Length >= 1 &&
            (m.GetParameters()[0].ParameterType == typeof(string) || m.GetParameters()[0].ParameterType.IsGenericParameter));
        if (found != null) return found;

        // Broader name patterns: Evaluate, RunScript
        foreach (var name in new[] { "EvaluateJavaScript", "RunJavaScript", "EvalScript", "RunScript" })
        {
            found = allMethods.FirstOrDefault(m =>
                m.Name == name &&
                m.GetParameters().Length >= 1 &&
                (m.GetParameters()[0].ParameterType == typeof(string) || m.GetParameters()[0].ParameterType.IsGenericParameter));
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

        // 7. Static classes with static methods in DotNetBrowser assemblies (extension methods)
        Logger.Log("DotNetBrowser", "--- DotNetBrowser static classes and methods (extension method candidates) ---");
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase)) continue;

                foreach (var type in asm.GetTypes())
                {
                    if (!type.IsAbstract || !type.IsSealed) continue; // static classes only
                    var staticMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
                    if (staticMethods.Length == 0) continue;

                    Logger.Log("DotNetBrowser", $"  Static class: {type.FullName}");
                    foreach (var m in staticMethods)
                    {
                        var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                        var generic = m.IsGenericMethod ? $"<{m.GetGenericArguments().Length}>" : "";
                        Logger.Log("DotNetBrowser", $"    {m.Name}{generic}({parms}) -> {m.ReturnType.Name}");
                    }
                }
            }
            catch { }
        }

        // 8. Assembly scan summary: type counts per DotNetBrowser assembly
        Logger.Log("DotNetBrowser", "--- Assembly scan summary ---");
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase)) continue;
                var typeCount = asm.GetTypes().Length;
                Logger.Log("DotNetBrowser", $"  {asmName}: {typeCount} types");
            }
            catch { }
        }

        // 9. Non-DotNetBrowser assemblies that reference DotNetBrowser types (IBrowser holders)
        Logger.Log("DotNetBrowser", "--- Root assemblies referencing DotNetBrowser ---");
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";
                if (asmName.StartsWith("System", StringComparison.OrdinalIgnoreCase) ||
                    asmName.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase) ||
                    asmName.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase) ||
                    asmName.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase))
                    continue;

                var refs = asm.GetReferencedAssemblies();
                foreach (var r in refs)
                {
                    if ((r.Name ?? "").StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase))
                    {
                        Logger.Log("DotNetBrowser", $"  {asmName} references {r.Name}");
                        // List types in this assembly that have DotNetBrowser-typed members
                        foreach (var type in asm.GetTypes())
                        {
                            try
                            {
                                var allFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                                foreach (var field in type.GetFields(allFlags))
                                {
                                    if ((field.FieldType.FullName ?? "").StartsWith("DotNetBrowser"))
                                        Logger.Log("DotNetBrowser", $"    {type.Name}.{field.Name} : {field.FieldType.Name} (field)");
                                }
                                foreach (var prop in type.GetProperties(allFlags))
                                {
                                    if ((prop.PropertyType.FullName ?? "").StartsWith("DotNetBrowser"))
                                        Logger.Log("DotNetBrowser", $"    {type.Name}.{prop.Name} : {prop.PropertyType.Name} (property)");
                                }
                            }
                            catch { }
                        }
                        break;
                    }
                }
            }
            catch { }
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

    /// <summary>
    /// Find an IBrowser instance without relying on BrowserView in the visual tree.
    /// Strategies (in order):
    /// 1. DI container: find IServiceProvider on Application.Current, resolve IEngine/IBrowser
    /// 2. Application instance fields: search Root's Application subclass for browser references
    /// 3. Static field scan: search all assemblies for static IBrowser/IEngine holders
    /// </summary>
    public object? FindBrowserDirect()
    {
        if (IBrowserType == null)
        {
            Logger.Log("DotNetBrowser", "FindBrowserDirect: IBrowserType not resolved, cannot search");
            return null;
        }

        // Fast path: if we already cached an IEngine from a previous call, just retry getting browsers
        if (_cachedEngine != null && _cachedEngineType != null)
        {
            var cachedResult = GetBrowserFromEngine(_cachedEngine, _cachedEngineType);
            if (cachedResult != null)
            {
                Logger.Log("DotNetBrowser", "FindBrowserDirect: IBrowser found via cached IEngine (retry)");
                return cachedResult;
            }
            // Engine exists but still no browsers — don't re-scan everything, just return null
            // The re-injection timer will try again
            return null;
        }

        var pub = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        // Find IEngine type for engine-based discovery
        Type? engineType = null;
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase)) continue;
                foreach (var type in asm.GetTypes())
                {
                    if (type.FullName == "DotNetBrowser.Engine.IEngine" || type.Name == "IEngine")
                    {
                        engineType = type;
                        break;
                    }
                }
                if (engineType != null) break;
            }
            catch { }
        }
        Logger.Log("DotNetBrowser", $"FindBrowserDirect: IEngine type = {(engineType != null ? engineType.FullName : "NOT FOUND")}");

        // Strategy 1: Splat service locator (Root uses ReactiveUI/Splat)
        var browser = FindBrowserViaSplat(engineType);
        if (browser != null) return browser;

        // Strategy 2: ViewModel chain — MainWindow.DataContext and its fields
        browser = FindBrowserViaViewModel(engineType);
        if (browser != null) return browser;

        // Strategy 3: DI container — find IServiceProvider from Application.Current
        browser = FindBrowserViaDI(engineType);
        if (browser != null) return browser;

        // Strategy 4: Walk Application.Current instance fields for DotNetBrowser references
        browser = FindBrowserViaAppInstance(engineType);
        if (browser != null) return browser;

        // Strategy 5: Static field scan across all assemblies
        browser = FindBrowserViaStaticScan(engineType, pub);
        if (browser != null) return browser;

        Logger.Log("DotNetBrowser", "FindBrowserDirect: no IBrowser instance found via any strategy");
        return null;
    }

    /// <summary>
    /// Try Splat's service locator — Root uses ReactiveUI which relies on Splat.
    /// Splat.Locator.Current is a static IReadonlyDependencyResolver with GetService(Type).
    /// </summary>
    private object? FindBrowserViaSplat(Type? engineType)
    {
        try
        {
            // Find Splat.Locator type
            Type? locatorType = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    if ((asm.GetName().Name ?? "").Equals("Splat", StringComparison.OrdinalIgnoreCase))
                    {
                        locatorType = asm.GetType("Splat.Locator");
                        break;
                    }
                }
                catch { }
            }

            if (locatorType == null)
            {
                Logger.Log("DotNetBrowser", "FindBrowserDirect/Splat: Splat.Locator not found");
                return null;
            }

            var currentProp = locatorType.GetProperty("Current", BindingFlags.Public | BindingFlags.Static);
            var resolver = currentProp?.GetValue(null);
            if (resolver == null)
            {
                Logger.Log("DotNetBrowser", "FindBrowserDirect/Splat: Locator.Current is null");
                return null;
            }

            Logger.Log("DotNetBrowser", $"FindBrowserDirect/Splat: resolver type = {resolver.GetType().FullName}");

            // GetService(Type serviceType, string? contract = null) on IReadonlyDependencyResolver
            var getServiceMethod = resolver.GetType().GetMethod("GetService",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(Type), typeof(string) }, null);

            // Try simpler overload: GetService(Type)
            getServiceMethod ??= resolver.GetType().GetMethod("GetService",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(Type) }, null);

            if (getServiceMethod == null)
            {
                Logger.Log("DotNetBrowser", "FindBrowserDirect/Splat: GetService method not found on resolver");
                return null;
            }

            var paramCount = getServiceMethod.GetParameters().Length;

            // Try resolving IBrowser
            try
            {
                var args = paramCount == 2 ? new object?[] { IBrowserType!, null } : new object[] { IBrowserType! };
                var resolved = getServiceMethod.Invoke(resolver, args);
                if (resolved != null)
                {
                    Logger.Log("DotNetBrowser", "FindBrowserDirect/Splat: IBrowser resolved");
                    return resolved;
                }
            }
            catch { }

            // Try resolving IEngine
            if (engineType != null)
            {
                try
                {
                    var args = paramCount == 2 ? new object?[] { engineType, null } : new object[] { engineType };
                    var engine = getServiceMethod.Invoke(resolver, args);
                    if (engine != null)
                    {
                        Logger.Log("DotNetBrowser", "FindBrowserDirect/Splat: IEngine resolved");
                        var browserFromEngine = GetBrowserFromEngine(engine, engineType);
                        if (browserFromEngine != null) return browserFromEngine;
                    }
                }
                catch { }
            }

            Logger.Log("DotNetBrowser", "FindBrowserDirect/Splat: no DotNetBrowser services registered");
        }
        catch (Exception ex)
        {
            Logger.Log("DotNetBrowser", $"FindBrowserDirect/Splat error: {ex.Message}");
        }
        return null;
    }

    /// <summary>
    /// Search the MainWindow.DataContext ViewModel hierarchy for DotNetBrowser references.
    /// Root uses MVVM — the ViewModel may hold browser service references.
    /// </summary>
    private object? FindBrowserViaViewModel(Type? engineType)
    {
        try
        {
            // Get Application.Current → MainWindow → DataContext
            var appType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
                .FirstOrDefault(t => t.FullName == "Avalonia.Application");
            if (appType == null) return null;

            var app = appType.GetProperty("Current", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
            if (app == null) return null;

            // Get MainWindow via desktop lifetime
            object? mainWindow = null;
            var lifetimeProp = appType.GetProperty("ApplicationLifetime", BindingFlags.Public | BindingFlags.Instance);
            var lifetime = lifetimeProp?.GetValue(app);
            if (lifetime != null)
            {
                var mwProp = lifetime.GetType().GetProperty("MainWindow", BindingFlags.Public | BindingFlags.Instance);
                mainWindow = mwProp?.GetValue(lifetime);
            }
            if (mainWindow == null) return null;

            // Get DataContext
            var dcProp = mainWindow.GetType().GetProperty("DataContext", BindingFlags.Public | BindingFlags.Instance);
            var dataContext = dcProp?.GetValue(mainWindow);
            if (dataContext == null)
            {
                Logger.Log("DotNetBrowser", "FindBrowserDirect/ViewModel: MainWindow.DataContext is null");
                return null;
            }

            Logger.Log("DotNetBrowser", $"FindBrowserDirect/ViewModel: DataContext type = {dataContext.GetType().FullName}");

            // Search the ViewModel and its fields recursively (depth-limited)
            var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
            var result = DeepSearchForBrowser(dataContext, engineType, visited, 0, maxDepth: 4);
            if (result != null) return result;

            Logger.Log("DotNetBrowser", "FindBrowserDirect/ViewModel: no IBrowser found in ViewModel chain");
        }
        catch (Exception ex)
        {
            Logger.Log("DotNetBrowser", $"FindBrowserDirect/ViewModel error: {ex.Message}");
        }
        return null;
    }

    /// <summary>
    /// Recursively search an object's fields for IBrowser/IEngine references.
    /// Depth-limited to avoid infinite loops and excessive scanning.
    /// </summary>
    private object? DeepSearchForBrowser(object obj, Type? engineType, HashSet<object> visited, int depth, int maxDepth)
    {
        if (depth > maxDepth || !visited.Add(obj)) return null;

        var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        // Check direct IBrowser fields/properties
        var direct = SearchInstanceForBrowser(obj, engineType);
        if (direct != null) return direct;

        // Recurse into fields that might hold services with DotNetBrowser references
        foreach (var field in obj.GetType().GetFields(flags))
        {
            try
            {
                var fieldType = field.FieldType;
                // Skip primitives, strings, value types, collections of primitives
                if (fieldType.IsPrimitive || fieldType == typeof(string) || fieldType.IsEnum) continue;
                if (fieldType.Namespace?.StartsWith("System") == true && !fieldType.Namespace.StartsWith("System.Collections")) continue;

                var val = field.GetValue(obj);
                if (val == null) continue;

                // Only recurse into Root-owned types or types that reference DotNetBrowser
                var valAsmName = val.GetType().Assembly.GetName().Name ?? "";
                if (!valAsmName.StartsWith("Root", StringComparison.OrdinalIgnoreCase) &&
                    !valAsmName.StartsWith("RootApp", StringComparison.OrdinalIgnoreCase))
                    continue;

                var result = DeepSearchForBrowser(val, engineType, visited, depth + 1, maxDepth);
                if (result != null)
                {
                    Logger.Log("DotNetBrowser", $"FindBrowserDirect/Deep: found at depth {depth + 1} via {obj.GetType().Name}.{field.Name}");
                    return result;
                }
            }
            catch { }
        }

        return null;
    }

    private object? FindBrowserViaDI(Type? engineType)
    {
        try
        {
            // Get Application.Current
            var appType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
                .FirstOrDefault(t => t.FullName == "Avalonia.Application");
            if (appType == null) return null;

            var currentProp = appType.GetProperty("Current", BindingFlags.Public | BindingFlags.Static);
            var app = currentProp?.GetValue(null);
            if (app == null) return null;

            Logger.Log("DotNetBrowser", $"FindBrowserDirect/DI: Application.Current = {app.GetType().FullName}");

            // Search app's type hierarchy for IServiceProvider access
            var instanceFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            object? serviceProvider = null;

            // Check properties on the concrete app type and all base types
            for (var t = app.GetType(); t != null && t != typeof(object); t = t.BaseType)
            {
                foreach (var prop in t.GetProperties(instanceFlags | BindingFlags.DeclaredOnly))
                {
                    try
                    {
                        if (typeof(IServiceProvider).IsAssignableFrom(prop.PropertyType))
                        {
                            serviceProvider = prop.GetValue(app);
                            if (serviceProvider != null)
                            {
                                Logger.Log("DotNetBrowser", $"FindBrowserDirect/DI: IServiceProvider found on {t.Name}.{prop.Name}");
                                break;
                            }
                        }
                        // Also check for IHost which has a .Services property
                        if (prop.PropertyType.Name == "IHost" || prop.PropertyType.Name == "Host")
                        {
                            var host = prop.GetValue(app);
                            if (host != null)
                            {
                                var servicesProp = host.GetType().GetProperty("Services", BindingFlags.Public | BindingFlags.Instance);
                                serviceProvider = servicesProp?.GetValue(host) as IServiceProvider;
                                if (serviceProvider != null)
                                {
                                    Logger.Log("DotNetBrowser", $"FindBrowserDirect/DI: IServiceProvider found via {t.Name}.{prop.Name}.Services");
                                    break;
                                }
                            }
                        }
                    }
                    catch { }
                }
                if (serviceProvider != null) break;

                // Also check fields
                foreach (var field in t.GetFields(instanceFlags | BindingFlags.DeclaredOnly))
                {
                    try
                    {
                        if (typeof(IServiceProvider).IsAssignableFrom(field.FieldType))
                        {
                            serviceProvider = field.GetValue(app);
                            if (serviceProvider != null)
                            {
                                Logger.Log("DotNetBrowser", $"FindBrowserDirect/DI: IServiceProvider found on {t.Name}.{field.Name}");
                                break;
                            }
                        }
                    }
                    catch { }
                }
                if (serviceProvider != null) break;
            }

            if (serviceProvider == null)
            {
                Logger.Log("DotNetBrowser", "FindBrowserDirect/DI: no IServiceProvider found on Application");
                return null;
            }

            // Try resolving IBrowser directly
            try
            {
                var resolved = serviceProvider.GetType().GetMethod("GetService")?.Invoke(serviceProvider, new object[] { IBrowserType! });
                if (resolved != null)
                {
                    Logger.Log("DotNetBrowser", $"FindBrowserDirect/DI: IBrowser resolved from DI");
                    return resolved;
                }
            }
            catch { }

            // Try resolving IEngine, then get browsers from it
            if (engineType != null)
            {
                try
                {
                    var engine = serviceProvider.GetType().GetMethod("GetService")?.Invoke(serviceProvider, new object[] { engineType });
                    if (engine != null)
                    {
                        Logger.Log("DotNetBrowser", $"FindBrowserDirect/DI: IEngine resolved from DI");
                        var browserFromEngine = GetBrowserFromEngine(engine, engineType);
                        if (browserFromEngine != null) return browserFromEngine;
                    }
                }
                catch { }
            }

            // Try resolving all registered services that might hold a browser
            // Search for service types from Root assemblies that reference DotNetBrowser
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var asmName = asm.GetName().Name ?? "";
                    if (!asmName.StartsWith("Root", StringComparison.OrdinalIgnoreCase)) continue;

                    foreach (var type in asm.GetTypes())
                    {
                        if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition) continue;

                        // Check if this type has DotNetBrowser-typed members
                        bool hasBrowserMembers = false;
                        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            if ((field.FieldType.FullName ?? "").StartsWith("DotNetBrowser"))
                            { hasBrowserMembers = true; break; }
                        }
                        if (!hasBrowserMembers)
                        {
                            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                            {
                                if ((prop.PropertyType.FullName ?? "").StartsWith("DotNetBrowser"))
                                { hasBrowserMembers = true; break; }
                            }
                        }
                        if (!hasBrowserMembers) continue;

                        // Try resolving this type from DI
                        try
                        {
                            var service = serviceProvider.GetType().GetMethod("GetService")?.Invoke(serviceProvider, new object[] { type });
                            if (service != null)
                            {
                                Logger.Log("DotNetBrowser", $"FindBrowserDirect/DI: resolved {type.Name} from DI, searching for IBrowser");
                                var result = SearchInstanceForBrowser(service, engineType);
                                if (result != null) return result;
                            }
                        }
                        catch { }

                        // Also try resolving by interface types this class implements
                        foreach (var iface in type.GetInterfaces())
                        {
                            try
                            {
                                var service = serviceProvider.GetType().GetMethod("GetService")?.Invoke(serviceProvider, new object[] { iface });
                                if (service != null && service.GetType() == type)
                                {
                                    Logger.Log("DotNetBrowser", $"FindBrowserDirect/DI: resolved {iface.Name} from DI ({type.Name}), searching for IBrowser");
                                    var result = SearchInstanceForBrowser(service, engineType);
                                    if (result != null) return result;
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch { }
            }

            Logger.Log("DotNetBrowser", "FindBrowserDirect/DI: no IBrowser found via DI resolution");
        }
        catch (Exception ex)
        {
            Logger.Log("DotNetBrowser", $"FindBrowserDirect/DI error: {ex.Message}");
        }
        return null;
    }

    /// <summary>
    /// Search an object instance's fields and properties for IBrowser or IEngine references.
    /// </summary>
    private object? SearchInstanceForBrowser(object instance, Type? engineType)
    {
        var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        var instType = instance.GetType();

        // Direct IBrowser properties/fields
        foreach (var prop in instType.GetProperties(flags))
        {
            try
            {
                if (IBrowserType!.IsAssignableFrom(prop.PropertyType))
                {
                    var val = prop.GetValue(instance);
                    if (val != null)
                    {
                        Logger.Log("DotNetBrowser", $"FindBrowserDirect: IBrowser from {instType.Name}.{prop.Name}");
                        return val;
                    }
                }
            }
            catch { }
        }
        foreach (var field in instType.GetFields(flags))
        {
            try
            {
                if (IBrowserType!.IsAssignableFrom(field.FieldType))
                {
                    var val = field.GetValue(instance);
                    if (val != null)
                    {
                        Logger.Log("DotNetBrowser", $"FindBrowserDirect: IBrowser from {instType.Name}.{field.Name}");
                        return val;
                    }
                }
            }
            catch { }
        }

        // IEngine properties/fields → get browsers from engine
        if (engineType != null)
        {
            foreach (var prop in instType.GetProperties(flags))
            {
                try
                {
                    if (engineType.IsAssignableFrom(prop.PropertyType))
                    {
                        var engine = prop.GetValue(instance);
                        if (engine != null)
                        {
                            Logger.Log("DotNetBrowser", $"FindBrowserDirect: IEngine from {instType.Name}.{prop.Name}");
                            var result = GetBrowserFromEngine(engine, engineType);
                            if (result != null) return result;
                        }
                    }
                }
                catch { }
            }
            foreach (var field in instType.GetFields(flags))
            {
                try
                {
                    if (engineType.IsAssignableFrom(field.FieldType))
                    {
                        var engine = field.GetValue(instance);
                        if (engine != null)
                        {
                            Logger.Log("DotNetBrowser", $"FindBrowserDirect: IEngine from {instType.Name}.{field.Name}");
                            var result = GetBrowserFromEngine(engine, engineType);
                            if (result != null) return result;
                        }
                    }
                }
                catch { }
            }
        }

        return null;
    }

    private object? FindBrowserViaAppInstance(Type? engineType)
    {
        try
        {
            var appType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
                .FirstOrDefault(t => t.FullName == "Avalonia.Application");
            if (appType == null) return null;

            var currentProp = appType.GetProperty("Current", BindingFlags.Public | BindingFlags.Static);
            var app = currentProp?.GetValue(null);
            if (app == null) return null;

            // Walk all instance fields/properties on the Application subclass
            var result = SearchInstanceForBrowser(app, engineType);
            if (result != null)
            {
                Logger.Log("DotNetBrowser", "FindBrowserDirect/AppInstance: found via Application.Current fields");
                return result;
            }
        }
        catch (Exception ex)
        {
            Logger.Log("DotNetBrowser", $"FindBrowserDirect/AppInstance error: {ex.Message}");
        }
        return null;
    }

    private object? FindBrowserViaStaticScan(Type? engineType, BindingFlags pub)
    {
        // Scan non-system, non-DotNetBrowser assemblies for static IBrowser/IEngine holders
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";
                if (asmName.StartsWith("System", StringComparison.OrdinalIgnoreCase) ||
                    asmName.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase) ||
                    asmName.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase) ||
                    asmName.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase) ||
                    asmName.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase))
                    continue;

                foreach (var type in asm.GetTypes())
                {
                    try
                    {
                        var browser = SearchTypeForBrowser(type, pub, isStatic: true);
                        if (browser != null)
                        {
                            Logger.Log("DotNetBrowser", $"FindBrowserDirect/Static: IBrowser found on {type.FullName}");
                            return browser;
                        }

                        if (engineType != null)
                        {
                            var engine = SearchTypeForEngine(type, pub, engineType, isStatic: true);
                            if (engine != null)
                            {
                                var browserFromEngine = GetBrowserFromEngine(engine, engineType);
                                if (browserFromEngine != null)
                                {
                                    Logger.Log("DotNetBrowser", $"FindBrowserDirect/Static: IBrowser via IEngine on {type.FullName}");
                                    return browserFromEngine;
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        // Also scan DotNetBrowser assemblies for singleton/static engine references
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase)) continue;

                foreach (var type in asm.GetTypes())
                {
                    try
                    {
                        var browser = SearchTypeForBrowser(type, pub, isStatic: true);
                        if (browser != null)
                        {
                            Logger.Log("DotNetBrowser", $"FindBrowserDirect/Static: IBrowser via DotNetBrowser on {type.FullName}");
                            return browser;
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        return null;
    }

    private object? SearchTypeForBrowser(Type type, BindingFlags flags, bool isStatic)
    {
        var staticFlag = isStatic ? BindingFlags.Static : BindingFlags.Instance;
        var searchFlags = (flags & ~(BindingFlags.Static | BindingFlags.Instance)) | staticFlag;

        // Search properties
        foreach (var prop in type.GetProperties(searchFlags))
        {
            try
            {
                if (IBrowserType!.IsAssignableFrom(prop.PropertyType))
                {
                    var val = prop.GetValue(isStatic ? null : type);
                    if (val != null) return val;
                }
            }
            catch { }
        }

        // Search fields
        foreach (var field in type.GetFields(searchFlags))
        {
            try
            {
                if (IBrowserType!.IsAssignableFrom(field.FieldType))
                {
                    var val = field.GetValue(isStatic ? null : type);
                    if (val != null) return val;
                }
            }
            catch { }
        }

        return null;
    }

    private static object? SearchTypeForEngine(Type type, BindingFlags flags, Type engineType, bool isStatic)
    {
        var staticFlag = isStatic ? BindingFlags.Static : BindingFlags.Instance;
        var searchFlags = (flags & ~(BindingFlags.Static | BindingFlags.Instance)) | staticFlag;

        foreach (var prop in type.GetProperties(searchFlags))
        {
            try
            {
                if (engineType.IsAssignableFrom(prop.PropertyType))
                {
                    var val = prop.GetValue(isStatic ? null : type);
                    if (val != null) return val;
                }
            }
            catch { }
        }

        foreach (var field in type.GetFields(searchFlags))
        {
            try
            {
                if (engineType.IsAssignableFrom(field.FieldType))
                {
                    var val = field.GetValue(isStatic ? null : type);
                    if (val != null) return val;
                }
            }
            catch { }
        }

        return null;
    }

    private object? GetBrowserFromEngine(object engine, Type engineType)
    {
        // Cache engine for retry on subsequent calls
        _cachedEngine = engine;
        _cachedEngineType = engineType;

        var pub = BindingFlags.Public | BindingFlags.Instance;

        // DotNetBrowser 3.x hierarchy: IEngine → IProfiles → IProfile → IBrowser
        // Get Profiles from engine
        var profilesProp = engineType.GetProperty("Profiles", pub);
        if (profilesProp == null)
        {
            Logger.Log("DotNetBrowser", "GetBrowserFromEngine: IEngine.Profiles property not found");
            return null;
        }

        var profiles = profilesProp.GetValue(engine);
        if (profiles == null)
        {
            Logger.Log("DotNetBrowser", "GetBrowserFromEngine: IEngine.Profiles is null");
            return null;
        }

        // Enumerate profiles (IProfiles implements IEnumerable<IProfile>)
        if (profiles is not System.Collections.IEnumerable profileEnum)
        {
            Logger.Log("DotNetBrowser", $"GetBrowserFromEngine: Profiles is not IEnumerable ({profiles.GetType().Name})");
            return null;
        }

        int profileIndex = 0;
        foreach (var profile in profileEnum)
        {
            if (profile == null) { profileIndex++; continue; }
            var profileType = profile.GetType();
            Logger.Log("DotNetBrowser", $"GetBrowserFromEngine: Profile[{profileIndex}] type={profileType.Name}");

            // Look for Browsers() method or Browsers property on the profile
            // DotNetBrowser 3.x: IProfile has Browsers() method returning IEnumerable<IBrowser>
            // or a Browsers property

            // Try property first
            var browsersProp = profileType.GetProperty("Browsers", pub);
            object? browsers = null;

            if (browsersProp != null)
            {
                try { browsers = browsersProp.GetValue(profile); }
                catch { }
            }

            // Try method: Browsers()
            if (browsers == null)
            {
                var browsersMethod = profileType.GetMethod("Browsers", pub, null, Type.EmptyTypes, null);
                if (browsersMethod != null)
                {
                    try { browsers = browsersMethod.Invoke(profile, null); }
                    catch { }
                }
            }

            // Also check all interfaces for Browsers
            if (browsers == null)
            {
                foreach (var iface in profileType.GetInterfaces())
                {
                    browsersProp = iface.GetProperty("Browsers", pub);
                    if (browsersProp != null)
                    {
                        try { browsers = browsersProp.GetValue(profile); break; }
                        catch { }
                    }
                    var browsersMethod = iface.GetMethod("Browsers", pub, null, Type.EmptyTypes, null);
                    if (browsersMethod != null)
                    {
                        try { browsers = browsersMethod.Invoke(profile, null); break; }
                        catch { }
                    }
                }
            }

            if (browsers == null)
            {
                // Log all properties/methods on profile to help diagnose
                Logger.Log("DotNetBrowser", $"  Profile[{profileIndex}]: no Browsers found. Properties:");
                foreach (var p in profileType.GetProperties(pub))
                    Logger.Log("DotNetBrowser", $"    {p.Name} : {p.PropertyType.Name}");
                foreach (var iface in profileType.GetInterfaces())
                {
                    foreach (var p in iface.GetProperties(pub))
                        Logger.Log("DotNetBrowser", $"    ({iface.Name}).{p.Name} : {p.PropertyType.Name}");
                }
                Logger.Log("DotNetBrowser", $"  Profile[{profileIndex}]: Methods:");
                foreach (var m in profileType.GetMethods(pub))
                {
                    if (m.DeclaringType == typeof(object)) continue;
                    var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                    Logger.Log("DotNetBrowser", $"    {m.Name}({parms}) -> {m.ReturnType.Name}");
                }
                profileIndex++;
                continue;
            }

            var browsersRuntimeType = browsers.GetType();
            Logger.Log("DotNetBrowser", $"  Profile[{profileIndex}]: Browsers collection type = {browsersRuntimeType.Name}");

            // Log interfaces on the Browsers collection to understand its API
            foreach (var iface in browsersRuntimeType.GetInterfaces())
            {
                if (iface.FullName != null && !iface.FullName.StartsWith("System.Runtime"))
                    Logger.Log("DotNetBrowser", $"  Browsers implements: {iface.FullName}");
            }

            // Log properties on the Browsers collection
            foreach (var p in browsersRuntimeType.GetProperties(pub))
                Logger.Log("DotNetBrowser", $"  Browsers.{p.Name} : {p.PropertyType.Name}");

            // Try multiple enumeration strategies

            // Strategy A: non-generic IEnumerable
            if (browsers is System.Collections.IEnumerable browserEnum)
            {
                int browserCount = 0;
                try
                {
                    foreach (var browser in browserEnum)
                    {
                        browserCount++;
                        if (browser != null && IBrowserType!.IsAssignableFrom(browser.GetType()))
                        {
                            Logger.Log("DotNetBrowser", $"  IBrowser found: Profile[{profileIndex}].Browsers[{browserCount - 1}]");
                            return browser;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("DotNetBrowser", $"  Enumeration threw: {ex.Message}");
                }
                Logger.Log("DotNetBrowser", $"  Profile[{profileIndex}]: {browserCount} browser(s) via IEnumerable");
            }

            // Strategy B: GetEnumerator() via reflection (for generic IEnumerable<T>)
            var getEnumMethod = browsersRuntimeType.GetMethod("GetEnumerator", pub);
            if (getEnumMethod != null)
            {
                try
                {
                    var enumerator = getEnumMethod.Invoke(browsers, null);
                    if (enumerator != null)
                    {
                        var moveNext = enumerator.GetType().GetMethod("MoveNext", pub);
                        var currentProp = enumerator.GetType().GetProperty("Current", pub);
                        if (moveNext != null && currentProp != null)
                        {
                            int count = 0;
                            while ((bool)moveNext.Invoke(enumerator, null)!)
                            {
                                var item = currentProp.GetValue(enumerator);
                                count++;
                                if (item != null && IBrowserType!.IsAssignableFrom(item.GetType()))
                                {
                                    Logger.Log("DotNetBrowser", $"  IBrowser found via GetEnumerator: Profile[{profileIndex}][{count - 1}]");
                                    return item;
                                }
                                Logger.Log("DotNetBrowser", $"  Profile[{profileIndex}] item[{count - 1}] = {item?.GetType().Name ?? "null"}");
                            }
                            Logger.Log("DotNetBrowser", $"  Profile[{profileIndex}]: {count} item(s) via GetEnumerator");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("DotNetBrowser", $"  GetEnumerator threw: {ex.Message}");
                }
            }

            // Strategy C: Values/Items/List property
            foreach (var propName in new[] { "Values", "Items", "List", "All" })
            {
                var valProp = browsersRuntimeType.GetProperty(propName, pub);
                if (valProp == null) continue;
                try
                {
                    var val = valProp.GetValue(browsers);
                    if (val is System.Collections.IEnumerable valEnum)
                    {
                        int count = 0;
                        foreach (var item in valEnum)
                        {
                            count++;
                            if (item != null && IBrowserType!.IsAssignableFrom(item.GetType()))
                            {
                                Logger.Log("DotNetBrowser", $"  IBrowser found via .{propName}: Profile[{profileIndex}][{count - 1}]");
                                return item;
                            }
                        }
                        Logger.Log("DotNetBrowser", $"  Profile[{profileIndex}].Browsers.{propName}: {count} items");
                    }
                }
                catch { }
            }

            // Strategy D: Count + indexer
            var countProp = browsersRuntimeType.GetProperty("Count", pub);
            if (countProp != null)
            {
                try
                {
                    var countVal = countProp.GetValue(browsers);
                    Logger.Log("DotNetBrowser", $"  Profile[{profileIndex}].Browsers.Count = {countVal}");
                    var indexer = browsersRuntimeType.GetProperty("Item", pub);
                    if (indexer != null && countVal is int c)
                    {
                        for (int i = 0; i < c; i++)
                        {
                            try
                            {
                                var item = indexer.GetValue(browsers, new object[] { i });
                                if (item != null && IBrowserType!.IsAssignableFrom(item.GetType()))
                                {
                                    Logger.Log("DotNetBrowser", $"  IBrowser found via indexer: Profile[{profileIndex}][{i}]");
                                    return item;
                                }
                            }
                            catch { break; }
                        }
                    }
                }
                catch { }
            }

            // Strategy E: Dig into private fields of Repository to find backing dictionary
            var privFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            foreach (var field in browsersRuntimeType.GetFields(privFlags))
            {
                try
                {
                    var fVal = field.GetValue(browsers);
                    if (fVal == null) continue;
                    Logger.Log("DotNetBrowser", $"  Browsers.__{field.Name} : {field.FieldType.Name} = {fVal.GetType().Name}");

                    // If it's a dictionary, enumerate values
                    if (fVal is System.Collections.IEnumerable dictEnum)
                    {
                        int dCount = 0;
                        foreach (var entry in dictEnum)
                        {
                            dCount++;
                            if (entry == null) continue;
                            // For KeyValuePair, get Value
                            var valueProp = entry.GetType().GetProperty("Value");
                            var dictVal = valueProp != null ? valueProp.GetValue(entry) : entry;
                            if (dictVal != null && IBrowserType!.IsAssignableFrom(dictVal.GetType()))
                            {
                                Logger.Log("DotNetBrowser", $"  IBrowser found in Browsers.__{field.Name} dict");
                                return dictVal;
                            }
                        }
                        if (dCount > 0)
                            Logger.Log("DotNetBrowser", $"  Browsers.__{field.Name}: {dCount} entries");
                    }
                }
                catch { }
            }

            // Also check what the IProfile interface actually exposes (first time only)
            if (profileIndex == 0)
            {
                Logger.Log("DotNetBrowser", $"  IProfile interfaces and their members:");
                foreach (var iface in profileType.GetInterfaces())
                {
                    if (iface.FullName == null || iface.FullName.StartsWith("System")) continue;
                    Logger.Log("DotNetBrowser", $"  {iface.FullName}:");
                    foreach (var p in iface.GetProperties(pub))
                        Logger.Log("DotNetBrowser", $"    {p.Name} : {p.PropertyType.Name}");
                    foreach (var m in iface.GetMethods(pub))
                    {
                        if (m.IsSpecialName) continue;
                        var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                        Logger.Log("DotNetBrowser", $"    {m.Name}({parms}) -> {m.ReturnType.Name}");
                    }
                }
            }

            profileIndex++;
        }

        Logger.Log("DotNetBrowser", $"GetBrowserFromEngine: checked {profileIndex} profiles, no IBrowser found");
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
    /// Get all frames from IBrowser.AllFrames.
    /// Returns a list of frame objects (IFrame instances).
    /// </summary>
    public List<object> GetAllFrames(object browser)
    {
        var frames = new List<object>();
        try
        {
            var allFramesProp = IBrowserType?.GetProperty("AllFrames",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (allFramesProp == null)
            {
                // Try on the concrete type
                allFramesProp = browser.GetType().GetProperty("AllFrames",
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            }
            if (allFramesProp == null)
            {
                Logger.Log("DotNetBrowser", "AllFrames property not found on IBrowser");
                return frames;
            }

            var allFramesObj = allFramesProp.GetValue(browser);
            if (allFramesObj == null) return frames;

            // AllFrames returns IEnumerable<IFrame> -- enumerate via IEnumerable
            if (allFramesObj is System.Collections.IEnumerable enumerable)
            {
                foreach (var f in enumerable)
                {
                    if (f != null)
                        frames.Add(f);
                }
            }
            Logger.Log("DotNetBrowser", $"AllFrames: {frames.Count} frame(s) found");
        }
        catch (Exception ex)
        {
            Logger.Log("DotNetBrowser", $"GetAllFrames error: {ex.Message}");
        }
        return frames;
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
            object? result;
            if (method.IsStatic)
            {
                // Extension method: first param is frame, second is script
                var parms = method.GetParameters();
                if (parms.Length == 2)
                    result = method.Invoke(null, new object[] { frame, script });
                else if (parms.Length > 2)
                {
                    // Fill optional params with defaults
                    var args = new object?[parms.Length];
                    args[0] = frame;
                    args[1] = script;
                    for (int i = 2; i < parms.Length; i++)
                        args[i] = DefaultForParam(parms[i]);
                    result = method.Invoke(null, args);
                }
                else
                    result = method.Invoke(null, new object[] { script });
            }
            else
            {
                // Instance method on frame
                var parms = method.GetParameters();
                if (parms.Length == 1)
                    result = method.Invoke(frame, new object[] { script });
                else
                {
                    // Multi-param: fill optional params with defaults
                    var args = new object?[parms.Length];
                    args[0] = script;
                    for (int i = 1; i < parms.Length; i++)
                        args[i] = DefaultForParam(parms[i]);
                    result = method.Invoke(frame, args);
                }
            }
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
    /// Get a safe default value for a method parameter.
    /// Uses the declared default if available, otherwise creates a default for value types
    /// (false for bool, 0 for int, etc.) to avoid passing null to non-nullable params.
    /// </summary>
    private static object? DefaultForParam(ParameterInfo param)
    {
        if (param.HasDefaultValue) return param.DefaultValue;
        var t = param.ParameterType;
        if (t.IsValueType) return Activator.CreateInstance(t); // false, 0, etc.
        return null;
    }

    /// <summary>
    /// Check if core DotNetBrowser assemblies are loaded.
    /// Only requires DotNetBrowser / DotNetBrowser.Core / DotNetBrowser.Chromium —
    /// the AvaloniaUi assembly is NOT needed (IBrowser, IFrame, ExecuteJavaScript
    /// all live in the core DotNetBrowser.dll).
    /// </summary>
    public static bool AreDotNetBrowserAssembliesLoaded()
    {
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            var name = asm.GetName().Name ?? "";
            if (name.Equals("DotNetBrowser", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("DotNetBrowser.Core", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("DotNetBrowser.Chromium", StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
}
