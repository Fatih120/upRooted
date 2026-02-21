---
name: didyounose
description: Deep-load session context — NEW-SESSION.md + TASKS.md + SESSION_STATE.md + selected ILSpy dumps based on task. Designed for 1M-token context models. Asks what to load before reading.
allowed-tools:
  - Read
  - Grep
  - Glob
  - AskUserQuestion
---

# Super-Hi — Deep Context Load

Load full session context **plus** targeted ILSpy dump files. Designed for 1M-token models where loading large amounts of source is cheap and maximally informative.

---

## Phase 1: Orient (always)

Read all three in parallel:

1. `NEW-SESSION.md` — dispatch table, file map, architecture, startup phases
2. `TASKS.md` — active task board
3. `hook/SESSION_STATE.md` — current hook state, recent changes, known issues

Give a short summary:
- **Version** from NEW-SESSION.md
- **Recent work** — 2–3 bullets from TASKS.md Done section
- **Top pending** — 3–4 bullets from TASKS.md Ready to Build
- **Hook state** — 1–2 sentences from SESSION_STATE.md (current status, anything broken)

---

## Phase 2: Ask What to Load

Use **AskUserQuestion** with two questions in a single call:

**Question 1 — Task** (header: "Task"):
Ask what the user is working on. Options:
- Channel system (channel list, content views, channel management)
- Message system (MessageView, reactions, replies, edit/delete)
- Browser / bridges / WebRTC
- Voice / calls / VoiceBar
- Direct messages
- Community / server (community views, logs, roles)
- Members / profile / popup
- Settings pages / navigation
- Theme system (colors, resource keys, CSS)
- Plugins / TypeScript layer
- gRPC / API protocol (service descriptors, DTOs)
- Installer / deployment
- Other (custom)

**Question 2 — ILSpy domains** (header: "ILSpy Load", multiSelect: true):
Ask which ILSpy dump categories to load. Options:
- Core views for selected task (auto-selected based on task)
- gRPC service descriptors (27 service files)
- API entity DTOs (request/response shapes)
- Browser system + bridges
- Style files (control templates)
- Controls library (RootTextbox, RootScrollViewer, etc.)
- UUID / core types

---

## Phase 3: Load Based on Answers

Read files **directly using Read tool** — no agents, no delegation. Load in parallel where possible.

### Always load (regardless of task)

Read `research/ILSPY_NAVIGATION.md` — needed to know which dump files exist and where they live.

### Task-specific file sets

#### Channel system
Read in parallel:
- `research/ilspy-dumps/ChannelView.cs`
- `research/ilspy-dumps/ChannelViewModel.cs`
- `research/ilspy-dumps/ChannelsView.cs`
- `research/ilspy-dumps/ChannelsViewModel.cs`
- `research/ilspy-dumps/ChannelGroupView.cs`
- `research/ilspy-dumps/ChannelGroupViewModel.cs`
- `research/ilspy-dumps/TextChannelContentView.cs`
- `research/ilspy-dumps/TextChannelContentViewModel.cs`
- `research/ilspy-dumps/VoiceChannelContentView.cs`
- `research/ilspy-dumps/AppChannelContentView.cs`
- `research/ilspy-dumps/ChannelGrpcService.cs`

#### Message system
Read in parallel:
- `research/ilspy-dumps/MessageView.cs` (3602 ln — read in sections)
- `research/ilspy-dumps/MessageViewModel.cs`
- `research/ilspy-dumps/AddReactionView.cs`
- `research/ilspy-dumps/AddReactionViewModel.cs`
- `research/ilspy-dumps/DeleteMessageView.cs`
- `research/ilspy-dumps/MessageRepliesView.cs`
- `research/ilspy-dumps/MessageReplyView.cs`
- `research/ilspy-dumps/MessageReplyViewModel.cs`
- `research/ilspy-dumps/TypingIndicatorView.cs`
- `research/ilspy-dumps/TypingIndicatorViewModel.cs`
- `research/ilspy-dumps/MessageGrpcService.cs`

