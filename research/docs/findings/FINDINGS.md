# Root Communications v0.9.86 - Security & Bug Findings

105 findings: 7 Critical, 23 High, 37 Medium, 20 Low, 13 Functional Bugs, 5 Informational

---

## CRITICAL

### C1. Hardcoded Bearer Token in Production WebRTC Bundle
File: WebRtcBundle\rootapp-desktop-webrtc.js:13929
Token appears 4 times in mock bridge class `Lqe`. Used as Authorization: Bearer header.
Token: AChObn-FjgGkgtSzdaVi0wAsrDJSU4oSpE_mAVP-Hid2s1K_... (128 bytes decoded, NOT JWT)
userId embedded in first 16 bytes = 00284e6e-7f85-8e01-a482-d4b375a562d3.
Combined with hardcoded userId, deviceId, communityId, channelId.
Gate: `SA()` user-agent check for "rootplatform", trivially bypassable.
Debug panel with admin controls (kick, mute, deafen) gated by same SA() check.
ACTIVE TEST: Token returns HTTP 401 (expired/revoked on server). However, token format and
endpoint confirmed working: gRPC-web calls to api.rootapp.com with application/grpc-web+proto
content type reach the actual auth layer (401, not 403 from WAF).
FIX: Strip mock bridge from production builds.

### C2. Hardcoded User/Community/Channel IDs
File: WebRtcBundle\rootapp-desktop-webrtc.js:13929
userId=AChObn-FjgGkgtSzdaVi0w, deviceId=ACysMlJTihKkT-YBU_4eJw
communityId=ACiNMcK2gwKa7z5MDT_ScQ, channelId=ACiNMcK3iQSUPvZBKshscQ
All IDs decode to 16-byte UUIDs with common `AC` prefix. Full credential + target set for API access.

### C3. Minecraft SendCommandRequest - Arbitrary Server Commands
File: RootApps\002c6a0d-3616-...\0.4.5\...\index-Ct4GOtmZ.js
Protobuf message `minecrafteasysetup.SendCommandRequest` sends arbitrary commands to Minecraft servers.
Fields: { server_id: string, command: string }. No command filtering or validation.
Any community member with access to the Minecraft app can execute op, ban, stop, give, etc.
Combined with XSS via bridge: `window.parent.__rootSdkBridgeWebToNative.send(craftedProtobuf)`.
FIX: Server-side command allowlisting. Role-based command authorization.

### C4. GetAllServersResponse Returns API Keys + SFTP Credentials to All Clients
File: Minecraft app protobuf definitions
`GetAllServersResponse` returns `repeated Server` where each Server contains:
- `api_key` (field 6) - full server plugin API key
- `sftp_host`, `sftp_port`, `sftp_username` (fields 20-22)
Any community member loading the Minecraft app receives all server credentials.
FIX: Field-level access control. Never return api_key to non-admin users.

