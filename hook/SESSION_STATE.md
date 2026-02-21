# Uprooted Hook - Session State (2026-02-20)

## Release: v0.4.2

## Shipped This Cycle

### Post-v0.4.2 (bug audit sweep — 2026-02-21)
- **15-commit codebase audit** — Thread safety, timer disposal, task leak removal, error handling, type correctness:
  - `UprootedSettings`: TOCTOU-safe cache, atomic Save() via temp-rename, OrdinalIgnoreCase plugins dict
  - `MessageStore`: IDisposable + WriteLock covers full I/O; skip bad timestamps instead of fabricating UtcNow
  - `MessageLogger`, `AuditLogEngine`, `LinkEmbedEngine`: IDisposable added; all dispose their `_scanTimer`
  - `ClearUrlsEngine`, `SidebarInjector`: redundant `Task.Delay().ContinueWith()` timeout tasks removed
  - `NsfwFilter`: `_seenImages` capped at 2000 entries to prevent unbounded session-long growth
  - `RootcordEngine`: `_applyCts` CancellationToken cancels pending `Task.Delay` callbacks on `Revert()`
  - `Logger`: static constructor now catches bad profile path and falls back to temp dir
  - `HtmlPatchVerifier`: `_debounceTimer = null` in Dispose()
  - `StartupHook`: diagnostic exception logged instead of silently swallowed
  - `plugin.ts` / `pluginLoader.ts`: Patch handlers restricted to synchronous return types (Promise<> broke cancellation)
  - `ContentPages`: lightbox dismiss methods null ref before remove; NsfwFilterInstance.UpdateConfig() guarded

### Post-v0.4.2 (custom theme overhaul)
- **Custom theme overhaul** — Auto-apply on keystroke, full OKLCH lightness range, smooth derivation, custom text color input, tag-based visual tree walker, variant switching
- **Card border fix** — 1.5px → 1.0px, color from Root's `Border` resource (matches native divider lines)
- **Nav item borders** — Visible 1px borders using Root's highlight resources
- **Auto-nav fix** — `_hasAutoNavigated` flag prevents re-navigation on variant change
- **Ping toggle island fix** — Hardcoded off-color for no-recolor island
- **ScrollViewer width fix** — `HorizontalScrollBarVisibility = Disabled` for correct content stretch
- **Overlay scrollbar** — `CreateOverlayScrollViewer` relocates vertical ScrollBar into content Grid column via deferred LayoutUpdated handler, matching Root's native RootScrollViewer overlay behavior (no content displacement)
- **Themes "Open" button** — taller (28px), border stroke, bold text to match toggle switch proportions
- **Filter toggle "Enabled" color** — hardcoded `#40A050` (stable green) instead of theme-dependent `AccentGreen`

### v0.4.2 features
- **Light/PureDark theme compatibility** — Settings UI adapts to all Root theme variants; live color system
- **Rootcord plugin** — Discord-style vertical server sidebar, live toggle (experimental)
- **Desktop notifications** — OS-level toast/notify-send when background update applied; respects AutoUpdateNotify
- **Plugin UI overhaul** — Sort (enabled-first → Stable→Experimental → A-Z), Show More button (4 card limit), experimental opt-in toggle
- **About page** — Links + Diagnostics cards removed; Open Logs button in title row
- **ScheduleWalkBurst debounce** — No more freeze when rapidly navigating Root's real settings tabs
- **AutoUpdater interval** — 6 hours → 1 minute
- **Testing statuses** — ClearURLs→Stable, Themes→Beta, SilentTyping→Experimental
- **Experimental toggle z-order fix** — Toggle pill now renders on top of banner

