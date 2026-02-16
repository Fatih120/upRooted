"""Find embedded WebRTC resource byte ranges in Root.exe."""
import mmap
import re
import sys

EXE = r"C:\Users\bash\AppData\Local\Root\current\Root.exe"

with open(EXE, "rb") as f:
    mm = mmap.mmap(f.fileno(), 0, access=mmap.ACCESS_READ)

    # Search for ANY <!DOCTYPE html> near the expected region
    print("=== Searching for <!DOCTYPE html> ===")
    for m in re.finditer(rb'<!DOCTYPE html>', mm):
        offset = m.start()
        # Show context
        end = mm.find(b'</html>', offset)
        if end != -1:
            end += 7
            size = end - offset
            if size < 2000:  # Only show small HTML files
                content = mm[offset:end].decode('utf-8', errors='replace')
                print(f"\nOffset: 0x{offset:08X} ({size} bytes)")
                print(content[:500])
                print()

    # Search for source map
    print("\n=== Searching for source maps ===")
    map_marker = b'{"version":3'
    pos = 0
    count = 0
    while count < 5:
        idx = mm.find(map_marker, pos)
        if idx == -1:
            break
        # Find size by looking for null byte or next resource
        end = idx
        for probe in range(idx, min(idx + 100*1024*1024, len(mm))):
            if mm[probe:probe+1] == b'\x00':
                end = probe
                break
        size = end - idx
        print(f"Source map at 0x{idx:08X}, ~{size} bytes ({size/1024:.0f} KB)")
        print(f"  First 80 chars: {mm[idx:idx+80]}")
        pos = idx + max(size, 1)
        count += 1

    # Search for Root WebRTC title
    print("\n=== Searching for 'Root WebRTC' ===")
    for m in re.finditer(rb'Root WebRTC', mm):
        offset = m.start()
        ctx = mm[max(0, offset-50):offset+50].decode('utf-8', errors='replace')
        print(f"  0x{offset:08X}: ...{ctx}...")

    mm.close()
