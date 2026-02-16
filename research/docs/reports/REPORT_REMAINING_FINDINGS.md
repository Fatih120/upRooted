# Root v0.9.86 - Everything Else

This is the stuff I left out of Adrian's report because it's client-side, local, or lower priority. None of it required live API exploitation. All of it is verifiable by looking at the shipped files on disk. Organized by theme, not severity, because severity depends on your threat model.

---

## Sentry Is Leaking Everything

You have two Sentry DSNs in production. Both accept events from any origin.

The WebRTC bundle has `sendDefaultPii: true` in the Sentry config. That automatically sends IP addresses, cookies, user-agent strings, and request headers to Sentry's US servers. There's no `beforeSend` hook to scrub anything. The Authorization: Bearer header gets captured in breadcrumbs and sent along with everything else.

On top of that, `enableLogs` is true for all non-debug environments. Every failed WebRTC Redux action gets sent to Sentry via `Sentry.logger.warn()` with full error payloads, ICE credentials, user IDs, channel IDs, and RTCPeerConnection stats including local and remote IP addresses. Session replay is set to 25% on errors - that's video-like DOM recording sent to Sentry with PII included.

The Raid Planner sub-app has `DEBUG_BUILD = true` in production because `__SENTRY_DEBUG__` isn't defined in the Vite build config. So all Sentry debug logging is active. Also the Raid Planner uses Sentry SDK v10.36.0 while the other sub-apps use v1.33.7. Two different SDK versions across the same app.

DSN 1 (sub-apps): `c1dfb07d783ad5325c245c1fd3725390@o447951.ingest.sentry.io/4509632503087104`
DSN 2 (WebRTC): `75fd2cd278ac35657edd7ee8df86eb37@o4509469920133120.ingest.us.sentry.io/4509832005681152`

Both confirmed exploitable - error events, PII events, replay events all return HTTP 200 from any origin.

---

## The Bridge Is a Free-for-All

The native bridge (`window.__rootSdkBridgeWebToNative`) is exposed to all code running in the DotNetBrowser context. No origin checks, no caller validation. If you get XSS anywhere, you get full bridge access.

What that buys you:
- `.send(buffer)` - raw protobuf injection, bypasses the client-side rate limiter
- `__webRtcToNative.kickPeer(userId)` - kick any user from a voice call
- `__webRtcToNative.setAdminMute(deviceId, true)` - admin mute anyone
- `__webRtcToNative.setAdminDeafen(deviceId, true)` - admin deafen anyone
- `__nativeToWebRtc.receiveRawPacket(buffer)` - inject signaling packets
- `__nativeToWebRtc.updateMyPermission(perms)` - client-side permission escalation
- `__rootSdkBridgeNativeToWeb.receive(protobuf, type)` - inject fake server responses
- `__rootSdkBridgeNativeToWeb.setTheme(obj)` - CSS injection via property values

The client-side rate limiter (`Ce$1`) caps at 10 req/s and 524KB/s. Calling `.send()` directly bypasses it entirely.

There's also a `restart(url)` method on the bridge with zero URL validation. Any call to `restart("javascript:alert(document.cookie)")` navigates the app to an attacker-controlled URL. Works with `javascript:`, `data:`, `file:`, whatever.

---

## XSS Vectors in the Client

### innerHTML in DevBar
The DevBar ships in production in all 5 sub-apps that have it. It injects user nicknames and IDs directly into innerHTML without sanitization. 19 innerHTML sinks in the Raid Planner alone, 5 confirmed exploitable. The DevBar activates when the user-agent doesn't contain "rootsdk" - which is every normal browser.

### encodeURI() Instead of encodeURIComponent() (5 Instances)
All 7 sub-apps use `encodeURI()` where they need `encodeURIComponent()` in 5 places. The worst one is `uriToImageUrl` which produces a double-question-mark URL: `asset?query=...?resolution=...`. That second `?` means the resolution parameter never actually reaches the server as a separate parameter. Image resolution has been silently broken across the entire app since at least v0.0.40. It's in all 11 bundles I checked.

