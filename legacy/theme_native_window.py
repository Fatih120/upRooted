"""
Apply forest green theme to Root.exe native window chrome via Windows DWM API.

Sets title bar color, border color, text color, and forces dark mode.
Must be run AFTER Root.exe is launched (non-persistent, resets on restart).

Requires: Windows 11 (Build 22000+), Python 3 with ctypes.
"""
import ctypes
import ctypes.wintypes
import sys
import time

# ── DWM constants ─────────────────────────────────────────────────────
DWMWA_USE_IMMERSIVE_DARK_MODE = 20
DWMWA_BORDER_COLOR = 34
DWMWA_CAPTION_COLOR = 35
DWMWA_TEXT_COLOR = 36

# ── Forest green palette (COLORREF = 0x00BBGGRR) ─────────────────────
def hex_to_colorref(hex_color):
    """Convert #RRGGBB to COLORREF (0x00BBGGRR)."""
    hex_color = hex_color.lstrip('#')
    r = int(hex_color[0:2], 16)
    g = int(hex_color[2:4], 16)
    b = int(hex_color[4:6], 16)
    return (b << 16) | (g << 8) | r

BORDER_COLOR  = hex_to_colorref("#14291A")  # dark green border
CAPTION_COLOR = hex_to_colorref("#040D04")  # near-black green title bar
TEXT_COLOR    = hex_to_colorref("#D4E8D4")  # green-white title text

# ── Win32 API setup ───────────────────────────────────────────────────
dwmapi = ctypes.windll.dwmapi
user32 = ctypes.windll.user32
kernel32 = ctypes.windll.kernel32
psapi = ctypes.windll.psapi

EnumWindows = user32.EnumWindows
GetWindowThreadProcessId = user32.GetWindowThreadProcessId
IsWindowVisible = user32.IsWindowVisible
GetWindowTextW = user32.GetWindowTextW
GetWindowTextLengthW = user32.GetWindowTextLengthW

OpenProcess = kernel32.OpenProcess
CloseHandle = kernel32.CloseHandle
GetModuleFileNameExW = psapi.GetModuleFileNameExW

PROCESS_QUERY_INFORMATION = 0x0400
PROCESS_VM_READ = 0x0010

WNDENUMPROC = ctypes.WINFUNCTYPE(ctypes.wintypes.BOOL, ctypes.wintypes.HWND, ctypes.wintypes.LPARAM)

def find_root_windows():
    """Find all visible Root.exe top-level windows."""
    results = []

    def enum_callback(hwnd, lparam):
        if not IsWindowVisible(hwnd):
            return True

        # Get process ID
        pid = ctypes.wintypes.DWORD()
        GetWindowThreadProcessId(hwnd, ctypes.byref(pid))

        # Open process to check executable name
        hproc = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, False, pid.value)
        if hproc:
            buf = ctypes.create_unicode_buffer(512)
            GetModuleFileNameExW(hproc, None, buf, 512)
            CloseHandle(hproc)
            exe_path = buf.value.lower()
            if exe_path.endswith("root.exe"):
                # Get window title for display
                length = GetWindowTextLengthW(hwnd)
                title_buf = ctypes.create_unicode_buffer(length + 1)
                GetWindowTextW(hwnd, title_buf, length + 1)
                results.append((hwnd, pid.value, title_buf.value, buf.value))
        return True

    EnumWindows(WNDENUMPROC(enum_callback), 0)
    return results

def apply_dwm_theme(hwnd):
    """Apply forest green DWM attributes to a window handle."""
    # Force immersive dark mode
    dark_mode = ctypes.c_int(1)
    hr = dwmapi.DwmSetWindowAttribute(
        hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE,
        ctypes.byref(dark_mode), ctypes.sizeof(dark_mode)
    )
    results = [f"  Dark mode: {'OK' if hr == 0 else f'FAILED (0x{hr & 0xFFFFFFFF:08X})'}"]

    # Border color
    border = ctypes.c_uint32(BORDER_COLOR)
    hr = dwmapi.DwmSetWindowAttribute(
        hwnd, DWMWA_BORDER_COLOR,
        ctypes.byref(border), ctypes.sizeof(border)
    )
    results.append(f"  Border color (#14291A): {'OK' if hr == 0 else f'FAILED (0x{hr & 0xFFFFFFFF:08X})'}")

    # Caption (title bar) color
    caption = ctypes.c_uint32(CAPTION_COLOR)
    hr = dwmapi.DwmSetWindowAttribute(
        hwnd, DWMWA_CAPTION_COLOR,
        ctypes.byref(caption), ctypes.sizeof(caption)
    )
    results.append(f"  Caption color (#040D04): {'OK' if hr == 0 else f'FAILED (0x{hr & 0xFFFFFFFF:08X})'}")

    # Text color
    text = ctypes.c_uint32(TEXT_COLOR)
    hr = dwmapi.DwmSetWindowAttribute(
        hwnd, DWMWA_TEXT_COLOR,
        ctypes.byref(text), ctypes.sizeof(text)
    )
    results.append(f"  Text color (#D4E8D4): {'OK' if hr == 0 else f'FAILED (0x{hr & 0xFFFFFFFF:08X})'}")

    return results

def main():
    print("Forest Green Theme — DWM Window Chrome")
    print("=" * 45)

    # Find Root.exe windows
    windows = find_root_windows()

    if not windows:
        print("\nRoot.exe window not found.")
        print("Make sure Root is running, then try again.")

        if "--wait" in sys.argv:
            print("\nWaiting for Root.exe to start...")
            for attempt in range(60):  # Wait up to 60 seconds
                time.sleep(1)
                windows = find_root_windows()
                if windows:
                    break
                if attempt % 10 == 9:
                    print(f"  Still waiting... ({attempt + 1}s)")

        if not windows:
            sys.exit(1)

    print(f"\nFound {len(windows)} Root.exe window(s):\n")

    for hwnd, pid, title, exe in windows:
        print(f"Window: \"{title}\" (PID {pid}, HWND 0x{hwnd:08X})")
        print(f"  EXE: {exe}")
        results = apply_dwm_theme(hwnd)
        for r in results:
            print(r)
        print()

    print("Done. Title bar + border should now be forest green.")
    print("Note: This resets when Root.exe restarts — re-run this script after each launch.")

if __name__ == "__main__":
    main()
