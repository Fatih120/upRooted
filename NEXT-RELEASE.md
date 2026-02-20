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
- **Plugin filter: dropdown → cycling toggle** — Replaced the 3-option dropdown overlay (Show All/Enabled/Disabled) with a single cycling pill button. Click to cycle through "All Plugins" → "Enabled" → "Disabled". Each state has a distinct color with `AdjustForHighlight` 1.5px border that updates on cycle. Bold text. Fixed `MinWidth` prevents width jitter when cycling.
  - File: `hook/ContentPages.cs`
- **Plugin sort order** — Stability tier now takes priority over enabled/disabled state. Stable plugins always list before Beta, regardless of toggle state.
  - File: `hook/ContentPages.cs`
- **Themes "Open" button** — Taller (28px) with border stroke and bold text to match toggle switch proportions.
  - File: `hook/ContentPages.cs`
- **Overlay scrollbar** — Settings pages use `CreateOverlayScrollViewer` which relocates the vertical ScrollBar into the content Grid column via a deferred LayoutUpdated handler, matching Root's native RootScrollViewer overlay behavior (no content displacement on scroll).
  - File: `hook/ContentPages.cs`
- **Filter toggle "Enabled" color** — Hardcoded to `#40A050` (stable green) instead of theme-dependent `AccentGreen` which changed with custom themes.
  - File: `hook/ContentPages.cs`
- **Experimental plugins banner: theme-aware toggle** — When disabled, uses theme colors (CardBg, CardBorder, TextWhite, TextMuted); when enabled, switches to hardcoded amber warning colors (`#2A2415` bg, `#E0A030` border). Warning icon (20px) hidden when disabled. Toggle pill uses `AdjustForHighlight(CardBg, 8)` off-color instead of hardcoded `#2A2A44`.
  - File: `hook/ContentPages.cs`
- **Restart banners: consistent card format** — Both plugin restart and update restart banners now use title + subtitle layout matching the experimental card height. Distinct burnt orange tint (`#2A1D15` bg, `#D06818` border/icon) differentiates from amber warnings. Update banner text changed from "use" to "install".
  - File: `hook/ContentPages.cs`
- **Restart buttons: accent button format** — Bold text, centered, `AdjustForHighlight(color, 30)` 1.5px border. Matches established button standard across all Uprooted tabs.
  - File: `hook/ContentPages.cs`
- **Cards-in-a-card layout** — Preset and custom theme sections wrapped in outer container cards (`BackgroundSecondary` bg, `Border` border, corner radius 12). Inner theme cards use 2nd-order styling: slightly lighter bg (`AdjustForHighlight(CardBg, 4.5)`), `CardBorder` rest border, `Lighten(CardBorder, 60)` hover border, 1.5px thickness. Equal-width Grid columns replace fixed-width cards.
  - File: `hook/ContentPages.cs`
- **Radio indicators (Root native style)** — Theme card radio selectors use neutral `TextWhite` color only (16×16, 1.0px border, 10×10 inner dot with 1px margin). Never accent-colored, border unaffected by selection state.
  - File: `hook/ContentPages.cs`
- **Typography (Root native style)** — Page titles bumped to 20px Bold, section headers to 14px Bold `TextPrimary`. About page renamed to "About Uprooted". Uses `SetFontWeight("Bold")` (string-based) as `SetFontWeightNumeric` silently fails.
  - File: `hook/ContentPages.cs`
- **Button borders and text** — Accent buttons (version badge, Check for Updates, Developer/Stable, Go) use `AdjustForHighlight(btnColor, 30)` 1.5px borders with Bold text. Non-accent buttons (Open Logs) use `AdjustForHighlight(bg, 15)`. Developer/Stable border updates dynamically on toggle.
  - File: `hook/ContentPages.cs`
- **Restart button color** — Deeper burnt orange (`#D06818`) for better visibility.
  - File: `hook/ContentPages.cs`
- **Plugin card vector icons** — Gear and info icons use `Shapes.Path` with `Stretch.Uniform` (Material Design 24x24 SVG paths). Replaced `PathIcon` which didn't scale geometry to fit.
  - Files: `hook/ContentPages.cs`, `hook/AvaloniaReflection.cs`
- **DPI-aware border thicknesses** — `ComputeDpiAwareBorders` reads `RenderScaling` from MainWindow and computes `ThinBorder` (1 physical pixel) and `ThickBorder` (next pixel boundary above thin). Outer cards use `ThinBorder`, inner cards and buttons use `ThickBorder`. Borders are always visually distinct at any DPI (100%, 125%, 150%, 200%).
  - Files: `hook/ContentPages.cs`, `hook/AvaloniaReflection.cs`
- **Radio selectors: Grid overlay** — Theme card radio indicators rebuilt as two sibling Borders in a Grid (ring + centered dot) instead of nested Border. Outer bumped to 18x18 so `(18-10)*scale` is always even — perfect centering at 100% and 150%.
  - File: `hook/ContentPages.cs`
- **Toggle switch centering** — Thumb 18→16 with margin 3→4, ensuring `(24-16)*scale` is always even for pixel-perfect centering at all common DPI scales.
  - File: `hook/ContentPages.cs`
- **Plugin card text** — Plugin names now render Bold via `SetFontWeight("Bold")` (previously `SetFontWeightNumeric(600)` which silently failed). Testing status badges (Alpha/Beta/Stable) also Bold with 1px border (was 0.5px).
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
