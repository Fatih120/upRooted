# Rootcord

> **What this is:** Rootcord plugin deep dive — Discord-style vertical server sidebar, apply/revert lifecycle, community swap, flyout flipping, user bar, and all the visual tree surgery that makes it tick.

Rootcord transforms Root's horizontal tab bar into a Discord-style vertical server strip on the left side of the window. It is the most invasive plugin in Uprooted — it rewrites the entire HomeView grid layout, rotates community member panels, flips flyout placements, injects a persistent user bar, and monitors overlay popups, all while tracking enough state to cleanly revert to stock Root layout on toggle-off.

> **Status:** Experimental (v0.4.2) — layout + community swap + flyout flip + user bar all working; known gap on deep margin/padding right-gutter in community view
> **Layer:** C# hook (Avalonia-native) — pure visual tree surgery, zero TypeScript
> **File:** [`hook/RootcordEngine.cs`](../../../hook/RootcordEngine.cs)
> **Settings key:** `Plugin.rootcord`

---

## What it does

| Feature | Without Rootcord | With Rootcord |
|---------|-----------------|---------------|
| Navigation | Horizontal tab bar across the top | Vertical server strip on the left (56px wide) |
| Server switching | Click tabs in top bar | Click server icons in left strip |
| Community members sidebar | Left side of chat | Right side of chat |
| Member profile flyouts | Open to the right | Open to the left |
| Utility pane (friends, DMs, notifications) | Opens on the right | Opens on the left (between strip and channel list) |
| User info | In Root's native header | Floating user card at bottom-left with avatar, name, status, and action buttons |

---

## Architecture

Rootcord operates in a single file (`hook/RootcordEngine.cs`, ~3300 lines) and is instantiated once by `StartupHook`. It implements a strict **Apply/Revert** contract — everything done in `Apply()` can be undone in `Revert()`.

### State tracking

Every mutation is logged to a saved-state field before being applied:

| Field | What it saves |
|-------|---------------|
| `_originalTabsRow` | Original grid row of the TabsControl |
| `_originalTabsColSpan` | Original column span of the TabsControl |
| `_originalTabsHeight` | Original height (was Double.NaN if auto) |
| `_originalSvColSpan` | Original column span of the SplitView |
| `_originalSvRow` | Original SplitView row |
| `_originalPanePlacement` | PanePlacement enum value before flip |
| `_savedCommunityColOrders` | Column assignments before community member rotation |
| `_modifiedColSpans` | Extra column-span overrides applied during community swap |
| `_flippedFlyoutIds` | Unique IDs of flyouts that have been flipped |

### HomeView grid layout (confirmed via ILSpy)

Root's HomeView uses a 6-column, 4-row grid:

```
Row 0 │ Header bar (SystemTrayBorder at Col 4)
Row 1 │ TabsControl (ColSpan=6)
Row 2 │ RootSplitView (ColSpan=6) — the main content area
Row 3 │ Minor / empty
```

After Rootcord applies:

```
Col 0  │ Col 1-5
56px   │ rest of window
───────┼────────────────────────────────
server │ (Row 0) Header
strip  │ (Row 1) TabsControl — hidden (Height=0)
       │ (Row 2) RootSplitView — PanePlacement flipped Left
       │ (Row 3) Minor
```

The server strip is injected at `Col=0, RowSpan=all_rows`. The existing grid columns are shifted right by inserting a 56px column at index 0.

---

## Apply / Revert lifecycle

### Apply()

`Apply()` runs through six phases in order. Each phase is self-contained and catches its own exceptions so a partial failure does not abort later phases.

