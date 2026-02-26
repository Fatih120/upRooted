---
name: didyounose
description: Deep-load session context — NEW-SESSION.md + TASKS.md + SESSION_STATE.md + targeted ILSpy dumps for the exact task. Designed for 1M-token context. Reads only what's needed, but everything that's needed.
allowed-tools:
  - Read
  - Grep
  - AskUserQuestion
---

# Didyounose — Precise Context Load

Load orientation context, then pull exactly the right ILSpy source files for the task at hand. No excess, no gaps.

---

## Phase 1: Orient (always — 3 files in parallel)

Read simultaneously:
1. `NEW-SESSION.md`
2. `TASKS.md`
3. `hook/SESSION_STATE.md`

Give a tight summary:
- **v{version}** — from NEW-SESSION.md
- **Recent** — 2 bullets from TASKS.md Done (most recent)
- **Pending** — top 3 from TASKS.md Ready to Build
- **Hook state** — 1 sentence from SESSION_STATE.md

---

## Phase 2: Ask What to Load

Use **AskUserQuestion** — single question, single call:

- **Question** (header: "Task"): "What are you working on?"
- **Options** (pick the one that fits; use Other to describe):
  1. Channel UI — channel list, sidebar, content views (text/voice/app channel)
  2. Message UI — MessageView, reactions, replies, typing, edit/delete
  3. Browser / bridges / WebRTC / network intercept
  4. Voice / calls / DMs / VoiceBar
- **multiSelect: false**

The "Other" option (auto-added) lets the user describe anything not in the 4 options. Read their description and map it to the appropriate file set below.

---

## Phase 3: Load — Exact File Sets per Sub-Task

Read all files in the set **in parallel**. Never load a set's files twice.

**Base path for all ILSpy dumps:** `research/ilspy-dumps/` (omitted below for brevity).
For large files (>1500 ln), see **Read Size Rules** at bottom.

Map the user's answer to the most specific matching sub-task below.

### CHANNEL UI

**Channel list / sidebar** — *"channel list", "ChannelsView", "Rootcord sidebar"*
`ChannelsView.cs`, `ChannelsViewModel.cs`, `ChannelView.cs`, `ChannelViewModel.cs`, `ChannelGroupView.cs`, `ChannelGroupViewModel.cs`

**Text channel content** — *"text channel", "chat panel", "TextChannelContent", "compose box"*
`TextChannelContentView.cs`, `TextChannelContentViewModel.cs`, `ChannelView.cs`

**Voice channel content** — *"voice channel", "VoiceChannelContent"*
`VoiceChannelContentView.cs`, `VoiceChannelContentViewModel.cs`, `CallingService.cs`

**App channel** — *"app channel", "AppChannelContent", "embedded app"*
`AppChannelContentView.cs`, `AppChannelContentViewModel.cs`, `AppToNativeBridge.cs`, `AppHubConnection.cs`

**Channel management** — *"create channel", "edit channel", "delete channel"*
`CreateChannelView.cs`, `CreateChannelViewModel.cs`, `EditChannelView.cs`, `EditChannelViewModel.cs`, `DeleteChannelView.cs`, `DeleteChannelViewModel.cs`, `ChannelGrpcService.cs`, `ChannelCreateRequest.cs`, `ChannelEditRequest.cs`

**Channel group management** — *"channel group", "create channel group"*
`CreateChannelGroupView.cs`, `CreateChannelGroupViewModel.cs`, `EditChannelGroupView.cs`, `EditChannelGroupViewModel.cs`, `DeleteChannelGroupView.cs`, `ChannelGroupGrpcService.cs`

**Channel permissions** — *"channel permissions", "access rules", "AccessRule"*
`AccessRulesView.cs`, `AccessRuleView.cs`, `AccessRuleSelectorView.cs`, `AccessRuleGrpcService.cs`, `AccessRuleCreateRequest.cs`, `ChannelPermission.cs`, `ChannelPermissionsExtensions.cs`

### MESSAGE UI

**MessageView / rendering** — *"MessageView", "message layout", "message visual tree"*
`MessageView.cs` (lines 1-300), `MessageViewModel.cs`, `Style_MessageMarkdown.cs` (lines 1-200)

**Message edit / delete detection** — *"edit detection", "delete detection", "MessageLogger", "INPC on message"*
`MessageViewModel.cs`, `MessageView.cs` (grep `IsEdited|IsDeleted|WasEdited`), `MessageEditRequest.cs`, `MessageDeleteRequest.cs`, `MessageDeletedPacket.cs`

