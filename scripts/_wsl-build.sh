#!/bin/bash
set -e
export PATH="$HOME/.cargo/bin:$PATH"
SRC=/mnt/c/Users/bash/workspace/no-more-configs/projects/uprooted/uprooted-private
W=/tmp/upr
rm -rf $W && mkdir -p $W/out $W/AppDir/usr/bin

echo "Copying binary..."
cp $SRC/installer/src-tauri/target/release/uprooted $W/out/Uprooted-0.5.0-linux-amd64
chmod +x $W/out/Uprooted-0.5.0-linux-amd64

echo "Building AppImage..."
cp $W/out/Uprooted-0.5.0-linux-amd64 $W/AppDir/usr/bin/uprooted
chmod +x $W/AppDir/usr/bin/uprooted
printf '[Desktop Entry]\nName=Uprooted\nExec=uprooted\nIcon=uprooted\nType=Application\nTerminal=true\nCategories=Utility;\n' > $W/AppDir/uprooted.desktop
touch $W/AppDir/uprooted.png
printf '#!/bin/bash\nHERE="$(dirname "$(readlink -f "${0}")")"\nexec "${HERE}/usr/bin/uprooted" "$@"\n' > $W/AppDir/AppRun
chmod +x $W/AppDir/AppRun

curl -sLo /tmp/ait https://github.com/AppImage/appimagetool/releases/download/continuous/appimagetool-x86_64.AppImage
chmod +x /tmp/ait
ARCH=x86_64 /tmp/ait --appimage-extract-and-run $W/AppDir $W/out/Uprooted-0.5.0-x86_64.AppImage 2>&1 | tail -1

echo "=== Done ==="
ls -lh $W/out/
