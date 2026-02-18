---
name: deploy
description: Build the C# hook and deploy it into Root (full autonomous build+deploy+relaunch)
allowed-tools:
  - Bash
  - Read
  - Grep
---

# Deploy

Build the C# hook and deploy it into Root. Fully autonomous — no user intervention needed.

## Instructions

### Step 1: Build the hook

Run `dotnet build hook/ -c Release`.

If the build fails:
- Show the first error
- Stop — do not attempt deployment with a broken build

### Step 2: Deploy and relaunch

Run the deploy script directly via PowerShell:

```
powershell.exe -ExecutionPolicy Bypass -File scripts/deploy-hook.ps1
```

This stops Root, copies artifacts, and relaunches Root via UprootedLauncher.

### Step 3: Report

Tell the user:
1. Whether the build succeeded or failed
2. Whether the deploy script ran successfully
3. Remind them they can run `scripts/watch-log.ps1` to tail the hook log
