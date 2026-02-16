# Uprooted Hook - Session State (2026-02-16 21:42 UTC)

## What Was Done This Session

### Feature: NSFW Content Filter Plugin (v0.1.96)

Full implementation of a Google Cloud Vision SafeSearch-based content filter that blurs NSFW images in chat.

**New files created:**
1. **`hook/DotNetBrowserReflection.cs`** (+203 lines) — Reflection cache for DotNetBrowser types (BrowserView, IBrowser, IFrame). Scans assemblies prefixed with `"DotNetBrowser"`, resolves types/members, provides `FindBrowserView()`, `GetBrowser()`, `GetMainFrame()`, `ExecuteJavaScript()`. Follows AvaloniaReflection patterns.

2. **`hook/NsfwFilter.cs`** (+291 lines) — Filter lifecycle orchestration. Finds BrowserView in visual tree, gets IBrowser → IFrame, injects config JSON + JS filter script via `ExecuteJavaScript()`. 30s re-injection timer for page navigation. `UpdateConfig()` for live settings changes. `IDisposable` cleanup.

3. **`hook/nsfw-filter.js`** (+275 lines) — MutationObserver-based image filter injected into DotNetBrowser. Pre-blur (8px) during classification, full blur (25px) + "Click to reveal" overlay for NSFW. Rate limiting (5 concurrent, 30/min), URL cache (2000 entries), fail-open on errors. CSS classes: `.uprooted-nsfw-pending`, `.uprooted-nsfw-blur`, `.uprooted-nsfw-revealed`.

**Modified files:**
4. **`hook/UprootedSettings.cs`** — Added `NsfwFilterEnabled` (bool), `NsfwApiKey` (string), `NsfwThreshold` (double, default 0.6). Load/Save with invariant culture parsing.

5. **`hook/ContentPages.cs`** (+317 lines) — Added `"content-filter"` case to `BuildPage()`. New `BuildContentFilterPage()` with 4 cards: enable toggle, API key input + save button, sensitivity radios (Strict/Normal/Relaxed), How It Works info. Added `NsfwFilterInstance` static property for live config updates from settings UI.

6. **`hook/SidebarInjector.cs`** — Added `("Content Filter", "content-filter")` as fourth nav item in Uprooted sidebar section.

7. **`hook/StartupHook.cs`** (+51 lines) — Added Phase 5 after "Hook Ready". Non-blocking background thread: waits for DotNetBrowser assemblies (30s), resolves types, initializes NsfwFilter, sets `ContentPages.NsfwFilterInstance`.

8. **`hook/HtmlPatchVerifier.cs`** (+24 lines) — Embeds `window.__UPROOTED_NSFW_CONFIG__` JSON in HTML patches (dual injection: HTML patch catches early loads, JS execution handles runtime). Added `BuildNsfwConfigJson()`. Extended `FindTargetHtmlFiles()` for HostBundle directory. Added bare tag stripping for `__UPROOTED_NSFW_CONFIG__`.

9. **`hook/UprootedHook.csproj`** — Added `nsfw-filter.js` as content with `CopyToOutputDirectory=PreserveNewest`.

### Version Bump: 0.1.95 → 0.1.96
Bumped across 33 files: hook source, installer configs (Cargo.toml, tauri.conf.json, package.json), scripts, TypeScript plugins, site, docs, PKGBUILD, session state.

## Current Architecture

### Startup Phases
| Phase | File | Purpose |
|-------|------|---------|
| 0 | HtmlPatchVerifier | Verify/repair HTML patches + FileSystemWatcher |
| 1 | StartupHook | Wait for Avalonia assemblies (30s) |
| 2 | StartupHook | Wait for Application.Current (30s) |
| 3 | StartupHook | Wait for MainWindow (60s) |
| 3.5 | StartupHook | Initialize ThemeEngine + apply saved theme |
| 4 | SidebarInjector | Start settings page monitor (200ms poll) |
| 5 | NsfwFilter | **NEW** — Background thread: DotNetBrowser discovery + JS injection |

### Sidebar Nav Items
1. Uprooted → `BuildUprootedPage()`
2. Plugins → `BuildPluginsPage()`
3. Themes → `BuildThemesPage()`
4. Content Filter → `BuildContentFilterPage()` **NEW**

