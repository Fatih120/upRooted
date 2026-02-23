#!/usr/bin/env bash
# build-local.sh — Full local build of ALL possible artifacts.
#
# Builds everything that can be built on the current platform.
# On Windows (Git Bash / MSYS2): Windows installer + shared artifacts + .uprpkg
# On Linux: Linux installer + .deb + AppImage + shared artifacts + .uprpkg
# On macOS: macOS installer + shared artifacts + .uprpkg
#
# Usage:
#   ./scripts/build-local.sh                    # Auto-detect version from VERSION file
#   ./scripts/build-local.sh 0.5.0-rc           # Override version
#   ./scripts/build-local.sh 0.5.0-rc --skip-ts # Skip TypeScript build (use prebuilt dist/)

set -euo pipefail

REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"
cd "$REPO_ROOT"

VERSION="${1:-$(tr -d '[:space:]' < VERSION)}"
SKIP_TS=false
for arg in "$@"; do [ "$arg" = "--skip-ts" ] && SKIP_TS=true; done

OUT="$REPO_ROOT/_release_artifacts"
ARTS="$REPO_ROOT/installer/src-tauri/artifacts"

rm -rf "$OUT"
mkdir -p "$OUT" "$ARTS"

echo ""
echo "  ╔══════════════════════════════════════╗"
echo "  ║    Uprooted Local Build v$VERSION"
echo "  ╚══════════════════════════════════════╝"
echo ""

# Detect platform
case "$(uname -s)" in
    MINGW*|MSYS*|CYGWIN*) PLATFORM="windows" ;;
    Linux*)               PLATFORM="linux" ;;
    Darwin*)              PLATFORM="macos" ;;
    *)                    echo "Unknown platform: $(uname -s)"; exit 1 ;;
esac
echo "  Platform: $PLATFORM"
echo "  Version:  $VERSION"
echo "  Output:   $OUT"
echo ""

# ─── Phase 1: TypeScript ───────────────────────────────────

if [ "$SKIP_TS" = true ]; then
    echo "[1/7] TypeScript: SKIPPED (using prebuilt dist/)"
else
    echo "[1/7] TypeScript: building..."
    pnpm install --frozen-lockfile 2>&1 | tail -1
    pnpm build 2>&1 | tail -1
fi

if [ ! -f dist/uprooted-preload.js ] || [ ! -f dist/uprooted.css ]; then
    echo "  ERROR: dist/uprooted-preload.js or dist/uprooted.css not found"
    exit 1
fi
echo "  dist/uprooted-preload.js  ($(wc -c < dist/uprooted-preload.js) bytes)"
echo "  dist/uprooted.css         ($(wc -c < dist/uprooted.css) bytes)"

# ─── Phase 2: C# Hook ──────────────────────────────────────

echo ""
echo "[2/7] C# Hook: building..."
dotnet build hook/UprootedHook.csproj -c Release 2>&1 | tail -3

HOOK_OUT="hook/bin/Release/net9.0"
for f in UprootedHook.dll UprootedHook.deps.json nsfw-filter.js link-embeds.js; do
    [ -f "$HOOK_OUT/$f" ] || { echo "  ERROR: Missing $f"; exit 1; }
    echo "  $f  ($(wc -c < "$HOOK_OUT/$f") bytes)"
done

# ─── Phase 3: Tests ────────────────────────────────────────

echo ""
echo "[3/7] Tests: running..."
TEST_OUT=$(dotnet test tests/UprootedTests/UprootedTests.csproj --no-build 2>&1 || dotnet test tests/UprootedTests/UprootedTests.csproj 2>&1)
PASSED=$(echo "$TEST_OUT" | grep -oP 'Passed:\s+\K\d+' || echo "?")
FAILED=$(echo "$TEST_OUT" | grep -oP 'Failed:\s+\K\d+' || echo "0")
echo "  Passed: $PASSED, Failed: $FAILED"
if [ "$FAILED" != "0" ]; then
    echo "  ERROR: Tests failed!"
    echo "$TEST_OUT" | tail -10
    exit 1
fi

# ─── Phase 4: Stage shared artifacts ───────────────────────

echo ""
echo "[4/7] Staging shared artifacts..."
cp "$HOOK_OUT/UprootedHook.dll" "$ARTS/"
cp "$HOOK_OUT/UprootedHook.deps.json" "$ARTS/"
cp "$HOOK_OUT/nsfw-filter.js" "$ARTS/"
cp "$HOOK_OUT/link-embeds.js" "$ARTS/"
cp dist/uprooted-preload.js "$ARTS/"
cp dist/uprooted.css "$ARTS/"
echo "  6 shared artifacts staged"

