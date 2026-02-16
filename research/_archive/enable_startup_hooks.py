"""Patch Root.exe to enable DOTNET_STARTUP_HOOKS by changing
   "System.StartupHookProvider.IsSupported": false
   to
   "System.StartupHookProvider.IsSupported": true
"""
import sys

EXE = r"C:\Users\bash\AppData\Local\Root\current\Root.exe"

SEARCH = b'"System.StartupHookProvider.IsSupported": false'
REPLACE = b'"System.StartupHookProvider.IsSupported": true '

assert len(SEARCH) == len(REPLACE), f"Length mismatch: {len(SEARCH)} vs {len(REPLACE)}"

with open(EXE, "rb") as f:
    data = f.read()

idx = data.find(SEARCH)
if idx == -1:
    # Maybe already patched?
    check = data.find(REPLACE)
    if check != -1:
        print(f"Already patched at offset 0x{check:08X}")
        sys.exit(0)
    print("ERROR: Pattern not found")
    sys.exit(1)

print(f"Found at offset 0x{idx:08X}")
print(f"Before: {data[idx:idx+len(SEARCH)]}")

data = data[:idx] + REPLACE + data[idx+len(SEARCH):]

print(f"After:  {data[idx:idx+len(REPLACE)]}")

with open(EXE, "wb") as f:
    f.write(data)

print("Patched successfully!")
