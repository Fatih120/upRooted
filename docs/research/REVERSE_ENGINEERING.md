# Reverse Engineering Root Communications

> **What this is:** Complete RE methodology narrative — how we went from an unknown .NET binary to a fully documented architecture, including source map extraction, binary analysis, protobuf discovery, token extraction, bridge API discovery, CSS/theme extraction, and DOM mapping.
> **Read when:** Understanding HOW findings were discovered; replicating RE techniques; extending the research.
> **Skip if:** You just need the findings → [ROOT_INTERNALS.md](ROOT_INTERNALS.md) or [SECURITY_RESEARCH.md](SECURITY_RESEARCH.md). You need the current token format → [ROOT_INTERNALS.md §5](ROOT_INTERNALS.md#5-authentication-and-tokens).
> **Does NOT cover:** Current Root architecture (reference format) → [ROOT_INTERNALS.md](ROOT_INTERNALS.md) | Security findings summary → [SECURITY_RESEARCH.md](SECURITY_RESEARCH.md)

How we went from an unknown 576 MB .NET executable to a fully documented application architecture,
complete with protocol definitions, bridge interfaces, theme specifications, and injection points.

> **Related docs:** [Root Internals](ROOT_INTERNALS.md) | [gRPC Protocol](GRPC_PROTOCOL.md) | [Security Research](SECURITY_RESEARCH.md) | [Research Index](RESEARCH_INDEX.md)

---

## Table of Contents

1. [Overview](#1-overview)
2. [Source Map Extraction](#2-source-map-extraction)
3. [Binary Analysis](#3-binary-analysis)
4. [Protobuf Schema Discovery](#4-protobuf-schema-discovery)
5. [Port Probing and Endpoint Discovery](#5-port-probing-and-endpoint-discovery)
6. [Token Extraction](#6-token-extraction)
7. [Bridge API Discovery](#7-bridge-api-discovery)
8. [CSS and Theme Extraction](#8-css-and-theme-extraction)
9. [DOM Structure Mapping](#9-dom-structure-mapping)
10. [Tools Used](#10-tools-used)
11. [Ethical Considerations](#11-ethical-considerations)
12. [Lessons Learned](#12-lessons-learned)

---

## 1. Overview

### The target

Root Communications is a desktop community platform built on .NET 10 with Avalonia UI 11.3.10.
The client ships as a single self-contained executable (`Root.exe`, 576 MB) that bundles the
entire .NET runtime, all managed assemblies, and native dependencies into one file. It uses
DotNetBrowser (a commercial Chromium wrapper for .NET) to host seven React/Vite sub-applications
and a WebRTC voice/video engine inside embedded browser frames.

The application stores profile data, cached web content, and browser resources at:

```
C:\Users\<user>\AppData\Local\Root Communications\Root\profile\default\
```

That directory contains 3,646 files totaling 1.18 GB -- HTML pages, JavaScript bundles, CSS,
images, and DotNetBrowser's internal Chromium databases.

### The challenge

No source code was available. No public API documentation existed. The desktop client used
proprietary binary IPC between the .NET host and the embedded Chromium browser. The backend
communicated via gRPC-web over HTTPS with protocol buffer serialization -- no `.proto` schema
files were published. The application was Authenticode-signed, and all web content was served
from local disk rather than remote URLs.

### The journey

The reverse engineering proceeded in roughly this order:

1. **Static analysis** -- PE header inspection, string scanning, file enumeration
2. **Source map extraction** -- Recovery of 802 original TypeScript files from a shipped source map
3. **Bridge interface mapping** -- Documenting the JavaScript bridge between .NET and Chromium
4. **Token extraction** -- Multiple methods for recovering authentication credentials
5. **Protocol reconstruction** -- Building gRPC-web client tooling from extracted type definitions
6. **Binary deep dives** -- Locating CSS theme data, AXAML resources, and IL injection points
7. **Active endpoint probing** -- Testing all 163 discovered gRPC service methods against production

Each phase built on discoveries from the previous one. The source map extraction (phase 2) was
the single most consequential breakthrough -- it unlocked nearly everything that followed.

---

## 2. Source Map Extraction

### Discovery

While exploring the profile data directory, we found the WebRTC bundle:

```
...\profile\default\WebRtcBundle\
  rootapp-desktop-webrtc.js       4.2 MB  (13,930 lines of minified JavaScript)
  rootapp-desktop-webrtc.js.map   13.5 MB  (source map)
  index.html
```

The `.js.map` file is a standard JavaScript source map -- a JSON document that maps every
character position in the minified bundle back to the original source file, line, and column.
It was shipped to every user's machine alongside the production bundle. No obfuscation, no
encryption, no integrity check prevented its use.

### The extraction tool

We built `source_map_extract.py` to parse the source map and reconstruct the original
source tree on disk.

**Source:** `research/pentesting/extraction/source_map_extract.py`

The tool performs three main tasks:

**Path normalization** (lines 13-40). Source map paths use webpack-style prefixes like
`webpack://root-webrtc/` or `webpack://root-desktop-webrtc/`. The `clean_source_path()`
function strips these prefixes and collapses `../../` sequences to produce clean relative
paths:

```python
for prefix in ["webpack://root-webrtc/", "webpack://root-desktop-webrtc/",
               "webpack:///", "webpack://"]:
    if path.startswith(prefix):
        path = path[len(prefix):]
        break
```

**Selective extraction** (lines 70-109). Rather than dumping all 802 source files (many of
which are node_modules boilerplate), the tool categorizes files and filters node_modules
entries by security-relevant keywords:

```python
dominated_by_interest = any(k in lower for k in [
    "permission", "role", "access_rule", "authorization",
    "community_member", "bridge", "grpc", "service.client",
    "_service", "invite"
])
```

**Categorized output** (lines 116-156). Extracted files are sorted into categories --
bridge files, permission/role files, service definitions, and files containing potentially
sensitive patterns like `bypass`, `debug`, `admin`, `mock`, or `secret`.

### What we recovered

The extraction yielded 328 first-party and security-relevant files. The directory structure
revealed Root's complete frontend architecture:

```
src_dump/Libs/JS/rootapp-desktop-webrtc/
  src/
    services/
      nativeToWebRtcBridge.ts         65 bridge command implementations
    types/
      baseWebRtcBridge.ts             Full bridge interface definitions
    redux/
      bridgeMiddleware.ts             Redux middleware for bridge IPC
    mocks/
      mockBridge.ts                   Mock bridge with HARDCODED credentials
  node_modules/@rootplatform/apiclient-internal/
    dist/grpc_client/base/webapi/
      services/                       27 gRPC service client stubs
      requests/                       Request message type definitions
      responses/                      Response message type definitions
      permission.js                   22 channel + 14 community permission enums
```

This single extraction gave us:

- The complete original TypeScript source for the WebRTC layer
- Full gRPC client definitions for all 27 backend services (163 RPC methods)
- Every protobuf request and response message schema
- The permission model (22 channel permissions, 14 community permissions)
- Bridge interface contracts with all 70 combined IPC methods
- A hardcoded development Bearer token and mock credentials

### Significance

Without the source map, reverse engineering the gRPC protocol would have required capturing
network traffic and manually decoding protobuf wire format byte by byte -- a process that
could take weeks for 163 methods. With the source map, we had complete type definitions for
every request and response message within minutes.

---

## 3. Binary Analysis

### PE header and .NET metadata

The first step was confirming what `Root.exe` actually is. We verified it as a self-contained
.NET 10 application by:

- Checking PE headers for .NET metadata markers
- Scanning the binary for assembly manifest strings (`Avalonia.Controls`, `Avalonia.Base`,
  `DotNetBrowser.Core`, etc.)
- Finding the embedded `.runtimeconfig.json`:
  `"framework": { "name": "Microsoft.NETCore.App", "version": "10.0.0" }`
- Identifying the 10 companion DLLs in the install directory

Code signing was documented but proved irrelevant to injection:

```
Subject:    CN="Root Communications, Inc."
Issuer:     CN=Microsoft ID Verified CS AOC CA 02
Thumbprint: 79DF21E0E0C3FFD923977F943788B751BA5290CB
```

.NET does not verify Authenticode signatures at P/Invoke load time, and CLR profiler injection
bypasses the signed binary entirely -- no file modification is needed.

### Identifying the embedded browser

Root does not use standard WebView2 or CEF for its web content. We discovered it uses
DotNetBrowser by exploring the profile directory structure:

```
...\profile\default\DotNetBrowser\
...\profile\default\DotNetBrowser\RootApps\Bundle\Host\index.html
```

The host `index.html` is a bare iframe container with no Content Security Policy (CSP) and no
sandbox attributes. Inside it, Root loads seven sub-applications as React/Vite bundles in
iframes, plus the WebRTC bundle for voice and video.

### Binary offset mapping for theme resources

We performed targeted binary analysis to locate CSS theme definitions embedded in the
executable. The tool `extract_css_boundaries.py` reads the binary at a known offset and
uses brace-depth tracking to determine exact boundaries:

**Source:** `research/pentesting/extraction/extract_css_boundaries.py` (lines 37-60)

The brace-depth algorithm walks the CSS byte stream, incrementing depth on `{` and
decrementing on `}`. When depth returns to zero and the next non-whitespace byte is a null
or non-ASCII character, it marks the end of the CSS block:

```python
for i in range(len(css_chunk)):
    b = css_chunk[i]
    if b == ord("{"):
        brace_depth += 1
        found_first = True
    elif b == ord("}"):
        brace_depth -= 1
        if brace_depth <= 0 and found_first:
            # Check next non-whitespace byte
            ...
```

The tool also performs a full binary scan for `--rootsdk-` pattern occurrences in both
UTF-8 and UTF-16LE encodings (lines 97-145), confirming whether all CSS variable references
reside within the identified CSS block or appear elsewhere in the binary.

### IL injection point discovery

For the hook system, we needed to understand Root's startup sequence to identify where to
inject managed code. This involved:

- Mapping the module load order to find which assemblies load first
- Identifying methods with suitable TypeRef metadata for `Assembly.LoadFrom` and
  `Assembly.CreateInstance`
- Understanding the Avalonia initialization lifecycle to time our visual tree modifications

The CLR profiler receives `ModuleLoadFinished` callbacks for every assembly. We wait for the
first module that contains proper TypeRef entries for `System.Reflection.Assembly`, then
create cross-module MemberRef tokens and prepend 26 bytes of IL into the first available
method body. This IL loads our managed hook DLL inside a try/catch wrapper so Root continues
normally if anything fails.

---

## 4. Protobuf Schema Discovery

### The problem

Root's backend communicates via gRPC-web over HTTPS at `api.rootapp.com`. All request and
response bodies are serialized as protocol buffers -- a compact binary format that is
unreadable without the original `.proto` schema definitions. No schema files were published.

### The solution: extracted type definitions

The source map extraction (Section 2) recovered the `@rootplatform/apiclient-internal`
package, which contained TypeScript client stubs for all 27 gRPC services. Each stub file
defined the request and response message structures with full field names and types.

From these stubs we reconstructed the protobuf encoding. The shared gRPC library we built
(`research/pentesting/grpc_lib.py`) implements encoding and decoding primitives.

**Source:** `research/pentesting/grpc_lib.py` (lines 44-108)

Encoding primitives:

```python
def varint(v):
    """Encode an unsigned integer as a protobuf varint."""
    r = bytearray()
    while v > 0x7f:
        r.append((v & 0x7f) | 0x80)
        v >>= 7
    r.append(v)
    return bytes(r)

def f_str(num, s):
    """Encode a string field."""
    b = s.encode('utf-8')
    return varint((num << 3) | 2) + varint(len(b)) + b

def grpc_frame(data=b''):
    """Wrap data in a gRPC-web frame (flag=0x00, 4-byte big-endian length)."""
    return b'\x00' + struct.pack('>I', len(data)) + data
```

### UUID encoding: the non-standard twist

The trickiest part of protocol reconstruction was UUID encoding. Root uses a non-standard
approach where UUIDs are split into two `fixed64` fields rather than the typical string or
bytes representation:

```
Protobuf message UUID {
  fixed64 high64 = 1;  // First 8 bytes of UUID, big-endian interpretation
  fixed64 low64  = 2;  // Last 8 bytes of UUID, big-endian interpretation
}
```

The encoding function (`grpc_lib.py`, lines 82-88):

```python
def uuid_pb(uid_str):
    """Encode a UUID string as protobuf {fixed64 high=1, fixed64 low=2}."""
    h = uid_str.replace('-', '')
    b = bytes.fromhex(h)
    high = int.from_bytes(b[:8], 'big')
    low = int.from_bytes(b[8:], 'big')
    return f_fixed64(1, high) + f_fixed64(2, low)
```

### Permission model

The extracted `permission.js` revealed the complete RBAC (Role-Based Access Control) model.
We mapped every permission field number to its name (`grpc_lib.py`, lines 388-428):

- 22 channel-level permissions (field numbers 10-31, skipping 11)
- 14 community-level permissions (field numbers 10-23)

Examples include `channel_full_control` (field 10), `channel_delete_message_other` (field 15),
`community_manage_roles` (field 11), and `community_full_control` (field 18).

### Protobuf response decoding

Decoding responses required a generic protobuf parser since we could not use compiled proto
definitions. The `parse_pb()` function (`grpc_lib.py`, lines 126-166) implements recursive
wire-format parsing:

- Wire type 0 (varint): integer values
- Wire type 1 (64-bit): fixed64 fields (used for UUID halves)
- Wire type 2 (length-delimited): strings, bytes, or nested messages
- Wire type 5 (32-bit): fixed32 fields

For length-delimited fields, the parser attempts UTF-8 string decoding first, then falls back
to recursive sub-message parsing (up to depth 5), and finally treats the data as raw bytes.

### Active testing results

Of the 163 methods defined in the source, active testing against production revealed 32
actually implemented endpoints across 8 services. The remaining methods returned gRPC
status 12 (UNIMPLEMENTED). This was tested systematically across 7 phases:

- Phase 1: Access rules and permissions
- Phase 2: File and directory operations
- Phase 3: Community and app management
- Phase 4: Invites and member operations
- Phase 5: Notifications and user profiles
- Phase 6: WebRTC and social features
- Phase 7: Remaining services

---

## 5. Port Probing and Endpoint Discovery

### Local IPC architecture

Network analysis of the running Root.exe process revealed:

```
127.0.0.1:49212    LISTENING     (dynamic port, changes each launch)
127.0.0.1:49212    ESTABLISHED   (4 active connections to self)
```

The listening port hosts DotNetBrowser's internal IPC. We probed it with multiple protocols:

- Raw TCP connection: responded with binary data (not HTTP)
- HTTP GET/POST: no valid HTTP response
- WebSocket upgrade: rejected
- Chrome DevTools Protocol (`/json/version`, `/json/list`): no response -- DevTools disabled

The IPC uses a proprietary binary protocol specific to DotNetBrowser's communication layer
between the .NET host and the embedded Chromium engine. It is not amenable to external
interception or injection.

### Remote endpoints

Outbound connections were mapped via `netstat` and DNS resolution:

| Endpoint | Purpose | Protocol |
|----------|---------|----------|
| `api.rootapp.com` (172.66.154.209, Cloudflare) | Backend API | gRPC-web over HTTPS |
| `installer.rootapp.com` (104.20.41.137, Cloudflare) | Auto-updates | HTTPS |
| `o447951.ingest.sentry.io` | Error telemetry (sub-apps) | HTTPS |
| `o4509469920133120.ingest.us.sentry.io` | Error telemetry (WebRTC) | HTTPS |
| `effectssdk.ai` | Audio/video AI processing WASM | HTTPS |

### DevBridge discovery

The extracted source code revealed a development bridge system with three localhost ports:

```
Port 8080: WebSocket (real-time messaging via protobuf)
Port 8081: REST API (file upload, user profiles, search)
Port 8082: WebSocket (update notifications)
```

All three ports serve unauthenticated, plaintext HTTP. The code ships in production builds
but the ports appear to be closed in release configurations. The REST API exposed endpoints
including `GET /users/list`, `GET /users/search`, `POST /asset/upload`, and
`GET /communityRoles/list`.

### Authentication testing

The recon scripts (`research/pentesting/recon/`) systematically tested API endpoint behavior.

`test_auth.py` (lines 6-85) verified that the gRPC endpoints enforce authentication by
sending requests without Bearer tokens and observing the response codes. It tested
`GetSelf`, `SignIn`, `ResetPassword`, `SignUp`, `VerifyEmail`, `ForgotPassword`, and
`Authenticate` -- confirming that unauthenticated requests receive proper rejection.

`check_response_headers.py` (lines 8-48) made authenticated gRPC-web calls and captured
the full HTTP response details -- status codes, headers, and body content -- for both
working endpoints (like `GetSelf`) and endpoints that returned empty responses, helping
distinguish between "not implemented" and "bad request" failure modes.

---

## 6. Token Extraction

### Token format

128 bytes total, base64url-encoded (~172 characters): userId UUID (16 bytes) + deviceId UUID (16 bytes) + cryptographic signature (96 bytes). No expiry, no IP binding. For full token format details and security implications, see [ROOT_INTERNALS.md §5](ROOT_INTERNALS.md#5-authentication-and-tokens).

### Method 1: Hardcoded token in production bundle

The most surprising discovery. The production WebRTC bundle contained a hardcoded development
token appearing four times:

```javascript
token = new tM("AChObn-FjgGkgtSzdaVi0wAsrDJSU4oSpE_mAVP-Hid2s1K_...");
baseUrl = "https://localhost:3005";
```

This was found using `find_hardcoded_token.py` and `extract_mock_bridge.py`. The mock bridge
activates when the user-agent does not contain `"rootplatform"`, meaning loading the WebRTC
bundle in a normal browser hands you a ready-to-use authenticated session. The token belonged
to a development account.

**Source:** `research/pentesting/tokens/find_hardcoded_token.py` (lines 6-9)

```python
for m in re.finditer(r'new tM\s*\(\s*["\']([^"\']+)["\']', content):
    print(f'new tM("{m.group(1)[:200]}") at position {m.start()}')
```

### Method 2: Process memory scanning

Root stores active tokens in plaintext in process memory. We built a memory scanner using
Windows APIs to walk committed memory regions and search for base64url patterns.

**Source:** `research/pentesting/extraction/memory_scan.py` (lines 74-173)

The scanner:

1. Enumerates Root.exe processes via `CreateToolhelp32Snapshot` (lines 50-71)
2. Opens each process with `PROCESS_VM_READ | PROCESS_QUERY_INFORMATION` (line 78)
3. Walks memory regions using `VirtualQueryEx`, filtering for committed and readable pages
   (lines 89-106)
4. Reads each region with `ReadProcessMemory` and searches for base64url strings 168-180
   characters long (line 120)
5. Decodes candidates and validates that they are exactly 128 bytes, then extracts the
   embedded userId UUID for verification (lines 122-137)

```python
for m in re.finditer(r'[A-Za-z0-9_-]{168,180}', text):
    candidate = m.group()
    padded = candidate.replace('-', '+').replace('_', '/')
    decoded = base64.b64decode(padded + '=' * (4 - len(padded) % 4))
    if len(decoded) == 128:
        uid_bytes = decoded[:16]
        # Format as UUID and validate
```

This reliably found tokens at addresses like `0x20bc04ad0d6`. Each extracted token was
validated by making a `GetSelf` gRPC call to the production API.

### Method 3: Bearer token interception via memory pattern matching

A second memory scanning approach (`scan_bearer.py`, lines 1-66) searched for higher-level
patterns rather than raw base64 strings:

```python
patterns = [
    (r'Bearer\s+([A-Za-z0-9_\-\.]{50,250})', 'Bearer token'),
    (r'authorization["\s:]+Bearer\s+([A-Za-z0-9_\-\.]{50,250})', 'Auth header'),
    (r'auth[Tt]oken["\s:=]+([A-Za-z0-9_\-\.]{50,250})', 'authToken value'),
    (r'ACzw[A-Za-z0-9_\-]{100,200}', 'ACzw-prefix token'),
]
```

This scanned both `Root.exe` and any `chromium.exe` child processes, searching for Bearer
token strings, Authorization headers, and tokens matching the known prefix pattern.

### Method 4: Chromium storage scanning

We searched DotNetBrowser's internal Chromium storage for persisted tokens.

**Source:** `research/pentesting/tokens/scan_chromium_storage.py` (lines 1-87)

The scanner:

1. Walks the DotNetBrowser data directory for SQLite databases (lines 9-20)
2. Searches Local Storage LevelDB files (`.ldb`, `.sst`) for token patterns (lines 46-65)
3. Checks LevelDB log files for `token`, `bearer`, or `auth` strings (lines 33-42)
4. Inspects SQLite tables for names containing `cookie`, `token`, `auth`, `login`, or
   `session` (lines 68-85)

Result: no tokens found in Chromium storage. Root runs its embedded browser with `--incognito`
mode, which disables persistent localStorage and sessionStorage. Tokens are held only in
process memory and the encrypted AuthToken file.

### Method 5: AuthToken file analysis and DPAPI decryption

The file at `Store/AuthToken` (390 bytes) was analyzed using `parse_authtoken.py`, which
attempted three approaches.

**Source:** `research/pentesting/tokens/parse_authtoken.py` (lines 1-146)

1. **Protobuf parsing** (lines 19-105): A generic protobuf wire-format parser successfully
   identified the file as a protobuf message containing a string field with what appeared to
   be a base64-encoded token.

2. **DPAPI decryption** (lines 108-137): Attempted via `CryptUnprotectData` with both
   `CurrentUser` and `LocalMachine` scopes, and with several entropy guesses (`"Root"`,
   `"Root Communications"`, `"AuthToken"`). All attempts failed, suggesting the file uses
   an application-specific encryption key rather than standard DPAPI.

3. **DPAPI via PowerShell** (`dpapi_decrypt.py`, lines 1-90): A more robust approach using
   `System.Security.Cryptography.ProtectedData` through a PowerShell subprocess, testing
   five different entropy values. Also failed.

### Summary of extraction methods

| Method | Success | Notes |
|--------|---------|-------|
| Hardcoded token in bundle | Yes | Development account token in production code |
| Process memory scan | Yes | Reliable, finds active session tokens |
| Bearer pattern matching | Yes | Finds tokens in both Root and Chromium processes |
| Chromium storage scan | No | Incognito mode prevents persistence |
| AuthToken file (DPAPI) | No | Unknown encryption scheme / entropy |
| Named pipe interception | Yes | Captures token during bridge `initialize()` call |

---

## 7. Bridge API Discovery

### What the bridges are

Root's .NET host communicates with the embedded Chromium browser through JavaScript bridge
objects injected into the browser's `window` scope. Four primary bridges were identified:

| Bridge Object | Direction | Methods |
|---------------|-----------|---------|
| `window.__rootSdkBridgeWebToNative` | Web -> .NET | 8 methods |
| `window.__rootSdkBridgeNativeToWeb` | .NET -> Web | Inbound events |
| `window.__nativeToWebRtc` | .NET -> WebRTC | 42 methods |
| `window.__webRtcToNative` | WebRTC -> .NET | 28 methods |

Plus globals: `window.__webRtcClient`, `window.__mediaManager`,
`window.__rootApiBaseUrl`, `window.__nativeScreenAudioService`, and
`globalThis.requestAuthFromMainThread`.

### Mock bridge extraction

The `extract_mock_bridge.py` tool searched the production WebRTC bundle for the mock
bridge initialization code.

**Source:** `research/pentesting/extraction/extract_mock_bridge.py` (lines 1-31)

It searched for:

1. `new tM("AChObn...` -- the mock token constructor calls (line 9)
2. Hardcoded `communityId` strings (line 17)
3. Sentry DSN URLs (line 21)
4. API base URLs including `api.rootapp.com` and `__rootApiBaseUrl` (lines 25-30)

### Interface mapping from source

The extracted `baseWebRtcBridge.ts` revealed two TypeScript interfaces totaling 70 methods:

**INativeToWebRtc** (42 methods) -- .NET host controlling the browser:

```typescript
initialize(params: InitializeParams): void
kick(userId: string): void
updateMyPermission(perms: Permissions): void
receiveRawPacket(buffer: ArrayBuffer): void
updateProfile(profile: UserProfile): void
setTheme(theme: ThemeConfig): void
```

**IWebRtcToNative** (28 methods) -- browser notifying the .NET host:

```typescript
kickPeer(userId: string): void
setAdminMute(deviceId: string, muted: boolean): void
setAdminDeafen(deviceId: string, deaf: boolean): void
setSpeaking(speaking: boolean, deviceId: string, userId: string): void
```

Zero authorization checks exist on any bridge method. Any code running in the DotNetBrowser
context has unrestricted bridge access.

### Sub-app bridge methods

The `__rootSdkBridgeWebToNative` bridge exposes a smaller but equally powerful surface:

- `.send(protobufBinary)` -- send arbitrary protobuf messages to native
- `.searchUserProfiles(query)` -- enumerate users
- `.listSuggestedUserProfiles()` -- list suggested users
- `.listCommunityRoles()` -- enumerate all roles
- `.uriToUrl(uri)` -- resolve `root://` URIs to HTTP URLs
- `.uriToImageUrl(uri, resolution)` -- resolve image URIs
- `.uploadTokenToPreviewImageUrl(token)` -- preview uploaded files
- `.restart(url)` -- restart the application (or redirect in dev mode)

---

## 8. CSS and Theme Extraction

### Dual theme system

Root uses two separate theme systems that operate simultaneously:

1. **CSS variables** injected into DotNetBrowser's Chromium webview -- 25 custom properties
   per theme defining all UI colors
2. **Compiled AXAML themes** embedded in the .NET binary -- primarily SVG asset path
   references for theme-specific icons

### CSS variable extraction

The CSS theme definitions were located at binary offset `0x12AE9676` in Root.exe. The
`extract_css_boundaries.py` tool determined the exact boundaries:

**Source:** `research/pentesting/extraction/extract_css_boundaries.py` (lines 16-94)

The extraction process:

1. Read 100,000 bytes starting at the known CSS offset (line 33)
2. Track brace depth to find the CSS block boundary (lines 37-60)
3. Scan the entire 576 MB binary for `--rootsdk-` in both UTF-8 and UTF-16LE to confirm
   no CSS variables exist outside the identified block (lines 97-145)
4. Search specifically for `rootsdk-background-primary` to verify theme variable locations
   (lines 149-196)

### Theme color definitions

Three theme variants were documented in `THEME_EXTRACTION.md`:

| Theme | Background Primary | Text Primary | Brand Primary |
|-------|-------------------|--------------|---------------|
| Dark | `#0D1521` (navy) | `#F2F2F2` | `#3B6AF8` |
| Light | `#FBFBFB` (near-white) | `#131313` | `#3B6AF8` |
| Pure Dark | `#161617` (gray) | `#F2F2F2` | `#3B6AF8` |

Each CSS variable uses a `var(--rootsdk-*)` override pattern, allowing SDK consumers (and
our mod framework) to inject custom values.

### AXAML resource locations

Compiled Avalonia XAML themes were found at specific binary offsets:

| Resource | Offset | Encoding | Size |
|----------|--------|----------|------|
| Light.axaml | `0x19EED01F` | UTF-16LE | ~28.5 KB |
| Dark.axaml | `0x19EF3FB0` | UTF-16LE | ~21.5 KB |
| PureDark.axaml | `0x19EF93D8` | UTF-16LE | ~196 B |

An initial discovery from binary string scanning: the extracted AXAML theme files contain no
color brush resources as text strings. The AXAML themes appeared to be purely icon theme
packs, mapping approximately 72 SVG resource keys to Light/Dark icon variants.

> **Correction (2026-02-19):** ILSpy decompilation later revealed that Root's AXAML themes
> DO contain 32 color resource keys — but the colors exist as `uint32` ARGB literals in
> compiled IL deferred factories (`XamlClosure_53/54/55.Build_N`), not as hex strings in the
> AXAML text. Binary string scanning missed them entirely. Root's native Avalonia UI binds to
> these 32 keys (`BrandPrimary`, `TextPrimary`, `BackgroundPrimary`, etc.) via
> `DynamicResourceExtension`. See [`research/ROOT_THEME_SYSTEM_FINDINGS.md`](../../research/ROOT_THEME_SYSTEM_FINDINGS.md)
> for the full catalog.

The web UI is themed through CSS variable overrides (straightforward). The native Avalonia
UI is themed by the hook's `ThemeEngine.cs`, which currently targets FluentTheme/SimpleTheme
keys as a compatibility layer (with visual tree walking to catch hardcoded colors). A
migration to target Root's actual 32 ThemeDictionary keys is planned.

---

## 9. DOM Structure Mapping

### Host iframe architecture

The DotNetBrowser host page (`index.html`) uses a bare iframe container with no CSP and no
sandbox attributes. Inside it, Root loads sub-applications as nested iframes:

| Sub-App | Framework | Bundle ID |
|---------|-----------|-----------|
| Hexatris | React/Vite | `002c8f0a-*` |
| Polls | React/Vite | `002c6a0c-*` |
| Suggestions | React/Vite | `002c6a0d-a29d-*` |
| Minecraft Easy Setup | React/Vite | `002c6a0d-3616-*` |
| Task Tracker | React/Vite | `002a3d83-e2e0-*` |
| Stickerwall | Alpine.js + PixiJS | `002a3d84-*` |
| Raid Planner | React/Vite | `002a3d83-c35a-*` |

The WebRTC bundle runs in a separate iframe loaded from the `WebRtcBundle/` directory.

### Security audit scripts

Two JavaScript audit tools were built to scan the DOM and JavaScript bundles for security-
relevant patterns.

**`extract.js`** (`research/pentesting/js/extract.js`, lines 1-88) performs pattern extraction
across three JavaScript files (WebRTC bundle, Raid Planner, Minecraft app) searching for 36
categories of security patterns. Each pattern definition includes a regex, context window, and
hit limit:

```javascript
const patterns = [
    { name: 'Math.random', regex: /Math\.random\s*\(\s*\)/g, ctx: 150 },
    { name: 'crypto.getRandomValues', regex: /crypto\.getRandomValues/g, ctx: 120 },
    { name: 'localStorage', regex: /localStorage/g, ctx: 120 },
    { name: 'Authorization header', regex: /[Aa]uthorization/g, ctx: 120 },
    { name: 'Bearer token', regex: /[Bb]earer/g, ctx: 120 },
    // ... 31 more patterns
];
```

**`audit.js`** (`research/pentesting/js/audit.js`, lines 1-87) performs a deeper security
audit with 43 pattern categories, adding detection for:

- `eval()` and `new Function()` calls (dynamic code execution)
- `innerHTML` assignments (XSS vectors)
- `dangerouslySetInnerHTML` (React XSS bypass)
- `postMessage` with wildcard origin
- Private IP address patterns
- TLS certificate validation bypass flags
- Hardcoded API keys and secrets

Both tools output matched patterns with surrounding context (100-300 characters) and
file/line/column positions, enabling rapid identification of injection points and security
weaknesses in the minified bundles.

### Plugin injection points

From the DOM analysis, we identified these injection-friendly locations:

- The host `index.html` accepts arbitrary `<script>` tag injection (no CSP, no SRI)
- Each sub-app iframe loads JavaScript from local disk without integrity checks
- The `data-theme` attribute on `document.body` controls CSS variable application
- Bridge objects are attached to `window` and accessible from any script context
- No iframe sandboxing prevents cross-frame communication

---

## 10. Tools Used

### Research tools summary

| Tool | Language | Location | Purpose |
|------|----------|----------|---------|
| `source_map_extract.py` | Python | `extraction/` | Parse webpack source maps, extract and categorize 802 original TypeScript files |
| `extract_css_boundaries.py` | Python | `extraction/` | Locate CSS theme blocks in binary by offset, verify boundaries with brace-depth tracking |
| `extract_mock_bridge.py` | Python | `extraction/` | Extract mock bridge initialization code, hardcoded tokens, and API URLs from JS bundles |
| `memory_scan.py` | Python | `extraction/` | Scan Root.exe process memory for base64url-encoded Bearer tokens using Windows APIs |
| `dpapi_decrypt.py` | Python | `extraction/` | Attempt DPAPI decryption of AuthToken file with multiple entropy values |
| `grpc_lib.py` | Python | `pentesting/` | Shared gRPC-web library: protobuf encoding/decoding, UUID handling, API call wrapper |
| `extract.js` | Node.js | `js/` | Pattern extraction across JS bundles: 36 security-relevant regex categories |
| `audit.js` | Node.js | `js/` | Deep security audit of JS bundles: 43 pattern categories with context extraction |
| `scan_bearer.py` | Python | `tokens/` | Scan Root and Chromium process memory for Bearer token patterns |
| `scan_chromium_storage.py` | Python | `tokens/` | Search DotNetBrowser Chromium storage (SQLite, LevelDB) for persisted tokens |
| `parse_authtoken.py` | Python | `tokens/` | Analyze Store/AuthToken file: protobuf parsing, DPAPI decryption, raw pattern search |
| `find_hardcoded_token.py` | Python | `tokens/` | Find hardcoded tokens in production WebRTC bundle via regex |
| `extract_token.py` | Python | `tokens/` | Extract clientToken values and long base64 strings from JS bundles |
| `test_auth.py` | Python | `recon/` | Test gRPC endpoint authentication enforcement (unauthenticated request probing) |
| `check_response_headers.py` | Python | `recon/` | Capture full HTTP response details for gRPC-web calls |
| `check_status.py` | Python | `recon/` | Verify token validity, community access, role service status |
| `quick_test.py` | Python | `recon/` | Rapid token validation against GetSelf endpoint |
| Phase scripts (1-7) | Python | `phases/` | Systematic endpoint testing across all 27 gRPC services |

### External dependencies

- `httpx` with HTTP/2 support for gRPC-web calls
- Python `ctypes` and `ctypes.wintypes` for Windows API memory access
- Node.js `fs` module for JavaScript bundle analysis
- PowerShell `System.Security.Cryptography.ProtectedData` for DPAPI attempts

---

## 11. Ethical Considerations

### Research boundaries

This reverse engineering was conducted for the purpose of building a client modification
framework (Uprooted) -- analogous to projects like Vencord for Discord or BetterDiscord.
The following principles guided the research:

**What we did:**

- Analyzed files already present on the local machine (the user's own installation)
- Read source maps shipped to every user as part of the standard installation
- Tested our own accounts against production endpoints
- Documented security findings for potential responsible disclosure
- Built tools that modify only the local client, not the server

**What we did NOT do:**

- Access other users' accounts, messages, or private data
- Exploit any vulnerability to gain unauthorized server-side access
- Attempt to escalate privileges beyond what our own accounts had
- Distribute extracted tokens, credentials, or sensitive data
- Modify, overload, or disrupt the production backend
- Bypass any server-side access controls

### Responsible disclosure

The security research (`research/docs/architecture/PENTEST_PLAN.md`) identified 21 security
findings across 5 severity levels. These findings were documented with full technical detail
and suggested remediations:

- 1 Critical (unauthenticated localhost services)
- 4 High (parameter injection, XSS, Alpine.js code execution)
- 10 Medium (missing CSP, DPAPI, DLL hijacking, update security)
- 5 Low (source maps in production, dev toolbar shipped, console.log leakage)

### No bug bounty program

At the time of research, Root Communications had no bug bounty program, no security page,
no responsible disclosure policy, and no `/trust` or `/security` page on their website.
The privacy policy analysis (`PRIVACY_ANALYSIS.md`) documented additional concerns about
undisclosed third-party data processors and session replay recording.

### Client modification ethics

Uprooted modifies only the local client installation and does not transmit any data to
third-party servers. It does not:

- Bypass server-side rate limiting or access controls
- Send modified or forged protocol messages
- Intercept or log other users' communications
- Disable security features that protect other users

---

## 12. Lessons Learned

### Key insights about reverse engineering .NET/Chromium hybrid apps

**1. Source maps are gold mines.**

A single `.js.map` file gave us more information than weeks of binary analysis would have.
The 13.5 MB source map contained the complete TypeScript source, all protobuf message schemas,
full bridge interface definitions, and hardcoded development credentials. Always check for
source maps first -- they are frequently shipped in production by accident.

**2. Hybrid apps have a wider attack surface than pure native or pure web apps.**

The boundary between .NET and Chromium creates multiple layers of IPC, each of which can be
instrumented independently. The JavaScript bridge objects are accessible to any code running
in the browser context, and the .NET host can be hooked via the CLR profiler API. Neither
side validates the other.

**3. DotNetBrowser is not Electron.**

Many reverse engineering guides assume Electron (Node.js backend, Chromium frontend, DevTools
accessible). DotNetBrowser is fundamentally different: the backend is .NET, the IPC is
proprietary binary, and Chrome DevTools Protocol is disabled. Techniques that work on Electron
apps (like connecting to the DevTools WebSocket) do not apply.

**4. Self-contained .NET apps bundle everything but protect nothing.**

Root.exe is a 576 MB self-contained deployment -- the entire runtime, all assemblies, and all
resources in one file. But .NET provides no built-in mechanism to prevent CLR profiler
injection or to verify that loaded assemblies are unmodified. The `DOTNET_STARTUP_HOOKS` and
`COR_PROFILER` environment variables are features of the runtime itself.

**5. Protobuf without proto files is solvable but tedious.**

Wire-format protobuf can be parsed generically (field number + wire type + value), but without
schema definitions you cannot distinguish between a string field and a nested message field --
both use wire type 2. Having the TypeScript client stubs from the source map saved enormous
effort compared to manual field-by-field mapping from network captures.

**6. Token security is only as strong as the weakest storage method.**

Root's tokens use a 96-byte cryptographic signature that prevents forgery. But the tokens are
stored in plaintext in process memory, embedded in production JavaScript bundles, and passed
through unencrypted bridge IPC. The cryptographic strength of the signature is irrelevant when
the token can be read directly from memory.

**7. Incognito mode does not protect tokens in memory.**

Running the embedded browser in incognito mode prevents localStorage and cookie persistence,
but tokens still exist in process memory and can be extracted by any process with read access
to the Root.exe address space.

**8. Binary analysis pays off for theme resources.**

While the source map gave us the protocol and bridge definitions, theme resources were only
discoverable through direct binary analysis. The CSS theme block at offset `0x12AE9676` and
the AXAML themes at offsets `0x19EED01F` through `0x19EF93D8` would not have been found
through JavaScript analysis alone.

**9. Permission models reveal architecture.**

The extracted permission enums (22 channel + 14 community permissions) told us more about
Root's feature set than any marketing documentation. Each permission implies a corresponding
feature: `channel_app_kick` tells us apps can be kicked from channels, `community_manage_apps`
tells us apps are a community-level concept, and `channel_make_message_public` tells us
messages have a public/private distinction.

**10. Systematic testing beats ad-hoc exploration.**

Testing all 163 gRPC methods in 7 organized phases, with a `TestTracker` class recording
verdicts for each call, produced a complete map of the implemented backend surface in a single
afternoon. Ad-hoc testing of interesting-looking endpoints would have missed the pattern that
most services return UNIMPLEMENTED (status 12) while a concentrated subset of 32 methods
across 8 services handle all actual functionality.

---

**Canonical for:** RE methodology narrative (source map extraction, binary analysis, protobuf schema discovery, port probing, token extraction techniques, bridge API discovery, CSS/theme extraction, DOM structure mapping), tools used, ethical considerations, lessons learned
**Not canonical for:** token format details → [ROOT_INTERNALS.md §5](ROOT_INTERNALS.md#5-authentication-and-tokens) | current Root architecture → [ROOT_INTERNALS.md](ROOT_INTERNALS.md) | security findings → [SECURITY_RESEARCH.md](SECURITY_RESEARCH.md) | gRPC protocol → [GRPC_PROTOCOL.md](GRPC_PROTOCOL.md)
*RE methodology reference. Last updated 2026-02-19.*
