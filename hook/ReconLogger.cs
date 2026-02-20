using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace Uprooted;

/// <summary>
/// Rootcord Recon Logger — DEBUG/DEV ONLY.
///
/// HOW TO USE:
///   1. Switch to the Developer update channel in Uprooted Settings → About.
///   2. On the Plugins page, enable "Recon Logger". Activates immediately.
///   3. Click/hover around the Rootcord UI (server icons, member list, expand arrows,
///      user bar, community member cards, the 4 popup buttons, etc.)
///   4. After each click an 800ms "session window" opens. All changes (bounds, popups,
///      RenderTransform rotations) are recorded. SESSION_END prints a summary.
///   5. Log file:
///         %LOCALAPPDATA%\Root Communications\Root\profile\default\rootcord_recon.log
///   6. Toggle off to stop logging immediately; toggle on to start a fresh log.
/// </summary>
internal static class ReconLogger
{
    private static AvaloniaReflection? _r;
    private static object?             _mainWindow;
    private static StreamWriter?       _logWriter;
    private static readonly object     _logLock = new();
    private static bool                _initialized;  // set once by Init(), guards against double-subscription
    private static bool                _enabled;      // toggled by Enable()/Disable(), checked by all handlers

    // ── Session (800ms window from last PointerPressed) ──────────────────────
    private static volatile bool     _inSession;
    private static DateTime          _sessionStart;
    private static Timer?            _sessionTimer;
    private static Timer?            _scanTimer;
    private static double            _sessionClickX, _sessionClickY;
    private static readonly List<string> _sessionEvents = new();
    private static readonly object       _sessionLock   = new();

    // ── Bounds snapshot (keyed on RuntimeHelpers.GetHashCode) ────────────────
    private static readonly Dictionary<int, (double X, double Y, double W, double H)> _boundsSnap = new();
    private static readonly Dictionary<int, bool> _visSnap = new();

    // ── Overlay / Win32 popup tracking ───────────────────────────────────────
    private static object? _overlayHandler;
    private static int     _lastTopLevelCount;

    // ── Transform watch (icon elements found near last click) ────────────────
    private static readonly HashSet<int> _watchedTransforms = new();

    // ── Grid attached-property method handles (cached once) ──────────────────
    private static MethodInfo? _gridGetRow;
    private static MethodInfo? _gridGetCol;
    private static MethodInfo? _gridGetRowSpan;
    private static MethodInfo? _gridGetColSpan;

    // =========================================================================
    // Public API
    // =========================================================================

    /// <summary>
    /// Subscribe events once. Must be called before Enable(). Safe to call multiple times.
    /// Called from StartupHook Phase 4.5i on developer channel.
    /// </summary>
    internal static void Init(AvaloniaReflection r, object mainWindow)
    {
        if (_initialized) return;
        _r          = r;
        _mainWindow = mainWindow;
        try
        {
            CacheGridMethods();
            r.SubscribePointerEvent(mainWindow, "PointerPressed",  OnPointerPressed);
            r.SubscribePointerEvent(mainWindow, "PointerReleased", OnPointerReleased);
            var overlay = r.GetOverlayLayer(mainWindow);
            if (overlay != null)
            {
                var children = r.GetChildren(overlay);
                if (children != null)
                    _overlayHandler = r.SubscribeCollectionChanged(children, () =>
                        r.RunOnUIThread(() => { try { OnOverlayChanged(overlay); } catch { } }));
            }
            _lastTopLevelCount = r.GetAllTopLevels().Count;
            _initialized = true;
            Logger.Log("ReconLogger", $"Init OK — overlay={(_overlayHandler != null ? "OK" : "null")}");
        }
        catch (Exception ex) { Logger.Log("ReconLogger", $"Init error: {ex.Message}"); }
    }

