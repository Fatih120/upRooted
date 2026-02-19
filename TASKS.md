# Tasks

> Active task board for Uprooted contributors. Pick up items, mark them done, add new ones.
> For strategic direction and long-term goals, see [docs/ROADMAP.md](docs/ROADMAP.md).
> For planned plugins and implementation strategies, see [docs/PLUGIN_ROADMAP.md](docs/PLUGIN_ROADMAP.md).

---

## Up Next

Short-term tasks ready to be picked up. Roughly priority-ordered.

- [ ] **MessageLogger: validate async deletion pollers** — Per-item async pollers deployed (HasBeenDeleted probe every 300ms for 3s, epoch-based cancellation on channel switch). Injection position fixed (order-based, not timestamp). Needs real-world validation: trigger actual message deletions and run `scripts/analyze-msglogger.ps1` to confirm HasBeenDeleted is set within the 3s window.
  - Files: `hook/MessageLogger.cs`

- [ ] **Theme switch color inconsistencies** — Some controls show incorrect color tints immediately after switching themes (e.g. "User Settings" tab text appears brighter than intended) but display correctly after reopening the settings screen. Likely a stale recolor or priority issue in the visual tree walk that self-corrects when controls are rebuilt.
  - Files: `hook/ThemeEngine.cs`

## Code Quality

Smaller fixes and cleanup that improve reliability.


## Testing

- [ ] **Set up Vitest** — Install Vitest, add `test`/`test:watch`/`test:coverage` scripts, co-locate test files as `.test.ts`.

- [ ] **PluginLoader unit tests** — Registration, start/stop lifecycle, patch install, event emission, race conditions.
  - Files: `src/core/pluginLoader.ts`

- [ ] **Settings unit tests** — JSON parsing, merge with defaults, deep merge, corrupted file recovery.
  - Files: `src/core/settings.ts`

- [x] **UprootedSettings INI parsing tests** (2026-02-18) — See `tests/UprootedTests/UprootedSettingsTests.cs`.
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

- [x] **Avalonia-native NSFW filter** (2026-02-18) — Rewrote `NsfwFilter.cs` to scan the Avalonia visual tree for `Image` controls, classify via Google Vision SAFE_SEARCH_DETECTION API in C# (no DotNetBrowser), and inject a native overlay. 500ms scan timer, `SemaphoreSlim` limits to 3 concurrent API calls, bitmapId-based dedup, PixelSize guard skips avatars, click-to-reveal, `RevealAllBlocked()` on toggle-off. Moved from Phase 5 (DotNetBrowser-gated) → Phase 4.5g (standalone, 20s delay).
  - Files: `hook/NsfwFilter.cs`, `hook/StartupHook.cs`

- [x] **MessageLogger: edit detection + Discord-style edit indicators** (2026-02-18) — Event-driven via CollectionChanged Replace: `HandleReplaced()` records edits only for messages seen via Add events and only if the Replace arrives >5s after Add (`EditGracePeriodSeconds`), filtering send-completion content settling. `InjectEditIndicators()` injects amber-tinted inline cards below edited messages (previous content italic+faded, `(edited)`/`(edited Nx)` label, amber left accent). Tag-based dedup + re-injection on VSP scroll recycling. Same Grid row pattern as deleted message cards.
  - Files: `hook/MessageLogger.cs`

- [x] **Silent Typing: C# reimplementation** (2026-02-18) — Replaced broken JS fetch/XHR intercept with `SilentTypingEngine`: patches Root's `HttpClient._handler` chain via `TypingBlockerHandler` (DelegatingHandler) that short-circuits `SetTypingIndicator` requests with a synthetic `200 OK`. Discovery via static field scan + ViewModel chain walk. TypeScript plugin gutted to no-op stub (v0.1.0 → v0.2.0). Plugin version promoted to TestingStatus=1 (beta).
  - Files: `hook/SilentTypingEngine.cs` (new), `hook/StartupHook.cs`, `src/plugins/silent-typing/index.ts`, `hook/ContentPages.cs`

