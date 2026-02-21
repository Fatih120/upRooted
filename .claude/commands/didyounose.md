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

Read all files in the set **in parallel**. Never load a set's files twice. Never load a file that isn't in the matching set.

Map the user's answer to the most specific matching sub-task below.

---

### CHANNEL UI

#### Channel list / sidebar
*Matches: "channel list", "ChannelsView", "channel sidebar", "channel order/reorder", "Rootcord sidebar"*
```
research/ilspy-dumps/ChannelsView.cs
research/ilspy-dumps/ChannelsViewModel.cs
research/ilspy-dumps/ChannelView.cs
research/ilspy-dumps/ChannelViewModel.cs
research/ilspy-dumps/ChannelGroupView.cs
research/ilspy-dumps/ChannelGroupViewModel.cs
```

#### Text channel content
*Matches: "text channel", "chat panel", "TextChannelContent", "send message in channel", "compose box"*
```
research/ilspy-dumps/TextChannelContentView.cs
research/ilspy-dumps/TextChannelContentViewModel.cs
research/ilspy-dumps/ChannelView.cs             ← context only
```

#### Voice channel content
*Matches: "voice channel", "VoiceChannelContent", "voice panel"*
```
research/ilspy-dumps/VoiceChannelContentView.cs
research/ilspy-dumps/VoiceChannelContentViewModel.cs
research/ilspy-dumps/CallingService.cs
```

#### App channel (embedded app)
*Matches: "app channel", "AppChannelContent", "embedded app", "RootApp channel"*
```
research/ilspy-dumps/AppChannelContentView.cs
research/ilspy-dumps/AppChannelContentViewModel.cs
research/ilspy-dumps/AppToNativeBridge.cs
research/ilspy-dumps/AppHubConnection.cs
```

#### Channel management (create/edit/delete)
*Matches: "create channel", "edit channel", "delete channel", "channel settings dialog"*
```
research/ilspy-dumps/CreateChannelView.cs
research/ilspy-dumps/CreateChannelViewModel.cs
research/ilspy-dumps/EditChannelView.cs
research/ilspy-dumps/EditChannelViewModel.cs
research/ilspy-dumps/DeleteChannelView.cs
research/ilspy-dumps/DeleteChannelViewModel.cs
research/ilspy-dumps/ChannelGrpcService.cs
research/ilspy-dumps/ChannelCreateRequest.cs
research/ilspy-dumps/ChannelEditRequest.cs
```

#### Channel group management
*Matches: "channel group", "create channel group", "edit channel group"*
```
research/ilspy-dumps/CreateChannelGroupView.cs
research/ilspy-dumps/CreateChannelGroupViewModel.cs
research/ilspy-dumps/EditChannelGroupView.cs
research/ilspy-dumps/EditChannelGroupViewModel.cs
research/ilspy-dumps/DeleteChannelGroupView.cs
research/ilspy-dumps/ChannelGroupGrpcService.cs
```

#### Channel permissions / access rules
*Matches: "channel permissions", "access rules", "AccessRule"*
```
research/ilspy-dumps/AccessRulesView.cs
research/ilspy-dumps/AccessRuleView.cs
research/ilspy-dumps/AccessRuleSelectorView.cs
research/ilspy-dumps/AccessRuleGrpcService.cs
research/ilspy-dumps/AccessRuleCreateRequest.cs
research/ilspy-dumps/ChannelPermission.cs
research/ilspy-dumps/ChannelPermissionsExtensions.cs
```

---

### MESSAGE UI

#### MessageView / message rendering / visual layout
*Matches: "MessageView", "message layout", "message visual", "message rendering", "message visual tree"*
```
research/ilspy-dumps/MessageView.cs             ← read lines 1–300 (named controls + constructor)
research/ilspy-dumps/MessageViewModel.cs
research/ilspy-dumps/Style_MessageMarkdown.cs   ← read lines 1–200 (CTextBlock, mention pills)
```
For `MessageView.cs` (3602 ln): use `Read` with `limit: 300`. Then `Grep` for specific method names if needed.

