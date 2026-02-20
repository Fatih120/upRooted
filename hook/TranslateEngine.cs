using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Uprooted;

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
    private const int ReceiveScanIntervalMs = 500;

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

    // Translation cache: raw text → translated text
    private readonly Dictionary<string, string> _translationCache = new();

    // Receive-side: messageId → translated text
    private readonly Dictionary<string, string> _receivedCache = new();

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
                try   { DiscoverToolbars(); }
                catch (Exception ex) { Logger.Log("Translate", $"DiscoverToolbars error: {ex.Message}"); }
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

                // The button strip should have at least 1 child (even if empty it's still our target)
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

            InsertChildBeforeLast(buttonStrip, btn);
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
                    callback(isRight);
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

    private void InsertChildBeforeLast(object panel, object child)
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
                    return;
                }

                // Fallback: append
                var addMethod = ct.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
                if (addMethod != null)
                {
                    addMethod.Invoke(coll, new[] { child });
                    Logger.Log("Translate", $"Appended button via {collPropName}.Add");
                    return;
                }
            }

            Logger.Log("Translate", $"InsertChildBeforeLast: no suitable collection found on {panel.GetType().Name}");
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"InsertChildBeforeLast error: {ex.Message}");
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
                    Logger.Log("Translate", $"Hooked TextArea for send-side translate (id={id})");
                }
                return;
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"HookTextArea error: {ex.Message}");
        }
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

                    // Substitute from cache if available (most common case after first send)
                    if (_translationCache.TryGetValue(text, out var cached))
                    {
                        _textProperty?.SetValue(capturedEditor, cached);
                        Logger.Log("Translate", $"Send-side: substituted from cache ({text.Length}→{cached.Length})");
                        return;
                    }

                    // Not cached — warm the cache in background; next send will use it
                    var fromLang = settings.TranslateSendFromLang;
                    var toLang   = settings.TranslateSendToLang;
                    var rawText  = text;
                    Task.Run(async () =>
                    {
                        try
                        {
                            var translated = await TranslateAsync(rawText, fromLang, toLang)
                                .ConfigureAwait(false);
                            if (!string.IsNullOrEmpty(translated))
                                _translationCache[rawText] = translated;
                        }
                        catch (Exception ex2)
                        {
                            Logger.Log("Translate", $"Cache-fill error: {ex2.Message}");
                        }
                    });
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
            if (tag.StartsWith("uprooted-translate:", StringComparison.Ordinal)) continue;

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
                    var translated = await TranslateAsync(rawText, fromLang, toLang).ConfigureAwait(false);
                    if (string.IsNullOrEmpty(translated) || translated == rawText) return;

                    _receivedCache[capturedMsgId] = translated;

                    _r.RunOnUIThread(() =>
                    {
                        try { InjectTranslatedRow(capturedNode, translated, capturedMsgId); }
                        catch (Exception ex) { Logger.Log("Translate", $"InjectRow error: {ex.Message}"); }
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

    private void InjectTranslatedRow(object anchorNode, string translatedText, string msgId)
    {
        try
        {
            // Walk up to find a Grid or StackPanel to add the translation row to
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

            // Build translation label: "🌐 {text}"
            var tb = _r.CreateTextBlock($"🌐 {translatedText}", 12, ContentPages.TextMuted, "Normal");
            if (tb == null) return;
            _r.SetMargin(tb, 0, 2, 0, 0);
            _r.SetTag(tb, $"uprooted-translate:{msgId}");
            _r.SetIsHitTestVisible(tb, false);

            // Also tag the anchor so it won't be re-queued
            _r.SetTag(anchorNode, $"uprooted-translate:{msgId}");

            _r.AddChild(container, tb);
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"InjectTranslatedRow error: {ex.Message}");
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Google Translate (unofficial, no API key required)
    // ──────────────────────────────────────────────────────────────────────────

    internal static async Task<string?> TranslateAsync(string text, string from, string to)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;
        if (from == to && from != "auto") return text;

        try
        {
            var url = "https://translate.googleapis.com/translate_a/single"
                    + $"?client=gtx&sl={Uri.EscapeDataString(from)}"
                    + $"&tl={Uri.EscapeDataString(to)}"
                    + "&dt=t"
                    + $"&q={Uri.EscapeDataString(text)}";

            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(8);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            var resp = await client.GetStringAsync(url).ConfigureAwait(false);
            return ParseGoogleTranslateResponse(resp);
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"TranslateAsync error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Parse Google Translate unofficial JSON response.
    /// Format: [[[translated, original, ...], ...], ...]
    /// No JSON serializer — uses regex (honors no-System.Text.Json rule).
    /// Collects all first-position quoted strings in sub-arrays (the translated segments).
    /// </summary>
    internal static string? ParseGoogleTranslateResponse(string json)
    {
        if (string.IsNullOrEmpty(json)) return null;
        try
        {
            var sb = new System.Text.StringBuilder();
            // Match first string in each [[" block — these are the translated segments
            var matches = Regex.Matches(json, @"\[\[""((?:[^""\\]|\\.)*)""");
            foreach (Match m in matches)
            {
                var seg = m.Groups[1].Value;
                // Unescape common JSON escape sequences
                seg = seg.Replace("\\n",  "\n")
                         .Replace("\\t",  "\t")
                         .Replace("\\\"", "\"")
                         .Replace("\\\\", "\\");
                seg = Regex.Replace(seg, @"\\u([0-9a-fA-F]{4})",
                    m2 => ((char)Convert.ToInt32(m2.Groups[1].Value, 16)).ToString());
                sb.Append(seg);
            }
            var result = sb.ToString().Trim();
            return result.Length > 0 ? result : null;
        }
        catch (Exception ex)
        {
            Logger.Log("Translate", $"ParseGoogleTranslateResponse error: {ex.Message}");
            return null;
        }
    }
}
