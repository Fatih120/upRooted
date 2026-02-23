#!/usr/bin/env bash
# bump-version.sh — Sync all version references from the VERSION file.
#
# Usage:
#   ./scripts/bump-version.sh              # Read version from VERSION file
#   ./scripts/bump-version.sh 0.6.0        # Override with explicit version
#
# Updates:
#   - VERSION (if explicit arg given)
#   - package.json
#   - installer/src-tauri/Cargo.toml
#   - hook/UprootedSettings.cs
#   - hook/StartupHook.cs
#   - hook/ContentPages.cs (plugin versions)
#   - install-uprooted-linux.sh
#   - site/src/pages/index.astro

set -euo pipefail

REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"

# Resolve version
if [ $# -ge 1 ]; then
    VERSION="$1"
    printf '%s\n' "$VERSION" > "$REPO_ROOT/VERSION"
    echo "Set VERSION file to $VERSION"
else
    VERSION="$(tr -d '[:space:]' < "$REPO_ROOT/VERSION")"
fi

if [ -z "$VERSION" ]; then
    echo "ERROR: No version found. Pass as argument or populate VERSION file." >&2
    exit 1
fi

echo "Bumping all version references to: $VERSION"

# 1. package.json
if command -v jq &>/dev/null; then
    tmp=$(mktemp)
    jq --arg v "$VERSION" '.version = $v' "$REPO_ROOT/package.json" > "$tmp"
    mv "$tmp" "$REPO_ROOT/package.json"
else
    sed -i "s/\"version\": \"[^\"]*\"/\"version\": \"$VERSION\"/" "$REPO_ROOT/package.json"
fi
echo "  updated package.json"

# 2. installer/src-tauri/Cargo.toml (first version = line under [package])
sed -i "0,/^version = \"[^\"]*\"/s//version = \"$VERSION\"/" "$REPO_ROOT/installer/src-tauri/Cargo.toml"
echo "  updated installer/src-tauri/Cargo.toml"

# 3. hook/UprootedSettings.cs
sed -i "s/Version { get; set; } = \"[^\"]*\"/Version { get; set; } = \"$VERSION\"/" \
    "$REPO_ROOT/hook/UprootedSettings.cs"
echo "  updated hook/UprootedSettings.cs"

# 4. hook/StartupHook.cs
sed -i "s/CurrentVersion = \"[^\"]*\"/CurrentVersion = \"$VERSION\"/" \
    "$REPO_ROOT/hook/StartupHook.cs"
echo "  updated hook/StartupHook.cs"

# 5. hook/ContentPages.cs — plugin version strings
sed -i "s/Version = \"[0-9][^\"]*\"/Version = \"$VERSION\"/g" \
    "$REPO_ROOT/hook/ContentPages.cs"
echo "  updated hook/ContentPages.cs"

# 6. install-uprooted-linux.sh
sed -i "s/^VERSION=\"[^\"]*\"/VERSION=\"$VERSION\"/" \
    "$REPO_ROOT/install-uprooted-linux.sh"
echo "  updated install-uprooted-linux.sh"

# 7. site/src/pages/index.astro (two occurrences of version string in spans)
if [ -f "$REPO_ROOT/site/src/pages/index.astro" ]; then
    sed -i "s/<span class=\"version\">[^<]*</<span class=\"version\">$VERSION</" \
        "$REPO_ROOT/site/src/pages/index.astro"
    echo "  updated site/src/pages/index.astro"
fi

echo ""
echo "All version references bumped to $VERSION"
