# Research Directory Index

> **Related docs:** [Security Research](SECURITY_RESEARCH.md) | [Reverse Engineering](REVERSE_ENGINEERING.md) | [gRPC Protocol](GRPC_PROTOCOL.md) | [gRPC Library Reference](GRPC_LIB_REFERENCE.md)

---

## Overview

The `research/` directory contains security research, penetration testing scripts, reverse
engineering tools, and development utilities for analyzing the Root Communications desktop
app. Covers its gRPC-web API, authentication, Chromium storage, binary internals, and WebRTC.

All scripts are research tooling, not production code. Most target a Windows environment with
Root installed. Python scripts require `httpx` for HTTP/2, and Windows-specific scripts use
`ctypes` for process memory access.

---

## Directory Structure

```
research/
├── pentesting/                    # Security research and penetration testing
│   ├── grpc_lib.py                # Shared gRPC-web library (core dependency)
│   ├── LIVE_TOKEN.txt             # Active bearer token (gitignored)
│   ├── phases/                    # Systematic API audit (7 phases)
│   ├── exploits/                  # Proof-of-concept exploit scripts
│   ├── extraction/                # Data extraction and decryption tools
│   ├── recon/                     # Reconnaissance and connectivity probes
│   ├── tokens/                    # Token discovery and analysis
│   ├── js/                        # JavaScript bundle analysis
│   └── ps1/                       # PowerShell source map and bundle tools
├── dev-scripts/                   # Hook development and testing utilities
├── docs/                          # Research documentation
│   ├── architecture/              # Pentest planning and analysis docs
│   ├── findings/                  # Vulnerability findings and evidence
│   └── reports/                   # Bug reports for Root developers
├── src_dump/                      # Extracted source from WebRTC source maps
└── _archive/                      # Deprecated/superseded scripts
```

---

## Core Library -- grpc_lib.py

Shared library imported by all phase scripts and most exploits. Provides a complete gRPC-web
client built from scratch with raw protobuf encoding (no generated stubs required).

**Location:** `research/pentesting/grpc_lib.py`

Provides: protobuf encoding/decoding primitives, gRPC-web `call()` with trailer parsing,
token loader (`LIVE_TOKEN.txt`), UUID encoding for Root's LE fixed64 pair format, permission
bitmap builders, known UUIDs for test accounts/communities/roles, protobuf response parser,
community discovery helpers, and `TestTracker` for structured test reporting.

See [gRPC Library Reference](GRPC_LIB_REFERENCE.md) for the full API.

---

## Pentesting Phases

Systematic audit of Root's gRPC services. All in `research/pentesting/phases/`.

| File | Services | Focus |
|------|----------|-------|
| `phase1_access_rules.py` | AccessRule, ChannelGroup, Channel | Privilege escalation via permission overrides |
| `phase2_file_directory.py` | File, Directory | Cross-community data exfiltration |
| `phase3_community_apps.py` | CommunityApp, AppStore | Privesc via SetDevSettings permission fields |
| `phase4_invites_members.py` | Link, MemberBan, Member, MemberInvite | Member manipulation, mass operations |
| `phase5_notifications_users.py` | Notification, User (31 methods) | Account manipulation, info disclosure |
| `phase6_webrtc_social.py` | WebRtc, FriendshipInvite, Friendship, FriendshipGroup | WebRTC abuse, friendship enumeration |
| `phase7_remaining.py` | CommunityLog, AppLog, Asset, Support | Rate limiting audit, input fuzzing |

All service names are prefixed `root.*GrpcService` (e.g., `root.AccessRuleGrpcService`).

---

## Exploit Scripts

PoC exploits in `research/pentesting/exploits/`, evolved as protobuf schemas were reverse-engineered.

**API exploitation (iterative refinement):**

| File | Purpose |
|------|---------|
| `grpc_exploit.py` | Test hardcoded WebRTC bundle token (C1) against production API |
| `root_exploit.py` | Full toolkit: impersonation, IDOR, privesc, DM creation, admin ops |
| `exploit_dm.py` | DM creation with correct UUID encoding (LE fixed64 pairs) |
| `exploit_full.py` | Service enumeration, DM creation, privilege escalation |
| `exploit_v2.py` | Improved gRPC status parsing from HTTP headers |
| `exploit_targeted.py` | DM list parsing, targeted DM creation and messaging |
| `exploit_p2.py` | Additional services, rejoin community, more IDOR vectors |
| `exploit_phase2.py` | DM creation with refreshed token |
| `exploit_phase3.py` | Memory token extraction + exploitation (skips RefreshToken) |
| `exploit_final.py` | Correct protobuf schemas from JS bundle analysis |
| `run_exploit.py` | Auto-exploit: memory token extraction + full test suite |
| `probe_session.py` | Token validation, IPC discovery, WebSocket auth probing |

