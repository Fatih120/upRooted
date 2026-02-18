# Uprooted Hook - Session State (2026-02-17)

## Release: v0.3.2

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
| 5 | StartupHook | DotNetBrowser: event-driven assembly detection, type resolution, NSFW + link embeds |

## Files Modified in This Session

| File | Changes |
|------|---------|
| `hook/DotNetBrowserReflection.cs` | Fixed assembly gate (removed AvaloniaUi), added `FindBrowserDirect()` with 5 strategies, `GetAllFrames()`, profile→browser navigation via private ConcurrentDictionary, broadened ExecJS search, enhanced diagnostics |
| `hook/StartupHook.cs` | Event-based assembly detection (ManualResetEventSlim + AssemblyLoad), 90s timeout |
| `hook/LinkEmbedEngine.cs` | Avalonia-native link embed engine (1260 lines): visual tree scan, OG metadata fetch via reflection-based HttpClient, native embed card injection. Replaces dead-end LinkEmbedInjector.cs |
| `hook/NsfwFilter.cs` | FindBrowserDirect fallback in TryInject() |

## Next Steps

Link embed engine is live (Phase 4.5b) but needs improvement for non-YouTube sites:
1. **Better User-Agent** — Twitter/X and others reject `Uprooted/0.2` bot UA; needs browser-like UA string
2. **Direct image URL detection** — Check Content-Type before parsing HTML; render image-only embeds for `.jpg`, `.png`, `.gif` URLs
3. **Fallback oEmbed providers** — Twitter, Reddit, etc. have oEmbed endpoints like YouTube does
4. **JS-rendered OG fallback** — Some sites serve no OG tags in static HTML; may need DotNetBrowser fetch as fallback

## Deployment

```powershell
Stop-Process -Name Root,chromium -Force -ErrorAction SilentlyContinue
Start-Sleep -Seconds 5
Copy-Item -Force 'hook\bin\Release\net10.0\UprootedHook.dll' "$env:LOCALAPPDATA\Root\uprooted\UprootedHook.dll"
# Then launch Root manually
```

Hook log: `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log`
