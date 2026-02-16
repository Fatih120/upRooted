"""Verify the binary patch was applied correctly."""
import mmap

EXE = r"C:\Users\bash\AppData\Local\Root\current\Root.exe"

with open(EXE, "rb") as f:
    mm = mmap.mmap(f.fileno(), 0, access=mmap.ACCESS_READ)

    # Check HTML
    html = mm[0x12AE8A95:0x12AE8A95 + 450]
    print("=== Patched HTML (450 bytes) ===")
    print(html.decode('utf-8', errors='replace'))
    print()

    # Check start of preload payload
    payload_start = mm[0x12F08F73:0x12F08F73 + 200]
    print("=== Preload payload (first 200 bytes) ===")
    print(payload_start.decode('utf-8', errors='replace'))
    print()

    # Check the preload contains key identifiers
    payload_region = mm[0x12F08F73:0x12F08F73 + 30000]
    checks = [
        b'uprooted-settings-toggle',
        b'__UPROOTED_SETTINGS__',
        b'setPluginLoader',
        b'uprooted-settings-panel',
        b'uprooted-injected-css',
    ]
    print("=== Content checks ===")
    for check in checks:
        found = check in payload_region
        print(f"  {'[OK]' if found else '[MISSING]'} {check.decode()}")

    mm.close()
