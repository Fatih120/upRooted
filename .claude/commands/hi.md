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

3. Read `.claude/UPROOT_CLAUDE_DEV.md`. This describes the project-specific Claude Code tooling available in this repo — slash commands, skills, agents, and hooks. Be aware of what tools you have at your disposal (e.g. `/ok`, `/build-hook`, `/nose`, the uprooted-reviewer agent, the critical rule guard hook).

4. Based on the user's likely next task (if known) or general orientation, identify 2-3 documents from the index that would be most relevant and read them. Good defaults if no specific task is apparent:
   - `docs/framework/ARCHITECTURE.md` — system design and layer boundaries
   - `hook/SESSION_STATE.md` — what changed last, pending issues

5. Provide a brief summary to the user confirming you are oriented:
   - Project identity and version
   - The two injection layers
   - Current state / recent work
   - Any known issues
   - Available dev commands (brief list from UPROOT_CLAUDE_DEV.md)
   - Which docs you read and which you'd recommend for their specific task
