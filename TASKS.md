# Tasks

> Active task board for Uprooted contributors. Pick up items, mark them done, add new ones.
> For strategic direction and long-term goals, see [docs/ROADMAP.md](docs/ROADMAP.md).

---

## Up Next

Short-term tasks ready to be picked up. Roughly priority-ordered.

- [ ] **Hide file names on image/video embeds** — Direct image/video link embeds show the file name as the title, but it's redundant since the URL is already visible. Hide file names by default, keep the domain label. Add a "Show embedded file names" toggle in the Link Embeds plugin settings.
  - Files: `hook/LinkEmbedEngine.cs`, `hook/ContentPages.cs`, `hook/UprootedSettings.cs`

- [ ] **Reddit link embeds** — Reddit post URLs should render rich embeds (title, subreddit, vote count, comment count). Reddit serves OG tags to crawlers; may need bot UA similar to Twitter/X handling.
  - Files: `hook/LinkEmbedEngine.cs`

- [ ] **Video preview embeds (.mp4)** — Direct `.mp4` URLs should show a thumbnail preview frame with play button overlay (similar to YouTube embeds). Currently get Cloudflare challenge pages or no embed. Consider extracting a frame server-side or using a placeholder with click-to-open.
  - Files: `hook/LinkEmbedEngine.cs`

- [ ] **Avalonia-native NSFW filter** — Redesign NSFW filter to intercept image-bearing controls in the Avalonia visual tree instead of JS injection into DotNetBrowser.
  - Files: `hook/NsfwFilter.cs`

- [ ] **Plugin toggle functionality** — Wire up toggle controls on the native Plugins page so users can enable/disable plugins at runtime. Currently display-only.
  - Files: `hook/ContentPages.cs`, `hook/UprootedSettings.cs`

- [ ] **Theme click handlers** — Add click handlers to the native Themes page so users can switch themes from the Avalonia settings UI.
  - Files: `hook/ContentPages.cs`, `hook/ThemeEngine.cs`

- [ ] **Implement `after` patch handler** — The `after` callback is defined in the `Patch` interface but `PluginLoader` never invokes it. Implement post-execution invocation.
  - Files: `src/core/pluginLoader.ts`, `src/types/plugin.ts`

- [ ] **Deep merge for settings** — Replace shallow spread merge with recursive merge that correctly combines nested objects (especially `plugins` map).
  - Files: `src/core/settings.ts`

- [ ] **Theme switch color inconsistencies** — Some controls show incorrect color tints immediately after switching themes (e.g. "User Settings" tab text appears brighter than intended) but display correctly after reopening the settings screen. Likely a stale recolor or priority issue in the visual tree walk that self-corrects when controls are rebuilt.
  - Files: `hook/ThemeEngine.cs`

## Code Quality

Smaller fixes and cleanup that improve reliability.

- [ ] **Non-null assertion guards in sentry-blocker** — `originalFetch!.call()` risks crash if `stop()` called mid-request. Add runtime null guards.
  - Files: `src/plugins/sentry-blocker/index.ts`

- [ ] **Scope MutationObserver to sidebar** — Settings panel observer watches entire document body including its own elements. Scope to sidebar subtree, skip `data-uprooted` mutations.
  - Files: `src/plugins/settings-panel/panel.ts`

- [ ] **Theme variable cleanup consolidation** — Two separate CSS variable name lists for cleanup. Consolidate into single source of truth.
  - Files: `src/plugins/themes/index.ts`

- [ ] **Event listener cleanup in settings panel** — Sidebar click listeners accumulate on repeated visits. Switch to event delegation, clean up in `stopObserving()`.
  - Files: `src/plugins/settings-panel/panel.ts`

- [ ] **Freeze window globals** — `Object.freeze()` on `window.__UPROOTED_SETTINGS__` to prevent tampering. Restrict loader access.
  - Files: `src/core/preload.ts`, `src/core/settings.ts`

## Testing

- [ ] **Set up Vitest** — Install Vitest, add `test`/`test:watch`/`test:coverage` scripts, co-locate test files as `.test.ts`.

- [ ] **PluginLoader unit tests** — Registration, start/stop lifecycle, patch install, event emission, race conditions.
  - Files: `src/core/pluginLoader.ts`

- [ ] **Settings unit tests** — JSON parsing, merge with defaults, deep merge, corrupted file recovery.
  - Files: `src/core/settings.ts`

- [ ] **UprootedSettings INI parsing tests** — Valid files, missing keys, malformed lines, empty files.
  - Files: `hook/UprootedSettings.cs`, `tests/UprootedTests/`

## Ideas / Backlog

Items not yet committed to but worth tracking.

- [ ] Simplify user-facing descriptions on About and Themes settings tabs — current text exposes too many backend/implementation details (e.g. "ResourceDictionary overrides", "reflection"). Rewrite for end users.
  - Files: `hook/ContentPages.cs`
- [ ] Structural fallback for settings page detection (currently depends on "APP SETTINGS" text)
- [ ] Avalonia version detection + graceful degradation
- [ ] Bridge version negotiation (`__nativeToWebRtc` / `__webRtcToNative`)
- [ ] ESLint/Biome + Prettier for TypeScript layer
- [ ] Plugin lifecycle error recovery (rollback on `start()` rejection)

## Done

Move completed items here with the date.

- [x] **Animated image embeds (.gif, .webp)** (2026-02-17) — Animated GIF/WebP URLs play inline with frame-accurate timing via SkiaSharp `SKCodec` reflection (`AnimatedImage.cs`). Graceful fallback to static first frame if SkiaSharp frame APIs are unavailable. Tenor URLs skipped (Root embeds natively).
- [x] **Fix link embeds for non-YouTube sites** (2026-02-17) — Chrome-like default UA, direct image URL fast path, Content-Type gate on OG fetch, oEmbed discovery from HTML `<link>` tags, bot UA for Twitter/X and embed-fixer domains (vxtwitter, fxtwitter, fixupx), `twitter:image`/`twitter:title`/`twitter:description` fallbacks
- [x] **Fix oEmbed parsing crash** (2026-02-17) — `DecodeJsonString` used `Regex.Replace` with lambda `MatchEvaluator` which was trimmed in Root's binary. Replaced with manual `\uXXXX` loop. Fixed `ReadAsStringAsync` → `ReadAsStreamAsync` for trimmed charset methods. Normalized fixupx/fxtwitter/fixvx URLs to vxtwitter.com for richer OG metadata with images.

---

_Update this file as you work. The `/hi` command reads it every session._
