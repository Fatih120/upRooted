# Coding Conventions

**Analysis Date:** 2026-02-17

## Naming Patterns

**Files:**
- camelCase for module files: `pluginLoader.ts`, `preload.ts`, `patcher.rs`, `settings.ts`
- Kebab-case for plugin/feature directories: `sentry-blocker/`, `settings-panel/`, `themes/`
- Index files use `index.ts` for barrel exports and plugin entry points
- C# hook files use PascalCase: `StartupHook.cs`, `AvaloniaReflection.cs`, `SidebarInjector.cs`
- Rust files use snake_case: `uprooted_profiler.c`, `patcher.rs`, `detection.rs`

**Functions:**
- camelCase for all function names: `loadSettings()`, `waitForElement()`, `injectCss()`, `getCurrentTheme()`
- Verb-based naming: `install()`, `uninstall()`, `register()`, `start()`, `stop()`
- Internal/private functions use camelCase with leading underscore if needed: `findTargetHtmlFiles()`, `removeHandlers()`
- C# methods: PascalCase, e.g., `LoadSettings()`, `RunOnUIThread()`, `CreateInstance()`

**Variables:**
- camelCase for all variables: `pluginLoader`, `activePlugins`, `eventHandlers`, `blockedCount`
- Constants use UPPER_SNAKE_CASE: `BACKUP_SUFFIX`, `INJECTION_MARKER`, `ID_PREFIX`, `PROFILE_DIR`
- Descriptive names favored over abbreviations: `pluginSettings` not `ps`, `eventHandlers` not `handlers`

**Types:**
- PascalCase for interfaces: `UprootedPlugin`, `PluginLoader`, `BridgeEvent`, `UprootedSettings`
- PascalCase for types: `Theme`, `TileType`, `WebRtcPermission`, `ColorMatrix`
- Branded types for semantic distinction: `type UserGuid = string & { readonly __brand: "UserGuid" }`
- Discriminated unions for variant types: `SettingField` uses `type: "boolean" | "string" | "number" | "select"`

## Code Style

