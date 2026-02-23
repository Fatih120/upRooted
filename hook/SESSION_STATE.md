# Uprooted Hook - Session State (2026-02-23)

## Release: v0.5.1-dev3

## Current State Summary

The Theme Engine is the most recently active area, with a critical startup safety fix applied this session.

- **Experimental plugin safe upgrade** — ALL experimental plugins are now blanket-disabled on every version upgrade via `ExperimentalPlugins` array in `StartupHook.cs`. This prevents startup hangs when unstable plugins survive across versions. The `ShowExperimentalPlugins` flag is also reset. Users re-enable manually from Settings > Plugins.
- **Theme Engine rebrand** — Renamed from "Themes" to "Theme Engine" across sidebar tab, page header, plugin card, About status field. Plugin enabled logic now reflects actual theme state.
- **8 preset themes** — Crimson, Cosmic Smoothie, Loki, Marine, Oreo, Sakura (first light preset), Ember. 2×4 grid layout. Loki reworked: gold as BrandPrimary, green as secondary.
- **Light theme palette adaptation** — Border/Muted formulas fixed (`- offset * dir` for symmetric direction). Direction-aware: Brand, Link, Info/Warning, Mentions, DropShadow. `IsValidHex` accepts 8-digit hex. Preset SVG mode is `"auto"` (luminance-based).
- **Accent-aware text contrast** — `ContrastText()` helper on all accent-bg buttons and toggle thumbs. Hardcoded on-colors for experimental/ping toggles.
- **Theme state machine fixes** — `PrepareForNewTheme` removes ALL keys (color + SVG) from current variant dict, preventing cross-variant palette/SVG leaks. Same-family variant pulse (Dark↔PureDark) prevents SVG flash. Sidebar highlight binding scoped to Themes tab only.
- **Version text fix** — Root's native version TextBlocks bound to TextTertiary during injection for stable revert.
- **ImmutableSolidColorBrush fix** — Root casts ThemeDictionary brushes directly to ISCB. We were writing `SolidColorBrush`, causing `InvalidCastException` that crashed A/V settings and VC join. Fixed by using `CreateImmutableBrush(hex)` with `uint` constructor (Color constructor is trimmed). The `_windowResources` workaround (commit 5826a4c) is reverted — it broke DynamicResource propagation.

### Known Issues / TODOs

- **Version copy intercept** — fixed (2026-02-21): Root's async `SetTextAsync` race no longer races with our clipboard write
- **MessageLogger card positioning** — `FindMessageGridInContainer` returns null; container structure needs investigation
- **Experimental plugin validation** — Rootcord, MessageLogger, NsfwFilter, Translate, PresenceBeacon all deployed but need real-world testing
- **AppImage v0.5.0 detection failure** — Linux user reports v0.5.0 fails to detect/patch AppImage while v0.4.2 works. Needs `--diagnose` output from the user to investigate further.

## Startup Phases

| Phase | File | Purpose |
|-------|------|---------|
| 0 | HtmlPatchVerifier | Verify/repair HTML patches + FileSystemWatcher |
| 0→1 | StartupHook | Version migration: force-disable unstable plugins on upgrade |
| 1 | StartupHook | Wait for Avalonia assemblies (30s) |
| 2 | StartupHook | Wait for Application.Current (30s) |
| 3 | StartupHook | Wait for MainWindow (60s) |
| 3.5 | StartupHook | Initialize ThemeEngine + apply saved theme |
| 4 | SidebarInjector | Start settings page monitor (LayoutUpdated event + 200ms safety poll) |
| 4.5 | BrowserDiscovery | Dump visual tree + assembly scan (diagnostic) |
| 4.5a | ClearUrlsEngine | Strip tracking params from compose editor on Enter (14s delay) |
| 4.5b | LinkEmbedEngine | Avalonia-native link embeds (OG + oEmbed + animated images) |
| 4.5c | MessageLogger | Message logger: discovery + collection subscription + visual indicators |
| 4.5d | AutoUpdater | Background update check (every 1 minute), encrypted .uprpkg |
| 4.5e | ProfileBadgeInjector | Dev badge on profile popup (5s delay, event-driven + 500ms fallback) |
| 4.5f | SilentTypingEngine | DiagnosticListener interception (12s delay) |
| 4.5g | NsfwFilter | NSFW content filter (Avalonia-native visual tree scan, 20s delay) |
| 4.5h | RootcordEngine | Rootcord sidebar plugin (8s delay, dormant if disabled) |
| 4.5i | TranslateEngine | DeepL-powered message translation |
| 4.5j | UprootedPresenceBeacon | Uprooted user detection via gRPC metadata |
| 5 | StartupHook | DotNetBrowser: event-driven assembly detection |

## Critical Finding: Root's Chat is Avalonia-Native

Root v0.9.93's chat UI is rendered **entirely in native Avalonia controls**, NOT in DotNetBrowser.
DotNetBrowser is auxiliary (WebRTC, OAuth, etc.). Any feature modifying chat messages must use
**Avalonia-native approaches** (visual tree manipulation), NOT JavaScript injection.

## Test Suite

170 xUnit tests, all passing. Coverage: ColorUtils (57), GradientBrush (15), ClearUrlsEngine (58), UprootedSettings (22), MessageStore (18).
Infra: `tests/Dockerfile.unittest`, `tests/run-docker-tests.sh`, `tests/docker-installer/`.

## ClearURLs Engine Notes

- Root's compose input is `AvaloniaEdit.TextEditor` (NOT `Avalonia.Controls.TextBox`)
- Must use `AddHandler` with ALL routing strategies (Bubble|Tunnel|Direct = 7) and `handledEventsToo=true`
- Hook TextArea (child of TextEditor) as primary target; read/write Text from parent TextEditor

## Deployment

```powershell
# Use deploy-hook.ps1 or manually:
Stop-Process -Name Root,chromium -Force -ErrorAction SilentlyContinue
Start-Sleep -Seconds 5
Copy-Item -Force 'hook\bin\Release\net10.0\UprootedHook.dll' "$env:LOCALAPPDATA\Root\uprooted\UprootedHook.dll"
# Then launch Root manually
```

Hook log: `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log`
