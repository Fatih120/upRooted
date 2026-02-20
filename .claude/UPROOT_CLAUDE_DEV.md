# Uprooted Claude Code Dev Tools

Project-specific Claude Code customizations for the Uprooted repository. These live directly in `.claude/` and provide commands, skills, agents, and hooks tailored to the dual-layer injection codebase.

## Structure

```
.claude/
├── UPROOT_CLAUDE_DEV.md        # This file
├── settings.json               # Claude Code settings
├── hooks.json                  # PreToolUse guard + Stop auto-review
├── commands/                   # Slash commands
│   ├── hi.md                   # /hi — onboard into the codebase
│   ├── ok.md                   # /ok — post-work documentation sweep
│   ├── build-hook.md           # /build-hook — dotnet build hook/ -c Release
│   ├── build-installer.md      # /build-installer — full pipeline build
│   ├── deploy.md               # /deploy — build hook + deploy to local Root
│   ├── inject.md               # /inject — build + close Root + deploy + relaunch (Windows only)
│   ├── watch-log.md            # /watch-log — tail the hook log file
│   ├── diagnose.md             # /diagnose — check installation health
│   ├── session-state.md        # /session-state — update SESSION_STATE.md + NEW-SESSION.md
│   ├── nose.md                 # /nose <ver> — bump all version references
│   ├── release.md              # /release <ver> — full release pipeline (doc sweep + version bump + build + ship)
│   └── decomp.md               # /decomp — search ILSpy dumps for info relevant to current issue
├── agents/                     # Specialized subagents
│   └── uprooted-reviewer.md   # Code reviewer (critical rules, conventions)
└── skills/                     # Knowledge skills (auto-loaded on relevant queries)
    ├── uprooted-architecture/  # Architecture, injection lifecycle, coding patterns
    │   ├── SKILL.md
    │   └── references/
    │       ├── critical-rules.md
    │       ├── csharp-patterns.md
    │       └── typescript-patterns.md
    └── uprooted-builder/       # Templates and registration steps for new components
        ├── SKILL.md
        └── references/
            ├── ts-plugin-reference.md
            ├── hook-feature-reference.md
            ├── bridge-methods.md
            └── theme-building.md
```

## Components

### Hooks

**File:** `hooks.json`

Two hooks are configured:

**1. PreToolUse — Critical Rule Guard**

A prompt hook on `Write|Edit` that blocks code changes violating Uprooted's critical rules before they land. Catches:

| # | Pattern | Why it's blocked |
|---|---------|-----------------|
| 1 | `Type.GetType(` in C# | Fails silently in single-file .NET — use `AvaloniaReflection` |
| 2 | `typeof(Avalonia` in C# | Same reason — use reflection cache |
| 3 | `System.Text.Json` / `JsonSerializer` in C# | `MissingMethodException` in profiler context |
| 4 | `.AddEventHandler(` on RoutedEvents | Incompatible with Avalonia — use `Expression.Lambda` |
| 5 | `.Content =` on ContentControl | Freezes UI permanently — use Grid overlay |
| 6 | `Enum.Parse` for `DispatcherPriority` | It's a struct, not enum in Avalonia 11+ |
| 7 | `localStorage.` in TypeScript | Root runs `--incognito`, data is wiped every launch |

Responds `APPROVE` or `BLOCK: [rule] [explanation]`. 15s timeout.

**2. Stop — Auto-Review Reminder**

A prompt hook on `*` that fires when Claude is about to stop. If source files in `hook/` or `src/` were edited during the session and no code review was run (via `uprooted-reviewer`), it blocks and reminds to review before finishing. Skips if the user explicitly opted out. 20s timeout.

### Commands

