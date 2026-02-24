# Dump Session: Rootcord User Bar Pane Snapping
Date: 2026-02-24
Task: User bar must snap to pane right edge when open, channels right edge when closed
Hook files: hook/RootcordEngine.cs

## Types Dumped
| File | Assembly | Full Type | Lines | Why |
|------|----------|-----------|-------|-----|
| HomeView.cs | RootApp.Client.Avalonia | RootApp.Client.Avalonia.UI.Home.HomeView | 1278 | SplitView wiring, grid columns |
| HomeViewModel.cs | RootApp.Client.Avalonia | RootApp.Client.Avalonia.UI.Home.HomeViewModel | 923 | PaneOpen/PaneWidth/togglePane |
| RootSplitView.cs | RootApp.Client.Avalonia | RootApp.Client.Avalonia.Controls.RootSplitView | 12 | Thin subclass of SplitView |

## Key Findings

### HomeView Grid (6 columns, 3 rows)
- Row 0: Banner/Titlebar
- Row 1: TabsControl (Col 0, ColSpan 6)
- Row 2: **RootSplitView** (Col 0, ColSpan 6) — main content + pane

### RootSplitView (the `_rootSplitView`)
- Native: `PanePlacement = Right` (Rootcord changes to `Left`)
- `IsPaneOpen` ← TwoWay → `HomeViewModel.PaneOpen`
- `DisplayMode` ← `HomeViewModel.PaneDisplayService.GlobalPaneDisplayMode`
- `OpenPaneLength` ← `HomeViewModel.PaneWidth`
- `Pane` = RootBorder (bg=Border 0.5px) → Border (bg=BackgroundPrimary) → ContentControl (content=PaneViewModel)
- `Content` = ContentControl (content=SelectedTabViewModel.ContentViewModel)

### Rootcord modifies SplitView position
- `ModifyGridColumns` shifts SplitView from Col 0,Span 6 → **Col 1,Span 5** (Col 0 = strip)
- PanePlacement flipped to Left: pane opens between strip and content

### HomeViewModel pane properties
- `PaneOpen` (bool) — master switch, TwoWay to SplitView.IsPaneOpen
- `ProfileOpen`, `FriendsOpen`, `DirectMessagesOpen`, `NotificationsOpen` (bool) — individual flags
- `PaneWidth` (double, default 320) — bound to SplitView.OpenPaneLength
- `PaneViewModel` (IViewModelBase) — bound to Pane content

### Pane widths by type
| Pane | togglePane index | PaneWidth |
|------|-----------------|-----------|
| Friends | 0 | 320 |
| DMs | 1 | 415 |
| Notifications | 2 | 320 |
| Profile | 3 | 320 |

### Root cause: why TranslatePoint doesn't work for pane detection
The SplitView.DisplayMode is bound to `GlobalPaneDisplayMode` (not `CommunityPaneDisplayMode`).
When display mode is **Overlay** (default when window < 735px), the pane floats ON TOP of content
without pushing it. TranslatePoint on the channels panel gives the same coords regardless of pane state.
ApplyUtilityPanePlacement sets `CommunityPaneDisplayMode` but the SplitView binds to `GlobalPaneDisplayMode`.

### Fix approach
Instead of relying on TranslatePoint shift (which only works in Inline mode), directly measure
the pane element's rendered bounds when PaneOpen=true. Use `_rootSplitView.Pane` → GetBounds →
TranslatePoint to get the pane's actual right edge in HomeViewGrid coords. Use
max(channelsRight, paneRight) as targetRight.
