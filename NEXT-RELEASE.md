# Next Release

> Changes since v0.5.1-dev2. This file is replaced each release.

### Fixed

- **Custom themes crash A/V settings and VC join** — Theme dictionary brushes now use `ImmutableSolidColorBrush` (matching Root's native type) instead of mutable `SolidColorBrush`. Fixes `InvalidCastException` that crashed when opening audio/video settings or joining voice chat with a custom theme active.
- **Theme switch lag on light→dark transitions (296ms→0ms)** — `SwitchVariantIfNeeded` tried PureDark variant which doesn't exist in current Root builds, bailing without switching. Variant stayed on Light, forcing manual SVG path rewrite of ~186 entries. Now falls back to Dark.
- **Theme switch lag from uncached reflection** — `AddResource`, `RemoveResource`, `CreateImmutableBrush` and other hot-path reflection calls used uncached `GetProperty`/`GetMethod`/`GetConstructor` on every invocation (~78 per switch). Now type-cached.
- **Custom color picker jank** — Live preview wrote all ~43 palette keys every 16ms frame. Now diff-based: only writes keys whose hex actually changed (~75% fewer writes per frame). Same-variant preset switches skip key removal (overwritten in-place).

### Changed

- **deploy-hook.ps1** — Auto-detects `net9.0`/`net10.0` build output directory instead of hardcoding TFM.
