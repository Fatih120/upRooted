"""
Hex-patch Root.exe to replace Avalonia/XAML color constants with forest green equivalents.

The Root.exe .NET single-file bundle contains compiled Avalonia UI resources with
hardcoded color values (stored as UTF-8 hex strings like "#0D1521" in BAML/AXAML).
This script finds and replaces those color strings directly in the binary.

IMPORTANT: Creates a backup before patching. Use revert_forest_theme.py to restore.
"""
import os
import shutil
import struct
import sys
import time
import io

# Force UTF-8 output on Windows
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8', errors='replace')
sys.stderr = io.TextIOWrapper(sys.stderr.buffer, encoding='utf-8', errors='replace')

ROOT_EXE = os.path.expandvars(r"%LOCALAPPDATA%\Root\current\Root.exe")
BACKUP_DIR = os.path.expandvars(r"%USERPROFILE%\claude\Root_APP_BACKUP_20260212_172827")
BACKUP_EXE = os.path.join(BACKUP_DIR, "Root.exe")

# ── Color replacement map ─────────────────────────────────────────────
# Each entry: (original_hex, replacement_hex, description)
# These are the known Avalonia dark theme colors → forest green equivalents
# Format: 7-char hex strings "#RRGGBB" as they appear in XAML/BAML resources

COLOR_MAP_HEX_STRINGS = [
    # Backgrounds
    ("#0D1521", "#040D04", "bg-primary (dark blue → dark green)"),
    ("#121A26", "#081408", "bg-secondary"),
    ("#07101B", "#020802", "bg-tertiary"),
    ("#090E13", "#051005", "input background"),

    # Brand colors
    ("#3B6AF8", "#1B8A4A", "brand-primary (blue → emerald)"),
    ("#A8FF5D", "#4ADE80", "brand-secondary (lime → green)"),

    # Borders
    ("#242C36", "#14291A", "border color"),

    # Text colors
    # NOTE: #F2F2F2 is very common in binaries — we use context-aware matching
    # to only replace it near other theme colors
    ("#A7A7A8", "#8FA88F", "text-secondary (gray → green-gray)"),

    # UI elements
    ("#4F5C6F", "#2D5A3A", "muted elements"),
    ("#88A5FF", "#34D399", "link/accent blue → emerald"),

    # Text and status colors
    ("#F2F2F2", "#D4E8D4", "text-primary (white -> green-white)"),
    ("#F03F36", "#DC2626", "error red (adjust hue)"),
]

# Additional ARGB binary patterns (4-byte: Alpha, Red, Green, Blue)
# Only replace distinctive 4-byte sequences that won't cause false positives
COLOR_MAP_ARGB = [
    # (original_argb_bytes, replacement_argb_bytes, description)
    # Format: (b'\xFF\xRR\xGG\xBB', b'\xFF\xRR\xGG\xBB', desc)
    (b'\xFF\x3B\x6A\xF8', b'\xFF\x1B\x8A\x4A', "brand-primary ARGB"),
    (b'\xFF\xA8\xFF\x5D', b'\xFF\x4A\xDE\x80', "brand-secondary ARGB"),
    (b'\xFF\x88\xA5\xFF', b'\xFF\x34\xD3\x99', "link blue ARGB"),
]

# 6-char hex without # (sometimes stored this way)
COLOR_MAP_NO_HASH = [
    ("0D1521", "040D04", "bg-primary no-hash"),
    ("121A26", "081408", "bg-secondary no-hash"),
    ("3B6AF8", "1B8A4A", "brand-primary no-hash"),
    ("A8FF5D", "4ADE80", "brand-secondary no-hash"),
    ("242C36", "14291A", "border no-hash"),
    ("88A5FF", "34D399", "link blue no-hash"),
]


def scan_for_color(data, pattern, context_size=32):
    """Find all occurrences of a byte pattern in data, with context."""
    positions = []
    start = 0
    while True:
        idx = data.find(pattern, start)
        if idx == -1:
            break
        positions.append(idx)
        start = idx + 1
    return positions


