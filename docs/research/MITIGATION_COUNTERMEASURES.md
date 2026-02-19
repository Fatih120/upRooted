# Mitigation Countermeasures

> **Related docs:** [Security Research](SECURITY_RESEARCH.md) | [Reverse Engineering](REVERSE_ENGINEERING.md) | [Research Index](RESEARCH_INDEX.md) | [Root Internals](ROOT_INTERNALS.md)

Pre-emptive technical analysis of the five mitigations Root Communications may implement against
Uprooted, with specific counter-strategies grounded in working prototype code already in the
codebase.

---

## Preamble: Root's Structural Weakness

Root installs to `%LOCALAPPDATA%\Root\current\` on Windows — a fully user-writable directory
that requires no administrator elevation. This single architectural fact is the reason every
counter below remains viable regardless of what application-layer mitigations Root ships:

- **No WDAC policy** — Windows Defender Application Control is not configured for Root's install
  path. Any DLL in `%LOCALAPPDATA%` loads without hash verification.
- **No SFC/WFP coverage** — Windows File Protection and the Windows File Protection driver only
  cover system directories (`System32`, `SysWOW64`). `%LOCALAPPDATA%` is explicitly excluded.
- **No runtime Authenticode verification** — Root does not call `WinVerifyTrust` on its loaded
  modules. The Windows loader performs Authenticode checks only for kernel-mode drivers and for
  user-mode DLLs when strict Authenticode enforcement is in force (requires a group policy or
  WDAC rule — neither applies here).

All five proposed mitigations operate at the application layer. They are defeated by mechanisms
that operate at the OS loader or OS registry layer, which Root cannot control.

---

## Counter 1: Windows Registry Persistence Defeats "Clean Environment" Launchers

**Defeats:** Root mitigations 1 (CLR kill switch) and 5 (signed launcher)

### The Mechanism

The installer writes CLR profiler environment variables directly to
`HKCU\Environment` in the Windows registry (`hook.rs:115`). This is the standard user-scoped
persistent environment storage — the same place `setx` writes to.

The critical behaviour is in `uprooted_launcher.c:162`:

```c
NULL,           /* lpEnvironment (inherit from us, including our env vars) */
```

When `CreateProcessW` is called with `lpEnvironment=NULL`, Windows does **not** simply inherit
the calling process's environment block. The Windows Session Manager (`smss.exe`) merges
`HKCU\Environment` and `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Environment`
into every new process's environment at creation time. This merge happens in the kernel
(`NtCreateUserProcess`), not in the calling process. A launcher that passes `NULL` for
`lpEnvironment` — the natural, default implementation — **inherits our registry keys unchanged**.

### What Root Must Do to Block This

To actually scrub our vars, Root's launcher must:

1. Call `CreateEnvironmentBlock(NULL, NULL, FALSE)` to get a clean merged block
2. Explicitly call `DeleteEnvironmentVariable` for each of our known var names:
   `CORECLR_ENABLE_PROFILING`, `CORECLR_PROFILER`, `CORECLR_PROFILER_PATH`,
   `DOTNET_EnableDiagnostics`, `DOTNET_PROFILER`, `DOTNET_PROFILER_PATH`
3. Pass the modified block to `CreateProcessW` as `lpEnvironment`

This is an arms race. If Root ships a blocklist:
- We can rename vars or use the `COMPlus_` prefix (`COMPlus_EnableDiagnostics`), which is
  checked by older CLR versions and is less likely to be on Root's radar
- The `uiohook_proxy.c:799` prototype already sets `COMPlus_EnableDiagnostics=1` as a fallback
- We escalate to Counter 2 (DLL proxy), which requires zero env vars

### Current State

Already fully implemented. The installer sets all required vars in `HKCU\Environment` via
`hook.rs:111-148`. No code changes required for this counter.

---

## Counter 2: DLL Proxy Injection — The Definitive Bypass

**Defeats:** All env-var-based mitigations (1, 5); completely orthogonal to env vars

### The Mechanism

`uiohook_proxy.c:786-790` documents the key insight:

```c
/*
 * Set CORECLR_PROFILER env vars BEFORE the CLR initializes.
 * uiohook.dll is loaded via PE import table, so DllMain fires
 * before Root.exe's entry point (which initializes the CLR).
 * The profiler loads into Root's actual CLR, not a separate one.
 */
