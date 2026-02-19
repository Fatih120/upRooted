# Uprooted Hook - Session State (2026-02-18)

## Release: v0.3.6-rc

## Critical Finding: Root's Chat is Avalonia-Native

Root v0.9.92's chat UI is rendered **entirely in native Avalonia controls**, NOT in DotNetBrowser.
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
- Root v0.9.92.0, RootApp.Client.Avalonia v0.9.92.0
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
| 4.5d | AutoUpdater | Background update check (every 6 hours), encrypted .uprpkg download + decrypt + apply |
| 4.5e | ProfileBadgeInjector | "Uprooted Dev" badge on profile popup (dev channel + hardcoded usernames, 5s delay, event-driven + 500ms fallback) |
| 4.5f | SilentTypingEngine | HttpClient handler injection: static field scan + ViewModel chain walk, TypingBlockerHandler drops SetTypingIndicator (12s delay) |
| 4.5g | NsfwFilter | NSFW content filter (Avalonia-native visual tree scan, 20s delay) |
| 5 | StartupHook | DotNetBrowser: event-driven assembly detection, type resolution (video thumbnails for LinkEmbedEngine) |

## Link Embed Engine (v0.3.3–0.3.6-rc)

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

## Files Modified Recently

| File | Changes |
|------|---------|
| `hook/ClearUrlsEngine.cs` | New file: strips tracking params from compose editor URLs on Enter key via AvaloniaEdit.TextArea routed event interception |
| `hook/StartupHook.cs` | Added Phase 4.5a: ClearURLs engine registration (14s delay) |
| `hook/ContentPages.cs` | Added ClearURLs PluginInfo entry (clear-urls, default enabled, TestingStatus=1) |
| `hook/LinkEmbedEngine.cs` | Chrome-like UA, bot UA for Twitter/X, embed-fixer normalization, oEmbed discovery, Content-Type gate, direct image fast path, twitter:* fallbacks, verbose logging, Reddit embeds (old.reddit.com OG, subreddit label, orange accent), HTTP status/Content-Type validation in HttpGetBytes, OG fallback for image-extension HTML pages, robust OG regex, image border corner fix |
| `src/plugins/link-embeds/providers.ts` | Robust OG regex: explicit attribute bridge handles meta tags with extra attributes between property and content |
| `hook/AnimatedImage.cs` | Animated GIF/WebP decoder + timer playback via SkiaSharp reflection |
| `hook/UprootedSettings.cs` | 10s TTL settings cache to reduce disk I/O |
| `hook/SidebarInjector.cs` | Back arrow management, DetachedFromVisualTree safety net, Click events for Buttons, section header 40px wrapper, UPROOTED section spacing, instant Uprooted→Root tab transitions via ListBox.SelectionChanged, LayoutUpdated event-based detection for same-frame settings injection (diagnostics deferred after injection) |
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
| `hook/MessageLogger.cs` | Edit detection + indicators: `HandleReplaced()` (Replace action=2) with `_addedViaEvent` + `EditGracePeriodSeconds` gates; `InjectEditIndicators()` + `BuildEditIndicatorCard()` amber-tinted card injection; `SetFontStyleItalic()`, `ReadHasBeenEdited()` helpers. 1795 → 1809 lines. |
| `hook/SilentTypingEngine.cs` | New file: C# silent typing engine. `SilentTypingEngine` scans assemblies + walks ViewModel chain to find Root's `HttpClient` instances and prepends `TypingBlockerHandler` to each. Handler drops `SetTypingIndicator` gRPC requests, returns synthetic `200 OK`. Timer-based discovery (12s startup delay, 5s retry, 30s once patched). |
| `hook/StartupHook.cs` | Added Phase 4.5f (SilentTypingEngine, 12s delay); Phase 4.5g (NsfwFilter, Avalonia-native, 20s delay); Phase 5 now DotNetBrowser-only (NSFW no longer gated on DotNetBrowser). |
| `src/plugins/silent-typing/index.ts` | Gutted fetch/XHR intercept; now a no-op stub (v0.1.0 → v0.2.0) with comment explaining C# handles interception. |
| `hook/ContentPages.cs` | SilentTyping plugin entry: version `0.1.0` → `0.2.0`, `TestingStatus` `0` → `1` (beta). |
| `hook/ContentPages.cs` | Fixed About > Status enabled plugin count: iterates `KnownPlugins` with same enabled-check as Plugins page (was counting ghost entries like `settings-panel` from legacy migration). Also: `Logger.Enable()` call on developer channel switch, `Logger.Disable()` + log reorder on stable channel switch. |
| `hook/Logger.cs` | Added `Enable()` method for runtime re-enable when switching to developer channel. |
| `hook/SidebarInjector.cs` | Save bar restore: replaced `ClearValue("IsVisibleProperty")` with `SetIsVisible(true)` — simpler, equivalent behavior. |

