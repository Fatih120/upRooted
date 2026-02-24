# Rootcord Recurring Bugs

Active bug tracking for the Discord-style Rootcord sidebar plugin.

---

## Audit Run (2026-02-24, pass 3: runtime-verified)

This pass fixed two additional code defects found via parallel ILSpy + code audit, then built, deployed, and runtime-verified all 7 reported bugs against live Root.

Sources used:
- `research/ilspy-dumps/HomeView.cs` (grid: 6 cols, 4 rows)
- `research/ilspy-dumps/MembersView.cs` (grid: 5 rows, InMenuPanel/OutMenuPanel at row 0)
- `research/ilspy-dumps/RootMemberVisibilitySwitch.cs` (`CommunityMembersSVG`, `ChannelMembersSVG`)
- `research/ilspy-dumps/-AvaloniaResources-GREPONLY.cs` (`UserSVG` confirmed in Light/Dark theme variants)

ILSpy confirmations:
- `HomeView.cs`: main Grid is 6 cols (16px, Auto, 0px, Star, 200px, 16px), 4 rows (Auto, 60px, Star, Auto).
- `MembersView.cs`: OutMenuPanel member-count icon is `UserSVG` DynamicResource, 10x10, Opacity=0.64. Text is FontSize=12, FontWeight=450 (Medium), bound to `Community.IMemberService.MemberCount`, foreground=`TextSecondary`.
- `RootMemberVisibilitySwitch.cs`: community icon = `CommunityMembersSVG` (21x16), channel icon = `ChannelMembersSVG` (14x14). Selected state uses `HighlightNormal` resource.

### New fixes in this pass

1. **Ghost header MinHeight storage/restore** (`InjectChannelsHeader`, `RestoreMembersViewHeader`): rows 0-3 MinHeight was zeroed but never restored on Revert, preventing header expansion after Rootcord toggle off+on. Now stores original MinHeight values in `_originalRowMinHeights` before zeroing and restores them in `RestoreMembersViewHeader`.

2. **Tab reconcile `_layoutTabVm` race** (`TryReconcileCommunityTab`): `_layoutTabVm` was set at the end of `SwapCommunityMembersToRight` but checked in early retries (16-80ms). Now explicitly set inside `TryReconcileCommunityTab` after successful reconciliation, so the "already reconciled" guard works immediately for in-flight retries.

---

## 1. Profile Card Snap Misalignment (Gap or Overextension)

**Status:** VERIFIED FIXED (2026-02-24 pass 3)

**Issue:** User bar could overextend into chat or leave a gap at the channels column right edge.

**Root Cause:** Left edge was pinned to `StripWidth` while right edge came from live panel geometry; this drifted when the channels panel started right of the strip due to layout wrappers or stale references.

**Files:** `hook/RootcordEngine.cs` (`UpdateUserBarWidth`, `RemoveInjectedHeader`)

**Fix:**
- User bar computes both left and right edges from translated channels-panel coordinates in HomeView space.
- Width = `channelsRight - channelsLeft`.
- Header removal clears stale panel references/subscriptions, preventing old geometry from being reused.

---

## 2. Server-Switch Inconsistency (Old Layout Flash, Mixed Collapse State)

**Status:** VERIFIED FIXED (2026-02-24 pass 3)

**Issue:** Switching servers could briefly show old layout and produce inconsistent member-list/header states.

**Root Cause:** Retry tasks and cached references from prior tabs were not generation-guarded; stale handlers could affect the newly selected tab. Additionally, `_layoutTabVm` was set too late in the reconcile flow, allowing duplicate `SwapCommunityMembersToRight` calls.

**Files:** `hook/RootcordEngine.cs` (`HandleSelectedTabChanged`, `ScheduleCommunityTabReconcile`, `TryReconcileCommunityTab`)

**Fix:**
- Monotonic `_tabSwitchGeneration` id guards all retry callbacks.
- Fast retry cadence (0, 16, 40, 80, 130, 200, 320, 500, 800ms) with generation + `ReferenceEquals` identity checks.
- Volatile per-tab refs (`_membersViewGrid`, `_hiddenHeaderPanels`, `_originalRowMinHeights`, `_headerMembersVm`) reset before each reconcile.
- `_layoutTabVm` now set inside `TryReconcileCommunityTab` immediately after successful reconciliation.

---

## 3. Ghost Header Reappears After Ctrl+U / Collapse-Expand

**Status:** VERIFIED FIXED (2026-02-24 pass 3)

**Issue:** MembersView native header (rows 0-3) could flash or reappear after Ctrl+U toggle or server switches.

**Root Cause (multi-layer):**
1. `MenuIn` subscription was not tied to the active `MembersViewModel`; stale callbacks could reapply wrong state.
2. Row MinHeight values were zeroed to prevent auto-expansion but never restored on Revert, causing rows to stay collapsed after Rootcord toggle off+on.

**Files:** `hook/RootcordEngine.cs` (`EnsureMembersMenuInHandler`, `BindMembersMenuInHandler`, `InjectChannelsHeader`, `RestoreMembersViewHeader`)

**Fix:**
- Single-owner bind/unbind for `MenuIn` with stale-VM guard in callback.
- `ReHideMembersViewHeader` + scheduled retries (50, 150, 300, 600ms) after expand.
- `_originalRowMinHeights` stored before zeroing rows 0-3 MinHeight; restored in `RestoreMembersViewHeader`.
- Cleared in `HandleSelectedTabChanged` cleanup alongside `_hiddenHeaderPanels`.

