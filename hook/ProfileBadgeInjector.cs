using System.Linq;

namespace Uprooted;

/// <summary>
/// Detects profile popups in Root's visual tree and injects a small "Uprooted Dev"
/// badge directly below the username. Only active when AutoUpdateChannel == "developer".
///
/// Architecture:
/// - 500ms timer polls all TopLevel windows for new popup/overlay controls
/// - Profile popups identified by presence of username TextBlock + avatar Image pattern
/// - Username TextBlock found by largest font size in popup
/// - Badge inserted at username's index+1 in its parent panel (centered, compact)
/// - Badge injected once per popup instance (tagged to prevent duplicates)
/// - Full tree dump logged on first popup detection for discovery/refinement
/// </summary>
internal class ProfileBadgeInjector
{
    private const string ScannedTag = "uprooted-profile-scanned";
    private const string BadgeTag = "uprooted-dev-badge";
    private const int PollIntervalMs = 500;
    private const string BadgeColor = "#8B6914"; // Gold/amber matching dev channel

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;
    private Timer? _timer;
    private int _scanning; // Interlocked reentrancy guard
    private bool _firstDumpDone; // Only dump full tree once per session

    public ProfileBadgeInjector(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _mainWindow = mainWindow;
    }

    public void Initialize()
    {
        Logger.Log("ProfileBadge", "Starting profile popup monitor (500ms poll)");
        _timer = new Timer(OnTimerTick, null, PollIntervalMs, PollIntervalMs);
    }

