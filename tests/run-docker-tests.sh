#!/usr/bin/env bash
# Run unit tests in an isolated Docker container (Linux/net10.0).
# Outputs pass/fail to stdout and extracts coverage XML to tests/coverage/.
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"

echo "=== Uprooted Unit Test Runner (Docker) ==="
echo ""

# Build the Docker image
echo "[1/2] Building test image..."
docker build \
    -f "$SCRIPT_DIR/Dockerfile.unittest" \
    -t uprooted-unit-tests \
    "$REPO_ROOT"

# Run tests; extract coverage to tests/coverage/
echo ""
echo "[2/2] Running tests..."
mkdir -p "$SCRIPT_DIR/coverage"

docker run --rm \
    -v "$SCRIPT_DIR/coverage:/coverage" \
    uprooted-unit-tests

echo ""
echo "Coverage report available at: $SCRIPT_DIR/coverage/"
echo ""
echo "=== Done ==="
