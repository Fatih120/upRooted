---
name: watch-log
description: Tail the Uprooted hook log file in real-time
allowed-tools:
  - Bash
  - Read
---

# Watch Log

Monitor the Uprooted C# hook log file in real-time for debugging.

## Instructions

The hook log file is located at `uprooted-hook.log` in Root's profile directory. The convenience script is:

```
powershell -File scripts/watch-log.ps1
```

If that script is not available or not on Windows, find the log file manually:

- **Windows**: `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log`
- **Linux**: Check `~/.local/share/Root Communications/Root/profile/default/uprooted-hook.log` or similar XDG path

Then tail it:

```bash
tail -f "<path-to-log>/uprooted-hook.log"
```

## Log Format

The hook log uses this format:
```
[timestamp] [Category] message
```

Categories include:
- `Startup` — Initialization phases (0-4)
- `Injector` — Sidebar injection state changes
- `Theme` — Theme engine operations
- `Walker` — Visual tree discovery
- `Settings` — Settings loading

## Tips

- The log is recreated each time Root starts (not appended)
- Look for `Phase X FAILED` lines to diagnose startup issues
- `=== Uprooted Hook Ready ===` confirms successful initialization
- Enable diagnostics by uncommenting the `DumpVisualTreeColors` section in `StartupHook.cs`
