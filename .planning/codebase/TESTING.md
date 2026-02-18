# Testing Patterns

**Analysis Date:** 2026-02-17

## C# Test Framework

**Location:** `tests/UprootedTests/`

**Framework:** xUnit with Coverlet

**Configuration:**
- Project: `tests/UprootedTests/UprootedTests.csproj`
- Target: `.NET 10.0`
- Dependencies: `xUnit 2.9.3`, `xunit.runner.visualstudio 3.1.4`, `coverlet.collector 6.0.4`, `Avalonia 11.2.7`

**Run Command:**
```bash
dotnet test tests/UprootedTests/
```

**Test Files:**
- `tests/UprootedTests/ColorUtilsTests.cs` — Color math (RGB/HSV/hex conversions)
- `tests/UprootedTests/GradientBrushTests.cs` — Gradient brush creation via reflection and direct API

## C# Test Structure

**Pattern: Theory Tests with InlineData**

```csharp
[Theory]
[InlineData("#FF0000", 255, 0, 0)]
[InlineData("#00FF00", 0, 255, 0)]
public void ParseHex_ReturnsCorrectRgb(string hex, byte r, byte g, byte b)
{
    var (pr, pg, pb) = ColorUtils.ParseHex(hex);
    Assert.Equal(r, pr);
    Assert.Equal(g, pg);
    Assert.Equal(b, pb);
}
```

**Pattern: Fact Tests for Single Cases**

```csharp
[Fact]
public void PureHueHex_MatchesHsvToHex()
{
    for (double h = 0; h < 360; h += 30)
    {
        Assert.Equal(ColorUtils.HsvToHex(h, 1.0, 1.0), ColorUtils.PureHueHex(h));
    }
}
```

**Assertion Style:**
- Use `Assert.Equal(expected, actual)` for direct equality
- Use `Assert.Equal(expected, actual, tolerance)` for floating-point tolerance
- Example: `Assert.Equal(expH, h, 1);  // 1 decimal tolerance for hue`

**Source Linking:**
The test project links source files directly from the hook project:
```xml
<Compile Include="..\..\hook\ColorUtils.cs" Link="ColorUtils.cs" />
```

This allows testing hook code without copying or NuGet packaging.

## TypeScript Testing Status

**Status:** No automated test suite

No testing framework is configured or used in the TypeScript/browser injection layer:
- No test files (zero `.test.ts`, `.spec.ts` files)
- No test runner configured (`jest.config.ts`, `vitest.config.ts` absent)
- No testing dependencies in `package.json`
- No test scripts in `package.json`

**Manual Testing Approach:**
TypeScript testing relies on:
1. Build verification: `pnpm build` must complete without errors
2. Deployment testing: `powershell -File scripts/install-hook.ps1` deploys to local Root
3. Runtime verification: Check `uprooted-hook.log` and browser console for `[Uprooted]` messages
4. Error visibility: Fatal errors display on-screen as red banner with full stack trace

## Recommended Testing Framework for TypeScript

**Choice:** Vitest

**Reasons:**
- Native ESM support (codebase uses ES modules)
- Fast in-memory execution (no transpilation overhead)
- Jest-compatible API (minimal learning curve)
- Minimal configuration needed
- Optional Vite integration if build moves to Vite

**Alternative:** Jest with ESM support (more mature ecosystem but heavier than Vitest)

## Testing Patterns Observed (Manual)

### Error Visibility

Fatal errors in `src/core/preload.ts` display as on-screen error banner:
```typescript
const banner = document.createElement("div");
banner.id = "uprooted-error";
banner.style.cssText = "position:fixed;top:0;left:0;...";
banner.textContent = `[Uprooted] Fatal: ${err instanceof Error ? err.stack ?? err.message : ...}`;
```

### Logging for Verification

Extensive console logging statements verify operation:
```typescript
console.log("[Uprooted] Started plugin: ${name}");
console.log("[Uprooted:sentry-blocker] Blocked fetch to sentry.io (${blockedCount} total)");
```

### Defensive Programming

- Early returns on validation failures: `if (!plugin) { console.error(...); return; }`
- Type guards and null checks: `if (settings?.enabled)`, `const plugin = this.plugins.get(name)`
- Graceful fallbacks: Settings loading returns `DEFAULT_SETTINGS` on error rather than throwing

