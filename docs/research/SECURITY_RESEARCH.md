# Security Research Summary

> **What this is:** Structured summary of 105 security findings from the Root Communications v0.9.86 security assessment — organized by category with severity ratings and source links.
> **Read when:** Understanding Root's security posture; planning security-aware features; reviewing what attack surfaces exist.
> **Skip if:** You need the RE methodology narrative → [REVERSE_ENGINEERING.md](REVERSE_ENGINEERING.md). You need mitigation strategies → [MITIGATION_COUNTERMEASURES.md](MITIGATION_COUNTERMEASURES.md).
> **Does NOT cover:** RE methodology → [REVERSE_ENGINEERING.md](REVERSE_ENGINEERING.md) | Counter-strategies → [MITIGATION_COUNTERMEASURES.md](MITIGATION_COUNTERMEASURES.md) | Root's architecture → [ROOT_INTERNALS.md](ROOT_INTERNALS.md)

> **Related docs:** [Root Internals](ROOT_INTERNALS.md) | [gRPC Protocol](GRPC_PROTOCOL.md) | [Reverse Engineering](REVERSE_ENGINEERING.md) | [Research Index](RESEARCH_INDEX.md)

Structured summary of 105 security findings from the Root Communications v0.9.86
security assessment. Raw findings, evidence, and exploit code live in `research/` --
this document organizes them by category and links back to the source material.

**Classification**: CONFIDENTIAL -- authorized testing only.

---

## Table of Contents