def patch_binary():
    if not os.path.exists(ROOT_EXE):
        print(f"ERROR: Root.exe not found at: {ROOT_EXE}")
        sys.exit(1)

    file_size = os.path.getsize(ROOT_EXE)
    print(f"Root.exe: {ROOT_EXE}")
    print(f"Size: {file_size / (1024*1024):.1f} MB")
    print()

    # Check for existing backup
    if os.path.exists(BACKUP_EXE):
        backup_size = os.path.getsize(BACKUP_EXE)
        print(f"Backup exists: {BACKUP_EXE} ({backup_size / (1024*1024):.1f} MB)")
    else:
        # Create backup
        print(f"Creating backup...")
        os.makedirs(BACKUP_DIR, exist_ok=True)
        shutil.copy2(ROOT_EXE, BACKUP_EXE)
        print(f"Backed up to: {BACKUP_EXE}")
    print()

    # Read the entire binary
    print("Reading Root.exe into memory...")
    with open(ROOT_EXE, "rb") as f:
        data = bytearray(f.read())
    print(f"Loaded {len(data)} bytes")
    print()

    total_replacements = 0

    # ── Phase 1: UTF-8 hex string replacements (#RRGGBB) ─────────────
    print("Phase 1: UTF-8 hex string replacements (#RRGGBB)")
    print("-" * 55)
    for old_hex, new_hex, desc in COLOR_MAP_HEX_STRINGS:
        old_bytes = old_hex.encode('utf-8')
        new_bytes = new_hex.encode('utf-8')
        assert len(old_bytes) == len(new_bytes), f"Length mismatch: {old_hex} vs {new_hex}"

        positions = scan_for_color(data, old_bytes)
        count = len(positions)

        if count > 0:
            # Replace all occurrences
            for pos in reversed(positions):  # Reverse to maintain positions
                data[pos:pos + len(old_bytes)] = new_bytes
            total_replacements += count
            print(f"  {old_hex} → {new_hex}  ({count:3d} hits) — {desc}")
        else:
            print(f"  {old_hex} → {new_hex}  (  0 hits) — {desc} [not found]")

    print()

    # ── Phase 2: Case-insensitive hex strings ─────────────────────────
    print("Phase 2: Lowercase hex variants")
    print("-" * 55)
    for old_hex, new_hex, desc in COLOR_MAP_HEX_STRINGS:
        old_lower = old_hex.lower().encode('utf-8')
        new_lower = new_hex.lower().encode('utf-8')
        # Skip if same as original (already lowercase)
        if old_lower == old_hex.encode('utf-8'):
            continue

        positions = scan_for_color(data, old_lower)
        count = len(positions)
        if count > 0:
            for pos in reversed(positions):
                data[pos:pos + len(old_lower)] = new_lower
            total_replacements += count
            print(f"  {old_hex.lower()} → {new_hex.lower()}  ({count:3d} hits) — {desc}")

    print()

    # ── Phase 3: No-hash hex strings ──────────────────────────────────
    print("Phase 3: No-hash hex string variants (RRGGBB)")
    print("-" * 55)
    for old_hex, new_hex, desc in COLOR_MAP_NO_HASH:
        # Only replace if preceded by a non-hex character (to avoid false positives)
        old_bytes = old_hex.encode('utf-8')
        new_bytes = new_hex.encode('utf-8')
        positions = scan_for_color(data, old_bytes)

        safe_count = 0
        for pos in reversed(positions):
            # Check that this isn't part of a longer hex string or already has #
            if pos > 0 and data[pos - 1:pos] == b'#':
                continue  # Already handled in Phase 1
            # Check context — should be near other theme-related bytes
            # (quotes, XML tags, color keywords, etc.)
            context_before = data[max(0, pos - 16):pos]
            # Only replace if in a string context (near quotes, =, :, or color keywords)
            if any(c in context_before for c in [ord('"'), ord("'"), ord(':'), ord('='), ord(',')]):
                data[pos:pos + len(old_bytes)] = new_bytes
                safe_count += 1

        if safe_count > 0:
            total_replacements += safe_count
            print(f"  {old_hex} → {new_hex}  ({safe_count:3d} safe hits) — {desc}")

    print()

    # ── Phase 4: ARGB binary patterns ─────────────────────────────────
    print("Phase 4: ARGB binary color values (4-byte)")
    print("-" * 55)
    for old_argb, new_argb, desc in COLOR_MAP_ARGB:
        positions = scan_for_color(data, old_argb)
        count = len(positions)
        if count > 0:
            # Only replace if count is reasonable (< 500 to avoid false positives)
            if count < 500:
                for pos in reversed(positions):
                    data[pos:pos + 4] = new_argb
                total_replacements += count
                print(f"  {old_argb.hex()} → {new_argb.hex()}  ({count:3d} hits) — {desc}")
            else:
                print(f"  {old_argb.hex()} → {new_argb.hex()}  ({count:3d} hits) — {desc} [SKIPPED: too many, likely false positives]")
        else:
            print(f"  {old_argb.hex()} → {new_argb.hex()}  (  0 hits) — {desc} [not found]")

    print()
    print(f"Total replacements: {total_replacements}")

    if total_replacements == 0:
        print("\nNo color patterns found to replace. The binary may already be patched")
        print("or uses a different color encoding. No changes written.")
        return

    # Write patched binary
    print(f"\nWriting patched Root.exe ({len(data)} bytes)...")
    with open(ROOT_EXE, "wb") as f:
        f.write(data)
    print("Done!")
    print(f"\nRestart Root.exe to see the patched Avalonia sidebar/chrome.")
    print(f"To restore original: python revert_forest_theme.py (restores from backup)")


