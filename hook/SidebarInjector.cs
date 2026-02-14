namespace Uprooted;

/// <summary>
/// Timer-based monitor that detects when the settings page is open and injects
/// UPROOTED UI using Avalonia's OverlayLayer -- a Canvas at the Window level,
/// completely outside the settings page visual tree.
///
/// This avoids the freeze-on-back-button caused by modifying the settings page tree
/// (OnDetachedFromVisualTreeCore walks all VisualChildren during teardown).
///
/// Architecture:
///   Window -> VisualLayerManager -> OverlayLayer (Canvas) <- OUR CONTROLS
///                                -> ContentPresenter (Root's normal UI)
///                                -> AdornerLayer
/// </summary>
internal class SidebarInjector
{
    private const string InjectedTag = "uprooted-injected";
    private const string ContentTag = "uprooted-content";
    private const int PollIntervalMs = 200;

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly UprootedSettings _settings;
    private Timer? _timer;
    private readonly object _window;

    // OverlayLayer state
    private object? _overlayLayer;        // OverlayLayer canvas (Window-level)
    private object? _sidebarOverlay;      // Our sidebar panel in OverlayLayer
    private object? _contentOverlay;      // Our content page in OverlayLayer

    // Anchors for positioning (elements in Root's tree we DON'T modify)
    private object? _sidebarAnchor;       // navContainer child[1] (version Border) - position reference
    private object? _contentAnchor;       // layout.ContentArea - position reference
    private object? _listBox;

    // Elements we hide (Opacity=0, IsHitTestVisible=false) to make room
    private object? _hiddenVersion;       // Border (version info)
    private object? _hiddenSignOut;       // ContentControl (sign out)

    // Navigation state
    private string? _activePage;
    private bool _ourItemClicked;

    // Cached positions for change detection
    private double _lastSidebarLeft, _lastSidebarTop;
    private double _lastContentLeft, _lastContentTop;
    private double _lastContentW, _lastContentH;

    // Thread safety
    private int _injecting;
    private bool _diagnosticsDone;

    public SidebarInjector(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _settings = UprootedSettings.Load();
        _window = mainWindow;
    }

    public void StartMonitoring()
    {
        Logger.Log("Injector", "Starting settings page monitor (OverlayLayer mode)");
        _timer = new Timer(OnTimerTick, null, PollIntervalMs, PollIntervalMs);
    }

    public void StopMonitoring()
    {
        _timer?.Dispose();
        _timer = null;
    }

