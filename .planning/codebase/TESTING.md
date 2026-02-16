# Testing Patterns

**Analysis Date:** 2026-02-16

## Test Framework

**Status:** Not detected

No testing framework is configured or used in this codebase. There are:
- No test files (zero `.test.ts`, `.spec.ts`, or similar files found)
- No test runner configured (`jest.config.ts`, `vitest.config.ts`, etc. not present)
- No testing dependencies in `package.json` (`jest`, `vitest`, `mocha`, `@testing-library`, etc.)
- No test scripts in `package.json` (`test`, `test:watch`, `test:coverage` commands absent)

**Implication:** All code is currently untested. This is a significant gap for production code, particularly:
- Plugin lifecycle management in `src/core/pluginLoader.ts`
- Bridge interception logic in `src/api/bridge.ts`
- Settings persistence in `src/core/settings.ts`
- Error handling in preload initialization

## Manual Testing Evidence

While no automated tests exist, the codebase contains patterns suggesting manual testing approach:

**Logging for verification:**
- Extensive console logging statements to verify operation: `console.log("[Uprooted] Started plugin: ${name}")`
- Plugin lifecycle hooks logged: start, stop, patch installation
- Network blocking counts logged: `[Uprooted:sentry-blocker] Blocked fetch to sentry.io (${blockedCount} total)`

**Error visibility:**
- Fatal errors displayed as on-screen error banner (observed in `src/core/preload.ts`):
  ```typescript
  const banner = document.createElement("div");
  banner.id = "uprooted-error";
  banner.style.cssText = "position:fixed;top:0;left:0;...";
  banner.textContent = `[Uprooted] Fatal: ${err instanceof Error ? err.stack ?? err.message : ...}`;
  ```

**Defensive programming:**
- Early returns on validation failures: `if (!plugin) { console.error(...); return; }`
- Type guards and null checks throughout: `if (settings?.enabled)`, `const plugin = this.plugins.get(name)`
- Graceful fallbacks: Settings loading returns `DEFAULT_SETTINGS` on error rather than throwing

## Test Organization (Not Applicable)

Since no tests exist, the following would be recommended if testing were to be added:

**Recommended Location:**
- Co-located with source: `src/core/__tests__/pluginLoader.test.ts` or `src/core/pluginLoader.test.ts`
- Separate test directory: `tests/core/`, `tests/api/`, `tests/plugins/`

**Recommended Naming:**
- Match source file: `pluginLoader.ts` → `pluginLoader.test.ts`
- Use `.test.ts` extension consistently

## Areas Requiring Testing

**High Priority (Core Functionality):**

1. **`src/core/pluginLoader.ts` - PluginLoader class**
   - Plugin registration: `register()` should prevent duplicates, accept new plugins
   - Plugin lifecycle: `start()` should install patches, inject CSS, call lifecycle hooks
   - Plugin stopping: `stop()` should uninstall patches, remove CSS, clean up
   - Event emission: `emit()` should route events to correct handlers, respect cancellation
   - Multi-plugin scenarios: Handlers from multiple plugins should chain correctly

2. **`src/core/settings.ts` - Settings persistence**
   - Loading settings: `loadSettings()` should parse JSON, merge with defaults on error
   - Saving settings: `saveSettings()` should create directory, write valid JSON
   - Error recovery: Settings should not be lost on parse errors (spread operator merges)
   - File location: Should use correct `PROFILE_DIR` path

3. **`src/api/bridge.ts` - Bridge proxy installation**
   - Proxy creation: `createBridgeProxy()` should wrap functions, emit events
   - Event interception: Plugins can cancel/modify calls before bridge sees them
   - Async bridge setup: Should handle asynchronous bridge availability via `Object.defineProperty`
   - Multiple proxies: Both `__nativeToWebRtc` and `__webRtcToNative` should be independently proxied

4. **`src/plugins/sentry-blocker/index.ts` - Sentry blocking**
   - Fetch blocking: Requests to `*.sentry.io` should return 200 response, others pass through
   - XHR blocking: XMLHttpRequest to Sentry URLs should redirect to `about:blank`
   - SendBeacon blocking: `navigator.sendBeacon` to Sentry URLs should return `true` silently
   - Count tracking: `blockedCount` should increment accurately

**Medium Priority (Integration/Plugin System):**

5. **`src/core/preload.ts` - Initialization sequence**
   - Settings check: Should skip init if settings disabled
   - Plugin registration: Built-in plugins should be registered before `startAll()`
   - Error handling: Fatal errors should display banner, not throw
   - Version availability: `window.__UPROOTED_VERSION__` should be set correctly

