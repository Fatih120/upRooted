# Root Communications v0.9.86 - Attack Surface Map

## External Endpoints (Outbound from App)

| Endpoint | Protocol | Purpose |
|----------|----------|---------|
| api.rootapp.com | HTTPS/WSS | Backend API + WebSocket messaging |
| installer.rootapp.com | HTTPS | Velopack auto-updates |
| o447951.ingest.sentry.io | HTTPS | Sentry telemetry (sub-apps) |
| o4509469920133120.ingest.us.sentry.io | HTTPS | Sentry telemetry (WebRTC) |
| effectssdk.ai | HTTPS | WASM audio/video AI models |

All behind Cloudflare. No certificate pinning.

## Exposed Sentry DSNs

DSN 1 (sub-apps, SDK v1.33.7):
  Key: c1dfb07d783ad5325c245c1fd3725390
  Org: o447951
  Project: 4509632503087104
  Confirmed exploitable: HTTP 200 on fake event

DSN 2 (WebRTC bundle):
  Key: 75fd2cd278ac35657edd7ee8df86eb37
  Org: o4509469920133120
  Project: 4509832005681152
  Config: sendDefaultPii:true, replaysOnErrorSampleRate:.25

## Bearer Token Format (Reverse Engineered)

Token parsed by `tM` class in WebRTC bundle:
  128 bytes decoded from base64url:
    Bytes  0-15:  userId UUID (16 bytes) → base64url → 22 chars
    Bytes 16-31:  deviceId UUID (16 bytes) → base64url → 22 chars
    Bytes 32-127: Cryptographic signature (96 bytes, prevents forgery)

Token replay IS possible (no expiry, no revocation). Token forgery NOT possible (96-byte signature).
RefreshToken endpoint extends access indefinitely from stolen token.

## WebRTC Bundle Token Flow (Production)

1. .NET native app authenticates user → obtains Bearer token from backend
2. .NET calls: window.__nativeToWebRtc.initialize({token: {clientToken, userId, deviceId}, ...})
3. WebRTC bundle stores token in QNe class → creates XU transport with Authorization header
4. All 27 gRPC services called via: fetch(url, {headers: {Authorization: "Bearer " + token}})
5. Token resides in JS memory — accessible to any code in WebRTC context
6. PacketManager WebSocket also uses same Bearer token for signaling

## Hardcoded Credentials (WebRTC Bundle, Line 13929)

Bearer token (4 occurrences):
  AChObn-FjgGkgtSzdaVi0wAsrDJSU4oSpE_mAVP-Hid2s1K_agqiTq35xjQmFtHY6sieXvQHTbQhbvTUGcbZwdar9JegpWlwWiFDMzXg6viHxLmglupX70uVimExkhJENIuwxIaKNxyXtpbEaKZdoVfKutg-0YEU6nIIRZ8UidY

Hardcoded IDs:
  userId:      AChObn-FjgGkgtSzdaVi0w
  deviceId:    ACysMlJTihKkT-YBU_4eJw
  communityId: ACiNMcK2gwKa7z5MDT_ScQ
  channelId:   ACiNMcK3iQSUPvZBKshscQ

Mock bridge base URL: https://localhost:3005
Activation gate: !SA() (user-agent check for "rootplatform", trivially bypassable)

## Local IPC

Port 49212 (dynamic): DotNetBrowser proprietary binary protocol
4 loopback connections between Root.exe and Chromium processes
NOT the dev bridge ports (8080/8081/8082 are closed in production)

.NET Named Pipe IPC (from binary analysis):
  AppToNativeBridge — app-to-native communication
  AppToNativePrivateBridge — private channel
  NativeToAppPrivateBridge — native-to-app private channel
  NativeToWebRtcBridge — native-to-WebRTC media bridge
  Named pipe infrastructure: NamedPipeServerStream, NamedPipeClientStream

.NET localhost endpoints (from binary strings):
  localhost:3005 — primary application server (mock bridge uses this)
  localhost:5000 — secondary service endpoint
  localhost:8969 — alternative service / debug stream