    private void OnTimerTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _injecting, 1, 0) != 0) return;

        try
        {
            _r.RunOnUIThread(() =>
            {
                try { CheckAndInject(); }
                catch (Exception ex) { Logger.Log("Injector", $"CheckAndInject error: {ex.Message}"); }
                finally { Interlocked.Exchange(ref _injecting, 0); }
            });

            // Safety timeout to prevent deadlock
            Task.Delay(3000).ContinueWith(_ =>
            {
                Interlocked.CompareExchange(ref _injecting, 0, 1);
            });
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"OnTimerTick error: {ex.Message}");
            Interlocked.Exchange(ref _injecting, 0);
        }
    }

    private void CheckAndInject()
    {
        if (_sidebarOverlay != null)
        {
            // Overlays exist -- check if settings page is still open
            var layout = _walker.FindSettingsLayout(_window);
            if (layout == null)
            {
                // Settings closed -- clean up overlays
                Logger.Log("Injector", "Settings page closed, cleaning up overlays");
                CleanupOverlays();
                return;
            }

            // Settings still open -- update positions and check navigation
            UpdateOverlayPositions();

            // Check if user clicked a Root sidebar item (ListBox selection changed)
            if (_contentOverlay != null && _listBox != null)
            {
                int selectedIdx = _r.GetSelectedIndex(_listBox);
                if (selectedIdx >= 0)
                {
                    Logger.Log("Injector", $"ListBox selection changed to {selectedIdx}, removing content overlay");
                    RemoveContentOverlay();
                }
            }
            return;
        }

        // No overlays -- check if settings page just opened
        var newLayout = _walker.FindSettingsLayout(_window);
        if (newLayout == null) return;

        Logger.Log("Injector", "Settings page detected, creating overlays via OverlayLayer");

        if (!_diagnosticsDone)
        {
            _diagnosticsDone = true;
            DumpSidebarStyling(newLayout);
        }

        CreateOverlays(newLayout);
    }

    // ===== Overlay lifecycle =====

    private void CreateOverlays(SettingsLayout layout)
    {
        try
        {
            // Step 1: Get the OverlayLayer
            _overlayLayer = _r.GetOverlayLayer(_window);
            if (_overlayLayer == null)
            {
                Logger.Log("Injector", "OverlayLayer not found on window, skipping injection");
                return;
            }
            Logger.Log("Injector", $"OverlayLayer resolved: {_overlayLayer.GetType().Name}");

            // Step 2: Find anchors in Root's tree (we read positions but don't modify)
            _listBox = layout.ListBox;
            var navContainer = layout.NavContainer;
            var navChildCount = _r.GetChildCount(navContainer);

            // navContainer is a StackPanel with children:
            //   [0] ListBox (14 items, W=250)
            //   [1] Border (version info "Root Version 0.9.86")
            //   [2] ContentControl (sign out)
            _hiddenVersion = navChildCount > 1 ? _r.GetChild(navContainer, 1) : null;
            _hiddenSignOut = navChildCount > 2 ? _r.GetChild(navContainer, 2) : null;
            _sidebarAnchor = _hiddenVersion ?? navContainer;
            _contentAnchor = layout.ContentArea;

            Logger.Log("Injector", $"NavContainer: {navContainer.GetType().Name}, {navChildCount} children");
            for (int i = 0; i < navChildCount && i < 5; i++)
            {
                var child = _r.GetChild(navContainer, i);
                Logger.Log("Injector", $"  Child[{i}]: {child?.GetType().Name}");
            }

            // Step 3: Calculate sidebar position
            var sidebarPos = _r.TranslatePoint(_sidebarAnchor, 0, 0, _overlayLayer);
            if (sidebarPos == null)
            {
                Logger.Log("Injector", "TranslatePoint failed for sidebar anchor, trying bounds fallback");
                sidebarPos = GetBoundsPosition(_sidebarAnchor);
            }
            if (sidebarPos == null)
            {
                Logger.Log("Injector", "Could not determine sidebar position, aborting");
                _overlayLayer = null;
                return;
            }

            var sidebarBounds = _r.GetBounds(_sidebarAnchor);
            double sidebarWidth = sidebarBounds?.W ?? 250;

            Logger.Log("Injector", $"Sidebar anchor position: ({sidebarPos.Value.X:F0}, {sidebarPos.Value.Y:F0}), width={sidebarWidth:F0}");

            // Step 4: Build sidebar overlay
            _sidebarOverlay = BuildSidebarOverlay(layout, sidebarWidth);
            if (_sidebarOverlay == null)
            {
                Logger.Log("Injector", "Failed to build sidebar overlay");
                _overlayLayer = null;
                return;
            }

            _r.SetTag(_sidebarOverlay, InjectedTag);
            _r.SetCanvasPosition(_sidebarOverlay, sidebarPos.Value.X, sidebarPos.Value.Y);
            _lastSidebarLeft = sidebarPos.Value.X;
            _lastSidebarTop = sidebarPos.Value.Y;

            _r.AddToOverlay(_overlayLayer, _sidebarOverlay);

            // Step 5: Hide original version + sign out (preserve layout space with Opacity=0)
            if (_hiddenVersion != null)
            {
                _r.SetOpacity(_hiddenVersion, 0);
                _r.SetIsHitTestVisible(_hiddenVersion, false);
            }
            if (_hiddenSignOut != null)
            {
                _r.SetOpacity(_hiddenSignOut, 0);
                _r.SetIsHitTestVisible(_hiddenSignOut, false);
            }

            // Step 6: Subscribe to ListBox clicks for navigation
            SubscribeListBoxSelection();

            Logger.Log("Injector", "Overlays created successfully");
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"CreateOverlays error: {ex}");
            CleanupOverlays();
        }
    }

    private void CleanupOverlays()
    {
        try
        {
            if (_overlayLayer != null)
            {
                if (_sidebarOverlay != null)
                    _r.RemoveFromOverlay(_overlayLayer, _sidebarOverlay);
                if (_contentOverlay != null)
                    _r.RemoveFromOverlay(_overlayLayer, _contentOverlay);
            }

            // Restore hidden elements
            if (_hiddenVersion != null)
            {
                _r.SetOpacity(_hiddenVersion, 1);
                _r.SetIsHitTestVisible(_hiddenVersion, true);
            }
            if (_hiddenSignOut != null)
            {
                _r.SetOpacity(_hiddenSignOut, 1);
                _r.SetIsHitTestVisible(_hiddenSignOut, true);
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"CleanupOverlays error: {ex.Message}");
        }

        _overlayLayer = null;
        _sidebarOverlay = null;
        _contentOverlay = null;
        _sidebarAnchor = null;
        _contentAnchor = null;
        _hiddenVersion = null;
        _hiddenSignOut = null;
        _listBox = null;
        _activePage = null;
    }

    private void UpdateOverlayPositions()
    {
        if (_overlayLayer == null || _sidebarAnchor == null) return;

        try
        {
            // Update sidebar position
            var sidebarPos = _r.TranslatePoint(_sidebarAnchor, 0, 0, _overlayLayer);
            if (sidebarPos != null && _sidebarOverlay != null)
            {
                if (Math.Abs(sidebarPos.Value.X - _lastSidebarLeft) > 0.5 ||
                    Math.Abs(sidebarPos.Value.Y - _lastSidebarTop) > 0.5)
                {
                    _r.SetCanvasPosition(_sidebarOverlay, sidebarPos.Value.X, sidebarPos.Value.Y);
                    _lastSidebarLeft = sidebarPos.Value.X;
                    _lastSidebarTop = sidebarPos.Value.Y;
                }
            }

            // Update content overlay position
            if (_contentOverlay != null && _contentAnchor != null)
            {
                var contentPos = _r.TranslatePoint(_contentAnchor, 0, 0, _overlayLayer);
                var contentBounds = _r.GetBounds(_contentAnchor);
                if (contentPos != null && contentBounds != null)
                {
                    bool moved = Math.Abs(contentPos.Value.X - _lastContentLeft) > 0.5 ||
                                 Math.Abs(contentPos.Value.Y - _lastContentTop) > 0.5;
                    bool resized = Math.Abs(contentBounds.Value.W - _lastContentW) > 0.5 ||
                                   Math.Abs(contentBounds.Value.H - _lastContentH) > 0.5;

                    if (moved)
                    {
                        _r.SetCanvasPosition(_contentOverlay, contentPos.Value.X, contentPos.Value.Y);
                        _lastContentLeft = contentPos.Value.X;
                        _lastContentTop = contentPos.Value.Y;
                    }
                    if (resized)
                    {
                        _r.SetWidth(_contentOverlay, contentBounds.Value.W);
                        _r.SetHeight(_contentOverlay, contentBounds.Value.H);
                        _lastContentW = contentBounds.Value.W;
                        _lastContentH = contentBounds.Value.H;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"UpdateOverlayPositions error: {ex.Message}");
        }
    }

    // ===== Sidebar overlay builder =====

    private object? BuildSidebarOverlay(SettingsLayout layout, double width)
    {
        var headerFontSize = _r.GetFontSize(layout.AppSettingsText) ?? 11;
        var headerFontWeight = _r.GetFontWeight(layout.AppSettingsText);
        var headerForeground = _r.GetForeground(layout.AppSettingsText);

        // Calculate height from hidden elements to match exactly
        double overlayHeight = 0;
        if (_hiddenVersion != null)
        {
            var vb = _r.GetBounds(_hiddenVersion);
            if (vb != null) overlayHeight += vb.Value.H;
        }
        if (_hiddenSignOut != null)
        {
            var sb = _r.GetBounds(_hiddenSignOut);
            if (sb != null) overlayHeight += sb.Value.H;
        }

        // Outer border matching sidebar background
        var container = _r.CreateStackPanel(vertical: true, spacing: 0);
        if (container == null) return null;

        _r.SetWidth(container, width);
        _r.SetBackground(container, "#ff07101b");

        // Section header: "UPROOTED" with dots
        var headerContainer = _r.CreateStackPanel(vertical: false, spacing: 0);
        if (headerContainer != null)
        {
            _r.SetMargin(headerContainer, 12, 12, 12, 0);

            var leftDot = _r.CreateEllipse(8, 8);
            if (leftDot != null)
            {
                _r.SetMargin(leftDot, 0, 0, 12, 0);
                _r.AddChild(headerContainer, leftDot);
            }

            var header = _r.CreateTextBlock("UPROOTED", headerFontSize);
            if (header != null)
            {
                if (headerFontWeight != null)
                    _r.TextBlockType?.GetProperty("FontWeight")?.SetValue(header, headerFontWeight);
                if (headerForeground != null)
                    _r.TextBlockType?.GetProperty("Foreground")?.SetValue(header, headerForeground);
                _r.AddChild(headerContainer, header);
            }

            var rightDot = _r.CreateEllipse(8, 8);
            if (rightDot != null)
            {
                _r.SetMargin(rightDot, 8, 0, 0, 0);
                _r.AddChild(headerContainer, rightDot);
            }

            _r.AddChild(container, headerContainer);
        }

        // Nav items
        foreach (var (label, page) in new[] { ("Uprooted", "uprooted"), ("Plugins", "plugins"), ("Themes", "themes") })
        {
            var item = BuildSidebarItem(label, page, layout);
            if (item != null)
                _r.AddChild(container, item);
        }

        // Version info replica
        var versionText = _walker.FindFirstTextBlockContaining(_window, "Root Version");
        string versionStr = versionText != null ? (_r.GetText(versionText) ?? "Root Version") : "Root Version";
        var uprootedVersionStr = $"Uprooted {_settings.Version}";

        var versionPanel = _r.CreateStackPanel(vertical: true, spacing: 2);
        if (versionPanel != null)
        {
            _r.SetMargin(versionPanel, 12, 8, 12, 8);

            var rootVersionLabel = _r.CreateTextBlock(versionStr, 11, "#66f2f2f2");
            _r.AddChild(versionPanel, rootVersionLabel);

            var uprootedVersionLabel = _r.CreateTextBlock(uprootedVersionStr, 11, "#66f2f2f2");
            if (uprootedVersionLabel != null)
            {
                _r.SetCursorHand(uprootedVersionLabel);
                _r.SubscribeEvent(uprootedVersionLabel, "PointerPressed", () =>
                {
                    _r.CopyToClipboard(_window, uprootedVersionStr);
                    Logger.Log("Injector", $"Version copied: {uprootedVersionStr}");
                });
            }
            _r.AddChild(versionPanel, uprootedVersionLabel);

            _r.AddChild(container, versionPanel);
        }

        return container;
    }

    private object? BuildSidebarItem(string label, string pageName, SettingsLayout layout)
    {
        var panel = _r.CreatePanel();
        if (panel == null) return null;
        _r.SetMargin(panel, 0, 2, 0, 2);
        _r.SetTag(panel, $"uprooted-item-{pageName}");
        _r.SetCursorHand(panel);

        var content = _r.CreateStackPanel(vertical: false, spacing: 0);
        if (content == null) return null;
        _r.SetMargin(content, 12, 8, 12, 8);
        _r.SetVerticalAlignment(content, "Center");

        var leftDot = _r.CreateEllipse(8, 8);
        if (leftDot != null)
        {
            _r.SetMargin(leftDot, 0, 0, 12, 0);
            _r.SetVerticalAlignment(leftDot, "Center");
            _r.AddChild(content, leftDot);
        }

        var textBlock = _r.CreateTextBlock(label, 14, "#f2f2f2");
        if (textBlock != null)
        {
            _r.SetFontWeightNumeric(textBlock, 450);
            _r.SetVerticalAlignment(textBlock, "Center");
            _r.AddChild(content, textBlock);
        }

        var rightDot = _r.CreateEllipse(8, 8);
        if (rightDot != null)
        {
            _r.SetMargin(rightDot, 8, 0, 0, 0);
            _r.SetVerticalAlignment(rightDot, "Center");
            _r.AddChild(content, rightDot);
        }

        _r.AddChild(panel, content);

        var highlight = _r.CreateBorder(cornerRadius: 12);
        if (highlight != null)
        {
            highlight.GetType().GetProperty("Height")?.SetValue(highlight, 36.0);
            _r.SetTag(highlight, $"uprooted-highlight-{pageName}");
            _r.AddChild(panel, highlight);
        }

        _r.SubscribeEvent(panel, "PointerEntered", () =>
        {
            if (_activePage != pageName && highlight != null)
                _r.SetBackground(highlight, "#0Dffffff");
        });

        _r.SubscribeEvent(panel, "PointerExited", () =>
        {
            if (_activePage != pageName && highlight != null)
                _r.SetBackground(highlight, (string?)null);
        });

        var capturedLayout = layout;
        _r.SubscribeEvent(panel, "PointerPressed", () =>
        {
            OnItemClicked(pageName, capturedLayout);
        });

        return panel;
    }

    // ===== Content overlay =====

    private void OnItemClicked(string pageName, SettingsLayout layout)
    {
        try
        {
            Logger.Log("Injector", $"Item clicked: {pageName}");
            _ourItemClicked = true;

            if (_activePage == pageName) return;

            RemoveContentOverlay();

            var page = ContentPages.BuildPage(pageName, _r, _settings);
            if (page == null)
            {
                Logger.Log("Injector", $"Failed to build page: {pageName}");
                return;
            }

            _r.SetTag(page, ContentTag);

            // Get content area position and size
            if (_overlayLayer == null || _contentAnchor == null) return;

            var contentPos = _r.TranslatePoint(_contentAnchor, 0, 0, _overlayLayer);
            var contentBounds = _r.GetBounds(_contentAnchor);

            if (contentPos == null || contentBounds == null)
            {
                Logger.Log("Injector", "Could not determine content area position/bounds");
                return;
            }

            double contentW = contentBounds.Value.W;
            double contentH = contentBounds.Value.H;

            // Wrap page in a Border with opaque background to cover Root's content
            var wrapper = _r.CreateBorder("#0D1521", 0, page);
            if (wrapper == null) return;

            _r.SetWidth(wrapper, contentW);
            _r.SetHeight(wrapper, contentH);
            // Clip content to the wrapper bounds
            wrapper.GetType().GetProperty("ClipToBounds")?.SetValue(wrapper, true);

            _r.SetCanvasPosition(wrapper, contentPos.Value.X, contentPos.Value.Y);
            _r.AddToOverlay(_overlayLayer, wrapper);

            _contentOverlay = wrapper;
            _activePage = pageName;
            _lastContentLeft = contentPos.Value.X;
            _lastContentTop = contentPos.Value.Y;
            _lastContentW = contentW;
            _lastContentH = contentH;

            Logger.Log("Injector", $"Content page '{pageName}' displayed at ({contentPos.Value.X:F0}, {contentPos.Value.Y:F0}) {contentW:F0}x{contentH:F0}");

            UpdateItemHighlights();

            // Deselect ListBox so Root doesn't show its own content
            if (_listBox != null)
                _r.SetSelectedIndex(_listBox, -1);
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"OnItemClicked error: {ex}");
        }
    }

    private void RemoveContentOverlay()
    {
        if (_overlayLayer != null && _contentOverlay != null)
        {
            try
            {
                _r.RemoveFromOverlay(_overlayLayer, _contentOverlay);
                Logger.Log("Injector", "Content overlay removed");
            }
            catch (Exception ex)
            {
                Logger.Log("Injector", $"RemoveContentOverlay error: {ex.Message}");
            }
        }

        _contentOverlay = null;
        _activePage = null;
        UpdateItemHighlights();
    }

    // ===== Navigation tracking =====

    private void SubscribeListBoxSelection()
    {
        if (_listBox == null) return;

        _r.SubscribeEvent(_listBox, "PointerPressed", () =>
        {
            if (_ourItemClicked)
            {
                _ourItemClicked = false;
                return;
            }
            if (_contentOverlay != null)
            {
                Logger.Log("Injector", "ListBox click, removing content overlay");
                RemoveContentOverlay();
            }
        });

        Logger.Log("Injector", "Navigation tracking active");
    }

    private void UpdateItemHighlights()
    {
        if (_sidebarOverlay == null) return;

        foreach (var node in _walker.DescendantsDepthFirst(_sidebarOverlay))
        {
            var tag = _r.GetTag(node);
            if (tag == null || !tag.StartsWith("uprooted-highlight-")) continue;

            var itemPage = tag["uprooted-highlight-".Length..];
            _r.SetBackground(node, itemPage == _activePage ? "#19ffffff" : null);
        }
    }

    // ===== Fallback positioning =====

    /// <summary>
    /// Fallback: compute screen-relative position by walking the parent chain
    /// and summing Bounds offsets. Used when TranslatePoint returns null.
    /// </summary>
    private (double X, double Y)? GetBoundsPosition(object? control)
    {
        if (control == null) return null;

        double x = 0, y = 0;
        var current = control;
        int depth = 0;

        while (current != null && depth < 50)
        {
            var bounds = _r.GetBounds(current);
            if (bounds != null)
            {
                x += bounds.Value.X;
                y += bounds.Value.Y;
            }
            current = _r.GetParent(current);
            depth++;
        }

        return (x, y);
    }

    // ===== Diagnostics =====

    private void DumpSidebarStyling(SettingsLayout layout)
    {
        try
        {
            var hdr = layout.AppSettingsText;
            Logger.Log("Style", $"=== Section header '{_r.GetText(hdr)}' ===");
            Logger.Log("Style", $"  FontSize={_r.GetFontSize(hdr)}, FontWeight={_r.GetFontWeight(hdr)}, " +
                $"Foreground={_r.GetForeground(hdr)}, Margin={GetMarginStr(hdr)}");

            var headerParent = _r.GetParent(hdr);
            if (headerParent != null)
                Logger.Log("Style", $"  HeaderParent: {headerParent.GetType().Name} Margin={GetMarginStr(headerParent)}");

            int itemsDumped = 0;
            if (_listBox != null)
            {
                Logger.Log("Style", "=== ListBox tree dump ===");
                DumpFullTree(_listBox, 0, 20, ref itemsDumped, true);
            }

            Logger.Log("Style", "=== Nav parent chain ===");
            var navParent = _r.GetParent(layout.NavContainer);
            for (int d = 0; d < 6 && navParent != null; d++)
            {
                Logger.Log("Style", $"  NavParent[{d}]: {navParent.GetType().Name}, Margin={GetMarginStr(navParent)}, BG={GetBgStr(navParent)}");
                navParent = _r.GetParent(navParent);
            }

            if (layout.IsGridLayout && layout.LayoutContainer != null)
            {
                Logger.Log("Style", "=== Grid definitions ===");
                try
                {
                    var colDefs = layout.LayoutContainer.GetType().GetProperty("ColumnDefinitions")?.GetValue(layout.LayoutContainer);
                    if (colDefs is System.Collections.IList cols)
                        for (int i = 0; i < cols.Count; i++)
                            Logger.Log("Style", $"  Col[{i}]: Width={cols[i]?.GetType().GetProperty("Width")?.GetValue(cols[i])}");
                    var rowDefs = layout.LayoutContainer.GetType().GetProperty("RowDefinitions")?.GetValue(layout.LayoutContainer);
                    if (rowDefs is System.Collections.IList rows)
                        for (int i = 0; i < rows.Count; i++)
                            Logger.Log("Style", $"  Row[{i}]: Height={rows[i]?.GetType().GetProperty("Height")?.GetValue(rows[i])}");
                }
                catch (Exception ex) { Logger.Log("Style", $"  Grid defs error: {ex.Message}"); }
            }

            // OverlayLayer info
            var overlay = _r.GetOverlayLayer(_window);
            Logger.Log("Style", $"=== OverlayLayer: {(overlay != null ? overlay.GetType().Name : "NULL")} ===");
            if (overlay != null)
            {
                var overlayBounds = _r.GetBounds(overlay);
                Logger.Log("Style", $"  Bounds: {overlayBounds?.X:F0},{overlayBounds?.Y:F0} {overlayBounds?.W:F0}x{overlayBounds?.H:F0}");
                Logger.Log("Style", $"  Children: {_r.GetChildren(overlay)?.Count ?? 0}");
            }

            Logger.Log("Style", "=== NavContainer ancestry ===");
            var nc = layout.NavContainer;
            Logger.Log("Style", $"  NavContainer itself: {nc.GetType().Name}({_r.GetChildCount(nc)} children)");
            var ncParent = _r.GetParent(nc);
            for (int d = 0; d < 10 && ncParent != null; d++)
            {
                var t = ncParent.GetType().Name;
                var cc = _r.GetChildCount(ncParent);
                Logger.Log("Style", $"  Ancestor[{d}]: {t}({cc} children)");
                ncParent = _r.GetParent(ncParent);
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Style", $"DumpSidebarStyling error: {ex.Message}");
        }
    }

    private void DumpFullTree(object node, int depth, int maxDepth, ref int itemsDumped, bool listBoxMode)
    {
        if (depth > maxDepth) return;
        if (listBoxMode && itemsDumped >= 3) return;

        var typeName = node.GetType().Name;
        var indent = new string(' ', depth * 2);
        var props = new List<string>();

        if (_r.IsTextBlock(node))
        {
            props.Add($"Text=\"{_r.GetText(node)}\"");
            props.Add($"FontSize={_r.GetFontSize(node)}");
            props.Add($"FontWeight={_r.GetFontWeight(node)}");
            props.Add($"Fg={_r.GetForeground(node)}");
            props.Add($"Margin={GetMarginStr(node)}");
        }
        else
        {
            props.Add($"Margin={GetMarginStr(node)}");
            var bg = GetBgStr(node);
            if (bg != "err" && bg != "none") props.Add($"BG={bg}");
            var fill = node.GetType().GetProperty("Fill")?.GetValue(node)?.ToString();
            if (fill != null) props.Add($"Fill={fill}");
            if (_r.IsBorder(node))
            {
                var cr = node.GetType().GetProperty("CornerRadius")?.GetValue(node);
                if (cr != null) props.Add($"CR={cr}");
            }
            var w = node.GetType().GetProperty("Width")?.GetValue(node);
            var h = node.GetType().GetProperty("Height")?.GetValue(node);
            if (w != null && w.ToString() != "NaN") props.Add($"W={w}");
            if (h != null && h.ToString() != "NaN") props.Add($"H={h}");
        }

        Logger.Log("Style", $"{indent}{typeName} {string.Join(", ", props)}");

        if (typeName == "ListBoxItem" && listBoxMode)
        {
            itemsDumped++;
            if (itemsDumped > 3) return;
        }

        foreach (var child in _r.GetVisualChildren(node))
        {
            DumpFullTree(child, depth + 1, maxDepth, ref itemsDumped, listBoxMode);
            if (listBoxMode && itemsDumped > 3) return;
        }
    }

    private string GetMarginStr(object? ctrl)
    {
        try { return ctrl?.GetType().GetProperty("Margin")?.GetValue(ctrl)?.ToString() ?? "none"; }
        catch { return "err"; }
    }

    private string GetBgStr(object? ctrl)
    {
        try { return ctrl?.GetType().GetProperty("Background")?.GetValue(ctrl)?.ToString() ?? "none"; }
        catch { return "err"; }
    }
}
