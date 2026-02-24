# Dump Session: Rootcord User Bar Width Regression
Date: 2026-02-24
Task: Profile card no longer snaps to correct edge and never resizes after ColumnDefinition constraint changes
Hook files: hook/RootcordEngine.cs

## Types Dumped
| File | Assembly | Full Type | Lines | Why |
|------|----------|-----------|-------|-----|
| CommunityViewModel.cs | RootApp.Client.Avalonia | RootApp.Client.Avalonia.UI.Community.CommunityViewModel | 716 | CommunityChannelsWidth TwoWay binding source |

## Key Findings

1. **CommunityChannelsWidth default = 280px** (`new GridLength(280.0)`), reset also goes to 280
2. **Persisted per-user per-community** in LocalDataStore under `[userId, communityId, "CommunityChannelsWidth"]`
3. **Type is GridLength** (value type) with TwoWay binding to ColumnDefinition.Width
4. **OnChanged handler** writes value.Value (double) back to LocalDataStore immediately
5. **Root cause**: If persisted value is 400px (set before Rootcord constraints), ColumnDefinition renders at 350 (MaxWidth clamp) but binding value stays at 400. GridSplitter calculates drag offsets from the binding value (400), creating a 50px "dead zone" where dragging produces no visible change.
6. **Fix**: After setting MinWidth/MaxWidth on the ColumnDefinition, clamp the current Width value to be within the new bounds. This syncs the binding value with the rendered width, eliminating the GridSplitter dead zone.
