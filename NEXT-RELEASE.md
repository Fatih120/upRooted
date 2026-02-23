# Next Release

> Changes since v0.5.1-dev3. This file is replaced each release.

### Fixed

- **Custom themes crash A/V settings and VC join** — Theme dictionary brushes now use `ImmutableSolidColorBrush` (matching Root's native type) instead of mutable `SolidColorBrush`. Fixes `InvalidCastException` that crashed when opening audio/video settings or joining voice chat with a custom theme active.
- **Theme switch lag on light→dark transitions (296ms→0ms)** — `SwitchVariantIfNeeded` tried PureDark variant which doesn't exist in current Root builds, bailing without switching. Variant stayed on Light, forcing manual SVG path rewrite of ~186 entries. Now falls back to Dark.
- **Theme switch lag from uncached reflection** — `AddResource`, `RemoveResource`, `CreateImmutableBrush` and other hot-path reflection calls used uncached `GetProperty`/`GetMethod`/`GetConstructor` on every invocation (~78 per switch). Now type-cached.
- **Custom color picker jank** — Live preview wrote all ~43 palette keys every 16ms frame. Now diff-based: only writes keys whose hex actually changed (~75% fewer writes per frame). Same-variant preset switches skip key removal (overwritten in-place).
- **Native theme card desync after preset switching** — Variant snapshot (`_originalVariantKey`) was cleared when switching between same-family themes (dark→dark), causing the Native card to show the wrong variant. Snapshot now persists across all theme switches; only `RevertTheme` clears it.
- **Loki gold buttons hard to read** — `ContrastText` threshold lowered from 0.45 to 0.38 so borderline light accents (Loki gold L≈0.43) get black text instead of white.
- **Sakura status text invisible on pink background** — Status indicators and update status now use `Link` palette key (darker blue `#3878B8`) instead of `BrandPrimary` (light blue) for text-on-background use.
- **Custom theme borders derived from accent hue** — Border color in `GenerateV2Palette` used `(accentHue + bgHue) / 2`; now uses pure background hue with 75% chroma and increased lightness offset for better contrast.

### Changed

- **deploy-hook.ps1** — Auto-detects `net9.0`/`net10.0` build output directory instead of hardcoding TFM.
- **Oreo theme darker** — Background pushed from `#111111` to `#0B0B0B` for more contrast with Root's native Dark.
- **Sakura accent hue shift** — Blue accent shifted from `#94D9FF` (H≈201° cyan-ish) to `#84C2FF` (H≈210° sky blue) across brand, link, and mention colors.
