# Tasks

> Active task board for Uprooted contributors. Pick up items, mark them done, add new ones.
> For strategic direction and long-term goals, see [docs/ROADMAP.md](docs/ROADMAP.md).
> For planned plugins and implementation strategies, see [docs/PLUGIN_ROADMAP.md](docs/PLUGIN_ROADMAP.md).

---

## Up Next

Short-term tasks ready to be picked up. Roughly priority-ordered.

- [ ] **MessageLogger: fix false-positive deletions** — Root's ObservableCollection is a windowed/virtualized data source; Remove events fire for buffer management (scroll-off, window shifts), not just real deletions. Diagnostic instrumentation deployed (probes HasBeenDeleted, tracks Add/Remove correlation, logs remove indices). Pending: analyze logs, implement Phase 2 detection fix (Strategy A: HasBeenDeleted property, B: Add/Remove correlation, or C: re-verification queue), then Phase 3 injection position fix (insert near original position, not at bottom).
  - Files: `hook/MessageLogger.cs`

- [ ] **MessageLogger: fix edit detection** — Edit detection (`PollEdits`) is currently disabled due to false positives from content changes during message send/render. Need a reliable approach — likely snapshot-on-Add (only compare content of messages that arrived via Add events, not initial snapshot) or property-change subscription if Root exposes one.
  - Files: `hook/MessageLogger.cs`

- [ ] **MessageLogger: Discord-style edit indicators** — Show original content faded above the new content with an "(edited)" label, matching Discord's MessageLogger style. Requires working edit detection first.
  - Files: `hook/MessageLogger.cs`

- [ ] **Avalonia-native NSFW filter** — Redesign NSFW filter to intercept image-bearing controls in the Avalonia visual tree instead of JS injection into DotNetBrowser.
  - Files: `hook/NsfwFilter.cs`

- [ ] **Refine ProfileBadgeInjector heuristics** — Detection is now event-driven (OverlayLayer CollectionChanged) with dev-username gating. Still needed: check tree dump logs from first real popup detection to refine `IsProfilePopup` heuristic (currently matches any popup with avatar image + large text, may false-positive on non-profile popups).
  - Files: `hook/ProfileBadgeInjector.cs`

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

- [x] **ClearURLs plugin** (2026-02-18) — Strips tracking parameters (utm_source, fbclid, gclid, si, etc.) from URLs when the user sends a message. Hooks AvaloniaEdit.TextEditor's TextArea via Avalonia routed events with `AddHandler(handledEventsToo=true)` using all routing strategies (Bubble|Tunnel|Direct). 33 tracking params, idempotent, fragment-preserving. Plugin ID: `clear-urls`, default enabled.
- [x] **Animated image embeds (.gif, .webp)** (2026-02-17) — Animated GIF/WebP URLs play inline with frame-accurate timing via SkiaSharp `SKCodec` reflection (`AnimatedImage.cs`). Graceful fallback to static first frame if SkiaSharp frame APIs are unavailable. Tenor URLs skipped (Root embeds natively).
- [x] **Fix link embeds for non-YouTube sites** (2026-02-17) — Chrome-like default UA, direct image URL fast path, Content-Type gate on OG fetch, oEmbed discovery from HTML `<link>` tags, bot UA for Twitter/X and embed-fixer domains (vxtwitter, fxtwitter, fixupx), `twitter:image`/`twitter:title`/`twitter:description` fallbacks
- [x] **Fix oEmbed parsing crash** (2026-02-17) — `DecodeJsonString` used `Regex.Replace` with lambda `MatchEvaluator` which was trimmed in Root's binary. Replaced with manual `\uXXXX` loop. Fixed `ReadAsStringAsync` → `ReadAsStreamAsync` for trimmed charset methods. Normalized fixupx/fxtwitter/fixvx URLs to vxtwitter.com for richer OG metadata with images.
- [x] **Plugin toggle functionality** (2026-02-18) — Plugin toggles save state to settings, show restart banner when state diverges from initial (hides on revert). Themes apply live (no restart needed). Restart button launches new Root.exe and exits.
- [x] **ProfileBadgeInjector** (2026-02-18) — "Uprooted Dev" badge on profile popups for known developer usernames only (hardcoded `DeveloperUsernames` HashSet). Event-driven detection via OverlayLayer.Children CollectionChanged + 500ms fallback poll for TopLevel popups. Phase 4.5e startup delay reduced from 25s to 5s.
- [x] **Restart banners + Diagnostics Open button** (2026-02-18) — Plugins page: state-aware amber restart banner. Updates section: green restart banner after update applied. Both with Restart button. DIAGNOSTICS card: "Open" button opens log file in Explorer.
- [x] **Reddit link embeds** (2026-02-18) — Dedicated handler in LinkEmbedEngine with `old.reddit.com` OG fetch, subreddit provider label (e.g. "r/programming"), Reddit orange (#FF4500) accent color. Falls through to generic OG if no title found. Also fixed: image embed borders (all 4 corners rounded), HTTP status/Content-Type validation in `HttpGetBytes`, OG fallback for image-extension URLs that serve HTML (Zipline `/view/`, `/u/`), robust OG regex for meta tags with extra attributes.
- [x] **Custom ping/reply highlight color** (2026-02-18) — Standalone `CustomPingColor` setting in UprootedSettings that persists across theme switches. "HIGHLIGHT OVERRIDE" card on Themes page with toggle indicator, color input row, swatch with color picker popup, reset button. ThemeEngine overrides `HighlightForegroundColor`, `HighlightForegroundBrush`, and `TextSelectionHighlightColor` (0x60 alpha) in both Styles[0].Resources and injected MergedDictionary. Applied as Phase 6 after theme apply + after live preview updates. Restored to theme defaults on clear.
- [x] **Video preview embeds (.mp4, .webm, .mov)** (2026-02-18) — Direct video URLs show a dark 16:9 placeholder with centered play button overlay; clicking opens in default browser (same UX as YouTube embeds). Detected by extension (`VideoUrlRegex`) or `video/*` Content-Type for extensionless URLs. Zero HTTP fetch for extension-matched URLs. `SynthesizeVideoEmbed()` sets `VideoId="direct"` to trigger play button behavior; `BuildVideoPlaceholder()` creates dark background (#1a1a2e) with 56px semi-transparent play circle.
- [x] **Hide file names on image/video embeds** (2026-02-18) — Image-only embeds hide filename title by default. New `LinkEmbedsShowFilenames` setting (INI: `LinkEmbeds.ShowFilenames`, default false). Settings lightbox toggle in LinkEmbeds plugin settings. `RefreshTitleVisibility()` updates all live cards instantly. `LinkEmbedEngine.Instance` static property enables ContentPages to trigger refresh.

---

_Update this file as you work. The `/hi` command reads it every session._
