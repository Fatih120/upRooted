using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace Uprooted;

/// <summary>
/// Blocks SetTypingIndicator gRPC calls via .NET's DiagnosticListener infrastructure.
///
/// Subscribes to <see cref="DiagnosticListener.AllListeners"/> and intercepts outbound
/// HTTP requests before they leave the process. Any request whose path contains
/// "SetTypingIndicator" is redirected to an unreachable address (localhost:0), causing
/// the call to silently fail without reaching Root's servers.
///
/// This replaces the previous 482-line HttpClient handler injection approach with a
/// clean ~80-line solution. DiagnosticListener is built into .NET's HttpClient stack
/// and fires for ALL outbound requests regardless of which HttpClient or GrpcChannel
/// instance makes the call — no discovery, no field walking, no handler patching.
///
/// Original DiagnosticListener approach by Kurumi Nanase.
/// </summary>
internal sealed class SilentTypingEngine
{
    private const string Tag = "SilentTyping";

    internal SilentTypingEngine(AvaloniaReflection resolver, object mainWindow)
    {
        // DiagnosticListener approach doesn't need resolver or mainWindow,
        // but constructor signature matches StartupHook Phase 4.5f call site
    }

    internal void Initialize()
    {
        DiagnosticListener.AllListeners.Subscribe(new ListenerObserver());
        Logger.Log(Tag, "Silent typing blocker active (DiagnosticListener)");
    }

    /// <summary>
    /// Watches for HTTP diagnostic listeners and subscribes a <see cref="RequestObserver"/>
    /// to intercept outbound requests.
    /// </summary>
    private sealed class ListenerObserver : IObserver<DiagnosticListener>
    {
        public void OnNext(DiagnosticListener listener)
        {
            if (listener.Name is "HttpHandlerDiagnosticListener" or "System.Net.Http")
                listener.Subscribe(new RequestObserver());
        }

        public void OnError(Exception ex) { }
        public void OnCompleted() { }
    }

    /// <summary>
    /// Observes outgoing HTTP requests and redirects SetTypingIndicator calls
    /// to an unreachable address so they silently fail.
    /// </summary>
    private sealed class RequestObserver : IObserver<KeyValuePair<string, object?>>
    {
        private const string TargetPath = "/root.v2.MessageGrpcService/SetTypingIndicator";

        public void OnNext(KeyValuePair<string, object?> kv)
        {
            if (kv.Key != "System.Net.Http.HttpRequestOut.Start")
                return;

            var request =
                kv.Value?.GetType().GetProperty("Request")?.GetValue(kv.Value)
                as HttpRequestMessage;

            if (request?.RequestUri?.AbsolutePath.Contains(TargetPath, StringComparison.OrdinalIgnoreCase) != true)
                return;

            // Check settings — allow toggle without restart
            try
            {
                var settings = UprootedSettings.Load();
                if (!settings.Plugins.TryGetValue("silent-typing", out var enabled) || !enabled)
                    return;
            }
            catch { return; }

            request.RequestUri = new Uri("http://localhost:0/blocked");
            Logger.Log("SilentTyping", "Blocked SetTypingIndicator");
        }

        public void OnError(Exception ex) { }
        public void OnCompleted() { }
    }
}
