# Next Release

> Changes since v0.4.0. This file is replaced each release.

### Added

- **Root theme system research** (`research/ROOT_THEME_SYSTEM_FINDINGS.md`) — ILSpy decompilation documenting Root's 32-key native Avalonia color system, complete color tables for Dark/Light/PureDark, theme switching mechanism, and correct override strategy
- **ILSpy decompilation dumps** (`research/ilspy-dumps/`) — 66 decompiled .cs files from Root v0.9.92 (App.cs, theme views, style files, message views, etc.)

### Fixed

- **Linux crash: `MissingMethodException` on startup** — `ManualResetEventSlim.Wait(TimeSpan)` doesn't exist on the .NET runtime Root ships on Linux; replaced with `Wait(int)` which is universally available (StartupHook Phase 5, SilentTypingEngine)

### Changed

- **Documentation: integrate Root theme system findings** — Corrected 12 docs that claimed Root's native AXAML "contains no color brush resources" or that the ThemeEngine should target FluentTheme keys. All updated docs now reference Root's actual 32 ThemeDictionary keys and cross-link to `ROOT_THEME_SYSTEM_FINDINGS.md`. Affected: HOW_IT_WORKS, ROOT_INTERNALS, THEME_ENGINE_DEEP_DIVE, HOOK_REFERENCE, ARCHITECTURE, AVALONIA_PATTERNS, ROOT_ENVIRONMENT, themes.md, REVERSE_ENGINEERING, RESEARCH_INDEX, INDEX.