```

`uiohook.dll` appears in Root's PE import table, meaning the Windows loader resolves it during
process initialization — before `WinMain` runs, before the CLR is initialized, before any
launcher verification can take effect. A proxy DLL placed at the same path is loaded by the OS
loader itself, not by any code Root controls.

### Deployment Steps

1. Place proxy `uiohook.dll` in `%LOCALAPPDATA%\Root\current\` (user-writable, no elevation)
2. Rename the original to `uiohook_real.dll`
3. Proxy forwards all exports via linker pragmas (`uiohook_proxy.c:20-70`):
   ```c
   #pragma comment(linker, "/export:hook_run=uiohook_real.hook_run")
   // ... 35 exports total
   ```
   Root functions identically — no missing symbol errors, no behavioral change
4. Proxy `DllMain` (`uiohook_proxy.c:781`) runs before Root's entry point and sets
   `CORECLR_ENABLE_PROFILING`, `CORECLR_PROFILER`, `CORECLR_PROFILER_PATH` via
   `SetEnvironmentVariableA` — at the point the CLR later reads these, they are already set
5. Two background threads start: the IPC injector (Counter 3) and a CLR hosting backup

### Entry Point in Managed Hook

`hook/NativeEntry.cs:13` exposes `Initialize(IntPtr args, int sizeBytes)` — the CLR hosting
entry point that `DllMain`'s CLR thread calls via `hostfxr`. This path is fully operational.

### What Root Needs to Defeat This

WDAC (Windows Defender Application Control) policy with DLL hash enforcement. This requires:
- An admin-protected install directory (cannot be `%LOCALAPPDATA%`)
- A code-signing certificate covering all loaded DLLs
- An enterprise group policy or Intune policy deploying the WDAC rules
- Every DLL update requiring a new signed policy deployment

This is enterprise infrastructure that is architecturally incompatible with Root's consumer
user-install model in `%LOCALAPPDATA%`. A consumer app using Velopack-style updates in a
user-writable directory cannot enforce WDAC without breaking its own update mechanism.

---

## Counter 3: IPC-Level In-Memory JS Injection

**Defeats:** Root mitigations 2 (HTML integrity enforcement) and 3 (`file://` blocking)

### The Mechanism

DotNetBrowser communicates with its embedded Chromium processes via `ipc64.dll`. This DLL
is located at:

```
%LOCALAPPDATA%\Root Communications\Root\profile\default\DotNetBrowser\WindowsX64\ipc64.dll
```

It is in a user-writable directory and is a third-party DotNetBrowser component — Root cannot
change its binary protocol without a DotNetBrowser major version bump.

The `uiohook_proxy.c:458-488` prototype:

1. **Loads `ipc64.dll`** and resolves `send_data`, `open_connection`, `close_connection`
2. **Installs a 14-byte x64 JMP hook** on `send_data` (`PatchJmp` at `uiohook_proxy.c:180-191`):
   ```
   48 B8 <absolute-address>  -- MOV RAX, imm64
   FF E0                      -- JMP RAX
   90 90                      -- NOP padding
   ```
3. **Hooked `send_data`** (`uiohook_proxy.c:269`) scans each message for the string
   `"ExecuteJavaScript"` — DotNetBrowser's RPC method name — and extracts the frame UUID from
   the protobuf payload
4. On the Nth `ExecuteJavaScript` call, the hook **replaces the entire protobuf message** with
   a custom-crafted message that executes our full preload JS+CSS inline
5. The replacement message is manually encoded protobuf (`uiohook_proxy.c:448`):
   ```c
   pos += WriteField(out + pos, 2, "rpc.Frame", 9);
   pos += WriteField(out + pos, 3, "ExecuteJavaScript", 17);
   pos += WriteField(out + pos, 5, payload, payloadLen);
   ```
