import sys
content = sys.stdin.read()
with open("C:/Users/bash/claude/Root.Dev/scan_baml_colors.py", "a", encoding="utf-8") as f:
    f.write(content)
print(f"Appended {len(content)} bytes")
