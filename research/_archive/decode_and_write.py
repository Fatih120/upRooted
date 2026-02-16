import base64, os
# This script decodes and writes scan_baml_colors.py
b64_data = ""
outpath = "C:/Users/bash/claude/Root.Dev/scan_baml_colors.py"
with open(outpath, "w") as out:
    out.write(base64.b64decode(b64_data).decode())
