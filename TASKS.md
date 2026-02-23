# Tasks

> Active task board for Uprooted contributors. Pick up items, mark them done, add new ones.
> For strategic direction: [docs/ROADMAP.md](docs/ROADMAP.md). For plugin plans: [docs/PLUGIN_ROADMAP.md](docs/PLUGIN_ROADMAP.md). For known bugs: [BUGS.md](BUGS.md).

---

## Priority Focus

Current focus areas (in order):
1. **Bugfixes** — broken functionality, crashes, regressions
2. **Visual consistency** — theme/UI mismatches, flash, stale colors
3. **Performance** — startup time, memory, CPU
4. **Anti-mitigation hardening** — survive Root updates, structural fallbacks
5. **Organization** — structured logging, testing, linting
6. **Hardening Alpha/Beta plugins** — production-ready validation
7. **Experimental plugins** — lowest priority, validation only

---

## Ready to Build

### Bugfixes


- [ ] **MessageLogger card positioning** — `FindMessageGridInContainer` returns null. Container structure may have changed; needs investigation.
  - Files: `hook/MessageLogger.cs`

- [ ] **ProfileBadgeInjector false-positive refinement** — `IsProfilePopup` heuristic may match non-profile popups. Needs tree dump log analysis.
  - Files: `hook/ProfileBadgeInjector.cs`

### Visual Consistency

- [ ] **Settings detection flash after long idle** — After 5+ minutes without touching settings, the first open shows a brief flash of Root's User Profile tab before auto-navigating to About. Likely cause: LayoutUpdated stops firing when no layout changes occur during idle.
  - Files: `hook/SidebarInjector.cs`

### Anti-Mitigation Hardening

- [ ] **Structural fallback for settings page detection** — Remove "APP SETTINGS" text dependency.
  - Files: `hook/VisualTreeWalker.cs`, `hook/SidebarInjector.cs`

- [ ] **Avalonia version detection + graceful degradation**
  - Files: `hook/AvaloniaReflection.cs`

- [ ] **Bridge version negotiation** (`__nativeToWebRtc` / `__webRtcToNative`)
  - Files: `src/api/bridge.ts`

### Organization

- [ ] **Set up Vitest** — Install Vitest, add `test`/`test:watch`/`test:coverage` scripts, co-locate test files as `.test.ts`.

- [ ] **PluginLoader unit tests** — Registration, start/stop lifecycle, patch install, event emission, race conditions.
  - Files: `src/core/pluginLoader.ts`

- [ ] **Settings unit tests** — JSON parsing, merge with defaults, deep merge, corrupted file recovery.
  - Files: `src/core/settings.ts`

- [ ] **ESLint/Biome + Prettier** for TypeScript layer

### Alpha/Beta Plugin Hardening

- [ ] **ClearURLs edge case testing** — Validate with unusual URL patterns, international domains, edge cases in compose editor.
  - Files: `hook/ClearUrlsEngine.cs`

- [ ] **SilentTyping second-account verification** — C# engine confirmed blocking SetTypingIndicator in log. Need second-account test to verify typing indicator actually suppressed.
  - Files: `hook/SilentTypingEngine.cs`

### Experimental (lowest priority)

- [ ] **Rootcord: validate tab-switch highlight fix** — `RefreshSelectedHighlight()` IsBorder guards added. Enable Rootcord → click server icons → check hook log.
  - Files: `hook/RootcordEngine.cs`

- [ ] **Rootcord: validate user card + 4-button cluster** — Profile intercept removed, avatar/text→ProfilePane, P/D/N/⚙ buttons.
  - Files: `hook/RootcordEngine.cs`

- [ ] **MessageLogger: real-world validation** — Major reliability overhaul deployed: property fix, author names, INPC detection, self-delete fallback. Validate delete/edit detection, card positioning, edit indicators.
  - Files: `hook/MessageLogger.cs`

- [ ] **NsfwFilter production validation** — Phase 4.5g Avalonia-native redesign deployed. Verify image classification, blur, overlay + click-to-reveal.
  - Files: `hook/NsfwFilter.cs`

- [ ] **Translate plugin validation** — DeepL-powered translation deployed. Verify language detection, translation quality, config popup.
  - Files: `hook/TranslateEngine.cs`, `hook/TranslateConfigPopup.cs`

- [ ] **Presence Beacon validation** — Uprooted user detection via gRPC metadata deployed. Verify detection, badge display.
  - Files: `hook/UprootedPresenceBeacon.cs`

- [ ] **WhoReacted validation** — Shows reactor avatars next to reaction pills. Verify avatar download, circular rendering, VSP recycling dedup, no UI thread blocking.
  - Files: `hook/WhoReactedEngine.cs`

---

## ILSpy Research

Tasks from [`research/ILSPY_DUMP_INDEX.md`](research/ILSPY_DUMP_INDEX.md). The raw dumps are in `research/ilspy-dumps/`.

### Analyze existing dumps (marked "N" — not yet distilled into docs)

