# Next Release

> Changes since v0.5.1-dev6. This file is replaced each release.

### Fixed

- **Linux profiler Console TypeRef crash** — Verbose catch handler's `Console.WriteLine` TypeRef was scoped to `System.Runtime` instead of `System.Console` assembly. Fixed by searching for `System.Console` AssemblyRef via `IMetaDataAssemblyImport`. Falls back to silent catch if not found.
  - File: `tools/uprooted_profiler_linux.c`
- **Linux installer: removed global env var pollution** — No more `~/.config/environment.d/uprooted.conf`, KDE plasma env script, or `~/.profile` injection. Wrapper script (`launch-root.sh`) scopes profiler env vars to Root only.
  - File: `install-uprooted-linux.sh`

### Changed

- **Linux installer simplified to two modes** — Removed build-from-source path entirely. Two modes: download from GitHub releases (default) or `--local` (deploy from repo build output). No more auto-installing dotnet/gcc/pnpm.
  - File: `install-uprooted-linux.sh`
- **GearLever AppImage compatibility** — Added `~/AppImages/` and lowercase `.appimage` patterns to Root search paths. Skip FUSE mount paths (`/tmp/.mount_*`) in /proc fallback.
  - File: `install-uprooted-linux.sh`
- **Linux installer auto-relaunch** — Installer kills running Root and relaunches via wrapper after install/repair.
  - File: `install-uprooted-linux.sh`