**Role escalation (6 iterations):**

| File | Purpose |
|------|---------|
| `role_assign.py` | Direct role assignment via CommunityMemberRoleGrpcService/Add |
| `role_escalate.py` | Edit EVERYONE role, create admin role, assign sudo |
| `role_escalate2.py` | OwnerEdit exception vector, fixed schemas |
| `role_escalate3.py` | Context injection, invite-based assignment, GetExtended |
| `role_final.py` | OwnerEdit, race conditions, invite service vectors |
| `role_invite.py` | Invite-based role assignment via MemberInviteGrpcService |
| `role_last_resort.py` | REST endpoints, admin paths, SQL injection, protobuf pollution |

---

## Extraction Tools

Data extraction from local storage, binaries, and memory. All in `research/pentesting/extraction/`.

| File | Purpose |
|------|---------|
| `decrypt_data.py` | Decrypt Root's AES-encrypted data.json (DPAPI detection) |
| `dpapi_decrypt.py` | Decrypt AuthToken file via DPAPI through PowerShell |
| `extract_and_test.py` | Extract bearer token from Root.exe memory and validate |
| `extract_css_boundaries.py` | Locate CSS boundary offsets within Root.exe binary |
| `extract_mock_bridge.py` | Extract mock bridge init code from WebRTC JS bundle |
| `memory_scan.py` | Scan Root.exe process memory for bearer tokens (128-byte format) |
| `source_map_extract.py` | Parse WebRTC source maps, extract original TypeScript files |

---

## Reconnaissance

API probing scripts in `research/pentesting/recon/`.

| File | Purpose |
|------|---------|
| `test_auth.py` | Test API reachability and unauthenticated gRPC responses |
| `check_status.py` | Verify token validity and community membership |
| `check_response_headers.py` | Analyze HTTP response details for endpoint behavior |
| `quick_test.py` | Validate multiple tokens (refreshed, original, hardcoded) |
| `bug_test.py` | Placeholder for targeted bug reproduction |

---

## Token Tools

Token discovery and analysis in `research/pentesting/tokens/`.

| File | Purpose |
|------|---------|
| `extract_token.py` | Extract clientToken values from WebRTC bundle via regex |
| `find_hardcoded_token.py` | Find tM class token instantiations in WebRTC bundle |
| `find_token2.py` | Search for dev bridge tokens, userId patterns, storage usage |
| `find_fresh_token.py` | Multi-approach recovery: Credential Manager, DPAPI, LevelDB, memory |
| `parse_authtoken.py` | Parse Store/AuthToken file (protobuf, DPAPI, raw analysis) |
| `scan_bearer.py` | Scan Root.exe/chromium.exe memory for Bearer strings |
| `scan_all_tokens.py` | Scan Root.exe memory for all potential tokens |
| `scan_chromium_storage.py` | Scan DotNetBrowser Chromium SQLite/LevelDB for tokens |
| `scan_chromium_tokens.py` | Scan chromium.exe processes for auth tokens in memory |

---

## JavaScript and PowerShell Analysis

**JS scripts** (`research/pentesting/js/`) for analyzing minified bundles:

| File | Purpose |
|------|---------|
| `audit.js` | Security audit: crypto, storage, auth headers, eval, XSS sinks |
| `extract.js` | Extract context around security patterns (WebSocket, ICE, innerHTML) |
| `analyze_hexatris.js` | Hexatris game bundle: score manipulation, bridge usage |
| `extract_hex_game.js` | Extract game-specific patterns from Hexatris bundle |
| `split_line206.js` | Extract specific long lines from minified bundles |

**PS1 scripts** (`research/pentesting/ps1/`) for source map analysis:

| File | Purpose |
|------|---------|
| `parse_sourcemap.ps1` | List all source file paths and indices from source map |
| `search_sourcemap.ps1` | Search source map for sensitive patterns (IPs, credentials) |
| `search_first_party.ps1` | Search first-party sources only (skip node_modules) |
| `extract_mock_bridge.ps1` | Extract mock bridge, effects SDK, sentry middleware source |
| `extract_more_sources.ps1` | Extract source modules by index |
| `extract_diagnose_sdk.ps1` | Extract DiagnoseSDK, AuthToken, API connection modules |
| `audit_search.ps1` | Generic string search across JS bundles with context |