### Known issues / TODOs
- **DynamicResource binding via reflection silently fails** — `BindToDynamicResource` code present but doesn't propagate changes; tag-based walker is the real mechanism
- **Root "Online" indicator and some sub-tabs** don't live-update with custom text color changes
- **SVG style UI removed** — Root resolves SVGs at variant-load time; variant switching handles common case
- **Uprooted tab header doesn't recolor** on custom theme changes (injected sidebar text)
- **Version copy intercept** — commented out, needs investigation (Root's async `SetTextAsync` races with our clipboard write). See `SidebarInjector.cs` ~line 1155.

## Wide Event Logging (v0.4.3)

### New Files

| File | Lines | Purpose |
|------|------:|---------|
| `WideEvent.cs` | ~150 | Structured wide event builder — IDisposable pattern, key=value pairs, automatic `dur_ms` on Dispose |
| `TailSampler.cs` | ~72 | Tail sampling for high-frequency scan ticks — suppresses repetitive scan output, emits periodic heartbeat |
| `LogConsole.cs` | ~200 | Dev-only "Live Console" button on About page — spawns PowerShell terminal with named pipe streaming |

### Log Format

**New structured format (wide events):**
```
[HH:mm:ss.fff] [Category|operation] key=value key=value dur_ms=N
```

**Old format (still works, backwards compatible):**
```
[HH:mm:ss.fff] [Category] message
```

`Logger.cs` grew from 113 to ~170 lines — gained `EmitWideEvent` method and `OnLine` callback for pipe streaming. ~1200 scattered `Logger.Log` calls across 11 files were migrated to ~100 wide events.

### Tail-Sampled Engines

Four high-frequency scan engines use `TailSampler` to suppress repetitive scan tick output while emitting periodic heartbeats:

| Engine | Purpose |
|--------|---------|
| `LinkEmbedEngine` | Avalonia-native link embed scanner |
| `MessageLogger` | Edit/delete detection scanner |
| `SidebarInjector` | Settings page monitor |
| `ClearUrlsEngine` | Tracking param stripper |

### LogConsole Architecture

- Dev-only "Live Console" button injected on the About settings page
- Creates a Windows named pipe server for real-time log streaming
- `Logger.OnLine` callback writes each log line to the pipe
- Spawns a PowerShell terminal process that connects to the named pipe and displays output
- Only available on developer channel builds

## Critical Finding: Root's Chat is Avalonia-Native

Root v0.9.93's chat UI is rendered **entirely in native Avalonia controls**, NOT in DotNetBrowser.
Confirmed through extensive investigation on 2026-02-17:

- Avalonia visual tree: 1647+ nodes, **0 browser-like controls**
- DotNetBrowser loads `rootapp://app/__index.html` — a shell page with `<iframe id="app-iframe">`
- The iframe **permanently stays at `about:blank`** with no `src` attribute, no content
- IBrowser.Title = "Root App Container", Size = 1280x800, URL = `rootapp://app/__index.html`
- DotNetBrowser is auxiliary (WebRTC, OAuth, etc.), NOT the chat renderer

**Implication**: Link embeds, NSFW filter on chat, or any feature modifying chat messages
must use **Avalonia-native approaches** (visual tree manipulation), NOT JavaScript injection.

## DotNetBrowser Discovery Chain (working)

Phase 5 successfully finds IBrowser via ViewModel chain walking:

```
MainWindow.DataContext (MainViewModel)
  → _memberProfileViewModelFactory
    → <directMessageOpenerService>P
      → <browserService>P
        → _engineManager (BrowserEngineManager)
          → .Engine (IEngine)
            → .Profiles[0].Browsers.__values (ConcurrentDictionary)
              → IBrowser (BrowserRpcService)
```

Key details:
- Assembly gate: only require `DotNetBrowser` or `DotNetBrowser.Core` (NOT AvaloniaUi — Root doesn't ship it)
- Event-driven detection via `AppDomain.AssemblyLoad` + `ManualResetEventSlim`, 90s timeout
- `Repository<BrowserId, IBrowserImpl>` doesn't implement IEnumerable — must access private `_values` field
- ExecuteJavaScript: `IFrame.ExecuteJavaScript(String, Boolean)` — 2 params, bool defaults to false
- Diagnostic readback: set `document.title` from JS, read `IBrowser.Title` from C#

## DotNetBrowser Shell Page

```html
<html lang="en">
<head>
  <title>Root App Container</title>
  <style>html, body { margin:0; padding:0; width:100%; height:100%; overflow:hidden; }
  iframe { display:block; width:100%; height:100%; border:none; }</style>
</head>
<body>
  <iframe id="app-iframe"></iframe>  <!-- permanently about:blank, no src -->
</body>
</html>
```

## Loaded Assemblies (relevant)

- DotNetBrowser v3.4.0.6253, DotNetBrowser.Core v3.4.0.6253, DotNetBrowser.Logging v3.4.0.6253
- DotNetBrowser.AvaloniaUi: **NOT loaded** (Root doesn't ship it)
- Root v0.9.93.0, RootApp.Client.Avalonia v0.9.93.0
- Avalonia 11.3.12, .NET 10.0.3, Chromium 144.0.0.0
- Chromium flags: `--incognito`, `--disable-web-security`

## Startup Phases

| Phase | File | Purpose |
|-------|------|---------|
| 0 | HtmlPatchVerifier | Verify/repair HTML patches + FileSystemWatcher |
| 0→1 | StartupHook | Version migration: force-disable unstable plugins on upgrade (cumulative, downgrade-safe) |
| 1 | StartupHook | Wait for Avalonia assemblies (30s) |
| 2 | StartupHook | Wait for Application.Current (30s) |
| 3 | StartupHook | Wait for MainWindow (60s) |
| 3.5 | StartupHook | Initialize ThemeEngine + apply saved theme |
| 4 | SidebarInjector | Start settings page monitor (LayoutUpdated event + 200ms safety poll) |
| 4.5 | BrowserDiscovery | Dump visual tree + assembly scan (diagnostic) |
| 4.5a | ClearUrlsEngine | Strip tracking params from compose editor on Enter (14s delay) |
| 4.5b | LinkEmbedEngine | Avalonia-native link embeds (OG + oEmbed + animated images) |
| 4.5c | MessageLogger | Message logger: discovery + collection subscription + visual indicators |
| 4.5d | AutoUpdater | Background update check (every 1 minute), encrypted .uprpkg download + decrypt + apply; DesktopNotification on apply |
| 4.5e | ProfileBadgeInjector | "Uprooted Dev" badge on profile popup (dev channel + hardcoded usernames, 5s delay, event-driven + 500ms fallback) |
| 4.5f | SilentTypingEngine | DiagnosticListener-based interception: subscribes to .NET HTTP diagnostics, redirects SetTypingIndicator to localhost:0 (12s delay). ~90 lines, by Kurumi Nanase. |
| 4.5g | NsfwFilter | NSFW content filter (Avalonia-native visual tree scan, 20s delay) |
| 4.5h | RootcordEngine | Rootcord sidebar plugin (8s delay, dormant if disabled) |
| 4.5i | TranslateEngine | DeepL-powered message translation |
| 4.5j | UprootedPresenceBeacon | Uprooted user detection via gRPC metadata |
| 5 | StartupHook | DotNetBrowser: event-driven assembly detection, type resolution (video thumbnails for LinkEmbedEngine) |

## Link Embed Engine (v0.3.3–0.4.0)

The Avalonia-native link embed engine is broadly functional:

**Working:**
- YouTube embeds (oEmbed + thumbnail)
- Direct image URL fast path (`.jpg`, `.png`, `.gif`, `.webp`, `.bmp`, `.svg` — zero network)
- Animated GIF/WebP playback via SkiaSharp `SKCodec` reflection (`AnimatedImage.cs`)
- oEmbed discovery from HTML `<link>` tags (any oEmbed-compatible site)
- Content-Type gate (skips PDFs/binaries, synthesizes image embed for `image/*`)
- Twitter/X and embed-fixer domains (vxtwitter, fxtwitter, fixupx) with per-request bot UA
- Embed-fixer normalization (fixupx/fxtwitter/fixvx → vxtwitter.com)
- `twitter:image`/`twitter:title`/`twitter:description` meta tag fallbacks
- Tenor: only `media.tenor.com` (direct CDN) skipped; `tenor.com/view/` pages go through OG pipeline for animated GIF embeds
- Settings cache: 10s TTL on `UprootedSettings.Load()`

- Reddit embeds: dedicated handler with old.reddit.com OG fetch, subreddit provider label, orange accent
- HTTP status code + Content-Type validation in `HttpGetBytes` (rejects Cloudflare challenge pages)
- OG fallback for image-extension URLs that serve HTML (Zipline `/view/`, `/u/`)
- Image embed borders round all 4 corners (imgBorder `HorizontalAlignment("Left")`)
- Robust OG regex: explicit attribute bridge handles `itemProp`, `data-next-head`, etc.
- Video embeds (.mp4, .webm, .mov): dark 16:9 placeholder with centered play button, click opens in browser
- `video/*` Content-Type detection for extensionless video URLs
- Image-only embed filename hiding: off by default, `LinkEmbedsShowFilenames` toggle, live `RefreshTitleVisibility()`
- `LinkEmbedEngine.Instance` static property for external access from ContentPages

**Known limitations:**
- JS-rendered OG fallback not available (some sites serve no OG in static HTML)
- Video embeds show placeholder (no first-frame extraction — SkiaSharp cannot decode MP4/WebM video codecs)

## New Files (post v0.4.2)

| File | Lines | Purpose |
|------|------:|---------|
| `hook/TranslateEngine.cs` | 1145 | DeepL-powered message translation (Phase 4.5i) |
| `hook/TranslateConfigPopup.cs` | 511 | Translate config popup UI (language picker, API key) |
| `hook/UprootedPresenceBeacon.cs` | 470 | Uprooted user detection via gRPC metadata (Phase 4.5j) |
| `hook/ReconLogger.cs` | 786 | Visual tree + style property diagnostic dumper |

## Files Modified Recently

| File | Changes |
|------|---------|
| `hook/UprootedSettings.cs` | Thread-safe cache (local copy), atomic Save() via temp-rename, OrdinalIgnoreCase plugins dict |
| `hook/HtmlPatchVerifier.cs` | `_debounceTimer = null` after Dispose() |
| `hook/Logger.cs` | Static ctor wrapped in try/catch with Path.GetTempPath() fallback |
| `hook/StartupHook.cs` | Diagnostic DumpVisualTreeColors exception now logged |
| `hook/MessageStore.cs` | IDisposable + timer disposal + WriteLock covers full I/O + skip bad timestamps |
| `hook/MessageLogger.cs` | IDisposable (disposes _scanTimer + _store) |
| `hook/AuditLogEngine.cs` | IDisposable (disposes _scanTimer) |
| `hook/LinkEmbedEngine.cs` | IDisposable (disposes _scanTimer) |
| `hook/ClearUrlsEngine.cs` | Removed redundant Task.Delay().ContinueWith() timeout task |
| `hook/SidebarInjector.cs` | Removed redundant Task.Delay(3000).ContinueWith() timeout task |
| `hook/NsfwFilter.cs` | _seenImages size cap at 2000 entries |
| `hook/RootcordEngine.cs` | _applyCts CancellationToken cancels pending delays on Revert(); covers both SubscribeTabChanges and OpenSettingsDirectly |
| `hook/ContentPages.cs` | Null-first dismiss pattern on all 3 lightbox types; NsfwFilterInstance.UpdateConfig() guarded with try/catch |
| `src/types/plugin.ts` | Patch handler return types restricted to synchronous (removed Promise<>) |
| `src/core/pluginLoader.ts` | EventHandler type restricted to void return |
| `hook/ClearUrlsEngine.cs` (original) | New file: strips tracking params from compose editor URLs on Enter key via AvaloniaEdit.TextArea routed event interception |
| `hook/StartupHook.cs` | Added Phase 4.5a: ClearURLs engine registration (14s delay) |
| `hook/ContentPages.cs` | Added ClearURLs PluginInfo entry (clear-urls, default enabled, TestingStatus=1) |
| `hook/LinkEmbedEngine.cs` | Chrome-like UA, bot UA for Twitter/X, embed-fixer normalization, oEmbed discovery, Content-Type gate, direct image fast path, twitter:* fallbacks, verbose logging, Reddit embeds (old.reddit.com OG, subreddit label, orange accent), HTTP status/Content-Type validation in HttpGetBytes, OG fallback for image-extension HTML pages, robust OG regex, image border corner fix |
| `src/plugins/link-embeds/providers.ts` | Robust OG regex: explicit attribute bridge handles meta tags with extra attributes between property and content |
| `hook/AnimatedImage.cs` | Animated GIF/WebP decoder + timer playback via SkiaSharp reflection |
| `hook/UprootedSettings.cs` | 10s TTL settings cache to reduce disk I/O |
| `hook/SidebarInjector.cs` | Back arrow structural search (FindHeaderControls by child order in header Grid) + collapse pattern (Opacity/MaxWidth/MaxHeight/Width/Margin zeroed — avoids fighting IsVisible binding); 50ms LayoutUpdated throttle (was 500ms); removed _injecting reentracy guard from LayoutUpdated; selection suppression with header state restore; DetachedFromVisualTree cleanup; settings reload on nav click; theme walk burst triggers |
| `hook/ContentPages.cs` | Renamed: "Plugins" → "Plugin Settings", "Themes" → "Theme Settings"; added Cosmic Smoothie preset card; search box font/padding/centering fix |
| `hook/ThemeEngine.cs` | Added "cosmic-smoothie" theme: TreeColorMap (26 color mappings) + Themes ResourceDictionary (full FluentTheme key set) |
| `src/plugins/themes/themes.json` | Added "cosmic-smoothie" theme entry with CSS variables |
| `hook/MessageLogger.cs` | Chat message logger (1624 lines) — per-item async deletion pollers (HasBeenDeleted probe every 300ms for 3s), epoch-based channel switch cancellation, per-type property cache, diagnostic instrumentation (DIAG-INJ/DIAG-FLUSH logging, boolean property dump), Discord-style deleted message rows |
| `hook/MessageStore.cs` | Flat-file persistence for message log (pipe-delimited, URI-encoded, append-only: MSG\|DEL\|EDIT\|CLR records) |
| `hook/StartupHook.cs` | Added Phase 4.5c message logger initialization (20s delay for chat population) |
| `hook/UprootedSettings.cs` | Added MessageLogger.LogDeletes, LogEdits, IgnoreSelf, MaxMessages settings |
| `hook/ContentPages.cs` | Added message-logger to KnownPlugins, settings lightbox with toggle UI, BuildChannelRow for update channel switching, restart banners (plugins + updates), state-aware plugin banner (hides on revert), Restart button (Process.Start + Environment.Exit), DIAGNOSTICS "Open" button (explorer /select) |
| `hook/ProfileBadgeInjector.cs` | Profile popup detection via event-driven OverlayLayer.Children CollectionChanged + 500ms fallback poll for TopLevel popups, heuristic matching (avatar + username + roles), tree dump diagnostics, username TextBlock discovery (largest font size), badge gated to hardcoded developer usernames (`DeveloperUsernames` HashSet), vertical panel walk-up to insert badge below username (`IsVerticalPanel()` helper), "Uprooted Dev" badge (gold #8B6914 pill, font 10, centered) |
| `hook/StartupHook.cs` | Added Phase 4.5e profile badge injector (5s delay, developer channel only); `CurrentVersion` const + `ForceDisableOnUpgrade` dictionary + version migration block (between Phase 0 and Phase 1) |
| `hook/UprootedSettings.cs` | Added `CustomPingColor` property (string, default empty), INI load/save |
| `hook/ThemeEngine.cs` | Added `_customPingColor` field, `SetCustomPingColor()`, `ClearCustomPingColor()`, `ApplyPingColorOverride()`, `RestoreThemeHighlightKeys()`, Phase 6 in `ApplyThemeInternal()`, re-apply in `UpdateCustomThemeLive()` |
| `hook/ContentPages.cs` | Ping Color merged into Custom Theme card (separated by divider); removed standalone `BuildPingColorSection()`; embed card `NotifyThemeChanged()` called from `ApplyThemedColors`/`UpdateLiveColors` |
| `hook/StartupHook.cs` | Added custom ping color apply in Phase 3.5 after theme init |
| `hook/ContentPages.cs` | Plugin DisplayNames renamed to PascalCase: SentryBlocker, LinkEmbeds, MessageLogger, ContentFilter |
| `hook/SidebarInjector.cs` | Theme walk burst triggers: after injection, on ListBox SelectionChanged, on Uprooted tab switch (WalkVisualTreeNow synchronous) |
| `hook/ThemeEngine.cs` | Added 50ms rapid follow-up to ScheduleRapidFollowUp walk sequence |
| `hook/MessageLogger.cs` | Restored missing `_initialSnapshotIds` and `_lastSubscriptionTime` field declarations (build fix) |
| `hook/LinkEmbedEngine.cs` | Video embeds: `VideoUrlRegex`, `SynthesizeVideoEmbed()`, `BuildVideoPlaceholder()`, `video/*` Content-Type gate, video placeholder in `BuildEmbedCard`. Also: `Instance` static property, `RefreshTitleVisibility()`, image-only title hiding with tag-based visibility toggle |
| `hook/LinkEmbedEngine.cs` | Theme-aware embed accent: constructor now accepts `ThemeEngine?`; `BuildEmbedCard` uses `_themeEngine?.GetAccentColor()` as fallback accent; `NotifyThemeChanged()` walks all live injected cards to update background + border brush (preserves site-specific OG colors via `_metadataCache`) |
| `hook/ContentPages.cs` | `BuildLinkEmbedSettings()` — "Show Embedded File Names" toggle in LinkEmbeds settings lightbox, calls `LinkEmbedEngine.Instance?.RefreshTitleVisibility()` |
| `hook/StartupHook.cs` | Sets `LinkEmbedEngine.Instance = engine` in Phase 4.5b for external access |
| `hook/UprootedSettings.cs` | `LinkEmbedsShowFilenames` property (bool, default false), INI load/save for `LinkEmbeds.ShowFilenames` |
| `hook/Logger.cs` | Added `LogSeparator()` method — writes blank lines to log for visual separation on startup |
| `hook/Entry.cs` | Calls `LogSeparator()` before first `[Entry]` log in both ModuleInitializer and constructor |
| `hook/ContentPages.cs` | Modular FontScale system: `PageScale` (15/11/13/12/13) for main pages, `LightboxScale` (24/20/20/17/18) for plugin info/settings lightboxes (Title/SectionHeader/Label/Description/Button). Lightbox titles, info box headers, helper text all wired to scale. All plugin descriptions and info box content rewritten for end users (no technical jargon). |
| `scripts/watch-log.ps1` | `[Entry]` lines colored green; `fallback` messages stay yellow instead of red |
| `hook/LinkEmbedEngine.cs` | Tenor: only `media.tenor.com` skipped (not bare `tenor.com`); video embeds respect "Show file names" toggle (`isFileOnlyEmbed` includes `VideoId=="direct"`) |
| `hook/AnimatedImage.cs` | Persistent canvas bitmap for frame compositing — fixes black pixels and frame ordering in delta-encoded GIFs; removed per-frame `DecodeFrame` method |
| `hook/Logger.cs` | Dev-channel-only logging: `_enabled` flag + `Disable()` method (suppresses writes, deletes log file). `IsEnabled` property for external queries. All log methods (`Log`, `LogSeparator`, `LogException`) check `_enabled` first. |
| `hook/StartupHook.cs` | Dev-channel log gate at top of `InjectorLoop()`: loads settings, checks `AutoUpdateChannel`, calls `Logger.Disable()` if not "developer". Safe fallback on settings load failure (keeps logging enabled). |
| `hook/ThemeEngine.cs` | Removed `ThemeAccentColor`/`ThemeAccentBrush` overrides from `ApplyPingColorOverride()` — global accent keys were bleeding the ping color into buttons and active-state UI; visual tree walk already paints the correct controls directly |
| `hook/UprootedSettings.cs` | Added `LastPackageHash` property (SHA-256 of last applied .uprpkg); load/save via `AutoUpdate.LastPackageHash` INI key |
| `hook/AutoUpdater.cs` | Hash-based same-version hotfix detection: compute SHA-256 of downloaded .uprpkg, compare to stored hash, skip if identical, apply and store new hash if different. `BackgroundUpdateApplied` static event fires when background timer applies an update (not on manual check). `_isManualCheck` flag distinguishes the two flows. `ComputeSha256Hex()` helper. |
| `hook/ContentPages.cs` | `ShowUpdateNotification(r, version)` — dismissable overlay card shown when `BackgroundUpdateApplied` fires; `DismissUpdateNotification()` removes it. `CheckForUpdate(isManual: true)` wired to button. |
| `hook/StartupHook.cs` | Subscribes to `AutoUpdater.BackgroundUpdateApplied` in Phase 4.5d; dispatches `ContentPages.ShowUpdateNotification()` to UI thread |
| `hook/MessageLogger.cs` | Injection position fix: `_orderedIds` (List) + `_orderedIdIndex` (dict) track collection insertion order. `InjectDeletedMessageCards` now uses order-index comparison instead of timestamps to find the correct adjacent container. Cleared on channel switch / reset / bulk-flush. |
| `hook/MessageLogger.cs` | Edit detection + indicators: `HandleReplaced()` (Replace action=2) with `_addedViaEvent` + `EditGracePeriodSeconds` gates; `InjectEditIndicators()` + `BuildEditIndicatorCard()` amber-tinted card injection; `SetFontStyleItalic()`, `ReadHasBeenEdited()` helpers. 1795 → 1876 lines. |
| `hook/SilentTypingEngine.cs` | **Fixed**: broadened HttpClient discovery (removed keyword gates, depth 8→12), added `HttpMessageHandler` detection, added `GrpcChannel`-aware patching (extracts `<HttpInvoker>k__BackingField` and patches `_handler`). Root uses `Grpc.Net.Client.GrpcChannel` (not raw HttpClient) for gRPC — previous scan patched analytics/connection clients but missed the gRPC transport. Now confirmed blocking SetTypingIndicator. Case-insensitive URL matching. 335→482 lines. |
| `hook/ContentPages.cs` | Stripped em dashes from SentryBlocker, Themes, ClearURLs, ContentFilter plugin descriptions. |
| `hook/NsfwFilter.cs` | Complete rewrite — Avalonia-native visual tree scanner replaces old DotNetBrowser JS injection. 500ms scan timer posts `ScanForImages()` to UI thread (Interlocked guard prevents queuing overlap). DFS walk finds `Image` controls: skips tagged/tiny (<100×100 PixelSize)/seen bitmaps, queues `ClassifyAndAct` on thread pool. `SemaphoreSlim(3)` caps concurrent Vision API calls. `GetBitmapBytes()` encodes via `Bitmap.Save(Stream)` reflection → PNG base64 → `POST /v1/images:annotate?key=`. `ParseLikelihood()` string-scans response (no JSON parser). NSFW images hidden, overlay injected (`Border > StackPanel > [⚠ NSFW, Click to reveal]`), Grid.Row/Col mirrored from image, click-to-reveal wired via `SubscribePointerEvent`. `RevealAllBlocked()` restores all on disable. No DotNetBrowserReflection dependency. 473 lines. |
| `hook/StartupHook.cs` | Added Phase 4.5f (SilentTypingEngine, 12s delay); Phase 4.5g (NsfwFilter, Avalonia-native, 20s delay); Phase 5 now DotNetBrowser-only (NSFW no longer gated on DotNetBrowser). |
| `src/plugins/silent-typing/index.ts` | Gutted fetch/XHR intercept; now a no-op stub (v0.1.0 → v0.2.0) with comment explaining C# handles interception. |
| `hook/ContentPages.cs` | SilentTyping plugin entry: version `0.1.0` → `0.2.0`, `TestingStatus` `0` → `1` (beta). |
| `hook/ContentPages.cs` | Fixed About > Status enabled plugin count: iterates `KnownPlugins` with same enabled-check as Plugins page (was counting ghost entries like `settings-panel` from legacy migration). Also: `Logger.Enable()` call on developer channel switch, `Logger.Disable()` + log reorder on stable channel switch. |
| `hook/Logger.cs` | Added `Enable()` method for runtime re-enable when switching to developer channel. |
| `hook/SidebarInjector.cs` | Save bar: replaced all `SetIsVisible`/`ClearValue` calls with `Opacity=0`/`MaxHeight=0`/`IsHitTestVisible=false` — avoids corrupting Root's `IsVisible` binding. `CollapseSaveBar()`/`RestoreSaveBar()` methods with `_saveBarCollapsed` flag for efficient LayoutUpdated intercept. |
| `hook/RootcordEngine.cs` | User card overhaul (2026-02-20): removed `_profileInterceptHandler` + `_allowProfileOnce` (blocked all ProfileOpen events — counterproductive with PanePlacement=Left guard active). Avatar grid + textPanel now have PointerPressed → ProfilePaneToggleCommand. Replaced SystemTray reparenting with always-on 4-button cluster: P (FriendsPaneToggle), D (DirectMessagesPaneToggle), N (NotificationsPaneToggle), ⚙ (ProfilePaneToggle). Dead fields removed: `_userBarTrayHost`, `_savedTrayRow/Col/ColSpan/RowSpan`. RevertUserBar simplified. |
| `hook/RootcordEngine.cs` | RefreshSelectedHighlight hardening (2026-02-20): "Object type Avalonia.Controls.Decorator does not match target type Avalonia.Controls.Grid" crash on every server icon click. Root cause: `GetBorderChild` has no try/catch; `_borderChild.GetValue()` throws when child[1] in strip container is a Decorator, not Border. Added IsBorder() guard before every GetBorderChild call + try/catch around each RefreshSelectedHighlight section. Logs diagnostic type name when non-Border is encountered. |
| `hook/SidebarInjector.cs` | Settings reload on nav click: `_settings` changed from `readonly` to mutable; `OnNavItemClicked` calls `UprootedSettings.Load()` before building page so channel badge reflects runtime changes. |
| `hook/ContentPages.cs` | Restart banner persists across tab switches: static `_launchPluginStates` snapshots once on first build (not re-snapshotted on rebuild). Banner starts visible on rebuild if any plugin already diverges from launch state. |
| `hook/UprootedSettings.cs` | Fixed: added `case "Version":` to INI parser — Version was never read from disk, breaking version migration detection |
| `hook/StartupHook.cs` | Fixed: force-disable on upgrade now sets `NsfwFilterEnabled = false` for `content-filter` (canonical toggle, not just `Plugins` dict). Uncommented `ForceDisableOnUpgrade` entry for v0.4.0: message-logger + content-filter. |

## MessageLogger Plugin (WIP — 2026-02-19)

**Status:** Major reliability overhaul deployed — property resolution fixed (DeletedAt/EditedAt), author names working, INPC event-driven detection added, self-delete fallback added. Needs real-world validation.

### Working
- **Phase 1 discovery**: Finds `RootMessageItemsControl` in visual tree, resolves ViewModel property accessors (MessageId, Content, AuthorId, Timestamp) via per-type cache
- **Collection subscription**: `ObservableCollection.CollectionChanged` via `Expression.Lambda` — handles Add, Remove, Replace, Reset events
- **Per-type property cache**: `Dictionary<Type, TypeProps>` handles multiple ViewModel types (MessageViewModel, ChannelStartMessageViewModel, ReplyUserTagViewModel) with nested `.Message` bridge property resolution
- **Property resolution (FIXED)**: Root's Message model uses `DeletedAt` (DateTimeOffset?) and `EditedAt` (DateTimeOffset?), NOT `HasBeenDeleted`/`IsDeleted` (bool). Non-null = deleted/edited. FindProp search order: `DeletedAt` → `HasBeenDeleted` → `IsDeleted` (with EditedAt equivalent).
- **Author name resolution (FIXED)**: Deep property chain: `Message.SenderMember` → `.GlobalUser` → `.UserName` (with `DisplayName`/`Name` fallbacks). Replaces hardcoded "Unknown".
- **INotifyPropertyChanged subscription**: Subscribes to `Message.PropertyChanged` for instant `DeletedAt`/`EditedAt` detection via `Expression.Lambda` delegate. Primary detection method. Cleanup on epoch change.
- **Async deletion pollers (fallback)**: Each removed item gets an async poller that checks `DeletedAt` every 300ms for 3s. Non-null DateTimeOffset = genuine deletion. Null after 3s triggers collection-presence fallback.
- **Self-delete fallback**: Root doesn't set `DeletedAt` for client-initiated deletes — it only removes from collection. After 3s polling timeout, checks if message ID is absent from live collection. If gone + has cached content → marked as self-deleted.
- **Dedup already-deleted**: `HandleRemoved` skips spawning pollers for messages already marked as deleted in cache.
- **Diagnostic instrumentation**: DIAG-INJ/DIAG-FLUSH logging, boolean + DateTimeOffset? property dumps, `_boolDumpCount` reset on epoch change
- **Channel switch handling**: Epoch-based cancellation, PropertyChanged unsubscribe, dump counter reset
- **Flat-file persistence**: `MessageStore.cs` — pipe-delimited, URI-encoded, append-only (MSG|DEL|EDIT|CLR record types)
- **Visual indicators**: Discord-style deleted message rows — full-width red-tinted background stripe with red left accent border
- **Scan interval**: 500ms (reduced from 1500ms) for faster VirtualizingStackPanel recycling recovery

### Known Issues
- **Card injection positioning**: `FindMessageGridInContainer` returns null — containers have `MessageView` descendants but no `RootMarkdownTextBlock` inside a Grid. Container structure may have changed; needs investigation.
- **Self-delete fallback untested**: Deployed but not yet validated by user. Second deploy cycle was cut short.

### Working — Edit Detection + Indicators (2026-02-19)
- **Dual-strategy edit detection**: (1) Primary: check `EditedAt` non-null on new item during Replace event (bypasses grace period); (2) Fallback: 5s grace period gating on Replace events without EditedAt change. Re-subscribes PropertyChanged on Replace.
- **INPC edit detection**: `OnEditedAtChanged` fires when `EditedAt` property changes on a subscribed message — instant detection.
- **Discord-style edit indicators**: `InjectEditIndicators()` runs each scan tick — finds edited messages in VSP visible children, injects amber-tinted inline card. Tag `uprooted-edit:{msgId}` for dedup + re-injection on scroll recycling.

### Key Learnings
- **Root's ObservableCollection is VIRTUALIZED** — Remove events fire for buffer management (scroll-off, window shifts), not just real deletions
- **DeletedAt (DateTimeOffset?) is the correct property** — NOT `HasBeenDeleted` (bool). Non-null = deleted. Only set for server-side/other-user deletions.
- **Self-deletes don't set DeletedAt** — Root removes from collection without setting the property. Must use collection-presence fallback.
- **Message implements INotifyPropertyChanged** — confirmed via ILSpy `MessageView.cs:308`. PropertyChanged is a standard CLR event (NOT a RoutedEvent), so `EventInfo.AddEventHandler` is safe.
- Root fires straggler Remove events 8-12s after channel switch (buffer settling) — epoch-based cancellation handles this
- Different ViewModel types have different `PropertyInfo` objects — per-type cache is essential
- Root creates new `RootMessageItemsControl` instances on channel switch

## Test Suite (2026-02-18)

170 xUnit tests, all passing, zero bugs found. Three modules now have full coverage:

| Module | Tests | Notes |
|--------|-------|-------|
| `ColorUtils` | 57 | HSL/HSV/RGB math |
| `GradientBrush` | 15 | Avalonia gradient via reflection |
| `ClearUrlsEngine.CleanUrl` | 58 | All 33 params, edge cases |
| `UprootedSettings` | 22 | INI parse, coercion, cache, migration |
| `MessageStore` | 18 | Records, URI encode, Truncate |

**Infra:** `tests/Dockerfile.unittest` (SDK 10.0 container), `tests/run-docker-tests.sh`, `tests/docker-installer/` (Ubuntu 24.04 installer sandbox).
See `docs/dev/TESTING.md` for full reference.

## Next Steps

Priority order per TASKS.md: bugfixes → visual consistency → performance → anti-mitigation → organization → hardening → experimental.

1. **Version copy intercept** — SidebarInjector clipboard handler commented out, needs fix for Root's async `SetTextAsync` race.
2. **MessageLogger card positioning** — `FindMessageGridInContainer` returns null; container structure investigation needed.
3. **Uprooted tab header recolor** — Injected sidebar text doesn't update on custom theme changes.
4. **Root "Online" indicator** — Doesn't live-update with custom text color.
5. **Structural fallback for settings detection** — Remove "APP SETTINGS" text dependency.
6. **Experimental plugin validation** — Rootcord, MessageLogger, NsfwFilter, Translate, Presence Beacon all deployed but need real-world testing.

## Rootcord Plugin (v0.4.2 — 2026-02-20)

**Status:** Functionally rebuilt. User card/button wiring corrected. Tab-switch highlight crash guarded. Awaiting validation with Rootcord actually enabled + server switching.

### Architecture (current)

| Area | Implementation |
|------|----------------|
| Server strip | `_serverStrip` StackPanel (vertical), Col 0 of HomeView grid; server icons + DM home button injected |
| User card | `_userBar` Border, ZIndex=10, ColSpan=all cols, Col 0, Row 1; floats over channel list as 240px overlay |
| User card — Col 0 | Avatar grid (`avatarGrid`) with PointerPressed → `ProfilePaneToggleCommand` |
| User card — Col 1 | Text panel (username + discriminator) with PointerPressed → `ProfilePaneToggleCommand` |
| User card — Col 2 | 4-button StackPanel: P→`FriendsPaneToggleCommand`, D→`DirectMessagesPaneToggleCommand`, N→`NotificationsPaneToggleCommand`, ⚙→`ProfilePaneToggleCommand` |
| PanePlacement guard | `_panePlacementGuardHandler` re-asserts `SplitView.PanePlacement=Left` if Root resets it |
| Members swap | `SwapCommunityMembersToRight()` — column rotation in layoutGrid, margin/padding clear |
| Flyout flip | `FlipMemberFlyoutPlacements()` — flips `RightEdgeAlignedTop`→`LeftEdgeAlignedTop` in tree |

### Key Fixes Applied

**Profile intercept removed**: `_profileInterceptHandler` subscribed to `HomeViewModel.ProfileOpen`
and reset it to false (closing the pane) on every toggle — UNLESS `_allowProfileOnce=true` was set
first. The "S" button never set that flag. With `PanePlacement=Left` now enforced by the guard, the
intercept was counterproductive. Removed entirely.

**4-button cluster corrected**: Was trying to reparent Root's native `_systemTrayBorder` (Row 1, Col 4
of HomeView grid) — fragile and error-prone. Now always uses 4 custom buttons with the correct
HomeViewModel commands. SystemTray reparenting code + save/restore state fully removed.

**Avatar/textPanel click**: Both avatar grid and text panel now have PointerPressed handlers that invoke
`ProfilePaneToggleCommand`, making the whole left portion of the user card clickable for profile.

### Known Issue — Tab Switch Highlight Crash

`RefreshSelectedHighlight()` throws on every server icon click:
```
Object type Avalonia.Controls.Decorator does not match target type Avalonia.Controls.Grid.
```

**Root cause**: `GetBorderChild(border)` uses `_borderChild = BorderType.GetProperty("Child")` — a
PropertyInfo whose DeclaringType is `Border`. When `border` is actually a `Decorator` (a supertype),
`_borderChild.GetValue(Decorator)` fails because `Grid`-specific targets in the PropertyInfo's
declaring type don't match. The server strip container's `children[1]` is sometimes a Decorator in
Root's layout rather than a Border.

**Defensive fix applied**: `IsBorder(child)` guard before every `GetBorderChild` call. Each logical
block wrapped in try/catch. Non-Border types logged as `RefreshHL: strip[N] child[1] is Decorator`.

**Status**: Awaiting test run with Rootcord enabled + server switching to confirm guards fire and which
visual element type Root uses in those slots.

### Recon Plugin Note

The [Recon] plugin (user's custom plugin) fires on settings page open, not HomeView. Output goes to
`uprooted-hook.log` as `[Recon]` entries (not a separate file). Useful for capturing style properties
of nav sidebar controls. Not yet fired during Rootcord-active HomeView interaction.

## ClearURLs Engine Notes

- Root's compose input is `AvaloniaEdit.TextEditor` (NOT `Avalonia.Controls.TextBox`)
- AvaloniaEdit marks Enter as `Handled=true` — CLR event handlers don't see it
- Must use `AddHandler(RoutedEvent, Delegate, RoutingStrategies, handledEventsToo=true)` with ALL routing strategies (Bubble|Tunnel|Direct = 7)
- Bubble alone is insufficient — AvaloniaEdit handles Enter before Bubble propagates
- Hook TextArea (child of TextEditor) as primary target; read/write Text from parent TextEditor
- TextEditor.Text CLR property works (AvaloniaEdit is third-party, not trimmed)

## Deployment

```powershell
# Use deploy-hook.ps1 or manually:
Stop-Process -Name Root,chromium -Force -ErrorAction SilentlyContinue
Start-Sleep -Seconds 5
Copy-Item -Force 'hook\bin\Release\net10.0\UprootedHook.dll' "$env:LOCALAPPDATA\Root\uprooted\UprootedHook.dll"
# Then launch Root manually
```

Hook log: `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log`
