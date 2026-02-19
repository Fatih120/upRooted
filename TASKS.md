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

- [ ] **Custom theme: Root settings controls don't recolor instantly** — Toggling Root's native selectors and switches while a custom theme is active doesn't immediately update their accent color. Colors correct themselves after changing tabs and reloading the page. Likely the visual tree walk doesn't re-trigger on control state changes (checked→unchecked, etc.).
  - Files: `hook/ThemeEngine.cs`

## Testing

- [ ] **Set up Vitest** — Install Vitest, add `test`/`test:watch`/`test:coverage` scripts, co-locate test files as `.test.ts`.

- [ ] **PluginLoader unit tests** — Registration, start/stop lifecycle, patch install, event emission, race conditions.
  - Files: `src/core/pluginLoader.ts`

- [ ] **Settings unit tests** — JSON parsing, merge with defaults, deep merge, corrupted file recovery.
  - Files: `src/core/settings.ts`


## ILSpy Research

Tasks from [`research/ilspy_dump_index.md`](research/ilspy_dump_index.md). The raw dumps are in `research/ilspy-dumps/`.

### Analyze existing dumps (marked "N" — not yet distilled into docs)

**Style files:**
- [ ] **Analyze Style_DropDownButton.cs** (136 lines) — DropDownButton: BackgroundTertiary bg, Border border
- [ ] **Analyze Style_DragTabItem.cs** (54 lines) — DragTabItem: draggable tab styling
- [ ] **Analyze Style_FlyoutPresenter.cs** (55 lines) — FlyoutPresenter: popup container
- [ ] **Analyze Style_MenuFlyoutPresenter.cs** (71 lines) — MenuFlyoutPresenter: context menu container. Relevant to Translate plugin (right-click context menu).
- [ ] **Analyze Style_RootColorPicker.cs** (106 lines) — RootColorPicker: color picker control
- [ ] **Analyze Style_RootImageLoader.cs** (76 lines) — RootImageLoader: image loading/placeholder
- [ ] **Analyze Style_BorderlessTextbox.cs** (75 lines) — BorderlessTextbox: no-border text input
- [ ] **Analyze Style_TextButton.cs** (73 lines) — TextButton: text-only button variant
- [ ] **Analyze Style_ToolTip.cs** (71 lines) — ToolTip: tooltip popup styling
- [ ] **Analyze Style_TabsControl.cs** (48 lines) — TabsControl: tab container
- [ ] **Analyze Style_TabsTheme.cs** (53 lines) — TabsTheme: tab visual theme
- [ ] **Analyze StylesAll.cs** (5,228 lines) — Aggregate of all 27 style registrations; useful for understanding load order

**Newly split — high priority (unblocks active work):**
- [ ] **Analyze RootSettingsContainer.cs** (990 lines) — Settings page host: save bar, page switching, revert. Fixes save bar collapse hack in SidebarInjector.
- [ ] **Analyze MainViewModel.cs** (400 lines) — Top-level VM: DataContext chain root, navigation, DI. Improves DotNetBrowser discovery.
- [ ] **Analyze RootMessageItemsControl.cs** (378 lines) — Virtualized message list. Critical for MessageLogger: reveals recycling strategy.
- [ ] **Analyze RootMenuFlyout.cs** (97 lines) — Context menu. Prerequisite for Translate plugin.
- [ ] **Analyze MainWindow.cs** (317 lines) — Top-level window: title bar, tray, close/minimize.
- [ ] **Analyze MainView.cs** (259 lines) — Main content shell: hosts RootSplitView, navigation.
- [ ] **Analyze ILocalDataStore.cs** (13 lines) + **LocalDataStore.cs** (271 lines) + **LocalDataStoreExtensions.cs** (232 lines) — Settings persistence: full API surface for per-user settings.

**Newly split — medium priority (useful context):**
- [ ] **Analyze RootTextbox.cs** (996 lines) — Custom textbox with validation, character limit, helper text
- [ ] **Analyze RootConfirmationControl.cs** (901 lines) — Confirmation dialog (delete/leave/ban flows)
- [ ] **Analyze RootMessageScrollViewer.cs** (645 lines) — Message list scroll container, load-more behavior
- [ ] **Analyze RootScrollViewer.cs** (431 lines) — Custom scroll viewer
- [ ] **Analyze SaveChangesView.cs** (396 lines) — Settings save/revert bar UI
- [ ] **Analyze RootMultiCheckBox.cs** (478 lines) — Multi-option checkbox group
- [ ] **Analyze RootMemberVisibilitySwitch.cs** (747 lines) — Member visibility toggle
- [ ] **Analyze MenuItemPageContainerView.cs** (281 lines) + **MenuItemPageContainerViewModel.cs** (135 lines) — Settings page container
- [ ] **Analyze RootSvgImage.cs** (144 lines) — SVG image with per-theme path binding
- [ ] **Analyze RootImageLoader.cs** (178 lines) — Image loading control
- [ ] **Analyze RootFlyout.cs** (172 lines) — Flyout popup base
- [ ] **Analyze SecureStorageImplementation.cs** (86 lines) — Encrypted credential storage

### Complete partial analysis

- [ ] **Complete ChatView.cs analysis** (424 lines) — Settings > Chat page: emoji toggle, tap-to-reply toggle. May reveal additional DataStore keys.
- [ ] **Complete App.cs analysis** (635 lines) — Application subclass: Initialize(), resource/style registration. Theme dict structure already extracted; remaining: style registration order, resource loading.
- [ ] **Complete MessageViewModel.cs analysis** (786 lines) — Message data model: HasSelfMention, HasLocalPendingReply, content, timestamps, edit state. Critical for MessageLogger and future message plugins.
- [ ] **Complete Style_ComboBox.cs analysis** (521 lines) — ComboBox template, dropdown, input background
- [ ] **Complete Style_MenuItem.cs analysis** (193 lines) — MenuItem: hover states, DeleteMenuItem class. Relevant to context menu injection (Translate plugin).
- [ ] **Complete Style_Slider.cs analysis** (238 lines) — Slider: BrandPrimary foreground, Border track background
- [ ] **Complete Style_RootSplitView.cs analysis** (626 lines) — Main app shell layout (sidebar + content split). Important for understanding Root's top-level layout.
- [ ] **Complete Application.cs analysis** (394 lines) — Avalonia Application base: Resources, Styles, ActualThemeVariantChanged, TryGetResource()
- [ ] **Complete ToggleSwitch.cs analysis** (88 lines) — ToggleSwitch: template parts, knob transitions
- [ ] **Complete SimpleTheme.cs analysis** (1,022 lines) — SimpleTheme base: ThemeControlHighlightLowBrush and other base keys

### Decompile new classes (not yet in ilspy-dumps/)

Remaining targets after batch split (Controls, Settings, UI.Main, Domain.Helpers.Store now done):

- [ ] **Decompile IRootSessionAccessor** — Session/user info service. Needed for per-user features.
- [ ] **Decompile profile popup views** — Profile popup structure. Would refine ProfileBadgeInjector's `IsProfilePopup` heuristic.
- [ ] **Decompile channel list / DM list views** — Channel sidebar. Low priority until channel-related plugins are planned.
- [ ] **Decompile navigation service** — How Root switches between pages. Would enable programmatic navigation.
- [ ] **Decompile MembersViewModel / CommunityLogViewModel / CommunityTabViewModel** — Referenced in XamlIlTrampolines, not yet extracted.
- [ ] **Decompile CTextBlock / CSpan / CHyperlink / CRun / CCode** — Markdown internals (sub-types of RootMarkdownTextBlock). Unlocks message content manipulation.

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