#### Browser / bridges / WebRTC
Read in parallel:
- `research/ilspy-dumps/BrowserService.cs`
- `research/ilspy-dumps/BrowserPool.cs`
- `research/ilspy-dumps/BrowserRegistry.cs`
- `research/ilspy-dumps/BrowserEngineManager.cs`
- `research/ilspy-dumps/IRootBrowser.cs`
- `research/ilspy-dumps/DeviceBrowser.cs`
- `research/ilspy-dumps/RootAppBrowser.cs`
- `research/ilspy-dumps/WebRtcBrowser.cs`
- `research/ilspy-dumps/AppToNativeBridge.cs`
- `research/ilspy-dumps/AppToNativePrivateBridge.cs`
- `research/ilspy-dumps/NativeToWebRtcBridge.cs`
- `research/ilspy-dumps/WebRtcToNativeBridge.cs`
- `research/ilspy-dumps/WebRtcService.cs`
- `research/ilspy-dumps/NativeToAppPrivateBridge.cs`
- `research/ilspy-dumps/JsPromise.cs`

#### Voice / calls / VoiceBar
Read in parallel:
- `research/ilspy-dumps/VoiceBarView.cs` (2693 ln — read in sections)
- `research/ilspy-dumps/VoiceBarViewModel.cs`
- `research/ilspy-dumps/ScreenshareView.cs`
- `research/ilspy-dumps/ScreensharePickerView.cs`
- `research/ilspy-dumps/CallPopoutWindow.cs`
- `research/ilspy-dumps/CallPopoutViewModel.cs`
- `research/ilspy-dumps/CallingService.cs`
- `research/ilspy-dumps/PushToTalkService.cs`
- `research/ilspy-dumps/WebRtcService.cs`

#### Direct messages
Read in parallel:
- `research/ilspy-dumps/DirectMessageTabView.cs`
- `research/ilspy-dumps/DirectMessageTabViewModel.cs`
- `research/ilspy-dumps/DirectMessageContentView.cs`
- `research/ilspy-dumps/DirectMessageContentViewModel.cs`
- `research/ilspy-dumps/DirectMessageCallContentView.cs`
- `research/ilspy-dumps/DirectMessageCallContentViewModel.cs`
- `research/ilspy-dumps/DirectMessageGrpcService.cs`

#### Community / server
Read in parallel:
- `research/ilspy-dumps/CommunityTabView.cs` (1232 ln)
- `research/ilspy-dumps/CommunityTabViewModel.cs`
- `research/ilspy-dumps/HomeView.cs`
- `research/ilspy-dumps/HomeViewModel.cs`
- `research/ilspy-dumps/CommunityLogsView.cs`
- `research/ilspy-dumps/CommunityLogsViewModel.cs`
- `research/ilspy-dumps/CommunityGrpcService.cs`

#### Members / profile
Read in parallel:
- `research/ilspy-dumps/MemberView.cs`
- `research/ilspy-dumps/MemberViewModel.cs`
- `research/ilspy-dumps/MembersView.cs`
- `research/ilspy-dumps/MembersViewModel.cs`
- `research/ilspy-dumps/MemberProfileView.cs`
- `research/ilspy-dumps/MemberProfileViewModel.cs`
- `research/ilspy-dumps/ProfileView.cs`
- `research/ilspy-dumps/ProfileViewModel.cs`
- `research/ilspy-dumps/CommunityMemberGrpcService.cs`

#### Settings pages / navigation
Read in parallel:
- `research/ilspy-dumps/Navigator.cs`
- `research/ilspy-dumps/RootSettingsContainer.cs`
- `research/ilspy-dumps/MenuItemPageContainerView.cs`
- `research/ilspy-dumps/MenuItemPageContainerViewModel.cs`
- `research/ilspy-dumps/SaveChangesView.cs`
- `research/ilspy-dumps/ProfileSettingsView.cs`
- `research/ilspy-dumps/ProfileSettingsViewModel.cs`
- `docs/framework/ROOT_CONTROL_REFERENCE.md` (settings page section)

#### Theme system
Read in parallel:
- `research/ilspy-dumps/ThemesDarkAxaml.cs`
- `research/ilspy-dumps/ThemesLightAxaml.cs`
- `research/ilspy-dumps/ThemesPureDarkAxaml.cs`
- `research/ilspy-dumps/ThemeService.cs`
- `research/ilspy-dumps/ThemeMapper.cs`
- `research/ilspy-dumps/RootThemeCss.cs`
- `research/ROOT_THEME_SYSTEM_FINDINGS.md`

#### Plugins / TypeScript layer
Read in parallel:
- `docs/framework/TYPESCRIPT_REFERENCE.md`
- `docs/plugins/API_REFERENCE.md`
- `docs/plugins/BRIDGE_REFERENCE.md`
- `src/core/preload.ts`
- `src/core/pluginLoader.ts`
- `src/api/bridge.ts`