#### Message edit / delete detection
*Matches: "edit detection", "delete detection", "MessageLogger", "message tracking", "INPC on message"*
```
research/ilspy-dumps/MessageViewModel.cs
research/ilspy-dumps/MessageView.cs             ← grep "IsEdited\|IsDeleted\|edit\|delete" first
research/ilspy-dumps/MessageEditRequest.cs
research/ilspy-dumps/MessageDeleteRequest.cs
research/ilspy-dumps/MessageDeletedPacket.cs
```
For `MessageView.cs`: `Grep pattern:"IsEdited|IsDeleted|WasEdited|deleted" path:"research/ilspy-dumps/MessageView.cs"` then read the matched sections.

#### Message reactions
*Matches: "reactions", "emoji reaction", "AddReaction", "reaction picker"*
```
research/ilspy-dumps/AddReactionView.cs
research/ilspy-dumps/AddReactionViewModel.cs
research/ilspy-dumps/MessageView.cs             ← grep "reaction\|Reaction" first
```

#### Message replies / threading
*Matches: "replies", "thread", "MessageReplies", "reply view"*
```
research/ilspy-dumps/MessageRepliesView.cs
research/ilspy-dumps/MessageRepliesViewModel.cs
research/ilspy-dumps/MessageReplyView.cs
research/ilspy-dumps/MessageReplyViewModel.cs
```

#### Typing indicator
*Matches: "typing indicator", "is typing", "TypingIndicator", "SilentTyping intercept"*
```
research/ilspy-dumps/TypingIndicatorView.cs
research/ilspy-dumps/TypingIndicatorViewModel.cs
research/ilspy-dumps/TextChannelContentViewModel.cs  ← grep "typing\|Typing" for dispatch
```

#### File/media messages (GIF, image, video, file)
*Matches: "GIF message", "image message", "video message", "file attachment", "FileMessage", "media message"*
```
research/ilspy-dumps/GifMessageView.cs
research/ilspy-dumps/ImageMessageView.cs
research/ilspy-dumps/VideoMessageView.cs
research/ilspy-dumps/FileMessageView.cs
research/ilspy-dumps/FileMessageViewModel.cs
```

#### Inline message embeds / link previews
*Matches: "link embed", "link preview", "LinkEmbedEngine", "OG metadata", "LinkMessage"*
```
research/ilspy-dumps/LinkMessageView.cs
research/ilspy-dumps/LinkMessageViewModel.cs
research/ilspy-dumps/MessageView.cs             ← grep "embed\|Embed\|link" first
```

#### Delete message dialog
*Matches: "delete message dialog", "confirm delete", "DeleteMessageView"*
```
research/ilspy-dumps/DeleteMessageView.cs
research/ilspy-dumps/DeleteMessageViewModel.cs
```

---

### BROWSER / BRIDGES / WEBRTC / NETWORK

#### Browser management (DotNetBrowser)
*Matches: "browser", "DotNetBrowser", "BrowserService", "browser pool", "browser lifecycle", "IBrowser"*
```
research/ilspy-dumps/BrowserService.cs
research/ilspy-dumps/BrowserPool.cs
research/ilspy-dumps/BrowserRegistry.cs
research/ilspy-dumps/BrowserEngineManager.cs
research/ilspy-dumps/BrowserProfileManager.cs
research/ilspy-dumps/IRootBrowser.cs
research/ilspy-dumps/DeviceBrowser.cs
research/ilspy-dumps/RootAppBrowser.cs
research/ilspy-dumps/WarmBrowser.cs
```

#### JS ↔ Native bridges
*Matches: "bridge", "AppToNative", "NativeToApp", "bridge interception", "bridge proxy", "__nativeToWebRtc", "bridge method"*
```
research/ilspy-dumps/AppToNativeBridge.cs
research/ilspy-dumps/AppToNativePrivateBridge.cs
research/ilspy-dumps/NativeToAppPrivateBridge.cs
research/ilspy-dumps/JsPromise.cs
research/ilspy-dumps/JsObjectExtensions.cs
```

