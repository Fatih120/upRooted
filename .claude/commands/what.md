---
name: what
description: "Deep-load session context, then use ILSpy tooling to dump every verbose type for what we need more info on, organizing it into a folder specific to that debugging session in research, then will carry on with debugging."
allowed-tools:
  - Read
  - Write
  - Edit
  - Grep
  - Glob
  - Bash
  - Task
  - AskUserQuestion
args:
  - name: task
    description: "What to dump types for (e.g., 'Rootcord plugin', 'MessageLogger card positioning', 'ProfileBadge popup detection')"
    required: false
---

# Dump — Fresh ILSpy Decompilation for Debugging

Decompile the Root types a hook engine references, organize into a session folder, analyze, and debug. Always decompile fresh.

**Key paths:** Extracted assemblies: `/tmp/root-extracted/` | Root.exe: `C:/Users/bash/AppData/Local/Root/current/Root.exe` | Sessions: `research/sessions/`

## Phase 1: Orient (3 files in parallel)

Read: `NEW-SESSION.md`, `TASKS.md`, `hook/SESSION_STATE.md`

Summary: **v{version}** | **Recent**: 2 bullets from Done | **Pending**: top 3 Ready to Build | **Hook state**: 1 sentence

## Phase 2: Identify Task

**If argument provided**, map to hook file(s):

| Keywords | Hook files |
|----------|-----------|
| `rootcord` | `RootcordEngine.cs` |
| `message logger` | `MessageLogger.cs`, `MessageStore.cs` |
| `profile badge` | `ProfileBadgeInjector.cs` |
| `translate` | `TranslateEngine.cs`, `TranslateConfigPopup.cs` |
| `link embed` | `LinkEmbedEngine.cs`, `AnimatedImage.cs` |
| `theme` | `ThemeEngine.cs` |
| `sidebar` | `SidebarInjector.cs` |
| `content pages`, `settings` | `ContentPages.cs` |
| `clearurls` | `ClearUrlsEngine.cs` |
| `nsfw` | `NsfwFilter.cs` |
| `who reacted` | `WhoReactedEngine.cs` |
| `user bio` | `UserBioEngine.cs` |
| `presence`, `beacon` | `UprootedPresenceBeacon.cs` |
| `audit log` | `AuditLogEngine.cs` |
| `silent typing` | `SilentTypingEngine.cs` |
| `auto update` | `AutoUpdater.cs` |
| `color picker` | `ColorPickerPopup.cs`, `ColorUtils.cs` |
| `notification` | `DesktopNotification.cs` |
| `startup` | `StartupHook.cs` |
| `html patch` | `HtmlPatchVerifier.cs` |
| `browser` | `DotNetBrowserReflection.cs`, `BrowserDiscovery.cs` |
| `dev console` | `DevConsoleDropdown.cs` |
| `focus mode` | `FocusModeEngine.cs` |
| `message drafts` | `MessageDraftsEngine.cs` |
| `reflection`, `avalonia types` | `AvaloniaReflection.cs` |
| `visual tree`, `walker` | `VisualTreeWalker.cs` |
| `settings`, `ini` | `UprootedSettings.cs` |
| `entry`, `profiler entry` | `Entry.cs`, `NativeEntry.cs` |
| `logger`, `logging` | `Logger.cs`, `WideEvent.cs`, `TailSampler.cs` |
| `log console`, `pipe` | `LogConsole.cs` |
| `recon` | `ReconLogger.cs` |
| `paths`, `platform` | `PlatformPaths.cs` |

All files in `hook/`. If no match: Glob for closest `.cs` in `hook/`, or ask.

**If no argument**, use AskUserQuestion (header: "Task"): "What are you debugging?" with top 3-4 TASKS.md items + "Other engine/file".

## Phase 3: Discover Trouble

**Skip** if trouble is already described in the argument.

Otherwise AskUserQuestion (header: "Trouble"): "What's been the trouble with {task}?"
1. Layout / positioning / visual tree structure
2. Property binding / INPC / data not updating
3. Crash / exception / null reference
4. Timing / race condition / event ordering

## Phase 4: Extract Type References

Read the hook source file(s). Grep for Root type references:

1. **String type names**: `Grep pattern:"RootApp\.|Avalonia\.|AvaloniaEdit\."` — types passed to reflection
2. **AvaloniaReflection calls**: `Grep pattern:_r\.(Is|Get|Create|Set|Find)[A-Z]` — what types/properties are used
3. **Reflection property access**: `Grep pattern:GetPropertyValue|SetPropertyValue|GetProperty\(|\.GetValue\(|\.SetValue\(`
4. **Type name refs**: `Grep pattern:nameof\(|typeof\(|"[A-Z][a-zA-Z]+View"`

Build lists of: needed types, needed properties, needed views.

## Phase 5: Resolve Full Type Names

Check `/tmp/root-extracted/` exists: `ls /tmp/root-extracted/RootApp.Client.Avalonia.dll 2>/dev/null`
If missing: `ilspycmd -d -o /tmp/root-extracted "C:/Users/bash/AppData/Local/Root/current/Root.exe"`

Resolve each type: `ilspycmd -l c /tmp/root-extracted/RootApp.Client.Avalonia.dll | grep -i "TypeName"`
Assembly search order: UI→`RootApp.Client.Avalonia.dll` | DTOs→`RootApp.WebApi.Shared.Entities.dll` | gRPC→`RootApp.WebApi.Shared.dll` | Core→`RootApp.Core.dll`

Expand: View↔ViewModel pairs, GrpcService+DTOs for gRPC data, Style_ files for created controls.

## Phase 6: Create Session Folder & Decompile

```bash
mkdir -p "research/sessions/YYYY-MM-DD-<task-slug>/"
# For each type:
ilspycmd /tmp/root-extracted/<assembly>.dll -t "Full.Namespace.TypeName" -r /tmp/root-extracted > "research/sessions/YYYY-MM-DD-slug/TypeName.cs"
```

Run decompilations in parallel. Verify outputs are non-empty valid C#.

Write `SESSION.md` index in the session folder:
```markdown
# Dump Session: {Task Name}
Date: {YYYY-MM-DD}
Task: {description}
Hook files: {comma-separated}

## Types Dumped
| File | Assembly | Full Type | Lines | Why |
|------|----------|-----------|-------|-----|

## Key Findings
(filled in after reading)
```

## Phase 7: Analyze & Report

Read dumps (large files: offset/limit first, then grep). Extract findings by trouble type:
- **Layout**: grid/panel structure, named controls, RowDef/ColDef, alignment
- **Binding**: INPC properties, DataContext, commands
- **Crash**: null checks, type casts, method signatures
- **Timing**: event subscriptions, async, Loaded/Initialized handlers

Report: 1) Types dumped with line counts 2) 5-8 key findings 3) Root cause hypothesis 4) Suggested approach. Update SESSION.md Key Findings.

## Phase 8: Continue

Ask: implement fix, dump more types, or use as reference. Session dumps persist in `research/sessions/`.