## MessageLogger Plugin (WIP — 2026-02-18)

**Status:** Async deletion pollers deployed — uses `HasBeenDeleted` property on bridge target (Message model) to distinguish real deletions from buffer management removes.

### Working
- **Phase 1 discovery**: Finds `RootMessageItemsControl` in visual tree, resolves ViewModel property accessors (MessageId, Content, AuthorId, Timestamp) via per-type cache
- **Collection subscription**: `ObservableCollection.CollectionChanged` via `Expression.Lambda` — handles Add, Remove, Replace, Reset events
- **Per-type property cache**: `Dictionary<Type, TypeProps>` handles multiple ViewModel types (MessageViewModel, ChannelStartMessageViewModel, ReplyUserTagViewModel) with nested `.Message` bridge property resolution
- **Async deletion pollers**: Each removed item gets an async poller that checks `HasBeenDeleted` every 300ms for 3s. If `HasBeenDeleted` becomes true → genuine deletion. If it stays false → buffer management (discard). Epoch counter cancels stale pollers on channel switch.
- **Diagnostic instrumentation**: DIAG-INJ/DIAG-FLUSH exhaustive logging, boolean property dump on first Remove event to discover correct deletion property, collection size and snapshot age tracking
- **Channel switch handling**: Epoch-based cancellation — each channel switch increments a counter, pollers check their birth epoch against current and self-cancel if stale
- **Flat-file persistence**: `MessageStore.cs` — pipe-delimited, URI-encoded, append-only (MSG|DEL|EDIT|CLR record types)
- **Visual indicators**: Discord-style deleted message rows — full-width red-tinted background stripe with red left accent border, right-click "Clear message history" context menu
- **Analysis tool**: `scripts/analyze-msglogger.ps1` — parses hook log for MsgLogger/DIAG entries and produces structured diagnostic summary

### Known Issues — Being Fixed
- **HasBeenDeleted reliability**: The async poller approach depends on `HasBeenDeleted` being set to true by Root within the 3s polling window. Needs real-world validation — if Root sets it asynchronously after a longer delay, the timeout may need adjustment.

### Working — Edit Detection + Indicators (2026-02-18)
- **Edit detection**: Event-driven via `HandleReplaced()` (CollectionChanged Replace action=2). Two false-positive gates: (1) message must have been seen via an Add event (`_addedViaEvent` dict, not initial snapshot); (2) Replace must arrive >5s after Add (`EditGracePeriodSeconds = 5.0`), filtering send-completion optimistic Replaces (arrive within 0.5–2s). `PollEdits` method retained but not called.
- **Discord-style edit indicators**: `InjectEditIndicators()` runs each scan tick — finds edited messages in VSP visible children, injects amber-tinted inline card as new Grid row. Card shows previous content (faded `#99BBBBBB`, italic) + `(edited)` / `(edited Nx)` label (amber `#99D4A843`), amber left accent border (3px `#50FFCC44`). Tag `uprooted-edit:{msgId}` for dedup + re-injection on scroll recycling.

### Key Learnings
- **Root's ObservableCollection is VIRTUALIZED** — Remove events fire for buffer management (scroll-off, window shifts), not just real deletions. The original assumption (Remove = real deletion) was wrong.
- **HasBeenDeleted on bridge target** is the primary signal — true means genuine deletion, false means buffer management
- Root fires straggler Remove events 8-12s after channel switch (buffer settling) — epoch-based cancellation handles this
- Different ViewModel types have different `PropertyInfo` objects that throw `TargetException` on wrong types — per-type cache is essential
- Root creates new `RootMessageItemsControl` instances on channel switch; old ones linger in the visual tree with stale data

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

1. **MessageLogger: validate async pollers + edit detection** — Async deletion pollers (HasBeenDeleted, 300ms/3s) deployed; injection position and edit indicators deployed. Need real-world validation: test actual deletions + edits, run `scripts/analyze-msglogger.ps1`. Confirm HasBeenDeleted fires within 3s, confirm edit cards appear after grace period.
2. **SilentTyping: validate C# interception** — `SilentTypingEngine` deployed (Phase 4.5f). Verify with two accounts: type in one session, confirm the other sees no typing indicator. Check log for `[SilentTyping] Blocked SetTypingIndicator` entries. If nothing is blocked, log will show which `HttpClient` instances were found/missed.
3. **Avalonia-native NSFW filter** — Phase 4.5g deployed; verify Avalonia-native redesign is working
6. **Refine ProfileBadgeInjector heuristics** — Check tree dump logs to refine `IsProfilePopup` (may false-positive on non-profile popups); detection and dev-gating are done

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