    private void OnTimerTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        try
        {
            _r.RunOnUIThread(() =>
            {
                try { ScanForProfilePopup(); }
                catch (Exception ex) { Logger.Log("ProfileBadge", $"Scan error: {ex.Message}"); }
                finally { Interlocked.Exchange(ref _scanning, 0); }
            });

            // Safety timeout: release guard if UI thread dispatch stalls
            Task.Delay(3000).ContinueWith(_ =>
            {
                Interlocked.CompareExchange(ref _scanning, 0, 1);
            });
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"OnTimerTick error: {ex.Message}");
            Interlocked.Exchange(ref _scanning, 0);
        }
    }

    private void ScanForProfilePopup()
    {
        // Check all TopLevel windows (MainWindow + PopupRoot instances)
        var topLevels = _r.GetAllTopLevels();
        foreach (var topLevel in topLevels)
        {
            // Skip the main window itself — profile popups are separate TopLevels
            if (topLevel == _mainWindow) continue;

            var typeName = topLevel.GetType().Name;

            // Skip if already scanned this popup instance
            if (_r.GetTag(topLevel) == ScannedTag) continue;

            // Check if this looks like a profile popup
            if (IsProfilePopup(topLevel))
            {
                Logger.Log("ProfileBadge", $"Profile popup detected: {typeName}");
                _r.SetTag(topLevel, ScannedTag);

                // Dump tree structure for discovery (first time only)
                if (!_firstDumpDone)
                {
                    _firstDumpDone = true;
                    DumpPopupTree(topLevel);
                }

                // Inject badge below the username TextBlock
                InjectBadgeUnderUsername(topLevel);
            }
        }

        // Also check the OverlayLayer on the main window (popups may render there)
        var overlay = _r.GetOverlayLayer(_mainWindow);
        if (overlay != null)
        {
            foreach (var child in _r.GetVisualChildren(overlay))
            {
                if (_r.GetTag(child) == ScannedTag) continue;

                if (IsProfilePopup(child))
                {
                    Logger.Log("ProfileBadge", $"Profile popup in overlay: {child.GetType().Name}");
                    _r.SetTag(child, ScannedTag);

                    if (!_firstDumpDone)
                    {
                        _firstDumpDone = true;
                        DumpPopupTree(child);
                    }

                    InjectBadgeUnderUsername(child);
                }
            }
        }
    }

    /// <summary>
    /// Heuristic: a profile popup contains a username TextBlock and an avatar Image
    /// in close proximity, plus potentially a "Roles" section header.
    /// </summary>
    private bool IsProfilePopup(object root)
    {
        bool hasImage = false;
        bool hasLargeText = false;
        bool hasRolesText = false;
        int nodeCount = 0;

        foreach (var node in _walker.DescendantsDepthFirst(root))
        {
            nodeCount++;
            if (nodeCount > 500) break; // Safety limit

            var typeName = node.GetType().Name;

            // Look for Image control (avatar)
            if (typeName == "Image" || typeName == "Ellipse")
            {
                var bounds = _r.GetBounds(node);
                // Avatar is typically 40-120px square
                if (bounds != null && bounds.Value.W >= 30 && bounds.Value.H >= 30)
                    hasImage = true;
            }

            // Look for username-like TextBlock (bold/semibold, medium font size)
            if (_r.IsTextBlock(node))
            {
                var fontSize = _r.GetFontSize(node);
                var text = _r.GetText(node);

                if (fontSize >= 16 && !string.IsNullOrEmpty(text) && text.Length >= 2 && text.Length <= 40)
                    hasLargeText = true;

                if (text != null && text.Equals("Roles", StringComparison.OrdinalIgnoreCase))
                    hasRolesText = true;
                if (text != null && text.Equals("ROLES", StringComparison.OrdinalIgnoreCase))
                    hasRolesText = true;
            }
        }

        // Profile popup: avatar + username, or avatar + "Roles" header
        bool isProfile = hasImage && (hasLargeText || hasRolesText);
        if (isProfile)
            Logger.Log("ProfileBadge", $"Heuristic match: image={hasImage} largeText={hasLargeText} roles={hasRolesText} nodes={nodeCount}");
        return isProfile;
    }

    /// <summary>
    /// Dump the full visual tree of a detected popup for discovery.
    /// This is essential for the first run — we don't know Root's exact structure.
    /// </summary>
    private void DumpPopupTree(object popup)
    {
        Logger.Log("ProfileBadge", "=== PROFILE POPUP TREE DUMP ===");
        Logger.Log("ProfileBadge", $"Root: {popup.GetType().FullName}");
        DumpNode(popup, depth: 0, maxDepth: 12);
        Logger.Log("ProfileBadge", "=== END POPUP TREE DUMP ===");
    }

    private void DumpNode(object node, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;

        var indent = new string(' ', depth * 2);
        var typeName = node.GetType().Name;
        var bounds = _r.GetBounds(node);
        var props = new List<string>();

        if (bounds != null)
            props.Add($"({bounds.Value.W:F0}x{bounds.Value.H:F0} @{bounds.Value.X:F0},{bounds.Value.Y:F0})");

        // Text content
        if (_r.IsTextBlock(node))
        {
            props.Add($"Text=\"{_r.GetText(node)}\"");
            props.Add($"FontSize={_r.GetFontSize(node)}");
            props.Add($"FontWeight={_r.GetFontWeight(node)}");
        }

        // Tag
        var tag = _r.GetTag(node);
        if (tag != null) props.Add($"Tag=\"{tag}\"");

        // Background/CornerRadius for Borders
        if (_r.IsBorder(node))
        {
            try
            {
                var bg = node.GetType().GetProperty("Background")?.GetValue(node);
                if (bg != null) props.Add($"BG={bg}");
                var cr = node.GetType().GetProperty("CornerRadius")?.GetValue(node);
                if (cr != null) props.Add($"CR={cr}");
            }
            catch { }
        }

        // Child count
        var childCount = 0;
        foreach (var _ in _r.GetVisualChildren(node)) childCount++;
        if (childCount > 0) props.Add($"children={childCount}");

        Logger.Log("ProfileBadge", $"{indent}{typeName} {string.Join(", ", props)}");

        foreach (var child in _r.GetVisualChildren(node))
            DumpNode(child, depth + 1, maxDepth);
    }

    /// <summary>
    /// Find the username TextBlock (largest font size in the popup, excluding known labels),
    /// then insert a compact badge immediately after it in its parent panel.
    /// </summary>
    private void InjectBadgeUnderUsername(object popup)
    {
        // Check if badge already exists anywhere in this popup
        foreach (var node in _walker.DescendantsDepthFirst(popup))
        {
            if (_r.GetTag(node) == BadgeTag)
            {
                Logger.Log("ProfileBadge", "Badge already present, skipping injection");
                return;
            }
        }

        // Find the username: largest-font TextBlock that isn't a label or action text
        object? usernameBlock = null;
        double maxFontSize = 0;

        foreach (var node in _walker.DescendantsDepthFirst(popup))
        {
            if (!_r.IsTextBlock(node)) continue;
            var text = _r.GetText(node);
            if (string.IsNullOrEmpty(text) || text.Length < 2 || text.Length > 40) continue;

            // Skip known UI labels
            if (text.Equals("Roles", StringComparison.OrdinalIgnoreCase)) continue;
            if (text.Equals("Notes", StringComparison.OrdinalIgnoreCase)) continue;
            if (text.StartsWith("Click here", StringComparison.OrdinalIgnoreCase)) continue;
            if (text.StartsWith("Message @", StringComparison.OrdinalIgnoreCase)) continue;
            if (text.StartsWith("Uprooted", StringComparison.OrdinalIgnoreCase)) continue;

            var fontSize = _r.GetFontSize(node);
            if (fontSize != null && fontSize >= 14 && fontSize > maxFontSize)
            {
                maxFontSize = fontSize.Value;
                usernameBlock = node;
            }
        }

        if (usernameBlock == null)
        {
            Logger.Log("ProfileBadge", "Username TextBlock not found — badge not injected");
            return;
        }

        Logger.Log("ProfileBadge", $"Found username: \"{_r.GetText(usernameBlock)}\" (fontSize={maxFontSize})");

        // Walk up from the username looking for a VERTICAL panel to insert into.
        // The username sits in a horizontal row; we need the outer vertical container
        // so the badge appears below the name, not beside it.
        var candidate = usernameBlock;
        for (int up = 0; up < 8; up++)
        {
            var parent = _r.GetParent(candidate);
            if (parent == null) break;

            if (IsVerticalPanel(parent))
            {
                var children = _r.GetChildren(parent);
                if (children != null)
                {
                    var idx = children.IndexOf(candidate);
                    if (idx >= 0)
                    {
                        var badge = CreateBadgePill();
                        if (badge == null) return;

                        if (TryInsertChild(parent, badge, idx + 1))
                        {
                            Logger.Log("ProfileBadge", $"Badge inserted below username in {parent.GetType().Name} at index {idx + 1}");
                            return;
                        }
                    }
                }
            }

            candidate = parent;
        }

        Logger.Log("ProfileBadge", "Could not find vertical panel above username — badge not injected");
    }

    /// <summary>
    /// Returns true if the panel lays out its children vertically.
    /// For StackPanel: checks Orientation property.
    /// For other panels: compares Y bounds of first two children.
    /// </summary>
    private bool IsVerticalPanel(object panel)
    {
        var typeName = panel.GetType().Name;

        if (typeName.Contains("StackPanel"))
        {
            try
            {
                var orientation = panel.GetType().GetProperty("Orientation")?.GetValue(panel);
                return orientation?.ToString() == "Vertical";
            }
            catch { return false; }
        }

        // For Grid, DockPanel, etc: check if children stack vertically via bounds
        var children = _r.GetChildren(panel);
        if (children == null || children.Count < 2) return false;
        try
        {
            var b0 = _r.GetBounds(children[0]);
            var b1 = _r.GetBounds(children[1]);
            if (b0 != null && b1 != null)
                return Math.Abs(b1.Value.Y - b0.Value.Y) > Math.Abs(b1.Value.X - b0.Value.X);
        }
        catch { }

        return false;
    }

    /// <summary>
    /// Insert a child into a panel's Children collection at a specific index.
    /// Falls back to appending if Insert is not available.
    /// </summary>
    private bool TryInsertChild(object panel, object child, int index)
    {
        try
        {
            var childrenProp = panel.GetType().GetProperty("Children");
            if (childrenProp == null) return false;

            var children = childrenProp.GetValue(panel);
            if (children == null) return false;

            var countProp = children.GetType().GetProperty("Count");
            var count = countProp != null ? (int)countProp.GetValue(children)! : 0;
            var insertAt = Math.Min(index, count);

            // Try typed Insert(int, IControl) or Insert(int, object)
            var insertMethod = children.GetType().GetMethods()
                .FirstOrDefault(m => m.Name == "Insert" && m.GetParameters().Length == 2);

            if (insertMethod != null)
            {
                insertMethod.Invoke(children, new object[] { insertAt, child });
                return true;
            }

            // Fallback: just append
            var addMethod = children.GetType().GetMethod("Add");
            addMethod?.Invoke(children, new[] { child });
            return true;
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"TryInsertChild error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Build the compact "Uprooted Dev" pill badge displayed below the username.
    /// Smaller than the role pills: tighter padding, smaller font, centered horizontally.
    /// Structure:
    ///   Border (pill, CR=8, padding 7,2,7,2, bg=#8B691420, border=#8B691460, HAlign=Center)
    ///     StackPanel (horizontal, spacing=4, VAlign=Center)
    ///       Border (6x6, CR=3, bg=#8B6914) — color dot
    ///       TextBlock ("Uprooted Dev", size=10, weight=500)
    /// </summary>
    private object? CreateBadgePill()
    {
        // Color dot (smaller: 6x6)
        var dot = _r.CreateBorder(BadgeColor, cornerRadius: 3);
        if (dot == null) return null;
        _r.SetWidth(dot, 6);
        _r.SetHeight(dot, 6);
        _r.SetVerticalAlignment(dot, "Center");

        // Label text (smaller: size 10)
        var label = _r.CreateTextBlock("Uprooted Dev", fontSize: 10, foregroundHex: "#fff2f2f2");
        if (label == null) return null;
        _r.SetFontWeightNumeric(label, 500);
        _r.SetVerticalAlignment(label, "Center");

        // Inner StackPanel (horizontal: dot + label, tighter spacing)
        var inner = _r.CreateStackPanel(vertical: false, spacing: 4);
        if (inner == null) return null;
        _r.SetVerticalAlignment(inner, "Center");
        _r.AddChild(inner, dot);
        _r.AddChild(inner, label);

        // Outer pill Border (smaller padding, centered)
        var pill = _r.CreateBorder(bgHex: BadgeColor + "20", cornerRadius: 8, child: inner);
        if (pill == null) return null;
        _r.SetPadding(pill, 7, 2, 7, 2);
        _r.SetTag(pill, BadgeTag);
        _r.SetHorizontalAlignment(pill, "Center");

        // Border stroke
        try
        {
            var strokeBrush = _r.CreateBrush(BadgeColor + "60");
            if (strokeBrush != null)
                pill.GetType().GetProperty("BorderBrush")?.SetValue(pill, strokeBrush);

            if (_r.ThicknessType != null)
            {
                var thickness = Activator.CreateInstance(_r.ThicknessType, 1.0);
                pill.GetType().GetProperty("BorderThickness")?.SetValue(pill, thickness);
            }
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"Badge border stroke error (non-fatal): {ex.Message}");
        }

        return pill;
    }
}
