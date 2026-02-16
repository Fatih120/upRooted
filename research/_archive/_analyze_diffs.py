import re

data = open(r"C:\Users\bash\.claude\projects\C--Users-bash-claude-Root-Dev\957a9e2b-d8f5-4cef-b7d7-15ae2df78e09\tool-results\toolu_012McbCdyDsDBF8SWdQ3V2vC.txt", encoding="utf-8", errors="replace").read()
lines = data.split("\n")

seen = set()
prev_backup = None
for line in lines:
    stripped = line.strip()
    if stripped.startswith("BACKUP:") and "|" in stripped:
        prev_backup = stripped
    elif stripped.startswith("CURRENT:") and "|" in stripped and prev_backup is not None:
        b_colors = re.findall(r"#[0-9A-Fa-f]{6,8}", prev_backup)
        c_colors = re.findall(r"#[0-9A-Fa-f]{6,8}", line)
        for i in range(min(len(b_colors), len(c_colors))):
            bc = b_colors[i]
            cc = c_colors[i]
            if bc != cc:
                seen.add((bc, cc))
        prev_backup = None
    else:
        prev_backup = None

print("UNIQUE COLOR STRING CHANGES (BACKUP -> CURRENT):")
print()
for bc, cc in sorted(seen):
    print(f"  {bc:20s} -> {cc}")
print()
print(f"Total unique: {len(seen)}")
