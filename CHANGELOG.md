# Changelog

All notable changes to Uprooted are documented in this file.

Format follows [Keep a Changelog](https://keepachangelog.com/). Versions follow [Semantic Versioning](https://semver.org/) starting from v0.2.0.

---

## [0.3.6-rc] - 2026-02-18

### Added
- **MessageLogger plugin** (WIP) — logs deleted messages in Avalonia-native chat with visual indicators
  - Deletion detection via `ObservableCollection.CollectionChanged` Remove events (event-based, not polling); buffered per-tick with debounce (10+ removes = channel switch → discard)
  - Diagnostic instrumentation: probes `HasBeenDeleted` property on removed items, tracks Add/Remove correlation per flush window, logs remove indices, one-time boolean property dump on first Remove event
  - Post-subscription settling filter: messages present at subscription time (`_initialSnapshotIds`) protected from false-positive removes for 30s; messages arriving via Add events trusted immediately with zero delay
  - Per-type property cache (`Dictionary<Type, TypeProps>`) handles multiple ViewModel types with nested `.Message` bridge property resolution
  - Deleted messages re-injected as Discord-style full-width red-tinted background stripes with 3px red left accent border, right-click "Clear message history" context menu
  - Channel switch handling: bulk remove detection, control freshness check every ~7.5s for `RootMessageItemsControl` instance swaps
  - Edit detection: disabled (false positives from content changes during send/render — needs redesign)
  - Files: `hook/MessageLogger.cs`, `hook/MessageStore.cs`
- **MessageStore** — flat-file persistence for message log data
  - Pipe-delimited append-only format (`MSG|EDIT|DEL` record types) with URI-encoded fields
  - Buffered flush every 5 seconds, automatic retention enforcement (configurable max messages)
  - File: `hook/MessageStore.cs`; location: `{profileDir}/uprooted-message-log.dat`
- **AutoUpdater** — in-process background updater with encrypted package delivery
  - Checks GitHub releases API every 6 hours; downloads single encrypted `.uprpkg` package containing all update files
  - Multi-layer XOR decryption: 64-byte master key + per-build 32-byte nonce + position-dependent key derivation
  - Files overwritten in-place; changes take effect on next Root restart (no user action required)
  - Developer channel (password-gated, pulls from private repo releases)
  - Reflection-based `HttpClient` to avoid trimmed method exceptions
  - Packing tool: `scripts/pack-update.py` (standalone Python, `--verify` round-trip flag)
  - File: `hook/AutoUpdater.cs`
- **Updates settings page** in native Avalonia UI — "Auto-check for updates", "Update notifications", and "Update channel" (stable/dev) controls; wired to `AutoUpdate.*` INI keys
- **MessageLogger settings page** in native Avalonia UI — Log Deleted Messages, Log Edited Messages, Ignore Own Messages toggles; Max Messages retention limit input
- **`BuildSettingsToggle` helper** in `ContentPages` — reusable pill-toggle + label + description component for any boolean plugin setting
- **TUI installer interactive mode selector** — launching the installer with no flags now shows an arrow-key menu (Install / Uninstall / Repair) instead of defaulting to install
- **ProfileBadgeInjector** — injects an "Uprooted Dev" badge into Root's profile popup overlay; active only when update channel is set to Developer; 500ms timer polls all TopLevel windows for new popup controls
  - File: `hook/ProfileBadgeInjector.cs`
- **ClearURLs engine** — strips tracking parameters from URLs in the compose editor before the message is sent
  - Hooks `AvaloniaEdit.TextEditor`'s `TextArea` via `AddHandler(RoutedEvent, …, handledEventsToo: true)` with all routing strategies (Bubble|Tunnel|Direct) — required because AvaloniaEdit marks Enter as `Handled=true`
  - 33 tracking params (utm_*, fbclid, gclid, etc.) in a case-insensitive `HashSet` for O(1) lookup; fragment-preserving; idempotent (no write if no params remain)
  - Timer-based `TextArea` discovery (2s interval) via visual tree walk for `RootMessageTextboxView`
  - File: `hook/ClearUrlsEngine.cs`
- **Plugin Roadmap** (`docs/PLUGIN_ROADMAP.md`) — planned plugins with architectural notes: ClearURLs, MessageLogger (design reference), NoReplyPing, Translate
- **Built-in plugin documentation** (`docs/plugins/builtin/`) — design doc for MessageLogger

### Changed
- **Sentry Blocker**: testing status promoted from Alpha → Beta
- **Link Embeds**: testing status promoted from Alpha → Beta
- **SidebarInjector**: UPROOTED nav section repositioned above "APP SETTINGS" (was appended at bottom); uses `FindAppSettingsInsertionPoint` to locate insertion index, falls back to append
- Rust installer `detection.rs`: `get_root_exe_path()` updated with 7-strategy Root detection to match bash installer (exact paths → glob patterns → `.desktop` file scan → `/proc/*/exe` → PATH lookup → `locate` database → shallow `find`)
- Version bumped to `0.3.6-rc` across all components (Cargo.toml, PKGBUILD, installer scripts)

### Fixed
- Cargo.toml version format: `0.3.6rc` → `0.3.6-rc` (bare version string caused `cargo build` to abort with exit code 101)
- **SidebarInjector**: UPROOTED section header spacing now matches Root's native section categories (16px top margin on container, -4px bottom margin on header wrapper)
- **SidebarInjector**: switching from Uprooted tabs back to Root tabs is now instant (subscribed `ListBox.SelectionChanged` instead of waiting for 200ms timer poll)
- **ProfileBadgeInjector**: fixed `double?` to `double` implicit conversion error in username font size comparison
- **ProfileBadgeInjector**: badge was appearing beside the username (inside the horizontal name row) instead of below it — fixed by walking up the visual tree to find the first vertical StackPanel (`Orientation == Vertical`), then inserting at the username row's index+1; added `IsVerticalPanel()` helper (checks `Orientation` property for StackPanels, falls back to Y-bounds delta comparison for Grid/DockPanel)
- **ProfileBadgeInjector**: badge made smaller and centered (font 12→10, padding 10,4→7,2, dot 8×8→6×6, `HorizontalAlignment=Center`)
- **Deploy script**: `deploy-hook.ps1` now relaunches Root via `UprootedLauncher.exe` (sets CLR profiler env vars) instead of bare `Root.exe`
- **SidebarInjector**: eliminated visible pop-in delay when opening settings — now uses `LayoutUpdated` event on MainWindow for same-frame detection instead of relying solely on 200ms timer poll; diagnostics (`DumpVersionRecon`) moved to run after injection so first-open UI is not blocked

### Documentation
- Added `docs/PLUGIN_ROADMAP.md` with implementation strategies for 4 planned plugins
- Added `docs/plugins/builtin/message-logger.md` — MessageLogger design reference
- Updated `ARCHITECTURE.md`, `HOOK_REFERENCE.md`, `INSTALLER.md`, `AVALONIA_PATTERNS.md`, `THEME_ENGINE_DEEP_DIVE.md` for v0.3.6-rc state
- Updated `TASKS.md`, `NEW-SESSION.md`, `NEXT-RELEASE.md` with v0.3.6-rc tracking

---

## [0.3.5] - 2026-02-18

### Added
- ClearURLs plugin — strips tracking parameters (utm_source, fbclid, gclid, si, etc.) from URLs in the compose editor when the user presses Enter to send (`hook/ClearUrlsEngine.cs`). Hooks AvaloniaEdit.TextEditor's TextArea via Avalonia routed events with `handledEventsToo=true` (all routing strategies required). 33 tracking params, idempotent, fragment-preserving.
- Animated image embeds — `.gif` and `.webp` URLs play inline with frame-accurate timing via SkiaSharp `SKCodec` reflection (`hook/AnimatedImage.cs`), with per-embed animation timers and automatic cleanup on card removal; graceful fallback to static first frame if SkiaSharp frame APIs are unavailable
- Link embeds: direct image URL fast path — `.jpg`, `.png`, `.gif`, `.webp`, `.bmp`, `.svg` URLs render instantly with zero network
- Link embeds: oEmbed discovery — scans HTML for `<link rel="alternate" type="application/json+oembed">` to support any oEmbed-compatible site
- Link embeds: Content-Type gate — skips OG parsing for PDFs, binaries; synthesizes image embed for `image/*` responses
- Link embeds: `twitter:image`, `twitter:title`, `twitter:description` meta tag fallbacks
- Link embeds: oEmbed photo type support — extracts `url` field for photo-type oEmbed responses per spec
- Link embeds: fallback to domain-only card when URL returns no OG metadata or title (e.g. Google Docs login redirects, JS-only SPAs)
- Theme: "Cosmic Smoothie" preset — deep purple accent (`#7328BA`) with dark space background (`#0A041E`), full TreeColorMap + ResourceDictionary + CSS variables
- Settings cache: 10-second TTL cache on `UprootedSettings.Load()` to avoid disk I/O on every 500ms timer tick

### Changed
- Link embeds: Chrome-like User-Agent replaces bot UA (`Uprooted/0.2`) for better site compatibility
- Link embeds: per-request bot UA for Twitter/X and embed-fixer domains (vxtwitter, fxtwitter, fixupx) that serve OG only to crawlers
- Link embeds: embed-fixer domain normalization — fixupx/fxtwitter/fixvx URLs normalized to vxtwitter.com for richer OG metadata with images
- Link embeds: skip Tenor URLs (`tenor.com`, `media.tenor.com`) and rootapp.gg invite links — Root renders these natively, avoids double-embedding
- Link embeds: verbose diagnostic logging gated behind `UPROOTED_VERBOSE=1` env var
- Settings sidebar: "Uprooted" nav item renamed to "About"
- Settings content headers: "Plugins" → "Plugin Settings", "Themes" → "Theme Settings"
- Plugin search box: increased font size (13→14), added horizontal padding and vertical centering for better placeholder text fit
- Linux installer: auto-fetches latest release version from GitHub API (10s timeout, graceful fallback to bundled version)
- Linux installer: `.desktop` file creation is now opt-in via `--desktop` flag (was auto-created)
- Linux installer: download errors now distinguish HTTP 404 from network errors, with actionable suggestions
- Linux installer: validates tarball integrity (gzip magic bytes) before extraction
- Linux installer: build-from-source falls back to pre-built artifacts on failure
- Linux installer: standalone script auto-detects when run outside the repo and uses pre-built artifacts
- Linux installer: post-install/repair messaging uses prominent ANSI box emphasizing "MUST log out and log back in"
- Deploy script: launch Root.exe directly instead of UprootedLauncher.exe

### Fixed
- Settings crash: clicking the back arrow on Uprooted tabs no longer freezes Root — back arrow hidden by position detection, `DetachedFromVisualTree` safety net clears ScrollViewer before recursive detach, Button events use `Click` instead of `PointerPressed`
- Settings header: Uprooted tabs now show page title and preserve X close button, matching Root's native `TabName [spacer] X` format
- Settings section header: "UPROOTED" sidebar section now uses 40px wrapper matching native ListBoxItem height for consistent vertical spacing
- Link embeds: `DecodeJsonString` crash — `Regex.Replace` with `MatchEvaluator` lambda triggers trimmed method; replaced with manual `\uXXXX` decoding loop
- Link embeds: oEmbed endpoint fetch failures — `ReadAsStringAsync` triggers trimmed charset/encoding methods; switched to `ReadAsStreamAsync` + `StreamReader`

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

[0.3.6-rc]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.3.5...v0.3.6-rc
[0.3.5]: https://github.com/watchthelight/uprooted-private/compare/v0.3.2...v0.3.5
[0.3.2]: https://github.com/watchthelight/uprooted-private/compare/v0.3.0...v0.3.2
[0.3.0]: https://github.com/watchthelight/uprooted-private/compare/v0.2.3...v0.3.0
[0.2.3]: https://github.com/watchthelight/uprooted-private/compare/v0.2.2...v0.2.3
[0.2.2]: https://github.com/watchthelight/uprooted-private/compare/v0.2.1...v0.2.2
[0.2.1]: https://github.com/watchthelight/uprooted-private/compare/v0.2.0...v0.2.1
[0.2.0]: https://github.com/watchthelight/uprooted-private/releases/tag/v0.2.0
