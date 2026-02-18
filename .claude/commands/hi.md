---
name: hi
description: Read project context — NEW-SESSION.md, then docs/INDEX.md — to orient yourself in the Uprooted codebase
allowed-tools:
  - Read
  - Glob
---

# Onboard

Orient yourself in the Uprooted codebase by reading the key context documents in order.

## Instructions

1. Read `NEW-SESSION.md` in the repository root. This is the quick-start reference card covering architecture, critical rules, file map, current state, and collaboration rules. Internalize all 9 critical rules and 10 common pitfalls.

2. Read `docs/INDEX.md`. This is the documentation navigation hub. Scan the full index to understand what documentation exists and where to find it.

3. Read `.claude/UPROOT_CLAUDE_DEV.md`. This describes the project-specific Claude Code tooling available in this repo — slash commands, skills, agents, and hooks. Be aware of what tools you have at your disposal.

4. Read `TASKS.md` in the repository root. This is the active task board — upcoming work, code quality items, testing gaps, and backlog ideas. Note which items are unchecked (pending) and which are done. Mention the top 2-3 pending tasks in your summary so the developer knows what's ready to pick up.

5. Read `docs/PLUGIN_ROADMAP.md`. This is the planned plugin pipeline — plugins to be built, their implementation approach, layer (C# hook vs TypeScript), and prerequisites. In your summary, mention which plugins are next to be created (name, layer, and rough complexity).

6. Based on the user's likely next task (if known) or general orientation, identify 2-3 documents from the index that would be most relevant and read them. Good defaults if no specific task is apparent:
   - `docs/framework/ARCHITECTURE.md` — system design and layer boundaries
   - `hook/SESSION_STATE.md` — what changed last, pending issues

7. Provide a brief summary to the user confirming you are oriented. Always use this exact structure:

   - Project identity and version
   - The two injection layers (1 line each)
   - Current state / recent work (bullet list)
   - Any known issues (bullet list)
   - Top pending tasks from TASKS.md (bullet list)
   - Next plugins to build from PLUGIN_ROADMAP.md (name, layer, complexity — bullet list)
   - Available dev commands as a **table** — always format commands in a table like this:

     | Command | Purpose |
     |---------|---------|
     | `/ok` | Post-work doc sweep — sync all docs with committed code |
     | `/build-hook` | Build C# hook (`dotnet build hook/ -c Release`) |
     | `/build-installer` | Build console TUI installer (Rust) |
     | `/deploy` | Build hook + remind user to deploy from Windows |
     | `/watch-log` | Tail the hook log file |
     | `/diagnose` | Check installation health |
     | `/session-state` | Update SESSION_STATE.md + NEW-SESSION.md |
     | `/nose <ver>` | Bump all version references |

     Update this table if commands have been added/removed/renamed since this file was last edited — read the actual contents of `.claude/commands/` to get the current list.

   - Which docs you read and which you'd recommend for their specific task
