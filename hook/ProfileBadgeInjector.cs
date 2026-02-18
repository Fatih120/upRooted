namespace Uprooted;

/// <summary>
/// Detects profile popups in Root's visual tree and injects an "Uprooted Dev" badge
/// into the roles section. Only active when AutoUpdateChannel == "developer".
///
/// Architecture:
/// - 500ms timer polls all TopLevel windows for new popup/overlay controls
/// - Profile popups identified by presence of username TextBlock + avatar Image pattern
/// - Roles container discovered by looking for pill-style Border+TextBlock pairs
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

                // Attempt to find roles container and inject badge
                var rolesContainer = FindRolesContainer(topLevel);
                if (rolesContainer != null)
                {
                    InjectDevBadge(rolesContainer);
                }
                else
                {
                    Logger.Log("ProfileBadge", "Roles container not found in popup — badge not injected");
                }
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

                    var rolesContainer = FindRolesContainer(child);
                    if (rolesContainer != null)
                        InjectDevBadge(rolesContainer);
                    else
                        Logger.Log("ProfileBadge", "Roles container not found in overlay popup");
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
    /// Find the container holding role badge pills in the profile popup.
    /// Looks for:
    /// 1. A "Roles" or "ROLES" TextBlock header
    /// 2. A sibling or descendant panel containing multiple small Border+TextBlock pairs (role pills)
    /// 3. Fallback: any WrapPanel or horizontal StackPanel with pill-shaped children
    /// </summary>
    private object? FindRolesContainer(object popup)
    {
        // Strategy 1: Find "Roles"/"ROLES" header and look at next sibling
        foreach (var node in _walker.DescendantsDepthFirst(popup))
        {
            if (!_r.IsTextBlock(node)) continue;
            var text = _r.GetText(node);
            if (text == null) continue;
            if (!text.Equals("Roles", StringComparison.OrdinalIgnoreCase) &&
                !text.Equals("ROLES", StringComparison.OrdinalIgnoreCase))
                continue;

            Logger.Log("ProfileBadge", $"Found roles header: \"{text}\" in {node.GetType().Name}");

            // Walk up to find the parent panel, then look for a child panel (the roles container)
            var parent = _r.GetParent(node);
            for (int d = 0; d < 5 && parent != null; d++)
            {
                var children = _r.GetChildren(parent);
                if (children == null || children.Count < 2)
                {
                    parent = _r.GetParent(parent);
                    continue;
                }

                // Find the index of the node (or its ancestor) in this parent
                int rolesHeaderIdx = -1;
                var current = node;
                for (int up = 0; up <= d; up++)
                {
                    rolesHeaderIdx = children.IndexOf(current);
                    if (rolesHeaderIdx >= 0) break;
                    current = _r.GetParent(current);
                }

                if (rolesHeaderIdx >= 0 && rolesHeaderIdx + 1 < children.Count)
                {
                    var candidate = children[rolesHeaderIdx + 1];
                    if (candidate == null) { parent = _r.GetParent(parent); continue; }
                    var candidateType = candidate.GetType().Name;
                    Logger.Log("ProfileBadge", $"Roles container candidate: {candidateType} (sibling after header)");

                    // Verify it contains pill-like children (Border with small height)
                    if (ContainsPills(candidate))
                        return candidate;
                }

                parent = _r.GetParent(parent);
            }
        }

        // Strategy 2: Find any WrapPanel containing pill-like children
        foreach (var node in _walker.DescendantsDepthFirst(popup))
        {
            var typeName = node.GetType().Name;
            if (typeName.Contains("WrapPanel"))
            {
                if (ContainsPills(node))
                {
                    Logger.Log("ProfileBadge", $"Roles container found via WrapPanel scan: {typeName}");
                    return node;
                }
            }
        }

        // Strategy 3: Any horizontal StackPanel or panel with pill children
        foreach (var node in _walker.DescendantsDepthFirst(popup))
        {
            var typeName = node.GetType().Name;
            if (!typeName.Contains("Panel")) continue;

            if (ContainsPills(node))
            {
                Logger.Log("ProfileBadge", $"Roles container found via panel scan: {typeName}");
                return node;
            }
        }

        return null;
    }

    /// <summary>
    /// Check if a container has pill-like children (Border with rounded corners
    /// containing a TextBlock, typically 20-30px tall).
    /// </summary>
    private bool ContainsPills(object container)
    {
        int pillCount = 0;
        foreach (var child in _r.GetVisualChildren(container))
        {
            if (!_r.IsBorder(child)) continue;
            var bounds = _r.GetBounds(child);
            if (bounds == null) continue;

            // Pill badges are typically 20-36px tall, 40-200px wide
            if (bounds.Value.H >= 16 && bounds.Value.H <= 40 && bounds.Value.W >= 30)
            {
                // Check for TextBlock inside
                foreach (var inner in _walker.DescendantsDepthFirst(child))
                {
                    if (_r.IsTextBlock(inner))
                    {
                        pillCount++;
                        break;
                    }
                }
            }
        }
        return pillCount >= 1; // At least one role pill
    }

    /// <summary>
    /// Inject the "Uprooted Dev" badge pill into the roles container.
    /// </summary>
    private void InjectDevBadge(object rolesContainer)
    {
        // Check if badge already exists in this container
        foreach (var node in _walker.DescendantsDepthFirst(rolesContainer))
        {
            if (_r.GetTag(node) == BadgeTag)
            {
                Logger.Log("ProfileBadge", "Badge already present, skipping injection");
                return;
            }
        }

        var badge = CreateBadgePill();
        if (badge == null)
        {
            Logger.Log("ProfileBadge", "Failed to create badge pill");
            return;
        }

        try
        {
            _r.AddChild(rolesContainer, badge);
            Logger.Log("ProfileBadge", "Uprooted Dev badge injected into roles container");
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"Badge injection failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Build the "Uprooted Dev" pill badge matching Root's native role pill style.
    /// Structure:
    ///   Border (pill shape, CR=10, padding 10,4,10,4, bg=#8B691420, border=#8B691460)
    ///     StackPanel (horizontal, spacing=6)
    ///       Border (8x8, CR=4, bg=#8B6914) — color dot
    ///       TextBlock ("Uprooted Dev", size=12, weight=500)
    /// </summary>
    private object? CreateBadgePill()
    {
        // Color dot
        var dot = _r.CreateBorder(BadgeColor, cornerRadius: 4);
        if (dot == null) return null;
        _r.SetWidth(dot, 8);
        _r.SetHeight(dot, 8);

        // Label text
        var label = _r.CreateTextBlock("Uprooted Dev", fontSize: 12, foregroundHex: "#fff2f2f2");
        if (label == null) return null;
        _r.SetFontWeightNumeric(label, 500);
        _r.SetVerticalAlignment(label, "Center");

        // Inner StackPanel (horizontal: dot + label)
        var inner = _r.CreateStackPanel(vertical: false, spacing: 6);
        if (inner == null) return null;
        _r.SetVerticalAlignment(inner, "Center");
        _r.AddChild(inner, dot);
        _r.AddChild(inner, label);

        // Outer pill Border
        var pill = _r.CreateBorder(bgHex: BadgeColor + "20", cornerRadius: 10, child: inner);
        if (pill == null) return null;
        _r.SetPadding(pill, 10, 4, 10, 4);
        _r.SetTag(pill, BadgeTag);

        // Border stroke (using BorderBrush + BorderThickness)
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