def scan_only():
    """Just scan and report what colors exist — no modifications."""
    if not os.path.exists(ROOT_EXE):
        print(f"ERROR: Root.exe not found at: {ROOT_EXE}")
        sys.exit(1)

    print(f"Scanning Root.exe for known color patterns...")
    print(f"File: {ROOT_EXE} ({os.path.getsize(ROOT_EXE) / (1024*1024):.1f} MB)")
    print()

    with open(ROOT_EXE, "rb") as f:
        data = f.read()

    print("UTF-8 hex strings (#RRGGBB):")
    print("-" * 55)
    for old_hex, new_hex, desc in COLOR_MAP_HEX_STRINGS:
        old_bytes = old_hex.encode('utf-8')
        positions = scan_for_color(data, old_bytes)
        if positions:
            print(f"  {old_hex}  {len(positions):3d} occurrences — {desc}")
            # Show first few positions with context
            for pos in positions[:3]:
                ctx = data[max(0, pos - 8):pos + len(old_bytes) + 8]
                # Show printable context
                ctx_str = ''.join(chr(b) if 32 <= b < 127 else '.' for b in ctx)
                print(f"    @ 0x{pos:08X}: ...{ctx_str}...")

    print()
    print("ARGB binary patterns:")
    print("-" * 55)
    for old_argb, new_argb, desc in COLOR_MAP_ARGB:
        positions = scan_for_color(data, old_argb)
        if positions:
            print(f"  {old_argb.hex()}  {len(positions):3d} occurrences — {desc}")


def main():
    print("Forest Green Theme — Binary Patcher for Root.exe")
    print("=" * 50)
    print()

    if "--scan" in sys.argv:
        scan_only()
    elif "--help" in sys.argv:
        print("Usage:")
        print("  python theme_patch_binary.py          Patch Root.exe with forest green colors")
        print("  python theme_patch_binary.py --scan    Scan only, report found colors (no changes)")
        print("  python theme_patch_binary.py --help    Show this help")
    else:
        patch_binary()


if __name__ == "__main__":
    main()
