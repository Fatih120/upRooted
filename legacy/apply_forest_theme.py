"""
Apply deep forest green theme to all Root Communications CSS + HTML files.
Full-app coverage: CSS variables, scrollbars, selections, form states,
hardcoded overrides, Tailwind colors, PrimeReact components, HTML backgrounds.
"""
import glob
import os
import re

ROOT_DIR = os.path.expandvars(r"%LOCALAPPDATA%\Root Communications\Root\profile\default")
HOST_DIR = os.path.expandvars(r"%LOCALAPPDATA%\Root\current\DotNetBrowser\RootApps\Bundle\Host")

# ── Deep forest palette ──────────────────────────────────────────────
# Backgrounds pushed near-black with heavy green tint
# Accents are rich emerald/jade, not pastel
THEME_CSS = """
/* ═══ ROOT VARIABLES ═══ */
:root{
--rootsdk-brand-primary:#1B8A4A !important;
--rootsdk-brand-secondary:#4ADE80 !important;
--rootsdk-brand-tertiary:#22C55E !important;
--rootsdk-text-primary:#D4E8D4 !important;
--rootsdk-text-secondary:#8FA88F !important;
--rootsdk-text-tertiary:#5A725A !important;
--rootsdk-text-white:#E0F0E0 !important;
--rootsdk-background-primary:#040D04 !important;
--rootsdk-background-secondary:#081408 !important;
--rootsdk-background-tertiary:#020802 !important;
--rootsdk-input:#051005 !important;
--rootsdk-border:#14291A !important;
--rootsdk-highlight-light:#4ADE8008 !important;
--rootsdk-highlight-normal:#4ADE8015 !important;
--rootsdk-highlight-strong:#4ADE8028 !important;
--rootsdk-info:#4ADE80 !important;
--rootsdk-warning:#CA8A04 !important;
--rootsdk-error:#DC2626 !important;
--rootsdk-muted:#2D5A3A !important;
--rootsdk-link:#34D399 !important;
--rootsdk-background-blur:#000000CC !important;
--rootsdk-self-mention:#DC262640 !important;
--rootsdk-community-mention:#4ADE8025 !important;
--rootsdk-channel-mention:#CA8A0435 !important;
--rootsdk-transparent:transparent !important;
--rootsdk-success:#16A34A !important;
--rootsdk-surface-primary:#040D04 !important;
--rootsdk-surface-secondary:#081408 !important;
--rootsdk-surface-tertiary:#020802 !important;
}

/* ═══ FALLBACK COLOR VARS (used by components directly) ═══ */
:root,
:root [data-theme=dark],
:root [data-theme=pure-dark],
:root [data-theme=light]{
--color-brand-primary:#1B8A4A !important;
--color-brand-secondary:#4ADE80 !important;
--color-brand-tertiary:#22C55E !important;
--color-text-primary:#D4E8D4 !important;
--color-text-secondary:#8FA88F !important;
--color-text-tertiary:#5A725A !important;
--color-text-white:#E0F0E0 !important;
--color-background-primary:#040D04 !important;
--color-background-secondary:#081408 !important;
--color-background-tertiary:#020802 !important;
--color-input:#051005 !important;
--color-border:#14291A !important;
--color-highlight-light:#4ADE8008 !important;
--color-highlight-normal:#4ADE8015 !important;
--color-highlight-strong:#4ADE8028 !important;
--color-info:#4ADE80 !important;
--color-warning:#CA8A04 !important;
--color-error:#DC2626 !important;
--color-muted:#2D5A3A !important;
--color-link:#34D399 !important;
--color-background-blur:#000000CC !important;
--color-self-mention:#DC262640 !important;
--color-community-mention:#4ADE8025 !important;
--color-channel-mention:#CA8A0435 !important;
--color-transparent:transparent !important;
--color-success:#16A34A !important;
}

/* ═══ SCROLLBARS ═══ */
*{scrollbar-width:thin !important;scrollbar-color:#14291A #040D04 !important}
::-webkit-scrollbar{width:6px !important;height:6px !important}
::-webkit-scrollbar-track{background:#040D04 !important}
::-webkit-scrollbar-thumb{background:#1B5A2E !important;border-radius:3px !important}
::-webkit-scrollbar-thumb:hover{background:#22C55E !important}
::-webkit-scrollbar-corner{background:#020802 !important}

/* ═══ SELECTIONS ═══ */
::selection{background:#1B8A4A !important;color:#E0F0E0 !important}
::-moz-selection{background:#1B8A4A !important;color:#E0F0E0 !important}

/* ═══ ROOT ELEMENT + BODY ═══ */
html,body,#root,#app{
background-color:#040D04 !important;
color:#D4E8D4 !important;
}

/* ═══ GLOBAL OVERRIDES FOR HARDCODED COLORS ═══ */
/* PrimeReact checkbox borders - force green */
.p-checkbox .p-checkbox-box{
border-color:#2D5A3A !important;
}
.p-checkbox.p-highlight .p-checkbox-box{
border-color:#1B8A4A !important;
background-color:#1B8A4A22 !important;
}
.p-checkbox .p-checkbox-box svg path{
fill:#4ADE80 !important;
}

/* PrimeReact dropdown panels */
.p-dropdown-panel,
.p-multiselect-panel{
background-color:#081408 !important;
}
.p-dropdown-panel .p-dropdown-items-wrapper,
.p-multiselect-panel .p-multiselect-items-wrapper{
background-color:#081408 !important;
border-color:#14291A !important;
}
.p-dropdown-panel .p-dropdown-items,
.p-multiselect-panel .p-multiselect-items{
background-color:#081408 !important;
}
.p-dropdown-item:hover,
.p-multiselect-item:hover,
.p-dropdown-panel .p-focus{
background-color:#0D1F0D !important;
}

/* PrimeReact inputs */
.p-inputtext{
background-color:#051005 !important;
border-color:#14291A !important;
color:#D4E8D4 !important;
}
.p-inputtext:enabled:focus,
.p-inputtext:focus{
border-color:#1B8A4A !important;
}
.p-inputtext::placeholder{
color:#5A725A !important;
}

/* PrimeReact dropdowns */
.p-dropdown{
border-color:#14291A !important;
}
.p-dropdown:not(.p-disabled).p-focus{
border-color:#1B8A4A !important;
}
.p-dropdown-label,
.p-dropdown-trigger{
color:#D4E8D4 !important;
}

/* PrimeReact buttons */
.p-button:not(.p-button-link).submit-button{
background-color:#1B8A4A !important;
color:#020802 !important;
}
.p-button:not(.p-button-link).cancel-button{
border-color:#5A725A !important;
color:#D4E8D4 !important;
}

/* PrimeReact dialogs */
.p-dialog,
.dialog-container.p-dialog{
background:#040D04 !important;
}
.p-dialog .p-dialog-header,
.dialog-container .p-dialog-header{
background-color:#040D04 !important;
color:#D4E8D4 !important;
border-color:#4ADE8015 !important;
}
.p-dialog .p-dialog-content,
.dialog-container .p-dialog-content{
background-color:#040D04 !important;
color:#D4E8D4 !important;
border-color:#4ADE8015 !important;
}
.dialog-container .p-dialog-footer{
background-color:#040D04 !important;
border-color:#4ADE8015 !important;
}

/* PrimeReact cards */
.p-card{
background-color:#081408 !important;
color:#D4E8D4 !important;
}

/* PrimeReact skeleton loading */
.p-skeleton{
background:#0D1F0D !important;
}
.p-skeleton:after{
background:linear-gradient(90deg,transparent,#4ADE800A,transparent) !important;
}

/* ═══ WEBRTC CALL UI ═══ */
.call{
background-color:#020802 !important;
}
.call .panel-group{
background-color:#040D04 !important;
}
.call .panel-group:has(#focused) #unfocused{
background-color:#020802 !important;
}
.stream-container:has(video.screen-stream){
background-color:#081408 !important;
}
.debug-panel{
background-color:#020802 !important;
color:#D4E8D4 !important;
}
.debug-form-card{
border-color:#14291A !important;
background-color:#081408 !important;
}
.resize-handle:hover,
.resize-handle[data-resize-handle-state=hover],
.resize-handle[data-resize-handle-state=drag]{
border-top-color:#1B8A4A !important;
}
.pending-overlay{
background-color:#000000CC !important;
}
.username{
background-color:#000000CC !important;
color:#D4E8D4 !important;
}
.muted-icon,.deafened-icon,.hand-raised{
background-color:#000000CC !important;
color:#D4E8D4 !important;
}
.full-focus-button{
background-color:#081408 !important;
color:#D4E8D4 !important;
border-color:#14291A !important;
}
.profile{
background-color:#081408 !important;
}

/* ═══ FORM FIELD STATES ═══ */
.text-input:focus,
.text-input:active,
.text-input:focus-visible,
.textarea-input:focus,
.textarea-input:active,
.textarea-input:focus-visible{
border-color:#1B8A4A !important;
}
.form-field-errors{
border-color:#4ADE8015 !important;
color:#8FA88F !important;
}

/* ═══ TAILWIND OVERRIDES (sub-apps using Tailwind v3/v4) ═══ */
/* Background utilities */
.bg-white{background-color:#081408 !important}
.bg-black{background-color:#020802 !important}
.bg-gray-50,.bg-slate-50,.bg-zinc-50,.bg-neutral-50{background-color:#0A1A0A !important}
.bg-gray-100,.bg-slate-100,.bg-zinc-100,.bg-neutral-100{background-color:#081408 !important}
.bg-gray-200,.bg-slate-200,.bg-zinc-200,.bg-neutral-200{background-color:#0D1F0D !important}
.bg-gray-300,.bg-slate-300,.bg-zinc-300,.bg-neutral-300{background-color:#14291A !important}
.bg-gray-700,.bg-slate-700,.bg-zinc-700,.bg-neutral-700{background-color:#0D1F0D !important}
.bg-gray-800,.bg-slate-800,.bg-zinc-800,.bg-neutral-800{background-color:#081408 !important}
.bg-gray-900,.bg-slate-900,.bg-zinc-900,.bg-neutral-900{background-color:#040D04 !important}
.bg-gray-950,.bg-slate-950,.bg-zinc-950,.bg-neutral-950{background-color:#020802 !important}

/* Text utilities */
.text-white{color:#D4E8D4 !important}
.text-black{color:#040D04 !important}
.text-gray-400,.text-slate-400,.text-zinc-400,.text-neutral-400{color:#5A725A !important}
.text-gray-500,.text-slate-500,.text-zinc-500,.text-neutral-500{color:#5A725A !important}
.text-gray-300,.text-slate-300,.text-zinc-300,.text-neutral-300{color:#8FA88F !important}

/* Border utilities */
.border-gray-200,.border-slate-200,.border-zinc-200,.border-neutral-200{border-color:#14291A !important}
.border-gray-300,.border-slate-300,.border-zinc-300,.border-neutral-300{border-color:#1B3520 !important}
.border-gray-700,.border-slate-700,.border-zinc-700,.border-neutral-700{border-color:#14291A !important}

/* Ring/focus */
.ring-blue-500,.ring-indigo-500,.focus\\:ring-blue-500:focus,.focus\\:ring-indigo-500:focus{
--tw-ring-color:#1B8A4A !important;
}

/* Hover states */
.hover\\:bg-gray-100:hover,.hover\\:bg-slate-100:hover{background-color:#0D1F0D !important}
.hover\\:bg-gray-700:hover,.hover\\:bg-slate-700:hover{background-color:#0D1F0D !important}
.hover\\:bg-gray-50:hover,.hover\\:bg-slate-50:hover{background-color:#0A1A0A !important}

/* Blue/indigo → green (buttons, badges, links) */
.bg-blue-500,.bg-blue-600,.bg-indigo-500,.bg-indigo-600{background-color:#1B8A4A !important}
.bg-blue-50,.bg-indigo-50{background-color:#0A1A0A !important}
.bg-blue-100,.bg-indigo-100{background-color:#0D1F0D !important}
.text-blue-500,.text-blue-600,.text-indigo-500,.text-indigo-600{color:#34D399 !important}
.text-blue-400,.text-indigo-400{color:#4ADE80 !important}
.border-blue-500,.border-indigo-500{border-color:#1B8A4A !important}
.hover\\:bg-blue-600:hover,.hover\\:bg-indigo-600:hover{background-color:#16A34A !important}
.hover\\:bg-blue-700:hover,.hover\\:bg-indigo-700:hover{background-color:#15803D !important}
.hover\\:text-blue-500:hover,.hover\\:text-indigo-500:hover{color:#34D399 !important}

/* Purple → deep emerald */
.bg-purple-500,.bg-purple-600,.bg-violet-500,.bg-violet-600{background-color:#0F7B3D !important}
.text-purple-500,.text-purple-600,.text-violet-500,.text-violet-600{color:#22C55E !important}

/* ═══ TAILWIND v4 THEME LAYER (Polls, newer apps) ═══ */
@layer theme{
:root{
--color-primary:#1B8A4A !important;
--color-secondary:#4ADE80 !important;
}
}

/* ═══ SVG ICON FILLS ═══ */
/* Override hardcoded brand colors in SVGs */
svg path[fill="#3B6AF8"],svg path[fill="#3b6af8"]{fill:#1B8A4A !important}
svg path[fill="#A8FF5D"],svg path[fill="#a8ff5d"]{fill:#4ADE80 !important}
svg path[fill="#49D6AC"],svg path[fill="#49d6ac"]{fill:#22C55E !important}
svg path[fill="#08A677"],svg path[fill="#08a677"]{fill:#16A34A !important}
svg path[fill="#F2F2F2"],svg path[fill="#f2f2f2"]{fill:#D4E8D4 !important}

/* ═══ IMAGE CONTAINERS ═══ */
.image-div{
background-color:#0D1F0D !important;
}

/* ═══ VOLUME/MEDIA CONTROLS ═══ */
.volume-container{
background-color:#081408 !important;
}
.volume-label,.volume-value{
color:#8FA88F !important;
}

/* ═══ SORTABLE/DRAG ELEMENTS ═══ */
.sortable-chip{
border-color:#14291A !important;
}

/* ═══ ANIMATIONS - green tinted ═══ */
@keyframes borderExpand{
0%{box-shadow:0 0 0 0 #4ADE80}
to{box-shadow:0 0 0 .0625rem #4ADE80}
}
@keyframes borderCollapse{
0%{box-shadow:0 0 0 .0625rem #4ADE80}
to{box-shadow:0 0 0 0 #4ADE80}
}

/* ═══ LINKS ═══ */
a{color:#34D399 !important}
a:hover{color:#4ADE80 !important}
a:visited{color:#22C55E !important}
"""

