"""
Binary patcher: Injects Uprooted preload into Root.exe's embedded WebRTC HTML.

Strategy:
  1. Compress the 450-byte embedded HTML to make room for a new <script> tag
  2. The new tag loads from ./rootapp-desktop-webrtc.js.map (repurposed)
  3. Replace the first N bytes of the ~77MB source map with our preload JS + CSS
  4. Pad remainder with a JS block comment to maintain size

Usage:
    python patch_binary.py          # Patch Root.exe
    python patch_binary.py --dry    # Dry run (show what would be patched)
    python patch_binary.py --undo   # Restore from backup
"""

import os
import sys
import shutil
import json

ROOT_EXE = os.path.join(os.environ.get("LOCALAPPDATA", ""), "Root", "current", "Root.exe")
BACKUP_EXE = ROOT_EXE + ".uprooted.bak"
DIST_DIR = os.path.join(os.path.dirname(os.path.dirname(os.path.abspath(__file__))), "dist")
SETTINGS_PATH = os.path.join(
    os.environ.get("LOCALAPPDATA", ""),
    "Root Communications", "Root", "profile", "default", "uprooted-settings.json"
)

# Known offsets (Root v0.9.86)
HTML_OFFSET = 0x12AE8A95
HTML_SIZE = 450  # bytes, ending with </html>

MAP_OFFSET = 0x12F08F73
MAP_MARKER = b'{"version":3'

# The original HTML (for verification - may have \r\n or \n line endings)
ORIGINAL_HTML_START = b"<!DOCTYPE html>"

def load_settings():
    """Load settings from disk, or return defaults."""
    defaults = {"enabled": True, "plugins": {}, "customCss": ""}
    try:
        if os.path.exists(SETTINGS_PATH):
            with open(SETTINGS_PATH, "r") as f:
                data = json.load(f)
                defaults.update(data)
    except Exception:
        pass
    return defaults

def build_patched_html(settings_json):
    """Build the compressed HTML with Uprooted script tags.

    Must be exactly HTML_SIZE bytes (padded with spaces before </head>).
    """
    # Settings inline script
    settings_tag = f'<script>window.__UPROOTED_SETTINGS__={settings_json}</script>'

    # Load preload from the repurposed .map file (executes before the module)
    preload_tag = '<script src=./rootapp-desktop-webrtc.js.map></script>'

    # Original tags (compressed: removed crossorigin, quotes on attrs)
    js_tag = '<script type=module src=./rootapp-desktop-webrtc.js></script>'
    css_tag = '<link rel=stylesheet href=./rootapp-desktop-webrtc.css>'

    # Build minimal HTML (no viewport meta, short title, no lang)
    head_content = (
        '<meta charset=UTF-8>'
        f'<title>Root WebRTC</title>'
        f'{settings_tag}'
        f'{preload_tag}'
        f'{js_tag}'
        f'{css_tag}'
    )

    html = f'<!DOCTYPE html><html><head>{head_content}</head><body><div id=root></div></body></html>'

    html_bytes = html.encode('utf-8')

    if len(html_bytes) > HTML_SIZE:
        # Try shorter title
        html = html.replace('<title>Root WebRTC</title>', '<title>R</title>')
        html_bytes = html.encode('utf-8')

    if len(html_bytes) > HTML_SIZE:
        print(f"ERROR: Patched HTML is {len(html_bytes)} bytes, max is {HTML_SIZE}")
        print(f"Content: {html}")
        sys.exit(1)

    # Pad with spaces before </html> to reach exact size
    padding_needed = HTML_SIZE - len(html_bytes)
    # Insert padding as spaces before </html>
    html = html[:-len('</html>')] + (' ' * padding_needed) + '</html>'
    html_bytes = html.encode('utf-8')

    assert len(html_bytes) == HTML_SIZE, f"Size mismatch: {len(html_bytes)} != {HTML_SIZE}"
    return html_bytes


def build_preload_payload():
    """Build the JS payload to replace the source map.

    Combines: uprooted-preload.js + inline CSS injection.
    Returns bytes.
    """
    js_path = os.path.join(DIST_DIR, "uprooted-preload.js")
    css_path = os.path.join(DIST_DIR, "uprooted.css")

    if not os.path.exists(js_path):
        print(f"ERROR: {js_path} not found. Run 'pnpm build' first.")
        sys.exit(1)

    with open(js_path, "r", encoding="utf-8") as f:
        js_code = f.read()

    # Remove the sourcemap comment from our JS
    js_code = js_code.replace('//# sourceMappingURL=uprooted-preload.js.map', '').rstrip()

    css_code = ""
    if os.path.exists(css_path):
        with open(css_path, "r", encoding="utf-8") as f:
            css_code = f.read()

    # Build combined payload:
    # 1. CSS injection (creates a <style> element)
    # 2. The preload JS bundle
    parts = []

    if css_code:
        # Escape the CSS for embedding in a JS string
        escaped_css = css_code.replace('\\', '\\\\').replace('`', '\\`').replace('${', '\\${')
        css_injector = (
            '(function(){'
            'var s=document.createElement("style");'
            's.id="uprooted-injected-css";'
            f's.textContent=`{escaped_css}`;'
            '(document.head||document.documentElement).appendChild(s);'
            '})();\n'
        )
        parts.append(css_injector)

    parts.append(js_code)

    payload = '\n'.join(parts)
    return payload.encode('utf-8')