## JavaScript Bridge (window.__rootSdkBridgeWebToNative)

Any code in DotNetBrowser context can call:
  .send(protobufBinary)           <-- RAW protobuf injection, bypasses rate limiter
  .searchUserProfiles(query)      <-- encodeURI injection
  .listSuggestedUserProfiles()    <-- user enumeration
  .listCommunityRoles()           <-- role enumeration
  .uriToUrl(uri)                  <-- scheme passthrough (javascript:, data:)
  .uriToImageUrl(uri, resolution) <-- double-? bug + encodeURI injection
  .uploadTokenToPreviewImageUrl(token) <-- encodeURI injection
  .restart(url)                   <-- OPEN REDIRECT, zero validation, javascript: exec

## JavaScript Bridge (window.__webRtcToNative) - WebRTC

  .kickPeer(userId)               <-- kick ANY user from call
  .setAdminMute(deviceId, bool)   <-- admin mute anyone
  .setAdminDeafen(deviceId, bool) <-- admin deafen anyone
  .setSpeaking(bool, deviceId, userId) <-- spoof speaking indicators
  .setHandRaised(bool, deviceId, userId)
  .viewProfileMenu(userId, coords)
  .viewContextMenu(userId, coords, tileType, data)
  .failed(error)                  <-- trigger error alert with JSON dump
  .initialized() / .disconnected() / .localAudio/Video/Screen Started/Stopped/Failed

## JavaScript Bridge (window.__nativeToWebRtc) - WebRTC Inbound

  .initialize(params)             <-- start rogue WebRTC session
  .kick(userId)                   <-- kick users
  .setAdminMute(userId, bool)     <-- admin mute
  .setAdminDeafen(userId, bool)   <-- admin deafen
  .updateMyPermission(perms)      <-- CLIENT-SIDE PERMISSION ESCALATION
  .receiveRawPacket(buffer)       <-- INJECT SIGNALING PACKETS
  .updateProfile(profile)         <-- spoof user profile
  .setIsVideoOn/AudioOn/ScreenShareOn
  .updateVideoDeviceId/AudioInputDeviceId/AudioOutputDeviceId

## JavaScript Bridge (window.__rootSdkBridgeNativeToWeb) - Inbound

  .receive(protobufBinary, type)  <-- INJECT FAKE SERVER RESPONSES
  .setTheme(themeObj)             <-- CSS injection via custom properties

## JavaScript Bridge (window.__mediaManager)

  .getDevices(kind)               <-- enumerate audio/video devices

## gRPC Service Routes

### Full Backend Catalog (27 services, 163 methods — all in WebRTC bundle)

