import sys
content = sys.stdin.read()
with open("C:/Users/bash/claude/Root.Dev/scan_baml_colors.py", "w", encoding="utf-8") as f:
    f.write(content)
print(f"Written {len(content)} bytes")
