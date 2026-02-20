# Next Release

> Changes since v0.4.2. This file is replaced each release.

### Added

- **Custom theme overhaul** — Major rework of the custom theme system:
  - Remove "Apply Custom" button — themes auto-apply on every keystroke via `UpdateCustomThemeLive`
  - Full OKLCH lightness range (0.05–0.93) — light custom backgrounds now work correctly
  - Smooth direction-aware color derivation (no binary isDark snapping at 50% threshold)
  - Custom text color input (empty = auto-derive from background lightness)
  - `CustomText` and `CustomSvgMode` settings fields persisted in INI
  - Files: `hook/ThemeEngine.cs`, `hook/ContentPages.cs`, `hook/UprootedSettings.cs`, `hook/StartupHook.cs`
- **Tag-based visual tree walker** — Controls tagged `dyn-fg:TextPrimary`, `dyn-bg:BackgroundSecondary`, etc. are recolored from the live palette on 100ms walker intervals. Text-only color map handles Root's native untagged controls (Foreground matching only).
  - File: `hook/ThemeEngine.cs`
- **DynamicResource binding via reflection** — `BindToDynamicResource(control, property, resourceKey)` attempts Avalonia DynamicResource binding. Silently fails at runtime but harmless; tag-based walker is the real mechanism.
  - File: `hook/AvaloniaReflection.cs`
- **Variant switching for custom themes** — Light custom themes trigger `SetRequestedThemeVariant(Light)` for correct SVG resolution. Guard prevents variant change handler from reverting during our switch.
  - Files: `hook/ThemeEngine.cs`, `hook/AvaloniaReflection.cs`
- **ColorPickerPopup.IsOpen** — Static property guards against page rebuilds during color picker drag.
  - File: `hook/ColorPickerPopup.cs`

### Changed

- **Card border style** — Border thickness reduced from 1.5px to 1.0px to match Root's native divider lines. Border color now reads from Root's `Border` resource instead of `AdjustForHighlight(CardBg, 15)`.
  - File: `hook/ContentPages.cs`
- **Nav item borders** — Sidebar nav items (About/Plugins/Themes) now have visible 1px borders using Root's `HighlightLight`/`HighlightNormal`/`HighlightStrong` resources. Adapts to both light and dark themes.
  - File: `hook/SidebarInjector.cs`
- **Custom theme card "no-recolor island"** — Hardcoded colors (`#1A1A2E` bg, `#F0F0F0` text, `#A0A0B0` muted) with `uprooted-no-recolor` tag. Ping color toggle uses hardcoded off-color (`#2A2A44`). Walker skips entire subtree.
  - File: `hook/ContentPages.cs`
- **ScrollViewer horizontal scroll disabled** — All content ScrollViewers set `HorizontalScrollBarVisibility = Disabled` so content stretches to fill available width.
  - File: `hook/AvaloniaReflection.cs`
- **Plugin filter: dropdown → cycling toggle** — Replaced the 3-option dropdown overlay (Show All/Enabled/Disabled) with a single cycling pill button. Click to cycle through "All Plugins" → "Enabled" → "Disabled". Each state has a distinct color. Hardcoded text/colors immune to custom themes.
  - File: `hook/ContentPages.cs`
- **Plugin sort order** — Stability tier now takes priority over enabled/disabled state. Stable plugins always list before Beta, regardless of toggle state.
  - File: `hook/ContentPages.cs`
- **Themes "Open" button sizing** — Shortened to match toggle switch dimensions (44×24) for visual consistency.
  - File: `hook/ContentPages.cs`
- **Theme-immune warning cards** — Experimental plugins banner and restart-required banner use hardcoded colors (`#2A2415` bg, `#E0A030` border, white text) with `uprooted-no-recolor` tag. Developer/Stable channel badge also hardcoded.
  - File: `hook/ContentPages.cs`

### Fixed

- **Theme revert** — Variant toggle forces DynamicResource re-resolution for Root native controls. `RestoreTaggedControls` walk restores injected elements from Root's live native colors after revert.
  - File: `hook/ThemeEngine.cs`
- **Auto-nav on variant change** — Settings page no longer auto-navigates to About tab when Root's theme variant changes. `_hasAutoNavigated` flag ensures auto-nav only fires on first settings open.
  - File: `hook/SidebarInjector.cs`

### Known Issues

- **DynamicResource binding via reflection silently fails** — Tag-based walker is the real mechanism for live recoloring
- **Root "Online" indicator and some sub-tabs don't live-update** with custom text color changes
- **SVG style UI removed** — Root resolves SVGs at variant-load time, not from ThemeDictionary writes; variant switching handles the common case
- **Uprooted tab header doesn't recolor** on custom theme changes (injected sidebar text)
