#!/usr/bin/env bash
# release.sh — Version bump, tag, push, and create a GitHub release.
#
# Usage:
#   ./scripts/release.sh 0.6.0              # Release a specific version
#   ./scripts/release.sh 0.6.0 --prerelease # Mark as pre-release
#   ./scripts/release.sh patch              # Auto-bump patch version
#   ./scripts/release.sh minor              # Auto-bump minor version
#   ./scripts/release.sh major              # Auto-bump major version

set -euo pipefail

REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"
PRERELEASE_FLAG=""

# Parse args
VERSION_ARG="${1:-}"
shift || true
for arg in "$@"; do
    case "$arg" in
        --prerelease) PRERELEASE_FLAG="--prerelease" ;;
        *) echo "Unknown arg: $arg" >&2; exit 1 ;;
    esac
done

if [ -z "$VERSION_ARG" ]; then
    echo "Usage: $0 <version|patch|minor|major> [--prerelease]" >&2
    exit 1
fi

# Read current version
CURRENT="$(tr -d '[:space:]' < "$REPO_ROOT/VERSION")"

# Compute new version
case "$VERSION_ARG" in
    patch)
        IFS='.' read -r major minor patch <<< "${CURRENT%%-*}"
        VERSION="${major}.${minor}.$((patch + 1))"
        ;;
    minor)
        IFS='.' read -r major minor patch <<< "${CURRENT%%-*}"
        VERSION="${major}.$((minor + 1)).0"
        ;;
    major)
        IFS='.' read -r major minor patch <<< "${CURRENT%%-*}"
        VERSION="$((major + 1)).0.0"
        ;;
    *)
        VERSION="$VERSION_ARG"
        ;;
esac

echo "Releasing: $CURRENT -> $VERSION"

# 1. Bump all version references
bash "$REPO_ROOT/scripts/bump-version.sh" "$VERSION"

# 2. Commit
cd "$REPO_ROOT"
git add -A
git commit -m "chore: bump to $VERSION"

# 3. Tag
git tag -a "v$VERSION" -m "Release v$VERSION"

# 4. Push
git push origin main "v$VERSION"

# 5. Create GitHub release (triggers release.yml)
NOTES_FLAG=""
if [ -f "$REPO_ROOT/NEXT-RELEASE.md" ]; then
    NOTES_FLAG="--notes-file NEXT-RELEASE.md"
fi

gh release create "v$VERSION" \
    --title "v$VERSION" \
    $NOTES_FLAG \
    $PRERELEASE_FLAG

echo ""
echo "Release v$VERSION created. CI will now build and publish artifacts."
echo "Monitor: gh run watch"
