# gRPC Library Reference

> **What this is:** Python library (`grpc_lib.py`) API reference — function signatures, UUID encoding, protobuf field types, making calls, service discovery, authentication, error handling, advanced patterns.
> **Read when:** Writing pentesting scripts that call Root's gRPC API; looking up function signatures; understanding `grpc_lib.py` usage.
> **Skip if:** You need the protocol wire format → [GRPC_PROTOCOL.md](GRPC_PROTOCOL.md). You need Root's internal architecture → [ROOT_INTERNALS.md](ROOT_INTERNALS.md).
> **Does NOT cover:** Protocol wire format details → [GRPC_PROTOCOL.md](GRPC_PROTOCOL.md) | Root internals → [ROOT_INTERNALS.md](ROOT_INTERNALS.md)

> **Related docs:** [gRPC Protocol](GRPC_PROTOCOL.md) | [Research Index](RESEARCH_INDEX.md) | [Security Research](SECURITY_RESEARCH.md) | [Reverse Engineering](REVERSE_ENGINEERING.md)

## Overview

`grpc_lib.py` (`research/pentesting/grpc_lib.py`, 654 lines) is a standalone Python library for making gRPC-web calls to Root's backend without `.proto` files. It implements raw protobuf encoding/decoding and gRPC-web framing from scratch. All phase scripts under `research/pentesting/phases/` import from this single library.

Capabilities: protobuf encoding/decoding primitives, Root's UUID binary format, gRPC-web frame construction and response parsing, permission builders, oneof UUID wrappers, token management, test tracking, and community discovery.

## Installation / Dependencies

Requires Python 3.8+ and one external package. Standard library: `struct`, `os`, `sys`, `time`.

```bash
pip install httpx[http2]
```

Create `LIVE_TOKEN.txt` in the same directory as `grpc_lib.py` with a valid Root bearer token. Loaded at import time via `load_token()` (`:15-18`).

## Quick Start

```python
import asyncio
from grpc_lib import call, create_client, verify_token, uuid_field, parse_pb, pf

async def main():
    async with create_client() as client:
        await verify_token(client)
        body = uuid_field(10, "002cf06b-0b01-8f01-bfe6-d4c70dc47966")
        status, message, frames = await call(client, "ChannelGroupGrpcService", "List", body)
        if status == '0' and frames:
            pf(parse_pb(frames[0]))

asyncio.run(main())
```

## Core Functions

### Encoding Functions

| Function | Signature | Wire Type | Line | Description |
|----------|-----------|-----------|------|-------------|
| `varint` | `(v: int) -> bytes` | -- | `:44` | Encode unsigned int as protobuf varint |
| `f_varint` | `(num: int, v: int) -> bytes` | 0 | `:74` | Tagged varint field |
| `f_bool` | `(num: int, v: bool) -> bytes` | 0 | `:78` | Tagged bool field (varint 0/1) |
| `f_str` | `(num: int, s: str) -> bytes` | 2 | `:61` | Tagged UTF-8 string field |
| `f_bytes` | `(num: int, b: bytes) -> bytes` | 2 | `:66` | Tagged bytes field |
| `f_msg` | `(num: int, data: bytes) -> bytes` | 2 | `:70` | Tagged embedded sub-message |
| `f_fixed64` | `(num: int, v: int) -> bytes` | 1 | `:53` | Tagged 64-bit fixed (little-endian) |
| `f_fixed32` | `(num: int, v: int) -> bytes` | 5 | `:57` | Tagged 32-bit fixed (little-endian) |

Wrapper type encoders -- encode Google well-known types (`{value = 1}`):

| Function | Signature | Line | Wraps |
|----------|-----------|------|-------|
| `string_value` | `(field_num: int, s: str) -> bytes` | `:94` | `google.protobuf.StringValue` |
| `bool_value` | `(field_num: int, v: bool) -> bytes` | `:98` | `google.protobuf.BoolValue` |
| `int32_value` | `(field_num: int, v: int) -> bytes` | `:102` | `google.protobuf.Int32Value` |

