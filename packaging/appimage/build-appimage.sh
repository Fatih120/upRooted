#!/usr/bin/env bash
# build-appimage.sh — Build an AppImage from a pre-compiled binary.
#
# Usage:
#   ./packaging/appimage/build-appimage.sh <binary-path> <version> <arch>
#
# Example:
#   ./packaging/appimage/build-appimage.sh ./Uprooted-0.5.0-linux-amd64 0.5.0 x86_64
#
# Outputs: Uprooted-<version>-<arch>.AppImage in the current directory.
#
# Requires: appimagetool (downloaded automatically if not in PATH)

set -euo pipefail

BINARY="$1"
VERSION="$2"
ARCH="$3"

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
APPDIR="$(mktemp -d)/Uprooted.AppDir"

echo "Building AppImage: version=$VERSION arch=$ARCH"

# Create AppDir structure
mkdir -p "$APPDIR/usr/bin"
cp "$BINARY" "$APPDIR/usr/bin/uprooted"
chmod +x "$APPDIR/usr/bin/uprooted"

# Desktop file and icon
cp "$SCRIPT_DIR/uprooted.desktop" "$APPDIR/"
if [ -f "$SCRIPT_DIR/uprooted.png" ]; then
    cp "$SCRIPT_DIR/uprooted.png" "$APPDIR/"
else
    # Generate a 1x1 placeholder if no icon exists
    printf '\x89PNG\r\n\x1a\n' > "$APPDIR/uprooted.png"
    echo "WARNING: No uprooted.png icon found, using placeholder"
fi

# AppRun
cat > "$APPDIR/AppRun" << 'APPRUN'
#!/bin/bash
HERE="$(dirname "$(readlink -f "${0}")")"
exec "${HERE}/usr/bin/uprooted" "$@"
APPRUN
chmod +x "$APPDIR/AppRun"

# Get appimagetool if not available
if ! command -v appimagetool &>/dev/null; then
    echo "Downloading appimagetool..."
    TOOL_ARCH="$ARCH"
    [ "$TOOL_ARCH" = "aarch64" ] && TOOL_ARCH="aarch64"
    [ "$TOOL_ARCH" = "x86_64" ] && TOOL_ARCH="x86_64"
    curl -sLO "https://github.com/AppImage/appimagetool/releases/download/continuous/appimagetool-${TOOL_ARCH}.AppImage"
    chmod +x "appimagetool-${TOOL_ARCH}.AppImage"
    APPIMAGETOOL="./appimagetool-${TOOL_ARCH}.AppImage"
else
    APPIMAGETOOL="appimagetool"
fi

# Build
ARCH="$ARCH" "$APPIMAGETOOL" "$APPDIR" "Uprooted-${VERSION}-${ARCH}.AppImage"

echo "Built: Uprooted-${VERSION}-${ARCH}.AppImage"

# Cleanup
rm -rf "$(dirname "$APPDIR")"