**Style files (line counts updated after re-split):**
- [ ] **Analyze Style_DropDownButton.cs** (121 lines) — DropDownButton: BackgroundTertiary bg, Border border
- [ ] **Analyze Style_DragTabItem.cs** (37 lines) — DragTabItem: draggable tab styling
- [ ] **Analyze Style_FlyoutPresenter.cs** (34 lines) — FlyoutPresenter: popup container
- [ ] **Analyze Style_MenuFlyoutPresenter.cs** (51 lines) — MenuFlyoutPresenter: context menu container. Relevant to Translate plugin (right-click context menu).
- [ ] **Analyze Style_RootColorPicker.cs** (83 lines) — RootColorPicker: color picker control
- [ ] **Analyze Style_BorderlessTextbox.cs** (50 lines) — BorderlessTextbox: no-border text input
- [ ] **Analyze Style_TextButton.cs** (58 lines) — TextButton: text-only button variant
- [ ] **Analyze Style_ToolTip.cs** (49 lines) — ToolTip: tooltip popup styling
- [ ] **Analyze Style_TabsControl.cs** (32 lines) — TabsControl: tab container
- [ ] **Analyze Style_TabsTheme.cs** (33 lines) — TabsTheme: tab visual theme

**Newly split — medium priority (useful context):**
- [ ] **Analyze RootTextbox.cs** (996 lines) — Custom textbox with validation, character limit, helper text
- [ ] **Analyze RootConfirmationControl.cs** (901 lines) — Confirmation dialog (delete/leave/ban flows)
- [ ] **Analyze RootMessageScrollViewer.cs** (645 lines) — Message list scroll container, load-more behavior
- [ ] **Analyze RootScrollViewer.cs** (431 lines) — Custom scroll viewer
- [ ] **Analyze RootMultiCheckBox.cs** (478 lines) — Multi-option checkbox group
- [ ] **Analyze RootMemberVisibilitySwitch.cs** (747 lines) — Member visibility toggle
- [ ] **Analyze MenuItemPageContainerView.cs** (281 lines) — Settings page container view
- [ ] **Analyze RootSvgImage.cs** (144 lines) — SVG image with per-theme path binding
- [ ] **Analyze RootImageLoader.cs** (178 lines) — Image loading control
- [ ] **Analyze RootFlyout.cs** (172 lines) — Flyout popup base
- [ ] **Complete HomeView.cs analysis** (1,280 lines) — Grid/row layout confirmed. Remaining: banner row, SystemTrayBorder, community switcher.
- [ ] **Complete HomeViewModel.cs analysis** (883 lines) — Pane commands confirmed. Remaining: Tabs collection, tab switch logic.

### Complete partial analysis

- [ ] **Complete ChatView.cs analysis** (424 lines) — Settings > Chat page: emoji toggle, tap-to-reply toggle
- [ ] **Complete App.cs analysis** (635 lines) — Application subclass: Initialize(), resource/style registration
- [ ] **Complete MessageViewModel.cs analysis** (786 lines) — Message data model. Critical for MessageLogger.
- [ ] **Complete Style_ComboBox.cs analysis** (521 lines) — ComboBox template, dropdown
- [ ] **Complete Style_MenuItem.cs analysis** (193 lines) — MenuItem: hover states. Relevant to Translate context menu.
- [ ] **Complete Style_Slider.cs analysis** (238 lines) — Slider: BrandPrimary foreground
- [ ] **Complete Style_RootSplitView.cs analysis** (626 lines) — Main app shell layout
- [ ] **Complete Application.cs analysis** (394 lines) — Avalonia Application base
- [ ] **Complete ToggleSwitch.cs analysis** (88 lines) — ToggleSwitch: template parts, knob transitions
- [ ] **Complete SimpleTheme.cs analysis** (1,022 lines) — SimpleTheme base keys

### Decompile new classes (not yet in ilspy-dumps/)

- [x] **Decompile channel list / DM list views** (2026-02-21) — ChannelsView, ChannelView, ChannelGroupView + VMs, TextChannelContentView, VoiceChannelContentView, DirectMessageContentView all added.
- [x] **Decompile BrowserService + browser system** (2026-02-21) — Full RootApp.Browser.* namespace: BrowserService, BrowserPool, DeviceBrowser, RootAppBrowser, WebRtcBrowser, all bridges (AppToNative, NativeToWebRtc, WebRtcToNative, Turnstile).
- [x] **Decompile all embedded assemblies** (2026-02-21) — RootApp.Core (UUID/asset types), RootApp.WebApi.Shared.Entities (~700 DTOs), RootApp.WebApi.Shared (gRPC services), RootApp.Utility, RootApp.AppHub.Client, RootApp.WebApi.Client.Shared, RootApp.Client.Domain.Windows.
- [ ] **Decompile community settings sub-views** — Roles management, apps/bots configuration. Referenced in ViewFactory but not yet extracted.

---

## Ideas / Backlog

Items not yet committed to but worth tracking.

