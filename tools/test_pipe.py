"""Test connection to Root.exe's .NET diagnostics pipe."""
import ctypes
from ctypes import wintypes
import struct
import sys

kernel32 = ctypes.WinDLL('kernel32', use_last_error=True)

def try_pipe(name):
    """Try to open a named pipe."""
    h = kernel32.CreateFileW(
        name,
        0xC0000000,  # GENERIC_READ | GENERIC_WRITE
        0,
        None,
        3,  # OPEN_EXISTING
        0,
        None
    )
    invalid = ctypes.c_void_p(-1).value
    if h == invalid or h == 0xFFFFFFFF or h == -1:
        return None
    return h

# Get PID from command line or find Root.exe
import subprocess
result = subprocess.run(['tasklist', '/fi', 'imagename eq Root.exe', '/fo', 'csv', '/nh'],
                       capture_output=True, text=True)
pid = None
for line in result.stdout.strip().split('\n'):
    if 'Root.exe' in line:
        parts = line.strip('"').split('","')
        pid = int(parts[1])
        break

if not pid:
    print("Root.exe not running")
    sys.exit(1)

print(f"Root.exe PID: {pid}")

# Try the standard pipe name
pipe_name = f"\\\\.\\pipe\\dotnet-diagnostic-{pid}"
print(f"Trying: {pipe_name}")
h = try_pipe(pipe_name)

if h is None:
    # Try with -connect suffix (used by newer .NET versions)
    pipe_name2 = f"\\\\.\\pipe\\dotnet-diagnostic-{pid}-default-connect"
    print(f"Trying: {pipe_name2}")
    h = try_pipe(pipe_name2)
    if h is not None:
        pipe_name = pipe_name2

if h is None:
    print(f"Failed to open pipe. Error: {ctypes.get_last_error()}")
    sys.exit(1)

print(f"CONNECTED to {pipe_name}!")

# Send ProcessInfo2 command (newer format)
# Header format: magic[14] + size[2] + reserved[1] + commandset[1] + commandid[1] + reserved[1]
magic = b"DOTNET_IPC_V1\x00"  # 14 bytes
header_size = 20
cmd = bytearray(header_size)
cmd[0:14] = magic
struct.pack_into('<H', cmd, 14, header_size)  # size = 20
cmd[16] = 0x00  # reserved
cmd[17] = 0x04  # CommandSet: Process
cmd[18] = 0x04  # CommandId: ProcessInfo2
cmd[19] = 0x00  # reserved

written = wintypes.DWORD()
kernel32.WriteFile(h, bytes(cmd), len(cmd), ctypes.byref(written), None)
print(f"Sent ProcessInfo2 command ({written.value} bytes)")

# Read response
buf = ctypes.create_string_buffer(8192)
read = wintypes.DWORD()
if kernel32.ReadFile(h, buf, 8192, ctypes.byref(read), None):
    data = buf.raw[:read.value]
    print(f"Response: {read.value} bytes")
    if len(data) >= 20:
        resp_size = struct.unpack_from('<H', data, 14)[0]
        resp_cmdset = data[17]
        resp_cmdid = data[18]
        print(f"  Header: size={resp_size} cmdSet={resp_cmdset} cmdId={resp_cmdid}")
        payload = data[20:]
        if resp_cmdset == 0xFF:
            # Server response
            if len(payload) >= 4:
                hresult = struct.unpack_from('<I', payload, 0)[0]
                print(f"  HRESULT: 0x{hresult:08X}")
                if hresult == 0:
                    print("  SUCCESS! Parsing process info...")
                    # ProcessInfo2 response: pid (uint64), runtimeCookie (GUID), cmdLine (string), OS (string), arch (string)
                    offset = 4
                    if len(payload) > offset + 8:
                        rpid = struct.unpack_from('<Q', payload, offset)[0]
                        print(f"  PID: {rpid}")
                        offset += 8
                    if len(payload) > offset + 16:
                        # GUID is 16 bytes
                        guid_bytes = payload[offset:offset+16]
                        print(f"  Runtime cookie: {guid_bytes.hex()}")
                        offset += 16
                    # Strings are length-prefixed (uint32 char count, then UTF-16LE)
                    for name in ["CommandLine", "OS", "Arch", "RuntimeVersion"]:
                        if len(payload) > offset + 4:
                            str_len = struct.unpack_from('<I', payload, offset)[0]
                            offset += 4
                            if str_len > 0 and len(payload) >= offset + str_len * 2:
                                s = payload[offset:offset+str_len*2].decode('utf-16-le', errors='replace').rstrip('\x00')
                                print(f"  {name}: {s}")
                                offset += str_len * 2
                else:
                    print(f"  Error response!")
        print(f"  Payload hex: {payload[:80].hex()}")
else:
    print(f"Read failed: {ctypes.get_last_error()}")

kernel32.CloseHandle(h)
