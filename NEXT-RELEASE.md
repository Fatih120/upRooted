# Next Release

> Changes since v0.5.2-dev2. This file is replaced each release.

### Fixed

- **Linux installer: silenced misleading HTML patch warnings** — First install showed alarming `[!]` warnings about missing HTML files, implying a broken install. HTML patches are optional: all core features (sidebar, settings, themes, chat plugins) are Avalonia-native. Confirmed fully functional without patches on Ubuntu 24 LTS and CachyOS 26 rolling.
  - File: `install-uprooted-linux.sh`
