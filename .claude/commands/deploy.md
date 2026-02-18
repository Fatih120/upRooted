---
name: deploy
description: Build the C# hook and tell the user to deploy from Windows
allowed-tools:
  - Bash
  - Read
  - Grep
---

# Deploy

Build the C# hook inside the devcontainer. Deployment is always done by the user on Windows.

## Instructions

### Step 1: Build the hook

Run `dotnet build hook/ -c Release`.

If the build fails:
- Show the first error
- Stop — do not attempt deployment with a broken build

### Step 2: Report and hand off to user

After a successful build, tell the user:

1. Build succeeded — `hook/bin/Release/net10.0/UprootedHook.dll` is ready
2. The workspace is bind-mounted, so the build output is already visible from Windows
3. Run these on your Windows machine:
   - `scripts/deploy-hook.ps1` — stops Root, copies the DLL, relaunches Root
   - `scripts/watch-log.ps1` — tails the hook log in real time

**Do not attempt any file copies, mkdir, or deployment yourself.** No `cp` to `/deploy/`, no `sudo`, no workarounds. The PowerShell deploy script is authoritative — it handles process management, artifact copying, and relaunch. Claude only builds.
