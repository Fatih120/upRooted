# Next Release

> Changes since v0.5.1-dev2. This file is replaced each release.

### Fixed

- **Custom themes crash A/V settings and VC join** — Theme dictionary brushes now use `ImmutableSolidColorBrush` (matching Root's native type) instead of mutable `SolidColorBrush`. Fixes `InvalidCastException` that crashed when opening audio/video settings or joining voice chat with a custom theme active.