#### WebRTC
*Matches: "WebRTC", "WebRtcService", "media stream", "screen capture audio", "WebRTC signaling"*
```
research/ilspy-dumps/WebRtcService.cs
research/ilspy-dumps/WebRtcBrowser.cs
research/ilspy-dumps/NativeToWebRtcBridge.cs
research/ilspy-dumps/WebRtcToNativeBridge.cs
research/ilspy-dumps/InitializeWebRtcPayload.cs
research/ilspy-dumps/WebRtcCodec.cs
research/ilspy-dumps/UserMediaStreamConstraints.cs
research/ilspy-dumps/AudioTrackConstraints.cs
```

#### HTTP request intercept / network handlers
*Matches: "HTTP intercept", "request handler", "network intercept", "block request", "intercept handler"*
```
research/ilspy-dumps/NetworkRequestGuardHandler.cs
research/ilspy-dumps/NetworkTransactionHandler.cs
research/ilspy-dumps/ContextMenuHandler.cs
research/ilspy-dumps/NavigationGuardHandler.cs
research/ilspy-dumps/CompositeInterceptHandler.cs
research/ilspy-dumps/IUrlSchemeHandler.cs
research/ilspy-dumps/BrowserService.cs          ← grep "handler\|Handler" for wiring
```

#### Turnstile (CAPTCHA)
*Matches: "Turnstile", "CAPTCHA", "AvaloniaTurnstileTokenProvider"*
```
research/ilspy-dumps/TurnstileBrowser.cs
research/ilspy-dumps/TurnstileToNativeBridge.cs
research/ilspy-dumps/AvaloniaTurnstileTokenProvider.cs
research/ilspy-dumps/TurnstileException.cs
```

---

### VOICE / CALLS / DMs / VOICEBAR

#### VoiceBar (status bar)
*Matches: "voice bar", "VoiceBar", "VoiceBarView", "voice status bar", "mute/deafen"*
```
research/ilspy-dumps/VoiceBarView.cs            ← read lines 1–200 (structure), then grep state
research/ilspy-dumps/VoiceBarViewModel.cs
```
For `VoiceBarView.cs` (2693 ln): `Read` with `limit: 200`, then `Grep` for `ViewModel\|Command\|State\|IsMuted\|IsDeafened` as needed.

#### Screenshare
*Matches: "screenshare", "screen share", "screenshare picker", "capture screen"*
```
research/ilspy-dumps/ScreenshareView.cs
research/ilspy-dumps/ScreenshareViewModel.cs
research/ilspy-dumps/ScreensharePickerView.cs
research/ilspy-dumps/ScreensharePickerViewModel.cs
research/ilspy-dumps/ScreenshareAudioFailedView.cs
research/ilspy-dumps/WebRtcService.cs           ← grep "screenshare\|screen" for WebRTC side
```

#### Call popup / CallingService
*Matches: "call popup", "CallPopout", "calling service", "join voice call", "PushToTalk"*
```
research/ilspy-dumps/CallPopoutWindow.cs
research/ilspy-dumps/CallPopoutViewModel.cs
research/ilspy-dumps/CallingService.cs
research/ilspy-dumps/PushToTalkService.cs
```

#### Direct messages (chat)
*Matches: "DM", "direct message", "DirectMessageContent", "DM chat", "DM list"*
```
research/ilspy-dumps/DirectMessageTabView.cs
research/ilspy-dumps/DirectMessageTabViewModel.cs
research/ilspy-dumps/DirectMessageContentView.cs
research/ilspy-dumps/DirectMessageContentViewModel.cs
```

#### DM calls
*Matches: "DM call", "DirectMessageCall", "voice call in DM"*
```
research/ilspy-dumps/DirectMessageCallContentView.cs
research/ilspy-dumps/DirectMessageCallContentViewModel.cs
research/ilspy-dumps/CallingService.cs
research/ilspy-dumps/VoiceBarView.cs            ← read lines 1–100 only
```

---

### SETTINGS / NAVIGATION

#### Settings page injection / navigation
*Matches: "settings injection", "sidebar injection", "Navigator", "settings navigation", "SidebarInjector", "add nav item"*
```
research/ilspy-dumps/Navigator.cs
research/ilspy-dumps/RootSettingsContainer.cs
research/ilspy-dumps/MenuItemPageContainerView.cs
research/ilspy-dumps/MenuItemPageContainerViewModel.cs
research/ilspy-dumps/IPage.cs
research/ilspy-dumps/SaveChangesView.cs
research/ilspy-dumps/ProfileSettingsView.cs     ← lines 1–150 (menu structure)
```

