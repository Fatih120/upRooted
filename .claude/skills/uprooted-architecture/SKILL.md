---
name: uprooted-architecture
description: This skill should be used when the user asks about "Uprooted architecture", "how injection works", "hook lifecycle", "bridge proxy", "Avalonia reflection", "plugin system", "sidebar injection", "visual tree", "content overlay pattern", "critical rules", "what not to do in the hook", "C# patterns", "TypeScript patterns", or when working on any file in hook/, src/, or installer/src-tauri/src/. Provides deep architectural knowledge and coding standards for the Uprooted client mod framework.
---

# Uprooted Architecture Knowledge

Uprooted is a dual-layer client modification framework for the Root Communications desktop app. Two independent injection layers operate in parallel targeting the same application.

## Dual-Layer Architecture

### Layer 1: C# .NET Hook (`hook/`)

Injects into Root's managed .NET 10/Avalonia process via CLR profiler. Adds native Avalonia controls to the settings page sidebar.

**Lifecycle (6 phases):**
1. **Phase 0** — Verify HTML patches on disk (filesystem only, no Avalonia). Self-heal via FileSystemWatcher.
2. **Phase 1** — Wait for Avalonia assemblies to load (30s timeout, poll 250ms).
3. **Phase 2** — Resolve all Avalonia types via reflection into `AvaloniaReflection` cache.
4. **Phase 3** — Wait for `Application.Current` then `MainWindow` (30s/60s timeouts).
5. **Phase 3.5** — Initialize ThemeEngine + apply saved theme.
6. **Phase 4** — Start `SidebarInjector` 200ms timer-based settings page monitor.
7. **Phase 5** — Background thread: DotNetBrowser discovery + JS injection (NsfwFilter, LinkEmbeds).

**Key files:**
- `StartupHook.cs` — Entry point, orchestrates the multi-phase sequence
- `AvaloniaReflection.cs` — 2029-line reflection cache for ~80 Avalonia types (most critical file)
- `SidebarInjector.cs` — 1090-line timer-based UI injection state machine
- `ContentPages.cs` — 2270-line native Avalonia page builders
- `ThemeEngine.cs` — 2218-line native theme application via resource dictionaries
- `DotNetBrowserReflection.cs` — 1914-line reflection cache for DotNetBrowser types, IBrowser discovery
- `VisualTreeWalker.cs` — 554-line structural discovery (fragile, text-anchor based)
- `HtmlPatchVerifier.cs` — Phase 0 self-healing patches
- `BrowserDiscovery.cs` — Phase 4.5 diagnostic scanner
- `LinkEmbedEngine.cs` — Avalonia-native link embed engine (Phase 4.5b, YouTube working, generic sites need fixes)
- `NsfwFilter.cs` — Content filter JS injection (needs Avalonia-native redesign)

### Layer 2: TypeScript Browser Injection (`src/`)

Injects into Root's embedded Chromium via HTML `<script>` tags. Provides the plugin/theme runtime and bridge proxy system.

**Lifecycle:**
1. Patcher inserts `<script>` and `<link>` tags into profile HTML files.
2. `preload.ts` runs before Root's bundles.
3. Reads settings from `window.__UPROOTED_SETTINGS__`.
4. Installs ES6 Proxy wrappers on bridge globals.
5. Creates `PluginLoader`, registers built-in plugins, starts enabled ones.

**Key files:**
- `src/core/preload.ts` — Entry point, bootstrap
- `src/core/pluginLoader.ts` — Plugin registry, lifecycle, event routing
- `src/api/bridge.ts` — ES6 Proxy bridge interception
- `src/api/css.ts`, `dom.ts`, `native.ts` — Plugin API modules
- `src/plugins/` — Built-in plugins (sentry-blocker, themes, settings-panel)

## Critical Rules

Before modifying any code, consult `references/critical-rules.md` for the full list of patterns that cause real bugs. The most important:

1. **Never use `Type.GetType()` for Avalonia types** — use `AvaloniaReflection`
2. **Never modify `ContentControl.Content` directly** — causes UI freeze
3. **Never use `System.Text.Json` in hook** — causes `MissingMethodException`
4. **Never use `EventInfo.AddEventHandler` for RoutedEvents** — use Expression lambdas
5. **Never use localStorage** — Root runs Chromium with `--incognito`
6. **`DispatcherPriority` is a struct not enum** in Avalonia 11+

## Coding Patterns

### C# Hook Patterns

For detailed C# patterns with code examples, consult `references/csharp-patterns.md`. Key patterns:

- **Reflection access**: ALL Avalonia types through `AvaloniaReflection` singleton
- **One-time init**: `Interlocked.CompareExchange` CAS guard (never `lock` or `bool` flag)
- **UI thread dispatch**: Background thread for heavy work, timer dispatches to `Dispatcher.UIThread`
- **Content overlay**: Add Uprooted page as Grid sibling with opaque background (never modify Content)
- **Tag identification**: `Control.Tag` strings starting with `"uprooted-"` for all injected controls
- **Error handling**: Never throw from injected code — catch and log. Logger swallows own exceptions.

### TypeScript Patterns

For detailed TypeScript patterns with code examples, consult `references/typescript-patterns.md`. Key patterns:

- **Plugin definition**: `export default { ... } satisfies UprootedPlugin`
- **Bridge interception**: ES6 Proxy wraps globals, `Object.defineProperty` traps for deferred installation
- **Settings persistence**: File-based JSON (no localStorage), inlined as `window.__UPROOTED_SETTINGS__`
- **Logging**: All logs prefixed with `[Uprooted]` or `[Uprooted:plugin-name]`
- **DOM utilities**: TreeWalker text matching, 80ms MutationObserver debounce, `data-uprooted` attributes

## Naming Conventions

- **Files**: camelCase for modules (`pluginLoader.ts`), kebab-case for directories (`settings-panel/`)
- **Functions**: camelCase, verb-based (`loadSettings()`, `injectCss()`)
- **Variables**: camelCase, descriptive (`pluginSettings` not `ps`)
- **Constants**: UPPER_SNAKE_CASE (`BACKUP_SUFFIX`, `ID_PREFIX`)
- **Types/Interfaces**: PascalCase (`UprootedPlugin`, `BridgeEvent`)
- **C# classes**: PascalCase, `internal` access, `Uprooted` namespace (except `StartupHook` which is global)

## Known Fragile Areas

- **AvaloniaReflection.cs** — Largest, most critical. Avalonia API changes break entire C# layer.
- **VisualTreeWalker.cs** — Text-based anchor ("APP SETTINGS") is fragile to Root UI updates.
- **SidebarInjector.cs** — Complex state machine, race conditions possible between timer/UI/events.
- **Bridge proxy installation** — `Object.defineProperty` deferred traps, complex descriptor interaction.
- **Plugin lifecycle** — No concurrent operation guards, rapid start/stop causes race conditions.
- **Settings merge** — Shallow object spread loses nested plugin configs.

## Build Commands

```bash
# C# hook
dotnet build hook/ -c Release

# TypeScript (esbuild)
pnpm build

# Full installer pipeline (PowerShell)
powershell -File scripts/build_installer.ps1

# Console TUI installer
cd installer/src-tauri && cargo build --release
```

## Additional Resources

### Reference Files

For detailed patterns and rules, consult:
- **`references/critical-rules.md`** — Complete list of forbidden patterns with explanations and correct alternatives
- **`references/csharp-patterns.md`** — C# hook coding patterns with code examples
- **`references/typescript-patterns.md`** — TypeScript injection patterns with code examples
