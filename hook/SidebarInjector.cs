namespace Uprooted;

/// <summary>
/// Timer-based monitor that detects when the settings page is open and injects
/// UPROOTED UI directly into Root's visual tree (Vencord-style).
///
/// Architecture:
/// - Sidebar items injected into NavContainer StackPanel (below the ListBox)
/// - Content pages placed directly into Root's content Panel
/// - Back button in Row=0 is hidden when Uprooted pages are active (prevents freeze)
/// - DetachedFromVisualTree on LayoutContainer cleans up BEFORE recursive detach
///   (prevents the freeze caused by OnDetachedFromVisualTreeCore walking our ScrollViewer wrapper)
/// </summary>
internal class SidebarInjector
{
    private const string InjectedTag = "uprooted-injected";
    private const int PollIntervalMs = 200;  // Safety-net poll; LayoutUpdated handles fast detection

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private UprootedSettings _settings;
    private readonly ThemeEngine _themeEngine;
    private Timer? _timer;
    private readonly object _window;

    // Root's original controls (read + restore references)
    private object? _listBox;
    private object? _navContainer;
    private object? _contentPanel;
    private object? _sidebarGrid;
    private object? _layoutContainer;                    // Main settings Grid (for save bar search)

    // Injection state
    private List<object> _injectedControls = new();   // All controls we injected (may be in NavContainer or items panel)
    private object? _scrollViewerWrapper;               // ScrollViewer wrapping NavContainer (if added)
    private object? _originalVersionBorder;              // Saved reference for restoration
    private object? _originalSignOutControl;             // Saved reference for restoration
    private object? _insertionPanel;                     // Panel where Uprooted container was actually inserted (may differ from NavContainer)
    private int _advancedIndex = -1;                     // "Advanced" position in ListBox (for diagnostics)

    // Content state
    private object? _activeContentPage;                  // Our page currently in content Panel
    private string? _activePage;                         // "uprooted" | "plugins" | "themes" | null
    private int _lastListBoxIdx = -1;                    // For selection change detection
    private bool _injected;                              // Whether we've injected
    private int _aliveCheckCounter;                      // Throttle alive checks
    private List<object> _hiddenContentChildren = new(); // Root's content children we hid (to restore later)

    // Save bar (freeze prevention for Revert button)
    private object? _saveBar;                            // Root's save bar container (collapse when Uprooted pages active)
    private object? _revertButton;                       // Revert button inside save bar (PointerPressed interception)
    private bool _saveBarCollapsed;                      // Whether we've collapsed the save bar (Opacity/MaxHeight, NOT IsVisible)

    // Header state (back arrow hidden + title set when Uprooted pages active)
    private object? _backButton;                         // The back arrow RootSvgButton in header (left side)
    private bool _backButtonWasVisible = true;           // Original visibility to restore
    private object? _headerTitleText;                    // TextBlock in header Grid showing page title
    private string? _headerTitleOriginal;                // Original title text to restore
    private object? _closeCommand;                       // ICommand from back button (for X button behavior)
    private object? _closeCommandParam;                  // CommandParameter for _closeCommand

    // Native font for content pages
    private object? _nativeFontFamily;                   // CircularXX TT font from native controls

    // Version box injection (grey box at bottom of sidebar)
    private object? _versionTextBlock;                   // "Uprooted 0.4.2" TextBlock in version box
    private object? _versionContainer;                   // StackPanel containing version texts

    // Thread safety
    private int _injecting;
    private bool _diagnosticsDone;
    private long _lastLayoutCheckMs;                       // Throttle for LayoutUpdated checks

    public SidebarInjector(AvaloniaReflection resolver, object mainWindow, ThemeEngine themeEngine)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _settings = UprootedSettings.Load();
        _themeEngine = themeEngine;
        _window = mainWindow;

