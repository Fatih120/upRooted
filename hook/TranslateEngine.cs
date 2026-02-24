using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Uprooted;

/// <summary>
/// Translation result containing the translated text and detected source language.
/// </summary>
internal readonly struct TranslationResult
{
    public readonly string? Text;
    public readonly string? SourceLanguage;
    public TranslationResult(string? text, string? sourceLanguage)
    {
        Text = text;
        SourceLanguage = sourceLanguage;
    }
}

/// <summary>
/// Translate plugin engine.
/// Injects a globe/translate icon button into the compose bar toolbar.
/// Left-click opens TranslateConfigPopup. Right-click toggles AutoTranslate mode.
/// When AutoTranslate is ON the button is accent-colored; OFF it is dimmed grey.
///
/// Send-side: intercepts Enter on AvaloniaEdit.TextArea; when AutoTranslate is ON
/// translates outgoing text to the configured target language before sending.
///
/// Receive-side (when enabled): 500ms scan timer walks the visible message list,
/// finds CTextBlock nodes not yet translated, fetches translation, injects a small
/// TextBlock row below each foreign message.
///
/// Discovery reuses the ClearUrlsEngine pattern: 2s timer + DescendantsDepthFirst
/// searching for RootMessageTextboxView nodes, RuntimeHelpers.GetHashCode dedup.
/// </summary>
internal class TranslateEngine
{
    private const int ScanIntervalMs = 2_000;
    private const int ReceiveScanIntervalMs = 2_000;
    private const int MaxCacheSize = 500;
    private const int DebounceMs = 1_000;

    // Google Translate API key (public, embedded in Google Translate web app)
    private const string GoogleApiKey = "AIzaSyDLEeFI5OtFBwYBIoK_jj5m32rZK5CkCXA";

    // ──────────────────────────────────────────────────────────────────────────
    // Language lists
    // ──────────────────────────────────────────────────────────────────────────

    internal static readonly (string Code, string Name)[] GoogleLanguages =
    {
        ("auto", "Detect language"),
        ("af", "Afrikaans"), ("sq", "Albanian"), ("am", "Amharic"), ("ar", "Arabic"),
        ("hy", "Armenian"), ("as", "Assamese"), ("ay", "Aymara"), ("az", "Azerbaijani"),
        ("bm", "Bambara"), ("eu", "Basque"), ("be", "Belarusian"), ("bn", "Bengali"),
        ("bho", "Bhojpuri"), ("bs", "Bosnian"), ("bg", "Bulgarian"), ("ca", "Catalan"),
        ("ceb", "Cebuano"), ("ny", "Chichewa"), ("zh-CN", "Chinese (Simplified)"),
        ("zh-TW", "Chinese (Traditional)"), ("co", "Corsican"), ("hr", "Croatian"),
        ("cs", "Czech"), ("da", "Danish"), ("dv", "Dhivehi"), ("doi", "Dogri"),
        ("nl", "Dutch"), ("en", "English"), ("eo", "Esperanto"), ("et", "Estonian"),
        ("ee", "Ewe"), ("tl", "Filipino"), ("fi", "Finnish"), ("fr", "French"),
        ("fy", "Frisian"), ("gl", "Galician"), ("ka", "Georgian"), ("de", "German"),
        ("el", "Greek"), ("gn", "Guarani"), ("gu", "Gujarati"), ("ht", "Haitian Creole"),
        ("ha", "Hausa"), ("haw", "Hawaiian"), ("iw", "Hebrew"), ("hi", "Hindi"),
        ("hmn", "Hmong"), ("hu", "Hungarian"), ("is", "Icelandic"), ("ig", "Igbo"),
        ("ilo", "Ilocano"), ("id", "Indonesian"), ("ga", "Irish"), ("it", "Italian"),
        ("ja", "Japanese"), ("jw", "Javanese"), ("kn", "Kannada"), ("kk", "Kazakh"),
        ("km", "Khmer"), ("rw", "Kinyarwanda"), ("gom", "Konkani"), ("ko", "Korean"),
        ("kri", "Krio"), ("ku", "Kurdish (Kurmanji)"), ("ckb", "Kurdish (Sorani)"),
        ("ky", "Kyrgyz"), ("lo", "Lao"), ("la", "Latin"), ("lv", "Latvian"),
        ("ln", "Lingala"), ("lt", "Lithuanian"), ("lg", "Luganda"),
        ("lb", "Luxembourgish"), ("mk", "Macedonian"), ("mai", "Maithili"),
        ("mg", "Malagasy"), ("ms", "Malay"), ("ml", "Malayalam"), ("mt", "Maltese"),
        ("mi", "Maori"), ("mr", "Marathi"), ("mni-Mtei", "Meiteilon (Manipuri)"),
        ("lus", "Mizo"), ("mn", "Mongolian"), ("my", "Myanmar (Burmese)"),
        ("ne", "Nepali"), ("no", "Norwegian"), ("or", "Odia (Oriya)"), ("om", "Oromo"),
        ("ps", "Pashto"), ("fa", "Persian"), ("pl", "Polish"), ("pt", "Portuguese"),
        ("pa", "Punjabi"), ("qu", "Quechua"), ("ro", "Romanian"), ("ru", "Russian"),
        ("sm", "Samoan"), ("sa", "Sanskrit"), ("gd", "Scots Gaelic"), ("nso", "Sepedi"),
        ("sr", "Serbian"), ("st", "Sesotho"), ("sn", "Shona"), ("sd", "Sindhi"),
        ("si", "Sinhala"), ("sk", "Slovak"), ("sl", "Slovenian"), ("so", "Somali"),
        ("es", "Spanish"), ("su", "Sundanese"), ("sw", "Swahili"), ("sv", "Swedish"),
        ("tg", "Tajik"), ("ta", "Tamil"), ("tt", "Tatar"), ("te", "Telugu"),
        ("th", "Thai"), ("ti", "Tigrinya"), ("ts", "Tsonga"), ("tr", "Turkish"),
        ("tk", "Turkmen"), ("ak", "Twi"), ("uk", "Ukrainian"), ("ur", "Urdu"),
        ("ug", "Uyghur"), ("uz", "Uzbek"), ("vi", "Vietnamese"), ("cy", "Welsh"),
        ("xh", "Xhosa"), ("yi", "Yiddish"), ("yo", "Yoruba"), ("zu", "Zulu"),
    };

    internal static readonly (string Code, string Name)[] DeepLLanguages =
    {
        ("", "Detect language"),
        ("ar", "Arabic"), ("bg", "Bulgarian"), ("cs", "Czech"), ("da", "Danish"),
        ("de", "German"), ("el", "Greek"), ("en-us", "English (American)"),
        ("en-gb", "English (British)"), ("es", "Spanish"), ("et", "Estonian"),
        ("fi", "Finnish"), ("fr", "French"), ("hu", "Hungarian"), ("id", "Indonesian"),
        ("it", "Italian"), ("ja", "Japanese"), ("ko", "Korean"), ("lt", "Lithuanian"),
        ("lv", "Latvian"), ("nb", "Norwegian (Bokmal)"), ("nl", "Dutch"),
        ("pl", "Polish"), ("pt-br", "Portuguese (Brazilian)"),
        ("pt-pt", "Portuguese (European)"), ("ro", "Romanian"), ("ru", "Russian"),
        ("sk", "Slovak"), ("sl", "Slovenian"), ("sv", "Swedish"), ("tr", "Turkish"),
        ("uk", "Ukrainian"), ("zh-hans", "Chinese (Simplified)"),
        ("zh-hant", "Chinese (Traditional)"),
    };

