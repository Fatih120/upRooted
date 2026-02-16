"""Probe the DotNetBrowser IPC port to understand the protocol."""
import subprocess
import socket
import json
import sys
import time

def find_port():
    """Find the DotNetBrowser port from chromium.exe command line."""
    result = subprocess.run(
        ["powershell", "-ExecutionPolicy", "Bypass", "-Command",
         "Get-CimInstance Win32_Process -Filter \"Name='chromium.exe'\" | "
         "Where-Object { $_.CommandLine -match '--browsercore' -and $_.CommandLine -notmatch '--type=' } | "
         "ForEach-Object { if ($_.CommandLine -match '--port=(\\d+)') { $Matches[1] } }"],
        capture_output=True, text=True, timeout=10
    )
    port_str = result.stdout.strip()
    if port_str:
        return int(port_str)
    return None

def probe_raw(host, port):
    """Connect and see what the server sends first (if anything)."""
    print(f"\n=== Raw TCP probe to {host}:{port} ===")
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        s.settimeout(3)
        s.connect((host, port))
        print("Connected!")

        # See if server sends anything first
        try:
            data = s.recv(4096)
            print(f"Server sent {len(data)} bytes: {data[:200]}")
            print(f"Hex: {data[:64].hex()}")
        except socket.timeout:
            print("No data from server (timeout)")

        s.close()
    except Exception as e:
        print(f"Error: {e}")

def probe_http(host, port):
    """Try HTTP GET."""
    print(f"\n=== HTTP GET probe ===")
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        s.settimeout(3)
        s.connect((host, port))
        s.sendall(b"GET / HTTP/1.1\r\nHost: localhost\r\n\r\n")
        try:
            data = s.recv(4096)
            print(f"Response ({len(data)} bytes): {data[:500]}")
        except socket.timeout:
            print("No response (timeout)")
        s.close()
    except Exception as e:
        print(f"Error: {e}")

def probe_websocket(host, port):
    """Try WebSocket upgrade."""
    print(f"\n=== WebSocket upgrade probe ===")
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        s.settimeout(3)
        s.connect((host, port))
        s.sendall(
            b"GET /json HTTP/1.1\r\n"
            b"Host: localhost\r\n"
            b"Upgrade: websocket\r\n"
            b"Connection: Upgrade\r\n"
            b"Sec-WebSocket-Key: dGhlIHNhbXBsZSBub25jZQ==\r\n"
            b"Sec-WebSocket-Version: 13\r\n\r\n"
        )
        try:
            data = s.recv(4096)
            print(f"Response ({len(data)} bytes): {data[:500]}")
        except socket.timeout:
            print("No response (timeout)")
        s.close()
    except Exception as e:
        print(f"Error: {e}")

def probe_cdp(host, port):
    """Try Chrome DevTools Protocol endpoints."""
    print(f"\n=== CDP probe (GET /json/version) ===")
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        s.settimeout(3)
        s.connect((host, port))
        s.sendall(b"GET /json/version HTTP/1.1\r\nHost: localhost\r\n\r\n")
        try:
            data = s.recv(4096)
            print(f"Response ({len(data)} bytes): {data[:500]}")
        except socket.timeout:
            print("No response (timeout)")
        s.close()
    except Exception as e:
        print(f"Error: {e}")

    print(f"\n=== CDP probe (GET /json/list) ===")
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        s.settimeout(3)
        s.connect((host, port))
        s.sendall(b"GET /json/list HTTP/1.1\r\nHost: localhost\r\n\r\n")
        try:
            data = s.recv(4096)
            print(f"Response ({len(data)} bytes): {data[:500]}")
        except socket.timeout:
            print("No response (timeout)")
        s.close()
    except Exception as e:
        print(f"Error: {e}")

def probe_send_bytes(host, port):
    """Send some bytes and see response."""
    print(f"\n=== Binary probe (send 4 null bytes) ===")
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        s.settimeout(3)
        s.connect((host, port))
        s.sendall(b"\x00\x00\x00\x00")
        try:
            data = s.recv(4096)
            print(f"Response ({len(data)} bytes): {data[:200]}")
            print(f"Hex: {data[:64].hex()}")
        except socket.timeout:
            print("No response (timeout)")
        s.close()
    except Exception as e:
        print(f"Error: {e}")

if __name__ == "__main__":
    port = find_port()
    if not port:
        print("ERROR: Could not find DotNetBrowser port. Is Root running?")
        sys.exit(1)

    print(f"Found DotNetBrowser port: {port}")
    host = "127.0.0.1"

    probe_raw(host, port)
    probe_http(host, port)
    probe_cdp(host, port)
    probe_websocket(host, port)
    probe_send_bytes(host, port)
