using System.Linq;

namespace Uprooted;

/// <summary>
/// Detects profile popups in Root's visual tree and injects recognition badges
/// directly below the username.
///
/// Two badge types:
/// - "Uprooted Dev" (gold): shown only on developer channel, gated to DeveloperUsernames (username) or DeveloperUserIds (UUID)
/// - "Alpha User" (blue): shown on ALL channels, gated to AlphaUserIds (UUID match — stable across renames)
///
/// UUID is read from MemberProfileView.DataContext → MessageContainerMember.GlobalUser.Id.ToString().
/// This is the canonical stable identifier Root uses internally (not affected by username changes).
///
/// Architecture:
/// - Event-driven: subscribes to OverlayLayer.Children CollectionChanged for instant detection
/// - Fallback: 500ms timer polls TopLevel windows for popups outside the overlay
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
    /// UUIDs (UserGuid.ToString()) of alpha/experimental build participants.
    /// UUID-based so the badge survives username changes.
    /// Format: lowercase UUID string, e.g. "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
    /// </summary>
    private static readonly HashSet<string> AlphaUserIds = new(StringComparer.OrdinalIgnoreCase)
    {
        "002a38cb-bb10-8601-a1da-23b218d4ae15",
        "002c3d33-6818-8a01-a97d-bed654f161a6",
        "002cec0a-81d3-8c01-ad41-b4fa06bdcaab",
        "002ce720-4be2-8701-96b8-cc3466f6f4b9",
        "002ce787-7e77-8601-a2a2-61a4acca6a67",
        "002ce9f4-1dca-8201-b6f9-44555f266188",
        "002cea01-2575-8301-8cb9-bd4783ee77c4",
        "002cea08-4244-8801-a6f2-cfd8b6ec4600",
        "002cea96-e368-8801-82f6-af0350f9ed72",
        "002cec7b-202d-8401-9123-f7ba22261d7a",
        "002cefdb-8b30-8d01-8df5-736cd771096b",
        "002cf11b-8ee6-8a01-96ed-c631439fca4b",
        "002cf276-0f42-8901-9220-e9d5eb724e6d",
        "002cf3b1-fc6d-8e01-b5cd-0f0f30961854",
        "002cf5d0-b964-8e01-8589-90e376873f86",
        "002cffd4-f23b-8501-ab4d-5b6c64563b5c",
        "002d04eb-8161-8c01-9950-f74b8415c7bd",
        "002d062b-e032-8401-9da4-daf8b70cf166",
        "002cefdf-5e47-8f01-bdd9-815e8c9ef50e",
        "002d0eee-40e7-8801-9202-5b5c676bb9e8",
        "002ced8c-6828-8d01-b87a-a6a691b07323",
        "002d0606-6469-8a01-96b4-2f6524c5ff91",
        "002ce6ef-6d3a-8101-ab2d-77895a58c9ef",
        "002c7dc3-c9ba-8101-b981-1564e838ddc2",
        "002ceaf6-71e0-8701-b13a-ca678ca79787",
        "002d1425-4ec6-8801-b38e-4c09c5ff0e6e",
        "0029fae6-91e1-8a01-a6df-b38b1e9c0893",
        "002d130c-27b2-8701-8a90-a63fc955e833",
        "002d1057-5156-8201-9f6d-b76dda6cba0c",
        "002ce7ed-fbe5-8801-bf3d-52a47ba31fc1",
        "002cf06f-8bd8-8f01-8209-e4005ccbc67a",
        "0029af3f-0b29-8601-88c7-e130cbecf963",
        "002d0034-c6ea-8901-bb10-cff05e34b266",
        "002cffc5-c637-8901-ba08-fb8806e937b9",
        "002d1993-8ac5-8c01-8079-e2034f69f230",
        "002d04f1-d744-8501-ae58-c19b7ad3e516",
        "002d0e00-c2bd-8101-b328-8ac2effba2f6",
        "002d0380-c0f9-8a01-af73-b51172fb1493",
        "002cf301-7d02-8601-9a1e-348cabc6cf6b",
        "002d0a06-8ba0-8501-9bb7-5766b44407e8",
        "002c8d10-b651-8301-b062-39ebcbdb68bb",
    };

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;
    private readonly bool _isDevChannel;
    private Timer? _fallbackTimer;
    private int _scanning; // Interlocked reentrancy guard
    private bool _firstDumpDone; // Only dump full tree once per session

    public ProfileBadgeInjector(AvaloniaReflection resolver, object mainWindow, bool isDevChannel)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _mainWindow = mainWindow;
        _isDevChannel = isDevChannel;
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

        // Try to get the user's UUID from the DataContext for alpha badge check
        var userId = TryGetUserIdFromPopup(popup);
        bool isAlphaUser = userId != null && AlphaUserIds.Contains(userId);

        if (alphaBadgePresent && (!_isDevChannel || devBadgePresent))
        {
            Logger.Log("ProfileBadge", "All applicable badges already present, skipping injection");
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

        bool isDevUser = _isDevChannel && (DeveloperUsernames.Contains(username) || (userId != null && DeveloperUserIds.Contains(userId)));

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
    /// Walk the popup's visual tree to find a MemberProfileView control, then read its
    /// DataContext to extract the viewed user's UUID via:
    ///   DataContext.MessageContainerMember.GlobalUser.Id.ToString()
    ///
    /// Returns the UUID string (lowercase) or null if not accessible.
    /// This is the stable identifier — unaffected by username changes.
    /// </summary>
    private string? TryGetUserIdFromPopup(object popup)
    {
        try
        {
            // Find MemberProfileView anywhere in the subtree
            object? profileView = null;
            foreach (var node in _walker.DescendantsDepthFirst(popup))
            {
                if (node.GetType().Name == "MemberProfileView")
                {
                    profileView = node;
                    break;
                }
            }

            // Also try the popup itself (sometimes the popup root IS the view)
            if (profileView == null && popup.GetType().Name == "MemberProfileView")
                profileView = popup;

            if (profileView == null)
            {
                Logger.Log("ProfileBadge", "TryGetUserIdFromPopup: MemberProfileView not found in subtree");
                return null;
            }

            // Read DataContext
            var dc = profileView.GetType().GetProperty("DataContext")?.GetValue(profileView);
            if (dc == null) return null;

            // DataContext.MessageContainerMember
            var member = dc.GetType().GetProperty("MessageContainerMember")?.GetValue(dc);
            if (member == null) return null;

            // .GlobalUser
            var globalUser = member.GetType().GetProperty("GlobalUser")?.GetValue(member);
            if (globalUser == null) return null;

            // .Id → UserGuid → .ToString()
            var id = globalUser.GetType().GetProperty("Id")?.GetValue(globalUser);
            if (id == null) return null;

            return id.ToString()?.ToLowerInvariant();
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"TryGetUserIdFromPopup error: {ex.Message}");
            return null;
        }
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
