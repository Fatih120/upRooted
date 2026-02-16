"""Search Root.exe for StartupHook configuration."""
import mmap
import re

EXE = r"C:\Users\bash\AppData\Local\Root\current\Root.exe"

with open(EXE, "rb") as f:
    mm = mmap.mmap(f.fileno(), 0, access=mmap.ACCESS_READ)

    # Search for StartupHook related strings
    patterns = [
        b'StartupHook',
        b'System.StartupHookProvider.IsSupported',
        b'startupHook',
        b'STARTUP_HOOKS',
    ]

    for pat in patterns:
        print(f"\n=== Searching for: {pat.decode()} ===")
        pos = 0
        count = 0
        while count < 10:
            idx = mm.find(pat, pos)
            if idx == -1:
                break
            # Show context
            start = max(0, idx - 100)
            end = min(len(mm), idx + len(pat) + 100)
            ctx = mm[start:end]
            print(f"  Offset 0x{idx:08X}:")
            # Try to show as text
            try:
                text = ctx.decode('utf-8', errors='replace')
                print(f"    {repr(text)}")
            except:
                print(f"    {ctx[:80]}")
            pos = idx + 1
            count += 1
        if count == 0:
            print("  Not found")

    # Also search for runtimeconfig.json content
    print(f"\n=== Searching for runtimeconfig.json ===")
    for pat in [b'"runtimeOptions"', b'"configProperties"', b'runtimeconfig']:
        pos = 0
        count = 0
        while count < 5:
            idx = mm.find(pat, pos)
            if idx == -1:
                break
            start = max(0, idx - 50)
            end = min(len(mm), idx + 200)
            ctx = mm[start:end].decode('utf-8', errors='replace')
            print(f"  0x{idx:08X}: ...{ctx[:200]}...")
            pos = idx + 1
            count += 1
        if count == 0:
            print(f"  {pat}: Not found")

    mm.close()
