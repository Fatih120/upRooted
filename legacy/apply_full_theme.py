"""
Root Communications - Forest Green Theme v2 (Complete Coverage)

Phases A-H: patches CSS files, HTML, JS bundles, binary CSS/BAML/ARGB, and IL BGRA.
Kills Root.exe, restores from backup, applies all patches, relaunches, themes DWM.

Phase A: CSS hex strings in binary (outside theme block)
Phase B: BAML Fluent theme colors in binary
Phase C: Binary ARGB/BGRA patterns
Phase D: CSS var() wrapper removal (server override bypass)
Phase E: JS bundle hex colors (theme objects, inline styles) — NEW in v2
Phase F: Canvas rgb/rgba colors (WebRTC overlays) — NEW in v2
Phase G: CSS injection expanded to ALL *.css files — NEW in v2
Phase H: IL BGRA constants (Avalonia sidebar/nav) — NEW in v2

Usage:
  python apply_full_theme.py
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

# ── Paths ─────────────────────────────────────────────────────────────
ROOT_EXE = os.path.expandvars(r"%LOCALAPPDATA%\Root\current\Root.exe")
ROOT_DIR = os.path.expandvars(r"%LOCALAPPDATA%\Root Communications\Root\profile\default")
HOST_DIR = os.path.expandvars(r"%LOCALAPPDATA%\Root\current\DotNetBrowser\RootApps\Bundle\Host")
BACKUP_DIR = os.path.expandvars(r"%USERPROFILE%\claude\Root_APP_BACKUP_20260212_172827")
BACKUP_EXE = os.path.join(BACKUP_DIR, "Root.exe")

# ── Full forest green CSS ────────────────────────────────────────────
THEME_CSS = r"""
:root{
--rootsdk-brand-primary:#1B8A4A !important;--rootsdk-brand-secondary:#4ADE80 !important;
--rootsdk-brand-tertiary:#22C55E !important;--rootsdk-text-primary:#D4E8D4 !important;
--rootsdk-text-secondary:#8FA88F !important;--rootsdk-text-tertiary:#5A725A !important;
--rootsdk-text-white:#E0F0E0 !important;--rootsdk-background-primary:#040D04 !important;
--rootsdk-background-secondary:#081408 !important;--rootsdk-background-tertiary:#020802 !important;
--rootsdk-input:#051005 !important;--rootsdk-border:#14291A !important;
--rootsdk-highlight-light:#4ADE8008 !important;--rootsdk-highlight-normal:#4ADE8015 !important;
--rootsdk-highlight-strong:#4ADE8028 !important;--rootsdk-info:#4ADE80 !important;
--rootsdk-warning:#CA8A04 !important;--rootsdk-error:#DC2626 !important;
--rootsdk-muted:#2D5A3A !important;--rootsdk-link:#34D399 !important;
--rootsdk-background-blur:#000000CC !important;--rootsdk-success:#16A34A !important;
--rootsdk-surface-primary:#040D04 !important;--rootsdk-surface-secondary:#081408 !important;
--rootsdk-surface-tertiary:#020802 !important;
}
:root,:root [data-theme=dark],:root [data-theme=pure-dark],:root [data-theme=light]{
--color-brand-primary:#1B8A4A !important;--color-brand-secondary:#4ADE80 !important;
--color-brand-tertiary:#22C55E !important;--color-text-primary:#D4E8D4 !important;
--color-text-secondary:#8FA88F !important;--color-text-tertiary:#5A725A !important;
--color-text-white:#E0F0E0 !important;--color-background-primary:#040D04 !important;
--color-background-secondary:#081408 !important;--color-background-tertiary:#020802 !important;
--color-input:#051005 !important;--color-border:#14291A !important;
--color-highlight-light:#4ADE8008 !important;--color-highlight-normal:#4ADE8015 !important;
--color-highlight-strong:#4ADE8028 !important;--color-info:#4ADE80 !important;
--color-warning:#CA8A04 !important;--color-error:#DC2626 !important;
--color-muted:#2D5A3A !important;--color-link:#34D399 !important;
--color-background-blur:#000000CC !important;--color-success:#16A34A !important;
}
*{scrollbar-width:thin !important;scrollbar-color:#14291A #040D04 !important}
::-webkit-scrollbar{width:6px !important;height:6px !important}
::-webkit-scrollbar-track{background:#040D04 !important}
::-webkit-scrollbar-thumb{background:#1B5A2E !important;border-radius:3px !important}
::-webkit-scrollbar-thumb:hover{background:#22C55E !important}
::-webkit-scrollbar-corner{background:#020802 !important}
::selection{background:#1B8A4A !important;color:#E0F0E0 !important}
html,body,#root,#app,.app,div[class*="app"]{background-color:#040D04 !important;color:#D4E8D4 !important}
.p-checkbox .p-checkbox-box{border-color:#2D5A3A !important}
.p-checkbox.p-highlight .p-checkbox-box{border-color:#1B8A4A !important;background-color:#1B8A4A22 !important}
.p-checkbox .p-checkbox-box svg path{fill:#4ADE80 !important}
.p-dropdown-panel,.p-multiselect-panel{background-color:#081408 !important}
.p-dropdown-item:hover,.p-multiselect-item:hover,.p-dropdown-panel .p-focus{background-color:#0D1F0D !important}
.p-inputtext{background-color:#051005 !important;border-color:#14291A !important;color:#D4E8D4 !important}
.p-inputtext:enabled:focus,.p-inputtext:focus{border-color:#1B8A4A !important}
.p-inputtext::placeholder{color:#5A725A !important}
.p-dropdown{border-color:#14291A !important}
.p-dropdown-label,.p-dropdown-trigger{color:#D4E8D4 !important}
.p-button:not(.p-button-link).submit-button{background-color:#1B8A4A !important;color:#020802 !important}
.p-dialog,.dialog-container.p-dialog{background:#040D04 !important}
.p-dialog .p-dialog-header,.dialog-container .p-dialog-header{background-color:#040D04 !important;color:#D4E8D4 !important}
.p-dialog .p-dialog-content,.dialog-container .p-dialog-content{background-color:#040D04 !important;color:#D4E8D4 !important}
.p-card{background-color:#081408 !important;color:#D4E8D4 !important}
.p-skeleton{background:#0D1F0D !important}
.call{background-color:#020802 !important}
.call .panel-group{background-color:#040D04 !important}
.debug-panel{background-color:#020802 !important;color:#D4E8D4 !important}
.pending-overlay{background-color:#000000CC !important}
.username{background-color:#000000CC !important;color:#D4E8D4 !important}
.profile{background-color:#081408 !important}
.bg-white{background-color:#081408 !important}
.bg-black{background-color:#020802 !important}
.bg-gray-50,.bg-slate-50,.bg-zinc-50,.bg-neutral-50{background-color:#0A1A0A !important}
.bg-gray-100,.bg-slate-100,.bg-zinc-100,.bg-neutral-100{background-color:#081408 !important}
.bg-gray-200,.bg-slate-200,.bg-zinc-200,.bg-neutral-200{background-color:#0D1F0D !important}
.bg-gray-700,.bg-slate-700,.bg-zinc-700,.bg-neutral-700{background-color:#0D1F0D !important}
.bg-gray-800,.bg-slate-800,.bg-zinc-800,.bg-neutral-800{background-color:#081408 !important}
.bg-gray-900,.bg-slate-900,.bg-zinc-900,.bg-neutral-900{background-color:#040D04 !important}
.text-white{color:#D4E8D4 !important}
.text-gray-400,.text-slate-400{color:#5A725A !important}
.text-gray-300,.text-slate-300{color:#8FA88F !important}
.border-gray-200,.border-slate-200{border-color:#14291A !important}
.border-gray-700,.border-slate-700{border-color:#14291A !important}
.bg-blue-500,.bg-blue-600,.bg-indigo-500,.bg-indigo-600{background-color:#1B8A4A !important}
.text-blue-500,.text-blue-600,.text-indigo-500,.text-indigo-600{color:#34D399 !important}
.text-blue-400,.text-indigo-400{color:#4ADE80 !important}
.border-blue-500,.border-indigo-500{border-color:#1B8A4A !important}
svg path[fill="#3B6AF8"],svg path[fill="#3b6af8"]{fill:#1B8A4A !important}
svg path[fill="#A8FF5D"],svg path[fill="#a8ff5d"]{fill:#4ADE80 !important}
svg path[fill="#F2F2F2"],svg path[fill="#f2f2f2"]{fill:#D4E8D4 !important}
a{color:#34D399 !important}a:hover{color:#4ADE80 !important}a:visited{color:#22C55E !important}
"""

# ══════════════════════════════════════════════════════════════════════
#  UTILITIES
# ══════════════════════════════════════════════════════════════════════
def kill_root():
    result = subprocess.run(["tasklist", "/FI", "IMAGENAME eq Root.exe", "/NH"],
                            capture_output=True, text=True, timeout=5)
    if "Root.exe" not in result.stdout:
        print("  Root.exe not running.")
        return False
    print("  Killing Root.exe...")
    subprocess.run(["taskkill", "/F", "/IM", "Root.exe"], capture_output=True, timeout=10)
    # Also kill chromium children
    subprocess.run(["taskkill", "/F", "/IM", "chromium.exe"], capture_output=True, timeout=5)
    for i in range(30):
        time.sleep(0.5)
        check = subprocess.run(["tasklist", "/FI", "IMAGENAME eq Root.exe", "/NH"],
                               capture_output=True, text=True, timeout=5)
        if "Root.exe" not in check.stdout:
            print(f"  Killed ({(i+1)*0.5:.1f}s)")
            return True
    return True

dwmapi = ctypes.windll.dwmapi
user32 = ctypes.windll.user32
kernel32 = ctypes.windll.kernel32
psapi = ctypes.windll.psapi
WNDENUMPROC = ctypes.WINFUNCTYPE(ctypes.wintypes.BOOL, ctypes.wintypes.HWND, ctypes.wintypes.LPARAM)

def find_root_windows():
    results = []
    def cb(hwnd, lparam):
        if not user32.IsWindowVisible(hwnd):
            return True
        pid = ctypes.wintypes.DWORD()
        user32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
        hproc = kernel32.OpenProcess(0x0410, False, pid.value)
        if hproc:
            buf = ctypes.create_unicode_buffer(512)
            psapi.GetModuleFileNameExW(hproc, None, buf, 512)
            kernel32.CloseHandle(hproc)
            if buf.value.lower().endswith("root.exe"):
                results.append((hwnd, pid.value))
        return True
    user32.EnumWindows(WNDENUMPROC(cb), 0)
    return results

def hex_to_colorref(h):
    h = h.lstrip('#')
    r, g, b = int(h[0:2],16), int(h[2:4],16), int(h[4:6],16)
    return (b << 16) | (g << 8) | r


# ══════════════════════════════════════════════════════════════════════
#  STEP 2: CSS FILE INJECTION
# ══════════════════════════════════════════════════════════════════════
CSS_MARKER = "/* FOREST_GREEN_THEME */"

def apply_css_files():
    count = 0
    webrtc_css = os.path.join(ROOT_DIR, "WebRtcBundle", "rootapp-desktop-webrtc.css")
    css_files = [webrtc_css] if os.path.exists(webrtc_css) else []
    apps_dir = os.path.join(ROOT_DIR, "RootApps")
    if os.path.exists(apps_dir):
        css_files += glob.glob(os.path.join(apps_dir, "**", "*.css"), recursive=True)
    for css_file in css_files:
        try:
            with open(css_file, "r", encoding="utf-8") as f:
                content = f.read()
            if CSS_MARKER in content:
                content = content[:content.index(CSS_MARKER)]
            with open(css_file, "w", encoding="utf-8") as f:
                f.write(content + CSS_MARKER + THEME_CSS)
            count += 1
        except Exception:
            pass
    return count


# ══════════════════════════════════════════════════════════════════════
#  STEP 3: HTML PATCHING
# ══════════════════════════════════════════════════════════════════════
HTML_MARKER = "<!-- FOREST_GREEN_THEME -->"
BODY_STYLE = 'style="background-color:#040D04;color:#D4E8D4"'
INLINE_STYLE = """<style>
/* FOREST_GREEN_THEME inline */
html,body,#root,#app{background-color:#040D04 !important;color:#D4E8D4 !important}
iframe{background:#040D04 !important}
</style>"""

def apply_html_files():
    count = 0
    html_files = []
    for path in [os.path.join(ROOT_DIR, "WebRtcBundle", "index.html"),
                 os.path.join(HOST_DIR, "index.html")]:
        if os.path.exists(path):
            html_files.append(path)
    apps_dir = os.path.join(ROOT_DIR, "RootApps")
    if os.path.exists(apps_dir):
        html_files += glob.glob(os.path.join(apps_dir, "**", "index.html"), recursive=True)
    for html_file in html_files:
        try:
            with open(html_file, "r", encoding="utf-8-sig") as f:
                content = f.read()
            if HTML_MARKER in content:
                content = content.replace(HTML_MARKER, "")
                content = re.sub(r'<style>\s*/\* FOREST_GREEN_THEME inline \*/.*?</style>\s*',
                                 '', content, flags=re.DOTALL)
                content = content.replace(f' {BODY_STYLE}', '')
            content = content.replace('</head>', f'{HTML_MARKER}\n{INLINE_STYLE}\n</head>', 1)
            content = re.sub(r'<body(?![^>]*style=)', f'<body {BODY_STYLE}', content, count=1)
            with open(html_file, "w", encoding="utf-8") as f:
                f.write(content)
            count += 1
        except Exception:
            pass
    return count


# ══════════════════════════════════════════════════════════════════════
#  STEP 4: JS BUNDLE PATCHING (Phase E + F)
# ══════════════════════════════════════════════════════════════════════

# Phase E: Hex colors inside JS theme objects and inline styles
# Same-length #RRGGBB → #RRGGBB replacements, safe for minified JS
JS_COLOR_MAP = [
    # Brand colors
    ("#3B6AF8", "#1B8A4A"), ("#A8FF5D", "#4ADE80"), ("#49D6AC", "#22C55E"),
    # Dark theme backgrounds
    ("#0D1521", "#040D04"), ("#121A26", "#081408"), ("#07101B", "#020802"),
    ("#090E13", "#051005"),
    # Dark borders
    ("#242C36", "#14291A"), ("#232C36", "#14291A"),
    # Dark text
    ("#F2F2F2", "#D4E8D4"), ("#A7A7A8", "#8FA88F"), ("#7B7B89", "#5A725A"),
    ("#4F5C6F", "#2D5A3A"),
    # Links / status
    ("#88A5FF", "#34D399"), ("#F03F36", "#DC2626"), ("#F0F250", "#4ADE80"),
    ("#E88F3D", "#CA8A04"),
    # Pure-dark theme
    ("#161617", "#040D04"), ("#1F1F22", "#081408"), ("#111113", "#020802"),
    ("#101112", "#051005"), ("#303030", "#14291A"),
    # Light theme
    ("#FBFBFB", "#F0FFF0"), ("#F5F6F8", "#E8F5E8"), ("#131313", "#0A1A0A"),
    ("#282828", "#0D1F0D"), ("#5E5E5E", "#2D5A3A"), ("#DDDDDD", "#14291A"),
    ("#58AC30", "#1B8A4A"), ("#08a677", "#22C55E"), ("#006eeb", "#059669"),
    # Muted variants
    ("#A9B3C0", "#8FA88F"),
]

# Phase F: Canvas rgb/rgba colors in WebRTC bundle
CANVAS_COLOR_MAP = [
    ("rgb(0, 0, 0)",        "rgb(4, 13, 4)"),
    ("rgb(0,0,0)",          "rgb(4,13,4)"),
    ("rgba(0, 0, 0, 0.7)",  "rgba(4, 13, 4, 0.7)"),
    ("rgba(0,0,0,0.7)",     "rgba(4,13,4,0.7)"),
    ("rgba(0, 0, 0, 0.25)", "rgba(4, 13, 4, 0.25)"),
    ("rgba(0,0,0,0.25)",    "rgba(4,13,4,0.25)"),
    ("rgba(0, 0, 0, 0.5)",  "rgba(4, 13, 4, 0.5)"),
    ("rgba(0,0,0,0.5)",     "rgba(4,13,4,0.5)"),
]

JS_MARKER = "/* FOREST_GREEN_JS */"

def apply_js_files():
    """Phase E + F: Patch hardcoded colors in all JS bundles."""
    js_files = []
    apps_dir = os.path.join(ROOT_DIR, "RootApps")
    if os.path.exists(apps_dir):
        js_files += glob.glob(os.path.join(apps_dir, "**", "*.js"), recursive=True)
    webrtc_js = os.path.join(ROOT_DIR, "WebRtcBundle", "rootapp-desktop-webrtc.js")
    if os.path.exists(webrtc_js):
        js_files.append(webrtc_js)

    js_count = 0
    canvas_count = 0
    files_patched = 0

    for js_file in js_files:
        try:
            with open(js_file, "r", encoding="utf-8", errors="replace") as f:
                content = f.read()

            # Strip previous patches if re-running
            if JS_MARKER in content:
                content = content[:content.index(JS_MARKER)]

            original = content
            file_canvas = 0

            # Phase E: Hex color replacements (case-insensitive)
            for old_hex, new_hex in JS_COLOR_MAP:
                # Replace exact case, lowercase, and uppercase variants
                for old_v, new_v in [(old_hex, new_hex),
                                     (old_hex.lower(), new_hex.lower()),
                                     (old_hex.upper(), new_hex.upper())]:
                    count_before = content.count(old_v)
                    if count_before > 0:
                        content = content.replace(old_v, new_v)
                        js_count += count_before

            # Phase F: Canvas rgb/rgba (only for WebRTC bundle, but check all)
            for old_rgb, new_rgb in CANVAS_COLOR_MAP:
                count_before = content.count(old_rgb)
                if count_before > 0:
                    content = content.replace(old_rgb, new_rgb)
                    file_canvas += count_before

            canvas_count += file_canvas

            if content != original:
                with open(js_file, "w", encoding="utf-8") as f:
                    f.write(content)
                files_patched += 1
        except Exception as e:
            pass

    return files_patched, js_count, canvas_count


# ══════════════════════════════════════════════════════════════════════
#  STEP 5: BINARY PATCH ROOT.EXE
# ══════════════════════════════════════════════════════════════════════

# ── Phase D (runs first): CSS var() removal ──────────────────────────
# The embedded CSS at offset ~0x12AE9676 uses:
#   --color-X: var(--rootsdk-X, #DEFAULT);
# The .NET host sets --rootsdk-X at runtime from server-sent theme data,
# overriding our patched defaults. Fix: REMOVE the var() wrappers entirely
# and hardcode green values. This makes the server-sent theme irrelevant.
ROOTSDK_GREEN = {
    b'brand-primary':       b'#1B8A4A',
    b'brand-secondary':     b'#4ADE80',
    b'brand-tertiary':      b'#22C55E',
    b'text-primary':        b'#D4E8D4',
    b'text-secondary':      b'#8FA88F',
    b'text-tertiary':       b'#5A725A',
    b'text-white':          b'#E0F0E0',
    b'background-primary':  b'#040D04',
    b'background-secondary':b'#081408',
    b'background-tertiary': b'#020802',
    b'input':               b'#051005',
    b'border':              b'#14291A',
    b'highlight-light':     b'#4ADE8008',
    b'highlight-normal':    b'#4ADE8015',
    b'highlight-strong':    b'#4ADE8028',
    b'info':                b'#4ADE80',
    b'warning':             b'#CA8A04',
    b'error':               b'#DC2626',
    b'muted':               b'#2D5A3A',
    b'link':                b'#34D399',
    b'background-blur':     b'#000000CC',
    b'self-mention':        b'#1B8A4A66',
    b'community-mention':   b'#4ADE8033',
    b'channel-mention':     b'#CA8A044D',
    b'transparent':         b'transparent',
}

# ── Phase A: Remaining CSS hex colors (#RRGGBB) ─────────────────────
# Catches colors OUTSIDE the main theme block (on-disk CSS bundles, etc.)
CSS_COLOR_MAP = [
    # Dark theme colors
    ("#0D1521", "#040D04"), ("#121A26", "#081408"), ("#07101B", "#020802"),
    ("#090E13", "#051005"), ("#3B6AF8", "#1B8A4A"), ("#A8FF5D", "#4ADE80"),
    ("#242C36", "#14291A"), ("#A7A7A8", "#8FA88F"), ("#4F5C6F", "#2D5A3A"),
    ("#88A5FF", "#34D399"), ("#F2F2F2", "#D4E8D4"), ("#F03F36", "#DC2626"),
    # Pure-dark theme colors (were missing before!)
    ("#161617", "#040D04"), ("#1F1F22", "#081408"), ("#111113", "#020802"),
    ("#101112", "#051005"), ("#303030", "#14291A"),
    # Light theme backgrounds → green too
    ("#FBFBFB", "#040D04"), ("#F5F6F8", "#020802"),
    # Tertiary brand
    ("#49D6AC", "#22C55E"), ("#08a677", "#22C55E"),
    # Muted variants
    ("#7B7B89", "#5A725A"), ("#A9B3C0", "#8FA88F"),
]

# ── Phase B: Avalonia Fluent BAML colors ─────────────────────────────
BAML_COLOR_MAP_7CHAR = [
    ("#202020", "#040D04"), ("#1E1E1E", "#051005"), ("#1C1C1C", "#040C04"),
    ("#1A1A1A", "#030B03"), ("#131313", "#020802"), ("#0A0A0A", "#010501"),
    ("#282828", "#0D1F0D"), ("#2C2C2C", "#0F220F"), ("#2E2E2E", "#102410"),
    ("#2D3236", "#14291A"), ("#393D1B", "#14291A"),
    ("#8A8A8A", "#5A725A"), ("#0F7B0F", "#1B8A4A"), ("#9D5D00", "#166534"),
]

BAML_COLOR_MAP_9CHAR = [
    ("#FF202020", "#FF040D04"), ("#00202020", "#00040D04"),
    ("#00F3F3F3", "#00E0F0E0"), ("#4DF9F9F9", "#4D0D1F0D"),
    ("#80F6F6F6", "#800D1F0D"), ("#80F9F9F9", "#800D1F0D"),
    ("#08FFFFFF", "#08D4E8D4"), ("#09FFFFFF", "#09D4E8D4"),
    ("#0AFFFFFF", "#0AD4E8D4"), ("#0BFFFFFF", "#0BD4E8D4"),
    ("#0DFFFFFF", "#0DD4E8D4"), ("#0FFFFFFF", "#0FD4E8D4"),
    ("#12FFFFFF", "#12D4E8D4"), ("#14FFFFFF", "#14D4E8D4"),
    ("#15FFFFFF", "#15D4E8D4"), ("#18FFFFFF", "#18D4E8D4"),
    ("#28FFFFFF", "#28D4E8D4"), ("#3FFFFFFF", "#3FD4E8D4"),
    ("#40FFFFFF", "#40D4E8D4"), ("#59FFFFFF", "#59D4E8D4"),
    ("#80FFFFFF", "#80D4E8D4"), ("#FFF2F2F2", "#FFD4E8D4"),
    ("#0F000000", "#0F000800"), ("#19000000", "#19000800"),
    ("#23000000", "#23000800"), ("#33000000", "#33000800"),
    ("#37000000", "#37000800"), ("#FFFF0000", "#FFDC2626"),
    ("#000000FF", "#001B8A4A"), ("#600000FF", "#601B8A4A"),
]

# ── Phase C: Binary ARGB/BGRA patterns ──────────────────────────────
ARGB_MAP = [
    (b'\xFF\x3B\x6A\xF8', b'\xFF\x1B\x8A\x4A'),
    (b'\xFF\xA8\xFF\x5D', b'\xFF\x4A\xDE\x80'),
    (b'\xFF\x88\xA5\xFF', b'\xFF\x34\xD3\x99'),
    (b'\xFF\x0D\x15\x21', b'\xFF\x04\x0D\x04'),
    (b'\xFF\x12\x1A\x26', b'\xFF\x08\x14\x08'),
    (b'\xFF\x07\x10\x1B', b'\xFF\x02\x08\x02'),
    (b'\xFF\x09\x0E\x13', b'\xFF\x05\x10\x05'),
    (b'\xFF\x24\x2C\x36', b'\xFF\x14\x29\x1A'),
    (b'\xFF\x4F\x5C\x6F', b'\xFF\x2D\x5A\x3A'),
    (b'\x21\x15\x0D\xFF', b'\x04\x0D\x04\xFF'),
    (b'\x26\x1A\x12\xFF', b'\x08\x14\x08\xFF'),
    # Pure-dark ARGB
    (b'\xFF\x16\x16\x17', b'\xFF\x04\x0D\x04'),
    (b'\xFF\x1F\x1F\x22', b'\xFF\x08\x14\x08'),
    (b'\xFF\x11\x11\x13', b'\xFF\x02\x08\x02'),
    (b'\xFF\x10\x11\x12', b'\xFF\x05\x10\x05'),
    (b'\xFF\x30\x30\x30', b'\xFF\x14\x29\x1A'),
]

BAML_START = 0x1B5B0000
BAML_END = 0x1B6B0000


def _find_all(data, pattern, start=0, end=None):
    positions = []
    pos = start
    if end is None:
        end = len(data)
    while pos < end:
        idx = data.find(pattern, pos, end)
        if idx == -1:
            break
        positions.append(idx)
        pos = idx + 1
    return positions


def patch_binary():
    if not os.path.exists(ROOT_EXE):
        print("  Root.exe not found!")
        return 0

    if not os.path.exists(BACKUP_EXE):
        os.makedirs(BACKUP_DIR, exist_ok=True)
        shutil.copy2(ROOT_EXE, BACKUP_EXE)
        print(f"  Created backup: {BACKUP_EXE}")

    # Always start from clean backup
    print(f"  Restoring from backup ({os.path.getsize(BACKUP_EXE) // (1024*1024)} MB)...")
    shutil.copy2(BACKUP_EXE, ROOT_EXE)

    with open(ROOT_EXE, "rb") as f:
        data = bytearray(f.read())
    total = 0

    # ── Phase D: Remove var() wrappers from embedded CSS ─────────
    # This is THE critical fix. Transforms:
    #   --color-X: var(--rootsdk-X, #DEFAULT);
    # Into:
    #   --color-X: #GREEN                    ;
    # (padded with spaces to maintain exact byte length)
    print("  Phase D: Removing CSS var() wrappers (server override bypass)...")
    phase_d = 0
    var_pattern = re.compile(rb'var\(--rootsdk-([a-z-]+),\s*([^)]+)\)')
    for match in reversed(list(var_pattern.finditer(data))):
        var_name = match.group(1)
        full_len = match.end() - match.start()
        green_val = ROOTSDK_GREEN.get(var_name, match.group(2))
        # Pad replacement with spaces to maintain exact byte count
        replacement = green_val.ljust(full_len)
        data[match.start():match.end()] = replacement
        phase_d += 1
    print(f"    {phase_d} var() wrappers removed (hardcoded green)")
    total += phase_d

    # ── Phase A: Remaining CSS hex colors ────────────────────────
    print("  Phase A: CSS hex strings (outside theme block)...")
    phase_a = 0
    for old_hex, new_hex in CSS_COLOR_MAP:
        for case_fn in [str, str.lower, str.upper]:
            old_b = case_fn(old_hex).encode('utf-8')
            new_b = case_fn(new_hex).encode('utf-8')
            for pos in reversed(_find_all(data, old_b)):
                data[pos:pos + len(old_b)] = new_b
                phase_a += 1
    print(f"    {phase_a} CSS color replacements")
    total += phase_a

    # ── Phase B: BAML Fluent theme colors ────────────────────────
    print("  Phase B: BAML Fluent theme colors...")
    phase_b = 0
    baml_end = min(BAML_END, len(data))
    for old_hex, new_hex in BAML_COLOR_MAP_9CHAR:
        old_b = old_hex.encode('utf-8')
        new_b = new_hex.encode('utf-8')
        for pos in reversed(_find_all(data, old_b, BAML_START, baml_end)):
            data[pos:pos + len(old_b)] = new_b
            phase_b += 1
    for old_hex, new_hex in BAML_COLOR_MAP_7CHAR:
        old_b = old_hex.encode('utf-8')
        new_b = new_hex.encode('utf-8')
        for pos in reversed(_find_all(data, old_b, BAML_START, baml_end)):
            data[pos:pos + len(old_b)] = new_b
            phase_b += 1
    print(f"    {phase_b} BAML color replacements")
    total += phase_b

    # ── Phase C: Binary ARGB/BGRA patterns ───────────────────────
    print("  Phase C: Binary ARGB patterns...")
    phase_c = 0
    for old_bytes, new_bytes in ARGB_MAP:
        positions = _find_all(data, old_bytes)
        if 0 < len(positions) < 500:
            for pos in reversed(positions):
                data[pos:pos + len(new_bytes)] = new_bytes
                phase_c += 1
        elif len(positions) >= 500:
            print(f"    Skipped {old_bytes.hex()}: {len(positions)} hits (too many)")
    print(f"    {phase_c} ARGB replacements")
    total += phase_c

    # ── Phase H: IL BGRA constants (Avalonia sidebar/nav) ──────────
    # Region-limited to 0x19BB0000-0x19BC0000 to avoid false positives
    print("  Phase H: IL BGRA constants (Avalonia native UI)...")
    IL_BGRA_MAP = [
        # BGRA order: Blue, Green, Red, Alpha
        (b'\xF8\x6A\x3B\xFF', b'\x4A\x8A\x1B\xFF'),  # brand blue
        (b'\x5D\xFF\xA8\xFF', b'\x80\xDE\x4A\xFF'),  # brand lime
        (b'\xF2\xF2\xF2\xFF', b'\xD4\xE8\xD4\xFF'),  # text white
        (b'\x21\x15\x0D\xFF', b'\x04\x0D\x04\xFF'),  # bg primary
        (b'\x36\x2C\x24\xFF', b'\x1A\x29\x14\xFF'),  # border
        (b'\x6F\x5C\x4F\xFF', b'\x3A\x5A\x2D\xFF'),  # muted
        (b'\xFF\xA5\x88\xFF', b'\x99\xD3\x34\xFF'),  # link
        (b'\x26\x1A\x12\xFF', b'\x08\x14\x08\xFF'),  # bg secondary
        (b'\x1B\x10\x07\xFF', b'\x02\x08\x02\xFF'),  # bg tertiary
        (b'\x13\x0E\x09\xFF', b'\x05\x10\x05\xFF'),  # input bg
        (b'\x17\x16\x16\xFF', b'\x04\x0D\x04\xFF'),  # pure-dark bg
        (b'\x22\x1F\x1F\xFF', b'\x08\x14\x08\xFF'),  # pure-dark bg2
        (b'\x13\x11\x11\xFF', b'\x02\x08\x02\xFF'),  # pure-dark bg3
        (b'\x30\x30\x30\xFF', b'\x1A\x29\x14\xFF'),  # pure-dark border
    ]
    il_start = 0x19BB0000
    il_end = min(0x19BC0000, len(data))
    phase_h = 0
    for old_bytes, new_bytes in IL_BGRA_MAP:
        positions = _find_all(data, old_bytes, il_start, il_end)
        for pos in reversed(positions):
            data[pos:pos + len(new_bytes)] = new_bytes
            phase_h += 1
    print(f"    {phase_h} IL BGRA replacements")
    total += phase_h

    if total > 0:
        print(f"  Writing patched binary ({len(data)} bytes)...")
        with open(ROOT_EXE, "wb") as f:
            f.write(data)
    return total


# ══════════════════════════════════════════════════════════════════════
#  STEP 5: LAUNCH + DWM
# ══════════════════════════════════════════════════════════════════════
def launch_root():
    if not os.path.exists(ROOT_EXE):
        return False
    subprocess.Popen([ROOT_EXE], cwd=os.path.dirname(ROOT_EXE),
                     creationflags=0x00000008)
    for i in range(60):
        time.sleep(1)
        windows = find_root_windows()
        if windows:
            print(f"  Window appeared ({i+1}s)")
            return True
        if i % 5 == 4:
            print(f"  Waiting... ({i+1}s)")
    return False

def apply_dwm():
    windows = find_root_windows()
    count = 0
    for hwnd, pid in windows:
        dark = ctypes.c_int(1)
        dwmapi.DwmSetWindowAttribute(hwnd, 20, ctypes.byref(dark), ctypes.sizeof(dark))
        for attr, color in [(34, "#14291A"), (35, "#040D04"), (36, "#D4E8D4")]:
            val = ctypes.c_uint32(hex_to_colorref(color))
            dwmapi.DwmSetWindowAttribute(hwnd, attr, ctypes.byref(val), ctypes.sizeof(val))
        count += 1
    return count


# ══════════════════════════════════════════════════════════════════════
#  MAIN
# ══════════════════════════════════════════════════════════════════════
def main():
    print("=" * 60)
    print("  FOREST GREEN THEME v2 - FULL AUTO INSTALL")
    print("  Phases: A-H (CSS + HTML + JS + Binary + IL BGRA)")
    print("=" * 60)
    print()

    # Step 1: Kill Root
    print("[1/6] Closing Root.exe...")
    was_running = kill_root()
    if was_running:
        print("  Waiting 3s for file locks...")
        time.sleep(3)
    print()

    # Step 2: CSS file injection (Phase G: ALL *.css files now)
    print("[2/6] Injecting CSS into ALL *.css files (Phase G)...")
    css_count = apply_css_files()
    print(f"  {css_count} CSS files patched")
    print()

    # Step 3: HTML patching
    print("[3/6] Patching HTML backgrounds...")
    html_count = apply_html_files()
    print(f"  {html_count} HTML files patched")
    print()

    # Step 4: JS bundle patching (Phase E + F)
    print("[4/6] Patching JS bundles (Phase E: hex colors + Phase F: canvas rgb)...")
    js_files_patched, js_hex_count, js_canvas_count = apply_js_files()
    print(f"  {js_files_patched} JS files patched")
    print(f"    Phase E: {js_hex_count} hex color replacements")
    print(f"    Phase F: {js_canvas_count} canvas rgb/rgba replacements")
    print()

    # Step 5: Binary patch (Phase D + A + B + C + H)
    print("[5/6] Binary-patching Root.exe (D:var + A:CSS + B:BAML + C:ARGB + H:IL)...")
    bin_count = patch_binary()
    print(f"  Total: {bin_count} binary replacements")
    print()

    # Step 6: Launch Root + DWM
    print("[6/6] Launching Root.exe...")
    launched = launch_root()
    if launched:
        print("  Waiting 5s for app to fully load...")
        time.sleep(5)
        dwm_count = apply_dwm()
        print(f"  DWM themed {dwm_count} window(s)")
    print()

    # Summary
    print("=" * 60)
    print("  DONE — Forest Green v2")
    print("=" * 60)
    print(f"  CSS files:        {css_count} (all *.css)")
    print(f"  HTML files:       {html_count}")
    print(f"  JS files:         {js_files_patched} ({js_hex_count} hex + {js_canvas_count} canvas)")
    print(f"  Binary patches:   {bin_count} (D+A+B+C+H)")
    print()
    print("  To uninstall: python revert_forest_theme.py")


if __name__ == "__main__":
    main()
