using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Uprooted;

/// <summary>
/// Strips tracking parameters (utm_source, fbclid, gclid, etc.) from URLs in the
/// compose editor when the user presses Enter to send.
///
/// ## Intercepting sent messages in Root
///
/// Root's compose input is AvaloniaEdit.TextEditor (NOT Avalonia.Controls.TextBox).
/// The visual tree path is: RootMessageTextboxView > TextEditor > TextArea > TextView.
///
/// AvaloniaEdit marks Enter (Key.Return) as Handled=true internally, which means:
///   - CLR event handlers (EventInfo.AddEventHandler) do NOT receive Enter
///   - Avalonia's AddHandler with RoutingStrategies.Bubble alone does NOT receive Enter
///   - You MUST use AddHandler with ALL routing strategies (Bubble|Tunnel|Direct = 7)
///     AND handledEventsToo=true
///
/// Hook TextArea (the actual editing surface) rather than TextEditor — it fires first
/// and is closer to where AvaloniaEdit processes input. Read/write text via the parent
/// TextEditor's CLR Text property (third-party lib, not subject to Root's trimming).
///
/// The handler fires BEFORE Root processes the Enter, so modifying TextEditor.Text in
/// the handler changes what gets sent.
///
/// This pattern (timer discovery + AddHandler on TextArea) is reusable for any feature
/// that needs to intercept or modify outgoing messages.
/// </summary>
internal class ClearUrlsEngine
{
    private const int ScanIntervalMs = 2000;

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;

    private Timer? _scanTimer;
    private int _scanning; // Interlocked reentrancy guard

    // Track hooked editors by identity hash to avoid double-hooking
    private readonly HashSet<int> _hookedEditors = new();

    // Resolved types
    private Type? _textEditorType;        // AvaloniaEdit.TextEditor
    private Type? _textAreaType;          // AvaloniaEdit.Editing.TextArea (actual input surface)
    private PropertyInfo? _textProperty;  // TextEditor.Text (CLR property, not trimmed)
    private object? _keyReturn;           // Key.Return enum value

    // Routed event subscription (AddHandler with handledEventsToo)
    private object? _keyDownRoutedEvent;  // InputElement.KeyDownEvent static field value
    private object? _routingAll;          // RoutingStrategies.Bubble | Tunnel | Direct
    private MethodInfo? _addHandlerMethod;

    private bool _typesResolved;
    private bool _typesValid;

    // Tracking parameters to strip (case-insensitive)
    private static readonly HashSet<string> TrackingParams = new(StringComparer.OrdinalIgnoreCase)
    {
        // Google Analytics
        "utm_source", "utm_medium", "utm_campaign", "utm_term", "utm_content", "utm_id", "utm_cid",
        // Facebook
        "fbclid",
        // Google Ads
        "gclid", "gclsrc", "dclid",
        // Microsoft
        "msclkid",
        // Instagram
        "igshid",
        // Twitter/X
        "twclid",
        // YouTube
        "si", "feature",
        // Mailchimp
        "mc_cid", "mc_eid",
        // Google internal
        "_ga", "_gl", "_gid",
        // Yandex
        "yclid",
        // HubSpot
        "_hsenc", "_hsmi", "__hstc", "__hsfp", "hsCtaTracking",
        // Various
        "wickedid", "rb_clickid", "soc_src", "soc_trk", "vero_id", "_openstat"
    };