MARKER = "/* FOREST_GREEN_THEME */"

modified = []
skipped = []

# 1. WebRTC bundle CSS
webrtc_css = os.path.join(ROOT_DIR, "WebRtcBundle", "rootapp-desktop-webrtc.css")
if os.path.exists(webrtc_css):
    with open(webrtc_css, "r", encoding="utf-8") as f:
        content = f.read()
    if MARKER not in content:
        with open(webrtc_css, "a", encoding="utf-8") as f:
            f.write(MARKER + THEME_CSS)
        modified.append(webrtc_css)
    else:
        # Replace existing theme
        idx = content.index(MARKER)
        with open(webrtc_css, "w", encoding="utf-8") as f:
            f.write(content[:idx] + MARKER + THEME_CSS)
        modified.append(webrtc_css + " (updated)")

# 2. All sub-app CSS files (index-*.css only, the main entry point)
apps_dir = os.path.join(ROOT_DIR, "RootApps")
if os.path.exists(apps_dir):
    for css_file in glob.glob(os.path.join(apps_dir, "**", "index-*.css"), recursive=True):
        try:
            with open(css_file, "r", encoding="utf-8") as f:
                content = f.read()
            if MARKER not in content:
                with open(css_file, "a", encoding="utf-8") as f:
                    f.write(MARKER + THEME_CSS)
                modified.append(css_file)
            else:
                idx = content.index(MARKER)
                with open(css_file, "w", encoding="utf-8") as f:
                    f.write(content[:idx] + MARKER + THEME_CSS)
                modified.append(css_file + " (updated)")
        except Exception as e:
            skipped.append(f"{css_file}: {e}")