6. Thread safety is maintained via `CRITICAL_SECTION g_hookLock` (`uiohook_proxy.c:107`):
   original bytes are restored before calling the real function, then re-patched afterward

### Why This Defeats HTML Integrity and `file://` Blocking

- HTML files are **never touched**. Root can have pristine, hash-verified `index.html` files.
- There are no `file://` loads. No scheme-blocked resources.
- Our JS executes **inside Chromium's existing frame context** via the same IPC channel
  DotNetBrowser uses for its own legitimate `ExecuteJavaScript` calls.
- No disk modification at all after initial `ipc64.dll` hook installation.

### Managed Fallback Path

If Uprooted is already loaded via the profiler or DLL proxy path, the managed API provides
the same capability without the IPC-level hook. `hook/DotNetBrowserReflection.cs` implements
`ExecuteJavaScript` via reflection against the live DotNetBrowser assembly — no native code
required. The IPC hook is the fallback for when the managed path is not yet initialized.

### What Root Needs to Defeat This

Encrypt or authenticate the `ipc64.dll` protobuf protocol. Since `ipc64.dll` ships with
DotNetBrowser (a closed-source third-party component), Root cannot change its wire format
without a DotNetBrowser major version upgrade. Even then, the same hook technique applies to
whatever new binary is placed in that user-writable directory.

---

## Counter 4: CSP Bypass

**Defeats:** Root mitigation 4 (Content-Security-Policy)

Three bypass paths, in order of ease:

### Path A — MutationObserver Race (Easiest)

Inject a small JS snippet before CSP meta tags are processed. A `MutationObserver` watching
`document.documentElement` for `childList+subtree` can intercept and remove
`<meta http-equiv="Content-Security-Policy">` nodes before the browser's HTML parser commits
them to the active CSP. This works because the observer callback fires synchronously during
the document parse phase when injected early enough.

Delivery: inject via Counter 3 (IPC hook) at document start, before any other frame script.
This is the lowest-effort hardening step and should be added to the TypeScript preload as a
defensive measure regardless of whether Root ships CSP.

### Path B — DotNetBrowser Response Filter (Medium)

DotNetBrowser exposes a `ResourceHandler` / response filter API that allows registering a
handler to intercept and modify HTTP responses before the browser processes them. Root cannot
prevent this — the API is part of the DotNetBrowser SDK that Root itself depends on.

Register a filter that strips `Content-Security-Policy` and `X-Content-Security-Policy`
headers from all `text/html` responses. `hook/DotNetBrowserReflection.cs` already performs
a full type scan of all loaded DotNetBrowser assemblies — extending it to locate and register
a response filter follows the same reflection pattern.

### Path C — IPC Response Rewriting (Advanced)

The `ipc64.dll` hook (Counter 3) intercepts both outgoing and incoming IPC messages. Incoming
navigation responses carrying HTML can be modified in-memory to strip CSP `<meta>` tags before
the frame parser sees them. This requires extending the `send_data` hook to also hook
`recv_data` or the equivalent response path in `ipc64.dll`.

### Why CSP Cannot Be Fully Effective Here

Counter 3 injects JS at the IPC level by replacing an `ExecuteJavaScript` RPC call — this
occurs at the DotNetBrowser API layer, which is **below** the frame's script context and
**below** any CSP enforcement point. Even a hypothetical engine-level CSP that Root might
configure via DotNetBrowser settings would need to gate the `IFrame.ExecuteJavaScript` API
itself to block this path, which would break Root's own DotNetBrowser usage.

---

## Counter 5: Signed Launcher Analysis

**Defeats:** Root mitigation 5 (signed launcher verifies process chain)

Even a correctly implemented signed launcher does not prevent injection. Three independent
reasons:

### Reason 1: DLL Side-Loading Survives All Launcher Checks

