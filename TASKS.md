# Tasks

> Active task board for Uprooted contributors. Pick up items, mark them done, add new ones.
> For strategic direction and long-term goals, see [docs/ROADMAP.md](docs/ROADMAP.md).
> For planned plugins and implementation strategies, see [docs/PLUGIN_ROADMAP.md](docs/PLUGIN_ROADMAP.md).

---

## Up Next

Short-term tasks ready to be picked up. Roughly priority-ordered.

- [ ] **MessageLogger: real-world validation** — Deletion pollers + edit detection both deployed. Validate: (1) trigger actual deletions — confirm HasBeenDeleted fires within 3s, red cards appear; (2) edit a message after 5s — confirm amber edit card appears; (3) run `scripts/analyze-msglogger.ps1` to check `[edit-gate]` / `[edit-detect]` log entries.
  - Files: `hook/MessageLogger.cs`

- [ ] **SilentTyping: validate C# interception** — `SilentTypingEngine` deployed (Phase 4.5f). Test with two accounts: type in one, confirm the other sees no typing indicator. Check hook log for `[SilentTyping] Blocked SetTypingIndicator`. If nothing is blocked, log will show which `HttpClient` instances were found/missed.
  - Files: `hook/SilentTypingEngine.cs`

- [ ] **NsfwFilter: validate Avalonia-native redesign** — Phase 4.5g deployed. Verify images are being classified and blurred. Check hook log for scan timer entries and Vision API calls. Confirm overlay + click-to-reveal works.
  - Files: `hook/NsfwFilter.cs`

- [ ] **Theme switch color inconsistencies** — Some controls show incorrect color tints immediately after switching themes (e.g. "User Settings" tab text appears brighter than intended) but display correctly after reopening the settings screen. Likely a stale recolor or priority issue in the visual tree walk that self-corrects when controls are rebuilt.
  - Files: `hook/ThemeEngine.cs`

## Testing

- [ ] **Set up Vitest** — Install Vitest, add `test`/`test:watch`/`test:coverage` scripts, co-locate test files as `.test.ts`.

- [ ] **PluginLoader unit tests** — Registration, start/stop lifecycle, patch install, event emission, race conditions.
  - Files: `src/core/pluginLoader.ts`

- [ ] **Settings unit tests** — JSON parsing, merge with defaults, deep merge, corrupted file recovery.
  - Files: `src/core/settings.ts`


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

---

_Update this file as you work. The `/hi` command reads it every session._
