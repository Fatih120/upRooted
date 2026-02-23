# Next Release

> Changes included in v0.5.0. This file is replaced each release.

<!-- NEVER mention obfuscation, ConfuserEx, protected names, or the obfuscation pipeline in this file. This content flows to public release notes. -->

---

# What's New in v0.5.0

This is a big one. Since the last release we shipped a new plugin, reworked the entire custom theme system, rebuilt Rootcord's user card, added a presence beacon with community badges, overhauled the settings UI, and made startup meaningfully faster. A lot happened.

---

## New: Translate Plugin

A translate button now appears directly in Root's message compose bar. Click it to pick a target language, and Uprooted rewrites your draft in place before you send. The engine starts 5 seconds after Root launches and self-gates on the plugin toggle, so you can turn it on and off without a restart. It shows up in Plugin Settings under the Experimental tier.

---

## New: Presence Beacon and Community Badges

Uprooted now runs a background presence beacon that registers with the Uprooted API 10 seconds after startup. This powers two visible features:

- **Role-based badges on profile popups** - Developers get a Dev badge, alpha testers get an Alpha badge. Dev takes priority if someone holds both roles. Badges are visible to all Uprooted users.
- **Session role cache** - Roles are fetched once per session and cached, so badge injection is instant the moment any profile popup opens. Detection is event-driven via `OverlayLayer.Children.CollectionChanged`, with a 500ms fallback poll for popups that bypass the overlay system.

---

## Updated: Rootcord

Several significant fixes and improvements to the Discord-style vertical server sidebar plugin since v0.4.2:

**User card rebuilt from scratch.** The old approach tried to reparent Root's native SystemTray border, which was fragile and frequently broke. The new user card is fully custom: the avatar and username area are click targets that open the profile pane, and a 4-button cluster (Friends, Direct Messages, Notifications, Settings) uses native `HomeViewModel` commands directly.

**Crash on server icon click fixed.** `RefreshSelectedHighlight()` was throwing on every server icon click because a `Decorator` appeared in the server strip container where a `Border` was expected. Type guards and per-section try/catch blocks now handle this correctly. The crash is gone.

**Member count tooltips.** Server icon tooltips now show online and total member counts below the server name, using Root's native dot-and-text style from `CommunityTabViewModel`.

**Live toggle.** Rootcord can be enabled and disabled without restarting Root. The engine always initializes a dormant instance at startup so the toggle can apply immediately.

---

## Updated: Theme Engine

The custom theme system is now a full Theme Engine. Renamed across the UI — sidebar tab, page header, plugin card, and About status field all say Theme Engine.

**8 presets.** Four new presets join the original four: Marine (deep ocean blue), Oreo (monochrome lavender), Sakura (the first light preset — cherry blossom pink), and Ember (warm charcoal with burning orange). The preset grid is now two rows of four.

**Light theme support.** Custom themes and presets with light backgrounds now fully adapt: borders darken instead of lighten, text goes dark, highlights use black-alpha overlays, Info/Warning switch to readable amber/orange, mentions and shadows adjust, and SVGs swap to the dark-icon set automatically.

**Accent-aware text contrast.** Buttons with accent-colored backgrounds (version badge, Check for Updates, channel badge, etc.) now pick black or white text based on accent luminance. Toggle switch thumbs do the same. Handles extreme accents like white or pastel that Root's native blue-only design never needed to consider.

**Loki reworked.** Gold is now the primary accent (buttons, badges, mentions). Green moves to secondary brand tones. The "Gold and green" identity is fully realized.

**Keystroke apply.** Themes apply live on every keystroke. Full OKLCH lightness range, smooth direction-aware derivation, custom text color input, tag-based visual tree walker, no-recolor island for the custom theme card, and variant switching for light backgrounds.

---

## Updated: Settings UI Polish

A comprehensive overhaul of the settings pages to match Root's native visual style:

**Cards-in-a-card layout.** Preset and custom theme sections sit inside outer container cards. Inner theme cards use second-order styling: slightly lighter background, thicker borders, and equal-width Grid columns instead of fixed pixel widths.

**DPI-aware borders.** `ThinBorder` (1 physical pixel) and `ThickBorder` (next pixel boundary above thin) are computed from `RenderScaling` at startup. Borders are always visually distinct at 100%, 125%, 150%, and 200% DPI.

**Vector icons.** Plugin card gear and info icons now use `Shapes.Path` with `Stretch.Uniform` and Material Design 24x24 SVG paths. The previous `PathIcon` approach did not scale geometry to fill the container.

**Typography.** Page titles are 20px Bold. Section headers are 14px Bold with `TextPrimary` color. Bold now actually works via `SetFontWeight("Bold")` - the old `SetFontWeightNumeric(600)` path silently failed on Avalonia's trimmed assembly.

**Radio indicators.** Theme card radio selectors are now two sibling Borders in a Grid (18x18 ring + 10x10 centered dot). The math ensures `(18-10) * scale` is always even, giving pixel-perfect centering at both 100% and 150% DPI.

