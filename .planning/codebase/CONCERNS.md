# Codebase Concerns

**Analysis Date:** 2026-02-17

## Tech Debt

**Weak Type Safety - Non-Assertion Casts:**
- Issue: Multiple instances of unsafe type assertions using non-null (`!`) operator without runtime validation, particularly in fetch wrapping
- Files: `src/plugins/sentry-blocker/index.ts` (lines 45, 59, 61, 72), `src/core/pluginLoader.ts` (line 144)
- Impact: If original fetch/XMLHttpRequest are somehow undefined at invocation, code crashes with unclear errors instead of graceful fallback. The `originalFetch!.call()` pattern is risky because `originalFetch` could theoretically be null if `stop()` is called while requests are in-flight
- Fix approach: Add runtime guards and null checks before using non-null assertions. Cache originals immediately upon `start()` and verify they exist before use
- Priority: Medium

**Type Safety Gaps in sentry-blocker:**
- Issue: Using `any[]` for XMLHttpRequest rest parameters in `src/plugins/sentry-blocker/index.ts` line 53
- Files: `src/plugins/sentry-blocker/index.ts`
- Impact: Loses type information for advanced XMLHttpRequest.open() overloads, could miss errors when passing invalid arguments
- Fix approach: Type rest parameters as `[async?: boolean, username?: string, password?: string]` per MDN spec
- Priority: Low

**Settings Mutation Concerns:**
- Issue: Settings loading in `src/core/settings.ts` uses shallow object spread merge (line 27), which means nested `plugins` object may not merge correctly if user settings only contain some plugin configs
- Files: `src/core/settings.ts` (line 27)
- Impact: If installed settings have `plugins: { "theme": { enabled: false } }` and defaults have other plugins, user's entry might override all defaults at plugin level
- Fix approach: Implement deep merge for `plugins` object in settings merge logic (tracked in TASKS.md)
- Priority: Medium

**No Linting or Formatting Tools:**
- Issue: TypeScript layer has no ESLint, Biome, or Prettier configuration
- Files: All `src/` TypeScript files
- Impact: Code style is inconsistent; no automated checks prevent regressions
- Fix approach: Add ESLint with TypeScript ruleset, Prettier for formatting, pre-commit hooks via husky/lefthook
- Priority: Medium

