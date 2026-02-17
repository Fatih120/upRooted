---
name: deploy
description: Build the C# hook and deploy it to the local Root installation for testing
allowed-tools:
  - Bash
  - Read
  - Grep
---

# Deploy

Build the C# hook and deploy it to the local Root installation in one step. This is the core dev loop command.

## Instructions

### Step 1: Check for running Root process

Run `pgrep -x Root` (Linux) or mention that on Windows you'd check `Get-Process -Name Root`. If Root is running, warn the user that they should close it first — deploying while Root is running will fail because the DLL is locked.

### Step 2: Build the hook

Run `dotnet build hook/ -c Release`.

If the build fails:
- Show the first error
- Stop — do not attempt deployment with a broken build

### Step 3: Identify deployment target

Check which platform we're on and determine paths. The deploy target is checked in priority order:

1. **Devcontainer with Windows mount** (preferred): If `/deploy/uprooted/` exists, deploy there. This is a bind mount to `%LOCALAPPDATA%\Root\uprooted\` on the Windows host.
2. **Linux native**: Deploy to `~/.local/share/uprooted/UprootedHook.dll`
3. **Windows native** (informational — scripts handle this): `powershell -File scripts/install-hook.ps1`

Hook DLL source is always: `hook/bin/Release/net10.0/UprootedHook.dll`

### Step 4: Deploy

**Devcontainer (Windows mount at /deploy/uprooted/):**

The 9p mount from Docker Desktop cannot overwrite existing files directly. Use the copy-to-temp-then-rename pattern:
```bash
sudo cp hook/bin/Release/net10.0/UprootedHook.dll /deploy/uprooted/UprootedHook.new.dll
sudo mv /deploy/uprooted/UprootedHook.new.dll /deploy/uprooted/UprootedHook.dll
```

**Linux native:**
```bash
cp hook/bin/Release/net10.0/UprootedHook.dll ~/.local/share/uprooted/UprootedHook.dll
```

**Windows native:** Run `powershell -File scripts/install-hook.ps1` which handles DLL copy, shortcut modification, protocol handler, and settings.

If the deploy target directory doesn't exist, warn the user that Uprooted hasn't been installed yet — they need to run the installer first.

### Step 5: Verify

Confirm the deployed DLL exists and report its timestamp. If the TypeScript bundle (`dist/uprooted-preload.js`) is also newer than what's deployed, mention that `pnpm build` may also be needed.

### Step 6: Summary

Report:
- Build: pass/fail
- Deployed to: path
- DLL size and timestamp
- Reminder: "Launch Root to test. Use `/watch-log` to monitor."