**Toggle switch.** Thumb reduced from 18px to 16px with a 4px margin (was 3px), keeping `(24-16) * scale` even at all common DPI scales.

**Plugin filter.** The 3-option dropdown was replaced by a cycling pill button. Click through: All Plugins, Enabled, Disabled. Each state has a distinct color and a 1.5px border that updates on cycle.

**Plugin sort order.** Stability tier now takes priority over enabled state. Stable plugins always appear before Beta plugins regardless of toggle state.

**Restart banners.** Both the plugin-change and update-applied banners now use a title + subtitle layout with burnt orange tint (`#2A1D15` background, `#D06818` border), distinct from the amber experimental warning color.

**Experimental plugins banner.** When the toggle is off, the banner uses theme colors. When on, it switches to hardcoded amber with a warning icon.

**Native theme settings button.** The "Default" preset theme card is now labeled "Native" and has a gear button in the bottom-right corner. Clicking it navigates directly to Root's native Change Theme settings page. Navigation uses the proper ViewModel-driven binding chain (`ListBox.SelectedItem` → `SelectedMenuItemPageContainerProperty` → `SelectMenuItem()`).

**Nav item borders.** Sidebar nav items now have visible 1px resting borders using Root's `HighlightLight`, `HighlightNormal`, and `HighlightStrong` resources. Adapts to both dark and light theme variants automatically.

---

## Updated: Overlay Scrollbar

Settings pages now use `CreateOverlayScrollViewer`, which relocates the vertical scrollbar into the content Grid column via a deferred `LayoutUpdated` handler. This matches Root's native `RootScrollViewer` overlay behavior exactly, eliminating the content width displacement that a standard `ScrollViewer` caused.

---

## Updated: Settings Detection and Header Fixes

**Instant detection.** The `LayoutUpdated` throttle was lowered from 500ms to 50ms and the reentracy guard was removed from the `LayoutUpdated` path. Settings open detection is now near-instant, including after rapid close and reopen cycles.

**Back arrow.** Now found by child order in the header Grid rather than by bounds or text matching. Hidden using a collapse pattern (Opacity, MaxWidth, MaxHeight, Width, and Margin all zeroed) instead of toggling `IsVisible`, which avoids conflicting with Root's data binding.

**Auto-nav guard.** A `_hasAutoNavigated` flag ensures the settings page only auto-navigates to the About tab once per open, not on every Root theme variant change.

---

## Updated: Theme Switch Performance and Live Recolor

Three architectural changes eliminate theme switch lag and fix live preview desync:

**In-place theme switching.** Switching between Uprooted themes (Crimson to Cosmic Smoothie, preset to custom, etc.) no longer flashes Root's default colors. The old path called `RevertTheme()` before every apply, removing all overrides and re-adding them (two full resolution passes). The new `PrepareForNewTheme` removes only stale ThemeDictionary keys — keys in the old theme but not the new one — while shared keys stay in place for overwrite. Full `RevertTheme` only runs when reverting to Native (correct there).

**Bind-once walker.** The visual tree walker's untagged-Foreground handler was destructively overwriting DynamicResource bindings on Root's native TextBlocks every 100ms via `prop.SetValue`. Controls then stopped responding to ThemeDictionary updates until the next walk pass. Now the walker detects untagged controls, binds them to the appropriate DynamicResource key (`TextPrimary`, `TextSecondary`, `TextTertiary`) via `BindToDynamicResource`, and tags them with `dyn-fg:` so subsequent walks use the efficient tag-based path. Once bound, controls auto-update from ThemeDictionary changes with zero walker involvement.

**O(n) live preview.** Live custom theme preview (dragging color sliders) previously walked the full visual tree on every 100ms tick — ~500+ nodes to find ~16 dyn-tagged controls. Now a `WeakReference<object>` list tracks discovered dyn-tagged controls. During live preview, `UpdateDynTaggedControlsFromPalette` iterates this list directly — O(~16) iterations instead of O(500+). Full tree walks still run on non-live paths (theme apply, sidebar injection) to discover new controls and populate the list.

**Computed palette keys.** Two new derived keys — `BackgroundButtonOnElevated` and `BackgroundButtonOnSecondary` — are luminance-aware highlight surfaces for gear/info buttons. These are bound via DynamicResource, so button backgrounds update live during color slider drag without waiting for a walker pass.

---

## Updated: Startup Performance

Four optimizations reduce the time between Root launching and Uprooted being active:

**Phase 0 runs in parallel.** HTML patch verification fires on a ThreadPool thread immediately. Phase 1 (waiting for Avalonia assemblies) starts without waiting for the filesystem work to finish.

**Faster readiness polling.** The `WaitFor` loop for detecting `Application.Current` and `MainWindow` polls every 50ms instead of 500ms. This removes up to ~900ms of worst-case idle wait across the two phases.

**Diagnostic scans gated to dev channel.** `BrowserDiscovery.DumpAllFindings()` and `DumpVisualTreeColors()` now only run when the update channel is set to `developer`. Both previously ran on the UI thread 10 seconds after every startup for all users.

**Single settings read for plugin phases.** Phases 4.5a through 4.5i each called `UprootedSettings.Load()` individually. They now share the `savedSettings` instance already loaded in Phase 3.5.