**Formatting:**
- 2-space indentation consistently across TypeScript, C#, and Rust
- Semicolons used at end of statements (TypeScript, C#)
- Line breaks between logical sections with comment dividers: `// ── Section Name ──`
- No explicit formatter configured (no .prettierrc, .eslintrc, or ESLint config), but style is consistent throughout codebase

**Linting:**
- TypeScript strict mode enabled: `"strict": true` in `tsconfig.json`
- Type safety enforced: `forceConsistentCasingInFileNames: true`, `isolatedModules: true`
- No ESLint configuration, but implicit strict typing expectations
- C# code follows .NET conventions (implicit style)
- Rust code runs `cargo clippy` for linting (see `CONTRIBUTING_TECHNICAL.md`)

## Import Organization

**Order (TypeScript):**
1. Node.js built-in modules: `import fs from "node:fs"; import path from "node:path";`
2. External packages: `import type { PluginLoader } from "@tauri-apps/api";`
3. Local modules with relative paths: `import type { UprootedPlugin } from "../types/plugin.js";`
4. Barrel exports: `import { css, dom, native, bridge } from "@uprooted/api";`

**Path Aliases:**
- `@uprooted/*` → `src/*` (defined in `tsconfig.json`)
- All imports from core, api, types, and plugins use relative paths: `../types/plugin.js`, `./css.js`
- All imports explicitly include `.js` extension (ES modules)

**Import Style:**
- Use `import type` for type-only imports: `import type { UprootedPlugin, Patch } from "../types/plugin.js";`
- Namespace imports for module exports: `export * as css from "./css.js";`
- Named imports for specific exports: `import { injectCss, removeCss } from "../api/css.js";`

## Error Handling

**Patterns (TypeScript/Chromium layer):**
- Try-catch for synchronous operations: `try { const plugin = await ...; } catch (err) { console.error(...); }`
- Async/await with try-catch for Promise handling (never `.catch()` chaining)
- Fallback defaults on error: `return { ...DEFAULT_SETTINGS }` when settings load fails
- Console logging for errors: `console.error("[Uprooted] Failed to...", err)`
- Plugin lifecycle hooks wrapped in try-catch: `start()` and `stop()` in `PluginLoader`
- Return early pattern for validation: `if (!plugin) { return; }` in `start()` and `stop()` methods
- No implicit undefined; always explicit type signature

**Patterns (C# hook layer):**
- Never throw exceptions from injected code — catch and log to prevent crashing Root
- Logger class swallows its own exceptions to avoid cascading failures
- All reflection calls handle null gracefully
- Phase-based startup (Phase 0-5) with per-phase error logging via `Logger.Log()`
- Use `Interlocked.CompareExchange` for atomic guards (never `lock` statements in profiler context)

## Logging

**Framework:**
- TypeScript: `console` object directly (no logging library)
- C#: Custom `Logger.cs` for file-based logging with timestamps

**Patterns (TypeScript):**
- All logs prefixed with `[Uprooted]` for easy identification: `console.log("[Uprooted] Started plugin: ${name}")`
- Plugin-specific logs include plugin name: `console.log("[Uprooted:sentry-blocker] Blocked fetch to sentry.io")`
- Error logs use `console.error()`: `console.error("[Uprooted] Failed to start plugin..."`
- Warning logs use `console.warn()`: `console.warn("[Uprooted] Plugin "${plugin.name}" already registered")`
- Info logs use `console.log()`: General status messages
- No structured logging (no timestamps, no log levels beyond console methods)
- Counters logged in messages: `Blocked ${blockedCount} requests`

**Patterns (C#):**
- Log messages use `[Category]` prefix: `Logger.Log("Startup", "Phase 1 OK: Avalonia assemblies loaded")`
- Produces: `[HH:mm:ss.fff] [Category] message` format
- Thread-safe file logging to `uprooted-hook.log` in profile directory
- Categories include: `[Startup]`, `[Injector]`, `[TreeWalker]`, `[Theme]`, `[Settings]`, `[Diag]`, `[Style]`
- Clean startup log pattern: Phase completion messages, assembly availability, UI state changes

## Comments

**When to Comment:**
- JSDoc comments on exported functions describing purpose, parameters, return values
- Inline comments explaining non-obvious logic or complex algorithms (color math in `themes/index.ts`, reflection patterns in C#)
- Section dividers using `// ── Section Name ──` pattern in UI components and large files (observed in `pages/main.ts`, `AvaloniaReflection.cs`)
- Pragmatic comments explaining "why" rather than "what" the code does
- Implementation details like "Root may set them asynchronously" explaining behavioral quirks
- Behavioral constraints for reflection: "Type.GetType() fails for trimmed Avalonia types -- use AvaloniaReflection instead"

**JSDoc/TSDoc:**
- Used consistently on public exported functions and classes:
  ```typescript
  /**
   * Plugin Loader — Discovers, validates, and manages the lifecycle of plugins.
   */
  export class PluginLoader { ... }
  ```
- Parameter documentation inline in comment text (not formal `@param` tags)
- Return type documentation inline: Documented in function signature, not separate `@returns`
- Used on complex utilities to explain intent: `waitForElement()`, `observe()`, `createBridgeProxy()`

## Function Design

**Size:**
- Most functions keep to single responsibility: `injectCss()` creates/updates style element, nothing else
- Larger functions documented with inline section comments: `installBridgeProxy()` has clear stages
- Plugin methods (`start()`, `stop()`) are 10-20 lines maximum when logic is simple
- C# reflection methods grouped by conceptual area (e.g., all property-setting methods together in `AvaloniaReflection.cs`)

**Parameters:**
- Typed parameters required (TypeScript strict mode): `function install(distDir: string): void`
- Optional parameters use `?`: `timeout = 10000` with default values
- Configuration objects destructured in parameters: Not observed; pass full object instead
- Generic type parameters for reusable utilities: `export function waitForElement<T extends Element = Element>(...)`
- C#: All parameters include explicit types; ref/out parameters documented with intent comments

**Return Values:**
- Void return for side effects: `export function install(distDir: string): void`
- Promise for async operations: `async start(name: string): Promise<void>`
- Explicit return types always specified (TypeScript strict)
- Early returns for validation/guards: Used throughout `PluginLoader` class
- No implicit undefined; always explicit type signature
- C#: Explicit return types on all methods, use `void` for side effects, use `bool` for success/failure

## Module Design

**Exports:**
- Named exports for individual utilities: `export function injectCss(...)`, `export function removeCss(...)`
- Default exports for plugin objects: `export default sentryBlockerPlugin satisfies UprootedPlugin;`
- Barrel exports using namespace: `export * as css from "./css.js";` in `api/index.ts`
- Type exports separate from implementation: `export type` and `export interface` in `types/` directory

**Barrel Files:**
- Central API barrel at `src/api/index.ts` re-exports all public modules: `css`, `dom`, `native`, `bridge`
- Plugin objects exported as defaults for clean import: `import settingsPanelPlugin from "../plugins/settings-panel/index.js"`
- Directory-based organization with index files: `src/plugins/settings-panel/index.ts`, `src/core/` directory

**Module Boundaries:**
- Clear separation: `src/api/` for public plugin APIs, `src/core/` for internal infrastructure, `src/types/` for shared types
- Plugins in `src/plugins/` are self-contained: include their own types, styles, and components
- No circular dependencies observed; imports flow downward through layer hierarchy
- C# hook: `Uprooted` namespace for most classes, exception: `Entry.cs` uses `UprootedHook`, `StartupHook` is global

## C# Conventions (hook/)

**Access Modifiers:**
- `internal` access for all non-entry classes
- `Entry.cs` uses `public` (must be accessible for `CreateInstance`)
- `StartupHook` class is global namespace (no `namespace` declaration)

**Namespace:**
- `Uprooted` namespace for most hook classes
- Exception: `Entry.cs` uses `UprootedHook` namespace, `StartupHook` is global

**Reflection Rules (CRITICAL):**
- ALL Avalonia type access through `AvaloniaReflection` — never `typeof()` or `Type.GetType()`
- Never use `Enum.Parse` for `DispatcherPriority` (it's a struct in Avalonia 11+)
- Never use `EventInfo.AddEventHandler` for Avalonia RoutedEvents — use `Expression.Lambda`
- Always use `ClearValue(AvaloniaProperty)` to restore bindings after `SetValue`
- CAS-based initialization using `Interlocked.CompareExchange` (never `lock` or `bool` flag)
- Pattern: `if (Interlocked.CompareExchange(ref _initialized, 1, 0) == 0)`

**UI Thread Safety:**
- Never block the UI thread — all heavy work on background threads
- Timer callback dispatches to `Dispatcher.UIThread` via `RunOnUIThread()`
- Interlocked flags for concurrent operation guards
- Never modify `ContentControl.Content` directly — causes UI freeze; use Grid overlay pattern

**Error Handling:**
- Never throw from injected code — catch and log
- Logger swallows its own exceptions
- All reflection calls handle null gracefully

**Logging:**
- `[Category]` prefix in log messages
- Thread-safe file logging to `uprooted-hook.log`

## TypeScript Browser Injection Patterns

**Bridge Interception:**
- Use ES6 Proxy to wrap Root's native bridge objects (`__nativeToWebRtc`, `__webRtcToNative`)
- Plugins can intercept calls before bridge sees them via `patches` array
- Always set proxies deferred via `Object.defineProperty` (bridge may not be immediately available)

**CSS Injection:**
- All injected style elements prefixed with `uprooted-css-` for isolation
- CSS variables use `:root` for theme application
- Always remove CSS on plugin stop via `removeCss()` by ID

**DOM Operations:**
- Use `MutationObserver` for DOM watching, clean up on observer completion
- `nextFrame()` uses `requestAnimationFrame` for layout-safe updates
- Never use `localStorage` — Root runs Chromium with `--incognito`

## Rust Conventions (installer/)

**Standard Rust Style:**
- Follow `cargo fmt` formatting
- Pass `cargo clippy` linting
- Functions return `Result` types for error propagation
- Use `anyhow` for application-level error handling
- Descriptive variable names matching Rust conventions

---

*Convention analysis: 2026-02-17*