A launcher can verify `Root.exe`'s Authenticode signature, check the parent process chain,
and validate that it was the process that spawned Root. None of this prevents the Windows
loader from loading our proxy `uiohook.dll` (Counter 2). PE import resolution happens in
`ntdll.dll`'s loader, which runs after the process is created — after any launcher
verification that occurs before `CreateProcessW`. The launcher has no hook point between
"process created" and "import DLLs resolved."

### Reason 2: Root.exe Is in a User-Writable Directory

A signed launcher launching `%LOCALAPPDATA%\Root\current\Root.exe` is launching a binary in
a path the current user can overwrite. Without a read-only install directory, verifying
Root.exe's signature at launch time is circular — we can replace `Root.exe` with a binary
that passes verification (or simply re-sign it with a cert we control, since the launcher
cannot enforce a specific cert issuer without a WDAC policy). The more relevant point:
the `uiohook.dll` proxy does not modify `Root.exe` at all, so signature verification of
`Root.exe` succeeds normally.

### Reason 3: Parent PID Spoofing (Last Resort)

If Root's launcher implements a "verify parent was the signed launcher" check via process
ancestry inspection, `PROC_THREAD_ATTRIBUTE_PARENT_PROCESS` in the extended startup info
passed to `CreateProcessW` allows spoofing the parent PID. A process can be created that
appears to have been spawned by any running process, including the signed launcher. This
technique is well-documented and used by legitimate software. It is noted here as a last
resort; Counter 2 makes it unnecessary in practice.

### What Root Needs to Actually Defeat Counter 2

As stated above: WDAC policy + admin-protected install directory + cryptographic DLL hash
verification at load time. This is the only technical path that closes Counter 2. The signed
launcher alone does not accomplish this.

---

## Counter 6: Linux Resilience

**Defeats:** Root mitigations 1 and 5 on Linux

### Current Mechanisms

All four Linux persistence mechanisms are implemented in `installer/src-tauri/src/hook.rs`:

| Mechanism | Path | Trigger |
|---|---|---|
| `systemd environment.d` | `~/.config/environment.d/uprooted.conf` (`hook.rs:244-266`) | All systemd user sessions after re-login |
| Wrapper script | `~/.local/share/uprooted/launch-root.sh` (`hook.rs:268-295`) | Terminal launches, explicit invocation |
| `.desktop` file hijack | `~/.local/share/applications/root-uprooted.desktop` (`hook.rs:298-299`) | GUI app menu / desktop launchers |
| KDE Plasma env script | `~/.config/plasma-workspace/env/uprooted.sh` (`hook.rs:301-327`) | KDE Plasma session startup |
| `~/.profile` fallback | `~/.profile` (`hook.rs:329-356`) | Non-systemd sessions, X11 login shells |

### If Root Ships a Clean-Env Launcher on Linux

**Wrapper script + `.desktop` hijack** is immune. Our `.desktop` in
`~/.local/share/applications/` takes priority over any system desktop file for the same app
(user-local applications directory has higher XDG precedence). It launches `launch-root.sh`
which calls `exec Root "$@"` with env vars set in the shell before exec — they are present
in Root's initial process environment regardless of what any wrapper Root ships does.

**`LD_PRELOAD` shim** is the Linux equivalent of Counter 2. The wrapper script can set
`LD_PRELOAD=/path/to/uprooted_shim.so`. The shim's `__attribute__((constructor))` function
runs before `main()`, setting all required env vars. The dynamic linker loads preload
libraries before the application's own libraries, before any application code runs.

**AppImage modification** applies if Root ships as an AppImage. AppImages are single-file
executables in user-writable locations. The standard extraction path (`./Root.AppImage --appimage-extract`) followed by AppRun modification and repacking produces a modified AppImage
with our env vars sourced at startup.

### What Root Needs to Defeat This on Linux

Flatpak or Snap with strict filesystem sandboxing, explicit environment isolation, and
mandatory access control (AppArmor/SELinux). This is a fundamental distribution model change.
Flatpak and Snap are incompatible with Root's current update model (user-managed binary
updates, direct filesystem deployment). Adopting them would require Root to submit updates
through Flathub/Snap Store review, add a D-Bus portal layer for all system access, and lose
the ability to hot-patch files in the install directory — which their own update mechanism
relies on.

