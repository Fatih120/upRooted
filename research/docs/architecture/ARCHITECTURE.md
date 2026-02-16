# Root Communications v0.9.86 - Architecture Reference

## What Root Is

Community platform desktop app. Tagline: "Not Just Chat, Built for Action!" Combines messaging with 7 embedded mini-apps (polls, task tracker, raid planner, stickerwall, suggestions, hexatris game, minecraft server setup). Voice/video calls via WebRTC. Windows/macOS/Linux/iOS/Android.

Company: Root Communications, Inc., West Hollywood, California, US.

## Tech Stack

- .NET 10, Avalonia Desktop 11.3.10 (WPF-like cross-platform UI)
- DotNetBrowser (embedded Chromium) for web sub-apps
- React 19, Vite, TanStack Router, Radix UI (sub-app frontend)
- Alpine.js (Stickerwall app only)
- Lexical (rich text editor in Raid Planner)
- Prism.js (syntax highlighting in Raid Planner)
- PixiJS/WebGL (Stickerwall graphics)
- Protocol Buffers (protobuf-ts) for message serialization
- WebRTC + Effects SDK (effectssdk.ai) for voice/video with ONNX Runtime WASM
- Velopack v0.0.1298 for auto-updates
- Sentry JavaScript Browser SDK (v1.33.7 in sub-apps, newer in WebRTC)
- Azure Code Signing (Microsoft ID Verified CS AOC CA 02)
- Backend: api.rootapp.com behind Cloudflare

## File Layout

### Install Directory
```
C:\Users\bash\AppData\Local\Root\
  current\
    Root.exe                    617 MB (self-contained .NET 10)
    uiohook.dll                 709 KB (UI input hooks - keylogger potential)
    av_libglesv2.dll            5.4 MB (OpenGL ES)
    libSkiaSharp.dll            9.4 MB (2D graphics)
    libHarfBuzzSharp.dll        1.8 MB (text shaping)
    D3DCompiler_47_cor3.dll     4.9 MB (Direct3D)
    PresentationNative_cor3.dll 1.2 MB (WPF)
    wpfgfx_cor3.dll             1.9 MB (WPF graphics)
    PenImc_cor3.dll             158 KB (pen input)
    vcruntime140_cor3.dll       125 KB (VC++ runtime)
    libonigwrap.dll             538 KB (regex)
    sq.version                  458 B  (Velopack manifest)
    rooticon.ico / rooticonstaging.ico
    DotNetBrowser\RootApps\Bundle\Host\index.html  (iframe container)
    Helpers\Emojis\emoji.json   1.9 MB
  packages\
    .betaId                     e3126fe30f9c4b6f91040b3e80497a8b
  velopack.log                  3 KB
```

### Profile Data (3,646 files, 1.18 GB)
```
C:\Users\bash\AppData\Local\Root Communications\Root\profile\default\
  Store\
    AuthToken                   390 bytes (binary/encrypted)
  RootApps\
    registry.json               (maps 7 app UUIDs to index.html paths)
    002c8f0a-...\0.4.2\         Hexatris (multiplayer game)
    002c6a0c-...\0.4.4\         Polls
    002c6a0d-a29d-...\0.4.4\    Suggestions
    002c6a0d-3616-...\0.4.5\    Minecraft Easy Setup
    002a3d83-e2e0-...\0.4.5\    Task Tracker
    002a3d84-...\0.4.8\         Stickerwall (Alpine.js + PixiJS)
    002a3d83-c35a-...\0.4.11\   Raid Planner (largest, 80k+ lines)
  WebRtcBundle\
    rootapp-desktop-webrtc.js       4.2 MB (13,930 lines)
    rootapp-desktop-webrtc.js.map   13.5 MB (source map!)
    index.html
  DotNetBrowser\WindowsX64\data\    (Chromium internal DBs, 8 files)
  Cache\                            (FASTER log files, cached media)
```

