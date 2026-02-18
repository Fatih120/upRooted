#!/usr/bin/env bash
# Curl shim for installer sandbox testing.
# Intercepts the GitHub artifacts download and serves a pre-made fake tarball.
# All other curl calls pass through to the real curl binary.

REAL_CURL="/usr/bin/curl.real"

OUTPUT_FILE=""
INTERCEPT=false

ARGS=("$@")
for ((i=0; i<${#ARGS[@]}; i++)); do
    case "${ARGS[i]}" in
        -o|--output)
            OUTPUT_FILE="${ARGS[$((i+1))]}"
            ;;
        *uprooted-linux-artifacts*)
            INTERCEPT=true
            ;;
    esac
done

if $INTERCEPT && [[ -n "$OUTPUT_FILE" ]]; then
    # Return our pre-made fake tarball and report HTTP 200
    cp /fake-tarball/artifacts.tar.gz "$OUTPUT_FILE"
    # curl -w "%{http_code}" prints the code to stdout
    printf "200"
    exit 0
fi

# Passthrough for all other requests
exec "$REAL_CURL" "$@"