- [ ] Simplify user-facing descriptions on About and Themes settings tabs — current text exposes backend details
  - Files: `hook/ContentPages.cs`
- [ ] Plugin lifecycle error recovery (rollback on `start()` rejection)
  - Files: `src/core/pluginLoader.ts`

---

## Done

Move completed items here with the date.

- [x] **Dev Console card + auto-update UX fixes** (2026-02-23) — Dev Console (developer channel only) with Spoofs/Diagnostics/Engines/ReconLogger inner cards. About page restart button orange, "Check for Updates" disabled when update applied. ReconLogger moved from Plugins page to Dev Console. "Dev Plugins" status row on About page.
  - Files: `hook/ContentPages.cs`, `hook/AutoUpdater.cs`
- [x] **Theme switch lag eliminated + live recolor fix** (2026-02-22) — In-place theme switching (`PrepareForNewTheme` removes only stale keys instead of full `RevertTheme`), bind-once walker (untagged controls get DynamicResource binding on first walk), WeakRef tracking for O(~16) live preview updates, computed palette keys (`BackgroundButtonOnElevated`/`BackgroundButtonOnSecondary`) for gear icon DynamicResource binding, card borders bound to DynamicResource, nav highlight bound to DynamicResource, `SetValueStylePriority` → `SetValueLocalPriority` rename.
  - Files: `hook/ThemeEngine.cs`, `hook/AvaloniaReflection.cs`, `hook/ContentPages.cs`, `hook/SidebarInjector.cs`
- [x] **Custom-theme visual parity pass completed** (2026-02-22) — fixed settings tab text recolor desync (including Root “Online”), corrected sidebar/header text key mapping (TextTertiary for section headers), restored/live-synced preset nested cards with directional elevated surfaces, added selected preset inner highlight wash, and matched Themes `Refresh` button styling/text behavior to About `Open Logs`.
  - Files: `hook/SidebarInjector.cs`, `hook/ThemeEngine.cs`, `hook/ContentPages.cs`, `research/SETTINGS_HEADER_BINDINGS.md`, `research/ROOT_THEME_SYSTEM_FINDINGS.md`, `docs/framework/ROOT_CONTROL_REFERENCE.md`
- [x] **Version copy intercept race condition fixed** (2026-02-21) — Root's async `SetTextAsync` no longer races with our clipboard write.
- [x] **Native theme card settings button** (2026-02-21) — Gear button on "Native" preset card opens Root's native Change Theme page via ViewModel-driven ListBox.SelectedItem binding. SelectRootTab helper for programmatic Root settings navigation.
- [x] **Structured logging overhaul** (2026-02-21) — WideEvent + TailSampler infrastructure, ~1200 Logger.Log calls migrated to ~100 wide events, 4 scan engines tail-sampled, LogConsole dev terminal.
- [x] **TranslateEngine shipped** (2026-02-21) — DeepL-powered message translation with language picker, API key config, context menu integration.
- [x] **UprootedPresenceBeacon shipped** (2026-02-21) — Uprooted user detection via gRPC metadata injection.
- [x] **ReconLogger shipped** (2026-02-21) — Visual tree + style property diagnostic dumper for development.
- [x] **Settings UI overhaul** (2026-02-20) — DPI borders, vector icons, cycling pill, radio indicators, toggle thumb.
- [x] **Rootcord member count tooltips** (2026-02-20)
- [x] **Startup performance optimizations** (2026-02-21) — cache no-Foreground types in WalkAndRecolor, reduce Phase 4.5 plugin startup delays.
- [x] **Rootcord: user card + button wiring overhaul** (2026-02-20) — Removed `_profileInterceptHandler`, avatar+textPanel→ProfilePane, 4-button cluster P/D/N/⚙.
- [x] **Rootcord: RefreshSelectedHighlight crash guard** (2026-02-20) — IsBorder() guard + try/catch per section.
- [x] **Theme Engine v2: resource-first rewrite with OKLCH** (2026-02-19)
- [x] **Silent Typing: DiagnosticListener rewrite** (2026-02-19)
- [x] **ThemeEngine: Color-vs-IBrush crash fix** (2026-02-19)
- [x] **ILSpy batch split + analysis session** (2026-02-19) — 156 individual files from 10 grouped dumps.
- [x] Analyze RootSettingsContainer, MainViewModel, RootMessageItemsControl, RootMenuFlyout, MainWindow, MainView, ILocalDataStore, etc. (2026-02-19)
- [x] Analyze Navigator, IRootSessionAccessor, RootSession, IViewModelBase, DirectMessageOpenerService (2026-02-19)
- [x] Analyze CTextBlock, CSpan, CHyperlink, CInline, CRun, CCode, CImage — markdown rendering system (2026-02-19)
- [x] Analyze MemberProfileView, MemberProfileViewModel, ViewFactory (2026-02-19)
- [x] Decompile IRootSessionAccessor, profile popup views, MembersViewModel, CTextBlock/CSpan/CHyperlink/CRun/CCode (2026-02-19)

---

_Update this file as you work. The `/hi` command reads it every session._
