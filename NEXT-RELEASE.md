# Next Release

> Changes since v0.4.0. This file is replaced each release.

### Added

- **Version-gated plugin force-disable on upgrade** — `ForceDisableOnUpgrade` dictionary in `StartupHook` declares which plugins to force-disable when users upgrade to (or through) a given version. Cumulative across skipped versions, downgrade-safe (stamps version only). `CurrentVersion` const replaces hardcoded banner string. Ships with empty dictionary; entries added per-release as needed.