1. **Discover HomeView** — finds the active `HomeView` Grid via `VisualTreeWalker`, locates the `TabsControl` (Tabalonia's tab bar) and `RootSplitView`
2. **Insert strip column** — inserts a new 56px column at index 0 in the HomeView grid; shifts all existing controls' `Grid.Column` values right by 1
3. **Hide tab bar** — sets `Height=0` on the TabsControl so it disappears without removing it from the tree (preserving its bindings)
4. **Build server strip** — constructs the vertical strip UI and injects it at `Col=0`
5. **Build user bar** — constructs the floating user card and injects it at `Col=0, RowSpan=all`
6. **Apply PanePlacement** — flips `RootSplitView.PanePlacement` from `Right` to `Left`
7. **Subscribe tab changes** — watches `SelectedTabViewModel` property on `HomeViewModel` to trigger community member swap on community tab activation

### Revert()

`Revert()` undoes each phase in reverse order:

1. Unsubscribe all event handlers
2. Remove user bar from grid
3. Remove server strip from grid
4. Restore original `Grid.Column` values on all controls shifted in Apply
5. Remove the 56px column from the grid
6. Restore TabsControl height
7. Restore `PanePlacement`
8. Revert community member rotation if active

---

## Server strip

`BuildAndInjectServerStrip()` constructs a 56px-wide `Border` containing:

```
Border (56px, bg color, Row=0..all, Col=0)
  └── Grid (2 rows)
       ├── Row 0: Home button + horizontal separator
       └── Row 1: ScrollViewer → StackPanel of server icons (+ separator above first community)
```

### Home button

`BuildHomeButton()` creates the topmost icon in the strip — a stylized envelope (`☒`) that selects the first DM tab when clicked. It shows an active pill when the current tab is a DM view.

### Server icons

`BuildServerIcon()` creates one icon per tab. Each icon is a 42px circle with:

- **Server icon**: uses the community's `LogoBitmap` if available; falls back to first letter of the community name on a colored circle
- **Selection pill**: a 3×20px rounded rectangle on the left edge; white when selected, dimmed when not
- **Pill animation**: fully white + icon bg = accent color when selected; no pill + normal bg when unselected; short white pill on hover
- **Unread badge**: a small dot in the top-right corner — red for mentions, orange for unread-only
- **Hover tooltip**: shows community name + member count pill (online/total format)

### Strip refresh

`RefreshSelectedHighlight()` updates all icon pill states when the active tab changes. It iterates the strip's server icon borders and applies accent/selected/unread styling based on the current `SelectedTabViewModel`.

---

## Tab monitoring

`SubscribeTabChanges()` subscribes to `HomeViewModel.PropertyChanged` and watches for the `SelectedTabViewModel` property. When it changes:

- **Community tab selected** → call `SwapCommunityMembersToRight()` to rotate columns
- **DM tab selected** → call `RevertCommunityMembersSwap()` to restore original column order

The subscription is stored in `_tabChangeHandler` and unsubscribed during `Revert()`.

Tabs collection changes (`Tabs.CollectionChanged`) also trigger a strip rebuild to handle communities being added or removed at runtime.

---

## Community member panel rotation

When a community tab is active, Root places the channel list (left), chat (middle), and members list (right) in a 3-column inner grid inside the community's content view (`CommunityView`). Rootcord rotates this so the members list appears on the **far right**, closer to the window edge, which is ergonomically correct when the server strip has pushed the entire layout rightward.

### How the swap works

`SwapCommunityMembersToRight()`:

1. Walks the visual tree from `RootSplitView.Content` down to find the inner layout `Grid` that holds the 3 community panels
2. Saves the current `Grid.Column` value of each non-splitter child
3. Determines which child is the members panel (rightmost or by type name `RootBorder`)
4. Rotates the column assignments so members get the highest column index

After rotation, it deep-clears margins and padding at multiple levels (SplitView wrapper → CommunityView → members column host → ScrollViewer inside the host) to eliminate the right-side gap that Avalonia's default spacing introduces.

### Zero-width trailing column guard

After rotation, any column definitions beyond the used columns that have non-zero width are zeroed out. This prevents phantom pixel-width columns from creating a right-side gutter.

### Revert

`RevertCommunityMembersSwap()` restores the saved column assignments and removes any flyout flip subscriptions attached to the community view's `LayoutUpdated` event.

---

## Flyout and tooltip placement flipping

### Why it's needed

Root's member panels use `Flyout` with `Placement=RightEdgeAlignedTop` for profile popups — when the members list was on the left (stock Root), this opened the popup to the right (into the chat area). After rotation, the members list is on the right side of the window, so popups still open right — they now fly off-screen.

### `FlipFlyoutsInTree(root, fromPlacement, toPlacement)`

Walks the visual tree from a root element and flips every `FlyoutBase.AttachedFlyout` whose `Placement` property contains "Right" to the equivalent "Left" placement. Handles both fixed controls (walked once) and virtualized rows (handled by `LayoutUpdated`).

### `FlipTooltipsInTree(root)`

Same pattern for `ToolTip.Placement` — flips `RightEdgeAlignedTop` to `LeftEdgeAlignedTop` on tooltip-bearing controls.

### LayoutUpdated subscription

Member lists in Root use `VirtualizingStackPanel`. When the user scrolls, new member rows are virtualized into view and their flyout placements are set to the default (Right). A `LayoutUpdated` handler is attached to the member panel that re-runs `FlipFlyoutsInTree` each time new rows materialize. The `_flippedFlyoutIds` HashSet prevents re-processing the same control in the same pass; the HashSet is cleared before each LayoutUpdated scan to allow re-flipping controls whose placement may have been reset by Root.

### Overlay popup monitor

For profile cards that appear as standalone overlay popups (injected directly onto the `OverlayLayer` Canvas), `SubscribeOverlayPopupMonitor()` subscribes to the overlay's `Children` collection changes. When a new popup appears with its left edge past 60% of the window width, it is translated leftward so it appears adjacent to (but not off-screen past) the members list.

---

## PanePlacement guard

Root's utility pane (friends list, DMs panel, notifications, profile settings) is housed in `RootSplitView.Pane`. The `PanePlacement` property controls which side the pane opens on.

`ApplyUtilityPanePlacement()` sets `PanePlacement=Left` and then subscribes to `PropertyChanged` on `RootSplitView` to detect if Root internally resets it to `Right`. If a reset is detected, the handler immediately re-asserts `Left` on the UI thread.

`RevertUtilityPanePlacement()` unsubscribes the guard before restoring the original value, so the restore itself does not trigger the guard.

---

## User bar

The user bar is a floating card at the bottom-left of the window. It shows the current user's avatar, display name, online status, and four quick-action buttons.

### Layout

```
Border (240px wide, floating, 12px radius)  ← _userBar
  └── Grid [56px | * | Auto]
       ├── Col 0: Avatar (32px circle) + online status dot
       ├── Col 1: Username (Bold, ellipsis trim) + status label (muted, smaller)
       └── Col 2: Four action buttons (Friends / DMs / Notifications / Settings)
```

The card is positioned as `Col=0, RowSpan=all, ColSpan=all, VerticalAlignment=Bottom, HorizontalAlignment=Left` in the HomeView grid, with an 8px margin from the left and bottom edges to create a floating appearance.

### Action buttons

Each button is a 30×30 icon with a transparent background that highlights on hover. The buttons invoke `HomeViewModel` commands directly:

| Button | Glyph | Command |
|--------|-------|---------|
| Friends | `\uE716` (Contact) | `FriendsPaneToggleCommand` |
| Messages | `\uE8F2` (Chat) | `DirectMessagesPaneToggleCommand` |
| Notifications | `\uEA8F` (Ringer) | `NotificationsPaneToggleCommand` |
| Settings | `\uE713` (Gear) | `ProfilePaneToggleCommand` |

Glyphs are rendered with `Segoe Fluent Icons, Segoe MDL2 Assets, Segoe UI Symbol` as the font family chain, falling back to the system sans-serif if none are available.

### Username resolution

`GetCurrentUsername()` walks a chain of property paths through `HomeViewModel`:

1. `RootSessionAccessor.Session.UserInfoService.SessionUser.Username`
2. `SessionAccessor.Session.SessionUser.Username`
3. `Session.User.Username`
4. Direct properties: `HomeViewModel.Username`, `HomeViewModel.DisplayName`

GUIDs are filtered out — if any path returns a value that parses as a GUID, it is skipped. This prevents the user ID from being shown instead of the display name.

### Profile picture

`TryGetProfileBitmap()` accesses `HomeViewModel.ProfilePictureAsyncBitmapWrapper`. This is an async bitmap loader; the method checks `IsCompleted` before reading `Result.Bitmap`. If not yet loaded (profile picture still downloading), it returns `null` and the avatar falls back to the initial letter.

### User card popup

Clicking the avatar or username opens an overlay popup via `ShowUserCardPopup()`:

- Appears above the user bar, pinned to its left edge
- Contains: large avatar (48px), display name, status selector (▾ dropdown), Settings button, Profile button, and Sign Out button (if `SignOutCommand` exists on HomeViewModel)
- A near-transparent click-outside backdrop covers the full window; clicking it dismisses the popup
- The popup and backdrop are added to `OverlayLayer` so they appear above all other content

### Status selector

The status dropdown in the popup calls `ShowStatusSelector()`, which injects a small inline list of status options (Online, Away, etc.) directly into the popup's content stack. Selecting a status calls `UserInfoService.SetMaxOnlineStatusAsync()` and dismisses the popup.

---

## Visual tree surgery details

Rootcord performs the following concrete mutations to Root's visual tree:

### Column insertion at Apply

```
Before:  [Col 0: SystemTray/header stuff] [Col 1..5: content]
After:   [Col 0: 56px strip] [Col 1: formerly Col 0] [Col 2..6: formerly Col 1..5]
```

Every control previously at `Grid.Column=N` is bumped to `Grid.Column=N+1`. The server strip occupies the new `Col=0`.

### TabsControl suppression

The Tabalonia `TabsControl` at `Row=1` is given `Height=0` and `MinHeight=0` rather than being removed. This preserves its ViewModel bindings (necessary for `SelectedTabViewModel` changes to fire correctly) while making it invisible and non-interactive.

### SplitView pane placement

`RootSplitView.PanePlacement` is set to `Left` via reflection (the property type is an enum). With this setting, when any pane button is activated, the pane slides out between the server strip and the channel list rather than pushing content off the right edge.

### Community column rotation example

For a 3-column community view with col assignments `[channels=0, chat=1, members=2]`:

```
Before:  channels(0) | chat(1) | members(2)
After:   members(0)  | chat(1) | channels(2)  ← No. See below.
```

Actually the swap preserves the channel-chat pair and moves members to the right:

```
Before:  RootBorder@col0 | RootBorder@col1 | Border@col2
After:   RootBorder@col1 | Border@col2     | RootBorder@col0  ← members moved to far right
```

The exact ordering depends on what Root places in each column. The swap code reads the existing assignments, finds the rightmost non-splitter child, and rotates so it ends up at the highest column index.

---

## Initialization

Rootcord is initialized in `StartupHook.Phase5()` with a 30-second startup delay:

```
Phase 5 (30s after Root launch):
  1. Check Plugin.rootcord setting
  2. If enabled: call RootcordEngine.Apply()
  3. Watch for setting changes (handled by StartupHook's periodic poll)
```

The 30-second delay ensures HomeView, the TabsControl, and the RootSplitView are all fully loaded and populated before Rootcord attempts to mutate them.

If `Apply()` fails to find HomeView on the first attempt, it retries up to 5 times with 2-second intervals. This handles slow startup where Avalonia loads the root window but HomeView's grid hasn't been fully composed yet.

### Toggle at runtime

Because of the strict Apply/Revert contract, Rootcord can be toggled live without restarting Root. The settings panel flips `Plugin.rootcord` in `uprooted-settings.ini`, and `StartupHook`'s periodic poll detects the change and calls `Apply()` or `Revert()`.

---

## Settings

Rootcord has no configurable sub-settings. It is toggled on/off via:

| Setting | INI key | Default |
|---------|---------|---------|
| Enable Rootcord | `Plugin.rootcord` | `false` |

---

## Known limitations

- **Right gutter in community view** — a 1–5px gap may appear on the right edge of the members panel after column rotation. Root's SplitView template adds margins at multiple nesting levels (SplitView wrapper → CommunityView → members column → ScrollViewer), and not all levels are cleared in the current deep margin/padding walk. Work is ongoing.
- **Virtualized flyouts** — flyout placement is flipped when rows are virtualized into view via LayoutUpdated, but there is a brief (~1 frame) window where a newly materialized row may show a right-aligned flyout before the flip runs.
- **Overlay popup positioning** — profile cards opened by clicking a member are repositioned by the OverlayMonitor, but the repositioning is heuristic (based on the popup's Canvas.Left vs. half window width). Popups that partially overlap the center may not be repositioned.
- **Crash guard on tab switch** — `RefreshSelectedHighlight()` includes a guard for `IsBorder()` type mismatches (a `Decorator` wrapped in a container was incorrectly typed as `Grid` in early builds). The guard is in place but the underlying cause (Root wrapping icons in a `Decorator` during virtualization) may recur with Root updates.
- **Username fallback** — if none of the property chains in `GetCurrentUsername()` resolve, the card shows "User" as a placeholder. This happens on first startup before the session is fully hydrated.
- **Profile picture async** — `TryGetProfileBitmap()` only returns the bitmap if it has already loaded. If the profile picture is still downloading when the user bar is built, the avatar shows the initial letter. It does not retry.

---

## Diagnostics

All Rootcord log lines are prefixed `[Rootcord]`. Key lines to look for in the hook log:

| Log line | Meaning |
|----------|---------|
| `Apply starting` | Apply() called |
| `HomeView grid found: X cols, Y rows` | Visual tree discovery succeeded |
| `Tab bar hidden` | TabsControl suppressed |
| `Server strip injected` | Strip successfully added to grid |
| `User bar injected` | User bar successfully added to grid |
| `Apply complete` | All phases finished |
| `Tab changed: Community` | Community tab selected, member swap triggered |
| `Tab changed: DM` | DM tab selected, community swap reverted |
| `Swapped community columns` | 3-column rotation applied |
| `Flipped N flyouts` | Flyout placement batch flipped |
| `LayoutUpdated: flipped N flyouts` | LayoutUpdated re-flip fired |
| `PanePlacement guard: re-asserted Left` | Root tried to reset PanePlacement |
| `Apply error: ...` | Phase-level exception (includes message) |
| `Revert complete` | Revert() finished cleanly |

To filter Rootcord entries from the hook log:

```powershell
Get-Content $logPath | Select-String '\[Rootcord\]'
```

Or use the convenience script:

```powershell
powershell -File scripts/watch-log.ps1
```

---

## File map

| File | Purpose |
|------|---------|
| `hook/RootcordEngine.cs` | Full implementation (~3300 lines) |
| `hook/VisualTreeWalker.cs` | DFS visual tree traversal used by Apply() to find HomeView |
| `hook/AvaloniaReflection.cs` | Reflection cache for all UI manipulation |
| `hook/StartupHook.cs` | Phase 5 initialization and periodic toggle check |
| `hook/SESSION_STATE.md` | Recent session context and pending issues |

---

**Canonical for:** Rootcord plugin behavior, Discord-style sidebar implementation, HomeView grid surgery, community member rotation, flyout flipping, user bar, PanePlacement guard
*Rootcord plugin reference. Last updated 2026-02-20.*
