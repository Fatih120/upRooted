# Planning Reference

Reference index for the `.planning/codebase/` automated analysis files.

> **Related docs:** [Roadmap](../ROADMAP.md) | [Architecture](../framework/ARCHITECTURE.md) | [Contributing Technical](CONTRIBUTING_TECHNICAL.md) | [Index](../INDEX.md)

---

## Overview

The `.planning/codebase/` directory contains seven automated analysis files generated on 2026-02-16 using the `/gsd:map-codebase` command. These files provide a machine-generated snapshot of the codebase's architecture, conventions, concerns, integrations, technology stack, file structure, and testing status. Together they total 1,676 lines across all seven documents.

These files are committed to git and serve as a baseline for planning work, onboarding new contributors, and tracking technical debt. They are not manually maintained -- regenerate them when the codebase changes significantly.

---

## File Summary

| File | Lines | Key Topics | Most Actionable Finding |
|------|------:|------------|------------------------|
| `ARCHITECTURE.md` | 356 | Dual-layer injection, data flow, key abstractions, entry points | Content overlay pattern (never modify ContentControl.Content) |
| `CONCERNS.md` | 248 | Tech debt, known bugs, security, performance, fragile areas | System.Text.Json broken in profiler context blocks C# settings |
| `CONVENTIONS.md` | 179 | Naming, code style, imports, error handling, C# rules | No linter/formatter configured; relies on TypeScript strict mode |
| `INTEGRATIONS.md` | 205 | External services, data storage, CI/CD, native system access | Sub-app bridge (rootSdkBridge) is NOT proxied by Uprooted |
| `STACK.md` | 149 | Languages, runtimes, frameworks, build pipeline, platforms | Two injection methods: CLR profiler (default) vs startup hooks |
| `STRUCTURE.md` | 305 | Directory layout, monorepo packages, where to add new code | Installer build pipeline has 5 stages orchestrated by PowerShell |
| `TESTING.md` | 234 | Test gaps, recommended framework, mocking strategy, priorities | Zero TypeScript test coverage; no test framework configured |

---

## Architecture Analysis

ARCHITECTURE.md maps the full dual-layer system across 15 identified layers. Key findings:

- **C# layer** has 7 sub-layers: CLR profiler, hook entry, Avalonia reflection, visual tree walker, sidebar injector, content pages, and settings. The reflection cache (`AvaloniaReflection.cs`, 815 lines) is the single most critical file.
- **TypeScript layer** has 8 sub-layers: installation, preload runtime, plugin management, bridge interception, plugin API, built-in plugins, installer app, and settings persistence.
- **Bridge event flow** documents the full ES6 Proxy interception chain, noting that `replace` takes priority over `before` and that `after` is defined but not yet implemented.
- **Content overlay pattern** is documented as the correct way to display Uprooted pages -- add as Grid sibling with opaque background, never modify ContentControl.Content directly.

---

## Concerns and Technical Debt

CONCERNS.md identifies 6 categories of issues. Current status against ROADMAP.md:

**Promoted to Roadmap (tracked):**
- System.Text.Json broken in profiler context -- Critical, workaround in progress
- Environment variables affect all .NET apps -- Critical, process guard mitigates
- AvaloniaReflection brittleness -- High severity
- Settings page text-based detection -- High severity
- Settings panel DOM discovery fragility -- Medium
- `after` patch handler not implemented -- Medium, short-term goal
- Browser-side settings not persisted to disk -- Medium
- Shallow settings merge -- Medium, short-term goal

**Not yet on Roadmap (still in CONCERNS.md only):**
- Non-null assertion casts in sentry-blocker fetch wrapping
- `any[]` typing for XMLHttpRequest parameters
- XHR redirect to about:blank (semantically wrong)
- MutationObserver circular triggering from own DOM changes
- Theme plugin variable cleanup duplication
- Plugin lifecycle async race conditions (no starting/stopping guards)
- Event listener memory leaks on repeated settings page visits
- Window global exposure (`__UPROOTED_LOADER__`, `__UPROOTED_SETTINGS__`)

---

## Conventions

CONVENTIONS.md documents naming and style patterns across both layers:

- **TypeScript**: camelCase functions/variables, PascalCase types, UPPER_SNAKE_CASE constants, kebab-case plugin directories. Imports use explicit `.js` extensions (ES modules). `import type` for type-only imports.
- **C#**: `internal` access by default, `Interlocked.CompareExchange` for one-time init guards, all Avalonia access through `AvaloniaReflection`, never throw from injected code.
- **No formatter or linter configured** -- code style relies on TypeScript strict mode and manual review. 2-space indentation, semicolons used consistently.
- **Logging**: `[Uprooted]` prefix in browser console, `[Category]` prefix in C# file logs.

---

## Integration Points

INTEGRATIONS.md catalogs external touchpoints:

- **Sentry.io**: Actively blocked by sentry-blocker plugin (fetch, XHR, sendBeacon).
- **Root WebRTC bridge**: Two bridge objects proxied (`__nativeToWebRtc` with 42 methods, `__webRtcToNative` with 29 methods). Only available during voice sessions.
- **Root sub-app bridge**: NOT proxied. Seven embedded sub-apps (Hexatris, Polls, Suggestions, Minecraft Easy Setup, Task Tracker, Stickerwall, Raid Planner) use a separate protobuf bridge that Uprooted does not intercept.
- **Windows registry**: Installer reads Root's install path and writes CLR profiler environment variables.
- **CI/CD**: GitHub Actions with two workflows (`build-installer.yml`, `build-linux.yml`).

---

## Technology Stack

STACK.md inventories all languages, runtimes, and tools:

- **Languages**: TypeScript 5.7+, Rust 2021, C# (.NET 10), C (profiler DLL), PowerShell, Python, Bash
- **Runtimes**: Node.js 20+, Rust stable, .NET 10, WebKit2GTK 4.1 (Linux)
- **Package manager**: pnpm 10 (monorepo with 3 packages: root, installer, site)
- **Build tools**: esbuild (preload bundle), Vite (installer UI), Astro (site), Tauri CLI
- **Two injection methods**: CLR profiler (default, survives Root updates) and startup hooks (requires binary patching, breaks on updates)

---

## Project Structure

STRUCTURE.md provides the full directory tree and guidance for adding new code:

- **Monorepo**: Root package (core library + scripts), `installer/` (Tauri desktop app), `site/` (Astro marketing site)
- **Build pipeline**: 5 stages -- pnpm build (TS), dotnet build (C# hook), cl.exe (profiler DLL), stage artifacts, tauri build (installer)
- **Special directories**: `dist/` (build output, not committed), `.planning/codebase/` (analysis, committed), `legacy/` (archived), `research/` (dev workspaces)
- **New plugin checklist**: Create `src/plugins/{name}/index.ts`, export default satisfying `UprootedPlugin`, register in `preload.ts`

---

## Testing Status

TESTING.md reveals the most significant gap in the project:

- **TypeScript layer**: Zero test coverage. No test framework, no test files, no test scripts. No `jest`, `vitest`, or `@testing-library` dependencies.
- **C# layer**: A manual test harness exists in `hook-test/` plus unit tests in `tests/UprootedTests/` (ColorUtils, GradientBrush). No automated integration tests.
- **Recommended framework**: Vitest (native ESM support, fast, minimal config).
- **High-priority test targets**: PluginLoader lifecycle, settings persistence/merge, bridge proxy interception, sentry-blocker network blocking.
- **Coverage goals (recommended)**: 60% statements, 50% branches, 70% functions.

---

## Roadmap Integration

The following table shows which `.planning/codebase/` findings have been promoted to `docs/ROADMAP.md` and which remain untracked:

| Finding | Source File | On Roadmap? | Roadmap Section |
|---------|-------------|-------------|-----------------|
| System.Text.Json broken | CONCERNS.md | Yes | Known Issues > Critical |
| Env vars affect all .NET apps | CONCERNS.md | Yes | Known Issues > Critical |
| AvaloniaReflection brittleness | CONCERNS.md | Yes | Known Issues > High |
| Text-based settings detection | CONCERNS.md | Yes | Known Issues > High |
| `after` handler not implemented | CONCERNS.md | Yes | Short-term Goals |
| Shallow settings merge | CONCERNS.md | Yes | Short-term Goals |
| Plugin lifecycle race conditions | CONCERNS.md | Yes | Medium-term Goals |
| Bridge version detection | CONCERNS.md | Yes | Medium-term Goals |
| Sentry blocker hardening | CONCERNS.md | Yes | Long-term Vision |
| No TypeScript test coverage | TESTING.md | No | -- |
| No linter/formatter config | CONVENTIONS.md | No | -- |
| Sub-app bridge not proxied | INTEGRATIONS.md | No | -- |
| Window global exposure risk | CONCERNS.md | No | -- |
| Event listener memory leaks | CONCERNS.md | No | -- |
| MutationObserver circular trigger | CONCERNS.md | No | -- |
| Theme variable cleanup duplication | CONCERNS.md | No | -- |

---

## Using These Files

**When to reference `.planning/codebase/` files:**

- **Planning a new feature**: Read ARCHITECTURE.md for layer boundaries, STRUCTURE.md for where to add code, CONVENTIONS.md for style expectations.
- **Investigating a bug**: Check CONCERNS.md for known fragile areas and existing bugs that may be related.
- **Evaluating technical debt**: CONCERNS.md and TESTING.md together give a complete picture of risk areas.
- **Onboarding**: Read all seven files in order (STACK, STRUCTURE, ARCHITECTURE, CONVENTIONS, INTEGRATIONS, CONCERNS, TESTING) for a full codebase orientation.
- **Preparing a release**: Cross-reference CONCERNS.md against ROADMAP.md to ensure critical issues are tracked.

**When to regenerate**: After major architectural changes, new layer additions, or significant refactoring. The analysis date at the bottom of each file indicates when it was last generated.

---

*Last updated: 2026-02-16*