#### gRPC service descriptors (if selected)
Read in parallel (all 27 service files from `research/ilspy-dumps/`):
- `MessageGrpcService.cs`, `ChannelGrpcService.cs`, `ChannelGroupGrpcService.cs`
- `CommunityGrpcService.cs`, `CommunityMemberGrpcService.cs`, `CommunityRoleGrpcService.cs`
- `CommunityLogGrpcService.cs`, `DirectMessageGrpcService.cs`
- `UserGrpcService.cs`, `WebRtcGrpcService.cs`
- `FriendshipGrpcService.cs`, `FriendshipGroupGrpcService.cs`
- `NotificationGrpcService.cs`, `FileGrpcService.cs`, `AssetGrpcService.cs`
- `LinkGrpcService.cs`, `AccessRuleGrpcService.cs`, `SupportGrpcService.cs`
- `DirectoryGrpcService.cs`, `AppStoreGrpcService.cs`

#### API entity DTOs (if selected)
For the task domain, read the relevant request/response files:
- Channel: `ChannelCreateRequest`, `ChannelResponse`, `ChannelListResponse`, `ChannelGroupResponse`
- Message: `MessageCreateRequest`, `MessageGetResponse`, `MessagePayload`, `MessagePacket`
- Community: `CommunityResponse`, `CommunityMemberResponse`, `CommunityRoleResponse`
- DM: `DirectMessageCreateRequest`, `DirectMessageGetResponse`
- User: `UserGetRequest`, `UserResponse`
- File: `FileCreateRequest`, `FileContainerResponse`
(Read the ones relevant to the task — not all 700)

#### Browser system + bridges (if selected)
Already covered in "Browser / bridges / WebRTC" set above. Also add:
- `research/ilspy-dumps/ContextMenuHandler.cs`
- `research/ilspy-dumps/NetworkRequestGuardHandler.cs`
- `research/ilspy-dumps/NavigationGuardHandler.cs`
- `research/ilspy-dumps/TurnstileToNativeBridge.cs`

#### Style files (if selected)
Read the style files relevant to the task:
- For controls: `Style_CheckBox.cs`, `Style_ScrollViewer.cs`, `Style_BorderButton.cs`, `Style_TransparentButton.cs`
- For messages: `Style_MessageMarkdown.cs`
- For menus: `Style_MenuItem.cs`, `Style_MenuFlyoutPresenter.cs`
- For tabs: `Style_TabItem.cs`, `Style_TabsControl.cs`

#### Controls library (if selected)
Read in parallel:
- `research/ilspy-dumps/RootScrollViewer.cs`
- `research/ilspy-dumps/RootTextbox.cs`
- `research/ilspy-dumps/RootConfirmationControl.cs`
- `research/ilspy-dumps/RootMenuFlyout.cs`
- `research/ilspy-dumps/RootBorder.cs`
- `research/ilspy-dumps/RootMessageScrollViewer.cs`

#### UUID / core types (if selected)
Read:
- `research/ilspy-dumps/RootUuid.cs`
- `research/ilspy-dumps/RootGuid.cs`
- `research/ilspy-dumps/RootWebApiConfig.cs`
- `research/ilspy-dumps/SemanticVersion.cs`
- `research/ilspy-dumps/WellKnownRootGuids.cs`
- The specific UUID file for the task domain (e.g., `ChannelUuid.cs`, `MessageUuid.cs`)

---

## Phase 4: Report What Was Loaded

After loading, give a concise report:

1. **Orientation** — version, recent work, top tasks, hook state (from Phase 1)
2. **Loaded ILSpy context** — list files read with line counts
3. **Key findings** — 3–5 bullet points of immediately useful facts from the dumps:
   - Property names available for INPC intercept
   - Class hierarchy / base types
   - Method signatures for reflection
   - Data shapes from DTOs
   - Bridge contracts
4. **Ready** — confirm ready to begin work

Keep the report tight. Don't summarize entire files — only the parts relevant to the user's stated task.

---

## Size Guidance for Large Files

Files over 1000 lines: use `Read` with `offset`/`limit` or `Grep` first to locate the relevant section, then read that section.

- `MessageView.cs` (3602 ln): read first 200 lines for named controls, then grep for methods
- `VoiceBarView.cs` (2693 ln): read first 150 lines for structure, grep for state/properties
- `TextChannelContentView.cs` (1624 ln): read first 200 lines for layout, grep for event handlers
- `CommunityTabView.cs` (1232 ln): read first 200 lines
- `-AvaloniaResources-GREPONLY.cs` (55k ln): grep only, never read

This is a 1M context model — load everything that's relevant. When in doubt, load it.
