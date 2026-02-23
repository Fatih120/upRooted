#!/usr/bin/env bash
# release.sh — Version bump, tag, push, and create a GitHub release.
#
# Three channels:
#   stable  — public repo, from main branch, triggers package manager updates
#   canary  — canary repo, from main branch, bleeding edge
#   dev     — private repo, from dev branch, invite only
#
# Usage:
#   ./scripts/release.sh stable 0.6.0
#   ./scripts/release.sh canary 0.6.0-canary.1
#   ./scripts/release.sh dev 0.6.0-dev.1
#   ./scripts/release.sh stable patch     # auto-bump patch
#   ./scripts/release.sh stable minor     # auto-bump minor

set -euo pipefail

REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"

# Parse args
CHANNEL="${1:-}"
VERSION_ARG="${2:-}"

if [ -z "$CHANNEL" ] || [ -z "$VERSION_ARG" ]; then
    echo "Usage: $0 <stable|canary|dev> <version|patch|minor|major>" >&2
    exit 1
fi

case "$CHANNEL" in
    stable|canary|dev) ;;
    *) echo "Invalid channel: $CHANNEL (must be stable, canary, or dev)" >&2; exit 1 ;;
esac

# Determine branch
case "$CHANNEL" in
    dev) BRANCH="dev" ;;
    *)   BRANCH="main" ;;
esac

# Discover remote name (not assumed to be origin)
REMOTE=$(cd "$REPO_ROOT" && git remote | head -1)
echo "Using remote: $REMOTE"

# Ensure we're on the right branch
CURRENT_BRANCH=$(cd "$REPO_ROOT" && git branch --show-current)
if [ "$CURRENT_BRANCH" != "$BRANCH" ]; then
    echo "Switching to $BRANCH branch..."
    cd "$REPO_ROOT" && git checkout "$BRANCH"
fi

# Pull latest
cd "$REPO_ROOT" && git pull "$REMOTE" "$BRANCH"

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

# Prerelease flag
PRERELEASE_FLAG=""
case "$CHANNEL" in
    canary|dev) PRERELEASE_FLAG="--prerelease" ;;
esac

echo ""
echo "Channel:  $CHANNEL"
echo "Branch:   $BRANCH"
echo "Version:  $CURRENT -> $VERSION"
echo "Remote:   $REMOTE"
echo ""

# 1. Bump all version references
bash "$REPO_ROOT/scripts/bump-version.sh" "$VERSION"

# 2. Commit
cd "$REPO_ROOT"
git add -A
git commit -m "release: v$VERSION ($CHANNEL)"

# 3. Tag
git tag -a "v$VERSION" -m "Release v$VERSION ($CHANNEL channel)"

# 4. Push
git push "$REMOTE" "$BRANCH" "v$VERSION"

# 5. Create GitHub release (triggers release.yml)
NOTES_FLAG=""
if [ "$CHANNEL" = "stable" ] && [ -f "$REPO_ROOT/NEXT-RELEASE.md" ]; then
    NOTES_FLAG="--notes-file NEXT-RELEASE.md"
else
    NOTES_FLAG="--notes ${CHANNEL^} release v$VERSION"
fi

gh release create "v$VERSION" \
    --title "v$VERSION" \
    $NOTES_FLAG \
    $PRERELEASE_FLAG

echo ""
echo "Release v$VERSION ($CHANNEL) created on $BRANCH branch."
echo "CI will now build and publish artifacts."
echo ""
echo "Channel routing:"
case "$CHANNEL" in
    stable) echo "  -> The-Uprooted-Project/uprooted (public) + all package managers" ;;
    canary) echo "  -> The-Uprooted-Project/uprooted-canary (public)" ;;
    dev)    echo "  -> The-Uprooted-Project/uprooted-private (private, dev branch)" ;;
esac
echo ""
echo "Monitor: gh run watch"