print(f"\nCSS theme applied to {len(modified)} files:")
for f in modified:
    short = f.replace(ROOT_DIR + os.sep, "")
    print(f"  + {short}")

if skipped:
    print(f"\nSkipped {len(skipped)} files:")
    for s in skipped:
        print(f"  ! {s}")

# ── HTML Patching ─────────────────────────────────────────────────────
# Inject background colors on <body> tags to prevent white flash on load
# and add inline <style> for pages that might not load themed CSS

HTML_MARKER = "<!-- FOREST_GREEN_THEME -->"
BODY_STYLE = 'style="background-color:#040D04;color:#D4E8D4"'

# Inline style block for HTML files (subset of full theme for instant load)
INLINE_STYLE = """<style>
/* FOREST_GREEN_THEME inline */
html,body,#root,#app{background-color:#040D04 !important;color:#D4E8D4 !important}
*{scrollbar-width:thin !important;scrollbar-color:#14291A #040D04 !important}
::-webkit-scrollbar{width:6px !important;height:6px !important}
::-webkit-scrollbar-track{background:#040D04 !important}
::-webkit-scrollbar-thumb{background:#1B5A2E !important;border-radius:3px !important}
::selection{background:#1B8A4A !important;color:#E0F0E0 !important}
iframe{background:#040D04 !important}
</style>"""

