# Next Release

> Changes since v0.4.2. This file is replaced each release.

## Fixes

- **Settings navigation freeze** — `ScheduleWalkBurst` now debounces to a single 150ms delayed walk instead of firing `WalkVisualTreeNow` immediately on every tab switch. Rapid navigation no longer stacks UI-thread tree walks.
- **Experimental toggle unclickable** — Fixed z-order in the Plugin Settings experimental banner so the toggle pill renders on top of the banner and receives pointer events.

## Changes

- **Plugin sort order** — Plugins sort enabled-first, then Stable → Experimental, then A-Z. Sort is dynamic and updates on every toggle.
- **Plugin Show More** — Plugin page opens with 4 cards (2 rows) to avoid scrolling. A "Show N More" button expands the full list; search and filter bypass the limit.
- **Desktop notifications** — Auto-updater shows an OS-level toast (Windows) or `notify-send` (Linux) when a background update is applied, in addition to the in-app overlay. Respects the `AutoUpdateNotify` setting (previously ignored).
- **Auto-update interval** — Background check interval reduced from 6 hours to 1 minute.
- **About page** — Removed Links and Diagnostics cards. Added compact "Open Logs" button to the page title row. No more scrolling required.
- **Testing status changes** — ClearURLs promoted to Stable, Themes demoted to Beta, SilentTyping demoted to Experimental.
- **Rootcord plugin** — Discord-style vertical server sidebar added as an experimental plugin. Supports live toggle without restart.
