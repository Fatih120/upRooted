using System.Linq;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Detects profile popups in Root's visual tree and injects recognition badges
/// directly below the username.
///
/// Two badge types:
/// - "Uprooted Dev" (gold): shown only on developer channel, gated to DeveloperUsernames (username) or DeveloperUserIds (UUID)
/// - "Alpha User" (blue): shown on ALL channels. A user qualifies if:
///     (a) their UUID is in AlphaUserIds, OR
///     (b) they appear in the session role cache (_confirmedAlphaIds), OR
///     (c) they have AlphaRoleId in their Roles collection (community context — live check)
///   On first sighting in the alpha community the UUID is cached; all subsequent opens
///   (DMs, other communities) use the cache so the badge appears globally.
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
    private const string NotProfileTag = "uprooted-not-profile"; // Marks confirmed non-profile overlays so retries skip them
    private const string BadgeTag = "uprooted-dev-badge";
    private const string AlphaBadgeTag = "uprooted-alpha-badge";
    private const string PresenceIconTag = "uprooted-presence-icon";
    private const string PresenceIconColor = "#4ADE80"; // Bright emerald green
    private const int FallbackPollMs = 50;
    private const string DevBadgeColor = "#8B6914";   // Gold/amber — developer channel
    private const string AlphaBadgeColor = "#1A6EBD"; // Blue — alpha participants

    /// <summary>
    /// Community ID where the Alpha User role lives. Used by the cross-community
    /// membership check to resolve the user's alpha role when the profile is opened
    /// from a different community context.
    /// </summary>
    private const string AlphaCommunityId = "002d082e-67d2-8e02-ae7d-8021acb2bc48";

    /// <summary>
    /// Role ID that grants the Alpha User badge automatically (community context).
    /// Any user with this role in AlphaCommunityId receives the badge without needing
    /// a UUID entry in AlphaUserIds.
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
    private readonly UprootedPresenceBeacon? _beacon;
    private readonly UserBioEngine? _bioEngine;
    private Timer? _fallbackTimer;
    private readonly TailSampler _sampler = new(heartbeatTicks: 150, slowThresholdMs: 50);
    private int _scanning; // Interlocked reentrancy guard
    private bool _firstDumpDone; // Only dump full tree once per session

    // Session-scoped role cache: UUIDs confirmed via role detection in community context.
    // Populated on first sighting; used for all subsequent profile opens regardless of context
    // (DMs, other communities) so badges appear globally, not just inside the alpha community.
    private readonly HashSet<string> _confirmedAlphaIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<string> _confirmedDevIds   = new(StringComparer.OrdinalIgnoreCase);

    // Cached alpha community object for cross-community membership checks.
    // Resolved once via Session.CommunityService.GetCommunity(AlphaCommunityGuid).
    // Null after resolution = viewer is not a member of the alpha community.
    private bool _alphaCommunityResolved;
    private object? _alphaCommunity;

    // Hover prefetch: track MemberView controls already subscribed to PointerEntered.
    // Uses reference equality to avoid interfering with Root's control lifecycle.
    private readonly HashSet<object> _hoverSubscribed = new(ReferenceEqualityComparer.Instance);
    private Timer? _hoverScanTimer;
    private object? _membersPanelCache; // Cached MembersView container for faster subtree scans

    public ProfileBadgeInjector(AvaloniaReflection resolver, object mainWindow, UprootedPresenceBeacon? beacon = null, UserBioEngine? bioEngine = null)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _mainWindow = mainWindow;
        _beacon = beacon;
        _bioEngine = bioEngine;
    }

    /// <summary>
    /// Removes all injected "Uprooted Dev" badges from all top-level windows.
    /// Used when switching from Developer to Stable channel so the UI updates immediately.
    /// </summary>
    internal static void ClearDevBadges(AvaloniaReflection r)
    {
        try
        {
            int removed = 0;
            var walker = new VisualTreeWalker(r);
            foreach (var top in r.GetAllTopLevels())
                removed += RemoveDevBadgesInSubtree(r, walker, top);
            if (removed > 0)
                Logger.Log("ProfileBadge", $"Cleared {removed} dev badge(s) after channel switch");
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"ClearDevBadges error: {ex.Message}");
        }
    }

    private static int RemoveDevBadgesInSubtree(AvaloniaReflection r, VisualTreeWalker walker, object root)
    {
        var toRemove = new List<object>();
        foreach (var node in walker.DescendantsDepthFirst(root))
        {
            if (r.GetTag(node) == BadgeTag)
                toRemove.Add(node);
        }

        int removed = 0;
        foreach (var node in toRemove)
        {
            try
            {
                var parent = r.GetParent(node);
                if (parent == null) continue;
                r.RemoveChild(parent, node);
                removed++;
            }
            catch { }
        }
        return removed;
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

        // Hover prefetch: subscribe to PointerEntered on MemberView controls
        // so presence + bio data is cached before the user opens a profile popup.
        StartMemberHoverPrefetch();
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
    /// Root sets the popup's DataContext asynchronously after adding it to the overlay.
    /// On first scan, if DataContext is not yet set on MemberProfileView, we subscribe to
    /// its DataContextChanged event for instant re-injection (0ms latency). A 200ms fallback
    /// retry catches edge cases where the event subscription fails.
    /// </summary>
    private void OnOverlayChildrenChanged()
    {
        try
        {
            Logger.Log("ProfileBadge", ">>> OverlayChanged START");
            ScanOverlayLayer(tagNonProfiles: false);  // First scan: don't tag — popup may still be loading
            ScheduleOverlayRetry(200, tagNonProfiles: true); // Safety fallback for slow-loading popups
            Logger.Log("ProfileBadge", "<<< OverlayChanged END");
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"Overlay event scan error: {ex.Message}");
        }
    }

    private void ScheduleOverlayRetry(int delayMs, bool tagNonProfiles = false)
    {
        Task.Delay(delayMs).ContinueWith(_ =>
        {
            _r.RunOnUIThread(() =>
            {
                try { ScanOverlayLayer(tagNonProfiles); }
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
                try
                {
                    using var ev = WideEvent.BeginSampled("ProfileBadge", "fallback_tick", _sampler);
                    ScanTopLevels();
                }
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
            var tag = _r.GetTag(topLevel);
            if (tag == ScannedTag) continue;
            // Fallback poll runs every 200ms — more than enough time for any popup to load.
            // Skip confirmed non-profiles to avoid repeated expensive IsProfilePopup walks.
            if (tag == NotProfileTag) continue;

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
            else
            {
                // Tag as confirmed non-profile so subsequent polls skip this TopLevel.
                _r.SetTag(topLevel, NotProfileTag);
            }
        }
    }

    private void ScanOverlayLayer(bool tagNonProfiles = false)
    {
        var overlay = _r.GetOverlayLayer(_mainWindow);
        if (overlay == null) return;

        foreach (var child in _r.GetVisualChildren(overlay))
        {
            var tag = _r.GetTag(child);
            if (tag == ScannedTag) continue; // Already confirmed as a profile popup
            // On retry scans (tagNonProfiles=true): also skip confirmed non-profiles.
            // On first scan (tagNonProfiles=false): always check — popup may still be loading.
            if (tagNonProfiles && tag == NotProfileTag) continue;

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
            else if (tagNonProfiles)
            {
                // Mark as confirmed non-profile so the 200ms retry skips this child.
                _r.SetTag(child, NotProfileTag);
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

            // Early exit: stop walking once we have conclusive evidence.
            // Avoids scanning remaining nodes when avatar + username are already found.
            if (hasImage && (hasLargeText || hasRolesText)) break;
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
        bool isDevChannel = UprootedSettings.Load().AutoUpdateChannel.Equals("developer", StringComparison.OrdinalIgnoreCase);

        // Determine which badges are already present
        bool devBadgePresent = false;
        bool alphaBadgePresent = false;
        foreach (var node in _walker.DescendantsDepthFirst(popup))
        {
            var tag = _r.GetTag(node);
            if (tag == BadgeTag) devBadgePresent = true;
            if (tag == AlphaBadgeTag) alphaBadgePresent = true;
        }

        // Channel switched to stable while popup is open: remove existing dev badge immediately.
        if (!isDevChannel && devBadgePresent)
        {
            RemoveDevBadgesInSubtree(_r, _walker, popup);
            devBadgePresent = false;
        }

        // Find the username first (DC-independent: TextBlock text is set from XAML template)
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

        // Get DataContext — used for UUID lookup and role check
        var dc = FindProfileDataContext(popup);
        var userId = TryGetUserIdFromDc(dc);

        // DataContext not ready yet: subscribe to DataContextChanged for instant re-injection
        // instead of relying on timer retries. This fires the moment Root sets the DataContext,
        // typically within 0-10ms (vs. the old 80-200ms timer delays).
        if (dc == null)
        {
            Logger.Log("ProfileBadge", "DataContext not ready — subscribing to DataContextChanged for instant injection");
            SubscribeProfileViewDataContextChanged(popup);
            return;
        }

        var username = _r.GetText(usernameBlock) ?? "";
        Logger.Log("ProfileBadge", $"Found username: \"{username}\" (fontSize={maxFontSize}, userId={userId ?? "null"})");

        // Alpha: static UUID list OR session cache OR live role check (community context only).
        bool alphaByRole = HasRoleId(dc, AlphaRoleId);
        if (alphaByRole && userId != null) _confirmedAlphaIds.Add(userId);

        // Cross-community fallback: if the profile was opened from a different community,
        // CommunityMember.Roles only has roles for that community — the alpha role won't appear.
        if (!alphaByRole && userId != null
            && !AlphaUserIds.Contains(userId) && !_confirmedAlphaIds.Contains(userId))
        {
            if (HasAlphaRoleViaCrossContextCheck(dc, userId))
                _confirmedAlphaIds.Add(userId);
        }

        bool isAlphaUser = (userId != null && (AlphaUserIds.Contains(userId) || _confirmedAlphaIds.Contains(userId)))
                           || alphaByRole;

        if (alphaBadgePresent && devBadgePresent)
        {
            Logger.Log("ProfileBadge", "All badges already present, skipping injection");
            return;
        }

        // Dev: same pattern — live role check populates session cache for global badge display.
        bool devByRole = HasRoleId(dc, DeveloperRoleId);
        if (devByRole && userId != null) _confirmedDevIds.Add(userId);
        bool isDevUser = isDevChannel && (DeveloperUsernames.Contains(username)
                         || (userId != null && (DeveloperUserIds.Contains(userId) || _confirmedDevIds.Contains(userId)))
                         || devByRole);
        if (isDevUser) isAlphaUser = false; // Dev badge supersedes alpha

        // Inject presence icon inline next to username (beacon check — independent of badge logic)
        if (_beacon != null && userId != null)
        {
            bool isSelf = string.Equals(userId, _beacon.OwnUuidStr, StringComparison.OrdinalIgnoreCase);
            if (isSelf)
            {
                InjectPresenceIconIntoPopup(popup, usernameBlock);
            }
            else
            {
                TryInjectPresenceIcon(popup, usernameBlock, userId);
            }
        }

        // Check if the viewed user has Uprooted (for bio injection — independent of badge eligibility)
        bool isOwnProfile = userId != null && _beacon != null
            && string.Equals(userId, _beacon.OwnUuidStr, StringComparison.OrdinalIgnoreCase);
        bool isUprootedUser = isOwnProfile || (userId != null && _beacon != null
            && _beacon.TryGetCached(userId) == true);

        // Speculative bio prefetch: when presence is unknown, start the bio HTTP fetch
        // in parallel with the presence query so the bio is cached by the time presence
        // confirms the user has Uprooted. Non-Uprooted users get a cheap 404/empty result.
        if (_bioEngine != null && userId != null && !isOwnProfile && _beacon != null
            && _beacon.TryGetCached(userId) == null)
        {
            _bioEngine.PrefetchBio(userId);
        }

        bool needsBadges = isDevUser || isAlphaUser;
        bool needsBio = _bioEngine != null && isUprootedUser && userId != null;

        if (!needsBadges && !needsBio)
        {
            Logger.Log("ProfileBadge", $"User \"{username}\" has no badges or bio to inject");
            return;
        }

        // Walk up from the username looking for a VERTICAL panel to insert into.
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

                        if (isAlphaUser && !alphaBadgePresent)
                        {
                            var alphaBadge = CreateAlphaBadgePill();
                            if (alphaBadge != null && TryInsertChild(parent, alphaBadge, idx + insertOffset))
                            {
                                Logger.Log("ProfileBadge", $"Alpha badge inserted at index {idx + insertOffset}");
                                insertOffset++;
                            }
                        }

                        if (isDevUser && !devBadgePresent)
                        {
                            var devBadge = CreateDevBadgePill();
                            if (devBadge != null && TryInsertChild(parent, devBadge, idx + insertOffset))
                            {
                                Logger.Log("ProfileBadge", $"Dev badge inserted at index {idx + insertOffset}");
                                insertOffset++;
                            }
                        }

                        if (needsBio)
                        {
                            _bioEngine!.TryInjectBio(popup, parent, idx + insertOffset, userId!);
                        }

                        return;
                    }
                }
            }

            candidate = parent;
        }

        Logger.Log("ProfileBadge", "Could not find vertical panel above username — badges/bio not injected");
    }

    /// <summary>
    /// Subscribe to DataContextChanged on the MemberProfileView inside the popup.
    /// When the DataContext is set (typically within 0-10ms), re-runs the full injection.
    /// This replaces the old 80ms/200ms timer retry approach with event-driven instant detection.
    /// </summary>
    private void SubscribeProfileViewDataContextChanged(object popup)
    {
        try
        {
            // Find MemberProfileView in the popup subtree
            object? profileView = null;
            foreach (var node in _walker.DescendantsDepthFirst(popup))
            {
                if (node.GetType().Name == "MemberProfileView") { profileView = node; break; }
            }
            if (profileView == null && popup.GetType().Name == "MemberProfileView")
                profileView = popup;

            if (profileView == null)
            {
                Logger.Log("ProfileBadge", "MemberProfileView not found — cannot subscribe to DataContextChanged");
                return;
            }

            bool fired = false; // Guard: only fire once per popup
            _r.SubscribeEvent(profileView, "DataContextChanged", () =>
            {
                if (fired) return;
                fired = true;
                try
                {
                    if (_r.GetParent(popup) == null) return;
                    Logger.Log("ProfileBadge", "DataContextChanged fired — injecting immediately");
                    InjectBadgeUnderUsername(popup);
                }
                catch (Exception ex)
                {
                    Logger.Log("ProfileBadge", $"DataContextChanged injection error: {ex.Message}");
                }
            });
            Logger.Log("ProfileBadge", "Subscribed to MemberProfileView.DataContextChanged");
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"SubscribeProfileViewDataContextChanged error: {ex.Message}");
        }
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

    // ===== Cross-community alpha membership check =====

    /// <summary>
    /// Checks whether a user holds AlphaRoleId in the alpha community by querying
    /// Session.CommunityService directly. Used when the profile popup is opened from
    /// a different community where CommunityMember.Roles only contains that community's roles.
    ///
    /// The alpha community object is resolved once and cached. If the viewing user is not
    /// a member of the alpha community, resolution returns null and this check is skipped
    /// on all subsequent calls.
    /// </summary>
    private bool HasAlphaRoleViaCrossContextCheck(object? dc, string userId)
    {
        if (dc == null) return false;
        try
        {
            if (!_alphaCommunityResolved)
            {
                _alphaCommunity = ResolveAlphaCommunity(dc);
                _alphaCommunityResolved = true;
                Logger.Log("ProfileBadge", _alphaCommunity != null
                    ? "Cross-context: alpha community resolved and cached"
                    : "Cross-context: alpha community not accessible (viewer not a member?)");
            }

            if (_alphaCommunity == null) return false;

            bool result = UserHasAlphaRoleInCommunity(_alphaCommunity, userId);
            if (result) Logger.Log("ProfileBadge", $"Cross-context: confirmed alpha role for userId={userId}");
            return result;
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"Cross-context alpha check error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Resolves the alpha community via reflection:
    ///   DataContext._rootSessionAccessor → Session.CommunityService.GetCommunity(AlphaCommunityGuid)
    /// Returns the Community object, or null if the viewing user is not a member of the
    /// alpha community or if any reflection step fails.
    /// </summary>
    private object? ResolveAlphaCommunity(object dc)
    {
        // Walk type hierarchy to find _rootSessionAccessor (private field on MemberProfileViewModel)
        object? rsa = null;
        for (var t = dc.GetType(); t != null; t = t.BaseType)
        {
            var f = t.GetField("_rootSessionAccessor",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (f != null) { rsa = f.GetValue(dc); break; }
        }
        if (rsa == null)
        {
            Logger.Log("ProfileBadge", "Cross-context: _rootSessionAccessor not found on DataContext");
            return null;
        }

        var session = rsa.GetType().GetProperty("Session")?.GetValue(rsa);
        if (session == null)
        {
            Logger.Log("ProfileBadge", "Cross-context: Session is null");
            return null;
        }

        var communityService = session.GetType().GetProperty("CommunityService")?.GetValue(session);
        if (communityService == null)
        {
            Logger.Log("ProfileBadge", "Cross-context: CommunityService not found");
            return null;
        }

        // Find GetCommunity(CommunityGuid) — single-parameter overload
        var getCommunity = communityService.GetType().GetMethods()
            .FirstOrDefault(m => m.Name == "GetCommunity" && m.GetParameters().Length == 1);
        if (getCommunity == null)
        {
            Logger.Log("ProfileBadge", "Cross-context: GetCommunity method not found on CommunityService");
            return null;
        }

        // Construct a CommunityGuid value from AlphaCommunityId string
        var communityGuidType = getCommunity.GetParameters()[0].ParameterType;
        var alphaCommunityGuid = CreateGuidTypeInstance(communityGuidType, AlphaCommunityId);
        if (alphaCommunityGuid == null)
        {
            Logger.Log("ProfileBadge", $"Cross-context: could not construct {communityGuidType.Name} from AlphaCommunityId");
            return null;
        }

        return getCommunity.Invoke(communityService, new[] { alphaCommunityGuid });
    }

    /// <summary>
    /// Constructs an instance of a Guid-wrapper struct (CommunityGuid, UserGuid, etc.)
    /// from a UUID string. Tries constructor(Guid) first, then implicit/explicit operators.
    /// </summary>
    private static object? CreateGuidTypeInstance(Type guidType, string uuidStr)
    {
        if (!Guid.TryParse(uuidStr, out var guid)) return null;

        // Constructor(Guid) — most common pattern for Root's ID types
        var ctor = guidType.GetConstructor(new[] { typeof(Guid) });
        if (ctor != null) return ctor.Invoke(new object[] { guid });

        // Implicit or explicit conversion operator from Guid
        var op = guidType.GetMethod("op_Implicit", BindingFlags.Public | BindingFlags.Static,
                     null, new[] { typeof(Guid) }, null)
               ?? guidType.GetMethod("op_Explicit", BindingFlags.Public | BindingFlags.Static,
                     null, new[] { typeof(Guid) }, null);
        if (op != null) return op.Invoke(null, new object[] { guid });

        return null;
    }

    /// <summary>
    /// Checks whether a user identified by userId holds AlphaRoleId in the given community.
    /// Accesses Community.Members.MembersXGroups.Items, finds the member by GlobalUser.Id,
    /// then checks their Roles.Items for a role whose Id matches AlphaRoleId.
    /// </summary>
    private bool UserHasAlphaRoleInCommunity(object community, string userId)
    {
        try
        {
            var members = community.GetType().GetProperty("Members")?.GetValue(community);
            if (members == null) return false;

            var mxg = members.GetType().GetProperty("MembersXGroups")?.GetValue(members);
            if (mxg == null) return false;

            // DynamicData IObservableList/SourceList exposes Items; fall back to direct cast
            var items = (mxg.GetType().GetProperty("Items")?.GetValue(mxg)
                      ?? mxg) as System.Collections.IEnumerable;
            if (items == null) return false;

            foreach (var item in items)
            {
                if (item == null) continue;

                // MembersXGroups contains both Member and MemberGroup objects — skip groups
                var globalUser = item.GetType().GetProperty("GlobalUser")?.GetValue(item);
                if (globalUser == null) continue;

                var id = globalUser.GetType().GetProperty("Id")?.GetValue(globalUser)?.ToString();
                if (!string.Equals(id, userId, StringComparison.OrdinalIgnoreCase)) continue;

                // Found the target member — check their Roles collection
                var roles = item.GetType().GetProperty("Roles")?.GetValue(item);
                if (roles == null) break;

                var roleItems = (roles.GetType().GetProperty("Items")?.GetValue(roles)
                              ?? roles) as System.Collections.IEnumerable;
                if (roleItems == null) break;

                foreach (var role in roleItems)
                {
                    var roleId = role?.GetType().GetProperty("Id")?.GetValue(role)?.ToString();
                    if (string.Equals(roleId, AlphaRoleId, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                break; // found the member, role not present
            }
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"UserHasAlphaRoleInCommunity error: {ex.Message}");
        }
        return false;
    }

    // ===== Presence icon (inline, horizontal StackPanel next to username) =====

    /// <summary>
    /// Check beacon cache and inject presence icon if the user has Uprooted installed.
    /// If not cached, fires a background query and injects when the result arrives.
    /// </summary>
    private void TryInjectPresenceIcon(object popup, object usernameBlock, string userId)
    {
        if (_beacon == null) return;

        var cached = _beacon.TryGetCached(userId);
        if (cached == true)
        {
            InjectPresenceIconIntoPopup(popup, usernameBlock);
        }
        else if (cached == null)
        {
            // Not cached — fire background query and inject on the UI thread when done.
            // Capture both references; verify popup is still attached before injecting
            // (user may close the profile before the HTTP round-trip completes).
            _beacon.QueryAsync(userId, result =>
            {
                if (!result) return;

                // Presence confirmed — ensure bio is cached (backup in case speculative prefetch missed)
                _bioEngine?.PrefetchBio(userId);

                _r.RunOnUIThread(() =>
                {
                    try
                    {
                        if (_r.GetParent(popup) == null) return; // popup detached — skip
                        InjectPresenceIconIntoPopup(popup, usernameBlock);

                        // Deferred bio injection: presence just confirmed this is an Uprooted user,
                        // so inject bio now if it wasn't injected during the initial badge pass
                        // (which would have skipped bio because isUprootedUser was false).
                        TryDeferredBioInjection(popup, usernameBlock, userId);
                    }
                    catch (Exception ex) { Logger.Log("ProfileBadge", $"Presence icon async inject error: {ex.Message}"); }
                });
            });
        }
        // cached == false → user doesn't have Uprooted, skip
    }

    /// <summary>
    /// Inject bio into a profile popup after the presence callback confirms the user has Uprooted.
    /// Called from the presence callback's UI-thread dispatch. Reuses <see cref="IsVerticalPanel"/>
    /// to walk up from usernameBlock, finds the insert index after any existing badges, then calls
    /// <see cref="UserBioEngine.TryInjectBio"/>. Dedup is safe — TryInjectBio checks BioTag internally.
    /// </summary>
    private void TryDeferredBioInjection(object popup, object usernameBlock, string userId)
    {
        if (_bioEngine == null) return;

        try
        {
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
                            // Scan forward past any existing badges to find the right insert position
                            int insertIdx = idx + 1;
                            while (insertIdx < children.Count)
                            {
                                var childTag = _r.GetTag(children[insertIdx]);
                                if (childTag == BadgeTag || childTag == AlphaBadgeTag)
                                    insertIdx++;
                                else
                                    break;
                            }

                            _bioEngine.TryInjectBio(popup, parent, insertIdx, userId);
                            return;
                        }
                    }
                }

                candidate = parent;
            }
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"TryDeferredBioInjection error: {ex.Message}");
        }
    }

    // ===== Hover prefetch: cache presence + bio data before profile popup opens =====

    /// <summary>
    /// Start periodic scanning for MemberView controls in the members panel.
    /// Subscribes to PointerEntered on each found control to trigger background
    /// prefetch of presence + bio data. When the user clicks to open a profile,
    /// the data is already cached and injection is instant.
    /// </summary>
    private void StartMemberHoverPrefetch()
    {
        if (_beacon == null && _bioEngine == null) return;
        // First scan after 2s, then every 15s to catch new MemberView controls from scrolling
        _hoverScanTimer = new Timer(ScanForMemberViews, null, 2000, 15000);
    }

    private void ScanForMemberViews(object? state)
    {
        _r.RunOnUIThread(() =>
        {
            try
            {
                // Use cached members panel if still in tree; otherwise walk from main window
                object root = _membersPanelCache ?? _mainWindow;
                if (_membersPanelCache != null && _r.GetParent(_membersPanelCache) == null)
                {
                    _membersPanelCache = null;
                    root = _mainWindow;
                }

                int subscribed = 0;
                foreach (var node in _walker.DescendantsDepthFirst(root))
                {
                    var typeName = node.GetType().Name;

                    // Cache the members panel container for faster future scans
                    if (_membersPanelCache == null && typeName == "MembersView")
                        _membersPanelCache = node;

                    if (typeName != "MemberView") continue;
                    if (_hoverSubscribed.Contains(node)) continue;
                    _hoverSubscribed.Add(node);

                    var capturedNode = node;
                    _r.SubscribeEvent(node, "PointerEntered", () => OnMemberHover(capturedNode));
                    subscribed++;
                }
                if (subscribed > 0)
                    Logger.Log("ProfileBadge", $"Hover prefetch: subscribed to {subscribed} MemberView controls (total: {_hoverSubscribed.Count})");
            }
            catch (Exception ex)
            {
                Logger.Log("ProfileBadge", $"ScanForMemberViews error: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Fires when the user hovers over a MemberView in the members panel.
    /// Extracts the user's UUID from MemberViewModel.Member.GlobalUser.Id and
    /// triggers background prefetch for presence + bio. The data is cached so
    /// that when the profile popup opens (typically 100-500ms after hover),
    /// injection uses cache hits instead of waiting for HTTP round-trips.
    /// </summary>
    private void OnMemberHover(object memberView)
    {
        try
        {
            var dc = _r.GetDataContext(memberView);
            if (dc == null) return;

            // MemberViewModel.Member.GlobalUser.Id
            var member = dc.GetType().GetProperty("Member")?.GetValue(dc);
            var globalUser = member?.GetType().GetProperty("GlobalUser")?.GetValue(member);
            var id = globalUser?.GetType().GetProperty("Id")?.GetValue(globalUser);
            var uuid = id?.ToString()?.ToLowerInvariant();
            if (string.IsNullOrEmpty(uuid)) return;

            // Skip if already fully cached
            bool presenceCached = _beacon?.TryGetCached(uuid) != null;
            bool bioCached = _bioEngine?.TryGetCachedBio(uuid) != null;
            if (presenceCached && bioCached) return;

            // Prefetch in background (non-blocking)
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    if (!presenceCached) _beacon?.QueryAsync(uuid, _ => { });
                    if (!bioCached) _bioEngine?.PrefetchBio(uuid);
                }
                catch { }
            });
        }
        catch { }
    }

    /// <summary>
    /// Insert the green Uprooted presence icon immediately after the username TextBlock
    /// in its direct parent (the horizontal StackPanel in MemberProfileView).
    /// Guards against duplicate injection with PresenceIconTag.
    /// </summary>
    private void InjectPresenceIconIntoPopup(object popup, object usernameBlock)
    {
        try
        {
            // Check if icon already present anywhere in popup
            foreach (var node in _walker.DescendantsDepthFirst(popup))
            {
                if (_r.GetTag(node) == PresenceIconTag) return;
            }

            // Get the immediate parent of the username TextBlock (horizontal StackPanel)
            var parent = _r.GetParent(usernameBlock);
            if (parent == null)
            {
                Logger.Log("ProfileBadge", "Presence icon: username parent not found");
                return;
            }

            var children = _r.GetChildren(parent);
            if (children == null)
            {
                Logger.Log("ProfileBadge", "Presence icon: parent has no Children");
                return;
            }

            var idx = children.IndexOf(usernameBlock);
            if (idx < 0)
            {
                Logger.Log("ProfileBadge", "Presence icon: username not in parent children");
                return;
            }

            var icon = CreatePresenceIcon();
            if (icon == null)
            {
                Logger.Log("ProfileBadge", "Presence icon: CreatePresenceIcon returned null");
                return;
            }

            if (TryInsertChild(parent, icon, idx + 1))
                Logger.Log("ProfileBadge", $"Presence icon injected at index {idx + 1} in horizontal panel");
        }
        catch (Exception ex)
        {
            Logger.Log("ProfileBadge", $"InjectPresenceIconIntoPopup error: {ex.Message}");
        }
    }

    /// <summary>
    /// Build the small green Uprooted logo path icon (14×14) displayed inline next to the username.
    /// Path: simplified Uprooted logo (downward arrow/chevron shape), color #4ADE80 (emerald green).
    /// Margin 6,0,0,0 to give a gap from the username text.
    /// </summary>
    private object? CreatePresenceIcon()
    {
        var icon = _r.CreatePathIcon(
            "M16 6 L12 16 L8 26 L12 24 L16 26 L20 24 L24 26 L20 16 Z",
            size: 14,
            foregroundHex: PresenceIconColor);

        if (icon == null) return null;

        _r.SetMargin(icon, 6, 0, 0, 0);
        _r.SetTag(icon, PresenceIconTag);
        _r.SetVerticalAlignment(icon, "Center");
        return icon;
    }

    /// <summary>
    /// Build the compact "Uprooted Dev" pill badge (gold) displayed below the username.
    /// Only injected when on the developer channel for known developer usernames.
    /// </summary>
    private object? CreateDevBadgePill() => CreateBadgePill("$Uprooted Dev_", DevBadgeColor, BadgeTag);

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
