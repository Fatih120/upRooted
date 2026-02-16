import sys
data = sys.stdin.read()
outpath = "C:/Users/bash/claude/Root.Dev/scan_baml_colors.py"
with open(outpath, "w", encoding="utf-8") as out:
    out.write(data)
print("Written", len(data), "bytes to", outpath)
