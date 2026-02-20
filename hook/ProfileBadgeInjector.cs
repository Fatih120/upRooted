using System.Linq;

namespace Uprooted;

/// <summary>
/// Detects profile popups in Root's visual tree and injects recognition badges
/// directly below the username.
///
/// Two badge types:
/// - "Uprooted Dev" (gold): shown only on developer channel, gated to DeveloperUsernames (username) or DeveloperUserIds (UUID)
/// - "Alpha User" (blue): shown on ALL channels. A user qualifies if:
///     (a) their UUID is in AlphaUserIds, OR
///     (b) they have AlphaRoleId in their Roles collection (community context only)
///   Role-based detection works automatically for all current role holders without UUID management.
///   UUID list supplements for cross-server / DM contexts where roles aren't visible.
///
/// UUID is read from MemberProfileView.DataContext → MessageContainerMember.GlobalUser.Id.ToString().
/// Roles read from MemberProfileView.DataContext → Roles (ReadOnlyObservableCollection&lt;IViewModelBase&gt;).
///
/// Architecture:
/// - Event-driven: subscribes to OverlayLayer.Children CollectionChanged for instant detection
/// - Fallback: 200ms timer polls TopLevel windows for popups outside the overlay
/// - On CollectionChanged: scan immediately + retry at 80ms and 200ms (DataContext set async)
/// - Profile popups identified by presence of username TextBlock + avatar Image pattern
/// - Username TextBlock found by largest font size in popup
/// - Badges inserted at username's index+1 in its parent panel (centered, compact)
/// - Badges injected once per popup instance (tagged to prevent duplicates)
/// - Full tree dump logged on first popup detection for discovery/refinement
/// </summary>
internal class ProfileBadgeInjector
{
    private const string ScannedTag = "uprooted-profile-scanned";
    private const string BadgeTag = "uprooted-dev-badge";
    private const string AlphaBadgeTag = "uprooted-alpha-badge";
    private const int FallbackPollMs = 200;
    private const string DevBadgeColor = "#8B6914";   // Gold/amber — developer channel
    private const string AlphaBadgeColor = "#1A6EBD"; // Blue — alpha participants

    /// <summary>
    /// Role ID that grants the Alpha User badge automatically (community context).
    /// Any user with this role in community 002d082e-67d2-8e02-ae7d-8021acb2bc48
    /// receives the badge without needing a UUID entry in AlphaUserIds.
    /// </summary>
    private const string AlphaRoleId = "002d0a32-ee9a-840b-8b91-d0bd98e8e693";

    /// <summary>
    /// Role ID that grants the Uprooted Dev badge automatically (community context, dev channel only).
    /// </summary>
    private const string DeveloperRoleId = "002d0850-916d-8f0b-9ed1-25bde2e91854";

    /// <summary>
    /// Known developer usernames who should display the "Uprooted Dev" badge.
    /// </summary>
    private static readonly HashSet<string> DeveloperUsernames = new(StringComparer.OrdinalIgnoreCase)
    {
        "watchthelight",
        "agomusio",
        "terrydavis",
    };

    /// <summary>
    /// Developer UUIDs for cases where matching by username isn't possible.
    /// Checked in addition to DeveloperUsernames — either match grants the dev badge.
    /// </summary>
    private static readonly HashSet<string> DeveloperUserIds = new(StringComparer.OrdinalIgnoreCase)
    {
        "002cf301-7d02-8601-9a1e-348cabc6cf6b",
    };

    /// <summary>
    /// Per-UUID override list for alpha badge. Empty — badge is now fully role-based
    /// via AlphaRoleId. Add individual UUIDs here only to cover DM or cross-server
    /// contexts where the role isn't visible.
    /// </summary>
    private static readonly HashSet<string> AlphaUserIds = new(StringComparer.OrdinalIgnoreCase)
    {
        // Role-based detection handles all cases — add individual UUIDs here only as needed
    };

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;
    private Timer? _fallbackTimer;
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
        // Primary: event-driven detection via OverlayLayer children changes
        _r.RunOnUIThread(() =>
        {
            try
            {
                SubscribeOverlayChildren();
            }
            catch (Exception ex)
            {
                Logger.Log("ProfileBadge", $"Overlay subscription error: {ex.Message}");
            }
        });