- [x] **TypeScript code quality fixes** (2026-02-18) — 7 fixes: `after` patch callback invocation, `deepMerge` for settings (fixes nested-object overwrite), theme var names derived from `generateCustomVariables` keys, MutationObserver skips own `[data-uprooted]` mutations, sidebar click listener removed in cleanup (no more accumulation), `Object.freeze` on settings global, sentry-blocker `!` assertions replaced with null-check fallbacks.
  - Files: `src/core/pluginLoader.ts`, `src/core/settings.ts`, `src/plugins/themes/index.ts`, `src/plugins/settings-panel/panel.ts`, `src/core/preload.ts`, `src/plugins/sentry-blocker/index.ts`

- [x] **Theme click handlers** (2026-02-18) — `PointerPressed` handler calls `themeEngine.ApplyTheme(themeId)` / `themeEngine.RevertTheme()`, saves settings, triggers page rebuild. Implemented in `ContentPages.cs` lines 2187-2223.
  - Files: `hook/ContentPages.cs`, `hook/ThemeEngine.cs`

- [x] **Vigorous unit testing + Docker sandbox** (2026-02-18) — 170 tests total (113 new); zero bugs found in the three pure-logic modules. Docker test infrastructure for both unit tests and Linux installer.
  - New test files: `ClearUrlsEngineTests.cs` (58), `UprootedSettingsTests.cs` (22), `MessageStoreTests.cs` (18)
  - New stubs: `TestStubs/Logger.cs`, `PlatformPaths.cs`, `AvaloniaReflection.cs`, `VisualTreeWalker.cs`
  - Docker: `tests/Dockerfile.unittest`, `tests/run-docker-tests.sh`
  - Installer sandbox: `tests/docker-installer/Dockerfile` (curl shim + Ubuntu 24.04 end-to-end install test)
  - Bug report: `tests/BUG_REPORT.md`
  - Files: `tests/UprootedTests/`

- [x] **ClearURLs plugin** (2026-02-18) — Strips tracking parameters (utm_source, fbclid, gclid, si, etc.) from URLs when the user sends a message. Hooks AvaloniaEdit.TextEditor's TextArea via Avalonia routed events with `AddHandler(handledEventsToo=true)` using all routing strategies (Bubble|Tunnel|Direct). 33 tracking params, idempotent, fragment-preserving. Plugin ID: `clear-urls`, default enabled.
- [x] **Animated image embeds (.gif, .webp)** (2026-02-17) — Animated GIF/WebP URLs play inline with frame-accurate timing via SkiaSharp `SKCodec` reflection (`AnimatedImage.cs`). Graceful fallback to static first frame if SkiaSharp frame APIs are unavailable. Tenor URLs skipped (Root embeds natively).
- [x] **Fix link embeds for non-YouTube sites** (2026-02-17) — Chrome-like default UA, direct image URL fast path, Content-Type gate on OG fetch, oEmbed discovery from HTML `<link>` tags, bot UA for Twitter/X and embed-fixer domains (vxtwitter, fxtwitter, fixupx), `twitter:image`/`twitter:title`/`twitter:description` fallbacks
- [x] **Fix oEmbed parsing crash** (2026-02-17) — `DecodeJsonString` used `Regex.Replace` with lambda `MatchEvaluator` which was trimmed in Root's binary. Replaced with manual `\uXXXX` loop. Fixed `ReadAsStringAsync` → `ReadAsStreamAsync` for trimmed charset methods. Normalized fixupx/fxtwitter/fixvx URLs to vxtwitter.com for richer OG metadata with images.
- [x] **Plugin toggle functionality** (2026-02-18) — Plugin toggles save state to settings, show restart banner when state diverges from initial (hides on revert). Themes apply live (no restart needed). Restart button launches new Root.exe and exits.
- [x] **ProfileBadgeInjector** (2026-02-18) — "Uprooted Dev" badge on profile popups for known developer usernames only (hardcoded `DeveloperUsernames` HashSet). Event-driven detection via OverlayLayer.Children CollectionChanged + 500ms fallback poll for TopLevel popups. Phase 4.5e startup delay reduced from 25s to 5s.
- [x] **Refine ProfileBadgeInjector heuristics** (2026-02-18) — `IsProfilePopup` tightened: avatar minimum 30px→40px; `StatusWords` HashSet ("Online"/"Away"/"Busy"/"Offline"/"Do Not Disturb"); `textBlockCount`; conjunction now requires `hasRolesText` OR `(hasLargeText AND hasStatusText)` OR `(hasLargeText AND textBlockCount≥3)`. Rejects notification toasts (avatar + sender name, no status word, ≤2 TextBlocks).
  - Files: `hook/ProfileBadgeInjector.cs`
