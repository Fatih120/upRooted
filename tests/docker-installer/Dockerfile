FROM ubuntu:24.04

# Minimal tools needed by the installer script
RUN apt-get update && apt-get install -y --no-install-recommends \
    bash curl sed grep tar gzip file \
    && rm -rf /var/lib/apt/lists/*

# Create a non-root test user (installer writes to $HOME)
RUN useradd -m -s /bin/bash testuser

# Copy installer and verification scripts
COPY --chown=testuser:testuser install-uprooted-linux.sh /home/testuser/install-uprooted-linux.sh
COPY --chown=testuser:testuser tests/docker-installer/curl-shim.sh /usr/local/bin/curl-shim.sh
COPY --chown=testuser:testuser tests/docker-installer/verify.sh /home/testuser/verify.sh
RUN chmod +x /home/testuser/install-uprooted-linux.sh \
             /usr/local/bin/curl-shim.sh \
             /home/testuser/verify.sh

# ── Build fake artifacts tarball ──
# The installer's --prebuilt mode downloads from GitHub.
# We intercept it by replacing curl with a shim that returns this tarball.
RUN mkdir -p /fake-tarball/extracted \
    && touch /fake-tarball/extracted/libuprooted_profiler.so \
    && touch /fake-tarball/extracted/UprootedHook.dll \
    && touch /fake-tarball/extracted/UprootedHook.deps.json \
    && printf '// fake uprooted preload\n' > /fake-tarball/extracted/uprooted-preload.js \
    && printf '/* fake uprooted css */\n' > /fake-tarball/extracted/uprooted.css \
    && tar -czf /fake-tarball/artifacts.tar.gz -C /fake-tarball/extracted . \
    && rm -rf /fake-tarball/extracted

# Install curl shim: intercepts GitHub download, passes everything else through
RUN mv /usr/bin/curl /usr/bin/curl.real \
    && ln -s /usr/local/bin/curl-shim.sh /usr/bin/curl

# Switch to test user
USER testuser
WORKDIR /home/testuser

# ── Set up fake Root installation ──
RUN mkdir -p Applications \
    && touch Applications/Root.AppImage \
    && chmod +x Applications/Root.AppImage

# Create fake profile directory with an HTML file to patch
RUN mkdir -p ".local/share/Root Communications/Root/profile/default/RootApps/MainApp" \
    && printf '<html>\n<head>\n</head>\n<body>Root</body>\n</html>\n' \
       > ".local/share/Root Communications/Root/profile/default/RootApps/MainApp/index.html"

# ── Run the installer ──
RUN bash install-uprooted-linux.sh \
    --prebuilt \
    --root-path "$HOME/Applications/Root.AppImage"

# ── Verify outputs ──
RUN bash verify.sh