#### Specific settings page (chat, privacy, audio, keybindings, etc.)
*Matches: "chat settings", "privacy settings", "audio settings", "notifications settings", "keybindings settings"*

Load the named page + navigator context:
```
research/ilspy-dumps/{PageName}View.cs
research/ilspy-dumps/{PageName}ViewModel.cs
research/ilspy-dumps/Navigator.cs
research/ilspy-dumps/RootSettingsContainer.cs
```
Where `{PageName}` is: `ChatView`, `PrivacySettingsView`, `AudioVideoView`, `NotificationSettingsView`, `KeybindingsView`, `AdvancedSettingsView`, `WindowsSettingsView`, `EditProfileView`, `ChangePasswordView`, `StreamerModeSettingsView`, `GameOverlaySettingsView`.

---

### THEME / STYLES / CONTROLS / MARKDOWN

#### Theme colors / resource keys
*Matches: "theme", "colors", "resource keys", "ThemeEngine", "color doesn't update", "brushes", "theme variant"*
```
research/ilspy-dumps/ThemesDarkAxaml.cs
research/ilspy-dumps/ThemesLightAxaml.cs
research/ilspy-dumps/ThemesPureDarkAxaml.cs
research/ilspy-dumps/ThemeService.cs
research/ilspy-dumps/ThemeMapper.cs
research/ROOT_THEME_SYSTEM_FINDINGS.md
```

#### Theme CSS (browser layer)
*Matches: "CSS variables", "browser theme", "theme CSS", "RootThemeCss", "JS theme vars"*
```
research/ilspy-dumps/RootThemeCss.cs
research/ilspy-dumps/ThemesDarkAxaml.cs         ← grep "Css\|css\|var\(-" for CSS-mapped keys
```

#### Control template / style for a specific control
*Matches: "style", "control template", "visual state", "CheckBox template", "Button style", etc.*

Load the specific `Style_*.cs` file matching the control:
- CheckBox / ToggleSwitch pill → `Style_CheckBox.cs`
- Button (border) → `Style_BorderButton.cs`
- Button (transparent) → `Style_TransparentButton.cs`
- Button (SVG icon) → `Style_SvgButton.cs`
- ScrollViewer → `Style_ScrollViewer.cs`
- ComboBox → `Style_ComboBox.cs` + `Style_ComboBoxItem.cs`
- MenuItem → `Style_MenuItem.cs`
- ListBox → `Style_ListBox.cs` + `Style_ListBoxItem.cs`
- TabItem → `Style_TabItem.cs`
- Slider → `Style_Slider.cs`
- DropDownButton → `Style_DropDownButton.cs`
- ToolTip → `Style_ToolTip.cs`
- FlyoutPresenter → `Style_FlyoutPresenter.cs`
- Message/markdown → `Style_MessageMarkdown.cs`

Also load the matching control implementation if it exists (e.g., `RootScrollViewer.cs` alongside `Style_ScrollViewer.cs`).

For style grep across all styles: `Grep pattern:"<pattern>" path:"research/ilspy-dumps/StylesAll-GREPONLY.cs.txt"` — never Read that file.

#### Specific custom control (RootTextbox, RootScrollViewer, etc.)
*Matches: "RootTextbox", "RootScrollViewer", "RootConfirmationControl", "RootMenuFlyout", "RootBorder", etc.*

Load the specific control file:
```
research/ilspy-dumps/{ControlName}.cs
```
And its style file if it exists: `Style_{ControlName}.cs` or check ILSPY_NAVIGATION.md §Controls Library for the mapping.

#### Markdown / rich text rendering
*Matches: "markdown", "CTextBlock", "rich text", "inline rendering", "CHyperlink", "text selection"*
```
research/ilspy-dumps/CTextBlock.cs
research/ilspy-dumps/CSpan.cs
research/ilspy-dumps/CInline.cs
research/ilspy-dumps/CHyperlink.cs
research/ilspy-dumps/CRun.cs
research/ilspy-dumps/Style_MessageMarkdown.cs   ← read lines 1–100 only
```