        // Populate default plugin entries if not already set
        if (!_settings.Plugins.ContainsKey("sentry-blocker"))
            _settings.Plugins["sentry-blocker"] = true;
        if (!_settings.Plugins.ContainsKey("themes"))
            _settings.Plugins["themes"] = true;
        if (!_settings.Plugins.ContainsKey("content-filter"))
            _settings.Plugins["content-filter"] = _settings.NsfwFilterEnabled;
    }

    public void StartMonitoring()
    {
        Logger.Log("Injector", "Starting settings page monitor (direct injection mode)");

        // Primary detection: LayoutUpdated fires on the UI thread whenever Avalonia
        // re-layouts. This catches settings page appearance within the same render
        // frame — no dispatch latency, no poll gap.
        _r.SubscribeEvent(_window, "LayoutUpdated", OnLayoutUpdated);

        // Safety-net poll: catches cases where LayoutUpdated alone misses detection,
        // and handles alive checks when injected.
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

    /// <summary>
    /// Fires on the UI thread whenever Avalonia re-layouts. Catches settings page
    /// appearance within the same render frame — zero dispatch latency.
    /// Throttled to avoid running FindSettingsLayout on every layout pass.
    /// </summary>
    private void OnLayoutUpdated()
    {
        if (_injected)
        {
            // Catch Root's async save bar creation the instant it triggers a layout pass.
            // When Root creates the SaveChangesView (~200ms after our ListBox deselection),
            // this fires on the same frame — collapsing it before a single painted frame.
            if (_activePage != null)
            {
                if (_saveBar != null)
                {
                    CollapseSaveBar();
                }
                else if (_layoutContainer != null)
                {
                    var bar = _walker.FindSaveBar(_layoutContainer);
                    if (bar != null)
                    {
                        _saveBar = bar;
                        CollapseSaveBar();
                        _revertButton = _walker.FindRevertButton(bar);
                        if (_revertButton != null)
                        {
                            _r.SubscribeEvent(_revertButton, "Click", () =>
                            {
                                Logger.Log("Injector", "Revert button Click -- cleaning up injection BEFORE teardown");
                                CleanupInjection();
                            });
                        }
                        Logger.Log("Injector", "Save bar caught+collapsed via LayoutUpdated intercept");
                    }
                }
            }
            return;
        }

        // Throttle: at most one check per 32ms (~2 frames at 60fps)
        long now = Environment.TickCount64;
        if (now - _lastLayoutCheckMs < 32) return;
        _lastLayoutCheckMs = now;

        // Shared re-entrancy guard with timer path
        if (Interlocked.CompareExchange(ref _injecting, 1, 0) != 0) return;
        try
        {
            CheckAndInject();
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"LayoutUpdated check error: {ex.Message}");
        }
        finally
        {
            Interlocked.Exchange(ref _injecting, 0);
        }
    }

    private void CheckAndInject()
    {
        if (_injected)
        {
            // Poll ListBox selection every tick for responsiveness
            if (_listBox != null)
            {
                int currentIdx = _r.GetSelectedIndex(_listBox);
                if (currentIdx >= 0 && currentIdx != _lastListBoxIdx)
                {
                    Logger.Log("Injector", $"ListBox selection changed {_lastListBoxIdx} -> {currentIdx}");
                    _lastListBoxIdx = currentIdx;
                    RemoveContentPage();
                    // New Root tab content loads with default colors — walk burst to recolor
                    _themeEngine.ScheduleWalkBurst();
                }
            }

            // Throttled alive check: every 5 ticks (~1 second), verify settings page still open.
            // Uses lightweight text search instead of full FindSettingsLayout to avoid chatty logs.
            _aliveCheckCounter++;
            if (_aliveCheckCounter % 5 == 0)
            {
                var appSettings = _walker.FindFirstTextBlock(_window, "APP SETTINGS")
                    ?? _walker.FindFirstTextBlock(_window, "App Settings");
                if (appSettings == null)
                {
                    Logger.Log("Injector", "Settings page closed (not found in tree), nulling state");
                    NullState();
                }
            }
            return;
        }

        // Not injected -- check if settings page just opened
        var newLayout = _walker.FindSettingsLayout(_window);
        if (newLayout == null) return;

        Logger.Log("Injector", "Settings page detected, injecting (direct injection mode)");

        InjectIntoSettings(newLayout);

        // Run diagnostics AFTER injection so the UI appears immediately.
        // DumpVersionRecon does expensive deep tree walks (10-depth dumps of ListBox items)
        // that would otherwise block the injection on first open.
        if (!_diagnosticsDone)
        {
            _diagnosticsDone = true;
            try { DumpVersionRecon(newLayout); }
            catch (Exception ex) { Logger.Log("Recon", $"DumpVersionRecon error: {ex.Message}"); }
        }
    }

    // ===== Core injection =====

    private void InjectIntoSettings(SettingsLayout layout)
    {
        try
        {
            // Guard: check if we already have injected controls in the tree
            // (protects against re-injection from false detach detection)
            if (_walker.HasTaggedDescendant(layout.NavContainer, InjectedTag))
            {
                Logger.Log("Injector", "Skipping injection: already-injected controls found in NavContainer");
                // Re-acquire references and mark as injected
                _navContainer = layout.NavContainer;
                _listBox = layout.ListBox;
                _contentPanel = layout.ContentArea;
                _sidebarGrid = layout.SidebarGrid;
                _lastListBoxIdx = _listBox != null ? _r.GetSelectedIndex(_listBox) : -1;
                _injected = true;
                return;
            }

            // Store references
            _listBox = layout.ListBox;
            _navContainer = layout.NavContainer;
            _contentPanel = layout.ContentArea;
            _sidebarGrid = layout.SidebarGrid;
            _layoutContainer = layout.LayoutContainer;
            _advancedIndex = layout.AdvancedIndex;
            _originalVersionBorder = layout.VersionBorder;
            _originalSignOutControl = layout.SignOutControl;

            // Step 0.5: Store native font family for content pages
            _nativeFontFamily = _r.GetFontFamily(layout.AppSettingsText);

            // Step 1: Handle save bar (freeze prevention for Revert button)
            // Record visibility NOW, before any tab click triggers ListBox deselection
            // (deselection makes Root think settings changed and show the save bar)
            _saveBar = layout.SaveBar;
            if (_saveBar != null)
            {
                _revertButton = _walker.FindRevertButton(_saveBar);
                if (_revertButton != null)
                {
                    // Use Click instead of PointerPressed -- Avalonia Button class handler
                    // sets Handled=true on PointerPressed, silently suppressing instance handlers.
                    _r.SubscribeEvent(_revertButton, "Click", () =>
                    {
                        Logger.Log("Injector", "Revert button Click -- cleaning up injection BEFORE teardown");
                        CleanupInjection();
                    });
                    Logger.Log("Injector", $"Revert button Click subscribed: {_revertButton.GetType().Name}");
                }
                else
                {
                    Logger.Log("Injector", "Save bar found but Revert button not located (may appear later)");
                }
            }

            // Step 1b: Subscribe DetachedFromVisualTree on LayoutContainer.
            // This fires BEFORE children are recursively detached (Avalonia pre-order),
            // giving us a chance to clean up our tree modifications before the walk
            // reaches our ScrollViewer wrapper (which causes the freeze).
            _r.SubscribeEvent(layout.LayoutContainer, "DetachedFromVisualTree", () =>
            {
                Logger.Log("Injector", "LayoutContainer detaching -- cleaning up before tree teardown");
                // Clear ScrollViewer content to prevent freeze during recursive detach.
                // Without this, the detach walk enters our ScrollViewer → NavContainer →
                // injected controls chain and freezes in OnDetachedFromVisualTreeCore.
                if (_scrollViewerWrapper != null)
                    _r.SetScrollViewerContent(_scrollViewerWrapper, null);
                // Remove our content page from content panel (different subtree branch,
                // hasn't been detached yet at this point)
                if (_activeContentPage != null && _contentPanel != null)
                {
                    try { _r.RemoveChild(_contentPanel, _activeContentPage); }
                    catch { }
                }
                // Restore hidden content children
                foreach (var child in _hiddenContentChildren)
                {
                    try { _r.SetIsVisible(child, true); }
                    catch { }
                }
                NullState();
            });
            Logger.Log("Injector", "DetachedFromVisualTree subscribed on LayoutContainer");

            // Step 1c: Store back button reference if found (may be null -- back button
            // only appears AFTER we deselect the ListBox in OnNavItemClicked).
            // We'll re-search for it after deselection.
            if (layout.BackButton != null)
            {
                try
                {
                    _closeCommand = layout.BackButton.GetType().GetProperty("Command")?.GetValue(layout.BackButton);
                    _closeCommandParam = layout.BackButton.GetType().GetProperty("CommandParameter")?.GetValue(layout.BackButton);
                }
                catch { }
                Logger.Log("Injector", $"Back button found during layout: {layout.BackButton.GetType().Name}, closeCommand={_closeCommand != null}");
            }
            else
            {
                Logger.Log("Injector", "Back button not found during layout (expected -- appears after deselection)");
            }

            // Step 2: Save and remove version Border and sign-out from NavContainer
            if (_navContainer != null)
            {
                if (_originalSignOutControl != null)
                    _r.RemoveChild(_navContainer, _originalSignOutControl);
                if (_originalVersionBorder != null)
                    _r.RemoveChild(_navContainer, _originalVersionBorder);
            }

            // Step 3: Build and insert our controls into NavContainer
            BuildAndInsertNavItems(layout);

            // Step 4: Wrap NavContainer in ScrollViewer for sidebar scrolling
            WrapInScrollViewer();

            // Step 5: Inject version text into the grey version box
            InjectVersionText();

            // Step 6: Record current ListBox selection
            _lastListBoxIdx = _listBox != null ? _r.GetSelectedIndex(_listBox) : -1;

            // Step 6b: Subscribe to ListBox.SelectionChanged for instant Uprooted→Root transitions
            // and immediate theme recoloring when Root loads new tab content.
            if (_listBox != null)
            {
                _r.SubscribeEvent(_listBox, "SelectionChanged", () =>
                {
                    int idx = _r.GetSelectedIndex(_listBox);
                    if (idx >= 0 && _activePage != null)
                    {
                        _lastListBoxIdx = idx;
                        RemoveContentPage();
                    }
                    // Walk burst on every Root tab selection — new content loads with default colors
                    if (idx >= 0)
                        _themeEngine.ScheduleWalkBurst();
                });
            }

            _injected = true;
            Logger.Log("Injector", $"Injection complete. {_injectedControls.Count} controls added, " +
                $"Advanced at index {_advancedIndex}, ListBox idx={_lastListBoxIdx}");

            // Auto-navigate to Uprooted About page on settings open.
            // Delayed: Root's initial ListBox selection (User profile) fires SelectionChanged
            // which would immediately remove our content page. Wait for it to settle first.
            var navTimer = new Timer(_ =>
            {
                _r.RunOnUIThread(() =>
                {
                    if (_injected && _activePage == null)
                        OnNavItemClicked("uprooted");
                });
            }, null, 150, Timeout.Infinite);

            // Walk burst after injection — Root will auto-select a tab, loading content
            // with default theme colors that needs immediate recoloring
            _themeEngine.ScheduleWalkBurst();
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"InjectIntoSettings error: {ex}");
            CleanupInjection();
        }
    }

    private void CleanupInjection()
    {
        if (!_injected) return;
        Logger.Log("Injector", "CleanupInjection: removing all injected controls");

        try
        {
            // Step 1: Unwrap ScrollViewer if we added one
            UnwrapScrollViewer();

            // Step 2: Remove all injected controls from wherever they were inserted
            // (may be an internal items panel rather than NavContainer)
            var removalTarget = _insertionPanel ?? _navContainer;
            if (removalTarget != null)
            {
                foreach (var ctrl in _injectedControls)
                {
                    try { _r.RemoveChild(removalTarget, ctrl); }
                    catch { }
                }
            }

            // Step 3: Re-add original version Border and sign-out to NavContainer
            if (_navContainer != null)
            {
                if (_originalVersionBorder != null)
                    _r.AddChild(_navContainer, _originalVersionBorder);
                if (_originalSignOutControl != null)
                    _r.AddChild(_navContainer, _originalSignOutControl);
            }

            // Step 4: Remove version text from grey version box
            RemoveVersionText();

            // Step 5: Restore back button and save bar
            RestoreBackButton();
            RestoreSaveBar();

            // Step 6: Remove our content from content Panel
            RemoveContentPage();
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"CleanupInjection error: {ex.Message}");
        }

        NullState();
    }

    private void NullState()
    {
        _injectedControls.Clear();
        _scrollViewerWrapper = null;
        _insertionPanel = null;
        _listBox = null;
        _navContainer = null;
        _contentPanel = null;
        _sidebarGrid = null;
        _layoutContainer = null;
        _saveBar = null;
        _revertButton = null;
        _saveBarCollapsed = false;
        _backButton = null;
        _backButtonWasVisible = true;
        _headerTitleText = null;
        _headerTitleOriginal = null;
        _closeCommand = null;
        _closeCommandParam = null;
        _nativeFontFamily = null;
        _originalVersionBorder = null;
        _originalSignOutControl = null;
        _activeContentPage = null;
        _activePage = null;
        _hiddenContentChildren.Clear();
        _versionTextBlock = null;
        _versionContainer = null;
        _lastListBoxIdx = -1;
        _injected = false;
        _aliveCheckCounter = 0;
    }

    // ===== NavContainer item building =====

    private void BuildAndInsertNavItems(SettingsLayout layout)
    {
        if (_navContainer == null) return;

        // Wrap all our items in a single Spacing=0 StackPanel.
        // The NavContainer StackPanel may have non-zero Spacing that adds gaps
        // between its direct children. By wrapping, only one gap applies (before
        // our container), not between each individual nav item.
        var container = _r.CreateStackPanel(vertical: true, spacing: 0);
        if (container == null) return;
        _r.SetTag(container, InjectedTag);
        // Top margin separates UPROOTED section from the sidebar top edge
        // (native sections inside the ListBox get spacing from ListBox padding)
        _r.SetMargin(container, 0, 16, 0, 0);

        // Get styling info from the "APP SETTINGS" header
        var headerFontSize = _r.GetFontSize(layout.AppSettingsText) ?? 11;
        var headerFontWeight = _r.GetFontWeight(layout.AppSettingsText);
        var headerForeground = _r.GetForeground(layout.AppSettingsText);
        var nativeFontFamily = _r.GetFontFamily(layout.AppSettingsText);

        // Get exact nav item foreground + font weight from a native ListBoxItem TextBlock
        object? nativeNavForeground = null;
        object? nativeNavFontWeight = null;
        if (layout.ListBox != null)
        {
            // Find a real nav item TextBlock (e.g. "Account" or "Notifications")
            foreach (var node in _walker.DescendantsDepthFirst(layout.ListBox))
            {
                if (!_r.IsTextBlock(node)) continue;
                var fs = _r.GetFontSize(node);
                if (fs is 14.0) // Nav items use FontSize=14
                {
                    nativeNavForeground = _r.GetForeground(node);
                    nativeNavFontWeight = _r.GetFontWeight(node);
                    Logger.Log("Injector", $"Native nav style: Fg={nativeNavForeground}, FW={nativeNavFontWeight}, Font={_r.GetFontFamily(node)}");
                    break;
                }
            }
        }

        // 1. UPROOTED section header (matching "APP SETTINGS" style)
        var sectionHeader = BuildSectionHeader("UPROOTED", headerFontSize, headerFontWeight, headerForeground, nativeFontFamily);
        if (sectionHeader != null)
            _r.AddChild(container, sectionHeader);

        // 2. Nav items: Uprooted, Plugins, Themes
        foreach (var (label, page) in new[] { ("About", "uprooted"), ("Plugins", "plugins"), ("Themes", "themes") })
        {
            var item = BuildNavItem(label, page, nativeFontFamily, nativeNavForeground, nativeNavFontWeight);
            if (item != null)
                _r.AddChild(container, item);
        }

        // Insert before APP SETTINGS so Uprooted appears between USER SETTINGS and APP SETTINGS.
        // Walk up from the "APP SETTINGS" TextBlock to find the items panel and insertion index.
        var (insertPanel, insertIdx) = FindAppSettingsInsertionPoint(layout.AppSettingsText);
        if (insertPanel != null && insertIdx >= 0)
        {
            _r.InsertChild(insertPanel, insertIdx, container);
            _insertionPanel = insertPanel;
            Logger.Log("Injector", $"Uprooted section inserted before APP SETTINGS at {insertPanel.GetType().Name}[{insertIdx}]");
        }
        else
        {
            // Fallback: append to NavContainer (below everything)
            _r.AddChild(_navContainer, container);
            Logger.Log("Injector", "Uprooted section appended to NavContainer (fallback)");
        }
        _injectedControls.Add(container);

        // 3. Re-add original version Border
        if (_originalVersionBorder != null)
        {
            _r.AddChild(_navContainer, _originalVersionBorder);
            // Don't add to _injectedControls -- it's Root's original control
        }

        // 4. Re-add original sign-out
        if (_originalSignOutControl != null)
        {
            _r.AddChild(_navContainer, _originalSignOutControl);
            // Don't add to _injectedControls -- it's Root's original control
        }
    }

    /// <summary>
    /// Walk up from the "APP SETTINGS" TextBlock toward NavContainer, finding the highest
    /// StackPanel ancestor where the text's ancestor is a Panel child. This locates the
    /// items panel (e.g. VirtualizingStackPanel inside the ListBox) and the index of the
    /// APP SETTINGS section within it, so we can insert our container before it.
    /// Falls back to NavContainer itself if no internal StackPanel is found.
    /// </summary>
    private (object? panel, int index) FindAppSettingsInsertionPoint(object appSettingsText)
    {
        object? bestPanel = null;
        int bestIndex = -1;

        var current = appSettingsText;
        while (current != null)
        {
            var parent = _r.GetParent(current);
            if (parent == null) break;

            var parentType = parent.GetType().Name;
            bool isStackPanel = parentType.Contains("StackPanel");
            bool isNavContainer = parent == _navContainer;

            if (isStackPanel || isNavContainer)
            {
                var children = _r.GetChildren(parent);
                if (children != null)
                {
                    int idx = children.IndexOf(current);
                    if (idx >= 0)
                    {
                        bestPanel = parent;
                        bestIndex = idx;
                    }
                }
            }

            if (isNavContainer) break;
            current = parent;
        }

        if (bestPanel != null)
            Logger.Log("Injector", $"APP SETTINGS insertion point: {bestPanel.GetType().Name}[{bestIndex}] ({_r.GetChildCount(bestPanel)} siblings)");
        else
            Logger.Log("Injector", "Could not find APP SETTINGS insertion point");

        return (bestPanel, bestIndex);
    }

    private object? BuildSectionHeader(string text, double fontSize, object? fontWeight, object? foreground, object? fontFamily)
    {
        // Match Root's native section header structure:
        //   ListBoxItem (H=40) → Panel (M=0,2,0,2) → ... → StackPanel (M=12,12,12,0)
        // We replicate: Panel (H=40, M=0,0,0,4) → StackPanel (M=12,14,12,0)
        //   14 = 12 (native StackPanel margin) + 2 (native Panel margin-top)
        //   Bottom margin 4 separates the section header from the first nav item
        var wrapper = _r.CreatePanel();
        if (wrapper == null) return null;
        _r.SetHeight(wrapper, 40);
        _r.SetMargin(wrapper, 0, 0, 0, -6);
        _r.SetTag(wrapper, InjectedTag);

        var container = _r.CreateStackPanel(vertical: false, spacing: 0);
        if (container == null) return wrapper;
        _r.SetMargin(container, 12, 14, 12, 0);

        var header = _r.CreateTextBlock(text, fontSize);
        if (header != null)
        {
            if (fontWeight != null)
                _r.TextBlockType?.GetProperty("FontWeight")?.SetValue(header, fontWeight);
            if (foreground != null)
                _r.TextBlockType?.GetProperty("Foreground")?.SetValue(header, foreground);
            if (fontFamily != null)
                _r.SetFontFamily(header, fontFamily);
            _r.AddChild(container, header);
        }

        _r.AddChild(wrapper, container);
        return wrapper;
    }

    private object? BuildNavItem(string label, string pageName, object? fontFamily,
        object? nativeForeground = null, object? nativeFontWeight = null)
    {
        // Match Root's native ListBoxItem structure:
        //   ListBoxItem (250x40)
        //     Panel (M=0,2,0,2, 250x36)
        //       ContentPresenter > MenuItemPageContainerView > ContentPresenter >
        //         StackPanel (M=12,8,12,8, 226x20) > TextBlock (FontSize=14, FW=450, VA=Center)
        //       Border (H=36, BG=Transparent, CR=12)  <- hover/selection highlight
        //
        // Our equivalent:
        //   Panel (H=40, tag, cursor, BG=transparent)
        //     Panel (M=0,2,0,2)  <- vertical spacing like native
        //       Border (H=36, CR=12)  <- highlight
        //       TextBlock (M=12,0,12,0, VA=Center, FW=450)  <- content

        var outerPanel = _r.CreatePanel();
        if (outerPanel == null) return null;
        _r.SetTag(outerPanel, $"uprooted-nav-{pageName}");
        _r.SetCursorHand(outerPanel);
        _r.SetHeight(outerPanel, 40); // Match native ListBoxItem height exactly
        _r.SetBackground(outerPanel, "#00000000"); // Transparent BG required for hit-testing (pointer events)

        // Inner panel with vertical spacing matching native ListBoxItem
        var innerPanel = _r.CreatePanel();
        if (innerPanel != null)
        {
            _r.SetMargin(innerPanel, 0, 2, 0, 2);

            // Highlight border (behind content, full width, rounded corners)
            var highlight = _r.CreateBorder(cornerRadius: 12);
            if (highlight != null)
            {
                _r.SetTag(highlight, $"uprooted-highlight-{pageName}");
                highlight.GetType().GetProperty("Height")?.SetValue(highlight, 36.0);
                _r.AddChild(innerPanel, highlight);
            }

            // Text label - matching native font and positioning exactly
            var textBlock = _r.CreateTextBlock(label, 14);
            if (textBlock != null)
            {
                // Apply exact native foreground and font weight (copied from real ListBoxItem)
                if (nativeForeground != null)
                    _r.TextBlockType?.GetProperty("Foreground")?.SetValue(textBlock, nativeForeground);
                else
                    _r.SetForeground(textBlock, "#fff2f2f2");

                if (nativeFontWeight != null)
                    _r.TextBlockType?.GetProperty("FontWeight")?.SetValue(textBlock, nativeFontWeight);
                else
                    _r.SetFontWeightNumeric(textBlock, 450);

                _r.SetMargin(textBlock, 12, 0, 12, 0);
                if (fontFamily != null)
                    _r.SetFontFamily(textBlock, fontFamily);
                _r.SetVerticalAlignment(textBlock, "Center");
                _r.AddChild(innerPanel, textBlock);
            }

            _r.AddChild(outerPanel, innerPanel);

            // Hover events on the outer panel (captures full area)
            _r.SubscribeEvent(outerPanel, "PointerEntered", () =>
            {
                if (_activePage != pageName && highlight != null)
                    _r.SetBackground(highlight, "#0Dffffff");
            });

            _r.SubscribeEvent(outerPanel, "PointerExited", () =>
            {
                if (_activePage != pageName && highlight != null)
                    _r.SetBackground(highlight, (string?)null);
            });
        }

        // Click handler
        _r.SubscribeEvent(outerPanel, "PointerPressed", () =>
        {
            OnNavItemClicked(pageName);
        });

        return outerPanel;
    }

    // ===== Content management =====

    private void OnNavItemClicked(string pageName)
    {
        try
        {
            // Reload settings so page builds reflect runtime changes (e.g. channel switch)
            _settings = UprootedSettings.Load();

            Logger.Log("Injector", $"Nav item clicked: {pageName}");
            if (_activePage == pageName) return;

            // Remove current content (without restoring back button -- we'll keep it hidden)
            if (_activeContentPage != null && _contentPanel != null)
            {
                try { _r.RemoveChild(_contentPanel, _activeContentPage); }
                catch { }
            }
            _activeContentPage = null;

            // Build new page (pass ThemeEngine and rebuild callback for theme switching)
            Action rebuildCurrentPage = () =>
            {
                // Remove current content and rebuild the same page
                if (_activeContentPage != null && _contentPanel != null)
                {
                    try { _r.RemoveChild(_contentPanel, _activeContentPage); }
                    catch { }
                }
                _activeContentPage = null;
                _activePage = null;
                OnNavItemClicked(pageName);

                // Trigger a visual tree walk after theme change
                _themeEngine.ScheduleWalkBurst();
            };
            var page = ContentPages.BuildPage(pageName, _r, _settings, _nativeFontFamily,
                _themeEngine, rebuildCurrentPage, onNavigate: OnNavItemClicked);
            if (page == null)
            {
                Logger.Log("Injector", $"Failed to build page: {pageName}");
                return;
            }

            // Add page to content Panel (hide Root's children instead of removing them)
            if (_contentPanel != null)
            {
                // Hide all existing Root children so we can show our page
                _hiddenContentChildren.Clear();
                var children = _r.GetChildren(_contentPanel);
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        if (child != null && child != _activeContentPage)
                        {
                            _r.SetIsVisible(child, false);
                            _hiddenContentChildren.Add(child);
                        }
                    }
                }
                _r.AddChild(_contentPanel, page);
                _activeContentPage = page;
            }

            // Deselect Root's ListBox items (triggers Root's "no tab" state with back arrow)
            if (_listBox != null)
            {
                _r.SetSelectedIndex(_listBox, -1);
                _lastListBoxIdx = -1;
            }

            // Find and hide the back arrow that appears after deselection.
            // Also set header title to our page name.
            FindAndHideBackButton(pageName);

            // Collapse save bar on Uprooted pages (prevents Revert freeze)
            // Search dynamically since save bar may appear after injection
            FindAndCollapseSaveBar();

            _activePage = pageName;
            UpdateNavHighlights();

            // Immediate synchronous theme walk to prevent flash of unthemed content.
            // The LayoutUpdated interceptor has an 80ms debounce that can skip the walk
            // if a recent walk happened, causing a 1-frame flash of Root's default colors.
            _themeEngine.WalkVisualTreeNow();

            if (_contentPanel != null)
                Logger.Log("Injector", $"Content page '{pageName}' displayed in content Panel");
            else
                Logger.Log("Injector", $"Content page '{pageName}' built but contentPanel is null (stale state)");

            // Schedule delayed save bar search: deselecting Root's ListBox triggers
            // Root's change detection which creates the save bar ASYNCHRONOUSLY.
            // We need to check again after Root has had time to create it.
            ScheduleDelayedSaveBarCollapse();
        }
        catch (Exception ex)
        {
            Logger.LogException("Injector", $"OnNavItemClicked('{pageName}')", ex);
        }
    }

    private void RemoveContentPage()
    {
        bool wasOnUprootedPage = _activePage != null;

        if (_activeContentPage != null && _contentPanel != null)
        {
            try { _r.RemoveChild(_contentPanel, _activeContentPage); }
            catch { }
        }
        _activeContentPage = null;
        _activePage = null;

        // Restore Root's hidden content children
        foreach (var child in _hiddenContentChildren)
        {
            try { _r.SetIsVisible(child, true); }
            catch { }
        }
        _hiddenContentChildren.Clear();

        // Restore back button visibility
        RestoreBackButton();

        // Only restore save bar when transitioning away from an Uprooted page.
        // When switching between Root tabs, don't touch Root's save bar —
        // it may be legitimately visible due to real unsaved settings changes.
        if (wasOnUprootedPage)
            RestoreSaveBar();

        UpdateNavHighlights();
    }

    private void ClearPanelChildren(object panel)
    {
        var children = _r.GetChildren(panel);
        if (children == null) return;

        // Remove children in reverse to avoid index shifting
        for (int i = children.Count - 1; i >= 0; i--)
        {
            try { children.RemoveAt(i); }
            catch { }
        }
    }

    // ===== ScrollViewer wrapping =====

    private void WrapInScrollViewer()
    {
        if (_navContainer == null || _sidebarGrid == null) return;

        try
        {
            // Create ScrollViewer to wrap the NavContainer StackPanel
            var scrollViewer = _r.CreateScrollViewer();
            if (scrollViewer == null) return;

            // Copy Grid row from NavContainer
            int navRow = _r.GetGridRow(_navContainer);

            // Remove NavContainer from its parent Grid
            _r.RemoveChild(_sidebarGrid, _navContainer);

            // Set NavContainer as ScrollViewer content
            _r.SetScrollViewerContent(scrollViewer, _navContainer);

            // Set Grid.Row on ScrollViewer
            _r.SetGridRow(scrollViewer, navRow);

            // Add ScrollViewer to parent Grid
            _r.AddChild(_sidebarGrid, scrollViewer);

            // Hide scrollbar for clean look
            scrollViewer.GetType().GetProperty("VerticalScrollBarVisibility")?.SetValue(
                scrollViewer,
                Enum.Parse(scrollViewer.GetType().Assembly.GetType("Avalonia.Controls.Primitives.ScrollBarVisibility")
                    ?? typeof(int), "Auto"));

            _scrollViewerWrapper = scrollViewer;
            Logger.Log("Injector", "NavContainer wrapped in ScrollViewer");
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"WrapInScrollViewer error: {ex.Message}");
            // If wrapping fails, try to restore NavContainer to parent
            if (_sidebarGrid != null && _navContainer != null)
            {
                try { _r.AddChild(_sidebarGrid, _navContainer); }
                catch { }
            }
        }
    }

    private void UnwrapScrollViewer()
    {
        if (_scrollViewerWrapper == null || _sidebarGrid == null || _navContainer == null) return;

        try
        {
            int navRow = _r.GetGridRow(_scrollViewerWrapper);

            // Remove ScrollViewer from Grid
            _r.RemoveChild(_sidebarGrid, _scrollViewerWrapper);

            // Clear ScrollViewer content
            _r.SetScrollViewerContent(_scrollViewerWrapper, null);

            // Restore Grid.Row and add NavContainer back
            _r.SetGridRow(_navContainer, navRow);
            _r.AddChild(_sidebarGrid, _navContainer);

            _scrollViewerWrapper = null;
            Logger.Log("Injector", "ScrollViewer unwrapped, NavContainer restored");
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"UnwrapScrollViewer error: {ex.Message}");
        }
    }

    // ===== Version box injection =====

    /// <summary>
    /// Inject "Uprooted 0.4.2" into the grey version info box at the bottom of the sidebar.
    /// The box lives in SidebarGrid Row=1 and contains "Root Version: X.Y.Z" and "System Info: ...".
    /// </summary>
    private void InjectVersionText()
    {
        if (_sidebarGrid == null) return;

        try
        {
            // Find the StackPanel containing "Root Version:" text inside SidebarGrid Row=1
            object? versionStackPanel = null;
            foreach (var child in _r.GetVisualChildren(_sidebarGrid))
            {
                if (_r.GetGridRow(child) != 1) continue;

                // Search this subtree for the TextBlock containing "Root Version"
                foreach (var node in _walker.DescendantsDepthFirst(child))
                {
                    if (!_r.IsTextBlock(node)) continue;
                    var txt = _r.GetText(node);
                    if (txt != null && txt.StartsWith("Root Version", StringComparison.OrdinalIgnoreCase))
                    {
                        versionStackPanel = _r.GetParent(node);
                        break;
                    }
                }
                break;
            }

            if (versionStackPanel == null)
            {
                Logger.Log("Injector", "Version box: could not find 'Root Version' text container");
                return;
            }

            // Create "Uprooted 0.4.2" TextBlock matching existing style (FontSize=10, Fg=#66f2f2f2)
            var versionText = _r.CreateTextBlock($"Uprooted {_settings.Version}", 10, "#66f2f2f2");
            if (versionText == null) return;

            _r.AddChild(versionStackPanel, versionText);
            _versionTextBlock = versionText;
            _versionContainer = versionStackPanel;

            // Intercept version copy button to include Uprooted version in clipboard
            var versionButton = FindAncestorOfType(versionStackPanel, "Button");
            if (versionButton != null)
            {
                _r.SubscribeEvent(versionButton, "Click", () =>
                {
                    try
                    {
                        var lines = new List<string>();
                        foreach (var child in _r.GetVisualChildren(versionStackPanel))
                        {
                            if (_r.IsTextBlock(child))
                            {
                                var txt = _r.GetText(child);
                                if (!string.IsNullOrEmpty(txt)) lines.Add(txt);
                            }
                        }
                        if (lines.Count > 0)
                        {
                            _r.CopyToClipboard(_window, string.Join("\n", lines));
                            Logger.Log("Injector", $"Version copy: overwrote clipboard with {lines.Count} lines");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Injector", $"Version copy error: {ex.Message}");
                    }
                });
                Logger.Log("Injector", "Version copy: subscribed to Button.Click");
            }

            Logger.Log("Injector", $"Version box: injected 'Uprooted {_settings.Version}' into version info");
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"InjectVersionText error: {ex.Message}");
        }
    }

    private void RemoveVersionText()
    {
        if (_versionTextBlock != null && _versionContainer != null)
        {
            try { _r.RemoveChild(_versionContainer, _versionTextBlock); }
            catch { }
        }
        _versionTextBlock = null;
        _versionContainer = null;
    }

    // ===== Back button management =====

    /// <summary>
    /// After deselecting Root's ListBox, a back arrow appears in Row=0 of the header.
    /// The header Grid (content side, ~900px wide) contains two RootSvgButtons:
    ///   - Left side (@~24px): back arrow — HIDE this
    ///   - Right side (@~836px): X close button — KEEP this
    /// Also sets the title TextBlock to our page display name.
    /// Called on every nav click (including switching between our tabs).
    /// </summary>
    private void FindAndHideBackButton(string pageName)
    {
        if (_layoutContainer == null) return;

        // Map internal page name to display title
        var displayName = pageName == "uprooted" ? "About"
            : char.ToUpper(pageName[0]) + pageName[1..];

        // If we already found the title TextBlock, just update the text
        if (_headerTitleText != null)
        {
            try { _headerTitleText.GetType().GetProperty("Text")?.SetValue(_headerTitleText, displayName); }
            catch { }
            Logger.Log("Injector", $"Title updated: \"{displayName}\"");
        }

        // Already found and hidden back button + title? Nothing more to search.
        if (_backButton != null && _headerTitleText != null) return;

        // Find the content header Grid in Row=0.
        // Multiple children sit at Row=0; the header is a Grid (not Panel/Border/Rectangle).
        object? headerGrid = null;
        foreach (var child in _r.GetVisualChildren(_layoutContainer))
        {
            if (_r.GetGridRow(child) != 0) continue;
            if (!_r.IsGrid(child)) continue;
            headerGrid = child;
            break;
        }

        if (headerGrid == null)
        {
            Logger.Log("Injector", "Content header Grid not found in Row=0");
            return;
        }

        var gridBounds = _r.GetBounds(headerGrid);
        double gridWidth = gridBounds?.W ?? 900;

        // Walk header Grid descendants to find back arrow and title
        foreach (var node in _walker.DescendantsDepthFirst(headerGrid))
        {
            var typeName = node.GetType().Name;

            // Identify buttons by position: left side = back arrow, right side = X close
            if (typeName.Contains("Button") && _backButton == null)
            {
                var b = _r.GetBounds(node);
                if (b != null && b.Value.X < gridWidth / 2)
                {
                    _backButton = node;
                    _backButtonWasVisible = _r.GetIsVisible(node);
                    _r.SetIsVisible(node, false);
                    Logger.Log("Injector", $"Back arrow hidden: {typeName} @{b.Value.X:F0},{b.Value.Y:F0}");

                    // Extract close command if we didn't get it during layout
                    if (_closeCommand == null)
                    {
                        try
                        {
                            _closeCommand = node.GetType().GetProperty("Command")?.GetValue(node);
                            _closeCommandParam = node.GetType().GetProperty("CommandParameter")?.GetValue(node);
                        }
                        catch { }
                    }
                }
            }

            // Find the title TextBlock (direct child of header Grid)
            if (_r.IsTextBlock(node) && _headerTitleText == null)
            {
                var parent = _r.GetParent(node);
                if (parent == headerGrid)
                {
                    _headerTitleText = node;
                    _headerTitleOriginal = _r.GetText(node);
                    try { node.GetType().GetProperty("Text")?.SetValue(node, displayName); }
                    catch { }
                    Logger.Log("Injector", $"Title set: \"{_headerTitleOriginal}\" -> \"{displayName}\"");
                }
            }
        }

        if (_backButton == null)
            Logger.Log("Injector", "Back arrow not found in header Grid (no left-side button)");
    }

    private void RestoreBackButton()
    {
        // Don't restore back arrow visibility — Root manages its own header state
        // when a tab is selected. Our local IsVisible=false will be discarded when
        // Root tears down and recreates the settings page.
        _backButton = null;

        // Don't restore title text — Root's ViewModel binding will overwrite it
        // when its own tab is selected. Setting it to "" first causes a visible flash.
        _headerTitleText = null;
        _headerTitleOriginal = null;
    }

    // ===== Helpers =====

    private object? FindAncestorOfType(object node, string typeName)
    {
        var current = _r.GetParent(node);
        for (int d = 0; d < 10 && current != null; d++)
        {
            if (current.GetType().Name == typeName || current.GetType().Name.Contains(typeName))
                return current;
            current = _r.GetParent(current);
        }
        return null;
    }

    /// <summary>
    /// Search for Root's save bar and collapse it if found. Also subscribes Revert button handler.
    /// </summary>
    private void FindAndCollapseSaveBar()
    {
        if (_saveBar == null && _layoutContainer != null)
        {
            _saveBar = _walker.FindSaveBar(_layoutContainer);
            if (_saveBar != null)
            {
                _revertButton = _walker.FindRevertButton(_saveBar);
                if (_revertButton != null)
                {
                    _r.SubscribeEvent(_revertButton, "Click", () =>
                    {
                        Logger.Log("Injector", "Revert button Click -- cleaning up injection BEFORE teardown (late)");
                        CleanupInjection();
                    });
                    Logger.Log("Injector", $"Revert button Click subscribed (late): {_revertButton.GetType().Name}");
                }
            }
        }
        CollapseSaveBar();
    }

    /// <summary>
    /// Collapse save bar visually without touching IsVisible. Uses Opacity/MaxHeight/IsHitTestVisible
    /// so Root's IsVisible binding remains untouched and can reassert normally on Root tabs.
    /// </summary>
    private void CollapseSaveBar()
    {
        if (_saveBar == null || _saveBarCollapsed) return;
        try
        {
            _saveBar.GetType().GetProperty("Opacity")?.SetValue(_saveBar, 0.0);
            _saveBar.GetType().GetProperty("IsHitTestVisible")?.SetValue(_saveBar, false);
            _saveBar.GetType().GetProperty("MaxHeight")?.SetValue(_saveBar, 0.0);
            _saveBarCollapsed = true;
        }
        catch (Exception ex) { Logger.Log("Injector", $"CollapseSaveBar error: {ex.Message}"); }
    }

    /// <summary>
    /// Restore save bar to normal state. Reverses CollapseSaveBar by resetting
    /// Opacity/MaxHeight/IsHitTestVisible to defaults. IsVisible is never touched.
    /// </summary>
    private void RestoreSaveBar()
    {
        if (_saveBar == null || !_saveBarCollapsed) return;
        try
        {
            _saveBar.GetType().GetProperty("Opacity")?.SetValue(_saveBar, 1.0);
            _saveBar.GetType().GetProperty("IsHitTestVisible")?.SetValue(_saveBar, true);
            _saveBar.GetType().GetProperty("MaxHeight")?.SetValue(_saveBar, double.PositiveInfinity);
            _saveBarCollapsed = false;
        }
        catch (Exception ex) { Logger.Log("Injector", $"RestoreSaveBar error: {ex.Message}"); }
    }

    /// <summary>
    /// Schedule delayed save bar search+collapse. Root creates the save bar ASYNCHRONOUSLY after
    /// we deselect its ListBox (which triggers its change detection). We need to poll briefly
    /// to catch it after Root has had time to create it.
    /// </summary>
    private void ScheduleDelayedSaveBarCollapse()
    {
        System.Threading.ThreadPool.QueueUserWorkItem(_ =>
        {
            int elapsed = 0;
            foreach (var checkAt in new[] { 200, 500, 1000 })
            {
                int sleepMs = checkAt - elapsed;
                Thread.Sleep(sleepMs);
                elapsed = checkAt;

                if (_activePage == null) return;

                _r.RunOnUIThread(() =>
                {
                    try
                    {
                        if (_activePage == null) return;
                        if (_saveBar != null)
                            CollapseSaveBar();
                        else
                        {
                            FindAndCollapseSaveBar();
                            if (_saveBar != null)
                                Logger.Log("Injector", $"Save bar found+collapsed via delayed search ({checkAt}ms)");
                        }
                    }
                    catch { }
                });
            }
        });
    }

    private void UpdateNavHighlights()
    {
        if (_navContainer == null) return;

        foreach (var node in _walker.DescendantsDepthFirst(_navContainer))
        {
            var tag = _r.GetTag(node);
            if (tag == null || !tag.StartsWith("uprooted-highlight-")) continue;

            var itemPage = tag["uprooted-highlight-".Length..];
            _r.SetBackground(node, itemPage == _activePage ? "#19ffffff" : null);
        }
    }

    // ===== Diagnostics =====

    /// <summary>
    /// Recon: dump ListBoxItem styling and section header details for pixel-perfect matching.
    /// </summary>
    private void DumpVersionRecon(SettingsLayout layout)
    {
        Logger.Log("Recon", "=== STYLE RECON ===");

        // 1. APP SETTINGS header: exact font, margin, padding, parent chain
        Logger.Log("Recon", "--- Section header: APP SETTINGS ---");
        var hdr = layout.AppSettingsText;
        Logger.Log("Recon", $"Text: \"{_r.GetText(hdr)}\"");
        Logger.Log("Recon", $"  Type: {hdr.GetType().Name}");
        Logger.Log("Recon", $"  FontSize: {_r.GetFontSize(hdr)}");
        Logger.Log("Recon", $"  FontWeight: {_r.GetFontWeight(hdr)}");
        Logger.Log("Recon", $"  Foreground: {_r.GetForeground(hdr)}");
        Logger.Log("Recon", $"  Margin: {GetPropStr(hdr, "Margin")}");
        Logger.Log("Recon", $"  Bounds: {BoundsStr(_r.GetBounds(hdr))}");
        // Walk up 4 parents to see containers/margins
        var p = _r.GetParent(hdr);
        for (int d = 0; d < 6 && p != null; d++)
        {
            var pb = _r.GetBounds(p);
            Logger.Log("Recon", $"  Parent[{d}]: {p.GetType().Name} M={GetPropStr(p, "Margin")} P={GetPropStr(p, "Padding")} Bounds={BoundsStr(pb)}");
            p = _r.GetParent(p);
        }

        // 1b. NavContainer StackPanel properties
        if (layout.NavContainer != null)
        {
            var spacingVal = layout.NavContainer.GetType().GetProperty("Spacing")?.GetValue(layout.NavContainer);
            Logger.Log("Recon", $"NavContainer Spacing: {spacingVal}");
        }

        // 2. First 3 ListBoxItems: full visual tree for style matching
        Logger.Log("Recon", "");
        Logger.Log("Recon", "--- ListBox items (first 3 + selected) ---");
        if (layout.ListBox != null)
        {
            var lb = layout.ListBox;
            Logger.Log("Recon", $"ListBox: {lb.GetType().Name} Bounds={BoundsStr(_r.GetBounds(lb))}");
            Logger.Log("Recon", $"  Margin: {GetPropStr(lb, "Margin")}");
            Logger.Log("Recon", $"  Padding: {GetPropStr(lb, "Padding")}");
            Logger.Log("Recon", $"  SelectedIndex: {_r.GetSelectedIndex(lb)}");

            int selectedIdx = _r.GetSelectedIndex(lb);
            int itemIdx = 0;
            foreach (var node in _r.GetVisualChildren(lb))
            {
                foreach (var item in _walker.DescendantsDepthFirst(node))
                {
                    if (item.GetType().Name != "ListBoxItem") continue;
                    bool shouldDump = itemIdx < 3 || itemIdx == selectedIdx;
                    if (shouldDump)
                    {
                        var ib = _r.GetBounds(item);
                        var text = FindFirstTextInTree(item);
                        string sel = itemIdx == selectedIdx ? " [SELECTED]" : "";
                        Logger.Log("Recon", $"");
                        Logger.Log("Recon", $"  ListBoxItem[{itemIdx}] \"{text}\"{sel}");
                        Logger.Log("Recon", $"    Bounds: {BoundsStr(ib)}");
                        Logger.Log("Recon", $"    Margin: {GetPropStr(item, "Margin")}");
                        Logger.Log("Recon", $"    Padding: {GetPropStr(item, "Padding")}");
                        Logger.Log("Recon", $"    MinHeight: {GetPropStr(item, "MinHeight")}");
                        Logger.Log("Recon", $"    Height: {GetPropStr(item, "Height")}");
                        // Deep dump visual tree with all style props (depth 10 to reach TextBlock)
                        DumpTreeDetailed(item, 2, 10);
                    }
                    itemIdx++;
                }
            }
            Logger.Log("Recon", $"  Total items: {itemIdx}");
        }

        // 3. ListBox parent chain for understanding x-offset
        Logger.Log("Recon", "");
        Logger.Log("Recon", "--- ListBox parent chain (for x-offset context) ---");
        if (layout.ListBox != null)
        {
            var lbp = _r.GetParent(layout.ListBox);
            for (int d = 0; d < 6 && lbp != null; d++)
            {
                var lbb = _r.GetBounds(lbp);
                Logger.Log("Recon", $"  Parent[{d}]: {lbp.GetType().Name} M={GetPropStr(lbp, "Margin")} P={GetPropStr(lbp, "Padding")} Bounds={BoundsStr(lbb)}");
                lbp = _r.GetParent(lbp);
            }
        }

        Logger.Log("Recon", "=== END STYLE RECON ===");
    }

    private string BoundsStr((double X, double Y, double W, double H)? b)
    {
        if (b == null) return "null";
        return $"({b.Value.W:F1}x{b.Value.H:F1} @{b.Value.X:F1},{b.Value.Y:F1})";
    }

    private string? FindFirstTextInTree(object root)
    {
        if (_r.IsTextBlock(root))
            return _r.GetText(root);
        foreach (var child in _r.GetVisualChildren(root))
        {
            var text = FindFirstTextInTree(child);
            if (text != null) return text;
        }
        return null;
    }

    private object? FindSidebarBorder(object navContainer)
    {
        var current = _r.GetParent(navContainer);
        int depth = 0;
        while (current != null && depth < 5)
        {
            if (_r.IsBorder(current))
                return current;
            current = _r.GetParent(current);
            depth++;
        }
        return null;
    }

    private string GetPropStr(object? ctrl, string propName)
    {
        if (ctrl == null) return "null";
        try { return ctrl.GetType().GetProperty(propName)?.GetValue(ctrl)?.ToString() ?? "null"; }
        catch { return "err"; }
    }

    /// <summary>Detailed tree dump with ALL style properties for pixel-perfect matching.</summary>
    private void DumpTreeDetailed(object node, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        var indent = new string(' ', depth * 2);
        var typeName = node.GetType().Name;
        var b = _r.GetBounds(node);
        var props = new List<string> { BoundsStr(b) };

        // Margin/Padding
        var m = GetPropStr(node, "Margin");
        var p = GetPropStr(node, "Padding");
        if (m != "0,0,0,0" && m != "0" && m != "null") props.Add($"M={m}");
        if (p != "0,0,0,0" && p != "0" && p != "null") props.Add($"P={p}");

        // Size
        var w = GetPropStr(node, "Width");
        var h = GetPropStr(node, "Height");
        var minH = GetPropStr(node, "MinHeight");
        if (w != "NaN" && w != "null") props.Add($"W={w}");
        if (h != "NaN" && h != "null") props.Add($"H={h}");
        if (minH != "0" && minH != "null" && minH != "NaN") props.Add($"MinH={minH}");

        // Background
        try { var bg = node.GetType().GetProperty("Background")?.GetValue(node); if (bg != null) props.Add($"BG={bg}"); } catch { }

        // Border-specific
        if (_r.IsBorder(node))
        {
            try
            {
                var cr = GetPropStr(node, "CornerRadius");
                var bt = GetPropStr(node, "BorderThickness");
                if (cr != "0" && cr != "0,0,0,0" && cr != "null") props.Add($"CR={cr}");
                if (bt != "0" && bt != "0,0,0,0" && bt != "null") props.Add($"BT={bt}");
            }
            catch { }
        }

        // TextBlock-specific
        if (_r.IsTextBlock(node))
        {
            props.Add($"Text=\"{_r.GetText(node)}\"");
            props.Add($"FontSize={_r.GetFontSize(node)}");
            props.Add($"FontWeight={_r.GetFontWeight(node)}");
            props.Add($"Fg={_r.GetForeground(node)}");
            var ff = GetPropStr(node, "FontFamily");
            if (ff != "null") props.Add($"FontFamily={ff}");
            var lh = GetPropStr(node, "LineHeight");
            if (lh != "NaN" && lh != "null" && lh != "0") props.Add($"LineHeight={lh}");
        }

        // HorizontalAlignment/VerticalAlignment
        var ha = GetPropStr(node, "HorizontalAlignment");
        var va = GetPropStr(node, "VerticalAlignment");
        if (ha != "Stretch" && ha != "null") props.Add($"HA={ha}");
        if (va != "Stretch" && va != "null") props.Add($"VA={va}");

        Logger.Log("Recon", $"    {indent}{typeName} {string.Join(", ", props)}");

        foreach (var child in _r.GetVisualChildren(node))
            DumpTreeDetailed(child, depth + 1, maxDepth);
    }
}