---

## Refactored: Wide Event Structured Logging

The entire hook logging system was refactored from freeform string messages to structured wide events, following the [loggingsucks.com](https://loggingsucks.com) methodology. ~1200 individual `Logger.Log` calls across 11 files were consolidated into ~100 wide events, each emitting a single structured line with machine-parseable key=value pairs.

**New log format:**
```
[HH:mm:ss.fff] [Category|operation] key=value key=value dur_ms=N
```

The old `[HH:mm:ss.fff] [Category] message` format still works -- existing callsites are unaffected and the two formats coexist in the same log file.

**Tail sampling for scan engines.** Four timer-based scan engines (LinkEmbedEngine, MessageLogger, NsfwFilter, ThemeEngine) that previously logged on every 500ms tick now use tail sampling: scan results are buffered and only emitted as a 30-second heartbeat summary. This eliminates thousands of repetitive log lines per session while preserving the information that matters (total scans, hits, errors).

**Log Console (developer only).** A new "Live Console" button on the About page (dev channel only) opens a named pipe server that streams log lines in real time. Connect with any pipe client to watch the log live without tailing the file.

**New files:**
- `hook/WideEvent.cs` (~150 lines) -- structured event builder with key=value serialization and duration tracking
- `hook/TailSampler.cs` (~72 lines) -- periodic heartbeat aggregator for high-frequency scan loops
- `hook/LogConsole.cs` (~200 lines) -- named pipe server for real-time log streaming (dev channel only)

---

## Developer Tools

**ReconLogger** is a new dev-channel-only diagnostic tool that hooks into Root's settings page interaction events and writes visual tree data to the hook log as `[Recon]` entries. Useful for capturing style properties of sidebar controls during Rootcord and Uprooted UI development. Only initializes when the update channel is `developer`.

**Planned plugin tier** is a new status level in the plugin system. Plugins in this tier appear in the UI (when the experimental opt-in is active) with a clear label that they are not yet functional. Translate lived here during development before the implementation was ready.

---

## Bug Fixes

- **Theme revert** - Reverting a theme now triggers a variant toggle to force DynamicResource re-resolution on Root's native controls, and `RestoreTaggedControls` restores injected controls from Root's live colors
- **Named color crash** - `Color.ToString()` returns `"White"` for `#FFFFFFFF` in Avalonia, crashing `ColorUtils.ParseHex()`. Colors are now extracted via the `A`, `R`, `G`, `B` byte properties directly
- **Online status dots** - `BrandSecondary` is no longer overridden in ThemeDictionaries. Root uses this key for online status dots (green `#A8FF5D`); overriding it caused all online indicators to show the theme accent color
- **Custom ping color bleed** - `ApplyPingColorOverride()` was overriding `ThemeAccentColor` and `ThemeAccentBrush` globally, causing buttons and active-state UI to take on the ping color. The visual tree walk already handles the correct controls
- **ResourceDictionary lookups** - `dict["key"]` only checks direct entries, not merged dictionaries. Switched to `Application.TryGetResource` for full resolution chain
- **Theme preview swatch hover** - Transparent background on the color preview swatch caused a `PointerExited` event when the mouse crossed over it. Fixed with `IsHitTestVisible = false`
- **Theme switch flash-of-defaults** - Switching between Uprooted themes (Crimson → Cosmic Smoothie) caused a brief flash of Root's default colors because `RevertTheme()` removed all overrides before re-applying. `PrepareForNewTheme` now does in-place key diffing
- **Live preview desync on Root native text** - The untagged Foreground walker destructively overwrote DynamicResource bindings via `prop.SetValue`, causing Root's native TextBlocks to stop responding to ThemeDictionary updates. Bind-once pattern preserves bindings
- **Card borders not updating during live preview** - Card borders had `dyn-bb:Border` walker tags but no DynamicResource binding for BorderBrush. Added `BindToDynamicResource` at all card creation points
- **Gear icon background stale until Refresh** - Theme card gear button used a computed color with no DynamicResource binding. New `BackgroundButtonOnElevated` and `BackgroundButtonOnSecondary` palette keys enable binding
- **Nav highlight not updating on variant change** - Selected nav highlight read `HighlightNormal` once at build time. Now bound to `DynamicResource("HighlightNormal")` for live updates
- **`SetValueStylePriority` misnamed** - Method used `BindingPriority.LocalValue` (enum value 0), not Style priority. Renamed to `SetValueLocalPriority` to match actual behavior
- **Version migration never fired** - `UprootedSettings.Load()` had no `case "Version":` in the INI parser switch, so the Version property always returned its hardcoded default. Upgrade detection never worked. The missing case is now added

---

## Known Issues

- **MessageLogger: card positioning** - `FindMessageGridInContainer` returns null; the container structure may have changed and needs investigation
- **NSFW filter** - The Avalonia-native visual tree scanner (Phase 4.5g) has not been validated with the Google Vision API in production
- **SilentTyping** - Both interception layers are deployed but have not been validated with two simultaneous accounts
