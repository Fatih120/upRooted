# Uprooted v0.5.0

We're releasing **Uprooted v0.5.0**: the biggest update since launch. New plugins, a redesigned theme engine, three release channels, and a full CI/CD pipeline backing everything.

## What's New

### Theme Engine v2
The theme system has been completely rewritten. Eight built-in presets ship out of the box: Crimson, Cosmic Smoothie, Loki, Marine, Oreo, Sakura, Ember, and Native. The engine now uses OKLCH color science for perceptually uniform palette generation, meaning custom themes look consistent across light and dark variants. Live preview updates in real-time as you adjust colors. No restart needed.

### New Plugins
- **Translate**: Right-click any message to translate it via DeepL. Configurable source/target language and API key.
- **Who Reacted**: See who reacted to a message directly on the reaction pill. Circular avatars render inline.
- **User Bio**: Set a short bio visible to other Uprooted users. Edit your own bio live from your profile card with a character counter and auto-save.
- **Presence Beacon**: Detect other Uprooted users in your community via gRPC metadata.

### Existing Plugin Improvements
- **Link Embeds**: Now supports animated GIFs/WebP, video embeds, and oEmbed providers.
- **Message Logger**: Major reliability overhaul: property-based edit detection, author name resolution, self-delete fallback.
- **ClearURLs**: Expanded tracking parameter list, international domain support.
- **Silent Typing**: Rewritten to use DiagnosticListener interception instead of bridge patching. More reliable, zero performance impact.
- **NSFW Filter**: Full Avalonia-native redesign with click-to-reveal overlay.

### Three Release Channels
You can now choose your update speed from the About page:
- **Stable**: Tested releases only. The default.
- **Canary**: Bleeding-edge builds from main. Toggle the switch on the About page.
- **Developer**: Invite-only. You know how to find it.

The auto-updater checks your channel every 60 seconds and applies updates in the background. Changes take effect on next restart.

### Settings UI Overhaul
- DPI-aware borders and vector icons throughout
- Plugin cards now show 6 by default (3 rows) with a "Show more" expansion
- Cycling status pill, radio indicators, and toggle thumb animations
- Search box filters plugins in real-time

### Under the Hood
- **Structured logging**: ~100 wide events replace ~1200 log calls. Tail sampling on high-frequency scan loops.
- **Auto-updater**: Encrypted `.uprpkg` packages with SHA-256 integrity verification. Silent hotfix detection for same-version updates.
- **Performance**: In-place theme switching (no full revert cycle), bind-once walker for DynamicResource bindings, WeakRef tracking for O(~16) live preview updates.
- **Cross-platform**: macOS profiler and installer stubs for forward compatibility. Full CI/CD pipeline builds Windows, Linux (x64/arm64), and macOS (x64/arm64) from a single workflow.

## Install

Downloads are server-gated. Join the [Uprooted server](https://rootapp.gg/AC0ILwUxgQqJ2MOSMXdGjw) on Root to get the installer. There are no public download links.

## Links
- [Server](https://rootapp.gg/AC0ILwUxgQqJ2MOSMXdGjw) (join here to download)
- [Source](https://github.com/The-Uprooted-Project/uprooted)
- [Website](https://uprooted.sh)
