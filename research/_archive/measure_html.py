"""Measure exact HTML size in binary."""
import mmap

EXE = r"C:\Users\bash\AppData\Local\Root\current\Root.exe"

with open(EXE, "rb") as f:
    mm = mmap.mmap(f.fileno(), 0, access=mmap.ACCESS_READ)
    start = 0x12AE8A95
    end_tag = b"</html>"
    end = mm.find(end_tag, start)
    if end != -1:
        end += len(end_tag)
        size = end - start
        content = mm[start:end]
        print(f"Offset: 0x{start:08X} to 0x{end:08X}")
        print(f"Size: {size} bytes")
        print(f"Content (repr):\n{repr(content)}")
        # Check what's right after
        after = mm[end:end+20]
        print(f"\nAfter </html>: {repr(after)}")
    mm.close()
