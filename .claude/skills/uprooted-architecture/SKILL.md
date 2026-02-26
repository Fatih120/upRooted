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

**Key files (40 .cs files total):**
- `StartupHook.cs` (738) — Entry point, orchestrates the multi-phase sequence
- `AvaloniaReflection.cs` (3487) — Reflection cache for ~54 Avalonia types (most critical file)
- `SidebarInjector.cs` (2048) — LayoutUpdated + timer-based UI injection state machine
- `ContentPages.cs` (5562) — Native Avalonia page builders + Dev Console
- `ThemeEngine.cs` (3080) — Resource-first theme engine v2, OKLCH palette
- `DotNetBrowserReflection.cs` (1926) — Reflection cache for DotNetBrowser types, IBrowser discovery
- `VisualTreeWalker.cs` (573) — Structural discovery (fragile, text-anchor based)
- `HtmlPatchVerifier.cs` (460) — Phase 0 self-healing patches
- `LinkEmbedEngine.cs` (2677) — Avalonia-native link embed engine (broadly functional)
- `RootcordEngine.cs` (6082) — Discord-style vertical sidebar (experimental)
- `MessageLogger.cs` (2238) — Edit/delete detection + visual indicators
- `TranslateEngine.cs` (2201) — Google Translate + DeepL translation
- `NsfwFilter.cs` (565) — Avalonia-native NSFW filter (visual tree scan + blur/reveal)
- `UprootedPresenceBeacon.cs` (527) — Uprooted user detection via gRPC
- `ReconLogger.cs` (788) — Visual tree + style diagnostic dumper

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

Full critical rules with code examples: [`docs/framework/ARCHITECTURE.md` § 9 Critical Rules](docs/framework/ARCHITECTURE.md#9-critical-rules).
Compact list: [`CLAUDE.md` § Critical Rules](CLAUDE.md#critical-rules).

The most important:

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
dotnet build hook/UprootedHook.csproj -c Release

# TypeScript (esbuild)
pnpm build

# Full installer pipeline (PowerShell)
powershell -File scripts/build-installer.ps1

# Console TUI installer
cd installer/src-tauri && cargo build --release
```

## Additional Resources

### Reference Files

For detailed patterns and rules, consult:
- **`references/csharp-patterns.md`** — C# hook coding patterns with code examples
- **`references/typescript-patterns.md`** — TypeScript injection patterns with code examples
- **`docs/framework/ARCHITECTURE.md` § 9** — Full critical rules with code examples (canonical source)