---

## Development Scripts

Build/test/debug utilities in `research/dev-scripts/`. All PowerShell/batch, Windows-only.

**Build:** `_rebuild.cmd`, `_rebuild2.ps1`, `_rebuild3.bat` -- build profiler DLL with MSVC.
`_build_and_test.ps1` -- full cycle: kill Root, build, deploy, launch, check logs.

**Launch:** `_launch_root.ps1`, `_launch_root2.ps1` -- start Root with profiler env vars.
`_focus_root.ps1` -- bring Root window to foreground via Win32.

**Test:** `_test_injection.ps1` / `2` / `3` -- profiler-to-hook injection tests.
`_test_profiler.ps1` / `2` -- verify profiler loading. `_test_startup_hooks.ps1` -- .NET
startup hook loading. `_test_loadfrom.ps1` -- Assembly.LoadFrom behavior. `_test_diag.ps1`,
`_check_diag.ps1` -- diagnostic output.

**Inspect:** `_check_dll.ps1` -- verify DLL size/timestamp. `_check_exports.ps1` -- verify
DLL exports. `_check_imports.ps1` -- check PE import table. `_find_startup_hook.ps1` --
search binary for StartupHookProvider patterns.

**UI:** `_screenshot_root.ps1`, `_screenshot.ps1` -- capture Root window screenshots.
`_navigate_themes.ps1` -- automate UI navigation via mouse/keyboard simulation.

---

## Root Theme System Research

| File | Purpose |
|------|---------|
| `ROOT_THEME_SYSTEM_FINDINGS.md` | Root's native Avalonia 32-key color system (ILSpy decompilation) — complete color tables for Dark/Light/PureDark, theme switching mechanism, resource hierarchy, App.cs wiring, style file analysis |

---

## Documentation

Research docs in `research/docs/`, organized into three areas:

**architecture/** -- `ARCHITECTURE.md` (app analysis), `PENTEST_PLAN.md` (methodology),
`PRIVACY_ANALYSIS.md` (data handling), `THEME_EXTRACTION.md`, `TOKEN_EXTRACTION.md`.

**findings/** -- `FINDINGS.md` (consolidated), `ATTACK_SURFACE.md`, `EVIDENCE.md`,
`VULNERABILITY_REPORT.md`.

**reports/** -- `BUG_REPORT_EMAIL.md`, `REPORT_FOR_ADRIAN.md`, `REPORT_REMAINING_FINDINGS.md`,
`REPORT_RESOLUTION_BUG.md`, `C5_RESTART_OPEN_REDIRECT.md`, `DEVBAR_AND_IP_LEAK.md`,
`REPORT_C7_CHANNEL_RENAME.md`.

**src_dump/** -- Extracted TypeScript/TSX source from the WebRTC source map, produced by
`source_map_extract.py`. Contains Root's `rootapp-desktop-webrtc` package.

**_archive/** -- Deprecated scripts (type checking, resource scanning, port probing). Not used.

---

## Adversarial Analysis

| Document | Purpose |
|----------|---------|
| [`docs/research/MITIGATION_COUNTERMEASURES.md`](../research/MITIGATION_COUNTERMEASURES.md) | Pre-emptive counter-strategies for all five proposed Root mitigations (CLR kill switch, HTML integrity, `file://` blocking, CSP, signed launcher) grounded in working prototype code |

---

## Usage Notes

**Python environment:** Requires Python 3.10+ with `httpx[http2]` (`pip install httpx[http2]`).

**Authentication:** API scripts need a bearer token in `pentesting/LIVE_TOKEN.txt` (gitignored).
Use `tokens/find_fresh_token.py`, `extraction/memory_scan.py`, or `extraction/extract_and_test.py`
to obtain one. Avoid calling RefreshToken as it invalidates the current session.

**Windows requirement:** Most scripts require Windows (Win32 ctypes, PowerShell, Root AppData
paths). Phase scripts and pure-API exploits can run from any platform with network access.

**Running phase scripts:** Each is standalone -- `python phases/phase1_access_rules.py` from
the `pentesting/` directory. Each prints a banner and reports results via `TestTracker`.
