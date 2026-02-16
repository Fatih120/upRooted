# Coding Conventions

**Analysis Date:** 2026-02-16

## Naming Patterns

**Files:**
- Kebab-case for module files: `plugin-loader.ts`, `sentry-blocker`, `settings-panel`
- PascalCase for components that export classes: Not used in current codebase
- Index files use `index.ts` for barrel exports

**Functions:**
- camelCase for all function names: `loadSettings()`, `waitForElement()`, `injectCss()`, `getCurrentTheme()`
- Verb-based naming: `install()`, `uninstall()`, `register()`, `start()`, `stop()`
- Internal/private functions use camelCase with leading underscore if needed: `findTargetHtmlFiles()`, `removeHandlers()`

**Variables:**
- camelCase for all variables: `pluginLoader`, `activePlugins`, `eventHandlers`, `blockedCount`
- Constants use camelCase: `BACKUP_SUFFIX`, `INJECTION_MARKER`, `ID_PREFIX`, `PROFILE_DIR`
- Descriptive names favored over abbreviations: `pluginSettings` not `ps`, `eventHandlers` not `handlers`

**Types:**
- PascalCase for interfaces: `UprootedPlugin`, `PluginLoader`, `BridgeEvent`, `PluginSettings`
- PascalCase for types: `Theme`, `TileType`, `WebRtcPermission`
- Branded types for semantic distinction: `type UserGuid = string & { readonly __brand: "UserGuid" }`
- Discriminated unions for variant types: `SettingField` uses `type: "boolean" | "string" | "number" | "select"`

## Code Style

**Formatting:**
- No explicit formatter configured (no .prettierrc, .eslintrc, or ESLint config found)
- 2-space indentation observed in all files
- Semicolons used consistently at end of statements
- Line breaks between logical sections (observed in `preload.ts`, `pluginLoader.ts`)

**Linting:**
- No ESLint configuration detected
- TypeScript strict mode enabled: `"strict": true` in `tsconfig.json`
- Type safety enforced: `forceConsistentCasingInFileNames: true`, `isolatedModules: true`

## Import Organization

**Order:**
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

**Patterns:**
- Try-catch for synchronous operations: Used in `loadSettings()` and `saveSettings()` to gracefully handle JSON parsing errors
- Async/await with try-catch for Promise handling: `try { const plugin = await ...; } catch (err) { console.error(...); }`
- Fallback defaults on error: `return { ...DEFAULT_SETTINGS }` when settings load fails
- Console logging for errors: `console.error("[Uprooted] Failed to...", err)`
- Plugin lifecycle hooks wrapped in try-catch: `start()` and `stop()` in `PluginLoader`
- Return early pattern for validation: `if (!plugin) { return; }` in `start()` and `stop()` methods
- Promise-based error handling: `.catch()` chaining not observed; async/await preferred

## Logging

**Framework:** `console` object directly (no logging library)

**Patterns:**
- All logs prefixed with `[Uprooted]` for easy identification: `console.log("[Uprooted] Started plugin: ${name}")`
- Plugin-specific logs include plugin name: `console.log("[Uprooted:sentry-blocker] Blocked fetch to sentry.io")`
- Error logs use `console.error()`: `console.error("[Uprooted] Failed to start plugin..."`
- Warning logs use `console.warn()`: `console.warn("[Uprooted] Plugin "${plugin.name}" already registered")`
- Info logs use `console.log()`: General status messages
- No structured logging (no timestamps, no log levels beyond console methods)
- Counters logged in messages: `Blocked ${blockedCount} requests`

## Comments

**When to Comment:**
- JSDoc comments on exported functions describing purpose, parameters, return values
- Inline comments explaining non-obvious logic or complex algorithms (color math in `themes/index.ts`)
- Section dividers using `// ── Section Name ──` pattern in UI components (observed in `pages/main.ts`)
- Pragmatic comments explaining "why" rather than "what" the code does
- Implementation details like "Root may set them asynchronously" explaining behavioral quirks

**JSDoc/TSDoc:**
- Used consistently on public exported functions and classes:
  ```typescript
  /**
   * Plugin Loader — Discovers, validates, and manages the lifecycle of plugins.
   */
  export class PluginLoader { ... }
  ```
- Parameter documentation in JSDoc: `@param` not observed; parameter descriptions inline in comment text
- Return type documentation inline: Documented in function signature, not separate `@returns`
- Used on complex utilities to explain intent: `waitForElement()`, `observe()`, `createBridgeProxy()`

## Function Design

**Size:**
- Most functions keep to single responsibility: `injectCss()` creates/updates style element, nothing else
- Larger functions documented with inline section comments: `installBridgeProxy()` has clear stages
- Plugin methods (`start()`, `stop()`) are 10-20 lines maximum when logic is simple

**Parameters:**
- Typed parameters required (TypeScript strict mode): `function install(distDir: string): void`
- Optional parameters use `?`: `timeout = 10000` with default values
- Configuration objects destructured in parameters: Not observed; pass full object instead
- Generic type parameters for reusable utilities: `export function waitForElement<T extends Element = Element>(...)`

**Return Values:**
- Void return for side effects: `export function install(distDir: string): void`
- Promise for async operations: `async start(name: string): Promise<void>`
- Explicit return types always specified (TypeScript strict)
- Early returns for validation/guards: Used throughout `PluginLoader` class
- No implicit undefined; always explicit type signature

## Module Design

**Exports:**
- Named exports for individual utilities: `export function injectCss(...)`, `export function removeCss(...)`
- Default exports for plugin objects: `export default sentryBlockerPlugin satisfies UprootedPlugin;`
- Barrel exports using namespace: `export * as css from "./css.js";` in `api/index.ts`
- Type exports separate from implementation: `export type` and `export interface` in `types/` directory

**Barrel Files:**
- Central API barrel at `src/api/index.ts` re-exports all public modules
- Plugin objects exported as defaults for clean import: `import settingsPanelPlugin from "../plugins/settings-panel/index.js"`
- Directory-based organization with index files: `src/plugins/settings-panel/index.ts`, `src/core/` directory

**Module Boundaries:**
- Clear separation: `src/api/` for public plugin APIs, `src/core/` for internal infrastructure, `src/types/` for shared types
- Plugins in `src/plugins/` are self-contained: include their own types, styles, and components
- No circular dependencies observed; imports flow downward through layer hierarchy

---

*Convention analysis: 2026-02-16*
