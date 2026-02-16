# Codebase Concerns

**Analysis Date:** 2026-02-16

## Tech Debt

**Weak Type Safety - Non-Assertion Casts:**
- Issue: Multiple instances of unsafe type assertions using non-null (`!`) operator without runtime validation, particularly in fetch wrapping
- Files: `src/plugins/sentry-blocker/index.ts` (lines 45, 59, 61, 72), `src/core/pluginLoader.ts` (line 144)
- Impact: If original fetch/XMLHttpRequest are somehow undefined at invocation, code crashes with unclear errors instead of graceful fallback. The `originalFetch!.call()` pattern is risky because `originalFetch` could theoretically be null if `stop()` is called while requests are in-flight
- Fix approach: Add runtime guards and null checks before using non-null assertions. Cache originals immediately upon `start()` and verify they exist before use

**Type Safety Gaps in sentry-blocker:**
- Issue: Using `any[]` for XMLHttpRequest rest parameters in `src/plugins/sentry-blocker/index.ts` line 53
- Files: `src/plugins/sentry-blocker/index.ts`
- Impact: Loses type information for advanced XMLHttpRequest.open() overloads, could miss errors when passing invalid arguments
- Fix approach: Type rest parameters as `[async?: boolean, username?: string, password?: string]` per MDN spec

**Settings Mutation Concerns:**
- Issue: Settings loading in `src/core/settings.ts` merges loaded settings with defaults using object spread, but merging is shallow - nested `plugins` object may not merge correctly if user settings only contain some plugin configs
- Files: `src/core/settings.ts` (line 27)
- Impact: If installed settings have `plugins: { "theme": { enabled: false } }` and defaults have a different plugin, the user's entry might override all defaults at plugin level
- Fix approach: Implement deep merge for `plugins` object in settings merge logic

## Known Bugs

**Settings Panel Selector Fragility:**
- Symptoms: Settings panel injection fails silently if DOM structure changes, especially if "APP SETTINGS" or "Advanced" text nodes move or change
- Files: `src/plugins/settings-panel/panel.ts` (lines 119-130)
- Trigger: Root Communications app updates that change settings page UI
- Workaround: The DOM discovery has a fallback mechanism using sibling size detection (lines 208-227), but this is unreliable for complex layouts

**XHR Redirection to about:blank:**
- Symptoms: Sentry XHR requests get redirected to `about:blank` which causes browser warnings and leaves XHR in odd state
- Files: `src/plugins/sentry-blocker/index.ts` (line 59)
- Trigger: Any code calling XMLHttpRequest to sentry.io
- Workaround: Returns modified request to `about:blank` which technically succeeds but is semantically wrong

**MutationObserver Debounce Timing:**
- Symptoms: Settings panel injection might be attempted multiple times rapidly on DOM changes, causing lag
- Files: `src/plugins/settings-panel/panel.ts` (lines 56-57)
- Trigger: Rapid DOM mutations during page load
- Workaround: Uses 80ms debounce, but if Root's settings page has expensive re-renders during load, this could still fire repeatedly

## Security Considerations

**Sentry Privacy Protection - Incomplete:**
- Risk: While sentry-blocker intercepts fetch/XHR/sendBeacon, Sentry SDK can also send data via other channels (WebSocket, image beacons)
- Files: `src/plugins/sentry-blocker/index.ts`
- Current mitigation: Blocks the three main transport channels that Sentry.io SDK uses
- Recommendations:
  - Monitor for Sentry SDK updates that introduce new transport mechanisms
  - Consider intercepting at the Sentry.init() level if possible
  - Add logging when sentry-blocker stops to show final blocked count

**HTML Injection Marker-Based Patching:**
- Risk: Patcher relies on simple HTML comment markers (`<!-- uprooted -->`) to detect already-injected state. If Root's build process changes or user manually removes marker, could cause double-injection
- Files: `src/core/patcher.ts` (lines 18, 63, 97)
- Current mitigation: Checks for marker before injecting, creates backups
- Recommendations:
  - Use more robust detection (e.g., check for actual script tag existence)
  - Consider adding hash-based integrity check to detect manual modifications
  - Test edge case of corrupted/incomplete previous injection

