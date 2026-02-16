---
name: Uprooted Builder
description: >-
  This skill should be used when the user asks to "build a plugin", "create a plugin",
  "new plugin", "scaffold plugin", "add a feature", "hook feature", "new settings page",
  "build a theme", "create a theme", "intercept bridge", "patch bridge method",
  "add a nav item", "scaffold a hook feature", "new content page",
  or when the user wants to generate new code for any Uprooted component.
  Provides templates, API references, and registration steps for building
  TypeScript plugins, C# hook features, bridge patches, and themes.
version: 0.1.0
---

# Uprooted Builder

Build new components for the Uprooted framework. This skill provides templates, API surfaces, and registration steps for each component type.

## What Are You Building?

| Goal | Reference File | Key Steps |
|------|---------------|-----------|
| TypeScript plugin | `references/ts-plugin-reference.md` | Define plugin object, register in preload.ts |
| C# hook feature | `references/hook-feature-reference.md` | Create class, add page in ContentPages.cs, wire nav in SidebarInjector.cs |
| Bridge method patch | `references/bridge-methods.md` | Add `patches` array to plugin, target bridge + method |
| Theme (CSS or native) | `references/theme-building.md` | Override `--rootsdk-*` variables and/or define color map |

## File Locations

### TypeScript Layer (`src/`)

| Component | Where to Create | Register In |
|-----------|----------------|-------------|
| New plugin | `src/plugins/<name>/index.ts` | `src/core/preload.ts` — import + `loader.register()` |
| API extension | `src/api/<module>.ts` | `src/api/index.ts` barrel export |
| New type | `src/types/<name>.ts` | Import where needed |
| Theme preset | Add entry to `src/plugins/themes/themes.json` | Auto-discovered by themes plugin |

### C# Hook Layer (`hook/`)

| Component | Where to Create | Register In |
|-----------|----------------|-------------|
| New feature class | `hook/<FeatureName>.cs` | Instantiate in `StartupHook.cs` Phase 4 |
| New settings page | Method in `hook/ContentPages.cs` | Add case in `BuildPage()` switch + nav item in `SidebarInjector.cs` |
| New utility | `hook/<UtilName>.cs` | Import in consuming class |

## Registration Steps

### Adding a TypeScript Plugin

1. Create `src/plugins/<name>/index.ts` with `export default { ... } satisfies UprootedPlugin`
2. In `src/core/preload.ts`, add: `import <name> from "../plugins/<name>/index.js";`
3. In the same file, add: `loader.register(<name>);` after the other register calls
4. Build: `pnpm build`

### Adding a C# Settings Page

1. Add a private static method `Build<Name>Page(...)` in `ContentPages.cs`
2. Add a case to the `BuildPage()` switch: `"<name>" => Build<Name>Page(r, settings, nativeFontFamily, themeEngine),`
3. In `SidebarInjector.cs`, add a nav item in the injection method (matching the existing pattern for "uprooted", "plugins", "themes")
4. Build: `dotnet build hook/ -c Release`

## Build & Test

```bash
# TypeScript layer
pnpm build                           # esbuild IIFE bundle -> dist/

# C# hook
dotnet build hook/ -c Release        # -> hook/bin/Release/net10.0/UprootedHook.dll

# Full installer (embeds both)
powershell -File scripts/build_installer.ps1

# Run C# tests
dotnet test tests/UprootedTests/
```

## Quick Checklist

Before building any component, verify:

- [ ] Read `references/` file for your component type
- [ ] Follow naming conventions (camelCase TS files, PascalCase C# classes)
- [ ] Use `AvaloniaReflection` for ALL Avalonia types in C# (never `Type.GetType()`)
- [ ] No `localStorage` in TypeScript (Root runs `--incognito`)
- [ ] No `System.Text.Json` in C# hook
- [ ] Wrap all code in try/catch — never throw from injected code
- [ ] Clean up in `stop()` (TS) or cleanup methods (C#)
- [ ] Tag all injected Avalonia controls with `uprooted-*` prefix