    /// <summary>
    /// Open log file and start recording. Requires Init() to have been called first.
    /// </summary>
    internal static void Enable()
    {
        if (!_initialized || _enabled) return;
        try
        {
            var logPath = Path.Combine(PlatformPaths.GetProfileDir(), "rootcord_recon.log");
            _logWriter = new StreamWriter(logPath, append: false) { AutoFlush = true };
            _enabled = true;
            WriteLog("=== Rootcord ReconLogger enabled " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ===");
            WriteLog("Click around the UI — each click starts an 800ms session window.");
            WriteLog("Log: " + logPath);
            WriteLog("");
            Logger.Log("ReconLogger", "Enabled — log at " + logPath);
        }
        catch (Exception ex) { Logger.Log("ReconLogger", $"Enable error: {ex.Message}"); }
    }

    /// <summary>
    /// Stop recording and close the log file immediately.
    /// </summary>
    internal static void Disable()
    {
        _enabled   = false;   // must be first — stops all handlers immediately
        _inSession = false;
        _sessionTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        _scanTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        _sessionTimer = null;  // null so ??= creates fresh timers on next Enable
        _scanTimer    = null;
        try { _logWriter?.Flush(); _logWriter?.Dispose(); } catch { }
        _logWriter = null;
        Logger.Log("ReconLogger", "Disabled");
    }

    // =========================================================================
    // Section A — Pointer handlers
    // =========================================================================

    private static void OnPointerPressed(double x, double y)
    {
        if (!_enabled) return;
        try
        {
            StartSession(x, y);

            // Hit test at click position
            var hit = InputHitTest(x, y);

            string typeName = hit?.GetType().Name ?? "null";
            string name     = GetStrProp(hit, "Name");
            string classes  = GetClassesStr(hit);
            string dcType   = GetDataContextTypeName(hit);
            string bounds   = GetBoundsStr(hit);
            string tbounds  = GetTransformedBoundsStr(hit);
            string grid     = GetGridAttached(hit);
            string chain    = GetParentChain(hit);

            string hitId = typeName + (name.Length > 0 ? "#" + name : "");

            string line = $"EVENT=PointerPressed Pos=({x:F0},{y:F0})"
                        + $" Hit={hitId}"
                        + (classes.Length > 0 ? $" Classes=[{classes}]" : "")
                        + (dcType.Length > 0  ? $" DataCtx={dcType}"    : "")
                        + $" Bounds={bounds} TBounds={tbounds}"
                        + (grid.Length > 0    ? $" Grid={grid}"          : "")
                        + $" ParentChain={chain}";

            WriteLogTs(line);
            LogSession(line);

            // Expander detection
            if (hit != null)
            {
                var tn = hit.GetType().Name;
                if (tn.Contains("ToggleButton", StringComparison.OrdinalIgnoreCase)
                    || tn.Contains("Expander",   StringComparison.OrdinalIgnoreCase)
                    || tn.Contains("SplitView",  StringComparison.OrdinalIgnoreCase))
                {
                    var expLine = $"EVENT=ExpanderPress Control={hitId}";
                    WriteLogTs(expLine);
                    LogSession(expLine);
                }
            }

            // Section D: find and subscribe icon/arrow elements near hit
            FindAndWatchIconElements(hit);
        }
        catch (Exception ex)
        {
            WriteLog("[Recon] OnPointerPressed error: " + ex.Message);
        }
    }

    private static void OnPointerReleased(double x, double y)
    {
        if (!_enabled) return;
        var line = $"EVENT=PointerReleased Pos=({x:F0},{y:F0})";
        WriteLogTs(line);
        LogSession(line);
    }

    // =========================================================================
    // Section B — Overlay / Popup monitoring
    // =========================================================================

    private static void OnOverlayChanged(object overlay)
    {
        if (!_enabled || _r == null) return;
        try
        {
            var children = _r.GetChildren(overlay);
            if (children == null) return;

            var wb  = _r.GetBounds(_mainWindow);
            double winW = wb?.W ?? 1920;

            var snapshot = new List<object>();
            foreach (var c in children)
                if (c != null) snapshot.Add(c);

            foreach (var child in snapshot)
            {
                var tag    = child.GetType().GetProperty("Tag")?.GetValue(child) as string ?? "";
                var bounds = _r.GetBounds(child);
                double bX  = bounds?.X ?? 0;
                double bY  = bounds?.Y ?? 0;
                double bW  = bounds?.W ?? 0;
                double bH  = bounds?.H ?? 0;

                bool overflowRight = (bX + bW) > winW * 0.85;
                string placement   = GetPopupPlacement(child);

                string line = $"EVENT=OverlayAdded Control={child.GetType().Name}"
                            + (tag.Length > 0       ? $" Tag={tag}"               : "")
                            + $" Bounds=({bX:F0},{bY:F0},{bW:F0},{bH:F0})"
                            + (placement.Length > 0 ? $" Placement={placement}"   : "")
                            + $" OverflowRight={overflowRight}";

                WriteLogTs(line);
                LogSession(line);
            }
        }
        catch (Exception ex)
        {
            WriteLog("[Recon] OnOverlayChanged error: " + ex.Message);
        }
    }

    private static string GetPopupPlacement(object? control)
    {
        if (control == null || _r == null) return "";
        try
        {
            var pm = control.GetType().GetProperty("PlacementMode")
                  ?? control.GetType().GetProperty("Placement");
            if (pm != null) return pm.GetValue(control)?.ToString() ?? "";

            // Walk one level of visual children
            foreach (var child in _r.GetVisualChildren(control))
            {
                var childPm = child.GetType().GetProperty("PlacementMode")
                           ?? child.GetType().GetProperty("Placement");
                if (childPm != null) return childPm.GetValue(child)?.ToString() ?? "";
            }
        }
        catch { }
        return "";
    }

    // =========================================================================
    // Session + Section C — Bounds tracking
    // =========================================================================

    private static void StartSession(double x, double y)
    {
        // Reset session state
        _sessionTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        _scanTimer?.Change(Timeout.Infinite, Timeout.Infinite);

        _inSession      = true;
        _sessionStart   = DateTime.UtcNow;
        _sessionClickX  = x;
        _sessionClickY  = y;

        lock (_sessionLock)
            _sessionEvents.Clear();

        // Take a bounds snapshot on the UI thread (we are already on it)
        try { TakeBoundsSnapshot(); } catch { }

        // Baseline TopLevel count for Win32 popup detection
        try { _lastTopLevelCount = _r?.GetAllTopLevels().Count ?? 0; } catch { }

        // 50ms scan ticks during session
        _scanTimer ??= new Timer(_ =>
        {
            if (_r != null)
                try { _r.RunOnUIThread(() => { try { ScanBoundsChanges(); } catch { } }); } catch { }
        }, null, Timeout.Infinite, Timeout.Infinite);
        _scanTimer.Change(50, 50);

        // 800ms session end
        _sessionTimer ??= new Timer(_ =>
        {
            if (_r != null)
                try { _r.RunOnUIThread(() => { try { EndSession(); } catch { } }); } catch { }
        }, null, Timeout.Infinite, Timeout.Infinite);
        _sessionTimer.Change(800, Timeout.Infinite);
    }

    private static void TakeBoundsSnapshot()
    {
        if (_r == null || _mainWindow == null) return;
        _boundsSnap.Clear();
        _visSnap.Clear();

        var walker = new VisualTreeWalker(_r);
        int count  = 0;
        foreach (var node in walker.DescendantsDepthFirst(_mainWindow))
        {
            if (++count > 300) break;
            int id  = RuntimeHelpers.GetHashCode(node);
            var b   = _r.GetBounds(node);
            if (b.HasValue)
                _boundsSnap[id] = (b.Value.X, b.Value.Y, b.Value.W, b.Value.H);
            _visSnap[id] = _r.GetIsVisible(node);
        }
    }

    private static void ScanBoundsChanges()
    {
        if (!_enabled || !_inSession || _r == null || _mainWindow == null) return;

        var wb    = _r.GetBounds(_mainWindow);
        double winW = wb?.W ?? 1920;

        var walker = new VisualTreeWalker(_r);
        int count  = 0;

        foreach (var node in walker.DescendantsDepthFirst(_mainWindow))
        {
            if (++count > 300) break;
            int id = RuntimeHelpers.GetHashCode(node);

            // ── Bounds delta check ────────────────────────────────────────────
            var newB = _r.GetBounds(node);
            if (newB.HasValue)
            {
                if (_boundsSnap.TryGetValue(id, out var oldB))
                {
                    double dx = Math.Abs(newB.Value.X - oldB.X);
                    double dy = Math.Abs(newB.Value.Y - oldB.Y);
                    double dw = Math.Abs(newB.Value.W - oldB.W);
                    double dh = Math.Abs(newB.Value.H - oldB.H);

                    if (dx > 1 || dy > 1 || dw > 1 || dh > 1)
                    {
                        bool nearRight = (newB.Value.X + newB.Value.W) > winW * 0.6;
                        var  parent    = _r.GetParent(node);
                        string parentId = parent != null
                            ? parent.GetType().Name + GetHashSuffix(parent)
                            : "none";

                        string line = $"EVENT=BoundsChanged Control={Identity(node)}"
                                    + $" Parent={parentId}"
                                    + $" Old=({oldB.X:F0},{oldB.Y:F0},{oldB.W:F0},{oldB.H:F0})"
                                    + $" New=({newB.Value.X:F0},{newB.Value.Y:F0},{newB.Value.W:F0},{newB.Value.H:F0})"
                                    + $" NearRightEdge={nearRight}";

                        WriteLogTs(line);
                        LogSession(line);
                        _boundsSnap[id] = (newB.Value.X, newB.Value.Y, newB.Value.W, newB.Value.H);
                    }
                }
                else
                {
                    _boundsSnap[id] = (newB.Value.X, newB.Value.Y, newB.Value.W, newB.Value.H);
                }
            }

            // ── Visibility change check ───────────────────────────────────────
            bool newVis = _r.GetIsVisible(node);
            if (_visSnap.TryGetValue(id, out bool oldVis))
            {
                if (newVis != oldVis)
                {
                    string line = $"EVENT=VisibilityChanged Control={Identity(node)} Visible={newVis}";
                    WriteLogTs(line);
                    LogSession(line);
                    _visSnap[id] = newVis;
                }
            }
            else
            {
                _visSnap[id] = newVis;
            }
        }

        // ── Win32 popup window detection ──────────────────────────────────────
        try
        {
            var topLevels = _r.GetAllTopLevels();
            int newCount  = topLevels.Count;

            if (newCount > _lastTopLevelCount)
            {
                for (int i = _lastTopLevelCount; i < topLevels.Count; i++)
                {
                    var tl     = topLevels[i];
                    var bounds = _r.GetBounds(tl);
                    bool nearRight = bounds.HasValue && (bounds.Value.X + bounds.Value.W) > winW * 0.6;

                    string line = $"EVENT=PopupWindowOpened Type={tl.GetType().Name}"
                                + $" Bounds=({bounds?.X:F0},{bounds?.Y:F0},{bounds?.W:F0},{bounds?.H:F0})"
                                + $" NearRightEdge={nearRight}";
                    WriteLogTs(line);
                    LogSession(line);
                }
            }
            else if (newCount < _lastTopLevelCount)
            {
                string line = $"EVENT=PopupWindowClosed (was {_lastTopLevelCount} → now {newCount})";
                WriteLogTs(line);
                LogSession(line);
            }

            _lastTopLevelCount = newCount;
        }
        catch { }
    }

    private static void EndSession()
    {
        if (!_inSession) return;
        _inSession = false;
        _scanTimer?.Change(Timeout.Infinite, Timeout.Infinite);

        lock (_sessionLock)
        {
            int boundsChanges = _sessionEvents.Count(e => e.Contains("BoundsChanged"));
            int popupsOpened  = _sessionEvents.Count(e => e.Contains("PopupWindowOpened"));
            int overlayAdded  = _sessionEvents.Count(e => e.Contains("OverlayAdded"));
            int visChanges    = _sessionEvents.Count(e => e.Contains("VisibilityChanged"));
            int transforms    = _sessionEvents.Count(e => e.Contains("Transform_"));

            // Determine which side the first popup appeared on
            string side = "NONE";
            var wb  = _r?.GetBounds(_mainWindow);
            double winW = wb?.W ?? 1920;
            var popupEvent = _sessionEvents.FirstOrDefault(e =>
                e.Contains("PopupWindowOpened") || e.Contains("OverlayAdded"));
            if (popupEvent != null)
            {
                var m = Regex.Match(popupEvent, @"Bounds=\((\d+(?:\.\d+)?)");
                if (m.Success && double.TryParse(m.Groups[1].Value, out double px))
                    side = px > winW / 2 ? "RIGHT" : "LEFT";
            }

            double ms = (DateTime.UtcNow - _sessionStart).TotalMilliseconds;
            string summary = $"===SESSION_END==="
                           + $" Click=({_sessionClickX:F0},{_sessionClickY:F0})"
                           + $" Duration={ms:F0}ms Events={_sessionEvents.Count}"
                           + $" BoundsChanges={boundsChanges} PopupsOpened={popupsOpened}"
                           + $" OverlayAdded={overlayAdded} VisChanges={visChanges}"
                           + $" Transforms={transforms} Side={side}";

            WriteLogTs(summary);
            WriteLog("");
        }
    }

    // =========================================================================
    // Section D — Arrow / caret / transform watching
    // =========================================================================

    private static void FindAndWatchIconElements(object? hitElement)
    {
        if (_r == null || hitElement == null) return;
        try
        {
            // Walk up a few levels to find a container to scan downward from
            object? container = hitElement;
            for (int i = 0; i < 5; i++)
            {
                var parent = _r.GetParent(container);
                if (parent == null) break;
                container = parent;
            }

            var walker = new VisualTreeWalker(_r);
            foreach (var node in walker.DescendantsDepthFirst(container))
            {
                int nodeId = RuntimeHelpers.GetHashCode(node);
                if (_watchedTransforms.Contains(nodeId)) continue;

                bool isIconLike = false;
                var  tn         = node.GetType().Name;
                var  nodeName   = node.GetType().GetProperty("Name")?.GetValue(node) as string ?? "";

                // Path / PathIcon → SVG arrow shapes
                if (tn.Contains("Path") || tn == "PathIcon")
                    isIconLike = true;

                // Single-char TextBlock → glyph icon
                if (tn.Contains("TextBlock"))
                {
                    var text = node.GetType().GetProperty("Text")?.GetValue(node) as string ?? "";
                    if (text.Length == 1) isIconLike = true;
                }

                // Arrow / caret / chevron / expand by name
                if (nodeName.Contains("arrow",   StringComparison.OrdinalIgnoreCase)
                    || nodeName.Contains("caret",   StringComparison.OrdinalIgnoreCase)
                    || nodeName.Contains("chevron", StringComparison.OrdinalIgnoreCase)
                    || nodeName.Contains("expand",  StringComparison.OrdinalIgnoreCase))
                    isIconLike = true;

                // Any control already carrying a non-null RenderTransform
                var rt = node.GetType().GetProperty("RenderTransform")?.GetValue(node);
                if (rt != null) isIconLike = true;

                if (!isIconLike) continue;

                _watchedTransforms.Add(nodeId);
                LogTransformState(node, "INITIAL");
                SubscribeTransformChanges(node);
            }
        }
        catch { }
    }

    private static void LogTransformState(object? control, string context)
    {
        if (control == null) return;
        try
        {
            var rt = control.GetType().GetProperty("RenderTransform")?.GetValue(control);
            if (rt == null) return;

            string rtName  = rt.GetType().Name;
            double? angle  = rt.GetType().GetProperty("Angle")?.GetValue(rt)  as double?;
            double? scaleX = rt.GetType().GetProperty("ScaleX")?.GetValue(rt) as double?;
            double? scaleY = rt.GetType().GetProperty("ScaleY")?.GetValue(rt) as double?;

            // TransformGroup — enumerate children
            string childInfo = "";
            var tgChildren = rt.GetType().GetProperty("Children")?.GetValue(rt) as IEnumerable;
            if (tgChildren != null)
            {
                var parts = new List<string>();
                foreach (var c in tgChildren)
                {
                    double? cAngle  = c.GetType().GetProperty("Angle")?.GetValue(c)  as double?;
                    double? cScaleX = c.GetType().GetProperty("ScaleX")?.GetValue(c) as double?;
                    parts.Add(c.GetType().Name
                        + (cAngle.HasValue  ? $"(Angle={cAngle:F1})"   : "")
                        + (cScaleX.HasValue ? $"(ScaleX={cScaleX:F2})" : ""));
                }
                childInfo = " Children=[" + string.Join(",", parts) + "]";
            }

            string line = $"EVENT=Transform_{context} Control={Identity(control)}"
                        + $" TransformType={rtName}"
                        + (angle.HasValue  ? $" Angle={angle:F1}"   : "")
                        + (scaleX.HasValue ? $" ScaleX={scaleX:F2}" : "")
                        + (scaleY.HasValue ? $" ScaleY={scaleY:F2}" : "")
                        + childInfo;

            WriteLogTs(line);
            LogSession(line);
        }
        catch { }
    }

    private static void SubscribeTransformChanges(object control)
    {
        if (_r == null) return;
        try
        {
            var eventInfo = control.GetType().GetEvent("PropertyChanged");
            if (eventInfo == null) return;

            var handlerType = eventInfo.EventHandlerType;
            if (handlerType == null) return;

            var invokeMethod = handlerType.GetMethod("Invoke");
            if (invokeMethod == null) return;

            var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();
            if (paramTypes.Length < 2) return;

            var p0 = Expression.Parameter(paramTypes[0], "sender");
            var p1 = Expression.Parameter(paramTypes[1], "e");

            // Capture control reference for the callback
            var capturedControl = control;
            Action<object, object> innerCb = (_, e) =>
            {
                try
                {
                    // e is AvaloniaPropertyChangedEventArgs — get Property.Name
                    var propHolder = e.GetType().GetProperty("Property")?.GetValue(e);
                    var propName   = propHolder?.GetType().GetProperty("Name")?.GetValue(propHolder) as string ?? "";
                    if (!propName.Equals("RenderTransform", StringComparison.OrdinalIgnoreCase)) return;
                    _r?.RunOnUIThread(() => LogTransformState(capturedControl, "Changed"));
                }
                catch { }
            };

            var cbConst = Expression.Constant(innerCb);
            var body    = Expression.Invoke(cbConst,
                Expression.Convert(p0, typeof(object)),
                Expression.Convert(p1, typeof(object)));
            var lambda  = Expression.Lambda(handlerType, body, p0, p1);

            eventInfo.AddEventHandler(control, lambda.Compile());
        }
        catch { }
    }

    // =========================================================================
    // Helpers
    // =========================================================================

    private static object? InputHitTest(double x, double y)
    {
        if (_r == null || _mainWindow == null || _r.PointType == null) return null;
        try
        {
            var pt = Activator.CreateInstance(_r.PointType, x, y);
            if (pt == null) return null;

            var flags = BindingFlags.Public | BindingFlags.Instance;
            var m = _mainWindow.GetType().GetMethod("InputHitTest", flags)
                 ?? _mainWindow.GetType().GetMethods(flags)
                        .FirstOrDefault(mi =>
                            (mi.Name == "InputHitTest" || mi.Name == "GetVisualAt")
                            && mi.GetParameters().Length == 1);

            return m?.Invoke(_mainWindow, new[] { pt });
        }
        catch { return null; }
    }

    private static void CacheGridMethods()
    {
        if (_r?.GridType == null) return;
        var f = BindingFlags.Public | BindingFlags.Static;
        _gridGetRow     = _r.GridType.GetMethod("GetRow",        f);
        _gridGetCol     = _r.GridType.GetMethod("GetColumn",     f);
        _gridGetRowSpan = _r.GridType.GetMethod("GetRowSpan",    f);
        _gridGetColSpan = _r.GridType.GetMethod("GetColumnSpan", f);
    }

    private static string GetGridAttached(object? control)
    {
        if (control == null || _gridGetRow == null) return "";
        try
        {
            var row     = _gridGetRow.Invoke(null, new[] { control });
            var col     = _gridGetCol?.Invoke(null, new[] { control });
            var rowSpan = _gridGetRowSpan?.Invoke(null, new[] { control });
            var colSpan = _gridGetColSpan?.Invoke(null, new[] { control });
            return $"Row:{row},Col:{col},RS:{rowSpan},CS:{colSpan}";
        }
        catch { return ""; }
    }

    private static string GetClassesStr(object? control)
    {
        if (control == null) return "";
        try
        {
            var classes = control.GetType().GetProperty("Classes")?.GetValue(control);
            if (classes is IEnumerable en)
            {
                var parts = new List<string>();
                foreach (var c in en)
                    if (c != null) parts.Add(c.ToString() ?? "");
                return string.Join(" ", parts);
            }
        }
        catch { }
        return "";
    }

    private static string GetDataContextTypeName(object? control)
    {
        if (control == null) return "";
        try
        {
            var dc = control.GetType().GetProperty("DataContext")?.GetValue(control);
            return dc?.GetType().Name ?? "";
        }
        catch { return ""; }
    }

    private static string GetBoundsStr(object? control)
    {
        if (_r == null || control == null) return "(?)";
        var b = _r.GetBounds(control);
        return b.HasValue
            ? $"({b.Value.X:F0},{b.Value.Y:F0},{b.Value.W:F0},{b.Value.H:F0})"
            : "(?)";
    }

    private static string GetTransformedBoundsStr(object? control)
    {
        if (control == null) return "(?)";
        try
        {
            // Reflection on Nullable<TransformedBounds>:
            // GetValue returns null if HasValue==false, or a boxed TransformedBounds if true.
            var tb = control.GetType().GetProperty("TransformedBounds")?.GetValue(control);
            if (tb == null) return "(null)";

            // tb is boxed TransformedBounds — read .Bounds (Rect)
            var bounds = tb.GetType().GetProperty("Bounds")?.GetValue(tb);
            if (bounds == null) return "(?)";

            double? bx = bounds.GetType().GetProperty("X")?.GetValue(bounds)      as double?;
            double? by = bounds.GetType().GetProperty("Y")?.GetValue(bounds)      as double?;
            double? bw = bounds.GetType().GetProperty("Width")?.GetValue(bounds)  as double?;
            double? bh = bounds.GetType().GetProperty("Height")?.GetValue(bounds) as double?;
            return $"({bx:F0},{by:F0},{bw:F0},{bh:F0})";
        }
        catch { return "(err)"; }
    }

    private static string GetParentChain(object? control)
    {
        if (_r == null || control == null) return "none";
        var parts   = new List<string>();
        var current = _r.GetParent(control);
        int depth   = 0;
        while (current != null && depth < 8)
        {
            var n = current.GetType().GetProperty("Name")?.GetValue(current) as string ?? "";
            parts.Add(current.GetType().Name + (n.Length > 0 ? "#" + n : ""));
            current = _r.GetParent(current);
            depth++;
        }
        return parts.Count > 0 ? string.Join(">", parts) : "none";
    }

    private static string Identity(object? control)
    {
        if (control == null) return "null";
        var n = control.GetType().GetProperty("Name")?.GetValue(control) as string ?? "";
        return control.GetType().Name
             + (n.Length > 0 ? "#" + n : "")
             + GetHashSuffix(control);
    }

    private static string GetHashSuffix(object? control)
        => control == null ? "" : "@" + (RuntimeHelpers.GetHashCode(control) & 0xFFFF).ToString("X4");

    private static string GetStrProp(object? obj, string propName)
    {
        if (obj == null) return "";
        try { return obj.GetType().GetProperty(propName)?.GetValue(obj) as string ?? ""; }
        catch { return ""; }
    }

    private static void LogSession(string line)
    {
        if (!_inSession) return;
        lock (_sessionLock)
            _sessionEvents.Add(line);
    }

    private static void WriteLogTs(string line)
    {
        var ts   = DateTime.Now.ToString("HH:mm:ss.fff");
        var full = $"[{ts}] {line}";
        WriteLog(full);
        Console.WriteLine("[Recon] " + full);
    }

    private static void WriteLog(string line)
    {
        if (_logWriter == null) return;
        lock (_logLock)
        {
            try { _logWriter.WriteLine(line); }
            catch { }
        }
    }
}
