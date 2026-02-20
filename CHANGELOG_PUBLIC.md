# Changelog

All public-facing notable changes to Uprooted are documented here. This file mirrors the [GitHub release notes](https://github.com/watchthelight/uprooted/releases).

---

## [v0.4.2](https://github.com/watchthelight/uprooted/releases/tag/v0.4.2) — 2026-02-20

### New

- **Light and PureDark theme support** — Uprooted's settings UI now fully adapts to Root's Light and PureDark theme variants. Previously, the settings pages were only designed for Dark mode — switching to Light theme resulted in white text on white backgrounds. All pages, cards, sidebar nav items, and version text now read Root's live color system.
- **Rootcord** — Experimental plugin that adds a Discord-style vertical server sidebar. Toggle it live from Plugin Settings — no restart required.
- **Desktop notifications for updates** — When an update is automatically applied in the background, you now get an OS notification (Windows toast or Linux `notify-send`) in addition to the in-app banner. Respects your notification preference in Update Settings.

### Improvements

- **Plugin page** — Opens showing just 4 cards (2 rows) so you don't have to scroll. A "Show More" button expands the full list. Plugins sort enabled-first, then by stability (Stable before Experimental), then A-Z.
- **About page** — Removed the Links and Diagnostics cards. Added a compact "Open Logs" button to the header row. The page no longer requires scrolling.
- **Silent Typing rewrite** — Completely rewritten from scratch using a cleaner, more reliable approach (contributed by Kurumi Nanase)
- **Themes tab** — The Themes plugin now has an "Open" button in Plugin Settings that takes you directly to the Themes tab

### Fixes

- Fixed a crash on startup when a theme was active (InvalidCastException: Color/IBrush type mismatch)
- Online status indicators (green dots next to usernames) no longer change color when using a custom theme
- Fixed UI freeze when quickly switching between Root's settings tabs (rapid navigation stacked visual tree walks on the UI thread)
- Fixed the "Show experimental plugins" toggle being unclickable (z-order issue — banner was covering the toggle)
- Fixed theme color issues on Light variant (named color crash, ResourceDictionary lookup not resolving merged entries)

### Testing status changes

- ClearURLs promoted to **Stable**
- Themes demoted to **Beta**
- SilentTyping demoted to **Experimental**

---

## [v0.4.1](https://github.com/watchthelight/uprooted/releases/tag/v0.4.1) — 2026-02-19

### Improvements

- **Theme Engine v2** — Complete rewrite of the theme engine for better color accuracy and consistency. Themes now override Root's actual color system directly, so all controls update instantly when you switch themes — no more brief flickers or stale colors. Palette generation uses OKLCH, a perceptually uniform color space, so lightness steps look natural across all hues.
- **Silent Typing restored** — The Silent Typing plugin now works correctly, blocking typing indicators at the network level (contributed by Kurumi Nanase).

### Fixes

- Theme colors no longer flicker when switching themes or opening settings
- Custom theme accent colors now apply instantly to all Root controls (no more needing to switch tabs)
- Fixed a startup crash on Linux

---

## [v0.4.0](https://github.com/watchthelight/uprooted/releases/tag/v0.4.0) — 2026-02-18

### New

- **LinkEmbeds — rich link previews in chat** — Discord-style embed cards for links posted in chat. As of v0.4.0, LinkEmbeds supports:
  - **YouTube** — Thumbnail preview with video title, channel name, and play button (click to open)
  - **Twitter/X** — Tweet text, author, and images via OpenGraph (works with x.com, twitter.com, and embed-fixer links like vxtwitter, fxtwitter, fixupx)
  - **Reddit** — Post title, subreddit label (e.g. "r/programming"), and Reddit's orange accent color
  - **Any site with OpenGraph or oEmbed** — Automatic rich previews with title, description, and thumbnail for thousands of sites
  - **Direct image links** — `.jpg`, `.png`, `.gif`, `.webp`, `.bmp`, `.svg` URLs render the image inline instantly with no network round-trip
  - **Animated GIFs and WebPs** — Play inline with smooth frame-accurate animation
  - **Tenor GIFs** — `tenor.com/view/` pages show the animated GIF inline (direct CDN links are left for Root's native embed)
  - **Video links** — `.mp4`, `.webm`, `.mov` URLs show a dark placeholder with a play button; click opens in your browser (thumbnail extraction not yet supported)
  - **Fallback cards** — Links with no metadata (login walls, JS-only pages) show a clean domain-only card instead of nothing
  - **Theme-aware accent colors** — Embed card borders match your active theme; site-specific brand colors (Reddit orange, custom OG colors) are preserved. Colors update live when you drag the color picker or switch themes.
  - **Hide filenames** — Image and video embeds hide the raw URL filename by default (toggleable in LinkEmbeds settings)
- **Auto-updater** — Uprooted now checks for updates automatically and applies them on the next restart. A developer channel is available for pre-release builds.
- **Message logger** — Deleted messages are preserved in chat with a red-tinted visual indicator so you can see what was removed. Message history persists across restarts.
- **Message logger settings** — Toggle logging of deleted/edited messages, ignore your own messages, and set a retention limit
- **Updates settings page** — Control auto-check frequency, notifications, and update channel (stable/dev) from the settings UI
- **Installer mode selector** — Running the installer without flags now shows a menu to choose between Install, Uninstall, and Repair

### Improvements

- **Lightbox text scaling** — Plugin info and settings popups now use larger, more readable text sizes while leaving main settings pages unchanged
- Plugin names now use PascalCase (SentryBlocker, LinkEmbeds, MessageLogger, ContentFilter) for consistency
- SentryBlocker and LinkEmbeds promoted from Alpha to Beta
- "UPROOTED" section in the sidebar now appears above "APP SETTINGS" instead of at the bottom
- Settings page font sizes better match Root's native look

### Fixes

- Fixed custom theme color swatches showing the wrong color in the theme editor
- Fixed animated GIFs rendering with black pixels or jumbled frames
- Opening settings no longer has a visible pop-in delay
- No more flash of unthemed colors when opening settings or switching tabs
- Switching from Uprooted tabs back to Root tabs is now instant
- Sidebar section spacing now matches Root's native layout
- Fixed phantom "Unsaved Changes" bar flashing when switching to Uprooted settings tabs
- Fixed "Unsaved Changes" bar being permanently suppressed for real Root settings changes after visiting Uprooted tabs
- Profile badge now appears below the username (not beside it), is smaller, and only shows for developers
- Link embeds: images now have properly rounded corners, no longer break on Cloudflare-protected pages or HTML redirects, and handle non-standard meta tag formats
- Auto-updater now shows a "Restart" button after applying updates instead of a generic "OK"
- Removed the "About Themes" info card from the bottom of the Themes page

### Known Issues

- **Message logger: edit detection may be unreliable** — Edited message indicators (amber cards) are deployed but may produce false positives or miss some edits. We're still validating this in real-world use.
- **Message logger: deletion detection may miss some deletions** — If a message is deleted with a longer server delay, the 3-second detection window may not catch it.
- **Profile badge may appear on the wrong popup** — The "Uprooted Dev" badge (developer channel only) may occasionally appear on non-profile popups.
- **Theme Engine v2 not yet validated** — The theme engine was completely rewritten for this release. While it should be much more reliable, it hasn't been tested in production yet. Report any visual glitches.
- **NSFW filter not yet validated** — The content filter has been redesigned but not yet tested end-to-end. It may not function correctly.
- **Silent Typing not yet validated** — The typing indicator blocker has been rebuilt but not yet confirmed working with two accounts.

### Linux

- Installer now uses 7 detection strategies to find Root, matching the bash installer
- All install scripts work on any machine (no more hardcoded paths)

---

## [v0.3.6-rc](https://github.com/watchthelight/uprooted/releases/tag/v0.3.6-rc) — 2026-02-18

Pre-release candidate.

### New

- **All plugins disabled by default** on new installs (existing users' settings are preserved)
- **Silent Typing** — Suppresses typing indicators so others can't see when you're typing (contributed by Kurumi Nanase)
- **Reddit link embeds** — Rich previews with subreddit labels and Reddit's orange accent color
- **Video preview embeds** — `.mp4`, `.webm`, and `.mov` links show a play button thumbnail; click to open in your browser
- **Hide filenames on image embeds** — Image embeds no longer show the raw URL filename (toggleable in settings)
- **Custom ping/reply highlight color** — Set your own mention highlight color that persists across theme switches
- **Plugin toggles** — Enable or disable individual plugins from the settings page, with a restart banner when changes need a relaunch
- **Profile badge** — "Uprooted Dev" badge on profile popups (developer channel only)
- **Message deletion detection** — Detects when messages are genuinely deleted vs. just scrolled out of view

---

## [v0.3.5](https://github.com/watchthelight/uprooted/releases/tag/v0.3.5) — 2026-02-18

### New

- **Animated image embeds** — `.gif` and `.webp` links play inline with smooth frame timing
- **Link embeds for non-YouTube sites** — Twitter/X, Reddit, and any site with OpenGraph or oEmbed support now gets rich link previews
- **Direct image embeds** — Image links (`.jpg`, `.png`, `.gif`, `.webp`) render instantly with zero network overhead
- **oEmbed discovery** — Automatically finds embed endpoints from any page, no hardcoded provider list needed
- **Fallback domain cards** — Links with no metadata (login walls, JS-only pages) get a clean domain card instead of nothing
- **Theme: "Cosmic Smoothie"** — Deep purple accent with dark space background

### Improvements

- Better site compatibility with a browser-like request signature
- Embed-fixer links (vxtwitter, fxtwitter, fixupx) auto-resolve to the best source for images
- PDFs, binaries, and non-HTML content are no longer accidentally parsed as link previews
- Falls back to Twitter-specific meta tags when standard OpenGraph tags are missing
- Tenor and rootapp.gg links are left alone since Root already handles them natively
- Settings sidebar renamed for clarity: "About", "Plugin Settings", "Theme Settings"
- Plugin search box is easier to read with better sizing and alignment
- Linux installer auto-fetches the latest version from GitHub
- Linux installer shows clearer error messages when downloads fail
- Linux installer validates downloads before extracting
- `.desktop` file creation is now opt-in (`--desktop` flag)
- Post-install message prominently reminds you to log out and back in

### Fixes

- Fixed settings crash when clicking the back arrow on Uprooted tabs
- Fixed settings header to show the correct page title and close button
- Fixed link preview crashes on certain sites

---

## [v0.3.2](https://github.com/watchthelight/uprooted/releases/tag/v0.3.2) — 2026-02-17

### Improvements

- Installer now auto-closes Root before install, repair, and uninstall
- Link embed text is more readable across all themes
- Link embed images load faster when scrolling through chat

---

## [v0.3.0](https://github.com/watchthelight/uprooted/releases/tag/v0.3.0) — 2026-02-17

### New

- **Console installer** — Lightweight console installer replaces the old GUI (~600KB vs ~100MB)
- **Debug mode** — Run the installer with `--debug` for live diagnostics
- **Link embeds** — Discord-style rich link previews and inline YouTube thumbnails
- KDE Plasma support for Linux

### Fixes

- Fixed blank/white window on Wayland (KDE Plasma, GNOME, Fedora)
- Fixed Linux compatibility issues

---

## [v0.2.5](https://github.com/watchthelight/uprooted/releases/tag/v0.2.5) — 2026-02-17

Bug fixes and improvements.

---

## [v0.2.3](https://github.com/watchthelight/uprooted/releases/tag/v0.2.3) — 2026-02-16

### Fixes

- Fixed crash on settings pages
- Fixed blank/white window on Wayland (KDE Plasma, Fedora, GNOME)
- Fixed file path handling on Linux

---

## [v0.2.1](https://github.com/watchthelight/uprooted/releases/tag/v0.2.1) — 2026-02-16

### Fixes

- Fixed crash when opening Uprooted settings pages

### Improvements

- Diagnostics card on the About page shows your log file location
- Better error logging for troubleshooting

---

## [v0.2.0](https://github.com/watchthelight/uprooted/releases/tag/v0.2.0) — 2026-02-16

### New

- Plugin cards with settings and info popups (replaces the old sidebar layout)
- Plugin testing status badges (Untested, Alpha, Beta, Closed)

---

## [v0.1.9](https://github.com/watchthelight/uprooted/releases/tag/v0.1.9) — 2026-02-16

### New

- **Live theme preview** — Dragging the color picker now updates the entire UI in real time
- **Color picker** — Pick any accent or background color with a hue slider and saturation/value plane

### Fixes

- Fixed window controls (close, minimize, drag) on Linux

---

## [v0.1.81](https://github.com/watchthelight/uprooted/releases/tag/v0.1.81) — 2026-02-15 (Pre-release)

Theme engine improvements.

---

## [v0.1.7](https://github.com/watchthelight/uprooted/releases/tag/v0.1.7) — 2026-02-15 (Pre-release)

### New

- **Custom theme engine** — Pick your own accent and background colors; all shades are auto-derived
- Preset theme cards: Default, Crimson, Loki

### Linux Support

- Linux builds (`.deb` and `.AppImage`)
- Standalone bash installer and uninstaller
- Arch Linux PKGBUILD