1. [Overview](#1-overview)
2. [Methodology](#2-methodology)
3. [Attack Surface Summary](#3-attack-surface-summary)
4. [Findings by Category](#4-findings-by-category)
   - [Authentication](#41-authentication)
   - [Authorization](#42-authorization)
   - [Injection](#43-injection)
   - [Privacy](#44-privacy)
   - [Supply Chain](#45-supply-chain)
   - [Network](#46-network)
   - [Local](#47-local)
5. [Critical Findings](#5-critical-findings)
6. [Disclosure Status](#6-disclosure-status)
7. [Reports Sent](#7-reports-sent)
8. [Recommendations](#8-recommendations)
9. [Research Limitations](#9-research-limitations)
10. [Evidence Index](#10-evidence-index)

---

## 1. Overview

### Scope

Security research conducted against Root Communications desktop application v0.9.86
(Windows x64). Root is a .NET 10 / Avalonia UI communications app that embeds Chromium
(DotNetBrowser) to host 7 React web sub-apps in iframes. It uses protobuf over a
proprietary IPC channel for native-to-web communication, WebRTC via Effects SDK for
voice/video, and Velopack for auto-updates. The backend API at `api.rootapp.com` sits
behind Cloudflare.

### Ethical Context

All testing was performed as authorized security research using the researcher's own
accounts (`watchthelight` and `electromutt`). Testing adhered to the following principles:

- Only the researcher's own accounts were used for active exploitation
- Destructive tests were limited to researcher-owned resources where possible
- The one exception (channel rename in the Root community, finding C7) was immediately
  disclosed to the platform team with a request to restore the original name
- No user data was exfiltrated, no accounts were compromised, and no services were disrupted
- Responsible disclosure was followed: findings were reported to Root via their support
  channel and directly to a named contact (Adrian) before any public mention
- No bug bounty or formal security program existed at Root at the time of testing

### Timeline

| Date | Event |
|------|-------|
| 2026-02-10 | Initial static analysis of client-side JavaScript bundles |
| 2026-02-11 | Deep audit of all 7 sub-apps, WebRTC bundle, and local file system |
| 2026-02-12 | Live API penetration testing (7 phases against production gRPC-web) |
| 2026-02-12 | C7 confirmed: unauthorized cross-community channel rename |
| 2026-02-12 | First disclosure sent via Root support channel (bug report email) |
| 2026-02-12 | Detailed report sent directly to Adrian (Root team contact) |
| 2026-02-13 | Follow-up reports: C5 open redirect, DevBar/IP leak, remaining findings |
| 2026-02-14 | Resolution bug report sent (double-question-mark URL) |

### Severity Breakdown

| Severity | Count |
|----------|-------|
| Critical | 7 |
| High | 24 |
| Medium | 37 |
| Low | 20 |
| Functional Bugs | 13 |
| Informational | 5 |
| **Total** | **105** |

---

## 2. Methodology

### 7-Phase Penetration Test Approach

Testing followed a structured 7-phase methodology, progressing from passive analysis
through active exploitation:

**Phase 0 -- Reconnaissance and Static Analysis**
Extracted and analyzed all client-side assets from the Root installation directory.
Deobfuscated minified JavaScript bundles using source maps (13.5 MB map shipped in
production). Identified 7 sub-app bundles, 1 WebRTC bundle, 9 HTML files, and 637
protobuf message type definitions embedded in the JavaScript.

**Phase 1 -- Access Rules and Channel Services**
Tested `AccessRuleGrpcService`, `ChannelGroupGrpcService`, and `ChannelGrpcService`
for authorization bypass. Discovered missing RBAC on channel edit (C7), cross-community
channel enumeration (H22, H23), and channel group exposure.

**Phase 2 -- File and Directory Services**
Tested `FileGrpcService` and `DirectoryGrpcService` for unauthorized access.
Most methods returned BAD_ARG due to container ID format requirements -- inconclusive.

**Phase 3 -- Community App Services**
Tested `CommunityAppGrpcService` and `AppStoreGrpcService`. App store enumeration
succeeded (9 apps listed). Community app operations returned UNAUTH, suggesting
proper scoping.

**Phase 4 -- Invites, Members, and Social**
Tested `CommunityMemberGrpcService`, `LinkGrpcService`, and social features.
Discovered full member list exposure (M29, 16,971 users), invite code enumeration (M30),
and cross-community member data access.

**Phase 5 -- Notifications and User Services**
Tested `NotificationGrpcService`, `UserGrpcService`, and identity services. Confirmed
notification suppression (M31), user lookup by username/UUID (M32, M33), mass account
creation (M34), SSRF via profile picture (H24), and token refresh session DoS (M26).

**Phase 6 -- WebRTC and Social Graph**
Tested `WebRtcGrpcService`, `FriendshipGrpcService`, and related services. Discovered
TURN credential disclosure without active session (M35) and social graph mapping (L18).

**Phase 7 -- Rate Limiting and Edge Cases**
Concurrent request testing confirmed zero server-side rate limiting (M36, M37).
Token refresh race condition testing confirmed proper server-side locking (L20).

### Tools Used

| Tool | Purpose |
|------|---------|
| Custom Python scripts (`grpc_lib.py`) | gRPC-web protocol implementation, protobuf encoding/decoding |
| `httpx` (Python HTTP/2 client) | gRPC-web API calls with HTTP/2 support |
| `phase1_access_rules.py` -- `phase7_remaining.py` | Per-phase automated test scripts |
| `memory_scan.py` | Process memory scanning for bearer tokens |
| `extract_and_test.py` | Token extraction and validation |
| `grpc_exploit.py` | Hardcoded token testing against production API |
| Source map extraction scripts | JavaScript deobfuscation |
| PowerShell / cmd scripts | Local file system analysis, DLL inspection, ACL auditing |
| `curl` | Sentry DSN exploitation verification |
| Browser DevTools | DOM inspection, sessionStorage analysis, bridge method enumeration |

### Testing Environment

- **OS**: Windows 11 Home 10.0.26200
- **Target**: Root Communications v0.9.86 (Windows x64, 617 MB self-contained .NET 10)
- **API endpoint**: `api.rootapp.com` (production, behind Cloudflare)
- **Test accounts**: `watchthelight` (primary), `electromutt` (alt)
- **Approach**: Black-box with client-side source access (shipped source maps + JS analysis)

---

## 3. Attack Surface Summary

### External Endpoints

| Endpoint | Protocol | Purpose | Findings |
|----------|----------|---------|----------|
| `api.rootapp.com` | HTTPS/gRPC-web/WSS | Backend API, WebSocket messaging | C6, C7, H22-H24, M26-M37, L18-L20 |
| `installer.rootapp.com` | HTTPS | Velopack auto-updates | M7, M8, L5 |
| `o447951.ingest.sentry.io` | HTTPS | Sentry telemetry (sub-apps) | M1 |
| `o4509469920133120.ingest.us.sentry.io` | HTTPS | Sentry telemetry (WebRTC) | H4, H5, H6, M16, M21 |
| `effectssdk.ai` | HTTPS | WASM audio/video AI models | H11, L3, L10, L13 |

### JavaScript Bridge Surface

| Bridge Object | Direction | Methods | Key Risk |
|---------------|-----------|---------|----------|
| `__rootSdkBridgeWebToNative` | Web to Native | `.send()`, `.restart()`, `.searchUserProfiles()`, 5 more | Raw protobuf injection, open redirect |
| `__rootSdkBridgeNativeToWeb` | Native to Web | `.receive()`, `.setTheme()` | Fake response injection, CSS injection |
| `__webRtcToNative` | WebRTC to Native | `.kickPeer()`, `.setAdminMute()`, `.setAdminDeafen()`, 8 more | Admin actions without auth |
| `__nativeToWebRtc` | Native to WebRTC | `.initialize()`, `.receiveRawPacket()`, `.updateMyPermission()`, 10 more | Token exposure, permission escalation |
| `__mediaManager` | Media | `.getDevices()` | Device enumeration |

### Sub-App Inventory

| UUID | App Name | Version | Notable Findings |
|------|----------|---------|-----------------|
| `002a3d83-c35a` | Raid Planner | 0.4.11 | H3, H9, M17, M25, B2, B4 |
| `002a3d83-e2e0` | Task Tracker | 0.4.5 | B8 |
| `002a3d84` | Stickerwall | 0.4.8 | H2, M13, M23, L12 |
| `002c6a0c` | Polls | 0.4.4 | (shared SDK vulns only) |
| `002c6a0d-a29d` | Suggestions | 0.4.4 | M24 |
| `002c6a0d-3616` | Minecraft Easy Setup | 0.4.5 | C3, C4, H8, M12 |
| `002c8f0a` | Hexatris | 0.4.2 | H15, L16, B9 |
| -- | WebRTC Bundle | -- | C1, C2, H4-H7, H11, H16-H19, M2, M16, M21, M26 |

### gRPC Backend Services (27 services, 163 methods)

Fully enumerated from the WebRTC bundle JavaScript. Of 97+ tested endpoints,
32 were confirmed implemented in production. Key services:

- **UserGrpcService** (31 methods) -- account operations, auth, profile management
- **CommunityGrpcService** (11 methods) -- community CRUD, membership
- **ChannelGrpcService** (6 methods) -- channel management (C7 found here)
- **DirectMessageGrpcService** (6 methods) -- DM creation without consent (C6)
- **v2.MessageGrpcService** (15 methods) -- messaging (identity from token only)
- **WebRtcGrpcService** (12 methods) -- voice/video, TURN credentials

Full service catalog with method counts: see `research/docs/findings/ATTACK_SURFACE.md`.

---

## 4. Findings by Category

### 4.1 Authentication

Findings related to token handling, session management, and credential storage.

| ID | Severity | Title | Status |
|----|----------|-------|--------|
| C1 | Critical | Hardcoded bearer token in production WebRTC bundle (4 occurrences) | Confirmed -- token expired/revoked but format and endpoint validated |
| C2 | Critical | Hardcoded userId, deviceId, communityId, channelId in production | Confirmed |
| H7 | High | Hardcoded localhost:3005 + debug alert in mock bridge | Confirmed |
| H12 | High | sessionStorage poisoning via `__rootUser` -- zero validation on JSON.parse | Confirmed |
| H13 | High | Client-supplied user_id/device_id in AppRpcMessageToHost transport envelope | Confirmed (dev bridge path) |
| H17 | High | Bearer token extractable from process memory in plaintext | Confirmed -- memory scan found token at specific address |
| H18 | High | Store\AuthToken file enables offline token extraction (390 bytes) | Confirmed -- user-readable, DPAPI fails |
| H19 | High | WebRTC postMessage auth key exchange interceptable via globalThis | Confirmed |
| M5 | Medium | AuthToken stored without DPAPI protection | Confirmed |
| M10 | Medium | User identity in sessionStorage as plaintext JSON | Confirmed |
| M19 | Medium | No session invalidation, no logout mechanism, no idle timeout | Confirmed |
| M22 | Medium | Auth artifacts (data.json, Login Data, Web Data, Trust Tokens) persist after app close | Confirmed |
| M26 | Medium | Token refresh enables indefinite session persistence + session DoS | Confirmed -- RefreshToken invalidates old token immediately |
| L8 | Low | JWT validation only checks header format, no signature or claims verification | Confirmed |
| L20 | Low | Token refresh race condition -- server properly locks (only 1 of 5 concurrent calls succeeds) | Confirmed |

**Key architectural observation**: Root uses two distinct authentication paths.
Sub-apps communicate through a protobuf bridge where the .NET native layer injects identity
from an encrypted token -- sub-apps never see the bearer token. The WebRTC bundle, however,
holds the bearer token directly in JavaScript memory and makes gRPC-web HTTP calls with
`Authorization: Bearer` headers. This makes the WebRTC context the primary target for token
theft.

The bearer token format (reverse-engineered from the `tM` class): 128 bytes total,
bytes 0-15 = userId UUID, bytes 16-31 = deviceId UUID, bytes 32-127 = 96-byte
cryptographic signature. Token forgery is not possible due to the signature, but token
replay is feasible because there is no client-enforced expiry, no server-side session
revocation, and `RefreshToken` extends access indefinitely.

### 4.2 Authorization

Findings related to access control, privilege escalation, and role enforcement.

| ID | Severity | Title | Status |
|----|----------|-------|--------|
| C3 | Critical | Minecraft SendCommandRequest -- arbitrary server commands with no filtering | Confirmed (static) |
| C4 | Critical | GetAllServersResponse returns API keys + SFTP credentials to all clients | Confirmed (static) |
| C6 | Critical | DM creation with any user -- no mutual consent required | Confirmed -- PoC against community owner |
| C7 | Critical | Cross-community unauthorized channel rename via ChannelGrpcService/Edit | Confirmed -- renamed "Chat" to "pwned-by-pentest" in Root community |
| H9 | High | IDOR: forgeable user_id/leader_id in Raid Planner protobuf requests | Confirmed (static) |
| H10 | High | XSS grants full bridge API access -- no authorization on any bridge method | Confirmed |
| H14 | High | COMMUNITY_DELETE message type (value 8) is fully client-serializable | Confirmed (static) |
| H15 | High | Hexatris client-authoritative game model -- score/board/line-clear forgery | Confirmed (static) |
| H16 | High | WebRTC bundle contains full gRPC client for all 27 backend services (163 methods) | Confirmed |
| H20 | High | Community creation open to all users with no rate limiting | Confirmed -- created PentestCommunity |
| H21 | High | Community leave is irreversible -- Join returns UNIMPLEMENTED | Confirmed |
| H22 | High | Cross-community channel group enumeration (6 groups exposed) | Confirmed |
| H23 | High | Cross-community channel enumeration (10 channels with full details) | Confirmed |
| H24 | High | SSRF via SetProfilePicture -- internal DNS resolution bypass | Confirmed -- IP blocking present but DNS bypass works |
| M9 | Medium | Native bridge accessible to any code in DotNetBrowser context | Confirmed |
| M17 | Medium | Client-side permission bypass via React Query cache (`setQueryData`) | Confirmed (static) |
| M29 | Medium | Full member list exposure: 16,971 users via CommunityMemberGrpcService/ListAll | Confirmed |
| M30 | Medium | Invite code enumeration -- "root" reveals community info | Confirmed |
| M31 | Medium | Notification suppression via DeleteAll (covers attacker tracks) | Confirmed |
| M32 | Medium | User info disclosure via GetExtendedUsersByUsername | Confirmed |
| M33 | Medium | User info disclosure via GetExtendedUsersById | Confirmed |
| M34 | Medium | Mass account creation from authenticated session (SignUp returns OK) | Confirmed |
| M36 | Medium | No server-side rate limiting on UserGrpcService/GetSelf | Confirmed -- 20 concurrent calls in 0.2s |
| M37 | Medium | No server-side rate limiting on NotificationGrpcService/List | Confirmed -- 20 concurrent calls in 0.1s |
| M27 | Medium | gRPC-web status in HTTP headers bypasses body parsing | Confirmed |
| M28 | Medium | Full gRPC service enumeration: 32 implemented endpoints across 8 services | Confirmed |
| B4 | Bug | Raid max participants counts removed/left users toward capacity | Confirmed (static) |
| B8 | Bug | Task Tracker MemberGroups client-writable -- arbitrary group assignment | Confirmed (static) |
| B9 | Bug | Hexatris RequestPieces count unbounded -- unlimited lookahead | Confirmed (static) |
| L18 | Low | Social graph mapping via GetConnectionBetween | Confirmed |
| L19 | Low | HubServer WebSocket endpoint disclosed (`wss://hub-1.rootapp.com/notifications`) | Confirmed |

**Key finding**: The ChannelGrpcService demonstrates inconsistent RBAC enforcement.
`Delete` correctly returns UNAUTH for non-admins, but `Edit` on the same channel has
no authorization check whatsoever. This pattern suggests other Edit endpoints may share
the same oversight.

### 4.3 Injection

Findings related to XSS, command injection, prototype pollution, and code execution vectors.

| ID | Severity | Title | Status |
|----|----------|-------|--------|
| C5 | Critical | `restart(url)` open redirect / javascript: URI execution in all 7 sub-apps | Confirmed (static) |
| H1 | High | encodeURI() parameter injection (5 instances in all 7 apps, includes double-? bug) | Confirmed -- present since v0.0.40 |
| H2 | High | Alpine.js Function() constructor (eval equivalent) in Stickerwall | Confirmed -- dead code but Function() is registered |
| H3 | High | innerHTML injection with user nicknames in DevBar (stored XSS, 19 sinks) | Confirmed (static) |
| M2 | Medium | postMessage wildcard origin leaks rrweb session replay data | Confirmed |
| M3 | Medium | No Content Security Policy on any of 9 HTML files | Confirmed |
| M11 | Medium | URI pass-through for javascript:, data:, file:, vbscript: schemes | Confirmed |
| M13 | Medium | Alpine.js x-html directive sets innerHTML with no sanitization | Confirmed -- dead code, latent risk |
| M14 | Medium | File upload: no client-side size/type/content validation | Confirmed |
| M15 | Medium | SSRF risk via root:// URI resolution through asset endpoint | Confirmed (static) |
| M18 | Medium | Dev bridge code ships in production (all 7 apps) -- devSetUser, devGetUsers exposed | Confirmed |
| M20 | Medium | WebSocket protocol weaknesses: plaintext ws://, no auth, self-asserted userId | Confirmed |
| M23 | Medium | Fabric.js loadFromJSON with untrusted data in Stickerwall -- IP exfiltration | Confirmed (static) |
| M24 | Medium | Suggestions app leaks anonymous creator identity via created_by field | Confirmed (static) |
| M25 | Medium | ReDoS via unescaped user input in `new RegExp()` for slash commands | Confirmed (static) |
| L2 | Low | DevBar code ships in production (5 sub-apps register custom element) | Confirmed |
| L4 | Low | console.log in production dumps state, user data, bridge messages | Confirmed |
| L9 | Low | Lexical Dragon NaturallySpeaking handler missing event.source check | Confirmed |
| L12 | Low | No SVG/HTML sanitization library (DOMPurify) in Stickerwall | Confirmed |
| L14 | Low | Sentry DEBUG_BUILD=true in Raid Planner (debug logging active in production) | Confirmed |
| B1 | Bug | Double-? URL bug in uriToImageUrl -- resolution parameter never reaches server | Confirmed since v0.0.40 |
| B2 | Bug | forEach with async callback creates fire-and-forget promises | Confirmed |
| B3 | Bug | process.exit() in browser code throws ReferenceError | Confirmed |
| B10 | Bug | Unknown message types crash client receive handler (permanent client DoS) | Confirmed (static) |

**Attack chain example**: An attacker exploits H3 (innerHTML XSS via DevBar nickname)
to trigger C5 (`restart("javascript:...")`) which executes arbitrary JavaScript in the
app context. From there, the attacker reads the bearer token from memory (H17), calls
`RefreshToken` to lock the victim out (M26), and maintains exclusive API access. Every
piece of this chain exists in shipped production code.

### 4.4 Privacy

Findings related to data leakage, telemetry, tracking, and PII exposure.

| ID | Severity | Title | Status |
|----|----------|-------|--------|
| H4 | High | `sendDefaultPii: true` in Sentry config -- IP, cookies, auth headers sent automatically | Confirmed |
| H5 | High | TURN/ICE credentials + full error payloads leaked to Sentry | Confirmed |
| H6 | High | Second Sentry DSN exposed (WebRTC) -- no origin restrictions | Confirmed exploitable |
| M1 | Medium | Sentry DSN exploitable (sub-apps) -- confirmed HTTP 200 on fake event | Confirmed |
| M16 | Medium | Sentry session replay at 25% error rate -- video-like DOM recording sent to Sentry | Confirmed |
| M21 | Medium | Sentry structured logging sends ICE credentials, IP addresses, full state dumps | Confirmed |
| M35 | Medium | TURN server credentials disclosed via GetIceInfo without active voice session | Confirmed |
| L1 | Low | Source maps shipped in production (13.5 MB, full original TypeScript) | Confirmed |
| L5 | Low | Persistent installation fingerprint (.betaId) sent on every update check | Confirmed |
| L7 | Low | Dev bridge HTTP API code shipped in production (ports 8080/8081/8082) | Confirmed |
| L13 | Low | Effects SDK developer build path disclosed (username `tmv`, macOS, project `vbsdk`) | Confirmed |
| L15 | Low | Staging icon shipped in production -- confirms staging/experimental/debug environments | Confirmed |
| L17 | Low | Protobuf schema fully extractable from JS bundle -- 637 message types | Confirmed |

**Privacy policy violations identified** (detailed in `research/docs/architecture/PRIVACY_ANALYSIS.md`):

1. Sentry not disclosed as a data processor (GDPR Art. 13/14 violation)
2. Session replay recording (25% of errors) without user consent (GDPR Art. 7)
3. `sendDefaultPii: true` transmits PII to US-based third party (GDPR Art. 44-49)
4. No data retention periods specified (GDPR Art. 13(2)(a))
5. Effects SDK (effectssdk.ai) not disclosed as third-party processor
6. No security page, no bug bounty, no responsible disclosure program
7. Do-Not-Track signals ignored while PII is transmitted automatically
8. No age-gating on telemetry -- minors' sessions recorded identically to adults

**Root v0.9.93 legal update (captured 2026-02-20):** February 19, 2026 Terms of Use still
explicitly prohibit modification/derivative works/reverse engineering/decompilation and broad
automated monitoring/copying behavior; see `research/docs/architecture/PRIVACY_ANALYSIS.md`
("Root v0.9.93 Policy Update").

### 4.5 Supply Chain

Findings related to dependency risks, update mechanism security, and code signing.

| ID | Severity | Title | Status |
|----|----------|-------|--------|
| H11 | High | EffectsSDK WASM models (11/52/7 MB) loaded without integrity verification | Confirmed -- no SRI, no checksums, CORS: * |
| M7 | Medium | Update manifest (releases.win.json) not cryptographically signed | Confirmed |
| M8 | Medium | No certificate pinning on API or update channels | Confirmed |
| L3 | Low | Effects SDK external WASM loading from effectssdk.ai (elevated to H11) | Confirmed |
| L6 | Low | External resources without SRI (Google Fonts, mc-heads.net, GitHub raw) | Confirmed |
| L10 | Low | Cache poisoning persistence for WASM models -- no TTL, no integrity check | Confirmed |
| L11 | Low | Math.random() fallback in Sentry UUID generation (non-cryptographic PRNG) | Confirmed |
| L16 | Low | Hexatris predictable PRNG (LCG) + shared seed exposed in multiplayer | Confirmed |
| B7 | Bug | Stale old app versions (0.0.40, 0.1.54, 0.1.33, 0.4.0) persist on disk | Confirmed |

**Critical supply chain risk**: Three WASM/ONNX audio processing models are downloaded
at runtime from `effectssdk.ai` with no integrity verification. The balanced model runs
as native WASM (Emscripten) with direct `HEAPF32` memory access to raw audio frames.
Compromise of effectssdk.ai would enable silent audio surveillance for all Root users.
The `Access-Control-Allow-Origin: *` header on effectssdk.ai means any domain can load
these models. The session API endpoint (`/sdk/session/`) returns 404, suggesting partial
service degradation.

### 4.6 Network

Findings related to TLS configuration, certificate pinning, and endpoint exposure.

| ID | Severity | Title | Status |
|----|----------|-------|--------|
| M8 | Medium | No certificate pinning -- standard OS cert store for api.rootapp.com and installer.rootapp.com | Confirmed |
| M27 | Medium | gRPC-web status returned in HTTP headers, bypasses body trailer parsing | Confirmed |
| M28 | Medium | Full gRPC service enumeration: 32 of 97+ endpoints implemented | Confirmed |
| L19 | Low | HubServer WebSocket endpoint disclosed | Confirmed |

**API security headers** (from api.rootapp.com responses):

| Header | Status |
|--------|--------|
| X-Frame-Options: SAMEORIGIN | Present |
| Referrer-Policy: same-origin | Present |
| Cache-Control: private, no-store, no-cache | Present |
| Strict-Transport-Security | **Missing** |
| Content-Security-Policy | **Missing** |
| X-Content-Type-Options | **Missing** |

### 4.7 Local

Findings related to file permissions, DPAPI usage, local storage security, and DLL integrity.

| ID | Severity | Title | Status |
|----|----------|-------|--------|
| H8 | High | SFTP passwords in plaintext via protobuf bridge (Minecraft app) | Confirmed (static) |
| M4 | Medium | DLL hijacking in user-writable location (10 DLLs, uiohook.dll = keylogger potential) | Confirmed |
| M5 | Medium | AuthToken without DPAPI -- user-readable 390-byte binary file | Confirmed |
| M6 | Medium | Local JS/HTML modifiable without integrity -- no SRI, no code signing at load | Confirmed |
| M12 | Medium | Minecraft RCON/SFTP credentials in plaintext via 137 protobuf message types | Confirmed (static) |
| M22 | Medium | Auth artifacts persist indefinitely -- data.json, Login Data, Web Data, Trust Tokens | Confirmed |
| L1 | Low | Source maps in production (13.5 MB) | Confirmed |
| L2 | Low | DevBar code in production builds | Confirmed |
| L15 | Low | Staging icon (rooticonstaging.ico) ships in production | Confirmed |
| B5 | Bug | Event listener leaks -- addEventListener without removeEventListener | Confirmed |
| B6 | Bug | No onerror handler on dev bridge WebSocket | Confirmed |
| B7 | Bug | Stale old versions on disk (4 apps) | Confirmed |

**Local file system attack surface**: All JavaScript bundles, HTML files, the WebRTC
bundle, and `registry.json` (which controls app loading) are user-writable. Five
confirmed attack chains exploit local file access: registry poisoning (redirect app
loading to attacker HTML), stale version hijacking (target older vulnerable bundles),
ghost CSS injection (inject styles via theme overrides), UNC path NTLM hash capture
(via modified asset URIs), and direct JavaScript modification (persistent backdoor).

**DLL risk**: All 10 DLLs under `%LOCALAPPDATA%\Root\current\` have full user write
permissions. Authenticode signatures are present but .NET does not verify them at
P/Invoke load time. `uiohook.dll` (UI input hooks) is the highest risk target for
DLL replacement, as it would provide keylogging capability within the Root process.

---

## 5. Critical Findings

The top 10 most impactful findings, ranked by combined severity, exploitability, and scope.

### Rank 1: C7 -- Cross-Community Unauthorized Channel Rename

| Field | Value |
|-------|-------|
| **Severity** | CRITICAL (CVSS 8.1) |
| **Service** | `root.ChannelGrpcService/Edit` |
| **Status** | Confirmed -- PoC executed against production |
| **Impact** | Any authenticated user can rename any channel in any community |

A non-admin, non-member user successfully renamed the "Chat" channel in the official Root
community (16,971 members) to "pwned-by-pentest" via a single gRPC-web API call.
`ChannelGrpcService/Delete` correctly enforces RBAC on the same channel, proving the
authorization framework exists but was not applied to the Edit endpoint. This same
oversight likely affects other Edit methods across services.

### Rank 2: C6 -- DM Creation with Any User Without Consent

| Field | Value |
|-------|-------|
| **Severity** | CRITICAL |
| **Service** | `root.DirectMessageGrpcService/Create` |
| **Status** | Confirmed -- DMs created with alt account and community owner |
| **Impact** | Unsolicited messaging to any platform user, harassment vector |

Successfully created DM conversations with arbitrary users by UUID. No friend request,
no shared community membership, and no consent flow required. Combined with the 16,971-user
member dump (M29), an attacker could spam every user on the platform.

### Rank 3: C5 -- restart(url) Open Redirect / JavaScript URI Execution

| Field | Value |
|-------|-------|
| **Severity** | CRITICAL (CVSS 8.8) |
| **Affected** | All 7 sub-apps |
| **Status** | Confirmed (static analysis) |
| **Impact** | Arbitrary JavaScript execution, phishing, credential theft |

The `restart(url)` bridge method navigates the entire application window to any URL with
zero validation. Combined with DotNetBrowser having no URL bar and no CSP, this enables
`javascript:` URI code execution, phishing via pixel-perfect fake login pages, and
`data:` URI code injection. Accessible from 4 global entry points.

### Rank 4: C3 -- Minecraft SendCommandRequest (Arbitrary Server Commands)

| Field | Value |
|-------|-------|
| **Severity** | CRITICAL (CVSS 9.8) |
| **Affected** | Minecraft Easy Setup sub-app |
| **Status** | Confirmed (static -- protobuf schema analysis) |
| **Impact** | Any community member can execute op, ban, stop, give on Minecraft servers |

The `SendCommandRequest` protobuf message accepts arbitrary command strings with no
server-side filtering or validation. Any community member with Minecraft app access can
execute operator-level commands on connected Minecraft servers.

### Rank 5: C4 -- GetAllServersResponse Leaks API Keys and SFTP Credentials

| Field | Value |
|-------|-------|
| **Severity** | CRITICAL (CVSS 8.6) |
| **Affected** | Minecraft Easy Setup sub-app |
| **Status** | Confirmed (static -- protobuf schema analysis) |
| **Impact** | All server credentials exposed to all community members |

`GetAllServersResponse` returns `api_key`, `sftp_host`, `sftp_port`, and `sftp_username`
for every server, unredacted, to any community member who loads the Minecraft app.

### Rank 6: H16 -- WebRTC Bundle Contains Full gRPC Client for All 27 Services

| Field | Value |
|-------|-------|
| **Severity** | HIGH |
| **Affected** | WebRTC bundle |
| **Status** | Confirmed |
| **Impact** | XSS in WebRTC context = full API access (163 methods across 27 services) |

The WebRTC bundle embeds gRPC-web clients for all backend services, not just WebRTC
endpoints. This includes `MessageGrpcService.Create` (send messages), `UserGrpcService.Delete`
(delete accounts), and `CommunityGrpcService.Delete` (destroy communities). The bearer
token is in JavaScript memory, so any XSS in the WebRTC context grants unrestricted API access.

### Rank 7: M26 -- Token Refresh Session DoS

| Field | Value |
|-------|-------|
| **Severity** | MEDIUM (HIGH impact when chained) |
| **Service** | `root.UserGrpcService/RefreshToken` |
| **Status** | Confirmed -- tested against own account |
| **Impact** | Token theft + RefreshToken = exclusive account access, victim locked out |

Calling `RefreshToken` issues a new token and immediately invalidates the previous one.
An attacker who steals a token can call RefreshToken to get a fresh token while breaking
the victim's entire session (all API calls return 401). The victim must restart the app
to re-authenticate. Combined with no session revocation (M19), the attacker maintains
exclusive access indefinitely.

### Rank 8: H24 -- SSRF via SetProfilePicture

| Field | Value |
|-------|-------|
| **Severity** | HIGH |
| **Service** | `root.UserGrpcService/SetProfilePicture` |
| **Status** | Confirmed -- DNS-based bypass works |
| **Impact** | Internal network probing, DNS rebinding attacks |

The server fetches URLs provided to SetProfilePicture. Direct IP addresses (169.254.169.254,
127.0.0.1) are blocked, but internal DNS names are processed normally, enabling DNS-based
SSRF and rebinding attacks against internal infrastructure.

### Rank 9: H11 -- EffectsSDK WASM Models Without Integrity

| Field | Value |
|-------|-------|
| **Severity** | HIGH |
| **Affected** | WebRTC bundle (all users with voice/video) |
| **Status** | Confirmed |
| **Impact** | Compromise of effectssdk.ai = silent audio surveillance |

Three models totaling 71 MB are downloaded at runtime from a third-party domain with
no SRI hashes, no checksums, and no code signing. The balanced model runs as native
WASM with direct memory access to raw audio frames. `Access-Control-Allow-Origin: *`
means any domain can serve replacement models.

### Rank 10: M29 -- Full Member List Exposure (16,971 Users)

| Field | Value |
|-------|-------|
| **Severity** | MEDIUM (amplifies all other findings) |
| **Service** | `root.CommunityMemberGrpcService/ListAll` |
| **Status** | Confirmed -- complete list in one API call |
| **Impact** | Complete user directory enables targeted attacks at scale |

The entire Root community member list (16,971 user UUIDs) was returned in a single
API call (~8 MB response) without requiring community membership. This enables targeted
exploitation of DM creation (C6), user lookup (M32/M33), and social graph mapping (L18).

---

## 6. Disclosure Status

### What Was Reported

All 105 findings were documented and reported to Root Communications through two channels:

1. **Support channel message** -- Initial disclosure with the most critical findings
   (Sentry DSN abuse, hardcoded token, TURN credential leak, innerHTML XSS)
2. **Direct contact** -- Detailed reports sent to Adrian (Root team member), including
   the C7 channel rename PoC, full API enumeration results, and all remaining findings

### Responses Received

The C7 channel rename was acknowledged through the reversal of the rename (the "Chat"
channel was restored to its original name). No formal written response was received
regarding the remaining findings. No bug bounty or acknowledgment program existed.

### What Remains Open

As of 2026-02-16, no formal response has been received confirming remediation of any
findings beyond the channel rename. The following critical items remain of unknown status:

- Hardcoded bearer token and IDs in production WebRTC bundle (C1, C2)
- Minecraft command injection (C3) and credential leakage (C4)
- Open redirect via restart() (C5)
- DM creation without consent (C6)
- Sentry PII leakage (H4, H5, H6)
- All client-side injection vectors (H1-H3, M11, M13, M23, M25)
- SSRF via profile picture (H24)
- No rate limiting (M36, M37)

---

## 7. Reports Sent

| Date | Report | Recipient | Content |
|------|--------|-----------|---------|
| 2026-02-12 | Bug Report Email | Root support channel | Sentry DSN abuse, hardcoded token, TURN leak, innerHTML XSS -- initial disclosure |
| 2026-02-12 | Report for Adrian | Adrian (Root team) | C7 channel rename PoC, cross-community enumeration, member list dump, DM abuse, SSRF, token refresh, rate limiting, user lookup |
| 2026-02-12 | C7 Channel Rename Report | Adrian (Root team) | Detailed PoC with raw protocol evidence, reproduction script, RBAC analysis, remediation plan |
| 2026-02-13 | C5 Open Redirect Report | Adrian (Root team) | restart(url) analysis, 4 exploitation methods, attack chain walkthrough, affected file inventory |
| 2026-02-13 | DevBar and IP Leak Report | Adrian (Root team) | DevBar in production analysis, Stickerwall Fabric.js IP exfiltration, chain scenarios |
| 2026-02-13 | Remaining Findings Report | Adrian (Root team) | Sentry leakage, bridge surface, XSS vectors, session/identity issues, local system risks, game exploits, supply chain, functional bugs |
| 2026-02-14 | Resolution Bug Report | Adrian (Root team) | Double-question-mark URL bug in uriToImageUrl (broken since v0.0.40) |

All reports are preserved in `research/docs/reports/`.

---

## 8. Recommendations

### Immediate Priority (P0)

| # | Action | Addresses |
|---|--------|-----------|
| 1 | Strip mock bridge class and hardcoded credentials from production builds | C1, C2, H7 |
| 2 | Add authorization check to `ChannelGrpcService/Edit` matching Delete's RBAC | C7 |
| 3 | Implement server-side command allowlisting for Minecraft SendCommand | C3 |
| 4 | Add field-level access control to GetAllServersResponse (strip api_key, SFTP creds) | C4 |
| 5 | Add URL scheme allowlist to `restart()` -- block javascript:, data:, file: | C5 |
| 6 | Require mutual consent for DM creation (friend request or shared community) | C6 |
| 7 | Set `sendDefaultPii: false` and add `beforeSend` hook to scrub credentials | H4, H5 |
| 8 | Rotate both Sentry DSN keys and enable origin restrictions | H6, M1 |

### Short-term Priority (P1)

| # | Action | Addresses |
|---|--------|-----------|
| 9 | Replace all `encodeURI()` with `encodeURIComponent()` and fix double-? bug | H1, B1 |
| 10 | Replace innerHTML with textContent for all user-supplied data in DevBar | H3 |
| 11 | Encrypt SFTP credential fields in protobuf bridge messages | H8 |
| 12 | Server must validate identity from auth token, not client-supplied protobuf fields | H9, H13 |
| 13 | Add origin/caller validation on all bridge methods | H10 |
| 14 | Implement SRI hashes and checksums for Effects SDK WASM models | H11 |
| 15 | Use signed session tokens instead of sessionStorage for user identity | H12 |
| 16 | Minimize WebRTC bundle service exposure (27 services down to WebRTC-only) | H16 |
| 17 | Strip DevBar and dev bridge code from production builds | L2, L7, M18 |

### Medium-term Priority (P2)

| # | Action | Addresses |
|---|--------|-----------|
| 18 | Implement Content Security Policy headers in DotNetBrowser | M3 |
| 19 | Protect AuthToken with DPAPI or Windows Credential Manager | M5, H18 |
| 20 | Add runtime integrity checks for local JS/HTML files | M6 |
| 21 | Cryptographically sign update manifest with asymmetric key | M7 |
| 22 | Implement certificate pinning on API and update channels | M8 |
| 23 | Implement logout flow with server-side session revocation | M19 |
| 24 | Implement secure deletion of auth artifacts on app close | M22 |
| 25 | Implement refresh token rotation with device binding | M26 |
| 26 | Add server-side rate limiting per token and per IP | M36, M37 |
| 27 | Restrict CommunityMemberGrpcService/ListAll to admins | M29 |
| 28 | Validate Fabric.js canvas_json schema, add DOMPurify to Stickerwall | M23, L12 |
| 29 | Strip created_by from anonymous suggestion responses | M24 |
| 30 | Add runtime DLL Authenticode verification | M4 |

### Long-term Priority (P3)

| # | Action | Addresses |
|---|--------|-----------|
| 31 | Adopt consistent authorization middleware for all gRPC methods | C7 pattern |
| 32 | Implement integration tests verifying auth on every endpoint | All authorization |
| 33 | Establish responsible disclosure / bug bounty program | Organizational |
| 34 | Conduct GDPR compliance audit for Sentry integration | H4, M16, privacy policy |
| 35 | Remove source maps from production builds | L1 |
| 36 | Consider server-authoritative model for Hexatris multiplayer | H15 |
| 37 | Move to server-side game state validation for all competitive features | H15, B9 |

---

## 9. Research Limitations

### What Was NOT Tested

The following areas were outside the scope of this assessment or could not be safely
tested without risk of service disruption:

- **Server-side code**: No access to backend source code. All server behavior was
  inferred from API responses. Authorization checks may exist server-side for protobuf
  bridge messages (H9 IDOR findings are based on static schema analysis).
- **Destructive operations**: `CommunityGrpcService/Delete`, `UserGrpcService/Delete`,
  and `COMMUNITY_DELETE` message type were not tested for authorization enforcement due
  to irreversible impact. `ChannelGrpcService/Delete` was tested (returns UNAUTH).
- **Message injection**: `v2.MessageGrpcService.Create` was not tested because message
  identity derives solely from the bearer token -- testing would require acting as another
  user, which was out of scope.
- **WebRTC media plane**: Actual voice/video interception, SRTP key extraction, and
  media stream manipulation were not tested.
- **SVG script execution in DotNetBrowser**: Whether `--disable-web-security` breaks
  SVG script sandboxing in the Fabric.js rendering path (M23) was not live-tested.
- **Mobile clients**: Only the Windows desktop app was tested. Android/iOS clients
  (if they exist) were not assessed.
- **Social engineering**: No phishing, pretexting, or social engineering was attempted.
- **Physical access attacks**: No hardware-level attacks or physical access scenarios.
- **Other communities**: Only the Root official community and the researcher's own
  PentestCommunity were targeted. Other communities were not enumerated or modified.

### Scope Boundaries

- All active testing used the researcher's own accounts only
- The channel rename (C7) was the only modification to a shared resource; it was
  immediately disclosed with a request to restore
- No data was exfiltrated from other users' accounts
- Token extraction was performed only on the researcher's own process and files
- DM creation testing targeted only the researcher's alt account and one Root team member
  (disclosed immediately)

---

## 10. Evidence Index

All raw evidence, exploit scripts, and supporting documentation is organized under
the `research/` directory tree:

### Documentation

| Path | Contents |
|------|----------|
| `research/docs/findings/FINDINGS.md` | All 105 findings in chronological order with full details |
| `research/docs/findings/ATTACK_SURFACE.md` | Complete attack surface map with bridge methods, gRPC services, file targets |
| `research/docs/findings/VULNERABILITY_REPORT.md` | Formal vulnerability assessment with CVSS scores and remediation priorities |
| `research/docs/findings/EVIDENCE.md` | Verbatim code extracts, exact file paths, line numbers, and shell output for each finding |
| `research/docs/architecture/PRIVACY_ANALYSIS.md` | Privacy policy vs actual behavior -- 10 specific discrepancies with GDPR/CCPA analysis |
| `research/docs/architecture/PENTEST_PLAN.md` | Original 6-phase penetration test plan with 21 initial findings |
| `research/docs/architecture/TOKEN_EXTRACTION.md` | 5 methods for extracting the Root bearer token |
| `research/docs/architecture/THEME_EXTRACTION.md` | Root theme system analysis |

### Reports (as sent to Root)

| Path | Contents |
|------|----------|
| `research/docs/reports/BUG_REPORT_EMAIL.md` | Initial disclosure via support channel |
| `research/docs/reports/REPORT_FOR_ADRIAN.md` | Comprehensive report to Root team contact |
| `research/docs/reports/REPORT_C7_CHANNEL_RENAME.md` | C7 detailed PoC with raw protocol evidence |
| `research/docs/reports/C5_RESTART_OPEN_REDIRECT.md` | C5 open redirect analysis and attack chains |
| `research/docs/reports/DEVBAR_AND_IP_LEAK.md` | DevBar production exposure and Stickerwall IP exfiltration |
| `research/docs/reports/REPORT_REMAINING_FINDINGS.md` | All remaining findings organized by theme |
| `research/docs/reports/REPORT_RESOLUTION_BUG.md` | Double-question-mark URL bug report |

### Exploit Scripts

| Path | Purpose |
|------|---------|
| `research/pentesting/grpc_lib.py` | Shared gRPC-web library: protobuf encoding, call(), token loading |
| `research/pentesting/phases/phase1_access_rules.py` | Phase 1: access rules, channel groups, channels (C7 discovered here) |
| `research/pentesting/phases/phase2_file_directory.py` | Phase 2: file and directory services |
| `research/pentesting/phases/phase3_community_apps.py` | Phase 3: community app services |
| `research/pentesting/phases/phase4_invites_members.py` | Phase 4: invites, members, social (M29 discovered here) |
| `research/pentesting/phases/phase5_notifications_users.py` | Phase 5: notifications, users (H24, M26 discovered here) |
| `research/pentesting/phases/phase6_webrtc_social.py` | Phase 6: WebRTC, social graph (M35 discovered here) |
| `research/pentesting/exploits/grpc_exploit.py` | Hardcoded token testing against production API |
| `research/pentesting/exploits/exploit_dm.py` | DM creation exploit (C6) |
| `research/pentesting/exploits/exploit_final.py` | Comprehensive final exploit suite |
| `research/pentesting/extraction/memory_scan.py` | Process memory scanning for bearer tokens (H17) |
| `research/pentesting/extraction/extract_and_test.py` | Token extraction and validation |

### Analysis Scripts

| Path | Purpose |
|------|---------|
| `research/pentesting/js/audit.js` | JavaScript bundle security audit |
| `research/pentesting/js/extract.js` | Protobuf schema extraction from minified JS |
| `research/pentesting/js/analyze_hexatris.js` | Hexatris game model analysis (H15) |
| `research/pentesting/extraction/source_map_extract.py` | Source map deobfuscation |
| `research/pentesting/extraction/extract_mock_bridge.py` | Mock bridge credential extraction (C1, C2) |

---

## Positive Security Findings

For completeness, the following security controls were found to be properly implemented:

1. **Authenticode code signing** on all binaries (Root.exe, 10 DLLs) with valid Microsoft ID Verified certificate
2. **AuthToken encryption** -- stored as binary/encrypted data, not plaintext (though not DPAPI)
3. **HTTPS for all remote communication** -- API, updates, and telemetry all use TLS
4. **Cloudflare protection** on API and update infrastructure
5. **Origin validation** on postMessage handlers in sub-apps
6. **URL sanitization** functions present (sanitizeURL, sanitizePath, SAFE_URL_PROTOCOLS)
7. **No eval()** in any production sub-app (0 instances; Alpine.js uses Function() instead)
8. **File upload accept filtering** -- client-side MIME type restrictions applied
9. **ACLs** restrict file access to user + SYSTEM + Administrators
10. **MessageCreateRequest** has no user_id field -- sender identity derived from bearer token, preventing message impersonation via field manipulation
11. **Token signature** -- 96-byte cryptographic signature prevents token forgery
12. **Token refresh locking** -- server correctly handles concurrent RefreshToken calls (only 1 succeeds)
13. **ChannelGrpcService/Delete** properly enforces RBAC (returns UNAUTH for non-admins)
14. **CommunityGrpcService/Delete** properly enforces RBAC (returns UNAUTH for non-owners)

---

*End of Security Research Summary*

*Raw data: `research/docs/findings/` | Exploit code: `research/pentesting/` | Reports: `research/docs/reports/`*

---

**Canonical for:** 105 security findings (authentication, authorization, data exposure, injection, privacy, infrastructure), severity ratings, evidence links, attack surface summary
**Not canonical for:** RE methodology → [REVERSE_ENGINEERING.md](REVERSE_ENGINEERING.md) | mitigation strategies → [MITIGATION_COUNTERMEASURES.md](MITIGATION_COUNTERMEASURES.md) | gRPC protocol → [GRPC_PROTOCOL.md](GRPC_PROTOCOL.md)
*Security research summary. Last updated 2026-02-19.*
