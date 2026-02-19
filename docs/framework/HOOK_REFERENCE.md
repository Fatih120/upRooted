# Hook Reference

> **What this is:** Implementation-level reference for all 24 C# hook classes — startup phases, sidebar injection, content pages, theme engine, settings, and every feature engine.
> **Read when:** Modifying or extending any C# hook feature; understanding startup sequence detail; debugging hook behavior.
> **Skip if:** You need the architecture overview or critical rules → [ARCHITECTURE.md](ARCHITECTURE.md). You need Avalonia reflection patterns → [AVALONIA_PATTERNS.md](AVALONIA_PATTERNS.md).
> **Does NOT cover:** Architecture overview or critical rules → [ARCHITECTURE.md](ARCHITECTURE.md) | Avalonia reflection specifics → [AVALONIA_PATTERNS.md](AVALONIA_PATTERNS.md) | TypeScript layer → [TYPESCRIPT_REFERENCE.md](TYPESCRIPT_REFERENCE.md)

> **Related docs:**
> [Architecture](ARCHITECTURE.md) |
> [Theme Engine Deep Dive](THEME_ENGINE_DEEP_DIVE.md) |
> [Avalonia Patterns](AVALONIA_PATTERNS.md) |
> [TypeScript Reference](TYPESCRIPT_REFERENCE.md) |
> [CLR Profiler](CLR_PROFILER.md) |
> [Build](../install/BUILD.md) |
> [How It Works](../HOW_IT_WORKS.md)

---

## Table of Contents

