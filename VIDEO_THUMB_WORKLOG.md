# Video Thumbnail Extraction — Work Log (2026-02-18)

## Feature Goal

Extract first-frame thumbnails from direct video URLs (.mp4, .webm, .mov) using DotNetBrowser's Chromium engine. Videos currently show a dark placeholder rectangle because SkiaSharp can't decode video codecs. Chromium CAN decode via HTML5 `<video>` + `<canvas>`.

## Files Modified

1. **`hook/DotNetBrowserReflection.cs`**
2. **`hook/StartupHook.cs`**
3. **`hook/LinkEmbedEngine.cs`**

## What Was Implemented

### DotNetBrowserReflection.cs

- Added 4 shared static fields for cross-component access:
  ```csharp
  internal static DotNetBrowserReflection? SharedInstance { get; set; }
  internal static object? SharedBrowser { get; set; }
  internal static object? SharedFrame { get; set; }
  internal static volatile bool IsReady;
  ```
- Added `_browserTitle` PropertyInfo cache (resolved in `ResolveMembers()`)
- Added `GetBrowserTitle(object browser)` method — reads `IBrowser.Title` for JS→C# data channel

### StartupHook.cs — Phase 5 Restructured

- **Phase 5 now always runs** (was gated behind `wantNsfw` / NSFW filter enabled)
- After DotNetBrowser type resolution, discovers IBrowser and stores in shared statics
- NSFW filter init remains conditional inside the same thread
- Discovery retries up to 6 times with 5s delays (30s window)

### LinkEmbedEngine.cs

- **New fields**: `s_videoThumbSemaphore` (serializes title-channel), `s_videoThumbFailed` (caches failures), timeout/prefix constants
- **`ExtractVideoThumbnail(string videoUrl)`** → `byte[]?`:
  - Waits for `DotNetBrowserReflection.IsReady` (up to 30s poll)
  - Acquires semaphore (document.title is single global channel)
  - Executes JS: hidden `<video>` → `loadeddata` → `<canvas>` → `toDataURL('image/jpeg', 0.7)` → writes to `document.title`
  - Polls `IBrowser.Title` every 200ms for up to 12s
  - On prefix match: extracts base64, decodes, returns JPEG bytes
  - Always restores original title + releases semaphore in `finally`
- **`ReplaceVideoPlaceholderWithThumbnail(data, bytes)`**: Finds card by tag, removes last child (dark placeholder), creates bitmap, adds `BuildThumbnailWithPlayButton` overlay
- **Phase B for video URLs** in `FetchAndInject`: After existing image Phase B, queues background `ExtractVideoThumbnail` for video URLs (`VideoId != null && Image == null`)
- **Play button color**: Changed `BuildVideoPlaceholder` from `#DDCC0000` (bright red) to `#CC000000` (semi-transparent black) matching YouTube thumbnail style

## Bugs Found & Fixed During Testing

### Bug 1: Assembly detection too narrow

`AreDotNetBrowserAssembliesLoaded()` checked 3 exact names: `"DotNetBrowser"`, `"DotNetBrowser.Core"`, `"DotNetBrowser.Chromium"`. Root's actual assemblies include `"DotNetBrowser.Logging"` etc. The event handler in StartupHook had the same problem.

**Fix**: Changed both to `StartsWith("DotNetBrowser")` — matches how `ResolveTypes()` already searches.

### Bug 2: FindBrowserDirect fails on background thread

`FindBrowserViaViewModel()` accesses `ApplicationLifetime`, `MainWindow`, and `DataContext` via reflection. These are Avalonia properties that require UI thread access. Calling from a ThreadPool thread caused `TargetInvocationException: Exception has been thrown by the target of an invocation.` on every attempt.

**Fix**: Dispatch `FindBrowserDirect()` to UI thread via `RunOnUIThread()` + `ManualResetEventSlim` for synchronization:
```csharp
capturedResolver.RunOnUIThread(() => {
    try { result = capturedBrowserReflection.FindBrowserDirect(); }
    catch (Exception ex) { Logger.Log(...); }
    found.Set();
});
found.Wait(TimeSpan.FromSeconds(15));
```

### Current Status: UNTESTED after Bug 2 fix

The UI-thread dispatch fix was the last change. Need to rebuild, deploy, and verify:
1. Phase 5 logs show `FindBrowserDirect` succeeding (IBrowser found)
2. `DotNetBrowserReflection.IsReady` becomes true
3. Video thumb extraction JS executes
4. Thumbnails appear on video embeds

## Key Architecture Notes

- **Chat is 100% Avalonia-native** (1647+ visual tree nodes, 0 browser controls) — DotNetBrowser is auxiliary only (WebRTC, OAuth, sub-apps)
- DotNetBrowser shell page: `rootapp://app/__index.html` with `<iframe id="app-iframe">` permanently at `about:blank`
- IBrowser.Title = "Root App Container" — we use it as a data channel (write from JS, read from C#)
- `DotNetBrowser.AvaloniaUi` is NOT loaded — Root accesses browser programmatically
- Assemblies: DotNetBrowser v3.4.0.6253, DotNetBrowser.Core v3.4.0.6253, DotNetBrowser.Logging v3.4.0.6253
- IBrowser discovery chain (from SESSION_STATE.md): MainWindow.DataContext → _memberProfileViewModelFactory → directMessageOpenerService → browserService → _engineManager → .Engine (IEngine) → Profiles[0].Browsers.__values
- `Repository<BrowserId, IBrowserImpl>` doesn't implement IEnumerable — must access private `_values` field
- ExecuteJavaScript: `IFrame.ExecuteJavaScript(String, Boolean)` — 2 params, bool defaults to false
- Chromium flags: `--incognito`, `--disable-web-security` (the `--disable-web-security` is why cross-origin video frames work)

## Flow

```
Video URL detected → SynthesizeVideoEmbed (VideoId="direct", Image=null)
  → Phase A: dark placeholder shown immediately (semi-transparent black play button)
  → Phase B (background thread):
      → ExtractVideoThumbnail: JS injects <video>→<canvas>→base64 via document.title
      → Poll IBrowser.Title for result (up to 12s)
      → On success: decode base64 → bytes → ReplaceVideoPlaceholderWithThumbnail
      → On failure: keep dark placeholder, cache failure
  → Cache re-injection: _imageBytesCache feeds InjectEmbedWithCachedImage → BuildEmbedCard
    which handles VideoId != null + bitmap → BuildThumbnailWithPlayButton
```

## What To Check If It Still Doesn't Work

1. Check Phase 5 logs for `FindBrowserDirect` result — does it find IBrowser on UI thread?
2. If IBrowser found but no thumbs: check for `Video thumb:` log entries — is JS executing?
3. If JS executes but title never changes: the shell page might block cross-origin video loading (check `--disable-web-security` is still active)
4. If title changes to error: the video URL might not be directly loadable by Chromium (CORS, auth, etc.)
5. `EscapeJsString` in the JS template — make sure video URLs with special chars don't break the JS