gRPC framing: `grpc_frame(data: bytes = b'') -> bytes` (`:106`) -- wraps data with flag byte `0x00` and 4-byte big-endian length prefix.

### Decoding Functions

| Function | Signature | Line | Description |
|----------|-----------|------|-------------|
| `decode_varint` | `(data: bytes, pos: int) -> (int, int)` | `:113` | Decode varint, returns `(value, new_pos)` |
| `parse_pb` | `(data: bytes, depth=0) -> list[tuple]` | `:126` | Parse binary protobuf into field tuples (recurses to depth 5) |
| `pf` | `(fields: list, indent=0) -> None` | `:168` | Pretty-print parsed fields to stdout |
| `extract_uuid` | `(fields: list, field_num: int) -> str\|None` | `:188` | Extract UUID string from fixed64 high/low sub-message |
| `extract_string` | `(fields: list, field_num: int) -> str\|None` | `:203` | Extract string at field number |
| `extract_varint` | `(fields: list, field_num: int) -> int\|None` | `:210` | Extract varint at field number |
| `extract_submsg` | `(fields: list, field_num: int) -> list\|None` | `:217` | Extract sub-message fields |
| `extract_all` | `(fields: list, field_num: int) -> list[tuple]` | `:224` | Extract all fields with given number (repeated fields) |

`parse_pb` returns tuples tagged by type: `'v'` (varint), `'f64'` (fixed64), `'f32'` (fixed32), `'s'` (string), `'b'` (raw bytes), `'m'` (sub-message with 4-element tuple: `(num, 'm', sub_fields, raw_bytes)`).

### gRPC Call Functions

**`call(client, svc, method, body=b'', label='', verbose=True, token=None)`** (`:239-322`)

Make a gRPC-web call. Parameters:
- `client` -- `httpx.AsyncClient` with HTTP/2 (from `create_client()`)
- `svc` -- Service name, e.g. `"AccessRuleGrpcService"` (the `root.` prefix is added automatically)
- `method` -- RPC method, e.g. `"Create"`, `"List"`
- `body` -- Protobuf-encoded request (concatenated field encoders)
- `label` -- Optional output label
- `verbose` -- Print decoded response fields (default `True`)
- `token` -- Override global TOKEN

Returns: `(status_code: str, status_message: str, data_frames: list[bytes])`
- `status_code` -- gRPC status as a string: `'0'` for OK, `'7'` for DENIED, etc.
- `status_message` -- Human-readable error from the server (may be empty string)
- `data_frames` -- List of raw protobuf response bodies (usually 0 or 1 entries)

The function automatically constructs the URL as `{BASE_URL}/root.{svc}/{method}`, sets `Content-Type: application/grpc-web+proto`, wraps the body in a gRPC frame, and parses both data frames and trailer frames from the response.

**`call_quiet(client, svc, method, body=b'', label='', token=None)`** (`:324-382`) -- same as `call()` but prints only the status line, no decoded field dump. Preferred for bulk testing where verbose output would be overwhelming.

## UUID Encoding

Root encodes UUIDs as a protobuf message with two `fixed64` fields representing the upper and lower 8 bytes:

```
message Uuid { fixed64 high = 1; fixed64 low = 2; }
```

For `002cf06b-05f1-8f01-bfe6-d4c70dc47966`: strip dashes, split at byte 8 into `002cf06b05f18f01` (high) and `bfe6d4c70dc47966` (low), parse each as big-endian 64-bit int, encode as little-endian `fixed64` on the wire.

- **`uuid_pb(uid_str) -> bytes`** (`:82`) -- raw UUID bytes (no outer tag)
- **`uuid_field(field_num, uid_str) -> bytes`** (`:90`) -- tagged UUID sub-message (most common)
- **`extract_uuid(fields, field_num) -> str|None`** (`:188`) -- decode UUID from parsed fields

## Protobuf Field Types

