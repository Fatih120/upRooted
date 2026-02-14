import mmap
import os
import sys

BINARY = r"C:\Users\bash\claude\Root_APP_BACKUP_20260212_172827\Root.exe"
FILE_SIZE = os.path.getsize(BINARY)

def show_context_at(mm, offset, context=200):
    start = max(0, offset - context)
    end = min(len(mm), offset + context)
    chunk = mm[start:end]
    ascii_repr = ""
    for b in chunk:
        if 32 <= b < 127:
            ascii_repr += chr(b)
        else:
            ascii_repr += "."
    line_width = 120
    for i in range(0, len(ascii_repr), line_width):
        line = ascii_repr[i:i+line_width]
        addr = start + i
        print(f"  {addr:08X}: {line}")

def search_pattern(mm, pattern, max_hits=None, start=0, end=None):
    if end is None:
        end = len(mm)
    results = []
    pos = start
    while True:
        idx = mm.find(pattern, pos, end)
        if idx == -1:
            break
        results.append(idx)
        pos = idx + 1
        if max_hits and len(results) >= max_hits:
            break
    return results

print(f"Opening {BINARY} ({FILE_SIZE:,} bytes)")
sep = "=" * 100

with open(BINARY, "rb") as f:
    mm = mmap.mmap(f.fileno(), 0, access=mmap.ACCESS_READ)

    # 1. ThemeCss
    print(f"\n{sep}")
    print("1. SEARCH: ThemeCss (UTF-8) - first 10 hits")
    print(sep)
    hits = search_pattern(mm, b"ThemeCss", max_hits=10)
    print(f"   Found {len(hits)} hits")
    for i, off in enumerate(hits):
        print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
        show_context_at(mm, off, 200)

    # 2. SetTheme / ApplyTheme / LoadTheme
    print(f"\n{sep}")
    print("2. SEARCH: SetTheme / ApplyTheme / LoadTheme")
    print(sep)
    for pat in [b"SetTheme", b"ApplyTheme", b"LoadTheme"]:
        hits = search_pattern(mm, pat, max_hits=20)
        pname = pat.decode()
        print(f"\n   {pname}: {len(hits)} hits")
        for i, off in enumerate(hits):
            print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
            show_context_at(mm, off, 200)

    # 3. ThemeVariant
    print(f"\n{sep}")
    print("3. SEARCH: ThemeVariant - first 5 hits")
    print(sep)
    hits = search_pattern(mm, b"ThemeVariant", max_hits=5)
    print(f"   Found {len(hits)} hits")
    for i, off in enumerate(hits):
        print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
        show_context_at(mm, off, 200)

    # 4. pure-dark and dark in RootThemeCss region
    print(f"\n{sep}")
    print("4. SEARCH: pure-dark / dark in RootThemeCss region 0x19D73C77-0x19F14CBC")
    print(sep)
    TS = 0x19D73C77
    TE = 0x19F14CBC
    for pat in [b"pure-dark", b"dark"]:
        hits = search_pattern(mm, pat, max_hits=20, start=TS, end=TE)
        pname = pat.decode()
        print(f"\n   {pname}: {len(hits)} hits in region")
        for i, off in enumerate(hits[:10]):
            print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
            show_context_at(mm, off, 150)
        if len(hits) > 10:
            print(f"   ... and {len(hits) - 10} more hits")

    # 5. #0D1521
    print(f"\n{sep}")
    print("5. SEARCH: #0D1521 in .NET region 0x19B00000-0x1A000000 and whole binary")
    print(sep)
    NS = 0x19B00000
    NE = min(0x1A000000, len(mm))
    hits = search_pattern(mm, b"#0D1521", max_hits=20, start=NS, end=NE)
    print(f"   Found {len(hits)} hits in .NET region")
    for i, off in enumerate(hits):
        print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
        show_context_at(mm, off, 200)
    hits_all = search_pattern(mm, b"#0D1521", max_hits=20)
    print(f"\n   Found {len(hits_all)} hits in entire binary")
    for i, off in enumerate(hits_all):
        print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
        show_context_at(mm, off, 200)

    # 6. background-color / background in RootThemeCss region
    print(f"\n{sep}")
    print("6. SEARCH: background-color / background: in RootThemeCss region")
    print(sep)
    hits = search_pattern(mm, b"background-color", max_hits=30, start=TS, end=TE)
    print(f"\n   background-color: {len(hits)} hits")
    for i, off in enumerate(hits[:15]):
        print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
        show_context_at(mm, off, 150)
    if len(hits) > 15:
        print(f"   ... and {len(hits) - 15} more hits")
    hits = search_pattern(mm, b"background:", max_hits=30, start=TS, end=TE)
    print(f"\n   background:: {len(hits)} hits")
    for i, off in enumerate(hits[:15]):
        print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
        show_context_at(mm, off, 100)
    if len(hits) > 15:
        print(f"   ... and {len(hits) - 15} more hits")

    # 7. css/CSS/stylesheet near ThemeCss
    print(f"\n{sep}")
    print("7. SEARCH: css/CSS/stylesheet/InjectCss etc near ThemeCss and whole binary")
    print(sep)
    WS = max(0, TS - 0x100000)
    WE = min(len(mm), TE + 0x100000)
    for pat in [b"stylesheet", b"StyleSheet", b"Stylesheet", b"cssText", b"CssText"]:
        hits = search_pattern(mm, pat, max_hits=10, start=WS, end=WE)
        pname = pat.decode()
        print(f"\n   {pname} (near theme): {len(hits)} hits")
        for i, off in enumerate(hits[:5]):
            print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
            show_context_at(mm, off, 150)
    for pat in [b"InjectCss", b"InsertCss", b"injectCss", b"insertCss", b"AddCss", b"addCss"]:
        hits = search_pattern(mm, pat, max_hits=10)
        pname = pat.decode()
        print(f"\n   {pname} (whole binary): {len(hits)} hits")
        for i, off in enumerate(hits[:5]):
            print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
            show_context_at(mm, off, 150)

    # 8. File paths near theme region
    print(f"\n{sep}")
    print("8. SEARCH: .css / .xaml / .axaml near theme region and theme file paths")
    print(sep)
    for pat in [b".css", b".xaml", b".axaml"]:
        hits = search_pattern(mm, pat, max_hits=50, start=WS, end=WE)
        pname = pat.decode()
        print(f"\n   {pname}: {len(hits)} hits in wide theme region")
        for i, off in enumerate(hits[:10]):
            print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
            show_context_at(mm, off, 150)
        if len(hits) > 10:
            print(f"   ... and {len(hits) - 10} more hits")

    print("\n--- Searching whole binary for theme-related file paths ---")
    for pat in [b"theme.css", b"Theme.css", b"theme.axaml", b"Theme.axaml",
                b"ThemeStyles", b"ThemeResource", b"RootTheme"]:
        hits = search_pattern(mm, pat, max_hits=20)
        pname = pat.decode()
        print(f"\n   {pname} (whole binary): {len(hits)} hits")
        for i, off in enumerate(hits[:5]):
            print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
            show_context_at(mm, off, 200)
        if len(hits) > 5:
            print(f"   ... and {len(hits) - 5} more hits")

    # BONUS
    print(f"\n{sep}")
    print("BONUS: Theme application methods and DotNetBrowser CSS injection")
    print(sep)
    for pat in [b"ExecuteJavaScript", b"InjectJs", b"EvaluateScript",
                b"DomContentLoaded", b"FrameLoadFinished",
                b"ThemeCssChanged", b"OnTheme", b"UpdateTheme",
                b"RootThemeCss", b"themeCss", b"theme_css"]:
        hits = search_pattern(mm, pat, max_hits=10)
        pname = pat.decode()
        print(f"\n   {pname} (whole binary): {len(hits)} hits")
        for i, off in enumerate(hits[:5]):
            print(f"\n--- Hit {i+1} at 0x{off:08X} ---")
            show_context_at(mm, off, 200)
        if len(hits) > 5:
            print(f"   ... and {len(hits) - 5} more hits")

    mm.close()

print(f"\n{sep}")
print("SEARCH COMPLETE")
print(sep)