html_modified = []

def patch_html(filepath):
    """Inject forest green background into an HTML file's <body> and <head>."""
    if not os.path.exists(filepath):
        return False
    with open(filepath, "r", encoding="utf-8-sig") as f:
        content = f.read()

    if HTML_MARKER in content:
        # Already patched — remove old injection, re-apply
        content = content.replace(HTML_MARKER, "")
        # Remove old inline style block
        content = re.sub(r'<style>\s*/\* FOREST_GREEN_THEME inline \*/.*?</style>\s*',
                         '', content, flags=re.DOTALL)
        # Remove old body style attribute
        content = content.replace(f' {BODY_STYLE}', '')

    # Inject marker + inline style into <head>
    content = content.replace('</head>', f'{HTML_MARKER}\n{INLINE_STYLE}\n</head>', 1)

    # Inject style on <body> tag
    content = re.sub(r'<body(?![^>]*style=)', f'<body {BODY_STYLE}', content, count=1)

    with open(filepath, "w", encoding="utf-8") as f:
        f.write(content)
    return True

# 3. WebRTC index.html
webrtc_html = os.path.join(ROOT_DIR, "WebRtcBundle", "index.html")
if patch_html(webrtc_html):
    html_modified.append(webrtc_html)

# 4. Host container index.html
host_html = os.path.join(HOST_DIR, "index.html")
if patch_html(host_html):
    html_modified.append(host_html)

# 5. All sub-app index.html files
apps_dir = os.path.join(ROOT_DIR, "RootApps")
if os.path.exists(apps_dir):
    for html_file in glob.glob(os.path.join(apps_dir, "**", "index.html"), recursive=True):
        try:
            if patch_html(html_file):
                html_modified.append(html_file)
        except Exception as e:
            skipped.append(f"{html_file}: {e}")

print(f"\nHTML background patched in {len(html_modified)} files:")
for f in html_modified:
    short = f.replace(ROOT_DIR + os.sep, "").replace(HOST_DIR + os.sep, "Host/")
    print(f"  + {short}")

print(f"\nDone. Restart Root to see changes.")
print("To revert, run: python revert_forest_theme.py")
