# Tasks

> Active task board for Uprooted contributors. Pick up items, mark them done, add new ones.
> For strategic direction and long-term goals, see [docs/ROADMAP.md](docs/ROADMAP.md).
> For planned plugins and implementation strategies, see [docs/PLUGIN_ROADMAP.md](docs/PLUGIN_ROADMAP.md).

---

## Up Next

Short-term tasks ready to be picked up. Roughly priority-ordered.

- [ ] **MessageLogger: real-world validation (v2)** — Major reliability overhaul deployed: property fix (DeletedAt/EditedAt DateTimeOffset?), author names (SenderMember chain), INPC event-driven detection, self-delete fallback (collection-presence check after 3s timeout), dedup. Validate: (1) delete own message — confirm self-delete fallback catches it; (2) have another user delete a message — confirm DeletedAt INPC event fires; (3) edit a message after 5s — confirm amber edit card appears with correct author name; (4) check card positioning (FindMessageGridInContainer was returning null — may need container structure investigation).
  - Files: `hook/MessageLogger.cs`

- [ ] **SilentTyping: verify with second account** — C# engine now successfully blocks SetTypingIndicator (confirmed in log). Need second-account test: type in one session, confirm the other sees no typing indicator.
  - Files: `hook/SilentTypingEngine.cs`

- [ ] **NsfwFilter: validate Avalonia-native redesign** — Phase 4.5g deployed. Verify images are being classified and blurred. Check hook log for scan timer entries and Vision API calls. Confirm overlay + click-to-reveal works.
  - Files: `hook/NsfwFilter.cs`

- [ ] **Theme Engine v2: validate in production** — ThemeEngine v2 (resource-first, OKLCH) rewrite is deployed but not yet tested in production. Verify: (1) all 3 presets (crimson, cosmic-smoothie, loki) display correctly; (2) custom theme live preview is smooth; (3) theme revert restores all defaults; (4) variant switching (Dark/PureDark/Light) preserves overrides; (5) ping color overrides SelfMention keys correctly.
  - Files: `hook/ThemeEngine.cs`

## Testing

- [ ] **Set up Vitest** — Install Vitest, add `test`/`test:watch`/`test:coverage` scripts, co-locate test files as `.test.ts`.

- [ ] **PluginLoader unit tests** — Registration, start/stop lifecycle, patch install, event emission, race conditions.
  - Files: `src/core/pluginLoader.ts`

- [ ] **Settings unit tests** — JSON parsing, merge with defaults, deep merge, corrupted file recovery.
  - Files: `src/core/settings.ts`


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
- [ ] **Analyze HomeViewModel.cs** (883 lines) — Main view after login: tab management, navigation, DM calls

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

- [ ] **Decompile channel list / DM list views** — Channel sidebar. Low priority until channel-related plugins are planned.
- [ ] **Decompile navigation service** — How Root switches between pages. Would enable programmatic navigation.

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

- [x] **Theme Engine v2: resource-first rewrite with OKLCH** (2026-02-19) — Full rewrite of ThemeEngine.cs (2638 → ~900 lines). Targets Root's actual 32 DynamicResource keys. OKLCH palette generation. Eliminates 11 walk methods + 500ms timer.
- [x] **Silent Typing: restored JS interception** (2026-02-19) — Replaced no-op stub with Kurumi Nanase's working fetch/XHR intercept. Both C# and JS layers now active.
- [x] **SilentTyping: fix C# engine to actually block gRPC calls** (2026-02-19) — Root uses `Grpc.Net.Client.GrpcChannel` (not raw HttpClient) for gRPC. Broadened discovery: removed keyword gate from instance walk, removed type-name filter from static scan, increased depth 8→12, added HttpMessageHandler detection, added GrpcChannel-aware patching (extracts internal `HttpInvoker` and patches its `_handler`). Now blocks SetTypingIndicator (confirmed in log). Also: stripped em dashes from plugin descriptions, case-insensitive URL matching.
- [x] **SilentTyping: DiagnosticListener rewrite** (2026-02-19) — Replaced 482-line HttpClient/GrpcChannel handler injection with ~90-line DiagnosticListener approach (by Kurumi Nanase). Subscribes to .NET HTTP diagnostics, redirects SetTypingIndicator to localhost:0.
- [x] **ThemeEngine: Color-vs-IBrush crash fix** (2026-02-19) — Fixed storing Color objects where Root expects IBrush in ThemeDictionaries. Added PreservedSemanticKeys to keep BrandSecondary (online indicators) at Root's original green.
- [x] **ILSpy batch split + analysis session** (2026-02-19) — Split 10 grouped namespace dumps into 156 individual files (66 → 219 total, 145k lines). Analyzed 29 files into ROOT_CONTROL_REFERENCE.md. Fixed Style_ file split boundaries (27 files). Tagged 3 oversized files with -GREPONLY suffix. Audited and corrected all 3 Root findings docs.
- [x] Analyze RootSettingsContainer, MainViewModel, RootMessageItemsControl, RootMenuFlyout, MainWindow, MainView, ILocalDataStore + LocalDataStore + LocalDataStoreExtensions, SaveChangesView, IPage, MenuItemPageContainerViewModel, SecureStorageImplementation (2026-02-19)
- [x] Analyze Navigator, IRootSessionAccessor, RootSessionAccessor, RootSession, IViewModelBase, DirectMessageOpenerService (2026-02-19)
- [x] Analyze CTextBlock, CSpan, CHyperlink, CInline, CRun, CCode, CImage — markdown rendering system (2026-02-19)
- [x] Analyze MemberProfileView, MemberProfileViewModel, ViewFactory (236 VM→View mappings) (2026-02-19)
- [x] Decompile IRootSessionAccessor, profile popup views, MembersViewModel/CommunityLogViewModel/CommunityTabViewModel, CTextBlock/CSpan/CHyperlink/CRun/CCode (2026-02-19)

---

_Update this file as you work. The `/hi` command reads it every session._
