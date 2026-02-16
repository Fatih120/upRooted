"""Fix serverAuditDocs.ts and update.ts to restore git config after successful push"""

def fix_audit_docs():
    path = "src/features/serverAuditDocs.ts"
    with open(path, "r") as f:
        lines = f.readlines()

    # Find the line with "Reset remote to not expose token" in the success path
    # We need to add restoration lines AFTER the execSync that resets the remote URL
    new_lines = []
    i = 0
    while i < len(lines):
        new_lines.append(lines[i])
        # Look for the remote reset line in success path
        if 'git remote set-url origin' in lines[i] and 'execSync' in lines[i]:
            # Check if the NEXT non-blank line is NOT already a restore line
            j = i + 1
            while j < len(lines) and lines[j].strip() == '':
                j += 1
            if j < len(lines) and 'Restore git config' not in lines[j]:
                new_lines.append('\n')
                new_lines.append('    // Restore git config to repo owner after push\n')
                new_lines.append("    execSync('git config user.name \"watchthelight\"', { cwd });\n")
                new_lines.append("    execSync('git config user.email \"admin@watchthelight.org\"', { cwd });\n")
        i += 1

    with open(path, "w") as f:
        f.writelines(new_lines)
    print(f"Fixed {path}")


def fix_update():
    path = "src/commands/update.ts"
    with open(path, "r") as f:
        lines = f.readlines()

    new_lines = []
    i = 0
    while i < len(lines):
        new_lines.append(lines[i])
        if 'git remote set-url origin' in lines[i] and 'execSync' in lines[i]:
            j = i + 1
            while j < len(lines) and lines[j].strip() == '':
                j += 1
            if j < len(lines) and 'Restore git config' not in lines[j]:
                new_lines.append('\n')
                new_lines.append('    // Restore git config to repo owner after push\n')
                new_lines.append("    execSync('git config user.name \"watchthelight\"', { cwd });\n")
                new_lines.append("    execSync('git config user.email \"admin@watchthelight.org\"', { cwd });\n")
        i += 1

    with open(path, "w") as f:
        f.writelines(new_lines)
    print(f"Fixed {path}")


fix_audit_docs()
fix_update()