Services accessible via gRPC-web with Bearer token (root.* namespace):

  AccessRuleGrpcService (7):     Create, Edit, Update, ListByChannelOrChannelGroup, ListByRoleOrMember, Get, Delete
  AppReviewGrpcService (5):      Create, List, ListMine, Edit, Delete
  AppStoreGrpcService (6):       Get, GetGlobalSettings, List, Search, ListVersion, ListShort
  AssetGrpcService (3):          Get, GetUploadTokenStatus, AppCreate
  ChannelGroupGrpcService (6):   Create, Get, List, Edit, Move, Delete
  ChannelGrpcService (6):        Create, Get, List, Move, Edit, Delete
  CommunityAppLogGrpcService (3): Create, Get, List
  CommunityAppGrpcService (14):  Get, List, Add, Remove, UpdateVersion, SetSettings, SetButton, SetAppOrganizationAccess, SetRating, GetSettings, Initialize, ListChannelGroups, ListChannelGroupsForAdd, SetDevSettings
  CommunityLogGrpcService (1):   List
  CommunityMemberBanGrpcService (7): Create, CreateBulk, Delete, Kick, KickBulk, List, Get
  CommunityMemberInviteGrpcService (6): Create, Respond, LinkJoin, Delete, Get, List
  CommunityMemberRoleGrpcService (4): Add, List, Remove, SetPrimary
  CommunityMemberGrpcService (6): Get, List, ListAll, Edit, Move, SetFavorite
  CommunityRoleGrpcService (6):  Create, Edit, Move, Delete, List, Get
  CommunityGrpcService (11):     ListMine, GetExtended, GetForApp, Get, Attach, Detach, Leave, Create, Edit, Delete, OwnerEdit
  DirectMessageGrpcService (6):  List, Create, AddMembers, Find, RemoveUser, RingDecline
  DirectoryGrpcService (8):      Create, Edit, Move, Delete, Get, List, GetAll, ListAll
  FileGrpcService (9):           Create, Edit, Move, Search, SearchCommunity, Delete, List, Get, Download
  FriendshipGroupGrpcService (5): Create, Delete, List, Edit, Move
  FriendshipInviteGrpcService (2): Create, Respond
  FriendshipGrpcService (2):     Delete, Move
  LinkGrpcService (6):           CommunityInviteLinkCreate/Get/GetInfo/Delete/List/ListMine
  v2.MessageGrpcService (15):    Create, Delete, Edit, Get, List, Flag, PinCreate, PinDelete, PinList, ReactionCreate, ReactionDelete, Search, SearchContainer, SetTypingIndicator, SetViewTime
  NotificationGrpcService (7):   List, CountUnviewed, SetViewed, SetContainerViewed, SetAllViewed, Delete, DeleteAll
  SupportGrpcService (1):        Create
  UserGrpcService (31):          GetSelf, GetExtendedUsersById, GetExtendedUsersByUsername, SignIn, SetMaxOnlineStatus, SetDeviceOnlineStatus, GetNewHubserverEndpoint, SignUp, SignOut, SetPassword, ForgotPassword, ForgotUsername, ResetPassword, SetEmailVerificationCode, ResendEmailVerificationCode, SetUsername, SetProfilePicture, SetEmail, SetDirectMessageInviteRequirement, SetFriendshipInviteRequirement, FindByUsername, GetConnectionBetween, GetUserNote, SetUserNote, SetMobileNotificationToken, Delete, BlockList, BlockCreate, BlockDelete, Flag, RefreshToken
  WebRtcGrpcService (12):        SetMuteAndDeafen, SetMuteAndDeafenOther, GetIceInfo, SessionCreate, Renegotiate, TracksCreate, DataChannelsCreate, TracksClose, Detach, Kick, List, ListTracksForSession

### Sub-App Services (via protobuf bridge to .NET)

Raid Planner (86 message types):
  /template.TemplateService/{Create,Update,Get,FindAll,Delete,NameExists}Template
  /raid.RaidService/{Create,GetById,FindAll,Cancel,ForceActivate,Update}Raid
  /raid_user.RaidUserService/{Create,Update}RaidUser
  /comment.CommentService/{Create,FindAll,Delete,Update}Comment
  /raid_action_log.RaidActionLogService/{Create,CreateMultiple,FindAll}RaidActionLog
  /app_role.PermissionsService/GetPermissions
  /community.CommunityService/{FindAllUsers,GetUserById,FindAllChannels}

Minecraft (51 message types):
  SftpConnect/Disconnect, InstallPlugin, ReadPluginConfig
  TestPluginConnection, SaveServer, GetAllServers, GetServerById
  DeleteServer, UpdateServer, RefreshServerStatus
  SendCommand, GetConsoleHistory, UpdatePlugin, UpdatePluginViaSftp
  RefreshAnalytics, GetAnalyticsCooldown

### High-Risk Request Messages (gRPC-web, target other users)
  CommunityMemberBanCreateRequest.user_id    <-- ban ANY user
  CommunityMemberBanKickRequest.user_id      <-- kick ANY user
  CommunityMemberBanKickBulkRequest.user_ids <-- mass kick
  CommunityMemberEditRequest.user_id         <-- change another user's nickname
  CommunityMemberRoleAddRequest.user_ids     <-- add roles to ANY users
  CommunityMemberRoleRemoveRequest.user_ids  <-- remove roles from ANY users
  CommunityOwnerEditRequest.owner_user_id    <-- transfer community ownership (needs password)
  WebRtcKickRequest.user_id                  <-- kick from call
  WebRtcSetMuteAndDeafenOtherRequest.user_id <-- mute/deafen ANY user