### NSFW Filter Architecture
```
C# side (NsfwFilter.cs):
  DotNetBrowserReflection → find BrowserView in Avalonia tree
  → IBrowser.MainFrame → IFrame.ExecuteJavaScript()
  → Injects: window.__UPROOTED_NSFW_CONFIG__ = {enabled, apiKey, threshold}
  → Injects: nsfw-filter.js (full MutationObserver script)
  → 30s timer re-checks injection (page navigation)

JS side (nsfw-filter.js):
  MutationObserver on document.body {childList, subtree}
  → New <img> detected → pre-blur(8px) → fetch Vision API
  → NSFW? → blur(25px) + overlay → click to reveal/re-blur
  → Safe? → remove pre-blur
  → Error? → fail open (show image)
  → Cache by normalized URL (2000 max)
  → Rate limit: 5 concurrent, 30/minute

Dual injection:
  HTML patch embeds config (catches early-loading images)
  + ExecuteJavaScript injects at runtime (handles config changes)
```

### Key Design Decisions
- **API calls in JS, not C#** — `--disable-web-security` eliminates CORS, avoids System.Text.Json
- **Pre-blur during classification** — prevents flash of NSFW content
- **Fail open** — errors never block image viewing, only add reveal step when flagged
- **Config JSON built manually** — no System.Text.Json (MissingMethodException in profiler context)

## Files Modified This Session
| File | State |
|------|-------|
| `hook/DotNetBrowserReflection.cs` | **NEW** — DotNetBrowser reflection cache |
| `hook/NsfwFilter.cs` | **NEW** — Filter orchestration |
| `hook/nsfw-filter.js` | **NEW** — JS filter script |
| `hook/UprootedSettings.cs` | Added 3 NSFW properties |
| `hook/ContentPages.cs` | Added Content Filter page |
| `hook/SidebarInjector.cs` | Added nav item |
| `hook/StartupHook.cs` | Added Phase 5 |
| `hook/HtmlPatchVerifier.cs` | NSFW config in patches |
| `hook/UprootedHook.csproj` | JS file copy to output |

## Pending / Potential Issues

1. **Cannot build-verify in this environment** — .NET 10 SDK not available in container. Code was verified via manual cross-reference of all type/method signatures. First real build should be done on dev machine.

2. **DotNetBrowser API surface unknown** — The reflection targets (`BrowserView.Browser`, `IBrowser.MainFrame`, `IFrame.ExecuteJavaScript`) are based on DotNetBrowser's documented API. If Root uses a different version or custom wrapper, the fallback type searches should catch it. If not, `IsResolved = false` and the filter silently disables.

3. **nsfw-filter.js deployment** — Script must be alongside `UprootedHook.dll` in the output directory (handled by csproj `CopyToOutputDirectory`). Alternatively, can be placed in the Uprooted assets directory (`PlatformPaths.GetUprootedDir()`).

4. **Settings page not yet visually tested** — The Content Filter page follows identical patterns to Uprooted/Plugins/Themes pages but hasn't been seen live. Radio indicators and API key save button need visual verification.

5. **Re-injection after page navigation** — The 30s timer re-injects the filter script. The script self-guards against double injection (`window.__UPROOTED_NSFW_ACTIVE__`). On navigation, the guard resets naturally. `UpdateConfig()` explicitly resets the guard to pick up new settings.

## Build & Test

```powershell
# Kill Root, build, launch with hook:
Stop-Process -Name Root -Force -ErrorAction SilentlyContinue; Start-Sleep 1
cd C:\Users\bash\claude\Root.Dev\uprooted\hook
dotnet build -c Release
# Then launch:
powershell -ExecutionPolicy Bypass -File ..\scripts\test-hook.ps1
# Check log:
Get-Content "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log" -Tail 30
```

### Testing the NSFW filter
1. Enable in Settings → Content Filter → toggle ON
2. Enter Google Cloud Vision API key → Save
3. Set sensitivity (Normal = default)
4. Send an image in a Root chat
5. Check log for Phase 5 messages: `[NsfwFilter] BrowserView found`, `Filter script injected`
6. Verify: images get light pre-blur briefly, then resolve (blur stays or clears)
