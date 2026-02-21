using System.Collections;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Rootcord: Discord-style vertical server strip replacing Root's horizontal tab bar.
/// Performs visual tree surgery on HomeView's Grid to hide the Tabalonia TabsControl
/// and inject a vertical StackPanel with circular server icons.
///
/// Apply/Revert lifecycle supports no-restart toggle from the Plugins page.
/// </summary>
internal class RootcordEngine
{
    private readonly AvaloniaReflection _r;
    private readonly object _mainWindow;
    private readonly ThemeEngine? _themeEngine;

    // Singleton for ContentPages toggle access
    internal static RootcordEngine? Instance;

    // Discovery results
    private object? _homeViewGrid;
    private object? _tabsControl;
    private object? _rootSplitView;
    private object? _homeViewModel;

    // Injected controls
    private object? _serverStrip;         // The vertical StackPanel
    private object? _serverStripBorder;   // Border wrapping the strip (placed in grid)
    private object? _homeButton;          // Home icon container (top of strip)
    private object? _stripGrid;           // Inner Grid (2 rows) inside the strip border

    // SystemTray (Row 1 collapse)
    private object? _systemTrayBorder;    // The SystemTray border at Row 1, Col 4
    private int _originalSystemTrayRow;   // Original grid row of SystemTray
    private double _originalRow1Height;   // Original Row 1 height (for revert)
    private string _originalRow1UnitType = "Pixel";

    // Original layout state (for revert)
    private double _originalCol0Width;
    private string _originalCol0UnitType = "Pixel";
    private double _originalCol4Width;
    private double _originalCol5Width;
    private bool _tabsWasVisible = true;
    private readonly List<(object control, int originalColSpan)> _modifiedColSpans = new();

    // Tab monitoring
    private object? _tabsCollectionHandler;
    private object? _selectionHandler;
    private object? _tabsCollection; // INotifyCollectionChanged source

    // Hover tooltip state
    private object? _tooltipOverlay;  // OverlayLayer reference
    private object? _tooltipPanel;    // The tooltip Border on the overlay

    // Cached theme colors (refreshed from ContentPages statics each Apply)
    private string _bg = "#0a1018";
    private string _cardBg = "#0f1923";
    private string _border = "#19ffffff";
    private string _text = "#fff2f2f2";
    private string _muted = "#a3f2f2f2";
    private string _dim = "#66f2f2f2";
    private string _accent = "#2A5A40";

    // Profile pane control
    private bool _wasProfilePaneOpen;

    // User bar (Discord-style bottom-left bar, overlaps channel list via ZIndex)
    private object? _userBar;

    // User bar — PanePlacement guard state
    private string? _originalPanePlacement;  // for Revert of PanePlacement
    private object? _panePlacementGuardHandler; // PropertyChanged guard to re-assert Left

    // ToolTip placement flip (member hover name tooltips) — cached via EnsureToolTipMethods()
    private MethodInfo? _toolTipGetTipMethod;
    private MethodInfo? _toolTipSetPlacementMethod;
    private object? _tooltipLeftPlacement;  // PlacementMode.Left enum value

    // Community members sidebar rotation
    private object? _communityGrid;
    private List<(object child, int originalCol)>? _originalChildColumns;
    private List<(int colIdx, double width, string unit)>? _originalColWidths;
    private List<(object splitter, int originalCol)>? _originalSplitterColumns;

    // Custom community header injected into channels panel
    private object? _injectedHeader;       // The header Border we built
    private object? _channelsPanelRef;     // The channels panel we modified

    // Flyout placement flip (member profile popups)
    private readonly HashSet<int> _flippedFlyoutIds = new();
    private object? _membersLayoutUpdatedHandler;
    private object? _membersPanel; // Members panel reference for LayoutUpdated unsub
    private long _lastFlyoutFlipTick;  // Debounce: Environment.TickCount64 of last flip
    private const int FlyoutFlipDebounceMs = 150;
    private object? _overlayMonitorHandler; // Monitors overlay layer for right-side popups

    // State
    public bool IsApplied { get; private set; }
    // CancellationTokenSource for fire-and-forget Task.Delay calls started during Apply.
    // Cancelled in Revert() so pending callbacks don't run after we've cleaned up state.
    private CancellationTokenSource? _applyCts;

    private const string Tag = "Rootcord";
    private const double StripWidth = 56;
    private const double IconSize = 42;
    private const double IconCornerRadius = 21;
    private const double IconSpacing = 4;
    private const double PillWidth = 3;
    private const double PillHeight = 20;
    // Icon glyphs for user bar buttons (Segoe Fluent Icons / Segoe MDL2 Assets)
    private const string GlyphFriends       = "\uE716"; // Contact / People
    private const string GlyphMessages      = "\uE8F2"; // Message / Chat
    private const string GlyphNotifications = "\uEA8F"; // Ringer / Bell
    private const string GlyphSettings      = "\uE713"; // Settings gear
    private const string GlyphIconFonts     = "Segoe Fluent Icons,Segoe MDL2 Assets,Segoe UI Symbol";

    public RootcordEngine(AvaloniaReflection resolver, object mainWindow, ThemeEngine? themeEngine)
    {
        _r = resolver;
        _mainWindow = mainWindow;
        _themeEngine = themeEngine;
    }

    // ===== Color helpers =====

    /// <summary>
    /// Refresh cached color fields from ContentPages statics (live-updated by ThemeEngine).
    /// </summary>
    private void RefreshColors()
    {
        _cardBg = ContentPages.CardBg;
        _bg     = ColorUtils.Darken(_cardBg, 6);
        _border = ContentPages.CardBorder;
        _text   = ContentPages.TextWhite;
        _muted  = ContentPages.TextMuted;
        _dim    = ContentPages.TextDim;
        _accent = ContentPages.AccentGreen;
    }

    /// <summary>
    /// Luminance-aware highlight: lightens on dark backgrounds, darkens on light backgrounds.
    /// Same algorithm as ContentPages.AdjustForHighlight.
    /// </summary>
    private static string AdjustForHighlight(string bg, double percent)
    {
        try
        {
            return ColorUtils.Luminance(bg) > 0.5
                ? ColorUtils.Darken(bg, percent)
                : ColorUtils.Lighten(bg, percent);
        }
        catch { return bg; }
    }

    /// <summary>
    /// Set border brush and thickness on a Border control.
    /// </summary>
    private void SetBorderStroke(object? b, string hex, double w)
    {
        _r.SetBorderBrush(b, hex);
        _r.SetBorderThickness(b, w);
    }

    // ===== Lifecycle =====

    public void Apply()
    {
        if (IsApplied)
        {
            Logger.Log(Tag, "Already applied, skipping");
            return;
        }

        try
        {
            Logger.Log(Tag, "Applying Discord-style layout...");

            // Fresh token for any fire-and-forget delays started during this Apply session
            _applyCts?.Cancel();
            _applyCts?.Dispose();
            _applyCts = new CancellationTokenSource();

            // Refresh cached colors from ContentPages statics
            RefreshColors();

            // Step 1: Discover HomeView grid
            if (!DiscoverHomeViewGrid())
            {
                Logger.Log(Tag, "Discovery failed: HomeView grid not found");
                return;
            }

            // Step 2: Find key controls
            if (!FindKeyControls())
            {
                Logger.Log(Tag, "Discovery failed: could not find TabsControl or SplitView");
                return;
            }

            // Step 3: Save original state and modify grid columns
            SaveOriginalState();
            ModifyGridColumns();

            // Step 4: Hide tab bar and collapse Row 1
            _tabsWasVisible = _r.GetIsVisible(_tabsControl);
            _r.SetIsVisible(_tabsControl, false);
            Logger.Log(Tag, "TabsControl hidden");

            CollapseTabBarRow();

            // Step 5: Close right-side Profile pane (our strip card replaces it)
            CloseProfilePane();

            // Step 6: Build and inject server strip + user bar
            BuildAndInjectServerStrip();
            BuildAndInjectUserBar();

            // Step 6b: Flip SplitView pane to open on the left (utility pane between strip + channel list)
            ApplyUtilityPanePlacement();

            // Step 7: Subscribe to tab changes + pane open monitoring
            SubscribeTabChanges();

            // Step 7b: Periodic check for Profile pane Sign Out button rearrangement.
            // We can't reliably hook into pane open events (RoutedEvent, not CLR event),
            // so we poll every 2s when the pane is open.
            var signOutToken = _applyCts?.Token ?? System.Threading.CancellationToken.None;
            _ = Task.Run(async () =>
            {
                while (!signOutToken.IsCancellationRequested)
                {
                    await Task.Delay(2000, signOutToken);
                    if (signOutToken.IsCancellationRequested) break;
                    _r.RunOnUIThread(() => { try { RearrangeSignOutButton(); } catch { } });
                }
            }, signOutToken);

            // Step 8: Swap community members sidebar to right (Discord-style)
            SwapCommunityMembersToRight();

            IsApplied = true;
            Logger.Log(Tag, "Apply complete — Discord-style layout active");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Apply error: {ex.Message}");
            // Attempt partial revert on failure
            try { Revert(); } catch { }
        }
    }

    public void Revert()
    {
        try
        {
            Logger.Log(Tag, "Reverting to original layout...");

            // Cancel any pending fire-and-forget delays (e.g. tab-switch retry)
            _applyCts?.Cancel();

            // Dismiss any active tooltip/popup
            DismissIconTooltip();
            DismissUserCardPopup();

            // Revert community members sidebar swap
            RevertCommunityMembersSwap();

            // Unsubscribe tab monitoring + profile intercept
            UnsubscribeTabChanges();

            // Restore Profile pane if it was open before Apply
            if (_wasProfilePaneOpen && _homeViewModel != null)
            {
                _r.SetPropertyValue(_homeViewModel, "PaneOpen", true);
                _r.SetPropertyValue(_homeViewModel, "ProfileOpen", true);
                Logger.Log(Tag, "Profile pane restored");
            }
            _wasProfilePaneOpen = false;

            // Remove user bar overlay
            RevertUserBar();

            // Restore SplitView PanePlacement
            RevertUtilityPanePlacement();

            // Remove server strip from grid
            if (_serverStripBorder != null && _homeViewGrid != null)
            {
                _r.RemoveChild(_homeViewGrid, _serverStripBorder);
                Logger.Log(Tag, "Server strip removed");
            }
            _serverStrip = null;
            _serverStripBorder = null;
            _homeButton = null;
            _stripGrid = null;

            // Restore Row 1 height
            RestoreTabBarRow();

            // Restore column widths
            RestoreGridColumns();

            // Restore ColSpan values
            foreach (var (control, originalSpan) in _modifiedColSpans)
            {
                _r.SetGridColumnSpan(control, originalSpan);
            }
            _modifiedColSpans.Clear();

            // Show tab bar
            if (_tabsControl != null)
            {
                _r.SetIsVisible(_tabsControl, _tabsWasVisible);
                Logger.Log(Tag, "TabsControl restored");
            }

            IsApplied = false;
            Logger.Log(Tag, "Revert complete — original layout restored");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Revert error: {ex.Message}");
            IsApplied = false;
        }
    }

    // ===== Profile pane control =====

    /// <summary>
    /// Close the right-side Profile pane on Apply.
    /// Our bottom-left user card handles Profile functionality.
    /// </summary>
    private void CloseProfilePane()
    {
        if (_homeViewModel == null) return;
        try
        {
            _wasProfilePaneOpen = _r.GetPropertyValue(_homeViewModel, "ProfileOpen") is true;
            if (_wasProfilePaneOpen)
            {
                _r.SetPropertyValue(_homeViewModel, "PaneOpen", false);
                _r.SetPropertyValue(_homeViewModel, "ProfileOpen", false);
                Logger.Log(Tag, "Profile pane closed (was open)");
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"CloseProfilePane error: {ex.Message}");
        }
    }

    // ===== Discovery =====

    /// <summary>
    /// Walk from MainWindow to find the HomeView's root Grid.
    /// HomeView is identified by its DataContext type containing "HomeViewModel".
    /// The Grid is the first child Grid with 4+ rows and 5+ columns.
    /// </summary>
    private bool DiscoverHomeViewGrid()
    {
        var walker = new VisualTreeWalker(_r);

        foreach (var node in walker.DescendantsDepthFirst(_mainWindow))
        {
            var dc = _r.GetDataContext(node);
            if (dc == null) continue;

            var dcTypeName = dc.GetType().Name;
            if (!dcTypeName.Contains("HomeViewModel")) continue;

            Logger.Log(Tag, $"Found HomeView candidate: {node.GetType().Name} (DC={dcTypeName})");

            // Look for the Grid child with the expected column/row count
            foreach (var child in _r.GetVisualChildren(node))
            {
                if (!_r.IsGrid(child)) continue;

                var colDefs = _r.GetColumnDefinitions(child);
                var rowDefs = _r.GetRowDefinitions(child);
                int colCount = colDefs?.Count ?? 0;
                int rowCount = rowDefs?.Count ?? 0;

                Logger.Log(Tag, $"  Grid candidate: {colCount} cols, {rowCount} rows");

                if (colCount >= 5 && rowCount >= 3)
                {
                    _homeViewGrid = child;
                    _homeViewModel = dc;
                    Logger.Log(Tag, $"HomeView Grid found: {colCount}x{rowCount}");
                    return true;
                }
            }

            // The HomeView itself might be the Grid
            if (_r.IsGrid(node))
            {
                var colDefs = _r.GetColumnDefinitions(node);
                var rowDefs = _r.GetRowDefinitions(node);
                int colCount = colDefs?.Count ?? 0;
                int rowCount = rowDefs?.Count ?? 0;
                if (colCount >= 5 && rowCount >= 3)
                {
                    _homeViewGrid = node;
                    _homeViewModel = dc;
                    Logger.Log(Tag, $"HomeView IS the Grid: {colCount}x{rowCount}");
                    return true;
                }
            }
        }

        Logger.Log(Tag, "HomeView Grid not found in visual tree");
        return false;
    }

    /// <summary>
    /// Find TabsControl and RootSplitView within the HomeView Grid.
    /// Uses heuristic type name matching for resilience against updates.
    /// </summary>
    private bool FindKeyControls()
    {
        if (_homeViewGrid == null) return false;

        int tabsRow = -1;

        foreach (var child in _r.GetVisualChildren(_homeViewGrid))
        {
            var typeName = child.GetType().Name;

            // TabsControl: Tabalonia's tab bar
            if (typeName.Contains("TabsControl") || typeName.Contains("TabControl"))
            {
                _tabsControl = child;
                tabsRow = _r.GetGridRow(child);
                int col = _r.GetGridColumn(child);
                Logger.Log(Tag, $"TabsControl found: {typeName} at Row={tabsRow} Col={col}");
            }

            // RootSplitView: main content + right pane
            if (typeName.Contains("SplitView"))
            {
                _rootSplitView = child;
                int row = _r.GetGridRow(child);
                int col = _r.GetGridColumn(child);
                Logger.Log(Tag, $"SplitView found: {typeName} at Row={row} Col={col}");
            }
        }

        // Find SystemTrayBorder: shares Row 1 with TabsControl but is NOT the TabsControl
        // It's typically a Border in the same row containing the action buttons
        if (tabsRow >= 0)
        {
            foreach (var child in _r.GetVisualChildren(_homeViewGrid))
            {
                if (child == _tabsControl) continue;
                int row = _r.GetGridRow(child);
                if (row == tabsRow && _r.IsBorder(child))
                {
                    _systemTrayBorder = child;
                    _originalSystemTrayRow = row;
                    Logger.Log(Tag, $"SystemTrayBorder found: {child.GetType().Name} at Row={row} Col={_r.GetGridColumn(child)}");
                    break;
                }
            }
        }

        if (_tabsControl == null)
            Logger.Log(Tag, "WARNING: TabsControl not found");
        if (_rootSplitView == null)
            Logger.Log(Tag, "WARNING: SplitView not found");
        if (_systemTrayBorder == null)
            Logger.Log(Tag, "WARNING: SystemTrayBorder not found");

        // We require at least TabsControl to proceed
        return _tabsControl != null;
    }

    // ===== Grid manipulation =====

    private void SaveOriginalState()
    {
        if (_homeViewGrid == null) return;
        var colDefs = _r.GetColumnDefinitions(_homeViewGrid);
        if (colDefs == null || colDefs.Count < 1) return;

        var col0Info = _r.GetColumnDefinitionWidth(colDefs[0]);
        if (col0Info != null)
        {
            _originalCol0Width = col0Info.Value.Value;
            _originalCol0UnitType = col0Info.Value.UnitType;
            Logger.Log(Tag, $"Original Col0: {_originalCol0Width} ({_originalCol0UnitType})");
        }
    }

    /// <summary>
    /// Widen Column 0 from padding (16px) to strip width (56px).
    /// Adjust ColSpan of controls that span all columns.
    /// </summary>
    private void ModifyGridColumns()
    {
        if (_homeViewGrid == null) return;
        var colDefs = _r.GetColumnDefinitions(_homeViewGrid);
        if (colDefs == null || colDefs.Count < 1) return;

        // Widen column 0 for the server strip
        _r.SetColumnDefinitionPixelWidth(colDefs[0], StripWidth);
        Logger.Log(Tag, $"Col0 widened to {StripWidth}px");

        // Zero Col 4 (SystemTray, 200px) and Col 5 (right padding, 16px)
        // so the SplitView content extends flush to the right window edge
        if (colDefs.Count >= 5)
        {
            var col4Info = _r.GetColumnDefinitionWidth(colDefs[4]);
            _originalCol4Width = col4Info?.Value ?? 200;
            _r.SetColumnDefinitionPixelWidth(colDefs[4], 0);
        }
        if (colDefs.Count >= 6)
        {
            var col5Info = _r.GetColumnDefinitionWidth(colDefs[5]);
            _originalCol5Width = col5Info?.Value ?? 16;
            _r.SetColumnDefinitionPixelWidth(colDefs[5], 0);
        }
        Logger.Log(Tag, $"Col4/Col5 zeroed for flush-right layout");

        // Find all children that start at Col=0 and span multiple columns.
        // These are typically TabsControl (ColSpan=6) and SplitView (ColSpan=6).
        // We need to adjust their ColSpan and Col to skip our strip column,
        // or if they're the TabsControl (hidden), leave them alone.
        foreach (var child in _r.GetVisualChildren(_homeViewGrid))
        {
            if (child == _serverStripBorder) continue; // skip our own control
            if (child == _tabsControl) continue;       // TabsControl is hidden

            int col = _r.GetGridColumn(child);
            int colSpan = _r.GetGridColumnSpan(child);

            // Controls starting at Col 0 with span > 1 need adjustment:
            // shift to Col 1 and reduce span by 1 so they don't overlap our strip
            if (col == 0 && colSpan > 1)
            {
                _modifiedColSpans.Add((child, colSpan));
                _r.SetGridColumn(child, 1);
                _r.SetGridColumnSpan(child, colSpan - 1);
                Logger.Log(Tag, $"  Adjusted {child.GetType().Name}: Col 0->1, Span {colSpan}->{colSpan - 1}");
            }
        }
    }

