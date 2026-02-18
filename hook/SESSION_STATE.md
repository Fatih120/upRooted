# Uprooted Hook - Session State (2026-02-18)

## Release: v0.3.5

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
| 5 | StartupHook | DotNetBrowser: event-driven assembly detection, type resolution, NSFW + link embeds |

## Link Embed Engine (v0.3.3–0.3.5)

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
| `hook/SidebarInjector.cs` | Back arrow management, header title, DetachedFromVisualTree safety net, Click events, section header 40px wrapper |
| `hook/ContentPages.cs` | Renamed: "Plugins" → "Plugin Settings", "Themes" → "Theme Settings"; Cosmic Smoothie preset; search box fix |
| `hook/ThemeEngine.cs` | Added "cosmic-smoothie" theme: TreeColorMap + Themes ResourceDictionary |

## Next Steps

1. **Reddit link embeds** — Reddit serves OG tags to crawlers; add dedicated handling
2. **Video preview embeds (.mp4)** — Thumbnail + play button for direct video URLs
3. **Avalonia-native NSFW filter** — Redesign to intercept image controls in visual tree
4. **Plugin toggle functionality** — Wire up Plugins page toggles for runtime enable/disable

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
