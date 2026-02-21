using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Cached reflection handles for all Avalonia types, properties, and methods.
/// Single-file apps can't use Type.GetType("..., Assembly") so we scan loaded assemblies.
/// </summary>
internal class AvaloniaReflection
{
    // Core types
    public Type? ApplicationType { get; private set; }
    public Type? DispatcherType { get; private set; }
    public Type? WindowType { get; private set; }
    public Type? ControlType { get; private set; }
    public Type? PanelType { get; private set; }
    public Type? StackPanelType { get; private set; }
    public Type? TextBlockType { get; private set; }
    public Type? BorderType { get; private set; }
    public Type? ScrollViewerType { get; private set; }
    public Type? GridType { get; private set; }
    public Type? ContentControlType { get; private set; }
    public Type? ButtonType { get; private set; }
    public Type? ToggleSwitchType { get; private set; }
    public Type? TextBoxType { get; private set; }
    public Type? EllipseType { get; private set; }
    public Type? CanvasType { get; private set; }
    public Type? PathIconType { get; private set; }  // Avalonia.Controls.PathIcon
    public Type? PathShapeType { get; private set; } // Avalonia.Controls.Shapes.Path
    public Type? GeometryType { get; private set; }  // Avalonia.Media.Geometry

    // Image types
    public Type? ImageType { get; private set; }     // Avalonia.Controls.Image
    public Type? BitmapType { get; private set; }    // Avalonia.Media.Imaging.Bitmap
    public Type? StretchType { get; private set; }   // Avalonia.Media.Stretch

    // Overlay / layout types
    public Type? OverlayLayerType { get; private set; }
    public Type? PointType { get; private set; }
    public Type? RectType { get; private set; }

    // Resource system types
    public Type? ResourceDictionaryType { get; private set; }
    public Type? IResourceDictionaryType { get; private set; }

    // Value types
    public Type? SolidColorBrushType { get; private set; }
    public Type? LinearGradientBrushType { get; private set; }
    public Type? GradientStopType { get; private set; }
    public Type? GradientStopsType { get; private set; }
    public Type? RelativePointType { get; private set; }
    public Type? RelativeUnitType { get; private set; }
    public Type? ColorType { get; private set; }
    public Type? ThicknessType { get; private set; }
    public Type? CornerRadiusType { get; private set; }

    // Grid layout types
    public Type? ColumnDefinitionType { get; private set; }
    public Type? RowDefinitionType { get; private set; }
    public Type? GridLengthType { get; private set; }
    public Type? GridUnitTypeEnum { get; private set; }

    // Enums / structs
    public Type? HorizontalAlignmentType { get; private set; }
    public Type? VerticalAlignmentType { get; private set; }
    public Type? OrientationType { get; private set; }
    public Type? TextWrappingType { get; private set; }
    public Type? FontWeightType { get; private set; }
    public Type? CursorType { get; private set; }
    public Type? StandardCursorType { get; private set; }

    // Extension methods + interfaces
    public Type? VisualExtensionsType { get; private set; }
    public Type? VisualType { get; private set; }
    public Type? DesktopLifetimeType { get; private set; }
    public Type? TopLevelType { get; private set; }

    // Cached property/method handles
    private PropertyInfo? _appCurrent;
    private PropertyInfo? _appLifetime;
    private PropertyInfo? _lifetimeMainWindow;
    private PropertyInfo? _lifetimeWindows;
    private PropertyInfo? _dispatcherUIThread;
    private MethodInfo? _dispatcherPost;
    private MethodInfo? _getVisualChildren;
    private MethodInfo? _colorParse;

    private PropertyInfo? _panelChildren;
    private PropertyInfo? _controlTag;
    private PropertyInfo? _controlIsVisible;
    private PropertyInfo? _controlMargin;
    private PropertyInfo? _controlCursor;

    private PropertyInfo? _textBlockText;
    private PropertyInfo? _textBlockFontSize;
    private PropertyInfo? _textBlockFontWeight;
    private PropertyInfo? _textBlockForeground;
    private PropertyInfo? _textBlockTextWrapping;

    private PropertyInfo? _borderChild;
    private PropertyInfo? _borderBackground;
    private PropertyInfo? _borderCornerRadius;
    private PropertyInfo? _borderBorderBrush;
    private PropertyInfo? _borderBorderThickness;

    private PropertyInfo? _scrollViewerContent;
    private PropertyInfo? _stackPanelOrientation;
    private PropertyInfo? _stackPanelSpacing;
    private PropertyInfo? _contentControlContent;

    private MethodInfo? _gridSetColumn;
    private MethodInfo? _gridGetColumn;
    private MethodInfo? _gridSetRow;
    private MethodInfo? _gridGetRow;
    private PropertyInfo? _toggleSwitchIsChecked;
    private FieldInfo? _textBoxTextProperty;       // TextBox.TextProperty (AvaloniaProperty)
    private FieldInfo? _textBoxWatermarkProperty;  // TextBox.WatermarkProperty (AvaloniaProperty)
    private FieldInfo? _textBoxMaxLengthProperty;  // TextBox.MaxLengthProperty (AvaloniaProperty)

    // Overlay / Canvas / Bounds
    private MethodInfo? _overlayGetOverlayLayer;  // OverlayLayer.GetOverlayLayer(Visual)
    private MethodInfo? _canvasSetLeft;            // Canvas.SetLeft(AvaloniaObject, double)
    private MethodInfo? _canvasSetTop;             // Canvas.SetTop(AvaloniaObject, double)
    private PropertyInfo? _layoutableBounds;       // Layoutable.Bounds -> Rect
    private MethodInfo? _translatePoint;           // Visual.TranslatePoint(Point, Visual) extension
    private PropertyInfo? _controlOpacity;
    private PropertyInfo? _controlIsHitTestVisible;

    // Image
    private PropertyInfo? _imageSource;    // Image.Source
    private PropertyInfo? _imageStretch;   // Image.Stretch

    // Resource system
    private PropertyInfo? _appResources;              // Application.Resources
    private PropertyInfo? _resourcesMergedDicts;       // IResourceDictionary.MergedDictionaries

    // DynamicResource binding support
    private bool _dynamicResourceResolved;
    private Type? _dynamicResourceExtType;             // DynamicResourceExtension
    private MethodInfo? _avaloniaBindMethod;            // AvaloniaObjectExtensions.Bind(obj, prop, IBinding)
    private MethodInfo? _avaloniaBindObservableMethod;  // AvaloniaObjectExtensions.Bind(obj, prop, IObservable)
    private object? _bindingPriorityLocalValue;         // BindingPriority.LocalValue enum value
    private bool _loggedBindStrategy;
    private bool _loggedBindStrategyB;
    private readonly Dictionary<string, FieldInfo?> _avaloniaPropertyFieldCache = new();

    public bool IsResolved { get; private set; }

    public bool Resolve()
    {
        using var ev = WideEvent.Begin("Reflection", "resolve");
        try
        {
            var missing = new List<string>();
            ResolveTypes(ev, missing);
            ResolveMembers(ev, missing);
            IsResolved = ApplicationType != null && DispatcherType != null && ControlType != null;
            ev.Set("resolved", IsResolved);
            ev.Set("app", ApplicationType != null);
            ev.Set("dispatcher", DispatcherType != null);
            ev.Set("control", ControlType != null);
            ev.Set("panel", PanelType != null);
            ev.Set("text_block", TextBlockType != null);
            if (missing.Count > 0)
                ev.Set("missing", string.Join(",", missing));
            return IsResolved;
        }
        catch (Exception ex)
        {
            ev.SetError(ex);
            return false;
        }
    }

    private void ResolveTypes(WideEvent ev, List<string> missing)
    {
        var typeMap = new Dictionary<string, Type>();

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var name = asm.GetName().Name ?? "";
                if (!name.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase)) continue;

