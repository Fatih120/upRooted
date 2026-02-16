# uprooted-dev — Claude Code Plugin

> Development workflow, standards enforcement, architecture knowledge, and builder references for the Uprooted project.

**Version:** 0.2.0 | **Author:** agomusio

---

## What This Plugin Does

This plugin makes Claude Code an effective contributor to the Uprooted codebase. It provides four capabilities:

1. **Guard Rails** — A pre-write hook automatically blocks code that violates critical rules (no `Type.GetType()`, no `System.Text.Json`, no `localStorage`, etc.) before it reaches disk.
2. **Architecture Knowledge** — A skill that surfaces Uprooted's dual-layer architecture, lifecycle, coding patterns, and fragile areas whenever Claude works on relevant files.
3. **Builder References** — A skill that provides templates, API surfaces, and registration steps for building new TypeScript plugins, C# hook features, bridge patches, and themes.
4. **Workflow Commands** — Slash commands for building the hook, building the full installer, watching logs, and reviewing code.

---

## Quick Reference

| Component | Trigger | What It Does |
|-----------|---------|-------------|
| `/uproot:build-hook` | Slash command | `dotnet build hook/ -c Release` |
| `/uproot:build-installer` | Slash command | Full pipeline: TS + C# + Tauri |
| `/uproot:watch-log` | Slash command | Tail `uprooted-hook.log` |
| Critical rules hook | Automatic on Write/Edit | Blocks 7 forbidden patterns |
| Architecture skill | Auto on `hook/`, `src/`, architecture questions | Surfaces patterns, rules, lifecycle |
| Builder skill | "build a plugin", "new feature", "create theme" | Templates, APIs, registration steps |
| Reviewer agent | "review my changes", "check for violations" | Full code review against all rules |

---

## Plugin Structure

```
.claude/plugins/uprooted-dev/
├── .claude-plugin/
│   └── plugin.json                          # Manifest (name, version, author)
├── hooks/
│   └── hooks.json                           # PreToolUse guard: blocks critical rule violations
├── commands/
│   ├── build-hook.md                        # /uproot:build-hook
│   ├── build-installer.md                   # /uproot:build-installer
│   └── watch-log.md                         # /uproot:watch-log
├── agents/
│   └── uprooted-reviewer.md                 # Code review agent (sonnet, on-demand)
├── skills/
│   ├── uprooted-architecture/               # Understanding the codebase
│   │   ├── SKILL.md                         # Dual-layer architecture, lifecycle, fragile areas
│   │   └── references/
│   │       ├── critical-rules.md            # 6 rules that cause real bugs
│   │       ├── csharp-patterns.md           # Reflection, CAS guards, overlay, tags, logging
│   │       └── typescript-patterns.md       # Plugin def, bridge proxy, settings, DOM, CSS
│   └── uprooted-builder/                    # Creating new components
│       ├── SKILL.md                         # Decision tree, file locations, registration
│       └── references/
│           ├── ts-plugin-reference.md       # UprootedPlugin interface, all APIs, templates
│           ├── hook-feature-reference.md    # AvaloniaReflection API, settings pages, styling
│           ├── bridge-methods.md            # 42+29 bridge method signatures, patch patterns
│           └── theme-building.md            # CSS variables, native color maps, dual-layer
└── README.md                                # This file
```

---

## Components In Detail

### 1. Critical Rules Hook

**File:** `hooks/hooks.json`
**Event:** `PreToolUse` on `Write` and `Edit`
**Type:** Prompt-based (15s timeout)

Every time Claude writes or edits a file, this hook inspects the content for 7 forbidden patterns:

| # | Pattern | Layer | Why It's Forbidden |
|---|---------|-------|--------------------|
| 1 | `Type.GetType(` | C# | Returns null in single-file .NET apps — use `AvaloniaReflection` |
| 2 | `typeof(Avalonia...` | C# | Can't resolve embedded assembly types — use `AvaloniaReflection` |
| 3 | `System.Text.Json` / `JsonSerializer` | C# | `MissingMethodException` in profiler context |
| 4 | `.AddEventHandler(` on RoutedEvents | C# | Silently fails — use `Expression.Lambda` |
| 5 | `.Content =` on ContentControl | C# | UI freeze from `OnDetachedFromVisualTreeCore` loop |
| 6 | `Enum.Parse` for DispatcherPriority | C# | It's a struct in Avalonia 11+, not an enum |
| 7 | `localStorage.` | TypeScript | Root runs `--incognito`, data cleared on every launch |

If a violation is detected, the hook responds with `BLOCK: [rule] [explanation]` and the write is prevented. Clean code gets `APPROVE`.

### 2. Architecture Skill

**File:** `skills/uprooted-architecture/SKILL.md`
**Activates on:** Architecture questions, working on `hook/`, `src/`, or `installer/` files

Provides Claude with deep knowledge of:

- **Dual-layer architecture** — C# .NET hook (Avalonia reflection) + TypeScript injection (Chromium bridge proxy)
- **5-phase startup lifecycle** — Phase 0 (HTML patches) through Phase 4 (sidebar injection)
- **Critical rules** — The 6 forbidden patterns with correct alternatives and code examples
- **C# patterns** — Reflection access, CAS guards, UI thread dispatch, content overlay, tag identification, phase waits, error handling, assembly scanning
- **TypeScript patterns** — Plugin definition, bridge interception, patch priority, plugin lifecycle, settings, logging, DOM utilities, CSS injection, imports, error handling
- **Fragile areas** — AvaloniaReflection, VisualTreeWalker, SidebarInjector, bridge proxy, plugin lifecycle, settings merge
- **Naming conventions** — camelCase, PascalCase, UPPER_SNAKE_CASE rules per layer

### 3. Builder Skill

**File:** `skills/uprooted-builder/SKILL.md`
**Activates on:** "build a plugin", "new feature", "create theme", "scaffold", "intercept bridge"

Routes to the correct reference based on what you're building:

#### TypeScript Plugin Reference (`references/ts-plugin-reference.md`, 320 lines)

- Full `UprootedPlugin` interface with all fields
- All 4 `SettingField` types (boolean, string, number, select)
- **CSS API**: `injectCss`, `removeCss`, `removeAllCss`
- **DOM API**: `waitForElement`, `observe`, `nextFrame`
- **Native API**: `getCurrentTheme`, `setCssVariable(s)`, `removeCssVariable`, `nativeLog`
- **Patch interface**: `before`/`replace`/`after` with execution priority
- Registration steps (import in preload.ts, `loader.register()`)
- Minimal template + full-featured template with lifecycle, patches, CSS, settings
- Debugging techniques (nativeLog, DOM indicators, error banners)
- Environment constraints (no localStorage, no dynamic imports, no DevTools, ES2022)

#### C# Hook Feature Reference (`references/hook-feature-reference.md`, 333 lines)

- Complete `AvaloniaReflection` API: 15 creation methods, 30+ property setters, event subscription, resource management
- How to add a new settings page: BuildPage method, switch case, SidebarInjector nav item
- Styling constants: card BG, corner radius, border, font sizes, margins (all with theme-aware values)
- Tag-based identification (`uprooted-*` prefix convention)
- UI thread dispatch patterns
- All 5 critical rules with correct alternatives
- Error handling (never throw, catch and Logger.Log)
- File structure conventions (internal classes, Uprooted namespace, zero NuGet deps)

#### Bridge Methods Reference (`references/bridge-methods.md`, 285 lines)

- Type aliases (`UserGuid`, `DeviceGuid`, `Theme`, `TileType`, `ScreenQualityMode`)
- `InitializeDesktopWebRtcPayload` fields
- **42 `INativeToWebRtc` methods** grouped by: Connection, Media, Devices, User State, Audio/Quality, UI, Moderation, Volume, Packets/Native Audio
- **29 `IWebRtcToNative` methods** grouped by: Session, Local Tracks, Remote Tracks, User State, Moderation, Profile/UI, Logging
- `BridgeEvent` interface
- 5 common patterns: observe, cancel, replace, multi-method monitor, modify arguments

