"""Probe all .NET diagnostics IPC commands on Root.exe's pipe."""
import ctypes
from ctypes import wintypes
import struct
import subprocess
import sys

kernel32 = ctypes.WinDLL('kernel32', use_last_error=True)

def open_pipe(pid):
    pipe_name = f"\\\\.\\pipe\\dotnet-diagnostic-{pid}"
    h = kernel32.CreateFileW(
        pipe_name, 0xC0000000, 0, None, 3, 0, None
    )
    if h == ctypes.c_void_p(-1).value or h == 0xFFFFFFFF:
        return None
    return h

def send_command(pid, cmd_set, cmd_id, payload=b""):
    h = open_pipe(pid)
    if h is None:
        return None, f"pipe open failed: {ctypes.get_last_error()}"

    size = 20 + len(payload)
    header = bytearray(20)
    header[0:14] = b"DOTNET_IPC_V1\x00"
    struct.pack_into('<H', header, 14, size)
    header[16] = 0x00
    header[17] = cmd_set
    header[18] = cmd_id
    header[19] = 0x00

    msg = bytes(header) + payload
    written = wintypes.DWORD()
    if not kernel32.WriteFile(h, msg, len(msg), ctypes.byref(written), None):
        kernel32.CloseHandle(h)
        return None, f"write failed: {ctypes.get_last_error()}"

    buf = ctypes.create_string_buffer(8192)
    read = wintypes.DWORD()
    if not kernel32.ReadFile(h, buf, 8192, ctypes.byref(read), None):
        kernel32.CloseHandle(h)
        return None, f"read failed: {ctypes.get_last_error()}"

    kernel32.CloseHandle(h)
    return buf.raw[:read.value], None

def parse_response(data):
    if len(data) < 20:
        return f"short response ({len(data)} bytes)"

    resp_size = struct.unpack_from('<H', data, 14)[0]
    cmd_set = data[17]
    cmd_id = data[18]
    payload = data[20:]

    result = f"size={resp_size} cmdSet=0x{cmd_set:02X} cmdId=0x{cmd_id:02X}"
    if cmd_set == 0xFF and len(payload) >= 4:
        hr = struct.unpack_from('<I', payload, 0)[0]
        result += f" HRESULT=0x{hr:08X}"
        if hr == 0:
            result += " SUCCESS"
        result += f" payload[4:]={payload[4:min(64,len(payload))].hex()}"
    else:
        result += f" payload={payload[:64].hex()}"

    return result

# Get Root PID
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
print()

# Probe all known command sets and IDs
commands = [
    # EventPipe (0x02)
    (0x02, 0x01, "EventPipe.StopTracing", b""),
    (0x02, 0x02, "EventPipe.CollectTracing", b""),
    (0x02, 0x03, "EventPipe.CollectTracing2", b""),

    # Profiler (0x03)
    (0x03, 0x01, "Profiler.AttachProfiler", b""),
    (0x03, 0x02, "Profiler.StartupProfiler", b""),

    # Process (0x04)
    (0x04, 0x00, "Process.ProcessInfo", b""),
    (0x04, 0x01, "Process.ResumeRuntime", b""),
    (0x04, 0x02, "Process.ProcessEnvironment", b""),
    (0x04, 0x03, "Process.SetEnvironmentVariable", b""),
    (0x04, 0x04, "Process.ProcessInfo2", b""),
    (0x04, 0x05, "Process.EnablePerfMap", b""),
    (0x04, 0x06, "Process.DisablePerfMap", b""),
    (0x04, 0x07, "Process.ApplyStartupHook", b""),

    # Dump (0x01)
    (0x01, 0x01, "Dump.GenerateCoreDump", b""),
    (0x01, 0x02, "Dump.GenerateCoreDump2", b""),
]

for cmd_set, cmd_id, name, payload in commands:
    data, err = send_command(pid, cmd_set, cmd_id, payload)
    if err:
        print(f"  {name:40s} -> ERROR: {err}")
    elif data:
        result = parse_response(data)
        print(f"  {name:40s} -> {result}")
    print()
