namespace Uprooted;

/// <summary>
/// Timer-based monitor that detects when the settings page is open and injects
/// UPROOTED UI directly into Root's visual tree (Vencord-style).
///
/// Architecture:
/// - Sidebar items injected into NavContainer StackPanel (below the ListBox)
/// - Content pages placed directly into Root's content Panel
/// - Back button collapsed (Opacity/MaxWidth=0, not IsVisible) when Uprooted pages are active
///   (avoids fighting Root's IsVisible binding on SelectedMenuItemPageContainer.Navigator.CanGoBack)
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
    private List<object> _hiddenContentChildren = new(); // Root's content children we hid (to restore later)

    // Save bar (freeze prevention for Revert button)
    private object? _saveBar;                            // Root's save bar container (collapse when Uprooted pages active)
    private object? _revertButton;                       // Revert button inside save bar (PointerPressed interception)
    private bool _saveBarCollapsed;                      // Whether we've collapsed the save bar (Opacity/MaxHeight, NOT IsVisible)

    // Header state (back arrow hidden + title set when Uprooted pages active)
    private object? _backButton;                         // The back arrow RootSvgButton in header (left side)
    private bool _backButtonWasVisible = true;           // Original visibility to restore
    private bool _backButtonCollapsed;                   // Whether we've collapsed the back button (Opacity/MaxWidth, NOT IsVisible)
    private object? _headerTitleText;                    // TextBlock in header Grid showing page title
    private string? _headerTitleOriginal;                // Original title text to restore
    private object? _closeCommand;                       // ICommand from back button (for X button behavior)
    private object? _closeCommandParam;                  // CommandParameter for _closeCommand

    // Native font for content pages
    private object? _nativeFontFamily;                   // CircularXX TT font from native controls

    // Version box injection (grey box at bottom of sidebar)
    private object? _versionTextBlock;                   // "Uprooted 0.5.0-rc" TextBlock in version box
    private object? _versionContainer;                   // StackPanel containing version texts
    private object? _versionButton;                      // Button wrapping version box (Command cleared for intercept)
    private object? _versionButtonOriginalCommand;       // Saved Command for restore on cleanup
    private Delegate? _versionClickHandler;              // Our Click handler delegate

    // Thread safety
    private int _injecting;
    private bool _diagnosticsDone;
    private long _lastLayoutCheckMs;                       // Throttle for LayoutUpdated checks
    private bool _hasAutoNavigated;                        // Only auto-nav to About once per settings open
    private long _selectionSuppressedUntilMs;               // Suppress Root ListBox auto-select until this tick
    private long _lastMenuRetargetMs;                      // Throttle native menu retarget while settings open
    private long _lastMenuForcedRebindMs;                  // Periodic forced rebind for nodes that keep recreating bindings
    private long _lastMenuRetargetLogMs;                   // Throttle noisy retarget logs

    // Wide event tail sampler for poll tick (150 ticks × 200ms = 30s heartbeat when fast poll,
    // or 150 ticks × 1000ms = 150s when slow poll after injection)
    private readonly TailSampler _sampler = new(heartbeatTicks: 150, slowThresholdMs: 50);

    public SidebarInjector(AvaloniaReflection resolver, object mainWindow, ThemeEngine themeEngine)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _settings = UprootedSettings.Load();
        _themeEngine = themeEngine;
        _window = mainWindow;

        // Re-inject sidebar when Root's theme variant changes (Dark↔Light↔PureDark)
        // so nav items and content pages get fresh colors from the new variant.
        _themeEngine.SetVariantChangedCallback(() =>
        {
            _r.RunOnUIThread(() =>
            {
                try
                {
                    // Reset saved theme to default — the user chose a Root theme,
                    // so don't let custom theme TextChanged re-apply it on rebuild.
                    if (_settings.ActiveTheme != "default-dark")
                    {
                        _settings.ActiveTheme = "default-dark";
                        try { _settings.Save(); }
                        catch (Exception sx) { Logger.Log("Injector", "Settings save on variant change: " + sx.Message); }
                    }

                    if (_injected)
                    {
                        // Root updates DynamicResource-bound controls on variant change,
                        // but our injected controls have hardcoded foreground/background.
                        // Remove our controls from the tree, null state, let next
                        // LayoutUpdated re-inject with fresh native colors.
                        Logger.Log("Injector", "Root variant changed — removing injected controls for re-injection");

                        // Unwrap ScrollViewer first (restores NavContainer to grid)
                        UnwrapScrollViewer();

                        // Remove our injected container from the nav panel
                        var removalTarget = _insertionPanel ?? _navContainer;
                        if (removalTarget != null)
                        {
                            foreach (var ctrl in _injectedControls)
                            {
                                try { _r.RemoveChild(removalTarget, ctrl); }
                                catch { }
                            }
                        }

                        // Remove content page
                        RemoveContentPage();

                        // Remove version text we injected (prevents duplicates on re-inject)
                        RemoveVersionText();

                        // Clear all state — don't try to restore version/signout
                        // (Root's DynamicResource bindings handle its own controls)
                        NullState();
                    }
                }
                catch (Exception ex) { Logger.Log("Injector", "Variant re-inject error: " + ex.Message); }
            });
        });

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
                using var ev = WideEvent.BeginSampled("Injector", "poll_tick", _sampler);
                ev.Set("injected", _injected);
                try { CheckAndInject(ev); }
                catch (Exception ex)
                {
                    ev.SetError(ex);
                }
                finally { Interlocked.Exchange(ref _injecting, 0); }
            });
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"OnTimerTick dispatch error: {ex.Message}");
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

        // Throttle: 50ms when not injected — prevents running FindSettingsLayout on every
        // single layout pass during animations. FindFirstTextBlockFast with 1500-node cap
        // takes ~150μs per call; at 50ms throttle that's ~20 calls/s = 3ms/s — negligible.
        // (Was 500ms, but that caused a visible flash of Root's default settings page on
        // fast open/close cycles: NullState resets the throttle, a LayoutUpdated fires during
        // the close animation burning the free check, then the 500ms window blocks detection.)
        long now = Environment.TickCount64;
        if (now - _lastLayoutCheckMs < 50) return;
        _lastLayoutCheckMs = now;

        // No _injecting guard here: both LayoutUpdated and the timer's RunOnUIThread callback
        // execute on the UI thread — they cannot overlap. The timer claims _injecting on its
        // threadpool thread BEFORE posting to UI, which would block this fast-path for ~200ms
        // until the timer callback runs and releases the lock. Skipping the guard lets
        // LayoutUpdated inject on the same render frame; the timer callback will then see
        // _injected=true and return harmlessly.
        try
        {
            CheckAndInject(null);
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"LayoutUpdated check error: {ex.Message}");
        }
    }

    private void CheckAndInject(WideEvent? ev)
    {
        if (_injected)
        {
            ev?.Set("phase", "alive_check");

            // Poll ListBox selection every tick for responsiveness
            if (_listBox != null)
            {
                int currentIdx = _r.GetSelectedIndex(_listBox);
                if (currentIdx >= 0 && currentIdx != _lastListBoxIdx)
                {
                    // Suppress during post-injection window (Root's initial auto-select)
                    if (Environment.TickCount64 < _selectionSuppressedUntilMs)
                    {
                        _r.SetSelectedIndex(_listBox, -1);
                        // Re-deselection fires the title binding to null — restore header state
                        if (_activePage != null)
                        {
                            CollapseBackButton();
                            SetHeaderTitle(_activePage);
                        }
                        ev?.Set("selection", "suppressed");
                    }
                    else
                    {
                        ev?.Set("selection_change", $"{_lastListBoxIdx}->{currentIdx}");
                        ev?.MarkNotable();
                        _lastListBoxIdx = currentIdx;
                        RemoveContentPage();
                        // New Root tab content loads with default colors — walk burst to recolor
                        _themeEngine.ScheduleWalkBurst();
                    }
                }
            }

            // Back button retry: find structurally if not found during injection
            if (_activePage != null && _backButton == null)
            {
                FindHeaderControls();
                if (_backButton != null)
                {
                    CollapseBackButton();
                    SetHeaderTitle(_activePage);
                    ev?.Set("back_button", "found_late");
                }
            }

            // Alive check every tick (~1s, since timer slows to 1000ms after injection).
            // Primary cleanup is DetachedFromVisualTree; this catches leaked state.
            // Uses lightweight text search instead of full FindSettingsLayout.
            var appSettings = _walker.FindFirstTextBlockFast(_window, "APP SETTINGS")
                ?? _walker.FindFirstTextBlockFast(_window, "App Settings");
            if (appSettings == null)
            {
                ev?.Set("result", "settings_closed");
                ev?.MarkNotable();
                _hasAutoNavigated = false;  // Reset so next settings open auto-navs again
                NullState();
            }

            // Native settings tab text can be recreated/rebound over time.
            // Re-apply dyn-fg tags periodically so custom-theme text edits stay live.
            var nowMs = Environment.TickCount64;
            if (_navContainer != null && nowMs - _lastMenuRetargetMs >= 300)
            {
                _lastMenuRetargetMs = nowMs;
                RetargetNativeMenuText(_navContainer, _listBox, _sidebarGrid);
            }
            return;
        }

        ev?.Set("phase", "detect");

        // Not injected -- check if settings page just opened
        var newLayout = _walker.FindSettingsLayout(_window);
        if (newLayout == null)
        {
            ev?.Set("result", "no_settings");
            return;
        }

        ev?.Set("result", "settings_detected");
        ev?.MarkNotable();

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
        using var ev = WideEvent.Begin("Injector", "inject");
        try
        {
            // Guard: check if we already have injected controls in the tree
            // (protects against re-injection from false detach detection)
            if (_walker.HasTaggedDescendant(layout.NavContainer, InjectedTag))
            {
                ev.Set("result", "already_injected");
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
                    ev.Set("revert_button", _revertButton.GetType().Name);
                }
                else
                {
                    ev.Set("revert_button", "not_found");
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
                _hasAutoNavigated = false;  // Reset so next settings open auto-navs again
                NullState();
            });
            ev.Set("detach_subscribed", true);

            // Step 1c: Find back button and title TextBlock in header Grid structurally.
            // This works immediately (no bounds needed) even before layout has run.
            // The back button is always in the tree; its IsVisible is binding-controlled.
            FindHeaderControls();

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

            // Step 3b: Keep Root native nav items untouched (no border injection).

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
                    // Suppress Root's initial auto-select during the post-injection window.
                    // Our OnNavItemClicked sets idx=-1; Root tries to re-select a real tab.
                    // Only suppress positive selections (idx >= 0), not our own deselections.
                    if (idx >= 0 && Environment.TickCount64 < _selectionSuppressedUntilMs)
                    {
                        _r.SetSelectedIndex(_listBox, -1);
                        // Re-deselection fires the title binding to null — restore header state
                        if (_activePage != null)
                        {
                            CollapseBackButton();
                            SetHeaderTitle(_activePage);
                        }
                        return;
                    }
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
            // Slow the safety-net poll to 1s — events (SelectionChanged, DetachedFromVisualTree)
            // handle fast transitions; the poll only needs to catch leaked state and run alive checks.
            _timer?.Change(1000, 1000);
            ev.Set("controls_added", _injectedControls.Count);
            ev.Set("advanced_index", _advancedIndex);
            ev.Set("listbox_idx", _lastListBoxIdx);
            ev.Set("result", "injected");

            // Auto-navigate to Uprooted About page on FIRST settings open only.
            // Skip on re-injection after variant change (user was already on a tab).
            // Suppression window swallows Root's initial ListBox auto-select (both event
            // handler and poll) so we can navigate immediately without a timer delay.
            if (!_hasAutoNavigated)
            {
                _hasAutoNavigated = true;
                _selectionSuppressedUntilMs = Environment.TickCount64 + 500;
                ev.Set("auto_nav", "uprooted");
                OnNavItemClicked("uprooted");
            }

            // Walk burst after injection — Root will auto-select a tab, loading content
            // with default theme colors that needs immediate recoloring
            _themeEngine.ScheduleWalkBurst();
        }
        catch (Exception ex)
        {
            ev.SetError(ex);
            CleanupInjection();
        }
    }

    private void CleanupInjection()
    {
        if (!_injected) return;
        using var ev = WideEvent.Begin("Injector", "cleanup");

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
            ev.SetError(ex);
        }

        ev.Set("result", "cleaned");
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
        _backButtonCollapsed = false;
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
        _versionButton = null;
        _versionButtonOriginalCommand = null;
        _versionClickHandler = null;
        _lastListBoxIdx = -1;
        _injected = false;
        _lastLayoutCheckMs = 0;                            // Allow instant LayoutUpdated detection on next settings open
        _selectionSuppressedUntilMs = 0;
        _lastMenuRetargetMs = 0;
        _lastMenuForcedRebindMs = 0;
        Interlocked.Exchange(ref _injecting, 0);           // Clear stale timer lock from previous cycle
        // Restore fast poll interval so settings-page detection is responsive after close.
        _timer?.Change(PollIntervalMs, PollIntervalMs);
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

        // Sync ContentPages colors from Root's live visual tree.
        // This captures whatever Root is actually rendering — Dark, Light, or PureDark.
        // Reads foreground hex from a native nav TextBlock, background from the content panel.
        SyncContentPagesFromNativeTree(layout, nativeNavForeground);
        RetargetNativeMenuText(layout);

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
            // Sidebar section headers are slightly dimmer than nav labels.
            _r.SetTag(header, "dyn-fg:TextTertiary");
            _r.BindToDynamicResource(header, "Foreground", "TextTertiary");
            if (fontFamily != null)
                _r.SetFontFamily(header, fontFamily);
            _r.AddChild(container, header);
        }

        _r.AddChild(wrapper, container);
        return wrapper;
    }

    /// <summary>
    /// Force native sidebar menu text onto deterministic dyn-fg tags:
    /// - Section headers (IsHeaderItem=true, FontSize=12) -> TextTertiary
    /// - Clickable menu items (FontSize=14) -> TextPrimary
    /// Mirrors Root's MenuItemForegroundConverter behavior and prevents custom-theme preview desync.
    /// </summary>
    private void RetargetNativeMenuText(SettingsLayout layout)
    {
        RetargetNativeMenuText(layout.NavContainer, layout.ListBox, layout.SidebarGrid);
    }

    private void RetargetNativeMenuText(object navContainer, object? listBox, object? sidebarRoot)
    {
        var nowMs = Environment.TickCount64;
        bool forceRebind = nowMs - _lastMenuForcedRebindMs >= 4000;
        if (forceRebind) _lastMenuForcedRebindMs = nowMs;

        int headerCount = 0;
        int itemCount = 0;
        int statusCount = 0;
        int updatedCount = 0;
        var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);

        void ScanRoot(object root)
        {
            foreach (var node in _walker.DescendantsDepthFirst(root))
            {
                if (!visited.Add(node)) continue;
                if (!_r.IsTextBlock(node)) continue;

                string? key = null;
                bool isStatusLabel = false;
                var text = _r.GetText(node)?.Trim();
                var fs = _r.GetFontSize(node);

                // Profile "Online/Away/Offline" label uses converter brushes that can desync
                // from custom-theme text edits unless forced back to dynamic resources.
                if (!string.IsNullOrEmpty(text) && IsProfileStatusLabel(text))
                {
                    key = "TextSecondary";
                    isStatusLabel = true;
                }
                else if (fs is 14.0)
                {
                    key = "TextPrimary";
                }
                else if (fs is 12.0)
                {
                    // Section headers are uppercase in Root's settings menu.
                    key = IsSettingsSectionHeader(text) ? "TextTertiary" : null;
                }
                else if (IsSettingsSectionHeader(text))
                {
                    // Structural fallback if Root tweaks header font sizes.
                    key = "TextTertiary";
                }

                if (key == null) continue;
                var desiredTag = $"dyn-fg:{key}";
                var existingTag = _r.GetTag(node);
                bool needsTagUpdate = !string.Equals(existingTag, desiredTag, StringComparison.Ordinal);
                if (needsTagUpdate)
                {
                    _r.SetTag(node, desiredTag);
                    updatedCount++;
                }

                if (needsTagUpdate || forceRebind)
                    _r.BindToDynamicResource(node, "Foreground", key);

                // Profile status text ("Online", etc.) uses converter-driven brushes that
                // can drift during live preview. Force DynamicResource path every pass so it
                // follows the same update mechanism as other sidebar text.
                if (isStatusLabel)
                    _r.BindToDynamicResource(node, "Foreground", "TextSecondary");

                if (key == "TextTertiary") headerCount++;
                else if (key == "TextSecondary") statusCount++;
                else itemCount++;
            }
        }

        if (listBox != null) ScanRoot(listBox);
        ScanRoot(navContainer);
        if (sidebarRoot != null) ScanRoot(sidebarRoot);

        if (updatedCount > 0)
        {
            // This path can run every few hundred milliseconds while settings is open.
            // Keep logs high-signal: always log forced rebind passes, otherwise cap to
            // one informational line every 15s.
            bool shouldLog = forceRebind || nowMs - _lastMenuRetargetLogMs >= 15000;
            if (shouldLog)
            {
                _lastMenuRetargetLogMs = nowMs;
                Logger.Log("Injector", $"Retargeted native menu text: headers={headerCount}, items={itemCount}, status={statusCount}, updated={updatedCount}, force={forceRebind}");
            }
        }
    }

    private static bool IsSettingsSectionHeader(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        return text.Equals("USER SETTINGS", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("APP SETTINGS", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("User Settings", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("App Settings", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsProfileStatusLabel(string text)
    {
        return text.Equals("Online", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("Away", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("Offline", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("Active", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("Inactive", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("Disconnected", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("Busy", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("Do Not Disturb", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("Invisible", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Read Root's live colors from the actual visual tree and sync to ContentPages statics.
    /// This ensures Uprooted pages match Root's current Dark/Light/PureDark variant.
    /// Called at injection time — before any page builds.
    /// </summary>
    private void SyncContentPagesFromNativeTree(SettingsLayout layout, object? nativeNavForeground)
    {
        try
        {
            // Extract foreground hex from the native nav TextBlock brush
            string? fgHex = null;
            if (nativeNavForeground != null)
            {
                var colorProp = nativeNavForeground.GetType().GetProperty("Color");
                if (colorProp != null)
                {
                    var color = colorProp.GetValue(nativeNavForeground);
                    fgHex = color?.ToString(); // "#AARRGGBB"
                }
            }

            // Extract background hex from the content panel or sidebar grid
            string? bgHex = null;
            foreach (var source in new[] { layout.ContentArea, layout.SidebarGrid, layout.NavContainer })
            {
                if (source == null) continue;
                var bgProp = source.GetType().GetProperty("Background");
                if (bgProp == null) continue;
                var bgBrush = bgProp.GetValue(source);
                if (bgBrush == null) continue;
                var colorProp = bgBrush.GetType().GetProperty("Color");
                if (colorProp == null) continue;
                var color = colorProp.GetValue(bgBrush);
                if (color != null)
                {
                    bgHex = color.ToString();
                    break;
                }
            }

            // Sync to ContentPages statics
            if (fgHex != null)
            {
                var h = fgHex.TrimStart('#');
                if (h.Length == 6) h = "FF" + h;
                var rgb = h.Length >= 8 ? h[2..] : h;
                ContentPages.TextWhite = $"#FF{rgb}";
                ContentPages.TextMuted = $"#A3{rgb}";
                ContentPages.TextDim = $"#66{rgb}";
            }

            if (bgHex != null)
            {
                ContentPages.CardBg = bgHex;
            }

            // Read accent from ThemeEngine (which already has the correct value)
            var accent = _themeEngine.GetAccentColor();
            ContentPages.AccentGreen = accent;

            Logger.Log("Injector", $"Native colors synced: fg={fgHex ?? "null"} bg={bgHex ?? "null"} accent={accent}");
        }
        catch (Exception ex)
        {
            Logger.Log("Injector", $"SyncContentPagesFromNativeTree error: {ex.Message}");
        }
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
        _r.SetCursorHand(outerPanel, enablePressFeedback: false);
        _r.SetHeight(outerPanel, 40); // Match native ListBoxItem height exactly
        _r.SetBackground(outerPanel, "#00000000"); // Transparent BG required for hit-testing (pointer events)
        object? highlight = null;

        // Inner panel with vertical spacing matching native ListBoxItem
        var innerPanel = _r.CreatePanel();
        if (innerPanel != null)
        {
            _r.SetMargin(innerPanel, 0, 2, 0, 2);

            // Highlight border (behind content, full width, rounded corners)
            // Background-only highlight (no border). Native tabs remain untouched.
            highlight = _r.CreateBorder(cornerRadius: 12);
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
                // Bind to DynamicResource so text auto-updates on theme/variant change.
                // Falls back to native foreground or hardcoded color if binding fails.
                // Set initial foreground from native or fallback, then tag for walker recoloring
                if (nativeForeground != null)
                    _r.TextBlockType?.GetProperty("Foreground")?.SetValue(textBlock, nativeForeground);
                else
                    _r.SetForeground(textBlock, "#fff2f2f2");
                _r.SetTag(textBlock, "dyn-fg:TextPrimary");

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

            // Hover/selection: read Root's highlight resources for correct variant colors.
            // Dark = HighlightLight=#0AFFFFFF, HighlightNormal=#19FFFFFF, HighlightStrong=#30FFFFFF
            // Light = HighlightLight=#0A000000, HighlightNormal=#19000000, HighlightStrong=#30000000
            // Hover events on the outer panel (captures full area)
            _r.SubscribeEvent(outerPanel, "PointerEntered", () =>
            {
                if (_activePage != pageName && highlight != null)
                {
                    var hoverBgColor = _themeEngine.ReadLiveRootColor("HighlightLight") ?? "#0Dffffff";
                    _r.SetBackground(highlight, hoverBgColor);
                }
            });

            _r.SubscribeEvent(outerPanel, "PointerExited", () =>
            {
                if (_activePage != pageName && highlight != null)
                {
                    _r.SetBackground(highlight, (string?)null);
                }
            });
        }

        _r.SubscribeEvent(outerPanel, "PointerPressed", () =>
        {
            // Injected nav items are press-activated (native-feeling immediate navigation).
            OnNavItemClicked(pageName);
        });

        return outerPanel;
    }

    /// <summary>
    /// Apply a visible resting border to each native Root ListBoxItem so they match
    /// the rounded-border style of our injected Uprooted nav items.
    ///
    /// Root's ListBoxItem template already contains a Border (CornerRadius=12, Height=36)
    /// for hover/selection highlight — it just has no border stroke at rest.
    /// We set BorderBrush=HighlightLight and BorderThickness=1 on that border, matching
    /// exactly what BuildNavItem does for our custom items.
    /// </summary>
    private void StyleNativeNavItems(SettingsLayout layout)
    {
        if (layout.ListBox == null) return;

        var borderHex = _themeEngine.ReadLiveRootColor("HighlightLight") ?? "#0Dffffff";
        int styled = 0;

        foreach (var node in _walker.DescendantsDepthFirst(layout.ListBox))
        {
            if (node.GetType().Name != "ListBoxItem") continue;

            // Skip section header items ("USER SETTINGS", "APP SETTINGS").
            // They are ListBoxItems too but contain FontSize=11 TextBlocks; nav items use FontSize=14.
            bool isNavItem = false;
            foreach (var tb in _walker.DescendantsDepthFirst(node))
            {
                if (!_r.IsTextBlock(tb)) continue;
                if (_r.GetFontSize(tb) is 14.0) { isNavItem = true; break; }
            }
            if (!isNavItem) continue;

            // Find the rounded highlight Border (CornerRadius=12) inside this ListBoxItem.
            // Native structure: ListBoxItem → Panel(M=0,2,0,2) → [ContentPresenter..., Border(H=36, CR=12)]
            // Only the highlight border has CR=12; all layout/container borders have CR=0.
            foreach (var child in _walker.DescendantsDepthFirst(node))
            {
                if (!_r.IsBorder(child)) continue;

                var crObj = child.GetType().GetProperty("CornerRadius")?.GetValue(child);
                if (crObj == null) continue;
                var tl = crObj.GetType().GetProperty("TopLeft")?.GetValue(crObj) as double?;
                if (tl == null || tl < 8.0) continue; // CR=12 → accept ≥8; skip CR=0 layout borders

                _r.SetBorderBrush(child, borderHex);
                _r.SetBorderThickness(child, 1.0);
                styled++;
                break; // one highlight border per ListBoxItem
            }
        }

        Logger.Log("Injector", $"Native nav items: {styled} items styled with resting border");
    }

    // ===== Content management =====

    private void OnNavItemClicked(string pageName)
    {
        using var ev = WideEvent.Begin("Injector", "nav_click");
        ev.Set("page", pageName);
        try
        {
            // Handle navigation to Root's native settings tabs
            if (pageName == "root-themes")
            {
                SelectRootTab("Theme");
                ev.Set("result", "root_native_tab");
                return;
            }

            // Reload settings so page builds reflect runtime changes (e.g. channel switch)
            _settings = UprootedSettings.Load();

            if (_activePage == pageName)
            {
                ev.Set("result", "already_active");
                return;
            }

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
                // Ignore stale callbacks from a page that is no longer active.
                // Theme/custom controls use delayed timers; without this guard, a delayed
                // callback from Themes can force navigation away from About.
                if (_activePage != pageName)
                {
                    Logger.Log("Injector", $"Ignoring stale rebuild callback for '{pageName}' (active='{_activePage ?? "null"}')");
                    return;
                }

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
                ev.SetError($"BuildPage returned null for '{pageName}'");
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

            // Collapse back button BEFORE deselecting ListBox to prevent visual flash.
            // The IsVisible binding fallback shows the back arrow when SelectedMenuItemPageContainer
            // becomes null; collapsing via Opacity/MaxWidth/MaxHeight avoids fighting the binding.
            CollapseBackButton();

            // Deselect Root's ListBox items
            if (_listBox != null)
            {
                _r.SetSelectedIndex(_listBox, -1);
                _lastListBoxIdx = -1;
            }

            // Set header title AFTER deselecting (binding fires synchronously to null, then we override)
            SetHeaderTitle(pageName);

            // Collapse save bar on Uprooted pages (prevents Revert freeze)
            // Search dynamically since save bar may appear after injection
            FindAndCollapseSaveBar();

            _activePage = pageName;
            UpdateNavHighlights();

            // Immediate synchronous theme walk to prevent flash of unthemed content.
            // The LayoutUpdated interceptor has an 80ms debounce that can skip the walk
            // if a recent walk happened, causing a 1-frame flash of Root's default colors.
            _themeEngine.WalkVisualTreeNow();

            ev.Set("result", _contentPanel != null ? "displayed" : "stale_panel");
            ev.Set("hidden_children", _hiddenContentChildren.Count);

            // Schedule delayed save bar search: deselecting Root's ListBox triggers
            // Root's change detection which creates the save bar ASYNCHRONOUSLY.
            // We need to check again after Root has had time to create it.
            ScheduleDelayedSaveBarCollapse();
        }
        catch (Exception ex)
        {
            ev.SetError(ex);
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

    /// <summary>
    /// Programmatically navigate to a Root native settings tab by matching MenuTitle.
    /// Uses ListBox.SelectedItem (not SelectedIndex) to trigger the two-way binding
    /// to RootSettingsContainer.SelectedMenuItemPageContainer, which fires
    /// OnPropertyChanged → SelectMenuItem() → page content loads via Navigator.
    /// </summary>
    private void SelectRootTab(string menuTitleSubstring)
    {
        if (_listBox == null)
        {
            Logger.Log("Injector", $"SelectRootTab: no ListBox available");
            return;
        }

        // Remove Uprooted content first so Root's content area is clean
        RemoveContentPage();

        // Get the items collection (ObservableCollection<MenuItemPageContainerViewModel>)
        var itemsSource = _listBox.GetType().GetProperty("ItemsSource")?.GetValue(_listBox)
            as System.Collections.IEnumerable;
        if (itemsSource == null)
        {
            Logger.Log("Injector", $"SelectRootTab: ItemsSource is null");
            return;
        }

        // Find the MenuItemPageContainerViewModel with matching MenuTitle
        object? targetItem = null;
        int idx = 0;
        foreach (var item in itemsSource)
        {
            var menuTitle = item.GetType().GetProperty("MenuTitle")?.GetValue(item) as string;
            var isHeader = item.GetType().GetProperty("IsHeaderItem")?.GetValue(item) as bool? ?? false;
            if (!isHeader && menuTitle != null &&
                menuTitle.IndexOf(menuTitleSubstring, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                targetItem = item;
                Logger.Log("Injector", $"SelectRootTab: found '{menuTitle}' at index {idx}");
                break;
            }
            idx++;
        }

        if (targetItem == null)
        {
            Logger.Log("Injector", $"SelectRootTab: no item matching '{menuTitleSubstring}'");
            return;
        }

        // Set SelectedItem to trigger the two-way binding chain
        var selectedItemProp = _listBox.GetType().GetProperty("SelectedItem");
        selectedItemProp?.SetValue(_listBox, targetItem);
        _lastListBoxIdx = _r.GetSelectedIndex(_listBox);
        Logger.Log("Injector", $"SelectRootTab: set SelectedItem, idx now {_lastListBoxIdx}");
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
    /// Inject "Uprooted 0.5.0-rc" into the grey version info box at the bottom of the sidebar.
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

            // Read foreground from Root's existing "Root Version" TextBlock for pixel-perfect match.
            string versionFg = ContentPages.TextDim;
            foreach (var child in _r.GetVisualChildren(versionStackPanel))
            {
                if (!_r.IsTextBlock(child)) continue;
                var fg = _r.GetForeground(child);
                if (fg != null)
                {
                    var colorProp = fg.GetType().GetProperty("Color");
                    if (colorProp != null)
                    {
                        var color = colorProp.GetValue(fg);
                        if (color != null) { versionFg = color.ToString()!; break; }
                    }
                }
            }

            var versionText = _r.CreateTextBlock($"Uprooted {_settings.Version}", 10, versionFg);
            if (versionText == null) return;
            _r.SetTag(versionText, "dyn-fg:TextTertiary");
            _r.BindToDynamicResource(versionText, "Foreground", "TextTertiary");

            _r.AddChild(versionStackPanel, versionText);
            _versionTextBlock = versionText;
            _versionContainer = versionStackPanel;

            // Intercept version copy button to include Uprooted version in clipboard.
            // Root's CopySystemInfoCommand races with our clipboard write.
            // Fix: ClearValue the Command so Root never fires, then handle copy ourselves directly.
            var versionButton = FindAncestorOfType(versionStackPanel, "Button");
            if (versionButton != null)
            {
                // Save original Command value, then clear it via Avalonia property system
                // (CLR setter is trimmed in Root's single-file binary)
                _versionButtonOriginalCommand = _r.GetAvaloniaPropertyByName(versionButton, "CommandProperty");
                _r.ClearValue(versionButton, "CommandProperty");

                _versionButton = versionButton;
                var container = versionStackPanel;
                _versionClickHandler = _r.SubscribeEvent(versionButton, "Click", () =>
                {
                    try
                    {
                        var lines = new System.Collections.Generic.List<string>();
                        foreach (var child in _r.GetVisualChildren(container))
                        {
                            if (!_r.IsTextBlock(child)) continue;
                            var txt = _r.GetText(child);
                            if (!string.IsNullOrWhiteSpace(txt))
                                lines.Add(txt);
                        }
                        if (lines.Count > 0)
                        {
                            var combined = string.Join("\n", lines);
                            _r.CopyToClipboard(_window, combined);
                            Logger.Log("Injector", $"Version copy: copied {lines.Count} lines to clipboard");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Injector", $"Version copy error: {ex.Message}");
                    }
                });
                Logger.Log("Injector", "Version copy: intercepted Button.Command, subscribed Click");
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
        // Restore Root's original Command on the version button
        if (_versionButton != null)
        {
            if (_versionClickHandler != null)
            {
                _r.UnsubscribeEvent(_versionButton, "Click", _versionClickHandler);
                _versionClickHandler = null;
            }
            if (_versionButtonOriginalCommand != null)
            {
                try { _r.SetAvaloniaPropertyByName(_versionButton, "CommandProperty", _versionButtonOriginalCommand); }
                catch { }
            }
        }
        _versionButton = null;
        _versionButtonOriginalCommand = null;

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
    /// Find the back button and title TextBlock in the header Grid by child order.
    /// The header Grid (Row=0 of layoutContainer) has 3 direct children:
    ///   [0] RootSvgButton (back arrow, Col=0) — DownArrowSVG rotated 90°
    ///   [1] TextBlock (page title, Col=1) — bound to Navigator.CurrentViewModel.PageTitle
    ///   [2] RootSvgButton (close button, Col=2) — ExitThickSVG
    /// Works before layout has run (no bounds needed). Called once during injection.
    /// </summary>
    private void FindHeaderControls()
    {
        if (_layoutContainer == null) return;

        // Find header Grid at Row=0
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
            Logger.Log("Injector", "FindHeaderControls: no Grid at Row=0");
            return;
        }

        // Direct children in order: back button (first Button-like), title (first TextBlock)
        foreach (var child in _r.GetVisualChildren(headerGrid))
        {
            var typeName = child.GetType().Name;

            if (_backButton == null && typeName.Contains("Button"))
            {
                _backButton = child;
                _backButtonWasVisible = _r.GetIsVisible(child);
                if (_closeCommand == null)
                {
                    try
                    {
                        _closeCommand = child.GetType().GetProperty("Command")?.GetValue(child);
                        _closeCommandParam = child.GetType().GetProperty("CommandParameter")?.GetValue(child);
                    }
                    catch { }
                }
                Logger.Log("Injector", $"Back button found: {typeName}, visible={_backButtonWasVisible}");
            }
            else if (_headerTitleText == null && _r.IsTextBlock(child))
            {
                _headerTitleText = child;
                _headerTitleOriginal = _r.GetText(child);
                Logger.Log("Injector", $"Title found: \"{_headerTitleOriginal}\"");
            }
        }
    }

    /// <summary>
    /// Collapse back button visually without touching IsVisible. Zeros Opacity, MaxWidth,
    /// MaxHeight, Width, and Margin so the Auto column in the header Grid measures to 0px.
    /// Root's IsVisible binding (SelectedMenuItemPageContainer.Navigator.CanGoBack) is unaffected.
    /// Same pattern as CollapseSaveBar — avoids fighting Avalonia bindings.
    /// </summary>
    private void CollapseBackButton()
    {
        if (_backButton == null || _backButtonCollapsed) return;
        try
        {
            _backButton.GetType().GetProperty("Opacity")?.SetValue(_backButton, 0.0);
            _backButton.GetType().GetProperty("IsHitTestVisible")?.SetValue(_backButton, false);
            _backButton.GetType().GetProperty("MaxWidth")?.SetValue(_backButton, 0.0);
            _backButton.GetType().GetProperty("MaxHeight")?.SetValue(_backButton, 0.0);
            _backButton.GetType().GetProperty("Width")?.SetValue(_backButton, 0.0);
            _r.SetMargin(_backButton, 0, 0, 0, 0);         // Root sets Margin=(24,0,0,0) — must zero for column collapse
            _backButtonCollapsed = true;
        }
        catch (Exception ex) { Logger.Log("Injector", $"CollapseBackButton error: {ex.Message}"); }
    }

    /// <summary>
    /// Restore back button to normal state. Reverses CollapseBackButton by resetting
    /// Opacity/MaxWidth/MaxHeight/Width/Margin to Root's original values. IsVisible is never touched.
    /// </summary>
    private void RestoreBackButtonVisuals()
    {
        if (_backButton == null || !_backButtonCollapsed) return;
        try
        {
            _backButton.GetType().GetProperty("Opacity")?.SetValue(_backButton, 1.0);
            _backButton.GetType().GetProperty("IsHitTestVisible")?.SetValue(_backButton, true);
            _backButton.GetType().GetProperty("MaxWidth")?.SetValue(_backButton, double.PositiveInfinity);
            _backButton.GetType().GetProperty("MaxHeight")?.SetValue(_backButton, double.PositiveInfinity);
            _backButton.GetType().GetProperty("Width")?.SetValue(_backButton, 40.0);  // Root's inline Width=40
            _r.SetMargin(_backButton, 24, 0, 0, 0);        // Root's inline Margin=(24,0,0,0)
            _backButtonCollapsed = false;
        }
        catch (Exception ex) { Logger.Log("Injector", $"RestoreBackButton error: {ex.Message}"); }
    }

    /// <summary>
    /// Set the header title TextBlock to display our page name.
    /// Called AFTER deselecting ListBox — the binding fires synchronously to null,
    /// then our SetValue overrides at the same priority level.
    /// </summary>
    private void SetHeaderTitle(string pageName)
    {
        if (_headerTitleText == null) return;
        var displayName = pageName == "uprooted" ? "About"
            : char.ToUpper(pageName[0]) + pageName[1..];
        try { _headerTitleText.GetType().GetProperty("Text")?.SetValue(_headerTitleText, displayName); }
        catch { }
    }

    private void RestoreBackButton()
    {
        RestoreBackButtonVisuals();
        // Keep _backButton and _headerTitleText references for next Uprooted tab switch.
        // Root's binding overwrites the title text when a Root tab is selected.
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

        // Use Root's highlight resources — adapts to Dark/Light themes
        var selectionBg = _themeEngine.ReadLiveRootColor("HighlightNormal") ?? "#19ffffff";

        foreach (var node in _walker.DescendantsDepthFirst(_navContainer))
        {
            var tag = _r.GetTag(node);
            if (tag == null || !tag.StartsWith("uprooted-highlight-")) continue;

            var itemPage = tag["uprooted-highlight-".Length..];
            bool isSelected = itemPage == _activePage;
            _r.SetBackground(node, isSelected ? selectionBg : null);
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
