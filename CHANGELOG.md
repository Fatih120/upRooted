# Changelog

All notable changes to Uprooted are documented in this file.

Format follows [Keep a Changelog](https://keepachangelog.com/). Versions follow [Semantic Versioning](https://semver.org/) starting from v0.2.0.

---

## [0.3.2] - 2026-02-17

### Changed
- Installer auto-closes Root before install, repair, and uninstall operations
- Wait for Root process exit before deploying files

### Fixed
- Link embed text readability improvements

## [0.3.0] - 2026-02-17

### Added
- Console TUI installer replacing Tauri GUI (~600KB vs ~100MB)
- `--debug` CLI mode with live installation diagnostics
- Link embeds plugin (Discord-style OpenGraph + YouTube previews)
- Dual-prefix environment variables: `DOTNET_` (primary) + `CORECLR_` (legacy .NET 8/9)
- Phase 4.5 (BrowserDiscovery) and Phase 5 (DotNetBrowser features)
- DotNetBrowserReflection: full type cache, IBrowser discovery via ViewModel chain walking
- Event-driven DotNetBrowser assembly detection (ManualResetEventSlim, 90s timeout)
- KDE Plasma environment variable propagation for profiler loading

### Changed
- Anti-reverse-engineering hardening: stripped symbols, LTO, no PDBs
- Hook log now read from profile directory instead of deploy directory

### Fixed
- `MYGUID` uses unsigned long (8 bytes on Linux x64), fixing all GUID comparisons on Linux
- `Assembly.CreateInstance` replaced with `GetType` + `Activator.CreateInstance`
- Link embeds registered in C# KnownPlugins array
- Wayland white window resolved (disabled WebKit GPU compositing)
- Line endings enforced as LF in bash installer

## [0.2.3] - 2026-02-16

### Fixed
- `TypeLoadException` in profiler context by replacing `ValueTuple` with plain class
- `file://` URL handling on Linux
- CRLF enforcement for cross-platform compatibility

## [0.2.2] - 2026-02-16

### Fixed
- Wayland blank window on Linux (disabled GPU compositing)
- `file://` URL resolution on Linux
- Bash installer improvements

## [0.2.1] - 2026-02-16

### Fixed
- Click-handler crash on Uprooted settings pages
- Release artifact naming consistency

## [0.2.0] - 2026-02-16

First stable baseline. Consolidates all prior development (v0.1.x series) into a versioned release with conventional commit history.

### Added
- **C# .NET hook** with multi-phase startup (Phase 0--5), Avalonia reflection cache (~80 types), sidebar injection, native settings pages
- **TypeScript browser injection** with plugin runtime, bridge proxies, CSS theme engine
- **Tauri/Rust installer** with Root auto-detection, HTML patching, file deployment, environment variable management
- **CLR profiler** (native C) for IL injection into Root.exe on Windows and Linux
- **Self-healing HTML patches** (Phase 0 + FileSystemWatcher) that auto-repair after Root updates
- **Theme engine** with live preview, custom accent/background colors, HSV color picker
- **Sentry blocker** plugin (fetch, XHR, sendBeacon interception)
- **NSFW content filter** plugin
- **Content Filter** plugin card with gear and info lightboxes
- INI-based settings persistence (`UprootedSettings`) as System.Text.Json workaround
- Linux support: AppImage loading, systemd environment.d integration, standalone bash installer
- Arch Linux CI and PKGBUILD packaging
- Plugin testing status badges
- Close-Root popup guard on install/uninstall/repair
- CI pipeline: artifact builds, public repo release publishing

---

[0.3.2]: https://github.com/watchthelight/uprooted-private/compare/v0.3.0...v0.3.2
[0.3.0]: https://github.com/watchthelight/uprooted-private/compare/v0.2.3...v0.3.0
[0.2.3]: https://github.com/watchthelight/uprooted-private/compare/v0.2.2...v0.2.3
[0.2.2]: https://github.com/watchthelight/uprooted-private/compare/v0.2.1...v0.2.2
[0.2.1]: https://github.com/watchthelight/uprooted-private/compare/v0.2.0...v0.2.1
[0.2.0]: https://github.com/watchthelight/uprooted-private/releases/tag/v0.2.0