**Window Global Exposure:**
- Risk: Exposing `window.__UPROOTED_LOADER__` (line 43 in preload.ts) and `window.__UPROOTED_SETTINGS__` makes internal state accessible to any script on page
- Files: `src/core/preload.ts` (lines 34, 43)
- Current mitigation: Settings are JSON-serialized, loader reference is kept but usage is guarded
- Recommendations:
  - Restrict loader access to plugin management only (not direct bridge interception)
  - Consider sealing/freezing settings object to prevent tampering
  - Add content security policy awareness

## Performance Bottlenecks

**Settings Panel DOM Discovery:**
- Problem: Text content matching with TreeWalker is inefficient and fires on every MutationObserver event (debounced to 80ms)
- Files: `src/plugins/settings-panel/panel.ts` (lines 77-102, 170-228)
- Cause: Uses `document.createTreeWalker()` with full document traversal for text matching instead of more targeted queries
- Improvement path:
  - Cache discovered elements after first successful injection
  - Use attribute-based selectors if Root marks settings page elements
  - Consider observer on specific subtree instead of entire body

**Theme Plugin Cleanup Duplication:**
- Problem: Variable names enumerated twice (once in themes.json analysis, once hardcoded in custom variables list)
- Files: `src/plugins/themes/index.ts` (lines 22-44)
- Cause: Maintains two separate lists of CSS variable names for cleanup
- Improvement path:
  - Single source of truth for variable names (move to constants or configuration)
  - Pre-compute cleanup set once at module load, not on every theme switch

**Circular MutationObserver in Settings Panel:**
- Problem: Settings panel DOM changes trigger MutationObserver callbacks which might inject more DOM, causing more mutations
- Files: `src/plugins/settings-panel/panel.ts` (lines 55-59)
- Cause: Observer watches entire body for changes including its own injections
- Improvement path:
  - Observer should only watch the sidebar element, not entire body
  - Add guard to skip observer callback if mutation is in data-uprooted elements

## Fragile Areas

**Plugin Lifecycle - Async Start/Stop:**
- Files: `src/core/pluginLoader.ts` (lines 40-70, 73-93)
- Why fragile: `start()` and `stop()` are async but there's no protection against rapid start/stop calls or concurrent operations. Calling `start(name)` immediately after `stop(name)` could result in race condition
- Safe modification: Add a `starting` / `stopping` Set to track in-progress operations, queue or reject concurrent calls
- Test coverage: No tests visible for concurrent plugin lifecycle operations

**Bridge Proxy Deferred Installation:**
- Files: `src/api/bridge.ts` (lines 76-102)
- Why fragile: Uses Object.defineProperty to defer proxy installation if globals don't exist yet. Complex interaction with descriptors could fail if Root modifies these properties unexpectedly
- Safe modification: Add try/catch around defineProperty calls; log all defineProperty failures
- Test coverage: Hard to test without Root's actual runtime behavior

**Settings Panel Sidebar Detection:**
- Files: `src/plugins/settings-panel/panel.ts` (lines 170-228)
- Why fragile: Multiple fallback mechanisms for finding layout (flex-row, grid, sibling size detection). If Root changes both primary and all fallback selectors, injection fails silently
- Safe modification: Add explicit error states beyond "not on settings page" - distinguish between "not on settings page" vs "on settings page but structure unrecognized"
- Test coverage: No test fixtures for different Root UI layouts

**Settings File Merge Logic:**
- Files: `src/core/settings.ts` (line 27)
- Why fragile: Shallow object spread merge means nested objects get completely replaced, not merged. If user has plugin A disabled and defaults have plugin B enabled, user settings will overwrite both
- Safe modification: Implement recursive merge function that combines plugin configs
- Test coverage: No tests for settings loading/merging

## Scaling Limits

