# Uprooted Claude Code Dev Tools

Project-specific Claude Code customizations for the Uprooted repository. These live directly in `.claude/` and provide commands, skills, agents, and hooks tailored to the dual-layer injection codebase.

## Structure

```
.claude/
├── UPROOT_CLAUDE_DEV.md        # This file
├── settings.json               # Claude Code settings
├── hooks.json                  # PreToolUse guard for critical rules
├── commands/                   # Slash commands (/uproot:*)
│   ├── hi.md                   # /uproot:hi — onboard into the codebase
│   ├── ok.md                   # /uproot:ok — git pull + status + recent commits
│   ├── build-hook.md           # /uproot:build-hook — dotnet build hook/ -c Release
│   ├── build-installer.md      # /uproot:build-installer — full pipeline build
│   └── watch-log.md            # /uproot:watch-log — tail the hook log file
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

### Hook: Critical Rule Guard

**File:** `hooks.json`

A `PreToolUse` prompt hook on `Write|Edit` that blocks code changes violating Uprooted's critical rules before they land. Catches:

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

### Commands

| Command | Purpose | When to use |
|---------|---------|-------------|
| `/uproot:hi` | Read `NEW-SESSION.md` and `docs/INDEX.md`, orient in the codebase | Start of a new session (first time) |
| `/uproot:ok` | `git pull`, `git status`, `git log --oneline -10` | Start of every session (sync with collaborator) |
| `/uproot:build-hook` | `dotnet build hook/ -c Release` | After modifying C# files |
| `/uproot:build-installer` | Console TUI installer (`cargo build --release`) | After modifying installer or before release |
| `/uproot:watch-log` | Tail `uprooted-hook.log` | Debugging hook behavior at runtime |

**Typical session start:** `/uproot:ok` then `/uproot:hi` (if new context needed).

### Agent: uprooted-reviewer

**Model:** Sonnet | **Color:** Red | **Trigger:** explicit request ("review my changes", "check for violations")

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

- **`hooks.json`** — registered automatically, runs on every `Write`/`Edit` tool use
- **`commands/*.md`** — available as `/uproot:<name>` slash commands
- **`agents/*.md`** — available as subagents when the trigger conditions match
- **`skills/*/SKILL.md`** — loaded automatically when the user's query matches the skill description

No marketplace, no installation, no `settings.json` plugin references needed. Files in `.claude/` are the source of truth.

## Adding New Components

**New command:** Create `.claude/commands/<name>.md` with YAML frontmatter (`name`, `description`, `allowed-tools`) and markdown instructions.

**New skill:** Create `.claude/skills/<name>/SKILL.md` with frontmatter (`name`, `description`) and put reference files in `.claude/skills/<name>/references/`.

**New agent:** Create `.claude/agents/<name>.md` with frontmatter (`name`, `description`, `model`, `tools`, `color`, `whenToUse`) and a system prompt body.

**New hook:** Add entries to `.claude/hooks.json` under the appropriate event (`PreToolUse`, `PostToolUse`, etc.).