---

### COMMUNITY / MEMBERS / PROFILE

#### Community / server views
*Matches: "community tab", "CommunityTab", "server view", "Rootcord main view"*
```
research/ilspy-dumps/CommunityTabView.cs        ← read lines 1–200 only
research/ilspy-dumps/CommunityTabViewModel.cs
research/ilspy-dumps/HomeView.cs                ← read lines 1–200 only
research/ilspy-dumps/HomeViewModel.cs           ← read lines 1–150 only
```
For files >800 ln: read first N lines only as specified, then grep for specific properties/methods.

#### Community audit log
*Matches: "community log", "audit log", "moderation log", "CommunityLog", "AuditLogEngine"*
```
research/ilspy-dumps/CommunityLogsView.cs
research/ilspy-dumps/CommunityLogsViewModel.cs
research/ilspy-dumps/CommunityLogView.cs
research/ilspy-dumps/CommunityLogViewModel.cs
research/ilspy-dumps/CommunityLogDetailsView.cs
research/ilspy-dumps/CommunityLogGrpcService.cs
```

#### Members panel
*Matches: "members panel", "member list", "MembersView", "member row", "member group header"*
```
research/ilspy-dumps/MembersView.cs
research/ilspy-dumps/MembersViewModel.cs
research/ilspy-dumps/MemberView.cs
research/ilspy-dumps/MemberViewModel.cs
research/ilspy-dumps/MemberGroupView.cs
research/ilspy-dumps/MemberGroupViewModel.cs
```

#### Member / user profile popup
*Matches: "profile popup", "MemberProfile", "user card", "profile card", "badge display"*
```
research/ilspy-dumps/MemberProfileView.cs
research/ilspy-dumps/MemberProfileViewModel.cs
research/ilspy-dumps/ProfileView.cs
research/ilspy-dumps/ProfileViewModel.cs
```

#### Roles
*Matches: "roles", "community roles", "role assignment", "CommunityRole"*
```
research/ilspy-dumps/CommunityRoleGrpcService.cs
research/ilspy-dumps/CommunityRoleResponse.cs
research/ilspy-dumps/CommunityRoleCreateRequest.cs
research/ilspy-dumps/CommunityMemberRoleGrpcService.cs
research/ilspy-dumps/CommunityPermission.cs
research/ilspy-dumps/CommunityPermissionsExtensions.cs
```

---

### PLUGINS / TYPESCRIPT

#### TypeScript plugin / bridge proxy
*Matches: "TypeScript", "plugin", "preload", "bridge proxy", "JS injection", "plugin API"*
```
docs/framework/TYPESCRIPT_REFERENCE.md
docs/plugins/API_REFERENCE.md
docs/plugins/BRIDGE_REFERENCE.md
src/core/preload.ts
src/core/pluginLoader.ts
src/api/bridge.ts
```

---

### gRPC / API DATA MODEL

#### gRPC for a specific domain
*Matches: "gRPC", "protobuf", "packet structure", "wire format" + specific domain*

Load only the service file + key entity files for that domain:
- Channels → `ChannelGrpcService.cs` + `ChannelResponse.cs` + `ChannelCreateRequest.cs`
- Messages → `MessageGrpcService.cs` + `MessagePayload.cs` + `MessageCreateRequest.cs`
- Community → `CommunityGrpcService.cs` + `CommunityResponse.cs`
- Members → `CommunityMemberGrpcService.cs` + `CommunityMemberResponse.cs`
- DMs → `DirectMessageGrpcService.cs`
- User → `UserGrpcService.cs` + `UserResponse.cs`
- WebRTC → `WebRtcGrpcService.cs`
- Files → `FileGrpcService.cs` + `FileContainerResponse.cs`
- Notifications → `NotificationGrpcService.cs` + `NotificationResponse.cs`
- Friendships → `FriendshipGrpcService.cs`

Also load: `docs/research/GRPC_PROTOCOL.md` if working at the wire level.

**Do not load all 27 service files** unless the user explicitly needs a cross-domain protocol overview.

---

### LOGIN / AUTH / SESSION

