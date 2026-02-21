# ILSpy Navigation Guide

> **Quick reference** for navigating ~1,395 decompiled Root v0.9.93 source files in `research/ilspy-dumps/`.
> **Full inventory** → [ILSPY_DUMP_INDEX.md](ILSPY_DUMP_INDEX.md)
> **Curated docs** → [ROOT_CONTROL_REFERENCE.md](../docs/framework/ROOT_CONTROL_REFERENCE.md) | [ROOT_THEME_SYSTEM_FINDINGS.md](ROOT_THEME_SYSTEM_FINDINGS.md)

---

## Quick Lookup by Task

| Working on... | Read these first | Then also read |
|---|---|---|
| **Channel sidebar / channel list** | `ChannelsView`, `ChannelView`, `ChannelViewModel`, `ChannelGroupView`, `ChannelGroupViewModel` | `TextChannelContentView`, `ChannelGrpcService`, `ChannelResponse`, `ChannelListResponse` |
| **Text channel messaging** | `TextChannelContentView`, `TextChannelContentViewModel` | `MessageView`, `MessageViewModel`, `MessageGrpcService`, `MessageCreateRequest` |
| **Voice channel / VoiceBar** | `VoiceBarView`, `VoiceBarViewModel`, `VoiceChannelContentView` | `CallingService`, `WebRtcService`, `CallPopoutWindow` |
| **Direct messages** | `DirectMessageContentView`, `DirectMessageContentViewModel` | `DirectMessageTabView`, `DirectMessageGrpcService`, `DirectMessageCreateRequest` |
| **DM calls** | `DirectMessageCallContentView`, `DirectMessageCallContentViewModel` | `CallingService`, `VoiceBarView`, `WebRtcService` |
| **Messages (edit/delete detection)** | `MessageView`, `MessageViewModel` | `MessageEditRequest`, `MessageDeleteRequest`, `MessagePacket`, `MessagePayload` |
| **Message reactions** | `AddReactionView`, `AddReactionViewModel` | `MessageView`, `ReactionView` |
| **Message replies** | `MessageRepliesView`, `MessageReplyView`, `MessageReplyViewModel` | `MessageView`, `MessageViewModel` |
| **Typing indicator** | `TypingIndicatorView`, `TypingIndicatorViewModel` | `TextChannelContentViewModel`, `DirectMessageContentViewModel` |
| **Profile / member popup** | `MemberProfileView`, `MemberProfileViewModel` | `MemberView`, `MemberViewModel`, `ProfileView`, `ProfileViewModel` |
| **Members panel** | `MembersView`, `MembersViewModel`, `MemberView`, `MemberViewModel` | `MemberGroupView`, `CommunityMemberGetResponse` |
| **Community / server list** | `CommunityTabView`, `CommunityTabViewModel`, `HomeView`, `HomeViewModel` | `CommunityGrpcService`, `CommunityResponse`, `CommunityListResponse` |
| **Community settings** | `CommunityLogsView`, `CommunityLogView` | `CommunityLogGrpcService`, `CommunityLogResponse` |
| **Settings pages (Root's)** | `ProfileSettingsView`, `RootSettingsContainer`, `Navigator`, `MenuItemPageContainerView` | `SaveChangesView`, `IPage`, `MenuItemPageContainerViewModel` |
| **Theme system** | `ThemesDarkAxaml`, `ThemesLightAxaml`, `ThemesPureDarkAxaml`, `ThemeService` | `ThemeMapper`, `RootThemeEnum`, `ThemeToBoolConverter`, `SimpleTheme` |
| **Theme CSS (JS layer)** | `RootThemeCss`, `RootThemeCssJsonContext` | `ThemesDarkAxaml`, `ThemesLightAxaml` |
| **Browser / DotNetBrowser** | `BrowserService`, `BrowserPool`, `BrowserRegistry` | `BrowserEngineManager`, `BrowserProfileManager`, `DeviceBrowser`, `RootAppBrowser` |
| **Bridge (JS↔native)** | `AppToNativeBridge`, `NativeToAppPrivateBridge` | `NativeToWebRtcBridge`, `WebRtcToNativeBridge`, `TurnstileToNativeBridge` |
| **WebRTC** | `WebRtcService`, `WebRtcBrowser` | `InitializeWebRtcPayload`, `WebRtcGrpcService`, `WebRtcUserInfoResponse`, `WebRtcCodec` |
| **HTTP request handling** | `NetworkRequestGuardHandler`, `ContextMenuHandler`, `NavigationGuardHandler` | `ImageCacheInterceptHandler`, `SoundEffectInterceptHandler` |
| **Login / auth flow** | `LoginView`, `LoginViewModel` | `RegisterView`, `RootSession`, `IRootSessionAccessor`, `RootSessionAccessor` |
| **gRPC services (API)** | See §gRPC Services below | `WebApiShared` assembly files |
| **API data model (DTOs)** | See §API Data Model below | `WebApi.Shared.Entities` assembly files |
| **Controls (scrollers, dialogs)** | `RootScrollViewer`, `RootTextbox`, `RootConfirmationControl` | `RootMenuFlyout`, `RootFlyout`, `RootMessageScrollViewer` |
| **Styles / control templates** | `Style_*.cs` files (27 total) | `StylesAll-GREPONLY.cs.txt` for grep |
| **Markdown / rich text** | `CTextBlock`, `CSpan`, `CHyperlink`, `CRun` | `CImage`, `CCode`, `CInline`, `RootMarkdownTextBlock` |
| **File upload / attachments** | `TextChannelFileUploadView`, `FileMessageView`, `FileMessageViewModel` | `FileGrpcService`, `FileCreateRequest`, `FileContainerResponse` |
| **Notifications** | `NotificationSettingsView`, `NotificationSettingsViewModel` | `NotificationGrpcService`, `NotificationResponse`, `ClientNotification` |
| **Overlay (game overlay)** | `OverlayWindow`, `OverlayViewModel`, `OverlayActionBar` | `OverlayMessageView`, `OverlayVoiceUser`, `GameOverlaySettingsView` |
| **Media viewer** | `MediaViewerView`, `MediaViewerViewModel` | `MediaViewerImageView`, `MediaViewerVideoView`, `MediaViewerGifView` |
| **Roles** | `CommunityRoleGrpcService`, `CommunityRoleCreateRequest`, `CommunityRoleResponse` | `CommunityRoleMapGrpcService` |
| **Friendships** | `FriendshipGrpcService`, `FriendshipGroupGrpcService` | `FriendshipCreatedPacket`, `FriendshipInviteGrpcService` |
| **UUID / GUID types** | See §UUID Types below | `RootCore` assembly files |
| **App hub / RootApps** | `AppHubConnection`, `AppRpcMessage` | `AppChannelService`, `RootAppBrowser`, `AppToNativeBridge` |
| **Keybindings** | `KeybindingsView`, `KeybindingsViewModel`, `KeybindingItemViewModel` | `KeybindingCategoryViewModel` |
| **Navigation service** | `Navigator`, `CommunityOpenerService`, `DirectMessageOpenerService` | `HomeViewModel`, `SelectChannelMessage`, `NavigateToChannelMessage` |

---

## File Groups by Domain

### 1. Channel System — 57 files

**UI (RootApp.Client.Avalonia):**
```
ChannelsView.cs               185 ln  — channel list (XAML shell)
ChannelsViewModel.cs               — channel list VM (reorder, select)
ChannelView.cs               1200 ln — single channel row UI
ChannelViewModel.cs           610 ln — channel state, voice join, app connect
ChannelGroupView.cs                — channel group header UI
ChannelGroupViewModel.cs           — channel group VM
ChannelGroupContainsAppView.cs     — channel group with embedded app
ChannelMediaMemberView.cs          — voice channel member avatar
SwitchVoiceCallConfirmationView.cs — "switch to this call?" dialog
```

**Channel management dialogs:**
```
CreateChannelView.cs         1692 ln
EditChannelView.cs
DeleteChannelView.cs
CreateChannelGroupView.cs
EditChannelGroupView.cs
DeleteChannelGroupView.cs
```

**Channel permissions UI:**
```
AccessRulesView.cs / AccessRuleView.cs / AccessRuleSelectorView.cs
```

**Channel content:**
```
TextChannelContentView.cs    1624 ln — text chat panel
TextChannelContentViewModel.cs 681 ln — send message, read, scroll
VoiceChannelContentView.cs         — voice channel panel
VoiceChannelContentViewModel.cs    — voice join/leave
AppChannelContentView.cs           — embedded app channel
AppChannelContentViewModel.cs      — app browser lifecycle
TextChannelFileUploadView.cs       — drag-drop file upload UI
```

**gRPC services (RootApp.WebApi.Shared):**
```
ChannelGrpcService.cs        271 ln — gRPC descriptor
ChannelGroupGrpcService.cs         — channel group service
AccessRuleGrpcService.cs           — permissions service
```

**API entities (RootApp.WebApi.Shared.Entities):**
```
ChannelCreateRequest/Response, ChannelEditRequest/Response
ChannelDeleteRequest/Response, ChannelGetRequest, ChannelListRequest/Response
ChannelResponse.cs, ChannelMovedPacket.cs, ChannelEditedPacket.cs
ChannelCreatedPacket.cs, ChannelDeletedPacket.cs
ChannelGroupCreateRequest/Response, ChannelGroupEditRequest/Response
ChannelGroupDeleteRequest/Response, ChannelGroupGetRequest
ChannelGroupListRequest/Response, ChannelGroupResponse.cs
ChannelGroupMovedPacket.cs, ChannelGroupCreatedPacket.cs
ChannelGroupEditedPacket.cs, ChannelGroupDeletedPacket.cs
ChannelOrChannelGroupGuid.cs, ChannelOrChannelGroupUuid.cs (Core)
```

**Converters:**
```
ChannelDescriptionToVisibilityConverter, ChannelPermissionMarginConverter
ChannelTypeToBoolConverter, ChannelTypeToSvgConverter
HasCustomChannelIconToMarginConverter
```

---

### 2. Message System — 82 files

**Primary views:**
```
MessageView.cs               3602 ln ★ — full message control (ANALYZED)
MessageViewModel.cs           786 ln ★ — message data model (partial)
CommunityMessageView.cs            — community chat message wrapper
CommunityMessageViewModel.cs
```

**Message action views:**
```
AddReactionView.cs            162 ln — emoji reaction picker
AddReactionViewModel.cs
DeleteMessageView.cs               — delete confirmation
DeleteMessageViewModel.cs
MessageRepliesView.cs              — thread view
MessageRepliesViewModel.cs
MessageReplyView.cs                — single reply row
MessageReplyViewModel.cs
TypingIndicatorView.cs             — "User is typing..."
TypingIndicatorViewModel.cs
ChannelStartMessageView.cs         — "Welcome to #channel" header
```

**Inline message type views (GIF/image/video/file/link/overlay):**
```
GifMessageView.cs / GifMessageViewModel.cs
ImageMessageView.cs / ImageMessageViewModel.cs
VideoMessageView.cs / VideoMessageViewModel.cs
FileMessageView.cs / FileMessageViewModel.cs
LinkMessageView.cs / LinkMessageViewModel.cs
OverlayMessageView.cs / OverlayMessageViewModel.cs
PendingMediaMessageView.cs / PendingMediaMessageViewModel.cs
```

**gRPC:**
```
MessageGrpcService.cs         568 ln — all message RPC descriptors
```

**API entities:**
```
MessageCreateRequest/Response, MessageEditRequest/Response
MessageDeleteRequest/Response, MessageGetRequest/Response
MessageListRequest/Response, MessageFlagRequest/Response
MessagePacket.cs, MessagePayload.cs, MessagePayloadAction.cs
MessagePayloadChannel.cs, MessagePayloadChannelState.cs
MessagePayloadCommunity.cs, MessagePayloadCommunityJoinThrottle.cs
MessageContainerResponse.cs
MessageDeletedPacket.cs
```

---

### 3. Direct Messages — 33 files

**UI:**
```
DirectMessageTabView.cs       522 ln — DM tab in sidebar
DirectMessageTabViewModel.cs  340 ln — DM list management
DirectMessageContentView.cs        — DM chat panel
DirectMessageContentViewModel.cs   — send/read/call in DM
DirectMessageCallContentView.cs    — active call UI inside DM
DirectMessageCallContentViewModel.cs
DirectMessageStartMessageView.cs   — DM start placeholder
DirectMessageStartMessageViewModel.cs
```

**gRPC + entities:**
```
DirectMessageGrpcService.cs
DirectMessageCreateRequest/Response, DirectMessageAddMembersRequest/Response
DirectMessageGetRequest, DirectMessageListRequest/Response
DirectMessageMemberGetRequest, DirectMessageMemberListRequest/Response
DirectMessageGuid.cs, DirectMessageMemberGuid.cs (Core)
```

---

### 4. Voice / Calls / VoiceBar — ~20 files

**VoiceBar UI:**
```
VoiceBarView.cs              2693 ln — voice status bar (large!)
VoiceBarViewModel.cs          438 ln — mute/deafen/screenshare state
ScreenshareView.cs                 — active screenshare display
ScreenshareViewModel.cs
ScreensharePickerView.cs           — screen/window selector
ScreensharePickerViewModel.cs
ScreenshareAudioFailedView.cs      — audio capture error
```

**Call popup:**
```
CallPopoutWindow.cs                — floating call window
CallPopoutViewModel.cs
```

**Services:**
```
CallingService.cs              71 ln — join/leave voice call
PushToTalkService.cs               — PTT keybind handling
CallPopoutService.cs               — popout window management
```

**VoiceBar converters:**
```
VoiceBarConnectionForegroundConverter, VoiceBarConnectionSvgConverter
VoiceBarConnectionTextConverter, VoiceBarScreenshareSvgConverter
VoiceBarSelectionBrushConverter, VoiceBarWebcamSvgConverter
```

---

### 5. Browser System & Bridges — ~45 files

**Browser management (RootApp.Browser):**
```
BrowserService.cs             252 ln — create/destroy all browser types
BrowserPool.cs                     — browser instance pool
BrowserRegistry.cs                 — registry of active browsers
BrowserEngineManager.cs            — DotNetBrowser engine lifecycle
BrowserProfileManager.cs           — browser profile (data dir)
BrowserAppearanceHelper.cs         — UI/theme browser integration
IRootBrowser.cs                    — browser abstraction interface
```

**Browser types:**
```
DeviceBrowser.cs / DeviceBrowserFactory.cs     — device-level browser
RootAppBrowser.cs / RootAppBrowserFactory.cs   — Root app channel browser
WebRtcBrowser.cs / WebRtcBrowserFactory.cs     — WebRTC browser
WarmBrowser.cs                                 — pre-warmed browser pool
TurnstileBrowser.cs / TurnstileBrowserFactory.cs — CAPTCHA browser
```

**HTTP handlers (RootApp.Browser.Handlers):**
```
ContextMenuHandler.cs              — intercept context menus
NavigationGuardHandler.cs          — block navigation
NetworkRequestGuardHandler.cs      — block network requests
NetworkTransactionHandler.cs       — inspect/modify requests
ImageCacheInterceptHandler.cs      — cache image responses
RootAppClientInterceptHandler.cs   — Root app client requests
RootAppDynamicAppInterceptHandler.cs
RootAppHostInterceptHandler.cs     — app host requests
SoundEffectInterceptHandler.cs     — audio intercept
WebRtcSuppressionInterceptHandler.cs — block WebRTC requests
CompositeInterceptHandler.cs       — chain multiple handlers
IUrlSchemeHandler.cs               — custom URL schemes
DevicePermissionHandler.cs / DevicePermissionHandlerFactory.cs
```

**Bridges (JS ↔ Native):**
```
AppToNativeBridge.cs          112 ln — app → native calls
AppToNativePrivateBridge.cs        — private app → native
NativeToAppPrivateBridge.cs        — native → app calls
NativeToWebRtcBridge.cs       196 ln — native → WebRTC
WebRtcToNativeBridge.cs            — WebRTC → native
TurnstileToNativeBridge.cs         — CAPTCHA → native
```

**JS utilities:**
```
JsObjectExtensions.cs              — JS object helpers
JsPromise.cs / JsPromiseHelper.cs  — Promise interop
```

---

### 6. WebRTC — ~20 files

```
WebRtcService.cs              621 ln — WebRTC session management
WebRtcBrowser.cs / WebRtcBrowserFactory.cs
WebRtcBundleExtractor.cs           — extract WebRTC resources
NoiseSuppressionBundleExtractor.cs — noise suppression resources
WebRtcToNativeBridge.cs            — bridge (see §Bridges)
NativeToWebRtcBridge.cs
InitializeWebRtcPayload.cs         — WebRTC init payload
WebRtcCodec.cs                     — codec type enum
WebRtcPermission.cs                — camera/mic permissions
```

**Track constraints:**
```
AudioTrackConstraints.cs, BaseVideoTrackConstraints.cs
VideoTrackConstraints.cs, ScreenTrackConstraints.cs
UserMediaStreamConstraints.cs, DisplayMediaStreamConstraints.cs
ConstrainBoolean.cs, ConstrainDouble.cs, ConstrainLong.cs, ConstrainString.cs
ScreenQualityMode.cs
```

**gRPC:**
```
WebRtcGrpcService.cs
WebRtcUserInfoResponse.cs, WebRtcUserDeviceSetTransportPacket.cs (entity)
```

---

### 7. Community System — 240 files

This is the largest domain. Community files cover all server functionality.

**Core UI:**
```
CommunityTabView.cs          1232 ln — channels panel + chat + members
CommunityTabViewModel.cs      678 ln — channel list, members, chat routing
HomeView.cs                  1280 ln — post-login main view (partial ANALYZED)
HomeViewModel.cs              883 ln — tab management, pane commands (partial ANALYZED)
```

**Community logs (audit log):**
```
CommunityLogsView.cs / CommunityLogsViewModel.cs — audit log list
CommunityLogView.cs / CommunityLogViewModel.cs   — single log entry
CommunityLogDateBreakView/VM                     — date separator
CommunityLogDetailsView/VM                       — log detail pane
CommunityLogFormatter.cs / FormattedCommunityLog.cs
CommunityLogGrpcService.cs
```

**gRPC (WebApiShared):**
```
CommunityGrpcService.cs            — community CRUD
CommunityMemberGrpcService.cs      — member management
CommunityMemberInviteGrpcService.cs — invites
CommunityMemberBanGrpcService.cs   — ban/unban
CommunityMemberRoleGrpcService.cs  — role assignment
CommunityRoleGrpcService.cs        — role CRUD
CommunityRoleMapGrpcService.cs     — role permission maps
CommunityAppGrpcService.cs         — app integration
CommunityAppLogGrpcService.cs
CommunityLogGrpcService.cs
```

**Notable entity files:**
```
CommunityResponse.cs, CommunityListResponse.cs
CommunityCreateRequest/Response, CommunityEditRequest/Response
CommunityMemberResponse.cs, CommunityMemberGetRequest
CommunityRoleResponse.cs, CommunityRoleCreateRequest/Response
CommunityJoinThrottlePacket.cs, CommunityPacket.cs
```

---

### 8. Roles & Permissions — ~30 files

```
CommunityRoleGrpcService.cs, CommunityRoleMapGrpcService.cs
ChannelPermission.cs, ChannelPermissionsExtensions.cs
CommunityPermission.cs, CommunityPermissionsExtensions.cs
PermissionPropertyFactory.cs, GlobalConstants.cs
IChannelPermissions.cs, ICommunityPermissions.cs (WebApiAbstractions)
AccessRuleGrpcService.cs
AccessRuleCreateRequest/Response, AccessRuleEditRequest/Response
AccessRuleDeleteRequest/Response, AccessRuleGetRequest
AccessRuleListByChannelOrChannelGroupRequest
AccessRuleListByRoleOrMemberRequest, AccessRuleListResponse
AccessRuleCreateRoleOrMemberRequest
ChannelOverlayPermission.cs
```

---

### 9. Settings Infrastructure — ~25 files (ANALYZED)

```
RootSettingsContainer.cs      990 ln ★ — settings host, save bar, page switching
SaveChangesView.cs            396 ln ★ — save/revert bar
Navigator.cs                  287 ln ★ — settings page navigation stack
MenuItemPageContainerView.cs  281 ln  — settings container with menu nav
MenuItemPageContainerViewModel.cs 135 ln ★
IPage.cs                        7 ln ★ — settings page interface
```

**Settings pages:**
```
ProfileSettingsView/VM        ★     — main settings hub
ChatView/VM                   ★     — chat settings (emoji, tap-to-reply)
ChangeThemeView/VM            ★     — theme picker
AudioVideoView/VM                   — audio/video settings
NotificationSettingsView/VM         — notification settings
PrivacySettingsView/VM              — privacy settings
AdvancedSettingsView/VM             — developer mode options
KeybindingsView/VM                  — keybindings editor
GameOverlaySettingsView/VM          — overlay settings
StreamerModeSettingsView/VM         — streamer mode
WindowsSettingsView/VM              — tray/startup settings
EditProfileView/VM                  — edit display name/bio/avatar
ChangePasswordView/VM               — password change
BlockedUsersView/VM                 — blocked user list
```

---

### 10. Theme System — 10 files (ANALYZED)

```
ThemesDarkAxaml.cs            285 ln ★ — dark theme: 32 color keys + SVG paths
ThemesLightAxaml.cs           284 ln ★ — light theme
ThemesPureDarkAxaml.cs        283 ln ★ — pure dark overrides
ThemeService.cs                64 ln ★ — SetTheme(), InitializeTheme()
ThemeMapper.cs                 75 ln ★ — PureDark variant, enum/variant conversion
RootThemeEnum.cs                9 ln ★ — enum: Default, Dark, Light, PureDark
ThemeToBoolConverter.cs        24 ln ★ — radio button binding
RootThemeCss.cs                    — CSS theme vars for browser layer
SimpleTheme.cs               1022 ln  — Avalonia SimpleTheme base (partial)
Application.cs                394 ln  — Avalonia Application base (partial)
```

---

### 11. Style Files — 27 files (compiled XAML templates)

All named `Style_*.cs`. Key ones:

```
Style_MessageMarkdown.cs     1087 ln ★ — CTextBlock, mentions, code blocks
Style_RootSplitView.cs        626 ln   — main app shell layout (partial)
Style_ComboBox.cs             521 ln   — ComboBox template (partial)
Style_BorderButton.cs         356 ln ★ — button variants
Style_SvgButton.cs            341 ln ★ — SVG icon buttons
Style_ComboBoxItem.cs         313 ln ★ — dropdown items
Style_ScrollViewer.cs         242 ln ★ — RootScrollViewer
Style_Slider.cs               238 ln   — slider (partial)
Style_TransparentButton.cs    227 ln ★ — transparent button variants
Style_CheckBox.cs             220 ln ★ — CheckBox + ToggleSwitch pill
Style_MenuItem.cs             193 ln   — context menu item (partial)
Style_DropDownButton.cs       136 ln
Style_ListBoxItem.cs          111 ln ★
Style_RootColorPicker.cs      106 ln
Style_TabItem.cs              103 ln ★
Style_LinkButton.cs            85 ln
Style_Separator.cs             83 ln
Style_ListBox.cs               80 ln ★
Style_RootImageLoader.cs       76 ln
Style_BorderlessTextbox.cs     75 ln
Style_TextButton.cs            73 ln
Style_ToolTip.cs               71 ln
Style_MenuFlyoutPresenter.cs   71 ln
Style_FlyoutPresenter.cs       55 ln
Style_DragTabItem.cs           54 ln
Style_TabsTheme.cs             53 ln
Style_TabsControl.cs           48 ln
```

**For grep searches:** use `StylesAll-GREPONLY.cs.txt` — all 27 styles aggregated.

---

### 12. Controls Library — 30 files

Custom Avalonia controls in `RootApp.Client.Avalonia.Controls.*`:

```
RootTextbox.cs                996 ln — textbox with validation, char limit
RootConfirmationControl.cs    901 ln — delete/leave/ban dialog
RootMemberVisibilitySwitch.cs 747 ln — multi-state member visibility
RootMessageScrollViewer.cs    645 ln — message list scroll + load-more
RootSettingsContainer.cs      990 ln ★ (see §Settings)
RootMultiCheckBox.cs          478 ln — multi-option checkboxes
RootScrollViewer.cs           431 ln — overlay scrollbar scrollviewer
RootCircularPanel.cs          290 ln — circular layout
RootWebApiStatus.cs           282 ln — API status indicator
RootCircleProgressBar.cs      277 ln — circular progress
RootImageLoader.cs            178 ln — async image with placeholder
RootFlyout.cs                 172 ln — flyout popup base
RootSvgImage.cs               144 ln — SVG with per-theme path binding
RootMenuFlyout.cs              97 ln ★ — context menu flyout
RootMarkdownTextBlock.cs       91 ln — markdown entry point
RootBorder.cs                  61 ln ★ — DPI-aware border
RootChannelTypeRadioButton.cs  61 ln — channel type radio
RootScrollBarThumb.cs          61 ln — scrollbar thumb
RootTrimTooltipTextBlock.cs    51 ln — trimmed text + tooltip
RootPercentageSlider.cs        35 ln — percentage slider
RootSvgButton.cs               35 ln — SVG icon button
RootSvgCheckBox.cs             35 ln — SVG checkbox
RootLinkButton.cs              30 ln — username link button
RootSplitView.cs               13 ln — main split view (thin wrapper)
```

---

### 13. Markdown / Rich Text — 18 files (ANALYZED)

```
CTextBlock.cs                 988 ln ★ — rich text renderer
CSpan.cs                      306 ln ★ — container inline
CInline.cs                    195 ln ★ — abstract inline base
CImage.cs                     174 ln ★ — inline image
CHyperlink.cs                 155 ln ★ — clickable link
CRun.cs                        75 ln ★ — leaf text node
CCode.cs                       13 ln ★ — code span
TextPointer.cs                144 ln  — selection position tracking
LineInfo.cs                   103 ln  — line layout info
CInlineUIContainer.cs          44 ln  — embedded control wrapper
SimpleTextSource.cs            43 ln  — TextFormatter source
CTextBlockAutomationPeer.cs    30 ln  — accessibility
CLineBreak.cs                  22 ln  — line break marker
CBold.cs / CItalic.cs / CStrikethrough.cs — 15 ln each — format spans
ITextPointerHandleable.cs / TextVerticalAlignment.cs — 9 ln each
RootMarkdownTextBlock.cs       91 ln  — XAML entry point
```

---

### 14. Navigation & Messaging — ~30 files

**Navigation:**
```
Navigator.cs                  287 ln ★ — settings page nav stack
CommunityOpenerService.cs      60 ln  — community navigation
DirectMessageOpenerService.cs 109 ln ★ — DM opening, DotNetBrowser link
ViewFactory.cs               1725 ln ★ — 236 VM→View mappings
ViewLocator.cs                        — IDataTemplate → ViewFactory
```

**WeakReferenceMessenger messages (MVVM Toolkit):**
```
SelectChannelMessage.cs        — select a channel
NavigateToChannelMessage.cs    — navigate to channel
SwitchChannelMessage.cs        — switch active channel
MarkChannelAsReadMessage.cs    — mark as read
SearchCurrentChannelMessage.cs — trigger search
SelectAppForCreateChannelMessage.cs
FocusDirectMessageCallMessage.cs
ResetVoiceBarMessage.cs
ScreenshareEnabledMessage.cs
```

---

### 15. Session & Core Domain — 10 files (ANALYZED)

```
RootSession.cs                505 ln ★ — all service refs, packet dispatch
RootSessionAccessor.cs         47 ln ★ — ObservableObject wrapper
IRootSessionAccessor.cs        14 ln ★ — session accessor interface
RootSessionFactory.cs          16 ln
IViewModelBase.cs              12 ln ★ — ViewModel interface
MainViewModel.cs              400 ln ★ — top-level VM, DI container root
MainView.cs                   259 ln ★ — main content shell
MainWindow.cs                 317 ln ★ — top-level window (ANALYZED)
```

---

### 16. Login / Auth / Register — ~20 files

```
LoginView.cs                  597 ln — login form UI
LoginViewModel.cs                  — login VM
RegisterView.cs                    — registration form
RegisterViewModel.cs / RegisterViewModelValidator.cs
ForgotUsernameOrPasswordPickerView.cs — forgot account picker
ForgotUsernameView.cs / ForgotUsernameViewModel.cs
ForgotPasswordViewModel.cs / ForgotPasswordViewModelValidator.cs
ResetPasswordView.cs / ResetPasswordViewModel.cs
TurnstileVerificationViewModel.cs  — CAPTCHA verification
AvaloniaTurnstileTokenProvider.cs  — CAPTCHA token bridge
TurnstileBrowser.cs                — CAPTCHA browser instance
```

---

### 17. Overlay (Game Overlay) — 10 files

```
OverlayWindow.cs                   — floating overlay window
OverlayViewModel.cs                — overlay state VM
OverlayActionBar.cs / OverlayActionBarViewModel.cs
OverlayMessageContainer.cs         — message container in overlay
OverlayMessageView.cs / OverlayMessageViewModel.cs
OverlayVoiceUser.cs / OverlayVoiceUserVisibilityConverter.cs
GameOverlaySettingsView/VM         — settings page for overlay
```

---

### 18. Media Viewer — 12 files

```
MediaViewerView.cs / MediaViewerViewModel.cs  — main media viewer
MediaViewerImageView/VM/Factory               — image viewer
MediaViewerVideoView/VM/Factory               — video player
MediaViewerGifView/VM/Factory                 — GIF viewer
LocalMediaViewerView.cs                       — local file viewer
FullScreenMediaViewerVideoWindow.cs           — fullscreen video
```

---

### 19. Friends / Presence — 37 files

```
FriendshipGrpcService.cs, FriendshipGroupGrpcService.cs
FriendshipInviteGrpcService.cs
FriendshipCreatedPacket.cs, FriendshipDeletedPacket.cs
FriendshipDeleteRequest/Response
FriendshipGroupCreateRequest/Response, FriendshipGroupEditRequest/Response
FriendshipGroupDeleteRequest/Response, FriendshipGroupMoveRequest/Response
FriendshipGroupMemberAddRequest/Response, FriendshipGroupMemberDeleteRequest
FriendshipGroupListRequest/Response, FriendshipGroupResponse.cs
FriendshipGroupGuid.cs, FriendshipGuid.cs (Core)
```

---

### 20. Notifications — 35 files

```
NotificationSettingsView/VM     — settings page (ANALYZED)
NotificationGrpcService.cs
NotificationResponse.cs, NotificationCountResponse.cs
NotificationCountListResponse.cs, NotificationCountUnviewedRequest.cs
NotificationDeleteAllRequest/Response
HubNotification.cs, ClientNotification.cs
LocalNotificationsWindow.cs, LocalNotificationShellViewModel.cs
```

---

### 21. Files / Uploads — 29 files

```
FileGrpcService.cs
FileCreateRequest/Response, FileDeleteRequest/Response
FileGetRequest, FileListRequest/Response
FileContainerResponse.cs
FileCreatedPacket.cs, FileDeletedPacket.cs
FileUploadResponse.cs, FileLoadingHelper.cs
FileMessageView/VM (see §Message System)
TextChannelFileUploadView/VM (see §Channel System)
MimeSignatureDetector.cs, MimeSignatureFactory.cs, MimeSignatureFilter.cs
ImageSizer.cs, AvifSizer.cs, GifSizer.cs, JpegSizer.cs, PngSizer.cs, WebPSizer.cs
```

---

### 22. Assets — 30 files (Core assembly)

```
AssetInformation.cs            — asset metadata (image/video/file/audio)
AssetImage.cs / AssetVideo.cs / AssetFile.cs / AssetAudio (via AssetPreviewAudio)
AssetPreview.cs                — preview info
AssetPreviewImage.cs / AssetPreviewVideo.cs / AssetPreviewAudio.cs
AssetImageLink.cs, AssetAspectRatio.cs
AssetAudioCodec.cs, AssetAudioFormat.cs
AssetVideoCodec.cs, AssetVideoFormat.cs
AssetInformationReflection.cs  — protobuf reflection
AssetGrpcService.cs (WebApiShared)
AssetAppCreateRequest/Response (entity)
AssetUuid.cs / AssetGuid.cs (Core)
AssetUriParser.cs              — parse asset URIs
```

---

### 23. gRPC Services — 27 files (RootApp.WebApi.Shared)

All gRPC service descriptor files:

```
MessageGrpcService.cs         568 ln — all message RPCs
ChannelGrpcService.cs         271 ln — channel CRUD + permissions
ChannelGroupGrpcService.cs         — channel group RPCs
CommunityGrpcService.cs            — community CRUD
CommunityMemberGrpcService.cs      — member management
CommunityMemberInviteGrpcService.cs — invite RPCs
CommunityMemberBanGrpcService.cs   — ban/unban RPCs
CommunityMemberRoleGrpcService.cs  — role assignment
CommunityRoleGrpcService.cs        — role CRUD
CommunityRoleMapGrpcService.cs     — role→permission maps
CommunityAppGrpcService.cs         — bot/app RPCs
CommunityAppLogGrpcService.cs
CommunityLogGrpcService.cs         — audit log RPCs
DirectMessageGrpcService.cs        — DM channel RPCs
UserGrpcService.cs                 — user profile RPCs
WebRtcGrpcService.cs               — WebRTC signaling RPCs
FriendshipGrpcService.cs           — friend RPCs
FriendshipGroupGrpcService.cs
FriendshipInviteGrpcService.cs
NotificationGrpcService.cs         — notification RPCs
FileGrpcService.cs                 — file upload RPCs
AssetGrpcService.cs                — asset management RPCs
LinkGrpcService.cs                 — link preview RPCs
AccessRuleGrpcService.cs           — permission rule RPCs
SupportGrpcService.cs              — support/report RPCs
DirectoryGrpcService.cs            — directory RPCs
AppStoreGrpcService.cs             — app store RPCs
```

---

### 24. API Data Model — ~700 files (RootApp.WebApi.Shared.Entities)

These are all protobuf-generated request/response DTOs. Organized by domain:

**Channels**: `ChannelCreate/Edit/Delete/Get/List/MoveRequest/Response`, `ChannelResponse`, `ChannelGroupCreate/Edit/Delete/Get/List/MoveRequest/Response`, `ChannelGroupResponse`

**Messages**: `MessageCreate/Edit/Delete/Get/List/FlagRequest/Response`, `MessagePayload*`, `MessagePacket`, `MessageContainerResponse`

**Community**: `CommunityCreate/Edit/Delete/Get/List/JoinRequest/Response`, `CommunityResponse`, `CommunityMember*`, `CommunityRole*`, `CommunityApp*`, `CommunityLog*`

**Direct Messages**: `DirectMessageCreate/AddMembers/Get/ListRequest/Response`, `DirectMessageMember*`

**Users**: `UserGetRequest/Response`, `UserUpdateRequest/Response`, `UserUpdateAvatarRequest/Response`, `UserResponse`

**WebRTC**: `WebRtcUserInfoResponse`, `WebRtcUserDeviceSetTransportPacket`, `WebRtcPeerConnectionPacket`

**Files/Assets**: `FileCreate/Delete/Get/ListRequest/Response`, `AssetApp*`

**Notifications**: `Notification*Request/Response`, `NotificationCount*`

**Friendships**: `Friendship*Request/Response`, `FriendshipGroup*`

**Packets** (server-push events): All `*CreatedPacket`, `*EditedPacket`, `*DeletedPacket`, `*Packet` files

---

### 25. UUID / GUID Types — ~120 files (RootApp.Core)

Every domain entity has both a UUID (protobuf-backed) and GUID (.NET Guid wrapper):

```
ChannelUuid / ChannelGuid
ChannelGroupUuid / ChannelGroupGuid
CommunityUuid / CommunityGuid
CommunityMemberUuid / CommunityMemberGuid
CommunityRoleUuid / CommunityRoleGuid
MessageUuid / MessageGuid
MessageContainerUuid / MessageContainerGuid
DirectMessageUuid / DirectMessageGuid
UserUuid / UserGuid
PersonUuid / PersonGuid
AssetUuid / AssetGuid
FileUuid / FileGuid
AppUuid / AppGuid / AppVersionUuid / AppDeploymentUuid
FriendshipUuid / FriendshipGuid
NotificationUuid / NotificationGuid
DirectoryUuid / DirectoryGuid
BadgeUuid / BadgeGuid
DeviceUuid / DeviceGuid
...
```

**Key utility types:**
```
RootUuid.cs                — base UUID type (protobuf IMessage)
RootGuid.cs                — base GUID type (.NET Guid wrapper)
RootUuidExtensionMethods.cs — UUID conversions
RootGuidExtensions.cs      — GUID conversions
WellKnownRootGuids.cs      — well-known GUIDs
IRootUuid.cs / IRootGuid.cs — interfaces
SemanticVersion.cs         — version comparison (protobuf)
RootWebApiConfig.cs        — API base URL, config constants
```

---

### 26. Utility (RootApp.Utility) — ~30 files

```
MimeSignatureDetector.cs       — file type detection by magic bytes
MimeSignatureFactory.cs / MimeSignatureFilter.cs
ImageSizer.cs                  — image dimension parsing
AvifSizer / GifSizer / JpegSizer / PngSizer / WebPSizer — per-format
RootClientFactory.cs           — HttpClient factory
RootClientHttpHandlers.cs      — HTTP handler chain
RootHttpHandlersOptions.cs / IRootHttpHandlers.cs
FireAndForgetHost.cs           — background task host
TaskHandle.cs / CancellationTokenExtensions.cs / TaskWaitExtensions.cs
ClampedDelayPolicy.cs          — exponential backoff
RootStringExtensions.cs / RootTimeSpanExtensions.cs / ScaleBytes.cs
RootCapitalization.cs          — text capitalization helpers
FilenameUtility.cs / RootContentInspectorExtensions.cs
```

---

### 27. App Hub & RootApps — ~14 files (RootApp.AppHub.Client)

```
AppHubConnection.cs            — app hub WebSocket connection
AppHubConnectionHandler.cs / IAppHubConnectionHandler.cs
KeepAliveManager.cs            — keep-alive heartbeat
AppRpcMessage.cs               — base RPC message
AppRpcMessageServerException.cs
AppRpcMessageToClient.cs / AppRpcMessageToHost.cs
AppRpcMessageToHostCommunityDelete.cs
AppRpcMessageType.cs           — message type enum
AppPacketToHost.cs / AppPingToHost.cs
AppConnection.cs / AppConnectionFactory.cs
```

---

### 28. API Connection Layer — ~25 files

**RootApp.WebApi.Client.Shared:**
```
ApiConnection.cs               — WebSocket API connection impl
ApiConnectionFactory.cs / IApiConnectionFactory.cs
ApiAuthorizationInterceptor.cs — auth token injection
WebApiHubConnectionHandler.cs  — hub connection
IApiConnection.cs              — connection interface
IConnectionStatusService.cs
ClientToken.cs                 — auth token model
ClientHubPacket.cs             — hub packet
TurnstileRetryPolicy.cs / TurnstileRequiredException.cs / TurnstileConstants.cs
NullTurnstileTokenProvider.cs / ITurnstileTokenProvider.cs
TokenResponse.cs, ReadOnlyProgressStream.cs
```

**RootApp.WebApi.Client:**
```
ApiConnectionHolder.cs / ApiConnectionManager.cs
ApiException.cs / NullApiConnection.cs
IRootDefaultApiConnection.cs / IRootDefaultApiConnectionManagement.cs
UploadContext.cs
```

**RootApp.WebApi.Abstractions:**
```
GlobalConstants.cs             — API base URLs, constants
IAuthenticated.cs
IChannelPermissions.cs / ICommunityPermissions.cs
```

---

### 29. Data Persistence — 5 files (ANALYZED)

```
LocalDataStore.cs             271 ln ★ — settings persistence
LocalDataStoreExtensions.cs   232 ln ★ — extension methods
ILocalDataStore.cs             13 ln ★
DataStoreKeys.cs               69 ln ★ — all 68 settings keys
SecureStorageImplementation.cs 86 ln ★ — encrypted credentials
```

---

### 30. XAML Infrastructure — 9 files (GREPONLY for large ones)

```
-AvaloniaResources-GREPONLY.cs   55,296 ln  — all resource population (GREPONLY)
XamlIlHelpers-GREPONLY.cs        33,645 ln  — XAML IL helpers (GREPONLY)
StylesAll-GREPONLY.cs.txt         5,228 ln  — all styles aggregated (GREPONLY)
XamlDynamicSetters.cs               584 ln  — property setter delegates
XamlIlContext.cs                    274 ln  — XAML IL context
XamlClosure_53/54/55.cs             233 ln each — theme preview closures
XamlIlTrampolines.cs                 36 ln  — command trampolines
```

---

## GREPONLY Files

Three files are too large to `Read` — always `Grep` them:

| File | Size | What to grep for |
|------|------|-----------------|
| `-AvaloniaResources-GREPONLY.cs` | 55k lines | Resource keys, control template registrations |
| `XamlIlHelpers-GREPONLY.cs` | 33k lines | Type resolution, setter method names |
| `StylesAll-GREPONLY.cs.txt` | 5k lines | Style class names, visual states |

---

## Annotation Key

- ★ = Analyzed and distilled into [ROOT_CONTROL_REFERENCE.md](../docs/framework/ROOT_CONTROL_REFERENCE.md) or [ROOT_THEME_SYSTEM_FINDINGS.md](ROOT_THEME_SYSTEM_FINDINGS.md)
- `partial` = partially analyzed
- no mark = raw dump only, not yet distilled

---

*Last updated: 2026-02-21 — Root v0.9.93, ~1,395 files across 16 assemblies.*
