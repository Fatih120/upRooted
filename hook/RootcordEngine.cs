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
    private object? _userCard;            // User card at bottom of strip
    private object? _stripGrid;           // Inner Grid (3 rows) inside the strip border

    // SystemTray (Row 1 collapse)
    private object? _systemTrayBorder;    // The SystemTray border at Row 1, Col 4
    private int _originalSystemTrayRow;   // Original grid row of SystemTray
    private double _originalRow1Height;   // Original Row 1 height (for revert)
    private string _originalRow1UnitType = "Pixel";

    // Original layout state (for revert)
    private double _originalCol0Width;
    private string _originalCol0UnitType = "Pixel";
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
    private object? _profileInterceptHandler;

    // State
    public bool IsApplied { get; private set; }

    private const string Tag = "Rootcord";
    private const double StripWidth = 56;
    private const double IconSize = 42;
    private const double IconCornerRadius = 21;
    private const double IconSpacing = 4;
    private const double PillWidth = 3;
    private const double PillHeight = 20;

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

            // Step 6: Build and inject server strip
            BuildAndInjectServerStrip();

            // Step 7: Subscribe to tab changes + intercept Profile pane reopens
            SubscribeTabChanges();

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

            // Dismiss any active tooltip/popup
            DismissIconTooltip();
            DismissUserCardPopup();

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

            // Remove server strip from grid
            if (_serverStripBorder != null && _homeViewGrid != null)
            {
                _r.RemoveChild(_homeViewGrid, _serverStripBorder);
                Logger.Log(Tag, "Server strip removed");
            }
            _serverStrip = null;
            _serverStripBorder = null;
            _homeButton = null;
            _userCard = null;
            _stripGrid = null;

            // Restore Row 1 and SystemTray position
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
        Logger.Log(Tag, $"Col0 restored to {_originalCol0Width}px");

        // Restore original Col positions
        foreach (var (control, originalSpan) in _modifiedColSpans)
        {
            _r.SetGridColumn(control, 0);
            // ColSpan restored separately below
        }
    }

    // ===== Row 1 collapse (tab bar row) =====

    /// <summary>
    /// Collapse Row 1 (the 60px tab bar row) to reclaim vertical space.
    /// Moves the SystemTray to Row 2 so its buttons remain accessible.
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

        // Move SystemTray from Row 1 to Row 2 (content row) so it stays accessible
        if (_systemTrayBorder != null)
        {
            int splitViewRow = _rootSplitView != null ? _r.GetGridRow(_rootSplitView) : 2;
            _r.SetGridRow(_systemTrayBorder, splitViewRow);

            // VerticalAlignment = Top so it appears at the top of the content area
            _r.SetVerticalAlignment(_systemTrayBorder, "Top");

            // Set ZIndex so it renders above the SplitView content
            try
            {
                _systemTrayBorder.GetType().GetProperty("ZIndex")?.SetValue(_systemTrayBorder, 10);
            }
            catch { }

            // Hide UserProfileButton within the SystemTray (our strip card replaces it)
            HideUserProfileButton();

            Logger.Log(Tag, $"SystemTray moved from Row {_originalSystemTrayRow} to Row {splitViewRow}");
        }

        // Collapse Row 1 to 0px
        _r.SetRowDefinitionPixelHeight(rowDefs[1], 0);
        Logger.Log(Tag, "Row 1 collapsed to 0px");
    }

    /// <summary>
    /// Restore Row 1 and SystemTray to original positions.
    /// </summary>
    private void RestoreTabBarRow()
    {
        if (_homeViewGrid == null) return;

        // Restore hidden profile button
        if (_hiddenProfilePanel != null)
        {
            _r.SetIsVisible(_hiddenProfilePanel, true);
            _hiddenProfilePanel = null;
        }

        // Restore SystemTray to original row
        if (_systemTrayBorder != null)
        {
            _r.SetGridRow(_systemTrayBorder, _originalSystemTrayRow);
            try
            {
                _systemTrayBorder.GetType().GetProperty("ZIndex")?.SetValue(_systemTrayBorder, 0);
            }
            catch { }
            Logger.Log(Tag, $"SystemTray restored to Row {_originalSystemTrayRow}");
        }

        // Restore Row 1 height
        var rowDefs = _r.GetRowDefinitions(_homeViewGrid);
        if (rowDefs != null && rowDefs.Count >= 2)
        {
            _r.SetRowDefinitionPixelHeight(rowDefs[1], _originalRow1Height);
            Logger.Log(Tag, $"Row 1 restored to {_originalRow1Height}px");
        }
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

    // ===== Server strip =====

    private void BuildAndInjectServerStrip()
    {
        if (_homeViewGrid == null) return;

        // Create the inner 3-row Grid: [Home+separator] [servers scroll] [user card]
        _stripGrid = _r.CreateGrid();
        if (_stripGrid == null)
        {
            Logger.Log(Tag, "Failed to create strip Grid");
            return;
        }
        _r.AddGridRowAuto(_stripGrid);   // Row 0: Home button + separator
        _r.AddGridRowStar(_stripGrid);   // Row 1: Scrollable server icons
        _r.AddGridRowAuto(_stripGrid);   // Row 2: User card

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

        // --- Row 2: User card ---
        _userCard = BuildUserCard();
        if (_userCard != null)
        {
            _r.SetGridRow(_userCard, 2);
            _r.AddChild(_stripGrid, _userCard);
        }

        // Wrap the 3-row Grid in a Border for background styling
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
        Logger.Log(Tag, $"Server strip injected: Col=0, RowSpan={rowCount} (3-row layout with Home + UserCard)");
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
            ShowIconTooltip(iconRef, "Direct Messages", null);
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
    /// Handle Home button click: open the Direct Messages pane (like Discord's Home icon).
    /// Checks if DMs are already showing to avoid toggle-close behavior.
    /// </summary>
    private void OnHomeButtonClicked()
    {
        if (_homeViewModel == null) return;
        try
        {
            // Check if DM pane is already open — if so, don't toggle (would close it)
            bool paneOpen = _r.GetPropertyValue(_homeViewModel, "PaneOpen") is true;
            var paneVm = _r.GetPropertyValue(_homeViewModel, "PaneViewModel");
            bool isShowingDms = paneOpen && paneVm != null &&
                paneVm.GetType().Name.Contains("DirectMessage");

            if (isShowingDms)
            {
                Logger.Log(Tag, "Home button: DM pane already open, no action needed");
                return;
            }

            // Invoke DirectMessagesPaneToggleCommand to open the DM list pane
            var dmCommand = _r.GetPropertyValue(_homeViewModel, "DirectMessagesPaneToggleCommand");
            if (dmCommand != null)
            {
                InvokeCommand(dmCommand);
                Logger.Log(Tag, "Home button: opened DM pane");
                RefreshSelectedHighlight();
                return;
            }

            // Fallback: select the first DM tab directly
            var tabs = _r.GetPropertyValue(_homeViewModel, "Tabs");
            if (tabs is IEnumerable tabsEnum)
            {
                foreach (var tabVm in tabsEnum)
                {
                    if (tabVm == null) continue;
                    if (IsDmTab(tabVm))
                    {
                        _r.SetPropertyValue(_homeViewModel, "SelectedTabViewModel", tabVm);
                        Logger.Log(Tag, $"Home button: selected DM tab '{GetTabDisplayName(tabVm)}'");
                        RefreshSelectedHighlight();
                        return;
                    }
                }
            }
            Logger.Log(Tag, "Home button: no DM command or tab found");
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

    /// <summary>
    /// Build the user card pinned to the bottom of the strip.
    /// Shows current user's avatar (or initial) and username.
    /// </summary>
    private object? BuildUserCard()
    {
        if (_homeViewModel == null) return null;

        var cardHover = AdjustForHighlight(_cardBg, 10);
        var onlineColor = "#43b581";

        string username = GetCurrentUsername() ?? "User";
        string initial = username.Length > 0 ? username[0].ToString().ToUpper() : "U";
        object? avatarBitmap = TryGetProfileBitmap();

        // Outer wrapper fills full strip width with opaque background
        var wrapper = _r.CreateBorder(_bg, 0);
        if (wrapper == null) return null;
        _r.SetTag(wrapper, "rootcord-usercard");

        // Inner card with rounded corners and padding
        var card = _r.CreateBorder(_cardBg, 8);
        if (card == null) return wrapper;
        _r.SetMargin(card, 4, 2, 4, 6);
        _r.SetCursorHand(card);

        // Horizontal layout: avatar + username
        var layout = _r.CreateStackPanel(vertical: false, spacing: 6);
        if (layout == null) return wrapper;
        _r.SetMargin(layout, 6, 6, 6, 6);
        _r.SetVerticalAlignment(layout, "Center");

        // Avatar: 28x28 circle with online status dot
        var avatarContainer = _r.CreateGrid();
        if (avatarContainer != null)
        {
            _r.SetWidth(avatarContainer, 28);
            _r.SetHeight(avatarContainer, 28);

            var avatarBorder = _r.CreateBorder(_bg, 14);
            if (avatarBorder != null)
            {
                _r.SetWidth(avatarBorder, 28);
                _r.SetHeight(avatarBorder, 28);
                _r.SetClipToBounds(avatarBorder, true);

                if (avatarBitmap != null)
                {
                    var img = _r.CreateImage("UniformToFill");
                    if (img != null)
                    {
                        _r.SetImageSource(img, avatarBitmap);
                        _r.SetWidth(img, 28);
                        _r.SetHeight(img, 28);
                        _r.SetBorderChild(avatarBorder, img);
                    }
                    else
                        SetAvatarInitial(avatarBorder, initial, _text);
                }
                else
                    SetAvatarInitial(avatarBorder, initial, _text);

                _r.AddChild(avatarContainer, avatarBorder);
            }

            // Online dot
            var statusDot = _r.CreateBorder(onlineColor, 5);
            if (statusDot != null)
            {
                _r.SetWidth(statusDot, 10);
                _r.SetHeight(statusDot, 10);
                _r.SetHorizontalAlignment(statusDot, "Right");
                _r.SetVerticalAlignment(statusDot, "Bottom");
                _r.AddChild(avatarContainer, statusDot);
            }
            _r.AddChild(layout, avatarContainer);
        }

        // Username text
        var nameText = _r.CreateTextBlock(username, 11, _text);
        if (nameText != null)
        {
            try
            {
                var trimProp = nameText.GetType().GetProperty("TextTrimming");
                if (trimProp != null)
                    trimProp.SetValue(nameText, Enum.Parse(trimProp.PropertyType, "CharacterEllipsis"));
            }
            catch { }
            _r.SetVerticalAlignment(nameText, "Center");
            _r.AddChild(layout, nameText);
        }

        _r.SetBorderChild(card, layout);
        _r.SetBorderChild(wrapper, card);

        // Click: show user card popup above this card
        var cardBgCapture = _cardBg;
        var cardRef = card;
        _r.SubscribeEvent(card, "PointerPressed", () =>
        {
            ShowUserCardPopup(cardRef);
        });

        // Hover
        _r.SubscribeEvent(card, "PointerEntered", () => _r.SetBackground(cardRef, cardHover));
        _r.SubscribeEvent(card, "PointerExited", () => _r.SetBackground(cardRef, cardBgCapture));

        return wrapper;
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

        // Profile button — opens Root's Profile pane (contains Settings, Support, Sign out)
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

    private static readonly (string Label, string Color, string[] CommandNames)[] StatusOptions =
    {
        ("Online",         "#43b581", new[] { "SetOnlineStatusCommand", "GoOnlineCommand", "OnlineCommand" }),
        ("Idle",           "#faa61a", new[] { "SetIdleStatusCommand", "GoIdleCommand", "IdleCommand" }),
        ("Do Not Disturb", "#f04747", new[] { "SetDndStatusCommand", "SetDoNotDisturbCommand", "DndCommand", "DoNotDisturbCommand" }),
        ("Invisible",      "#747f8d", new[] { "SetInvisibleStatusCommand", "GoInvisibleCommand", "InvisibleCommand", "SetOfflineStatusCommand" }),
    };

    private object? _statusSelectorPanel; // inline dropdown within the popup

    /// <summary>
    /// Get the current user status label by probing HomeViewModel properties.
    /// </summary>
    private string GetCurrentStatusLabel()
    {
        if (_homeViewModel == null) return "Online";
        try
        {
            // Try common property names for current status
            var status = _r.GetPropertyValue(_homeViewModel, "UserStatus")
                ?? _r.GetPropertyValue(_homeViewModel, "Status")
                ?? _r.GetPropertyValue(_homeViewModel, "PresenceStatus")
                ?? _r.GetPropertyValue(_homeViewModel, "CurrentStatus");

            if (status != null)
            {
                var s = status.ToString() ?? "";
                // Map enum/string values to our labels
                if (s.Contains("Idle", StringComparison.OrdinalIgnoreCase)) return "Idle";
                if (s.Contains("Dnd", StringComparison.OrdinalIgnoreCase) ||
                    s.Contains("DoNotDisturb", StringComparison.OrdinalIgnoreCase) ||
                    s.Contains("Busy", StringComparison.OrdinalIgnoreCase)) return "Do Not Disturb";
                if (s.Contains("Invisible", StringComparison.OrdinalIgnoreCase) ||
                    s.Contains("Offline", StringComparison.OrdinalIgnoreCase)) return "Invisible";
                if (s.Contains("Online", StringComparison.OrdinalIgnoreCase) ||
                    s.Contains("Available", StringComparison.OrdinalIgnoreCase)) return "Online";
                Logger.Log(Tag, $"GetCurrentStatusLabel: unrecognized status '{s}'");
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
        "Idle"           => "#faa61a",
        "Do Not Disturb" => "#f04747",
        "Invisible"      => "#747f8d",
        _                => "#43b581",
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

        foreach (var (label, color, cmdNames) in StatusOptions)
        {
            var row = BuildStatusRow(label, color, _text, _cardBg, cmdNames, popupContent);
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
        string bgColor, string[] cmdNames, object popupContent)
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

        // Click: try to invoke the status command, then dismiss
        var capturedLabel = label;
        var capturedCmdNames = cmdNames;
        _r.SubscribeEvent(row, "PointerPressed", () =>
        {
            if (_homeViewModel == null) return;
            bool invoked = false;

            // Try each command name on HomeViewModel
            foreach (var name in capturedCmdNames)
            {
                var cmd = _r.GetPropertyValue(_homeViewModel, name);
                if (cmd != null)
                {
                    InvokeCommand(cmd);
                    Logger.Log(Tag, $"Status changed to '{capturedLabel}' via {name}");
                    invoked = true;
                    break;
                }
            }

            // Fallback: try SetStatus(string) / SetPresence method
            if (!invoked)
            {
                try
                {
                    var setStatus = _homeViewModel.GetType().GetMethod("SetStatus",
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (setStatus != null)
                    {
                        setStatus.Invoke(_homeViewModel, new object[] { capturedLabel });
                        Logger.Log(Tag, $"Status changed to '{capturedLabel}' via SetStatus()");
                        invoked = true;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(Tag, $"SetStatus fallback error: {ex.Message}");
                }
            }

            if (!invoked)
                Logger.Log(Tag, $"Status '{capturedLabel}': no command found (visual-only change)");

            DismissUserCardPopup();
        });

        return row;
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

                var iconChild = _r.GetBorderChild(iconBorder);
                if (iconChild != null && _r.IsTextBlock(iconChild))
                    _r.SetForeground(iconChild, isDmSelected ? "#FFFFFF" : _text);
            }
        }

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

            var containerChildren = _r.GetChildren(container);
            if (containerChildren == null || containerChildren.Count < 2) { idx++; continue; }

            var pill = containerChildren[0];
            var iconBorder = containerChildren[1];

            _r.SetBackground(pill, isSelected ? _accent : "#00000000");
            _r.SetHeight(pill, isSelected ? PillHeight : 8);
            _r.SetBackground(iconBorder, isSelected ? _accent : _cardBg);

            var iconChild = _r.GetBorderChild(iconBorder);
            if (iconChild != null && _r.IsTextBlock(iconChild))
                _r.SetForeground(iconChild, isSelected ? "#FFFFFF" : _text);

            idx++;
        }
    }

    // ===== Tab monitoring =====

    private void SubscribeTabChanges()
    {
        if (_homeViewModel == null) return;

        // Watch for SelectedTabViewModel changes
        _selectionHandler = _r.SubscribePropertyChanged(_homeViewModel, (propName) =>
        {
            if (propName == "SelectedTabViewModel")
            {
                _r.RunOnUIThread(() =>
                {
                    try { RefreshSelectedHighlight(); }
                    catch (Exception ex) { Logger.Log(Tag, $"Selection change handler error: {ex.Message}"); }
                });
            }
        });

        if (_selectionHandler != null)
            Logger.Log(Tag, "Subscribed to SelectedTabViewModel changes");

        // Intercept Profile pane from reopening while Rootcord is active
        _profileInterceptHandler = _r.SubscribePropertyChanged(_homeViewModel, (prop) =>
        {
            if (prop == "ProfileOpen" && IsApplied &&
                _r.GetPropertyValue(_homeViewModel, "ProfileOpen") is true)
            {
                _r.RunOnUIThread(() =>
                {
                    _r.SetPropertyValue(_homeViewModel, "ProfileOpen", false);
                    _r.SetPropertyValue(_homeViewModel, "PaneOpen", false);
                });
            }
        });

        if (_profileInterceptHandler != null)
            Logger.Log(Tag, "Subscribed to ProfileOpen intercept");

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
        // Unsubscribe profile intercept first (before restoring pane state)
        if (_profileInterceptHandler != null && _homeViewModel != null)
        {
            _r.UnsubscribePropertyChanged(_homeViewModel, _profileInterceptHandler);
            _profileInterceptHandler = null;
        }

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
    /// Show a themed tooltip pill to the right of the hovered icon.
    /// Matches Root's native card style: CardBg, 12 radius, subtle 1.5px border.
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

        // Build tooltip: native card — CardBg, 12 radius, subtle border
        _tooltipPanel = _r.CreateBorder(_cardBg, 12);
        if (_tooltipPanel == null) return;
        _r.SetTag(_tooltipPanel, "rootcord-tooltip");
        SetBorderStroke(_tooltipPanel, AdjustForHighlight(_cardBg, 15), 1.5);

        // Single line: community name centered in the pill
        var nameText = _r.CreateTextBlock(displayName, 13, _text);
        if (nameText == null) return;
        _r.SetFontWeight(nameText, "SemiBold");
        _r.SetMargin(nameText, 12, 8, 12, 8);
        _r.SetBorderChild(_tooltipPanel, nameText);

        // Position tooltip: right of icon with gap, vertically centered on icon
        double tooltipX = iconX + IconSize + 12;
        double tooltipY = iconY + (iconH / 2) - 16; // center on icon
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