6. **`src/plugins/themes/index.ts` - Theme engine**
   - Color math: `darken()`, `lighten()`, `luminance()` produce correct hex values
   - Custom theme generation: `generateCustomVariables()` produces valid CSS variable object
   - Theme switching: CSS variables cleared on stop, applied on start
   - Fallback handling: Invalid theme names should be ignored gracefully

7. **`src/api/css.ts` - CSS injection**
   - Injection: `injectCss()` should create/update style element with correct ID
   - Removal: `removeCss()` should remove specific style, not others
   - Batch removal: `removeAllCss()` should remove all Uprooted-injected styles only
   - ID prefixing: All injected styles prefixed with `uprooted-css-` for isolation

8. **`src/api/dom.ts` - DOM utilities**
   - Element waiting: `waitForElement()` should resolve immediately if element exists
   - Timeout handling: Should reject after timeout with descriptive message
   - Observer cleanup: Should disconnect observer when element found
   - Frame scheduling: `nextFrame()` should use `requestAnimationFrame` correctly

**Low Priority (Edge Cases):**

9. **`src/plugins/settings-panel/components.ts` - UI components**
   - Component creation: Each function should return correct DOM element type
   - Event handling: Toggle, select, textarea changes should trigger callbacks
   - Debouncing: Textarea input should debounce callback by 300ms

10. **Installer pages - `installer/src/pages/`**
    - Page routing: Page switches should initialize only once
    - Detection: Root installation detection should correctly identify paths
    - Installation/uninstallation: Should create backups, inject HTML, restore originals

## Recommended Testing Framework

**Choice:** Vitest
- Reasons:
  - Native ESM support (codebase uses ES modules)
  - Fast in-memory execution
  - Jest-compatible API
  - Minimal configuration needed
  - Vite integration (if build moves to Vite)

**Alternative:** Jest with ESM support
- Still requires `--experimental-modules` flag
- More mature ecosystem
- Heavier than Vitest

## Mocking Strategy (Recommended)

**What to Mock:**

1. **DOM APIs** (for non-DOM tests):
   ```typescript
   const mockElement = document.createElement('div');
   vi.spyOn(document, 'querySelector').mockReturnValue(mockElement);
   ```

2. **File System** (in `settings.ts` tests):
   ```typescript
   vi.mock('node:fs', () => ({
     readFileSync: vi.fn(),
     writeFileSync: vi.fn(),
     existsSync: vi.fn(),
   }));
   ```

3. **Window globals** (in preload and bridge tests):
   ```typescript
   const mockNativeToWebRtc = { method1: vi.fn() };
   window.__nativeToWebRtc = mockNativeToWebRtc;
   ```

4. **Native API calls** (Tauri):
   ```typescript
   vi.mock('@tauri-apps/api/window', () => ({
     getCurrentWindow: vi.fn(() => ({
       minimize: vi.fn(),
       close: vi.fn(),
     })),
   }));
   ```

**What NOT to Mock:**

1. **Plugin registration logic** - Test real plugin registration flow
2. **Event emission chains** - Test actual handler execution order
3. **Color math functions** - Test real darken/lighten calculations against known values
4. **Settings merge logic** - Test actual object spreading behavior

## Test Coverage Goals (Recommended)

If testing were implemented, prioritize:
- **Statements:** 60% minimum (focus on critical paths)
- **Branches:** 50% minimum (error paths, conditional logic)
- **Functions:** 70% minimum (all exported functions covered)
- **Lines:** 60% minimum

Critical uncovered paths would be:
- Plugin lifecycle error scenarios (`start()`, `stop()` try-catch blocks)
- Bridge interception edge cases (deferred proxy setup)
- File system failures in settings (`mkdirSync`, `writeFileSync` errors)

## Run Commands (Recommended if Vitest Added)

```bash
npm run test              # Run all tests once
npm run test:watch       # Watch mode for development
npm run test:coverage    # Generate coverage report
npm run test:ui          # Vitest UI dashboard (optional)
```

## Current Testing Gaps

**No coverage for:**
- Plugin lifecycle state management (which plugins are active, pending, etc.)
- Event handler cleanup and plugin isolation (handlers from stopped plugins still firing)
- Bridge proxy deferred setup (async window property assignment)
- Settings JSON corruption recovery
- CSS style ID collision prevention
- DOM observer cleanup on timeout
- Theme variable name collision with custom CSS
- Installer HTML injection edge cases (missing `</head>` tag, already-patched files)

---

*Testing analysis: 2026-02-16*