### Roaming Data
```
C:\Users\bash\AppData\Roaming\Root Communications\Root\profile\default\
  data.json                     1,592 bytes (encrypted/binary)
```

## IPC Architecture

```
Root.exe (.NET 10)
  |
  +-- DotNetBrowser Chromium
  |     |
  |     +-- Host index.html (bare iframe, no CSP, no sandbox)
  |     |     |
  |     |     +-- Sub-app iframes x7 (React/Vite bundles)
  |     |     +-- WebRTC bundle iframe (voice/video)
  |     |
  |     +-- Proprietary binary IPC on localhost:49212 (dynamic port)
  |     |     4 established loopback connections
  |     |
  |     +-- JS Bridge: window.__rootSdkBridgeWebToNative
  |     +-- JS Bridge: window.__rootSdkBridgeNativeToWeb
  |     +-- JS Bridge: window.__rootSdkBridgeDev
  |
  +-- Outbound HTTPS to api.rootapp.com (Cloudflare: 172.66.154.209)
  +-- Outbound HTTPS to installer.rootapp.com (Cloudflare: 104.20.41.137)
  +-- Outbound HTTPS to o447951.ingest.sentry.io (Sentry telemetry)
  +-- Outbound HTTPS to o4509469920133120.ingest.us.sentry.io (Sentry WebRTC)
  +-- Outbound HTTPS to effectssdk.ai (WASM audio/video AI)
```

## Native Bridge Methods

Exposed on window.parent.__rootSdkBridgeWebToNative:
- .send(protobufBinary) - send arbitrary protobuf messages to native
- .searchUserProfiles(query) - enumerate users
- .listSuggestedUserProfiles() - list suggested users
- .listCommunityRoles() - enumerate roles
- .uriToUrl(uri) - resolve root:// URIs
- .uriToImageUrl(uri, resolution) - resolve image URIs
- .uploadTokenToPreviewImageUrl(token) - preview uploaded files
- .restart(url) - restart app (or redirect to URL in dev mode)

Additional window globals (WebRTC bundle):
- window.__webRtcClient
- window.__webRtcToNative
- window.__nativeToWebRtc
- window.__mediaManager
- window.__rootApiBaseUrl
- window.__nativeScreenAudioService
- globalThis.requestAuthFromMainThread

## DevBridge API Endpoints (code ships, ports closed in production)

Ports: 8080 (WebSocket), 8081 (REST), 8082 (update socket)
All plaintext HTTP, all unauthenticated:
- POST /asset/upload (FormData)
- POST /users/profiles (JSON body with IDs)
- GET /users/suggested
- GET /users/search?search=<query>
- GET /users/list
- GET /communityRoles/list
- GET /asset?query=<root://uri>
- GET /asset/preview?query=<token>
- POST /dev/users/role/add
- POST /dev/users/role/delete
- GET /dev/users/list

## Autostart

Via Startup folder shortcut (Root.lnk), NOT registry Run key.
Shortcut locations per sq.version: Startup, Desktop, StartMenuRoot.

## Code Signing

All 11 EXE/DLL files have valid Authenticode signatures:
- Root Communications, Inc.: Root.exe, av_libglesv2.dll, libonigwrap.dll, uiohook.dll
- Microsoft Corporation: D3DCompiler, libHarfBuzzSharp, libSkiaSharp, vcruntime140
- Microsoft .NET: PenImc, PresentationNative, wpfgfx

Certificate: CN="Root Communications, Inc."
Issuer: CN=Microsoft ID Verified CS AOC CA 02
Thumbprint: 79DF21E0E0C3FFD923977F943788B751BA5290CB
Validity: 2026-02-09 to 2026-02-12 (3-day window)

## Network (Active Connections Observed)

- 2x HTTPS to 172.66.154.209:443 (Cloudflare, api.rootapp.com)
- 1x HTTPS to 104.20.41.137:443 (Cloudflare, installer.rootapp.com)
- Local IPC: 127.0.0.1:49212 listening, 4 established connections
- No services, no scheduled tasks, no raw TCP/UDP listeners