    private void RestoreGridColumns()
    {
        if (_homeViewGrid == null) return;
        var colDefs = _r.GetColumnDefinitions(_homeViewGrid);
        if (colDefs == null || colDefs.Count < 1) return;

        // Restore original Col 0 width
        _r.SetColumnDefinitionPixelWidth(colDefs[0], _originalCol0Width);

        // Restore Col 4 and Col 5
        if (colDefs.Count >= 5)
            _r.SetColumnDefinitionPixelWidth(colDefs[4], _originalCol4Width);
        if (colDefs.Count >= 6)
            _r.SetColumnDefinitionPixelWidth(colDefs[5], _originalCol5Width);
        Logger.Log(Tag, $"Col0/4/5 restored to {_originalCol0Width}/{_originalCol4Width}/{_originalCol5Width}px");

        // Restore original Col positions
        foreach (var (control, originalSpan) in _modifiedColSpans)
        {
            _r.SetGridColumn(control, 0);
            // ColSpan restored separately below
        }
    }

    // ===== Utility pane placement (SplitView PanePlacement = Left) =====

    /// <summary>
    /// Flip SplitView.PanePlacement to Left so utility panes (people, DMs, notifications)
    /// open to the LEFT of the SplitView content — between the server strip and channel list.
    /// </summary>
    private void ApplyUtilityPanePlacement()
    {
        if (_rootSplitView == null) return;
        try
        {
            var pp = _rootSplitView.GetType().GetProperty("PanePlacement");
            if (pp == null) { Logger.Log(Tag, "PanePlacement not found on SplitView"); return; }
            _originalPanePlacement = pp.GetValue(_rootSplitView)?.ToString();
            var leftVal = Enum.Parse(pp.PropertyType, "Left");
            pp.SetValue(_rootSplitView, leftVal);
            Logger.Log(Tag, $"SplitView PanePlacement → Left (was {_originalPanePlacement})");

            // Prevent the Overlay→Inline flash in togglePane():
            // Root's togglePane() checks CommunityPaneDisplayMode==Overlay and switches to Inline.
            // We set CommunityPaneDisplayMode to CompactInline (not Overlay and not Inline) so:
            // - The check `== Overlay` fails → no mode switch → no flash
            // - GlobalPaneDisplayMode stays Overlay → pane content fills full width when closed
            // We must NOT set GlobalPaneDisplayMode to Inline (that reserves 320px pane space).
            try
            {
                if (_homeViewModel != null)
                {
                    var pds = _r.GetPropertyValue(_homeViewModel, "PaneDisplayService");
                    if (pds != null)
                    {
                        // Try to set CommunityPaneDisplayMode directly (bypasses GlobalPaneDisplayMode)
                        var commProp = pds.GetType().GetProperty("CommunityPaneDisplayMode");
                        if (commProp != null && commProp.CanWrite)
                        {
                            // Set to CompactInline — not Overlay (avoids flash) and not Inline (avoids space reservation)
                            var compactInline = Enum.Parse(commProp.PropertyType, "CompactInline");
                            commProp.SetValue(pds, compactInline);
                            Logger.Log(Tag, "PaneDisplayService.CommunityPaneDisplayMode → CompactInline (prevents flash)");
                        }
                        else
                        {
                            // Fallback: try SetCommunityPaneDisplayMode method
                            var setCommunityMethod = pds.GetType().GetMethod("SetCommunityPaneDisplayMode");
                            if (setCommunityMethod != null)
                            {
                                var parms = setCommunityMethod.GetParameters();
                                if (parms.Length > 0)
                                {
                                    var compactInline = Enum.Parse(parms[0].ParameterType, "CompactInline");
                                    setCommunityMethod.Invoke(pds, new[] { compactInline });
                                    Logger.Log(Tag, "PaneDisplayService.CommunityPaneDisplayMode → CompactInline via method");
                                }
                            }
                            else
                            {
                                Logger.Log(Tag, "PaneDisplayService: no way to set CommunityPaneDisplayMode (will use native behavior)");
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Logger.Log(Tag, $"PaneDisplayService set error: {ex.Message}"); }

            // Guard: if Root resets PanePlacement to Right, immediately re-assert Left
            var capturedSplitView = _rootSplitView;
            _panePlacementGuardHandler = _r.SubscribePropertyChanged(_rootSplitView, (propName) =>
            {
                if (propName == "PanePlacement" && IsApplied)
                {
                    var current = capturedSplitView.GetType().GetProperty("PanePlacement")
                        ?.GetValue(capturedSplitView)?.ToString() ?? "";
                    if (!current.Equals("Left", StringComparison.OrdinalIgnoreCase))
                    {
                        _r.RunOnUIThread(() =>
                        {
                            try
                            {
                                var ppInner = capturedSplitView.GetType().GetProperty("PanePlacement");
                                if (ppInner != null)
                                {
                                    var lv = Enum.Parse(ppInner.PropertyType, "Left");
                                    ppInner.SetValue(capturedSplitView, lv);
                                    Logger.Log(Tag, "PanePlacement guard: re-asserted Left");
                                }
                            }
                            catch { }
                        });
                    }
                }
            });
            if (_panePlacementGuardHandler != null)
                Logger.Log(Tag, "PanePlacement guard subscribed");
        }
        catch (Exception ex) { Logger.Log(Tag, $"ApplyUtilityPanePlacement error: {ex.Message}"); }
    }

    /// <summary>
    /// Restore SplitView.PanePlacement to its original value.
    /// </summary>
    private void RevertUtilityPanePlacement()
    {
        // Unsubscribe guard first so it doesn't fight the restore
        if (_panePlacementGuardHandler != null && _rootSplitView != null)
        {
            try { _r.UnsubscribePropertyChanged(_rootSplitView, _panePlacementGuardHandler); }
            catch { }
            _panePlacementGuardHandler = null;
        }

        if (_rootSplitView == null || _originalPanePlacement == null) return;
        try
        {
            var pp = _rootSplitView.GetType().GetProperty("PanePlacement");
            if (pp == null) return;
            var val = Enum.Parse(pp.PropertyType, _originalPanePlacement);
            pp.SetValue(_rootSplitView, val);
            Logger.Log(Tag, $"SplitView PanePlacement restored → {_originalPanePlacement}");
            _originalPanePlacement = null;

            // Note: PaneDisplayService.CommunityPaneDisplayMode was set to Inline during Apply
            // to prevent toggle flash. Root will reset it naturally on next theme/layout cycle.
        }
        catch (Exception ex) { Logger.Log(Tag, $"RevertUtilityPanePlacement error: {ex.Message}"); }
    }

    // ===== Row 1 collapse (tab bar row) =====

    /// <summary>
    /// Collapse Row 1 (the 60px tab bar row) to reclaim vertical space.
    /// Hides the SystemTray entirely — our user card popup replaces all its functions.
    /// </summary>
    private void CollapseTabBarRow()
    {
        if (_homeViewGrid == null) return;

        var rowDefs = _r.GetRowDefinitions(_homeViewGrid);
        if (rowDefs == null || rowDefs.Count < 2) return;

        // Save original Row 1 height
        var row1Info = _r.GetRowDefinitionHeight(rowDefs[1]);
        if (row1Info != null)
        {
            _originalRow1Height = row1Info.Value.Value;
            _originalRow1UnitType = row1Info.Value.UnitType;
            Logger.Log(Tag, $"Original Row 1: {_originalRow1Height} ({_originalRow1UnitType})");
        }

        // Hide SystemTray — our user card at the bottom of the strip replaces all its functions
        if (_systemTrayBorder != null)
            _r.SetIsVisible(_systemTrayBorder, false);

        // Collapse Row 1 to 0px
        _r.SetRowDefinitionPixelHeight(rowDefs[1], 0);
        Logger.Log(Tag, "Row 1 collapsed to 0px");
    }

    /// <summary>
    /// Restore Row 1 height to original state and show SystemTray.
    /// </summary>
    private void RestoreTabBarRow()
    {
        if (_homeViewGrid == null) return;

        // Restore Row 1 height
        var rowDefs = _r.GetRowDefinitions(_homeViewGrid);
        if (rowDefs != null && rowDefs.Count >= 2)
        {
            _r.SetRowDefinitionPixelHeight(rowDefs[1], _originalRow1Height);
            Logger.Log(Tag, $"Row 1 restored to {_originalRow1Height}px");
        }

        // Show SystemTray (Row 1 is restored, it's visible again)
        if (_systemTrayBorder != null)
            _r.SetIsVisible(_systemTrayBorder, true);
    }

    /// <summary>
    /// Hide the UserProfileButton (avatar button) within the SystemTray.
    /// It's the last panel/button in the SystemTray's child grid.
    /// </summary>
    private object? _hiddenProfilePanel;

    private void HideUserProfileButton()
    {
        if (_systemTrayBorder == null) return;
        try
        {
            // Walk into SystemTray: Border → Grid → Panels containing buttons
            var trayChild = _r.GetBorderChild(_systemTrayBorder);
            if (trayChild == null) return;

            // The child may be a Grid; get its children
            var trayChildren = _r.GetChildren(trayChild);
            if (trayChildren == null || trayChildren.Count == 0)
            {
                // Try visual children
                foreach (var vc in _r.GetVisualChildren(trayChild))
                {
                    trayChildren = _r.GetChildren(vc);
                    if (trayChildren != null && trayChildren.Count > 0)
                    {
                        trayChild = vc;
                        break;
                    }
                }
            }
            if (trayChildren == null) return;

            // The UserProfileButton is the last panel (panel21) in the tray grid
            // Find it by looking for a panel containing an Image/Button
            for (int i = trayChildren.Count - 1; i >= 0; i--)
            {
                var panel = trayChildren[i];
                if (panel == null) continue;

                // Check if this panel contains something with "Profile" or is the last visible item
                // The profile panel typically has a 30x30 Button with TransparentButton class
                var panelChildren = _r.GetChildren(panel);
                if (panelChildren == null)
                {
                    // Try visual children
                    var visualKids = new List<object>();
                    foreach (var vk in _r.GetVisualChildren(panel))
                        visualKids.Add(vk);
                    if (visualKids.Count > 0)
                    {
                        // Last panel in tray is the profile button panel
                        _hiddenProfilePanel = panel;
                        _r.SetIsVisible(panel, false);
                        Logger.Log(Tag, $"Hidden UserProfileButton panel (index {i})");
                        return;
                    }
                }
                else if (panelChildren.Count > 0)
                {
                    // Check last panel - the profile panel is the rightmost (last) one
                    if (i == trayChildren.Count - 1)
                    {
                        _hiddenProfilePanel = panel;
                        _r.SetIsVisible(panel, false);
                        Logger.Log(Tag, $"Hidden UserProfileButton panel (last child, index {i})");
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"HideUserProfileButton error: {ex.Message}");
        }
    }

    // ===== Community members sidebar swap =====

    /// <summary>
    /// Swap the community sidebar (col 0) and chat content (last col) so members appear on the right.
    /// Walks from SplitView to find CommunityView Grid, then swaps column positions + widths.
    /// No-op when a DM tab is selected (no CommunityView in the tree).
    /// </summary>
    private void SwapCommunityMembersToRight()
    {
        if (_rootSplitView == null) return;

        // Revert any previous swap before applying new one
        RevertCommunityMembersSwap();

        try
        {
            var walker = new VisualTreeWalker(_r);

            // Walk from SplitView to find the CommunityView
            object? communityView = null;
            foreach (var node in walker.DescendantsDepthFirst(_rootSplitView))
            {
                var dc = _r.GetDataContext(node);
                if (dc == null) continue;
                if (dc.GetType().Name.Contains("CommunityViewModel"))
                {
                    communityView = node;
                    break;
                }
            }

            if (communityView == null)
            {
                Logger.Log(Tag, "SwapCommunityMembers: no CommunityView found (DM tab?)");
                return;
            }

            // Find the main layout Grid inside CommunityView (2+ column definitions)
            // Use deep tree walk — the Grid may be several levels down
            object? layoutGrid = null;

            Logger.Log(Tag, $"SwapCommunityMembers: CommunityView is {communityView.GetType().Name}");

            foreach (var descendant in walker.DescendantsDepthFirst(communityView))
            {
                if (_r.IsGrid(descendant))
                {
                    var colDefs = _r.GetColumnDefinitions(descendant);
                    int colCount = colDefs?.Count ?? 0;
                    if (colCount >= 2)
                    {
                        layoutGrid = descendant;
                        Logger.Log(Tag, $"SwapCommunityMembers: found Grid ({descendant.GetType().Name}) with {colCount} cols");
                        break;
                    }
                }
            }

            if (layoutGrid == null)
            {
                // Diagnostic: log what controls exist in the CommunityView
                int childCount = 0;
                foreach (var child in _r.GetVisualChildren(communityView))
                {
                    Logger.Log(Tag, $"SwapCommunityMembers: child[{childCount}] = {child.GetType().Name}");
                    childCount++;
                }
                Logger.Log(Tag, $"SwapCommunityMembers: no layout Grid found in CommunityView ({childCount} direct children)");
                return;
            }

            var gridColDefs = _r.GetColumnDefinitions(layoutGrid);
            if (gridColDefs == null || gridColDefs.Count < 2)
            {
                Logger.Log(Tag, $"SwapCommunityMembers: Grid has {gridColDefs?.Count ?? 0} cols, need 2+");
                return;
            }

            // Gather layout-area children sorted by column.
            // Separate GridSplitters (resize handles) from content panels.
            var nonSplitters = new List<(object child, int col)>();
            var gridSplitters = new List<(object child, int col)>();
            foreach (var child in _r.GetVisualChildren(layoutGrid))
            {
                var typeName = child.GetType().Name;
                if (typeName == "Decorator" || typeName == "AdornerDecorator") continue;
                int col = _r.GetGridColumn(child);
                if (typeName.Contains("GridSplitter"))
                    gridSplitters.Add((child, col));
                else
                    nonSplitters.Add((child, col));
            }
            nonSplitters.Sort((a, b) => a.col.CompareTo(b.col));

            if (nonSplitters.Count < 2)
            {
                Logger.Log(Tag, $"SwapCommunityMembers: only {nonSplitters.Count} non-splitter children, need 2+");
                return;
            }

            // Save original state for revert
            _communityGrid = layoutGrid;
            _originalChildColumns = nonSplitters.Select(c => (c.child, c.col)).ToList();
            _originalSplitterColumns = gridSplitters.Select(s => (s.child, s.col)).ToList();
            _originalColWidths = new List<(int, double, string)>();
            for (int i = 0; i < gridColDefs.Count; i++)
            {
                var w = _r.GetColumnDefinitionWidth(gridColDefs[i]);
                _originalColWidths.Add(w != null
                    ? (i, w.Value.Value, w.Value.UnitType)
                    : (i, 0, "Auto"));
            }
            Logger.Log(Tag, $"SwapCommunityMembers: {gridColDefs.Count} colDefs, {nonSplitters.Count} panels, {gridSplitters.Count} splitters");

            // Rotate: move members sidebar (col 0) to rightmost, shift others left.
            // Original: [Members(col A)] [Channels(col B)] [Chat(col C)]
            // Target:   [Channels(col A)] [Chat(col B)]    [Members(col C)]
            var columns = nonSplitters.Select(c => c.col).ToList();

            // Build old→new column mapping for the rotation
            int n = nonSplitters.Count;
            var colMapping = new Dictionary<int, int>(); // oldCol → newCol
            for (int i = 0; i < n; i++)
            {
                int newCol = columns[(i + n - 1) % n];
                colMapping[columns[i]] = newCol;
                _r.SetGridColumn(nonSplitters[i].child, newCol);
            }

            // Rotate GridSplitters using the same column mapping.
            // A splitter at col X should move to the mapped col for X, or the nearest mapped col.
            foreach (var (splitter, oldCol) in gridSplitters)
            {
                if (colMapping.TryGetValue(oldCol, out int mappedCol))
                {
                    _r.SetGridColumn(splitter, mappedCol);
                }
                else
                {
                    // Splitter is between two panels — find the nearest panel column <= splitterCol
                    // and apply its mapping offset
                    int bestOld = -1;
                    foreach (var oc in columns)
                        if (oc <= oldCol && oc > bestOld) bestOld = oc;
                    if (bestOld >= 0 && colMapping.TryGetValue(bestOld, out int panelNew))
                    {
                        int offset = oldCol - bestOld;
                        _r.SetGridColumn(splitter, panelNew + offset);
                    }
                }
            }
            Logger.Log(Tag, $"SwapCommunityMembers: rotated {gridSplitters.Count} GridSplitters");

            // Set correct column widths after rotation.
            // The original layout is [Members=Auto] [Splitter=1px] [Chat=Star].
            // After rotation: Members moves to the rightmost column.
            // The chat panel (originally Star) must KEEP Star wherever it lands.
            // The members panel (now rightmost) must be Auto.
            // All other panels keep their original width type.
            //
            // Find which panel ended up in the rightmost column (that's Members now).
            int maxAssignedCol = -1;
            int maxAssignedIdx = -1;
            for (int i = 0; i < n; i++)
            {
                int assignedCol = _r.GetGridColumn(nonSplitters[i].child);
                if (assignedCol > maxAssignedCol) { maxAssignedCol = assignedCol; maxAssignedIdx = i; }
            }

            for (int i = 0; i < n; i++)
            {
                int assignedCol = _r.GetGridColumn(nonSplitters[i].child);
                if (assignedCol >= gridColDefs.Count) continue;

                if (i == maxAssignedIdx)
                {
                    // This is the rightmost panel (Members after rotation) → Auto
                    SetColumnDefinitionWidth(gridColDefs[assignedCol], 0, "Auto");
                }
                else
                {
                    // Preserve original width type from this panel's source column
                    int origCol = nonSplitters[i].col;
                    var origWidth = _originalColWidths.FirstOrDefault(x => x.colIdx == origCol);
                    if (origWidth.unit == "Star" || origWidth.unit == "star")
                        SetColumnDefinitionWidth(gridColDefs[assignedCol], origWidth.width, "Star");
                    else if (origWidth.unit == "Auto" || origWidth.width == 0)
                        SetColumnDefinitionWidth(gridColDefs[assignedCol], 0, "Auto");
                    else
                        SetColumnDefinitionWidth(gridColDefs[assignedCol], origWidth.width, origWidth.unit);
                }
            }

            // Safety: ensure at least one Star column exists for chat to fill space
            bool hasStar = false;
            for (int i = 0; i < gridColDefs.Count; i++)
            {
                var w = _r.GetColumnDefinitionWidth(gridColDefs[i]);
                if (w?.UnitType == "Star") { hasStar = true; break; }
            }
            if (!hasStar)
            {
                // Find the panel that originally had Star and force its new column to Star
                for (int i = 0; i < n; i++)
                {
                    int origCol = nonSplitters[i].col;
                    var origWidth = _originalColWidths.FirstOrDefault(x => x.colIdx == origCol);
                    if (origWidth.unit == "Star" || origWidth.unit == "star")
                    {
                        int assignedCol = _r.GetGridColumn(nonSplitters[i].child);
                        if (assignedCol < gridColDefs.Count)
                        {
                            SetColumnDefinitionWidth(gridColDefs[assignedCol], 1, "Star");
                            Logger.Log(Tag, $"SwapCommunityMembers: forced ColDef[{assignedCol}] to Star (was {origWidth.unit})");
                        }
                        break;
                    }
                }
            }

            var names = string.Join("  ", nonSplitters.Select((c, idx) =>
                $"{c.child.GetType().Name}@{c.col}→{columns[(idx + n - 1) % n]}"));
            Logger.Log(Tag, $"SwapCommunityMembers: rotated {nonSplitters.Count} children: {names}");
            // (column rebuild below will replace these definitions)

            // Deep-clear right-side margins/paddings from SplitView down to the members column host
            // Level 1+2: SplitView immediate visual children (content template wrapper + their children)
            try
            {
                foreach (var child in _r.GetVisualChildren(_rootSplitView))
                {
                    _r.SetMargin(child, 0, 0, 0, 0);
                    _r.SetPadding(child, 0, 0, 0, 0);
                    foreach (var grandchild in _r.GetVisualChildren(child))
                    {
                        _r.SetMargin(grandchild, 0, 0, 0, 0);
                        _r.SetPadding(grandchild, 0, 0, 0, 0);
                    }
                    break; // only the content-side host
                }
            }
            catch { }

            // CommunityView, layoutGrid, and their immediate children — zero margins/padding
            // so the grid stretches to fill the full SplitView content area
            try
            {
                _r.SetMargin(communityView, 0, 0, 0, 0);
                _r.SetPadding(communityView, 0, 0, 0, 0);
                foreach (var child in _r.GetVisualChildren(communityView))
                {
                    _r.SetMargin(child, 0, 0, 0, 0);
                    _r.SetPadding(child, 0, 0, 0, 0);
                }
                // Also zero the layoutGrid itself (it has Margin=(6,0,6,0) from CommunityTabView)
                _r.SetMargin(layoutGrid, 0, 0, 0, 0);
                _r.SetPadding(layoutGrid, 0, 0, 0, 0);
            }
            catch { }

            // Members column host: rightmost non-splitter child; walk its tree to clear ScrollViewer padding
            try
            {
                object? membersHost = null;
                int maxMemberCol = -1;
                foreach (var child in _r.GetVisualChildren(layoutGrid))
                {
                    if (child.GetType().Name.Contains("GridSplitter")) continue;
                    int col = _r.GetGridColumn(child);
                    if (col > maxMemberCol) { maxMemberCol = col; membersHost = child; }
                }
                if (membersHost != null)
                {
                    _r.SetMargin(membersHost, 0, 0, 0, 0);
                    _r.SetPadding(membersHost, 0, 0, 0, 0);
                    var deepWalker = new VisualTreeWalker(_r);
                    foreach (var node in deepWalker.DescendantsDepthFirst(membersHost))
                    {
                        var tn = node.GetType().Name;
                        if (tn.Contains("ScrollViewer") || tn.Contains("ScrollContentPresenter"))
                        {
                            _r.SetPadding(node, 0, 0, 0, 0);
                            _r.SetMargin(node, 0, 0, 0, 0);
                        }
                    }
                }
            }
            catch { }

            // Zero out any extra column definitions beyond the rightmost non-splitter column
            try
            {
                int maxUsedCol = columns.Max();
                if (gridColDefs != null)
                {
                    for (int i = maxUsedCol + 1; i < gridColDefs.Count; i++)
                    {
                        var w = _r.GetColumnDefinitionWidth(gridColDefs[i]);
                        if (w != null && w.Value.Value > 0 && w.Value.UnitType == "Pixel")
                            _r.SetColumnDefinitionPixelWidth(gridColDefs[i], 0);
                    }
                }
            }
            catch { }

            // Avalonia's Grid layout engine doesn't re-measure columns correctly
            // when ColumnDefinition.Width is changed via reflection (trimmed binary).
            // Workaround: instead of changing column definitions, set HorizontalAlignment=Stretch
            // on the chat panel and remove the layoutGrid's existing column definitions entirely,
            // replacing them with fresh ones.
            try
            {
                // Clear ALL column definitions and re-add them with the correct sizing
                var colDefsList = _r.GetColumnDefinitions(layoutGrid);
                if (colDefsList != null)
                {
                    // Read current count before clearing
                    int originalCount = colDefsList.Count;

                    // Remove all existing column defs
                    var clearMethod = colDefsList.GetType().GetMethod("Clear");
                    clearMethod?.Invoke(colDefsList, null);

                    // Re-add column defs with correct sizing
                    // Find which columns need what type
                    for (int ci = 0; ci < originalCount; ci++)
                    {
                        // Determine what this column should be
                        bool isChatCol = false;
                        bool isMembersCol = false;
                        for (int pi = 0; pi < n; pi++)
                        {
                            int assignedCol = _r.GetGridColumn(nonSplitters[pi].child);
                            if (assignedCol == ci)
                            {
                                if (pi == maxAssignedIdx)
                                    isMembersCol = true;
                                else
                                {
                                    int origCol = nonSplitters[pi].col;
                                    var origW = _originalColWidths.FirstOrDefault(x => x.colIdx == origCol);
                                    if (origW.unit == "Star" || origW.unit == "star")
                                        isChatCol = true;
                                }
                            }
                        }

                        if (isChatCol)
                            _r.AddGridColumnStar(layoutGrid, 1.0);
                        else if (isMembersCol)
                            _r.AddGridColumnAuto(layoutGrid);
                        else
                        {
                            // Channels or splitter column — use original Pixel width.
                            // The channels panel was at a Pixel column in Root's native
                            // layout (resizable via GridSplitter). Keep it Pixel so the
                            // splitter continues to work. Use 300px default if Auto/tiny.
                            var origW = _originalColWidths.FirstOrDefault(x => x.colIdx == ci);
                            if (origW.unit == "Pixel" && origW.width > 10)
                                _r.AddGridColumnPixel(layoutGrid, origW.width);
                            else
                                _r.AddGridColumnPixel(layoutGrid, 300); // Root's native default ~300px
                        }
                    }
                    Logger.Log(Tag, $"SwapCommunityMembers: rebuilt {originalCount} column definitions");
                }

                // Invalidate to trigger fresh layout
                layoutGrid.GetType().GetMethod("InvalidateMeasure")?.Invoke(layoutGrid, null);
                layoutGrid.GetType().GetMethod("InvalidateArrange")?.Invoke(layoutGrid, null);
            }
            catch (Exception ex) { Logger.Log(Tag, $"SwapCommunityMembers: column rebuild error: {ex.Message}"); }

            // Build a custom community header and inject it at the top of the channels column.
            // Also hide the native header in the members column.
            // Deferred 500ms so the ContentControl→MembersView/ChannelsView visual tree is ready.
            var headerLayoutGrid = layoutGrid;
            var headerNonSplitters = nonSplitters;
            var headerMaxIdx = maxAssignedIdx;
            var headerToken = _applyCts?.Token ?? System.Threading.CancellationToken.None;
            Task.Delay(500, headerToken).ContinueWith(_ =>
            {
                if (headerToken.IsCancellationRequested) return;
                _r.RunOnUIThread(() =>
                {
                    try { InjectChannelsHeader(headerLayoutGrid, headerNonSplitters, headerMaxIdx); }
                    catch (Exception ex) { Logger.Log(Tag, $"InjectChannelsHeader error: {ex.Message}"); }
                });
            }, System.Threading.Tasks.TaskContinuationOptions.NotOnCanceled);

            // Flip member profile flyout placements (Right → Left) after rotation
            FlipMemberFlyoutPlacements();
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"SwapCommunityMembers error: {ex.Message}");
        }
    }

    /// <summary>
    /// Revert the community sidebar column rotation to original positions.
    /// </summary>
    private void RevertCommunityMembersSwap()
    {
        // Revert flyout placements before restoring column positions
        RevertMemberFlyoutPlacements();

        if (_communityGrid == null) return;

        try
        {
            // Restore child column assignments
            if (_originalChildColumns != null)
            {
                foreach (var (child, originalCol) in _originalChildColumns)
                    _r.SetGridColumn(child, originalCol);
            }

            // Restore GridSplitter column assignments
            if (_originalSplitterColumns != null)
            {
                foreach (var (splitter, originalCol) in _originalSplitterColumns)
                    _r.SetGridColumn(splitter, originalCol);
            }

            // Restore column definition widths
            var colDefs = _r.GetColumnDefinitions(_communityGrid);
            if (colDefs != null && _originalColWidths != null)
            {
                foreach (var (colIdx, width, unit) in _originalColWidths)
                {
                    if (colIdx < colDefs.Count)
                        SetColumnDefinitionWidth(colDefs[colIdx], width, unit);
                }
            }

            Logger.Log(Tag, "SwapCommunityMembers: reverted to original layout");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"RevertCommunityMembersSwap error: {ex.Message}");
        }

        _communityGrid = null;
        _originalChildColumns = null;
        _originalColWidths = null;
        _originalSplitterColumns = null;
    }

    // ===== Community header injection =====

    /// <summary>
    /// Build a custom community header and inject it at the top of the channels panel.
    /// Also hide the native header in the members panel so it's not duplicated.
    /// Uses data from the selected CommunityTabViewModel (name, icon, member count).
    /// </summary>
    private void InjectChannelsHeader(object layoutGrid, List<(object child, int col)> nonSplitters, int membersIdx)
    {
        if (_homeViewModel == null) return;

        // Get the selected tab's data
        var selectedTab = _r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel");
        if (selectedTab == null || IsDmTab(selectedTab)) return;

        string communityName = GetTabDisplayName(selectedTab);
        var (attached, total) = GetTabMemberCounts(selectedTab);
        object? iconBitmap = TryGetTabBitmap(selectedTab);
        string initial = communityName.Length > 0 ? communityName[0].ToString().ToUpper() : "?";

        // Find the channels panel (leftmost non-chat after rotation)
        object? channelsPanel = null;
        for (int i = 0; i < nonSplitters.Count; i++)
        {
            if (i == membersIdx) continue;
            int origCol = nonSplitters[i].col;
            var origW = _originalColWidths?.FirstOrDefault(x => x.colIdx == origCol);
            if (origW != null && origW.Value.unit != "Star" && origW.Value.unit != "star")
            {
                channelsPanel = nonSplitters[i].child;
                break;
            }
        }
        if (channelsPanel == null) return;

        // Skip if we already injected a header into this panel
        if (_injectedHeader != null && _channelsPanelRef == channelsPanel) return;

        // Build the header: [Icon 40x40] [Name + member count]
        var headerBorder = _r.CreateBorder(_cardBg, 12);
        if (headerBorder == null) return;
        _r.SetTag(headerBorder, "rootcord-channel-header");
        _r.SetMargin(headerBorder, 10, 10, 10, 6);
        SetBorderStroke(headerBorder, AdjustForHighlight(_cardBg, 15), 0.5);

        var headerGrid = _r.CreateGrid();
        if (headerGrid == null) return;
        _r.SetMargin(headerGrid, 10, 8, 10, 8);

        // 3 columns: [Auto icon] [8px gap] [* text]
        _r.AddGridColumn(headerGrid, 1.0); // col 0 — will set to Auto
        _r.AddGridColumn(headerGrid, 1.0); // col 1 — 8px
        _r.AddGridColumn(headerGrid, 1.0); // col 2 — Star (text)
        var hColDefs = _r.GetColumnDefinitions(headerGrid);
        if (hColDefs?.Count >= 3 && _r.GridUnitTypeEnum != null && _r.GridLengthType != null)
        {
            try
            {
                var autoUnit = Enum.Parse(_r.GridUnitTypeEnum, "Auto");
                var pixelUnit = Enum.Parse(_r.GridUnitTypeEnum, "Pixel");
                hColDefs[0]?.GetType().GetProperty("Width")?.SetValue(hColDefs[0],
                    Activator.CreateInstance(_r.GridLengthType, 0d, autoUnit));
                hColDefs[1]?.GetType().GetProperty("Width")?.SetValue(hColDefs[1],
                    Activator.CreateInstance(_r.GridLengthType, 8d, pixelUnit));
                // Col 2 stays Star
            }
            catch { }
        }

        // Col 0: Community icon (40x40 circle)
        var iconContainer = _r.CreateBorder(_bg, 6);
        if (iconContainer != null)
        {
            _r.SetWidth(iconContainer, 40);
            _r.SetHeight(iconContainer, 40);
            _r.SetClipToBounds(iconContainer, true);
            _r.SetVerticalAlignment(iconContainer, "Center");
            _r.SetGridColumn(iconContainer, 0);

            if (iconBitmap != null)
            {
                var img = _r.CreateImage("UniformToFill");
                if (img != null)
                {
                    _r.SetImageSource(img, iconBitmap);
                    _r.SetWidth(img, 40);
                    _r.SetHeight(img, 40);
                    _r.SetBorderChild(iconContainer, img);
                }
                else
                    SetAvatarInitial(iconContainer, initial, _text);
            }
            else
                SetAvatarInitial(iconContainer, initial, _text);

            _r.AddChild(headerGrid, iconContainer);
        }

        // Col 2: Name + member count
        var textPanel = _r.CreateStackPanel(vertical: true, spacing: 2);
        if (textPanel != null)
        {
            _r.SetGridColumn(textPanel, 2);
            _r.SetVerticalAlignment(textPanel, "Center");

            var nameText = _r.CreateTextBlock(communityName, 14, _text);
            if (nameText != null)
            {
                _r.SetFontWeight(nameText, "SemiBold");
                try
                {
                    nameText.GetType().GetProperty("TextTrimming")?.SetValue(nameText,
                        Enum.Parse(nameText.GetType().GetProperty("TextTrimming")!.PropertyType, "CharacterEllipsis"));
                }
                catch { }
                _r.AddChild(textPanel, nameText);
            }

            string countStr = total > 0 ? $"{total} members" : (attached > 0 ? $"{attached} online" : "");
            if (countStr.Length > 0)
            {
                var countText = _r.CreateTextBlock(countStr, 11, _muted);
                if (countText != null) _r.AddChild(textPanel, countText);
            }

            _r.AddChild(headerGrid, textPanel);
        }

        _r.SetBorderChild(headerBorder, headerGrid);

        // Inject into the channels panel by wrapping its existing content in a Grid(2 rows)
        var existingContent = _r.GetBorderChild(channelsPanel);
        if (existingContent != null)
        {
            _r.SetBorderChild(channelsPanel, null); // detach

            var wrapperGrid = _r.CreateGrid();
            if (wrapperGrid != null)
            {
                _r.SetTag(wrapperGrid, "rootcord-channel-wrapper");
                _r.AddGridRowAuto(wrapperGrid);   // Row 0: header
                _r.AddGridRowStar(wrapperGrid);   // Row 1: channel list (fills remaining)

                _r.SetGridRow(headerBorder, 0);
                _r.AddChild(wrapperGrid, headerBorder);

                _r.SetGridRow(existingContent, 1);
                _r.AddChild(wrapperGrid, existingContent);

                _r.SetBorderChild(channelsPanel, wrapperGrid);
            }
            else
            {
                _r.SetBorderChild(channelsPanel, existingContent); // rollback
            }
        }

        _injectedHeader = headerBorder;
        _channelsPanelRef = channelsPanel;

        // Hide the native header in the members panel (Row 0-3 of MembersView's inner Grid)
        var membersPanel = nonSplitters[membersIdx].child;
        var walker = new VisualTreeWalker(_r);
        foreach (var node in walker.DescendantsDepthFirst(membersPanel))
        {
            if (!_r.IsGrid(node)) continue;
            var rowDefs = _r.GetRowDefinitions(node);
            if (rowDefs == null || rowDefs.Count < 4) continue;

            // Found MembersView inner Grid — hide rows 0-3
            foreach (var child in _r.GetVisualChildren(node))
            {
                int row = _r.GetGridRow(child);
                if (row <= 3) _r.SetIsVisible(child, false);
            }
            for (int ri = 0; ri <= 3 && ri < rowDefs.Count; ri++)
                _r.SetRowDefinitionPixelHeight(rowDefs[ri], 0);

            Logger.Log(Tag, "InjectChannelsHeader: hidden native header in members panel");
            break;
        }

        Logger.Log(Tag, $"InjectChannelsHeader: injected '{communityName}' header into channels panel");
    }

    // ===== Flyout placement flip (member profile popups) =====

    /// <summary>
    /// Flip member profile flyout placements from RightEdgeAlignedTop to LeftEdgeAlignedTop.
    /// After rotating members to the right side, profiles would open further right / off-screen.
    /// Subscribes LayoutUpdated on the members panel to handle virtualized rows.
    /// </summary>
    private void FlipMemberFlyoutPlacements()
    {
        if (_communityGrid == null) return;

        try
        {
            // Find the rightmost child (members panel after rotation)
            object? membersPanel = null;
            int maxCol = -1;
            foreach (var child in _r.GetVisualChildren(_communityGrid))
            {
                if (child.GetType().Name.Contains("GridSplitter")) continue;
                int col = _r.GetGridColumn(child);
                if (col > maxCol)
                {
                    maxCol = col;
                    membersPanel = child;
                }
            }

            if (membersPanel == null)
            {
                Logger.Log(Tag, "FlipMemberFlyouts: no members panel found");
                return;
            }

            _membersPanel = membersPanel;
            int flipped = FlipFlyoutsInTree(membersPanel, "", "");
            Logger.Log(Tag, $"FlipMemberFlyouts: initial scan flipped {flipped} flyouts");

            // Also flip ToolTip placements for member name hover pills (Left so they open to the left)
            FlipTooltipsInTree(membersPanel);

            // Subscribe LayoutUpdated to catch virtualized rows appearing on scroll.
            // LayoutUpdated uses EventHandler (not RoutedEvent), so we can subscribe via reflection.
            try
            {
                var eventInfo = membersPanel.GetType().GetEvent("LayoutUpdated");
                if (eventInfo != null)
                {
                    var capturedPanel = membersPanel;
                    EventHandler handler = (_, _) =>
                    {
                        // Debounce: skip if less than 150ms since last flip (LayoutUpdated
                        // fires dozens of times per second during scrolling/animation).
                        var now = Environment.TickCount64;
                        if (now - _lastFlyoutFlipTick < FlyoutFlipDebounceMs) return;
                        _lastFlyoutFlipTick = now;

                        try
                        {
                            _flippedFlyoutIds.Clear();
                            FlipFlyoutsInTree(capturedPanel, "", "");
                        }
                        catch { }
                        try { FlipTooltipsInTree(capturedPanel); }
                        catch { }
                    };
                    eventInfo.AddEventHandler(membersPanel, handler);
                    _membersLayoutUpdatedHandler = handler;
                    Logger.Log(Tag, "FlipMemberFlyouts: subscribed LayoutUpdated for virtualized rows");
                }
            }
            catch (Exception ex2)
            {
                Logger.Log(Tag, $"FlipMemberFlyouts: LayoutUpdated subscription failed: {ex2.Message}");
            }

            // Subscribe overlay monitor to catch dynamically created profile popouts
            SubscribeOverlayPopupMonitor();
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"FlipMemberFlyouts error: {ex.Message}");
        }
    }

    /// <summary>
    /// Walk a visual tree and flip flyout + context menu placements from one mode to another.
    /// Uses a HashSet to skip already-processed flyouts. Returns count of newly flipped.
    /// </summary>
    private int FlipFlyoutsInTree(object root, string fromPlacement, string toPlacement)
    {
        int count = 0;
        var walker = new VisualTreeWalker(_r);

        foreach (var node in walker.DescendantsDepthFirst(root))
        {
            int nodeId = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(node);
            if (_flippedFlyoutIds.Contains(nodeId)) continue;

            bool flipped = false;

            try
            {
                // Check if this control has a Flyout property
                var flyoutProp = node.GetType().GetProperty("Flyout");
                if (flyoutProp != null)
                {
                    var flyout = flyoutProp.GetValue(node);
                    if (flyout != null)
                    {
                        var placementProp = flyout.GetType().GetProperty("Placement");
                        if (placementProp != null)
                        {
                            var currentPlacement = placementProp.GetValue(flyout);
                            if (currentPlacement != null)
                            {
                                var placementType = placementProp.PropertyType;
                                if (placementType.IsEnum)
                                {
                                    // Flip any "Right*" placement to its Left equivalent
                                    var currentStr = currentPlacement.ToString() ?? "";
                                    if (currentStr.Contains("Right", StringComparison.OrdinalIgnoreCase))
                                    {
                                        try
                                        {
                                            string flippedName = currentStr.Replace("Right", "Left",
                                                StringComparison.OrdinalIgnoreCase);
                                            var toValue = Enum.Parse(placementType, flippedName);
                                            placementProp.SetValue(flyout, toValue);
                                            flipped = true;
                                            count++;
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                var leftVal = Enum.Parse(placementType, "Left");
                                                placementProp.SetValue(flyout, leftVal);
                                                flipped = true;
                                                count++;
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            // Also flip ContextMenu.PlacementMode / ContextMenu.Placement (Change D)
            try
            {
                var contextMenuProp = node.GetType().GetProperty("ContextMenu");
                if (contextMenuProp != null)
                {
                    var cm = contextMenuProp.GetValue(node);
                    if (cm != null)
                    {
                        var placementProp = cm.GetType().GetProperty("PlacementMode")
                            ?? cm.GetType().GetProperty("Placement");
                        if (placementProp != null)
                        {
                            var val = placementProp.GetValue(cm)?.ToString() ?? "";
                            if (val.Contains("Right", StringComparison.OrdinalIgnoreCase))
                            {
                                try
                                {
                                    // Build the "to" name by replacing Right → Left
                                    string toName = val.Replace("Right", "Left",
                                        StringComparison.OrdinalIgnoreCase);
                                    var leftVal = Enum.Parse(placementProp.PropertyType, toName);
                                    placementProp.SetValue(cm, leftVal);
                                    if (!flipped) { flipped = true; count++; }
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch { }

            if (flipped)
                _flippedFlyoutIds.Add(nodeId);
        }

        return count;
    }

    /// <summary>
    /// Revert member flyout placements back to RightEdgeAlignedTop and unsubscribe LayoutUpdated.
    /// </summary>
    private void RevertMemberFlyoutPlacements()
    {
        // Unsubscribe LayoutUpdated handler
        if (_membersLayoutUpdatedHandler != null && _membersPanel != null)
        {
            try
            {
                // LayoutUpdated is an EventHandler, unsubscribe via reflection
                var eventInfo = _membersPanel.GetType().GetEvent("LayoutUpdated");
                if (eventInfo != null && _membersLayoutUpdatedHandler is Delegate del)
                    eventInfo.RemoveEventHandler(_membersPanel, del);
            }
            catch { }
            _membersLayoutUpdatedHandler = null;
        }

        // Flip any LeftEdgeAlignedTop back to RightEdgeAlignedTop
        if (_membersPanel != null)
        {
            try
            {
                // Clear the HashSet so we re-scan everything
                _flippedFlyoutIds.Clear();
                int reverted = FlipFlyoutsInTree(_membersPanel, "LeftEdgeAlignedTop", "RightEdgeAlignedTop");
                Logger.Log(Tag, $"RevertMemberFlyouts: reverted {reverted} flyouts");
            }
            catch { }

            // Revert ToolTip placements to Pointer (Avalonia default)
            try { RevertTooltipsInTree(_membersPanel); }
            catch { }
        }

        _flippedFlyoutIds.Clear();
        _membersPanel = null;

        // Unsubscribe overlay popup monitor
        if (_overlayMonitorHandler != null)
        {
            try
            {
                var mainWindow = _r.GetMainWindow();
                if (mainWindow != null)
                {
                    var overlay = _r.GetOverlayLayer(mainWindow);
                    if (overlay != null)
                    {
                        var children = _r.GetChildren(overlay);
                        if (children != null)
                            _r.UnsubscribeCollectionChanged(children, _overlayMonitorHandler);
                    }
                }
            }
            catch { }
            _overlayMonitorHandler = null;
        }
    }

    /// <summary>
    /// Subscribe to the OverlayLayer's children collection to catch dynamically created profile
    /// popouts that appear on the right side and reposition them leftward.
    /// </summary>
    private void SubscribeOverlayPopupMonitor()
    {
        try
        {
            var mainWindow = _r.GetMainWindow();
            if (mainWindow == null) { Logger.Log(Tag, "OverlayMonitor: mainWindow not found"); return; }
            var overlay = _r.GetOverlayLayer(mainWindow);
            if (overlay == null) { Logger.Log(Tag, "OverlayMonitor: overlay layer not found"); return; }

            var children = _r.GetChildren(overlay);
            if (children == null) { Logger.Log(Tag, "OverlayMonitor: overlay children not accessible"); return; }

            _overlayMonitorHandler = _r.SubscribeCollectionChanged(children, () =>
            {
                _r.RunOnUIThread(() =>
                {
                    try { RepositionOverlayPopupsLeft(overlay); }
                    catch { }
                });
            });

            if (_overlayMonitorHandler != null)
                Logger.Log(Tag, "OverlayMonitor: subscribed to overlay children changes");
            else
                Logger.Log(Tag, "OverlayMonitor: subscription returned null (INCC not supported?)");
        }
        catch (Exception ex) { Logger.Log(Tag, $"OverlayMonitor subscribe error: {ex.Message}"); }
    }

    /// <summary>
    /// Walk the overlay layer children; any popup that appears in the right half of the window
    /// gets repositioned so it opens leftward toward the center.
    /// Skips our own rootcord-tagged overlays.
    /// </summary>
    private void RepositionOverlayPopupsLeft(object overlay)
    {
        var windowBounds = _r.GetBounds(_mainWindow);
        double winW = windowBounds?.W ?? 1920;

        var children = _r.GetChildren(overlay);
        if (children == null) return;

        // Snapshot the list to avoid mutation issues during iteration
        var snapshot = new List<object>();
        foreach (var c in children) if (c != null) snapshot.Add(c);

        foreach (var child in snapshot)
        {
            // Skip our own overlays
            var tag = child.GetType().GetProperty("Tag")?.GetValue(child) as string ?? "";
            if (tag.StartsWith("rootcord-")) continue;

            var bounds = _r.GetBounds(child);
            if (bounds == null) continue;

            double x = bounds.Value.X;
            double popupW = bounds.Value.W;
            if (popupW <= 0) continue;

            // If the popup's right edge is beyond 60% of the window width, move it left
            if (x + popupW > winW * 0.6)
            {
                double newX = x - popupW - 8; // shift left by its own width + 8px gap
                if (newX < 8) newX = 8;
                _r.SetCanvasPosition(child, newX, bounds.Value.Y);
                Logger.Log(Tag, $"OverlayMonitor: repositioned popup from X={x:F0} → X={newX:F0}");
            }
        }
    }

    /// <summary>
    /// Set a ColumnDefinition's Width to a GridLength with the given value and unit type.
    /// unitType should be "Pixel", "Star", or "Auto".
    /// </summary>
    private void SetColumnDefinitionWidth(object? colDef, double value, string unitType)
    {
        if (colDef == null || _r.GridLengthType == null || _r.GridUnitTypeEnum == null) return;
        try
        {
            var unit = Enum.Parse(_r.GridUnitTypeEnum, unitType);
            var gridLength = Activator.CreateInstance(_r.GridLengthType, value, unit);
            colDef.GetType().GetProperty("Width")?.SetValue(colDef, gridLength);
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"SetColumnDefinitionWidth error: {ex.Message}");
        }
    }

    // ===== ToolTip placement flip (member hover name pills) =====

    /// <summary>
    /// Lazily discover ToolTip.GetTip / SetPlacement static methods via reflection.
    /// Caches result; safe to call multiple times.
    /// </summary>
    private bool EnsureToolTipMethods()
    {
        if (_toolTipGetTipMethod != null && _toolTipSetPlacementMethod != null && _tooltipLeftPlacement != null)
            return true;
        try
        {
            Type? toolTipType = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try { toolTipType = asm.GetType("Avalonia.Controls.ToolTip"); } catch { }
                if (toolTipType != null) break;
            }
            if (toolTipType == null) return false;

            _toolTipGetTipMethod = Array.Find(
                toolTipType.GetMethods(BindingFlags.Public | BindingFlags.Static),
                m => m.Name == "GetTip" && m.GetParameters().Length == 1);
            _toolTipSetPlacementMethod = Array.Find(
                toolTipType.GetMethods(BindingFlags.Public | BindingFlags.Static),
                m => m.Name == "SetPlacement" && m.GetParameters().Length == 2);

            if (_toolTipSetPlacementMethod == null) return false;

            var placementType = _toolTipSetPlacementMethod.GetParameters()[1].ParameterType;
            if (!placementType.IsEnum) return false;

            _tooltipLeftPlacement = Enum.Parse(placementType, "Left");
            return true;
        }
        catch { return false; }
    }

    /// <summary>
    /// Walk visual tree and flip ToolTip.Placement to Left on any control with a tooltip.
    /// Makes member name hover pills open to the left after the members panel is rotated right.
    /// </summary>
    private void FlipTooltipsInTree(object root)
    {
        if (!EnsureToolTipMethods()) return;
        var walker = new VisualTreeWalker(_r);
        foreach (var node in walker.DescendantsDepthFirst(root))
        {
            try
            {
                var tip = _toolTipGetTipMethod!.Invoke(null, new[] { node });
                if (tip != null)
                    _toolTipSetPlacementMethod!.Invoke(null, new[] { node, _tooltipLeftPlacement });
            }
            catch { }
        }
    }

    /// <summary>
    /// Revert ToolTip placements to Pointer (Avalonia default) for controls in the given tree.
    /// </summary>
    private void RevertTooltipsInTree(object root)
    {
        if (!EnsureToolTipMethods()) return;
        if (_toolTipSetPlacementMethod == null) return;
        var placementType = _toolTipSetPlacementMethod.GetParameters()[1].ParameterType;
        object pointerValue;
        try { pointerValue = Enum.Parse(placementType, "Pointer"); }
        catch { return; }

        var walker = new VisualTreeWalker(_r);
        foreach (var node in walker.DescendantsDepthFirst(root))
        {
            try
            {
                var tip = _toolTipGetTipMethod?.Invoke(null, new[] { node });
                if (tip != null)
                    _toolTipSetPlacementMethod.Invoke(null, new[] { node, pointerValue });
            }
            catch { }
        }
    }

    // ===== Server strip =====

    private void BuildAndInjectServerStrip()
    {
        if (_homeViewGrid == null) return;

        // Create the inner 2-row Grid: [Home+separator] [servers scroll]
        _stripGrid = _r.CreateGrid();
        if (_stripGrid == null)
        {
            Logger.Log(Tag, "Failed to create strip Grid");
            return;
        }
        _r.AddGridRowAuto(_stripGrid);   // Row 0: Home button + separator
        _r.AddGridRowStar(_stripGrid);   // Row 1: Scrollable server icons

        // --- Row 0: Home button + separator ---
        var selectedTab = _r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel");
        bool isDmSelected = selectedTab != null && IsDmTab(selectedTab);

        var topPanel = _r.CreateStackPanel(vertical: true, spacing: 0);
        if (topPanel != null)
        {
            _r.SetHorizontalAlignment(topPanel, "Center");
            _r.SetMargin(topPanel, 0, 8, 0, 2);

            _homeButton = BuildHomeButton(isDmSelected);
            if (_homeButton != null) _r.AddChild(topPanel, _homeButton);

            var separator = BuildSeparator();
            if (separator != null) _r.AddChild(topPanel, separator);

            _r.SetGridRow(topPanel, 0);
            _r.AddChild(_stripGrid, topPanel);
        }

        // --- Row 1: ScrollViewer with community server icons ---
        _serverStrip = _r.CreateStackPanel(vertical: true, spacing: IconSpacing);
        if (_serverStrip == null)
        {
            Logger.Log(Tag, "Failed to create server strip StackPanel");
            return;
        }
        _r.SetMargin(_serverStrip, 0, 4, 0, 4);
        _r.SetHorizontalAlignment(_serverStrip, "Center");

        PopulateServerStrip();

        // Delayed refresh: community bitmaps may not be loaded yet on first render.
        // Re-populate after 2s to pick up async-loaded images.
        var bitmapRefreshToken = _applyCts?.Token ?? System.Threading.CancellationToken.None;
        Task.Delay(2000, bitmapRefreshToken).ContinueWith(_ =>
        {
            if (bitmapRefreshToken.IsCancellationRequested) return;
            _r.RunOnUIThread(() =>
            {
                try { PopulateServerStrip(); Logger.Log(Tag, "Server strip: delayed bitmap refresh"); }
                catch { }
            });
        }, System.Threading.Tasks.TaskContinuationOptions.NotOnCanceled);

        var scrollViewer = _r.CreateScrollViewer(_serverStrip);
        if (scrollViewer != null)
        {
            scrollViewer.GetType().GetProperty("VerticalScrollBarVisibility")
                ?.SetValue(scrollViewer, Enum.Parse(
                    scrollViewer.GetType().GetProperty("VerticalScrollBarVisibility")!.PropertyType,
                    "Hidden"));
            scrollViewer.GetType().GetProperty("HorizontalScrollBarVisibility")
                ?.SetValue(scrollViewer, Enum.Parse(
                    scrollViewer.GetType().GetProperty("HorizontalScrollBarVisibility")!.PropertyType,
                    "Disabled"));
        }

        var scrollContent = scrollViewer ?? _serverStrip;
        _r.SetGridRow(scrollContent, 1);
        _r.AddChild(_stripGrid, scrollContent);

        // Wrap the 2-row Grid in a Border for background styling
        _serverStripBorder = _r.CreateBorder(_bg, 0, _stripGrid);
        if (_serverStripBorder == null)
        {
            Logger.Log(Tag, "Failed to create server strip border");
            return;
        }
        _r.SetTag(_serverStripBorder, "rootcord-strip");

        // Place in HomeView Grid: Column 0, spanning all rows
        _r.SetGridColumn(_serverStripBorder, 0);
        _r.SetGridRow(_serverStripBorder, 0);

        var rowDefs = _r.GetRowDefinitions(_homeViewGrid);
        int rowCount = rowDefs?.Count ?? 4;
        _r.SetGridRowSpan(_serverStripBorder, rowCount);

        _r.AddChild(_homeViewGrid, _serverStripBorder);
        Logger.Log(Tag, $"Server strip injected: Col=0, RowSpan={rowCount} (2-row layout: Home + servers)");
    }

    /// <summary>
    /// Build and inject a Discord-style user bar at the bottom-left of the HomeView Grid.
    /// Flush to left/bottom window edges (no floating dock look). Only top-right corner rounded.
    /// 5-column layout: [Auto avatar] [8px gap] [* username MinWidth=120] [8px gap] [Auto buttons].
    /// </summary>
    private void BuildAndInjectUserBar()
    {
        if (_homeViewGrid == null) return;

        try
        {
            string username = GetCurrentUsername() ?? "User";
            string initial = username.Length > 0 ? username[0].ToString().ToUpper() : "U";
            object? avatarBitmap = TryGetProfileBitmap();
            string onlineColor = "#43b581";
            string statusLabel = GetCurrentStatusLabel();

            // Docked card: flush to bottom-left, only top-right corner rounded (Discord-style)
            _userBar = _r.CreateBorder(_cardBg, 0);
            if (_userBar == null) return;
            _r.SetTag(_userBar, "rootcord-userbar");
            _r.SetCornerRadius(_userBar, 0, 12, 0, 0); // only top-right rounded
            _r.SetMinWidth(_userBar, 300);
            _r.SetMaxWidth(_userBar, 400);

            // Position in HomeView Grid: Col=0, spanning all rows, bottom-left, ZIndex=10
            _r.SetGridColumn(_userBar, 0);
            _r.SetGridRow(_userBar, 0);
            var rowDefs = _r.GetRowDefinitions(_homeViewGrid);
            int rowCount = rowDefs?.Count ?? 4;
            _r.SetGridRowSpan(_userBar, rowCount);
            var allCols = _r.GetColumnDefinitions(_homeViewGrid)?.Count ?? 6;
            _r.SetGridColumnSpan(_userBar, allCols);
            _r.SetVerticalAlignment(_userBar, "Bottom");
            _r.SetHorizontalAlignment(_userBar, "Left");
            _r.SetMargin(_userBar, 0, 0, 0, 0); // flush to window edges
            _userBar.GetType().GetProperty("ZIndex")?.SetValue(_userBar, 10);

            // 5-column content Grid: [Auto avatar] [8px] [* username] [8px] [Auto buttons]
            var contentGrid = _r.CreateGrid();
            if (contentGrid == null) return;
            _r.SetMargin(contentGrid, 10, 8, 10, 8); // inner padding

            // Add 5 columns: Auto, 8px, Star, 8px, Auto
            _r.AddGridColumn(contentGrid, 1.0); // Col 0: Auto (avatar)
            _r.AddGridColumn(contentGrid, 1.0); // Col 1: 8px gap
            _r.AddGridColumn(contentGrid, 1.0); // Col 2: Star (username)
            _r.AddGridColumn(contentGrid, 1.0); // Col 3: 8px gap
            _r.AddGridColumn(contentGrid, 1.0); // Col 4: Auto (buttons)

            var colDefs = _r.GetColumnDefinitions(contentGrid);
            if (colDefs != null && colDefs.Count >= 5 && _r.GridUnitTypeEnum != null && _r.GridLengthType != null)
            {
                try
                {
                    var autoUnit = Enum.Parse(_r.GridUnitTypeEnum, "Auto");
                    var pixelUnit = Enum.Parse(_r.GridUnitTypeEnum, "Pixel");
                    var autoLen = Activator.CreateInstance(_r.GridLengthType, 0d, autoUnit)!;
                    var gap8Len = Activator.CreateInstance(_r.GridLengthType, 8d, pixelUnit)!;

                    colDefs[0]?.GetType().GetProperty("Width")?.SetValue(colDefs[0], autoLen);  // Col 0: Auto
                    colDefs[1]?.GetType().GetProperty("Width")?.SetValue(colDefs[1], gap8Len);  // Col 1: 8px
                    // Col 2: Star (default from AddGridColumn) — leave as-is
                    colDefs[3]?.GetType().GetProperty("Width")?.SetValue(colDefs[3], gap8Len);  // Col 3: 8px
                    colDefs[4]?.GetType().GetProperty("Width")?.SetValue(colDefs[4], autoLen);  // Col 4: Auto
                }
                catch (Exception ex) { Logger.Log(Tag, $"UserBar: column setup error: {ex.Message}"); }
            }

            // --- Col 0: Avatar circle + online dot ---
            var avatarGrid = _r.CreateGrid();
            if (avatarGrid != null)
            {
                _r.SetWidth(avatarGrid, 32);
                _r.SetHeight(avatarGrid, 32);
                _r.SetHorizontalAlignment(avatarGrid, "Center");
                _r.SetVerticalAlignment(avatarGrid, "Center");
                _r.SetGridColumn(avatarGrid, 0);

                var avBorder = _r.CreateBorder(_bg, 16);
                if (avBorder != null)
                {
                    _r.SetWidth(avBorder, 32);
                    _r.SetHeight(avBorder, 32);
                    _r.SetClipToBounds(avBorder, true);
                    if (avatarBitmap != null)
                    {
                        var img = _r.CreateImage("UniformToFill");
                        if (img != null)
                        {
                            _r.SetImageSource(img, avatarBitmap);
                            _r.SetWidth(img, 32);
                            _r.SetHeight(img, 32);
                            _r.SetBorderChild(avBorder, img);
                        }
                        else
                            SetAvatarInitial(avBorder, initial, _text);
                    }
                    else
                        SetAvatarInitial(avBorder, initial, _text);
                    _r.AddChild(avatarGrid, avBorder);
                }

                var statusDot = _r.CreateBorder(onlineColor, 5);
                if (statusDot != null)
                {
                    _r.SetWidth(statusDot, 10);
                    _r.SetHeight(statusDot, 10);
                    _r.SetHorizontalAlignment(statusDot, "Right");
                    _r.SetVerticalAlignment(statusDot, "Bottom");
                    _r.AddChild(avatarGrid, statusDot);
                }
                // Clicking the avatar opens the profile pane
                _r.SetCursorHand(avatarGrid);
                _r.SubscribeEvent(avatarGrid, "PointerPressed", () => InvokeHomeCommand("ProfilePaneToggleCommand"));
                _r.AddChild(contentGrid, avatarGrid);
            }

            // --- Col 2: Username + status label (star column, MinWidth=120) ---
            var textPanel = _r.CreateStackPanel(vertical: true, spacing: 0);
            if (textPanel != null)
            {
                _r.SetGridColumn(textPanel, 2);
                _r.SetVerticalAlignment(textPanel, "Center");
                _r.SetMinWidth(textPanel, 120);

                var nameText = _r.CreateTextBlock(username, 13, _text);
                if (nameText != null)
                {
                    _r.SetFontWeight(nameText, "Bold");
                    try
                    {
                        nameText.GetType().GetProperty("TextWrapping")?.SetValue(nameText,
                            Enum.Parse(nameText.GetType().GetProperty("TextWrapping")!.PropertyType, "NoWrap"));
                        nameText.GetType().GetProperty("TextTrimming")?.SetValue(nameText,
                            Enum.Parse(nameText.GetType().GetProperty("TextTrimming")!.PropertyType, "CharacterEllipsis"));
                    }
                    catch { }

                    // ToolTip: show full username on hover (in case it's ellipsized)
                    try
                    {
                        EnsureToolTipMethods();
                        var setTipMethod = _toolTipGetTipMethod?.DeclaringType?.GetMethod("SetTip",
                            BindingFlags.Public | BindingFlags.Static);
                        if (setTipMethod != null)
                            setTipMethod.Invoke(null, new[] { nameText, (object)username });
                    }
                    catch { }

                    _r.AddChild(textPanel, nameText);
                }

                var statusText = _r.CreateTextBlock(statusLabel, 11, _muted);
                if (statusText != null)
                {
                    try
                    {
                        statusText.GetType().GetProperty("TextWrapping")?.SetValue(statusText,
                            Enum.Parse(statusText.GetType().GetProperty("TextWrapping")!.PropertyType, "NoWrap"));
                    }
                    catch { }
                    _r.AddChild(textPanel, statusText);
                }
                // Clicking the text area also opens the profile pane
                _r.SetCursorHand(textPanel);
                _r.SubscribeEvent(textPanel, "PointerPressed", () => InvokeHomeCommand("ProfilePaneToggleCommand"));
                _r.AddChild(contentGrid, textPanel);
            }

            // --- Col 4: 4 action buttons (People / DMs / Notifications / Settings) ---
            var trayHost = _r.CreateStackPanel(vertical: false, spacing: 2);
            if (trayHost != null)
            {
                _r.SetGridColumn(trayHost, 4);
                _r.SetVerticalAlignment(trayHost, "Center");

                var btnP = BuildActionButton(GlyphFriends,       () => InvokeHomeCommand("FriendsPaneToggleCommand"));
                var btnD = BuildActionButton(GlyphMessages,      () => InvokeHomeCommand("DirectMessagesPaneToggleCommand"));
                var btnN = BuildActionButton(GlyphNotifications, () => InvokeHomeCommand("NotificationsPaneToggleCommand"));
                var btnS = BuildActionButton(GlyphSettings,      () => OpenSettingsDirectly());
                if (btnP != null) _r.AddChild(trayHost, btnP);
                if (btnD != null) _r.AddChild(trayHost, btnD);
                if (btnN != null) _r.AddChild(trayHost, btnN);
                if (btnS != null) _r.AddChild(trayHost, btnS);

                _r.AddChild(contentGrid, trayHost);
            }

            _r.SetBorderChild(_userBar, contentGrid);

            _r.AddChild(_homeViewGrid, _userBar);
            Logger.Log(Tag, "User bar injected: flush docked card, 5-col layout, MinWidth=300");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"BuildAndInjectUserBar error: {ex.Message}");
        }
    }

    /// <summary>
    /// Invoke a named command on HomeViewModel (for fallback user bar buttons).
    /// </summary>
    private void InvokeHomeCommand(string commandName)
    {
        if (_homeViewModel == null) return;
        try
        {
            var cmd = _r.GetPropertyValue(_homeViewModel, commandName);
            if (cmd != null)
            {
                InvokeCommand(cmd);
                Logger.Log(Tag, $"InvokeHomeCommand: '{commandName}' executed");
            }
            else Logger.Log(Tag, $"InvokeHomeCommand: '{commandName}' not found");

            // After opening any pane, schedule Sign Out button rearrangement
            // (ProfileView may now be in the visual tree)
            var token = _applyCts?.Token ?? System.Threading.CancellationToken.None;
            Task.Delay(500, token).ContinueWith(_ =>
            {
                if (token.IsCancellationRequested) return;
                _r.RunOnUIThread(() =>
                {
                    try
                    {
                        Logger.Log(Tag, "RearrangeSignOut: triggered from InvokeHomeCommand");
                        RearrangeSignOutButton();
                    }
                    catch (Exception ex) { Logger.Log(Tag, $"RearrangeSignOut deferred error: {ex.Message}"); }
                });
            }, System.Threading.Tasks.TaskContinuationOptions.NotOnCanceled);
        }
        catch (Exception ex) { Logger.Log(Tag, $"InvokeHomeCommand '{commandName}' error: {ex.Message}"); }
    }

    /// <summary>
    /// Open Root's settings directly: opens the profile pane then programmatically clicks the
    /// "Settings" button within it, replicating the native user-bar → Settings flow.
    /// </summary>
    private void OpenSettingsDirectly()
    {
        // Open the profile pane first
        InvokeHomeCommand("ProfilePaneToggleCommand");

        // After the pane has had time to open, find and invoke the Settings button inside it
        var openSettingsToken = _applyCts?.Token ?? System.Threading.CancellationToken.None;
        Task.Delay(350, openSettingsToken).ContinueWith(_ => _r.RunOnUIThread(() =>
        {
            try
            {
                if (_rootSplitView == null || openSettingsToken.IsCancellationRequested) return;
                var walker = new VisualTreeWalker(_r);
                foreach (var node in walker.DescendantsDepthFirst(_rootSplitView))
                {
                    // Read text content from common properties
                    string? text = null;
                    foreach (var prop in new[] { "Text", "Content", "Header" })
                    {
                        try { text = _r.GetPropertyValue(node, prop) as string; } catch { }
                        if (!string.IsNullOrEmpty(text)) break;
                    }
                    if (!string.Equals(text?.Trim(), "Settings", StringComparison.OrdinalIgnoreCase))
                        continue;

                    // Found a node labelled "Settings" — try to invoke its Command
                    var cmd = _r.GetPropertyValue(node, "Command");
                    if (cmd == null)
                    {
                        // Button's command may live on its DataContext
                        var dc = _r.GetPropertyValue(node, "DataContext");
                        if (dc != null)
                            cmd = _r.GetPropertyValue(dc, "Command") ?? _r.GetPropertyValue(dc, "OpenCommand");
                    }
                    if (cmd != null)
                    {
                        InvokeCommand(cmd);
                        Logger.Log(Tag, $"OpenSettingsDirectly: invoked via {node.GetType().Name}.Command");
                        return;
                    }
                    Logger.Log(Tag, $"OpenSettingsDirectly: found '{node.GetType().Name}' text=Settings but no command");
                }
                Logger.Log(Tag, "OpenSettingsDirectly: Settings button not found in pane visual tree");
            }
            catch (Exception ex) { Logger.Log(Tag, $"OpenSettingsDirectly error: {ex.Message}"); }
        }), System.Threading.Tasks.TaskContinuationOptions.NotOnCanceled);
    }

    /// <summary>
    /// Build an icon button for the user bar tray using Segoe icon font glyphs.
    /// Falls back to a Unicode character if the icon font is unavailable.
    /// </summary>
    private object? BuildActionButton(string glyph, Action onClick)
    {
        var btn = _r.CreateBorder("#00000000", 6);
        if (btn == null) return null;
        _r.SetWidth(btn, 30);
        _r.SetHeight(btn, 30);
        _r.SetCursorHand(btn);
        var icon = _r.CreateTextBlock(glyph, 16, _muted);
        if (icon != null)
        {
            _r.SetHorizontalAlignment(icon, "Center");
            _r.SetVerticalAlignment(icon, "Center");
            // Apply icon font chain for proper glyph rendering on Windows
            try
            {
                var ffProp = icon.GetType().GetProperty("FontFamily");
                if (ffProp != null)
                {
                    var ff = Activator.CreateInstance(ffProp.PropertyType, GlyphIconFonts);
                    if (ff != null) ffProp.SetValue(icon, ff);
                }
            }
            catch { /* use system default font — glyph renders as Unicode fallback */ }
            _r.SetBorderChild(btn, icon);
        }
        var btnRef = btn;
        var iconRef = icon;
        var hoverBg = AdjustForHighlight(_cardBg, 14);
        _r.SubscribeEvent(btn, "PointerPressed", onClick);
        _r.SubscribeEvent(btn, "PointerEntered", () =>
        {
            _r.SetBackground(btnRef, hoverBg);
            if (iconRef != null) _r.SetForeground(iconRef, _text);
        });
        _r.SubscribeEvent(btn, "PointerExited", () =>
        {
            _r.SetBackground(btnRef, "#00000000");
            if (iconRef != null) _r.SetForeground(iconRef, _muted);
        });
        return btn;
    }

    /// <summary>
    /// Remove the user bar overlay from the HomeView Grid.
    /// Reparents SystemTray back to its original location if it was moved.
    /// </summary>
    private void RevertUserBar()
    {
        if (_userBar != null && _homeViewGrid != null)
        {
            try { _r.RemoveChild(_homeViewGrid, _userBar); } catch { }
            Logger.Log(Tag, "User bar removed");
        }
        _userBar = null;
    }

    /// <summary>
    /// Populate the server strip with icons for each tab in HomeViewModel.Tabs.
    /// </summary>
    private void PopulateServerStrip()
    {
        if (_serverStrip == null || _homeViewModel == null) return;

        // Clear existing children
        var children = _r.GetChildren(_serverStrip);
        if (children != null)
        {
            var toRemove = new List<object>();
            foreach (var c in children)
                if (c != null) toRemove.Add(c);
            foreach (var c in toRemove)
                _r.RemoveChild(_serverStrip, c);
        }

        // Read tabs from HomeViewModel
        var tabs = _r.GetPropertyValue(_homeViewModel, "Tabs");
        if (tabs == null)
        {
            Logger.Log(Tag, "HomeViewModel.Tabs is null");
            return;
        }

        var selectedTab = _r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel");
        int tabIndex = 0;

        if (tabs is IEnumerable tabsEnum)
        {
            foreach (var tabVm in tabsEnum)
            {
                if (tabVm == null) continue;
                if (IsDmTab(tabVm)) continue; // DMs handled by Home button
                var icon = BuildServerIcon(tabVm, tabVm == selectedTab);
                if (icon != null)
                {
                    _r.AddChild(_serverStrip, icon);
                    tabIndex++;
                }
            }
        }

        Logger.Log(Tag, $"Server strip populated: {tabIndex} community tab(s) (DMs filtered)");
    }

    /// <summary>
    /// Resolve a display name from a tab ViewModel.
    /// CommunityTabViewModel exposes Community.Name (nested).
    /// DirectMessageTabViewModel exposes DirectMessageName.
    /// </summary>
    private string GetTabDisplayName(object tabViewModel)
    {
        // DirectMessageTabViewModel: DirectMessageName
        var dmName = _r.GetPropertyValue(tabViewModel, "DirectMessageName") as string;
        if (!string.IsNullOrEmpty(dmName)) return dmName;

        // CommunityTabViewModel: Community.Name (nested property)
        var community = _r.GetPropertyValue(tabViewModel, "Community");
        if (community != null)
        {
            var communityName = _r.GetPropertyValue(community, "Name") as string;
            if (!string.IsNullOrEmpty(communityName)) return communityName;
        }

        // Fallback: try common property names
        var fallback = _r.GetPropertyValue(tabViewModel, "DisplayName") as string
            ?? _r.GetPropertyValue(tabViewModel, "Name") as string
            ?? _r.GetPropertyValue(tabViewModel, "Title") as string;
        if (!string.IsNullOrEmpty(fallback)) return fallback;

        // Last resort: try Tab.ContainerId or type name
        var tab = _r.GetPropertyValue(tabViewModel, "Tab");
        if (tab != null)
        {
            var containerId = _r.GetPropertyValue(tab, "ContainerId") as string;
            if (!string.IsNullOrEmpty(containerId)) return containerId;
        }

        Logger.Log(Tag, $"Could not resolve display name for {tabViewModel.GetType().Name}");
        return "?";
    }

    /// <summary>
    /// Check if a tab ViewModel represents a DM tab.
    /// </summary>
    private bool IsDmTab(object? tabViewModel)
    {
        return tabViewModel != null && tabViewModel.GetType().Name.Contains("DirectMessage");
    }

    /// <summary>
    /// Try to extract an already-loaded Avalonia Bitmap from the tab ViewModel.
    /// CommunityTabViewModel exposes CommunityPictureAsyncBitmapWrapper (Task&lt;BitmapWrapper?&gt;).
    /// If the task is completed, we extract BitmapWrapper.Bitmap for the icon.
    /// </summary>
    private object? TryGetTabBitmap(object tabViewModel)
    {
        try
        {
            // CommunityTabViewModel: CommunityPictureAsyncBitmapWrapper -> Task<BitmapWrapper?>
            var bitmapTask = _r.GetPropertyValue(tabViewModel, "CommunityPictureAsyncBitmapWrapper");
            if (bitmapTask == null) return null;

            // Check if the Task has completed
            var isCompleted = _r.GetPropertyValue(bitmapTask, "IsCompleted");
            if (isCompleted is not true) return null;

            // Get the Result (BitmapWrapper?)
            var bitmapWrapper = _r.GetPropertyValue(bitmapTask, "Result");
            if (bitmapWrapper == null) return null;

            // BitmapWrapper has a Bitmap property (Avalonia.Media.Imaging.Bitmap)
            var bitmap = _r.GetPropertyValue(bitmapWrapper, "Bitmap")
                ?? _r.GetPropertyValue(bitmapWrapper, "Value")
                ?? _r.GetPropertyValue(bitmapWrapper, "Source");

            if (bitmap != null)
                Logger.Log(Tag, $"  Got bitmap from {tabViewModel.GetType().Name}: {bitmap.GetType().Name}");
            return bitmap;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"  TryGetTabBitmap error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Check if a community tab has unread messages or mention notifications.
    /// Uses confirmed ILSpy property chains from CommunityTabViewModel:
    ///   - Tab.Notifications.ContainerUnviewedNotificationCount (int) for mentions/notifications
    ///   - HasAnyActivity (bool) for unread channel activity without explicit notifications
    /// Returns (hasUnread, mentionCount).
    /// </summary>
    private (bool hasUnread, int mentions) GetTabUnreadState(object tabViewModel)
    {
        if (tabViewModel == null || IsDmTab(tabViewModel)) return (false, 0);
        try
        {
            int notifCount = 0;
            bool hasActivity = false;

            // CommunityTabViewModel.Tab.Notifications.ContainerUnviewedNotificationCount
            var tab = _r.GetPropertyValue(tabViewModel, "Tab");
            if (tab != null)
            {
                var notifications = _r.GetPropertyValue(tab, "Notifications");
                if (notifications != null)
                {
                    var countProp = _r.GetPropertyValue(notifications, "ContainerUnviewedNotificationCount");
                    if (countProp is int count) notifCount = count;
                }
            }

            // CommunityTabViewModel.HasAnyActivity: true when any channel has unread messages
            var activityProp = _r.GetPropertyValue(tabViewModel, "HasAnyActivity");
            if (activityProp is bool b) hasActivity = b;

            return (notifCount > 0 || hasActivity, notifCount);
        }
        catch { return (false, 0); }
    }

    /// <summary>
    /// Build a single circular server icon for a tab ViewModel.
    /// Uses the community's picture bitmap if available, falls back to initial letter.
    /// </summary>
    private object? BuildServerIcon(object tabViewModel, bool isSelected)
    {
        var displayName = GetTabDisplayName(tabViewModel);
        var isDm = IsDmTab(tabViewModel);

        // Try to get the community's image bitmap
        var bitmap = TryGetTabBitmap(tabViewModel);

        // Get the first letter for the icon (fallback when no image)
        var initial = isDm ? "\u2709" : (displayName.Length > 0 ? displayName[0].ToString().ToUpper() : "?");
        Logger.Log(Tag, $"  Icon: {displayName} -> '{initial}' (DM={isDm}, selected={isSelected}, hasImage={bitmap != null})");

        // Icon colors from cached ContentPages statics
        var bgColor = _cardBg;
        var bgHover = AdjustForHighlight(_cardBg, 10);

        // Outer container: holds the pill indicator + icon
        var container = _r.CreateStackPanel(vertical: false, spacing: 0);
        if (container == null) return null;
        _r.SetHorizontalAlignment(container, "Center");
        _r.SetTag(container, $"rootcord-icon-{displayName}");

        // Selection pill (Discord-style left indicator)
        var pillBorder = _r.CreateBorder(isSelected ? _accent : "#00000000", 2);
        if (pillBorder != null)
        {
            _r.SetWidth(pillBorder, PillWidth);
            _r.SetHeight(pillBorder, isSelected ? PillHeight : 8);
            _r.SetVerticalAlignment(pillBorder, "Center");
            _r.SetMargin(pillBorder, 0, 0, 3, 0);
            _r.AddChild(container, pillBorder);
        }

        // Circular icon border (ClipToBounds clips image to circle)
        var iconBorder = _r.CreateBorder(isSelected ? _accent : bgColor, IconCornerRadius);
        if (iconBorder == null) return container;

        _r.SetWidth(iconBorder, IconSize);
        _r.SetHeight(iconBorder, IconSize);
        _r.SetCursorHand(iconBorder);
        _r.SetClipToBounds(iconBorder, true);
        _r.SetBorderThickness(iconBorder, 0); // prevent square stroke outline

        // Content: community image if available, otherwise initial letter
        if (bitmap != null)
        {
            var img = _r.CreateImage("UniformToFill");
            if (img != null)
            {
                _r.SetImageSource(img, bitmap);
                _r.SetWidth(img, IconSize);
                _r.SetHeight(img, IconSize);
                _r.SetBorderChild(iconBorder, img);
            }
            else
            {
                // Fallback to letter if image creation fails
                SetIconToLetter(iconBorder, initial, isSelected, _text);
            }
        }
        else
        {
            SetIconToLetter(iconBorder, initial, isSelected, _text);
        }

        // Click handler: switch tab
        var capturedVm = tabViewModel;
        var capturedName = displayName;
        var iconRef = iconBorder;
        _r.SubscribeEvent(iconBorder, "PointerPressed", () =>
        {
            OnServerIconClicked(capturedVm);
        });

        // Hover: highlight + show tooltip
        _r.SubscribeEvent(iconBorder, "PointerEntered", () =>
        {
            if (_r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel") != capturedVm)
                _r.SetBackground(iconRef, bgHover);
            ShowIconTooltip(iconRef, capturedName, capturedVm);
        });
        _r.SubscribeEvent(iconBorder, "PointerExited", () =>
        {
            if (_r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel") != capturedVm)
                _r.SetBackground(iconRef, bgColor);
            DismissIconTooltip();
        });

        // Unread badge: wrap icon in a Grid so badge can overlay top-right corner
        var (hasUnread, mentionCount) = GetTabUnreadState(tabViewModel);
        if (hasUnread && !isSelected)
        {
            var iconWrapper = _r.CreateGrid();
            if (iconWrapper != null)
            {
                _r.SetWidth(iconWrapper, IconSize);
                _r.SetHeight(iconWrapper, IconSize);
                _r.AddChild(iconWrapper, iconBorder);

                // Red dot for mentions, yellow-orange for unread only
                string badgeColor = mentionCount > 0 ? "#e74c3c" : "#f0b429";
                var badge = _r.CreateBorder(badgeColor, 4);
                if (badge != null)
                {
                    _r.SetWidth(badge, 8);
                    _r.SetHeight(badge, 8);
                    _r.SetHorizontalAlignment(badge, "Right");
                    _r.SetVerticalAlignment(badge, "Top");
                    _r.SetMargin(badge, 0, 0, -1, 0);
                    _r.AddChild(iconWrapper, badge);
                }
                _r.AddChild(container, iconWrapper);
                return container;
            }
        }

        _r.AddChild(container, iconBorder);
        return container;
    }

    /// <summary>
    /// Build the Home button icon at the top of the strip.
    /// Clicking it selects the first DM tab. Shows active pill when a DM is selected.
    /// </summary>
    private object? BuildHomeButton(bool isDmSelected)
    {
        var bgColor = _cardBg;
        var bgHover = AdjustForHighlight(_cardBg, 10);

        // Outer container: pill + icon (same pattern as BuildServerIcon)
        var container = _r.CreateStackPanel(vertical: false, spacing: 0);
        if (container == null) return null;
        _r.SetHorizontalAlignment(container, "Center");
        _r.SetTag(container, "rootcord-home");

        // Selection pill
        var pillBorder = _r.CreateBorder(isDmSelected ? _accent : "#00000000", 2);
        if (pillBorder != null)
        {
            _r.SetWidth(pillBorder, PillWidth);
            _r.SetHeight(pillBorder, isDmSelected ? PillHeight : 8);
            _r.SetVerticalAlignment(pillBorder, "Center");
            _r.SetMargin(pillBorder, 0, 0, 3, 0);
            _r.AddChild(container, pillBorder);
        }

        // Circular icon
        var iconBorder = _r.CreateBorder(isDmSelected ? _accent : bgColor, IconCornerRadius);
        if (iconBorder == null) return container;
        _r.SetWidth(iconBorder, IconSize);
        _r.SetHeight(iconBorder, IconSize);
        _r.SetCursorHand(iconBorder);
        _r.SetClipToBounds(iconBorder, true);

        // Home symbol
        var text = _r.CreateTextBlock("\u2302", 20, isDmSelected ? "#FFFFFF" : _text);
        _r.SetFontWeight(text, "Bold");
        _r.SetHorizontalAlignment(text, "Center");
        _r.SetVerticalAlignment(text, "Center");
        _r.SetBorderChild(iconBorder, text);

        // Click: select first DM tab
        var iconRef = iconBorder;
        _r.SubscribeEvent(iconBorder, "PointerPressed", () =>
        {
            OnHomeButtonClicked();
        });

        // Hover
        _r.SubscribeEvent(iconBorder, "PointerEntered", () =>
        {
            var sel = _r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel");
            if (sel == null || !IsDmTab(sel))
                _r.SetBackground(iconRef, bgHover);
            ShowIconTooltip(iconRef, "Explore Servers", null);
        });
        _r.SubscribeEvent(iconBorder, "PointerExited", () =>
        {
            var sel = _r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel");
            if (sel == null || !IsDmTab(sel))
                _r.SetBackground(iconRef, bgColor);
            DismissIconTooltip();
        });

        _r.AddChild(container, iconBorder);
        return container;
    }

    /// <summary>
    /// Open the native profile pane (Settings / Support / Sign out).
    /// Used by user bar click — skips our custom popup entirely.
    /// </summary>
    private void OpenProfilePane()
    {
        if (_homeViewModel == null) return;
        try
        {
            var cmd = _r.GetPropertyValue(_homeViewModel, "ProfilePaneToggleCommand");
            if (cmd != null)
            {
                InvokeCommand(cmd);
                Logger.Log(Tag, "OpenProfilePane: ProfilePaneToggleCommand invoked");
                return;
            }
            // Fallback: set properties directly
            _r.SetPropertyValue(_homeViewModel, "PaneOpen", true);
            _r.SetPropertyValue(_homeViewModel, "ProfileOpen", true);
            Logger.Log(Tag, "OpenProfilePane: set PaneOpen+ProfileOpen directly");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"OpenProfilePane error: {ex.Message}");
        }
    }

    /// <summary>
    /// Handle Home button click: select the first DM tab directly (replaces community view).
    /// Falls back to DirectMessagesPaneToggleCommand if no DM tab exists.
    /// </summary>
    private void OnHomeButtonClicked()
    {
        if (_homeViewModel == null) return;
        try
        {
            // Open the server discovery / new tab page (the "+" button in Root's native tab bar).
            // HomeViewModel.CreateNewTabCommand creates or selects the NewTab page.
            var cmd = _r.GetPropertyValue(_homeViewModel, "CreateNewTabCommand");
            if (cmd != null)
            {
                InvokeCommand(cmd);
                Logger.Log(Tag, "Home: opened server discovery (CreateNewTabCommand)");
                RefreshSelectedHighlight();
                return;
            }

            // Fallback: find and select an existing NewTab in the tabs collection
            var tabs = _r.GetPropertyValue(_homeViewModel, "Tabs") as IEnumerable;
            if (tabs != null)
            {
                foreach (var tab in tabs)
                {
                    if (tab != null && tab.GetType().Name.Contains("NewTab"))
                    {
                        _r.SetPropertyValue(_homeViewModel, "SelectedTabViewModel", tab);
                        Logger.Log(Tag, "Home: selected existing NewTab");
                        RefreshSelectedHighlight();
                        return;
                    }
                }
            }

            Logger.Log(Tag, "Home: no CreateNewTabCommand or NewTab found");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"OnHomeButtonClicked error: {ex.Message}");
        }
    }

    /// <summary>
    /// Invoke an ICommand/IRelayCommand.Execute(null).
    /// </summary>
    private static void InvokeCommand(object command)
    {
        var executeMethod = command.GetType().GetMethod("Execute", new[] { typeof(object) });
        executeMethod?.Invoke(command, new object?[] { null });
    }

    /// <summary>
    /// Build a thin horizontal separator line between Home button and server icons.
    /// </summary>
    private object? BuildSeparator()
    {
        var sepColor = AdjustForHighlight(_cardBg, 15);
        var sep = _r.CreateBorder(sepColor, 1);
        if (sep == null) return null;
        _r.SetWidth(sep, 32);
        _r.SetHeight(sep, 2);
        _r.SetHorizontalAlignment(sep, "Center");
        _r.SetMargin(sep, 0, 6, 0, 6);
        return sep;
    }

    // ===== User card popup =====

    private object? _userCardPopupOverlay;
    private object? _userCardPopupPanel;
    private object? _userCardBackdrop;

    /// <summary>
    /// Show a popup card expanding upward from the user card (like Discord).
    /// Contains avatar, username, status, Settings, Sign out.
    /// Styled to match Root's native card pattern.
    /// </summary>
    private void ShowUserCardPopup(object anchorControl)
    {
        // If already showing, dismiss
        if (_userCardPopupPanel != null)
        {
            DismissUserCardPopup();
            return;
        }

        var mainWindow = _r.GetMainWindow();
        if (mainWindow == null) return;

        var overlay = _r.GetOverlayLayer(mainWindow);
        if (overlay == null) return;
        _userCardPopupOverlay = overlay;

        // Position above the user card
        var anchorBounds = _r.GetBounds(anchorControl);
        var translated = _r.TranslatePoint(anchorControl, 0, 0, overlay);
        if (anchorBounds == null || translated == null) return;

        double anchorX = translated.Value.X;
        double anchorY = translated.Value.Y;

        var onlineColor = "#43b581";

        string username = GetCurrentUsername() ?? "User";
        object? avatarBitmap = TryGetProfileBitmap();
        string initial = username.Length > 0 ? username[0].ToString().ToUpper() : "U";

        // Popup container — native card: CardBg, 12 radius, subtle border
        _userCardPopupPanel = _r.CreateBorder(_cardBg, 12);
        if (_userCardPopupPanel == null) return;
        _r.SetTag(_userCardPopupPanel, "rootcord-usercard-popup");
        _r.SetWidth(_userCardPopupPanel, 220);
        SetBorderStroke(_userCardPopupPanel, AdjustForHighlight(_cardBg, 15), 1.5);

        var content = _r.CreateStackPanel(vertical: true, spacing: 0);
        if (content == null) return;

        // Avatar + name section
        var headerPanel = _r.CreateStackPanel(vertical: true, spacing: 6);
        if (headerPanel != null)
        {
            _r.SetMargin(headerPanel, 14, 14, 14, 10);
            _r.SetHorizontalAlignment(headerPanel, "Center");

            // Large avatar (48x48)
            var avatarGrid = _r.CreateGrid();
            if (avatarGrid != null)
            {
                _r.SetWidth(avatarGrid, 48);
                _r.SetHeight(avatarGrid, 48);
                _r.SetHorizontalAlignment(avatarGrid, "Center");

                var avBorder = _r.CreateBorder(_bg, 24);
                if (avBorder != null)
                {
                    _r.SetWidth(avBorder, 48);
                    _r.SetHeight(avBorder, 48);
                    _r.SetClipToBounds(avBorder, true);
                    if (avatarBitmap != null)
                    {
                        var img = _r.CreateImage("UniformToFill");
                        if (img != null) { _r.SetImageSource(img, avatarBitmap); _r.SetWidth(img, 48); _r.SetHeight(img, 48); _r.SetBorderChild(avBorder, img); }
                        else SetAvatarInitial(avBorder, initial, _text);
                    }
                    else SetAvatarInitial(avBorder, initial, _text);
                    _r.AddChild(avatarGrid, avBorder);
                }
                var dot = _r.CreateBorder(onlineColor, 7);
                if (dot != null) { _r.SetWidth(dot, 14); _r.SetHeight(dot, 14); _r.SetHorizontalAlignment(dot, "Right"); _r.SetVerticalAlignment(dot, "Bottom"); _r.AddChild(avatarGrid, dot); }
                _r.AddChild(headerPanel, avatarGrid);
            }

            // Username
            var nameBlock = _r.CreateTextBlock(username, 14, _text);
            if (nameBlock != null) { _r.SetFontWeight(nameBlock, "SemiBold"); _r.SetHorizontalAlignment(nameBlock, "Center"); _r.AddChild(headerPanel, nameBlock); }

            // Status selector — clickable, shows current status with dropdown
            var currentStatus = GetCurrentStatusLabel();
            var statusColor = GetStatusColor(currentStatus);
            var statusBtnBg = AdjustForHighlight(_cardBg, 5);
            var statusBtn = _r.CreateBorder(statusBtnBg, 10);
            if (statusBtn != null)
            {
                _r.SetHorizontalAlignment(statusBtn, "Center");
                _r.SetCursorHand(statusBtn);

                var statusLayout = _r.CreateStackPanel(vertical: false, spacing: 4);
                if (statusLayout != null)
                {
                    _r.SetMargin(statusLayout, 10, 4, 10, 4);
                    _r.SetVerticalAlignment(statusLayout, "Center");

                    var statusDotText = _r.CreateTextBlock("\u25CF", 10, statusColor);
                    if (statusDotText != null) { _r.SetVerticalAlignment(statusDotText, "Center"); _r.AddChild(statusLayout, statusDotText); }

                    var statusLabel = _r.CreateTextBlock(currentStatus, 11, _muted);
                    if (statusLabel != null) { _r.SetVerticalAlignment(statusLabel, "Center"); _r.AddChild(statusLayout, statusLabel); }

                    var chevron = _r.CreateTextBlock("\u25BE", 10, _muted);
                    if (chevron != null) { _r.SetVerticalAlignment(chevron, "Center"); _r.AddChild(statusLayout, chevron); }

                    _r.SetBorderChild(statusBtn, statusLayout);
                }

                var statusBtnHover = AdjustForHighlight(_cardBg, 10);
                var statusBtnNormal = statusBtnBg;
                var statusBtnRef = statusBtn;
                _r.SubscribeEvent(statusBtn, "PointerEntered", () => _r.SetBackground(statusBtnRef, statusBtnHover));
                _r.SubscribeEvent(statusBtn, "PointerExited", () => _r.SetBackground(statusBtnRef, statusBtnNormal));
                _r.SubscribeEvent(statusBtn, "PointerPressed", () =>
                {
                    ShowStatusSelector(statusBtnRef, content);
                });
                _r.AddChild(headerPanel, statusBtn);
            }

            _r.AddChild(content, headerPanel);
        }

        // Divider
        var dividerColor = AdjustForHighlight(_cardBg, 15);
        var divider = _r.CreateBorder(dividerColor, 0);
        if (divider != null) { _r.SetHeight(divider, 1); _r.SetMargin(divider, 8, 2, 8, 2); _r.AddChild(content, divider); }

        // Settings button — opens Root's settings page directly
        var settingsBtn = BuildPopupButton("\u2699  Settings", _text, _cardBg);
        if (settingsBtn != null)
        {
            _r.SubscribeEvent(settingsBtn, "PointerPressed", () =>
            {
                DismissUserCardPopup();
                var cmd = _r.GetPropertyValue(_homeViewModel, "SettingsPaneToggleCommand")
                    ?? _r.GetPropertyValue(_homeViewModel, "SettingsCommand")
                    ?? _r.GetPropertyValue(_homeViewModel, "OpenSettingsCommand");
                if (cmd != null)
                {
                    InvokeCommand(cmd);
                    Logger.Log(Tag, "Settings opened via command");
                }
                else
                {
                    // Fallback: open Profile pane (which contains Settings access)
                    var profileCmd = _r.GetPropertyValue(_homeViewModel, "ProfilePaneToggleCommand");
                    if (profileCmd != null) InvokeCommand(profileCmd);
                    Logger.Log(Tag, "Settings: no direct command, opened Profile pane as fallback");
                }
            });
            _r.AddChild(content, settingsBtn);
        }

        // Profile button — opens Root's Profile pane
        var profileBtn = BuildPopupButton("Profile", _text, _cardBg);
        if (profileBtn != null)
        {
            _r.SubscribeEvent(profileBtn, "PointerPressed", () =>
            {
                DismissUserCardPopup();
                var cmd = _r.GetPropertyValue(_homeViewModel, "ProfilePaneToggleCommand");
                if (cmd != null) InvokeCommand(cmd);
            });
            _r.AddChild(content, profileBtn);
        }

        // Sign out button — only shown if a sign-out command exists on HomeViewModel
        var signOutCmd = _r.GetPropertyValue(_homeViewModel, "SignOutCommand")
            ?? _r.GetPropertyValue(_homeViewModel, "LogOutCommand")
            ?? _r.GetPropertyValue(_homeViewModel, "LogoutCommand");
        if (signOutCmd != null)
        {
            var signOutBtn = BuildPopupButton("Sign out", "#e74c3c", _cardBg);
            if (signOutBtn != null)
            {
                var capturedCmd = signOutCmd;
                _r.SubscribeEvent(signOutBtn, "PointerPressed", () =>
                {
                    DismissUserCardPopup();
                    InvokeCommand(capturedCmd);
                });
                _r.AddChild(content, signOutBtn);
            }
        }

        _r.SetBorderChild(_userCardPopupPanel, content);

        // Click-outside-to-dismiss: transparent backdrop covering the entire window
        var windowBounds = _r.GetBounds(_mainWindow);
        double winW = windowBounds?.W ?? 1920;
        double winH = windowBounds?.H ?? 1080;

        _userCardBackdrop = _r.CreateBorder("#00000001", 0); // near-transparent, catches clicks
        if (_userCardBackdrop != null)
        {
            _r.SetWidth(_userCardBackdrop, winW);
            _r.SetHeight(_userCardBackdrop, winH);
            _r.SetCanvasPosition(_userCardBackdrop, 0, 0);
            _r.AddToOverlay(overlay, _userCardBackdrop);

            _r.SubscribeEvent(_userCardBackdrop, "PointerPressed", () =>
            {
                DismissUserCardPopup();
            });
        }

        // Position popup: above the anchor, aligned to left edge of strip
        double popupH = 200; // estimated height (avatar + name + status + 2 buttons)
        double popupX = anchorX;
        double popupY = anchorY - popupH;
        if (popupY < 0) popupY = 0;

        _r.SetCanvasPosition(_userCardPopupPanel, popupX, popupY);
        _r.AddToOverlay(overlay, _userCardPopupPanel);
    }

    private object? BuildPopupButton(string label, string textColor, string bgColor)
    {
        var hoverBg = AdjustForHighlight(bgColor, 10);
        var normalBg = AdjustForHighlight(bgColor, 5);
        var btn = _r.CreateBorder(normalBg, 4);
        if (btn == null) return null;
        _r.SetMargin(btn, 8, 2, 8, 6);
        _r.SetCursorHand(btn);

        var text = _r.CreateTextBlock(label, 13, textColor);
        if (text != null) { _r.SetMargin(text, 10, 6, 10, 6); _r.SetBorderChild(btn, text); }

        var btnRef = btn;
        _r.SubscribeEvent(btn, "PointerEntered", () => _r.SetBackground(btnRef, hoverBg));
        _r.SubscribeEvent(btn, "PointerExited", () => _r.SetBackground(btnRef, normalBg));
        return btn;
    }

    private void DismissUserCardPopup()
    {
        if (_userCardPopupOverlay != null)
        {
            if (_userCardBackdrop != null)
                _r.RemoveFromOverlay(_userCardPopupOverlay, _userCardBackdrop);
            if (_userCardPopupPanel != null)
                _r.RemoveFromOverlay(_userCardPopupOverlay, _userCardPopupPanel);
        }
        _userCardPopupPanel = null;
        _userCardBackdrop = null;
        _userCardPopupOverlay = null;
        _statusSelectorPanel = null;
    }

    // ===== Status selector =====

    // Root only supports Active / Inactive / Disconnected (no DND or Invisible).
    // We map them to user-friendly labels: Online → Active, Away → Inactive.
    private static readonly (string Label, string Color, string EnumValue)[] StatusOptions =
    {
        ("Online", "#43b581", "Active"),
        ("Away",   "#faa61a", "Inactive"),
    };

    private object? _statusSelectorPanel; // inline dropdown within the popup

    /// <summary>
    /// Get the current user status label by probing session/user properties.
    /// Root uses UserOnlineStatus enum: Active, Inactive, Disconnected.
    /// </summary>
    private string GetCurrentStatusLabel()
    {
        if (_homeViewModel == null) return "Online";
        try
        {
            // Walk: HomeViewModel → RootSessionAccessor → Session → UserInfoService → SessionUser → OnlineStatus
            // Also try GlobalUser.OnlineStatus
            foreach (var accProp in new[] { "RootSessionAccessor", "SessionAccessor", "Session" })
            {
                var accessor = _r.GetPropertyValue(_homeViewModel, accProp);
                if (accessor == null) continue;
                var session = accProp == "Session" ? accessor : _r.GetPropertyValue(accessor, "Session");
                if (session == null) continue;

                // Try UserInfoService.SessionUser.OnlineStatus
                var userInfoService = _r.GetPropertyValue(session, "UserInfoService");
                if (userInfoService != null)
                {
                    var sessionUser = _r.GetPropertyValue(userInfoService, "SessionUser");
                    if (sessionUser != null)
                    {
                        var onlineStatus = _r.GetPropertyValue(sessionUser, "OnlineStatus")
                            ?? _r.GetPropertyValue(sessionUser, "MaxOnlineStatus")
                            ?? _r.GetPropertyValue(sessionUser, "DeviceOnlineStatus");
                        if (onlineStatus != null)
                        {
                            var s = onlineStatus.ToString() ?? "";
                            Logger.Log(Tag, $"GetCurrentStatusLabel: raw='{s}' from UserInfoService.SessionUser");
                            if (s.Contains("Inactive", StringComparison.OrdinalIgnoreCase)) return "Away";
                            if (s.Contains("Disconnected", StringComparison.OrdinalIgnoreCase)) return "Away";
                            return "Online"; // Active or anything else
                        }
                    }
                }
            }

            // Fallback: try common property names directly on HomeViewModel
            var status = _r.GetPropertyValue(_homeViewModel, "UserStatus")
                ?? _r.GetPropertyValue(_homeViewModel, "Status")
                ?? _r.GetPropertyValue(_homeViewModel, "PresenceStatus");
            if (status != null)
            {
                var s = status.ToString() ?? "";
                if (s.Contains("Inactive", StringComparison.OrdinalIgnoreCase) ||
                    s.Contains("Idle", StringComparison.OrdinalIgnoreCase) ||
                    s.Contains("Away", StringComparison.OrdinalIgnoreCase)) return "Away";
                return "Online";
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"GetCurrentStatusLabel error: {ex.Message}");
        }
        return "Online"; // default
    }

    private static string GetStatusColor(string label) => label switch
    {
        "Away" => "#faa61a",
        _      => "#43b581",
    };

    /// <summary>
    /// Show an inline status selector replacing the area below the status button.
    /// Each option is a row: colored dot + label, clickable.
    /// </summary>
    private void ShowStatusSelector(object anchorBtn, object popupContent)
    {
        // Toggle: if already open, remove it
        if (_statusSelectorPanel != null)
        {
            _r.RemoveChild(popupContent, _statusSelectorPanel);
            _statusSelectorPanel = null;
            return;
        }

        _statusSelectorPanel = _r.CreateStackPanel(vertical: true, spacing: 0);
        if (_statusSelectorPanel == null) return;
        _r.SetMargin(_statusSelectorPanel, 8, 0, 8, 4);

        // Thin divider above selector
        var divColor = AdjustForHighlight(_cardBg, 15);
        var div = _r.CreateBorder(divColor, 0);
        if (div != null) { _r.SetHeight(div, 1); _r.SetMargin(div, 0, 2, 0, 4); _r.AddChild(_statusSelectorPanel, div); }

        foreach (var (label, color, enumValue) in StatusOptions)
        {
            var row = BuildStatusRow(label, color, _text, _cardBg, enumValue, popupContent);
            if (row != null) _r.AddChild(_statusSelectorPanel, row);
        }

        // Insert selector into popup content (before the main divider/buttons)
        // Find insertion point: after the header panel (index 0), before divider (index 1)
        var children = _r.GetChildren(popupContent);
        if (children != null && children.Count >= 2)
        {
            // Insert at index 1 (after header, before divider)
            try { children.Insert(1, _statusSelectorPanel); }
            catch { _r.AddChild(popupContent, _statusSelectorPanel); }
        }
        else
        {
            _r.AddChild(popupContent, _statusSelectorPanel);
        }
    }

    private object? BuildStatusRow(string label, string dotColor, string textColor,
        string bgColor, string enumValue, object popupContent)
    {
        var rowBg = "#00000000"; // transparent default
        var rowHover = AdjustForHighlight(bgColor, 10);

        var row = _r.CreateBorder(rowBg, 6);
        if (row == null) return null;
        _r.SetCursorHand(row);
        _r.SetMargin(row, 0, 1, 0, 1);

        var layout = _r.CreateStackPanel(vertical: false, spacing: 8);
        if (layout == null) return row;
        _r.SetMargin(layout, 8, 5, 8, 5);
        _r.SetVerticalAlignment(layout, "Center");

        // Status dot
        var dot = _r.CreateBorder(dotColor, 5);
        if (dot != null) { _r.SetWidth(dot, 10); _r.SetHeight(dot, 10); _r.SetVerticalAlignment(dot, "Center"); _r.AddChild(layout, dot); }

        // Label
        var text = _r.CreateTextBlock(label, 12, textColor);
        if (text != null) { _r.SetVerticalAlignment(text, "Center"); _r.AddChild(layout, text); }

        _r.SetBorderChild(row, layout);

        // Hover
        var rowRef = row;
        _r.SubscribeEvent(row, "PointerEntered", () => _r.SetBackground(rowRef, rowHover));
        _r.SubscribeEvent(row, "PointerExited", () => _r.SetBackground(rowRef, rowBg));

        // Click: set status via UserInfoService.SetMaxOnlineStatusAsync()
        var capturedLabel = label;
        var capturedEnum = enumValue;
        _r.SubscribeEvent(row, "PointerPressed", () =>
        {
            SetUserOnlineStatus(capturedEnum, capturedLabel);
            DismissUserCardPopup();
        });

        return row;
    }

    /// <summary>
    /// Set the user's online status via RootSession.UserInfoService.SetMaxOnlineStatusAsync().
    /// Root uses UserOnlineStatus enum: Active, Inactive, Disconnected.
    /// Falls back to SetDeviceOnlineStatusAsync (UserDeviceOnlineStatus: Active, Disconnected).
    /// </summary>
    private void SetUserOnlineStatus(string enumValue, string label)
    {
        if (_homeViewModel == null) return;
        try
        {
            // Walk: HomeViewModel → RootSessionAccessor → Session → UserInfoService
            object? userInfoService = null;
            foreach (var accProp in new[] { "RootSessionAccessor", "SessionAccessor", "Session" })
            {
                var accessor = _r.GetPropertyValue(_homeViewModel, accProp);
                if (accessor == null) continue;
                var session = accProp == "Session" ? accessor : _r.GetPropertyValue(accessor, "Session");
                if (session == null) continue;
                userInfoService = _r.GetPropertyValue(session, "UserInfoService");
                if (userInfoService != null) break;
            }

            if (userInfoService == null)
            {
                Logger.Log(Tag, $"Status '{label}': UserInfoService not found");
                return;
            }

            // Try each method name, find one where the enum value parses
            var serviceType = userInfoService.GetType();
            var methodNames = new[] { "SetMaxOnlineStatusAsync", "SetDeviceOnlineStatusAsync" };

            foreach (var methodName in methodNames)
            {
                // Search type + all interfaces for the method
                var candidates = new List<MethodInfo>();
                foreach (var m in serviceType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                    if (m.Name == methodName) candidates.Add(m);
                foreach (var iface in serviceType.GetInterfaces())
                    foreach (var m in iface.GetMethods())
                        if (m.Name == methodName) candidates.Add(m);

                foreach (var method in candidates)
                {
                    var parms = method.GetParameters();
                    if (parms.Length == 0) continue;

                    var enumType = parms[0].ParameterType;
                    if (!enumType.IsEnum) continue;

                    // Check if our enum value exists in this enum type
                    if (!Enum.IsDefined(enumType, enumValue))
                    {
                        // For "Inactive" on UserDeviceOnlineStatus (which only has Active/Disconnected),
                        // skip this method and try the next one
                        continue;
                    }

                    var statusValue = Enum.Parse(enumType, enumValue);

                    // Invoke with just the enum param (skip overloads with CancellationToken)
                    object?[] args = parms.Length == 1
                        ? new[] { statusValue }
                        : new[] { statusValue, System.Threading.CancellationToken.None };

                    method.Invoke(userInfoService, args);
                    Logger.Log(Tag, $"Status changed to '{label}' ({enumValue}) via {methodName}");
                    return;
                }
            }

            Logger.Log(Tag, $"Status '{label}': no compatible Set*OnlineStatusAsync method found on {serviceType.Name}");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Status '{label}' error: {ex.Message}");
        }
    }

    private void SetAvatarInitial(object avatarBorder, string initial, string textColor)
    {
        var text = _r.CreateTextBlock(initial, 11, textColor);
        _r.SetFontWeight(text, "Bold");
        _r.SetHorizontalAlignment(text, "Center");
        _r.SetVerticalAlignment(text, "Center");
        _r.SetBorderChild(avatarBorder, text);
    }

    /// <summary>
    /// Try to extract current username from HomeViewModel via multiple property chains.
    /// Skips GUID-like values that are IDs, not display names.
    /// </summary>
    private string? GetCurrentUsername()
    {
        if (_homeViewModel == null) return null;
        try
        {
            // Helper: return value only if it looks like a real name (not a GUID)
            static string? ValidName(object? obj)
            {
                var s = obj as string;
                if (string.IsNullOrEmpty(s)) return null;
                // Skip GUIDs (8-4-4-4-12 hex pattern)
                if (s.Length >= 32 && s.Contains('-') && Guid.TryParse(s, out _)) return null;
                return s;
            }

            // Try deep property chains through any accessor-like property
            foreach (var propName in new[] { "RootSessionAccessor", "SessionAccessor", "Session" })
            {
                var accessor = _r.GetPropertyValue(_homeViewModel, propName);
                if (accessor == null) continue;

                // Walk: accessor → Session → various user paths
                var session = propName == "Session" ? accessor : _r.GetPropertyValue(accessor, "Session");
                if (session == null) continue;

                // Try multiple paths to find user info
                foreach (var userPath in new[] { "UserInfoService.SessionUser", "SessionUser", "User", "CurrentUser" })
                {
                    object? user = session;
                    foreach (var part in userPath.Split('.'))
                    {
                        user = _r.GetPropertyValue(user, part);
                        if (user == null) break;
                    }
                    if (user == null) continue;

                    var name = ValidName(_r.GetPropertyValue(user, "Username"))
                        ?? ValidName(_r.GetPropertyValue(user, "Name"))
                        ?? ValidName(_r.GetPropertyValue(user, "DisplayName"))
                        ?? ValidName(_r.GetPropertyValue(user, "UserName"));
                    if (name != null)
                    {
                        Logger.Log(Tag, $"GetCurrentUsername: found '{name}' via {propName}.{userPath}");
                        return name;
                    }
                }
            }

            // Fallback: probe common property names directly on HomeViewModel
            var fallback = ValidName(_r.GetPropertyValue(_homeViewModel, "Username"))
                ?? ValidName(_r.GetPropertyValue(_homeViewModel, "DisplayName"))
                ?? ValidName(_r.GetPropertyValue(_homeViewModel, "UserName"));
            if (fallback != null) return fallback;

            // Last resort: try to find username from the DM tab names or profile
            Logger.Log(Tag, "GetCurrentUsername: all property chains returned null/GUID");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"GetCurrentUsername error: {ex.Message}");
        }
        return null;
    }

    /// <summary>
    /// Try to get the user's profile picture bitmap from HomeViewModel.
    /// Follows: ProfilePictureAsyncBitmapWrapper → Task → Result → Bitmap
    /// </summary>
    private object? TryGetProfileBitmap()
    {
        if (_homeViewModel == null) return null;
        try
        {
            var bitmapTask = _r.GetPropertyValue(_homeViewModel, "ProfilePictureAsyncBitmapWrapper");
            if (bitmapTask == null) return null;

            var isCompleted = _r.GetPropertyValue(bitmapTask, "IsCompleted");
            if (isCompleted is not true) return null;

            var bitmapWrapper = _r.GetPropertyValue(bitmapTask, "Result");
            if (bitmapWrapper == null) return null;

            return _r.GetPropertyValue(bitmapWrapper, "Bitmap")
                ?? _r.GetPropertyValue(bitmapWrapper, "Value")
                ?? _r.GetPropertyValue(bitmapWrapper, "Source");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"TryGetProfileBitmap error: {ex.Message}");
            return null;
        }
    }

    private void SetIconToLetter(object iconBorder, string initial, bool isSelected, string textColor)
    {
        var text = _r.CreateTextBlock(initial, 18, isSelected ? "#FFFFFF" : textColor);
        _r.SetFontWeight(text, "Bold");
        _r.SetHorizontalAlignment(text, "Center");
        _r.SetVerticalAlignment(text, "Center");
        _r.SetBorderChild(iconBorder, text);
    }

    /// <summary>
    /// Handle server icon click: set SelectedTabViewModel on the HomeViewModel.
    /// </summary>
    private void OnServerIconClicked(object tabViewModel)
    {
        if (_homeViewModel == null) return;
        try
        {
            bool set = _r.SetPropertyValue(_homeViewModel, "SelectedTabViewModel", tabViewModel);
            if (set)
            {
                Logger.Log(Tag, $"Tab selected: {GetTabDisplayName(tabViewModel)}");
                RefreshSelectedHighlight();
            }
            else
            {
                Logger.Log(Tag, "Failed to set SelectedTabViewModel");
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"OnServerIconClicked error: {ex.Message}");
        }
    }

    /// <summary>
    /// Update visual highlight on the currently selected tab icon,
    /// including the Home button state.
    /// </summary>
    private void RefreshSelectedHighlight()
    {
        if (_homeViewModel == null) return;

        var selectedTab = _r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel");
        bool isDmSelected = selectedTab != null && IsDmTab(selectedTab);

        // Update Home button highlight
        try
        {
            if (_homeButton != null)
            {
                var homeChildren = _r.GetChildren(_homeButton);
                if (homeChildren != null && homeChildren.Count >= 2)
                {
                    var pill = homeChildren[0];
                    var iconBorder = homeChildren[1];

                    _r.SetBackground(pill, isDmSelected ? _accent : "#00000000");
                    _r.SetHeight(pill, isDmSelected ? PillHeight : 8);
                    _r.SetBackground(iconBorder, isDmSelected ? _accent : _cardBg);

                    if (_r.IsBorder(iconBorder))
                    {
                        var iconChild = _r.GetBorderChild(iconBorder);
                        if (iconChild != null && _r.IsTextBlock(iconChild))
                            _r.SetForeground(iconChild, isDmSelected ? "#FFFFFF" : _text);
                    }
                    else
                    {
                        Logger.Log(Tag, $"RefreshHL: homeButton iconBorder is {iconBorder?.GetType().Name ?? "null"}, skipping GetBorderChild");
                    }
                }
            }
        }
        catch (Exception ex) { Logger.Log(Tag, $"RefreshHL home button error: {ex.Message}"); }

        // Update community server icons
        if (_serverStrip == null) return;
        var children = _r.GetChildren(_serverStrip);
        if (children == null) return;

        var tabs = _r.GetPropertyValue(_homeViewModel, "Tabs");
        if (tabs == null) return;

        // Build community-only tab list (matching PopulateServerStrip filter)
        var communityTabs = new List<object>();
        if (tabs is IEnumerable tabsEnum)
        {
            foreach (var t in tabsEnum)
            {
                if (t != null && !IsDmTab(t)) communityTabs.Add(t);
            }
        }

        int idx = 0;
        foreach (var container in children)
        {
            if (container == null) continue;
            bool isSelected = idx < communityTabs.Count && communityTabs[idx] == selectedTab;

            try
            {
                var containerChildren = _r.GetChildren(container);
                if (containerChildren == null || containerChildren.Count < 2) { idx++; continue; }

                var pill = containerChildren[0];
                var iconBorder = containerChildren[1];

                _r.SetBackground(pill, isSelected ? _accent : "#00000000");
                _r.SetHeight(pill, isSelected ? PillHeight : 8);
                _r.SetBackground(iconBorder, isSelected ? _accent : _cardBg);

                if (_r.IsBorder(iconBorder))
                {
                    var iconChild = _r.GetBorderChild(iconBorder);
                    if (iconChild != null && _r.IsTextBlock(iconChild))
                        _r.SetForeground(iconChild, isSelected ? "#FFFFFF" : _text);
                }
                else
                {
                    Logger.Log(Tag, $"RefreshHL: strip[{idx}] child[1] is {iconBorder?.GetType().Name ?? "null"}, not Border — skipping text recolor");
                }
            }
            catch (Exception ex) { Logger.Log(Tag, $"RefreshHL strip[{idx}] error: {ex.Message}"); }

            idx++;
        }
    }

    // ===== Profile pane rearrangement =====

    /// <summary>
    /// Move the Sign Out button from Row 1 (bottom of the Profile pane Grid) to Row 0
    /// (inside the StackPanel, after the Support button). This makes it visible without
    /// scrolling to the bottom of a tall pane.
    /// Root's ProfileView layout: Grid(Row0=StackPanel[avatar,name,status,divider,Settings,Support,Update], Row1=SignOut@Bottom).
    /// </summary>
    private void RearrangeSignOutButton()
    {
        if (_rootSplitView == null || !IsApplied) return;

        // Walk from SplitView.Pane (the utility pane host), not the entire SplitView.
        // ProfileView is: SplitView.Pane → RootBorder → Border → ContentControl → ProfileView → Grid
        // Try multiple ways to get the pane content host
        var paneHost = _r.GetPropertyValue(_rootSplitView, "Pane")
            ?? _r.GetPropertyValue(_rootSplitView, "PaneContent");
        if (paneHost == null)
        {
            // Fallback: walk the SplitView's visual tree looking for the pane template part
            foreach (var child in _r.GetVisualChildren(_rootSplitView))
            {
                foreach (var gc in _r.GetVisualChildren(child))
                {
                    var name = gc.GetType().GetProperty("Name")?.GetValue(gc) as string;
                    if (name == "PART_PaneRoot" || name == "PaneRoot")
                    {
                        paneHost = gc;
                        break;
                    }
                }
                if (paneHost != null) break;
                // If no named part, look for a Panel/Border in the first visual child
                // that contains a ContentControl
                var tn = child.GetType().Name;
                if (tn.Contains("Panel") || tn.Contains("Grid"))
                {
                    paneHost = child;
                    break;
                }
            }
        }
        if (paneHost == null) return; // Pane not in visual tree (not open)

        var walker = new VisualTreeWalker(_r);
        int gridCount = 0;
        foreach (var node in walker.DescendantsDepthFirst(paneHost))
        {
            if (!_r.IsGrid(node)) continue;
            var rowDefs = _r.GetRowDefinitions(node);
            if (rowDefs == null) continue;
            gridCount++;
            if (rowDefs.Count != 2) continue;
            // This is a candidate ProfileView Grid

            // Look at visual children (not just Panel.Children) since the Grid
            // may have its children as visual children
            object? signOutButton = null;
            object? stackPanel = null;

            foreach (var child in _r.GetVisualChildren(node))
            {
                if (child == null) continue;
                int row = _r.GetGridRow(child);
                var typeName = child.GetType().Name;

                if (row == 1 && typeName.Contains("Button"))
                {
                    // Row 1 Button = Sign Out button (only button in Row 1 of ProfileView)
                    signOutButton = child;
                    Logger.Log(Tag, $"RearrangeSignOut: found Row 1 Button: {typeName}");
                }
                else if (row == 0 && (typeName.Contains("StackPanel") || typeName.Contains("Panel")))
                {
                    stackPanel = child;
                }
            }

            if (signOutButton == null || stackPanel == null)
            {
                if (signOutButton != null) Logger.Log(Tag, "RearrangeSignOut: no StackPanel found in row 0");
                continue;
            }

            // Already tagged = already moved
            var tag = signOutButton.GetType().GetProperty("Tag")?.GetValue(signOutButton) as string;
            if (tag == "rootcord-signout-moved") continue;

            // Remove from Grid Row 1
            _r.RemoveChild(node, signOutButton);

            // Clear the Bottom alignment and Row assignment
            _r.SetVerticalAlignment(signOutButton, "Top");
            _r.SetGridRow(signOutButton, 0);
            _r.SetMargin(signOutButton, 0, 12, 0, 0);

            // Tag it so we don't re-process
            _r.SetTag(signOutButton, "rootcord-signout-moved");

            // Add to StackPanel after the last existing child
            _r.AddChild(stackPanel, signOutButton);

            Logger.Log(Tag, "RearrangeSignOutButton: moved Sign Out under Support button");
            return;
        }
    }

    // ===== Tab monitoring =====

    private void SubscribeTabChanges()
    {
        if (_homeViewModel == null) return;

        // Watch for SelectedTabViewModel + PaneOpen changes
        _selectionHandler = _r.SubscribePropertyChanged(_homeViewModel, (propName) =>
        {
            // When pane opens, rearrange the Sign Out button in the Profile pane
            if (propName == "PaneViewModel" || propName == "PaneOpen" || propName == "ProfileOpen")
            {
                Logger.Log(Tag, $"PropertyChanged: {propName} — scheduling SignOut rearrangement");
                var paneToken = _applyCts?.Token ?? System.Threading.CancellationToken.None;
                Task.Delay(500, paneToken).ContinueWith(_ =>
                {
                    if (paneToken.IsCancellationRequested) return;
                    _r.RunOnUIThread(() =>
                    {
                        try
                        {
                            Logger.Log(Tag, "RearrangeSignOut: executing from PropertyChanged");
                            RearrangeSignOutButton();
                        }
                        catch (Exception ex) { Logger.Log(Tag, $"RearrangeSignOut error: {ex.Message}"); }
                    });
                }, System.Threading.Tasks.TaskContinuationOptions.NotOnCanceled);
            }

            if (propName == "SelectedTabViewModel")
            {
                _r.RunOnUIThread(() =>
                {
                    try
                    {
                        RefreshSelectedHighlight();

                        // Re-apply community members swap based on current tab type
                        var sel = _r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel");
                        if (sel != null && !IsDmTab(sel))
                        {
                            SwapCommunityMembersToRight();
                            // CommunityView may not be in the visual tree yet — progressive retry
                            if (_communityGrid == null)
                            {
                                var capturedSel = sel;
                                var capturedToken = _applyCts?.Token ?? System.Threading.CancellationToken.None;
                                int[] delays = { 100, 300, 600, 1000 };
                                foreach (var delay in delays)
                                {
                                    var d = delay;
                                    System.Threading.Tasks.Task.Delay(d, capturedToken).ContinueWith(_ =>
                                    {
                                        if (capturedToken.IsCancellationRequested) return;
                                        _r.RunOnUIThread(() =>
                                        {
                                            try
                                            {
                                                var current = _r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel");
                                                if (current == capturedSel && _communityGrid == null)
                                                {
                                                    SwapCommunityMembersToRight();
                                                    if (_communityGrid != null)
                                                        Logger.Log(Tag, $"Tab switch: swap succeeded at {d}ms");
                                                }
                                            }
                                            catch { }
                                        });
                                    }, System.Threading.Tasks.TaskContinuationOptions.NotOnCanceled);
                                }
                            }
                        }
                        else
                            RevertCommunityMembersSwap();
                    }
                    catch (Exception ex) { Logger.Log(Tag, $"Selection change handler error: {ex.Message}"); }
                });
            }
        });

        if (_selectionHandler != null)
            Logger.Log(Tag, "Subscribed to SelectedTabViewModel changes");

        // Watch for Tabs collection changes (tab added/removed)
        _tabsCollection = _r.GetPropertyValue(_homeViewModel, "Tabs");
        if (_tabsCollection != null)
        {
            _tabsCollectionHandler = _r.SubscribeCollectionChanged(_tabsCollection, () =>
            {
                _r.RunOnUIThread(() =>
                {
                    try
                    {
                        Logger.Log(Tag, "Tabs collection changed, rebuilding strip");
                        PopulateServerStrip();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(Tag, $"Collection change handler error: {ex.Message}");
                    }
                });
            });

            if (_tabsCollectionHandler != null)
                Logger.Log(Tag, "Subscribed to Tabs collection changes");
        }
    }

    private void UnsubscribeTabChanges()
    {
        if (_selectionHandler != null && _homeViewModel != null)
        {
            _r.UnsubscribePropertyChanged(_homeViewModel, _selectionHandler);
            _selectionHandler = null;
        }

        if (_tabsCollectionHandler != null && _tabsCollection != null)
        {
            _r.UnsubscribeCollectionChanged(_tabsCollection, _tabsCollectionHandler);
            _tabsCollectionHandler = null;
        }
        _tabsCollection = null;
    }

    // ===== Hover tooltip =====

    /// <summary>
    /// Resolve member counts from a CommunityTabViewModel.
    /// Returns (attached/online, total). Both may be 0 for DMs or when unavailable.
    /// Path: tabViewModel.Members (IMemberService) → AttachedMemberCount / MemberCount.
    /// Matches CommunityTabView.MemberCountTextBlock binding: Members.AttachedMemberCount.
    /// </summary>
    private (int attached, int total) GetTabMemberCounts(object? tabViewModel)
    {
        if (tabViewModel == null || IsDmTab(tabViewModel)) return (0, 0);
        try
        {
            var members = _r.GetPropertyValue(tabViewModel, "Members");
            if (members == null) return (0, 0);

            // AttachedMemberCount = online/attached members (shown in native tab bar)
            // MemberCount         = total server members
            int attached = _r.GetPropertyValue(members, "AttachedMemberCount") is int a ? a : 0;
            int total    = _r.GetPropertyValue(members, "MemberCount")         is int t ? t : 0;
            return (attached, total);
        }
        catch { return (0, 0); }
    }

    /// <summary>
    /// Show a native-style tooltip to the right of the hovered server icon.
    /// Replicates Root's CommunityTabView member count pill exactly:
    ///   - Name:  13px, SemiBold, TextPrimary (_text)
    ///   - Count: 11px, Medium(500), TextSecondary (_muted), with 5×5 BrandPrimary dot
    ///   - Card:  CardBg, 12px radius, 1.5px border (ContentPages.CreateCard style)
    /// </summary>
    private void ShowIconTooltip(object iconBorder, string displayName, object? tabViewModel)
    {
        DismissIconTooltip();

        var mainWindow = _r.GetMainWindow();
        if (mainWindow == null) return;

        var overlay = _r.GetOverlayLayer(mainWindow);
        if (overlay == null) return;
        _tooltipOverlay = overlay;

        // Position: to the right of the icon, vertically centered
        var iconBounds = _r.GetBounds(iconBorder);
        var translated = _r.TranslatePoint(iconBorder, 0, 0, overlay);
        if (iconBounds == null || translated == null) return;

        double iconX = translated.Value.X;
        double iconY = translated.Value.Y;
        double iconH = iconBounds.Value.H;

        // Member counts (0 for DMs or unavailable)
        var (attached, total) = GetTabMemberCounts(tabViewModel);
        bool hasAttached = attached > 0;
        bool hasTotal    = total > 0;
        bool hasCounts   = hasAttached || hasTotal;

        // Card: CardBg, 12px radius, 1.5px border — matches ContentPages.CreateCard()
        _tooltipPanel = _r.CreateBorder(_cardBg, 12);
        if (_tooltipPanel == null) return;
        _r.SetTag(_tooltipPanel, "rootcord-tooltip");
        SetBorderStroke(_tooltipPanel, AdjustForHighlight(_cardBg, 15), 1.5);

        // Outer content stack (vertical, name + optional count row)
        var content = _r.CreateStackPanel(vertical: true, spacing: 4);
        if (content == null) return;
        _r.SetMargin(content, 12, 8, 12, 8);

        // --- Name line: 13px, SemiBold, TextPrimary ---
        // Matches CommunityNameTextBlock font/weight from CommunityTabView
        var nameText = _r.CreateTextBlock(displayName, 13, _text);
        if (nameText == null) return;
        _r.SetFontWeight(nameText, "SemiBold");
        _r.SetVerticalAlignment(nameText, "Center");
        _r.AddChild(content, nameText);

        // --- Member count row: [●dot] [X online • Y members] ---
        // Matches Root's native MemberCountTextBlock: 11px, Medium(500), TextSecondary
        if (hasCounts)
        {
            var countRow = _r.CreateStackPanel(vertical: false, spacing: 4);
            if (countRow != null)
            {
                _r.SetVerticalAlignment(countRow, "Center");

                // 5×5 BrandPrimary dot (matches the ellipse in CommunityTabView native pill)
                var dot = _r.CreateBorder(_accent, 3); // ~3px radius for a 5px circle
                if (dot != null)
                {
                    _r.SetWidth(dot, 5);
                    _r.SetHeight(dot, 5);
                    _r.SetVerticalAlignment(dot, "Center");
                    _r.AddChild(countRow, dot);
                }

                // Count label: "X online • Y members" or "X online" or "Y members"
                string countLabel;
                if (hasAttached && hasTotal && total != attached)
                    countLabel = $"{attached} online \u2022 {total} members";
                else if (hasTotal)
                    countLabel = $"{total} members";
                else
                    countLabel = $"{attached} online";

                var countText = _r.CreateTextBlock(countLabel, 11, _muted);
                if (countText != null)
                {
                    // FontWeight.Medium (500) — matches MemberCountTextBlock native style
                    _r.SetFontWeight(countText, "Medium");
                    _r.SetVerticalAlignment(countText, "Center");
                    _r.AddChild(countRow, countText);
                }

                _r.AddChild(content, countRow);
            }
        }

        _r.SetBorderChild(_tooltipPanel, content);

        // Position: right of icon with 12px gap, vertically centered on icon.
        // Estimated height: 30px (name only) or 46px (name + count)
        double estH = hasCounts ? 46 : 30;
        double tooltipX = iconX + IconSize + 12;
        double tooltipY = iconY + (iconH / 2) - (estH / 2);
        _r.SetCanvasPosition(_tooltipPanel, tooltipX, tooltipY);

        _r.AddToOverlay(overlay, _tooltipPanel);
    }

    private void DismissIconTooltip()
    {
        if (_tooltipOverlay != null && _tooltipPanel != null)
        {
            _r.RemoveFromOverlay(_tooltipOverlay, _tooltipPanel);
        }
        _tooltipPanel = null;
        _tooltipOverlay = null;
    }
}