### High-Risk Request Messages (sub-app protobuf, Raid Planner)
  CreateRaidUserRequest.user_id   <-- sign up OTHER users
  CreateRaidRequest.leader_id     <-- impersonate leadership
  CreateCommentRequest.user_id    <-- comment as others
  CreateRaidActionLogRequest.actor_id <-- forge audit logs
  CancelRaidRequest.id            <-- IDOR deletion
  ForceActivateRaidRequest.id     <-- admin action
  SendCommandRequest.command      <-- arbitrary Minecraft commands
  InstallPluginRequest.session_id <-- session replay attack

### Key Security Finding: MessageCreateRequest
  root.MessageCreateRequest has NO user_id field.
  Fields: context, container_id, community_id, content, attachment_token_uris,
          parent_message_ids, needs_parent_message_notification.
  Sender identity derived SOLELY from Bearer token.
  Message impersonation requires token theft, not field manipulation.

## XSS Sinks

innerHTML with user data (Raid Planner DevBar):
  Line 35249: nickname + ID injected raw
  Line 35170: nickname in selector dropdown
  Line 35244: nickname alternate render
  Line 35261: community role names
  Line 35269: theme name with attribute injection
  23 total innerHTML sinks in Raid Planner
  11 dangerouslySetInnerHTML (React DOM internals)

Alpine.js Function() constructor (Stickerwall):
  Line 14: new Function(["scope"], `with (scope) { ... ${expression} ... }`)
  Equivalent to eval(). Any user input in x-data/x-text/x-on/x-bind = RCE.

Alpine.js x-html directive (Stickerwall):
  directive("html", ...) sets e.innerHTML = t with no sanitization

## URL Construction Bugs

encodeURI() instead of encodeURIComponent() in all 7 apps:
  users/search?search= + encodeURI(input)         <-- & = ? # pass through
  asset?query= + encodeURI(uri)                    <-- same
  asset?query= + encodeURI(uri) + ?resolution=...  <-- double ? bug
  asset/preview?query= + encodeURI(token)          <-- same

URI pass-through for non-root:// schemes:
  uriToUrl() returns input unchanged if not root://
  javascript: and data: URIs pass through unfiltered

## Session Storage

__rootUser: JSON with userId, deviceId, nickname, communityRoleIds, onlineStatus
__virtualRootUser: same structure
Any XSS = trivial exfiltration

## File System Targets

All user-writable (ENTROPY\bash Full Control):

Critical files:
  C:\...\Store\AuthToken (390 bytes, binary)
  C:\...\RootApps\registry.json (controls which apps load)
  C:\...\WebRtcBundle\rootapp-desktop-webrtc.js (4.2 MB, code execution)
  C:\...\RootApps\*\*\app\client\dist\assets\index-*.js (all 7 app bundles)

Critical DLLs:
  C:\...\Root\current\uiohook.dll (input hooks, keylogger potential)
  C:\...\Root\current\vcruntime140_cor3.dll
  C:\...\Root\current\libonigwrap.dll

No runtime Authenticode verification on any of these.
No SRI hashes on JS/HTML files.

## WASM Supply Chain (effectssdk.ai)

Base URLs: Settings.API_URL = "https://effectssdk.ai/sdk/session/"
           Settings.SDK_URL = "https://effectssdk.ai/sdk/audio/"

Runtime downloads (confirmed live):
  https://effectssdk.ai/sdk/audio/models/audio-model-3.3.2.wasm  (11.2 MB, balanced) - 200 OK
  https://effectssdk.ai/sdk/audio/models/audio-model-2.5.0.tsvb  (52.8 MB, quality)  - 200 OK
  https://effectssdk.ai/sdk/audio/models/audio-model-1.0.1.tsvb  (7.1 MB, speed)     - 200 OK

