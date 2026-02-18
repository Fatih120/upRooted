# Tasks

> Active task board for Uprooted contributors. Pick up items, mark them done, add new ones.
> For strategic direction and long-term goals, see [docs/ROADMAP.md](docs/ROADMAP.md).

---

## Up Next

Short-term tasks ready to be picked up. Roughly priority-ordered.

- [ ] **Fix link embeds for non-YouTube sites** — The Avalonia-native engine (`LinkEmbedEngine.cs`, Phase 4.5b) works for YouTube but fails on many other sites. Sub-tasks:
  - [ ] **Better User-Agent** — Current UA is `Mozilla/5.0 (compatible; Uprooted/0.2)` which Twitter/X and others reject as a bot. Switch to a browser-like UA string (e.g. Chrome on Windows). Set in `EnsureHttpResolved()` where `TryAddWithoutValidation` is called.
  - [ ] **Direct image URL embeds** — URLs ending in `.jpg`/`.png`/`.gif`/`.webp` or returning `image/*` Content-Type get fed through `FetchOgMetadata` which expects HTML. Add early detection: check URL extension or do a HEAD request, skip OG parsing, render an image-only embed card.
  - [ ] **oEmbed for Twitter/Reddit** — YouTube works because it has a dedicated oEmbed path (`FetchYouTubeMetadata`). Add similar paths for Twitter (`publish.twitter.com/oembed`), Reddit (`www.reddit.com/oembed`), and other major providers. Could generalize into a provider registry pattern.
  - [ ] **Content-Type gate on OG fetch** — `FetchOgMetadata` calls `HttpGetString(url)` blindly. Should check `Content-Type` header first (via HEAD or from the GET response) and bail early for non-HTML (PDFs, binaries, etc.) instead of trying to parse them as HTML.
  - Files: `hook/LinkEmbedEngine.cs` — all changes are in this one file
  - Why YouTube works: dedicated oEmbed API returns clean JSON, thumbnail URL is predictable (`img.youtube.com/vi/{id}/hqdefault.jpg`), no HTML parsing needed
  - Why others fail: `HttpGetString` fetches raw HTML → regex parses OG tags → many sites serve no/empty OG to bot UAs, or need JS rendering, or aren't HTML at all

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

- [ ] Structural fallback for settings page detection (currently depends on "APP SETTINGS" text)
- [ ] Avalonia version detection + graceful degradation
- [ ] Bridge version negotiation (`__nativeToWebRtc` / `__webRtcToNative`)
- [ ] ESLint/Biome + Prettier for TypeScript layer
- [ ] Plugin lifecycle error recovery (rollback on `start()` rejection)

## Done

Move completed items here with the date.

<!-- - [x] **Example task** (2026-02-17) — Brief note on what was done -->

---

_Update this file as you work. The `/hi` command reads it every session._
