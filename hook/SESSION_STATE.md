# Uprooted Hook - Session State (2026-02-18)

## Release: v0.3.6rc

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
| 1 | StartupHook | Wait for Avalonia assemblies (30s) |
| 2 | StartupHook | Wait for Application.Current (30s) |
| 3 | StartupHook | Wait for MainWindow (60s) |
| 3.5 | StartupHook | Initialize ThemeEngine + apply saved theme |
| 4 | SidebarInjector | Start settings page monitor (200ms poll) |
| 4.5 | BrowserDiscovery | Dump visual tree + assembly scan (diagnostic) |
| 4.5a | ClearUrlsEngine | Strip tracking params from compose editor on Enter (14s delay) |
| 4.5b | LinkEmbedEngine | Avalonia-native link embeds (OG + oEmbed + animated images) |
| 4.5c | MessageLogger | Message logger: discovery + collection subscription + visual indicators |
| 4.5d | AutoUpdater | Background update check (every 6 hours), download + apply |
| 4.5e | ProfileBadgeInjector | "Uprooted Dev" badge on profile popup (developer channel only, 25s delay) |
| 5 | StartupHook | DotNetBrowser: event-driven assembly detection, type resolution, NSFW + link embeds |

## Link Embed Engine (v0.3.3–0.3.6rc)

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
- Tenor URL skip (Root embeds natively)
- Settings cache: 10s TTL on `UprootedSettings.Load()`

**Known limitations:**
- Reddit embeds not yet implemented (OG tags available but no dedicated handler)
- Video preview embeds (.mp4) not yet implemented
- JS-rendered OG fallback not available (some sites serve no OG in static HTML)

## Files Modified Recently