**Message reactions** — *"reactions", "emoji reaction", "AddReaction"*
`AddReactionView.cs`, `AddReactionViewModel.cs`, `MessageView.cs` (grep `reaction|Reaction`)

**Message replies / threading** — *"replies", "thread", "MessageReplies"*
`MessageRepliesView.cs`, `MessageRepliesViewModel.cs`, `MessageReplyView.cs`, `MessageReplyViewModel.cs`

**Typing indicator** — *"typing indicator", "TypingIndicator", "SilentTyping"*
`TypingIndicatorView.cs`, `TypingIndicatorViewModel.cs`, `TextChannelContentViewModel.cs` (grep `typing|Typing`)

**File/media messages** — *"GIF message", "image message", "video message", "file attachment"*
`GifSizer.cs`, `AssetImage.cs`, `AssetVideo.cs`, `MessageView.cs` (grep `Gif|Image|Video|File`)
Note: media views are inlined in MessageView.cs, not separate files.

**Link embeds / link previews** — *"link embed", "link preview", "LinkEmbedEngine", "OG metadata"*
`MessageView.cs` (grep `embed|Embed|link|Link`), `MessageViewModel.cs` (grep `Link|Embed`)
Note: link previews are part of MessageView, not separate files.

**Delete message dialog** — *"delete message dialog", "confirm delete"*
`DeleteMessageView.cs`, `DeleteMessageViewModel.cs`

### BROWSER / BRIDGES / WEBRTC / NETWORK

**Browser management** — *"browser", "DotNetBrowser", "BrowserService", "browser pool", "IBrowser"*
`BrowserService.cs`, `BrowserPool.cs`, `BrowserRegistry.cs`, `BrowserEngineManager.cs`, `BrowserProfileManager.cs`, `IRootBrowser.cs`, `DeviceBrowser.cs`, `RootAppBrowser.cs`, `WarmBrowser.cs`

**JS-Native bridges** — *"bridge", "AppToNative", "NativeToApp", "bridge proxy", "__nativeToWebRtc"*
`AppToNativeBridge.cs`, `AppToNativePrivateBridge.cs`, `NativeToAppPrivateBridge.cs`, `JsPromise.cs`, `JsObjectExtensions.cs`

**WebRTC** — *"WebRTC", "WebRtcService", "media stream", "screen capture audio"*
`WebRtcService.cs`, `WebRtcBrowser.cs`, `NativeToWebRtcBridge.cs`, `WebRtcToNativeBridge.cs`, `InitializeWebRtcPayload.cs`, `WebRtcCodec.cs`, `UserMediaStreamConstraints.cs`, `AudioTrackConstraints.cs`

**HTTP request intercept** — *"HTTP intercept", "request handler", "network intercept"*
`NetworkRequestGuardHandler.cs`, `NetworkTransactionHandler.cs`, `ContextMenuHandler.cs`, `NavigationGuardHandler.cs`, `CompositeInterceptHandler.cs`, `IUrlSchemeHandler.cs`, `BrowserService.cs` (grep `handler|Handler`)

**Turnstile (CAPTCHA)** — *"Turnstile", "CAPTCHA"*
`TurnstileBrowser.cs`, `TurnstileToNativeBridge.cs`, `AvaloniaTurnstileTokenProvider.cs`, `TurnstileException.cs`

### VOICE / CALLS / DMs / VOICEBAR

**VoiceBar** — *"voice bar", "VoiceBar", "mute/deafen"*
`VoiceBarView.cs` (lines 1-200), `VoiceBarViewModel.cs`

**Screenshare** — *"screenshare", "screen share", "screenshare picker"*
`ScreenshareView.cs`, `ScreenshareViewModel.cs`, `ScreensharePickerView.cs`, `ScreensharePickerViewModel.cs`, `ScreenshareAudioFailedView.cs`, `WebRtcService.cs` (grep `screenshare|screen`)

**Call popup / CallingService** — *"call popup", "CallPopout", "PushToTalk"*
`CallPopoutWindow.cs`, `CallPopoutViewModel.cs`, `CallingService.cs`, `PushToTalkService.cs`

**Direct messages** — *"DM", "direct message", "DM chat", "DM list"*
`DirectMessageTabView.cs`, `DirectMessageTabViewModel.cs`, `DirectMessageContentView.cs`, `DirectMessageContentViewModel.cs`

**DM calls** — *"DM call", "DirectMessageCall"*
`DirectMessageCallContentView.cs`, `DirectMessageCallContentViewModel.cs`, `CallingService.cs`, `VoiceBarView.cs` (lines 1-100)

### SETTINGS / NAVIGATION

