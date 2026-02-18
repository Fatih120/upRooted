#!/usr/bin/env bash
# Verification script for the installer Docker sandbox.
# Checks that all expected outputs were created by install-uprooted-linux.sh.

PASS=0
FAIL=0

check() {
    local desc="$1"
    local condition="$2"
    if eval "$condition" 2>/dev/null; then
        echo "  PASS: $desc"
        PASS=$((PASS + 1))
    else
        echo "  FAIL: $desc"
        FAIL=$((FAIL + 1))
    fi
}

INSTALL_DIR="$HOME/.local/share/uprooted"
PROFILE_DIR="$HOME/.local/share/Root Communications/Root/profile/default"
ENV_CONF="$HOME/.config/environment.d/uprooted.conf"
WRAPPER="$INSTALL_DIR/launch-root.sh"

echo ""
echo "=== Installer Verification ==="
echo ""

# 1. Artifacts deployed
echo "-- Deployed artifacts --"
check "libuprooted_profiler.so exists" "[[ -f '$INSTALL_DIR/libuprooted_profiler.so' ]]"
check "UprootedHook.dll exists"        "[[ -f '$INSTALL_DIR/UprootedHook.dll' ]]"
check "UprootedHook.deps.json exists"  "[[ -f '$INSTALL_DIR/UprootedHook.deps.json' ]]"
check "uprooted-preload.js exists"     "[[ -f '$INSTALL_DIR/uprooted-preload.js' ]]"
check "uprooted.css exists"            "[[ -f '$INSTALL_DIR/uprooted.css' ]]"
echo ""

# 2. Env var config file
echo "-- Environment variables --"
check "environment.d/uprooted.conf exists"               "[[ -f '$ENV_CONF' ]]"
check "uprooted.conf has DOTNET_ENABLE_PROFILING=1"      "grep -q 'DOTNET_ENABLE_PROFILING=1' '$ENV_CONF'"
check "uprooted.conf has DOTNET_PROFILER guid"           "grep -q 'DOTNET_PROFILER=' '$ENV_CONF'"
check "uprooted.conf has DOTNET_PROFILER_PATH"           "grep -q 'DOTNET_PROFILER_PATH=' '$ENV_CONF'"
check "uprooted.conf has CORECLR_ENABLE_PROFILING=1"     "grep -q 'CORECLR_ENABLE_PROFILING=1' '$ENV_CONF'"
check "~/.profile has DOTNET_ENABLE_PROFILING"           "grep -q 'DOTNET_ENABLE_PROFILING' '$HOME/.profile'"
echo ""

# 3. Wrapper script
echo "-- Launch wrapper --"
check "launch-root.sh exists"      "[[ -f '$WRAPPER' ]]"
check "launch-root.sh is executable" "[[ -x '$WRAPPER' ]]"
check "launch-root.sh sets profiling env" "grep -q 'DOTNET_ENABLE_PROFILING' '$WRAPPER'"
echo ""

# 4. HTML patching
echo "-- HTML patches --"
HTML="$PROFILE_DIR/RootApps/MainApp/index.html"
check "MainApp/index.html exists"     "[[ -f '$HTML' ]]"
check "MainApp/index.html is patched" "grep -qE '(uprooted:start|uprooted-preload|<!-- uprooted -->)' '$HTML'"
check "MainApp/index.html has script tag"  "grep -q 'uprooted-preload.js' '$HTML'"
check "MainApp/index.html has css tag"     "grep -q 'uprooted.css' '$HTML'"
echo ""

# Summary
echo "=== Results: $PASS passed, $FAIL failed ==="
echo ""

if [[ $FAIL -gt 0 ]]; then
    exit 1
fi
exit 0