---

## 4. Recreated Header Uses Wrong Member-Count Icon

**Status:** VERIFIED CORRECT (2026-02-24 pass 3: no fix needed)

**Issue:** Reported as using the wrong icon for member count in the injected channels header.

**ILSpy Evidence:** MembersView OutMenuPanel uses `UserSVG` DynamicResource at 10x10, Opacity=0.64. Our `InjectChannelsHeader` calls `CreateNativeSvgImage("UserSVG", 10, 10, 0.64)` which matches exactly.

**Files:** `hook/RootcordEngine.cs` (`InjectChannelsHeader` line ~2202)

**Result:** Icon resource, size, and opacity all match native MembersView behavior. No code change needed.

---

## 5. Channels Column Resize Reliability

**Status:** VERIFIED FIXED (2026-02-24 pass 3)

**Issue:** GridSplitter behavior was inconsistent across server switches; channels column could become non-resizable.

**Root Cause:** Width tracking LayoutUpdated handler and overlay references persisted across tabs, interfering with the splitter's live behavior.

**Files:** `hook/RootcordEngine.cs` (`HandleSelectedTabChanged`, `TeardownUserBarWidthTracking`, `SetupUserBarWidthTracking`)

**Fix:**
- `TeardownUserBarWidthTracking` explicitly called in `HandleSelectedTabChanged` before reconcile.
- Rebinds tracking after the selected tab is fully reconciled.
- User bar `IsHitTestVisible=true` with `ZIndex=10` ensures it doesn't block splitter drag.

---

## 6. Hide Member List Button Visibility / Theme Behavior

**Status:** VERIFIED FIXED (2026-02-24 pass 3)

**Issue:** Titlebar "Hide Member List" button could be invisible or have wrong contrast in some themes.

**Root Cause:** Icon resource drift plus stale collapse state from a wrong VM could leave button dimmed or using a non-theme-variant path.

**Files:** `hook/RootcordEngine.cs` (`InjectTitleBarButton`, `BindMembersMenuInHandler`)

**Fix:**
- `SvgPath` bound via `BindToDynamicResource(btn, "SvgPath", "CommunityMembersSVG")` which auto-flips with theme variant.
- `SvgOpacity=1.0` forced; collapse state dimming handled via control `Opacity` (0.6 collapsed, 1.0 expanded).
- Collapse state tracks only the active server's `MembersViewModel`.

---

## 7. User-Bar Action Buttons Unresponsive

**Status:** VERIFIED FIXED (2026-02-24 pass 3)

**Issue:** Friends, DMs, Notifications, and Settings buttons in the user bar did not respond to clicks (regression).

**Investigation:** `SubscribeEvent` with `PointerPressed` was confirmed working (Avalonia exposes it as a CLR event wrapper on `InputElement`; the same pattern is used for server icon hover/press feedback throughout Rootcord). The regression was a layout/positioning issue: the user bar buttons were present but the user bar's width was not being set correctly after the channels header injection changes, causing the action buttons in Col 4 (Auto-width) to be squeezed off the visible area.

**Root Cause:** `_channelsPanelRef` could become stale after header injection changes, causing `UpdateUserBarWidth` to compute wrong bounds. The `SetupUserBarWidthTracking` call was also using the wrong panel reference when `_channelsPanelRef` was already set from a prior tab.

**Files:** `hook/RootcordEngine.cs` (`SetupUserBarWidthTracking`, `UpdateUserBarWidth`)

**Fix:** Already fixed in pass 2 changes: `TeardownUserBarWidthTracking` resets `_channelsWidthPanel` and `_lastUserBarWidth`/`_lastUserBarLeft` to NaN on each tab switch, and `SetupUserBarWidthTracking` re-discovers the channels panel fresh. The width tracking LayoutUpdated handler ensures the user bar width updates immediately when the channels panel bounds change.

---

## Summary

All 7 reported bugs verified against live Root build:

| # | Bug | Status | Fix Type |
|---|-----|--------|----------|
| 1 | User bar snap misalignment | Verified fixed | Dual-edge TranslatePoint |
| 2 | Server-switch layout drift | Verified fixed | Generation guard + `_layoutTabVm` race fix |
| 3 | Ghost header flash | Verified fixed | MinHeight store/restore + MenuIn bind/unbind |
| 4 | Wrong member-count icon | No fix needed | Already matches native `UserSVG` 10x10 0.64 |
| 5 | Channels not resizable | Verified fixed | Width tracking teardown/rebind per tab |
| 6 | Hide member button invisible | Verified fixed | `CommunityMembersSVG` DynamicResource + opacity |
| 7 | User bar buttons unresponsive | Verified fixed | Stale panel ref + width tracking reset |

### Code changes in pass 3

**`hook/RootcordEngine.cs`:**
- Added `_originalRowMinHeights` field to store MembersView row 0-3 MinHeight before zeroing
- `InjectChannelsHeader`: stores original MinHeight values before zeroing rows 0-3
- `RestoreMembersViewHeader`: restores MinHeight values from stored originals
- `HandleSelectedTabChanged`: nulls `_originalRowMinHeights` in cleanup
- `TryReconcileCommunityTab`: sets `_layoutTabVm = selectedTab` after successful reconcile (prevents duplicate SwapCommunityMembersToRight calls from in-flight retries)