                foreach (var type in asm.GetTypes())
                {
                    var fn = type.FullName;
                    if (fn != null) typeMap[fn] = type;
                }
            }
            catch { }
        }

        ev.Set("types_scanned", typeMap.Count);

        Type? Find(string fullName) => typeMap.TryGetValue(fullName, out var t) ? t : null;

        ApplicationType = Find("Avalonia.Application");
        DispatcherType = Find("Avalonia.Threading.Dispatcher");
        WindowType = Find("Avalonia.Controls.Window");
        ControlType = Find("Avalonia.Controls.Control");
        PanelType = Find("Avalonia.Controls.Panel");
        StackPanelType = Find("Avalonia.Controls.StackPanel");
        TextBlockType = Find("Avalonia.Controls.TextBlock");
        BorderType = Find("Avalonia.Controls.Border");
        ScrollViewerType = Find("Avalonia.Controls.ScrollViewer");
        GridType = Find("Avalonia.Controls.Grid");
        ContentControlType = Find("Avalonia.Controls.ContentControl");
        ButtonType = Find("Avalonia.Controls.Button");
        ToggleSwitchType = Find("Avalonia.Controls.ToggleSwitch");
        TextBoxType = Find("Avalonia.Controls.TextBox");
        EllipseType = Find("Avalonia.Controls.Shapes.Ellipse");

        PathIconType = Find("Avalonia.Controls.PathIcon");
        PathShapeType = Find("Avalonia.Controls.Shapes.Path");
        GeometryType = Find("Avalonia.Media.Geometry");

        // Fallback: search by name if namespace differs
        EllipseType ??= typeMap.Values.FirstOrDefault(t =>
            t.Name == "Ellipse" && t.Namespace?.StartsWith("Avalonia") == true && !t.IsAbstract);
        PathShapeType ??= typeMap.Values.FirstOrDefault(t =>
            t.Name == "Path" && t.Namespace?.Contains("Shapes") == true && !t.IsAbstract);

        SolidColorBrushType = Find("Avalonia.Media.SolidColorBrush");
        LinearGradientBrushType = Find("Avalonia.Media.LinearGradientBrush");
        GradientStopType = Find("Avalonia.Media.GradientStop");
        GradientStopsType = Find("Avalonia.Media.GradientStops");
        RelativePointType = Find("Avalonia.RelativePoint");
        RelativeUnitType = Find("Avalonia.RelativeUnit");
        ColorType = Find("Avalonia.Media.Color");
        ThicknessType = Find("Avalonia.Thickness");
        CornerRadiusType = Find("Avalonia.CornerRadius");

        ColumnDefinitionType = Find("Avalonia.Controls.ColumnDefinition");
        RowDefinitionType = Find("Avalonia.Controls.RowDefinition");
        GridLengthType = Find("Avalonia.Controls.GridLength");
        GridUnitTypeEnum = Find("Avalonia.Controls.GridUnitType");

        HorizontalAlignmentType = Find("Avalonia.Layout.HorizontalAlignment");
        VerticalAlignmentType = Find("Avalonia.Layout.VerticalAlignment");
        OrientationType = Find("Avalonia.Layout.Orientation");
        TextWrappingType = Find("Avalonia.Media.TextWrapping");
        FontWeightType = Find("Avalonia.Media.FontWeight");
        CursorType = Find("Avalonia.Input.Cursor");
        StandardCursorType = Find("Avalonia.Input.StandardCursorType");

        VisualExtensionsType = Find("Avalonia.VisualTree.VisualExtensions");
        VisualType = Find("Avalonia.Visual");
        TopLevelType = Find("Avalonia.Controls.TopLevel");

        // Image types
        ImageType = Find("Avalonia.Controls.Image");
        BitmapType = Find("Avalonia.Media.Imaging.Bitmap");
        StretchType = Find("Avalonia.Media.Stretch");

        // Overlay / Canvas / layout types
        OverlayLayerType = Find("Avalonia.Controls.Primitives.OverlayLayer");
        CanvasType = Find("Avalonia.Controls.Canvas");
        PointType = Find("Avalonia.Point");
        RectType = Find("Avalonia.Rect");

        // Resource system
        ResourceDictionaryType = Find("Avalonia.Controls.ResourceDictionary");
        // IResourceDictionary may be in different namespaces
        IResourceDictionaryType = Find("Avalonia.Controls.IResourceDictionary");

        // IClassicDesktopStyleApplicationLifetime - match by suffix
        foreach (var kv in typeMap)
        {
            if (kv.Key.EndsWith("IClassicDesktopStyleApplicationLifetime") && kv.Value.IsInterface)
            {
                DesktopLifetimeType = kv.Value;
                break;
            }
        }

        // Collect missing types for the wide event
        if (DesktopLifetimeType == null) missing.Add("DesktopLifetime");
        if (VisualExtensionsType == null) missing.Add("VisualExtensions");
        if (ToggleSwitchType == null) missing.Add("ToggleSwitch");
        if (EllipseType == null) missing.Add("Ellipse");
        if (VisualType == null) missing.Add("Visual");
        if (ImageType == null) missing.Add("Image");
        if (BitmapType == null) missing.Add("Bitmap");
        if (StretchType == null) missing.Add("Stretch");
        if (OverlayLayerType == null) missing.Add("OverlayLayer");
        if (CanvasType == null) missing.Add("Canvas");
    }

    private void ResolveMembers(WideEvent ev, List<string> missing)
    {
        var pub = BindingFlags.Public | BindingFlags.Instance;
        var stat = BindingFlags.Public | BindingFlags.Static;

        // Application
        _appCurrent = ApplicationType?.GetProperty("Current", BindingFlags.Public | BindingFlags.Static);
        _appLifetime = ApplicationType?.GetProperty("ApplicationLifetime", pub);

        // Desktop Lifetime
        _lifetimeMainWindow = DesktopLifetimeType?.GetProperty("MainWindow", pub);
        _lifetimeWindows = DesktopLifetimeType?.GetProperty("Windows", pub);

        // Dispatcher
        _dispatcherUIThread = DispatcherType?.GetProperty("UIThread", stat);

        // Find Post(Action, DispatcherPriority) - 2 params
        _dispatcherPost = DispatcherType?.GetMethods(pub)
            .Where(m => m.Name == "Post" && !m.IsGenericMethod)
            .FirstOrDefault(m =>
            {
                var p = m.GetParameters();
                return p.Length == 2 && p[0].ParameterType == typeof(Action);
            });

        // Fallback: Post(Action) - 1 param
        _dispatcherPost ??= DispatcherType?.GetMethods(pub)
            .Where(m => m.Name == "Post" && !m.IsGenericMethod)
            .FirstOrDefault(m =>
            {
                var p = m.GetParameters();
                return p.Length == 1 && p[0].ParameterType == typeof(Action);
            });

        // Fallback: InvokeAsync(Action)
        _dispatcherPost ??= DispatcherType?.GetMethods(pub)
            .Where(m => m.Name == "InvokeAsync" && !m.IsGenericMethod)
            .FirstOrDefault(m =>
            {
                var p = m.GetParameters();
                return p.Length == 1 && p[0].ParameterType == typeof(Action);
            });

        if (_dispatcherPost != null)
            ev.Set("dispatcher_post", _dispatcherPost.Name + "(" + _dispatcherPost.GetParameters().Length + ")");
        else
            missing.Add("Dispatcher.Post");

        // VisualExtensions.GetVisualChildren(Visual)
        _getVisualChildren = VisualExtensionsType?.GetMethods(stat)
            .FirstOrDefault(m => m.Name == "GetVisualChildren" && m.GetParameters().Length == 1);

        // Color.Parse(string)
        _colorParse = ColorType?.GetMethod("Parse", stat, null, new[] { typeof(string) }, null);

        // Panel.Children
        _panelChildren = PanelType?.GetProperty("Children", pub);

        // Control properties
        _controlTag = ControlType?.GetProperty("Tag", pub);
        _controlIsVisible = ControlType?.GetProperty("IsVisible", pub);
        _controlMargin = ControlType?.GetProperty("Margin", pub);
        _controlCursor = ControlType?.GetProperty("Cursor", pub);

        // TextBlock properties
        _textBlockText = TextBlockType?.GetProperty("Text", pub);
        _textBlockFontSize = TextBlockType?.GetProperty("FontSize", pub);
        _textBlockFontWeight = TextBlockType?.GetProperty("FontWeight", pub);
        _textBlockForeground = TextBlockType?.GetProperty("Foreground", pub);
        _textBlockTextWrapping = TextBlockType?.GetProperty("TextWrapping", pub);

        // Border properties
        _borderChild = BorderType?.GetProperty("Child", pub);
        _borderBackground = BorderType?.GetProperty("Background", pub);
        _borderCornerRadius = BorderType?.GetProperty("CornerRadius", pub);
        _borderBorderBrush = BorderType?.GetProperty("BorderBrush", pub);
        _borderBorderThickness = BorderType?.GetProperty("BorderThickness", pub);

        // ScrollViewer
        _scrollViewerContent = ScrollViewerType?.GetProperty("Content", pub);

        // StackPanel
        _stackPanelOrientation = StackPanelType?.GetProperty("Orientation", pub);
        _stackPanelSpacing = StackPanelType?.GetProperty("Spacing", pub);

        // ContentControl
        _contentControlContent = ContentControlType?.GetProperty("Content", pub);

        // Grid static methods
        _gridSetColumn = GridType?.GetMethod("SetColumn", stat);
        _gridGetColumn = GridType?.GetMethod("GetColumn", stat);
        _gridSetRow = GridType?.GetMethod("SetRow", stat);
        _gridGetRow = GridType?.GetMethod("GetRow", stat);

        // ToggleSwitch
        _toggleSwitchIsChecked = ToggleSwitchType?.GetProperty("IsChecked", pub);

        // TextBox
        var staticPub = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
        _textBoxTextProperty = TextBoxType?.GetField("TextProperty", staticPub);
        _textBoxWatermarkProperty = TextBoxType?.GetField("WatermarkProperty", staticPub);
        _textBoxMaxLengthProperty = TextBoxType?.GetField("MaxLengthProperty", staticPub);

        // OverlayLayer.GetOverlayLayer(Visual) - static method
        _overlayGetOverlayLayer = OverlayLayerType?.GetMethod("GetOverlayLayer", stat);

        // Canvas.SetLeft / Canvas.SetTop - static methods
        _canvasSetLeft = CanvasType?.GetMethod("SetLeft", stat);
        _canvasSetTop = CanvasType?.GetMethod("SetTop", stat);

        // Layoutable.Bounds (inherited by all controls via Control -> Layoutable)
        _layoutableBounds = ControlType?.GetProperty("Bounds", pub);

        // TranslatePoint: try instance method on Visual first (Avalonia 11+),
        // then fall back to static extension on VisualExtensions.
        // Walk the Control type hierarchy to find it (may be on Visual base class).
        var translateSearch = ControlType;
        while (translateSearch != null && _translatePoint == null)
        {
            _translatePoint = translateSearch.GetMethods(pub | BindingFlags.DeclaredOnly)
                .FirstOrDefault(m => m.Name == "TranslatePoint" && m.GetParameters().Length == 2);
            translateSearch = translateSearch.BaseType;
        }
        _translatePoint ??= VisualExtensionsType?.GetMethods(stat)
            .FirstOrDefault(m => m.Name == "TranslatePoint" && m.GetParameters().Length == 3);
        // Last resort: search all Avalonia types for a matching TranslatePoint
        if (_translatePoint == null)
        {
            ev.Set("translate_point_fallback", true);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var asmName = asm.GetName().Name ?? "";
                if (!asmName.StartsWith("Avalonia", StringComparison.OrdinalIgnoreCase)) continue;
                try
                {
                    foreach (var t in asm.GetTypes())
                    {
                        if (t.IsAbstract && t.IsSealed) // static class
                        {
                            var m = t.GetMethods(stat)
                                .FirstOrDefault(mi => mi.Name == "TranslatePoint");
                            if (m != null)
                                _translatePoint = m;
                        }
                    }
                }
                catch { }
                if (_translatePoint != null) break;
            }
        }

        // Resource system
        _appResources = ApplicationType?.GetProperty("Resources", pub);
        // MergedDictionaries is on IResourceDictionary or ResourceDictionary
        if (IResourceDictionaryType != null)
            _resourcesMergedDicts = IResourceDictionaryType.GetProperty("MergedDictionaries", pub);
        // Fallback: try on the concrete Resources object type later at runtime

        if (ResourceDictionaryType == null) missing.Add("ResourceDictionary");
        if (IResourceDictionaryType == null) missing.Add("IResourceDictionary");
        if (_appResources == null) missing.Add("App.Resources");

        // Image
        _imageSource = ImageType?.GetProperty("Source", pub);
        _imageStretch = ImageType?.GetProperty("Stretch", pub);

        // Opacity and IsHitTestVisible
        _controlOpacity = ControlType?.GetProperty("Opacity", pub)
            ?? VisualType?.GetProperty("Opacity", pub);
        _controlIsHitTestVisible = ControlType?.GetProperty("IsHitTestVisible", pub);

        if (_overlayGetOverlayLayer == null) missing.Add("OverlayLayer.GetOverlayLayer");
        if (_canvasSetLeft == null) missing.Add("Canvas.SetLeft");
        if (_layoutableBounds == null) missing.Add("Layoutable.Bounds");
        if (_translatePoint != null)
            ev.Set("translate_point", (_translatePoint.IsStatic ? "static" : "instance") + ":" + _translatePoint.DeclaringType?.Name + "." + _translatePoint.Name + "(" + _translatePoint.GetParameters().Length + ")");
        else
            missing.Add("TranslatePoint");
    }

    // ===== Core access =====

    public object? GetAppCurrent() => _appCurrent?.GetValue(null);

    public object? GetMainWindow()
    {
        var app = GetAppCurrent();
        if (app == null) return null;
        var lifetime = _appLifetime?.GetValue(app);
        if (lifetime == null) return null;
        return _lifetimeMainWindow?.GetValue(lifetime);
    }

    /// <summary>
    /// Returns the DPI scale factor from the main window (e.g. 1.0 for 100%, 1.5 for 150%).
    /// Falls back to 1.0 if unavailable.
    /// </summary>
    public double GetRenderScaling()
    {
        try
        {
            var window = GetMainWindow();
            var val = window?.GetType().GetProperty("RenderScaling")?.GetValue(window);
            if (val is double d && d > 0) return d;
        }
        catch { }
        return 1.0;
    }

    /// <summary>
    /// Returns all open windows (main + popups). Avalonia popups like user profile
    /// cards and context menus live in separate Window objects, not in MainWindow's tree.
    /// </summary>
    public List<object> GetAllWindows()
    {
        var result = new List<object>();
        try
        {
            var app = GetAppCurrent();
            if (app == null) return result;
            var lifetime = _appLifetime?.GetValue(app);
            if (lifetime == null) return result;
            var windows = _lifetimeWindows?.GetValue(lifetime);
            if (windows is IEnumerable enumerable)
            {
                foreach (var w in enumerable)
                {
                    if (w != null) result.Add(w);
                }
            }
        }
        catch { }
        return result;
    }

    private Type? _windowImplType;
    private PropertyInfo? _windowImplOwner;
    private FieldInfo? _windowImplSInstances;
    private bool _windowImplResolved;

    /// <summary>
    /// Returns all open TopLevel instances (MainWindow + PopupRoot + any dialogs).
    /// Uses Avalonia's internal WindowImpl.s_instances list to find all native windows,
    /// then gets .Owner (IInputRoot = TopLevel) for each.
    /// </summary>
    public List<object> GetAllTopLevels()
    {
        if (!_windowImplResolved)
        {
            _windowImplResolved = true;
            ResolveWindowImpl();
        }

        var result = new List<object>();

        if (_windowImplType == null || _windowImplOwner == null || _windowImplSInstances == null)
        {
            // Fallback: just MainWindow
            var mw = GetMainWindow();
            if (mw != null) result.Add(mw);
            return result;
        }

        try
        {
            var instances = _windowImplSInstances.GetValue(null);
            if (instances is IEnumerable enumerable)
            {
                foreach (var impl in enumerable)
                {
                    if (impl == null) continue;
                    try
                    {
                        var owner = _windowImplOwner.GetValue(impl);
                        if (owner != null && !result.Contains(owner))
                            result.Add(owner);
                    }
                    catch { }
                }
            }
        }
        catch { }

        if (result.Count == 0)
        {
            var mw = GetMainWindow();
            if (mw != null) result.Add(mw);
        }

        return result;
    }

    private void ResolveWindowImpl()
    {
        try
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!(asm.FullName?.Contains("Avalonia") == true)) continue;
                try
                {
                    foreach (var type in asm.GetTypes())
                    {
                        if (type.Name != "WindowImpl") continue;
                        _windowImplType = type;
                        _windowImplOwner = type.GetProperty("Owner",
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        _windowImplSInstances = type.GetField("s_instances",
                            BindingFlags.NonPublic | BindingFlags.Static);

                        Logger.Log("Reflection", "WindowImpl resolved: Owner=" +
                            (_windowImplOwner != null) + " s_instances=" + (_windowImplSInstances != null));
                        return;
                    }
                }
                catch { }
            }
        }
        catch { }
    }

    public void RunOnUIThread(Action action)
    {
        var dispatcher = _dispatcherUIThread?.GetValue(null);
        if (dispatcher == null || _dispatcherPost == null)
        {
            action();
            return;
        }

        var pcount = _dispatcherPost.GetParameters().Length;
        if (pcount == 1)
        {
            _dispatcherPost.Invoke(dispatcher, new object[] { action });
        }
        else if (pcount == 2)
        {
            // DispatcherPriority in Avalonia 11+ is a struct, not an enum.
            // Get the "Normal" value via static property or field on the type.
            var priorityType = _dispatcherPost.GetParameters()[1].ParameterType;
            object? normalPriority = null;

            // Try static property first (Avalonia 11+)
            var normalProp = priorityType.GetProperty("Normal", BindingFlags.Public | BindingFlags.Static);
            if (normalProp != null)
                normalPriority = normalProp.GetValue(null);

            // Try static field
            if (normalPriority == null)
            {
                var normalField = priorityType.GetField("Normal", BindingFlags.Public | BindingFlags.Static);
                if (normalField != null)
                    normalPriority = normalField.GetValue(null);
            }

            // Try enum parse as fallback
            if (normalPriority == null && priorityType.IsEnum)
                normalPriority = Enum.Parse(priorityType, "Normal");

            // Last resort: use default value of the struct/enum
            if (normalPriority == null)
                normalPriority = priorityType.IsValueType ? Activator.CreateInstance(priorityType) : null;

            if (normalPriority != null)
                _dispatcherPost.Invoke(dispatcher, new object[] { action, normalPriority });
            else
                action(); // fallback: run inline
        }
        else
        {
            action();
        }
    }

    public IEnumerable<object> GetVisualChildren(object visual)
    {
        if (_getVisualChildren == null) yield break;

        object? result;
        try { result = _getVisualChildren.Invoke(null, new[] { visual }); }
        catch { yield break; }

        if (result is IEnumerable enumerable)
        {
            foreach (var child in enumerable)
            {
                if (child != null) yield return child;
            }
        }
    }

    /// <summary>
    /// Get logical children of a control. Popup controls are logical children,
    /// not visual, so we need this to find open popups in the tree.
    /// Uses ILogical.LogicalChildren or StyledElement.LogicalChildren.
    /// </summary>
    public IEnumerable<object> GetLogicalChildren(object control)
    {
        // Try LogicalChildren property (available on StyledElement/Control)
        var prop = control.GetType().GetProperty("LogicalChildren",
            BindingFlags.Public | BindingFlags.Instance);
        if (prop == null) yield break;

        object? result;
        try { result = prop.GetValue(control); }
        catch { yield break; }

        if (result is IEnumerable enumerable)
        {
            foreach (var child in enumerable)
            {
                if (child != null) yield return child;
            }
        }
    }

    // ===== Control creation =====

    public object? CreateTextBlock(string text, double fontSize = 14, string? foregroundHex = null, string? fontWeight = null)
    {
        if (TextBlockType == null) return null;

        var tb = Activator.CreateInstance(TextBlockType);
        _textBlockText?.SetValue(tb, text);
        _textBlockFontSize?.SetValue(tb, fontSize);

        if (foregroundHex != null)
        {
            var brush = CreateBrush(foregroundHex);
            if (brush != null) _textBlockForeground?.SetValue(tb, brush);
        }

        if (fontWeight != null) SetFontWeight(tb, fontWeight);

        return tb;
    }

    /// <summary>
    /// Create an icon from SVG path data string. Prefers Avalonia.Controls.Shapes.Path
    /// with Stretch.Uniform (scales any viewbox to fit), falls back to PathIcon.
    /// </summary>
    public object? CreatePathIcon(string pathData, double size, string? foregroundHex = null)
    {
        if (GeometryType == null) return null;
        try
        {
            // Geometry.Parse(string) is a static method on Avalonia.Media.Geometry
            var parseMethod = GeometryType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null);
            if (parseMethod == null) return null;

            var geometry = parseMethod.Invoke(null, new object[] { pathData });
            if (geometry == null) return null;

            // Prefer Path shape — has explicit Stretch.Uniform to scale any viewbox
            if (PathShapeType != null && StretchType != null)
            {
                var path = Activator.CreateInstance(PathShapeType);
                if (path != null)
                {
                    path.GetType().GetProperty("Data")?.SetValue(path, geometry);
                    path.GetType().GetProperty("Width")?.SetValue(path, size);
                    path.GetType().GetProperty("Height")?.SetValue(path, size);

                    // Stretch.Uniform — scale geometry to fit size, preserving aspect ratio
                    var uniform = Enum.Parse(StretchType, "Uniform");
                    path.GetType().GetProperty("Stretch")?.SetValue(path, uniform);

                    if (foregroundHex != null)
                    {
                        var brush = CreateBrush(foregroundHex);
                        if (brush != null) path.GetType().GetProperty("Fill")?.SetValue(path, brush);
                    }

                    return path;
                }
            }

            // Fallback: PathIcon (no Stretch — may clip if viewbox != size)
            if (PathIconType != null)
            {
                var icon = Activator.CreateInstance(PathIconType);
                if (icon == null) return null;

                PathIconType.GetProperty("Data")?.SetValue(icon, geometry);
                icon.GetType().GetProperty("Width")?.SetValue(icon, size);
                icon.GetType().GetProperty("Height")?.SetValue(icon, size);

                if (foregroundHex != null)
                {
                    var brush = CreateBrush(foregroundHex);
                    if (brush != null) icon.GetType().GetProperty("Foreground")?.SetValue(icon, brush);
                }

                return icon;
            }

            return null;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"CreatePathIcon failed: {ex.Message}");
            return null;
        }
    }

    public object? CreateStackPanel(bool vertical = true, double spacing = 0)
    {
        if (StackPanelType == null) return null;

        var sp = Activator.CreateInstance(StackPanelType);

        if (OrientationType != null)
        {
            var orientation = Enum.Parse(OrientationType, vertical ? "Vertical" : "Horizontal");
            _stackPanelOrientation?.SetValue(sp, orientation);
        }

        if (spacing > 0)
            _stackPanelSpacing?.SetValue(sp, spacing);

        return sp;
    }

    public object? CreateBorder(string? bgHex = null, double cornerRadius = 0, object? child = null)
    {
        if (BorderType == null) return null;

        var border = Activator.CreateInstance(BorderType);

        if (bgHex != null)
        {
            var brush = CreateBrush(bgHex);
            if (brush != null) _borderBackground?.SetValue(border, brush);
        }

        if (cornerRadius > 0 && CornerRadiusType != null)
        {
            var cr = Activator.CreateInstance(CornerRadiusType, cornerRadius);
            _borderCornerRadius?.SetValue(border, cr);
        }

        if (child != null)
            _borderChild?.SetValue(border, child);

        return border;
    }

    public object? CreateEllipse(double width, double height, string? fillHex = null,
        string? strokeHex = null, double strokeThickness = 0)
    {
        if (EllipseType == null) return null;
        try
        {
            var ellipse = Activator.CreateInstance(EllipseType);
            ellipse?.GetType().GetProperty("Width")?.SetValue(ellipse, width);
            ellipse?.GetType().GetProperty("Height")?.SetValue(ellipse, height);
            if (fillHex != null)
            {
                var brush = CreateBrush(fillHex);
                if (brush != null)
                    ellipse?.GetType().GetProperty("Fill")?.SetValue(ellipse, brush);
            }
            if (strokeHex != null && strokeThickness > 0)
            {
                var brush = CreateBrush(strokeHex);
                if (brush != null)
                    ellipse?.GetType().GetProperty("Stroke")?.SetValue(ellipse, brush);
                ellipse?.GetType().GetProperty("StrokeThickness")?.SetValue(ellipse, strokeThickness);
            }
            return ellipse;
        }
        catch { return null; }
    }

    /// <summary>Set the Fill brush on a Shape (Ellipse, Path, etc).</summary>
    public void SetFill(object? shape, string? hex)
    {
        if (shape == null) return;
        if (hex == null) { shape.GetType().GetProperty("Fill")?.SetValue(shape, null); return; }
        var brush = CreateBrush(hex);
        if (brush != null) shape.GetType().GetProperty("Fill")?.SetValue(shape, brush);
    }

    public object? CreateTextBox(string? watermark = null, string? text = null, int maxLength = 0)
    {
        if (TextBoxType == null) return null;

        try
        {
            var tb = Activator.CreateInstance(TextBoxType);
            if (tb == null) return null;

            // CLR property setters are trimmed in Root's single-file binary.
            // Use Avalonia's SetValue(AvaloniaProperty, object) instead.
            if (watermark != null) SetAvaloniaProperty(tb, _textBoxWatermarkProperty, watermark);
            if (text != null) SetAvaloniaProperty(tb, _textBoxTextProperty, text);
            if (maxLength > 0) SetAvaloniaProperty(tb, _textBoxMaxLengthProperty, maxLength);
            return tb;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"CreateTextBox error: {ex.Message}");
            return null;
        }
    }

    public string? GetTextBoxText(object? textBox)
    {
        if (textBox == null) return null;
        return GetAvaloniaProperty(textBox, _textBoxTextProperty) as string;
    }

    public void SetTextBoxText(object? textBox, string text)
    {
        if (textBox == null) return;
        SetAvaloniaProperty(textBox, _textBoxTextProperty, text);
    }

    /// <summary>
    /// Set a value via the Avalonia property system, bypassing trimmed CLR setters.
    /// avaloniaPropertyField is a FieldInfo for the static AvaloniaProperty (e.g. TextBox.TextProperty).
    /// Uses SetValue(AvaloniaProperty, object?, BindingPriority) -- the 3-param non-generic overload.
    /// </summary>
    private void SetAvaloniaProperty(object control, FieldInfo? avaloniaPropertyField, object value)
    {
        if (avaloniaPropertyField == null) return;
        var avProp = avaloniaPropertyField.GetValue(null);
        if (avProp == null) return;

        // Ensure BindingPriority is resolved
        EnsureBindingPriorityResolved();

        // Ensure the 3-param SetValue method is cached
        if (_setValueWithPriority == null && _bindingPriorityStyle != null)
        {
            var methods = control.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == "SetValue" && !m.IsGenericMethod && m.GetParameters().Length == 3);
            foreach (var method in methods)
            {
                var parms = method.GetParameters();
                if (parms[0].ParameterType.IsAssignableFrom(avProp.GetType())
                    && parms[2].ParameterType.Name == "BindingPriority")
                {
                    _setValueWithPriority = method;
                    break;
                }
            }
        }

        if (_setValueWithPriority != null && _bindingPriorityStyle != null)
        {
            _setValueWithPriority.Invoke(control, new[] { avProp, value, _bindingPriorityStyle });
        }
        else
        {
            Logger.Log("Reflection", $"SetAvaloniaProperty FAILED: method={_setValueWithPriority != null} priority={_bindingPriorityStyle != null} field={avaloniaPropertyField.Name}");
        }
    }

    /// <summary>Resolve BindingPriority.LocalValue once.</summary>
    private void EnsureBindingPriorityResolved()
    {
        if (_priorityResolved) return;
        _priorityResolved = true;
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            var bpType = asm.GetType("Avalonia.Data.BindingPriority");
            if (bpType != null)
            {
                _bindingPriorityStyle = Enum.ToObject(bpType, 0); // LocalValue = 0
                break;
            }
        }
    }

    /// <summary>
    /// Get a value via the Avalonia property system, bypassing trimmed CLR getters.
    /// </summary>
    private object? GetAvaloniaProperty(object control, FieldInfo? avaloniaPropertyField)
    {
        if (avaloniaPropertyField == null) return null;
        var avProp = avaloniaPropertyField.GetValue(null);
        if (avProp == null) return null;

        // Find GetValue(AvaloniaProperty) - non-generic, 1-param overload
        var methods = control.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.Name == "GetValue" && !m.IsGenericMethod && m.GetParameters().Length == 1);
        foreach (var method in methods)
        {
            var parms = method.GetParameters();
            if (parms[0].ParameterType.IsAssignableFrom(avProp.GetType()))
            {
                return method.Invoke(control, new[] { avProp });
            }
        }
        return null;
    }

    /// <summary>
    /// Get a value via the Avalonia property system by static field name, bypassing trimmed CLR getters.
    /// </summary>
    public object? GetAvaloniaPropertyByName(object? control, string propertyFieldName)
    {
        if (control == null) return null;
        var field = control.GetType().GetField(propertyFieldName,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        return GetAvaloniaProperty(control, field);
    }

    /// <summary>
    /// Set a value via the Avalonia property system by static field name, bypassing trimmed CLR setters.
    /// </summary>
    public void SetAvaloniaPropertyByName(object control, string propertyFieldName, object value)
    {
        var field = control.GetType().GetField(propertyFieldName,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        if (field != null) SetAvaloniaProperty(control, field, value);
    }

    public object? CreatePanel()
    {
        if (PanelType == null) return null;
        return Activator.CreateInstance(PanelType);
    }

    public object? CreateScrollViewer(object? content = null)
    {
        if (ScrollViewerType == null) return null;

        var sv = Activator.CreateInstance(ScrollViewerType);
        if (content != null)
            _scrollViewerContent?.SetValue(sv, content);

        // Disable horizontal scrollbar so content stretches to fill available width
        // (without this, ScrollViewer may give content infinite horizontal space)
        try
        {
            var visType = ScrollViewerType.Assembly.GetType("Avalonia.Controls.Primitives.ScrollBarVisibility");
            if (visType != null)
                sv?.GetType().GetProperty("HorizontalScrollBarVisibility")?.SetValue(sv, Enum.Parse(visType, "Disabled"));
        }
        catch { }

        return sv;
    }

    public object? CreateBrush(string hex)
    {
        if (_colorParse == null || SolidColorBrushType == null) return null;

        try
        {
            var color = _colorParse.Invoke(null, new object[] { hex });
            // SolidColorBrush(Color) constructor is trimmed in Root's single-file binary.
            // Use parameterless constructor + Color property setter instead.
            var brush = Activator.CreateInstance(SolidColorBrushType);
            SolidColorBrushType.GetProperty("Color")?.SetValue(brush, color);
            return brush;
        }
        catch { return null; }
    }

    // ===== Property setters =====

    public void SetMargin(object? control, double left, double top, double right, double bottom)
    {
        if (control == null || ThicknessType == null) return;
        var thickness = Activator.CreateInstance(ThicknessType, left, top, right, bottom);
        _controlMargin?.SetValue(control, thickness);
    }

    public void SetPadding(object? control, double left, double top, double right, double bottom)
    {
        if (control == null || ThicknessType == null) return;
        var thickness = Activator.CreateInstance(ThicknessType, left, top, right, bottom);
        // Padding exists on Border (Decorator), TemplatedControl, etc. - search the actual type
        var paddingProp = control.GetType().GetProperty("Padding");
        paddingProp?.SetValue(control, thickness);
    }

    /// <summary>
    /// Read the current Margin from a control as a (Left, Top, Right, Bottom) tuple.
    /// Returns null if the control has no Margin or it cannot be read.
    /// </summary>
    public (double Left, double Top, double Right, double Bottom)? GetMargin(object? control)
    {
        if (control == null) return null;
        try
        {
            var val = _controlMargin?.GetValue(control);
            if (val == null) return null;
            var t = val.GetType();
            var left = (double)(t.GetProperty("Left")?.GetValue(val) ?? 0.0);
            var top = (double)(t.GetProperty("Top")?.GetValue(val) ?? 0.0);
            var right = (double)(t.GetProperty("Right")?.GetValue(val) ?? 0.0);
            var bottom = (double)(t.GetProperty("Bottom")?.GetValue(val) ?? 0.0);
            return (left, top, right, bottom);
        }
        catch { return null; }
    }

    /// <summary>
    /// Set per-corner CornerRadius on a Border control.
    /// Avalonia CornerRadius(topLeft, topRight, bottomRight, bottomLeft).
    /// </summary>
    public void SetCornerRadius(object? border, double topLeft, double topRight, double bottomRight, double bottomLeft)
    {
        if (border == null || CornerRadiusType == null) return;
        try
        {
            var cr = Activator.CreateInstance(CornerRadiusType, topLeft, topRight, bottomRight, bottomLeft);
            _borderCornerRadius?.SetValue(border, cr);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"SetCornerRadius error: {ex.Message}");
        }
    }

    public void SetTag(object? control, string tag) => _controlTag?.SetValue(control, tag);
    public string? GetTag(object? control) => _controlTag?.GetValue(control) as string;

    public void SetIsVisible(object? control, bool visible) => _controlIsVisible?.SetValue(control, visible);
    public bool GetIsVisible(object? control) => _controlIsVisible?.GetValue(control) is true;

    public void SetBackground(object? control, string? hex)
    {
        if (control == null) return;
        var bgProp = control.GetType().GetProperty("Background");
        if (bgProp == null) return;

        if (hex == null)
        {
            bgProp.SetValue(control, null);
            return;
        }

        var brush = CreateBrush(hex);
        if (brush != null) bgProp.SetValue(control, brush);
    }

    public void SetForeground(object? control, string hex)
    {
        if (control == null) return;
        var brush = CreateBrush(hex);
        if (brush == null) return;
        var fgProp = control.GetType().GetProperty("Foreground");
        fgProp?.SetValue(control, brush);
    }

    public void SetFontWeight(object? control, string weight)
    {
        if (control == null || FontWeightType == null) return;

        // FontWeight in Avalonia is a struct with static properties (Bold, Normal, SemiBold, etc.)
        var weightProp = FontWeightType.GetProperty(weight, BindingFlags.Public | BindingFlags.Static);
        if (weightProp != null)
        {
            _textBlockFontWeight?.SetValue(control, weightProp.GetValue(null));
            return;
        }

        // Try as field (some Avalonia versions use static readonly fields)
        var weightField = FontWeightType.GetField(weight, BindingFlags.Public | BindingFlags.Static);
        if (weightField != null)
        {
            _textBlockFontWeight?.SetValue(control, weightField.GetValue(null));
            return;
        }

        // Try enum parse
        try
        {
            var parsed = Enum.Parse(FontWeightType, weight);
            _textBlockFontWeight?.SetValue(control, parsed);
        }
        catch { }
    }

    /// <summary>
    /// Set FontWeight by numeric value (e.g., 450 for between Normal/400 and Medium/500).
    /// Avalonia FontWeight is a struct with a constructor taking int.
    /// </summary>
    public void SetFontWeightNumeric(object? control, int weight)
    {
        if (control == null || FontWeightType == null) return;
        try
        {
            var fw = Activator.CreateInstance(FontWeightType, weight);
            _textBlockFontWeight?.SetValue(control, fw);
        }
        catch { }
    }

    public void SetTextWrapping(object? control, string wrapping)
    {
        if (control == null || TextWrappingType == null) return;
        try
        {
            var val = Enum.Parse(TextWrappingType, wrapping);
            _textBlockTextWrapping?.SetValue(control, val);
        }
        catch { }
    }

    public void SetHorizontalAlignment(object? control, string alignment)
    {
        if (control == null || HorizontalAlignmentType == null) return;
        try
        {
            var val = Enum.Parse(HorizontalAlignmentType, alignment);
            control.GetType().GetProperty("HorizontalAlignment")?.SetValue(control, val);
        }
        catch { }
    }

    public void SetVerticalAlignment(object? control, string alignment)
    {
        if (control == null || VerticalAlignmentType == null) return;
        try
        {
            var val = Enum.Parse(VerticalAlignmentType, alignment);
            control.GetType().GetProperty("VerticalAlignment")?.SetValue(control, val);
        }
        catch { }
    }

    public void SetCursorHand(object? control)
    {
        if (control == null || CursorType == null || StandardCursorType == null) return;
        try
        {
            var hand = Enum.Parse(StandardCursorType, "Hand");
            var cursor = Activator.CreateInstance(CursorType, hand);
            _controlCursor?.SetValue(control, cursor);
        }
        catch { }
    }

    // ===== Text property access =====

    public string? GetText(object? control) => _textBlockText?.GetValue(control) as string;
    public double? GetFontSize(object? control) => _textBlockFontSize?.GetValue(control) as double?;
    public object? GetFontWeight(object? control) => _textBlockFontWeight?.GetValue(control);
    public object? GetForeground(object? control) => _textBlockForeground?.GetValue(control);
    public object? GetFontFamily(object? control)
    {
        if (control == null) return null;
        return control.GetType().GetProperty("FontFamily")?.GetValue(control);
    }

    /// <summary>
    /// Set FontFamily on a TextBlock using an Avalonia font family string.
    /// Supports both simple names ("CircularXX TT") and avares URIs.
    /// </summary>
    public void SetFontFamily(object? control, object? fontFamily)
    {
        if (control == null || fontFamily == null) return;
        try
        {
            control.GetType().GetProperty("FontFamily")?.SetValue(control, fontFamily);
        }
        catch { }
    }

    // ===== Panel children helpers =====

    public IList? GetChildren(object? panel)
    {
        if (panel == null) return null;
        try { return _panelChildren?.GetValue(panel) as IList; }
        catch { return null; } // non-Panel types (ItemsControl, etc.) don't have Panel.Children
    }

    public void AddChild(object? panel, object? child)
    {
        if (panel == null || child == null) return;
        GetChildren(panel)?.Add(child);
    }

    public void InsertChild(object? panel, int index, object? child)
    {
        if (panel == null || child == null) return;
        GetChildren(panel)?.Insert(index, child);
    }

    public int GetChildCount(object? panel) => GetChildren(panel)?.Count ?? 0;

    public object? GetChild(object? panel, int index)
    {
        var children = GetChildren(panel);
        if (children == null || index < 0 || index >= children.Count) return null;
        return children[index];
    }

    public void RemoveChild(object? panel, object? child)
    {
        if (panel == null || child == null) return;
        GetChildren(panel)?.Remove(child);
    }

    public void SetBorderChild(object? border, object? child) => _borderChild?.SetValue(border, child);
    public object? GetBorderChild(object? border) => _borderChild?.GetValue(border);

    public void SetBorderBrush(object? border, string hex)
    {
        if (border == null || _borderBorderBrush == null) return;
        var brush = CreateBrush(hex);
        if (brush != null) _borderBorderBrush.SetValue(border, brush);
    }

    public void SetBorderThickness(object? border, double uniform)
    {
        if (border == null || _borderBorderThickness == null || ThicknessType == null) return;
        var thickness = Activator.CreateInstance(ThicknessType, uniform, uniform, uniform, uniform);
        _borderBorderThickness.SetValue(border, thickness);
    }

    public void SetScrollViewerContent(object? sv, object? content) => _scrollViewerContent?.SetValue(sv, content);

    public void SetGridColumn(object? control, int column)
    {
        if (control == null || _gridSetColumn == null) return;
        try { _gridSetColumn.Invoke(null, new[] { control, (object)column }); }
        catch { }
    }
    public int GetGridColumn(object? control)
    {
        if (control == null || _gridGetColumn == null) return 0;
        try { return (int)_gridGetColumn.Invoke(null, new[] { control })!; }
        catch { return 0; }
    }
    public void SetGridRow(object? control, int row) => _gridSetRow?.Invoke(null, new[] { control, (object)row });
    public int GetGridRow(object? control)
    {
        if (control == null || _gridGetRow == null) return 0;
        try { return (int)_gridGetRow.Invoke(null, new[] { control })!; }
        catch { return 0; }
    }

    // ===== Grid creation =====

    public object? CreateGrid()
    {
        if (GridType == null) return null;
        return Activator.CreateInstance(GridType);
    }

    /// <summary>
    /// Add a star-width column definition to a Grid.
    /// </summary>
    public void AddGridColumn(object? grid, double starWidth = 1.0)
    {
        if (grid == null || ColumnDefinitionType == null || GridLengthType == null || GridUnitTypeEnum == null) return;
        try
        {
            // GridUnitType: Auto=0, Pixel=1, Star=2 -- use Parse to avoid wrong-value bugs
            var starUnit = Enum.Parse(GridUnitTypeEnum, "Star");
            // new GridLength(starWidth, GridUnitType.Star)
            var gridLength = Activator.CreateInstance(GridLengthType, starWidth, starUnit);
            // new ColumnDefinition { Width = gridLength }
            var colDef = Activator.CreateInstance(ColumnDefinitionType);
            ColumnDefinitionType.GetProperty("Width")?.SetValue(colDef, gridLength);

            // grid.ColumnDefinitions.Add(colDef)
            var colDefs = grid.GetType().GetProperty("ColumnDefinitions")?.GetValue(grid);
            if (colDefs is System.Collections.IList colList)
                colList.Add(colDef);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"AddGridColumn error: {ex.Message}");
        }
    }

    public void AddGridColumnStar(object? grid, double starWidth = 1.0) => AddGridColumn(grid, starWidth);

    public void AddGridColumnAuto(object? grid)
    {
        if (grid == null || ColumnDefinitionType == null || GridLengthType == null || GridUnitTypeEnum == null) return;
        try
        {
            var autoUnit = Enum.Parse(GridUnitTypeEnum, "Auto");
            var gridLength = Activator.CreateInstance(GridLengthType, 0d, autoUnit);
            var colDef = Activator.CreateInstance(ColumnDefinitionType);
            ColumnDefinitionType.GetProperty("Width")?.SetValue(colDef, gridLength);
            var colDefs = grid.GetType().GetProperty("ColumnDefinitions")?.GetValue(grid);
            if (colDefs is System.Collections.IList colList)
                colList.Add(colDef);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"AddGridColumnAuto error: {ex.Message}");
        }
    }

    public void AddGridColumnPixel(object? grid, double pixels)
    {
        if (grid == null || ColumnDefinitionType == null || GridLengthType == null || GridUnitTypeEnum == null) return;
        try
        {
            var pixelUnit = Enum.Parse(GridUnitTypeEnum, "Pixel");
            var gridLength = Activator.CreateInstance(GridLengthType, pixels, pixelUnit);
            var colDef = Activator.CreateInstance(ColumnDefinitionType);
            ColumnDefinitionType.GetProperty("Width")?.SetValue(colDef, gridLength);
            var colDefs = grid.GetType().GetProperty("ColumnDefinitions")?.GetValue(grid);
            if (colDefs is System.Collections.IList colList)
                colList.Add(colDef);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"AddGridColumnPixel error: {ex.Message}");
        }
    }

    /// <summary>
    /// Add an auto-height row definition to a Grid.
    /// </summary>
    public void AddGridRowAuto(object? grid)
    {
        if (grid == null || RowDefinitionType == null || GridLengthType == null || GridUnitTypeEnum == null) return;
        try
        {
            var autoUnit = Enum.Parse(GridUnitTypeEnum, "Auto");
            var gridLength = Activator.CreateInstance(GridLengthType, 0d, autoUnit);
            var rowDef = Activator.CreateInstance(RowDefinitionType);
            RowDefinitionType.GetProperty("Height")?.SetValue(rowDef, gridLength);

            var rowDefs = grid.GetType().GetProperty("RowDefinitions")?.GetValue(grid);
            if (rowDefs is System.Collections.IList rowList)
                rowList.Add(rowDef);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"AddGridRowAuto error: {ex.Message}");
        }
    }

    /// <summary>
    /// Add a star-height row definition to a Grid.
    /// </summary>
    public void AddGridRowStar(object? grid, double starHeight = 1.0)
    {
        if (grid == null || RowDefinitionType == null || GridLengthType == null || GridUnitTypeEnum == null) return;
        try
        {
            var starUnit = Enum.Parse(GridUnitTypeEnum, "Star");
            var gridLength = Activator.CreateInstance(GridLengthType, starHeight, starUnit);
            var rowDef = Activator.CreateInstance(RowDefinitionType);
            RowDefinitionType.GetProperty("Height")?.SetValue(rowDef, gridLength);

            var rowDefs = grid.GetType().GetProperty("RowDefinitions")?.GetValue(grid);
            if (rowDefs is System.Collections.IList rowList)
                rowList.Add(rowDef);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"AddGridRowStar error: {ex.Message}");
        }
    }

    // ===== Type checking =====

    public bool IsTextBlock(object? obj) => obj != null && TextBlockType?.IsAssignableFrom(obj.GetType()) == true;
    public bool IsPanel(object? obj) => obj != null && PanelType?.IsAssignableFrom(obj.GetType()) == true;
    public bool IsBorder(object? obj) => obj != null && BorderType?.IsAssignableFrom(obj.GetType()) == true;
    public bool IsGrid(object? obj) => obj != null && GridType?.IsAssignableFrom(obj.GetType()) == true;
    public bool IsScrollViewer(object? obj) => obj != null && ScrollViewerType?.IsAssignableFrom(obj.GetType()) == true;

    // ===== Event subscription via Expression.Lambda =====

    public Delegate? SubscribeEvent(object control, string eventName, Action callback)
    {
        try
        {
            var type = control.GetType();
            var eventInfo = type.GetEvent(eventName);
            if (eventInfo == null)
            {
                Logger.Log("Reflection", $"Event '{eventName}' not found on {type.Name}");
                return null;
            }

            var handlerType = eventInfo.EventHandlerType!;
            var invokeMethod = handlerType.GetMethod("Invoke")!;
            var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();

            var p0 = Expression.Parameter(paramTypes[0], "sender");
            var p1 = Expression.Parameter(paramTypes[1], "e");
            var callbackExpr = Expression.Constant(callback);
            var invokeExpr = Expression.Invoke(callbackExpr);
            var lambda = Expression.Lambda(handlerType, invokeExpr, p0, p1);
            var handler = lambda.Compile();

            eventInfo.AddEventHandler(control, handler);
            return handler;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"SubscribeEvent({eventName}) failed: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Unsubscribe a delegate previously returned by SubscribeEvent.
    /// </summary>
    public void UnsubscribeEvent(object control, string eventName, Delegate? handler)
    {
        if (handler == null) return;
        try
        {
            var eventInfo = control.GetType().GetEvent(eventName);
            eventInfo?.RemoveEventHandler(control, handler);
        }
        catch { }
    }

    // ===== Avalonia property ClearValue =====

    /// <summary>
    /// Calls ClearValue on an Avalonia control to remove a local value override,
    /// allowing data bindings to reassert their values.
    /// </summary>
    public bool ClearValue(object? control, string propertyFieldName)
    {
        if (control == null) return false;
        try
        {
            // Find the static AvaloniaProperty field (e.g. ContentControl.ContentProperty)
            var field = control.GetType().GetField(propertyFieldName,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (field == null)
            {
                Logger.Log("Reflection", $"ClearValue: field '{propertyFieldName}' not found on {control.GetType().Name}");
                return false;
            }

            var avaloniaProperty = field.GetValue(null);
            if (avaloniaProperty == null) return false;

            // Find ClearValue(AvaloniaProperty) - non-generic overload
            var clearMethods = control.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == "ClearValue" && !m.IsGenericMethod && m.GetParameters().Length == 1);

            foreach (var method in clearMethods)
            {
                var paramType = method.GetParameters()[0].ParameterType;
                if (paramType.IsAssignableFrom(avaloniaProperty.GetType()))
                {
                    method.Invoke(control, new[] { avaloniaProperty });
                    Logger.Log("Reflection", $"ClearValue({propertyFieldName}) succeeded on {control.GetType().Name}");
                    return true;
                }
            }

            Logger.Log("Reflection", $"ClearValue: no matching ClearValue overload for {avaloniaProperty.GetType().Name}");
            return false;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"ClearValue error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Silent version of ClearValue for batch operations (e.g. theme revert walk).
    /// Removes the local value override, letting DynamicResource bindings reassert.
    /// </summary>
    public bool ClearValueSilent(object? control, string propertyFieldName)
    {
        if (control == null) return false;
        try
        {
            var field = control.GetType().GetField(propertyFieldName,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (field == null) return false;

            var avaloniaProperty = field.GetValue(null);
            if (avaloniaProperty == null) return false;

            var clearMethods = control.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == "ClearValue" && !m.IsGenericMethod && m.GetParameters().Length == 1);

            foreach (var method in clearMethods)
            {
                var paramType = method.GetParameters()[0].ParameterType;
                if (paramType.IsAssignableFrom(avaloniaProperty.GetType()))
                {
                    method.Invoke(control, new[] { avaloniaProperty });
                    return true;
                }
            }
            return false;
        }
        catch { return false; }
    }

    // ===== SetValue with BindingPriority =====

    private System.Reflection.MethodInfo? _setValueWithPriority;
    private object? _bindingPriorityStyle;
    private bool _priorityResolved;

    /// <summary>
    /// Set an Avalonia property value at Style priority (lower than LocalValue).
    /// This allows hover/pressed style triggers to override our value,
    /// then revert back to our themed value when the trigger deactivates.
    /// propertyFieldName is the static field name (e.g. "BackgroundProperty").
    /// Returns true if successful, false if fallback to CLR SetValue is needed.
    /// </summary>
    public bool SetValueStylePriority(object control, string propertyFieldName, object? value)
    {
        try
        {
            EnsureBindingPriorityResolved();
            if (_bindingPriorityStyle == null) return false;

            // Find the static AvaloniaProperty field
            var field = control.GetType().GetField(propertyFieldName,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (field == null) return false;

            var avaloniaProperty = field.GetValue(null);
            if (avaloniaProperty == null) return false;

            // Find SetValue(AvaloniaProperty, object, BindingPriority)
            if (_setValueWithPriority == null)
            {
                var methods = control.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.Name == "SetValue" && !m.IsGenericMethod && m.GetParameters().Length == 3);
                foreach (var method in methods)
                {
                    var parms = method.GetParameters();
                    if (parms[0].ParameterType.IsAssignableFrom(avaloniaProperty.GetType())
                        && parms[2].ParameterType.Name == "BindingPriority")
                    {
                        _setValueWithPriority = method;
                        break;
                    }
                }
            }

            if (_setValueWithPriority != null)
            {
                _setValueWithPriority.Invoke(control, new[] { avaloniaProperty, value, _bindingPriorityStyle });
                return true;
            }

            return false;
        }
        catch { return false; }
    }

    // ===== DynamicResource Binding =====

    /// <summary>
    /// Bind an injected control's property to a DynamicResource key.
    /// The control auto-updates when ThemeDictionaries change — no rebuild needed.
    ///
    /// Usage:
    ///   r.BindToDynamicResource(border, "Background", "BackgroundSecondary");
    ///   r.BindToDynamicResource(textBlock, "Foreground", "TextPrimary");
    ///   r.BindToDynamicResource(border, "BorderBrush", "Border");
    ///
    /// Programmatic equivalent of XAML: Background="{DynamicResource BackgroundSecondary}"
    /// </summary>
    public bool BindToDynamicResource(object? control, string propertyName, string resourceKey)
    {
        if (control == null) return false;

        EnsureDynamicResourceTypesResolved();

        try
        {
            // Step 1: Find the static AvaloniaProperty field (e.g. Border.BackgroundProperty)
            var fieldName = propertyName + "Property";
            var avProp = FindAvaloniaPropertyField(control.GetType(), fieldName);
            if (avProp == null)
            {
                Logger.Log("Reflection", $"BindToDynRes: {control.GetType().Name}.{fieldName} not found");
                return false;
            }

            // Strategy A: GetResourceObservable + Bind
            var getResObs = control.GetType().GetMethod("GetResourceObservable",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(object) }, null);

            if (getResObs != null)
            {
                var observable = getResObs.Invoke(control, new object[] { resourceKey });
                if (observable != null)
                {
                    if (_avaloniaBindObservableMethod == null)
                        ResolveBindObservableMethod();

                    if (_avaloniaBindObservableMethod != null)
                    {
                        try
                        {
                            var paramCount = _avaloniaBindObservableMethod.GetParameters().Length;
                            var args = paramCount == 4
                                ? new[] { control, avProp, observable, _bindingPriorityLocalValue }
                                : paramCount == 3
                                    ? new[] { control, avProp, observable }
                                    : new[] { control, avProp, observable, null };
                            _avaloniaBindObservableMethod.Invoke(null, args);
                            if (!_loggedBindStrategy) { _loggedBindStrategy = true; Logger.Log("Reflection", "BindToDynRes: Strategy A (observable) succeeded"); }
                            return true;
                        }
                        catch (Exception ex)
                        {
                            if (!_loggedBindStrategy) { _loggedBindStrategy = true; Logger.Log("Reflection", $"BindToDynRes: Strategy A failed: {ex.InnerException?.Message ?? ex.Message}"); }
                        }
                    }
                    else if (!_loggedBindStrategy)
                    {
                        _loggedBindStrategy = true;
                        Logger.Log("Reflection", "BindToDynRes: Strategy A skipped — no BindObservable method found");
                    }
                }
            }
            else if (!_loggedBindStrategy)
            {
                _loggedBindStrategy = true;
                Logger.Log("Reflection", $"BindToDynRes: GetResourceObservable not found on {control.GetType().Name}");
            }

            // Strategy B: DynamicResourceExtension as IBinding (fallback)
            if (_dynamicResourceExtType != null && _avaloniaBindMethod != null)
            {
                var ext = Activator.CreateInstance(_dynamicResourceExtType, resourceKey);
                if (ext != null)
                {
                    var paramCount = _avaloniaBindMethod.GetParameters().Length;
                    var args = paramCount == 4
                        ? new[] { control, avProp, ext, null }
                        : new[] { control, avProp, ext };
                    _avaloniaBindMethod.Invoke(null, args);
                    if (!_loggedBindStrategyB) { _loggedBindStrategyB = true; Logger.Log("Reflection", $"BindToDynRes: Strategy B (DynResExt) — IBinding type={ext.GetType().Name}, interfaces={string.Join(",", ext.GetType().GetInterfaces().Select(i => i.Name))}"); }
                    return true;
                }
            }

            Logger.Log("Reflection", $"BindToDynRes: no strategy worked for {propertyName}={resourceKey}");
            return false;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"BindToDynRes({propertyName}, {resourceKey}): {ex.InnerException?.Message ?? ex.Message}");
            return false;
        }
    }

    private void EnsureDynamicResourceTypesResolved()
    {
        if (_dynamicResourceResolved) return;
        _dynamicResourceResolved = true;

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                // DynamicResourceExtension
                if (_dynamicResourceExtType == null)
                {
                    _dynamicResourceExtType = asm.GetType(
                        "Avalonia.Markup.Xaml.MarkupExtensions.DynamicResourceExtension");
                }

                // AvaloniaObjectExtensions — contains Bind(AvaloniaObject, AvaloniaProperty, IBinding)
                if (_avaloniaBindMethod == null)
                {
                    var extType = asm.GetType("Avalonia.AvaloniaObjectExtensions");
                    if (extType != null)
                    {
                        // Find Bind overload: (AvaloniaObject, AvaloniaProperty, IBinding)
                        foreach (var m in extType.GetMethods(BindingFlags.Public | BindingFlags.Static))
                        {
                            if (m.Name != "Bind" || m.IsGenericMethod) continue;
                            var parms = m.GetParameters();
                            if (parms.Length >= 3 &&
                                parms[2].ParameterType.Name == "IBinding")
                            {
                                _avaloniaBindMethod = m;
                                break;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        // Resolve BindingPriority.LocalValue for observable binding
        if (_bindingPriorityLocalValue == null)
        {
            EnsureBindingPriorityResolved();
            // BindingPriority.LocalValue = 0 in Avalonia
            foreach (var bpAsm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var bpType = bpAsm.GetType("Avalonia.Data.BindingPriority");
                if (bpType != null)
                {
                    _bindingPriorityLocalValue = Enum.ToObject(bpType, 0);
                    break;
                }
            }
        }

        Logger.Log("Reflection", $"DynamicResource types: ext={_dynamicResourceExtType != null} bind={_avaloniaBindMethod != null}");
    }

    /// <summary>
    /// Resolve AvaloniaObjectExtensions.Bind overload that accepts IObservable.
    /// Signature: Bind(AvaloniaObject, AvaloniaProperty, IObservable&lt;object?&gt;, BindingPriority)
    /// </summary>
    private void ResolveBindObservableMethod()
    {
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var extType = asm.GetType("Avalonia.AvaloniaObjectExtensions");
                if (extType == null) continue;

                // Look for non-generic Bind that takes IObservable parameter
                foreach (var m in extType.GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    if (m.Name != "Bind" || m.IsGenericMethod) continue;
                    var parms = m.GetParameters();
                    if (parms.Length < 3) continue;

                    // Check 3rd param is IObservable<T> or IObservable
                    var p2Type = parms[2].ParameterType;
                    if (p2Type.Name.StartsWith("IObservable"))
                    {
                        _avaloniaBindObservableMethod = m;
                        Logger.Log("Reflection", $"BindObservable method found: {parms.Length} params, p2={p2Type.Name}");
                        return;
                    }
                }
            }
            catch { }
        }
        Logger.Log("Reflection", "BindObservable method NOT found — will use DynamicResourceExtension fallback");
    }

    /// <summary>
    /// Find a static AvaloniaProperty field on a control type (cached).
    /// E.g. FindAvaloniaPropertyField(typeof(Border), "BackgroundProperty")
    /// </summary>
    private object? FindAvaloniaPropertyField(Type controlType, string fieldName)
    {
        var cacheKey = controlType.FullName + "." + fieldName;
        if (_avaloniaPropertyFieldCache.TryGetValue(cacheKey, out var cached))
            return cached?.GetValue(null);

        // Search up the hierarchy
        var field = controlType.GetField(fieldName,
            BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        _avaloniaPropertyFieldCache[cacheKey] = field;

        return field?.GetValue(null);
    }

    /// <summary>
    /// Map a property name to its Avalonia static property field name.
    /// E.g. "Background" -> "BackgroundProperty"
    /// </summary>
    public static string PropertyToFieldName(string propertyName) => propertyName + "Property";

    // ===== MaxHeight / MaxWidth helpers =====

    public void SetMaxWidth(object? control, double maxWidth)
    {
        if (control == null) return;
        control.GetType().GetProperty("MaxWidth")?.SetValue(control, maxWidth);
    }

    public void SetMaxHeight(object? control, double maxHeight)
    {
        if (control == null) return;
        control.GetType().GetProperty("MaxHeight")?.SetValue(control, maxHeight);
    }

    public double GetMaxHeight(object? control)
    {
        if (control == null) return double.NaN;
        return control.GetType().GetProperty("MaxHeight")?.GetValue(control) as double? ?? double.NaN;
    }

    public void ClearMaxHeight(object? control)
    {
        if (control == null) return;
        control.GetType().GetProperty("MaxHeight")?.SetValue(control, double.NaN);
    }

    // ===== ContentControl helpers =====

    public object? GetContent(object? contentControl)
    {
        if (contentControl == null) return null;
        return _contentControlContent?.GetValue(contentControl)
            ?? contentControl.GetType().GetProperty("Content")?.GetValue(contentControl);
    }

    public void SetContent(object? contentControl, object? content)
    {
        if (contentControl == null) return;
        if (_contentControlContent != null)
            _contentControlContent.SetValue(contentControl, content);
        else
            contentControl.GetType().GetProperty("Content")?.SetValue(contentControl, content);
    }

    // ===== ListBox helpers =====

    public int GetSelectedIndex(object? listBox)
    {
        if (listBox == null) return -1;
        var prop = listBox.GetType().GetProperty("SelectedIndex");
        return prop?.GetValue(listBox) as int? ?? -1;
    }

    public void SetSelectedIndex(object? listBox, int index)
    {
        if (listBox == null) return;
        var prop = listBox.GetType().GetProperty("SelectedIndex");
        prop?.SetValue(listBox, index);
    }

    // ===== Clipboard =====

    public void CopyToClipboard(object window, string text)
    {
        try
        {
            // TopLevel.Clipboard in Avalonia 11+
            var clipboardProp = window.GetType().GetProperty("Clipboard");
            var clipboard = clipboardProp?.GetValue(window);
            if (clipboard == null)
            {
                Logger.Log("Reflection", "Clipboard property not found on window");
                return;
            }

            // Try concrete type first, then search interfaces (handles explicit implementations)
            var setTextAsync = clipboard.GetType().GetMethod("SetTextAsync", new[] { typeof(string) });
            if (setTextAsync == null)
            {
                foreach (var iface in clipboard.GetType().GetInterfaces())
                {
                    setTextAsync = iface.GetMethod("SetTextAsync", new[] { typeof(string) });
                    if (setTextAsync != null) break;
                }
            }

            if (setTextAsync != null)
            {
                setTextAsync.Invoke(clipboard, new object[] { text });
                Logger.Log("Reflection", $"Copied {text.Length} chars to clipboard");
            }
            else
            {
                Logger.Log("Reflection", $"CopyToClipboard: SetTextAsync not found on {clipboard.GetType().FullName}");
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"CopyToClipboard error: {ex.Message}");
        }
    }

    // ===== Parent access =====

    public object? GetParent(object? node)
    {
        if (node == null) return null;
        var type = node.GetType();

        // Try VisualParent first, then Parent
        var vpProp = type.GetProperty("VisualParent");
        if (vpProp != null) return vpProp.GetValue(node);

        var pProp = type.GetProperty("Parent");
        return pProp?.GetValue(node);
    }

    // ===== OverlayLayer / Canvas / Positioning =====

    /// <summary>
    /// Gets the OverlayLayer for a Visual (typically the Window).
    /// OverlayLayer is a Canvas at the Window level, outside the normal visual tree.
    /// </summary>
    public object? GetOverlayLayer(object visual)
    {
        if (_overlayGetOverlayLayer == null) return null;
        try
        {
            return _overlayGetOverlayLayer.Invoke(null, new[] { visual });
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"GetOverlayLayer error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Sets Canvas.Left and Canvas.Top attached properties for positioning in OverlayLayer.
    /// </summary>
    public void SetCanvasPosition(object? control, double left, double top)
    {
        if (control == null) return;
        try
        {
            _canvasSetLeft?.Invoke(null, new[] { control, (object)left });
            _canvasSetTop?.Invoke(null, new[] { control, (object)top });
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"SetCanvasPosition error: {ex.Message}");
        }
    }

    /// <summary>
    /// Gets the Bounds (Rect) of a control. Returns (X, Y, Width, Height) or null.
    /// </summary>
    public (double X, double Y, double W, double H)? GetBounds(object? control)
    {
        if (control == null || _layoutableBounds == null) return null;
        try
        {
            var bounds = _layoutableBounds.GetValue(control);
            if (bounds == null) return null;
            var bt = bounds.GetType();
            var x = (double)(bt.GetProperty("X")?.GetValue(bounds) ?? 0.0);
            var y = (double)(bt.GetProperty("Y")?.GetValue(bounds) ?? 0.0);
            var w = (double)(bt.GetProperty("Width")?.GetValue(bounds) ?? 0.0);
            var h = (double)(bt.GetProperty("Height")?.GetValue(bounds) ?? 0.0);
            return (x, y, w, h);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"GetBounds error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Translates a point from one visual's coordinate space to another.
    /// Handles both instance method (Avalonia 11+: visual.TranslatePoint(Point, Visual))
    /// and static extension (VisualExtensions.TranslatePoint(Visual, Point, Visual)).
    /// </summary>
    public (double X, double Y)? TranslatePoint(object from, double x, double y, object to)
    {
        if (_translatePoint == null || PointType == null) return null;
        try
        {
            var point = Activator.CreateInstance(PointType, x, y);

            object? result;
            if (_translatePoint.IsStatic)
            {
                // Static extension: TranslatePoint(Visual from, Point point, Visual to)
                result = _translatePoint.Invoke(null, new[] { from, point, to });
            }
            else
            {
                // Instance method: from.TranslatePoint(Point point, Visual to)
                result = _translatePoint.Invoke(from, new[] { point, to });
            }

            if (result == null) return null;

            // Result is Point? (nullable). Unwrap if needed.
            var resultType = result.GetType();

            // Check if it's Nullable<Point>
            if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var hasValue = (bool)(resultType.GetProperty("HasValue")?.GetValue(result) ?? false);
                if (!hasValue) return null;
                result = resultType.GetProperty("Value")?.GetValue(result);
                if (result == null) return null;
                resultType = result.GetType();
            }

            var rx = (double)(resultType.GetProperty("X")?.GetValue(result) ?? 0.0);
            var ry = (double)(resultType.GetProperty("Y")?.GetValue(result) ?? 0.0);
            return (rx, ry);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"TranslatePoint error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Add a child to the OverlayLayer (which is a Panel/Canvas).
    /// </summary>
    public void AddToOverlay(object? overlay, object? child)
    {
        if (overlay == null || child == null) return;
        GetChildren(overlay)?.Add(child);
    }

    /// <summary>
    /// Remove a child from the OverlayLayer.
    /// </summary>
    public void RemoveFromOverlay(object? overlay, object? child)
    {
        if (overlay == null || child == null) return;
        try { GetChildren(overlay)?.Remove(child); }
        catch (Exception ex) { Logger.Log("Reflection", $"RemoveFromOverlay error: {ex.Message}"); }
    }

    // ===== Width / Height / Opacity =====

    public void SetWidth(object? control, double width)
    {
        if (control == null) return;
        control.GetType().GetProperty("Width")?.SetValue(control, width);
    }

    public void SetHeight(object? control, double height)
    {
        if (control == null) return;
        control.GetType().GetProperty("Height")?.SetValue(control, height);
    }

    public void SetMinWidth(object? control, double minWidth)
    {
        if (control == null) return;
        control.GetType().GetProperty("MinWidth")?.SetValue(control, minWidth);
    }

    public void SetOpacity(object? control, double opacity)
    {
        if (control == null) return;
        _controlOpacity?.SetValue(control, opacity);
    }

    public void SetIsHitTestVisible(object? control, bool visible)
    {
        if (control == null) return;
        _controlIsHitTestVisible?.SetValue(control, visible);
    }

    public object? CreateCanvas()
    {
        if (CanvasType == null) return null;
        return Activator.CreateInstance(CanvasType);
    }

    // ===== Resource Dictionary System =====

    /// <summary>
    /// Gets Application.Current.Resources
    /// </summary>
    public object? GetAppResources()
    {
        var app = GetAppCurrent();
        if (app == null || _appResources == null) return null;
        return _appResources.GetValue(app);
    }

    /// <summary>
    /// Gets Application.Styles[index].Resources - where Root's theme colors live.
    /// Style[0] is SimpleTheme with ThemeAccentColor, ThemeAccentBrush, etc.
    /// </summary>
    public object? GetStyleResources(int styleIndex)
    {
        var app = GetAppCurrent();
        if (app == null) return null;
        try
        {
            var stylesProp = app.GetType().GetProperty("Styles");
            var styles = stylesProp?.GetValue(app);
            if (styles is IEnumerable styleEnum)
            {
                int i = 0;
                foreach (var style in styleEnum)
                {
                    if (i == styleIndex && style != null)
                    {
                        var resProp = style.GetType().GetProperty("Resources");
                        return resProp?.GetValue(style);
                    }
                    i++;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", "GetStyleResources error: " + ex.Message);
        }
        return null;
    }

    /// <summary>
    /// Gets a resource value from a resource dictionary by key.
    /// </summary>
    public object? GetResource(object? dict, string key)
    {
        if (dict == null) return null;
        try
        {
            // Use TryGetValue or indexer
            var indexer = dict.GetType().GetProperty("Item",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance,
                null, null, new[] { typeof(object) }, null);
            if (indexer != null)
            {
                return indexer.GetValue(dict, new object[] { key });
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Get a resource using Avalonia's full resolution chain: direct entries → MergedDictionaries → deferred values.
    /// Uses TryGetResource(key, ThemeVariant, out value) which is the correct lookup for ResourceDictionary.
    /// Falls back to indexer if TryGetResource isn't available.
    /// </summary>
    public object? GetResolvedResource(object? dict, string key, object? themeVariant = null)
    {
        if (dict == null) return null;
        try
        {
            // Try TryGetResource(object key, ThemeVariant? theme, out object? value)
            // This is the proper Avalonia resource lookup that resolves through MergedDictionaries and deferred values.
            MethodInfo? tryGetMethod = null;
            foreach (var m in dict.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (m.Name != "TryGetResource") continue;
                var ps = m.GetParameters();
                if (ps.Length == 3 && ps[2].IsOut)
                {
                    tryGetMethod = m;
                    break;
                }
            }

            if (tryGetMethod != null)
            {
                var args = new object?[] { key, themeVariant, null };
                var found = tryGetMethod.Invoke(dict, args);
                if (found is true)
                    return args[2];
            }
        }
        catch { }

        // Fallback to indexer
        return GetResource(dict, key);
    }

    /// <summary>
    /// Gets the MergedDictionaries collection from a resource dictionary object.
    /// </summary>
    public IList? GetMergedDictionaries(object? resources)
    {
        if (resources == null) return null;
        // Try cached property first
        if (_resourcesMergedDicts != null)
        {
            try { return _resourcesMergedDicts.GetValue(resources) as IList; }
            catch { }
        }
        // Fallback: search on the actual type
        var prop = resources.GetType().GetProperty("MergedDictionaries",
            BindingFlags.Public | BindingFlags.Instance);
        return prop?.GetValue(resources) as IList;
    }

    /// <summary>
    /// Creates a new ResourceDictionary instance.
    /// </summary>
    public object? CreateResourceDictionary()
    {
        if (ResourceDictionaryType == null) return null;
        return Activator.CreateInstance(ResourceDictionaryType);
    }

    /// <summary>
    /// Adds a key-value pair to a ResourceDictionary.
    /// Uses the IDictionary indexer: dict[key] = value
    /// </summary>
    public void AddResource(object? dict, string key, object? value)
    {
        if (dict == null || value == null) return;
        try
        {
            // ResourceDictionary implements IDictionary<object, object?>
            // Use the indexer via reflection
            var indexer = dict.GetType().GetProperty("Item",
                BindingFlags.Public | BindingFlags.Instance,
                null, null, new[] { typeof(object) }, null);
            if (indexer != null)
            {
                indexer.SetValue(dict, value, new object[] { key });
                return;
            }

            // Fallback: try Add method
            var addMethod = dict.GetType().GetMethod("Add",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(object), typeof(object) }, null);
            addMethod?.Invoke(dict, new[] { (object)key, value });
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"AddResource({key}) error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a key from a ResourceDictionary.
    /// </summary>
    public bool RemoveResource(object? dict, string key)
    {
        if (dict == null) return false;
        try
        {
            var removeMethod = dict.GetType().GetMethod("Remove",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(object) }, null);
            if (removeMethod != null)
            {
                removeMethod.Invoke(dict, new object[] { key });
                return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Parses a hex color string into an Avalonia Color struct.
    /// </summary>
    public object? ParseColor(string hex)
    {
        if (_colorParse == null) return null;
        try
        {
            return _colorParse.Invoke(null, new object[] { hex });
        }
        catch { return null; }
    }

    // ===== Linear Gradient Brush =====

    /// <summary>
    /// Creates a LinearGradientBrush with the specified start/end points and gradient stops.
    /// startX/startY/endX/endY are 0-1 relative coordinates.
    /// stops is an array of (hexColor, offset) tuples where offset is 0-1.
    /// </summary>
    public object? CreateLinearGradientBrush(double startX, double startY, double endX, double endY,
        (string hex, double offset)[] stops)
    {
        if (LinearGradientBrushType == null || GradientStopType == null || _colorParse == null) return null;
        try
        {
            var brush = Activator.CreateInstance(LinearGradientBrushType);
            if (brush == null) return null;

            // Set StartPoint and EndPoint as RelativePoint (using relative 0-1 coordinates)
            if (RelativePointType != null)
            {
                object? startPoint = null, endPoint = null;

                // Primary method: RelativePoint.Parse("X%,Y%") -- always produces Relative unit
                var parseMethod = RelativePointType.GetMethod("Parse",
                    BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null);
                if (parseMethod != null)
                {
                    try
                    {
                        string startStr = $"{startX * 100:F0}%,{startY * 100:F0}%";
                        string endStr = $"{endX * 100:F0}%,{endY * 100:F0}%";
                        startPoint = parseMethod.Invoke(null, new object[] { startStr });
                        endPoint = parseMethod.Invoke(null, new object[] { endStr });
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Reflection", $"RelativePoint.Parse failed: {ex.Message}");
                    }
                }

                // Fallback: constructor with Enum.Parse by name (not by int value)
                if (startPoint == null)
                {
                    var relUnitType = RelativeUnitType ?? RelativePointType.GetConstructors()
                        .SelectMany(c => c.GetParameters())
                        .FirstOrDefault(p => p.ParameterType.Name == "RelativeUnit")?.ParameterType;

                    if (relUnitType != null)
                    {
                        try
                        {
                            var relativeUnit = Enum.Parse(relUnitType, "Relative");
                            startPoint = Activator.CreateInstance(RelativePointType, startX, startY, relativeUnit);
                            endPoint = Activator.CreateInstance(RelativePointType, endX, endY, relativeUnit);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log("Reflection", $"RelativePoint ctor fallback failed: {ex.Message}");
                        }
                    }
                }

                if (startPoint != null)
                    brush.GetType().GetProperty("StartPoint")?.SetValue(brush, startPoint);
                if (endPoint != null)
                    brush.GetType().GetProperty("EndPoint")?.SetValue(brush, endPoint);

                Logger.Log("Reflection", $"LinearGradientBrush: start={startPoint != null} end={endPoint != null} method={(parseMethod != null ? "Parse" : "ctor")}");
            }

            // Add gradient stops
            var gradientStops = brush.GetType().GetProperty("GradientStops")?.GetValue(brush);
            if (gradientStops == null && GradientStopsType != null)
            {
                gradientStops = Activator.CreateInstance(GradientStopsType);
                brush.GetType().GetProperty("GradientStops")?.SetValue(brush, gradientStops);
            }

            if (gradientStops is IList stopList)
            {
                foreach (var (hex, offset) in stops)
                {
                    var color = _colorParse.Invoke(null, new object[] { hex });
                    if (color == null) continue;
                    var stop = Activator.CreateInstance(GradientStopType);
                    if (stop == null) continue;
                    stop.GetType().GetProperty("Color")?.SetValue(stop, color);
                    stop.GetType().GetProperty("Offset")?.SetValue(stop, offset);
                    stopList.Add(stop);
                }
            }

            return brush;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"CreateLinearGradientBrush error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Set Background on a control using a pre-created brush object (not a hex string).
    /// </summary>
    public void SetBackgroundBrush(object? control, object? brush)
    {
        if (control == null) return;
        var bgProp = control.GetType().GetProperty("Background");
        bgProp?.SetValue(control, brush);
    }

    /// <summary>
    /// Subscribe to a pointer event and extract the position relative to the control.
    /// Callback receives (x, y) coordinates in the control's coordinate space.
    /// </summary>
    public void SubscribePointerEvent(object control, string eventName, Action<double, double> callback)
    {
        try
        {
            var type = control.GetType();
            var eventInfo = type.GetEvent(eventName);
            if (eventInfo == null)
            {
                Logger.Log("Reflection", $"PointerEvent '{eventName}' not found on {type.Name}");
                return;
            }

            var handlerType = eventInfo.EventHandlerType!;
            var invokeMethod = handlerType.GetMethod("Invoke")!;
            var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();

            // Build: (sender, e) => { var pos = e.GetPosition(sender); callback(pos.X, pos.Y); }
            var p0 = Expression.Parameter(paramTypes[0], "sender");
            var p1 = Expression.Parameter(paramTypes[1], "e");

            // Find GetPosition method on the event args type
            var getPositionMethod = paramTypes[1].GetMethod("GetPosition",
                new[] { VisualType ?? typeof(object) });
            // Fallback: try with IVisual or any single-param overload
            getPositionMethod ??= paramTypes[1].GetMethods()
                .FirstOrDefault(m => m.Name == "GetPosition" && m.GetParameters().Length == 1);

            if (getPositionMethod == null)
            {
                Logger.Log("Reflection", $"GetPosition not found on {paramTypes[1].Name}");
                return;
            }

            // Cast sender to the parameter type expected by GetPosition
            var castSender = Expression.Convert(p0, getPositionMethod.GetParameters()[0].ParameterType);
            var posExpr = Expression.Call(p1, getPositionMethod, castSender);

            var posXProp = getPositionMethod.ReturnType.GetProperty("X");
            var posYProp = getPositionMethod.ReturnType.GetProperty("Y");
            if (posXProp == null || posYProp == null) return;

            var xExpr = Expression.Property(posExpr, posXProp);
            var yExpr = Expression.Property(posExpr, posYProp);

            var callbackExpr = Expression.Constant(callback);
            var invokeExpr = Expression.Invoke(callbackExpr, xExpr, yExpr);
            var lambda = Expression.Lambda(handlerType, invokeExpr, p0, p1);
            var handler = lambda.Compile();

            eventInfo.AddEventHandler(control, handler);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"SubscribePointerEvent({eventName}) failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets ClipToBounds on a control to prevent child content from rendering outside bounds.
    /// </summary>
    public void SetClipToBounds(object? control, bool clip)
    {
        if (control == null) return;
        control.GetType().GetProperty("ClipToBounds")?.SetValue(control, clip);
    }

    // ===== Image helpers =====

    /// <summary>
    /// Creates an Avalonia Image control with the specified Stretch mode.
    /// </summary>
    public object? CreateImage(string stretch = "Uniform")
    {
        if (ImageType == null) return null;
        try
        {
            var img = Activator.CreateInstance(ImageType);
            if (img != null && StretchType != null && _imageStretch != null)
            {
                var stretchVal = Enum.Parse(StretchType, stretch);
                _imageStretch.SetValue(img, stretchVal);
            }
            return img;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"CreateImage error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Creates an Avalonia Bitmap from a MemoryStream via Bitmap(Stream) constructor.
    /// Returns null if the Bitmap type wasn't resolved or construction fails.
    /// </summary>
    public object? CreateBitmapFromStream(System.IO.Stream stream)
    {
        if (BitmapType == null) return null;
        try
        {
            // Bitmap(Stream) constructor
            var ctor = BitmapType.GetConstructor(new[] { typeof(System.IO.Stream) });
            if (ctor == null)
            {
                Logger.Log("Reflection", "Bitmap(Stream) constructor not found");
                return null;
            }
            return ctor.Invoke(new object[] { stream });
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"CreateBitmapFromStream error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Sets Image.Source = bitmap on an Image control.
    /// </summary>
    public void SetImageSource(object? image, object? bitmap)
    {
        if (image == null || bitmap == null) return;
        if (_imageSource == null)
        {
            Logger.Log("Reflection", "SetImageSource: _imageSource PropertyInfo is null — Image.Source reflection failed");
            return;
        }
        _imageSource.SetValue(image, bitmap);
    }

    // ===== ThemeDictionaries Access (for Theme Engine v2) =====

    /// <summary>
    /// Gets ResourceDictionary.ThemeDictionaries from a resource dictionary.
    /// Returns the IDictionary&lt;ThemeVariant, IThemeVariantProvider&gt; for keying.
    /// </summary>
    public IDictionary? GetThemeDictionaries(object? resources)
    {
        if (resources == null) return null;
        try
        {
            var prop = resources.GetType().GetProperty("ThemeDictionaries",
                BindingFlags.Public | BindingFlags.Instance);
            return prop?.GetValue(resources) as IDictionary;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"GetThemeDictionaries error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Gets Application.Current.ActualThemeVariant (a ThemeVariant struct).
    /// </summary>
    public object? GetActiveThemeVariant()
    {
        var app = GetAppCurrent();
        if (app == null) return null;
        try
        {
            var prop = app.GetType().GetProperty("ActualThemeVariant",
                BindingFlags.Public | BindingFlags.Instance);
            return prop?.GetValue(app);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"GetActiveThemeVariant error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Sets Application.Current.RequestedThemeVariant to switch the active variant.
    /// The variantKey should be a ThemeVariant object (from GetActiveThemeVariant or
    /// GetThemeVariantByName). Setting this changes ActualThemeVariant synchronously
    /// on the UI thread, which triggers ActualThemeVariantChanged.
    /// </summary>
    public bool SetRequestedThemeVariant(object? variantKey)
    {
        if (variantKey == null) return false;
        var app = GetAppCurrent();
        if (app == null) return false;
        try
        {
            var prop = app.GetType().GetProperty("RequestedThemeVariant",
                BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
            {
                Logger.Log("Reflection", "RequestedThemeVariant property not found");
                return false;
            }
            prop.SetValue(app, variantKey);
            Logger.Log("Reflection", $"Set RequestedThemeVariant -> {variantKey}");
            return true;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"SetRequestedThemeVariant error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Get a ThemeVariant static field by name ("Dark", "Light", "Default").
    /// Uses the runtime type of ActualThemeVariant to find the ThemeVariant class,
    /// then reads the static field.
    /// </summary>
    public object? GetThemeVariantByName(string name)
    {
        var actual = GetActiveThemeVariant();
        if (actual == null) return null;
        try
        {
            var tvType = actual.GetType();
            var field = tvType.GetField(name, BindingFlags.Public | BindingFlags.Static);
            if (field != null) return field.GetValue(null);

            var prop = tvType.GetProperty(name, BindingFlags.Public | BindingFlags.Static);
            return prop?.GetValue(null);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"GetThemeVariantByName({name}) error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Find a ThemeVariant key by string name (e.g. "PureDark", "Dark", "Light").
    /// Iterates ThemeDictionaries keys and matches by ToString().
    /// </summary>
    public object? FindVariantByName(IDictionary themeDicts, string variantName)
    {
        try
        {
            foreach (var key in themeDicts.Keys)
            {
                if (key?.ToString() == variantName)
                    return key;
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"FindVariantByName error: {ex.Message}");
        }
        return null;
    }

    /// <summary>
    /// Subscribe to Application.ActualThemeVariantChanged event.
    /// Fires when the user switches between Dark/Light/PureDark.
    /// </summary>
    public void SubscribeActualThemeVariantChanged(Action callback)
    {
        var app = GetAppCurrent();
        if (app == null) return;
        SubscribeEvent(app, "ActualThemeVariantChanged", callback);
    }

    /// <summary>
    /// Enumerate all resource keys in a resource dictionary (for diagnostics).
    /// </summary>
    public void EnumerateResources(object? resources, Action<string, object?> callback)
    {
        if (resources == null) return;
        try
        {
            // Try IEnumerable<KeyValuePair<object, object?>>
            if (resources is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    if (item == null) continue;
                    var itemType = item.GetType();
                    var keyProp = itemType.GetProperty("Key");
                    var valueProp = itemType.GetProperty("Value");
                    if (keyProp != null && valueProp != null)
                    {
                        var key = keyProp.GetValue(item)?.ToString() ?? "null";
                        var value = valueProp.GetValue(item);
                        callback(key, value);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"EnumerateResources error: {ex.Message}");
        }
    }

    // ===== Grid column manipulation (for Rootcord layout surgery) =====

    /// <summary>
    /// Gets the ColumnDefinitions collection from a Grid.
    /// </summary>
    public IList? GetColumnDefinitions(object? grid)
    {
        if (grid == null) return null;
        try
        {
            return grid.GetType().GetProperty("ColumnDefinitions")?.GetValue(grid) as IList;
        }
        catch { return null; }
    }

    /// <summary>
    /// Gets the RowDefinitions collection from a Grid.
    /// </summary>
    public IList? GetRowDefinitions(object? grid)
    {
        if (grid == null) return null;
        try
        {
            return grid.GetType().GetProperty("RowDefinitions")?.GetValue(grid) as IList;
        }
        catch { return null; }
    }

    /// <summary>
    /// Gets the Height (GridLength) of a RowDefinition.
    /// Returns (value, unitType) where unitType is "Auto", "Pixel", or "Star".
    /// </summary>
    public (double Value, string UnitType)? GetRowDefinitionHeight(object? rowDef)
    {
        if (rowDef == null) return null;
        try
        {
            var heightProp = rowDef.GetType().GetProperty("Height");
            var gridLength = heightProp?.GetValue(rowDef);
            if (gridLength == null) return null;
            var glType = gridLength.GetType();
            var value = (double)(glType.GetProperty("Value")?.GetValue(gridLength) ?? 0.0);
            var unitType = glType.GetProperty("GridUnitType")?.GetValue(gridLength);
            return (value, unitType?.ToString() ?? "Pixel");
        }
        catch { return null; }
    }

    /// <summary>
    /// Sets the Height of a RowDefinition to a pixel value.
    /// </summary>
    public void SetRowDefinitionPixelHeight(object? rowDef, double pixels)
    {
        if (rowDef == null || GridLengthType == null || GridUnitTypeEnum == null) return;
        try
        {
            var pixelUnit = Enum.Parse(GridUnitTypeEnum, "Pixel");
            var gridLength = Activator.CreateInstance(GridLengthType, pixels, pixelUnit);
            rowDef.GetType().GetProperty("Height")?.SetValue(rowDef, gridLength);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"SetRowDefinitionPixelHeight error: {ex.Message}");
        }
    }

    /// <summary>
    /// Gets the Width (GridLength) of a ColumnDefinition.
    /// Returns (value, unitType) where unitType is "Auto", "Pixel", or "Star".
    /// </summary>
    public (double Value, string UnitType)? GetColumnDefinitionWidth(object? colDef)
    {
        if (colDef == null) return null;
        try
        {
            var widthProp = colDef.GetType().GetProperty("Width");
            var gridLength = widthProp?.GetValue(colDef);
            if (gridLength == null) return null;
            var glType = gridLength.GetType();
            var value = (double)(glType.GetProperty("Value")?.GetValue(gridLength) ?? 0.0);
            var unitType = glType.GetProperty("GridUnitType")?.GetValue(gridLength);
            return (value, unitType?.ToString() ?? "Pixel");
        }
        catch { return null; }
    }

    /// <summary>
    /// Sets the Width of a ColumnDefinition to a pixel value.
    /// </summary>
    public void SetColumnDefinitionPixelWidth(object? colDef, double pixels)
    {
        if (colDef == null || GridLengthType == null || GridUnitTypeEnum == null) return;
        try
        {
            var pixelUnit = Enum.Parse(GridUnitTypeEnum, "Pixel");
            var gridLength = Activator.CreateInstance(GridLengthType, pixels, pixelUnit);
            colDef.GetType().GetProperty("Width")?.SetValue(colDef, gridLength);
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"SetColumnDefinitionPixelWidth error: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets Grid.ColumnSpan attached property on a control.
    /// </summary>
    public void SetGridColumnSpan(object? control, int span)
    {
        if (control == null || GridType == null) return;
        try
        {
            var setSpan = GridType.GetMethod("SetColumnSpan", BindingFlags.Public | BindingFlags.Static);
            setSpan?.Invoke(null, new[] { control, (object)span });
        }
        catch { }
    }

    /// <summary>
    /// Gets Grid.ColumnSpan attached property from a control.
    /// </summary>
    public int GetGridColumnSpan(object? control)
    {
        if (control == null || GridType == null) return 1;
        try
        {
            var getSpan = GridType.GetMethod("GetColumnSpan", BindingFlags.Public | BindingFlags.Static);
            return (int)(getSpan?.Invoke(null, new[] { control }) ?? 1);
        }
        catch { return 1; }
    }

    /// <summary>
    /// Gets Grid.RowSpan attached property from a control.
    /// </summary>
    public int GetGridRowSpan(object? control)
    {
        if (control == null || GridType == null) return 1;
        try
        {
            var getSpan = GridType.GetMethod("GetRowSpan", BindingFlags.Public | BindingFlags.Static);
            return (int)(getSpan?.Invoke(null, new[] { control }) ?? 1);
        }
        catch { return 1; }
    }

    /// <summary>
    /// Sets Grid.RowSpan attached property on a control.
    /// </summary>
    public void SetGridRowSpan(object? control, int span)
    {
        if (control == null || GridType == null) return;
        try
        {
            var setSpan = GridType.GetMethod("SetRowSpan", BindingFlags.Public | BindingFlags.Static);
            setSpan?.Invoke(null, new[] { control, (object)span });
        }
        catch { }
    }

    // ===== DataContext / ViewModel access =====

    /// <summary>
    /// Gets the DataContext (ViewModel) of a control.
    /// </summary>
    public object? GetDataContext(object? control)
    {
        if (control == null) return null;
        try
        {
            return control.GetType().GetProperty("DataContext")?.GetValue(control);
        }
        catch { return null; }
    }

    /// <summary>
    /// Gets a property value from any object by property name (useful for ViewModel access).
    /// </summary>
    public object? GetPropertyValue(object? obj, string propertyName)
    {
        if (obj == null) return null;
        try
        {
            return obj.GetType().GetProperty(propertyName,
                BindingFlags.Public | BindingFlags.Instance)?.GetValue(obj);
        }
        catch { return null; }
    }

    /// <summary>
    /// Sets a property value on any object by property name (useful for ViewModel access).
    /// </summary>
    public bool SetPropertyValue(object? obj, string propertyName, object? value)
    {
        if (obj == null) return false;
        try
        {
            var prop = obj.GetType().GetProperty(propertyName,
                BindingFlags.Public | BindingFlags.Instance);
            if (prop == null || !prop.CanWrite) return false;
            prop.SetValue(obj, value);
            return true;
        }
        catch { return false; }
    }

    // ===== INotifyPropertyChanged / INotifyCollectionChanged =====

    /// <summary>
    /// Subscribe to PropertyChanged on an INotifyPropertyChanged object.
    /// Callback receives the property name that changed.
    /// </summary>
    public object? SubscribePropertyChanged(object? obj, Action<string> callback)
    {
        if (obj == null) return null;
        try
        {
            var inpcType = obj.GetType().GetInterface("System.ComponentModel.INotifyPropertyChanged");
            if (inpcType == null) return null;

            var eventInfo = inpcType.GetEvent("PropertyChanged");
            if (eventInfo == null) return null;

            var handlerType = eventInfo.EventHandlerType!;
            var paramTypes = handlerType.GetMethod("Invoke")!.GetParameters()
                .Select(p => p.ParameterType).ToArray();

            var p0 = Expression.Parameter(paramTypes[0], "sender");
            var p1 = Expression.Parameter(paramTypes[1], "e");
            var propNameExpr = Expression.Property(p1, "PropertyName");
            var callbackExpr = Expression.Constant(callback);
            var invokeExpr = Expression.Invoke(callbackExpr, propNameExpr);
            var lambda = Expression.Lambda(handlerType, invokeExpr, p0, p1);
            var handler = lambda.Compile();

            eventInfo.AddEventHandler(obj, (Delegate)handler);
            return handler;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"SubscribePropertyChanged error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Unsubscribe a previously returned PropertyChanged handler.
    /// </summary>
    public void UnsubscribePropertyChanged(object? obj, object? handler)
    {
        if (obj == null || handler == null) return;
        try
        {
            var inpcType = obj.GetType().GetInterface("System.ComponentModel.INotifyPropertyChanged");
            var eventInfo = inpcType?.GetEvent("PropertyChanged");
            eventInfo?.RemoveEventHandler(obj, (Delegate)handler);
        }
        catch { }
    }

    /// <summary>
    /// Subscribe to CollectionChanged on an INotifyCollectionChanged object.
    /// Callback fires on any collection change.
    /// </summary>
    public object? SubscribeCollectionChanged(object? collection, Action callback)
    {
        if (collection == null) return null;
        try
        {
            var inccType = collection.GetType().GetInterface("System.Collections.Specialized.INotifyCollectionChanged");
            if (inccType == null) return null;

            var eventInfo = inccType.GetEvent("CollectionChanged");
            if (eventInfo == null) return null;

            var handlerType = eventInfo.EventHandlerType!;
            var paramTypes = handlerType.GetMethod("Invoke")!.GetParameters()
                .Select(p => p.ParameterType).ToArray();

            var p0 = Expression.Parameter(paramTypes[0], "sender");
            var p1 = Expression.Parameter(paramTypes[1], "e");
            var callbackExpr = Expression.Constant(callback);
            var invokeExpr = Expression.Invoke(callbackExpr);
            var lambda = Expression.Lambda(handlerType, invokeExpr, p0, p1);
            var handler = lambda.Compile();

            eventInfo.AddEventHandler(collection, (Delegate)handler);
            return handler;
        }
        catch (Exception ex)
        {
            Logger.Log("Reflection", $"SubscribeCollectionChanged error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Unsubscribe a previously returned CollectionChanged handler.
    /// </summary>
    public void UnsubscribeCollectionChanged(object? collection, object? handler)
    {
        if (collection == null || handler == null) return;
        try
        {
            var inccType = collection.GetType().GetInterface("System.Collections.Specialized.INotifyCollectionChanged");
            var eventInfo = inccType?.GetEvent("CollectionChanged");
            eventInfo?.RemoveEventHandler(collection, (Delegate)handler);
        }
        catch { }
    }
}