    // Shared HttpClient — reused across all translation requests to avoid socket exhaustion.
    private static readonly HttpClient s_httpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(8)
    };
    static TranslateEngine()
    {
        s_httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
    }

    // Singleton for ContentPages access
    internal static TranslateEngine? Instance { get; set; }

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;
    private readonly ThemeEngine? _themeEngine;

    private Timer? _discoverTimer;
    private Timer? _receiveTimer;
    private int _discovering;   // Interlocked reentrancy guard
    private int _receiving;     // Interlocked reentrancy guard

    // Track injected toolbars by RuntimeHelpers.GetHashCode identity
    // Key = RuntimeHelpers.GetHashCode(RootMessageTextboxView)
    // Value = RuntimeHelpers.GetHashCode(button strip child panel) — prevents double-inject
    private readonly HashSet<int> _injectedToolbars = new();

    // Send-side: keyed by hooked TextArea identity hash
    private readonly HashSet<int> _hookedEditors = new();

    // Translation cache: raw text → translated text (thread-safe, capped at MaxCacheSize)
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, string> _translationCache = new();

    // Receive-side: messageId → translated text (thread-safe, capped at MaxCacheSize)
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, string> _receivedCache = new();

    // Context menu tracking
    private readonly HashSet<int> _hookedContextMenus = new();
    private Type? _menuItemType;
    private Type? _separatorType;
    private bool _contextMenuTypesResolved;

    // Send-side debounce
    private Timer? _debounceTimer;
    private string? _pendingDebounceText;
    private bool _isSettingText;
    private object? _cachedTextEditor; // the TextEditor for debounce callback

    // Translation display tracking (hash of display panels injected)
    private readonly HashSet<int> _translatedDisplays = new();

    // AvaloniaEdit types (resolved once)
    private Type? _textEditorType;
    private Type? _textAreaType;
    private PropertyInfo? _textProperty;

    // Routed event plumbing (same as ClearUrlsEngine)
    private object? _keyReturn;
    private object? _keyDownRoutedEvent;
    private object? _routingAll;
    private MethodInfo? _addHandlerMethod;

    private bool _typesResolved;
    private bool _typesValid;

    // All injected button objects, keyed by identity hash of their parent textbox view
    private readonly Dictionary<int, object> _buttons = new();

    // Globe/translate SVG path (Material Design "translate" 24×24 viewbox)
    private const string TranslateIconPath =
        "M12.87 15.07l-2.54-2.51.03-.03c1.74-1.94 2.98-4.17 3.71-6.53H17V4h-7V2H8v2H1v1.99h11.17C11.5 7.92 10.44 9.75 9 11.35 8.07 10.32 7.3 9.19 6.69 8h-2c.73 1.63 1.73 3.17 2.98 4.56l-5.09 5.02L4 19l5-5 3.11 3.11.76-2.04zM18.5 10h-2L12 22h2l1.12-3h4.75L21 22h2l-4.5-12zm-2.62 7l1.62-4.33L19.12 17h-3.24z";

    // Button color when AutoTranslate is OFF — dimmed white, matching other toolbar icon tones
    private const string ButtonDimColor = "#66f2f2f2";

    internal TranslateEngine(AvaloniaReflection resolver, object mainWindow, ThemeEngine? themeEngine)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _mainWindow = mainWindow;
        _themeEngine = themeEngine;
    }

    internal void Initialize()
    {
        Logger.Log("Translate", "Starting translate engine");

        if (!ResolveTypes())
            Logger.Log("Translate", "Failed to resolve AvaloniaEdit types — send-side disabled");

        _discoverTimer = new Timer(OnDiscoverTick, null, 0, ScanIntervalMs);
        _receiveTimer  = new Timer(OnReceiveTick, null, 2_000, ReceiveScanIntervalMs);

        Logger.Log("Translate", "Discovery + receive timers started");
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Type resolution (AvaloniaEdit + routed event plumbing)
    // ──────────────────────────────────────────────────────────────────────────

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
                        if (t.FullName == "AvaloniaEdit.TextEditor")         _textEditorType = t;
                        else if (t.FullName == "AvaloniaEdit.Editing.TextArea") _textAreaType = t;
                    }
                }
                if (asmName.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var t in asm.GetTypes())
                    {
                        var fn = t.FullName;
                        if (fn == "Avalonia.Input.Key")                      keyType = t;
                        else if (fn == "Avalonia.Input.InputElement")        inputElementType = t;
                        else if (fn == "Avalonia.Interactivity.RoutingStrategies") routingStrategiesType = t;
                    }
                }
            }

            if (_textEditorType == null || keyType == null || inputElementType == null
                || routingStrategiesType == null)
            {
                Logger.Log("Translate", "Missing required types");
                return false;
            }

            _textProperty = _textEditorType.GetProperty("Text",
                BindingFlags.Public | BindingFlags.Instance);
            if (_textProperty == null) { Logger.Log("Translate", "TextEditor.Text not found"); return false; }

            _keyReturn = Enum.Parse(keyType, "Return");

            var keyDownField = inputElementType.GetField("KeyDownEvent",
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (keyDownField == null) return false;
            _keyDownRoutedEvent = keyDownField.GetValue(null);

            var bubble = (int)Enum.Parse(routingStrategiesType, "Bubble");
            var tunnel = (int)Enum.Parse(routingStrategiesType, "Tunnel");
            var direct = (int)Enum.Parse(routingStrategiesType, "Direct");
            _routingAll = Enum.ToObject(routingStrategiesType, bubble | tunnel | direct);

            _addHandlerMethod = _textEditorType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == "AddHandler" && !m.IsGenericMethod && m.GetParameters().Length == 4)
                .FirstOrDefault(m =>
                {
                    var ps = m.GetParameters();
                    return ps[1].ParameterType == typeof(Delegate)
                        || ps[1].ParameterType.IsAssignableFrom(typeof(Delegate));
                });

            Logger.Log("Translate", $"Types OK: TextArea={_textAreaType != null}, AddHandler={_addHandlerMethod != null}");
            _typesValid = true;
            return true;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"ResolveTypes error: {ex.Message}");
            return false;
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Toolbar discovery
    // ──────────────────────────────────────────────────────────────────────────

    private void OnDiscoverTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _discovering, 1, 0) != 0) return;
        try
        {
            var settings = UprootedSettings.Load();
            if (!(settings.Plugins.TryGetValue("translate", out var en) && en))
            {
                Interlocked.Exchange(ref _discovering, 0);
                return;
            }

            _r.RunOnUIThread(() =>
            {
                try
                {
                    DiscoverToolbars();
                    ScanContextMenus();
                }
                catch (Exception ex) { Logger.Log("Translate", $"Discover error: {ex.Message}"); }
                finally { Interlocked.Exchange(ref _discovering, 0); }
            });

            // Fallback release of the guard
            Task.Delay(ScanIntervalMs * 2).ContinueWith(_ =>
                Interlocked.CompareExchange(ref _discovering, 0, 1));
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"OnDiscoverTick error: {ex.Message}");
            Interlocked.Exchange(ref _discovering, 0);
        }
    }

    private void DiscoverToolbars()
    {
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var typeName = node.GetType().Name;
            if (!typeName.Contains("RootMessageTextboxView", StringComparison.OrdinalIgnoreCase))
                continue;

            int id = RuntimeHelpers.GetHashCode(node);
            if (_injectedToolbars.Contains(id)) continue;

            LogChildrenDebug(node);

            if (TryInjectButton(node))
            {
                _injectedToolbars.Add(id);
                Logger.Log("Translate", $"Injected translate button into toolbar id={id}");

                if (_typesValid)
                    HookTextArea(node);
            }
            else
            {
                Logger.Log("Translate", $"Toolbar injection failed for id={id}, will retry");
            }
        }
    }

    private void LogChildrenDebug(object view)
    {
        try
        {
            var childrenProp = view.GetType().GetProperty("Children",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            if (childrenProp == null) return;
            var children = childrenProp.GetValue(view) as System.Collections.IEnumerable;
            if (children == null) return;
            var names = new List<string>();
            foreach (var child in children)
                names.Add(child.GetType().Name);
            Logger.Log("Translate", $"  Children of RootMessageTextboxView: [{string.Join(", ", names)}]");
        }
        catch { }
    }

    private bool TryInjectButton(object textboxView)
    {
        try
        {
            // From visual tree analysis, the toolbar button container is:
            //   RootMessageTextboxView > Panel > RootBorder > Grid > RootBorder > Grid > ItemsControl
            // There are two ItemsControls inside the view:
            //   1. TextEditor > Border > ScrollViewer > TextArea > DockPanel > ItemsControl  (editor text lines)
            //   2. RootBorder > Grid > ItemsControl  ← THE BUTTON STRIP we want
            // We find the button strip by looking for an ItemsControl that is NOT inside
            // TextEditor/TextArea/ScrollViewer.

            object? buttonStrip = null;
            foreach (var node in _walker.DescendantsDepthFirst(textboxView))
            {
                var nodeName = node.GetType().Name;
                if (nodeName != "ItemsControl") continue;

                // Skip if this ItemsControl is inside the TextEditor (text line container)
                if (IsInsideTextEditor(node, textboxView)) continue;

                int childCount = GetChildCount(node);
                Logger.Log("Translate", $"  Candidate ItemsControl: children={childCount}");

                // The real button strip always has existing buttons (e.g. emoji, GIF, attach).
                // An ItemsControl with 0 children is likely a template/placeholder — skip it.
                if (childCount == 0)
                {
                    Logger.Log("Translate", "  Skipping empty ItemsControl (0 children)");
                    continue;
                }

                buttonStrip = node;
                Logger.Log("Translate", $"  Found button strip ItemsControl (children={childCount})");
                break;
            }

            if (buttonStrip == null)
            {
                // Fallback: look for any Grid/StackPanel that's a sibling of TextEditor
                buttonStrip = FindButtonStripFallback(textboxView);
                if (buttonStrip == null)
                {
                    Logger.Log("Translate", "  No button strip found, will retry");
                    return false;
                }
            }

            int stripId = RuntimeHelpers.GetHashCode(buttonStrip);
            if (_injectedToolbars.Contains(stripId)) return true;

            var settings = UprootedSettings.Load();
            string color = settings.TranslateAutoTranslate ? ContentPages.AccentGreen : ButtonDimColor;

            var btn = BuildTranslateButton(color);
            if (btn == null) return false;

            if (!InsertChildBeforeLast(buttonStrip, btn))
            {
                Logger.Log("Translate", "  Insert failed, will retry");
                return false;
            }
            _injectedToolbars.Add(stripId);
            _buttons[RuntimeHelpers.GetHashCode(textboxView)] = btn;

            // Wire pointer events — left-click opens popup, right-click toggles AutoTranslate
            var capturedBtn = btn;
            SubscribePointerPressed(capturedBtn, (isRight) =>
            {
                if (!isRight)
                {
                    _r.RunOnUIThread(() =>
                    {
                        try { TranslateConfigPopup.Show(_r, capturedBtn, _themeEngine, this); }
                        catch (Exception ex) { Logger.Log("Translate", $"Popup show error: {ex.Message}"); }
                    });
                }
                else
                {
                    _r.RunOnUIThread(() =>
                    {
                        try { ToggleAutoTranslate(capturedBtn); }
                        catch (Exception ex) { Logger.Log("Translate", $"Toggle error: {ex.Message}"); }
                    });
                }
            });

            return true;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"TryInjectButton error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Returns true if the given node is inside TextEditor / TextArea / ScrollViewer.
    /// Used to distinguish the editor-internal ItemsControl from the toolbar ItemsControl.
    /// Walks up at most 10 levels.
    /// </summary>
    private bool IsInsideTextEditor(object node, object stopAt)
    {
        try
        {
            var current = node;
            for (int i = 0; i < 10; i++)
            {
                var parentProp = current.GetType().GetProperty("Parent",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                var parent = parentProp?.GetValue(current);
                if (parent == null || ReferenceEquals(parent, stopAt)) break;

                var pn = parent.GetType().Name;
                if (pn == "TextEditor" || pn == "TextArea" || pn == "ScrollViewer"
                    || (_textEditorType != null && _textEditorType.IsAssignableFrom(parent.GetType()))
                    || (_textAreaType   != null && _textAreaType.IsAssignableFrom(parent.GetType())))
                    return true;

                current = parent;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Fallback: find the button strip by looking at the children of the inner Grid
    /// (the one that contains TextEditor as a sibling). Returns a Grid or StackPanel
    /// that is a sibling of TextEditor and has at least 1 child.
    /// </summary>
    private object? FindButtonStripFallback(object textboxView)
    {
        try
        {
            // Walk all descendants; find each Grid/Panel; check if any sibling child
            // is a TextEditor type → that Grid IS the outer container, so return it.
            foreach (var node in _walker.DescendantsDepthFirst(textboxView))
            {
                var nodeName = node.GetType().Name;
                if (nodeName != "Grid" && nodeName != "StackPanel"
                    && nodeName != "DockPanel" && nodeName != "WrapPanel") continue;

                if (HasTextEditorChildAndOtherChild(node))
                {
                    Logger.Log("Translate", $"  Fallback: found container Grid with TextEditor sibling");
                    return node;
                }
            }
        }
        catch { }
        return null;
    }

    private bool HasTextEditorChildAndOtherChild(object panel)
    {
        try
        {
            var childrenProp = panel.GetType().GetProperty("Children",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            if (childrenProp == null) return false;
            var children = childrenProp.GetValue(panel) as System.Collections.IEnumerable;
            if (children == null) return false;

            bool hasTextEditor = false;
            int  otherCount    = 0;
            foreach (var child in children)
            {
                var cn = child.GetType().Name;
                if (cn == "TextEditor" || (_textEditorType != null && _textEditorType.IsAssignableFrom(child.GetType())))
                    hasTextEditor = true;
                else
                    otherCount++;
            }
            return hasTextEditor && otherCount >= 1;
        }
        catch { return false; }
    }

    /// <summary>
    /// Subscribe to PointerPressed on a control. Callback receives isRight=true for right button.
    /// Uses direct reflection + Expression lambda (same pattern as ClearUrlsEngine).
    /// </summary>
    private void SubscribePointerPressed(object control, Action<bool> callback)
    {
        try
        {
            var eventInfo = control.GetType().GetEvent("PointerPressed");
            if (eventInfo == null) { Logger.Log("Translate", "PointerPressed event not found"); return; }

            var handlerType = eventInfo.EventHandlerType!;
            var invokeMethod = handlerType.GetMethod("Invoke")!;
            var paramTypes   = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();
            // paramTypes[1] is PointerPressedEventArgs

            // We use Action<object> to receive the args object and inspect it via reflection
            Action<object> argsHandler = (argsObj) =>
            {
                try
                {
                    bool isRight = false;
                    bool isShift = false;
                    // GetCurrentPoint(relativeTo) → PointerPoint → Properties → IsRightButtonPressed
                    var getCurrentPoint = argsObj.GetType().GetMethod("GetCurrentPoint",
                        BindingFlags.Public | BindingFlags.Instance);
                    if (getCurrentPoint != null)
                    {
                        var pp = getCurrentPoint.Invoke(argsObj, new object?[] { null });
                        if (pp != null)
                        {
                            var propsProp = pp.GetType().GetProperty("Properties");
                            var props = propsProp?.GetValue(pp);
                            if (props != null)
                            {
                                var isRightProp = props.GetType().GetProperty("IsRightButtonPressed");
                                isRight = isRightProp?.GetValue(props) is bool b && b;
                            }
                        }
                    }
                    // Detect Shift key modifier — shift+left-click acts as toggle (like right-click)
                    var keyModsProp = argsObj.GetType().GetProperty("KeyModifiers");
                    if (keyModsProp != null)
                    {
                        var keyMods = keyModsProp.GetValue(argsObj);
                        if (keyMods != null)
                            isShift = ((int)keyMods & 2) != 0; // KeyModifiers.Shift = 2
                    }
                    callback(isRight || isShift);
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"PointerPressed handler error: {ex.Message}");
                    callback(false);
                }
            };

            // Build: (sender, e) => argsHandler((object)e)
            var p0       = Expression.Parameter(paramTypes[0], "sender");
            var p1       = Expression.Parameter(paramTypes[1], "e");
            var cbExpr   = Expression.Constant(argsHandler);
            var castE    = Expression.Convert(p1, typeof(object));
            var invExpr  = Expression.Invoke(cbExpr, castE);
            var lambda   = Expression.Lambda(handlerType, invExpr, p0, p1);
            var handler  = lambda.Compile();

            eventInfo.AddEventHandler(control, handler);
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"SubscribePointerPressed error: {ex.Message}");
        }
    }

    private int GetChildCount(object panel)
    {
        try
        {
            foreach (var propName in new[] { "Children", "Items" })
            {
                var prop = panel.GetType().GetProperty(propName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                if (prop == null) continue;
                var coll = prop.GetValue(panel) as System.Collections.IEnumerable;
                if (coll == null) continue;
                int c = 0;
                foreach (var _ in coll) c++;
                return c;
            }
        }
        catch { }
        return 0;
    }

    private bool InsertChildBeforeLast(object panel, object child)
    {
        try
        {
            // Try Children first (Panel-derived: Grid, StackPanel, etc.)
            // Then try Items (ItemsControl — toolbar uses ItemsControl)
            foreach (var collPropName in new[] { "Children", "Items" })
            {
                var collProp = panel.GetType().GetProperty(collPropName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                if (collProp == null) continue;

                var coll = collProp.GetValue(panel);
                if (coll == null) continue;

                var ct           = coll.GetType();
                var insertMethod = ct.GetMethod("Insert", BindingFlags.Public | BindingFlags.Instance);
                var countProp    = ct.GetProperty("Count",  BindingFlags.Public | BindingFlags.Instance);

                if (insertMethod != null && countProp != null)
                {
                    int count    = (int)(countProp.GetValue(coll) ?? 0);
                    int insertAt = Math.Max(0, count - 1);
                    insertMethod.Invoke(coll, new object[] { insertAt, child });
                    Logger.Log("Translate", $"Inserted button at index {insertAt} via {collPropName} (count was {count})");
                    return true;
                }

                // Fallback: append
                var addMethod = ct.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
                if (addMethod != null)
                {
                    addMethod.Invoke(coll, new[] { child });
                    Logger.Log("Translate", $"Appended button via {collPropName}.Add");
                    return true;
                }
            }

            Logger.Log("Translate", $"InsertChildBeforeLast: no suitable collection found on {panel.GetType().Name}");
            return false;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"InsertChildBeforeLast error: {ex.Message}");
            return false;
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Button construction
    // ──────────────────────────────────────────────────────────────────────────

    private object? BuildTranslateButton(string iconColor)
    {
        try
        {
            // 32×32 rounded Border container
            var btn = _r.CreateBorder(null, 6);
            if (btn == null) return null;
            _r.SetWidth(btn, 32);
            _r.SetHeight(btn, 32);
            _r.SetTag(btn, "uprooted-translate-btn");
            _r.SetIsHitTestVisible(btn, true);

            // Canvas holding the path
            var canvas = _r.CreateCanvas();
            if (canvas == null) return btn;
            _r.SetWidth(canvas, 24);
            _r.SetHeight(canvas, 24);

            var path = BuildSvgPath(TranslateIconPath, iconColor);
            if (path != null)
                _r.AddChild(canvas, path);

            // Center the 24×24 canvas in the 32×32 border
            _r.SetMargin(canvas, 4, 4, 4, 4);
            _r.SetBorderChild(btn, canvas);

            return btn;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"BuildTranslateButton error: {ex.Message}");
            return null;
        }
    }

    private object? BuildSvgPath(string pathData, string color)
    {
        try
        {
            Type? pathGeometryType = null;
            Type? pathType         = null;
            Type? stretchType      = null;

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase)) continue;
                foreach (var t in asm.GetTypes())
                {
                    if (t.FullName == "Avalonia.Media.PathGeometry")        pathGeometryType = t;
                    else if (t.FullName == "Avalonia.Controls.Shapes.Path") pathType = t;
                    else if (t.FullName == "Avalonia.Media.Stretch")        stretchType = t;
                }
            }

            if (pathGeometryType == null || pathType == null) return null;

            var parseMethod = pathGeometryType.GetMethod("Parse",
                BindingFlags.Public | BindingFlags.Static,
                null, new[] { typeof(string) }, null);
            if (parseMethod == null) return null;

            var geometry    = parseMethod.Invoke(null, new object[] { pathData });
            if (geometry == null) return null;

            var pathControl = Activator.CreateInstance(pathType);
            if (pathControl == null) return null;

            pathType.GetProperty("Data")?.SetValue(pathControl, geometry);
            pathType.GetProperty("Fill")?.SetValue(pathControl, _r.CreateBrush(color));
            pathType.GetProperty("Width")?.SetValue(pathControl, 24.0);
            pathType.GetProperty("Height")?.SetValue(pathControl, 24.0);

            if (stretchType != null)
            {
                var uniform = Enum.Parse(stretchType, "Uniform");
                pathType.GetProperty("Stretch")?.SetValue(pathControl, uniform);
            }

            return pathControl;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"BuildSvgPath error: {ex.Message}");
            return null;
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // AutoTranslate toggle
    // ──────────────────────────────────────────────────────────────────────────

    internal void ToggleAutoTranslate(object? btn = null)
    {
        var settings = UprootedSettings.Load();
        settings.TranslateAutoTranslate = !settings.TranslateAutoTranslate;
        settings.Save();

        Logger.Log("Translate", $"AutoTranslate toggled: {settings.TranslateAutoTranslate}");
        RefreshButtonColors(settings.TranslateAutoTranslate);
    }

    /// <summary>
    /// Refresh the color of all injected toolbar buttons to match the current AutoTranslate state.
    /// Called by the config popup when the toggle changes state without going through ToggleAutoTranslate.
    /// </summary>
    internal void RefreshButtonColors(bool autoTranslateOn)
    {
        var newColor = autoTranslateOn ? ContentPages.AccentGreen : ButtonDimColor;
        _r.RunOnUIThread(() =>
        {
            foreach (var b in _buttons.Values)
                UpdateButtonColor(b, newColor);
        });
    }

    internal void UpdateButtonColor(object btn, string color)
    {
        try
        {
            var canvas = _r.GetBorderChild(btn);
            if (canvas == null) return;

            var childrenProp = canvas.GetType().GetProperty("Children",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            var children = childrenProp?.GetValue(canvas) as System.Collections.IEnumerable;
            if (children == null) return;

            var brush = _r.CreateBrush(color);
            foreach (var child in children)
            {
                var fillProp = child.GetType().GetProperty("Fill");
                if (fillProp != null)
                    fillProp.SetValue(child, brush);
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"UpdateButtonColor error: {ex.Message}");
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Send-side: hook AvaloniaEdit TextArea for AutoTranslate on Enter
    // ──────────────────────────────────────────────────────────────────────────

    private void HookTextArea(object textboxView)
    {
        if (_textAreaType == null || _textEditorType == null) return;
        try
        {
            foreach (var node in _walker.DescendantsDepthFirst(textboxView))
            {
                if (!_textAreaType.IsAssignableFrom(node.GetType())) continue;

                int id = RuntimeHelpers.GetHashCode(node);
                if (_hookedEditors.Contains(id)) continue;

                var parentEditor = FindParentOfType(node, _textEditorType);
                if (parentEditor == null) continue;

                if (AttachTranslateKeyHandler(node, parentEditor))
                {
                    _hookedEditors.Add(id);
                    _hookedEditors.Add(RuntimeHelpers.GetHashCode(parentEditor));
                    _cachedTextEditor = parentEditor;
                    Logger.Log("Translate", $"Hooked TextArea for send-side translate (id={id})");

                    // Hook TextChanged for pre-translate debounce
                    HookTextChanged(parentEditor);
                }
                return;
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"HookTextArea error: {ex.Message}");
        }
    }

    /// <summary>
    /// Hook TextEditor.TextChanged for pre-translate debounce.
    /// While typing with AutoTranslate on, starts a 1s timer that pre-translates
    /// and caches the result so Enter is instant.
    /// </summary>
    private void HookTextChanged(object textEditor)
    {
        try
        {
            var eventInfo = textEditor.GetType().GetEvent("TextChanged");
            if (eventInfo == null)
            {
                Logger.Log("Translate", "TextChanged event not found on TextEditor");
                return;
            }

            var handlerType = eventInfo.EventHandlerType!;
            var invokeMethod = handlerType.GetMethod("Invoke")!;
            var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();

            var capturedEditor = textEditor;
            Action<object> onTextChanged = (_) =>
            {
                try
                {
                    if (_isSettingText) return;
                    var settings = UprootedSettings.Load();
                    if (!settings.TranslateAutoTranslate) return;

                    string? text = _textProperty?.GetValue(capturedEditor) as string;
                    if (string.IsNullOrWhiteSpace(text)) return;

                    // Already cached?
                    if (_translationCache.ContainsKey(text)) return;

                    // Restart debounce timer
                    _pendingDebounceText = text;
                    _debounceTimer?.Dispose();
                    _debounceTimer = new Timer(OnDebounceElapsed, null, DebounceMs, Timeout.Infinite);
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"TextChanged handler error: {ex.Message}");
                }
            };

            var p0 = Expression.Parameter(paramTypes[0], "sender");
            var p1 = Expression.Parameter(paramTypes[1], "e");
            var cbExpr = Expression.Constant(onTextChanged);
            var castE = Expression.Convert(p1, typeof(object));
            var invExpr = Expression.Invoke(cbExpr, castE);
            var lambda = Expression.Lambda(handlerType, invExpr, p0, p1);
            var handler = lambda.Compile();

            eventInfo.AddEventHandler(textEditor, handler);
            Logger.Log("Translate", "Hooked TextChanged for debounce pre-translate");
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"HookTextChanged error: {ex.Message}");
        }
    }

    private void OnDebounceElapsed(object? state)
    {
        var text = _pendingDebounceText;
        if (string.IsNullOrWhiteSpace(text)) return;

        var settings = UprootedSettings.Load();
        if (!settings.TranslateAutoTranslate) return;

        var fromLang = settings.TranslateSendFromLang;
        var toLang = settings.TranslateSendToLang;

        Task.Run(async () =>
        {
            try
            {
                var result = await TranslateServiceAsync(text, fromLang, toLang)
                    .ConfigureAwait(false);
                if (!string.IsNullOrEmpty(result.Text))
                {
                    if (_translationCache.Count >= MaxCacheSize) _translationCache.Clear();
                    _translationCache[text] = result.Text;
                    Logger.Log("Translate", $"Debounce: cached translation ({text.Length} chars)");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Translate", $"Debounce translate error: {ex.Message}");
            }
        });
    }

    private bool AttachTranslateKeyHandler(object eventTarget, object textEditor)
    {
        try
        {
            var eventInfo = eventTarget.GetType().GetEvent("KeyDown");
            if (eventInfo == null) return false;

            var handlerType  = eventInfo.EventHandlerType!;
            var paramTypes   = handlerType.GetMethod("Invoke")!.GetParameters()
                                          .Select(p => p.ParameterType).ToArray();
            var keyProp      = paramTypes[1].GetProperty("Key");
            if (keyProp == null) return false;

            var capturedEditor    = textEditor;
            var capturedKeyReturn = _keyReturn;

            Action<object> onKeyDown = (args) =>
            {
                try
                {
                    var key = keyProp.GetValue(args);
                    if (key == null || !key.Equals(capturedKeyReturn)) return;

                    var settings = UprootedSettings.Load();
                    if (!settings.TranslateAutoTranslate) return;

                    string? text = _textProperty?.GetValue(capturedEditor) as string;
                    if (string.IsNullOrWhiteSpace(text)) return;

                    // Cache hit — synchronous SetValue (instant, common case after debounce)
                    if (_translationCache.TryGetValue(text, out var cached))
                    {
                        _isSettingText = true;
                        try { _textProperty?.SetValue(capturedEditor, cached); }
                        finally { _isSettingText = false; }
                        Logger.Log("Translate", $"Send-side: cache hit ({text.Length}→{cached.Length})");
                        return;
                    }

                    // Cache miss — synchronous translate (blocks ~200-500ms, rare after debounce)
                    var fromLang = settings.TranslateSendFromLang;
                    var toLang   = settings.TranslateSendToLang;
                    try
                    {
                        var result = TranslateServiceAsync(text, fromLang, toLang)
                            .GetAwaiter().GetResult();
                        if (!string.IsNullOrEmpty(result.Text))
                        {
                            _isSettingText = true;
                            try { _textProperty?.SetValue(capturedEditor, result.Text); }
                            finally { _isSettingText = false; }
                            if (_translationCache.Count >= MaxCacheSize) _translationCache.Clear();
                            _translationCache[text] = result.Text;
                            Logger.Log("Translate", $"Send-side: sync translate ({text.Length}→{result.Text.Length})");
                        }
                    }
                    catch (Exception ex2)
                    {
                        Logger.Log("Translate", $"Send-side sync translate error: {ex2.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"KeyDown callback error: {ex.Message}");
                }
            };

            // Expression lambda: (sender, e) => onKeyDown((object)e)
            var p0      = Expression.Parameter(paramTypes[0], "sender");
            var p1      = Expression.Parameter(paramTypes[1], "e");
            var cbExpr  = Expression.Constant(onKeyDown);
            var castE   = Expression.Convert(p1, typeof(object));
            var invExpr = Expression.Invoke(cbExpr, castE);
            var lambda  = Expression.Lambda(handlerType, invExpr, p0, p1);
            var handler = lambda.Compile();

            if (_addHandlerMethod != null && _keyDownRoutedEvent != null && _routingAll != null)
            {
                try
                {
                    _addHandlerMethod.Invoke(eventTarget,
                        new[] { _keyDownRoutedEvent, handler, _routingAll, (object)true });
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"AddHandler failed: {ex.Message}");
                }
            }

            // Fallback: CLR event (won't see handled events, but better than nothing)
            eventInfo.AddEventHandler(eventTarget, handler);
            return true;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"AttachTranslateKeyHandler error: {ex.Message}");
            return false;
        }
    }

    private object? FindParentOfType(object child, Type targetType)
    {
        try
        {
            var current = child;
            for (int i = 0; i < 10; i++)
            {
                var parentProp = current.GetType().GetProperty("Parent",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                var parent = parentProp?.GetValue(current);
                if (parent == null) break;
                if (targetType.IsAssignableFrom(parent.GetType())) return parent;
                current = parent;
            }
        }
        catch { }
        return null;
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Receive-side: translate incoming messages in the visible message list
    // ──────────────────────────────────────────────────────────────────────────

    private void OnReceiveTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _receiving, 1, 0) != 0) return;
        try
        {
            var settings = UprootedSettings.Load();
            if (!(settings.Plugins.TryGetValue("translate", out var en) && en)
                || !settings.TranslateAutoTranslate)
            {
                Interlocked.Exchange(ref _receiving, 0);
                return;
            }

            _r.RunOnUIThread(() =>
            {
                try   { ScanReceivedMessages(); }
                catch (Exception ex) { Logger.Log("Translate", $"Receive scan error: {ex.Message}"); }
                finally { Interlocked.Exchange(ref _receiving, 0); }
            });

            Task.Delay(ReceiveScanIntervalMs * 4).ContinueWith(_ =>
                Interlocked.CompareExchange(ref _receiving, 0, 1));
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"OnReceiveTick error: {ex.Message}");
            Interlocked.Exchange(ref _receiving, 0);
        }
    }

    private void ScanReceivedMessages()
    {
        var settings = UprootedSettings.Load();

        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var typeName = node.GetType().Name;
            if (typeName.IndexOf("CTextBlock", StringComparison.OrdinalIgnoreCase) < 0) continue;

            var tag = _r.GetTag(node) as string ?? "";
            if (tag.StartsWith("uprooted-translate", StringComparison.Ordinal)) continue;

            string? text = TryGetCTextBlockContent(node);
            if (string.IsNullOrWhiteSpace(text) || text.Length < 3) continue;

            string msgId = GetMessageId(node);
            if (string.IsNullOrEmpty(msgId)) continue;
            if (_receivedCache.ContainsKey(msgId)) continue;
            if (IsOwnMessage(node)) continue;

            // Tag now to prevent re-queuing while async runs
            _r.SetTag(node, $"uprooted-translate:pending:{msgId}");

            var fromLang       = settings.TranslateFromLang;
            var toLang         = settings.TranslateToLang;
            var rawText        = text;
            var capturedMsgId  = msgId;
            var capturedNode   = node;

            Task.Run(async () =>
            {
                try
                {
                    var result = await TranslateServiceAsync(rawText, fromLang, toLang)
                        .ConfigureAwait(false);
                    if (string.IsNullOrEmpty(result.Text) || result.Text == rawText) return;

                    if (_receivedCache.Count >= MaxCacheSize) _receivedCache.Clear();
                    _receivedCache[capturedMsgId] = result.Text;

                    _r.RunOnUIThread(() =>
                    {
                        try
                        {
                            InjectTranslationDisplay(capturedNode, result.Text,
                                result.SourceLanguage, capturedMsgId);
                        }
                        catch (Exception ex) { Logger.Log("Translate", $"InjectDisplay error: {ex.Message}"); }
                    });
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"Receive translate error: {ex.Message}");
                }
            });
        }
    }

    private string? TryGetCTextBlockContent(object cTextBlock)
    {
        try
        {
            foreach (var propName in new[] { "Text", "Content", "RawText", "PlainText" })
            {
                var prop = cTextBlock.GetType().GetProperty(propName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                if (prop?.GetValue(cTextBlock) is string val && val.Length > 0)
                    return val;
            }
        }
        catch { }
        return null;
    }

    private string GetMessageId(object node)
    {
        try
        {
            var current = node;
            for (int i = 0; i < 12; i++)
            {
                var dcProp = current.GetType().GetProperty("DataContext",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                var dc = dcProp?.GetValue(current);
                if (dc != null)
                {
                    foreach (var idName in new[] { "MessageId", "Id", "LocalId" })
                    {
                        var idProp = dc.GetType().GetProperty(idName,
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        var idVal = idProp?.GetValue(dc);
                        if (idVal != null) return idVal.ToString() ?? "";
                    }
                }
                var parentProp = current.GetType().GetProperty("Parent",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                var parent = parentProp?.GetValue(current);
                if (parent == null) break;
                current = parent;
            }
        }
        catch { }
        return RuntimeHelpers.GetHashCode(node).ToString();
    }

    private bool IsOwnMessage(object node)
    {
        try
        {
            var current = node;
            for (int i = 0; i < 12; i++)
            {
                var dcProp = current.GetType().GetProperty("DataContext",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                var dc = dcProp?.GetValue(current);
                if (dc != null)
                {
                    foreach (var pn in new[] { "IsSelf", "IsOwn", "IsLocalUser", "IsMe" })
                    {
                        var p = dc.GetType().GetProperty(pn,
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        if (p?.GetValue(dc) is bool b) return b;
                    }
                    // SenderMember.IsSelf chain
                    var msgProp = dc.GetType().GetProperty("Message",
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                    var msg = msgProp?.GetValue(dc);
                    if (msg != null)
                    {
                        var senderProp = msg.GetType().GetProperty("SenderMember",
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                        var sender = senderProp?.GetValue(msg);
                        if (sender != null)
                        {
                            foreach (var sn in new[] { "IsSelf", "IsLocalUser", "IsMe" })
                            {
                                var sp = sender.GetType().GetProperty(sn,
                                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                                if (sp?.GetValue(sender) is bool sb) return sb;
                            }
                        }
                    }
                }
                var parentProp = current.GetType().GetProperty("Parent",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                var parent = parentProp?.GetValue(current);
                if (parent == null) break;
                current = parent;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Inject a styled translation display panel below the anchor node.
    /// Shows translated text, source language info, and a dismiss link.
    /// </summary>
    private void InjectTranslationDisplay(object anchorNode, string translatedText,
        string? sourceLanguage, string trackingId)
    {
        try
        {
            // Walk up to find a Grid or StackPanel to add the display to
            object? container = null;
            var current = anchorNode;
            for (int i = 0; i < 10; i++)
            {
                var parentProp = current.GetType().GetProperty("Parent",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                var parent = parentProp?.GetValue(current);
                if (parent == null) break;
                var pn = parent.GetType().Name;
                if (pn == "Grid" || pn == "StackPanel" || pn == "DockPanel")
                {
                    container = parent;
                    break;
                }
                current = parent;
            }
            if (container == null) return;

            var displayTag = $"uprooted-translate-display:{trackingId}";

            // Check if already injected
            foreach (var child in _walker.DescendantsDepthFirst(container))
            {
                var t = _r.GetTag(child) as string;
                if (t == displayTag) return;
            }

            // Build display panel: StackPanel(vertical, spacing=4)
            var panel = _r.CreateStackPanel(vertical: true, spacing: 4);
            if (panel == null) return;
            _r.SetMargin(panel, 0, 4, 0, 2);
            _r.SetTag(panel, displayTag);

            // Translated text (13pt, TextWhite, word-wrap)
            var textTb = _r.CreateTextBlock(translatedText, 13, ContentPages.TextWhite, "Normal");
            if (textTb != null)
            {
                _r.SetTag(textTb, "uprooted-no-recolor");
                SetTextWrapping(textTb, "Wrap");
                _r.AddChild(panel, textTb);
            }

            // Footer row: "Translated from {lang}" (left) + "Dismiss" (right)
            var footer = _r.CreateGrid();
            if (footer != null)
            {
                _r.SetTag(footer, "uprooted-no-recolor");

                var langLabel = sourceLanguage != null
                    ? $"Translated from {sourceLanguage}"
                    : "Translated";
                var langTb = _r.CreateTextBlock(langLabel, 11, ContentPages.TextMuted, "Normal");
                if (langTb != null)
                {
                    _r.SetHorizontalAlignment(langTb, "Left");
                    _r.SetVerticalAlignment(langTb, "Center");
                    _r.SetTag(langTb, "uprooted-no-recolor");
                    // Set italic via FontStyle
                    SetFontStyle(langTb, "Italic");
                    _r.AddChild(footer, langTb);
                }

                // Dismiss link
                var dismiss = _r.CreateTextBlock("Dismiss", 11, ContentPages.AccentGreen, "Normal");
                if (dismiss != null)
                {
                    _r.SetHorizontalAlignment(dismiss, "Right");
                    _r.SetVerticalAlignment(dismiss, "Center");
                    _r.SetIsHitTestVisible(dismiss, true);
                    _r.SetTag(dismiss, "uprooted-no-recolor");

                    var capturedPanel = panel;
                    var capturedContainer = container;
                    var capturedTrackingId = trackingId;
                    _r.SubscribeClickReleased(dismiss, () =>
                    {
                        try
                        {
                            RemoveChild(capturedContainer, capturedPanel);
                            _translatedDisplays.Remove(RuntimeHelpers.GetHashCode(capturedPanel));
                            // Allow re-translation by clearing the tag on anchor
                            _r.SetTag(anchorNode, "");
                            Logger.Log("Translate", $"Dismissed translation display: {capturedTrackingId}");
                        }
                        catch (Exception ex)
                        {
                            Logger.Log("Translate", $"Dismiss error: {ex.Message}");
                        }
                    });

                    _r.AddChild(footer, dismiss);
                }

                _r.AddChild(panel, footer);
            }

            // Tag the anchor so it won't be re-queued
            _r.SetTag(anchorNode, $"uprooted-translate-display:{trackingId}");

            _r.AddChild(container, panel);
            _translatedDisplays.Add(RuntimeHelpers.GetHashCode(panel));
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"InjectTranslationDisplay error: {ex.Message}");
        }
    }

    private void SetTextWrapping(object textBlock, string mode)
    {
        try
        {
            var prop = textBlock.GetType().GetProperty("TextWrapping");
            if (prop != null)
            {
                var wrapEnum = Enum.Parse(prop.PropertyType, mode);
                prop.SetValue(textBlock, wrapEnum);
            }
        }
        catch { }
    }

    private void SetFontStyle(object textBlock, string style)
    {
        try
        {
            var prop = textBlock.GetType().GetProperty("FontStyle");
            if (prop != null)
            {
                var styleEnum = Enum.Parse(prop.PropertyType, style);
                prop.SetValue(textBlock, styleEnum);
            }
        }
        catch { }
    }

    private void RemoveChild(object parent, object child)
    {
        try
        {
            foreach (var propName in new[] { "Children", "Items" })
            {
                var prop = parent.GetType().GetProperty(propName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                if (prop == null) continue;
                var coll = prop.GetValue(parent);
                if (coll == null) continue;
                var removeMethod = coll.GetType().GetMethod("Remove",
                    BindingFlags.Public | BindingFlags.Instance);
                if (removeMethod != null)
                {
                    removeMethod.Invoke(coll, new[] { child });
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"RemoveChild error: {ex.Message}");
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Context menu: inject "Translate" into message right-click menus
    // ──────────────────────────────────────────────────────────────────────────

    private bool ResolveContextMenuTypes()
    {
        if (_contextMenuTypesResolved) return _menuItemType != null;
        _contextMenuTypesResolved = true;
        try
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase)) continue;
                foreach (var t in asm.GetTypes())
                {
                    if (t.FullName == "Avalonia.Controls.MenuItem") _menuItemType = t;
                    else if (t.FullName == "Avalonia.Controls.Separator") _separatorType = t;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"ResolveContextMenuTypes error: {ex.Message}");
        }
        return _menuItemType != null;
    }

    private void ScanContextMenus()
    {
        if (!ResolveContextMenuTypes()) return;

        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var typeName = node.GetType().Name;

            // Look for message-related controls that might have context menus
            bool isMessageControl =
                typeName.Contains("Message", StringComparison.OrdinalIgnoreCase)
                && (typeName.Contains("Border", StringComparison.OrdinalIgnoreCase)
                    || typeName.Contains("Background", StringComparison.OrdinalIgnoreCase)
                    || typeName.Contains("View", StringComparison.OrdinalIgnoreCase)
                    || typeName.Contains("Item", StringComparison.OrdinalIgnoreCase));

            if (!isMessageControl) continue;

            int id = RuntimeHelpers.GetHashCode(node);
            if (_hookedContextMenus.Contains(id)) continue;

            // Check for ContextFlyout or ContextMenu
            object? flyout = null;
            foreach (var propName in new[] { "ContextFlyout", "ContextMenu" })
            {
                var prop = node.GetType().GetProperty(propName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                flyout = prop?.GetValue(node);
                if (flyout != null) break;
            }

            if (flyout == null) continue;

            HookContextMenu(node, flyout);
            _hookedContextMenus.Add(id);
        }
    }

    private void HookContextMenu(object ownerControl, object flyout)
    {
        try
        {
            // Subscribe to Opened/Opening event
            var openedEvent = flyout.GetType().GetEvent("Opened")
                           ?? flyout.GetType().GetEvent("Opening");
            if (openedEvent == null)
            {
                Logger.Log("Translate", $"No Opened/Opening event on {flyout.GetType().Name}");
                return;
            }

            var closedEvent = flyout.GetType().GetEvent("Closed")
                           ?? flyout.GetType().GetEvent("Closing");

            // Track injected items for removal on close
            var injectedItems = new List<object>();
            var capturedOwner = ownerControl;
            var capturedFlyout = flyout;

            // Build Opened handler
            var openedHandlerType = openedEvent.EventHandlerType!;
            var openedParamTypes = openedHandlerType.GetMethod("Invoke")!
                .GetParameters().Select(p => p.ParameterType).ToArray();

            Action<object> onOpened = (_) =>
            {
                try
                {
                    // Find the Items collection on the flyout
                    var itemsColl = GetItemsCollection(capturedFlyout);
                    if (itemsColl == null) return;

                    // Create separator
                    if (_separatorType != null)
                    {
                        var sep = Activator.CreateInstance(_separatorType);
                        if (sep != null)
                        {
                            _r.SetTag(sep, "uprooted-translate-ctx");
                            AddToCollection(itemsColl, sep);
                            injectedItems.Add(sep);
                        }
                    }

                    // Create "Translate" menu item
                    if (_menuItemType != null)
                    {
                        var menuItem = Activator.CreateInstance(_menuItemType);
                        if (menuItem != null)
                        {
                            _menuItemType.GetProperty("Header")?.SetValue(menuItem, "Translate");
                            _r.SetTag(menuItem, "uprooted-translate-ctx");

                            // Subscribe to Click
                            var clickEvent = _menuItemType.GetEvent("Click");
                            if (clickEvent != null)
                            {
                                var clickHandlerType = clickEvent.EventHandlerType!;
                                var clickParamTypes = clickHandlerType.GetMethod("Invoke")!
                                    .GetParameters().Select(p => p.ParameterType).ToArray();

                                var capturedOwnerForClick = capturedOwner;
                                Action<object> onClick = (__) =>
                                {
                                    try { TranslateMessageContent(capturedOwnerForClick); }
                                    catch (Exception ex) { Logger.Log("Translate", $"Context translate error: {ex.Message}"); }
                                };

                                var cp0 = Expression.Parameter(clickParamTypes[0], "s");
                                var cp1 = Expression.Parameter(clickParamTypes[1], "e");
                                var ccb = Expression.Constant(onClick);
                                var cce = Expression.Convert(cp1, typeof(object));
                                var cinv = Expression.Invoke(ccb, cce);
                                var clambda = Expression.Lambda(clickHandlerType, cinv, cp0, cp1);
                                clickEvent.AddEventHandler(menuItem, clambda.Compile());
                            }

                            AddToCollection(itemsColl, menuItem);
                            injectedItems.Add(menuItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"Context menu Opened handler error: {ex.Message}");
                }
            };

            var op0 = Expression.Parameter(openedParamTypes[0], "sender");
            var op1 = Expression.Parameter(openedParamTypes[1], "e");
            var ocb = Expression.Constant(onOpened);
            var oce = Expression.Convert(op1, typeof(object));
            var oinv = Expression.Invoke(ocb, oce);
            var olambda = Expression.Lambda(openedHandlerType, oinv, op0, op1);
            openedEvent.AddEventHandler(flyout, olambda.Compile());

            // Build Closed handler to remove injected items
            if (closedEvent != null)
            {
                var closedHandlerType = closedEvent.EventHandlerType!;
                var closedParamTypes = closedHandlerType.GetMethod("Invoke")!
                    .GetParameters().Select(p => p.ParameterType).ToArray();

                Action<object> onClosed = (_) =>
                {
                    try
                    {
                        var itemsColl = GetItemsCollection(capturedFlyout);
                        if (itemsColl != null)
                        {
                            foreach (var item in injectedItems)
                                RemoveFromCollection(itemsColl, item);
                        }
                        injectedItems.Clear();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Translate", $"Context menu Closed handler error: {ex.Message}");
                    }
                };

                var cp0 = Expression.Parameter(closedParamTypes[0], "sender");
                var cp1 = Expression.Parameter(closedParamTypes[1], "e");
                var ccb = Expression.Constant(onClosed);
                var cce = Expression.Convert(cp1, typeof(object));
                var cinv = Expression.Invoke(ccb, cce);
                var clambda = Expression.Lambda(closedHandlerType, cinv, cp0, cp1);
                closedEvent.AddEventHandler(flyout, clambda.Compile());
            }

            Logger.Log("Translate", $"Hooked context menu on {ownerControl.GetType().Name}");
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"HookContextMenu error: {ex.Message}");
        }
    }

    /// <summary>
    /// Translate the content of a message view (triggered by context menu click).
    /// </summary>
    private void TranslateMessageContent(object messageControl)
    {
        try
        {
            // Walk descendants to find CTextBlock
            string? text = null;
            object? cTextBlock = null;
            foreach (var node in _walker.DescendantsDepthFirst(messageControl))
            {
                var tn = node.GetType().Name;
                if (tn.IndexOf("CTextBlock", StringComparison.OrdinalIgnoreCase) < 0) continue;

                // Skip already-translated
                var tag = _r.GetTag(node) as string ?? "";
                if (tag.StartsWith("uprooted-translate", StringComparison.Ordinal)) continue;

                text = TryGetCTextBlockContent(node);
                if (!string.IsNullOrWhiteSpace(text) && text.Length >= 3)
                {
                    cTextBlock = node;
                    break;
                }
            }

            if (cTextBlock == null || string.IsNullOrWhiteSpace(text)) return;

            string trackingId = GetMessageId(cTextBlock);
            var capturedNode = cTextBlock;
            var capturedText = text;

            var settings = UprootedSettings.Load();
            var fromLang = settings.TranslateFromLang;
            var toLang = settings.TranslateToLang;

            // Tag to prevent re-processing
            _r.SetTag(capturedNode, $"uprooted-translate:pending:{trackingId}");

            Task.Run(async () =>
            {
                try
                {
                    var result = await TranslateServiceAsync(capturedText, fromLang, toLang)
                        .ConfigureAwait(false);
                    if (string.IsNullOrEmpty(result.Text) || result.Text == capturedText) return;

                    _r.RunOnUIThread(() =>
                    {
                        try
                        {
                            InjectTranslationDisplay(capturedNode, result.Text,
                                result.SourceLanguage, trackingId);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log("Translate", $"Context translate display error: {ex.Message}");
                        }
                    });
                }
                catch (Exception ex)
                {
                    Logger.Log("Translate", $"Context translate async error: {ex.Message}");
                }
            });
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"TranslateMessageContent error: {ex.Message}");
        }
    }

    private object? GetItemsCollection(object flyoutOrMenu)
    {
        foreach (var propName in new[] { "Items", "ItemsSource" })
        {
            var prop = flyoutOrMenu.GetType().GetProperty(propName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            var coll = prop?.GetValue(flyoutOrMenu);
            if (coll != null) return coll;
        }
        return null;
    }

    private void AddToCollection(object collection, object item)
    {
        var addMethod = collection.GetType().GetMethod("Add",
            BindingFlags.Public | BindingFlags.Instance);
        addMethod?.Invoke(collection, new[] { item });
    }

    private void RemoveFromCollection(object collection, object item)
    {
        var removeMethod = collection.GetType().GetMethod("Remove",
            BindingFlags.Public | BindingFlags.Instance);
        removeMethod?.Invoke(collection, new[] { item });
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Service layer: unified translation dispatcher
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Translate text using the configured service (Google, DeepL Free, DeepL Pro).
    /// Returns a TranslationResult with translated text and detected source language.
    /// </summary>
    internal static async Task<TranslationResult> TranslateServiceAsync(
        string text, string from, string to)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new TranslationResult(null, null);
        if (from == to && from != "auto" && from != "")
            return new TranslationResult(text, null);

        var settings = UprootedSettings.Load();
        var service = settings.TranslateService ?? "google";

        if (service == "deepl" || service == "deepl-pro")
        {
            var apiKey = settings.TranslateDeeplApiKey;
            if (!string.IsNullOrEmpty(apiKey))
            {
                var result = await DeepLTranslateAsync(text, from, to, apiKey,
                    isPro: service == "deepl-pro").ConfigureAwait(false);
                if (result.Text != null) return result;
                // DeepL returned null but didn't throw 403 — fall through to Google
            }
            else
            {
                Logger.Log("Translate", "DeepL selected but no API key configured, falling back to Google");
            }
        }

        return await GoogleTranslateAsync(text, from, to).ConfigureAwait(false);
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Google Translate
    // ──────────────────────────────────────────────────────────────────────────

    private static async Task<TranslationResult> GoogleTranslateAsync(
        string text, string from, string to)
    {
        // Try new endpoint first, fall back to old
        try
        {
            var result = await GoogleTranslateNewAsync(text, from, to).ConfigureAwait(false);
            if (result.Text != null) return result;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"Google new endpoint failed, trying old: {ex.Message}");
        }

        return await GoogleTranslateOldAsync(text, from, to).ConfigureAwait(false);
    }

    /// <summary>
    /// Google Translate PA endpoint (newer, returns structured response with source language).
    /// </summary>
    private static async Task<TranslationResult> GoogleTranslateNewAsync(
        string text, string from, string to)
    {
        var sl = from == "auto" ? "" : from;
        var url = "https://translate-pa.googleapis.com/v1/translate"
                + $"?params.client=gtx&dataTypes=TRANSLATION"
                + $"&key={GoogleApiKey}"
                + $"&query.sourceLanguage={Uri.EscapeDataString(sl)}"
                + $"&query.targetLanguage={Uri.EscapeDataString(to)}"
                + $"&query.text={Uri.EscapeDataString(text)}";

        var resp = await s_httpClient.GetStringAsync(url).ConfigureAwait(false);
        if (string.IsNullOrEmpty(resp))
            return new TranslationResult(null, null);

        // Extract translation and source language from response
        string? translated = null;
        string? sourceLang = null;

        var translationMatch = Regex.Match(resp, @"""translation""\s*:\s*""((?:[^""\\]|\\.)*)""");
        if (translationMatch.Success)
            translated = UnescapeJsonString(translationMatch.Groups[1].Value);

        var langMatch = Regex.Match(resp, @"""sourceLanguage""\s*:\s*""([^""]*)""");
        if (langMatch.Success)
            sourceLang = GetLanguageDisplayName(langMatch.Groups[1].Value);

        return new TranslationResult(translated, sourceLang);
    }

    /// <summary>
    /// Google Translate old endpoint (client=gtx, battle-tested fallback).
    /// </summary>
    private static async Task<TranslationResult> GoogleTranslateOldAsync(
        string text, string from, string to)
    {
        try
        {
            var url = "https://translate.googleapis.com/translate_a/single"
                    + $"?client=gtx&sl={Uri.EscapeDataString(from)}"
                    + $"&tl={Uri.EscapeDataString(to)}"
                    + "&dt=t"
                    + $"&q={Uri.EscapeDataString(text)}";

            var resp = await s_httpClient.GetStringAsync(url).ConfigureAwait(false);
            var translated = ParseGoogleOldResponse(resp);

            // Extract detected source language from response (third element at top level)
            string? sourceLang = null;
            var langMatch = Regex.Match(resp, @"\],null,""([a-z]{2}(?:-[A-Za-z]{2,5})?)""");
            if (langMatch.Success)
                sourceLang = GetLanguageDisplayName(langMatch.Groups[1].Value);

            return new TranslationResult(translated, sourceLang);
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"Google old endpoint error: {ex.Message}");
            return new TranslationResult(null, null);
        }
    }

    /// <summary>
    /// Parse Google Translate old endpoint JSON response.
    /// Format: [[[translated, original, ...], ...], ...]
    /// No JSON serializer — uses regex (honors no-System.Text.Json rule).
    /// </summary>
    internal static string? ParseGoogleOldResponse(string json)
    {
        if (string.IsNullOrEmpty(json)) return null;
        try
        {
            var sb = new System.Text.StringBuilder();
            var matches = Regex.Matches(json, @"\[\[""((?:[^""\\]|\\.)*)""");
            foreach (Match m in matches)
                sb.Append(UnescapeJsonString(m.Groups[1].Value));
            var result = sb.ToString().Trim();
            return result.Length > 0 ? result : null;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"ParseGoogleOldResponse error: {ex.Message}");
            return null;
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // DeepL
    // ──────────────────────────────────────────────────────────────────────────

    private static async Task<TranslationResult> DeepLTranslateAsync(
        string text, string from, string to, string apiKey, bool isPro)
    {
        try
        {
            var host = isPro ? "api.deepl.com" : "api-free.deepl.com";
            var url = $"https://{host}/v2/translate";

            // Build form body — omit source_lang for auto-detect
            var body = $"auth_key={Uri.EscapeDataString(apiKey)}"
                     + $"&text={Uri.EscapeDataString(text)}"
                     + $"&target_lang={Uri.EscapeDataString(to.ToUpperInvariant())}";

            if (!string.IsNullOrEmpty(from) && from != "auto")
                body += $"&source_lang={Uri.EscapeDataString(from.ToUpperInvariant())}";

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body, System.Text.Encoding.UTF8, new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded"))
            };

            var response = await s_httpClient.SendAsync(request).ConfigureAwait(false);
            var statusCode = (int)response.StatusCode;

            if (statusCode == 403)
            {
                Logger.Log("Translate", "DeepL: invalid API key (403)");
                return new TranslationResult(null, null); // don't fallback for auth errors
            }

            if (statusCode == 456)
            {
                Logger.Log("Translate", "DeepL: quota exceeded (456), falling back to Google");
                return new TranslationResult(null, null); // caller will fall back to Google
            }

            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Parse response: {"translations":[{"detected_source_language":"EN","text":"..."}]}
            string? translated = null;
            string? sourceLang = null;

            var textMatch = Regex.Match(resp, @"""text""\s*:\s*""((?:[^""\\]|\\.)*)""");
            if (textMatch.Success)
                translated = UnescapeJsonString(textMatch.Groups[1].Value);

            var langMatch = Regex.Match(resp, @"""detected_source_language""\s*:\s*""([^""]*)""");
            if (langMatch.Success)
                sourceLang = GetLanguageDisplayName(langMatch.Groups[1].Value.ToLowerInvariant());

            return new TranslationResult(translated, sourceLang);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("403"))
        {
            Logger.Log("Translate", $"DeepL auth error: {ex.Message}");
            return new TranslationResult(null, null);
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"DeepL error: {ex.Message}");
            return new TranslationResult(null, null);
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Helpers
    // ──────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Get the display name for a language code, checking both Google and DeepL lists.
    /// </summary>
    internal static string GetLanguageDisplayName(string code)
    {
        if (string.IsNullOrEmpty(code)) return "Unknown";
        var lc = code.ToLowerInvariant();
        foreach (var (c, n) in GoogleLanguages)
            if (c.Equals(lc, StringComparison.OrdinalIgnoreCase)) return n;
        foreach (var (c, n) in DeepLLanguages)
            if (c.Equals(lc, StringComparison.OrdinalIgnoreCase)) return n;
        return code;
    }

    /// <summary>
    /// Get the language list for the current service.
    /// </summary>
    internal static (string Code, string Name)[] GetLanguagesForService(string service)
    {
        return service == "deepl" || service == "deepl-pro" ? DeepLLanguages : GoogleLanguages;
    }

    private static string UnescapeJsonString(string s)
    {
        s = s.Replace("\\n", "\n")
             .Replace("\\t", "\t")
             .Replace("\\\"", "\"")
             .Replace("\\\\", "\\");
        s = Regex.Replace(s, @"\\u([0-9a-fA-F]{4})",
            m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString());
        return s;
    }
}