## High-Priority Testing Targets

**1. Plugin Lifecycle (`src/core/pluginLoader.ts`):**
- Plugin registration: `register()` should prevent duplicates, accept new plugins
- Plugin starting: `start()` should install patches, inject CSS, call lifecycle hooks
- Plugin stopping: `stop()` should uninstall patches, remove CSS, clean up
- Event emission: `emit()` should route events to correct handlers, respect cancellation
- Multi-plugin scenarios: Handlers from multiple plugins should chain correctly

**2. Settings Persistence (`src/core/settings.ts`):**
- Loading settings: `loadSettings()` should parse JSON, merge with defaults on error
- Saving settings: `saveSettings()` should create directory, write valid JSON
- Error recovery: Settings should not be lost on parse errors (spread operator merges)
- File location: Should use correct `PROFILE_DIR` path (Windows/Linux platform detection)

**3. Bridge Proxy (`src/api/bridge.ts`):**
- Proxy creation: `createBridgeProxy()` should wrap functions, emit events
- Event interception: Plugins can cancel/modify calls before bridge sees them
- Async bridge setup: Should handle asynchronous bridge availability via `Object.defineProperty`
- Multiple proxies: Both `__nativeToWebRtc` and `__webRtcToNative` should be independently proxied

**4. Sentry Blocker (`src/plugins/sentry-blocker/index.ts`):**
- Fetch blocking: Requests to `*.sentry.io` should return 200 response, others pass through
- XHR blocking: XMLHttpRequest to Sentry URLs should redirect to `about:blank`
- SendBeacon blocking: `navigator.sendBeacon` to Sentry URLs should return `true` silently
- Count tracking: `blockedCount` should increment accurately

## Medium-Priority Testing Targets

**5. Initialization Sequence (`src/core/preload.ts`):**
- Settings check: Should skip init if settings disabled
- Plugin registration: Built-in plugins should be registered before `startAll()`
- Error handling: Fatal errors should display banner, not throw
- Version availability: `window.__UPROOTED_VERSION__` should be set correctly

**6. Theme Engine (`src/plugins/themes/index.ts`):**
- Color math: `darken()`, `lighten()`, `luminance()` produce correct hex values
- Custom theme generation: `generateCustomVariables()` produces valid CSS variable object
- Theme switching: CSS variables cleared on stop, applied on start
- Fallback handling: Invalid theme names should be ignored gracefully

**7. CSS Injection (`src/api/css.ts`):**
- Injection: `injectCss()` should create/update style element with correct ID
- Removal: `removeCss()` should remove specific style, not others
- Batch removal: `removeAllCss()` should remove all Uprooted-injected styles only
- ID prefixing: All injected styles prefixed with `uprooted-css-` for isolation

**8. DOM Utilities (`src/api/dom.ts`):**
- Element waiting: `waitForElement()` should resolve immediately if element exists
- Timeout handling: Should reject after timeout with descriptive message
- Observer cleanup: Should disconnect observer when element found
- Frame scheduling: `nextFrame()` should use `requestAnimationFrame` correctly

## Low-Priority Testing Targets

**9. Settings Panel (`src/plugins/settings-panel/components.ts`):**
- Component creation: Each function should return correct DOM element type
- Event handling: Toggle, select, textarea changes should trigger callbacks
- Debouncing: Textarea input should debounce callback by 300ms

**10. Installer Pages (`installer/src/pages/`):**
- Page routing: Page switches should initialize only once
- Detection: Root installation detection should correctly identify paths
- Installation/uninstallation: Should create backups, inject HTML, restore originals

## Mocking Strategy (for Future Tests)

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

3. **Window Globals** (in preload and bridge tests):
   ```typescript
   const mockNativeToWebRtc = { method1: vi.fn() };
   window.__nativeToWebRtc = mockNativeToWebRtc;
   ```

4. **Native API Calls** (Tauri):
   ```typescript
   vi.mock('@tauri-apps/api/window', () => ({
     getCurrentWindow: vi.fn(() => ({
       minimize: vi.fn(),
       close: vi.fn(),
     })),
   }));
   ```