| File | Changes |
|------|---------|
| `hook/ClearUrlsEngine.cs` | New file: strips tracking params from compose editor URLs on Enter key via AvaloniaEdit.TextArea routed event interception |
| `hook/StartupHook.cs` | Added Phase 4.5a: ClearURLs engine registration (14s delay) |
| `hook/ContentPages.cs` | Added ClearURLs PluginInfo entry (clear-urls, default enabled, TestingStatus=1) |
| `hook/LinkEmbedEngine.cs` | Chrome-like UA, bot UA for Twitter/X, embed-fixer normalization, oEmbed discovery, Content-Type gate, direct image fast path, twitter:* fallbacks, verbose logging |
| `hook/AnimatedImage.cs` | Animated GIF/WebP decoder + timer playback via SkiaSharp reflection |
| `hook/UprootedSettings.cs` | 10s TTL settings cache to reduce disk I/O |
| `hook/SidebarInjector.cs` | Back arrow management: hide left-side RootSvgButton by position, set header title TextBlock, DetachedFromVisualTree safety net, Click events for Buttons, section header 40px wrapper, UPROOTED section spacing (16px top margin, -4px header bottom margin), instant Uprooted→Root tab transitions via ListBox.SelectionChanged |
| `hook/ContentPages.cs` | Renamed: "Plugins" → "Plugin Settings", "Themes" → "Theme Settings"; added Cosmic Smoothie preset card; search box font/padding/centering fix |
| `hook/ThemeEngine.cs` | Added "cosmic-smoothie" theme: TreeColorMap (26 color mappings) + Themes ResourceDictionary (full FluentTheme key set) |
| `src/plugins/themes/themes.json` | Added "cosmic-smoothie" theme entry with CSS variables |
| `hook/MessageLogger.cs` | Chat message logger (1393 lines) — per-type property cache, event-based deletion detection with diagnostic instrumentation (HasBeenDeleted probe, Add/Remove correlation, remove index tracking, one-time property dump), bulk threshold raised to 10, flush-before-clear on channel switch, Discord-style deleted message rows |
| `hook/MessageStore.cs` | Flat-file persistence for message log (pipe-delimited, URI-encoded, append-only: MSG\|DEL\|EDIT\|CLR records) |
| `hook/StartupHook.cs` | Added Phase 4.5c message logger initialization (20s delay for chat population) |
| `hook/UprootedSettings.cs` | Added MessageLogger.LogDeletes, LogEdits, IgnoreSelf, MaxMessages settings |
| `hook/ContentPages.cs` | Added message-logger to KnownPlugins, settings lightbox with toggle UI, BuildChannelRow for update channel switching, restart banners (plugins + updates), state-aware plugin banner (hides on revert), Restart button (Process.Start + Environment.Exit), DIAGNOSTICS "Open" button (explorer /select) |
| `hook/ProfileBadgeInjector.cs` | New file: profile popup detection via TopLevel/OverlayLayer scan, heuristic matching (avatar + username + roles), tree dump diagnostics, role pill container discovery, "Uprooted Dev" badge injection (gold #8B6914 pill) |
| `hook/StartupHook.cs` | Added Phase 4.5e profile badge injector (25s delay, developer channel only) |

## MessageLogger Plugin (WIP — 2026-02-18)

**Status:** Diagnostic build deployed — gathering data to distinguish real deletions from buffer management removes.

### Working
- **Phase 1 discovery**: Finds `RootMessageItemsControl` in visual tree, resolves ViewModel property accessors (MessageId, Content, AuthorId, Timestamp) via per-type cache
- **Collection subscription**: `ObservableCollection.CollectionChanged` via `Expression.Lambda` — handles Add, Remove, Replace, Reset events
- **Per-type property cache**: `Dictionary<Type, TypeProps>` handles multiple ViewModel types (MessageViewModel, ChannelStartMessageViewModel, ReplyUserTagViewModel) with nested `.Message` bridge property resolution
- **Diagnostic instrumentation** (Phase 1): Probes `HasBeenDeleted` on every removed item, tracks Add/Remove correlation per flush window, logs remove indices (`OldStartingIndex`), one-time dump of ALL boolean properties on first Remove event
- **Deletion detection**: Event-based via Remove events (not polling). Buffered per-tick with debounce:
  - 10+ removes in one tick = channel switch → discard
  - Fewer removes: checked individually against settling filter and diagnostic data
- **Flush-before-clear**: Channel switch paths flush buffered removes before discarding, preventing real deletions from being lost
- **Post-subscription settling filter**: Messages present at subscription time (`_initialSnapshotIds`) are protected from false-positive removes for 30s after channel switch. Messages arriving AFTER subscription (via Add events) are trusted immediately — zero delay
- **Channel switch handling**: Bulk removes detected, control freshness check every ~7.5s detects when Root swaps `RootMessageItemsControl` instances
- **Flat-file persistence**: `MessageStore.cs` — pipe-delimited, URI-encoded, append-only (MSG|DEL|EDIT|CLR record types)
- **Visual indicators**: Discord-style deleted message rows — full-width red-tinted background stripe with red left accent border, right-click "Clear message history" context menu

### Known Issues — Being Fixed
- **False positives**: Root's ObservableCollection IS a windowed/virtualized data source — Remove events fire for buffer management (scroll-off, window shifts), NOT just real deletions. Diagnostic build deployed to discover how to distinguish the two.
- **Wrong injection position**: Deleted message cards always appear at the bottom of the chat panel instead of in-place near the original message position.

### Not Yet Working / Disabled
- **Edit detection**: Disabled — `PollEdits` produces false positives from content changes during message send/render. `ApplyEditIndicators` injects TextBlocks that break Avalonia's message layout (greyed out messages, "(edited 12x)" on every message)
- **Discord-style edit indicators**: Planned — show original content faded above new content with "(edited)" label, matching Discord's MessageLogger style

### Key Learnings
- **Root's ObservableCollection is VIRTUALIZED** — Remove events fire for buffer management (scroll-off, window shifts), not just real deletions. The original assumption (Remove = real deletion) was wrong.
- Root fires straggler Remove events 8-12s after channel switch (buffer settling) — must be filtered
- Different ViewModel types have different `PropertyInfo` objects that throw `TargetException` on wrong types — per-type cache is essential
- Root creates new `RootMessageItemsControl` instances on channel switch; old ones linger in the visual tree with stale data

## Next Steps

1. **MessageLogger Phase 2** — Analyze diagnostic logs to determine deletion detection strategy: HasBeenDeleted property (best), Add/Remove correlation heuristic, or re-verification queue
2. **MessageLogger Phase 3** — Fix injection position (insert deleted cards near original message position, not at bottom)
3. **Fix MessageLogger edit detection** — Need reliable edit detection that doesn't false-positive on content changes during send/render
4. **Discord-style edit indicators** — Show OG content faded above new content with "(edited)" label
3. **Reddit link embeds** — Reddit serves OG tags to crawlers; add dedicated handling
4. **Video preview embeds (.mp4)** — Thumbnail + play button for direct video URLs
5. **Avalonia-native NSFW filter** — Redesign to intercept image controls in visual tree
6. **Refine ProfileBadgeInjector** — Check tree dump logs for actual popup structure, refine heuristics in `IsProfilePopup` and `FindRolesContainer`

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