Confirmed 404 (broken/stale paths):
  https://effectssdk.ai/sdk/session/                              - 404 (auth API dead)
  https://effectssdk.ai/sdk/audio/dev/3.2.2/ort-wasm.wasm        - 404
  https://effectssdk.ai/sdk/audio/dev/3.2.2/ort-wasm-simd.wasm   - 404

Infrastructure: Cloudflare CDN, Access-Control-Allow-Origin: *, Cache-Control: 3 days
Cache: Browser Cache API ('atsvb'), no expiration, no integrity check
No SRI hashes. No checksums. CUSTOMER_ID empty in bundle.
Balanced model runs as native WASM (Emscripten) with direct HEAPF32 memory access.
Compromise of effectssdk.ai = silent audio surveillance for all Root users.

## Update Channel

URL: https://installer.rootapp.com/installer/Windows/X64/releases.win.json?arch=x64&os=win&rid=win-x64&id=Root&localVersion=0.9.86
Sends .betaId (e3126fe30f9c4b6f91040b3e80497a8b) for tracking
Manifest has SHA256 hashes but no cryptographic signature
No certificate pinning

## Minecraft App Credential Flow

Protobuf defs in 002c6a0d-3616 app:
  sftp_host, sftp_port, sftp_username, sftp_password
  api_endpoint, api_key, server_id
  updatePluginViaSftp({serverId, sftpPassword})

SFTP passwords flow through protobuf IPC bridge in cleartext.
UI displays credentials: username@host:port

## postMessage

24+ send sites, 15+ message event listeners across all bundles.

CRITICAL: WebRTC bundle Sentry rrweb replay:
  window.parent.postMessage({ type: "rrweb", event: le(at), origin: window.location.origin, isCheckout: ot }, "*")
  Sends: complete DOM snapshots, mouse movements, input values, mutations
  Target: wildcard "*" - any parent frame receives all data

Lexical Dragon NaturallySpeaking handler (Raid Planner, Minecraft, Task Tracker):
  addEventListener("message", handler) WITH origin check (same-origin)
  Accepts: nuanria_messaging protocol, makeChanges function
  Inserts arbitrary text into Lexical editor when focused

Audio Workers (secure - private channels):
  Effects SDK Worker <-> Worklet: Float32Array audio frames, auth keys
  Noise Gate Worklet: pushToTalk state, talking indicators
  Native Screen Audio Worklet: audioData buffers
  Prism.js Worker: syntax highlighting (language, code)
  PixiJS Workers: ImageBitmap, Basis transcoder, KTX2 transcoder

## No CSP

Zero Content Security Policy on all 9 HTML files.
Host iframe: no sandbox attribute, no allow attribute.
No script-src, no frame-ancestors, no object-src restrictions.

## Console Logging in Production

WebRTC bundle logs: Payload, Old State, New State (full state dumps)
Sub-apps log: WebSocket state, user data, bridge messages, connection info

## Auth Artifact Persistence (Token Replay Risk)

All persist on disk indefinitely after app close, no secure deletion:
  data.json:        1,737 bytes (encrypted, NOT DPAPI, entropy 7.67)
  Login Data:       40KB  SQLite (Chromium credential storage)
  Account Web Data: 76KB  SQLite (account data)
  Web Data:        150KB  SQLite (cookies/form data)
  Trust Tokens:     SQLite (OAuth-style tokens)

No logout mechanism. No session revocation. Extracted token = indefinite access.

## Account Hijacking Vectors (Ranked)

1. IDOR via protobuf (H9/H13): Client-supplied user_id in requests — act as another user
2. Token persistence (M22): Extract data.json from disk — full account takeover
3. Hardcoded token (C1): Public token in bundle — access as specific user
4. XSS → bridge (H3/H10/H12): XSS chains to bridge access + identity spoofing
5. Dev bridge (M18/M20): sessionStorage poisoning → CLIENT_ATTACH impersonation
