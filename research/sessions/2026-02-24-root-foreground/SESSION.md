# Dump Session: Root Foreground on Login
Date: 2026-02-24
Task: Find why Root foregrounds itself every time the user logs in / unlocks Windows
Hook files: N/A (Root internals investigation)

## Types Dumped
| File | Assembly | Full Type | Lines | Why |
|------|----------|-----------|-------|-----|
| RootLauncher.cs | RootApp.Client.Avalonia | RootApp.Client.Avalonia.RootLauncher | 102 | Entry point: Mutex single-instance + ActivationPipe signal |
| ActivationPipe.cs | RootApp.Client.Avalonia | RootApp.Client.Avalonia.Helpers.Activation.ActivationPipe | 125 | Named pipe IPC: RestoreWindow sets Topmost=true then Activate() |
| App.cs | RootApp.Client.Avalonia | RootApp.Client.Avalonia.App | 634 | StartServerLoop called in OpenMainWindowAsync, CloseToTray config |
| MainWindow.cs | RootApp.Client.Avalonia | RootApp.Client.Avalonia.UI.Main.MainWindow | 316 | RestoreFromTray + HideToTray + window state save/restore |

## Key Findings

1. **Single-instance via Global Mutex**: `RootLauncher.Run()` creates `Global\RootApp-{profile}-SingleInstance` mutex. If a second instance starts, it calls `ActivationPipe.SignalFirstInstance()` then exits.

2. **ActivationPipe is the foreground mechanism**: Named pipe `RootApp-{profile}-ActivationPipe`. Server loop runs in the first instance. When a second instance connects (sends "activate" or "launch:{id}"), the server calls `RestoreWindow()`.

3. **RestoreWindow is aggressive**:
   ```csharp
   P_0.Topmost = true;   // force to front
   P_0.Activate();        // grab focus
   P_0.Topmost = false;   // drop topmost
   ```
   This is the "main character syndrome": Topmost=true + Activate() is the most aggressive foreground-steal technique.

4. **Root.lnk in Startup folder**: Even though disabled in StartupApproved registry, the shortcut file physically exists. The behavior happens because:
   - Windows Startup folder shortcut launches Root.exe on login
   - Second instance hits the Mutex, signals ActivationPipe, exits
   - First instance receives signal, calls RestoreWindow with Topmost trick

5. **CloseToTray**: `_closeToTray` is read from `DataStoreKeys.CloseToTray` (default=1/true). When enabled, closing minimizes to tray instead of quitting. The tray icon + ActivationPipe means Root stays alive across sessions.

6. **The fix is simple**: Either delete the startup shortcut (prevents second instance launch) or disable it properly via registry so Windows doesn't execute it on login.