| Command | Purpose | When to use |
|---------|---------|-------------|
| `/hi` | Two-phase onboard: orient in codebase, then ask what's planned and deep-read all relevant docs | Start of a new session (first time) |
| `/ok` | **Doc sweep:** update session state, changelog, tasks, NEW-SESSION, CLAUDE.md to match committed code | After committing and pushing work |
| `/build-hook` | `dotnet build hook/ -c Release` | Quick build check after C# changes |
| `/build-installer` | Console TUI installer (`cargo build --release`) | After modifying installer or before release |
| `/deploy` | Build C# hook + deploy to local Root installation | Core dev loop: edit → build → deploy → test |
| `/inject` | Build + close Root + deploy + relaunch (Windows only) | Native Windows: full automatic cycle |
| `/watch-log` | Tail `uprooted-hook.log` | Debugging hook behavior at runtime |
| `/diagnose` | Check installation health: files, env vars, HTML patches, settings, log | When something isn't working after deployment |
| `/session-state` | Update `SESSION_STATE.md` + `NEW-SESSION.md` from git history | After a work cycle, before ending session |
| `/nose <ver>` | Bump all version refs (with content freshness audit) | Release prep |
| `/release <ver>` | Full release pipeline: doc sweep + version bump + build + changelog audit + commit + tag + push (two user gates) | Major/minor releases |
| `/decomp` | Search ILSpy decompilation dumps for classes/controls/styles relevant to the current issue | Investigating Root internals for a feature or fix |

**Typical workflows:**

- **Session start:** `/hi` to orient in the codebase
- **Dev loop:** edit code → `/deploy` → launch Root → `/watch-log`
- **Debug:** `/diagnose` → fix → `/deploy` → retest
- **After work:** commit + push → `/ok` (sync all docs with committed code)
- **End of session:** `/ok` covers session state, or `/session-state` for a lighter update
- **Release:** `/release 0.5.0` — full pipeline with two user gates before ship

### Agent: uprooted-reviewer

**Model:** Sonnet | **Color:** Red | **Trigger:** explicit request ("review my changes", "check for violations"), or reminded by Stop hook

Runs a structured code review against staged/modified files:

1. Identifies changed files via `git diff`
2. Checks C# for critical rule violations (6 rules) and TypeScript for anti-patterns (4 rules)
3. Validates naming conventions, access modifiers, error handling, logging
4. Reports findings as **CRITICAL** / **WARNING** / **INFO** with file:line and fix

### Skill: uprooted-architecture

**Trigger:** questions about architecture, injection lifecycle, critical rules, coding patterns, or work touching `hook/`, `src/`, or `installer/src-tauri/src/`.

Provides:
- Dual-layer architecture overview (C# hook phases, TypeScript bootstrap)
- All 6 critical rules with explanations
- C# and TypeScript coding patterns (from `references/` files)
- Naming conventions, known fragile areas, build commands

### Skill: uprooted-builder

**Trigger:** "build a plugin", "scaffold a feature", "add a settings page", "create a theme", "intercept bridge method".

Provides:
- File location guides for each component type
- Step-by-step registration sequences (TypeScript plugin, C# settings page, bridge patch, theme)
- Templates and API surfaces from `references/` files
- Pre-build checklist (11 items covering all critical rules)

## How It Works

These aren't a "plugin" — Claude Code discovers them directly from the `.claude/` directory:

- **`hooks.json`** — registered automatically, runs on every `Write`/`Edit` tool use and on session stop
- **`commands/*.md`** — available as `/<name>` slash commands
- **`agents/*.md`** — available as subagents when the trigger conditions match
- **`skills/*/SKILL.md`** — loaded automatically when the user's query matches the skill description

No marketplace, no installation, no `settings.json` plugin references needed. Files in `.claude/` are the source of truth.

## Adding New Components

**New command:** Create `.claude/commands/<name>.md` with YAML frontmatter (`name`, `description`, `allowed-tools`) and markdown instructions.

**New skill:** Create `.claude/skills/<name>/SKILL.md` with frontmatter (`name`, `description`) and put reference files in `.claude/skills/<name>/references/`.

**New agent:** Create `.claude/agents/<name>.md` with frontmatter (`name`, `description`, `model`, `tools`, `color`, `whenToUse`) and a system prompt body.

**New hook:** Add entries to `.claude/hooks.json` under the appropriate event (`PreToolUse`, `PostToolUse`, `Stop`, etc.). Prompt hooks are preferred for context-aware decisions. Hooks reload on session restart.
