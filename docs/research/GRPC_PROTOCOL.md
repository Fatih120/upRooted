# gRPC-Web Protocol Reference

> **Related docs:** [Root Internals](ROOT_INTERNALS.md) | [gRPC Library Reference](GRPC_LIB_REFERENCE.md) | [Security Research](SECURITY_RESEARCH.md) | [Reverse Engineering](REVERSE_ENGINEERING.md)

Complete reference for the gRPC-web protocol as used by Root Communications
for client-server communication. All information was discovered through reverse
engineering of the Root desktop client and its JavaScript bundles.

---

## Table of Contents

1. [Overview](#overview)
2. [Protocol Mechanics](#protocol-mechanics)
3. [Protobuf Encoding](#protobuf-encoding)
4. [UUID Format](#uuid-format)
5. [Authentication](#authentication)
6. [Service Catalog](#service-catalog)
7. [Request/Response Patterns](#requestresponse-patterns)
8. [Key Service Details](#key-service-details)
9. [Permission System](#permission-system)
10. [Endpoint Discovery](#endpoint-discovery)
11. [Error Handling](#error-handling)
12. [Rate Limiting](#rate-limiting)
13. [Usage from Python](#usage-from-python)

---

## Overview

Root Communications uses **gRPC-web** as its primary client-server protocol.
Every action in the desktop app -- sending messages, joining communities,
managing roles, uploading files -- is a gRPC remote procedure call to
`api.rootapp.com`.

Standard gRPC requires full HTTP/2 with bidirectional streaming, which browsers
cannot support. gRPC-web adapts the protocol for HTTP/1.1 or HTTP/2 by encoding
trailers as a final frame in the response body, using `application/grpc-web+proto`
content type, and limiting to unary and server-streaming calls. Root's embedded
DotNetBrowser (Chromium) and the .NET host both communicate through gRPC-web.

Understanding gRPC-web lets Uprooted intercept and modify requests/responses,
implement custom API calls from plugins, and build bridge proxies between the
native and browser layers.

Source: `research/pentesting/grpc_lib.py` (655 lines).

---

## Protocol Mechanics

### Endpoint URL Structure

Every call is an HTTP POST to `https://api.rootapp.com/{package}.{Service}/{Method}`.
Root uses `root.` as the package prefix. The `v2` sub-package is used for the
second-generation message service.

```
POST https://api.rootapp.com/root.UserGrpcService/GetSelf
POST https://api.rootapp.com/root.CommunityGrpcService/Create
POST https://api.rootapp.com/root.v2.MessageGrpcService/Create
```

Source: `research/pentesting/grpc_lib.py`, lines 256-260.

### Frame Format

Both requests and responses use **length-prefixed frames**:

```
+--------+-------------------+-----------------------------+
| 1 byte |     4 bytes       |      N bytes                |
|  flag  |  length (BE u32)  |      payload                |
+--------+-------------------+-----------------------------+
```

- **Flag**: `0x00` for data frames, `0x80` for trailer frames.
- **Length**: Big-endian unsigned 32-bit byte count.
- **Payload**: Protobuf-encoded message (data) or UTF-8 status text (trailer).

Requests contain exactly one data frame. Empty requests: `\x00\x00\x00\x00\x00`.
Responses contain zero or more data frames followed by one trailer frame.

```python
def grpc_frame(data=b''):
    return b'\x00' + struct.pack('>I', len(data)) + data
```

Source: `research/pentesting/grpc_lib.py`, lines 106-108, 288-304.

### Trailer Frame

The trailer frame payload is UTF-8 with CR-LF-separated key-value pairs:

```
grpc-status:0\r\ngrpc-message:\r\n          # success
grpc-status:7\r\ngrpc-message:PERMISSION_DENIED\r\n  # error
```

Some responses also include `grpc-status` in HTTP headers. The parser checks
both locations, preferring trailer values.

### Required HTTP Headers

| Header          | Value                           | Purpose                      |
|-----------------|---------------------------------|------------------------------|
| Content-Type    | `application/grpc-web+proto`    | Identifies gRPC-web protocol |
| Accept          | `application/grpc-web+proto`    | Expected response format     |
| X-Grpc-Web      | `1`                             | gRPC-web marker              |
| Authorization   | `Bearer {token}`                | Authentication token         |
| User-Agent      | Standard browser UA string      | Identifies client            |

Source: `research/pentesting/grpc_lib.py`, lines 261-267.

### gRPC-web vs Standard gRPC

| Aspect       | Standard gRPC            | gRPC-web (Root)              |
|--------------|--------------------------|------------------------------|
| Transport    | HTTP/2 required          | HTTP/1.1 or HTTP/2           |
| Trailers     | HTTP/2 trailers          | Encoded in response body     |
| Content-Type | application/grpc         | application/grpc-web+proto   |
| Streaming    | Bidirectional            | Unary + server-streaming     |

---

## Protobuf Encoding

Root uses Protocol Buffers for serialization. Since no .proto files are
available, `grpc_lib.py` implements manual encoding/decoding using the wire
format directly.

### Wire Types

Each field is preceded by a tag: `tag = (field_number << 3) | wire_type`

| Wire Type | ID | Format             | Python Functions                         |
|-----------|----|--------------------|-----------------------------------------|
| Varint    | 0  | int32/64, bool     | `f_varint()`, `f_bool()`                |
| Fixed 64  | 1  | fixed64, double    | `f_fixed64()`                           |
| Length-del | 2  | string, bytes, msg | `f_str()`, `f_bytes()`, `f_msg()`       |
| Fixed 32  | 5  | fixed32, float     | `f_fixed32()`                           |

Varints use 7 data bits per byte with a continuation bit:

```python
def varint(v):
    r = bytearray()
    while v > 0x7f:
        r.append((v & 0x7f) | 0x80)
        v >>= 7
    r.append(v)
    return bytes(r)
```

Source: `research/pentesting/grpc_lib.py`, lines 43-80.

### Decoding

`parse_pb()` decodes arbitrary protobuf binary into field tuples using
heuristics to distinguish strings from sub-messages. Recursion capped at
depth 5. Returns tuples tagged as:

| Tag   | Meaning      | Format                                  |
|-------|--------------|-----------------------------------------|
| `'v'` | Varint       | `(field_num, 'v', int_value)`           |
| `'f64'`| Fixed 64    | `(field_num, 'f64', int_value)`         |
| `'f32'`| Fixed 32    | `(field_num, 'f32', int_value)`         |
| `'s'` | String       | `(field_num, 's', str_value)`           |
| `'b'` | Raw bytes    | `(field_num, 'b', bytes_value)`         |
| `'m'` | Sub-message  | `(field_num, 'm', [fields], raw_bytes)` |

Extraction helpers: `extract_uuid()`, `extract_string()`, `extract_varint()`,
`extract_submsg()`, `extract_all()` (for repeated fields).

Source: `research/pentesting/grpc_lib.py`, lines 112-226.

---

## UUID Format

Every entity identifier in Root is a UUID encoded as a sub-message with two
`fixed64` fields:

```
message RootUuid {
    fixed64 high = 1;  // Upper 8 bytes, stored as LE fixed64
    fixed64 low  = 2;  // Lower 8 bytes, stored as LE fixed64
}
```

For `002cf06b-05f1-8f01-bfe6-d4c70dc47966`:
- **high** (bytes 0-7): `0x002cf06b05f18f01`
- **low** (bytes 8-15): `0xbfe6d4c70dc47966`

```python
def uuid_pb(uid_str):
    h = uid_str.replace('-', '')
    b = bytes.fromhex(h)
    high = int.from_bytes(b[:8], 'big')
    low = int.from_bytes(b[8:], 'big')
    return f_fixed64(1, high) + f_fixed64(2, low)

def uuid_field(field_num, uid_str):
    return f_msg(field_num, uuid_pb(uid_str))
```

Source: `research/pentesting/grpc_lib.py`, lines 82-92.

### UUID Wrapper Types

All share the same binary layout but are used at different field positions:
`UserUuid`, `CommunityUuid`, `ChannelUuid`, `ChannelGroupUuid`,
`CommunityRoleUuid`, `DirectMessageUuid`, `MessageContainerUuid`,
`FileUuid`, `NotificationUuid`, `CommunityAppUuid`.

### Oneof UUID Wrappers

Some fields accept multiple entity types via `oneof`:

```python
# ChannelOrChannelGroupUuid: field 1=channel, field 2=group
def channel_or_group_uuid(uid_str, is_group=False):
    return f_msg(2 if is_group else 1, uuid_pb(uid_str))

# RoleOrMemberUuid: field 1=role, field 2=member
def role_or_member_uuid(uid_str, is_member=False):
    return f_msg(2 if is_member else 1, uuid_pb(uid_str))
```

Source: `research/pentesting/grpc_lib.py`, lines 452-467.

---

## Authentication

### Token Format

Root uses Bearer tokens -- base64url-encoded binary blobs (~180-200 chars)
beginning with `AC`. The first 16 decoded bytes contain the user's UUID. Tokens
are stored encrypted in `AppData\Local\Root Communications\Root\profile\default\Store\AuthToken`.

### Passing Tokens

Every request includes `Authorization: Bearer {token}`. No cookies, no
per-request rotation.

### Validation and Refresh

```python
# Validate: call GetSelf, expect status 0 (OK). Status 16 = expired.
gs, gm, df = await call(client, "UserGrpcService", "GetSelf")

# Refresh: call RefreshToken with empty body. Returns new token.
gs, gm, df = await call(client, "UserGrpcService", "RefreshToken", b'')
```

Testing revealed concurrent refresh calls can all succeed -- no single-use
locking on token refresh.

Source: `research/pentesting/grpc_lib.py`, lines 261-267, 568-577;
`research/pentesting/phases/phase7_remaining.py`, lines 222-244.

---

## Service Catalog

27 services, ~163 methods. Compiled from JS bundle analysis, endpoint probing,
and 7 pentest phases.

### Authentication and Users

| Service                    | Mtds | Description                              |
|----------------------------|------|------------------------------------------|
| `root.UserGrpcService`     | 31   | User management, auth, profiles, privacy |

Methods: GetSelf, Get, GetByUsername, GetExtendedUsersById,
GetExtendedUsersByUsername, GetConnectionBetween, GetUserNote, SetUserNote,
GetNewHubserverEndpoint, SetUsername, SetPassword, SetEmail, SetProfilePicture,
SetMaxOnlineStatus, SetDeviceOnlineStatus, SetDirectMessageInviteRequirement,
SetFriendshipInviteRequirement, SignUp, SignIn, SignOut, Delete, RefreshToken,
ForgotPassword, ForgotUsername, ResetPassword, Flag, BlockCreate, BlockDelete,
BlockList, FindByUsername, VerifyEmail

### Communities

| Service                                  | Mtds | Description                  |
|------------------------------------------|------|------------------------------|
| `root.CommunityGrpcService`              | 8    | CRUD, join, leave, settings  |
| `root.CommunityRoleGrpcService`          | 6    | Role CRUD                    |
| `root.CommunityMemberGrpcService`        | 6    | Member management            |
| `root.CommunityMemberRoleGrpcService`    | 4    | Role assignment              |
| `root.CommunityMemberBanGrpcService`     | 7    | Ban, kick, bulk ops          |
| `root.CommunityMemberInviteGrpcService`  | 6    | Direct invitations           |
| `root.CommunityLogGrpcService`           | 1    | Audit log                    |

CommunityGrpcService: Create, Get, List, ListMine, Edit, OwnerEdit, Delete, Leave.
CommunityRoleGrpcService: Create, Get, List, Edit, Delete, Move.
CommunityMemberGrpcService: Get, List, ListAll, Edit, Move, SetFavorite.
CommunityMemberRoleGrpcService: Add, Remove, List, SetPrimary.
CommunityMemberBanGrpcService: Create, CreateBulk, Delete, Get, List, Kick, KickBulk.
CommunityMemberInviteGrpcService: Create, Get, List, Delete, Respond, LinkJoin.
CommunityLogGrpcService: List.

### Channels

| Service                          | Mtds | Description              |
|----------------------------------|------|--------------------------|
| `root.ChannelGrpcService`        | 6    | Channel CRUD             |
| `root.ChannelGroupGrpcService`   | 6    | Channel group CRUD       |

Both: Create, Get, List, Edit, Delete, Move.

### Messaging

| Service                          | Mtds | Description              |
|----------------------------------|------|--------------------------|
| `root.DirectMessageGrpcService`  | 6    | DM conversations         |
| `root.v2.MessageGrpcService`     | 6    | Messages (v2 API)        |
| `root.MessageGrpcService`        | 6    | Messages (v1, legacy)    |

DirectMessageGrpcService: Create, Find, Get, List, AddMembers, RemoveUser.
v2.MessageGrpcService: Create, Get, List, Edit, Delete, Search.

### Access Control

| Service                          | Mtds | Description              |
|----------------------------------|------|--------------------------|
| `root.AccessRuleGrpcService`     | 7    | Permission overrides     |

Methods: Create, Edit, Delete, Update, ListByChannelOrChannelGroup,
ListByRoleOrMember, Get.

### Files and Directories

| Service                     | Mtds | Description              |
|-----------------------------|------|--------------------------|
| `root.FileGrpcService`      | 9    | File CRUD, search, DL    |
| `root.DirectoryGrpcService` | 8    | Directory management     |
| `root.AssetGrpcService`     | 3    | Asset/media management   |

FileGrpcService: Create, Get, List, Edit, Move, Delete, Search, SearchCommunity, Download.
DirectoryGrpcService: Create, Get, GetAll, List, ListAll, Edit, Move, Delete.
AssetGrpcService: Get, GetUploadTokenStatus, AppCreate.

### Apps

| Service                           | Mtds | Description              |
|-----------------------------------|------|--------------------------|
| `root.CommunityAppGrpcService`    | 14   | Community app management |
| `root.AppStoreGrpcService`        | 6    | App store browsing       |
| `root.CommunityAppLogGrpcService` | 3    | App audit logging        |
| `root.AppReviewGrpcService`       | 1+   | App reviews              |

CommunityAppGrpcService: Add, Remove, Get, List, GetSettings, SetDevSettings,
SetRating, Initialize, SetAppOrganizationAccess, ListChannelGroups, Create,
Edit, Delete, GetDevSettings.

### Social, WebRTC, Notifications, Misc

| Service                              | Mtds | Description              |
|--------------------------------------|------|--------------------------|
| `root.FriendshipInviteGrpcService`   | 2    | Friend requests          |
| `root.FriendshipGrpcService`         | 2    | Friendship management    |
| `root.FriendshipGroupGrpcService`    | 5    | Friend groups            |
| `root.LinkGrpcService`               | 6    | Invite links             |
| `root.WebRtcGrpcService`             | 12   | Voice/video sessions     |
| `root.NotificationGrpcService`       | 7    | Notifications            |
| `root.SupportGrpcService`            | 1    | Support tickets          |
| `root.ReactionGrpcService`           | 2+   | Message reactions        |
| `root.PollGrpcService`               | 2+   | Polls                    |
| `root.SuggestionGrpcService`         | 1+   | Suggestions              |
| `root.TaskTrackerGrpcService`        | 1+   | Task tracker             |
| `root.UserSettingsGrpcService`       | 2    | User settings            |

WebRtcGrpcService: GetIceInfo, List, SessionCreate, Kick, SetMuteAndDeafen,
SetMuteAndDeafenOther, Detach, Renegotiate, SetVideoStream, SetScreenShare,
Move, GetActiveSession.
NotificationGrpcService: List, CountUnviewed, SetViewed, SetAllViewed, Delete,
DeleteAll, Get.
LinkGrpcService: CommunityInviteLinkCreate, CommunityInviteLinkList,
CommunityInviteLinkListMine, CommunityInviteLinkGetInfo,
CommunityInviteLinkDelete, CommunityInviteLinkJoin.

Source: All phase files in `research/pentesting/phases/`;
`research/pentesting/exploits/run_exploit.py`, lines 148-204.

---

## Request/Response Patterns

### Unary Calls

Most calls are unary: one request frame, one response frame. The `call()`
function returns `(status_code_str, status_message, data_frames_list)`.

### Field Number Conventions

**Pattern A (f10+)** -- Most services:
`f10` = primary UUID (community_id), `f11` = secondary UUID, `f12` = tertiary, etc.

**Pattern B (f4+)** -- CommunityApp services:
`f4` = community_id, `f5` = app_id, `f6` = community_permission, `f7` = channel_permission.

Source: `research/pentesting/phases/phase3_community_apps.py`, line 7.

### Wrapper Types

Root uses Google protobuf wrappers for nullable fields. Each wraps the value
in a sub-message at inner field 1:

```python
string_value(field_num, s)  # google.protobuf.StringValue
bool_value(field_num, v)    # google.protobuf.BoolValue
int32_value(field_num, v)   # google.protobuf.Int32Value
```

Source: `research/pentesting/grpc_lib.py`, lines 94-104.

### Repeated Fields

Encoded by repeating the same field number:

```python
body = uuid_field(10, MAIN_UID) + uuid_field(10, ALT_UID)  # two user IDs
```

### Empty Requests

Services accepting empty bodies: GetSelf, CountUnviewed, DM List,
FriendshipGroup List, RefreshToken, BlockList.

---

## Key Service Details

### Messaging

**DirectMessageGrpcService/Create** -- f10 = repeated UserUuid (member_user_ids):
```python
body = uuid_field(10, ALT_UID)  # or both users for explicit
```

**DirectMessageGrpcService/Find** -- f10 = repeated UserUuid:
```python
body = uuid_field(10, MAIN_UID) + uuid_field(10, ALT_UID)
```

**v2.MessageGrpcService/Create** -- f10 = MessageContainerUuid, f12 = StringValue:
```python
container = f_msg(10, f_fixed64(1, high) + f_fixed64(2, low))
body = container + string_value(12, "Hello!")
```

Source: `research/pentesting/exploits/exploit_final.py`, lines 4-7, 155-258.

### Communities

**CommunityGrpcService/Create** -- f10 = string (name):
```python
body = f_str(10, "MyCommunity")
```

**CommunityGrpcService/Get** -- f10 = CommunityUuid.
**CommunityGrpcService/List** -- UNIMPLEMENTED; use ListMine or notification data.

Source: `research/pentesting/exploits/exploit_final.py`, lines 262-286.

### Roles

**CommunityRoleGrpcService/Create**:
```python
body = (uuid_field(10, community_id) + f_str(11, "role-name")
      + f_str(12, "#FF0000") + f_msg(15, all_community_perms())
      + f_msg(16, all_channel_perms()) + f_bool(17, True))
```

**CommunityMemberRoleGrpcService/Add** -- f10=community, f11=role, f12=user:
```python
body = uuid_field(10, comm_id) + uuid_field(11, role_id) + uuid_field(12, user_id)
```

Source: `research/pentesting/exploits/role_escalate.py`, lines 128-182.

### Channels

**ChannelGrpcService/Create**:
```python
body = (uuid_field(10, comm_id) + uuid_field(11, group_id)
      + f_str(12, "general") + f_varint(14, 1) + f_bool(15, True))
```

**ChannelGrpcService/List** -- f10=community, f11=channel_group. Response:
repeated Channel at f10, each with f12=channel_id, f13=name.

**ChannelGroupGrpcService/List** -- f10=community. Response: repeated at f10,
each with f11=group_id, f12=name.

Source: `research/pentesting/phases/phase1_access_rules.py`, lines 236-276;
`research/pentesting/grpc_lib.py`, lines 610-629.

### Files

**FileGrpcService/SearchCommunity** -- f10=community, f12=search string:
```python
body = uuid_field(10, community_id) + f_str(12, "*")
```

**FileGrpcService/Get** and **Download** -- f10=community, f11=container, f12=file.

Source: `research/pentesting/phases/phase2_file_directory.py`, lines 77-145.

### Users

**UserGrpcService/GetExtendedUsersByUsername** -- f10=repeated string (usernames).
**UserGrpcService/SetPassword** -- f10=old_password, f11=new_password.
**UserGrpcService/SignUp** -- f10=username, f11=password, f12=email.
**UserGrpcService/Flag** -- f5=user_id, f6=reason (enum), f7=property (enum).

Source: `research/pentesting/phases/phase5_notifications_users.py`, lines 87-226.

### WebRTC

**WebRtcGrpcService/GetIceInfo** -- Empty body. Returns STUN/TURN server info.
**WebRtcGrpcService/SessionCreate** -- f10=community, f11=container, f12=SDP string.

Source: `research/pentesting/phases/phase6_webrtc_social.py`, lines 42-84.

---

## Permission System

Two parallel permission structures, both protobuf messages with boolean fields.

### Community Permissions (fields 10-23)

```
f10: manage_community    f11: manage_roles     f12: manage_emojis
f13: manage_audit_log    f14: create_invite    f15: manage_invites
f16: create_ban          f17: manage_bans      f18: full_control
f19: kick                f20: change_my_nick   f21: change_other_nick
f22: create_channel_group f23: manage_apps
```

### Channel Permissions (fields 10-31, skipping 11)

```
f10: full_control        f12: view              f13: use_external_emoji
f14: create_message      f15: delete_msg_other  f16: manage_pinned
f17: view_msg_history    f18: create_attachment f19: create_mention
f20: create_reaction     f21: make_msg_public   f22: move_user_other
f23: voice_talk          f24: voice_mute_other  f25: voice_deafen_other
f26: voice_kick          f27: video_stream      f28: create_file
f29: manage_files        f30: view_file         f31: app_kick
```

### Permission Builders

```python
all_community_perms()         # All community bools set to True
all_channel_perms()           # All channel bools set to True
all_channel_overlay_perms()   # Channel perms as BoolValue wrappers (tri-state)
```

Access rules use BoolValue wrappers for three states: grant, deny, or inherit.

Source: `research/pentesting/grpc_lib.py`, lines 387-449.

---

## Endpoint Discovery

### Primary Endpoint

```
https://api.rootapp.com  (Cloudflare: 172.66.154.209)
```

Discovered via: (1) network traffic analysis of Root.exe outbound connections,
(2) hardcoded base URL in embedded Chromium JS bundles, (3) the
`window.__rootApiBaseUrl` global exposed by the native bridge.

### Other Endpoints

| Host                                         | Purpose                |
|----------------------------------------------|------------------------|
| `installer.rootapp.com`                      | Auto-update / Velopack |
| `o447951.ingest.sentry.io`                   | Telemetry (sub-apps)   |
| `o4509469920133120.ingest.us.sentry.io`      | Telemetry (WebRTC)     |
| `effectssdk.ai`                              | Video effects WASM     |
| `127.0.0.1:{dynamic}` (e.g., 49212)         | Local IPC (binary, not gRPC) |

Source: `research/docs/architecture/ARCHITECTURE.md`, lines 79-103.

---

## Error Handling

### gRPC Status Codes

Most commonly observed codes and their library constant names:

| Code | Name               | Constant     | Meaning                          |
|------|--------------------|--------------|----------------------------------|
| 0    | OK                 | `OK`         | Success                          |
| 3    | INVALID_ARGUMENT   | `BAD_ARG`    | Malformed request                |
| 5    | NOT_FOUND          | `NOT_FOUND`  | Entity does not exist            |
| 7    | PERMISSION_DENIED  | `DENIED`     | RBAC check failed                |
| 8    | RESOURCE_EXHAUSTED | `RESOURCE_EXHAUSTED` | Rate limited             |
| 12   | UNIMPLEMENTED      | `UNIMPL`     | Method stub, not implemented     |
| 13   | INTERNAL           | `INTERNAL`   | Server-side error                |
| 16   | UNAUTHENTICATED    | `UNAUTH`     | Invalid or expired token         |

All 17 standard gRPC codes (0-16) are defined in the library. Less commonly
seen: CANCELLED(1), UNKNOWN(2), DEADLINE(4), EXISTS(6), PRECOND(9),
ABORTED(10), OUT_OF_RANGE(11), UNAVAILABLE(14), DATA_LOSS(15).

Source: `research/pentesting/grpc_lib.py`, lines 231-237.

### Error Response Behavior

Errors are in the trailer frame. The body may or may not include a data frame.
Notable: some services return NOT_FOUND (5) instead of PERMISSION_DENIED (7)
for cross-community access, leaking entity existence information.

Source: `research/pentesting/phases/phase2_file_directory.py`, lines 130-145.

---

## Rate Limiting

Testing with 20 rapid concurrent calls per endpoint revealed inconsistent
enforcement:

| Endpoint                               | Rate Limited? | Notes                 |
|----------------------------------------|---------------|-----------------------|
| `UserGrpcService/GetSelf`              | No            | 20/20 OK in ~1.5s    |
| `NotificationGrpcService/List`         | No            | 20/20 OK              |
| `CommunityGrpcService/List`            | No            | 20/20 OK              |
| `SupportGrpcService/Create`            | No            | Mass ticket creation  |
| `UserGrpcService/RefreshToken`         | Partial       | Multiple concurrent   |
| `FriendshipInviteGrpcService/Create`   | No            | Mass friend requests  |
| `UserGrpcService/Flag`                 | No            | Mass user flagging    |

When rate limiting activates, the server returns gRPC status 8
(RESOURCE_EXHAUSTED). The general absence of limits means mass enumeration,
spam, and flooding attacks are feasible on most endpoints.

Source: `research/pentesting/phases/phase7_remaining.py`, lines 155-186.

---

## Usage from Python

See [GRPC_LIB_REFERENCE.md](GRPC_LIB_REFERENCE.md) for the complete API.

### Quick Start

```python
import asyncio
from grpc_lib import (call, create_client, verify_token, uuid_field,
    f_str, f_varint, f_msg, f_bool, string_value, parse_pb, pf, extract_uuid)

async def main():
    async with create_client() as c:
        await verify_token(c)
        gs, gm, df = await call(c, "CommunityGrpcService", "Get",
            uuid_field(10, community_id), label="get community")
        if str(gs) == '0' and df:
            pf(parse_pb(df[0]))

asyncio.run(main())
```

**Prerequisites**: Python 3.10+, `httpx[http2]`, valid token in
`research/pentesting/LIVE_TOKEN.txt`.

### Building Bodies and Parsing Responses

```python
# Encoding
body = f_str(10, "name")                                     # string
body = uuid_field(10, "002cf06b-05f1-8f01-bfe6-d4c70dc47966")   # UUID
body = uuid_field(10, comm) + f_str(12, "ch") + f_varint(14, 1) # multi-field
body = uuid_field(10, u1) + uuid_field(10, u2)               # repeated
body = string_value(12, "optional")                           # wrapper
body = uuid_field(10, comm) + f_msg(15, all_community_perms())  # nested

# Decoding
fields = parse_pb(df[0])
name = extract_string(fields, 10)
uid  = extract_uuid(fields, 5)
sub  = extract_submsg(fields, 12)
items = extract_all(fields, 10)   # repeated
```

Use `call_quiet()` for batch operations -- prints status only, no field dump.

Source: `research/pentesting/grpc_lib.py`, lines 239-382, 564-567.

---

## Appendix: Known Entity IDs

```python
MAIN_UID       = "002cf06b-05f1-8f01-bfe6-d4c70dc47966"  # Primary test user
ALT_UID        = "002cf099-1311-8801-a960-6210d2e7b665"  # Alt test account
ROOT_COMMUNITY = "00285411-3480-8302-a0e4-4da546be7be9"  # Root official community
EVERYONE_ROLE  = "0024c0c2-9400-800b-8000-000000000001"  # @everyone (universal)
```

Source: `research/pentesting/grpc_lib.py`, lines 25-39;
`research/pentesting/exploits/role_escalate.py`, lines 14-18.
