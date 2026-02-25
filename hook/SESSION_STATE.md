# Uprooted Hook - Session State (2026-02-25)

## Release: v0.5.2-dev2

## Current State Summary

Live Console Linux fix is the most recent work. LogConsole now uses FIFO (`mkfifo`) on Linux instead of `NamedPipeServerStream` (which creates Unix domain sockets with a validation handshake incompatible with `cat`/`socat`). Windows path unchanged.

- **Bash installer `--channel` flag** — `--channel canary` or `--channel dev` to download from canary/dev repos instead of stable-only public repo. Dev requires GITHUB_TOKEN. Canary/dev use `?per_page=1` endpoint since prereleases are invisible to `/releases/latest`.
- **Installer non-fatal HTML patching** — HTML patch step is now a warning (not fatal) when Root hasn't been launched yet. Hook's HtmlPatchVerifier self-heals at runtime. Both TUI and plain CLI modes updated.
- **Updates card reworked** — Auto-check and notification toggles fixed for DPI rounding errors by delegating to `BuildToggleSwitch` (44×24, 16×16 thumb, 4px margin) instead of inline pill (52×26, 3px margins). Update channel control replaced with badge button cycling Stable↔Canary on click — matching Developer badge style. Dev channel easter egg timing tightened: 6 taps/1.5s (was 3s).
- **`LayoutUpdated` log noise eliminated** — Pre-throttle `>>> LayoutUpdated` log removed from `SidebarInjector.OnLayoutUpdated`; was firing on every Avalonia layout pass before the 50ms throttle and `_injected` early-return.
- **ProfileBadge/Injector log spam eliminated** — Fallback tick (200ms) and poll tick no longer emit raw START/END logs on every cycle. `ProfileBadgeInjector` now uses `TailSampler` + `WideEvent.BeginSampled`. `SidebarInjector` redundant plain `Logger.Log` lines removed (already had `WideEvent.BeginSampled`).
- **ReconLogger card redesigned** — Dev Console ReconLogger card rebuilt in plugin card format: bold name left, toggle right, description, "Dev" status badge. Toggle now refreshes the About page so Dev Plugins count updates immediately.
- **ThemeEngine enabled state fixed** — `BuildPluginsPage` `isEnabled` check and `rebuildGrid` filter/sort all now use `themeEngine.ActiveThemeName` (not `settings.Plugins["themes"]`, which was always `true` from legacy migration). `SidebarInjector` no longer force-sets `Plugins["themes"] = true` for new installs.
- **Experimental Plugins toggle dot** — `BuildToggleSwitch` gains `onThumbColor` override param. Experimental toggle passes `"#FFFFFF"` to prevent amber pill → black dot from `ContrastText`.
- **Verbose CLR profiler catch handler** — Both Windows and Linux profilers now inject a 27-byte verbose catch handler into the IL try/catch that loads the hook DLL. On failure, prints full exception + attempted DLL path to console via `Console.WriteLine`. Falls back to 3-byte silent catch if metadata token creation fails. Pre-flight check at profiler init logs DLL existence and file size.
- **Twemoji emoji rendering** — Emoji in user bio text now rendered as Twemoji images via `UserBioEngine`.
- **Linux installer fixes** — Stripped UTF-8 BOM from bash installer shebang. Diagnose now checks correct `$PROFILE_DIR` path for hook log.
- **Experimental plugin safe upgrade** — ALL experimental plugins are now blanket-disabled on every version upgrade via `ExperimentalPlugins` array in `StartupHook.cs`. This prevents startup hangs when unstable plugins survive across versions. The `ShowExperimentalPlugins` flag is also reset. Users re-enable manually from Settings > Plugins.
- **Theme Engine rebrand** — Renamed from "Themes" to "Theme Engine" across sidebar tab, page header, plugin card, About status field. Plugin enabled logic now reflects actual theme state.
- **8 preset themes** — Crimson, Cosmic Smoothie, Loki, Marine, Oreo, Sakura (first light preset), Ember. 2×4 grid layout. Loki reworked: gold as BrandPrimary, green as secondary.
- **Light theme palette adaptation** — Border/Muted formulas fixed (`- offset * dir` for symmetric direction). Direction-aware: Brand, Link, Info/Warning, Mentions, DropShadow. `IsValidHex` accepts 8-digit hex. Preset SVG mode is `"auto"` (luminance-based).
- **Accent-aware text contrast** — `ContrastText()` helper on all accent-bg buttons and toggle thumbs. Hardcoded on-colors for experimental/ping toggles.
- **Theme state machine fixes** — `PrepareForNewTheme` removes ALL keys (color + SVG) from current variant dict, preventing cross-variant palette/SVG leaks. Same-family variant pulse (Dark↔PureDark) prevents SVG flash. Sidebar highlight binding scoped to Themes tab only.
- **Version text fix** — Root's native version TextBlocks bound to TextTertiary during injection for stable revert.
- **ImmutableSolidColorBrush fix** — Root casts ThemeDictionary brushes directly to ISCB. We were writing `SolidColorBrush`, causing `InvalidCastException` that crashed A/V settings and VC join. Fixed by using `CreateImmutableBrush(hex)` with `uint` constructor (Color constructor is trimmed). The `_windowResources` workaround (commit 5826a4c) is reverted — it broke DynamicResource propagation.
- **Theme switch performance** — Reflection caching for AddResource/RemoveResource/CreateImmutableBrush etc (~78 uncached lookups per switch). Diff-based live preview for color picker (only writes changed keys). `_svgSetIsDark` tracking skips SVG enumeration, `_writingThemeDicts` guard prevents ActualThemeVariantChanged handler during dict writes, PrepareForNewTheme always uses fast same-dict overwrite.
- **Variant desync fix** — `SwitchVariantIfNeeded` no longer changes `RequestedThemeVariant`. Was causing Root's native theme page to show wrong selection (Dark user sees "Light" when Sakura active). SVG icons handled by `SwapSvgPathsIfNeeded` path rewriting instead.
- **Light theme hover fix** — Theme card border/dot hover uses card bg luminance for direction (not border's own L). Both darken 70% on light hosts to match Root native Light hover.
- **Sakura lightness hierarchy** — Background layers reordered: Primary > Secondary > Input > Tertiary. Tightened Primary/Secondary contrast.
- **Dev Console card** (developer channel only) — New card on About page with 3-column inner card grid: Spoofs (Update Popup, Update Installed, Reset), Diagnostics (Dump Visual Tree/Colors/Resource Keys), Engines (Force Theme Walk, Revert Theme), Recon Logger (own card with toggle). `AutoUpdater.SpoofUpdateApplied()` for UI testing.
- **Auto-update UX fixes** — About page restart button now orange (#D06818) matching Plugins page. "Check for Updates" button becomes disabled "Update Pending" (dimmed, 50% opacity) when update applied. Hover/click handlers no longer go stale.
- **ReconLogger moved to Dev Console** — Removed from KnownPlugins array and Plugins page. Toggle now in Dev Console inner card. "Dev Plugins · X active" status row on About identity card.

### Known Issues / TODOs

- **Version copy intercept** — fixed (2026-02-21): Root's async `SetTextAsync` race no longer races with our clipboard write
- **MessageLogger card positioning** — `FindMessageGridInContainer` returns null; container structure needs investigation
- **Experimental plugin validation** — Rootcord, MessageLogger, NsfwFilter, Translate, PresenceBeacon all deployed but need real-world testing
- **AppImage v0.5.0 detection failure** — Linux user reports v0.5.0 fails to detect/patch AppImage while v0.4.2 works. Needs `--diagnose` output from the user to investigate further.
- **Native theme card desync** — RESOLVED. Variant is no longer switched; Root's native theme page always shows the user's real preference.

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
