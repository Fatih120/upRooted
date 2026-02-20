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

    // Original layout state (for revert)
    private double _originalCol0Width;
    private string _originalCol0UnitType = "Pixel";
    private bool _tabsWasVisible = true;
    private readonly List<(object control, int originalColSpan)> _modifiedColSpans = new();

    // Tab monitoring
    private object? _tabsCollectionHandler;
    private object? _selectionHandler;
    private object? _tabsCollection; // INotifyCollectionChanged source

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

            // Step 4: Hide tab bar
            _tabsWasVisible = _r.GetIsVisible(_tabsControl);
            _r.SetIsVisible(_tabsControl, false);
            Logger.Log(Tag, "TabsControl hidden");

            // Step 5: Build and inject server strip
            BuildAndInjectServerStrip();

            // Step 6: Subscribe to tab changes
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

            // Unsubscribe tab monitoring
            UnsubscribeTabChanges();

            // Remove server strip from grid
            if (_serverStripBorder != null && _homeViewGrid != null)
            {
                _r.RemoveChild(_homeViewGrid, _serverStripBorder);
                Logger.Log(Tag, "Server strip removed");
            }
            _serverStrip = null;
            _serverStripBorder = null;

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

        foreach (var child in _r.GetVisualChildren(_homeViewGrid))
        {
            var typeName = child.GetType().Name;

            // TabsControl: Tabalonia's tab bar
            if (typeName.Contains("TabsControl") || typeName.Contains("TabControl"))
            {
                _tabsControl = child;
                int row = _r.GetGridRow(child);
                int col = _r.GetGridColumn(child);
                Logger.Log(Tag, $"TabsControl found: {typeName} at Row={row} Col={col}");
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

        if (_tabsControl == null)
            Logger.Log(Tag, "WARNING: TabsControl not found");
        if (_rootSplitView == null)
            Logger.Log(Tag, "WARNING: SplitView not found");

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

    // ===== Server strip =====

    private void BuildAndInjectServerStrip()
    {
        if (_homeViewGrid == null) return;

        // Create the vertical StackPanel for server icons
        _serverStrip = _r.CreateStackPanel(vertical: true, spacing: IconSpacing);
        if (_serverStrip == null)
        {
            Logger.Log(Tag, "Failed to create server strip StackPanel");
            return;
        }
        _r.SetMargin(_serverStrip, 0, 6, 0, 6);
        _r.SetHorizontalAlignment(_serverStrip, "Center");

        // Populate with current tabs
        PopulateServerStrip();

        // Wrap in a ScrollViewer for overflow, then in a Border for styling
        var scrollViewer = _r.CreateScrollViewer(_serverStrip);
        if (scrollViewer != null)
        {
            // Hide scrollbar but allow scrolling
            scrollViewer.GetType().GetProperty("VerticalScrollBarVisibility")
                ?.SetValue(scrollViewer, Enum.Parse(
                    scrollViewer.GetType().GetProperty("VerticalScrollBarVisibility")!.PropertyType,
                    "Hidden"));
            scrollViewer.GetType().GetProperty("HorizontalScrollBarVisibility")
                ?.SetValue(scrollViewer, Enum.Parse(
                    scrollViewer.GetType().GetProperty("HorizontalScrollBarVisibility")!.PropertyType,
                    "Disabled"));
        }

        // Create a border wrapping the scroll viewer
        var stripBg = GetThemedColor("BackgroundSecondary", "#0a1018");
        _serverStripBorder = _r.CreateBorder(stripBg, 0, scrollViewer ?? _serverStrip);
        if (_serverStripBorder == null)
        {
            Logger.Log(Tag, "Failed to create server strip border");
            return;
        }
        _r.SetTag(_serverStripBorder, "rootcord-strip");

        // Place in Grid: Column 0, spanning all rows from Row 0
        _r.SetGridColumn(_serverStripBorder, 0);
        _r.SetGridRow(_serverStripBorder, 0);

        // Span all rows in the grid
        var rowDefs = _r.GetRowDefinitions(_homeViewGrid);
        int rowCount = rowDefs?.Count ?? 4;
        _r.SetGridRowSpan(_serverStripBorder, rowCount);

        _r.AddChild(_homeViewGrid, _serverStripBorder);
        Logger.Log(Tag, $"Server strip injected: Col=0, RowSpan={rowCount}");
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
                var icon = BuildServerIcon(tabVm, tabVm == selectedTab);
                if (icon != null)
                {
                    _r.AddChild(_serverStrip, icon);
                    tabIndex++;
                }
            }
        }

        Logger.Log(Tag, $"Server strip populated: {tabIndex} tab(s)");
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
    private bool IsDmTab(object tabViewModel)
    {
        return tabViewModel.GetType().Name.Contains("DirectMessage");
    }

    /// <summary>
    /// Build a single circular server icon for a tab ViewModel.
    /// </summary>
    private object? BuildServerIcon(object tabViewModel, bool isSelected)
    {
        var displayName = GetTabDisplayName(tabViewModel);
        var isDm = IsDmTab(tabViewModel);

        // Get the first letter for the icon (DM tabs show envelope icon)
        var initial = isDm ? "\u2709" : (displayName.Length > 0 ? displayName[0].ToString().ToUpper() : "?");
        Logger.Log(Tag, $"  Icon: {displayName} -> '{initial}' (DM={isDm}, selected={isSelected})");

        // Icon colors
        var bgColor = GetThemedColor("BackgroundTertiary", "#1a2535");
        var bgHover = ColorUtils.Lighten(bgColor, 12);
        var textColor = GetThemedColor("TextPrimary", "#f2f2f2");
        var accentColor = GetThemedColor("BrandPrimary", "#3B6AF8");

        // Outer container: holds the pill indicator + icon
        var container = _r.CreateStackPanel(vertical: false, spacing: 0);
        if (container == null) return null;
        _r.SetHorizontalAlignment(container, "Center");
        _r.SetTag(container, $"rootcord-icon-{displayName}");

        // Selection pill (Discord-style left indicator)
        var pillBorder = _r.CreateBorder(isSelected ? accentColor : "#00000000", 2);
        if (pillBorder != null)
        {
            _r.SetWidth(pillBorder, PillWidth);
            _r.SetHeight(pillBorder, isSelected ? PillHeight : 8);
            _r.SetVerticalAlignment(pillBorder, "Center");
            _r.SetMargin(pillBorder, 0, 0, 3, 0);
            _r.AddChild(container, pillBorder);
        }

        // Circular icon
        var iconBorder = _r.CreateBorder(isSelected ? accentColor : bgColor, IconCornerRadius);
        if (iconBorder == null) return container;

        _r.SetWidth(iconBorder, IconSize);
        _r.SetHeight(iconBorder, IconSize);
        _r.SetCursorHand(iconBorder);

        // Initial letter
        var text = _r.CreateTextBlock(initial, 18, isSelected ? "#FFFFFF" : textColor);
        _r.SetFontWeight(text, "Bold");
        _r.SetHorizontalAlignment(text, "Center");
        _r.SetVerticalAlignment(text, "Center");
        _r.SetBorderChild(iconBorder, text);

        // Click handler: switch tab
        var capturedVm = tabViewModel;
        var iconRef = iconBorder;
        var pillRef = pillBorder;
        var textRef = text;
        _r.SubscribeEvent(iconBorder, "PointerPressed", () =>
        {
            OnServerIconClicked(capturedVm);
        });

        // Hover effects
        _r.SubscribeEvent(iconBorder, "PointerEntered", () =>
        {
            if (_r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel") != capturedVm)
                _r.SetBackground(iconRef, bgHover);
        });
        _r.SubscribeEvent(iconBorder, "PointerExited", () =>
        {
            if (_r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel") != capturedVm)
                _r.SetBackground(iconRef, bgColor);
        });

        _r.AddChild(container, iconBorder);
        return container;
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
                Logger.Log(Tag, $"Tab selected: {_r.GetPropertyValue(tabViewModel, "DisplayName") ?? "?"}");
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
    /// Update visual highlight on the currently selected tab icon.
    /// </summary>
    private void RefreshSelectedHighlight()
    {
        if (_serverStrip == null || _homeViewModel == null) return;

        var selectedTab = _r.GetPropertyValue(_homeViewModel, "SelectedTabViewModel");
        var accentColor = GetThemedColor("BrandPrimary", "#3B6AF8");
        var bgColor = GetThemedColor("BackgroundTertiary", "#1a2535");
        var textColor = GetThemedColor("TextPrimary", "#f2f2f2");

        var children = _r.GetChildren(_serverStrip);
        if (children == null) return;

        var tabs = _r.GetPropertyValue(_homeViewModel, "Tabs");
        if (tabs == null) return;

        // Build tab list to match by index
        var tabList = new List<object>();
        if (tabs is IEnumerable tabsEnum)
        {
            foreach (var t in tabsEnum)
                if (t != null) tabList.Add(t);
        }

        int idx = 0;
        foreach (var container in children)
        {
            if (container == null) continue;
            bool isSelected = idx < tabList.Count && tabList[idx] == selectedTab;

            // container is a StackPanel with [pill, iconBorder]
            var containerChildren = _r.GetChildren(container);
            if (containerChildren == null || containerChildren.Count < 2) { idx++; continue; }

            var pill = containerChildren[0];
            var iconBorder = containerChildren[1];

            // Update pill
            _r.SetBackground(pill, isSelected ? accentColor : "#00000000");
            _r.SetHeight(pill, isSelected ? PillHeight : 8);

            // Update icon background
            _r.SetBackground(iconBorder, isSelected ? accentColor : bgColor);

            // Update text color
            var iconChild = _r.GetBorderChild(iconBorder);
            if (iconChild != null && _r.IsTextBlock(iconChild))
            {
                _r.SetForeground(iconChild, isSelected ? "#FFFFFF" : textColor);
            }

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

    // ===== Theme integration =====

    /// <summary>
    /// Get a themed color from the ThemeEngine palette, with fallback.
    /// </summary>
    private string GetThemedColor(string key, string fallback)
    {
        if (_themeEngine == null) return fallback;
        try
        {
            var palette = _themeEngine.GetPalette();
            if (palette != null && palette.TryGetValue(key, out var color))
                return color;
        }
        catch { }
        return fallback;
    }
}