**System.Text.Json Broken in Profiler Context:**
- Issue: `System.Text.Json` causes `MissingMethodException` when used inside the CLR profiler-injected hook, preventing JSON deserialization in the C# layer
- Files: `hook/UprootedSettings.cs`
- Current mitigation: INI-based `UprootedSettings` (fully implemented using key=value format)
- Impact: The C# hook returns hardcoded defaults only — cannot read user preferences from JSON. Native Avalonia UI (themes page, plugins page) cannot reflect actual user configuration beyond what INI can store
- Workaround: Manual INI parsing or loading from a different assembly context (requires .NET upgrade or separate process)
- Priority: High (blocker for C# settings pages to be functional)

## Known Bugs

**Settings Panel Selector Fragility:**
- Symptoms: Settings panel injection fails silently if DOM structure changes, especially if "APP SETTINGS" or "Advanced" text nodes move or change
- Files: `src/plugins/settings-panel/panel.ts` (lines 119-130)
- Trigger: Root Communications app updates that change settings page UI
- Workaround: The DOM discovery has a fallback mechanism using sibling size detection (lines 208-227), but this is unreliable for complex layouts
- Priority: High

**XHR Redirection to about:blank:**
- Symptoms: Sentry XHR requests get redirected to `about:blank` which causes browser warnings and leaves XHR in odd state
- Files: `src/plugins/sentry-blocker/index.ts` (line 59)
- Trigger: Any code calling XMLHttpRequest to sentry.io
- Workaround: Returns modified request to `about:blank` which technically succeeds but is semantically wrong
- Better fix: Create a mock response object instead of redirecting
- Priority: Medium

**MutationObserver Debounce Timing:**
- Symptoms: Settings panel injection might be attempted multiple times rapidly on DOM changes, causing lag
- Files: `src/plugins/settings-panel/panel.ts` (lines 56-57)
- Trigger: Rapid DOM mutations during page load
- Workaround: Uses 80ms debounce, but if Root's settings page has expensive re-renders during load, this could still fire repeatedly
- Fix approach: Scope observer to sidebar element only instead of entire body; add guard to skip callback for `data-uprooted` mutations
- Priority: Low

**Root's Chat UI is Avalonia-Native (Critical Discovery):**
- Symptoms: Link embeds and NSFW filter cannot affect chat because chat is rendered entirely in Avalonia native controls, not in DotNetBrowser
- Files: `hook/LinkEmbedEngine.cs`, `hook/NsfwFilter.cs`, `hook/DotNetBrowserReflection.cs`
- Root cause: Root v0.9.92+ renders chat via Avalonia visual tree (~1647+ nodes), not in Chromium/DotNetBrowser. DotNetBrowser shell page loads at `about:blank` permanently
- Impact: JavaScript injection into DotNetBrowser cannot modify chat content. Avalonia-native approaches required for chat-affecting features
- Current implementation: LinkEmbedEngine and NsfwFilter attempt JS injection; only LinkEmbedEngine has Avalonia-native implementation
- Priority: High (blocks chat-related features)

**Link Embeds Incomplete for Non-YouTube Sites:**
- Symptoms: Link embed engine works for YouTube but fails on Twitter/X, Reddit, and most other sites
- Files: `hook/LinkEmbedEngine.cs` (1260 lines), Phase 4.5b in startup sequence
- Root causes:
  1. User-Agent `Mozilla/5.0 (compatible; Uprooted/0.2)` rejected as bot-like by Twitter/X and others
  2. No direct image URL detection — PDF/image files fed to HTML OG parser which fails
  3. No oEmbed support for non-YouTube sites (Twitter, Reddit, etc.)
  4. Some sites serve no OG tags to bot User-Agents
- Fix approach (tracked in TASKS.md):
  - Better User-Agent string (browser-like, e.g., Chrome on Windows)
  - Check Content-Type before HTML parsing; skip to image-only embed for `.jpg`/`.png`/`.gif`
  - Add oEmbed providers for Twitter (`publish.twitter.com/oembed`), Reddit (`www.reddit.com/oembed`)
  - Content-Type gate on OG fetch
- Priority: High

**NSFW Filter Still Uses DotNetBrowser JS Injection:**
- Symptoms: NSFW filter cannot affect Avalonia-native chat because chat is not in DotNetBrowser
- Files: `hook/NsfwFilter.cs` (305 lines)
- Root cause: Filter uses JS injection into DotNetBrowser iframe, which stays at `about:blank` permanently
- Impact: NSFW filter has no effect on chat messages (which are Avalonia-native)
- Fix approach: Full redesign to intercept image-bearing controls in Avalonia visual tree instead of JS
- Priority: High (tracked in TASKS.md)

## Security Considerations

**Sentry Privacy Protection - Incomplete:**
- Risk: While sentry-blocker intercepts fetch/XHR/sendBeacon, Sentry SDK can also send data via other channels (WebSocket, image beacons)
- Files: `src/plugins/sentry-blocker/index.ts`
- Current mitigation: Blocks the three main transport channels that Sentry.io SDK uses
- Recommendations (from ROADMAP.md):
  - Monitor for Sentry SDK updates that introduce new transport mechanisms
  - Consider intercepting at the Sentry.init() level if possible
  - Add logging when sentry-blocker stops to show final blocked count
  - Expand sentry-blocker transport coverage and consider session replay blocking
- Priority: Medium

**HTML Injection Marker-Based Patching:**
- Risk: Patcher relies on simple HTML comment markers (`<!-- uprooted -->`) to detect already-injected state. If Root's build process changes or user manually removes marker, could cause double-injection
- Files: `hook/HtmlPatchVerifier.cs` (Phase 0 verification)
- Current mitigation: Checks for marker before injecting, creates backups, self-healing via FileSystemWatcher
- Recommendations:
  - Use more robust detection (e.g., check for actual script tag existence)
  - Consider adding hash-based integrity check to detect manual modifications
  - Test edge case of corrupted/incomplete previous injection
- Priority: Medium

**Window Global Exposure:**
- Risk: Exposing `window.__UPROOTED_LOADER__` and `window.__UPROOTED_SETTINGS__` makes internal state accessible to any script on page
- Files: `src/core/preload.ts` (lines 34, 43)
- Current mitigation: Settings are JSON-serialized, loader reference is kept but usage is guarded
- Recommendations (from ROADMAP.md):
  - Restrict loader access to plugin management only (not direct bridge interception)
  - Consider sealing/freezing settings object to prevent tampering via `Object.freeze()`
  - Add content security policy awareness
  - Tracked in TASKS.md as "Freeze window globals"
- Priority: Medium

**Root Security Findings (105 total):**
- Reference: `docs/research/SECURITY_RESEARCH.md` documents 105 security findings in Root Communications v0.9.86 (7 Critical, 24 High, 37 Medium, 20 Low)
- Key findings affecting Uprooted's design:
  - C5: `restart(url)` open redirect via bridge (affects all Uprooted plugins using bridge)
  - H17: Bearer token extractable from process memory (Uprooted runs in same process)
  - M26: Token refresh DoS (user session can be locked out)
  - H4/H5: Sentry leaks PII and credentials (Uprooted blocks via sentry-blocker)
  - H24: SSRF via SetProfilePicture (if Uprooted adds profile-pic handling)
- Implications for Uprooted:
  - Bridge proxy must validate/restrict dangerous URLs
  - Token handling in Uprooted should avoid retaining tokens in closures
  - Log sanitizer needed to scrub token values from console output
- Priority: High (architectural concern)

## Performance Bottlenecks

**Settings Panel DOM Discovery:**
- Problem: Text content matching with TreeWalker is inefficient and fires on every MutationObserver event (debounced to 80ms)
- Files: `src/plugins/settings-panel/panel.ts` (lines 77-102, 170-228)
- Cause: Uses `document.createTreeWalker()` with full document traversal for text matching instead of more targeted queries
- Improvement path:
  - Cache discovered elements after first successful injection
  - Use attribute-based selectors if Root marks settings page elements
  - Consider observer on specific subtree instead of entire body
  - Scope MutationObserver to sidebar only (tracked in TASKS.md)
- Priority: Low

**Theme Plugin Cleanup Duplication:**
- Problem: Variable names enumerated twice (once in themes.json analysis, once hardcoded in custom variables list)
- Files: `src/plugins/themes/index.ts` (lines 22-44)
- Cause: Maintains two separate lists of CSS variable names for cleanup
- Improvement path:
  - Single source of truth for variable names (move to constants or configuration)
  - Pre-compute cleanup set once at module load, not on every theme switch
  - Consolidate into single source of truth (tracked in TASKS.md)
- Priority: Low

**Circular MutationObserver in Settings Panel:**
- Problem: Settings panel DOM changes trigger MutationObserver callbacks which might inject more DOM, causing more mutations
- Files: `src/plugins/settings-panel/panel.ts` (lines 55-59)
- Cause: Observer watches entire body for changes including its own injections
- Improvement path:
  - Observer should only watch the sidebar element, not entire body
  - Add guard to skip observer callback if mutation is in data-uprooted elements
  - Tracked in TASKS.md as "Scope MutationObserver to sidebar"
- Priority: Low

## Fragile Areas

**Plugin Lifecycle - Async Start/Stop:**
- Files: `src/core/pluginLoader.ts` (lines 40-70, 73-93)
- Why fragile: `start()` and `stop()` are async but there's no protection against rapid start/stop calls or concurrent operations. Calling `start(name)` immediately after `stop(name)` could result in race condition
- Safe modification: Add a `starting` / `stopping` Set to track in-progress operations, queue or reject concurrent calls (tracked in TASKS.md and ROADMAP.md)
- Test coverage: No tests visible for concurrent plugin lifecycle operations
- Priority: Medium

**Bridge Proxy Deferred Installation:**
- Files: `src/api/bridge.ts` (lines 76-102)
- Why fragile: Uses Object.defineProperty to defer proxy installation if globals don't exist yet. Complex interaction with descriptors could fail if Root modifies these properties unexpectedly
- Safe modification: Add try/catch around defineProperty calls; log all defineProperty failures
- Test coverage: Hard to test without Root's actual runtime behavior
- Priority: Medium

**Settings Panel Sidebar Detection:**
- Files: `src/plugins/settings-panel/panel.ts` (lines 170-228)
- Why fragile: Multiple fallback mechanisms for finding layout (flex-row, grid, sibling size detection). If Root changes both primary and all fallback selectors, injection fails silently
- Safe modification: Add explicit error states beyond "not on settings page" - distinguish between "not on settings page" vs "on settings page but structure unrecognized"
- Test coverage: No test fixtures for different Root UI layouts
- Priority: Medium

**Settings File Merge Logic:**
- Files: `src/core/settings.ts` (line 27)
- Why fragile: Shallow object spread merge means nested objects get completely replaced, not merged. If user has plugin A disabled and defaults have plugin B enabled, user settings will overwrite both
- Safe modification: Implement recursive merge function that combines plugin configs (tracked in TASKS.md)
- Test coverage: No tests for settings loading/merging
- Priority: Medium

**AvaloniaReflection Brittleness:**
- Files: `hook/AvaloniaReflection.cs` (1943 lines, ~80 cached types)
- Why fragile: The reflection cache assumes specific Avalonia type names, property names, and method signatures. Avalonia version updates could rename or remove any of these
- Safe modification: Add Avalonia version detection and per-feature graceful degradation (tracked in ROADMAP.md)
- Impact: Any Avalonia API change breaks all UI injection — entire C# layer becomes non-functional
- Workaround: Currently tested against Avalonia 11.3.12 (from SESSION_STATE.md v0.3.43)
- Priority: High

**Settings Page Text-Based Detection (Native):**
- Files: `hook/VisualTreeWalker.cs`, `hook/SidebarInjector.cs`
- Why fragile: Sidebar injector finds the settings page by searching for exact text "APP SETTINGS" in the visual tree. If Root renames this text, injection silently fails
- Safe modification: Add structural fallback detection that does not depend on text anchor (tracked in ROADMAP.md Ideas/Backlog)
- Impact: Complete loss of native UI injection with no error visible to user
- Priority: High

**Back Button Freeze Risk:**
- Files: `hook/SidebarInjector.cs`
- Why fragile: If injected controls are still in the visual tree when Root's back button triggers navigation teardown, the app freezes
- Current mitigation: PointerPressed subscription on back button calls `CleanupInjection()` before Root's handler fires
- Safe modification: Verify cleanup runs on all exit paths
- Impact: If the event subscription fails or fires too late, Root freezes
- Priority: Medium

## Scaling Limits

**DOM TreeWalker in High-Mutation Environments:**
- Current capacity: TreeWalker can efficiently scan ~5000 DOM elements
- Limit: If Root's DOM exceeds 10,000+ elements or settings page is rendered multiple times, TreeWalker searches become perceptible lag
- Scaling path: Implement caching after first discovery, or add fallback to attribute selectors if text matching fails
- Priority: Low

**Global Event Handlers Memory:**
- Current capacity: Sidebar listener (line 344 in panel.ts) and all item click listeners accumulate without cleanup
- Limit: If settings panel is opened/closed many times, event listeners leak (only cleaned up via global cleanup function)
- Scaling path: Use event delegation instead of attaching listeners to each item; properly clean up listeners in `stopObserving()` (tracked in TASKS.md)
- Priority: Low

**Visual Tree Walk Frequency:**
- Current capacity: Theme engine schedules continuous 500ms walks + rapid follow-ups (200ms, 500ms, 1000ms) after navigation
- Limit: If Root's visual tree is extremely large (>10,000 nodes), constant walks could cause perceptible lag
- Scaling path: Add configurable walk throttling; increase interval on slower machines
- Priority: Low

## C# Hook Layer Concerns

**Environment Variables Affect ALL .NET Apps:**
- Risk: CLR profiler env vars (`DOTNET_ENABLE_PROFILING`, `DOTNET_PROFILER`, `DOTNET_PROFILER_PATH`) are user-scoped and persistent. They apply to every .NET process the user launches, not just Root
- Files: `installer/src-tauri/src/hook.rs`, install scripts
- Current mitigation: Profiler has a process name guard — checks for "Root" and returns `E_FAIL` for other processes (lines 891-905 in `tools/uprooted_profiler.c`)
- Impact: Other .NET apps incur slight startup overhead from profiler loading/unloading. Both `DOTNET_` and `CORECLR_` prefixes are set for .NET 8/9 compatibility
- Recommendations: Consider process-specific env var injection or use `DOTNET_STARTUP_HOOKS` method instead (tracked in ROADMAP.md, Medium-term Goals)
- Priority: Medium

**C# Theme Switching and Plugin Management are Display-Only:**
- Problem: The native Avalonia Themes page shows themes with "ACTIVE" badges but has no click handlers. The Plugins page lists plugins but cannot toggle them
- Files: `hook/ContentPages.cs`
- Impact: Users cannot manage plugins or themes from the native settings UI
- Blocks: Full native UI functionality for plugin/theme management
- Status: Implementation tracked in TASKS.md ("Plugin toggle functionality", "Theme click handlers")
- Priority: High

## Dependencies at Risk

**No External Dependencies - Single Points of Failure:**
- Risk: Codebase has no npm dependencies (only dev tools), which is good for security but means zero buffering for Root API changes. If Root's bridge interface changes, all code breaks
- Impact: Any Root update that changes `__nativeToWebRtc` or `__webRtcToNative` signature breaks Uprooted
- Migration plan (from ROADMAP.md):
  - Maintain version compatibility matrix (which Root versions this Uprooted version supports)
  - Add bridge interface version detection/negotiation
  - Consider semver-based compatibility layer
  - Tracked as "Bridge version detection" (Medium-term Goals)
- Version tracking: Currently supports Root v0.9.86 - v0.9.92 (active development target, from ROADMAP.md)
- Priority: Medium

**TypeScript and Node Version Pinning:**
- Risk: `package.json` specifies `typescript: ^5.7.0` which allows minor updates. If minor TypeScript version introduces new type checking, builds could fail
- Impact: CI/CD could break unexpectedly on minor version bumps
- Migration plan: Pin exact versions for build tools (`typescript: 5.7.0` not `^5.7.0`)
- Priority: Low

**CLR Profiler DLL Brittleness:**
- Risk: The profiler targeting depends on IL injection into arbitrary methods. If Root's loaded assemblies change (new third-party deps), profiler's target module selection could fail
- Files: `tools/uprooted_profiler.c` (lines 440-624 for module/method selection)
- Current mitigation: Profiler scans all modules and picks the first with `System.Object` TypeRef (typically Sentry.dll)
- Impact: If that assembly is removed from Root's dependencies, profiler has no injection target
- Priority: Low

## Missing Critical Features

**`after` Patch Handler Not Yet Implemented:**
- Problem: The `after` callback is defined in the `Patch` interface but the loader does not invoke it at runtime
- Files: `src/types/plugin.ts`, `src/core/pluginLoader.ts`
- Impact: Plugins defining `after` handlers get no runtime behavior — the handler is silently ignored
- Blocks: Post-execution observation patterns (e.g., logging return values)
- Status: Tracked in TASKS.md and ROADMAP.md (Short-term Goals)
- Priority: Medium

**Browser-Side Settings Not Written Back to Disk:**
- Problem: While settings ARE persisted to `uprooted-settings.json` and inlined into HTML by the patcher, runtime changes made via the browser-side settings panel only update the in-memory `window.__UPROOTED_SETTINGS__` object. The browser layer (running in DotNetBrowser's incognito Chromium) has no file system access to write changes back
- Files: `src/plugins/settings-panel/`, `src/core/settings.ts`
- Impact: Settings changed at runtime (e.g., toggling a plugin) take effect immediately but revert on Root restart
- Note: The installer/CLI CAN write settings to disk. Initial configuration persists. Only runtime UI changes are session-only
- Workaround: None currently. Requires bridge channel for settings persistence (tracked in ROADMAP.md, Long-term Vision as "Full settings bidirectional sync")
- Priority: Medium

**No Error Recovery in Plugin Lifecycle:**
- Problem: If `plugin.start()` rejects, error is logged but plugin is marked active anyway (line 65 in pluginLoader.ts)
- Files: `src/core/pluginLoader.ts`
- Blocks: Plugin state becomes inconsistent if start fails partway through
- Workaround: None - manually call `stop()` to attempt recovery
- Status: Tracked in ROADMAP.md (Medium-term Goals) as "Error recovery in plugin lifecycle"
- Priority: Medium

## Test Coverage Gaps

**No Test Framework at All:**
- Status: TypeScript layer has ZERO test coverage. No test framework, no test files, no test scripts
- Impact: All functionality relies on manual testing; regressions not caught automatically
- Setup needed (from ROADMAP.md):
  - Install Vitest as test runner
  - Adopt co-located test files (`.test.ts` suffix)
  - Set initial coverage targets: 60% statements, 50% branches, 70% functions
  - Add `test`, `test:watch`, `test:coverage` npm scripts
- C# layer: Basic manual test harness + two unit test files (`tests/UprootedTests/`)
- Priority: High

**Bridge Proxy Interception:**
- What's not tested: Bridge method intercept, cancellation, return value replacement
- Files: `src/api/bridge.ts`, `src/core/pluginLoader.ts`
- Risk: Patches might not apply correctly to bridge calls if proxy has subtle bugs
- Unit test targets (from ROADMAP.md):
  - Proxy creation, event interception, call cancellation, return value replacement
  - Deferred setup via `Object.defineProperty`, independent proxying of both bridge objects
- Priority: High

**Settings Panel DOM Discovery:**
- What's not tested: Settings page layout detection with various DOM structures
- Files: `src/plugins/settings-panel/panel.ts`
- Risk: Settings panel fails silently on layout changes, users can't access config
- Integration test: Mock a DOM structure resembling Root's settings page, trigger MutationObserver, verify Uprooted's sidebar items appear
- Priority: High

**Plugin Lifecycle Concurrency:**
- What's not tested: Rapid start/stop calls, concurrent plugin operations
- Files: `src/core/pluginLoader.ts`
- Risk: Race conditions in plugin state management
- Unit test targets (from ROADMAP.md):
  - Registration, start/stop lifecycle, patch installation, event emission, multi-plugin handler chaining, concurrent start/stop race conditions
- Priority: Medium

**Settings Merge/Load:**
- What's not tested: Merging partial user settings with defaults, corrupted settings files
- Files: `src/core/settings.ts`
- Risk: Settings corruption or unexpected behavior when upgrading Uprooted versions
- Unit test targets (from ROADMAP.md):
  - JSON parsing, merge with defaults, deep merge for nested plugin configs, error recovery from corrupted settings files, file path resolution
- Priority: Medium

**Sentry Blocker Edge Cases:**
- What's not tested: Concurrent fetch requests, redirected Sentry URLs, failures during wrap operations
- Files: `src/plugins/sentry-blocker/index.ts`
- Risk: Sentry URLs slip through or wrapping fails ungracefully
- Manual test (from ROADMAP.md): Start blocker, simulate Sentry requests, verify blocked count
- Priority: Medium

**UprootedSettings INI Parsing:**
- What's not tested: Valid files, missing keys, malformed lines, empty files
- Files: `hook/UprootedSettings.cs`
- Risk: Settings corruption if INI parsing has edge cases
- Test targets (from ROADMAP.md): This can be tested without Root's runtime
- Priority: Low

---

*Concerns audit: 2026-02-17*
