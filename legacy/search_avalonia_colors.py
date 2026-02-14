import mmap
import struct
import os
import sys

BINARY_PATH = r"C:\Users\bash\AppData\Local\Root\current\Root.exe"

def hex_dump(data, base_offset=0, width=16):
    """Produce a hex dump with ASCII sidebar."""
    lines = []
    for i in range(0, len(data), width):
        chunk = data[i:i+width]
        hex_part = ' '.join(f'{b:02X}' for b in chunk)
        ascii_part = ''.join(chr(b) if 32 <= b < 127 else '.' for b in chunk)
        lines.append(f"  {base_offset + i:010X}  {hex_part:<{width*3}}  |{ascii_part}|")
    return '\n'.join(lines)

def find_all(mm, needle, max_results=None):
    """Find all occurrences of needle in mmap."""
    positions = []
    pos = 0
    while True:
        pos = mm.find(needle, pos)
        if pos == -1:
            break
        positions.append(pos)
        pos += 1
        if max_results and len(positions) >= max_results:
            break
    return positions

def main():
    file_size = os.path.getsize(BINARY_PATH)
    print(f"Opening {BINARY_PATH}")
    print(f"File size: {file_size:,} bytes ({file_size / (1024*1024):.1f} MB)")
    print("=" * 80)

    with open(BINARY_PATH, 'rb') as f:
        mm = mmap.mmap(f.fileno(), 0, access=mmap.ACCESS_READ)

        # ====================================================================
        # 1. Avalonia Fluent theme resource names (UTF-8)
        # ====================================================================
        print("\n" + "=" * 80)
        print("1. AVALONIA FLUENT THEME RESOURCE NAMES (UTF-8 strings)")
        print("=" * 80)

        resource_names = [
            "TextFillColorPrimary",
            "ControlFillColorDefault",
            "SystemFillColorCritical",
            "AccentFillColorDefault",
            "SubtleFillColorSecondary",
        ]

        for name in resource_names:
            needle = name.encode('utf-8')
            positions = find_all(mm, needle)
            print(f"\n  '{name}' — {len(positions)} hit(s)")
            for pos in positions:
                start = max(0, pos - 128)
                end = min(len(mm), pos + len(needle) + 128)
                context = mm[start:end]
                print(f"    Offset: 0x{pos:010X} ({pos:,})")
                print(hex_dump(context, start))

        # Also search UTF-16LE versions
        print("\n  --- Also checking UTF-16LE encoding ---")
        for name in resource_names:
            needle = name.encode('utf-16-le')
            positions = find_all(mm, needle)
            print(f"\n  '{name}' (UTF-16LE) — {len(positions)} hit(s)")
            for pos in positions[:5]:
                start = max(0, pos - 64)
                end = min(len(mm), pos + len(needle) + 64)
                context = mm[start:end]
                print(f"    Offset: 0x{pos:010X} ({pos:,})")
                print(hex_dump(context, start))

        # ====================================================================
        # 2. Raw ARGB 4-byte color values
        # ====================================================================
        print("\n" + "=" * 80)
        print("2. RAW ARGB COLOR VALUES (4 bytes)")
        print("=" * 80)

        argb_colors = {
            "FF0D1521 (bg primary #0D1521)":   bytes([0xFF, 0x0D, 0x15, 0x21]),
            "FF121A26 (bg secondary #121A26)":  bytes([0xFF, 0x12, 0x1A, 0x26]),
            "FF07101B (bg tertiary #07101B)":   bytes([0xFF, 0x07, 0x10, 0x1B]),
            "FF090E13 (input bg #090E13)":      bytes([0xFF, 0x09, 0x0E, 0x13]),
        }

        for label, needle in argb_colors.items():
            positions = find_all(mm, needle, max_results=500)
            print(f"\n  {label} — {len(positions)} hit(s)")
            for pos in positions[:10]:
                start = max(0, pos - 16)
                end = min(len(mm), pos + 20)
                context = mm[start:end]
                print(f"    0x{pos:010X}: {hex_dump(context, start).strip()}")

        # ====================================================================
        # 3. BGRA byte order (reversed)
        # ====================================================================
        print("\n" + "=" * 80)
        print("3. BGRA COLOR VALUES (reversed byte order)")
        print("=" * 80)

        bgra_colors = {
            "21150DFF (bg primary BGRA)":   bytes([0x21, 0x15, 0x0D, 0xFF]),
            "261A12FF (bg secondary BGRA)": bytes([0x26, 0x1A, 0x12, 0xFF]),
            "1B1007FF (bg tertiary BGRA)":  bytes([0x1B, 0x10, 0x07, 0xFF]),
            "130E09FF (input bg BGRA)":     bytes([0x13, 0x0E, 0x09, 0xFF]),
        }

        for label, needle in bgra_colors.items():
            positions = find_all(mm, needle, max_results=500)
            print(f"\n  {label} — {len(positions)} hit(s)")
            for pos in positions[:10]:
                start = max(0, pos - 16)
                end = min(len(mm), pos + 20)
                context = mm[start:end]
                print(f"    0x{pos:010X}: {hex_dump(context, start).strip()}")

        # ====================================================================
        # 4. Avalonia-specific strings
        # ====================================================================
        print("\n" + "=" * 80)
        print("4. AVALONIA-SPECIFIC STRINGS")
        print("=" * 80)

        avalonia_strings = [
            "SolidColorBrush",
            "Color x:Key",
            "ThemeVariant",
            "FluentTheme",
            "ColorPaletteResources",
            "DynamicResource",
            "StaticResource",
            "ResourceDictionary",
            "avares://",
            "Avalonia.Themes.Fluent",
            "SystemAccentColor",
            "ThemeBackgroundColor",
            "ThemeControlMidColor",
        ]

        for s in avalonia_strings:
            # UTF-8
            positions_utf8 = find_all(mm, s.encode('utf-8'), max_results=50)
            # UTF-16LE
            positions_utf16 = find_all(mm, s.encode('utf-16-le'), max_results=50)

            total = len(positions_utf8) + len(positions_utf16)
            if total > 0:
                print(f"\n  '{s}' — UTF-8: {len(positions_utf8)}, UTF-16LE: {len(positions_utf16)}")
                for pos in positions_utf8[:5]:
                    start = max(0, pos - 32)
                    end = min(len(mm), pos + len(s) + 64)
                    context = mm[start:end]
                    print(f"    [UTF-8] 0x{pos:010X}:")
                    print(hex_dump(context, start))
                for pos in positions_utf16[:5]:
                    start = max(0, pos - 32)
                    end = min(len(mm), pos + len(s)*2 + 64)
                    context = mm[start:end]
                    print(f"    [UTF-16LE] 0x{pos:010X}:")
                    print(hex_dump(context, start))
            else:
                print(f"  '{s}' — NOT FOUND")

        # ====================================================================
        # 5. Hex dump around 0x1B683000
        # ====================================================================
        print("\n" + "=" * 80)
        print("5. HEX DUMP AT 0x1B683000 (4KB)")
        print("=" * 80)

        target_offset = 0x1B683000
        if target_offset < len(mm):
            end = min(len(mm), target_offset + 4096)
            data = mm[target_offset:end]
            print(f"  Reading {end - target_offset} bytes from 0x{target_offset:010X}:")
            print(hex_dump(data, target_offset))
        else:
            print(f"  Offset 0x{target_offset:010X} is beyond file size (0x{len(mm):010X})")

        mm.close()

    print("\n" + "=" * 80)
    print("SEARCH COMPLETE")
    print("=" * 80)

if __name__ == '__main__':
    main()
