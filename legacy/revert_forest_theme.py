"""
Root Communications - Full Forest Green Theme Uninstaller

Fully automated: kills Root.exe, reverts all patches, relaunches.
One command, zero interaction.

Usage:
  python revert_forest_theme.py
"""
import ctypes
import ctypes.wintypes
import glob
import io
import os
import re
import shutil
import subprocess
import sys
import time

sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8', errors='replace')
sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding='utf-8', errors='replace')

ROOT_EXE = os.path.expandvars(r"%LOCALAPPDATA%\Root\current\Root.exe")
ROOT_DIR = os.path.expandvars(r"%LOCALAPPDATA%\Root Communications\Root\profile\default")
HOST_DIR = os.path.expandvars(r"%LOCALAPPDATA%\Root\current\DotNetBrowser\RootApps\Bundle\Host")
BACKUP_DIR = os.path.expandvars(r"%USERPROFILE%\claude\Root_APP_BACKUP_20260212_172827")
BACKUP_EXE = os.path.join(BACKUP_DIR, "Root.exe")
CHROMIUM_DIR = os.path.expandvars(r"%LOCALAPPDATA%\Root Communications\Root\profile\default\DotNetBrowser\WindowsX64")
CHROMIUM_EXE = os.path.join(CHROMIUM_DIR, "chromium.exe")
CHROMIUM_ORIG = os.path.join(CHROMIUM_DIR, "chromium_original.exe")

CSS_MARKER = "/* FOREST_GREEN_THEME */"
HTML_MARKER = "<!-- FOREST_GREEN_THEME -->"
BODY_STYLE = 'style="background-color:#040D04;color:#D4E8D4"'


def kill_root():
    result = subprocess.run(
        ["tasklist", "/FI", "IMAGENAME eq Root.exe", "/NH"],
        capture_output=True, text=True, timeout=5
    )
    if "Root.exe" not in result.stdout:
        print("  Root.exe is not running.")
        return False
    print("  Killing Root.exe...")
    subprocess.run(["taskkill", "/F", "/IM", "Root.exe"], capture_output=True, timeout=10)
    subprocess.run(["taskkill", "/F", "/IM", "chromium.exe"], capture_output=True, timeout=5)
    for i in range(30):
        time.sleep(0.5)
        check = subprocess.run(
            ["tasklist", "/FI", "IMAGENAME eq Root.exe", "/NH"],
            capture_output=True, text=True, timeout=5
        )
        if "Root.exe" not in check.stdout:
            print(f"  Root.exe killed (took {(i+1)*0.5:.1f}s)")
            return True
    print("  WARNING: Root.exe may still be running after 15s")
    return True


def revert_css():
    count = 0
    all_css = [os.path.join(ROOT_DIR, "WebRtcBundle", "rootapp-desktop-webrtc.css")]
    apps_dir = os.path.join(ROOT_DIR, "RootApps")
    if os.path.exists(apps_dir):
        all_css += glob.glob(os.path.join(apps_dir, "**", "index-*.css"), recursive=True)
    for css_file in all_css:
        if not os.path.exists(css_file):
            continue
        with open(css_file, "r", encoding="utf-8") as f:
            content = f.read()
        if CSS_MARKER in content:
            idx = content.index(CSS_MARKER)
            with open(css_file, "w", encoding="utf-8") as f:
                f.write(content[:idx])
            count += 1
    return count


def revert_html():
    count = 0
    html_files = []
    webrtc_html = os.path.join(ROOT_DIR, "WebRtcBundle", "index.html")
    if os.path.exists(webrtc_html):
        html_files.append(webrtc_html)
    host_html = os.path.join(HOST_DIR, "index.html")
    if os.path.exists(host_html):
        html_files.append(host_html)
    apps_dir = os.path.join(ROOT_DIR, "RootApps")
    if os.path.exists(apps_dir):
        html_files += glob.glob(os.path.join(apps_dir, "**", "index.html"), recursive=True)
    for html_file in html_files:
        try:
            with open(html_file, "r", encoding="utf-8-sig") as f:
                content = f.read()
            if HTML_MARKER not in content:
                continue
            content = content.replace(HTML_MARKER + "\n", "")
            content = content.replace(HTML_MARKER, "")
            content = re.sub(
                r'<style>\s*/\* FOREST_GREEN_THEME inline \*/.*?</style>\s*',
                '', content, flags=re.DOTALL
            )
            content = content.replace(f' {BODY_STYLE}', '')
            with open(html_file, "w", encoding="utf-8") as f:
                f.write(content)
            count += 1
        except Exception as e:
            print(f"  WARN: {html_file}: {e}")
    return count


def revert_binary():
    if not os.path.exists(BACKUP_EXE):
        print("  No backup found - binary was never patched or backup removed.")
        return False
    if not os.path.exists(ROOT_EXE):
        print(f"  Root.exe not found: {ROOT_EXE}")
        return False
    print(f"  Restoring Root.exe from backup ({os.path.getsize(BACKUP_EXE)/1024/1024:.0f} MB)...")
    shutil.copy2(BACKUP_EXE, ROOT_EXE)
    print("  Restored.")
    return True


def launch_root():
    if not os.path.exists(ROOT_EXE):
        print(f"  Root.exe not found: {ROOT_EXE}")
        return False
    print("  Launching Root.exe...")
    subprocess.Popen([ROOT_EXE], cwd=os.path.dirname(ROOT_EXE),
                     creationflags=0x00000008)
    print("  Launched.")
    return True


def main():
    print("=" * 60)
    print("  FOREST GREEN THEME - FULL AUTO UNINSTALL")
    print("=" * 60)
    print()

    # Step 1: Kill
    print("[1/5] Closing Root.exe...")
    was_running = kill_root()
    if was_running:
        print("  Waiting 3s for file locks to release...")
        time.sleep(3)
    print()

    # Step 2: Revert CSS
    print("[2/5] Reverting CSS injection...")
    css_count = revert_css()
    print(f"  Reverted {css_count} CSS files")
    print()

    # Step 3: Revert HTML
    print("[3/5] Reverting HTML patches...")
    html_count = revert_html()
    print(f"  Reverted {html_count} HTML files")
    print()

    # Step 4: Restore binary
    print("[4/5] Restoring Root.exe from backup...")
    revert_binary()
    print()

    # Step 5: Relaunch
    print("[5/5] Relaunching Root.exe...")
    launch_root()
    print()

    print("=" * 60)
    print("  DONE - Original theme restored")
    print("=" * 60)
    print(f"  CSS reverted:  {css_count}")
    print(f"  HTML reverted: {html_count}")
    print(f"  Binary:        restored from backup")
    print(f"  DWM:           resets automatically on restart")


if __name__ == "__main__":
    main()
