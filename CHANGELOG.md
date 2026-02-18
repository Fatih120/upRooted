# Changelog

All notable changes to Uprooted are documented in this file.

Format follows [Keep a Changelog](https://keepachangelog.com/). Versions follow [Semantic Versioning](https://semver.org/) starting from v0.2.0.

---

## [0.3.4] - 2026-02-17

### Added
- Animated image embeds — `.gif` and `.webp` URLs play inline with frame-accurate timing via SkiaSharp `SKCodec` reflection (`hook/AnimatedImage.cs`)
- Per-embed animation timers with automatic cleanup on card removal
- Graceful fallback: if SkiaSharp frame APIs are unavailable or trimmed, renders static first frame (zero regression)
- Theme: "Cosmic Smoothie" preset — deep purple accent (`#7328BA`) with dark space background (`#0A041E`), full TreeColorMap + ResourceDictionary + CSS variables

### Changed
- Link embeds: skip Tenor URLs (`tenor.com`, `media.tenor.com`) — Root renders these natively, avoids double-embedding
- Settings sidebar: "Uprooted" nav item renamed to "About"
- Settings content headers: "Plugins" → "Plugin Settings", "Themes" → "Theme Settings"
- Plugin search box: increased font size (13→14), added horizontal padding and vertical centering for better placeholder text fit

### Fixed
- Settings crash: clicking the back arrow on Uprooted tabs no longer freezes Root — back arrow hidden by position detection (left-side RootSvgButton in header Grid), `DetachedFromVisualTree` safety net clears ScrollViewer before recursive detach, Button events use `Click` instead of `PointerPressed` (Avalonia class handler suppression)
- Settings header: Uprooted tabs now show page title and preserve X close button, matching Root's native `TabName [spacer] X` format
- Settings section header: "UPROOTED" sidebar section now uses 40px wrapper matching native ListBoxItem height for consistent vertical spacing

## [0.3.3] - 2026-02-17

### Added
- Link embeds: direct image URL fast path — `.jpg`, `.png`, `.gif`, `.webp`, `.bmp`, `.svg` URLs render instantly with zero network
- Link embeds: oEmbed discovery — scans HTML for `<link rel="alternate" type="application/json+oembed">` to support any oEmbed-compatible site
- Link embeds: Content-Type gate — skips OG parsing for PDFs, binaries; synthesizes image embed for `image/*` responses
- Link embeds: `twitter:image`, `twitter:title`, `twitter:description` meta tag fallbacks
- Link embeds: oEmbed photo type support — extracts `url` field for photo-type oEmbed responses per spec
- Settings cache: 10-second TTL cache on `UprootedSettings.Load()` to avoid disk I/O on every 500ms timer tick

### Changed
- Link embeds: Chrome-like User-Agent replaces bot UA (`Uprooted/0.2`) for better site compatibility
- Link embeds: per-request bot UA for Twitter/X and embed-fixer domains (vxtwitter, fxtwitter, fixupx) that serve OG only to crawlers
- Link embeds: embed-fixer domain normalization — fixupx/fxtwitter/fixvx URLs normalized to vxtwitter.com for richer OG metadata with images
- Link embeds: `HttpGetWithContentType` uses `ReadAsStreamAsync` + `StreamReader` instead of trimmed `ReadAsStringAsync`
- Link embeds: verbose diagnostic logging gated behind `UPROOTED_VERBOSE=1` env var
- Deploy script: launch Root.exe directly instead of UprootedLauncher.exe

### Fixed
- Link embeds: `DecodeJsonString` crash — `Regex.Replace` with `MatchEvaluator` lambda triggers trimmed method in Root's binary; replaced with manual `\uXXXX` decoding loop
- Link embeds: oEmbed endpoint fetch failures — `ReadAsStringAsync` triggers trimmed charset/encoding methods on JSON responses; switched to `ReadAsStreamAsync` + `StreamReader`

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

[0.3.4]: https://github.com/watchthelight/uprooted-private/compare/v0.3.3...v0.3.4
[0.3.3]: https://github.com/watchthelight/uprooted-private/compare/v0.3.2...v0.3.3
[0.3.2]: https://github.com/watchthelight/uprooted-private/compare/v0.3.0...v0.3.2
[0.3.0]: https://github.com/watchthelight/uprooted-private/compare/v0.2.3...v0.3.0
[0.2.3]: https://github.com/watchthelight/uprooted-private/compare/v0.2.2...v0.2.3
[0.2.2]: https://github.com/watchthelight/uprooted-private/compare/v0.2.1...v0.2.2
[0.2.1]: https://github.com/watchthelight/uprooted-private/compare/v0.2.0...v0.2.1
[0.2.0]: https://github.com/watchthelight/uprooted-private/releases/tag/v0.2.0
