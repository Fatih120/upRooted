using System.Net;
using System.Net.Http;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Blocks SetTypingIndicator gRPC calls at the .NET HttpClient layer so the typing
/// indicator is never sent to Root's servers.
///
/// ## Why C# and not JS
///
/// Root's chat UI is fully Avalonia-native (1647+ visual tree nodes, zero browser
/// controls). SetTypingIndicator is triggered by Root's own .NET code when the user
/// types in the AvaloniaEdit compose box. The call goes through Root's HttpClient or
/// gRPC channel — never through DotNetBrowser's fetch API. The original JS plugin
/// (v0.1.0) therefore never fired; this engine replaces it.
///
/// ## How interception works
///
/// HttpClient inherits from HttpMessageInvoker, which stores its handler chain in the
/// private field `_handler`. Root uses Grpc.Net.Client.GrpcChannel for gRPC calls,
/// which stores an internal HttpMessageInvoker in `&lt;HttpInvoker&gt;k__BackingField`.
/// We locate and patch handler chains via three strategies:
///
///   1. Static fields: scan ALL types in non-framework assemblies for static
///      HttpClient/HttpMessageHandler fields.
///   2. Instance fields: walk Root's ViewModel chain from MainWindow.DataContext,
///      recursing unconditionally into all non-framework types (up to depth 12)
///      to find HttpClient, HttpMessageHandler, and GrpcChannel instances.
///   3. GrpcChannel: when a GrpcChannel is found, extract its internal HttpInvoker
///      and patch the `_handler` field directly — this is the critical path for
///      intercepting SetTypingIndicator gRPC-web calls.
///
/// Once found, we prepend a TypingBlockerHandler to each handler chain.
/// The blocker intercepts any outbound request whose path contains "SetTypingIndicator"
/// and short-circuits it with a synthetic 200 OK — the same response Root would expect
/// for a successful (but silent) gRPC call.
///
/// ## Enable/disable
///
/// Reads `UprootedSettings.Get("silent-typing", "enabled")` on each request so the
/// user can toggle the plugin without restarting Root. When disabled, all requests pass
/// through normally.
/// </summary>
internal class SilentTypingEngine
{
    private const string Tag = "SilentTyping";
    private const int ScanIntervalMs = 5_000;
    private const int MaxInstanceWalkDepth = 12;

    private readonly AvaloniaReflection _r;
    private readonly object _mainWindow;

    // Patched client/handler set (by identity) to avoid double-wrapping
    private readonly HashSet<int> _patched = new();

    private Timer? _scanTimer;
    private int _scanning; // Interlocked reentrancy guard
    private int _patchCount;
    private bool _firstScanDone;

    // Cached reflection handles for HttpMessageInvoker._handler
    private static readonly FieldInfo? HandlerField =
        typeof(System.Net.Http.HttpMessageInvoker).GetField(
            "_handler", BindingFlags.NonPublic | BindingFlags.Instance);

    // Lazily resolved: GrpcChannel.<HttpInvoker>k__BackingField
    private static FieldInfo? _grpcInvokerField;
    private static bool _grpcFieldResolved;

    internal SilentTypingEngine(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _mainWindow = mainWindow;
    }

    internal void Initialize()
    {
        if (HandlerField == null)
        {
            Logger.Log(Tag, "HttpMessageInvoker._handler field not found — engine disabled");
            return;
        }

        Logger.Log(Tag, "Starting silent typing engine (HttpClient handler injection)");
        _scanTimer = new Timer(OnScanTick, null, 0, ScanIntervalMs);
    }

    // ===== Timer callback =====

