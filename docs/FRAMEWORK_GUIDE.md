# Uprooted Framework Guide

Authoritative reference for the Uprooted client modification framework. This document is the source of truth for any AI session or developer working on this codebase.

---

## 1. Project Overview

**Uprooted** is a client modification framework for [Root Communications](https://rootapp.com) v0.9.86, analogous to [Vencord](https://vencord.dev/) for Discord. It provides a plugin and theme system that hooks into Root's desktop app at runtime, allowing users to customize the UI and intercept internal bridge calls without permanently modifying the application binary.

### What it does

- Injects custom UI (sidebar sections, settings pages) into Root's native Avalonia settings panel
- Proxies Root's internal JavaScript bridge interfaces so plugins can intercept, modify, or cancel bridge method calls
- Provides a theme engine that overrides Root's CSS variable system
- Offers an installer/uninstaller that persists across Root restarts via .NET CLR profiler injection

### What it is not

- Not a backend exploit tool. Uprooted operates entirely on the client side and does not interact with Root's gRPC backend.
- Not a binary patcher (the legacy approach was superseded). The current injection uses CLR profiler IL injection or .NET startup hooks.
- Not yet publicly distributed. The project is awaiting explicit approval from Root's developers before distributing working injection code.

### Dual-layer architecture

Uprooted has two independent injection layers that operate in parallel:

1. **C# .NET hook** (`hook/`) -- Injects into Root's managed .NET 10/Avalonia process via CLR profiler. Adds native Avalonia controls to the settings page sidebar.
2. **TypeScript browser injection** (`src/`) -- Injects into Root's embedded Chromium (DotNetBrowser) context via HTML `<script>` tags. Provides the plugin/theme runtime and bridge proxy system.

The C# layer handles native UI integration. The TypeScript layer handles web content modification. They share no runtime state but target the same application.

### Repositories and collaboration

There are two GitHub repositories:

| Repo | Visibility | Purpose |
|---|---|---|
| [`watchthelight/uprooted`](https://github.com/watchthelight/uprooted) | **Public** | Scaffold, types, documentation, landing page. No working injection code until Root developer approval. |
| [`watchthelight/uprooted-private`](https://github.com/watchthelight/uprooted-private) | **Private** | Full working codebase including all injection code, debug tooling, build artifacts, profiler DLLs, test harnesses. This is the active development repo. |

**Collaborators on `uprooted-private`:**
- `watchthelight` (admin) -- primary developer
- `agomusio` (write) -- collaborator/developer

The private repo contains everything: source, compiled DLLs, debug scripts, host trace logs, hook test harnesses, legacy patchers, and the framework guide. It is the developer repo, not a cleaned distribution.

**Git remotes** (in the local working copy):
- `origin` -> `watchthelight/uprooted` (public)
- `private` -> `watchthelight/uprooted-private` (private, active development)

When pushing changes, push to `private` for development work. Only push to `origin` when updating the public scaffold (types, docs, site).

---

## 2. High-Level Architecture

### Mental model

```
Root.exe (.NET 10, Avalonia)
  |
  +-- Native UI (Avalonia controls)
  |     |
  |     +-- [C# Hook] SidebarInjector adds UPROOTED section to Settings
  |           Uses: AvaloniaReflection -> VisualTreeWalker -> ContentPages
  |           Entry: CLR Profiler IL injection -> Entry.cs -> StartupHook.cs
  |
  +-- DotNetBrowser (Chromium 144)
        |
        +-- WebRtcBundle/index.html (voice/video)
        |     |
        |     +-- [TS Injection] preload.ts runs before Root's bundles
        |           Sets up: bridge proxies, plugin loader, theme engine
        |           Plugins: settings-panel (DOM), themes (CSS vars)
        |
        +-- RootApps/*/index.html (polls, tasks, raids, etc.)
              |
              +-- [TS Injection] Same preload.ts + CSS injected here too
```

### Data flow

**C# hook lifecycle:**
1. CLR profiler injects IL into Root's `<Main>$` method at process startup
2. IL calls `Assembly.LoadFrom("UprootedHook.dll")` then `CreateInstance("UprootedHook.Entry")`
3. `Entry.cs` triggers `StartupHook.Initialize()` (once, via interlocked CAS)
4. Background thread runs 4-phase wait: Avalonia assemblies -> Application.Current -> MainWindow -> SidebarInjector
5. `SidebarInjector` polls every 200ms on the UI thread, detects settings page open/close, injects/removes UI

**TypeScript injection lifecycle:**
1. Patcher inserts `<script>` and `<link>` tags into profile HTML files (before `</head>`)
2. On page load, `preload.ts` runs before Root's bundles
3. Reads settings from `window.__UPROOTED_SETTINGS__` (inlined by patcher)
4. Installs ES6 Proxy wrappers on `window.__nativeToWebRtc` and `window.__webRtcToNative`
5. Creates `PluginLoader`, registers built-in plugins, starts enabled ones
6. Plugins apply patches (bridge intercepts), inject CSS, run lifecycle hooks

### Integration points with Root

| Component | How Uprooted hooks in |
|---|---|
| Settings sidebar | C# reflection: walks Avalonia visual tree, finds "APP SETTINGS" text, locates Grid layout, injects StackPanel with custom items |
| WebRTC bridge | TS Proxy: wraps `__nativeToWebRtc` / `__webRtcToNative` globals before Root assigns them |
| Theme system | CSS variable override: Root uses `--rootsdk-*` variables; themes call `document.documentElement.style.setProperty()` |
| Profile HTML | Patcher: inserts `<script>` tags into `WebRtcBundle/index.html` and `RootApps/*/index.html` |

---

## 3. Repository Structure

The Uprooted code lives at `uprooted/` within the Root.Dev workspace. It is a separate git repository (`https://github.com/watchthelight/uprooted`).

```
uprooted/
├── src/                    # TypeScript source (browser injection layer)
│   ├── core/               # Runtime core
│   ├── api/                # Public API for plugins
│   ├── plugins/            # Built-in plugins
│   └── types/              # TypeScript type definitions
├── hook/                   # C# .NET managed code (native injection layer)
├── scripts/                # Build and install scripts
├── installer/              # Tauri-based GUI installer (separate workspace)
├── site/                   # Astro documentation website (uprooted.sh)
├── legacy/                 # Superseded Python binary patchers
├── tools/                  # Binary tools (chromium_wrapper.exe)
├── dist/                   # Build output (git-ignored)
├── package.json            # pnpm workspace root
├── tsconfig.json           # TypeScript config
├── Install-Uprooted.ps1   # PowerShell installer
├── Uninstall-Uprooted.ps1 # PowerShell uninstaller
├── README.md
└── CONTRIBUTING.md
```

### `src/core/` -- Runtime core

| File | Purpose |
|---|---|
| `preload.ts` | Entry point injected into Chromium. Reads settings, installs bridge proxies, starts plugins. |
| `pluginLoader.ts` | Plugin lifecycle manager. Register, start, stop plugins. Routes bridge events to patch handlers. |
| `patcher.ts` | Injects/removes script and CSS tags from Root's profile HTML files. Used by CLI install/uninstall. |
| `settings.ts` | File-based settings persistence. Reads/writes `uprooted-settings.json` in Root's profile directory. |

**What belongs here:** Core runtime infrastructure that all plugins depend on. Entry points, lifecycle management, persistence.

**What does NOT belong here:** Plugin-specific logic, UI components, API wrappers.

### `src/api/` -- Public plugin API

| File | Purpose |
|---|---|
| `bridge.ts` | ES6 Proxy wrappers for Root's bridge globals. Intercepts all method calls and routes to plugin handlers. |
| `css.ts` | Inject/remove `<style>` elements by ID. |
| `dom.ts` | DOM utilities: `waitForElement()`, `observe()`, `nextFrame()`. |
| `native.ts` | CSS variable get/set, native bridge logging. |
| `index.ts` | Barrel export: `import { css, dom, native, bridge } from "@uprooted/api"`. |

**What belongs here:** Stable, documented APIs intended for use by plugin authors. Functions should be general-purpose and not tied to specific plugins.

**What does NOT belong here:** Internal implementation details, plugin-specific helpers, unstable experimental APIs.

### `src/plugins/` -- Built-in plugins

Each plugin is a subdirectory with at minimum an `index.ts` exporting an `UprootedPlugin` object.

| Plugin | Directory | Purpose |
|---|---|---|
| `themes` | `plugins/themes/` | CSS variable theme engine. Reads theme definitions from `themes.json`. |
| `settings-panel` | `plugins/settings-panel/` | Injects UPROOTED sidebar section and content pages into Root's web-rendered settings. |

**What belongs here:** Self-contained plugin implementations. Each plugin should be independently startable/stoppable.

**Naming convention:** Directory name = plugin name used in settings and registration.

### `src/types/` -- Type definitions

| File | Purpose |
|---|---|
| `plugin.ts` | `UprootedPlugin` interface, `Patch` interface, `SettingField` union type. |
| `settings.ts` | `UprootedSettings` interface, `PluginSettings` interface, `DEFAULT_SETTINGS` constant. |
| `bridge.ts` | Full type definitions for `INativeToWebRtc` (42 methods) and `IWebRtcToNative` (28 methods). |
| `root.ts` | Global `Window` augmentation declaring Root's runtime globals and Uprooted's injected properties. |

### `hook/` -- C# .NET hook

| File | Lines | Purpose |
|---|---|---|
| `Entry.cs` | 34 | Profiler injection entry. `ModuleInitializer` + constructor, both guarded by interlocked CAS. |
| `NativeEntry.cs` | 67 | Alternative entry via `hostfxr load_assembly_and_get_function_pointer`. Diagnostic-heavy. |
| `StartupHook.cs` | 121 | Main initialization. Process guard, 4-phase Avalonia wait, starts SidebarInjector. |
| `AvaloniaReflection.cs` | 815 | Reflection cache for ~80 Avalonia types, properties, and methods. Control creation and manipulation. |
| `VisualTreeWalker.cs` | 331 | Visual tree traversal. Settings page layout discovery (finds nav, content, grid). |
| `SidebarInjector.cs` | 610 | Timer-based monitor. Injects UPROOTED section, manages content panel swapping, handles click events. |
| `ContentPages.cs` | 443 | Page builders for Uprooted, Plugins, Themes pages. Matches Root's native card styling. |
| `UprootedSettings.cs` | 42 | Settings placeholder. Currently returns defaults (JSON deserialization disabled due to MissingMethodException). |
| `Logger.cs` | 30 | Thread-safe file logging to `uprooted-hook.log` in profile directory. |
| `UprootedHook.csproj` | 7 | .NET 10.0 project file, nullable enabled, implicit usings. |

**What belongs here:** Code that runs inside Root's .NET process. All Avalonia interaction. All reflection-based type access.

**What does NOT belong here:** Browser-side code, Node.js utilities, build tooling.

### `scripts/` -- Build and install scripts

| File | Purpose |
|---|---|
| `build.ts` | esbuild bundler. Produces `dist/uprooted-preload.js` (IIFE) and `dist/uprooted.css`. |
| `install.ts` | CLI wrapper: calls `patcher.install()`. |
| `uninstall.ts` | CLI wrapper: calls `patcher.uninstall()`. |

### `legacy/` -- Superseded code

Old Python scripts for binary patching (theme application, revert). These are no longer the active approach. Keep for reference but do not extend.

### Key configuration files

| File | Purpose |
|---|---|
| `package.json` | pnpm workspace root. v0.1.95, custom license (see LICENSE), Node >= 20. Scripts: build, dev, install-root, uninstall-root. |
| `tsconfig.json` | ES2022 target/module, strict mode, `@uprooted/*` path alias to `src/*`. |
| `pnpm-workspace.yaml` | Monorepo: `installer/`, `site/`. |
| `Install-Uprooted.ps1` | PowerShell one-click installer. Supports Profiler and StartupHooks methods. |
| `Uninstall-Uprooted.ps1` | PowerShell uninstaller. Removes env vars, restores backup, cleans up. |

---

## 4. Core Abstractions & Patterns

### Plugin interface (`src/types/plugin.ts`)

Every plugin implements this interface:

```typescript
interface UprootedPlugin {
  name: string;           // Unique identifier, matches directory name
  description: string;
  version: string;
  authors: Author[];

  start?(): void | Promise<void>;   // Called when enabled
  stop?(): void | Promise<void>;    // Called when disabled

  patches?: Patch[];                // Bridge method intercepts
  css?: string;                     // CSS injected while active
  settings?: SettingsDefinition;    // Plugin-specific config schema
}
```

Plugins are registered with `PluginLoader.register()` and managed via `start()`/`stop()`. The loader handles CSS injection/removal and patch installation/cleanup automatically.

### Patch system (bridge interception)

Patches intercept calls on Root's bridge objects:

```typescript
interface Patch {
  bridge: "nativeToWebRtc" | "webRtcToNative";
  method: string;
  before?(args: unknown[]): boolean | void;   // Return false to cancel
  replace?(...args: unknown[]): unknown;       // Replace entirely
  after?(result: unknown, args: unknown[]): void;
}
```

The bridge proxy (`src/api/bridge.ts`) wraps each bridge global with an ES6 `Proxy`. On every method call:
1. A `BridgeEvent` is created: `{ method, args, cancelled, returnValue }`
2. The `PluginLoader.emit()` fires the event to all registered handlers for that method
3. Handlers can cancel the call (`event.cancelled = true`), modify args (`event.args`), or set a return value (`event.returnValue`)
4. If not cancelled, the original method is called with (potentially modified) args

**Deferred proxy installation:** Root may assign bridge globals asynchronously. The proxy handles this via `Object.defineProperty` getter/setter traps that automatically wrap any value assigned to `window.__nativeToWebRtc` or `window.__webRtcToNative`.

### AvaloniaReflection pattern (C# reflection cache)

Root.exe is a single-file .NET 10 binary. Standard `Type.GetType("Avalonia.Controls.TextBlock, Avalonia.Controls")` fails because assembly names aren't resolvable in single-file context.

`AvaloniaReflection` solves this by:
1. Scanning all loaded assemblies filtered by `"Avalonia"` prefix
2. Building a `Dictionary<string, Type>` of all types
3. Looking up types by full name (e.g., `"Avalonia.Controls.TextBlock"`)
4. Caching all `PropertyInfo` and `MethodInfo` handles at resolution time

All Avalonia control creation and property manipulation goes through this class. Direct `typeof()` or `Type.GetType()` is never used for Avalonia types.

Key design decisions in the reflection layer:

- **DispatcherPriority is a struct, not an enum** in Avalonia 11+. `RunOnUIThread()` tries static property `.Normal`, then static field, then `Enum.Parse`, then `Activator.CreateInstance` as fallbacks.
- **Event subscription uses `Expression.Lambda`** because CLR `EventInfo.AddEventHandler` does not work for Avalonia's `RoutedEvent` system. The lambda compiles a delegate matching the exact event handler signature.
- **`ClearValue(AvaloniaProperty)`** removes local value overrides, re-enabling data bindings. This is critical: calling `SetValue` on a control overrides its binding permanently until `ClearValue` is called.

### Visual tree discovery pattern (C# settings page)

The `VisualTreeWalker.FindSettingsLayout()` method discovers Root's settings page structure by:
1. Finding the "APP SETTINGS" TextBlock via depth-first traversal
2. Walking up from that text to find a StackPanel with >= 8 children (the nav container)
3. Walking further up to find a multi-column Grid (skipping single-column Grids)
4. Identifying the content area as the Grid child in the same row but different (higher) column as the nav

This approach is fragile but necessary because Root provides no stable selectors. The layout is discovered by structure, not by class names or IDs.

### Tag-based element identification (C# injection)

Injected Avalonia controls are tagged via `Control.Tag` property:
- `"uprooted-injected"` -- The injected sidebar section
- `"uprooted-content"` -- The active content page overlay
- `"uprooted-version"` -- The version text
- `"uprooted-item-{page}"` -- Individual nav items
- `"uprooted-highlight-{page}"` -- Highlight overlays for nav items

Tags allow the injector to detect whether injection has already occurred and to locate its own controls for removal.

### Content overlay pattern (C# settings page)

The injector does NOT modify Root's content panel directly. Instead:
1. The Uprooted page is added as a new child of the settings Grid
2. Grid.Column and Grid.Row are set to match the content area's position
3. An opaque background (`#0D1521`) ensures the Uprooted page covers Root's content via z-order
4. When the user clicks a Root sidebar item, the overlay is removed from the Grid

This avoids the freeze that occurs when modifying `ContentControl.Content` or `ScrollContentPresenter.Content` directly.

### Data attribute identification (TypeScript injection)

In the browser layer, injected DOM elements use `data-uprooted` attributes:
- `data-uprooted="section"` -- The sidebar section container
- `data-uprooted="header"` -- Section header
- `data-uprooted="item"` -- Nav item
- `data-uprooted-page="..."` -- Which page the item opens
- `data-uprooted="content"` -- The content panel
- `data-uprooted="version"` -- Version text

### Settings persistence

Because Root runs Chromium with `--incognito`, localStorage is wiped on every launch. Settings are persisted to `%LOCALAPPDATA%/Root Communications/Root/profile/default/uprooted-settings.json` and inlined into HTML by the patcher as `window.__UPROOTED_SETTINGS__`.

The C# hook currently cannot load settings from JSON due to a `MissingMethodException` when using `System.Text.Json` in the profiler-injected context. It uses hardcoded defaults.

---

## 5. Extension / Feature Development Guide

### Adding a new TypeScript plugin

1. **Create the plugin directory:**
   ```
   src/plugins/my-plugin/
   ├── index.ts      # Plugin definition (required)
   └── styles.css    # Optional CSS (auto-collected by build)
   ```

2. **Define the plugin in `index.ts`:**
   ```typescript
   import type { UprootedPlugin } from "../../types/plugin.js";

   export default {
     name: "my-plugin",
     description: "What this plugin does",
     version: "0.1.0",
     authors: [{ name: "Your Name" }],

     // Optional: intercept bridge calls
     patches: [
       {
         bridge: "nativeToWebRtc",
         method: "setTheme",
         before(args) {
           console.log("Theme changing to:", args[0]);
           // Return false to cancel the call
         },
       },
     ],

     // Optional: inject CSS
     css: `:root { --my-custom-var: #ff0000; }`,

     // Optional: lifecycle hooks
     start() {
       console.log("Plugin started");
     },

     stop() {
       console.log("Plugin stopped");
     },
   } satisfies UprootedPlugin;
   ```

3. **Register the plugin in `src/core/preload.ts`:**
   ```typescript
   import myPlugin from "../plugins/my-plugin/index.js";
   // ... after other registrations:
   loader.register(myPlugin);
   ```

4. **Build and install:**
   ```bash
   pnpm build
   pnpm install-root
   ```

### Adding a new C# settings page

1. **Add a page builder in `ContentPages.cs`:**
   ```csharp
   private static object? BuildMyPage(AvaloniaReflection r, UprootedSettings settings)
   {
       var page = r.CreateStackPanel(vertical: true, spacing: 0);
       if (page == null) return null;
       r.SetMargin(page, 24, 24, 24, 0);

       var card = CreateCard(r);
       // ... build card content using r.CreateTextBlock, r.CreateStackPanel, etc.

       return r.CreateScrollViewer(page);
   }
   ```

2. **Register in the `BuildPage` dispatcher:**
   ```csharp
   public static object? BuildPage(string pageName, AvaloniaReflection r, UprootedSettings settings)
   {
       return pageName switch
       {
           "uprooted" => BuildUprootedPage(r, settings),
           "plugins" => BuildPluginsPage(r, settings),
           "themes" => BuildThemesPage(r, settings),
           "my-page" => BuildMyPage(r, settings),  // Add here
           _ => null
       };
   }
   ```

3. **Add a sidebar item in `SidebarInjector.InjectSection()`:**
   ```csharp
   foreach (var (label, page) in new[] {
       ("Uprooted", "uprooted"),
       ("Plugins", "plugins"),
       ("Themes", "themes"),
       ("My Page", "my-page"),    // Add here
   })
   ```

### Adding a new theme

Add an entry to `src/plugins/themes/themes.json`:

```json
{
  "name": "my-theme",
  "display_name": "My Theme",
  "description": "Description of the theme",
  "author": "Your Name",
  "variables": {
    "--rootsdk-brand-primary": "#ff0000",
    "--rootsdk-background-primary": "#1a1a1a"
  },
  "preview_colors": {
    "background": "#1a1a1a",
    "text": "#ffffff",
    "accent": "#ff0000",
    "border": "#333333"
  }
}
```

Variable names must match Root's CSS variable names (`--rootsdk-*`). See `docs/plugins/ROOT_ENVIRONMENT.md` for the full list of CSS variables.

### Common mistakes to avoid

1. **Never use `Type.GetType()` for Avalonia types in C#.** Single-file apps cannot resolve assembly-qualified names. Always use `AvaloniaReflection`.

2. **Never use `Enum.Parse` for `DispatcherPriority`.** It is a struct in Avalonia 11+, not an enum. Use the `.Normal` static property.

3. **Never modify `ContentControl.Content` or `ScrollContentPresenter.Content` directly** in the C# hook. This causes the UI to freeze. Use the Grid overlay pattern.

4. **Never add children directly to a `VirtualizingStackPanel`** in the sidebar. They get recycled. Add to the parent container instead.

5. **Never use `EventInfo.AddEventHandler` for Avalonia RoutedEvents.** Use `AvaloniaReflection.SubscribeEvent()` which compiles Expression lambdas.

6. **Never use `SetValue` on Avalonia controls without considering bindings.** `SetValue` overrides bindings permanently. Use `ClearValue(AvaloniaProperty)` to restore them.

7. **Never use localStorage** for persistent state. Root runs Chromium with `--incognito`, wiping localStorage on every launch. Use file-based persistence.

8. **Never use `System.Text.Json` in the C# hook.** It causes `MissingMethodException` in the profiler-injected context.

---

## 6. Build, Dev, and Runtime Environment

### Prerequisites

- **Node.js** >= 20
- **pnpm** (package manager)
- **.NET 10 SDK** (for building UprootedHook.dll)
- **MSVC** (for building uprooted_profiler.dll -- C compiler)
- **Windows 11** (Root Communications is Windows-only)

### Build commands

```bash
# TypeScript layer
pnpm install              # Install dependencies
pnpm build                # Bundle src/ -> dist/uprooted-preload.js + dist/uprooted.css
pnpm dev                  # Watch mode (rebuilds on file changes)

# C# layer
dotnet build hook/ -c Release    # Build UprootedHook.dll

# Install into Root's profile
pnpm install-root         # Inject script/CSS tags into profile HTML files
pnpm uninstall-root       # Remove injection and restore backups

# Full install (builds + copies DLL + sets env vars)
powershell -File Install-Uprooted.ps1              # Profiler method (default)
powershell -File Install-Uprooted.ps1 -Method StartupHooks   # Startup hooks method

# Uninstall everything
powershell -File Uninstall-Uprooted.ps1
```

### Build output

```
dist/
├── uprooted-preload.js       # IIFE bundle, injected via <script> tag
├── uprooted-preload.js.map   # Source map
└── uprooted.css              # Combined CSS from all plugins
```

The build script:
- Uses **esbuild** with IIFE format, Chrome 120 target, ES2022
- Defines `__UPROOTED_VERSION__` from `package.json` version
- Excludes `node:fs` and `node:path` (CLI-only, not needed in browser)
- Collects all `.css` files from `src/plugins/` and concatenates them

### Injection methods

**Method 1: CLR Profiler (default, recommended)**

Environment variables (set by `Install-Uprooted.ps1`):
- `CORECLR_ENABLE_PROFILING=1`
- `CORECLR_PROFILER={D1A6F5A0-1234-4567-89AB-CDEF01234567}`
- `CORECLR_PROFILER_PATH=%LOCALAPPDATA%\Root\uprooted\uprooted_profiler.dll`
- `DOTNET_ReadyToRun=0` (required -- single-file R2R ignores `COR_PRF_DISABLE_ALL_NGEN_IMAGES`)

The profiler DLL (`uprooted_profiler.dll`, C/MSVC) implements `ICorProfilerCallback`. On `ModuleLoadFinished`, it enumerates TypeRefs, creates MemberRefs, and prepends IL to `<Main>$` that calls `Assembly.LoadFrom` + `CreateInstance("UprootedHook.Entry")` with try/catch.

**Method 2: Startup Hooks**

Requires patching Root.exe to change `"System.StartupHookProvider.IsSupported": false` to `true` (same-length byte replacement). Then set `DOTNET_STARTUP_HOOKS` to the UprootedHook.dll path.

Cleaner than profiler but breaks on Root updates (requires re-patching).

### Runtime file locations

| File | Path |
|---|---|
| Root.exe | `%LOCALAPPDATA%\Root\current\Root.exe` |
| Profile directory | `%LOCALAPPDATA%\Root Communications\Root\profile\default\` |
| WebRTC HTML | `...\profile\default\WebRtcBundle\index.html` |
| Sub-app HTML | `...\profile\default\RootApps\*\index.html` |
| Hook log | `...\profile\default\uprooted-hook.log` |
| Settings JSON | `...\profile\default\uprooted-settings.json` |
| Installed DLLs | `%LOCALAPPDATA%\Root\uprooted\` |

### Debugging

**C# hook:** Check `uprooted-hook.log` in the profile directory. The hook logs extensively with timestamps and categories. Logger swallows exceptions to avoid crashing Root.

**TypeScript layer:** Root's Chromium has no DevTools (`--remote-debugging-port` is not available). Errors display as a red banner at the top of the page (`#uprooted-error`). The settings panel plugin has a green debug overlay at the bottom (`#uprooted-debug`) when `DEBUG = true`.

Console output goes to DotNetBrowser's internal console (not accessible externally). The `nativeLog()` API function sends messages through the bridge to .NET logging.

---

## 7. Conventions & Invariants

### Architectural rules

1. **All Avalonia type access must go through `AvaloniaReflection`.** Never use `typeof()`, `Type.GetType()`, or direct assembly references for Avalonia types.

2. **The C# hook must never block the UI thread.** All heavy work runs on a background thread. UI mutations dispatch to `Dispatcher.UIThread` via `RunOnUIThread()`.

3. **The C# hook uses a single `Timer` for polling (200ms).** It does NOT use `MutationObserver` equivalents or event subscriptions on the visual tree. Only `PointerPressed` and `PointerEntered/Exited` events are subscribed on injected controls.

4. **Settings page detection is text-based.** The injector finds "APP SETTINGS" TextBlock as the anchor point. If Root renames this text, injection breaks.

5. **Bridge proxies must be installed before Root assigns bridge globals.** The deferred `Object.defineProperty` trap handles the case where Root sets them after page load.

6. **Plugin CSS is namespaced by plugin name.** `injectCss("plugin-{name}", css)` creates `<style id="uprooted-css-plugin-{name}">`. This prevents collisions and enables per-plugin removal.

7. **All injected DOM elements use `data-uprooted` attributes.** All injected Avalonia controls use `Control.Tag` strings starting with `"uprooted-"`. These are the cleanup handles.

### Code conventions

- **TypeScript:** Strict mode, ES modules, no default exports (except plugin definitions that use `satisfies UprootedPlugin`).
- **C#:** Nullable enabled, implicit usings, `internal` access for all non-entry classes. Namespace: `Uprooted` (except `Entry.cs` which is `UprootedHook` and `StartupHook` which is global).
- **Logging:** `[Uprooted]` prefix in browser console. `[Category]` prefix in C# log file.
- **Error handling:** Never throw from injected code. Catch and log. The C# Logger swallows its own exceptions. The TS preload shows a visible error banner on fatal errors.
- **One-time initialization:** Use interlocked CAS (`Interlocked.CompareExchange`) to prevent double execution (see `Entry.cs`).

### Styling invariants (C# content pages)

Root's settings page uses exact styling values. The C# `ContentPages` class replicates these:

| Element | Style |
|---|---|
| Card background | `#081408` |
| Card corner radius | 12 |
| Card border | `#19ffffff`, thickness 0.5 |
| Card inner padding | 24px all sides |
| Page container margin | 24, 24, 24, 0 |
| Title text | FontSize=14, Bold, `#fff2f2f2` |
| Label text | FontSize=13, Weight=450, `#a3f2f2f2` |
| Body text | FontSize=13, Normal, `#a3f2f2f2` |
| Accent green | `#2D7D46` (Uprooted brand) |
| Accent blue | `#3b6af8` (Root brand, used for decorative dots) |

### Threading model

- **C# hook:** Background thread for initialization. Timer callback dispatches to UI thread. `_injecting` interlocked flag prevents concurrent `CheckAndInject` calls. `_ourItemClicked` flag coordinates between `PointerPressed` handlers on custom items and the ListBox.
- **TypeScript:** Single-threaded (browser main thread). MutationObserver callbacks are debounced (80ms). Plugin `start()` is async-safe but called sequentially (`await` in loop).

---

## 8. Known Limitations & Risk Areas

### Fragile integrations

1. **Settings page layout discovery** depends on the "APP SETTINGS" TextBlock existing with exact text, a nav StackPanel with >= 8 children, and a multi-column Grid ancestor. Any Root UI restructuring breaks this.

2. **Bridge proxy installation** assumes Root sets `window.__nativeToWebRtc` and `window.__webRtcToNative` as simple property assignments. If Root uses `Object.freeze()`, `Proxy`, or symbols, the interception fails.

3. **CLR profiler injection** depends on Root's `<Main>$` method being JIT-compiled (requires `DOTNET_ReadyToRun=0`). If Root ships with AOT compilation enabled, the profiler approach fails entirely.

4. **CSS variable names** (`--rootsdk-*`) are hardcoded in theme definitions. Root may rename or remove variables in updates.

### Areas requiring extra care

1. **`AvaloniaReflection.cs`** (815 lines) -- The largest and most critical file. Changes here affect all UI operations. Every reflection call should handle null gracefully. Never assume a type or property exists.

2. **`SidebarInjector.cs`** (610 lines) -- Complex state machine with timer-based polling. Race conditions are possible between the timer callback, UI thread dispatch, and click event handlers. The `_injecting` interlocked flag and `_ourItemClicked` flag are the primary coordination mechanisms.

3. **`src/api/bridge.ts`** -- The Proxy trap intercepts ALL property accesses on bridge objects, not just method calls. Non-function properties are passed through without interception, but this means any future property-based API on the bridge will bypass plugins.

### Known bugs / incomplete features

1. **C# settings persistence is disabled.** `UprootedSettings.Load()` returns hardcoded defaults. `System.Text.Json` deserialization causes `MissingMethodException` in profiler-injected context. Workaround needed (manual JSON parsing, or loading from a different assembly context).

2. **TypeScript settings-panel plugin (`panel.ts`) has a debug overlay** that is always visible when `DEBUG = true` (line 25). This should be toggled off for production builds.

3. **Theme switching in the C# pages is display-only.** The Themes page shows available themes with "ACTIVE" badges but has no click handlers to actually switch themes.

4. **Plugin management is display-only.** The Plugins page lists plugins but cannot install, uninstall, or toggle plugins at runtime.

5. **Custom CSS injection from settings** is only available in the TypeScript layer (session-only). The C# page shows "will be available in a future update."

6. **Environment variables set by the profiler method affect ALL .NET apps**, not just Root. The profiler has a process name guard (`"Root"`) but the env vars are user-scoped and persistent.

### Security considerations

- Plugins have unrestricted access to Root's bridge and DOM. There is no sandboxing.
- The profiler DLL loads into Root's process with full trust. Malicious plugins could exfiltrate tokens, modify messages, or impersonate the user.
- The `--disable-web-security` flag on Root's Chromium means same-origin policy is not enforced. Injected scripts can access any origin.

---

## 9. Guidance for Future AI Contributors

### How to reason about changes

1. **Identify which layer the change belongs to.** C# hook changes affect native Avalonia UI. TypeScript changes affect browser-rendered content. They are independent codebases with independent build systems.

2. **Check if the change touches shared state.** The only shared state between layers is the settings JSON file and the visual result (both layers inject into the same app). They do not communicate at runtime.

3. **Trace the data flow before modifying it.** For C# changes: Entry -> StartupHook -> SidebarInjector -> ContentPages, with AvaloniaReflection and VisualTreeWalker as utilities. For TypeScript changes: preload -> pluginLoader -> plugins, with bridge/css/dom/native as APIs.

4. **Test against Root's actual behavior.** The hook log file (`uprooted-hook.log`) is the primary debugging tool for C# changes. The browser debug overlay and error banner are the primary tools for TypeScript changes.

### How to avoid over-refactoring

1. **Do not abstract the reflection layer further.** `AvaloniaReflection` is already the right level of abstraction. Adding a "UI framework" on top of it adds complexity without benefit.

2. **Do not introduce a dependency injection framework** for the C# hook. The codebase is small enough that constructor injection and static methods work fine.

3. **Do not add a state management library** (Redux, MobX) for the TypeScript layer. The `PluginLoader` and direct DOM manipulation are sufficient for the current scope.

4. **Do not replace the polling-based injector with event-driven detection** unless you have confirmed that Avalonia's visual tree fires reliable change events that can be subscribed via reflection. Previous attempts with `SelectionChanged` and `PropertyChanged` have failed due to RoutedEvent limitations.

### When to follow existing patterns vs. introduce new ones

**Follow existing patterns when:**
- Adding a new settings page (copy the pattern in `ContentPages.cs`)
- Adding a new plugin (copy the structure in `src/plugins/themes/`)
- Adding a new API function (add to appropriate file in `src/api/`)
- Adding new Avalonia control creation (add to `AvaloniaReflection`)

**Consider a new pattern when:**
- The existing approach has a demonstrated failure mode
- The change enables a fundamentally new capability (e.g., plugin hot-reloading)
- Multiple contributors have independently hit the same limitation

### How to verify correctness

1. **TypeScript:** Run `pnpm build` and confirm no errors. The build output should be a single IIFE bundle.

2. **C#:** Run `dotnet build hook/ -c Release` and confirm no errors. The output should be a single DLL.

3. **Integration:** Install into Root (`Install-Uprooted.ps1`), launch Root, open Settings, and verify:
   - "UPROOTED" section appears in the sidebar
   - All three pages (Uprooted, Plugins, Themes) render correctly
   - Clicking Root's sidebar items restores Root's content
   - Version text appears near Root's version
   - No errors in `uprooted-hook.log`

4. **Cleanup:** Verify that `Uninstall-Uprooted.ps1` fully removes all traces (env vars, DLLs, backups).

### Key files to read first when onboarding

In order of importance:
1. `src/types/plugin.ts` -- Understand the plugin contract
2. `src/core/preload.ts` -- Understand the TypeScript entry point
3. `hook/StartupHook.cs` -- Understand the C# entry point
4. `src/api/bridge.ts` -- Understand the bridge proxy mechanism
5. `hook/SidebarInjector.cs` -- Understand the native UI injection
6. `hook/AvaloniaReflection.cs` -- Understand the reflection constraints

### Things that will almost certainly break on Root updates

- Root renames "APP SETTINGS" text
- Root changes the settings page Grid column/row layout
- Root changes CSS variable names (`--rootsdk-*` prefix)
- Root moves to AOT compilation (breaks profiler injection)
- Root moves HTML files to a different profile path
- Root adds integrity checks to its HTML files
- Root switches from DotNetBrowser to a different embedded browser
- Root changes bridge global names (`__nativeToWebRtc`, `__webRtcToNative`)

---

## 10. Installer

The Uprooted Installer is a self-contained Tauri v2 application (`Uprooted.exe`) that manages the full install/uninstall lifecycle on Windows.

### What it does

1. **Deploys files** -- Extracts 5 embedded binaries to `%LOCALAPPDATA%\Root\uprooted\`:
   - `uprooted_profiler.dll` -- CLR profiler (native C DLL)
   - `UprootedHook.dll` + `UprootedHook.deps.json` -- Managed hook assembly
   - `uprooted-preload.js` + `uprooted.css` -- TypeScript injection payload
2. **Sets environment variables** -- Configures user-scoped CLR profiler env vars via the Windows registry:
   - `CORECLR_ENABLE_PROFILING=1`
   - `CORECLR_PROFILER={D1A6F5A0-1234-4567-89AB-CDEF01234567}`
   - `CORECLR_PROFILER_PATH=%LOCALAPPDATA%\Root\uprooted\uprooted_profiler.dll`
   - `DOTNET_ReadyToRun=0`
   - Broadcasts `WM_SETTINGCHANGE` so new processes see the updated env vars immediately
3. **Patches HTML** -- Injects `<script>` and `<link>` tags into Root's profile HTML files (WebRtcBundle + RootApps)
4. **Uninstall** -- Reverses all three steps: removes env vars, restores HTML backups, deletes deployed files
5. **Repair** -- Re-deploys files, re-sets env vars, re-patches HTML (handles Root updates that overwrite HTML)

### File locations

| Path | Content |
|------|---------|
| `%LOCALAPPDATA%\Root\uprooted\` | All deployed binaries (profiler, hook, JS, CSS) |
| `%LOCALAPPDATA%\Root\uprooted\profiler.log` | Profiler debug log (written at runtime) |
| `HKCU\Environment` | CLR profiler env vars (user-scoped, persistent) |
| Root profile `*.html` | Patched HTML files (with `.uprooted.bak` backups) |

### Build command

```powershell
powershell -File scripts/build_installer.ps1
```

This pipeline:
1. `pnpm build` -- TypeScript layer -> `dist/uprooted-preload.js` + `dist/uprooted.css`
2. `dotnet build hook/ -c Release` -- C# hook -> `UprootedHook.dll`
3. `cl.exe` via VS Build Tools -- Profiler -> `uprooted_profiler.dll`
4. Stages all 5 files to `installer/src-tauri/artifacts/`
5. `pnpm tauri build` -- Tauri app -> `installer/src-tauri/target/release/Uprooted Installer.exe`

### Frontend status display

The installer shows 5 status rows:
- **Root.exe** -- Whether the Root executable was found
- **Profile** -- Number of HTML files detected in the profile directory
- **Hook DLLs** -- Whether all 5 files are deployed to the install directory
- **Env Vars** -- Whether CLR profiler env vars are correctly set in the registry
- **HTML Patch** -- Whether the HTML injection markers are present

### Testing

Use `scripts/test_sandbox.wsb` to launch Windows Sandbox with the built .exe mapped in:
1. Install Root in the sandbox
2. Run `Uprooted Installer.exe` and click Install
3. Verify all 5 status rows turn green
4. Launch Root -- check for profiler log + hook sidebar injection
5. Click Uninstall -- verify everything reverts
