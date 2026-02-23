# Next Release

> Changes since v0.5.1-dev1. This file is replaced each release.

### Fixed

- **Experimental plugins cause hang on boot after update** — Users who enabled experimental plugins (Rootcord, WhoReacted, etc.) before updating could get stuck in an unrecoverable hang on startup. The only fix was manually deleting the settings file. Now ALL experimental plugins are blanket-disabled on every version upgrade. Users can re-enable them from Settings > Plugins after confirming the update is stable.
  - File: `hook/StartupHook.cs`
