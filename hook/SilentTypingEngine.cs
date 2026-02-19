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
/// private field `_handler`. We locate Root's HttpClient instance(s) via two strategies:
///
///   1. Static fields: scan all loaded assemblies for static HttpClient fields on types
///      whose names suggest gRPC/messaging involvement.
///   2. Instance fields: walk Root's ViewModel chain from MainWindow.DataContext and
///      inspect each reachable object's fields for HttpClient instances.
///
/// Once found, we prepend a TypingBlockerHandler to each HttpClient's handler chain.
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
    private const int MaxInstanceWalkDepth = 8;

    // Keywords to identify candidate types during assembly scan
    private static readonly string[] CandidateKeywords =
        { "Typing", "Message", "Grpc", "Api", "Service", "Client", "Http", "Channel" };

    private readonly AvaloniaReflection _r;
    private readonly object _mainWindow;

    // Patched client set (by identity) to avoid double-wrapping
    private readonly HashSet<int> _patched = new();

    private Timer? _scanTimer;
    private int _scanning; // Interlocked reentrancy guard
    private int _patchCount;

    // Cached reflection handles for HttpMessageInvoker._handler
    private static readonly FieldInfo? HandlerField =
        typeof(System.Net.Http.HttpMessageInvoker).GetField(
            "_handler", BindingFlags.NonPublic | BindingFlags.Instance);

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
            done.Wait(TimeSpan.FromSeconds(10));

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
                    if (!NameMatchesCandidates(type.Name)) continue;

                    try
                    {
                        var staticFields = type.GetFields(
                            BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                        foreach (var field in staticFields)
                        {
                            if (!IsHttpClientField(field.FieldType)) continue;
                            try
                            {
                                var client = field.GetValue(null) as System.Net.Http.HttpClient;
                                if (client != null)
                                    TryPatch(client, $"{type.Name}.{field.Name} (static)");
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }
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

                // Skip primitives, strings, enums, BCL types
                if (fieldType.IsPrimitive || fieldType == typeof(string) || fieldType.IsEnum) continue;
                if (IsFrameworkType(fieldType)) continue;

                // Recurse into Root's own types
                if (NameMatchesCandidates(type.Name) || NameMatchesCandidates(fieldType.Name))
                {
                    var child = field.GetValue(obj);
                    if (child != null)
                        WalkInstanceFields(child, depth + 1, visited);
                }
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

    // ===== Helpers =====

    private static bool IsHttpClientField(Type t) =>
        t == typeof(System.Net.Http.HttpClient) ||
        typeof(System.Net.Http.HttpClient).IsAssignableFrom(t);

    private static bool NameMatchesCandidates(string name)
    {
        foreach (var kw in CandidateKeywords)
            if (name.Contains(kw, StringComparison.OrdinalIgnoreCase))
                return true;
        return false;
    }

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
                if (path.Contains("SetTypingIndicator", StringComparison.Ordinal))
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