    private void OnScanTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        try
        {
            var settings = UprootedSettings.Load();
            if (!settings.Plugins.TryGetValue("silent-typing", out var enabled) || !enabled)
            {
                // Plugin disabled — nothing to patch, just exit
                Interlocked.Exchange(ref _scanning, 0);
                return;
            }

            // Strategy 1: static fields — safe to call from any thread
            ScanStaticFields();

            // Strategy 2: instance fields — requires UI thread for ViewModel access
            using var done = new ManualResetEventSlim(false);
            _r.RunOnUIThread(() =>
            {
                try { ScanViewModelChain(); }
                catch (Exception ex) { Logger.Log(Tag, $"ViewModel scan error: {ex.Message}"); }
                finally { done.Set(); }
            });
            done.Wait(10_000);

            if (!_firstScanDone)
            {
                _firstScanDone = true;
                Logger.Log(Tag, $"First scan complete: {_patchCount} HttpClient/handler(s) patched");
            }

            if (_patchCount > 0)
            {
                // At least one client patched — slow the scan down; keep running in case
                // Root creates new HttpClient instances (e.g. after reconnect)
                if (_scanTimer != null)
                    _scanTimer.Change(30_000, 30_000);
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"ScanTick error: {ex.Message}");
        }
        finally
        {
            Interlocked.Exchange(ref _scanning, 0);
        }
    }

    // ===== Strategy 1: static HttpClient fields =====

    private void ScanStaticFields()
    {
        int typesScanned = 0;

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";

                // Skip BCL, Avalonia, and our own assembly — only inspect Root's code
                if (IsFrameworkAssembly(asmName)) continue;

                Type[] types;
                try { types = asm.GetTypes(); }
                catch { continue; }

                foreach (var type in types)
                {
                    typesScanned++;

                    try
                    {
                        var staticFields = type.GetFields(
                            BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                        foreach (var field in staticFields)
                        {
                            try
                            {
                                if (IsHttpClientField(field.FieldType))
                                {
                                    var client = field.GetValue(null) as System.Net.Http.HttpClient;
                                    if (client != null)
                                        TryPatch(client, $"{type.Name}.{field.Name} (static)");
                                }
                                else if (IsHandlerField(field.FieldType))
                                {
                                    var handler = field.GetValue(null) as System.Net.Http.HttpMessageHandler;
                                    if (handler != null)
                                        TryPatchHandler(handler, field, null, $"{type.Name}.{field.Name} (static handler)");
                                }
                                else if (IsGrpcChannelType(field.FieldType))
                                {
                                    var channel = field.GetValue(null);
                                    if (channel != null)
                                        TryPatchGrpcChannel(channel, $"{type.Name}.{field.Name} (static GrpcChannel)");
                                }
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        if (!_firstScanDone)
            Logger.Log(Tag, $"Static scan: {typesScanned} types scanned");
    }

    // ===== Strategy 2: instance fields via ViewModel chain =====

    private void ScanViewModelChain()
    {
        // Root: MainWindow.DataContext -> ViewModel -> ... -> service layer
        object? dataContext = null;
        try
        {
            var dcProp = _mainWindow.GetType().GetProperty(
                "DataContext", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            dataContext = dcProp?.GetValue(_mainWindow);
        }
        catch { }

        if (dataContext == null) return;

        var visited = new HashSet<int>();
        WalkInstanceFields(dataContext, 0, visited);
    }

    private void WalkInstanceFields(object obj, int depth, HashSet<int> visited)
    {
        if (depth > MaxInstanceWalkDepth) return;

        int id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        if (!visited.Add(id)) return;

        var type = obj.GetType();

        // GrpcChannel special case: it's not a framework type but stores its
        // HttpMessageInvoker internally — we need to patch that invoker's _handler
        if (IsGrpcChannelType(type))
        {
            TryPatchGrpcChannel(obj, $"{type.Name} (instance, depth={depth})");
            return; // Don't recurse further into GrpcChannel internals
        }

        if (IsFrameworkType(type)) return;

        var fields = type.GetFields(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            try
            {
                var fieldType = field.FieldType;

                // Direct HttpClient match
                if (IsHttpClientField(fieldType))
                {
                    var client = field.GetValue(obj) as System.Net.Http.HttpClient;
                    if (client != null)
                        TryPatch(client, $"{type.Name}.{field.Name} (instance, depth={depth})");
                    continue;
                }

                // Direct HttpMessageHandler match
                if (IsHandlerField(fieldType))
                {
                    var handler = field.GetValue(obj) as System.Net.Http.HttpMessageHandler;
                    if (handler != null)
                        TryPatchHandler(handler, field, obj, $"{type.Name}.{field.Name} (instance handler, depth={depth})");
                    continue;
                }

                // Skip primitives, strings, enums, BCL types
                if (fieldType.IsPrimitive || fieldType == typeof(string) || fieldType.IsEnum) continue;

                // Allow recursion into GrpcChannel fields even though they're "framework-ish"
                if (IsGrpcChannelType(fieldType))
                {
                    var channel = field.GetValue(obj);
                    if (channel != null)
                    {
                        int chId = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(channel);
                        if (visited.Add(chId))
                            TryPatchGrpcChannel(channel, $"{type.Name}.{field.Name} (GrpcChannel, depth={depth})");
                    }
                    continue;
                }

                if (IsFrameworkType(fieldType)) continue;

                // Recurse into all non-framework types unconditionally — the existing
                // safeguards (IsFrameworkType, visited set, MaxInstanceWalkDepth,
                // primitive/string/enum skip) prevent explosion
                var child = field.GetValue(obj);
                if (child != null)
                    WalkInstanceFields(child, depth + 1, visited);
            }
            catch { }
        }
    }

    // ===== Handler injection =====

    private void TryPatch(System.Net.Http.HttpClient client, string location)
    {
        if (HandlerField == null) return;

        int id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(client);
        if (_patched.Contains(id)) return;

        try
        {
            var existing = HandlerField.GetValue(client) as System.Net.Http.HttpMessageHandler;
            if (existing is TypingBlockerHandler)
            {
                _patched.Add(id); // already wrapped (e.g. from a previous scan)
                return;
            }

            var blocker = new TypingBlockerHandler(existing!);
            HandlerField.SetValue(client, blocker);
            _patched.Add(id);
            _patchCount++;

            Logger.Log(Tag, $"Patched HttpClient @ {location} (total patched: {_patchCount})");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Patch failed @ {location}: {ex.Message}");
        }
    }

    /// <summary>
    /// Wraps a raw HttpMessageHandler field with TypingBlockerHandler.
    /// Used when Root holds handlers directly (e.g. gRPC transport) without HttpClient.
    /// </summary>
    private void TryPatchHandler(System.Net.Http.HttpMessageHandler handler, FieldInfo field, object? owner, string location)
    {
        int id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(handler);
        if (_patched.Contains(id)) return;

        if (handler is TypingBlockerHandler)
        {
            _patched.Add(id);
            return;
        }

        try
        {
            var blocker = new TypingBlockerHandler(handler);
            field.SetValue(owner, blocker);
            _patched.Add(id);
            _patchCount++;

            Logger.Log(Tag, $"Patched handler @ {location} (total patched: {_patchCount})");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Handler patch failed @ {location}: {ex.Message}");
        }
    }

    /// <summary>
    /// Patches a GrpcChannel's internal HttpInvoker._handler with TypingBlockerHandler.
    /// GrpcChannel stores an HttpMessageInvoker (not HttpClient) that handles all gRPC calls.
    /// </summary>
    private void TryPatchGrpcChannel(object channel, string location)
    {
        if (HandlerField == null) return;

        try
        {
            // Lazy-resolve the GrpcChannel.<HttpInvoker>k__BackingField
            if (!_grpcFieldResolved)
            {
                _grpcFieldResolved = true;
                _grpcInvokerField = channel.GetType().GetField(
                    "<HttpInvoker>k__BackingField",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                if (_grpcInvokerField == null)
                {
                    Logger.Log(Tag, "GrpcChannel.<HttpInvoker>k__BackingField not found");
                    return;
                }
            }

            if (_grpcInvokerField == null) return;

            // Get the HttpMessageInvoker from the channel
            var invoker = _grpcInvokerField.GetValue(channel);
            if (invoker == null) return;

            // Patch the invoker's _handler field (same field as HttpClient uses)
            int id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(invoker);
            if (_patched.Contains(id)) return;

            var existing = HandlerField.GetValue(invoker) as System.Net.Http.HttpMessageHandler;
            if (existing is TypingBlockerHandler)
            {
                _patched.Add(id);
                return;
            }

            var blocker = new TypingBlockerHandler(existing!);
            HandlerField.SetValue(invoker, blocker);
            _patched.Add(id);
            _patchCount++;

            Logger.Log(Tag, $"Patched GrpcChannel invoker @ {location} (handler: {existing?.GetType().Name}, total patched: {_patchCount})");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"GrpcChannel patch failed @ {location}: {ex.Message}");
        }
    }

    // ===== Helpers =====

    private static bool IsHttpClientField(Type t) =>
        t == typeof(System.Net.Http.HttpClient) ||
        typeof(System.Net.Http.HttpClient).IsAssignableFrom(t);

    private static bool IsHandlerField(Type t) =>
        typeof(System.Net.Http.HttpMessageHandler).IsAssignableFrom(t) &&
        t != typeof(TypingBlockerHandler);

    private static bool IsGrpcChannelType(Type t) =>
        t.FullName == "Grpc.Net.Client.GrpcChannel" ||
        (t.Name == "GrpcChannel" && (t.Namespace ?? "").StartsWith("Grpc", StringComparison.Ordinal));

    private static bool IsFrameworkAssembly(string name)
    {
        if (name.StartsWith("System", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("AvaloniaEdit", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("Uprooted", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("mscorlib", StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    private static bool IsFrameworkType(Type t)
    {
        var ns = t.Namespace ?? "";
        if (ns.StartsWith("System", StringComparison.OrdinalIgnoreCase)) return true;
        if (ns.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase)) return true;
        if (ns.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }
}

/// <summary>
/// DelegatingHandler that silently drops SetTypingIndicator gRPC requests.
/// Checks UprootedSettings on each call so the toggle takes effect immediately.
/// </summary>
internal sealed class TypingBlockerHandler : System.Net.Http.DelegatingHandler
{
    internal TypingBlockerHandler(System.Net.Http.HttpMessageHandler inner) : base(inner) { }

    protected override Task<System.Net.Http.HttpResponseMessage> SendAsync(
        System.Net.Http.HttpRequestMessage request, CancellationToken ct)
    {
        try
        {
            var settings = UprootedSettings.Load();
            if (settings.Plugins.TryGetValue("silent-typing", out var enabled) && enabled)
            {
                var path = request.RequestUri?.AbsolutePath ?? "";
                if (path.Contains("SetTypingIndicator", StringComparison.OrdinalIgnoreCase))
                {
                    Logger.Log("SilentTyping", "Blocked SetTypingIndicator");
                    return Task.FromResult(
                        new System.Net.Http.HttpResponseMessage(HttpStatusCode.OK));
                }
            }
        }
        catch { }

        return base.SendAsync(request, ct);
    }
}