# ─── Phase 5: Platform profiler + installer ─────────────────

echo ""
echo "[5/7] Building $PLATFORM profiler + installer..."

case "$PLATFORM" in
    windows)
        # Need MSVC cl.exe — check if available
        if ! command -v cl.exe &>/dev/null; then
            echo "  cl.exe not found. Trying to activate MSVC..."
            # Try common VS paths
            for vsver in "2022" "2019"; do
                for edition in "Community" "Professional" "Enterprise" "BuildTools"; do
                    VCVARS="/c/Program Files/Microsoft Visual Studio/$vsver/$edition/VC/Auxiliary/Build/vcvars64.bat"
                    if [ -f "$VCVARS" ]; then
                        echo "  Found: $VCVARS"
                        # Can't source .bat in bash — user must run from Developer Command Prompt
                        echo ""
                        echo "  ERROR: cl.exe not in PATH. Run this script from a"
                        echo "  'Developer Command Prompt for VS' or 'x64 Native Tools'"
                        echo "  command prompt, OR run: source vcvars64.bat"
                        exit 1
                    fi
                done
            done
            echo "  ERROR: Visual Studio not found. Install VS Build Tools."
            exit 1
        fi

        cl.exe /LD /O2 /GS- \
            /Fe:"$ARTS/uprooted_profiler.dll" \
            tools/uprooted_profiler.c \
            /link ole32.lib kernel32.lib shell32.lib \
            /DEF:tools/uprooted_profiler.def \
            /DEBUG:NONE /OPT:REF /OPT:ICF 2>&1 | tail -2
        echo "  uprooted_profiler.dll  ($(wc -c < "$ARTS/uprooted_profiler.dll") bytes)"

        cd installer/src-tauri
        cargo build --release 2>&1 | tail -3
        cd "$REPO_ROOT"

        BINARY="installer/src-tauri/target/release/uprooted.exe"
        ARTIFACT="Uprooted-${VERSION}-windows-amd64.exe"
        cp "$BINARY" "$OUT/$ARTIFACT"
        echo "  $ARTIFACT  ($(wc -c < "$OUT/$ARTIFACT") bytes)"
        ;;

    linux)
        gcc -shared -fPIC -O2 -s -fvisibility=hidden \
            -o "$ARTS/libuprooted_profiler.so" \
            tools/uprooted_profiler_linux.c
        echo "  libuprooted_profiler.so  ($(wc -c < "$ARTS/libuprooted_profiler.so") bytes)"

        cd installer/src-tauri
        cargo build --release 2>&1 | tail -3
        cd "$REPO_ROOT"

        BINARY="installer/src-tauri/target/release/uprooted"
        chmod +x "$BINARY"

        # amd64
        ARTIFACT="Uprooted-${VERSION}-linux-amd64"
        cp "$BINARY" "$OUT/$ARTIFACT"
        chmod +x "$OUT/$ARTIFACT"
        echo "  $ARTIFACT  ($(wc -c < "$OUT/$ARTIFACT") bytes)"

        # .deb (amd64)
        if command -v fpm &>/dev/null; then
            echo ""
            echo "  Building .deb (amd64)..."
            mkdir -p /tmp/deb_amd64/usr/bin
            cp "$OUT/$ARTIFACT" /tmp/deb_amd64/usr/bin/uprooted
            chmod +x /tmp/deb_amd64/usr/bin/uprooted
            fpm -s dir -t deb -n uprooted -v "$VERSION" \
                --description "Client mod framework for Root Communications" \
                --url "https://uprooted.sh" --license "Uprooted-1.0" \
                --architecture amd64 \
                --maintainer "watchthelight <admin@watchthelight.org>" \
                -C /tmp/deb_amd64 \
                -p "$OUT/uprooted_${VERSION}_amd64.deb"
            echo "  uprooted_${VERSION}_amd64.deb  ($(wc -c < "$OUT/uprooted_${VERSION}_amd64.deb") bytes)"
            rm -rf /tmp/deb_amd64
        else
            echo "  fpm not found — skipping .deb build (gem install fpm)"
        fi

        # AppImage (amd64)
        if [ -f packaging/appimage/build-appimage.sh ]; then
            echo ""
            echo "  Building AppImage (x86_64)..."
            chmod +x packaging/appimage/build-appimage.sh
            bash packaging/appimage/build-appimage.sh "$OUT/$ARTIFACT" "$VERSION" x86_64
            mv "Uprooted-${VERSION}-x86_64.AppImage" "$OUT/" 2>/dev/null || true
        fi
        ;;

    macos)
        clang -shared -fPIC -O2 \
            -o "$ARTS/libuprooted_profiler.dylib" \
            tools/uprooted_profiler_macos.c
        echo "  libuprooted_profiler.dylib  ($(wc -c < "$ARTS/libuprooted_profiler.dylib") bytes)"

        cd installer/src-tauri
        cargo build --release 2>&1 | tail -3
        cd "$REPO_ROOT"

        BINARY="installer/src-tauri/target/release/uprooted"
        chmod +x "$BINARY"

        ARCH=$(uname -m)
        if [ "$ARCH" = "arm64" ] || [ "$ARCH" = "aarch64" ]; then
            SUFFIX="macos-arm64"
        else
            SUFFIX="macos-amd64"
        fi
        ARTIFACT="Uprooted-${VERSION}-${SUFFIX}"
        cp "$BINARY" "$OUT/$ARTIFACT"
        chmod +x "$OUT/$ARTIFACT"
        echo "  $ARTIFACT  ($(wc -c < "$OUT/$ARTIFACT") bytes)"
        ;;