### Fabric.js loadFromJSON in Stickerwall
Stickerwall uses `loadFromJSON()` to deserialize canvas data with zero sanitization. `FabricImage.fromObject()` fetches whatever URL is in the `src` field. A malicious sticker with `{"type":"Image","src":"https://attacker.com/beacon"}` causes every user who opens it to hit the attacker URL, leaking their IP. Stickers are broadcast to all users in real-time. No DOMPurify or any sanitization library exists in the entire Stickerwall bundle.

### Alpine.js Dead Code
Alpine.js ships in Stickerwall via a Preline UI dependency. The app uses React for everything. Alpine is dead code but the `Function()` constructor (eval equivalent) and the `x-html` directive (innerHTML with no sanitization) are both registered and available. If Alpine gets activated programmatically, both become exploitable.

### URI Pass-Through
`uriToUrl()` returns non-root:// URIs unchanged. `javascript:`, `data:`, `file:`, `vbscript:` all pass through. React's `sanitizeURL` blocks `javascript:` on `href` but not on `img src`.

---

## Session and Identity Problems

### No Logout
There is no logout, sign-out, or session invalidation code anywhere in either bundle. No server-side session invalidation from the client. No session rotation. No idle timeout. SessionStorage persists until you close the tab.

### sessionStorage Poisoning
`__rootUser` is read from sessionStorage with zero validation. An XSS attacker can set arbitrary userId, nickname, and communityRoleIds, then reload. The dev bridge WebSocket sends this identity with no authentication token and no challenge-response.

### Auth Artifacts Stay Forever
`data.json` (encrypted, not DPAPI), `Login Data` (SQLite), `Web Data` (SQLite), `Trust Tokens` (SQLite) all persist on disk indefinitely. Combined with no logout, credentials survive until you manually delete them.

### Token Extractable From Process Memory
The bearer token sits in Root.exe process memory in plaintext base64url. A memory scan for 128-byte base64url strings matching the user's UUID prefix pulls it out of ~2GB of process memory in seconds. I tested this on PID 32964 and found it at address 0x20bc04ad0d6.

### AuthToken File on Disk
`Store\AuthToken` is 390 bytes, user-readable, no DPAPI protection. The file header matches the DPAPI provider GUID but `CryptUnprotectData` fails - probably DPAPI with unknown per-app entropy or a modified blob format. Either way, any process running as the current user can read it.

---

## Local System Issues

