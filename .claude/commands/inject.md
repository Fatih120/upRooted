---
name: inject
description: Build the C# hook, close Root, deploy artifacts, and relaunch — fully automatic (Windows only)
allowed-tools:
  - Bash
  - Read
  - Grep
---

# Inject

Build the C# hook, stop Root, deploy the DLL, and relaunch Root with Uprooted. One command, zero manual steps.

**Requires:** Native Windows environment with PowerShell. Will not work from WSL or devcontainers — use `/deploy` instead for those environments.

## Instructions

### Step 1: Build the hook

Run `dotnet build hook/ -c Release`.

If the build fails:
- Show the first error
- Stop — do not attempt deployment with a broken build

### Step 2: Deploy and relaunch

Run the deploy script via PowerShell:

```
powershell.exe -ExecutionPolicy Bypass -File scripts/deploy-hook.ps1
```

This script handles the full cycle:
1. Stops Root.exe and chromium processes
2. Copies UprootedHook.dll + deps to `%LOCALAPPDATA%\Root\uprooted\`
3. Relaunches Root via UprootedLauncher.exe

### Step 3: Report

Print a short summary:
1. Build result (success/fail)
2. Deploy result (success/fail)
3. Remind: `/watch-log` to tail the hook log