**What NOT to Mock:**

1. Plugin registration logic — test real registration flow
2. Event emission chains — test actual handler execution order
3. Color math functions — test real darken/lighten calculations against known values
4. Settings merge logic — test actual object spreading behavior

## C# Hook Testing

**Manual Test Harness:** `hook-test/`

A standalone .NET project for testing the hook DLL outside of Root's process. This is a manual verification tool for the reflection-based Avalonia injection, not an automated test suite.

**Automated C# Tests:**
- `ColorUtilsTests.cs` — Coverage for RGB/HSV/hex color conversion
- `GradientBrushTests.cs` — Coverage for gradient brush creation (direct API and reflection approach)

**Run:**
```bash
dotnet test tests/UprootedTests/
```

**Debug via Logs:**

The primary testing tool for the C# hook layer is the log file `uprooted-hook.log` in Root's profile directory:
- Windows: `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log`
- Linux: `~/.local/share/Root Communications/Root/profile/default/uprooted-hook.log`

Real-time color-coded log tailing:
```powershell
powershell -File scripts/watch-log.ps1
powershell -File scripts/watch-log.ps1 -filter "Injector"
powershell -File scripts/watch-log.ps1 -errors
```

Post-mortem analysis:
```bash
python scripts/analyze-log.py [path-to-logfile]
```

## Test Coverage Goals (Recommended for TypeScript)

If testing were implemented, prioritize:
- **Statements:** 60% minimum (focus on critical paths)
- **Branches:** 50% minimum (error paths, conditional logic)
- **Functions:** 70% minimum (all exported functions covered)
- **Lines:** 60% minimum

**Critical uncovered paths:**
- Plugin lifecycle error scenarios (`start()`, `stop()` try-catch blocks)
- Bridge interception edge cases (deferred proxy setup)
- File system failures in settings (`mkdirSync`, `writeFileSync` errors)

## Recommended Setup (if Adding Tests)

### Install Vitest

```bash
pnpm install -D vitest @vitest/ui @vitest/coverage-v8 jsdom
```

### Configure vitest.config.ts

```typescript
import { defineConfig } from 'vitest/config';

export default defineConfig({
  test: {
    environment: 'jsdom',
    coverage: {
      provider: 'v8',
      reporter: ['text', 'html'],
      exclude: ['node_modules/', 'dist/', '**/*.test.ts'],
    },
    globals: true,
  },
});
```

### Add Test Scripts to package.json

```json
{
  "scripts": {
    "test": "vitest run",
    "test:watch": "vitest",
    "test:coverage": "vitest run --coverage",
    "test:ui": "vitest --ui"
  }
}
```

### Test File Locations

- **Co-located:** `src/core/__tests__/pluginLoader.test.ts` or `src/core/pluginLoader.test.ts`
- **Separate:** `tests/core/pluginLoader.test.ts`, `tests/api/`, `tests/plugins/`

**Naming:** Match source file, use `.test.ts` extension: `pluginLoader.ts` → `pluginLoader.test.ts`

## Current Testing Gaps

### TypeScript Layer — No Automated Coverage For:
- Plugin lifecycle state management (which plugins are active, pending, etc.)
- Event handler cleanup and plugin isolation (handlers from stopped plugins still firing)
- Bridge proxy deferred setup (async window property assignment)
- Settings JSON corruption recovery
- CSS style ID collision prevention
- DOM observer cleanup on timeout
- Theme variable name collision with custom CSS
- Installer HTML injection edge cases (missing `</head>` tag, already-patched files)

### C# Hook Layer — Limited Automated Coverage:
- AvaloniaReflection type resolution (relies on actual Avalonia assemblies being loaded)
- Visual tree walker layout discovery (requires Root's actual UI structure)
- SidebarInjector state machine (timer-based, threading-dependent)
- ContentPages rendering (reflection-based control creation)
- Concurrent injection/cleanup races (interlocked flag coordination)
- Back button cleanup timing (must fire before Root's handler)
- Settings JSON loading workaround (System.Text.Json MissingMethodException)

**Note:** C# hook is fundamentally hard to unit test because it depends on Root's actual runtime environment and Avalonia visual tree structure.

---

*Testing analysis: 2026-02-17*
