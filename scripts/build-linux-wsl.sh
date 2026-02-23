#!/bin/bash
set -e
export PATH="$HOME/.cargo/bin:$PATH"

SRC="/mnt/c/Users/bash/workspace/no-more-configs/projects/uprooted/uprooted-private"
WORK="/tmp/uprooted-linux-build"
rm -rf "$WORK" && mkdir -p "$WORK/out"

echo "[1/3] Copying linux binary..."
cp "$SRC/installer/src-tauri/target/release/uprooted" "$WORK/Uprooted-0.5.0-linux-amd64"
chmod +x "$WORK/Uprooted-0.5.0-linux-amd64"
echo "  $(wc -c < "$WORK/Uprooted-0.5.0-linux-amd64") bytes"
cp "$WORK/Uprooted-0.5.0-linux-amd64" "$WORK/out/"

echo "[2/3] Building .deb..."
mkdir -p "$WORK/deb/usr/bin"
cp "$WORK/Uprooted-0.5.0-linux-amd64" "$WORK/deb/usr/bin/uprooted"
chmod +x "$WORK/deb/usr/bin/uprooted"
fpm -s dir -t deb -n uprooted -v 0.5.0 \
  --description "Client mod framework for Root Communications" \
  --url "https://uprooted.sh" --license "Uprooted-1.0" \
  --architecture amd64 \
  --maintainer "watchthelight <admin@watchthelight.org>" \
  -C "$WORK/deb" \
  -p "$WORK/out/uprooted_0.5.0_amd64.deb" 2>&1 | tail -1
echo "  $(wc -c < "$WORK/out/uprooted_0.5.0_amd64.deb") bytes"

echo "[3/3] Building AppImage..."
mkdir -p "$WORK/AppDir/usr/bin"
cp "$WORK/Uprooted-0.5.0-linux-amd64" "$WORK/AppDir/usr/bin/uprooted"
chmod +x "$WORK/AppDir/usr/bin/uprooted"
cat > "$WORK/AppDir/uprooted.desktop" << 'DESK'
[Desktop Entry]
Name=Uprooted
Exec=uprooted
Icon=uprooted
Type=Application
Terminal=true
Categories=Utility;System;
DESK
# Minimal placeholder icon
touch "$WORK/AppDir/uprooted.png"
cat > "$WORK/AppDir/AppRun" << 'APPRUN'
#!/bin/bash
HERE="$(dirname "$(readlink -f "${0}")")"
exec "${HERE}/usr/bin/uprooted" "$@"
APPRUN
chmod +x "$WORK/AppDir/AppRun"

if [ ! -f /tmp/appimagetool ]; then
  echo "  Downloading appimagetool..."
  curl -sLo /tmp/appimagetool https://github.com/AppImage/appimagetool/releases/download/continuous/appimagetool-x86_64.AppImage
  chmod +x /tmp/appimagetool
fi
ARCH=x86_64 /tmp/appimagetool --appimage-extract-and-run "$WORK/AppDir" "$WORK/out/Uprooted-0.5.0-x86_64.AppImage" 2>&1 | tail -2

echo ""
echo "=== Built ==="
ls -lh "$WORK/out/"

# Copy back to Windows
cp "$WORK/out/"* "$SRC/_release_artifacts/" 2>/dev/null || echo "Copy to _release_artifacts failed (NTFS permissions), files in $WORK/out/"