1. [Overview](#overview)
2. [Entry Points](#entry-points)
3. [Startup Sequence](#startup-sequence)
4. [LinkEmbedEngine: Trimming Quirks and Provider Behavior](#linkembedengine-trimming-quirks-and-provider-behavior)
5. [ClearUrlsEngine: Compose Input Interception](#clearurlsengine-compose-input-interception)
6. [AutoUpdater: Update Channels and Package System](#autoupdater-update-channels-and-package-system)
7. [AvaloniaReflection Deep Dive](#avaloniareflection-deep-dive)
   - [Advanced AvaloniaReflection Patterns](#advanced-avaloniareflection-patterns)
8. [Visual Tree Discovery](#visual-tree-discovery)
9. [Sidebar Injection](#sidebar-injection)
10. [Content Pages](#content-pages)
11. [Theme Engine](#theme-engine)
12. [Color Utilities](#color-utilities)
13. [HTML Patch Verifier](#html-patch-verifier)
14. [Settings](#settings)
15. [Infrastructure](#infrastructure)
16. [Dependency Map](#dependency-map)

---

## Overview

The hook layer is the C# .NET component of Uprooted. It is a managed DLL injected
into Root Communications' desktop application (`Root.exe`) via a CLR profiler. Root is
built on .NET 10 with Avalonia UI, so Uprooted cannot reference Avalonia assemblies at
compile time -- the target app ships as a trimmed single-file binary. Instead, the hook
discovers every Avalonia type, property, and method through runtime reflection, then
constructs and manipulates the native Avalonia visual tree to add settings pages,
sidebar navigation, theme overrides, and more.

The hook layer consists of 24 source files in the `hook/` directory:

| File | Lines | Purpose |
|------|------:|---------|
| `Entry.cs` | 33 | `[ModuleInitializer]` profiler injection entry point |
| `NativeEntry.cs` | 66 | Native `hostfxr` entry point for DLL proxy injection |
| `StartupHook.cs` | 518 | Multi-phase startup orchestrator (Phase 0-5), version migration |
| `AvaloniaReflection.cs` | 2030 | Reflection cache for ~50 Avalonia types, ~55 members |
| `VisualTreeWalker.cs` | 554 | DFS visual tree traversal, settings layout discovery |
| `SidebarInjector.cs` | 1408 | Event + timer-based sidebar injection and content management |
| `ContentPages.cs` | ~3400 | Page builders for Uprooted/Plugins/Themes settings |
| `ThemeEngine.cs` | ~2510 | Runtime theme engine with resource + visual tree color override + custom ping color |
| `ColorUtils.cs` | 262 | HSL/HSV/RGB conversion and manipulation |
| `ColorPickerPopup.cs` | 533 | HSV color picker overlay (Discord-style) |
| `HtmlPatchVerifier.cs` | 429 | Self-healing HTML patch system with FileSystemWatcher |
| `DotNetBrowserReflection.cs` | 1914 | Reflection cache for DotNetBrowser types, IBrowser discovery |
| `BrowserDiscovery.cs` | 496 | Phase 4.5 diagnostic scanner (visual tree + assembly dump) |
| `ClearUrlsEngine.cs` | 467 | Strips tracking params (utm_*, fbclid, gclid, etc.) from URLs in compose editor on Enter. Hooks AvaloniaEdit TextArea via routed events with `handledEventsToo: true`. |
| `LinkEmbedEngine.cs` | 1763 | Avalonia-native link embed engine (OG/oEmbed fetch, visual tree injection) |
| `AnimatedImage.cs` | 795 | Animated GIF/WebP decoder and timer-based playback via SkiaSharp SKCodec reflection. Frame extraction, disposal method handling, per-frame delay timers |
| `MessageLogger.cs` | 1876 | Message logger plugin: per-item async deletion pollers (`HasBeenDeleted` probe, 300ms/3s, epoch-based channel switch cancellation), event-driven edit detection (`HandleReplaced`, `_addedViaEvent` + 5s grace period), Discord-style red deleted-message cards + amber edit indicator cards, tag-based dedup, insertion-order tracking |
| `MessageStore.cs` | 232 | Flat-file persistence for message log data. Pipe-delimited format with URI-encoded fields, append-only writes via buffered flush timer, startup truncation for retention limits |
| `AutoUpdater.cs` | ~810 | In-process auto-updater: checks GitHub releases API (stable/dev channel), downloads encrypted `.uprpkg`, multi-layer XOR decryption, staging + verify + overwrite in-place. HTTP via reflection. |
| `ProfileBadgeInjector.cs` | ~450 | Injects "Uprooted Dev" badge below username in profile popups. 500ms timer polls TopLevel windows + OverlayLayer. Heuristic popup detection, username found by largest font size, vertical panel walk-up for correct insertion point. Dev channel only. |
| `SilentTypingEngine.cs` | 335 | Blocks `SetTypingIndicator` gRPC calls at the .NET HttpClient layer. Scans assemblies + walks ViewModel chain to find Root's `HttpClient` instances; prepends `TypingBlockerHandler` (DelegatingHandler) that short-circuits the request with a synthetic `200 OK`. Phase 4.5f, 12s startup delay. |
| `NsfwFilter.cs` | 473 | NSFW content filter (Phase 4.5g, Avalonia-native visual tree scan) |
| `UprootedSettings.cs` | 161 | INI-based settings persistence |
| `Logger.cs` | 46 | Thread-safe file logging |
| `PlatformPaths.cs` | 29 | Cross-platform path resolution |

---

## Entry Points

Two entry paths exist for getting Uprooted's code running inside Root's process.

### Entry.cs -- Profiler-Based IL Injection

**File:** `hook/Entry.cs` (33 lines)

The CLR profiler modifies IL bytecode at load time to call `Assembly.LoadFrom` followed
by `Assembly.CreateInstance("UprootedHook.Entry")`. This triggers either the
`[ModuleInitializer]` static method or the constructor.

```csharp
public class Entry
{
    private static int _initialized = 0;

    [ModuleInitializer]
    internal static void ModuleInit()
    {
        if (Interlocked.CompareExchange(ref _initialized, 1, 0) == 0)
        {
            Logger.Log("Entry", "=== ModuleInitializer triggered ===");
            StartupHook.Initialize();
        }
    }

    public Entry()
    {
        if (Interlocked.CompareExchange(ref _initialized, 1, 0) == 0)
        {
            Logger.Log("Entry", "=== Constructor triggered ===");
            StartupHook.Initialize();
        }
    }
}
```

**Key details:**

- **Double-init guard** (`Entry.cs:13,18,27`): An `Interlocked.CompareExchange` CAS
  on the static `_initialized` field guarantees `StartupHook.Initialize()` is called
  exactly once, even if both the `[ModuleInitializer]` and the constructor fire.
- The `[ModuleInitializer]` attribute (C# 9) causes `ModuleInit()` to run when the
  assembly's module is first accessed -- before any constructor.
- Both paths funnel into `StartupHook.Initialize()`.

### NativeEntry.cs -- hostfxr DLL Proxy

**File:** `hook/NativeEntry.cs` (66 lines)

An alternative entry when loaded via `hostfxr_load_assembly_and_get_function_pointer`.
The native DLL proxy calls into this static method.

```csharp
public static int Initialize(IntPtr args, int sizeBytes)
```

**Behavior** (`NativeEntry.cs:14-65`):

1. Logs all loaded assemblies to diagnose whether we are in Root's CLR or a separate
   runtime (`NativeEntry.cs:28-34`).
2. Searches for any assembly starting with `"Avalonia"` to confirm we share Root's
   AppDomain (`NativeEntry.cs:37-52`).
3. Calls `StartupHook.Initialize()` (`NativeEntry.cs:55`).
4. Returns `0` on success, `1` on fatal error.

### Entry Flow Summary

```
CLR Profiler injects IL -> Assembly.LoadFrom -> Entry.ModuleInit()
                                             -> Entry.ctor()
           OR
Native DLL proxy -> hostfxr -> NativeEntry.Initialize()
           |
           v
     StartupHook.Initialize()  (guarded: only Root.exe process)
           |
           v
     new Thread("Uprooted-Injector") -> InjectorLoop()
```

---

## Startup Sequence

**File:** `hook/StartupHook.cs` (518 lines)

The `StartupHook` class is the internal, no-namespace class required by .NET's
`DOTNET_STARTUP_HOOKS` mechanism. It contains the multi-phase initialization sequence
(Phase 0 through Phase 5), version migration logic, and the `CurrentVersion` constant.

### Process Guard

```csharp
var processName = Path.GetFileNameWithoutExtension(Environment.ProcessPath ?? "");
if (!processName.Equals("Root", StringComparison.OrdinalIgnoreCase))
    return;
```

(`StartupHook.cs:16-18`) Only Root.exe is targeted. Any other .NET process in the
system (e.g., `dotnet.exe`, MSBuild) exits immediately.

### Thread Creation

A background thread named `"Uprooted-Injector"` is started (`StartupHook.cs:20-25`).
All subsequent work happens off the main thread to avoid blocking Root's startup.

### Phase 0: HTML Patch Verification

**Time:** Immediate (no Avalonia needed)
**Timeout:** None
**Failure mode:** Non-fatal; logged and skipped

```csharp
var verifier = new HtmlPatchVerifier();
var repaired = verifier.VerifyAndRepair();
verifier.StartWatching();
s_patchVerifier = verifier;  // prevent GC
```

(`StartupHook.cs:40-52`)

- Creates an `HtmlPatchVerifier` to scan HTML files in Root's profile directory.
- Calls `VerifyAndRepair()` to patch any unpatched `index.html` files.
- Calls `StartWatching()` to install `FileSystemWatcher` instances for auto-repair.
- The verifier is stored in the static field `s_patchVerifier` to prevent garbage
  collection of the `FileSystemWatcher` instances for the process lifetime.

### Version Migration (between Phase 0 and Phase 1)

**Failure mode:** Non-fatal; settings update only

```csharp
var migrationSettings = UprootedSettings.Load();
var cmp = AutoUpdater.CompareVersions(CurrentVersion, migrationSettings.Version);
if (cmp > 0)
{
    // Upgrade: apply cumulative force-disable entries, stamp new version
    foreach (var (version, disableList) in ForceDisableOnUpgrade)
        if (AutoUpdater.CompareVersions(version, oldVersion) > 0 &&
            AutoUpdater.CompareVersions(version, CurrentVersion) <= 0)
            foreach (var pluginId in disableList)
                migrationSettings.Plugins[pluginId] = false;
    migrationSettings.Version = CurrentVersion;
    migrationSettings.Save();
}
else if (cmp < 0)
{
    // Downgrade: stamp version only, no plugin changes
    migrationSettings.Version = CurrentVersion;
    migrationSettings.Save();
}
```

(`StartupHook.cs:55-87`)

Runs once per version transition, before any plugin launch decisions are made. The
`ForceDisableOnUpgrade` dictionary is a compile-time table mapping version strings to
arrays of plugin IDs that should be force-disabled when upgrading to (or through) that
version. Semantics:

- **Upgrade only:** Fires when `CurrentVersion > settings.Version`. Uses
  `AutoUpdater.CompareVersions()` which handles `major.minor.patch-suffix` with correct
  prerelease ordering (`0.3.6-rc < 0.3.6`).
- **Cumulative:** If a user skips from 0.3.6 to 0.5.0, entries for 0.4.0 *and* 0.5.0
  both apply.
- **Explicit false insert:** Sets `Plugins[id] = false` even if the key doesn't exist,
  because missing keys fall back to `DefaultEnabled` which may be `true`.
- **Downgrade-safe:** On downgrade, stamps the version but applies no disable policies.
- **No-op on same version:** Skips entirely when versions match (normal restart).

To add entries, edit the `ForceDisableOnUpgrade` dictionary at the top of `StartupHook`:

```csharp
private static readonly Dictionary<string, string[]> ForceDisableOnUpgrade = new()
{
    { "0.4.0", new[] { "message-logger", "silent-typing" } },
};
```

### Phase 1: Wait for Avalonia Assemblies

**Timeout:** 30 seconds (250ms poll)
**Failure mode:** Fatal; returns and aborts injection

```csharp
if (!WaitForAvaloniaAssemblies(TimeSpan.FromSeconds(30)))
{
    Logger.Log("Startup", "Phase 1 FAILED: Avalonia assemblies not found after 30s");
    return;
}
```

(`StartupHook.cs:89-95`)

The helper `WaitForAvaloniaAssemblies` (`StartupHook.cs:149-163`) polls
`AppDomain.CurrentDomain.GetAssemblies()` every 250ms looking for an assembly named
`"Avalonia.Controls"`. Once found, Phase 1 succeeds.

After assemblies are detected, `AvaloniaReflection.Resolve()` is called
(`StartupHook.cs:64-69`) to scan and cache all Avalonia types and members. If
resolution fails, startup aborts.

### Phase 2: Wait for Application.Current

**Timeout:** 30 seconds (500ms poll)
**Failure mode:** Fatal; returns and aborts injection

```csharp
if (!WaitFor(() => resolver.GetAppCurrent() != null, TimeSpan.FromSeconds(30)))
```

(`StartupHook.cs:72-78`)

Uses the generic `WaitFor` helper (`StartupHook.cs:165-178`) which polls a condition
every 500ms. Waits for `Application.Current` to be non-null, indicating Avalonia's
application object is initialized.

### Phase 3: Wait for MainWindow

**Timeout:** 60 seconds (500ms poll)
**Failure mode:** Fatal; returns and aborts injection

```csharp
if (!WaitFor(() =>
{
    mainWindow = resolver.GetMainWindow();
    return mainWindow != null;
}, TimeSpan.FromSeconds(60)))
```

(`StartupHook.cs:81-92`)

The longer timeout (60s vs 30s) accounts for Root's splash screen and login flow.

### Phase 3.5: Initialize Theme Engine

**Failure mode:** Non-fatal; logged and skipped

```csharp
var themeEngine = new ThemeEngine(resolver);
var savedSettings = UprootedSettings.Load();
resolver.RunOnUIThread(() => {
    if (savedSettings.ActiveTheme == "custom")
        themeEngine.ApplyCustomTheme(savedSettings.CustomAccent, savedSettings.CustomBackground);
    else if (savedSettings.ActiveTheme != "default-dark")
        themeEngine.ApplyTheme(savedSettings.ActiveTheme);

    // Apply saved custom ping color override (persists across theme switches)
    if (!string.IsNullOrEmpty(savedSettings.CustomPingColor) && ColorUtils.IsValidHex(savedSettings.CustomPingColor))
        themeEngine.SetCustomPingColor(savedSettings.CustomPingColor);
});
```

(`StartupHook.cs:95-140`)

Creates a `ThemeEngine` instance and loads saved settings. The actual theme application
runs on the UI thread via `resolver.RunOnUIThread()` because Avalonia's
`ResourceDictionary` requires Dispatcher access. After the theme is applied, any saved
custom ping color override is applied on top via `SetCustomPingColor()`.

### Phase 4: Settings Page Monitor

```csharp
var injector = new SidebarInjector(resolver, mainWindow!, themeEngine);
injector.StartMonitoring();
```

(`StartupHook.cs:135-137`)

Creates the `SidebarInjector` and starts monitoring. Primary detection uses the
`LayoutUpdated` event on the main window for same-frame injection (zero dispatch
latency). A 200ms safety-net timer handles alive checks and edge cases.

### Phase 4.5: Browser Discovery (Diagnostic)

**Time:** 10 seconds after Phase 4
**Failure mode:** Non-fatal; purely informational

**File:** `hook/BrowserDiscovery.cs` (496 lines)

A diagnostic phase that performs a full DFS visual tree dump of the main window and
scans all loaded assemblies. Results are logged for architecture discovery and debugging.
This phase confirmed that the Avalonia visual tree contains 1647+ nodes and 0 browser
controls — leading to the discovery that chat is Avalonia-native.

### Phase 4.5a: ClearURLs

**Time:** 14 seconds after Phase 4
**Failure mode:** Non-fatal; URLs are simply not cleaned

**File:** `hook/ClearUrlsEngine.cs` (467 lines)

Strips tracking parameters (utm_source, fbclid, gclid, si, etc.) from URLs in the
compose editor when the user presses Enter to send. Uses timer-based discovery (2s
interval) to find AvaloniaEdit TextArea controls in the visual tree, then attaches
KeyDown handlers via Avalonia's `AddHandler` with `handledEventsToo=true`.

See [ClearUrlsEngine: Compose Input Interception](#clearurlsengine-compose-input-interception) for the full technical reference.

### Phase 4.5b: Link Embeds

**Time:** 15 seconds after Phase 4
**Failure mode:** Non-fatal; links simply don't get embeds

**File:** `hook/LinkEmbedEngine.cs`

Starts the Avalonia-native link embed engine. Scans visual tree for CTextBlock nodes
containing URLs, fetches OG/oEmbed metadata via reflection-based HttpClient, and injects
native Avalonia embed cards. Supports YouTube (dedicated oEmbed), Twitter/X and
embed-fixer domains (bot UA + oEmbed discovery), direct image URLs (zero-network fast
path), animated GIF/WebP playback (via `AnimatedImage.cs` + SkiaSharp reflection), and
generic sites via OpenGraph tags with Content-Type gating. Direct Tenor CDN URLs
(`media.tenor.com`) are skipped (Root renders them natively); `tenor.com/view/` pages
go through the OG pipeline to extract and render the animated GIF inline.

Sets `LinkEmbedEngine.Instance` static property for external access from ContentPages
(settings lightbox "Show file names" toggle).

### Phase 4.5c: Message Logger

**Time:** 20 seconds after Phase 4 (allows chat to populate)
**Failure mode:** Non-fatal; messages simply aren't logged

**File:** `hook/MessageLogger.cs` (1876 lines), `hook/MessageStore.cs` (232 lines)

Starts the message logger plugin (WIP). Finds `RootMessageItemsControl` in the visual
tree, resolves ViewModel property accessors (MessageId, Content, AuthorId, Timestamp)
via per-type cache, and subscribes to `ObservableCollection.CollectionChanged` via
`Expression.Lambda`. Handles Add, Remove, Replace, and Reset events with epoch-based
channel switch cancellation and async deletion pollers (HasBeenDeleted probe every
300ms for 3s).

### Phase 4.5d: Auto-Updater

**Time:** After Phase 4.5c
**Failure mode:** Non-fatal; updates simply don't happen

**File:** `hook/AutoUpdater.cs` (~910 lines)

Initializes the in-process auto-updater. Creates an `AutoUpdater` instance, sets
`AutoUpdater.Instance` for UI access from ContentPages, and subscribes to
`AutoUpdater.BackgroundUpdateApplied` to show a dismissable notification overlay when a
background update is applied.

See [AutoUpdater: Update Channels and Package System](#autoupdater-update-channels-and-package-system) for the full technical reference.

### Phase 4.5e: Profile Badge Injector

**Time:** 5 seconds after Phase 4 (developer channel only)
**Failure mode:** Non-fatal; no badge appears

**File:** `hook/ProfileBadgeInjector.cs` (~535 lines)

Injects "Uprooted Dev" badge below username in profile popups. Only runs on the
developer update channel. 500ms timer polls TopLevel windows + OverlayLayer
`CollectionChanged` events. Heuristic popup detection (avatar + username + roles),
username TextBlock found by largest font size, badge gated to hardcoded developer
usernames.

### Phase 4.5f: Silent Typing

**Time:** 12 seconds after Phase 4
**Failure mode:** Non-fatal; typing indicators are sent normally

**File:** `hook/SilentTypingEngine.cs` (335 lines)

Blocks `SetTypingIndicator` gRPC calls at the .NET HttpClient layer. Scans assemblies
and walks the ViewModel chain to find Root's `HttpClient` instances, then prepends a
`TypingBlockerHandler` (DelegatingHandler) that short-circuits matching requests with a
synthetic `200 OK` response. Timer-based discovery with 5s retry interval, 30s interval
once patched.

### Phase 4.5g: NSFW Filter

**Time:** 20 seconds after Phase 4
**Failure mode:** Non-fatal; images are not filtered

**File:** `hook/NsfwFilter.cs` (473 lines)

Avalonia-native NSFW content filter. 500ms scan timer posts `ScanForImages()` to the UI
thread. DFS walks the visual tree finding `Image` controls (skips tagged/tiny/seen),
queues classification on thread pool with `SemaphoreSlim(3)` concurrency cap. NSFW
images are hidden and an overlay injected (Border > StackPanel > warning text +
click-to-reveal). `RevealAllBlocked()` restores all on disable.

### Phase 5: DotNetBrowser Feature Loading

**Timeout:** 90 seconds (event-driven, not polling)
**Failure mode:** Non-fatal; features degrade gracefully

**File:** `hook/StartupHook.cs`, `hook/DotNetBrowserReflection.cs`

Uses `AppDomain.CurrentDomain.AssemblyLoad` event + `ManualResetEventSlim` to detect
when DotNetBrowser assemblies load. Assembly gate requires `DotNetBrowser` or
`DotNetBrowser.Core` (NOT `DotNetBrowser.AvaloniaUi`, which Root does not ship).

On detection:
1. `DotNetBrowserReflection` resolves all DotNetBrowser types (IBrowser, IFrame, IEngine,
   etc.) via reflection.
2. Discovers `IBrowser` via ViewModel chain walking: `MainWindow.DataContext` →
   `BrowserService` → `BrowserEngineManager` → `IEngine` → `Profiles[0].Browsers._values`
   (ConcurrentDictionary).

DotNetBrowser is auxiliary (WebRTC, OAuth, sub-apps) — chat is Avalonia-native.

---

## LinkEmbedEngine: Trimming Quirks and Provider Behavior

**File:** `hook/LinkEmbedEngine.cs`

Root's single-file trimming removes methods unpredictably. The link embed engine hits trimming issues that other hook code does not, because it performs HTTP requests and JSON-like parsing against external servers. This section documents every workaround and the rationale behind each.

### HTTP Method Survival

Methods resolved via reflection in `EnsureHttpResolved()`. Trimming status observed at runtime:

| Method | Status | Notes |
|--------|--------|-------|
| `GetStringAsync(string)` | Survives | Works for HTML pages. Fails on some JSON responses (YouTube oEmbed) — likely trimmed charset/decompression in response pipeline |
| `GetByteArrayAsync(string)` | Trimmed | Never available. `HttpGetBytes` falls back to `GetAsync` + stream |
| `GetAsync(string)` | Survives | Works for binary downloads. Response body read via `ReadAsStreamAsync` |
| `SendAsync(HttpRequestMessage, CancellationToken)` | Survives | Most reliable. Supports per-request UA override via `HttpRequestMessage.Headers` |
| `ReadAsStringAsync()` | Survives (partial) | Works for HTML responses. Throws `MissingMethodException` on some JSON responses — trimmed charset/encoding detection |
| `ReadAsStreamAsync()` | Survives | Always works. Use `StreamReader.ReadToEnd()` to convert to string |

**Rule:** Always prefer `SendAsync` + `ReadAsStreamAsync` + `StreamReader` for string responses. `ReadAsStringAsync` is unreliable.

### JSON Parsing Without System.Text.Json

`System.Text.Json` is forbidden in the hook (Critical Rule #3 — causes `MissingMethodException`). All JSON field extraction uses compiled `Regex` patterns:

```csharp
// Pattern: "field_name" : "value" (handles escaped quotes)
private static readonly Regex JsonTitleRegex = new(
    @"""title""\s*:\s*""([^""\\]*(?:\\.[^""\\]*)*)""", RegexOptions.Compiled);
```

**Trimming trap:** `Regex.Replace(string, string, MatchEvaluator)` with a lambda delegate is trimmed. The `MatchEvaluator` delegate type or its invocation path references a trimmed method. Use manual string loops instead:

```csharp
// BAD — trimmed in Root's binary:
Regex.Replace(s, @"\\u([0-9a-fA-F]{4})", m => ((char)Convert.ToInt32(...)).ToString());

// GOOD — manual loop:
while ((idx = s.IndexOf("\\u", idx)) >= 0 && idx + 5 < s.Length)
{
    var hex = s.Substring(idx + 2, 4);
    if (int.TryParse(hex, NumberStyles.HexNumber, null, out int code))
        s = string.Concat(s.AsSpan(0, idx), ((char)code).ToString(), s.AsSpan(idx + 6));
}
```

**Escape ordering:** Process `\\` → `\` replacement LAST. If done first, `\\u0041` becomes `\u0041` and gets decoded to `A`. The correct sequence: unescape `\"`, `\/` first, decode `\uXXXX`, then `\\` last.

### Embed-Fixer Domain Behavior

These third-party services rewrite Twitter/X URLs for better embeds. Each behaves differently:

| Domain | OG Tags | oEmbed | Image Source | Bot UA Required |
|--------|---------|--------|-------------|-----------------|
| `vxtwitter.com` | `twitter:title`, `twitter:image` | type="photo", empty title, no `url` field | OG `twitter:image` | Yes (Discordbot) |
| `fixupx.com` | Title only, no image tag | type="rich", title="Embed", engagement stats in `author_name` | None — must normalize to vxtwitter | Yes (Discordbot) |
| `fxtwitter.com` | Similar to fixupx | Similar to fixupx | Normalize to vxtwitter | Yes (Discordbot) |
| `fixvx.com` | Similar to fixupx | Similar to fixupx | Normalize to vxtwitter | Yes (Discordbot) |
| `x.com` / `twitter.com` | Full OG tags with bot UA | N/A (no oEmbed discovery link) | OG `og:image` with bot UA | Yes (Discordbot) |

**Key insight:** Chrome-like UA gets redirect stubs or React SPA shells from all these domains. Only bot/crawler UAs (e.g., `Discordbot/2.0`) receive OG metadata.

**Normalization:** All non-vxtwitter embed-fixer domains are rewritten to `vxtwitter.com` before metadata fetch, because vxtwitter consistently provides `twitter:image` in its OG tags. The embed card still links to the original URL.

### oEmbed Response Formats

oEmbed spec defines four types. Image location varies by type:

| Type | Image Field | Notes |
|------|------------|-------|
| `photo` | `url` (required) | The photo itself. `thumbnail_url` is optional preview |
| `video` | `thumbnail_url` | Preview image. `html` contains embed iframe |
| `rich` | `thumbnail_url` | Preview image. `html` contains rich content |
| `link` | `thumbnail_url` | Optional preview |

**YouTube oEmbed:** Returns `title`, `author_name`, `thumbnail_url`. Fetched via `GetStringAsync` which fails (trimmed response pipeline). Falls back to page scraping — fetch watch page HTML, extract `<title>` tag, strip " - YouTube" suffix.

**Twitter oEmbed (via embed-fixers):** Returns `author_name` and `html` (blockquote with tweet text in `<p>` tags) but often no `title` or `description`. The engine extracts text from `<p>` tags in the `html` field as description fallback.

### Metadata Routing

`FetchMetadata()` routes URLs through a priority chain:

```
0. Native embed domain?    → skip (media.tenor.com, rootapp.gg — Root handles natively)
1. Cache hit?              → return cached (animated images re-inject from byte cache)
2. Image URL regex?        → SynthesizeImageEmbed (zero network)
3. YouTube regex?          → FetchYouTubeMetadata (oEmbed + page scrape fallback)
4. All other URLs          → FetchOgMetadata:
   a. Normalize embed-fixer domains → vxtwitter.com
   b. Detect Twitter/embed-fixer → set bot UA
   c. HttpGetWithContentType (SendAsync + ReadAsStreamAsync)
   d. Content-Type gate (HTML → proceed, image/* → synthesize, other → bail)
   e. oEmbed discovery (<link> tags in HTML) → TryOEmbedDiscovery
   f. OG tag parsing (og:*, twitter:* fallbacks)

Image rendering (all paths):
   → Fetch image bytes → cache in _imageBytesCache
   → AnimatedImage.IsAnimated(bytes)?
     → Yes: DecodeFrames → CreateAnimatedControl (timer-based playback)
     → No:  Bitmap(Stream) static image (existing behavior)
```

### Animated Image Playback

**File:** `hook/AnimatedImage.cs`

Animated `.gif` and `.webp` URLs play inline using SkiaSharp's `SKCodec`, resolved via reflection from assemblies already loaded in Root's process (Avalonia ships SkiaSharp as its rendering backend).

**Detection:** `MightBeAnimated()` checks magic bytes (GIF87a/GIF89a for GIF, RIFF+WEBP header for WebP). For WebP, detection delegates to `SKCodec.FrameCount` rather than parsing VP8X flags (fragile across WebP variants). `IsAnimated()` confirms multi-frame via SKCodec.

**Frame extraction:** For each frame (capped at 100): `SKCodec.GetPixels()` with `SKCodecOptions { FrameIndex = i }` → `SKImage.FromBitmap()` → `SKImage.Encode(Png)` → `SKData.ToArray()` → Avalonia `Bitmap(Stream)`. PNG round-trip avoids needing WriteableBitmap/PixelFormat reflection.

**Playback:** Per-embed `System.Threading.Timer` cycles frames on the UI thread via `RunOnUIThread()`. Per-frame delays from GIF/WebP metadata (minimum 20ms floor). Dispose action stops timer on card removal.

**SkiaSharp API quirks:**
- `SKCodec.FrameCount` property absent in Root's SkiaSharp version — use `FrameInfo[].Length` as fallback
- `SKCodec.GetPixels` is 4-param `(SKImageInfo, IntPtr, int rowBytes, SKCodecOptions)`, not the 3-param version documented in most examples
- `SKCodec.Create(SKData)` preferred over `Create(SKStream)` — avoids stream lifecycle issues
- `SKData.CreateCopy(byte[])` used to create independent data copies for codec input

**Native embed domain skip:** Only direct Tenor CDN URLs (`media.tenor.com`) are skipped in `FetchAndInject()` because Root renders them natively. `tenor.com/view/` pages go through the generic OG pipeline — their `og:image` points to a `media.tenor.com` GIF URL, which our animated image support renders inline.

**Graceful fallback:** If any SkiaSharp type or method is missing or trimmed, all `AnimatedImage` methods return null/false. The caller falls back to static first-frame rendering via `Bitmap(Stream)` — existing behavior, zero regression.

### Verbose Logging

Set `UPROOTED_VERBOSE=1` environment variable to enable step-by-step HTTP diagnostic logs (`HGWCT[1]` through `HGWCT[7]`), oEmbed JSON body dumps, and parsed field summaries. Error logs are always enabled regardless of this flag.

---

## ClearUrlsEngine: Compose Input Interception

**File:** `hook/ClearUrlsEngine.cs` (467 lines)

Strips tracking parameters from URLs in the compose editor when the user presses Enter.
This is the first plugin to intercept outgoing messages, and the patterns here are
reusable for any feature that needs to modify text before it is sent.

### Root's Compose Input

Root's message compose area is **AvaloniaEdit.TextEditor** — a third-party code editor
library, NOT `Avalonia.Controls.TextBox`. The visual tree path is:

```
RootMessageTextboxView > TextEditor > TextArea > TextView > TextLayer
```

Key characteristics:
- `AvaloniaReflection.TextBoxType` (`Avalonia.Controls.TextBox`) does NOT match — there
  are 0 TextBox controls in the chat visual tree
- `TextEditor.Text` CLR property works for reading and writing (AvaloniaEdit is
  third-party, not subject to Root's single-file trimming)
- Multiple TextEditor instances exist (one per open channel/DM); timer-based discovery
  finds new ones as channels are opened

### Enter Key Interception

AvaloniaEdit marks Enter (Key.Return) as `Handled=true` internally. This means:

1. **CLR event handlers** (`EventInfo.AddEventHandler`) do NOT receive Enter — they fire
   for modifier keys (Ctrl, Shift) but AvaloniaEdit swallows Enter before CLR sees it
2. **Avalonia AddHandler with `RoutingStrategies.Bubble` alone** does NOT receive Enter
3. **The working approach:** `AddHandler` with ALL routing strategies
   (`Bubble | Tunnel | Direct = 7`) AND `handledEventsToo=true`

```csharp
// Must combine all three strategies — Bubble alone is insufficient
var bubble = (int)Enum.Parse(routingStrategiesType, "Bubble");   // 4
var tunnel = (int)Enum.Parse(routingStrategiesType, "Tunnel");   // 2
var direct = (int)Enum.Parse(routingStrategiesType, "Direct");   // 1
var routingAll = Enum.ToObject(routingStrategiesType, bubble | tunnel | direct); // 7

// Non-generic AddHandler(RoutedEvent, Delegate, RoutingStrategies, bool)
addHandlerMethod.Invoke(textArea, new[] {
    keyDownRoutedEvent,  // InputElement.KeyDownEvent
    handler,             // Expression.Lambda-compiled delegate
    routingAll,          // RoutingStrategies.Bubble | Tunnel | Direct
    (object)true         // handledEventsToo
});
```

**Hook TextArea, not TextEditor.** TextArea is the actual editing surface (child of
TextEditor) and fires first. Read/write text via the parent TextEditor's CLR `Text`
property.

**Timing:** The handler fires BEFORE Root processes the Enter key, so modifying
`TextEditor.Text` in the handler changes what gets sent.

### Discovery and Hooking

Timer-based discovery (2s interval) walks the visual tree via
`VisualTreeWalker.DescendantsDepthFirst()`:

1. Find nodes assignable from `AvaloniaEdit.Editing.TextArea`
2. Walk up via `Parent` property to find the parent `TextEditor`
3. Attach KeyDown handler to TextArea via `AddHandler`
4. Track hooked controls by `RuntimeHelpers.GetHashCode` to avoid double-hooking
5. When TextArea is hooked, also mark the parent TextEditor to prevent fallback hooking

### URL Cleaning

On Enter key:
1. Read `TextEditor.Text` via CLR property
2. Fast reject: skip if text doesn't contain `?`
3. Match URLs via regex (`https?://[^\s<>"')\]]+`)
4. For each URL with a query string, split on `&` and filter out tracking parameters
   (33 params in a case-insensitive HashSet)
5. Preserve fragments (`#hash`), non-tracking params, and URL structure
6. Write cleaned text back to `TextEditor.Text`
7. Idempotent: if no tracking params found, no write occurs

### Reusing This Pattern

Any feature that needs to intercept outgoing messages can follow this same pattern:

1. Resolve `AvaloniaEdit.TextEditor` and `AvaloniaEdit.Editing.TextArea` from loaded
   assemblies (scan `AppDomain.CurrentDomain.GetAssemblies()`)
2. Timer-based discovery to find TextArea controls in the visual tree
3. `AddHandler` with all routing strategies + `handledEventsToo=true` on TextArea
4. Read/write text via parent TextEditor's CLR `Text` property
5. Handler fires before Root processes Enter — modifications affect sent content

---

## AutoUpdater: Update Channels and Package System

**File:** `hook/AutoUpdater.cs` (~910 lines)

The auto-updater checks GitHub releases for new versions, downloads an encrypted
`.uprpkg` package, decrypts and unpacks it to a staging directory, verifies all expected
files are present and non-empty, then overwrites the installed files in-place. Changes
take effect on the next Root restart.

### Update Channels

Two independent update channels exist, each pulling from a different GitHub repository:

| Property | Stable Channel | Developer Channel |
|----------|---------------|-------------------|
| **Source repo** | `watchthelight/uprooted` (public) | `The-Uprooted-Project/uprooted-private` (private) |
| **API endpoint** | `https://api.github.com/repos/watchthelight/uprooted/releases/latest` | `https://api.github.com/repos/The-Uprooted-Project/uprooted-private/releases?per_page=1` |
| **Download base** | `https://github.com/watchthelight/uprooted/releases/download` | `https://github.com/The-Uprooted-Project/uprooted-private/releases/download` |
| **Authentication** | None (public repo) | XOR-encrypted GitHub PAT (read-only, scoped to private repo) |
| **Release type** | Non-prerelease only (`/releases/latest`) | Pre-releases included (`?per_page=1`) |
| **Access gate** | Open | SHA-256 password validation required to switch to this channel |

**Why `/releases?per_page=1` for dev channel:** The GitHub API endpoint `/releases/latest`
only returns non-prerelease releases. Dev-channel builds are published as pre-releases,
so `/releases/latest` would miss them entirely. Using `?per_page=1` returns the most
recent release regardless of pre-release status.

**Developer channel access:** Users must enter a password to switch to the developer
channel. The password is validated by comparing its SHA-256 hash against a stored
constant (`DevPasswordHash`). This is a UX gate, not a security measure — the PAT is
embedded in the binary.

The active channel is determined by the `AutoUpdateChannel` setting in
`uprooted-settings.ini` (values: `"stable"` or `"developer"`). The setting is read fresh
on each check via `UprootedSettings.Load()`.

### Check Flow

1. **Throttle:** `ShouldCheck()` compares `AutoUpdate.LastCheck` (ISO 8601 timestamp) against
   the current time. Skips if less than 6 hours have passed (`CheckIntervalHours = 6`).
2. **API request:** Hits the channel-appropriate GitHub API endpoint. For the dev channel,
   sets `Authorization: Bearer {decryptedPat}` per-request via `SendAsync` (can't set
   per-request headers with `GetStringAsync`).
3. **Parse response:** Extracts `tag_name` from the JSON response via regex
   (`"tag_name"\s*:\s*"([^"]+)"`) — no `System.Text.Json` (Critical Rule #3).
4. **Version compare:** `CompareVersions()` handles `major.minor.patch-suffix` with correct
   pre-release ordering (`0.3.6-rc < 0.3.6`). Pre-release suffixes sort below the bare
   version.
5. **Download decision:**
   - `latest > current` → download and apply
   - `latest == current` AND manual check → download and compare package hash (catches
     silent hotfixes published under the same version tag)
   - `latest <= current` (background check) → skip ("Already up to date")

### Package Download and Apply

The download URL follows the pattern:
`{downloadBase}/{tag}/auto-update.uprpkg`

For example:
- Stable: `https://github.com/watchthelight/uprooted/releases/download/v0.4.0/auto-update.uprpkg`
- Dev: `https://github.com/The-Uprooted-Project/uprooted-private/releases/download/v0.4.0/auto-update.uprpkg`

**Package contents** (6 files — profiler DLL excluded because it's locked on Windows and rarely changes):

| File | Description |
|------|-------------|
| `UprootedHook.dll` | C# hook assembly |
| `UprootedHook.deps.json` | .NET dependency manifest |
| `uprooted-preload.js` | TypeScript bundle (browser injection) |
| `uprooted.css` | Theme and UI styles |
| `nsfw-filter.js` | NSFW filter browser-side script |
| `link-embeds.js` | Link embeds browser-side script |

**Apply sequence:**
1. Create staging directory (`{uprootedDir}/update-staging/`)
2. Download `auto-update.uprpkg` (with auth token for dev channel)
3. Compute SHA-256 of the downloaded package
4. **Hash-based hotfix detection:** If the installed version matches the release version,
   compare the package hash against the stored hash (`LastPackageHash` in settings). If
   identical, skip (no hotfix). If different, apply the new package.
5. Decrypt and unpack via `UnpackPackage()` (see [Package Format](#package-format))
6. Write extracted files to staging directory
7. Verify all 6 expected files are present and non-empty
8. Copy from staging to the Uprooted install directory (`CopyFileRobust`)
9. Clean up staging directory
10. Update settings: `Version`, `PendingUpdateVersion`, `LastPackageHash`

### Package Format (.uprpkg)

Binary format with multi-layer XOR encryption:

```
Offset  Field               Size        Description
0       Magic               4 bytes     "UPRK" (0x55 0x50 0x52 0x4B)
4       Version             1 byte      0x01 (format version)
5       File count          2 bytes     uint16 LE
7       Nonce               32 bytes    Random per-build nonce

Per file (repeated file_count times):
+0      Filename length     2 bytes     uint16 LE
+2      Filename            N bytes     UTF-8 encoded
+N+2    Data length         4 bytes     uint32 LE
+N+6    Encrypted data      M bytes     XOR-encrypted file contents
```

**Decryption:** Each byte is XOR'd with a position-dependent key derived from three
sources:

```
key[pos] = MasterKey[pos % 64] ^ Nonce[pos % 32] ^ ((pos >> 8) & 0xFF)
```

The 64-byte master key is shared between `hook/AutoUpdater.cs` and
`scripts/pack-update.py`. The 32-byte nonce is randomly generated per build and embedded
in the package header.

### Locked File Handling

`CopyFileRobust()` handles the case where the destination file is locked (e.g.,
`UprootedHook.dll` loaded in the current process). On Windows, renaming a loaded DLL is
permitted — the old handle stays valid. The method renames the locked file to `.old`,
copies the new file in, then attempts to delete the `.old` file.

### Background Update Notification

When a background (non-manual) check successfully applies an update,
`BackgroundUpdateApplied` static event fires with the new version string. `StartupHook`
subscribes to this event in Phase 4.5d and dispatches
`ContentPages.ShowUpdateNotification()` to the UI thread, showing a dismissable overlay
card with a restart button.

Manual checks (user presses "Check for Updates") do not fire this event — the user is
already looking at the settings page and sees the status directly.

### HTTP via Reflection

Uses its own copy of the reflection-based `HttpClient` pattern (project convention of
self-contained files). `EnsureHttpResolved()` resolves `HttpClient`, `GetStringAsync`,
`GetByteArrayAsync`, `GetAsync`, and `SendAsync` from loaded assemblies. Default headers
include `User-Agent: Uprooted-AutoUpdater/1.0`, `Accept: application/vnd.github+json`,
and `Accept-Encoding: identity` (avoids trimmed decompression handlers).

For authenticated requests (dev channel), uses `SendAsync` with per-request
`Authorization: Bearer` header rather than `GetStringAsync`/`GetByteArrayAsync` which
only support default headers.

### Settings

| INI Key | Property | Type | Default | Description |
|---------|----------|------|---------|-------------|
| `AutoUpdate.Enabled` | `AutoUpdateEnabled` | bool | `true` | Enable periodic background checks |
| `AutoUpdate.Notify` | `AutoUpdateNotify` | bool | `true` | Show UI notification on background update |
| `AutoUpdate.Channel` | `AutoUpdateChannel` | string | `"stable"` | `"stable"` or `"developer"` |
| `AutoUpdate.LastCheck` | `LastUpdateCheck` | string | `""` | ISO 8601 UTC timestamp of last check attempt |
| `AutoUpdate.LastPackageHash` | `LastPackageHash` | string | `""` | SHA-256 hex of last applied `.uprpkg` (hotfix detection) |

### Version Comparison

`CompareVersions(a, b)` parses `major.minor.patch-suffix` and returns positive if `a > b`,
negative if `a < b`, 0 if equal. Pre-release suffixes sort below the bare version
(`0.3.6-rc < 0.3.6`). Used by the auto-updater for update decisions and by `StartupHook`
for version migration (force-disable on upgrade).

---

## AvaloniaReflection Deep Dive

**File:** `hook/AvaloniaReflection.cs` (2030 lines)

### Why Reflection Is Needed

Root ships as a trimmed single-file .NET binary. Uprooted cannot reference Avalonia
assemblies at compile time because:

1. The assemblies are embedded in `Root.exe` and only extracted to memory at runtime.
2. `Type.GetType("Avalonia.Controls.Button, Avalonia.Controls")` fails because the
   assembly name doesn't match what's loaded in a single-file context.
3. Many CLR property setters are trimmed away, so `textBox.Text = "foo"` throws
   `MissingMethodException`. The Avalonia property system
   (`SetValue(AvaloniaProperty, object, BindingPriority)`) must be used instead.

`AvaloniaReflection` solves this by scanning all loaded assemblies at runtime and
caching every type, property, method, and field handle needed.

### Type Resolution

The `ResolveTypes()` method (`AvaloniaReflection.cs:148-246`) iterates all assemblies
whose name starts with `"Avalonia"`, collects every type into a dictionary keyed by
`FullName`, and then looks up specific types:

#### Cached Types (All Public Properties)

**Core Controls:**
- `ApplicationType` -- `Avalonia.Application`
- `DispatcherType` -- `Avalonia.Threading.Dispatcher`
- `WindowType` -- `Avalonia.Controls.Window`
- `ControlType` -- `Avalonia.Controls.Control`
- `PanelType` -- `Avalonia.Controls.Panel`
- `StackPanelType` -- `Avalonia.Controls.StackPanel`
- `TextBlockType` -- `Avalonia.Controls.TextBlock`
- `BorderType` -- `Avalonia.Controls.Border`
- `ScrollViewerType` -- `Avalonia.Controls.ScrollViewer`
- `GridType` -- `Avalonia.Controls.Grid`
- `ContentControlType` -- `Avalonia.Controls.ContentControl`
- `ButtonType` -- `Avalonia.Controls.Button`
- `ToggleSwitchType` -- `Avalonia.Controls.ToggleSwitch`
- `TextBoxType` -- `Avalonia.Controls.TextBox`
- `EllipseType` -- `Avalonia.Controls.Shapes.Ellipse` (with fallback name search)
- `CanvasType` -- `Avalonia.Controls.Canvas`

**Overlay / Layout:**
- `OverlayLayerType` -- `Avalonia.Controls.Primitives.OverlayLayer`
- `PointType` -- `Avalonia.Point`
- `RectType` -- `Avalonia.Rect`

**Resource System:**
- `ResourceDictionaryType` -- `Avalonia.Controls.ResourceDictionary`
- `IResourceDictionaryType` -- `Avalonia.Controls.IResourceDictionary`

**Value Types / Brushes:**
- `SolidColorBrushType` -- `Avalonia.Media.SolidColorBrush`
- `LinearGradientBrushType` -- `Avalonia.Media.LinearGradientBrush`
- `GradientStopType` -- `Avalonia.Media.GradientStop`
- `GradientStopsType` -- `Avalonia.Media.GradientStops`
- `RelativePointType` -- `Avalonia.RelativePoint`
- `RelativeUnitType` -- `Avalonia.RelativeUnit`
- `ColorType` -- `Avalonia.Media.Color`
- `ThicknessType` -- `Avalonia.Thickness`
- `CornerRadiusType` -- `Avalonia.CornerRadius`

**Grid Layout:**
- `ColumnDefinitionType` -- `Avalonia.Controls.ColumnDefinition`
- `GridLengthType` -- `Avalonia.Controls.GridLength`
- `GridUnitTypeEnum` -- `Avalonia.Controls.GridUnitType`

**Enums / Structs:**
- `HorizontalAlignmentType` -- `Avalonia.Layout.HorizontalAlignment`
- `VerticalAlignmentType` -- `Avalonia.Layout.VerticalAlignment`
- `OrientationType` -- `Avalonia.Layout.Orientation`
- `TextWrappingType` -- `Avalonia.Media.TextWrapping`
- `FontWeightType` -- `Avalonia.Media.FontWeight`
- `CursorType` -- `Avalonia.Input.Cursor`
- `StandardCursorType` -- `Avalonia.Input.StandardCursorType`

**Extension Methods / Interfaces:**
- `VisualExtensionsType` -- `Avalonia.VisualTree.VisualExtensions`
- `VisualType` -- `Avalonia.Visual`
- `TopLevelType` -- `Avalonia.Controls.TopLevel`
- `DesktopLifetimeType` -- matches `IClassicDesktopStyleApplicationLifetime` by suffix

### Member Resolution

The `ResolveMembers()` method (`AvaloniaReflection.cs:248-426`) caches the following:

**Application Access:**
- `_appCurrent` -- `Application.Current` (static property)
- `_appLifetime` -- `Application.ApplicationLifetime`
- `_lifetimeMainWindow` -- `IClassicDesktopStyleApplicationLifetime.MainWindow`
- `_lifetimeWindows` -- `IClassicDesktopStyleApplicationLifetime.Windows`
- `_appResources` -- `Application.Resources`

**Dispatcher:**
- `_dispatcherUIThread` -- `Dispatcher.UIThread` (static property)
- `_dispatcherPost` -- `Dispatcher.Post(Action, DispatcherPriority)` with 3 fallbacks:
  1. `Post(Action, DispatcherPriority)` (2-param)
  2. `Post(Action)` (1-param)
  3. `InvokeAsync(Action)` (1-param)

**Visual Tree:**
- `_getVisualChildren` -- `VisualExtensions.GetVisualChildren(Visual)` (static)
- `_colorParse` -- `Color.Parse(string)` (static)

**Control Properties:**
- `_panelChildren` -- `Panel.Children`
- `_controlTag` -- `Control.Tag`
- `_controlIsVisible` -- `Control.IsVisible`
- `_controlMargin` -- `Control.Margin`
- `_controlCursor` -- `Control.Cursor`
- `_controlOpacity` -- `Control.Opacity` (fallback: `Visual.Opacity`)
- `_controlIsHitTestVisible` -- `Control.IsHitTestVisible`

**TextBlock Properties:**
- `_textBlockText` -- `TextBlock.Text`
- `_textBlockFontSize` -- `TextBlock.FontSize`
- `_textBlockFontWeight` -- `TextBlock.FontWeight`
- `_textBlockForeground` -- `TextBlock.Foreground`
- `_textBlockTextWrapping` -- `TextBlock.TextWrapping`

**Border Properties:**
- `_borderChild` -- `Border.Child`
- `_borderBackground` -- `Border.Background`
- `_borderCornerRadius` -- `Border.CornerRadius`
- `_borderBorderBrush` -- `Border.BorderBrush`
- `_borderBorderThickness` -- `Border.BorderThickness`

**Container Properties:**
- `_scrollViewerContent` -- `ScrollViewer.Content`
- `_stackPanelOrientation` -- `StackPanel.Orientation`
- `_stackPanelSpacing` -- `StackPanel.Spacing`
- `_contentControlContent` -- `ContentControl.Content`

**Grid Attached Properties:**
- `_gridSetColumn` / `_gridGetColumn` -- `Grid.SetColumn` / `Grid.GetColumn` (static)
- `_gridSetRow` / `_gridGetRow` -- `Grid.SetRow` / `Grid.GetRow` (static)

**ToggleSwitch / TextBox:**
- `_toggleSwitchIsChecked` -- `ToggleSwitch.IsChecked`
- `_textBoxTextProperty` -- `TextBox.TextProperty` (static AvaloniaProperty field)
- `_textBoxWatermarkProperty` -- `TextBox.WatermarkProperty`
- `_textBoxMaxLengthProperty` -- `TextBox.MaxLengthProperty`

**Overlay / Canvas / Positioning:**
- `_overlayGetOverlayLayer` -- `OverlayLayer.GetOverlayLayer(Visual)` (static)
- `_canvasSetLeft` / `_canvasSetTop` -- `Canvas.SetLeft` / `Canvas.SetTop` (static)
- `_layoutableBounds` -- `Control.Bounds` (inherited from Layoutable)
- `_translatePoint` -- `Visual.TranslatePoint(Point, Visual)` with multi-strategy
  resolution (`AvaloniaReflection.cs:361-404`): walks the type hierarchy, tries
  `VisualExtensions` static methods, and falls back to scanning all Avalonia types.

**Resource System:**
- `_resourcesMergedDicts` -- `IResourceDictionary.MergedDictionaries`

### RunOnUIThread

```csharp
public void RunOnUIThread(Action action)
```

(`AvaloniaReflection.cs:553-604`)

Marshals an action to the Avalonia UI thread via `Dispatcher.UIThread.Post()`. Handles
three scenarios:

1. **2-param Post**: Resolves `DispatcherPriority.Normal` via static property/field/enum
   parse. Critical note: `DispatcherPriority` is a **struct** in Avalonia 11+, not an
   enum (`AvaloniaReflection.cs:569`).
2. **1-param Post**: Calls directly with just the action.
3. **Fallback**: Invokes the action inline if no dispatcher is available.

### Control Creation Helpers

All creation methods return `object?` (the Avalonia control instance):

- **`CreateTextBlock(text, fontSize, foregroundHex?, fontWeight?)`**
  (`AvaloniaReflection.cs:650-667`) -- Creates a TextBlock with optional foreground brush and font weight.
- **`CreateStackPanel(vertical, spacing)`** (`AvaloniaReflection.cs:669-685`)
- **`CreateBorder(bgHex?, cornerRadius, child?)`** (`AvaloniaReflection.cs:687-709`)
- **`CreateEllipse(width, height, fillHex?)`** (`AvaloniaReflection.cs:711-728`)
- **`CreateTextBox(watermark?, text?, maxLength)`** (`AvaloniaReflection.cs:730-751`) --
  Uses `SetAvaloniaProperty` internally because CLR property setters are trimmed.
- **`CreatePanel()`** (`AvaloniaReflection.cs:845-849`)
- **`CreateScrollViewer(content?)`** (`AvaloniaReflection.cs:851-859`)
- **`CreateGrid()`** (`AvaloniaReflection.cs:1098-1102`)
- **`CreateCanvas()`** (`AvaloniaReflection.cs:1567-1571`)
- **`CreateBrush(hex)`** (`AvaloniaReflection.cs:861-875`) -- Creates a
  `SolidColorBrush` using the parameterless constructor + Color property setter
  (the `SolidColorBrush(Color)` constructor is trimmed).
- **`CreateLinearGradientBrush(startX, startY, endX, endY, stops)`**
  (`AvaloniaReflection.cs:1739-1830`) -- Creates gradient brushes with
  `RelativePoint.Parse("%,%" )` for start/end points.
- **`CreateResourceDictionary()`** (`AvaloniaReflection.cs:1660-1664`)

### Avalonia Property System Integration

- **`SetAvaloniaProperty(control, avaloniaPropertyField, value)`**
  (`AvaloniaReflection.cs:770-804`) -- Bypasses trimmed CLR setters by calling
  `control.SetValue(AvaloniaProperty, object, BindingPriority.LocalValue)`.
- **`GetAvaloniaProperty(control, avaloniaPropertyField)`**
  (`AvaloniaReflection.cs:825-843`) -- Reads via `control.GetValue(AvaloniaProperty)`.
- **`SetValueStylePriority(control, propertyFieldName, value)`**
  (`AvaloniaReflection.cs:1266-1307`) -- Sets a value at Style priority (lower than
  LocalValue), allowing hover/pressed style triggers to temporarily override it.
- **`ClearValue(control, propertyFieldName)`** (`AvaloniaReflection.cs:1178-1218`) --
  Calls `ClearValue(AvaloniaProperty)` to remove a local override, letting
  DynamicResource bindings reassert.
- **`ClearValueSilent(control, propertyFieldName)`**
  (`AvaloniaReflection.cs:1224-1251`) -- Silent version for batch operations (no logging).
- **`EnsureBindingPriorityResolved()`** (`AvaloniaReflection.cs:807-820`) -- Lazily
  resolves `BindingPriority.LocalValue` (value `0`) from `Avalonia.Data.BindingPriority`.

### Event Subscription via Expression.Lambda

```csharp
public void SubscribeEvent(object control, string eventName, Action callback)
```

(`AvaloniaReflection.cs:1141-1170`)

Avalonia RoutedEvents cannot use `EventInfo.AddEventHandler` directly. Instead, this
method builds a compiled `Expression.Lambda` matching the event's delegate signature:

```csharp
var lambda = Expression.Lambda(handlerType, invokeExpr, p0, p1);
var handler = lambda.Compile();
eventInfo.AddEventHandler(control, handler);
```

A pointer-event variant exists at `AvaloniaReflection.cs:1846-1901`:

```csharp
public void SubscribePointerEvent(object control, string eventName, Action<double, double> callback)
```

This extracts pointer position via `e.GetPosition(sender)` and passes `(x, y)` to the
callback.

### Resource Dictionary Operations

- **`GetAppResources()`** (`AvaloniaReflection.cs:1578-1583`) -- `Application.Current.Resources`
- **`GetStyleResources(styleIndex)`** (`AvaloniaReflection.cs:1589-1616`) --
  `Application.Styles[index].Resources` where Root's theme colors live.
- **`GetResource(dict, key)`** (`AvaloniaReflection.cs:1621-1637`)
- **`GetMergedDictionaries(resources)`** (`AvaloniaReflection.cs:1642-1655`)
- **`AddResource(dict, key, value)`** (`AvaloniaReflection.cs:1670-1696`)
- **`RemoveResource(dict, key)`** (`AvaloniaReflection.cs:1701-1717`)
- **`EnumerateResources(resources, callback)`** (`AvaloniaReflection.cs:1915-1942`)

### Other Notable Methods

- **`GetAllWindows()`** (`AvaloniaReflection.cs:445-465`) -- Returns all open windows
  from `IClassicDesktopStyleApplicationLifetime.Windows`.
- **`GetAllTopLevels()`** (`AvaloniaReflection.cs:477-522`) -- Uses Avalonia's internal
  `WindowImpl.s_instances` list to find ALL native windows (including popup roots for
  profile cards, context menus, etc.), then gets the `Owner` (TopLevel) for each.
- **`GetParent(node)`** (`AvaloniaReflection.cs:1398-1409`) -- Tries `VisualParent`
  first, then `Parent`.
- **`CopyToClipboard(window, text)`** (`AvaloniaReflection.cs:1371-1394`) -- Uses
  `TopLevel.Clipboard.SetTextAsync(string)`.
- **`PropertyToFieldName(propertyName)`** (`AvaloniaReflection.cs:1313`) -- Maps
  `"Background"` to `"BackgroundProperty"`.

### Advanced AvaloniaReflection Patterns

The sections above cover the general-purpose reflection cache. What follows describes
four advanced patterns that are not obvious from the API surface alone: how windows are
discovered, how coordinate spaces are bridged, how event subscription works around CLR
limitations, and how Avalonia's style priority system governs value precedence.

For conceptual background on Avalonia's property system, visual tree, and threading
model, see [Avalonia Patterns](AVALONIA_PATTERNS.md).

#### WindowImpl.s_instances -- Discovering All Native Windows

Avalonia creates one native window per top-level surface. The main application window is
accessible via `IClassicDesktopStyleApplicationLifetime.MainWindow`, but popup roots
(profile cards, context menus, tooltips, overlay windows) are **not** -- they live in
separate `Window` objects that the lifetime does not expose. The theme engine and overlay
system need access to every live surface.

Avalonia's internal `WindowImpl` class (platform-specific: `Win32WindowImpl` on Windows,
`X11WindowImpl` on Linux) keeps a private static list of all active native window
instances:

```csharp
// Internal to Avalonia -- not part of the public API
private static readonly List<WindowImpl> s_instances;
```

`AvaloniaReflection.ResolveWindowImpl()` (`AvaloniaReflection.cs:524-551`) discovers
this list at runtime:

1. Scans all loaded assemblies whose `FullName` contains `"Avalonia"`.
2. Searches for a type named `WindowImpl` (exact name match on `Type.Name`).
3. Resolves the `Owner` property (`BindingFlags.Public | NonPublic | Instance`) --
   this returns the Avalonia `TopLevel` (or `IInputRoot`) that owns the native window.
4. Resolves the `s_instances` field (`BindingFlags.NonPublic | Static`).

Once resolved, `GetAllTopLevels()` (`AvaloniaReflection.cs:477-522`) iterates
`s_instances`, reads `Owner` on each, and returns the deduplicated list of all live
`TopLevel` objects. The method is lazy -- `ResolveWindowImpl()` runs only on the first
call, guarded by `_windowImplResolved`.

**Fallback behavior:** If `WindowImpl` resolution fails (e.g., Avalonia renamed the
internal type), the method silently returns a single-element list containing only
`MainWindow`. This ensures the theme engine and overlay system degrade gracefully rather
than crashing.

**Why this matters for theming:** `ThemeEngine.WalkAllWindows()` uses `GetAllTopLevels()`
to color-walk every surface. Without this, popup windows and overlay cards would retain
Root's default blue accent while the main window shows the user's chosen theme.

#### TranslatePoint -- Coordinate Mapping Between Visual Nodes

The color picker popup, overlay layer, and diagnostic tools need to convert between
coordinate spaces (e.g., "where is this swatch relative to the main window?"). Avalonia
provides `TranslatePoint(Point, Visual)` for this, but the method location varies
between Avalonia versions and build configurations.

`AvaloniaReflection` resolves `_translatePoint` (`AvaloniaReflection.cs:358-404`) using
a three-strategy fallback chain:

**Strategy 1 -- Instance method on the type hierarchy:**

```csharp
var translateSearch = ControlType;
while (translateSearch != null && _translatePoint == null)
{
    _translatePoint = translateSearch.GetMethods(pub | BindingFlags.DeclaredOnly)
        .FirstOrDefault(m => m.Name == "TranslatePoint" && m.GetParameters().Length == 2);
    translateSearch = translateSearch.BaseType;
}
```

Walks from `Control` up through `Layoutable`, `Visual`, and `StyledElement` looking for
an instance method named `TranslatePoint` with exactly 2 parameters (the `Point` and
the target `Visual`). In Avalonia 11+, this is typically on `Visual`.

**Strategy 2 -- Static extension method on VisualExtensions:**

```csharp
_translatePoint ??= VisualExtensionsType?.GetMethods(stat)
    .FirstOrDefault(m => m.Name == "TranslatePoint" && m.GetParameters().Length == 3);
```

Older Avalonia versions or certain trimmed configurations expose `TranslatePoint` as a
static extension method on `Avalonia.VisualTree.VisualExtensions`. The static variant
takes 3 parameters: `(Visual from, Point point, Visual to)`.

**Strategy 3 -- Brute-force scan of all Avalonia types:**

If neither strategy finds the method, `ResolveMembers` logs a warning and iterates every
type in every Avalonia assembly, checking both static classes and instance types for any
method named `TranslatePoint`.

The public wrapper `TranslatePoint(object from, double x, double y, object to)`
(`AvaloniaReflection.cs:1477-1520`) handles both the instance and static calling
conventions:

```csharp
if (_translatePoint.IsStatic)
    result = _translatePoint.Invoke(null, new[] { from, point, to });
else
    result = _translatePoint.Invoke(from, new[] { point, to });
```

The return type is `Point?` (nullable struct). The wrapper unwraps it by reading the
`HasValue` property (if present), then extracts `X` and `Y` via reflection on the
result's `Type`.

#### Expression.Lambda for Pointer and Routed Events

**The problem:** Avalonia RoutedEvents (like `PointerPressed`, `PointerMoved`,
`PointerReleased`, `Click`) use custom delegate types such as
`EventHandler<PointerPressedEventArgs>`. At compile time, Uprooted has no reference to
these types. The standard `EventInfo.AddEventHandler(object, Delegate)` requires a
delegate whose type exactly matches the event's `EventHandlerType`. Passing a plain
`Action` or `EventHandler` throws `ArgumentException` because the CLR cannot convert
between unrelated delegate types.

**The solution:** Build a compiled expression tree that matches the exact delegate
signature at runtime, then use `EventInfo.AddEventHandler` with the correctly-typed
compiled delegate.

**Simple events** (`SubscribeEvent`, `AvaloniaReflection.cs:1141-1170`):

```csharp
// 1. Discover the event's delegate type and parameter types
var handlerType = eventInfo.EventHandlerType!;
var invokeMethod = handlerType.GetMethod("Invoke")!;
var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();

// 2. Build expression parameters matching the delegate signature
var p0 = Expression.Parameter(paramTypes[0], "sender");   // object
var p1 = Expression.Parameter(paramTypes[1], "e");         // PointerPressedEventArgs

// 3. Build a lambda that ignores sender/e and calls our Action
var callbackExpr = Expression.Constant(callback);
var invokeExpr = Expression.Invoke(callbackExpr);
var lambda = Expression.Lambda(handlerType, invokeExpr, p0, p1);

// 4. Compile and register
var handler = lambda.Compile();
eventInfo.AddEventHandler(control, handler);
```

The key insight is `Expression.Lambda(handlerType, ...)` -- this creates a lambda whose
`.GetType()` matches the event's `EventHandlerType`, satisfying the CLR's delegate type
check. The body ignores both parameters and simply invokes the captured `Action`.

**Pointer events with position extraction** (`SubscribePointerEvent`,
`AvaloniaReflection.cs:1846-1901`):

The pointer variant is more complex because it extracts `(x, y)` coordinates from the
event args. The expression tree encodes the equivalent of:

```csharp
(sender, e) => {
    var pos = e.GetPosition((Visual)sender);
    callback(pos.X, pos.Y);
}
```

Built step by step:

1. Find `GetPosition(Visual)` on the event args type (`paramTypes[1]`). Falls back to
   any single-parameter `GetPosition` overload if the `Visual` type cannot be matched.
2. Build `Expression.Convert(p0, ...)` to cast `sender` to the parameter type expected
   by `GetPosition`.
3. Build `Expression.Call(p1, getPositionMethod, castSender)` to invoke
   `e.GetPosition(sender)`.
4. Extract `pos.X` and `pos.Y` via `Expression.Property`.
5. Build `Expression.Invoke(callbackExpr, xExpr, yExpr)` to call the
   `Action<double, double>` with the coordinates.
6. Wrap in `Expression.Lambda(handlerType, invokeExpr, p0, p1)` and compile.

This compiled delegate runs at near-native speed despite being built at runtime. The
expression tree is compiled once per subscription and cached by the CLR's JIT.

**Why not use a dynamic method or Delegate.CreateDelegate?** Both alternatives require
knowing the delegate type at compile time or building IL manually. Expression trees are
higher-level, produce readable debug output, and integrate naturally with the CLR's type
system. They are also safe to use in partial-trust and profiler-injected contexts where
direct IL emission may be restricted.

#### BindingPriority -- Value Precedence in the Avalonia Property System

Avalonia's property system resolves each property's effective value by checking multiple
sources in priority order. The `BindingPriority` type defines these levels (from highest
to lowest precedence):

| Value | Name | Meaning |
|------:|------|---------|
| -1 | `Animation` | Active animation values (highest priority) |
| 0 | `LocalValue` | Explicit `control.SetValue(prop, val)` -- equivalent to XAML `Property="Value"` |
| 1 | `StyleTrigger` | Pseudo-class triggers (`:pointerover`, `:pressed`, `:focus`) |
| 2 | `Template` | Values set inside `ControlTemplate` |
| 3 | `Style` | Values set by `Style` setters (theme styles, global styles) |
| 4 | `Inherited` | Values inherited from the parent visual |
| 5 | `Unset` | No value set; the property's default applies |

**Critical Avalonia 11 detail:** `BindingPriority` is a **struct** (value type), not an
enum. It has static properties like `BindingPriority.LocalValue` that return struct
instances. Code that tries `Enum.Parse(typeof(BindingPriority), "LocalValue")` will
throw. The reflection code in `EnsureBindingPriorityResolved()`
(`AvaloniaReflection.cs:807-820`) works around this by looking up the type
`Avalonia.Data.BindingPriority` and using `Enum.ToObject(bpType, 0)` which succeeds
because the struct's underlying representation is still an integer.

**How Uprooted uses priorities:**

`SetAvaloniaProperty()` (`AvaloniaReflection.cs:770-804`) writes values at
**LocalValue** priority (0). This is used for control construction -- creating
TextBlocks, Borders, etc. -- where the value should be definitive and override styles.

`SetValueStylePriority()` (`AvaloniaReflection.cs:1266-1307`) writes values at
**Style** priority (3). This is critical for the theme engine's visual tree walk. When
the theme engine recolors a Button's `Background`, it sets the replacement brush at
Style priority. If the user hovers over that button, Avalonia's `:pointerover`
StyleTrigger (priority 1) temporarily overrides the Style-priority value with the hover
color. When the pointer leaves, the StyleTrigger deactivates and the theme engine's
Style-priority value reasserts -- producing correct hover/unhover behavior.

If the theme engine used **LocalValue** priority instead, hover triggers would never
fire because LocalValue (0) outranks StyleTrigger (1). The button would appear frozen
with no hover feedback.

**Live preview exception:** During color picker drag (`UpdateCustomThemeLive`), the
theme engine deliberately uses direct `prop.SetValue()` which writes at LocalValue
priority. This is necessary because Root's own controls may have LocalValue-priority
colors set via code-behind, and Style-priority writes would not override them. The
trade-off is that hover feedback is suppressed during live preview, but this is
acceptable for the brief duration of a color drag.

**How style priorities ensure Uprooted wins:**

The theme engine applies colors through two channels, each targeting a different
priority level:

1. **Resource dictionary injection** (Phase 1-2 of `ApplyThemeInternal`): Overwrites
   named resource keys like `ThemeAccentBrush` and `SystemAccentColor` in
   `Styles[0].Resources` and a merged `ResourceDictionary`. Controls that use
   `DynamicResource` bindings automatically pick up the new values because the binding
   system re-evaluates when the resource changes. No explicit priority is needed here
   -- the resource itself changes, so all bindings resolve to the new value regardless
   of the priority at which they were originally bound.

2. **Visual tree color walk** (Phase 4 of `ApplyThemeInternal`): For controls with
   hardcoded brushes (not bound to resources), the walk sets replacement brushes at
   Style priority via `SetValueStylePriority`. This is high enough to override
   `Inherited` and `Unset` values, but low enough to let `StyleTrigger` hover effects
   work. The theme engine's values thus "win" over Root's theme defaults (which are
   typically at Style priority or lower) while preserving interactive feedback.

   The one exception is controls where Root sets colors at LocalValue priority in
   code-behind. For these, Style-priority writes are invisible. The live preview mode
   handles this by escalating to LocalValue, and the normal walk's `prop.SetValue`
   fallback (`ThemeEngine.cs:1015`) catches cases where `SetValueStylePriority` cannot
   override the existing value.

---

## Visual Tree Discovery

**File:** `hook/VisualTreeWalker.cs` (554 lines)

### The SettingsLayout Record

```csharp
internal class SettingsLayout
{
    public required object NavContainer { get; init; }
    public required object ContentArea { get; init; }
    public required object LayoutContainer { get; init; }
    public required object AppSettingsText { get; init; }
    public object? ListBox { get; init; }
    public bool IsGridLayout { get; init; }
    public int ContentColumnIndex { get; init; }
    public int ContentRowIndex { get; init; }
    public object? BackButton { get; init; }
    public object? VersionBorder { get; init; }
    public object? SignOutControl { get; init; }
    public object? SidebarGrid { get; init; }
    public object? SaveBar { get; init; }
    public int AdvancedIndex { get; init; }
}
```

(`VisualTreeWalker.cs:3-21`)

| Field | Meaning |
|-------|---------|
| `NavContainer` | The StackPanel holding all navigation items (section headers + ListBox) |
| `ContentArea` | The panel where page content is displayed (right side of settings Grid) |
| `LayoutContainer` | The parent Grid containing both nav and content |
| `AppSettingsText` | The "APP SETTINGS" TextBlock (used for style matching) |
| `ListBox` | Root's ListBox with nav items (Account, Notifications, etc.) |
| `IsGridLayout` | Whether the layout uses a multi-column Grid |
| `ContentColumnIndex` | Grid column index of the content area |
| `ContentRowIndex` | Grid row index of the content area |
| `BackButton` | The "<" back button in the header row |
| `VersionBorder` | NavContainer child[1] containing version info |
| `SignOutControl` | NavContainer child[2] containing sign-out |
| `SidebarGrid` | Grid parent of NavContainer (rows: [1*, Auto]) |
| `SaveBar` | "You have unsaved changes" bar with Revert/Save buttons |
| `AdvancedIndex` | Index of "Advanced" item in the ListBox (-1 if not found) |

### DFS Algorithm

```csharp
public IEnumerable<object> DescendantsDepthFirst(object root)
```

(`VisualTreeWalker.cs:35-49`)

Stack-based iterative DFS. Pushes all visual children of each node onto the stack and
yields them one at a time. Used throughout the codebase for tree searches.

### FindSettingsLayout Step-by-Step

`FindSettingsLayout(object window)` (`VisualTreeWalker.cs:79-181`):

**Step 1** (`VisualTreeWalker.cs:82-84`): Find the "APP SETTINGS" TextBlock by exact
text match. Also tries "App Settings" as a fallback.

**Step 2** (`VisualTreeWalker.cs:87-108`): Walk up from the TextBlock to find the nav
container StackPanel via `FindNavContainer()` (`VisualTreeWalker.cs:363-398`). This
walks up the parent chain looking for a StackPanel with 8+ children (the nav container
has section headers + many items). Falls back to the best StackPanel with 3+ children.

**Step 2.5** (`VisualTreeWalker.cs:99-107`): Search the nav container for a `ListBox`.

**Step 3** (`VisualTreeWalker.cs:110-125`): Find the content area via
`FindContentArea()` (`VisualTreeWalker.cs:403-513`). Walks up from the nav container
to find a multi-column Grid layout. Skips single-column Grids. Identifies the content
area as the sibling in a different Grid column from the nav, preferring higher column
indices (content is to the right of nav).

**Steps 4-6** (`VisualTreeWalker.cs:131-180`): Discover additional elements:
- `FindBackButton()` searches Grid Row=0 for a TextBlock containing "<", then walks up
  to find a clickable ancestor.
- `FindSaveBar()` searches Grid Row=2 or any child containing "Revert" text.
- `FindAdvancedIndex()` enumerates ListBoxItems looking for "Advanced" text.

### Helper Methods

- **`FindFirstTextBlock(root, exactText)`** (`VisualTreeWalker.cs:51-59`) -- DFS
  search for a TextBlock with exact text match.
- **`FindFirstTextBlockContaining(root, substring)`**
  (`VisualTreeWalker.cs:61-73`) -- Case-insensitive substring search.
- **`FindRevertButton(saveBar)`** (`VisualTreeWalker.cs:294-323`) -- Finds the
  "Revert" button inside the save bar by walking up from the text to a Button ancestor.
- **`HasTaggedDescendant(root, tag)`** (`VisualTreeWalker.cs:526-534`) -- Checks if
  any descendant has a specific `Control.Tag` value.
- **`DumpTree(root, maxDepth)`** (`VisualTreeWalker.cs:539-553`) -- Diagnostic tree
  dump with type names, text content, and tags.

---

## Sidebar Injection

**File:** `hook/SidebarInjector.cs` (1408 lines)

### Architecture Overview

The `SidebarInjector` is a timer-based monitor that:
1. Detects when the settings page is open.
2. Injects Uprooted navigation items into Root's sidebar.
3. Manages custom content pages in Root's content area.
4. Cleans up on page close or navigation.

### Timer and Polling

```csharp
private const int PollIntervalMs = 200;
```

(`SidebarInjector.cs:16`)

The timer fires every 200ms (`SidebarInjector.cs:81`). Each tick:

1. **Interlocked guard** (`SidebarInjector.cs:92`): `CompareExchange` prevents
   concurrent execution. A 3-second safety timeout releases the guard if the UI thread
   callback hangs (`SidebarInjector.cs:102-105`).
2. **RunOnUIThread** (`SidebarInjector.cs:95-99`): All visual tree work happens on the
   Avalonia UI thread.
3. **CheckAndInject** (`SidebarInjector.cs:114-160`): The main decision logic.

### Injection Pipeline

When the settings page is detected (`SidebarInjector.cs:164-246`), `InjectIntoSettings`
runs these steps:

**Step 0.5** (`SidebarInjector.cs:194`): Capture native font family from the
"APP SETTINGS" TextBlock for pixel-perfect styling.

**Step 1** (`SidebarInjector.cs:197-214`): Handle save bar -- find the Revert button
and subscribe `PointerPressed` to clean up injection BEFORE Root tears down the visual
tree (prevents freeze from `OnDetachedFromVisualTreeCore` walking a modified tree).

**Step 2** (`SidebarInjector.cs:217-223`): Remove Root's version border and sign-out
control from `NavContainer` (they'll be re-added at the bottom after our items).

**Step 3** (`SidebarInjector.cs:226`): Build and insert nav items via
`BuildAndInsertNavItems()` (`SidebarInjector.cs:322-389`):
- Creates a wrapper StackPanel (spacing: 0) to avoid extra gaps from NavContainer's
  non-zero spacing.
- Adds an "UPROOTED" section header matching Root's "APP SETTINGS" styling.
- Adds three nav items: "Uprooted", "Plugins", "Themes".
- Re-adds the original version border and sign-out controls at the bottom.

**Step 4** (`SidebarInjector.cs:229`): Wrap NavContainer in a ScrollViewer for sidebar
scrolling via `WrapInScrollViewer()` (`SidebarInjector.cs:632-676`).

**Step 5** (`SidebarInjector.cs:232`): Inject "Uprooted {version}" version text into
Root's grey version info box via `InjectVersionText()` (`SidebarInjector.cs:711-788`).

**Step 6** (`SidebarInjector.cs:235`): Record current ListBox selection index for
change detection.

### Nav Item Structure

Each nav item (`BuildNavItem`, `SidebarInjector.cs:415-498`) matches Root's native
ListBoxItem structure:

```
Panel (H=40, cursor=Hand, BG=transparent)
  Panel (M=0,2,0,2)
    Border (H=36, CR=12)              <- highlight
    TextBlock (M=12,0,12,0, VA=Center, FW=450)  <- content
```

Hover events on the outer panel toggle the highlight border's background. Click events
call `OnNavItemClicked(pageName)`.

### Content Management

**`OnNavItemClicked(pageName)`** (`SidebarInjector.cs:502-590`):

1. Removes current content page from the content panel.
2. Builds a new page via `ContentPages.BuildPage()`.
3. Hides all existing Root children in the content panel (sets `IsVisible = false`
   instead of removing them, to preserve Root's state).
4. Adds the new page to the content panel.
5. Deselects Root's ListBox (sets `SelectedIndex = -1`).
6. Hides the save bar to prevent Revert freeze.
7. Updates nav highlights.
8. Schedules delayed save bar hide (Root creates the save bar asynchronously after
   ListBox deselection).

**`RemoveContentPage()`** (`SidebarInjector.cs:592-615`): Removes our page, restores
Root's hidden children visibility, restores save bar visibility.

### Alive-Check Mechanism

When injected (`SidebarInjector.cs:116-143`):
- **ListBox selection polling**: Every tick, checks if Root's ListBox selection changed.
  If the user clicks a Root nav item, removes our content page.
- **Throttled alive check**: Every 5 ticks (~1 second), searches the visual tree for
  "APP SETTINGS" text. If not found, the settings page has closed -- calls `NullState()`
  to reset all state.

### Cleanup and State Reset

**`CleanupInjection()`** (`SidebarInjector.cs:248-293`): Full cleanup in order:
1. Unwrap ScrollViewer.
2. Remove all injected controls from NavContainer.
3. Re-add original version border and sign-out.
4. Remove version text from the grey box.
5. Restore save bar visibility.
6. Remove content page.

**`NullState()`** (`SidebarInjector.cs:295-318`): Nulls all references and resets all
state flags to allow re-injection on next settings page open.

### Tag System

All injected controls are tagged with `"uprooted-injected"` via `Control.Tag`. This
allows:
- Detection of already-injected controls (`HasTaggedDescendant` check at
  `SidebarInjector.cs:170`).
- The theme engine to identify Uprooted UI elements via `"uprooted-no-recolor"` tags.

---

## Content Pages

**File:** `hook/ContentPages.cs` (~3340 lines)

### Page Builder Pattern

```csharp
public static object? BuildPage(string pageName, AvaloniaReflection r,
    UprootedSettings settings, object? nativeFontFamily = null,
    ThemeEngine? themeEngine = null, Action? onThemeChanged = null)
```

(`ContentPages.cs:112-137`)

Routes to one of three page builders by name:
- `"uprooted"` -> `BuildUprootedPage()`
- `"plugins"` -> `BuildPluginsPage()`
- `"themes"` -> `BuildThemesPage()`

Each page is wrapped in a ScrollViewer with a themed background color. The page is
tagged `"uprooted-content"`.

### Color Theming for Pages

Static color fields (`ContentPages.cs:18-31`) are updated at page build time by
`ApplyThemedColors()` (`ContentPages.cs:69-110`). When a theme is active, card
backgrounds, text colors, and accent colors derive from the theme palette. The
`UpdateLiveColors()` method (`ContentPages.cs:37-63`) is called during live color picker
drag for real-time preview.

### Styling Constants

Root's native settings pages use these styles:
- **Cards:** `BG=#0f1923`, `CornerRadius=12`, `BorderThickness=0.5`
- **Section headers:** `FontSize=12`, `FontWeight=Medium(500)`, `Fg=#66f2f2f2`
- **Field labels:** `FontSize=13`, `FontWeight=450`, `Fg=#a3f2f2f2`
- **Page title:** `FontSize=20`, `FontWeight=SemiBold(600)`, `Fg=#fff2f2f2`
- **Font:** CircularXX TT (captured from native controls)
- **Page container:** `StackPanel(Margin=24,24,24,0)`

### Uprooted Page

`BuildUprootedPage()` (`ContentPages.cs:145-280`)

Four cards:
1. **Identity card** -- "UPROOTED" header with version badge, about text.
2. **Status card** -- Shows hook status, settings injection, plugin count, theme override.
3. **Links card** -- GitHub and website links.
4. **Hook Info card** -- Technical explanation of how the hook works.

### Plugins Page

`BuildPluginsPage()` (`ContentPages.cs:303-529`)

Features:
- **Search box** -- TextBox with "Search for a plugin..." watermark, live filtering.
- **Filter dropdown** -- OverlayLayer popup (Show All / Show Enabled / Show Disabled)
  via `ShowFilterDropdown()` (`ContentPages.cs:665-767`).
- **Plugin cards** in a 2-column grid layout -- each card has a name, toggle switch,
  optional info icon, and description.
- **Plugin info lightbox** -- `ShowPluginInfoLightbox()` (`ContentPages.cs:793-903`)
  shows a centered overlay card with full plugin details.

Known plugins are defined in `KnownPlugins` (`ContentPages.cs:283-291`):
- `sentry-blocker` -- Blocks Sentry error tracking (enabled by default)
- `themes` -- Built-in theme engine (enabled by default)

The `BuildToggleSwitch()` method (`ContentPages.cs:978-1035`) creates a pill-shaped
toggle (44x24) with animated thumb and hover effects.

### Themes Page

`BuildThemesPage()` (`ContentPages.cs:1037-1135`)

Three sections:
1. **Preset Themes** -- Three theme cards in a horizontal row: Default, Crimson, Loki.
   Each card shows a mini preview swatch (`BuildThemePreview()`,
   `ContentPages.cs:1594-1662`) with an accent bar, background, and simplified layout.
2. **Custom Theme** -- `BuildCustomThemeSection()` (`ContentPages.cs:1140-1375`) with:
   - Radio-style header (clickable to activate custom theme).
   - Accent color input row with hex TextBox and color swatch.
   - Background color input row with hex TextBox and color swatch.
   - Clicking a swatch opens the `ColorPickerPopup`.
   - "Apply Custom" button.
   - Debounced auto-save (1 second after last change).
   - Live theme preview on TextChanged events.
3. **Highlight Override** -- `BuildPingColorSection()` (`ContentPages.cs:~2000-2170`) with:
   - Card with toggle indicator (filled circle when active, empty when inactive).
   - Description: "Override the mention/reply highlight color. Persists across theme switches."
   - Color input row via `BuildColorInputRow()` with live preview via `themeEngine.SetCustomPingColor()`.
   - Swatch tagged `uprooted-no-recolor` to prevent tree walker interference.
   - Reset button to clear the override and restore theme defaults.
   - Debounced auto-save (1 second after last change).
   - Clicking the header when active clears the override.
4. **About Themes** -- Informational card about how themes work.

### How to Add a New Page

1. Add a new case to the `switch` in `BuildPage()` (`ContentPages.cs:116-121`).
2. Create a `BuildNewPage()` method following the pattern of `BuildUprootedPage()`.
3. Call `ApplyThemedColors(themeEngine)` at the start.
4. Build the page as a StackPanel with `Margin=24,24,24,0`.
5. Return it wrapped in `r.CreateScrollViewer(page)`.
6. Add the nav item in `SidebarInjector.BuildAndInsertNavItems()` at
   `SidebarInjector.cs:366`.

---

## Theme Engine

**File:** `hook/ThemeEngine.cs` (~2510 lines)

The theme engine is the largest and most complex component. It modifies Root's native
Avalonia UI colors at runtime through two complementary strategies: resource dictionary
injection and visual tree color walking.

### Architecture

Theme application has 6 phases (in `ApplyThemeInternal`, `ThemeEngine.cs:365-586`):

1. **Phase 1: Override Styles[0].Resources** -- SimpleTheme keys
   (`ThemeAccentColor`, `ThemeAccentBrush`, etc.) in `Application.Styles[0].Resources`
   are directly overwritten. Original values are saved in `_savedOriginals` for revert.
   *(Note: These are SimpleTheme keys, not Root's actual theme keys. Root's views bind to
   32 custom keys (`BrandPrimary`, `TextPrimary`, etc.) in `ThemeDictionaries`.)*

2. **Phase 2: Add MergedDictionary** -- FluentTheme keys are injected via a
   new `ResourceDictionary` added to `Application.Resources.MergedDictionaries`. For
   every Color key, a corresponding Brush variant is auto-generated.
   *(Note: Root uses `MediaFluentTheme`, not standard `FluentTheme`. Its controls do not
   bind to these keys (`SystemAccentColor`, etc.). See
   [`research/ROOT_THEME_SYSTEM_FINDINGS.md`](../../research/ROOT_THEME_SYSTEM_FINDINGS.md)
   for the correct target keys.)*

3. **Phase 3: Visual Tree Color Maps** -- Builds a mapping of original ARGB colors to
   replacement colors. Also cross-maps from previous themes and stale colors via
   `_rootOriginals`.

4. **Phase 4: Visual Tree Walks** -- Immediate full walk + scheduled 500ms continuous
   walks + LayoutUpdated interceptor for instant navigation detection.

5. **Phase 5: DWM Title Bar** -- Sets the Windows 11 title bar color via
   `DwmSetWindowAttribute(DWMWA_CAPTION_COLOR)`.

6. **Phase 6: Custom Ping Color Override** -- If the user has set a custom ping/reply
   highlight color (`_customPingColor`), overrides `HighlightForegroundColor`,
   `HighlightForegroundBrush`, and `TextSelectionHighlightColor` (with `0x60` alpha)
   in both `Styles[0].Resources` and the injected `MergedDictionary`. This runs last
   so the override persists across theme switches.
   *(Note: Root's actual mention highlight keys are `SelfMentionBackground`,
   `SelfMentionBorder`, `OtherMentionBackground`, etc. in `ThemeDictionaries`. The
   current `HighlightForegroundColor` target is a SimpleTheme key that may not affect
   Root's mention rendering. See
   [`research/ROOT_THEME_SYSTEM_FINDINGS.md`](../../research/ROOT_THEME_SYSTEM_FINDINGS.md)
   for the correct mention color keys.)*

### Resource Dictionary Injection

(`ThemeEngine.cs:379-493`)

Each key in the palette is tested: if it contains `"Brush"` or ends with `"Fill"`, a
`SolidColorBrush` is created; otherwise, a `Color` struct is created. Resources are
written to both `Styles[0].Resources` and the injected `MergedDictionary`.

### Visual Tree Color Walk

The walk uses a two-pass approach (`WalkAndRecolor`, `ThemeEngine.cs:970-1023`):

**Pass 1 -- Collect** (`CollectColorChanges`, `ThemeEngine.cs:1025-1071`):
Recursively walks the visual tree, reading `Background`, `Foreground`, `BorderBrush`,
and `Fill` properties. For each `SolidColorBrush`, extracts the color string and looks
it up in `_activeColorMap`. Skips subtrees tagged `"uprooted-no-recolor"`. Max depth: 50.

**Pass 2 -- Apply**: For each pending change, creates a new brush and sets it. In
normal mode, uses `SetValueStylePriority` (Style priority preserves hover/pressed
triggers). In live preview mode, uses direct `prop.SetValue` (LocalValue priority to
force-override existing LocalValues).

### Walk Scheduling

- **Continuous timer** (`ScheduleVisualTreeWalks`, `ThemeEngine.cs:599-624`): 500ms
  interval, first walk at 200ms. Root rebuilds its visual tree on every navigation.
- **Layout interceptor** (`InstallLayoutInterceptor`, `ThemeEngine.cs:631-666`): Hooks
  `MainWindow.LayoutUpdated` for instant detection. Debounced to 80ms minimum interval.
- **Rapid follow-up** (`ScheduleRapidFollowUp`, `ThemeEngine.cs:672-692`): After
  navigation, walks at +200ms, +500ms, +1000ms.
- **Walk burst** (`ScheduleWalkBurst`, `ThemeEngine.cs:845-858`): Public method for
  external callers (e.g., after page rebuild).

### All Windows Walk

`WalkAllWindows()` (`ThemeEngine.cs:728-738`) iterates ALL TopLevel instances (not just
MainWindow) via `GetAllTopLevels()`. This catches popup windows for profile cards,
context menus, and overlays that exist in separate window objects.

### Preset Themes

Two preset themes are defined in `Themes` (`ThemeEngine.cs:2029-2217`):

**Crimson** (`ThemeEngine.cs:2031-2128`):
- Accent: `#C42B1C` (deep red)
- Background: `#241414` (warm dark)
- Text tint: warm pink (`#F0EAEA`)
- Full palette of ~55 resource keys

**Loki** (`ThemeEngine.cs:2130-2215`):
- Accent: `#2A5A40` (moss green)
- Background: `#0F1210` (trestle dark)
- Text tint: warm earthy (`#F0ECE0`)
- Highlight foreground: `#D4A847` (gold)
- Full palette of ~55 resource keys

### Tree Color Maps

Static tree color maps in `TreeColorMaps` (`ThemeEngine.cs:865-958`) define ARGB-to-ARGB
replacements for hardcoded colors in the visual tree. Each map covers:
- Blue accent variants -> themed accent
- Structural dark backgrounds -> themed backgrounds
- Dark borders -> themed borders
- Semi-transparent text -> themed text

**Critical rule:** Every replacement value must be UNIQUE within its map. The reverse
map (replacement -> original) would lose data if two originals shared a replacement.
The code nudges collisions by +1 on the green channel
(`ThemeEngine.cs:1997-2019`).

### Custom Theme Generation

`GenerateCustomTheme(accent, bg)` (`ThemeEngine.cs:1778-1920`):

From two user-chosen colors (accent + background), generates a full 55-key palette:
- Accent variations: 3 lighter and 3 darker shades via HSL manipulation.
- Accent saturation capped at 0.88, lightness clamped to [0.02, 0.65].
- Backgrounds use the BG color's own hue with saturation clamped to [0.06, 0.35] and
  lightness clamped to [0.03, 0.18]. Four levels with increasing lightness steps.
- Text color is hue-tinted via `DeriveTextColorTinted()` for warmth.
- Control fills, strokes, buttons, ListBox items, toggles, scrollbar, and text controls
  all derive from the accent with varying alpha values.

`GenerateCustomTreeColorMap(accent, bg)` (`ThemeEngine.cs:1928-2022`):

Maps Root's ~25 original ARGB colors to custom-derived equivalents. Backgrounds use
the BG hue; borders use the accent hue for a dual-tone effect.

### Live Preview

`UpdateCustomThemeLive(accentHex, bgHex)` (`ThemeEngine.cs:170-359`):

Lightweight update for color picker drag, throttled to 16ms (~60fps). Skips
`RevertTheme()` and audits. Steps:

1. Update `Styles[0].Resources` in-place.
2. Replace injected `MergedDictionary` contents.
3. Build combined color map with cross-mappings from:
   - Previous `_activeColorMap` replacements.
   - `_rootOriginals` for stale theme colors.
   - Uprooted's own UI element colors (`ContentPages` statics).
   - Raw bg/accent values for page backgrounds.
4. Immediate tree walk with brush caching (`_liveBrushCache`) to avoid recreating
   identical brushes for controls sharing the same color.
5. Update DWM title bar.

### Revert Mechanism

`RevertTheme()` (`ThemeEngine.cs:1174-1320`):

1. Cancel walk timer and disable layout interceptor.
2. **Phase 1: Restore resources** -- Restore saved originals in `Styles[0].Resources`;
   remove keys that were added (had no original).
3. Remove injected `MergedDictionary`.
4. **Phase 2: Targeted purge** -- `PurgeKnownColors()` (`ThemeEngine.cs:1365-1436`)
   walks the tree and calls `ClearValue` only on controls whose current color is in the
   known set (both originals and replacements). If `ClearValue` leaves the property
   null, falls back to `_rootOriginals` for the true original color.
5. Restore default DWM title bar color.

### DWM Title Bar Color

(`ThemeEngine.cs:54-113`)

On Windows, calls `DwmSetWindowAttribute(hwnd, DWMWA_CAPTION_COLOR, colorRef)` to set
the title bar color. Gets the HWND via `TopLevel.TryGetPlatformHandle().Handle`.
Converts `#RRGGBB` to COLORREF format (`0x00BBGGRR`).

### Diagnostic Methods

- **`DumpVisualTreeColors()`** (`ThemeEngine.cs:1496-1545`) -- Scans all TopLevels and
  logs the top 80 color/property combinations by frequency.
- **`DumpVisualTreeStructure()`** (`ThemeEngine.cs:1616-1624`) -- Logs nested tree
  structure with type names, child counts, and background colors.
- **`DumpResourceKeys()`** (`ThemeEngine.cs:1676-1729`) -- Dumps all keys from
  `Styles[0].Resources`, `Application.Resources`, and `MergedDictionaries`.
- **`RunColorAudit()`** (`ThemeEngine.cs:745-826`) -- Runs 1.5 seconds after theme
  apply; reports stale original colors that survived the walk.

### Public API

- `ApplyTheme(name)` -- Apply a named preset (crimson, loki).
- `ApplyCustomTheme(accentHex, bgHex)` -- Apply a custom theme.
- `UpdateCustomThemeLive(accentHex, bgHex)` -- Live preview update.
- `RevertTheme()` -- Restore original Root colors.
- `WalkVisualTreeNow()` -- Trigger an immediate walk.
- `ScheduleWalkBurst()` -- Immediate + follow-up walks.
- `SetCustomPingColor(hex)` -- Set a custom ping/reply highlight color override (persists across theme switches).
- `ClearCustomPingColor()` -- Clear the override and restore theme defaults.
- `GetCustomPingColor()` -- Get the current custom ping color (null if not set).
- `ActiveThemeName` -- Current theme name (null if reverted).
- `GetAccentColor()` -- Current accent hex.
- `GetBgPrimary()` -- Current background hex.
- `GetPalette()` -- Current full palette dictionary.

For full algorithm documentation including live preview, tree fingerprinting, color
audit, custom generation, and revert mechanics, see
[Theme Engine Deep Dive](THEME_ENGINE_DEEP_DIVE.md).

---

## Color Utilities

### ColorUtils.cs

**File:** `hook/ColorUtils.cs` (262 lines)

All methods work with `"#RRGGBB"` hex strings (6-digit with hash prefix). Methods that
receive `#AARRGGBB` format strip the alpha prefix automatically.

**Validation:**
- `IsValidHex(hex)` (`ColorUtils.cs:13-14`) -- Regex check for `#[0-9A-Fa-f]{6}`.

**Parsing / Formatting:**
- `ParseHex(hex)` (`ColorUtils.cs:16-25`) -- Returns `(byte R, byte G, byte B)`.
- `ToHex(r, g, b)` (`ColorUtils.cs:27-28`) -- Returns `"#RRGGBB"`.

**Color Manipulation:**
- `Darken(hex, percent)` (`ColorUtils.cs:33-42`) -- Multiplies channels by
  `(1 - pct/100)`.
- `Lighten(hex, percent)` (`ColorUtils.cs:48-57`) -- Blends toward white:
  `channel + (255 - channel) * pct/100`.
- `WithAlpha(hex, alpha)` (`ColorUtils.cs:63-68`) -- Prepends alpha byte:
  `"#AARRGGBB"` (Avalonia convention).
- `WithAlphaFraction(hex, alpha)` (`ColorUtils.cs:129-130`) -- Alpha as float 0.0-1.0.
- `Mix(hexA, hexB, ratio)` (`ColorUtils.cs:114-124`) -- Linear interpolation.
- `HueShift(hex, hueDelta, satMul, litDelta)` (`ColorUtils.cs:188-192`) -- HSL shift.
- `TintWithHue(accentHex, saturation, lightness)` (`ColorUtils.cs:199-202`) --
  Creates a color with the accent's hue at specific S/L.

**Luminance:**
- `Luminance(hex)` (`ColorUtils.cs:73-80`) -- WCAG 2.0 relative luminance.
- `DeriveTextColor(bgHex)` (`ColorUtils.cs:88-89`) -- Near-white for dark backgrounds,
  near-black for light.
- `DeriveTextColorTinted(bgHex, accentHex)` (`ColorUtils.cs:95-109`) -- Text color
  tinted with the accent hue for warmth. On dark backgrounds: near-white with 3-12%
  accent saturation; on light: near-black with up to 10% accent saturation.

**HSL Conversion:**
- `RgbToHsl(hex)` (`ColorUtils.cs:135-156`) -- H: 0-360, S: 0-1, L: 0-1.
- `HslToHex(h, s, l)` (`ColorUtils.cs:161-183`) -- Standard HSL-to-RGB formula.

**HSV Conversion:**
- `RgbToHsv(hex)` (`ColorUtils.cs:207-228`) -- H: 0-360, S: 0-1, V: 0-1.
- `HsvToHex(h, s, v)` (`ColorUtils.cs:233-255`) -- Standard HSV-to-RGB formula.
- `PureHueHex(hue)` (`ColorUtils.cs:260-261`) -- Full saturation/value for a hue.

### ColorPickerPopup.cs

**File:** `hook/ColorPickerPopup.cs` (533 lines)

A Discord-style HSV color picker displayed on the Avalonia OverlayLayer. Singleton
pattern -- only one popup can be open at a time.

**Layout Constants:**
- SV area: 262x180 pixels
- Hue slider: 12px height with 8x18 pill thumb
- Popup padding: 16px

**Components:**
1. **Saturation-Value gradient area** (`BuildSVArea`, `ColorPickerPopup.cs:194-283`):
   - Layer 0: Solid pure hue background.
   - Layer 1: White-to-transparent horizontal gradient (saturation).
   - Layer 2: Transparent-to-black vertical gradient (value).
   - Layer 3: Ellipse cursor indicator with white border.
   - PointerPressed/Moved/Released events for drag interaction.

2. **Hue slider** (`BuildHueSlider`, `ColorPickerPopup.cs:287-363`):
   - Rainbow gradient with 7 stops (ROYGBIV cycle).
   - Pill-shaped white thumb.
   - Drag events for hue selection.

3. **Hex input row** (`BuildHexRow`, `ColorPickerPopup.cs:367-429`):
   - Preview swatch showing current color.
   - "#" label + TextBox for hex input (bidirectional sync).

**Interaction:**
- `HandleSVInput(r, x, y)` (`ColorPickerPopup.cs:433-439`) -- Clamps x/y to [0,1]
  for saturation/value.
- `HandleHueInput(r, x)` (`ColorPickerPopup.cs:441-446`) -- Maps x position to
  0-360 hue.
- `UpdateAllVisuals(r)` (`ColorPickerPopup.cs:450-495`) -- Updates SV base color,
  cursor position, hue thumb position, preview swatch, and hex text.
- `UpdateLinkedTextBox(r)` (`ColorPickerPopup.cs:497-506`) -- Sets the external
  TextBox text and fires the live `_onColorChanged` callback.

**Positioning** (`ColorPickerPopup.cs:100-118`): Popup appears to the right of the
swatch, or to the left if near the window edge. Vertically centered on the swatch.

---

## HTML Patch Verifier

**File:** `hook/HtmlPatchVerifier.cs` (429 lines)

### Purpose

Root's DotNetBrowser Chromium loads HTML files from disk. Uprooted injects `<script>`
and `<link>` tags into these files to load the TypeScript layer. The patch verifier
ensures these tags survive Root auto-updates.

See [TypeScript Reference](TYPESCRIPT_REFERENCE.md) for the browser-side injection.

### Markers

```csharp
private const string MarkerStart = "<!-- uprooted:start -->";
private const string MarkerEnd = "<!-- uprooted:end -->";
private const string LegacyMarker = "<!-- uprooted -->";
private const string PreloadMarker = "uprooted-preload";
```

(`HtmlPatchVerifier.cs:10-13`)

The injection block is wrapped in start/end markers for clean identification and removal.

### Phase 0 Verification

`VerifyAndRepair()` (`HtmlPatchVerifier.cs:34-72`):

1. Calls `FindTargetHtmlFiles()` (`HtmlPatchVerifier.cs:316-335`) to discover:
   - `{profileDir}/WebRtcBundle/index.html`
   - `{profileDir}/RootApps/*/index.html`
2. For each file, reads content and checks `IsPatched()` (`HtmlPatchVerifier.cs:309-314`)
   which looks for any of the three markers.
3. If not patched, calls `PatchFile()` (`HtmlPatchVerifier.cs:173-200`):
   - Strips existing injection via `StripExistingInjection()` (`HtmlPatchVerifier.cs:270-307`).
   - Creates a backup (`.uprooted.bak`) if none exists.
   - Builds the injection block via `BuildInjectionBlock()` (`HtmlPatchVerifier.cs:203-217`).
   - Inserts before `</head>`.

### Injection Block

```html
    <!-- uprooted:start -->
    <script>window.__UPROOTED_SETTINGS__={...};</script>
    <script src="file:///path/to/uprooted-preload.js"></script>
    <link rel="stylesheet" href="file:///path/to/uprooted.css">
    <!-- uprooted:end -->
```

The settings JSON is built without `System.Text.Json` (`BuildSettingsJson`,
`HtmlPatchVerifier.cs:219-242`). It first tries to read `uprooted-settings.json`
raw, falling back to manual JSON construction with `EscapeJsonString()`
(`HtmlPatchVerifier.cs:247-265`).

### FileSystemWatcher

`StartWatching()` (`HtmlPatchVerifier.cs:77-98`):

Creates watchers on:
- `{profileDir}/WebRtcBundle/` -- filter: `index.html`
- Each `{profileDir}/RootApps/{appDir}/` -- filter: `index.html`

Watchers monitor `LastWrite`, `Size`, and `CreationTime` notifications.

### Debounce and Cooldown

(`HtmlPatchVerifier.cs:125-171`)

- **Debounce**: File change events trigger a 1-second `Timer`. Multiple rapid events
  reset the timer, so only the last one fires.
- **Cooldown**: After patching, a 2-second cooldown (`PatchCooldown`) prevents
  self-triggering (our own write triggers the watcher).
- **Single-entry guard**: `Interlocked.CompareExchange` on `_patchGuard` prevents
  concurrent patch operations.

### Strip Logic

`StripExistingInjection()` (`HtmlPatchVerifier.cs:270-307`) processes line-by-line:
- Removes everything between `MarkerStart` and `MarkerEnd`.
- Strips lines containing `LegacyMarker`.
- Strips bare `uprooted-preload` script tags and `uprooted.css` link tags (from the
  bash installer which doesn't use markers).
- Strips `__UPROOTED_SETTINGS__` script tags.

---

## Settings

**File:** `hook/UprootedSettings.cs` (161 lines)

### Format

Settings use a simple INI format (not JSON). This is a deliberate design choice:
**`System.Text.Json` is forbidden in the profiler context** because it causes
`MissingMethodException` when called from code injected via the CLR profiler.

### Properties

```csharp
public bool Enabled { get; set; } = true;
public string Version { get; set; } = "0.4.0";
public string ActiveTheme { get; set; } = "default-dark";
public Dictionary<string, bool> Plugins { get; set; } = new();
public string CustomCss { get; set; } = "";
public string CustomAccent { get; set; } = "#3B6AF8";
public string CustomBackground { get; set; } = "#0D1521";
public string CustomPingColor { get; set; } = "";  // Empty = use theme default
// MessageLogger settings
public bool MessageLoggerLogDeletes { get; set; } = true;
public bool MessageLoggerLogEdits { get; set; } = true;
public bool MessageLoggerIgnoreSelf { get; set; } = false;
public int MessageLoggerMaxMessages { get; set; } = 1000;
// AutoUpdate settings
public bool AutoUpdateEnabled { get; set; } = true;
public bool AutoUpdateNotify { get; set; } = true;
public string AutoUpdateChannel { get; set; } = "stable";
public string AutoUpdateLastCheck { get; set; } = "";
```

### File Location

```
{LocalAppData}/Root Communications/Root/profile/default/uprooted-settings.ini
```

Resolved via `PlatformPaths.GetProfileDir()` (`UprootedSettings.cs:20-21`).

### Load

`UprootedSettings.Load()` (`UprootedSettings.cs:30-63`):

Reads lines, splits on `=`, and maps keys:
- `ActiveTheme`, `Enabled`, `CustomCss`, `CustomAccent`, `CustomBackground`, `CustomPingColor` -- direct properties.
- `MessageLogger.*` -- MessageLogger settings (LogDeletes, LogEdits, IgnoreSelf, MaxMessages).
- `AutoUpdate.*` -- AutoUpdater settings (Enabled, Notify, Channel, LastCheck).
- `Plugin.{name}` -- entries in the `Plugins` dictionary.

Returns a default instance if the file doesn't exist.

### Save

`UprootedSettings.Save()` (`UprootedSettings.cs:65-90`):

Writes all properties as `Key=Value` lines. Plugin entries are prefixed with `Plugin.`.

### INI File Format Example

```ini
ActiveTheme=crimson
Enabled=true
Version=0.4.0
CustomCss=
CustomAccent=#3B6AF8
CustomBackground=#0D1521
CustomPingColor=#FF0000
MessageLogger.LogDeletes=true
MessageLogger.LogEdits=true
MessageLogger.IgnoreSelf=false
MessageLogger.MaxMessages=1000
AutoUpdate.Enabled=true
AutoUpdate.Notify=true
AutoUpdate.Channel=stable
Plugin.sentry-blocker=true
Plugin.themes=true
```

---

## Infrastructure

### Logger.cs

**File:** `hook/Logger.cs` (46 lines)

Thread-safe file logger. Writes to `{profileDir}/uprooted-hook.log`.

```csharp
internal static class Logger
{
    private static readonly string LogPath;
    private static readonly object Lock = new();

    static Logger()
    {
        var profileDir = PlatformPaths.GetProfileDir();
        Directory.CreateDirectory(profileDir);
        LogPath = Path.Combine(profileDir, "uprooted-hook.log");
    }

    internal static void Log(string message)
    {
        lock (Lock)
        {
            File.AppendAllText(LogPath, $"[{DateTime.Now:HH:mm:ss.fff}] {message}\n");
        }
    }

    internal static void Log(string category, string message) => Log($"[{category}] {message}");
}
```

**Key details:**
- **Thread safety**: All writes are inside `lock (Lock)`.
- **Format**: `[HH:mm:ss.fff] [Category] Message`
- **No rotation**: The log file grows unbounded. Manual deletion is needed.
- **Fail-silent**: The `try/catch` around `File.AppendAllText` swallows all exceptions
  to prevent logging failures from crashing the hook.

### PlatformPaths.cs

**File:** `hook/PlatformPaths.cs` (29 lines)

Cross-platform path resolution for Root's profile directory and Uprooted's asset
directory.

```csharp
internal static string GetProfileDir()
```

Returns: `{LocalAppData}/Root Communications/Root/profile/default`

```csharp
internal static string GetUprootedDir()
```

Returns:
- **Windows**: `{LocalAppData}/Root/uprooted/`
- **Linux**: `~/.local/share/uprooted/`

---

## Dependency Map

```
                        +-------------------+
                        |    Entry.cs       |
                        | NativeEntry.cs    |
                        +--------+----------+
                                 |
                                 v
                        +-------------------+
                        |  StartupHook.cs   |
                        +--+---+------+-----+
                           |   |      |
              +------------+   |      +-------------+
              |                |                     |
              v                v                     v
  +-----------------+  +-------------------+  +------------------------+
  |HtmlPatchVerifier|  |AvaloniaReflection |  |DotNetBrowserReflection |
  +-----------------+  +--------+----------+  +----------+-------------+
  | PlatformPaths   |          |                         |
  | UprootedSettings|  +-------+--------+       +--------+--------+
  +-----------------+  |                |       |                 |
                       v                v       v                 v
            +------------------+ +-----------+ +---------------+ +---------------+
            |VisualTreeWalker  | |ThemeEngine| |LinkEmbedInjctr| |  NsfwFilter   |
            +--------+---------+ +--+--+-----+ +---------------+ +---------------+
                     |              |  |
                     v              |  v
            +------------------+   | +----------+
            | SidebarInjector  |<--+ |ColorUtils|
            +--------+---------+     +----------+
                     |                     ^
                     v                     |
            +------------------+   +-------+--------+
            | ContentPages     |-->|ColorPickerPopup|
            +------------------+   +----------------+

    +------------------+
    |BrowserDiscovery  |  (Phase 4.5 diagnostic, depends on AvaloniaReflection)
    +------------------+

    Cross-cutting dependencies (used by most files):
    +----------+    +----------------+    +-------------------+
    | Logger   |    | PlatformPaths  |    | UprootedSettings  |
    +----------+    +----------------+    +-------------------+
```

**Dependency details:**

| Source | Depends On | Relationship |
|--------|-----------|-------------|
| `Entry.cs` | `StartupHook`, `Logger` | Calls `Initialize()` |
| `NativeEntry.cs` | `StartupHook`, `Logger` | Calls `Initialize()` |
| `StartupHook.cs` | `HtmlPatchVerifier`, `AvaloniaReflection`, `VisualTreeWalker`, `SidebarInjector`, `ThemeEngine`, `DotNetBrowserReflection`, `BrowserDiscovery`, `NsfwFilter`, `LinkEmbedEngine`, `UprootedSettings`, `Logger` | Orchestrates all phases (0-5) |
| `AvaloniaReflection.cs` | `Logger` | Logs resolution results |
| `VisualTreeWalker.cs` | `AvaloniaReflection`, `Logger` | Uses reflection for tree traversal |
| `SidebarInjector.cs` | `AvaloniaReflection`, `VisualTreeWalker`, `ContentPages`, `ThemeEngine`, `UprootedSettings`, `Logger` | Orchestrates injection + content |
| `ContentPages.cs` | `AvaloniaReflection`, `ColorUtils`, `ColorPickerPopup`, `ThemeEngine`, `UprootedSettings`, `Logger` | Builds page UI |
| `ThemeEngine.cs` | `AvaloniaReflection`, `ColorUtils`, `ContentPages`, `Logger` | Theme resource + tree color management |
| `ColorPickerPopup.cs` | `AvaloniaReflection`, `ColorUtils` | HSV picker UI |
| `ColorUtils.cs` | (none) | Pure utility functions |
| `HtmlPatchVerifier.cs` | `PlatformPaths`, `UprootedSettings`, `Logger` | HTML patch + watch |
| `DotNetBrowserReflection.cs` | `AvaloniaReflection`, `Logger` | DotNetBrowser type resolution + IBrowser discovery |
| `BrowserDiscovery.cs` | `AvaloniaReflection`, `Logger` | Diagnostic visual tree + assembly dump |
| `LinkEmbedEngine.cs` | `AvaloniaReflection`, `VisualTreeWalker`, `ContentPages`, `ColorUtils`, `UprootedSettings`, `Logger` | Avalonia-native link embed engine |
| `NsfwFilter.cs` | `DotNetBrowserReflection`, `Logger` | JS injection into DotNetBrowser |
| `UprootedSettings.cs` | `PlatformPaths`, `Logger` | INI persistence |
| `Logger.cs` | `PlatformPaths` | File path for log |
| `PlatformPaths.cs` | (none) | Pure path resolution |

---

**Canonical for:** all 24 C# class implementations, startup phase detail (Phase 0–5), entry points, version migration, sidebar injection, content pages, theme engine overview, settings INI format, dependency map, LinkEmbedEngine, ClearUrlsEngine, AutoUpdater, MessageLogger, ProfileBadgeInjector, SilentTypingEngine, NsfwFilter
**Not canonical for:** architecture overview → [ARCHITECTURE.md](ARCHITECTURE.md) | Avalonia reflection patterns → [AVALONIA_PATTERNS.md](AVALONIA_PATTERNS.md) | theme algorithm deep dive → [THEME_ENGINE_DEEP_DIVE.md](THEME_ENGINE_DEEP_DIVE.md)
*Hook reference for Uprooted v0.4.0. Last updated 2026-02-19.*
