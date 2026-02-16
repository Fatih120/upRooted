"""Find embedded WebRTC resource byte ranges in Root.exe."""
import mmap
import sys

EXE = r"C:\Users\bash\AppData\Local\Root\current\Root.exe"

with open(EXE, "rb") as f:
    mm = mmap.mmap(f.fileno(), 0, access=mmap.ACCESS_READ)

    # Find the HTML
    html_start = mm.find(b"<!DOCTYPE html>\n<html lang=\"en\">\n    <head>\n        <meta charset=\"UTF-8\">", 0x12AE0000)
    if html_start == -1:
        print("ERROR: Could not find embedded HTML")
        sys.exit(1)

    html_end = mm.find(b"</html>", html_start)
    if html_end == -1:
        print("ERROR: Could not find </html>")
        sys.exit(1)
    html_end += len(b"</html>")

    print(f"HTML: offset=0x{html_start:08X} to 0x{html_end:08X} ({html_end - html_start} bytes)")
    print(f"Content:\n{mm[html_start:html_end].decode('utf-8', errors='replace')}")
    print()

    # Find the source map (starts with {"version":3)
    # Search in the WebRTC resource region (around 0x12AE-0x1400)
    map_marker = b'{"version":3'
    map_start = mm.find(map_marker, 0x12AE0000)
    if map_start == -1:
        print("ERROR: Could not find source map")
        # Try broader search
        map_start = mm.find(map_marker)
        if map_start == -1:
            print("ERROR: Source map not found anywhere")
            sys.exit(1)

    # Find the end - source maps end with a closing }
    # But they can be huge. Let's find the length by looking for the next resource boundary.
    # Source maps typically end with }\n or just }
    # Let's search for the end by finding a null byte or the next resource header

    # Alternative: search for the end of the JSON
    # Source maps are single-line JSON, ending with }
    # But they can have newlines. Let's look for }\n followed by non-JSON content

    # Actually, let's just find how big it is by looking for null bytes or resource boundaries
    # The resource should be contiguous

    # Search for pattern after the map that indicates end of resource
    # Common patterns: null bytes, or the start of a new resource

    # Let's try finding the last } in a reasonable range
    pos = map_start + 1000  # skip into the map
    # Maps can be megabytes. Let's search in chunks.
    chunk_size = 1024 * 1024  # 1MB
    map_end = -1

    for offset in range(map_start, min(map_start + 50 * chunk_size, len(mm)), chunk_size):
        chunk = mm[offset:offset + chunk_size]
        # Look for the end of the source map JSON
        # It should end with } possibly followed by newline then different content
        # Let's look for a null byte which indicates end of the resource
        null_pos = chunk.find(b'\x00')
        if null_pos != -1:
            # Check if this is actually the end (and not a null inside the JSON)
            candidate = offset + null_pos
            # Source maps don't contain null bytes, so this should be the end
            # But first, back up to find the actual last byte of the JSON
            # Check previous non-null char
            while candidate > map_start and mm[candidate - 1:candidate] in (b'\x00', b'\n', b'\r', b' '):
                candidate -= 1
            map_end = candidate
            break

    if map_end == -1:
        print("ERROR: Could not find end of source map")
        sys.exit(1)

    map_size = map_end - map_start
    print(f"Source map: offset=0x{map_start:08X} to 0x{map_end:08X} ({map_size} bytes / {map_size/1024:.1f} KB)")
    print(f"First 100 bytes: {mm[map_start:map_start+100]}")
    print(f"Last 100 bytes: {mm[map_end-100:map_end]}")
    print()

    # Also find the CSS and JS to understand the resource layout
    css_marker = b':root [data-theme'
    css_start = mm.find(css_marker, 0x12AE0000)
    if css_start != -1:
        # Back up to find the actual start of the CSS resource
        # CSS files typically start with a comment or rule
        print(f"CSS theme block found at: 0x{css_start:08X}")

    # Find the JS bundle
    js_marker = b'rootapp-desktop-webrtc.js'
    # Already known it's referenced in the HTML, let's find the actual JS content
    # Vite bundles often start with a specific pattern
    # Let's search for the typical Vite module start

    # Check what's right after the HTML
    after_html = mm[html_end:html_end + 200]
    print(f"After HTML (200 bytes): {after_html[:100]}")
    print(f"... {after_html[100:200]}")

    mm.close()