**DOM TreeWalker in High-Mutation Environments:**
- Current capacity: TreeWalker can efficiently scan ~5000 DOM elements
- Limit: If Root's DOM exceeds 10,000+ elements or settings page is rendered multiple times, TreeWalker searches become perceptible lag
- Scaling path: Implement caching after first discovery, or add fallback to attribute selectors if text matching fails

**Global Event Handlers Memory:**
- Current capacity: Sidebar listener (line 344 in panel.ts) and all item click listeners accumulate without cleanup
- Limit: If settings panel is opened/closed many times, event listeners leak (only cleaned up via global cleanup function)
- Scaling path: Use event delegation instead of attaching listeners to each item; properly clean up listeners in `stopObserving()`

## Dependencies at Risk

**No External Dependencies - Single Points of Failure:**
- Risk: Codebase has no npm dependencies (only dev tools), which is good for security but means zero buffering for Root API changes. If Root's bridge interface changes, all code breaks
- Impact: Any Root update that changes `__nativeToWebRtc` or `__webRtcToNative` signature breaks Uprooted
- Migration plan:
  - Maintain version compatibility matrix (which Root versions this Uprooted version supports)
  - Add bridge interface version detection/negotiation
  - Consider semver-based compatibility layer

**TypeScript and Node Version Pinning:**
- Risk: `package.json` specifies `typescript: ^5.7.0` which allows minor updates. If minor TypeScript version introduces new type checking, builds could fail
- Impact: CI/CD could break unexpectedly on minor version bumps
- Migration plan: Pin exact versions for build tools (`typescript: 5.7.0` not `^5.7.0`)

## Missing Critical Features

**No Settings Persistence Across Sessions:**
- Problem: Plugin enable/disable state and theme changes are session-only (resets on Root restart)
- Blocks: Long-term theme/plugin preferences, user must reconfigure every session
- Current workaround: Patcher can set defaults, but no UI-based persistence mechanism

**No Plugin Configuration Storage:**
- Problem: Plugin settings (defined in `settings?: SettingsDefinition`) have no persistence mechanism
- Blocks: Plugins cannot store user-configured settings beyond current session
- Workaround: Plugins must implement their own localStorage/file storage if needed

**No Error Recovery in Plugin Lifecycle:**
- Problem: If `plugin.start()` rejects, error is logged but plugin is marked active anyway (line 65 in pluginLoader.ts)
- Blocks: Plugin state becomes inconsistent if start fails partway through
- Workaround: None - manually call `stop()` to attempt recovery

## Test Coverage Gaps

**Bridge Proxy Interception:**
- What's not tested: Bridge method intercept, cancellation, return value replacement
- Files: `src/api/bridge.ts`, `src/core/pluginLoader.ts`
- Risk: Patches might not apply correctly to bridge calls if proxy has subtle bugs
- Priority: High - bridge interception is core functionality

**Settings Panel DOM Discovery:**
- What's not tested: Settings page layout detection with various DOM structures
- Files: `src/plugins/settings-panel/panel.ts`
- Risk: Settings panel fails silently on layout changes, users can't access config
- Priority: High - causes UI unavailability

**Plugin Lifecycle Concurrency:**
- What's not tested: Rapid start/stop calls, concurrent plugin operations
- Files: `src/core/pluginLoader.ts`
- Risk: Race conditions in plugin state management
- Priority: Medium - depends on user behavior patterns

**Settings Merge/Load:**
- What's not tested: Merging partial user settings with defaults, corrupted settings files
- Files: `src/core/settings.ts`
- Risk: Settings corruption or unexpected behavior when upgrading Uprooted versions
- Priority: Medium - affects reliability across version upgrades

**Sentry Blocker Edge Cases:**
- What's not tested: Concurrent fetch requests, redirected Sentry URLs, failures during wrap operations
- Files: `src/plugins/sentry-blocker/index.ts`
- Risk: Sentry URLs slip through or wrapping fails ungracefully
- Priority: Medium - privacy feature must be reliable

---

*Concerns audit: 2026-02-16*