#### Login / registration flow
*Matches: "login", "register", "auth", "login UI", "LoginView"*
```
research/ilspy-dumps/LoginView.cs
research/ilspy-dumps/LoginViewModel.cs
research/ilspy-dumps/RegisterView.cs
research/ilspy-dumps/RegisterViewModel.cs
research/ilspy-dumps/RootSession.cs             ← grep "Login\|Token\|Auth" for session wiring
research/ilspy-dumps/IRootSessionAccessor.cs
```

#### Session / data persistence
*Matches: "RootSession", "LocalDataStore", "DataStoreKeys", "settings persistence", "session state"*
```
research/ilspy-dumps/RootSession.cs
research/ilspy-dumps/RootSessionAccessor.cs
research/ilspy-dumps/LocalDataStore.cs
research/ilspy-dumps/LocalDataStoreExtensions.cs
research/ilspy-dumps/DataStoreKeys.cs
```

---

### OVERLAY / MEDIA VIEWER / OTHER UI

#### Game overlay
*Matches: "overlay", "game overlay", "OverlayWindow", "OverlayViewModel"*
```
research/ilspy-dumps/OverlayWindow.cs
research/ilspy-dumps/OverlayViewModel.cs
research/ilspy-dumps/OverlayActionBar.cs
research/ilspy-dumps/OverlayActionBarViewModel.cs
research/ilspy-dumps/OverlayVoiceUser.cs
research/ilspy-dumps/GameOverlaySettingsView.cs
research/ilspy-dumps/GameOverlaySettingsViewModel.cs
```

#### Media viewer
*Matches: "media viewer", "image viewer", "video player", "MediaViewer"*
```
research/ilspy-dumps/MediaViewerView.cs
research/ilspy-dumps/MediaViewerViewModel.cs
research/ilspy-dumps/MediaViewerImageView.cs
research/ilspy-dumps/MediaViewerVideoView.cs
research/ilspy-dumps/MediaViewerGifView.cs
```

#### New tab / community discovery
*Matches: "new tab", "NewTab", "verified communities", "discover communities"*
```
research/ilspy-dumps/NewTabContentView.cs
research/ilspy-dumps/NewTabContentViewModel.cs
research/ilspy-dumps/NewTabCommunityView.cs
research/ilspy-dumps/DiscoverVerifiedCommunitiesView.cs
research/ilspy-dumps/VerifiedCommunitiesView.cs
```

---

## Phase 4: Report

After loading:

1. **Context loaded** — one line per file read, with line count
2. **Key facts** — 3–5 bullets directly relevant to the task:
   - Property names (for INPC intercept)
   - Method signatures (for reflection calls)
   - Visual tree structure (named controls, XAML parts)
   - Data shapes (DTO fields, gRPC message structure)
   - Bridge contracts / JS-callable methods
3. **Ready** — confirm ready to begin

Do not summarize files in full. Only extract the facts that bear on the stated task.

---

## Read Size Rules

| File | Rule |
|------|------|
| ≤ 500 ln | Read fully |
| 500–1500 ln | Read fully unless only one section is relevant; then grep first |
| > 1500 ln | `Read` with explicit `offset`/`limit` per the table below, OR `Grep` then targeted read |
| GREPONLY suffix | `Grep` only — never `Read` |

**Large file offsets** (first section covers structure; grep for the rest):

| File | Lines | First-pass read |
|------|------:|-----------------|
| `MessageView.cs` | 3602 | lines 1–300 (named controls, constructor) |
| `VoiceBarView.cs` | 2693 | lines 1–200 (layout structure) |
| `TextChannelContentView.cs` | 1624 | lines 1–200 (layout + named controls) |
| `CommunityTabView.cs` | 1232 | lines 1–200 |
| `HomeView.cs` | 1280 | lines 1–200 |
| `HomeViewModel.cs` | 883 | lines 1–150 |
| `RootSettingsContainer.cs` | 990 | lines 1–150 |
| `-AvaloniaResources-GREPONLY.cs` | 55k | Grep only |
| `XamlIlHelpers-GREPONLY.cs` | 33k | Grep only |
| `StylesAll-GREPONLY.cs.txt` | 5k | Grep only |