- [x] **Restart banners + Diagnostics Open button** (2026-02-18) — Plugins page: state-aware amber restart banner. Updates section: green restart banner after update applied. Both with Restart button. DIAGNOSTICS card: "Open" button opens log file in Explorer.
- [x] **Reddit link embeds** (2026-02-18) — Dedicated handler in LinkEmbedEngine with `old.reddit.com` OG fetch, subreddit provider label (e.g. "r/programming"), Reddit orange (#FF4500) accent color. Falls through to generic OG if no title found. Also fixed: image embed borders (all 4 corners rounded), HTTP status/Content-Type validation in `HttpGetBytes`, OG fallback for image-extension URLs that serve HTML (Zipline `/view/`, `/u/`), robust OG regex for meta tags with extra attributes.
- [x] **Custom ping/reply highlight color** (2026-02-18) — Standalone `CustomPingColor` setting in UprootedSettings that persists across theme switches. "HIGHLIGHT OVERRIDE" card on Themes page with toggle indicator, color input row, swatch with color picker popup, reset button. ThemeEngine overrides `HighlightForegroundColor`, `HighlightForegroundBrush`, and `TextSelectionHighlightColor` (0x60 alpha) in both Styles[0].Resources and injected MergedDictionary. Applied as Phase 6 after theme apply + after live preview updates. Restored to theme defaults on clear.
- [x] **Video preview embeds (.mp4, .webm, .mov)** (2026-02-18) — Direct video URLs show a dark 16:9 placeholder with centered play button overlay; clicking opens in default browser (same UX as YouTube embeds). Detected by extension (`VideoUrlRegex`) or `video/*` Content-Type for extensionless URLs. Zero HTTP fetch for extension-matched URLs. `SynthesizeVideoEmbed()` sets `VideoId="direct"` to trigger play button behavior; `BuildVideoPlaceholder()` creates dark background (#1a1a2e) with 56px semi-transparent play circle.
- [x] **Hide file names on image/video embeds** (2026-02-18) — Image-only embeds hide filename title by default. New `LinkEmbedsShowFilenames` setting (INI: `LinkEmbeds.ShowFilenames`, default false). Settings lightbox toggle in LinkEmbeds plugin settings. `RefreshTitleVisibility()` updates all live cards instantly. `LinkEmbedEngine.Instance` static property enables ContentPages to trigger refresh.
- [x] **Fix embed card accent color not reflecting theme** (2026-02-18) — Embed cards previously used hardcoded Discord blue (`#5865F2`) for their left border strip regardless of active theme. `LinkEmbedEngine` now accepts `ThemeEngine?` in its constructor and uses `GetAccentColor()` for new cards. `NotifyThemeChanged()` walks all live injected cards and updates colors — preserving site-specific OG colors (Reddit orange, `og:theme-color`) while replacing default-accent cards with the current theme accent. Wired into `ApplyThemedColors` (page rebuild) and `UpdateLiveColors` (live color picker drag).
- [x] **Merge Ping Color into Custom Theme card** (2026-02-18) — Ping Color toggle moved from its own standalone card into the Custom Theme card, separated by a thin 1px divider line. Removed `BuildPingColorSection()` method entirely.

---

_Update this file as you work. The `/hi` command reads it every session._