### DLL Hijacking
All 10 DLLs in `%LOCALAPPDATA%\Root\current\` are user-writable. `uiohook.dll` is the juicy one - it hooks UI input, so a replaced version is a keylogger. .NET doesn't verify Authenticode at P/Invoke load time.

### No Integrity Checks on Local JS
All 7 app bundles, the WebRTC bundle, and `registry.json` are user-writable with no SRI or code signing verification at load time. `registry.json` controls which apps load and from where. Modify it to point at your own HTML and you own the app.

### Update Manifest Not Signed
`releases.win.json` has SHA256 hashes but no cryptographic signature. Old versions (0.9.82-0.9.85) are still downloadable. Packages are publicly enumerable - 413MB nupkg files directly downloadable from the update server.

### No Certificate Pinning
Standard OS cert store for api.rootapp.com and installer.rootapp.com. Both behind Cloudflare.

---

## Sub-App Game Exploits

### Hexatris Is Fully Client-Authoritative
The entire multiplayer game model trusts the client. `ReportGameOverRequest` lets you send any `finalScore`, `linesCleared`, `maxCombo`. `UpdateBoardStateRequest` lets you send a fabricated empty board while your real board is nearly full. `ReportLineClearRequest` lets you fake Tetris clears to flood your opponent with garbage. Server responses contain `{accepted, error}` with no evidence of validation.

Practice mode uses a predictable LCG PRNG: `seed * 1103515245 + 12345 & 2147483647`. In multiplayer, `MatchStartedEvent` exposes the `shared_seed` to both players. If the server uses the same LCG, the entire piece sequence is known before the game starts.

### Raid Planner IDOR
Multiple protobuf messages accept user identity as a client-supplied field:
- `CreateRaidUserRequest.user_id` - sign up OTHER users for raids
- `CreateRaidRequest.leader_id` - impersonate raid leadership
- `CreateCommentRequest.user_id` - post comments as other users
- `CreateRaidActionLogRequest.actor_id` - forge audit log entries

Client-side permission model has exactly two flags: `isAdmin` and `isRaidCreator`. An attacker can override both via React Query cache: `queryClient.setQueryData(["permissions"], { isAdmin: true })`.

### Suggestions App De-anonymization
The Suggestions app has anonymous submissions, but the `created_by` user_id field is included in the response payload even for anonymous suggestions. Any client can de-anonymize any "anonymous" suggestion by matching the UUID against the user directory.

### Task Tracker Group Manipulation
Task Tracker lets clients set `MemberGroups` in task update requests. You can assign arbitrary users to groups, potentially granting them access to tasks or projects they shouldn't see.

---

## Supply Chain

### EffectsSDK WASM Models - No Integrity Verification
Three WASM/ONNX models (11MB, 52MB, 7MB) downloaded at runtime from effectssdk.ai for audio processing. No SRI hashes, no checksums, no code signing. `Access-Control-Allow-Origin: *` on effectssdk.ai. Cache API stores them with no expiration or integrity check. Compromise effectssdk.ai and you get silent audio surveillance on all Root users.

The WASM module also leaks a developer build path: `file:///Users/tmv/work/vbsdk/audio/src/DenoiseFilter/wasm_bindings.js`.

---

## Miscellaneous

- `COMMUNITY_DELETE` message type (value 8) exists in the `AppRpcMessageType` enum and the protobuf is fully client-serializable. Whether the server actually honors it without auth is unknown.
- `postMessage` with wildcard origin sends complete rrweb session replay data to any parent frame.
- WebRTC auth key exchange via `requestAuthFromMainThread` is on `globalThis`, meaning any code in the worker context can intercept it. No origin validation on responses.
- Dev bridge WebSocket uses plaintext `ws://` not `wss://`, has no auth in the handshake, and the update socket connects to the wrong port.
- `Math.random()` fallback in Sentry's UUID generation. Chromium should always have `crypto.getRandomValues` but the fallback exists.
- Staging icon (`rooticonstaging.ico`) ships in production, confirming the existence of staging/experimental/debug environments.
- The Lexical Dragon NaturallySpeaking handler does check origin but not `event.source`. Same-origin frames could inject text into the editor.
- `console.log` in production dumps WebSocket state, user data, payloads, bridge messages, and full state (old/new/payload) via logging middleware.
- Installation fingerprint `.betaId` sent to installer.rootapp.com on every update check.

---

## Bugs (Not Security, Just Broken)

- Image resolution parameter never reaches the server due to the double-? URL bug. Broken since v0.0.40.
- `forEach` with async callback in Raid Planner creates fire-and-forget promises. Race conditions and unhandled rejections.
- `process.exit()` called in browser context where `process` is undefined. Dead code from a Node.js dependency.
- Raid participant count includes users who left. Dead users block new signups.
- addEventListener without removeEventListener in multiple locations. Memory leaks on every navigation.
- No onerror handler on dev bridge WebSocket. Silent failures.
- 4 apps have stale old versions sitting on disk alongside current versions.
- Unknown message types crash the client receive handler. An unrecognized type throws an unhandled exception that kills all message processing until page reload.
- DM Create allows duplicate conversations with the same pair of users.
- Empty DMs created without content show up as blank entries in the DM list.

- bash