**Settings page injection** — *"settings injection", "sidebar injection", "Navigator", "add nav item"*
`Navigator.cs`, `RootSettingsContainer.cs`, `MenuItemPageContainerView.cs`, `MenuItemPageContainerViewModel.cs`, `IPage.cs`, `SaveChangesView.cs`, `ProfileSettingsView.cs` (lines 1-150)

**Specific settings page** — *"chat settings", "privacy settings", "audio settings", etc.*
Load `{PageName}View.cs` + `{PageName}ViewModel.cs` + `Navigator.cs` + `RootSettingsContainer.cs`.
PageNames: `ChatView`, `PrivacySettingsView`, `AudioVideoView`, `NotificationSettingsView`, `KeybindingsView`, `AdvancedSettingsView`, `WindowsSettingsView`, `EditProfileView`, `ChangePasswordView`, `StreamerModeSettingsView`, `GameOverlaySettingsView`.

### THEME / STYLES / CONTROLS / MARKDOWN

**Theme colors / resource keys** — *"theme", "colors", "resource keys", "ThemeEngine", "brushes"*
`ThemesDarkAxaml.cs`, `ThemesLightAxaml.cs`, `ThemesPureDarkAxaml.cs`, `ThemeService.cs`, `ThemeMapper.cs`, `research/ROOT_THEME_SYSTEM_FINDINGS.md`

**Theme CSS (browser layer)** — *"CSS variables", "browser theme", "RootThemeCss"*
`RootThemeCss.cs`, `ThemesDarkAxaml.cs` (grep `Css|css|var(`)

**Control template / style** — *"style", "control template", "visual state"*
Load the matching `Style_*.cs` file:
CheckBox→`Style_CheckBox.cs`, BorderButton→`Style_BorderButton.cs`, TransparentButton→`Style_TransparentButton.cs`, SvgButton→`Style_SvgButton.cs`, ScrollViewer→`Style_ScrollViewer.cs`, ComboBox→`Style_ComboBox.cs`+`Style_ComboBoxItem.cs`, MenuItem→`Style_MenuItem.cs`, ListBox→`Style_ListBox.cs`+`Style_ListBoxItem.cs`, TabItem→`Style_TabItem.cs`, Slider→`Style_Slider.cs`, DropDownButton→`Style_DropDownButton.cs`, ToolTip→`Style_ToolTip.cs`, FlyoutPresenter→`Style_FlyoutPresenter.cs`, MessageMarkdown→`Style_MessageMarkdown.cs`
Also load matching control impl if it exists (e.g., `RootScrollViewer.cs` alongside `Style_ScrollViewer.cs`).
For cross-style grep: `Grep path:"research/ilspy-dumps/StylesAll-GREPONLY.cs.txt"` (never Read).

**Custom control** — *"RootTextbox", "RootScrollViewer", "RootConfirmationControl", etc.*
Load `{ControlName}.cs` + `Style_{ControlName}.cs` if it exists.

**Markdown / rich text** — *"markdown", "CTextBlock", "rich text", "CHyperlink"*
`CTextBlock.cs`, `CSpan.cs`, `CInline.cs`, `CHyperlink.cs`, `CRun.cs`, `Style_MessageMarkdown.cs` (lines 1-100)

### COMMUNITY / MEMBERS / PROFILE

**Community / server views** — *"community tab", "CommunityTab", "Rootcord main view"*
`CommunityTabView.cs` (lines 1-200), `CommunityTabViewModel.cs`, `HomeView.cs` (lines 1-200), `HomeViewModel.cs` (lines 1-150)

**Community audit log** — *"community log", "audit log", "CommunityLog", "AuditLogEngine"*
`CommunityLogsView.cs`, `CommunityLogsViewModel.cs`, `CommunityLogView.cs`, `CommunityLogViewModel.cs`, `CommunityLogGrpcService.cs`

**Members panel** — *"members panel", "member list", "MembersView"*
`MembersView.cs`, `MembersViewModel.cs`, `MemberView.cs`, `MemberViewModel.cs`, `MemberGroupView.cs`, `MemberGroupViewModel.cs`

**Member / user profile popup** — *"profile popup", "MemberProfile", "user card", "badge display"*
`MemberProfileView.cs`, `MemberProfileViewModel.cs`, `ProfileView.cs`, `ProfileViewModel.cs`

**Roles** — *"roles", "community roles", "CommunityRole"*
`CommunityRoleGrpcService.cs`, `CommunityRoleResponse.cs`, `CommunityRoleCreateRequest.cs`, `CommunityMemberRoleGrpcService.cs`, `CommunityPermission.cs`, `CommunityPermissionsExtensions.cs`