    // URL regex (same pattern as LinkEmbedEngine)
    private static readonly Regex UrlRegex = new(
        @"https?://[^\s<>""')\]]+",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    internal ClearUrlsEngine(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _mainWindow = mainWindow;
    }

    internal void Initialize()
    {
        Logger.Log("ClearUrls", "Starting ClearURLs engine (KeyDown intercept on AvaloniaEdit.TextEditor)");

        if (!ResolveTypes())
        {
            Logger.Log("ClearUrls", "Failed to resolve required types — engine disabled");
            return;
        }

        _scanTimer = new Timer(OnScanTick, null, 0, ScanIntervalMs);
        Logger.Log("ClearUrls", $"Discovery timer started ({ScanIntervalMs}ms interval)");
    }

    /// <summary>
    /// Resolve AvaloniaEdit.TextEditor, its Text property, Key.Return, and the
    /// Avalonia routed event machinery for AddHandler with handledEventsToo.
    /// </summary>
    private bool ResolveTypes()
    {
        if (_typesResolved) return _typesValid;
        _typesResolved = true;

        try
        {
            Type? keyType = null;
            Type? inputElementType = null;
            Type? routingStrategiesType = null;

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var asmName = asm.GetName().Name ?? "";

                if (asmName.StartsWith("AvaloniaEdit", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var t in asm.GetTypes())
                    {
                        if (t.FullName == "AvaloniaEdit.TextEditor")
                            _textEditorType = t;
                        else if (t.FullName == "AvaloniaEdit.Editing.TextArea")
                            _textAreaType = t;
                    }
                }

                if (asmName.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var t in asm.GetTypes())
                    {
                        var fn = t.FullName;
                        if (fn == "Avalonia.Input.Key") keyType = t;
                        else if (fn == "Avalonia.Input.InputElement") inputElementType = t;
                        else if (fn == "Avalonia.Interactivity.RoutingStrategies") routingStrategiesType = t;
                    }
                }
            }

            if (_textEditorType == null) { Logger.Log("ClearUrls", "AvaloniaEdit.TextEditor not found"); return false; }
            if (keyType == null) { Logger.Log("ClearUrls", "Key enum not found"); return false; }
            if (inputElementType == null) { Logger.Log("ClearUrls", "InputElement not found"); return false; }
            if (routingStrategiesType == null) { Logger.Log("ClearUrls", "RoutingStrategies not found"); return false; }

            // TextEditor.Text CLR property
            _textProperty = _textEditorType.GetProperty("Text", BindingFlags.Public | BindingFlags.Instance);
            if (_textProperty == null) { Logger.Log("ClearUrls", "TextEditor.Text not found"); return false; }

            // Key.Return
            _keyReturn = Enum.Parse(keyType, "Return");

            // InputElement.KeyDownEvent static RoutedEvent field
            var keyDownField = inputElementType.GetField("KeyDownEvent",
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (keyDownField == null) { Logger.Log("ClearUrls", "KeyDownEvent field not found"); return false; }
            _keyDownRoutedEvent = keyDownField.GetValue(null);
            if (_keyDownRoutedEvent == null) { Logger.Log("ClearUrls", "KeyDownEvent value is null"); return false; }

            // RoutingStrategies: combine Bubble | Tunnel | Direct for maximum coverage
            // (Bubble alone is insufficient — AvaloniaEdit handles Enter before Bubble propagates)
            var bubble = (int)Enum.Parse(routingStrategiesType, "Bubble");
            var tunnel = (int)Enum.Parse(routingStrategiesType, "Tunnel");
            var direct = (int)Enum.Parse(routingStrategiesType, "Direct");
            _routingAll = Enum.ToObject(routingStrategiesType, bubble | tunnel | direct);

            // Find AddHandler(RoutedEvent, Delegate, RoutingStrategies, bool) — non-generic
            // Search on the TextEditor type itself (inherited from Interactive)
            var addMethods = _textEditorType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == "AddHandler" && !m.IsGenericMethod && m.GetParameters().Length == 4)
                .ToList();

            foreach (var m in addMethods)
            {
                var ps = m.GetParameters();
                if (ps[1].ParameterType == typeof(Delegate) || ps[1].ParameterType.IsAssignableFrom(typeof(Delegate)))
                    _addHandlerMethod = m;
            }

            if (_addHandlerMethod == null)
            {
                Logger.Log("ClearUrls", "No matching AddHandler found, will fall back to CLR event");
            }

            Logger.Log("ClearUrls", $"Resolved OK: TextArea={_textAreaType != null}, AddHandler={_addHandlerMethod != null}");
            _typesValid = true;
            return true;
        }
        catch (Exception ex)
        {
            Logger.Log("ClearUrls", $"ResolveTypes error: {ex.Message}");
            return false;
        }
    }

    private void OnScanTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        try
        {
            var settings = UprootedSettings.Load();
            if (settings.Plugins.TryGetValue("clear-urls", out var enabled) && !enabled)
            {
                Interlocked.Exchange(ref _scanning, 0);
                return;
            }

            _r.RunOnUIThread(() =>
            {
                try
                {
                    DiscoverAndHook();
                }
                catch (Exception ex)
                {
                    Logger.Log("ClearUrls", $"Discovery error: {ex.Message}");
                }
                finally
                {
                    Interlocked.Exchange(ref _scanning, 0);
                }
            });

            Task.Delay(ScanIntervalMs * 2).ContinueWith(_ =>
            {
                Interlocked.CompareExchange(ref _scanning, 0, 1);
            });
        }
        catch (Exception ex)
        {
            Logger.Log("ClearUrls", $"OnScanTick error: {ex.Message}");
            Interlocked.Exchange(ref _scanning, 0);
        }
    }

    private void DiscoverAndHook()
    {
        if (_textEditorType == null) return;

        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var nodeType = node.GetType();

            // Prefer hooking TextArea (actual input surface, fires first) with TextEditor for text access
            if (_textAreaType != null && _textAreaType.IsAssignableFrom(nodeType))
            {
                int id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(node);
                if (_hookedEditors.Contains(id)) continue;

                object? parentEditor = FindParentOfType(node, _textEditorType);
                if (parentEditor != null && AttachKeyDownHandler(node, parentEditor))
                {
                    _hookedEditors.Add(id);
                    // Also mark the parent TextEditor so we don't double-hook
                    _hookedEditors.Add(System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(parentEditor));
                    Logger.Log("ClearUrls", $"Hooked TextArea (id={id})");
                }
                continue;
            }

            // Fallback: hook TextEditor directly if TextArea wasn't found/resolved
            if (_textEditorType.IsAssignableFrom(nodeType))
            {
                int id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(node);
                if (_hookedEditors.Contains(id)) continue;

                if (AttachKeyDownHandler(node, node))
                {
                    _hookedEditors.Add(id);
                    Logger.Log("ClearUrls", $"Hooked TextEditor (id={id})");
                }
            }
        }
    }

    /// <summary>Find the first ancestor of a given type by walking up the visual tree.</summary>
    private object? FindParentOfType(object child, Type targetType)
    {
        try
        {
            // Use VisualTreeWalker's parent method if available, otherwise use reflection
            var getParentMethod = child.GetType().GetProperty("Parent",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            var current = child;
            for (int i = 0; i < 10; i++) // max 10 levels up
            {
                var parent = getParentMethod?.GetValue(current);
                if (parent == null) break;
                if (targetType.IsAssignableFrom(parent.GetType()))
                    return parent;
                current = parent;
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Attach a KeyDown handler. Tries Avalonia AddHandler with handledEventsToo=true first
    /// (required because AvaloniaEdit marks Enter as Handled). Falls back to CLR event.
    /// eventTarget = the control to attach the handler to (TextEditor or TextArea)
    /// textEditor = the TextEditor to read/write Text from (may be same as eventTarget)
    /// </summary>
    private bool AttachKeyDownHandler(object eventTarget, object textEditor)
    {
        try
        {
            // Get the CLR event to determine the handler delegate type
            var eventInfo = eventTarget.GetType().GetEvent("KeyDown");
            if (eventInfo == null)
            {
                Logger.Log("ClearUrls", $"KeyDown event not found on {eventTarget.GetType().Name}");
                return false;
            }

            var handlerType = eventInfo.EventHandlerType!;
            var invokeMethod = handlerType.GetMethod("Invoke")!;
            var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();

            var keyProp = paramTypes[1].GetProperty("Key");
            if (keyProp == null)
            {
                Logger.Log("ClearUrls", $"Key property not found on {paramTypes[1].Name}");
                return false;
            }

            // Build the callback
            var capturedEditor = textEditor;
            var capturedKeyReturn = _keyReturn;
            Action<object> onKeyDown = (args) =>
            {
                try
                {
                    var key = keyProp.GetValue(args);
                    if (key != null && key.Equals(capturedKeyReturn))
                        CleanEditor(capturedEditor);
                }
                catch (Exception ex)
                {
                    Logger.Log("ClearUrls", $"KeyDown callback error: {ex.Message}");
                }
            };

            // Build Expression: (sender, e) => onKeyDown((object)e)
            var p0 = Expression.Parameter(paramTypes[0], "sender");
            var p1 = Expression.Parameter(paramTypes[1], "e");
            var callbackExpr = Expression.Constant(onKeyDown);
            var castE = Expression.Convert(p1, typeof(object));
            var invokeExpr = Expression.Invoke(callbackExpr, castE);
            var lambda = Expression.Lambda(handlerType, invokeExpr, p0, p1);
            var handler = lambda.Compile();

            // AddHandler(RoutedEvent, Delegate, RoutingStrategies all, handledEventsToo=true)
            if (_addHandlerMethod != null && _keyDownRoutedEvent != null && _routingAll != null)
            {
                try
                {
                    _addHandlerMethod.Invoke(eventTarget, new[] { _keyDownRoutedEvent, handler, _routingAll, (object)true });
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Log("ClearUrls", $"AddHandler failed on {eventTarget.GetType().Name}: {ex.Message}");
                }
            }

            // Fallback: CLR event (won't see handled events like Enter, but better than nothing)
            eventInfo.AddEventHandler(eventTarget, handler);
            return true;
        }
        catch (Exception ex)
        {
            Logger.Log("ClearUrls", $"AttachKeyDownHandler error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Clean tracking params from all URLs in the TextEditor text.
    /// Uses the CLR Text property (AvaloniaEdit is third-party, not trimmed).
    /// </summary>
    private void CleanEditor(object editor)
    {
        try
        {
            string? text = _textProperty?.GetValue(editor) as string;
            if (text == null || text.Length == 0) return;
            if (!text.Contains('?')) return;

            var matches = UrlRegex.Matches(text);
            if (matches.Count == 0) return;

            string newText = text;
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                var url = match.Value;
                if (!url.Contains('?')) continue;

                var cleaned = CleanUrl(url);
                if (cleaned != url)
                    newText = newText.Substring(0, match.Index) + cleaned + newText.Substring(match.Index + match.Length);
            }

            if (newText == text) return;

            _textProperty?.SetValue(editor, newText);
            Logger.Log("ClearUrls", $"Cleaned on send: {text.Length} -> {newText.Length} chars");
        }
        catch (Exception ex)
        {
            Logger.Log("ClearUrls", $"CleanEditor error: {ex.Message}");
        }
    }

    /// <summary>
    /// Strips tracking parameters from a URL. Returns the original URL if no changes needed.
    /// Preserves fragment (#hash), non-tracking query params, and URL structure.
    /// </summary>
    internal static string CleanUrl(string url)
    {
        // Split off fragment first
        string fragment = "";
        int hashIdx = url.IndexOf('#');
        string urlNoFragment = url;
        if (hashIdx >= 0)
        {
            fragment = url.Substring(hashIdx);
            urlNoFragment = url.Substring(0, hashIdx);
        }

        // Find query string
        int qIdx = urlNoFragment.IndexOf('?');
        if (qIdx < 0) return url; // No query string

        string baseUrl = urlNoFragment.Substring(0, qIdx);
        string query = urlNoFragment.Substring(qIdx + 1);

        if (query.Length == 0) return url;

        // Split on & and filter out tracking params
        var parts = query.Split('&');
        var kept = new List<string>(parts.Length);

        foreach (var part in parts)
        {
            if (part.Length == 0) continue;

            // Extract key (everything before '=')
            int eqIdx = part.IndexOf('=');
            string key = eqIdx >= 0 ? part.Substring(0, eqIdx) : part;

            if (!TrackingParams.Contains(key))
                kept.Add(part);
        }

        // If nothing was removed, return original
        if (kept.Count == parts.Length) return url;

        // Reconstruct
        if (kept.Count == 0)
            return baseUrl + fragment;

        return baseUrl + "?" + string.Join("&", kept) + fragment;
    }
}