        // Fallback: poll TopLevel windows for popups outside the overlay
        Logger.Log("ProfileBadge", $"Starting profile popup monitor (overlay events + {FallbackPollMs}ms fallback poll)");
        _fallbackTimer = new Timer(OnFallbackTick, null, FallbackPollMs, FallbackPollMs);
    }

    /// <summary>
    /// Subscribe to CollectionChanged on OverlayLayer.Children for instant popup detection.
    /// OverlayLayer inherits from Panel; its Children is AvaloniaList which implements
    /// INotifyCollectionChanged. This is a standard .NET event (not a RoutedEvent),
    /// so EventInfo.AddEventHandler works correctly.
    /// </summary>
    private void SubscribeOverlayChildren()
    {
        var overlay = _r.GetOverlayLayer(_mainWindow);
        if (overlay == null)
        {
            Logger.Log("ProfileBadge", "OverlayLayer not found — relying on fallback poll only");
            return;
        }

        // Get the Children collection object
        var childrenProp = overlay.GetType().GetProperty("Children");
        var children = childrenProp?.GetValue(overlay);
        if (children == null)
        {
            Logger.Log("ProfileBadge", "OverlayLayer.Children not accessible — relying on fallback poll only");
            return;
        }

        // Subscribe to CollectionChanged (INotifyCollectionChanged — standard .NET event, not RoutedEvent)
        try
        {
            _r.SubscribeEvent(children, "CollectionChanged", OnOverlayChildrenChanged);
            Logger.Log("ProfileBadge", "Subscribed to OverlayLayer.Children CollectionChanged — instant popup detection active");
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"CollectionChanged subscription failed: {ex.Message} — relying on fallback poll only");
        }
    }

    /// <summary>
    /// Fires immediately when a child is added/removed from the OverlayLayer.
    /// Runs on the UI thread (Avalonia collection events fire on the UI thread).
    ///
    /// Root sets the popup's DataContext asynchronously after adding it to the overlay,
    /// so we scan immediately and then retry at 80ms + 200ms to catch the window where
    /// MemberProfileView.DataContext isn't set yet on the first scan.
    /// </summary>
    private void OnOverlayChildrenChanged()
    {
        try
        {
            ScanOverlayLayer();
            ScheduleOverlayRetry(80);
            ScheduleOverlayRetry(200);
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"Overlay event scan error: {ex.Message}");
        }
    }

    private void ScheduleOverlayRetry(int delayMs)
    {
        Task.Delay(delayMs).ContinueWith(_ =>
        {
            _r.RunOnUIThread(() =>
            {
                try { ScanOverlayLayer(); }
                catch { }
            });
        });
    }

    /// <summary>
    /// Fallback timer: only scans TopLevel windows (not overlay, which is event-driven).
    /// </summary>
    private void OnFallbackTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        try
        {
            _r.RunOnUIThread(() =>
            {
                try { ScanTopLevels(); }
                catch (Exception ex) { Logger.Log("ProfileBadge", $"Fallback scan error: {ex.Message}"); }
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
            Logger.Log("ProfileBadge", $"OnFallbackTick error: {ex.Message}");
            Interlocked.Exchange(ref _scanning, 0);
        }
    }

    private void ScanTopLevels()
    {
        var topLevels = _r.GetAllTopLevels();
        foreach (var topLevel in topLevels)
        {
            if (topLevel == _mainWindow) continue;
            if (_r.GetTag(topLevel) == ScannedTag) continue;

            if (IsProfilePopup(topLevel))
            {
                Logger.Log("ProfileBadge", $"Profile popup detected (TopLevel): {topLevel.GetType().Name}");
                _r.SetTag(topLevel, ScannedTag);

                if (!_firstDumpDone)
                {
                    _firstDumpDone = true;
                    DumpPopupTree(topLevel);
                }

                InjectBadgeUnderUsername(topLevel);
            }
        }
    }

    private void ScanOverlayLayer()
    {
        var overlay = _r.GetOverlayLayer(_mainWindow);
        if (overlay == null) return;

        foreach (var child in _r.GetVisualChildren(overlay))
        {
            if (_r.GetTag(child) == ScannedTag) continue;

            if (IsProfilePopup(child))
            {
                Logger.Log("ProfileBadge", $"Profile popup detected (overlay event): {child.GetType().Name}");
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
    /// then insert recognition badges immediately after it in its parent panel.
    ///
    /// Two badge types are considered:
    /// - Dev badge: injected if on developer channel AND username matches DeveloperUsernames
    /// - Alpha badge: injected if UUID (from DataContext) matches AlphaUserIds (all channels)
    /// </summary>
    private void InjectBadgeUnderUsername(object popup)
    {
        // Determine which badges are already present
        bool devBadgePresent = false;
        bool alphaBadgePresent = false;
        foreach (var node in _walker.DescendantsDepthFirst(popup))
        {
            var tag = _r.GetTag(node);
            if (tag == BadgeTag) devBadgePresent = true;
            if (tag == AlphaBadgeTag) alphaBadgePresent = true;
        }

        // Get DataContext once — used for both UUID lookup and role check
        var dc = FindProfileDataContext(popup);
        var userId = TryGetUserIdFromDc(dc);
        bool isAlphaUser = (userId != null && AlphaUserIds.Contains(userId)) || HasRoleId(dc, AlphaRoleId);

        if (alphaBadgePresent && devBadgePresent)
        {
            Logger.Log("ProfileBadge", "All badges already present, skipping injection");
            return;
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
            Logger.Log("ProfileBadge", "Username TextBlock not found — badges not injected");
            return;
        }

        var username = _r.GetText(usernameBlock) ?? "";
        Logger.Log("ProfileBadge", $"Found username: \"{username}\" (fontSize={maxFontSize}, userId={userId ?? "null"})");

        bool isDevUser = DeveloperUsernames.Contains(username) || (userId != null && DeveloperUserIds.Contains(userId)) || HasRoleId(dc, DeveloperRoleId);

        if (!isDevUser && !isAlphaUser)
        {
            Logger.Log("ProfileBadge", $"User \"{username}\" has no badges to inject");
            return;
        }

        // Walk up from the username looking for a VERTICAL panel to insert into.
        // The username sits in a horizontal row; we need the outer vertical container
        // so badges appear below the name, not beside it.
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
                        int insertOffset = 1;

                        // Inject alpha badge first (appears directly below username)
                        if (isAlphaUser && !alphaBadgePresent)
                        {
                            var alphaBadge = CreateAlphaBadgePill();
                            if (alphaBadge != null && TryInsertChild(parent, alphaBadge, idx + insertOffset))
                            {
                                Logger.Log("ProfileBadge", $"Alpha badge inserted at index {idx + insertOffset}");
                                insertOffset++;
                            }
                        }

                        // Inject dev badge below alpha badge (or directly below username if no alpha)
                        if (isDevUser && !devBadgePresent)
                        {
                            var devBadge = CreateDevBadgePill();
                            if (devBadge != null && TryInsertChild(parent, devBadge, idx + insertOffset))
                            {
                                Logger.Log("ProfileBadge", $"Dev badge inserted at index {idx + insertOffset}");
                            }
                        }

                        return;
                    }
                }
            }

            candidate = parent;
        }

        Logger.Log("ProfileBadge", "Could not find vertical panel above username — badges not injected");
    }

    /// <summary>
    /// Finds MemberProfileView in the popup subtree and returns its DataContext,
    /// or null if the view isn't found or DataContext isn't set yet.
    /// Called once per injection attempt; result shared between UUID and role checks.
    /// </summary>
    private object? FindProfileDataContext(object popup)
    {
        try
        {
            object? profileView = null;
            foreach (var node in _walker.DescendantsDepthFirst(popup))
            {
                if (node.GetType().Name == "MemberProfileView") { profileView = node; break; }
            }
            if (profileView == null && popup.GetType().Name == "MemberProfileView")
                profileView = popup;
            if (profileView == null)
            {
                Logger.Log("ProfileBadge", "FindProfileDataContext: MemberProfileView not found (DataContext not ready yet — retries will catch it)");
                return null;
            }
            return profileView.GetType().GetProperty("DataContext")?.GetValue(profileView);
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"FindProfileDataContext error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Reads the viewed user's UUID from a MemberProfileViewModel DataContext via:
    ///   DataContext.MessageContainerMember.GlobalUser.Id.ToString()
    /// Returns lowercase UUID string, or null if not accessible.
    /// </summary>
    private string? TryGetUserIdFromDc(object? dc)
    {
        if (dc == null) return null;
        try
        {
            var member = dc.GetType().GetProperty("MessageContainerMember")?.GetValue(dc);
            var globalUser = member?.GetType().GetProperty("GlobalUser")?.GetValue(member);
            var id = globalUser?.GetType().GetProperty("Id")?.GetValue(globalUser);
            return id?.ToString()?.ToLowerInvariant();
        }
        catch { return null; }
    }

    /// <summary>
    /// Checks whether the viewed user holds a specific role by inspecting
    /// MemberProfileViewModel.Roles (ReadOnlyObservableCollection&lt;IViewModelBase&gt;).
    /// Only works in community context (CommunityMember is null in DMs).
    /// Each role ViewModel is reflected for Id directly, or via a nested Role.Id property.
    /// </summary>
    private bool HasRoleId(object? dc, string roleId)
    {
        if (dc == null) return false;
        try
        {
            var vmRoles = dc.GetType().GetProperty("Roles")?.GetValue(dc);
            if (vmRoles is not System.Collections.IEnumerable roles) return false;
            foreach (var roleVm in roles)
            {
                if (roleVm == null) continue;
                // Try direct Id property on the ViewModel
                var id = roleVm.GetType().GetProperty("Id")?.GetValue(roleVm)?.ToString();
                if (id != null && id.Equals(roleId, StringComparison.OrdinalIgnoreCase))
                    return true;
                // Try via nested Role.Id (ViewModel wrapping a Role model)
                var inner = roleVm.GetType().GetProperty("Role")?.GetValue(roleVm);
                if (inner != null)
                {
                    id = inner.GetType().GetProperty("Id")?.GetValue(inner)?.ToString();
                    if (id != null && id.Equals(roleId, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"HasRoleId error: {ex.Message}");
        }
        return false;
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
    /// Build the compact "Uprooted Dev" pill badge (gold) displayed below the username.
    /// Only injected when on the developer channel for known developer usernames.
    /// </summary>
    private object? CreateDevBadgePill() => CreateBadgePill("Uprooted Dev", DevBadgeColor, BadgeTag);

    /// <summary>
    /// Build the compact "Alpha User" pill badge (blue) displayed below the username.
    /// Injected on all channels when the viewed user's UUID is in AlphaUserIds.
    /// </summary>
    private object? CreateAlphaBadgePill() => CreateBadgePill("Alpha User", AlphaBadgeColor, AlphaBadgeTag);

    /// <summary>
    /// Build a compact pill badge with the given label, color, and tag.
    /// Structure:
    ///   Border (pill, CR=8, padding 7,2,7,2, bg=color20, border=color60, HAlign=Center)
    ///     StackPanel (horizontal, spacing=4, VAlign=Center)
    ///       Border (6x6, CR=3, bg=color) — color dot
    ///       TextBlock (label, size=10, weight=500)
    /// </summary>
    private object? CreateBadgePill(string label, string color, string tag)
    {
        // Color dot (6x6)
        var dot = _r.CreateBorder(color, cornerRadius: 3);
        if (dot == null) return null;
        _r.SetWidth(dot, 6);
        _r.SetHeight(dot, 6);
        _r.SetVerticalAlignment(dot, "Center");

        // Label text
        var text = _r.CreateTextBlock(label, fontSize: 10, foregroundHex: "#fff2f2f2");
        if (text == null) return null;
        _r.SetFontWeightNumeric(text, 500);
        _r.SetVerticalAlignment(text, "Center");

        // Inner StackPanel (horizontal: dot + label)
        var inner = _r.CreateStackPanel(vertical: false, spacing: 4);
        if (inner == null) return null;
        _r.SetVerticalAlignment(inner, "Center");
        _r.AddChild(inner, dot);
        _r.AddChild(inner, text);

        // Outer pill Border
        var pill = _r.CreateBorder(bgHex: color + "20", cornerRadius: 8, child: inner);
        if (pill == null) return null;
        _r.SetPadding(pill, 7, 2, 7, 2);
        _r.SetTag(pill, tag);
        _r.SetHorizontalAlignment(pill, "Center");

        // Border stroke
        try
        {
            var strokeBrush = _r.CreateBrush(color + "60");
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