esac

# ─── Phase 6: Auto-update package (.uprpkg) ─────────────────

echo ""
echo "[6/7] Packing auto-update.uprpkg..."
UPRPKG_DIR="$REPO_ROOT/_uprpkg_staging"
rm -rf "$UPRPKG_DIR"
mkdir -p "$UPRPKG_DIR"
cp "$HOOK_OUT/UprootedHook.dll" "$UPRPKG_DIR/"
cp "$HOOK_OUT/UprootedHook.deps.json" "$UPRPKG_DIR/"
cp "$HOOK_OUT/nsfw-filter.js" "$UPRPKG_DIR/"
cp "$HOOK_OUT/link-embeds.js" "$UPRPKG_DIR/"
cp dist/uprooted-preload.js "$UPRPKG_DIR/"
cp dist/uprooted.css "$UPRPKG_DIR/"

python3 scripts/pack-update.py --input-dir "$UPRPKG_DIR" --output "$OUT/auto-update.uprpkg" --verify 2>&1 | tail -3
echo "  auto-update.uprpkg  ($(wc -c < "$OUT/auto-update.uprpkg") bytes)"
rm -rf "$UPRPKG_DIR"

# ─── Phase 7: Supplementary files + checksums ───────────────

echo ""
echo "[7/7] Supplementary files + checksums..."

# Linux artifacts tarball (shared artifacts for bash installer --prebuilt)
mkdir -p "$REPO_ROOT/_tarball_staging"
cp "$HOOK_OUT/UprootedHook.dll" "$REPO_ROOT/_tarball_staging/"
cp "$HOOK_OUT/UprootedHook.deps.json" "$REPO_ROOT/_tarball_staging/"
cp "$HOOK_OUT/nsfw-filter.js" "$REPO_ROOT/_tarball_staging/"
cp "$HOOK_OUT/link-embeds.js" "$REPO_ROOT/_tarball_staging/"
cp dist/uprooted-preload.js "$REPO_ROOT/_tarball_staging/"
cp dist/uprooted.css "$REPO_ROOT/_tarball_staging/"
tar -czf "$OUT/uprooted-linux-artifacts.tar.gz" -C "$REPO_ROOT/_tarball_staging" .
echo "  uprooted-linux-artifacts.tar.gz  ($(wc -c < "$OUT/uprooted-linux-artifacts.tar.gz") bytes)"
rm -rf "$REPO_ROOT/_tarball_staging"

# Copy bash installer
cp install-uprooted-linux.sh "$OUT/"
echo "  install-uprooted-linux.sh  ($(wc -c < "$OUT/install-uprooted-linux.sh") bytes)"

# Generate checksums
cd "$OUT"
sha256sum * > checksums.txt 2>/dev/null || shasum -a 256 * > checksums.txt
echo "  checksums.txt generated"
cd "$REPO_ROOT"

# ─── Summary ────────────────────────────────────────────────

echo ""
echo "  ╔══════════════════════════════════════╗"
echo "  ║         Build Complete!              ║"
echo "  ╚══════════════════════════════════════╝"
echo ""
echo "  Artifacts in: $OUT"
echo ""
ls -lh "$OUT/" | awk 'NR>1 {printf "  %-45s %s\n", $NF, $5}'
echo ""
echo "  Upload to releases:"
echo "    gh release upload v$VERSION $OUT/* --repo <REPO> --clobber"