### PLUGINS / TYPESCRIPT

**TypeScript plugin / bridge proxy** — *"TypeScript", "plugin", "preload", "bridge proxy", "JS injection"*
`docs/framework/TYPESCRIPT_REFERENCE.md`, `docs/plugins/API_REFERENCE.md`, `docs/plugins/BRIDGE_REFERENCE.md`, `src/core/preload.ts`, `src/core/pluginLoader.ts`, `src/api/bridge.ts`

### gRPC / API DATA MODEL

**gRPC for a specific domain** — *"gRPC", "protobuf", "packet structure" + specific domain*
Load only the service + key entity files for the domain:
Channels→`ChannelGrpcService.cs`+`ChannelResponse.cs`+`ChannelCreateRequest.cs` | Messages→`MessageGrpcService.cs`+`MessagePayload.cs`+`MessageCreateRequest.cs` | Community→`CommunityGrpcService.cs`+`CommunityResponse.cs` | Members→`CommunityMemberGrpcService.cs`+`CommunityMemberResponse.cs` | DMs→`DirectMessageGrpcService.cs` | User→`UserGrpcService.cs`+`UserResponse.cs` | WebRTC→`WebRtcGrpcService.cs` | Files→`FileGrpcService.cs`+`FileContainerResponse.cs` | Notifications→`NotificationGrpcService.cs`+`NotificationResponse.cs` | Friendships→`FriendshipGrpcService.cs`
Also load `docs/research/GRPC_PROTOCOL.md` if working at the wire level. Do not load all 27 service files unless explicitly needed.

### LOGIN / AUTH / SESSION

**Login / registration** — *"login", "register", "auth", "LoginView"*
`LoginView.cs`, `LoginViewModel.cs`, `RegisterView.cs`, `RegisterViewModel.cs`, `RootSession.cs` (grep `Login|Token|Auth`), `IRootSessionAccessor.cs`

**Session / data persistence** — *"RootSession", "LocalDataStore", "DataStoreKeys"*
`RootSession.cs`, `RootSessionAccessor.cs`, `LocalDataStore.cs`, `LocalDataStoreExtensions.cs`, `DataStoreKeys.cs`

### OVERLAY / MEDIA VIEWER / OTHER UI

**Game overlay** — *"overlay", "game overlay", "OverlayWindow"*
`OverlayWindow.cs`, `OverlayViewModel.cs`, `OverlayActionBar.cs`, `OverlayActionBarViewModel.cs`, `OverlayVoiceUser.cs`, `GameOverlaySettingsView.cs`, `GameOverlaySettingsViewModel.cs`

**Media viewer** — *"media viewer", "image viewer", "video player"*
`MediaViewerView.cs`, `MediaViewerViewModel.cs`, `MediaViewerImageView.cs`, `MediaViewerVideoView.cs`, `MediaViewerGifView.cs`

**New tab / community discovery** — *"new tab", "NewTab", "verified communities"*
`NewTabContentView.cs`, `NewTabContentViewModel.cs`, `NewTabCommunityView.cs`, `DiscoverVerifiedCommunitiesView.cs`, `VerifiedCommunitiesView.cs`

## Phase 4: Report

After loading:
1. **Context loaded** — one line per file, with line count
2. **Key facts** — 3-5 bullets relevant to task (property names, method signatures, visual tree structure, data shapes, bridge contracts)
3. **Ready** — confirm ready to begin

Only extract facts that bear on the stated task. Do not summarize files in full.

## Read Size Rules

| Size | Rule |
|------|------|
| ≤500 ln | Read fully |
| 500-1500 ln | Read fully unless only one section relevant; grep first |
| >1500 ln | Read with offset/limit per table below, or grep then targeted read |
| GREPONLY suffix | Grep only, never Read |

**Large file first-pass reads:**

| File | Lines | Read |
|------|------:|------|
| `MessageView.cs` | 3602 | 1-300 |
| `VoiceBarView.cs` | 2693 | 1-200 |
| `TextChannelContentView.cs` | 1624 | 1-200 |
| `CommunityTabView.cs` | 1232 | 1-200 |
| `HomeView.cs` | 1280 | 1-200 |
| `HomeViewModel.cs` | 883 | 1-150 |
| `RootSettingsContainer.cs` | 990 | 1-150 |
| `-AvaloniaResources-GREPONLY.cs` | 55k | Grep only |
| `XamlIlHelpers-GREPONLY.cs` | 33k | Grep only |
| `StylesAll-GREPONLY.cs.txt` | 5k | Grep only |