def patch(dry_run=False):
    """Apply the Uprooted binary patch to Root.exe."""

    if not os.path.exists(ROOT_EXE):
        print(f"ERROR: Root.exe not found at {ROOT_EXE}")
        sys.exit(1)

    # Load settings
    settings = load_settings()
    settings_json = json.dumps(settings, separators=(',', ':'))

    # Build payloads
    patched_html = build_patched_html(settings_json)
    preload_payload = build_preload_payload()

    print(f"Patched HTML: {len(patched_html)} bytes")
    print(f"Preload payload: {len(preload_payload)} bytes ({len(preload_payload)/1024:.1f} KB)")
    print(f"HTML content:\n{patched_html.decode('utf-8')[:200]}...")
    print()

    # Read and verify the binary
    with open(ROOT_EXE, "rb") as f:
        # Verify HTML location
        f.seek(HTML_OFFSET)
        current_html = f.read(HTML_SIZE)

        if current_html.startswith(ORIGINAL_HTML_START):
            print(f"[OK] Found original HTML at 0x{HTML_OFFSET:08X}")
        elif current_html.startswith(b'<!DOCTYPE html><html>'):
            print(f"[OK] Found previously patched HTML at 0x{HTML_OFFSET:08X}")
        else:
            print(f"WARNING: Unexpected content at 0x{HTML_OFFSET:08X}")
            print(f"First 80 bytes: {current_html[:80]}")
            resp = input("Continue anyway? [y/N] ") if not dry_run else "n"
            if resp.lower() != 'y':
                sys.exit(1)

        # Verify source map location
        f.seek(MAP_OFFSET)
        current_map_start = f.read(len(MAP_MARKER))

        if current_map_start == MAP_MARKER:
            print(f"[OK] Found source map at 0x{MAP_OFFSET:08X}")
        elif current_map_start.startswith(b'(function()'):
            print(f"[OK] Found previously patched preload at 0x{MAP_OFFSET:08X}")
        elif current_map_start.startswith(b'"use strict"'):
            print(f"[OK] Found previously patched preload at 0x{MAP_OFFSET:08X}")
        else:
            print(f"WARNING: Unexpected content at 0x{MAP_OFFSET:08X}")
            print(f"First 80 bytes: {current_map_start + f.read(80 - len(MAP_MARKER))}")
            resp = input("Continue anyway? [y/N] ") if not dry_run else "n"
            if resp.lower() != 'y':
                sys.exit(1)

        # Find the actual end of the source map region
        # We need to know how much space we have
        f.seek(MAP_OFFSET)
        # Read 1MB at a time to find the first null byte
        map_region_size = 0
        chunk_size = 1024 * 1024
        found_end = False
        while not found_end:
            chunk = f.read(chunk_size)
            if not chunk:
                break
            null_pos = chunk.find(b'\x00')
            if null_pos != -1:
                map_region_size += null_pos
                found_end = True
            else:
                map_region_size += len(chunk)

        print(f"Source map region: {map_region_size} bytes ({map_region_size/1024/1024:.1f} MB)")

    if len(preload_payload) > map_region_size:
        print(f"ERROR: Preload ({len(preload_payload)} bytes) exceeds map region ({map_region_size} bytes)")
        sys.exit(1)

    print(f"Space usage: {len(preload_payload)}/{map_region_size} bytes ({len(preload_payload)*100/map_region_size:.1f}%)")

    if dry_run:
        print("\n[DRY RUN] No changes made.")
        return

    # Create backup
    if not os.path.exists(BACKUP_EXE):
        print(f"\nBacking up Root.exe to {BACKUP_EXE}...")
        shutil.copy2(ROOT_EXE, BACKUP_EXE)
        print("Backup created.")
    else:
        print(f"\nBackup already exists at {BACKUP_EXE}")

    # Apply patches
    print("\nPatching Root.exe...")

    with open(ROOT_EXE, "r+b") as f:
        # Patch HTML
        f.seek(HTML_OFFSET)
        f.write(patched_html)
        print(f"  Patched HTML at 0x{HTML_OFFSET:08X} ({len(patched_html)} bytes)")

        # Patch source map region with preload + padding
        f.seek(MAP_OFFSET)
        f.write(preload_payload)

        # Pad remainder with a JS block comment
        remaining = map_region_size - len(preload_payload)
        if remaining > 0:
            # Write /* ... */ comment filled with spaces
            pad_start = b'\n/*'
            pad_end = b'*/'
            pad_fill = remaining - len(pad_start) - len(pad_end)
            if pad_fill > 0:
                f.write(pad_start)
                f.write(b' ' * pad_fill)
                f.write(pad_end)
            else:
                f.write(b' ' * remaining)

        print(f"  Patched source map at 0x{MAP_OFFSET:08X} ({len(preload_payload)} bytes payload + {remaining} bytes padding)")

    print("\nDone! Restart Root to apply.")


def undo():
    """Restore Root.exe from backup."""
    if not os.path.exists(BACKUP_EXE):
        print("No backup found. Nothing to restore.")
        sys.exit(1)

    print(f"Restoring Root.exe from {BACKUP_EXE}...")
    shutil.copy2(BACKUP_EXE, ROOT_EXE)
    print("Restored. Restart Root to apply.")


if __name__ == "__main__":
    if "--undo" in sys.argv:
        undo()
    elif "--dry" in sys.argv:
        patch(dry_run=True)
    else:
        patch()