| Wire Type | Name | Encoders | Decoder Tag | Types |
|-----------|------|----------|-------------|-------|
| 0 | Varint | `f_varint`, `f_bool` | `'v'` | int32/64, uint32/64, bool, enum |
| 1 | 64-bit | `f_fixed64` | `'f64'` | fixed64, sfixed64, double |
| 2 | Length-delimited | `f_str`, `f_bytes`, `f_msg` | `'s'`/`'b'`/`'m'` | string, bytes, messages |
| 5 | 32-bit | `f_fixed32` | `'f32'` | fixed32, sfixed32, float |

## Making Calls

### Step-by-Step

1. **Build the message body** by concatenating field encoder calls. Field numbers must match the service's expected proto schema (discovered via reverse engineering).

2. **Call the endpoint** using `call()` or `call_quiet()`. Pass the service name and method.

3. **Check the status code** -- `'0'` means OK, anything else is an error or denial.

4. **Parse and extract** response fields from `data_frames[0]`.

```python
# Step 1: Build body
body = uuid_field(10, community_id) + f_str(12, "name") + f_varint(14, 1)

# Step 2: Call
gs, gm, df = await call(client, "ChannelGrpcService", "Create", body, "create")

# Steps 3-4: Check and extract
if str(gs) == '0' and df:
    fields = parse_pb(df[0])
    channel_id = extract_uuid(fields, 12)
    channel_name = extract_string(fields, 13)
```

## Service Discovery

Endpoints follow the URL pattern:

```
https://api.rootapp.com/root.<ServiceName>/<MethodName>
```

The `call()` function adds the `root.` prefix automatically if absent, so both forms work:

```python
await call(client, "ChannelGrpcService", "List", body)         # auto-prefixed
await call(client, "root.ChannelGrpcService", "List", body)    # explicit prefix
```

Known services discovered through research:

> For the complete service catalog with method counts and field conventions, see [GRPC_PROTOCOL.md §Service Catalog](GRPC_PROTOCOL.md#service-catalog).

## Authentication

> For token format and authentication protocol details, see [GRPC_PROTOCOL.md §Authentication](GRPC_PROTOCOL.md#authentication).

All requests carry an `Authorization: Bearer {token}` header. The library manages this automatically:

1. At import, `load_token()` (`:15-18`) reads `LIVE_TOKEN.txt` from the script directory
2. The token is stored in the module-level `TOKEN` variable
3. Every `call()` and `call_quiet()` invocation injects the header

Override the token per-call:

```python
await call(client, "UserGrpcService", "GetSelf", token="alt_token_here")
```

Validate a token before running tests:

```python
await verify_token(client)  # calls GetSelf; sys.exit(1) if invalid
```

## Error Handling

### gRPC Status Codes

The `STATUS_NAMES` dict (`:231-237`) maps numeric codes to short names:

> For the complete gRPC status code table, see [GRPC_PROTOCOL.md §Error Handling](GRPC_PROTOCOL.md#error-handling).

Network errors (DNS failure, connection refused, timeout) return `('ERR', error_message, [])`.

### Status Extraction

The gRPC status is extracted from two locations in priority order:

1. HTTP response header `grpc-status` (`:282-284`)
2. Trailer frame in the response body -- identified by flag byte `0x80` (`:295-300`)

If neither provides a status, the value falls back to `'N/A'`.

## Advanced Usage

**Nested messages** -- build inner first, wrap with `f_msg()`:

```python
overlay = bool_value(10, True) + bool_value(12, True)
body = (uuid_field(10, comm_id) +
        f_msg(11, channel_or_group_uuid(chan_id)) +
        f_msg(12, role_or_member_uuid(user_id, is_member=True)) +
        f_msg(13, overlay))
```

**Repeated fields** -- decode with `extract_all()`:

```python
items = extract_all(parse_pb(df[0]), 10)
for item in items:
    if item[1] == 'm':
        print(extract_uuid(item[2], 11), extract_string(item[2], 12))
```

**Oneof UUID wrappers** (`:459-467`):
- `channel_or_group_uuid(uid, is_group=False)` -- field 1=channel, field 2=group
- `role_or_member_uuid(uid, is_member=False)` -- field 1=role, field 2=member

**Permission builders** (`:430-449`): `all_channel_perms()` (plain bools), `all_channel_overlay_perms()` (BoolValue wrappers for access rule overlays), `all_community_perms()` (plain bools). Field maps at `:388-428`.

## Examples

### 1. List Channel Groups (from phase1_access_rules.py:198-220)

```python
async def list_channel_groups(community_id):
    async with create_client() as client:
        await verify_token(client)
        gs, gm, df = await call(client, "ChannelGroupGrpcService", "List",
                                uuid_field(10, community_id), "list groups")
        if str(gs) == '0' and df:
            for f in parse_pb(df[0]):
                if f[0] == 10 and f[1] == 'm':
                    print(extract_uuid(f[2], 11), extract_string(f[2], 12))
```

### 2. Create Access Rule with Full Permissions (from phase1_access_rules.py:96-121)

```python
async def create_access_rule(community_id, channel_id, user_id):
    async with create_client() as client:
        body = (uuid_field(10, community_id) +
                f_msg(11, channel_or_group_uuid(channel_id)) +
                f_msg(12, role_or_member_uuid(user_id, is_member=True)) +
                f_msg(13, all_channel_overlay_perms()))
        gs, gm, df = await call(client, "AccessRuleGrpcService", "Create", body)
        return str(gs) == '0'
```

### 3. Cross-Community File Search (from phase2_file_directory.py:78-95)

```python
async def search_files(community_id, query="*"):
    async with create_client() as client:
        body = uuid_field(10, community_id) + f_str(12, query)
        gs, gm, df = await call(client, "FileGrpcService", "SearchCommunity", body)
        if str(gs) == '7':
            print("Access denied (RBAC enforced)")
```

### 4. Test Tracking (from phase1_access_rules.py:28-36, phase2_file_directory.py:27-31)

```python
async def run_tests():
    tracker = TestTracker("My Phase")
    async with create_client() as client:
        await verify_token(client)
        pentest, target, _, _ = await discover_communities(client)
        gs, gm, df = await call(client, "DirectoryGrpcService", "ListAll",
                                uuid_field(10, target), "target ListAll")
        finding = "HIGH: Cross-community listing" if str(gs) == '0' and df else None
        tracker.record("T1", "Directory ListAll (target)", gs, "DENIED", finding)
        tracker.summary()
```

## Utility Functions

- **`create_client() -> httpx.AsyncClient`** (`:564`) -- HTTP/2 client with 15s timeout, use as async context manager
- **`verify_token(client) -> list[bytes]`** (`:568`) -- validates token via GetSelf, exits on failure
- **`banner(title: str) -> None`** (`:579`) -- prints visual separator
- **`discover_communities(client) -> (str, str, str|None, str|None)`** (`:585`) -- discovers community/channel/group IDs via notifications; returns `(pentest_comm, target_comm, target_group, target_channel)`

## Module-Level Constants

| Constant | Description |
|----------|-------------|
| `BASE_URL` | `https://api.rootapp.com` |
| `MAIN_UID` / `ALT_UID` | Test user UUIDs |
| `PENTEST_COMMUNITY` / `ROOT_COMMUNITY` | Known community UUIDs |
| `CHANNEL_PERMS` (`:388`) | Channel permission field numbers (10-31), 20 permissions |
| `COMMUNITY_PERMS` (`:412`) | Community permission field numbers (10-23), 14 permissions |
| `STATUS_NAMES` (`:231`) | gRPC status code-to-name map (codes 0-16) |

---

**Canonical for:** `grpc_lib.py` function signatures, encoding/decoding API, UUID helper functions, call patterns, error extraction, advanced usage examples, utility functions, module constants
**Not canonical for:** protocol wire format → [GRPC_PROTOCOL.md](GRPC_PROTOCOL.md) | service catalog → [GRPC_PROTOCOL.md §Service Catalog](GRPC_PROTOCOL.md#service-catalog) | status codes → [GRPC_PROTOCOL.md §Error Handling](GRPC_PROTOCOL.md#error-handling) | UUID binary format (canonical) → [GRPC_PROTOCOL.md §UUID Format](GRPC_PROTOCOL.md#uuid-format)
*gRPC library reference. Last updated 2026-02-19.*