### C5. restart(url) Open Redirect / JavaScript URI Execution
File: All 7 sub-app index-*.js (line ~35630)
`restart(e$9) { e$9 ? window.location.href = e$9 : window.location.reload(); }`
ZERO validation on the URL parameter. Any call to:
`window.parent.__rootSdkBridgeWebToNative.restart("javascript:alert(document.cookie)")`
navigates the entire app to attacker-controlled URL.
Enables: phishing, credential theft, javascript: URI code execution.
FIX: URL scheme allowlist (https:// only). Validate against known hosts.

---

## HIGH

### H1. encodeURI() Parameter Injection (5 Instances, All 7 Apps)
File: All 7 sub-app index-*.js (lines ~35505-35525)
5 instances of encodeURI() where encodeURIComponent() is required:
1. `searchUserProfiles`: `users/search?search=` + encodeURI(input) - parameter injection via &
2. `uriToUrl`: `asset?query=` + encodeURI(uri) - parameter injection
3. `uriToImageUrl`: `asset?query=` + encodeURI(uri) + `?resolution=` + encodeURI(res) - DOUBLE-? BUG
4. `uploadTokenToPreviewImageUrl`: `asset/preview?query=` + encodeURI(token) - parameter injection
5. TanStack Router `_splat` parameter: encodeURI(value) instead of encodeURIComponent
Double-? bug confirmed in ALL 7 current + 4 older versions (11 bundles total, since v0.0.40).
Resolution parameter is NEVER received by the server as a separate parameter.
FIX: Replace all encodeURI with encodeURIComponent. Fix ? to &.

### H2. Alpine.js Function() Constructor (eval equivalent)
File: RootApps\002a3d84-...\0.4.8\...\index-Dg0hJEdq.js:14
Function(["scope"], `with (scope) { let __result = ${expression}; return __result }`)
If user input reaches Alpine.js directives = arbitrary JS execution.
NOTE: Deep audit confirmed Alpine.js is DEAD CODE in Stickerwall — bundled via Preline UI/HSOverlay
dependency but the app uses React for all rendering. Zero Alpine.store(), Alpine.data(), or x-data
directives in app code. However, the Function() constructor is still available in the bundle and
could be invoked programmatically. Risk reduced but not eliminated without CSP.
FIX: CSP with unsafe-eval blocked. Tree-shake or remove unused Alpine.js dependency.

### H3. innerHTML Injection with User Data (Stored XSS)
File: RootApps\002a3d83-c35a-...\0.4.11\...\index-BG-RjBRK.js:35249
DevBar injects nickname and ID into innerHTML without sanitization.
19 innerHTML sinks in Raid Planner, 5 confirmed exploitable (all in DevBar).
DevBar present in ALL 5 sub-apps that have it, ships in production.
Exploitable sinks: nickname+ID (35249), nickname in selector (35170), alternate render (35244),
community role names (35261), theme name with attribute injection (35269).
FIX: Use textContent. Strip DevBar from production.

### H4. sendDefaultPii: true + No beforeSend Hook
File: WebRtcBundle\rootapp-desktop-webrtc.js:523
Sentry.init() config: `sendDefaultPii: true, enableLogs: dF !== "debug"`
Automatically sends IP, cookies, user-agent, request headers to Sentry.
NO `beforeSend` hook configured. NO `beforeBreadcrumb` hook.
Authorization: Bearer headers captured in breadcrumbs and sent to Sentry.
RTCPeerConnection stats (local/remote IP addresses) sent via Sentry.logger.warn().
9 default integrations loaded (Breadcrumbs captures console, dom, fetch, history, xhr).
FIX: Set sendDefaultPii: false. Add beforeSend + beforeBreadcrumb hooks to scrub PII.

### H5. TURN/ICE Credentials + Full Error Payloads Leaked to Sentry
File: WebRtcBundle\rootapp-desktop-webrtc.js:598, 13768-13775
TURN credentials dynamically fetched via `getIceInfo` gRPC call.
On error, credential+username included in error payload via `Nn(Ot.PeerConnectionFailed, t, {credential, username})`.
Redux middleware sends ALL failed actions to Sentry via `Sentry.logger.warn()` including:
- Full error payload (with ICE credentials)
- currentUserId, username, channelId, communityId
- RTCPeerConnection stats
- Accumulated T9 string of ALL WebRTC state changes
enableLogs is true for all non-debug environments.
FIX: Sanitize error payloads. Never include credentials. Add beforeSend hook.

### H6. Second Sentry DSN Exposed (WebRTC) - No Origin Restrictions
File: WebRtcBundle\rootapp-desktop-webrtc.js:523
DSN: https://75fd2cd278ac35657edd7ee8df86eb37@o4509469920133120.ingest.us.sentry.io/4509832005681152
Confirmed: ZERO origin restrictions. Accepts events from any source (curl, any domain).
DSN 1 (sub-apps): Has partial origin allowlist (accepts null + no-header, rejects named origins).
Both confirmed exploitable: error events, PII-laden events, replay events all return HTTP 200.
FIX: Rotate both DSN keys. Enable origin restrictions on DSN 2.

### H7. Hardcoded localhost:3005 + Debug Alert
File: WebRtcBundle\rootapp-desktop-webrtc.js:13929
Mock bridge: window.__rootApiBaseUrl = "https://localhost:3005"
alert("Something terrible happened... Error: " + JSON.stringify(e))
Opens in any non-rootplatform browser with hardcoded bearer token.
FIX: Strip mock bridge from production.

### H8. SFTP Passwords in Plaintext via Protobuf Bridge
File: Minecraft app protobuf definitions
`SftpConnectRequest.password` (field 4) and `UpdatePluginViaSftpRequest.sftp_password` (field 2)
send SFTP passwords in cleartext over the IPC bridge.
`ReadPluginConfigResponse.api_key` returns API key in plaintext.
`SaveServerRequest` persists api_key server-side.
No encryption between web iframe and native bridge for credential messages.
FIX: Encrypt credential fields. Use credential manager for storage.

### H9. IDOR: Forgeable user_id/leader_id in Protobuf Requests
File: Raid Planner protobuf definitions
Multiple client-to-server messages accept user identity as a client-supplied field:
- `CreateRaidUserRequest.user_id` - sign up OTHER users for raids
- `CreateRaidRequest.leader_id` - impersonate raid leadership
- `CreateCommentRequest.user_id` - post comments as other users
- `CreateRaidActionLogRequest.actor_id` - forge audit log entries
- `CancelRaidRequest.id` / `DeleteCommentRequest.id` / `DeleteTemplateByIdRequest.id` - delete by ID
- `ForceActivateRaidRequest.id` - force-activate any raid
All messages sent via `window.parent.__rootSdkBridgeWebToNative.send()`.
FIX: Server MUST validate user identity from auth token, not client-supplied fields.

### H10. XSS = Full Bridge API Access (No Authorization on Any Method)
File: All sub-apps + WebRTC bundle
None of the bridge methods perform authorization checks:
- `__rootSdkBridgeWebToNative.send(buffer)` - raw protobuf injection (bypasses rate limiter)
- `__webRtcToNative.kickPeer(userId)` - kick any user from call
- `__webRtcToNative.setAdminMute(deviceId, true)` - admin mute anyone
- `__webRtcToNative.setAdminDeafen(deviceId, true)` - admin deafen anyone
- `__nativeToWebRtc.kick(userId)` / `.setAdminMute()` / `.setAdminDeafen()`
- `__nativeToWebRtc.receiveRawPacket(buffer)` - inject signaling packets
- `__nativeToWebRtc.updateMyPermission(perms)` - client-side permission escalation
- `__rootSdkBridgeNativeToWeb.receive(protobuf, type)` - inject fake server responses
- `__rootSdkBridgeNativeToWeb.setTheme(obj)` - CSS injection via property values
FIX: Origin/caller validation. Minimum: validate caller context.

### H11. EffectsSDK WASM Models Loaded Without Integrity Verification
File: WebRtcBundle\rootapp-desktop-webrtc.js:4577-4689
Three WASM/ONNX models (11MB, 52MB, 7MB) downloaded at runtime from effectssdk.ai:
- `audio-model-3.3.2.wasm` (balanced, native WASM) - processes raw audio frames
- `audio-model-2.5.0.tsvb` (quality, ONNX) - 52.8 MB
- `audio-model-1.0.1.tsvb` (speed, ONNX) - 7.1 MB
NO SRI hashes, NO checksums, NO code signing on any of them.
Cache API (`caches.open('atsvb')`) stores models with no expiration or integrity checks.
CORS: `Access-Control-Allow-Origin: *` on effectssdk.ai.
Session API endpoint (`/sdk/session/`) returning 404. ONNX Runtime dev/3.2.2 paths all 404.
Compromise of effectssdk.ai = silent audio surveillance for all Root users.
FIX: Bundle models locally. Or implement SRI hashes + checksum verification.

### H12. SessionStorage Poisoning = User Impersonation
File: All 7 sub-app index-*.js (lines ~35573-35611)
`__rootUser` read from sessionStorage with ZERO validation:
```
#ge() { let e = sessionStorage.getItem("__rootUser"); if (e) return JSON.parse(e); ... }
```
An XSS attacker can set arbitrary userId, nickname, communityRoleIds, then reload.
Dev bridge WebSocket CLIENT_ATTACH sends this identity with no authentication token.
No challenge-response, no token, no HMAC on the WebSocket handshake.
FIX: Validate user identity server-side. Use signed session tokens.

### H13. AppRpcMessageToHost — Client-Supplied Identity in Transport Envelope
File: All 7 sub-app index-*.js (lines ~34612-34717)
The transport envelope `AppRpcMessageToHost` wraps every protobuf message with client-supplied identity:
- `user_id` (field 7) — client asserts their own identity
- `device_id` (field 8) — client asserts device
- `community_id` (field 4) — client asserts community context
Production bridge `Ne$2.create()` sends only `{path, body, requestId}` (NO userId), so the .NET native
bridge presumably fills identity from the auth token. However, the dev bridge wraps every message with
`userId` from sessionStorage `__rootUser`, making impersonation trivial in dev mode.
The inner `AppRpcMessage` protobuf contains only path + body + requestId, meaning the outer envelope
is the sole source of identity for routing. If the server ever trusts the envelope's user_id over
the auth token, any client can impersonate any user.
FIX: Server MUST ignore client-supplied user_id/device_id in envelope. Always derive from auth token.

### H14. COMMUNITY_DELETE Message Type — Community Destruction by Any Client
File: All 7 sub-app index-*.js (lines ~34718-34760)
The `AppRpcMessageType` enum includes `COMMUNITY_DELETE` (value 8 in enum), and the `AppRpcMessageToHost`
protobuf message is fully client-serializable. Any code with bridge access can construct and send a
COMMUNITY_DELETE message type through `window.parent.__rootSdkBridgeWebToNative.send()`.
Whether the server validates authorization for this message type is unknown, but the client provides
the full serialization capability. Combined with XSS (H3/H10), this is a potential community destruction
vector.
FIX: Server-side authorization check for destructive message types. Client should not be able to serialize admin-only operations.

### H15. Hexatris Client-Authoritative Game Model — Score/Board/LineClear Forgery
File: RootApps\002c8f0a-...\0.4.2\...\index-VcQt9H-b.js
Hexatris multiplayer operates on a fully client-authoritative model. Three protobuf messages allow
arbitrary game state manipulation:
- `ReportGameOverRequest`: Client sends `finalScore`, `linesCleared`, `maxCombo`, `garbageSent`,
  `garbageReceived`, `piecesPlaced` — all read from local game engine, trivially modifiable via DevTools
- `UpdateBoardStateRequest`: Client sends entire board state including score, level, combo —
  can send fabricated empty board while real board is nearly full
- `ReportLineClearRequest`: Client reports `linesCleared`, `combo`, `isTetris` — fake Tetris clears
  with high combo flood opponent with unearned garbage lines
Server responses (`UpdateBoardStateResponse`, `ReportLineClearResponse`) contain only `{accepted, error}`
with no evidence of server-side board state validation, hash checking, or consistency verification.
FIX: Server-side game state validation. Implement move-chain verification or server-authoritative model.

---

## MEDIUM

### M1. Sentry DSN Exploitable (Confirmed HTTP 200)
DSN: https://c1dfb07d783ad5325c245c1fd3725390@o447951.ingest.sentry.io/4509632503087104
SDK: sentry.javascript.browser v1.33.7 (outdated, latest 8.x)
Has partial origin allowlist: accepts `Origin: null` and no-Origin header, rejects named origins.
FIX: Rotate key. Enable strict origin restrictions. Rate limit. Update SDK.

### M2. postMessage Wildcard Origin Leaks Session Replay Data
File: WebRtcBundle\rootapp-desktop-webrtc.js:502
`window.parent.postMessage({ type: "rrweb", event: le(at), origin: window.location.origin, isCheckout: ot }, "*")`
Complete rrweb session recording events (DOM snapshots, mouse movements, input values, mutations) sent to ANY parent frame.
FIX: Specify expected origin instead of "*".

### M3. No Content Security Policy
All 9 HTML files. Host iframe: no sandbox attribute, no allow attribute.
No script-src, no frame-ancestors, no object-src restrictions.
FIX: Implement strict CSP.

### M4. DLL Hijacking in User-Writable Location
All 10 DLLs in %LOCALAPPDATA%\Root\current\ are user-writable.
uiohook.dll is highest risk (UI input hooks - keylogger potential).
.NET doesn't verify Authenticode at P/Invoke load.
FIX: Runtime signature verification. Read-only ACLs post-install.

### M5. AuthToken Without DPAPI
Store\AuthToken: 390 bytes, binary, user-readable.
ENTROPY\bash:(I)(F) permissions. Not protected by DPAPI or Credential Manager.
FIX: Protect with DPAPI or Windows Credential Manager.

### M6. Local JS/HTML Modifiable Without Integrity
All 7 app bundles + WebRTC bundle are user-writable.
No SRI, no code signing verification at load time.
registry.json (controls which apps load) also user-writable and unsigned.
5 attack chains: registry poisoning, stale version hijacking, ghost CSS injection,
UNC path NTLM capture, direct JS modification. 11 broken file references found.
FIX: Implement integrity checks. Sign registry.json.

### M7. Update Manifest Not Signed + Downgrade Attack
releases.win.json: static file, ignores all query parameters.
SHA256 hashes but no cryptographic signature.
Old versions (0.9.82-0.9.85) still downloadable with known hashes = downgrade attack.
Packages publicly enumerable (413MB nupkg directly downloadable).
No HSTS, no certificate pinning on installer.rootapp.com.
FIX: Asymmetric signature on manifest. Remove old versions. Enforce minimum version.

### M8. No Certificate Pinning
Standard OS cert store only for api.rootapp.com + installer.rootapp.com.
Both behind Cloudflare (172.66.154.209, 104.20.41.137).
FIX: Implement certificate pinning.

### M9. Native Bridge Accessible to Any Code (Documented in H10)
window.__rootSdkBridgeWebToNative exposed to all code in DotNetBrowser.
Raw .send() bypasses internal path validation and rate limiter (256KB/10 req/s).
Rate limiter is client-side only (`Ce$1`): max 10 req/s, max 524288 bytes/s.
FIX: Origin/caller validation on bridge methods. Server-side rate limiting.

### M10. User Identity in sessionStorage (Documented in H12)
__rootUser and __virtualRootUser store userId, deviceId, nickname as plaintext JSON.
XSS = trivial exfiltration + impersonation.
FIX: Move to in-memory store with server-signed tokens.

### M11. URI Pass-Through for Dangerous Schemes
uriToUrl() returns non-root:// URIs unchanged.
javascript:, data:, file:, vbscript: schemes all pass through unfiltered.
React's sanitizeURL blocks javascript: on href but NOT on img src.
data: URIs with SVG content can execute in some contexts.
FIX: Protocol allowlist in bridge (root:// and https:// only).

### M12. Minecraft RCON/SFTP Credentials via Protobuf (Documented in H8)
137 total protobuf message types across Raid Planner (86) + Minecraft (51).
Additional high-risk messages:
- `InstallPluginRequest` with session_id replay = unauthorized plugin installation
- `UpdateServerRequest` can change server API key/endpoint to attacker values
- `OnlinePlayer.is_op` reveals operator status; x/y/z enables player tracking
FIX: Server-side session validation. Encrypt credential fields.

### M13. Alpine.js x-html Directive
Stickerwall: directive("html",...) sets e.innerHTML = t with no sanitization.
NOTE: Alpine.js is dead code in Stickerwall (bundled via dependency, not used by app).
The x-html handler is registered but no x-html directives exist in app templates.
Latent risk only — exploitable if Alpine is activated programmatically.
FIX: Remove unused Alpine.js dependency. Or sanitize x-html if Alpine is needed.

### M14. File Upload: No Client-Side Validation
SDK bridge uploads via FormData with no size/type/content checks.
FIX: Add client-side validation. Server-side is critical.

### M15. SSRF Risk via root:// URI Resolution
asset?query=<uri> could access internal services if server doesn't validate.
encodeURI provides insufficient protection (doesn't encode & = ? # @ +).
FIX: Server-side URI validation + encodeURIComponent.

### M16. Sentry Session Replay at 25% Error Rate
replaysOnErrorSampleRate: .25 + enableLogs: dF !== "debug"
Video-like DOM recording sent to Sentry US servers with PII.
Sentry.logger.warn() sends structured logs including full WebRTC state dumps.
FIX: Disable or disclose with user consent.

### M17. Client-Side Permission Bypass via React Query Cache
File: Raid Planner index-BG-RjBRK.js:88771-88819
AuthProvider derives `isAdmin` from `useGetPermissions()` React Query response.
Route guard on templateLayoutRoute uses context.isAdmin.
All component checks (canManageRaid, canEdit, canDelete) are client-side only.
An attacker can override: `queryClient.setQueryData(["permissions"], { isAdmin: true })`.
Only 2 permission flags in entire model: isAdmin, isRaidCreator.
FIX: Enforce all permissions server-side. Client checks are UI-only.

### M18. Dev Bridge Code Ships in Production (All 7 Sub-Apps)
File: All 7 sub-app index-*.js (line ~35634)
Activation: `navigator.userAgent.toLowerCase().indexOf("rootsdk") < 0`
When active: `window.__rootSdkBridgeDev = e$9; window.rootSdk = e$9;`
Exposes: devSetUser, devGetUsers, devUserAddRole, devUserRemoveRole, devSetTheme.
All dev endpoints are unauthenticated HTTP on ports 8080/8081/8082.
Ports are closed in production, but the code + API wiring ships.
FIX: Strip dev bridge from production builds entirely.

### M19. No Session Invalidation / No Logout Mechanism
No logout, sign-out, or session invalidation code in either bundle.
No server-side session invalidation triggered from client.
No session rotation. No idle timeout. No periodic re-authentication.
SessionStorage persists until tab is closed.
FIX: Implement logout flow with server-side session revocation.

### M20. WebSocket Protocol Weaknesses (Dev Bridge)
File: All 7 sub-app index-*.js (lines ~35360-35402)
- Plaintext ws:// not wss:// in dev bridge
- No authentication in WebSocket handshake (self-asserted userId)
- Complete identity spoofing possible
- No reconnection logic
- Unhandled opcodes crash client
- Update socket connects to wrong port (bug)
- Update socket is dead code
- Predictable sequence numbers, no replay protection
FIX: Use wss://. Add authentication token to handshake.

### M21. Sentry Structured Logging Sends Credentials
File: WebRtcBundle\rootapp-desktop-webrtc.js:13768-13775
`{logger: Vee} = oye` where Vee = Sentry.logger (from SDK's `ise` export).
`enableLogs: dF !== "debug"` enables this for production/staging/experimental.
On every failed WebRTC Redux action, `Vee.warn(T9, { error: t.payload, stats, ... })` sends:
- Full error payload (can include ICE credentials)
- Accumulated T9 string of ALL state changes
- RTCPeerConnection stats (IP addresses, ports)
FIX: Disable enableLogs or add sensitive data filtering.

### M22. Auth Artifacts Persist After Logout — Token Replay Risk
Files: Multiple locations under Root Communications profile directory
Authentication artifacts persist on disk indefinitely with no secure deletion:
- `data.json` (1,737 bytes, encrypted, NOT DPAPI) — contains auth token, persists after app close
- `Login Data` (40KB SQLite) — Chromium credential storage, persists
- `Account Web Data` (76KB SQLite) — account data, persists
- `Web Data` (150KB SQLite) — cookies/form data, persists
- `Trust Tokens` (SQLite) — OAuth-style tokens, persists
Combined with M19 (no logout mechanism): once a user authenticates, their credentials persist
indefinitely on disk. An attacker with file access (M5, M6) can extract and replay the auth token
even after the user believes they have "closed" the app.
The data.json encryption is NOT DPAPI (first bytes: f1 6d 6f 73), Shannon entropy 7.67/8.0.
Encryption scheme unknown without .NET binary decompilation.
FIX: Implement secure session management with expiry. Use DPAPI for on-disk credential storage. Implement logout with secure deletion of all auth artifacts.

### M23. Fabric.js loadFromJSON with Untrusted Data (Stickerwall)
File: RootApps\002a3d84-...\0.4.8\...\index-Dg0hJEdq.js
Stickerwall uses Fabric.js `loadFromJSON()` to deserialize canvas data from a `canvas_json` field.
The JSON is loaded without any sanitization or schema validation. Fabric.js `loadFromJSON` calls
`FabricImage.fromObject()` which fetches ANY URL specified in the `src` field of Image objects.
Attack: A malicious user creates a sticker with canvas_json containing `{"type":"Image","src":"https://attacker.com/beacon"}`.
When ANY other user opens or previews this sticker, their client fetches the attacker URL, leaking
their IP address. The `crossOrigin` attribute is set from the JSON data. Stickers are broadcast to
ALL users in real-time via `broadcastLightweightPlacement`.
SVG generation via `toSVG()` uses `escapeXml` for text content but Image/path attributes from
the JSON pass through, potentially allowing SVG attribute injection in the broadcast.
canvas_json is gzip-compressed + base64 encoded, but raw JSON is accepted if no "gzip:" prefix.
Zero DOMPurify or any HTML/SVG sanitization library in the entire Stickerwall app.
FIX: Validate canvas_json schema before loading. Allowlist object types. Block external URLs in Image objects. Add DOMPurify.

### M24. Suggestions App Leaks Anonymous Creator Identity
File: RootApps\002c6a0d-a29d-...\0.4.4\...\index-*.js
The Suggestions app has an anonymous suggestions feature, but the `created_by` field (user_id)
is included in the response payload sent to all clients. The field is present in the protobuf
response even when the suggestion was submitted anonymously, allowing any client to de-anonymize
the suggestion author by matching the `created_by` user_id against the user directory.
FIX: Server must strip `created_by` from responses for anonymous suggestions.

### M25. ReDoS via Unescaped User Input in RegExp Constructor
File: RootApps\002a3d83-c35a-...\0.4.11\...\index-BG-RjBRK.js:82164
Slash command typeahead passes user query directly into `new RegExp(queryString, "i")` without
escaping regex special characters. The regex is then tested against all option titles and keywords.
A malicious input like `((((.+)+)+)+)+` causes catastrophic backtracking (ReDoS), freezing the
browser main thread. While primarily self-inflicted, deeplinks or pre-filled editor state could
enable external exploitation.
FIX: Escape regex special characters before `new RegExp()`, or use `String.includes()` instead.

---

## LOW / INFORMATIONAL

### L1. Source Maps in Production
rootapp-desktop-webrtc.js.map: 13.5 MB. Full source code exposed.
FIX: Exclude from production builds.

### L2. DevBar Code in Production
Full dev toolbar with innerHTML XSS vulnerabilities in ALL 5 sub-apps.
Custom element `rootsdk-devbar` defined when userAgent lacks "rootsdk".
FIX: Strip from production.

### L3. Effects SDK External WASM (Supply Chain) (Elevated to H11)
effectssdk.ai loads WASM at runtime without SRI.
Session API returning 404. ONNX Runtime dev/3.2.2 paths all 404.
`Access-Control-Allow-Origin: *` allows any domain to load models.
FIX: Add SRI hashes. Bundle locally. Pin versions with checksums.

### L4. console.log in Production
WebSocket state, user data, payloads, bridge messages logged.
Full state dumps via logging middleware (Old State, New State, Payload).
Debug: `Re$2("Sending %s %o", path, message)` logs all gRPC requests.
FIX: Strip console.log from production.

### L5. Persistent Installation Fingerprint
.betaId: e3126fe30f9c4b6f91040b3e80497a8b sent during every update check.
Sent to installer.rootapp.com. Enables long-term installation tracking.
FIX: Disclose in privacy policy. Allow opt-out.

### L6. External Resources Without SRI
Google Fonts in Hexatris. mc-heads.net in Minecraft. raw.githubusercontent.com.
effectssdk.ai WASM models (elevated to H11).
FIX: Add SRI hashes.

### L7. Dev Bridge HTTP API Code Shipped
DevBridge class with ports 8080/8081/8082 in all 7 sub-apps.
Ports verified closed in production. Code is dead but ships.
FIX: Strip from production builds.

### L8. JWT Validation Only Checks Header Format
File: Raid Planner index-BG-RjBRK.js:36908-36922
`isValidJWT` function only validates JWT header structure (typ, alg fields).
No claims checked (exp, iss, aud, nbf, iat). No signature verification.
No token expiry enforcement client-side. No refresh token flow.
Context: Zod schema validation, not auth flow. Low direct impact.
FIX: Implement proper JWT validation if used for auth decisions.

### L9. Lexical Dragon NaturallySpeaking Handler
File: Raid Planner, Minecraft, Task Tracker bundles (line 80649+)
`addEventListener("message", handler)` for Dragon voice-to-text integration.
Has origin check (`event.origin !== window.location.origin`) - properly implemented.
No `event.source` check. Same-origin frames could inject text into editor.
FIX: Add event.source validation for defense-in-depth.

### L10. Cache Poisoning Persistence for WASM Models
File: WebRtcBundle effectssdk.ai Loader class
Cache API (`caches.open('atsvb')`) stores models with no expiration or integrity check.
Cache key is URL only, no version hash. Poisoned model persists indefinitely.
`Loader.delete()` exists but is not called automatically.
FIX: Implement cache version keys and expiration. Verify integrity on load.

### L11. Math.random() Fallback in UUID Generation
File: WebRtcBundle\rootapp-desktop-webrtc.js (Sentry SDK)
Sentry's UUID generation for event_id falls back to `Math.random()` when `crypto.getRandomValues`
is unavailable. While DotNetBrowser's Chromium should always provide the crypto API, the fallback
to a non-cryptographic PRNG means event IDs are predictable if crypto is somehow unavailable.
FIX: Remove Math.random() fallback. Require crypto.getRandomValues.

### L12. No SVG/HTML Sanitization Library in Stickerwall
File: RootApps\002a3d84-...\0.4.8\...\index-Dg0hJEdq.js
Zero instances of DOMPurify, sanitize-html, or any HTML/SVG sanitization library in the
entire Stickerwall app bundle. Combined with Fabric.js SVG rendering, Alpine.js x-html directive,
and innerHTML usage in DevBar, there is no defense-in-depth against injection attacks.
FIX: Add DOMPurify. Sanitize all user-supplied HTML/SVG content.

### L13. Effects SDK Developer Build Path Disclosed
File: WebRtcBundle\rootapp-desktop-webrtc.js:5476
`_scriptName = "file:///Users/tmv/work/vbsdk/audio/src/DenoiseFilter/wasm_bindings.js"`
Reveals developer username (`tmv`), platform (macOS), internal project name (`vbsdk`),
and full source directory structure from the Emscripten-compiled WASM module.
FIX: Strip source paths from WASM compilation output.

### L14. Sentry DEBUG_BUILD=true in Production (Raid Planner)
File: RootApps\002a3d83-c35a-...\0.4.11\...\index-BG-RjBRK.js:18106
`DEBUG_BUILD = typeof __SENTRY_DEBUG__ === "undefined" || __SENTRY_DEBUG__`
`__SENTRY_DEBUG__` is NOT replaced at build time by Vite, so DEBUG_BUILD evaluates to `true`.
All Sentry debug logging active in production. Also: Raid Planner uses Sentry SDK v10.36.0
(NOT v1.33.7 as in other sub-apps), meaning two different Sentry SDK versions are in use.
FIX: Define `__SENTRY_DEBUG__` as `false` in Vite build config.

### L15. Staging Icon Shipped in Production Build
File: C:\Users\bash\AppData\Local\Root\current\rooticonstaging.ico (and Resources\Assets\)
A staging-specific icon `rooticonstaging.ico` ships alongside the production `rooticon.ico`.
Confirms existence of a staging environment. Combined with the Sentry `environment` parameter
accepting "production", "staging", "experimental", "debug", reveals at least 4 environments.
FIX: Exclude staging assets from production builds.

### L16. Hexatris Predictable PRNG + Shared Seed Exposure
File: RootApps\002c8f0a-...\0.4.2\...\index-VcQt9H-b.js
Practice mode uses a Linear Congruential Generator (LCG) with known constants:
`seed = seed * 1103515245 + 12345 & 2147483647` (standard glibc LCG).
Given any seed, the entire piece sequence is deterministic and trivially predictable.
In multiplayer, `MatchStartedEvent` exposes `shared_seed` (field 4) to both players.
If the server uses the same LCG for piece generation, piece sequences are predictable.
FIX: Use cryptographic PRNG for multiplayer piece generation. Don't expose seed to clients.

### H16. WebRTC Bundle Contains Full gRPC Client for ALL 27 Backend Services
File: WebRtcBundle\rootapp-desktop-webrtc.js (lines 590-594)
The WebRTC bundle embeds a complete gRPC-web client library that can call ALL 27 backend services
(163 total methods), not just WebRTC-related endpoints. Critical services accessible:
- `root.v2.MessageGrpcService` — Create, Delete, Edit, Get, List, Search (SEND MESSAGES)
- `root.DirectMessageGrpcService` — Create, AddMembers, Find, RemoveUser (START DMs)
- `root.UserGrpcService` — SignIn, SignUp, RefreshToken, Delete, SetPassword (ACCOUNT OPS)
- `root.CommunityGrpcService` — Create, Edit, Delete, OwnerEdit (COMMUNITY DESTRUCTION)
- `root.CommunityMemberBanGrpcService` — Create, Kick, KickBulk (BAN/KICK USERS)
- `root.ChannelGrpcService` — Create, Edit, Delete (CHANNEL MANAGEMENT)
- `root.FileGrpcService` — Create, Edit, Delete, Download (FILE ACCESS)
- Plus 20 more services covering roles, notifications, access rules, etc.
All calls authenticated via Bearer token from `window.__nativeToWebRtc.initialize({token})`.
Token is in JS memory — any XSS in WebRTC context = FULL API ACCESS to entire platform.
MessageCreateRequest notably has NO user_id field — identity derived solely from Bearer token.
FIX: Minimize service exposure. WebRTC bundle should only access WebRTC-related services.

### M26. Token Refresh Enables Indefinite Session Persistence + Session DoS
File: WebRtcBundle\rootapp-desktop-webrtc.js (root.UserGrpcService)
The `root.UserGrpcService.RefreshToken` method allows refreshing an access token without
re-authentication. Combined with M19 (no session invalidation) and M22 (auth persistence),
a stolen token can be refreshed indefinitely to maintain access even if the user changes
their password.
ACTIVE TEST CONFIRMED: Calling RefreshToken returns a new valid token AND immediately
invalidates the previous token (subsequent calls with old token return HTTP 401).
This creates an account takeover + session DoS vector: attacker steals token → calls
RefreshToken → gets new token → victim's app session is broken (all API calls fail with 401).
Victim must restart app to re-authenticate. Attacker now has exclusive access.
Token rotation confirmed: old signature revoked, new signature issued, same userId/deviceId.
FIX: Implement refresh token rotation. Invalidate all tokens on password change.
Add device binding and concurrent session detection.

### H17. Bearer Token Extractable from Process Memory (No Obfuscation)
File: Root.exe process memory (PID varies)
The active Bearer token resides in Root.exe process memory in plaintext (base64url format).
A simple memory scan for 128-byte base64url strings matching the user's UUID prefix
successfully extracts the token from ~2GB of process memory in seconds.
ACTIVE TEST: Memory scan of PID 32964 found the token at address 0x20bc04ad0d6:
- UserId: 002cf06b-05f1-8f01-bfe6-d4c70dc47966
- DeviceId: 002cf43a-7f3b-8f12-a3e2-69cc2844857f
Token validated against production API (GetSelf returned username + email).
No token obfuscation, encryption, or integrity protection in memory.
Any malware/tool running as the same user can extract tokens.
FIX: Encrypt tokens in memory. Use process isolation. Consider hardware-backed key storage.

### H18. Store\AuthToken File Enables Offline Token Extraction
File: %LOCALAPPDATA%\Root Communications\Root\profile\default\Store\AuthToken (390 bytes)
The auth token is persisted to disk in a binary file. The file header matches the DPAPI
provider GUID ({df9d8cd0-0115-11d1-8c7a-00c04fc297eb}), master key GUID {467e46a3-...},
but CryptUnprotectData fails with "data is invalid" both with and without entropy.
This suggests DPAPI with unknown per-app entropy, or a modified blob format.
The file is readable by any process running as the current user (no ACL restrictions).
Combined with H17 (memory extraction), this provides two paths to token theft.
FIX: Use Windows Credential Manager or DPAPI with per-app entropy. Set restrictive ACLs.

### H19. WebRTC postMessage Auth Key Exchange — Interceptable
File: WebRtcBundle\rootapp-desktop-webrtc.js:11109-11137
The WebRTC bundle exchanges auth keys with the main thread via postMessage using
`requestAuthFromMainThread(payload, authType)`. The function is exposed on `globalThis`,
meaning any code in the WebRTC worker context can intercept auth key responses.
Auth responses arrive via `event.data.key` with no origin validation.
Combined with no CSP (M3) and modifiable local JS (M6), injected code can intercept
all auth key exchanges and exfiltrate them.
FIX: Remove globalThis exposure. Validate postMessage origins. Use secure IPC channel.

### C6. DM Creation with ANY User — No Mutual Consent Required
File: api.rootapp.com → root.DirectMessageGrpcService/Create
ACTIVE TEST CONFIRMED: Successfully created DMs with arbitrary users via gRPC-web API.
- Created DM with alt account (electromutt, UID 002cf099-...) — gRPC 0 OK
- Created DM with Root community OWNER (UID 00284fa4-6789-8c01-...) — gRPC 0 OK
- No friend request, mutual follow, or shared community required
- Only needs the target user's UUID (obtainable from notifications, DM list, community data)
The DirectMessageCreateRequest requires field 10 = repeated UserUuid (member_user_ids).
Both the caller's UUID and target UUID must be included. Server validates caller identity
from Bearer token but does NOT validate target user consent.
DM container ID returned in response (field 5 = DirectMessageUuid), usable as MessageContainerUuid.
DM count increased from 4 to 6 after exploit (confirmed via DM List).
Enables: unsolicited messaging to any platform user, harassment vector, social engineering.
FIX: Require mutual friend/follow/community membership. Add DM request + accept flow.

### H20. Community Creation — Open to All Users
File: api.rootapp.com → root.CommunityGrpcService/Create
ACTIVE TEST: Successfully created "PentestCommunity" via gRPC-web API.
CommunityCreateRequest: field 10 = name, field 11 = picture_hex, field 12 = template_type.
Response returns: community_id, owner_user_id (our user), default_channel_id, name, picture_hex.
Template "gaming" worked. No rate limiting observed.
While community creation is normal functionality, combined with no rate limiting this enables
spam community creation. Also confirms full write access to production API via extracted token.
FIX: Rate limit community creation. Consider requiring email verification or account age.

### H21. Community Leave Is Irreversible — Join Not Implemented
File: api.rootapp.com → root.CommunityGrpcService/Leave + Join
ACTIVE TEST: CommunityGrpcService/Leave returned gRPC 0 (success) — user left Root community.
CommunityGrpcService/Join returns gRPC 12 (UNIMPLEMENTED) — cannot rejoin.
An attacker who tricks a user into calling Leave (via XSS + bridge, or token theft) can
permanently remove them from communities with no self-service recovery path.
CommunityGrpcService/Get returns UNAUTH after leaving — access correctly revoked.
CommunityGrpcService/Delete returns UNAUTH for non-owners — authorization works correctly.
FIX: Implement CommunityGrpcService/Join. Add invite-based rejoin mechanism.

### M27. gRPC-web Status Returned in HTTP Headers — Bypasses Body Parsing
File: api.rootapp.com (all gRPC-web endpoints)
ACTIVE TEST: The server returns gRPC status codes via HTTP response headers (grpc-status,
grpc-message) rather than in-body gRPC-web trailers. Endpoints with non-zero status return
HTTP 200 with 0-byte body and status in headers only. Client parsers that only check body
trailers will miss error responses entirely.
This caused initial enumeration to incorrectly classify all endpoints as "implemented"
since HTTP 200 was received for all calls. Correct parsing revealed only 32 of 97+ tested
endpoints are actually implemented.
FIX: Return status in both HTTP headers AND body trailers for gRPC-web compatibility.

### M28. Full gRPC Service Enumeration — 32 Implemented Endpoints
File: api.rootapp.com (production gRPC-web endpoint)
ACTIVE TEST: Complete enumeration with correct HTTP header parsing revealed 32 implemented
endpoints across 8 services (the rest return gRPC 12 UNIMPLEMENTED):

WORKING (gRPC 0 with data):
- UserGrpcService: GetSelf
- DirectMessageGrpcService: List, Create, Find
- NotificationGrpcService: List
- CommunityGrpcService: Get, ListMine, Create, Leave, Delete(auth-checked)

IMPLEMENTED but need correct params (gRPC 3 BAD_ARG):
- UserGrpcService: Delete
- DirectMessageGrpcService: AddMembers, RemoveUser
- CommunityGrpcService: GetExtended, GetForApp, Edit, OwnerEdit
- ChannelGrpcService: List, Get, Create, Delete, Move, Edit
- ChannelGroupGrpcService: List, Get, Create
- CommunityMemberGrpcService: List, Get
- CommunityRoleGrpcService: List
- DirectoryGrpcService: List, Get
- AssetGrpcService: Get

COMPLETELY UNIMPLEMENTED (gRPC 12):
- MessageGrpcService (ALL methods — messages use different mechanism)
- FriendGrpcService, RoleGrpcService, MemberGrpcService (ALL)
- MediaGrpcService, WebRtcGrpcService, AuthGrpcService (ALL)
- PresenceGrpcService, EmojiGrpcService, StickerGrpcService (ALL)
- ReportGrpcService, InviteGrpcService, TokenGrpcService (ALL)

### L17. Protobuf Schema Fully Extractable from JS Bundle — 637 Message Types
File: WebRtcBundle\rootapp-desktop-webrtc.js
ACTIVE TEST: Successfully extracted complete protobuf message schemas from the minified
WebRTC JS bundle, including all 637 message type definitions, field numbers, types,
and nesting structure. This enabled correct API interaction by revealing:
- UUID encoding: two fixed64 fields (high64, low64) in big-endian byte order
- Data fields start at field 10 (field 1 is always RootContext)
- google.protobuf.StringValue wrapper for optional strings
- Timestamp encoding as Unix seconds + nanos in a message
Combined with L1 (source maps), provides complete API documentation for attackers.
FIX: Consider protobuf obfuscation. Strip unused service definitions from client bundles.

### B11. Community Leave Without Matching Join Creates Orphaned State
ACTIVE TEST: Calling CommunityGrpcService/Leave on a community where Join is unimplemented
results in permanent removal with no recovery path. The client JS expects both operations
to be available but the server only implements Leave. This creates a one-way door.
FIX: Implement Join before exposing Leave, or prevent Leave when Join is unavailable.

### B12. DM Create Allows Duplicate Conversations
ACTIVE TEST: Creating a DM with the same pair of users that already have an existing DM
succeeded and created a SECOND DM conversation (DM count went from 4 to 5, then 6).
The Find endpoint correctly returns the existing DM, but Create doesn't check for duplicates.
DM 0 and DM 4 in the list both have the same participants (main + alt).
FIX: DM Create should return existing DM if one already exists with same member set.

### B13. Empty DM Created Without Content
ACTIVE TEST: The newly created DMs (with alt and with community owner) appear in DM List
with no last_message field (field 14 absent), meaning they show up as empty conversations.
The client app may display these as blank DM entries.
FIX: Require initial message on DM creation, or hide empty DMs from list.

---

## FUNCTIONAL BUGS

### B1. Double-? URL Bug in uriToImageUrl (Critical - Feature Broken)
File: All 7 sub-app index-*.js (line ~35522)
`"asset?query=" + encodeURI(uri) + "?resolution=" + encodeURI(resolution)`
Second `?` means resolution parameter is NEVER received as a separate query parameter.
Image resolution feature is silently broken across entire application.
FIX: Change `?resolution=` to `&resolution=`.

### B2. forEach with async Callback (Fire-and-Forget Promises)
File: Raid Planner index-BG-RjBRK.js:64987
`array.forEach(async (item) => { await asyncOperation(item); })`
forEach doesn't await the async callback. Promises are created but never awaited.
Can cause race conditions, unhandled rejections, data inconsistency.
FIX: Use `for...of` with await, or `Promise.all(array.map(async ...))`.

### B3. process.exit() in Browser Code
File: Raid Planner index-BG-RjBRK.js:34974
`process.exit()` called in browser context where `process` is undefined.
Throws ReferenceError at runtime. Dead code from Node.js dependency.
FIX: Remove or guard with environment check.

### B4. Raid Max Participants Logic Bug
File: Raid Planner index-BG-RjBRK.js:85374
Participant count includes users with removed/left status.
Users who left still count toward max capacity, preventing new signups.
FIX: Filter participant count to active users only.

### B5. Event Listener Leaks (Multiple Locations)
File: Multiple sub-app bundles
addEventListener calls without corresponding removeEventListener in cleanup.
Each component mount adds new listeners without removing old ones.
Memory leak grows with each navigation/remount.
FIX: Return cleanup functions from useEffect. Remove listeners on unmount.

### B6. No onerror Handler on WebSocket
File: All 7 sub-app index-*.js (dev bridge WebSocket)
WebSocket created without `onerror` handler.
Connection failures produce silent errors.
FIX: Add onerror handler with reconnection logic.

### B7. Stale Old Versions on Disk (4 Apps)
File: RootApps\ (multiple version directories)
4 apps have older versions on disk (0.0.40, 0.1.54, 0.1.33, 0.4.0) alongside current versions.
registry.json entries are all valid (7/7 paths verified), but old versions are not cleaned up.
Old versions contain older SDK code with potentially different vulnerability profiles.
FIX: Clean up stale versions on app update.

### B8. Task Tracker MemberGroups Client-Writable
File: RootApps\002a3d83-e2e0-...\0.4.5\...\index-*.js
Task Tracker allows clients to set `MemberGroups` in task update requests. The client can assign
arbitrary users to member groups, potentially granting them access to tasks or projects they
shouldn't see. No server-side validation that the requesting user has permission to modify
group membership.
FIX: Server-side authorization for MemberGroups modification.

### B9. Hexatris RequestPieces Count Unbounded
File: RootApps\002c8f0a-...\0.4.2\...\index-VcQt9H-b.js
`RequestPiecesRequest` accepts a `count` parameter with no bounds checking. Client normally
requests 10-20 pieces. An attacker can request `count: 10000` to obtain the entire future piece
sequence, gaining massive lookahead advantage over opponent.
FIX: Server-side cap on piece request count.

### B10. Unknown Message Types Crash Client Receive Handler
File: All 7 sub-app index-*.js (line ~35026)
The `receive()` method's switch statement only handles `APP_MESSAGE` (1) and `APP_MESSAGE_EXCEPTION` (5).
All other message types (PING=4, HUB_DISCONNECTING=6, COMMUNITY_ADD=7, COMMUNITY_DELETE=8) hit
the default case which throws: `throw new u$7(h$8.RootInternalException, "received an unknown message type: " + t$11)`.
The WebSocket `onmessage` handler has no try-catch, so this unhandled exception crashes message
processing entirely. A server-side bug or injected message of any non-handled type causes permanent
client-side DoS (no more messages processed until page reload).
FIX: Handle all defined message types gracefully. Add try-catch around receive handler.

---

## PHASE 1-7 LIVE API TEST FINDINGS (2026-02-12)

The following findings were discovered via automated gRPC-web API testing across 7 phases,
using scripts `phase1_access_rules.py` through `phase7_remaining.py` with `grpc_lib.py`.
All tests performed as user `watchthelight` (UID 002cf06b-05f1-8f01-bfe6-d4c70dc47966)
against production API at `api.rootapp.com`.

---

### C7. Cross-Community Channel Edit — Unauthorized Channel Rename (PoC Confirmed)
**Severity**: CRITICAL
**Service**: `root.ChannelGrpcService/Edit`
**Phase**: 1 (Test 1.9c)

ACTIVE TEST CONFIRMED: A non-admin user successfully renamed the "Chat" channel in the Root
community (community_id `00285411-3480-8302-a0e4-4da546be7be9`) to "pwned-by-pentest" via
`ChannelGrpcService/Edit`. The user is not an admin or moderator in the target community.

Request: `f10=target_community_id + f11=channel_id + f12="pwned-by-pentest"`
Response: gRPC 0 OK (275 bytes) — server returned full channel object with updated name.
Subsequent `ChannelGrpcService/Get` confirmed the rename persisted (f13="pwned-by-pentest").

Channel ID: `0029d12e-5afc-8e04-b91e-f9191c9ebe0b`
Original name: "Chat"
New name: "pwned-by-pentest"

Note: `ChannelGrpcService/Delete` on the same channel returned UNAUTH — so delete is properly
protected but edit is not. This is an inconsistent RBAC enforcement.

FIX: Enforce RBAC on ChannelGrpcService/Edit. Only community admins/moderators should be
able to rename channels. Apply same authorization as Delete.

---

### H22. Cross-Community Channel Group Enumeration
**Severity**: HIGH
**Service**: `root.ChannelGroupGrpcService/List` + `root.ChannelGroupGrpcService/Get`
**Phase**: 1 (Tests 1.8a, 1.8b)

ACTIVE TEST: Successfully enumerated all 6 channel groups in the Root community from an
unprivileged account:

| UUID | Name |
|------|------|
| 002b1b0e-72a6-8b05-81b0-893d057c7845 | General |
| 002b1b15-c611-8f05-9d6e-8d8f76e69581 | Support |
| 002b1b19-8b2b-8a05-88a2-b9e96b972bb7 | Self Promotion |
| 002b1b1b-607e-8305-a381-0407afb26b22 | Welcome |
| 002cc19c-4698-8005-b783-ed370bebd622 | Community Program |
| 002ccbe2-12fe-8a05-8e91-e0bdfac0dff5 | Apps Demo |

Response includes full group details: community_id (f10), group_id (f11), name (f12),
sort_order (f13), permissions (f14), and role UUIDs (f16, repeated).

`ChannelGroupGrpcService/Get` also returns full details for individual groups (332 bytes).

FIX: Restrict ChannelGroupGrpcService/List and Get to community members only.

---

### H23. Cross-Community Channel Enumeration — Full Channel Details Exposed
**Severity**: HIGH
**Service**: `root.ChannelGrpcService/List` + `root.ChannelGrpcService/Get`
**Phase**: 1 (Tests 1.9b, 1.9e)

ACTIVE TEST: Successfully enumerated all 10 channels in the Root community's "General"
group (3,311 bytes response):

| UUID | Name | Description |
|------|------|-------------|
| 0029d12e-5afc-8e04-... | Chat | Casual chat - use #Support for Root technical questions |
| 002b1b10-67a7-8904-... | Off-Topic | For all things not Root |
| 002b1b10-bff2-8404-... | Gaming | Gaming and game industry chat |
| 002b1b11-3270-8504-... | Memes | Memes and only memes! |
| 002b1b11-960a-8b04-... | Something-I-Made | Share your creations |
| 002b1b12-0b0c-8404-... | Tech | Hardware, software, and all things tech |
| 002b1b12-6105-8e04-... | Music | Share your favorite music & music chat |
| 002b1b12-bb68-8404-... | Photography | Share your best pics |
| 002bb17c-1aed-8504-... | Pets | Pics, funny stories and cuteness only! |
| 002bcf33-dcee-8c04-... | Sports | For all things sporty! |

Each channel entry includes: community_id (f10), group_id (f11), channel_id (f12),
name (f13), description (f14), icon (f15), channel_type (f16), sort_order (f17),
permissions (f19), last_message timestamp (f20), and role UUIDs (f24, repeated).

`ChannelGrpcService/Get` for individual channels returns 439 bytes with full details.

FIX: Restrict ChannelGrpcService/List and Get to community members only.

---

### H24. SSRF via SetProfilePicture — Internal DNS Resolution
**Severity**: HIGH
**Service**: `root.UserGrpcService/SetProfilePicture`
**Phase**: 5 (Test 5.13)

ACTIVE TEST: `SetProfilePicture` with an internal DNS URL returned gRPC 0 OK (118 bytes),
indicating the server attempted to fetch or process the URL. Direct IP addresses
(http://169.254.169.254, http://127.0.0.1) returned UNKNOWN errors, suggesting IP-based
SSRF blocking is in place but DNS-based bypass works.

This allows an attacker to:
- Probe internal network via DNS resolution timing
- Potentially access internal services that resolve via DNS but not via direct IP
- Exfiltrate data via DNS rebinding attacks

FIX: Validate URLs against an allowlist of schemes and hosts. Block all non-HTTPS URLs.
Resolve DNS and validate against private IP ranges before fetching.

---

### M29. Cross-Community Full Member List Exposure (16,971 Users)
**Severity**: MEDIUM
**Service**: `root.CommunityMemberGrpcService/ListAll`
**Phase**: 4 (Test 4.10a)

ACTIVE TEST: `CommunityMemberGrpcService/ListAll` with target community_id returned the
complete member list of 16,971 users including their UUIDs. Response was approximately 8MB.
No pagination required — entire list returned in a single response.

This exposes:
- Complete user UUID directory for the entire community
- Enables targeted attacks against specific users (DM creation, friend requests)
- Combined with GetExtendedUsersById (M32), can enumerate all user profiles

FIX: Restrict ListAll to community admins. Regular members should only see limited member
lists with pagination.

---

### M30. Invite Code Enumeration — 'root' Reveals Community Info
**Severity**: MEDIUM
**Service**: `root.LinkGrpcService/CommunityInviteLinkGetInfo`
**Phase**: 4 (Test 4.3)

ACTIVE TEST: Calling `CommunityInviteLinkGetInfo` with invite code "root" returned gRPC 0 OK
with 104 bytes of community information. Other tested codes ("test", "admin", "discord",
"invite") returned NOT_FOUND.

The code "root" is trivially guessable and reveals:
- Community UUID
- Community metadata

FIX: Rate limit invite code lookups. Use random, non-guessable invite codes.

---

### M31. Security Notification Suppression via DeleteAll
**Severity**: MEDIUM
**Service**: `root.NotificationGrpcService/DeleteAll`
**Phase**: 5 (Test 5.2c)

ACTIVE TEST: `NotificationGrpcService/DeleteAll` returned gRPC 0 OK, successfully deleting
all 14 notifications. An attacker with a stolen token can:
- Delete security notifications (login alerts, password changes)
- Suppress notifications about unauthorized community actions
- Cover tracks after performing malicious operations

`SetAllViewed` also worked, allowing mass-marking notifications as read.

FIX: Protect security-sensitive notifications from bulk deletion. Implement notification
categories where security alerts cannot be deleted by the user.

---

### M32. User Information Disclosure via Username Lookup
**Severity**: MEDIUM
**Service**: `root.UserGrpcService/GetExtendedUsersByUsername`
**Phase**: 5 (Tests 5.5)

ACTIVE TEST: Successfully looked up user details by username for "admin", "root", and "test":

| Username | UUID | Profile |
|----------|------|---------|
| admin | 002a05a5-61a7-8e01-bd8e-17fbe332d4ac | defaultProfile0.png |
| root | 00284fa4-6789-8c01-9486-6d8d67ff1d79 | root://asset/image/ACocSQeYi... |
| test | 002a292e-1bab-8e01-af03-0e3d98498c00 | defaultProfile0.png |

Any authenticated user can look up any other user's UUID and profile picture by username.

FIX: Rate limit username lookups. Consider requiring mutual community membership.

---

### M33. User Information Disclosure via UUID Lookup
**Severity**: MEDIUM
**Service**: `root.UserGrpcService/GetExtendedUsersById`
**Phase**: 5 (Test 5.6)

ACTIVE TEST: Successfully retrieved extended user info for alt account by UUID:
- Username: "electromutt"
- Profile picture: root://asset/ACzwmw1Xhxuo2cqXoBYVq...
- Online status flag (f13=1)

Any authenticated user can enumerate user details by UUID.

FIX: Restrict extended user info to users who share a community or friendship.

---

### M34. Mass Account Creation from Authenticated Session
**Severity**: MEDIUM
**Service**: `root.UserGrpcService/SignUp`
**Phase**: 5 (Test 5.9)

ACTIVE TEST: `UserGrpcService/SignUp` returned gRPC 0 OK when called from an already-
authenticated session. This enables:
- Mass bot account creation
- Platform abuse via account farming
- Bypassing any registration rate limits tied to IP (since requests go through the
  authenticated gRPC channel)

FIX: Rate limit SignUp per IP and per authenticated session. Require CAPTCHA or email
verification for account creation.

---

### M35. TURN Server Credentials Disclosed via GetIceInfo
**Severity**: MEDIUM
**Service**: `root.WebRtcGrpcService/GetIceInfo`
**Phase**: 6 (Test 6.1)

ACTIVE TEST: `GetIceInfo` returned TURN server credentials without requiring the user to be
in an active voice session:

TURN servers (Cloudflare):
- `turn:turn.cloudflare.com:3478?transport=udp`
- `turn:turn.cloudflare.com:3478?transport=tcp`
- `turns:turn.cloudflare.com:5349?transport=tcp`
- `turn:turn.cloudflare.com:53?transport=udp`
- `turn:turn.cloudflare.com:80?transport=tcp`
- `turns:turn.cloudflare.com:443?transport=tcp`

Username: `g0fd587634e3eb2e7cf3471a325e233312dbfe832bb8bccaac305146af074ca6`
Credential: `0eedc00a28febd8171d7597efbcbb2dd6277a57a2955f13dc3c877fae0d25e3f`

While these are likely short-lived credentials, they can be used to:
- Relay traffic through Root's TURN infrastructure
- Potentially enumerate active sessions
- Abuse TURN bandwidth

FIX: Only return TURN credentials when user is actively joining a voice channel.

---

### M36. No Server-Side Rate Limiting on UserGrpcService/GetSelf
**Severity**: MEDIUM
**Service**: `root.UserGrpcService/GetSelf`
**Phase**: 7 (Test 7.8)

ACTIVE TEST: 20 concurrent calls completed in 0.2s, all returning gRPC 0 OK.
Zero rate limiting observed. Enables:
- Token validity brute-force testing
- API abuse / DoS potential

FIX: Implement server-side rate limiting per token/IP.

---

### M37. No Server-Side Rate Limiting on NotificationGrpcService/List
**Severity**: MEDIUM
**Service**: `root.NotificationGrpcService/List`
**Phase**: 7 (Test 7.8)

ACTIVE TEST: 20 concurrent calls completed in 0.1s, all returning gRPC 0 OK.
Zero rate limiting observed. Enables:
- Notification polling abuse
- API resource exhaustion

FIX: Implement server-side rate limiting per token/IP.

---

### L18. Social Graph Mapping via GetConnectionBetween
**Severity**: LOW
**Service**: `root.UserGrpcService/GetConnectionBetween`
**Phase**: 5 (Test 5.7)

ACTIVE TEST: `GetConnectionBetween` returned gRPC 0 OK (20 bytes) with a connection UUID
(f11) between two users. Enables mapping the social graph by querying pairs of user UUIDs.

FIX: Restrict to querying connections involving the authenticated user only.

---

### L19. HubServer WebSocket Endpoint Disclosed
**Severity**: LOW
**Service**: `root.UserGrpcService/GetNewHubserverEndpoint`
**Phase**: 5 (Test 5.xd)

ACTIVE TEST: Returns `wss://hub-1.rootapp.com/notifications` (39 bytes).
Reveals internal infrastructure naming. Combined with the notification WebSocket protocol,
this could enable notification injection or eavesdropping if the WebSocket auth is weak.

FIX: Consider whether this endpoint needs to be a separate gRPC call vs. hardcoded in client.

---

### L20. Token Refresh Race Condition — Proper Locking Confirmed
**Severity**: LOW (Informational)
**Service**: `root.UserGrpcService/RefreshToken`
**Phase**: 7 (Test 7.10)

ACTIVE TEST: 5 concurrent RefreshToken calls resulted in:
- 1x gRPC 0 OK (new token issued)
- 2x CODE_N/A (rejected)
- 2x UNKNOWN error

Server properly handles concurrent refresh — only one succeeds. This confirms the token
rotation behavior documented in M26 and demonstrates proper server-side locking.

However, the token rotation itself remains a session DoS vector (M26): any call to
RefreshToken invalidates the current token, so a stolen token can be used to permanently
lock out the legitimate user.

---

### I1. AccessRuleGrpcService Returns INTERNAL Errors
**Severity**: INFORMATIONAL
**Service**: `root.AccessRuleGrpcService/*`
**Phase**: 1 (Tests 1.1, 1.3-1.6)

All AccessRuleGrpcService methods (ListByChannelOrChannelGroup, Create, Edit, Delete, Update)
returned gRPC INTERNAL errors. This may indicate:
- Service is deployed but has internal bugs
- Protobuf schema mismatch in test payloads (ChannelOrChannelGroupUuid wrapper format)
- Service is partially implemented

Note: ListByRoleOrMember returned OK, suggesting some methods work while others have issues.

---

### I2. ChannelGrpcService/Create Returns INTERNAL on Target Community
**Severity**: INFORMATIONAL
**Service**: `root.ChannelGrpcService/Create`
**Phase**: 1 (Test 1.9a)

`ChannelGrpcService/Create` returned INTERNAL error when attempting to create a channel
in the target community. This contrasts with Edit (which succeeded as C7) and Delete
(which returned UNAUTH). The INTERNAL error may mask an authorization check.

---

### I3. Friendship Services Return CODE_N/A
**Severity**: INFORMATIONAL
**Service**: `root.FriendshipInviteGrpcService/*`, `root.FriendshipGrpcService/*`, `root.FriendshipGroupGrpcService/*`
**Phase**: 6 (Tests 6.5-6.8)

All 6 friendship service methods returned CODE_N/A (no gRPC status in response).
This may indicate the services use a different transport (WebSocket hub) rather than
gRPC-web, or the protobuf schemas are incorrect.

---

### I4. File/Directory Services Require Container ID Format
**Severity**: INFORMATIONAL
**Service**: `root.FileGrpcService/*`, `root.DirectoryGrpcService/*`
**Phase**: 2

All file and directory service methods returned BAD_ARG, suggesting they require a
specific container_id format (possibly a channel UUID in a wrapper message) rather than
a bare community UUID. Further testing with correct protobuf schemas needed.

---

### I5. CommunityAppGrpcService Consistently Returns UNAUTH
**Severity**: INFORMATIONAL
**Service**: `root.CommunityAppGrpcService/*`
**Phase**: 3

All CommunityAppGrpcService methods returned UNAUTH, while AppStoreGrpcService/List
worked and enumerated 9 apps (Raid Planner, Task Tracker, Sticker Wall, Community Analytics,
Polling, Command Block, Suggestions, Hexatris, Moderation).

The UNAUTH may indicate community app operations require a different auth scope or
app-specific authorization.
