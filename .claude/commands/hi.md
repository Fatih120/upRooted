---
name: hi
description: Read project context — NEW-SESSION.md, then docs/INDEX.md — to orient yourself in the Uprooted codebase
allowed-tools:
  - Read
  - Glob
  - AskUserQuestion
---

# Onboard

Orient yourself in the Uprooted codebase, then deep-read all documentation relevant to today's work.

This command has two phases: **orient** (read core context, summarize, ask what's planned) and **deep-read** (find and read every relevant doc before starting work).

## Phase 1: Orient

1. Read `NEW-SESSION.md` in the repository root. This is the quick-start reference card covering architecture, critical rules, file map, current state, and collaboration rules. Internalize all 9 critical rules and 10 common pitfalls.

2. Read `docs/INDEX.md`. This is the documentation navigation hub. Scan the full index to understand what documentation exists and where to find it. Pay special attention to the **Quick-Reference Topic Finder** table at the bottom — you will use it in Phase 2 to map the user's task to relevant docs.

3. Read `.claude/UPROOT_CLAUDE_DEV.md`. This describes the project-specific Claude Code tooling available in this repo — slash commands, skills, agents, and hooks. Be aware of what tools you have at your disposal.

4. Read `TASKS.md` in the repository root. This is the active task board — upcoming work, code quality items, testing gaps, and backlog ideas. Note which items are unchecked (pending) and which are done. Mention the top 2-3 pending tasks in your summary so the developer knows what's ready to pick up.

5. Read `docs/PLUGIN_ROADMAP.md`. This is the planned plugin pipeline — plugins to be built, their implementation approach, layer (C# hook vs TypeScript), and prerequisites. In your summary, mention which plugins are next to be created (name, layer, and rough complexity).

6. Read `hook/SESSION_STATE.md` — what changed last, pending issues, deployment notes.

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
     | `/inject` | Build + close Root + deploy + relaunch (Windows only) |
     | `/watch-log` | Tail the hook log file |
     | `/diagnose` | Check installation health |
     | `/session-state` | Update SESSION_STATE.md + NEW-SESSION.md |
     | `/nose <ver>` | Bump all version references |

     Update this table if commands have been added/removed/renamed since this file was last edited — read the actual contents of `.claude/commands/` to get the current list.

8. **Ask the user what they want to work on today.** Use `AskUserQuestion` with the top pending tasks from TASKS.md as options (plus an "Other" escape hatch). If the user already stated their task in the same message as `/hi`, skip the question and proceed directly to Phase 2 with their stated task.

## Phase 2: Deep-Read

Once you know the task, load all documentation that could prevent rediscovering known bugs, quirks, and patterns.

9. **Map the task to topics.** Go back to `docs/INDEX.md` and use both the **Documentation Map** table and the **Quick-Reference Topic Finder** table to identify every document and section relevant to the user's task. Think broadly — include:
   - Documents directly about the feature area (e.g., LinkEmbedEngine work → Hook Reference § Link embeds, Link Embeds built-in plugin doc)
   - Documents about the patterns the task will use (e.g., visual tree work → Hook Reference § Visual tree traversal, Avalonia Patterns)
   - Documents about layers the task touches (e.g., C# hook work → Architecture, .NET Runtime, Avalonia Patterns)
   - The source files listed in TASKS.md for the task, if any
   - Any built-in plugin docs (`docs/plugins/builtin/`) relevant to the feature
   - Research docs if the task involves gRPC, DotNetBrowser, or Root internals

10. **Read every document identified in step 9.** Read them all — do not skip or summarize from memory. These docs contain hard-won quirks and patterns that prevent wasted debugging cycles. Read source files too if they are listed in TASKS.md for the task or are directly relevant (e.g., the .cs file you'll be modifying).

11. **Report what you read.** After completing the deep-read, tell the user:
    - Which documents you read and why each is relevant
    - Any specific quirks, warnings, or patterns from those docs that are directly applicable to the task
    - Confirm you are ready to begin work

**Why this matters:** This project has many hard-to-discover runtime quirks (trimming, reflection failures, AvaloniaEdit behaviors, SkiaSharp API mismatches). Every quirk is documented somewhere. Reading the docs upfront prevents re-discovering the same bugs 3+ times across sessions, saving significant time and context.