#### Theme Building Reference (`references/theme-building.md`, 278 lines)

- All 25 `--rootsdk-*` CSS override variables with defaults and categories
- How the override mechanism works (`var(--rootsdk-*, fallback)`)
- Adding preset themes to `themes.json`
- Programmatic theming via `setCssVariables()` / `removeCssVariable()`
- Custom theme generation algorithm (`generateCustomVariables(accent, bg)`)
- C# ThemeEngine: ARGB color map structure, 55 resource palette keys
- Two-phase application (resource injection + visual tree walk)
- Live preview pattern (brush caching, 16ms throttle)
- Multi-phase cleanup (disable map, restore resources, remove dict, targeted purge, follow-up walks)

### 4. Slash Commands

#### `/uproot:build-hook`
Builds the C# hook DLL in Release configuration.
```
dotnet build hook/ -c Release
→ hook/bin/Release/net10.0/UprootedHook.dll
```
Reports success/failure, identifies errors, checks output DLL exists.

#### `/uproot:build-installer`
Runs the full build pipeline via `scripts/build_installer.ps1`:
1. TypeScript build (esbuild → `dist/`)
2. C# hook build (dotnet → `UprootedHook.dll`)
3. Profiler DLL compile (MSVC → `uprooted_profiler.dll`)
4. Stage artifacts to `installer/src-tauri/artifacts/`
5. Tauri build (cargo → final installer)

Also supports partial builds for individual components.

#### `/uproot:watch-log`
Tails the hook log file for real-time debugging. Locates the log at Root's profile directory (Windows or Linux) and runs `tail -f`. Documents log format, categories, and diagnostic tips.

### 5. Reviewer Agent

**File:** `agents/uprooted-reviewer.md`
**Model:** Sonnet (fast, cost-effective for review tasks)
**Trigger:** "review my changes", "check for violations", "validate before committing"

Performs a 6-step review:

1. **Identify changed files** — `git diff`, `git status`
2. **Read changed content** — Focus on diffs for large files
3. **Check critical rules** — All 7 forbidden patterns (C# and TypeScript)
4. **Check conventions** — Naming, access modifiers, namespaces, error handling, logging
5. **Check commit message** — Format compliance (`type: description`)
6. **Report findings** — Organized by severity: CRITICAL / WARNING / INFO

Each finding includes file path, line number, what's wrong, why, and the correct approach.

---

## How Skills Work Together

The **architecture** and **builder** skills are complementary:

| Scenario | Architecture Skill | Builder Skill |
|----------|-------------------|--------------|
| "How does the bridge proxy work?" | Explains the ES6 Proxy pattern, deferred installation | — |
| "Build a plugin that intercepts setMute" | — | Provides Patch interface, template, registration steps |
| "Add a new settings page to the hook" | Explains content overlay pattern, tag system | Provides AvaloniaReflection API, BuildPage pattern, styling constants |
| "Why does my theme look wrong?" | Explains dual-layer architecture, known fragile areas | Provides CSS variable list, color map format, cleanup pattern |

The **hook** guards against mistakes in real-time, while the **reviewer** catches subtler issues on demand.

---

## Adding to This Plugin

### New slash command

Create `commands/<name>.md` with YAML frontmatter:
```yaml
---
name: <name>
description: What the command does
allowed-tools:
  - Bash
  - Read
---
```
Invoked as `/uproot:<name>`.

### New skill reference

Add a `.md` file under the appropriate `skills/*/references/` directory and link to it from the parent `SKILL.md`.

### New hook rule

Edit `hooks/hooks.json` to add patterns to the prompt-based PreToolUse hook, or add new hook events (PostToolUse, Stop, etc.).

### New agent

Create `agents/<name>.md` with YAML frontmatter specifying model, tools, color, and trigger conditions.