---

## Counter 7: In-Process Repair Loop Beats External Integrity Checkers

**Defeats:** Root mitigation 2 (HTML integrity check and restore loop)

### Why an External Checker Loses the Race

`hook/HtmlPatchVerifier.cs:18` runs **inside Root's own process** via profiler injection.
It installs a `FileSystemWatcher` on each directory containing a patched HTML file. If Root
ships an external integrity checker that monitors the same files:

- Both watchers respond to the same kernel-level file change notification
- Scheduling priority is equal — neither process has priority over the other for
  `ReadDirectoryChangesW` notifications
- Our 1-second debounce (`HtmlPatchVerifier.cs:146`) and 2-second cooldown
  (`HtmlPatchVerifier.cs:21`) can be reduced toward zero if Root ships an active repair loop

Additionally, Phase 0 in `hook/StartupHook.cs` re-patches HTML files before DotNetBrowser
loads during every startup, ensuring the patched state is present from the first moment the
browser reads the file.

### Why This Counter Is Superseded by Counter 3

Once IPC-level injection (Counter 3) is production-ready, HTML file state is completely
irrelevant. Root can restore pristine HTML files as fast as it wants. Our JS executes via
the IPC hook regardless of what is on disk. Counter 7 documents the in-process repair race
as a defense-in-depth measure that remains active during the transition period while Counter 3
is being hardened for production deployment.

---

## Summary Matrix

| Root Mitigation | Primary Counter | Backup Counter |
|---|---|---|
| 1. CLR kill switch (env var scrubbing) | C2: DLL proxy (env-var-free) | C1: registry inheritance |
| 2. HTML integrity (hash + restore) | C3: IPC in-memory injection | C7: in-process repair race |
| 3. Block `file://` loads | C3: IPC injection (no `file://` needed) | — |
| 4. CSP | C4-A: MutationObserver race | C4-B: DotNetBrowser response filter |
| 5. Signed launcher | C2: DLL side-loading survives | C5-R3: PID spoof (last resort) |
| All mitigations combined | C2 + C3: DLL proxy + IPC injection | Full stack |

---

## Implementation Priority

These counters have different production-readiness levels:

| Counter | Status | Next Step |
|---|---|---|
| C1 (registry persistence) | Production | No changes needed |
| C2 (DLL proxy) | Prototype complete | Path resolution: replace hardcoded dev paths with `PlatformPaths.GetUprootedDir()`; integrate into installer |
| C3 (IPC injection) | Prototype working | Production path resolution; settings-aware JS payload builder (reads `uprooted-settings.ini`) |
| C4-A (MutationObserver guard) | Not implemented | Add to TypeScript preload now, defensive measure regardless of Root CSP rollout |
| C4-B (response filter) | Not implemented | Extend `DotNetBrowserReflection.cs` type scan to find and register response filter API |
| C5 (PID spoof) | Not needed | Document only; C2 makes this unnecessary |
| C6 (Linux mechanisms) | Production | No changes needed |
| C7 (in-process repair) | Production | Reduce debounce/cooldown if Root ships active repair loop |

---

## Key Prototype Files

| File | Counters | Notes |
|---|---|---|
| `tools/uiohook_proxy.c` | C2, C3 | DLL proxy + IPC hook + CLR hosting. Hardcoded dev paths need production path resolution |
| `tools/uprooted_launcher.c` | C1 | Documents `lpEnvironment=NULL` behaviour at line 162 |
| `hook/DotNetBrowserReflection.cs` | C3 (managed) | Managed JS injection fallback via DotNetBrowser reflection API |
| `hook/HtmlPatchVerifier.cs` | C7 | In-process FileSystemWatcher; debounce 1s (line 146), cooldown 2s (line 21) |
| `hook/NativeEntry.cs` | C2 | CLR hosting entry point `Initialize(IntPtr, int)` at line 13 |
| `installer/src-tauri/src/hook.rs` | C1, C6 | Windows registry writes (line 111-148); Linux env mechanisms (line 232-359) |
