# ILSpy Dump Index

> Master index for all decompiled files in `research/ilspy-dumps/`.
> Source: ILSpy decompilation of Root v0.9.92 and its Avalonia dependencies.
> Each file preserves original ILSpy comments (assembly version, full namespace path).

**Total:** 219 files, 145,326 lines, 9.0 MB

**Already analyzed into docs:** Files marked with a checkmark have been distilled into [ROOT_CONTROL_REFERENCE.md](../docs/framework/ROOT_CONTROL_REFERENCE.md) or [ROOT_THEME_SYSTEM_FINDINGS.md](ROOT_THEME_SYSTEM_FINDINGS.md). The raw dumps remain the authoritative source — the docs are curated summaries.

**GREPONLY convention:** Three files exceed 30k lines and have been renamed with a `-GREPONLY` suffix to signal they should not be read in full — use targeted line ranges or grep. `StylesAll-GREPONLY.cs.txt` additionally has a `.txt` extension to prevent linting errors. All `Style_*.cs` files cover StylesAll's contents in an indexed manner.

---

## Source Assemblies

| Assembly | Version | Files | Description |
|----------|---------|-------|-------------|
| `RootApp.Client.Avalonia` | 0.9.92.0 | 196 | Main UI assembly — views, styles, themes, controls, settings, navigation |
| `RootApp.Client.Domain` | 0.9.92.0 | 5 | Domain layer (DataStoreKeys, ILocalDataStore, LocalDataStore, extensions, secure storage) |
| `RootApp.Client.CoreDomain` | 0.9.92.0 | 4 | Core domain (IRootSessionAccessor, RootSessionAccessor, RootSession, RootSessionFactory) |
| `Root` | 0.9.92.0 | 1 | Root.exe entry point |
| `Avalonia.Controls` | 11.3.12.0 | 3 | Framework controls (Application, CheckBox, ToggleSwitch) |
| `Avalonia.Themes.Simple` | 11.3.12.0 | 1 | SimpleTheme base class |
| `AvaloniaEdit` | 11.3.0.0 | 3 | Text editor theme resources |

---

## Namespace Map

Namespaces discovered from ILSpy comment headers (line 2 of each file):

```
Root (exe)
├── Program                                          → Program.cs

RootApp.Client.Avalonia
├── App                                              → App.cs
├── CachedViewModelBase                              → CachedViewModelBase.cs
├── IBlocksEscapeKey                                 → IBlocksEscapeKey.cs
├── IRootAppWindowManager                            → IRootAppWindowManager.cs
├── IViewModelBase                                   → IViewModelBase.cs
├── RootAppWindowManager                             → RootAppWindowManager.cs
├── RootLauncher                                     → RootLauncher.cs
├── SelectableViewModelBase                          → SelectableViewModelBase.cs
├── ViewFactory                                      → ViewFactory.cs (static, 236 VM→View mappings)
├── ViewLocator                                      → ViewLocator.cs
├── ViewModelBase<T>                                 → ViewModelBase_T_.cs
├── Helpers
│   └── Navigation
│       ├── CommunityOpenerService                   → CommunityOpenerService.cs
│       ├── DirectMessageOpenerService               → DirectMessageOpenerService.cs
│       └── Navigator                                → Navigator.cs
├── Controls
│   ├── HexInputBorder                               → HexInputBorder.cs
│   ├── MemberVisibilityOption                       → MemberVisibilityOption.cs
│   ├── RootBorder                                   → RootBorder.cs
│   ├── RootChannelTypeRadioButton                   → RootChannelTypeRadioButton.cs
│   ├── RootCircleProgressBar                        → RootCircleProgressBar.cs
│   ├── RootCircularPanel                            → RootCircularPanel.cs
│   ├── RootConfirmationControl                      → RootConfirmationControl.cs
│   ├── RootFlyout                                   → RootFlyout.cs
│   ├── RootImageLoader                              → RootImageLoader.cs
│   ├── RootLinkButton                               → RootLinkButton.cs
│   ├── RootMarkdownTextBlock                        → RootMarkdownTextBlock.cs
│   ├── RootMemberVisibilitySwitch                   → RootMemberVisibilitySwitch.cs
│   ├── RootMenuFlyout                               → RootMenuFlyout.cs
│   ├── RootMessageItemsControl                      → RootMessageItemsControl.cs
│   ├── RootMessageScrollViewer                      → RootMessageScrollViewer.cs
│   ├── RootMultiCheckBox                            → RootMultiCheckBox.cs
│   ├── RootPercentageSlider                         → RootPercentageSlider.cs
│   ├── RootScrollBarThumb                           → RootScrollBarThumb.cs
│   ├── RootScrollViewer                             → RootScrollViewer.cs
│   ├── RootSplitView                                → RootSplitView.cs
│   ├── RootSvgButton                                → RootSvgButton.cs
│   ├── RootSvgCheckBox                              → RootSvgCheckBox.cs
│   ├── RootSvgImage                                 → RootSvgImage.cs
│   ├── RootTextbox                                  → RootTextbox.cs
│   ├── RootToolTip                                  → RootToolTip.cs
│   ├── RootTrimTooltipTextBlock                     → RootTrimTooltipTextBlock.cs
│   ├── RootWebApiStatus                             → RootWebApiStatus.cs
│   ├── RootZoomContainer                            → RootZoomContainer.cs
│   ├── SaveChangesView                              → SaveChangesView.cs
│   ├── StreamerModeBanner                           → StreamerModeBanner.cs
│   ├── StreamerModeBannerViewModel                  → StreamerModeBannerViewModel.cs
│   ├── TextWithBadgePanel                           → TextWithBadgePanel.cs
│   └── Settings
│       ├── IPage                                    → IPage.cs
│       ├── MenuItemPageContainerView                → MenuItemPageContainerView.cs
│       ├── MenuItemPageContainerViewModel           → MenuItemPageContainerViewModel.cs
│       └── RootSettingsContainer                    → RootSettingsContainer.cs
├── Markdown
│   └── Components
│       ├── CBold                                    → CBold.cs
│       ├── CCode                                    → CCode.cs
│       ├── CHyperlink                               → CHyperlink.cs
│       ├── CImage                                   → CImage.cs
│       ├── CInline                                  → CInline.cs
│       ├── CInlineUIContainer                       → CInlineUIContainer.cs
│       ├── CItalic                                  → CItalic.cs
│       ├── CLineBreak                               → CLineBreak.cs
│       ├── CRun                                     → CRun.cs
│       ├── CSpan                                    → CSpan.cs
│       ├── CStrikethrough                           → CStrikethrough.cs
│       ├── CTextBlock                               → CTextBlock.cs
│       ├── CTextBlockAutomationPeer                 → CTextBlockAutomationPeer.cs
│       ├── ITextPointerHandleable                   → ITextPointerHandleable.cs
│       ├── LineInfo                                  → LineInfo.cs
│       ├── SimpleTextSource                         → SimpleTextSource.cs
│       ├── TextPointer                              → TextPointer.cs
│       └── TextVerticalAlignment                    → TextVerticalAlignment.cs
├── Resources
│   ├── Converters
│   │   └── ThemeToBoolConverter                     → ThemeToBoolConverter.cs
│   └── Themes
│       ├── RootThemeEnum                            → RootThemeEnum.cs
│       ├── ThemeMapper                              → ThemeMapper.cs
│       └── ThemeService                             → ThemeService.cs
├── UI
│   ├── Community
│   │   ├── Members
│   │   │   ├── MemberBadgeDisplay                   → MemberBadgeDisplay.cs
│   │   │   ├── MemberCheckBoxView                   → MemberCheckBoxView.cs
│   │   │   ├── MemberCheckBoxViewModel              → MemberCheckBoxViewModel.cs
│   │   │   ├── MemberCheckBoxViewModelFactory       → MemberCheckBoxViewModelFactory.cs
│   │   │   ├── MemberGroupView                      → MemberGroupView.cs
│   │   │   ├── MemberGroupViewModel                 → MemberGroupViewModel.cs
│   │   │   ├── MemberGroupViewModelFactory          → MemberGroupViewModelFactory.cs
│   │   │   ├── MemberInviteView                     → MemberInviteView.cs
│   │   │   ├── MemberInviteViewModel                → MemberInviteViewModel.cs
│   │   │   ├── MemberInviteViewModelFactory         → MemberInviteViewModelFactory.cs
│   │   │   ├── MemberPickerView                     → MemberPickerView.cs
│   │   │   ├── MemberPickerViewModel                → MemberPickerViewModel.cs
│   │   │   ├── MemberPickerViewModelFactory         → MemberPickerViewModelFactory.cs
│   │   │   ├── MemberView                           → MemberView.cs
│   │   │   ├── MemberViewModel                      → MemberViewModel.cs
│   │   │   ├── MemberViewModelFactory               → MemberViewModelFactory.cs
│   │   │   ├── MembersView                          → MembersView.cs
│   │   │   ├── MembersViewModel                     → MembersViewModel.cs
│   │   │   └── MembersViewModelFactory              → MembersViewModelFactory.cs
│   │   └── Settings
│   │       └── (CommunityLogViewModel, etc. — referenced but not decompiled)
│   ├── Home
│   │   ├── CommunityTabView                         → CommunityTabView.cs
│   │   ├── CommunityTabViewModel                    → CommunityTabViewModel.cs
│   │   ├── CommunityTabViewModelFactory             → CommunityTabViewModelFactory.cs
│   │   ├── DirectMessageTabView                     → DirectMessageTabView.cs
│   │   ├── DirectMessageTabViewModel                → DirectMessageTabViewModel.cs
│   │   ├── DirectMessageTabViewModelFactory         → DirectMessageTabViewModelFactory.cs
│   │   ├── HomeView                                 → HomeView.cs
│   │   ├── HomeViewModel                            → HomeViewModel.cs
│   │   ├── HomeViewModelFactory                     → HomeViewModelFactory.cs
│   │   ├── ITabViewModel                            → ITabViewModel.cs
│   │   ├── NewTabView                               → NewTabView.cs
│   │   ├── NewTabViewModel                          → NewTabViewModel.cs
│   │   ├── NewTabViewModelFactory                   → NewTabViewModelFactory.cs
│   │   ├── PrivacyBlockedActionType                 → PrivacyBlockedActionType.cs
│   │   ├── PrivacyBlockedActionView                 → PrivacyBlockedActionView.cs
│   │   ├── PrivacyBlockedActionViewModel            → PrivacyBlockedActionViewModel.cs
│   │   ├── PrivacyBlockedActionViewModelFactory     → PrivacyBlockedActionViewModelFactory.cs
│   │   └── SystemTray.Profile.Settings
│   │       ├── ChangeThemeView                      → ChangeThemeView.cs
│   │       ├── ChangeThemeViewModel                 → ChangeThemeViewModel.cs
│   │       ├── ChangeThemeViewModelFactory          → ChangeThemeViewModelFactory.cs
│   │       ├── ChatView                             → ChatView.cs
│   │       └── ChatViewModel                        → ChatViewModel.cs
│   ├── Main
│   │   ├── ConnectionBlockingView                   → ConnectionBlockingView.cs
│   │   ├── ConnectionBlockingViewModel              → ConnectionBlockingViewModel.cs
│   │   ├── ConnectionBlockingViewModelFactory       → ConnectionBlockingViewModelFactory.cs
│   │   ├── MainView                                 → MainView.cs
│   │   ├── MainViewModel                            → MainViewModel.cs
│   │   ├── MainViewModelFactory                     → MainViewModelFactory.cs
│   │   └── MainWindow                               → MainWindow.cs
│   ├── Members
│   │   ├── MemberProfileView                        → MemberProfileView.cs
│   │   ├── MemberProfileViewModel                   → MemberProfileViewModel.cs
│   │   └── MemberProfileViewModelFactory            → MemberProfileViewModelFactory.cs
│   ├── Messages
│   │   ├── ChannelStartMessageView                  → ChannelStartMessageView.cs
│   │   ├── ChannelStartMessageViewModel             → ChannelStartMessageViewModel.cs
│   │   ├── MessageView                              → MessageView.cs
│   │   └── MessageViewModel                         → MessageViewModel.cs
│   └── Moderation
│       ├── BanMemberView                            → BanMemberView.cs
│       ├── BanMemberViewModel                       → BanMemberViewModel.cs
│       ├── BanMemberViewModelFactory                → BanMemberViewModelFactory.cs
│       ├── DeleteCommunityView                      → DeleteCommunityView.cs
│       ├── DeleteCommunityViewModel                 → DeleteCommunityViewModel.cs
│       ├── DeleteCommunityViewModelFactory          → DeleteCommunityViewModelFactory.cs
│       ├── DeleteCommunityViewModelValidator        → DeleteCommunityViewModelValidator.cs
│       ├── EnterVerificationCodeView                → EnterVerificationCodeView.cs
│       ├── EnterVerificationCodeViewModel           → EnterVerificationCodeViewModel.cs
│       ├── EnterVerificationCodeViewModelFactory    → EnterVerificationCodeViewModelFactory.cs
│       ├── EnterVerificationCodeViewModelValidator  → EnterVerificationCodeViewModelValidator.cs
│       ├── KickMemberView                           → KickMemberView.cs
│       ├── KickMemberViewModel                      → KickMemberViewModel.cs
│       ├── KickMemberViewModelFactory               → KickMemberViewModelFactory.cs
│       ├── LeaveCommunityView                       → LeaveCommunityView.cs
│       ├── LeaveCommunityViewModel                  → LeaveCommunityViewModel.cs
│       ├── LeaveCommunityViewModelFactory           → LeaveCommunityViewModelFactory.cs
│       ├── TransferOwnershipView                    → TransferOwnershipView.cs
│       ├── TransferOwnershipViewModel               → TransferOwnershipViewModel.cs
│       ├── TransferOwnershipViewModelFactory        → TransferOwnershipViewModelFactory.cs
│       └── TransferOwnershipViewModelValidator      → TransferOwnershipViewModelValidator.cs
│   └── (Invite flows)
│       ├── CreateCommunityView                      → CreateCommunityView.cs
│       ├── CreateCommunityViewModel                 → CreateCommunityViewModel.cs
│       ├── CreateCommunityViewModelFactory          → CreateCommunityViewModelFactory.cs
│       ├── CreateCommunityViewModelValidator        → CreateCommunityViewModelValidator.cs
│       ├── InviteMembersLinkSettingsView            → InviteMembersLinkSettingsView.cs
│       ├── InviteMembersLinkSettingsViewModel       → InviteMembersLinkSettingsViewModel.cs
│       ├── InviteMembersLinkSettingsViewModelFactory → InviteMembersLinkSettingsViewModelFactory.cs
│       ├── InviteMembersView                        → InviteMembersView.cs
│       ├── InviteMembersViewModel                   → InviteMembersViewModel.cs
│       └── InviteMembersViewModelFactory            → InviteMembersViewModelFactory.cs
│   └── (Voice)
│       ├── VoiceCallMemberAvatarViewModel           → VoiceCallMemberAvatarViewModel.cs
│       └── VoiceCallMemberAvatarViewModelFactory    → VoiceCallMemberAvatarViewModelFactory.cs

RootApp.Client.CoreDomain
├── IRootSessionAccessor                             → IRootSessionAccessor.cs
├── RootSessionAccessor                              → RootSessionAccessor.cs
├── RootSession                                      → RootSession.cs
└── RootSessionFactory                               → RootSessionFactory.cs

RootApp.Client.Domain
└── Helpers.Store
    ├── DataStoreKeys                                → DataStoreKeys.cs
    ├── ILocalDataStore                              → ILocalDataStore.cs
    ├── LocalDataStore                               → LocalDataStore.cs
    ├── LocalDataStoreExtensions                     → LocalDataStoreExtensions.cs
    └── SecureStorageImplementation                   → SecureStorageImplementation.cs

CompiledAvaloniaXaml (XAML-to-C# generated code)
├── !AvaloniaResources                               → -AvaloniaResources-GREPONLY.cs (55k lines)
│   ├── XamlClosure_53                               → XamlClosure_53.cs
│   ├── XamlClosure_54                               → XamlClosure_54.cs
│   └── XamlClosure_55                               → XamlClosure_55.cs
├── XamlDynamicSetters                               → XamlDynamicSetters.cs
├── XamlIlContext                                    → XamlIlContext.cs
├── XamlIlHelpers                                    → XamlIlHelpers-GREPONLY.cs (33k lines)
└── XamlIlTrampolines                                → XamlIlTrampolines.cs

Avalonia.Controls
├── Application                                      → Application.cs
├── CheckBox                                         → CheckBox.cs
└── ToggleSwitch                                     → ToggleSwitch.cs

Avalonia.Themes.Simple
└── SimpleTheme                                      → SimpleTheme.cs

AvaloniaEdit (CompiledAvaloniaXaml.!AvaloniaResources.NamespaceInfo)
├── /Themes/Base.xaml                                → NamespaceInfo_Themes_Base.cs
├── /Themes/Fluent/AvaloniaEdit.xaml                 → NamespaceInfo_Themes_Fluent.cs
└── /Themes/Simple/AvaloniaEdit.xaml                 → NamespaceInfo_Themes_Simple.cs
```

---

## File Inventory

### Views (code-behind)

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `MessageView.cs` | 3,602 | `UI.Messages.MessageView` | Y | Chat message control — named controls, updateBackgroundColor(), action bar, context menu, mention/hover highlights, role colors |
| `MemberProfileView.cs` | 1,582 | `UI.Members.MemberProfileView` | Y | Member profile popup — avatar, badges, online status, notes, quick message, action buttons |
| `MembersView.cs` | 1,319 | `UI.Community.Members.MembersView` | N | Community members panel |
| `HomeView.cs` | 1,280 | `UI.Home.HomeView` | N | Main view after login — tab host |
| `CommunityTabView.cs` | 1,232 | `UI.Home.CommunityTabView` | N | Community tab — channels, chat, members |
| `CreateCommunityView.cs` | 1,156 | `UI.Moderation` | N | Create community dialog |
| `InviteMembersView.cs` | 841 | `UI.Moderation` | N | Invite members dialog |
| `ChangeThemeView.cs` | 815 | `UI.Home.SystemTray.Profile.Settings.ChangeThemeView` | Y | Settings > Theme picker — RadioButton per theme, preview SVGs |
| `MemberView.cs` | 728 | `UI.Community.Members.MemberView` | N | Single member row in members panel |
| `InviteMembersLinkSettingsView.cs` | 633 | `UI.Moderation` | N | Invite link settings |
| `BanMemberView.cs` | 594 | `UI.Moderation.BanMemberView` | N | Ban member dialog |
| `MemberInviteView.cs` | 584 | `UI.Community.Members.MemberInviteView` | N | Member invite row |
| `DirectMessageTabView.cs` | 522 | `UI.Home.DirectMessageTabView` | N | DM tab — conversation list, DM chat |
| `ChatView.cs` | 424 | `UI.Home.SystemTray.Profile.Settings.ChatView` | partial | Settings > Chat page: emoji toggle, tap-to-reply toggle |
| `MemberCheckBoxView.cs` | 418 | `UI.Community.Members.MemberCheckBoxView` | N | Member checkbox row (role assignment) |
| `ChannelStartMessageView.cs` | 352 | `UI.Messages.ChannelStartMessageView` | N | "Welcome to #channel" start message |
| `MainWindow.cs` | 317 | `UI.Main.MainWindow` | Y | Top-level window — title bar, tray icon, close/minimize behavior |
| `MenuItemPageContainerView.cs` | 281 | `Controls.Settings.MenuItemPageContainerView` | N | Settings page container with menu navigation |
| `MainView.cs` | 259 | `UI.Main.MainView` | Y | Main content shell — hosts RootSplitView, navigation |
| `MemberPickerView.cs` | 247 | `UI.Community.Members.MemberPickerView` | N | Member picker (search + select) |
| `MemberGroupView.cs` | 187 | `UI.Community.Members.MemberGroupView` | N | Member group header in members panel |
| `NewTabView.cs` | 165 | `UI.Home.NewTabView` | N | "New Tab" creation page |
| `TransferOwnershipView.cs` | 161 | `UI.Moderation` | N | Transfer community ownership dialog |
| `DeleteCommunityView.cs` | 159 | `UI.Moderation` | N | Delete community dialog |
| `EnterVerificationCodeView.cs` | 151 | `UI.Moderation` | N | Verification code entry |
| `KickMemberView.cs` | 138 | `UI.Moderation` | N | Kick member dialog |
| `LeaveCommunityView.cs` | 137 | `UI.Moderation` | N | Leave community dialog |
| `PrivacyBlockedActionView.cs` | 129 | `UI.Home.PrivacyBlockedActionView` | N | Privacy blocked action banner |
| `ConnectionBlockingView.cs` | 91 | `UI.Main.ConnectionBlockingView` | N | Connection blocked / offline banner |
| `StreamerModeBanner.cs` | 202 | `Controls.StreamerModeBanner` | N | Streamer mode notification banner |
| `SaveChangesView.cs` | 396 | `Controls.SaveChangesView` | Y | Settings save/revert bar UI |

### ViewModels

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `HomeViewModel.cs` | 883 | `UI.Home.HomeViewModel` | N | Main view after login: tab management, navigation, DM calls |
| `MessageViewModel.cs` | 786 | `UI.Messages.MessageViewModel` | partial | Message data: HasSelfMention, HasLocalPendingReply, content, timestamps, edit state |
| `CommunityTabViewModel.cs` | 678 | `UI.Home.CommunityTabViewModel` | N | Community tab VM — channel lists, members, chat |
| `CreateCommunityViewModel.cs` | 464 | `UI.Moderation` | N | Create community VM |
| `MembersViewModel.cs` | 450 | `UI.Community.Members.MembersViewModel` | N | Members panel VM — member list, filters |
| `MemberProfileViewModel.cs` | 408 | `UI.Members.MemberProfileViewModel` | Y | Member profile VM — BadgeDisplays, IsSelf, Roles, commands |
| `MainViewModel.cs` | 400 | `UI.Main.MainViewModel` | Y | Top-level VM — DataContext chain root, navigation, DI container |
| `InviteMembersViewModel.cs` | 363 | `UI.Moderation` | N | Invite members VM |
| `DirectMessageTabViewModel.cs` | 340 | `UI.Home.DirectMessageTabViewModel` | N | DM tab VM — DM list, conversation management |
| `MemberInviteViewModel.cs` | 207 | `UI.Community.Members.MemberInviteViewModel` | N | Member invite VM |
| `MemberViewModel.cs` | 200 | `UI.Community.Members.MemberViewModel` | N | Single member VM |
| `InviteMembersLinkSettingsViewModel.cs` | 183 | `UI.Moderation` | N | Invite link settings VM |
| `MenuItemPageContainerViewModel.cs` | 135 | `Controls.Settings.MenuItemPageContainerViewModel` | Y | Settings page container VM — page navigation, menu item selection |
| `TransferOwnershipViewModel.cs` | 126 | `UI.Moderation` | N | Transfer ownership VM |
| `DeleteCommunityViewModel.cs` | 134 | `UI.Moderation` | N | Delete community VM |
| `EnterVerificationCodeViewModel.cs` | 125 | `UI.Moderation` | N | Verification code VM |
| `ChatViewModel.cs` | 116 | `UI.Home.SystemTray.Profile.Settings.ChatViewModel` | Y | Settings > Chat VM: AutoConvertEmojis, TapToReply (IPage, DataStore) |
| `BanMemberViewModel.cs` | 115 | `UI.Moderation.BanMemberViewModel` | N | Ban member VM |
| `DirectMessageOpenerService.cs` | 109 | `Helpers.Navigation.DirectMessageOpenerService` | Y | DM opener — DotNetBrowser discovery chain link |
| `ViewModelBase_T_.cs` | 107 | `ViewModelBase<T>` | N | Generic ViewModel base class |
| `LeaveCommunityViewModel.cs` | 100 | `UI.Moderation` | N | Leave community VM |
| `KickMemberViewModel.cs` | 93 | `UI.Moderation` | N | Kick member VM |
| `MemberCheckBoxViewModel.cs` | 83 | `UI.Community.Members.MemberCheckBoxViewModel` | N | Member checkbox VM |
| `MemberPickerViewModel.cs` | 84 | `UI.Community.Members.MemberPickerViewModel` | N | Member picker VM |
| `StreamerModeBannerViewModel.cs` | 67 | `Controls.StreamerModeBannerViewModel` | N | Streamer mode toggle state |
| `CommunityOpenerService.cs` | 60 | `Helpers.Navigation.CommunityOpenerService` | N | Community navigation service |
| `ChangeThemeViewModel.cs` | 55 | `UI.Home.SystemTray.Profile.Settings.ChangeThemeViewModel` | Y | Theme picker VM |
| `VoiceCallMemberAvatarViewModel.cs` | 53 | `UI.Home` | N | Voice call avatar VM |
| `NewTabViewModel.cs` | 49 | `UI.Home.NewTabViewModel` | N | New tab VM |
| `MemberGroupViewModel.cs` | 47 | `UI.Community.Members.MemberGroupViewModel` | N | Member group VM |
| `PrivacyBlockedActionViewModel.cs` | 37 | `UI.Home.PrivacyBlockedActionViewModel` | N | Privacy blocked VM |
| `ChannelStartMessageViewModel.cs` | 17 | `UI.Messages.ChannelStartMessageViewModel` | Y | Thin wrapper: exposes Message property |
| `ConnectionBlockingViewModel.cs` | 14 | `UI.Main.ConnectionBlockingViewModel` | N | Connection blocking state |

### ViewModel Factories

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `HomeViewModelFactory.cs` | 29 | `UI.Home` | N | DI factory |
| `MainViewModelFactory.cs` | 24 | `UI.Main` | N | DI factory |
| `MemberProfileViewModelFactory.cs` | 22 | `UI.Members` | N | DI factory |
| `CommunityTabViewModelFactory.cs` | 22 | `UI.Home` | N | DI factory |
| `CreateCommunityViewModelValidator.cs` | 20 | `UI.Moderation` | N | Validation rules |
| `MembersViewModelFactory.cs` | 19 | `UI.Community.Members` | N | DI factory |
| `DirectMessageTabViewModelFactory.cs` | 18 | `UI.Home` | N | DI factory |
| `InviteMembersViewModelFactory.cs` | 17 | `UI.Moderation` | N | DI factory |
| `InviteMembersLinkSettingsViewModelFactory.cs` | 16 | `UI.Moderation` | N | DI factory |
| `MemberCheckBoxViewModelFactory.cs` | 16 | `UI.Community.Members` | N | DI factory |
| `MemberInviteViewModelFactory.cs` | 16 | `UI.Community.Members` | N | DI factory |
| `MemberPickerViewModelFactory.cs` | 16 | `UI.Community.Members` | N | DI factory |
| `MemberViewModelFactory.cs` | 16 | `UI.Community.Members` | N | DI factory |
| `PrivacyBlockedActionViewModelFactory.cs` | 16 | `UI.Home` | N | DI factory |
| `DeleteCommunityViewModelFactory.cs` | 16 | `UI.Moderation` | N | DI factory |
| `LeaveCommunityViewModelFactory.cs` | 16 | `UI.Moderation` | N | DI factory |
| `NewTabViewModelFactory.cs` | 15 | `UI.Home` | N | DI factory |
| `CreateCommunityViewModelFactory.cs` | 15 | `UI.Moderation` | N | DI factory |
| `EnterVerificationCodeViewModelValidator.cs` | 15 | `UI.Moderation` | N | Validation rules |
| `TransferOwnershipViewModelFactory.cs` | 14 | `UI.Moderation` | N | DI factory |
| `TransferOwnershipViewModelValidator.cs` | 14 | `UI.Moderation` | N | Validation rules |
| `BanMemberViewModelFactory.cs` | 14 | `UI.Moderation` | N | DI factory |
| `DeleteCommunityViewModelValidator.cs` | 14 | `UI.Moderation` | N | Validation rules |
| `EnterVerificationCodeViewModelFactory.cs` | 14 | `UI.Moderation` | N | DI factory |
| `KickMemberViewModelFactory.cs` | 14 | `UI.Moderation` | N | DI factory |
| `MemberGroupViewModelFactory.cs` | 13 | `UI.Community.Members` | N | DI factory |
| `VoiceCallMemberAvatarViewModelFactory.cs` | 13 | `UI.Home` | N | DI factory |
| `ConnectionBlockingViewModelFactory.cs` | 12 | `UI.Main` | N | DI factory |
| `ChangeThemeViewModelFactory.cs` | 10 | `UI.Home.SystemTray.Profile.Settings` | Y | DI factory for ChangeThemeViewModel |

### Custom Controls

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `RootTextbox.cs` | 996 | `Controls.RootTextbox` | N | Custom textbox with validation, character limit, helper text |
| `RootSettingsContainer.cs` | 990 | `Controls.Settings.RootSettingsContainer` | Y | Settings page host — save bar, page switching, revert logic |
| `RootConfirmationControl.cs` | 901 | `Controls.RootConfirmationControl` | N | Confirmation dialog (delete/leave/ban flows) |
| `RootMemberVisibilitySwitch.cs` | 747 | `Controls.RootMemberVisibilitySwitch` | N | Member visibility toggle (complex multi-state control) |
| `RootMessageScrollViewer.cs` | 645 | `Controls.RootMessageScrollViewer` | N | Message list scroll container, load-more behavior |
| `RootMultiCheckBox.cs` | 478 | `Controls.RootMultiCheckBox` | N | Multi-option checkbox group |
| `RootScrollViewer.cs` | 431 | `Controls.RootScrollViewer` | N | Custom scroll viewer with opacity-based thumb show/hide |
| `RootMessageItemsControl.cs` | 378 | `Controls.RootMessageItemsControl` | Y | Virtualized message list — critical for MessageLogger |
| `RootCircularPanel.cs` | 290 | `Controls.RootCircularPanel` | N | Circular layout panel |
| `RootWebApiStatus.cs` | 282 | `Controls.RootWebApiStatus` | N | Web API status indicator |
| `RootCircleProgressBar.cs` | 277 | `Controls.RootCircleProgressBar` | N | Circular progress indicator |
| `RootImageLoader.cs` | 178 | `Controls.RootImageLoader` | N | Image loading control with placeholder |
| `RootFlyout.cs` | 172 | `Controls.RootFlyout` | N | Flyout popup base control |
| `RootSvgImage.cs` | 144 | `Controls.RootSvgImage` | N | SVG image with per-theme path binding |
| `HexInputBorder.cs` | 123 | `Controls.HexInputBorder` | N | Hex color input with border |
| `RootMenuFlyout.cs` | 97 | `Controls.RootMenuFlyout` | Y | Context menu flyout — Translate plugin prerequisite |
| `RootMarkdownTextBlock.cs` | 91 | `Controls.RootMarkdownTextBlock` | N | Markdown renderer entry point |
| `RootZoomContainer.cs` | 79 | `Controls.RootZoomContainer` | N | Zoom/scale container |
| `RootToolTip.cs` | 70 | `Controls.RootToolTip` | N | Custom tooltip |
| `TextWithBadgePanel.cs` | 66 | `Controls.TextWithBadgePanel` | N | Text + badge layout panel |
| `RootBorder.cs` | 61 | `Controls.RootBorder` | Y | Border subclass with DPI-aware DynamicBorderThickness |
| `RootChannelTypeRadioButton.cs` | 61 | `Controls.RootChannelTypeRadioButton` | N | Channel type radio button |
| `RootScrollBarThumb.cs` | 61 | `Controls.RootScrollBarThumb` | N | Custom scrollbar thumb |
| `RootTrimTooltipTextBlock.cs` | 51 | `Controls.RootTrimTooltipTextBlock` | N | TextBlock with tooltip on text trim |
| `RootPercentageSlider.cs` | 35 | `Controls.RootPercentageSlider` | N | Percentage slider |
| `RootSvgButton.cs` | 35 | `Controls.RootSvgButton` | N | Icon button (styled in Style_SvgButton.cs) |
| `RootSvgCheckBox.cs` | 35 | `Controls.RootSvgCheckBox` | N | Checkbox with SVG check mark |
| `RootLinkButton.cs` | 30 | `Controls.RootLinkButton` | N | Username display button |
| `RootSplitView.cs` | 13 | `Controls.RootSplitView` | N | Split view (thin wrapper — template in Style_RootSplitView.cs) |
| `MemberVisibilityOption.cs` | 8 | `Controls.MemberVisibilityOption` | N | Enum for member visibility options |

### Markdown Components

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `CTextBlock.cs` | 988 | `Markdown.Components.CTextBlock` | Y | Rich text renderer — AvaloniaList<CInline> content, CGeometry layout, text selection, pointer hit-testing |
| `CSpan.cs` | 306 | `Markdown.Components.CSpan` | Y | Container inline — child inlines, border/padding support, DecoratorGeometry wrapping |
| `CInline.cs` | 195 | `Markdown.Components.CInline` | Y | Abstract inline base — StyledElement, text properties, MeasureOverride/AsString abstract methods, invalidation chain |
| `CImage.cs` | 174 | `Markdown.Components.CImage` | Y | Inline image — async BitmapWrapper loading, aspect ratio, shrink-to-fit |
| `CHyperlink.cs` | 155 | `Markdown.Components.CHyperlink` | Y | Clickable link — extends CSpan, Command/CommandParameter, hover/press handlers |
| `TextPointer.cs` | 144 | `Markdown.Components.TextPointer` | N | Text selection position tracking |
| `LineInfo.cs` | 103 | `Markdown.Components.LineInfo` | N | Line layout info for CTextBlock rendering |
| `CRun.cs` | 75 | `Markdown.Components.CRun` | Y | Leaf inline — plain text, TextFormatter line breaking |
| `CInlineUIContainer.cs` | 44 | `Markdown.Components.CInlineUIContainer` | N | Embedded Control wrapper for inline rendering |
| `SimpleTextSource.cs` | 43 | `Markdown.Components.SimpleTextSource` | N | TextFormatter text source for CRun |
| `CTextBlockAutomationPeer.cs` | 30 | `Markdown.Components.CTextBlockAutomationPeer` | N | Accessibility automation peer |
| `CLineBreak.cs` | 22 | `Markdown.Components.CLineBreak` | N | Line break marker inline |
| `CBold.cs` | 15 | `Markdown.Components.CBold` | N | Bold formatting span (trivial CSpan subclass) |
| `CItalic.cs` | 15 | `Markdown.Components.CItalic` | N | Italic formatting span (trivial CSpan subclass) |
| `CStrikethrough.cs` | 14 | `Markdown.Components.CStrikethrough` | N | Strikethrough formatting span (trivial CSpan subclass) |
| `CCode.cs` | 13 | `Markdown.Components.CCode` | Y | Code span — trivial CSpan subclass, styling from Style_MessageMarkdown |
| `ITextPointerHandleable.cs` | 9 | `Markdown.Components.ITextPointerHandleable` | N | Interface for text selection handling |
| `TextVerticalAlignment.cs` | 9 | `Markdown.Components.TextVerticalAlignment` | N | Enum for inline vertical alignment |

### Settings Infrastructure

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `RootSettingsContainer.cs` | 990 | `Controls.Settings.RootSettingsContainer` | Y | Settings page host — save bar, page switching, revert logic |
| `SaveChangesView.cs` | 396 | `Controls.SaveChangesView` | Y | Settings save/revert bar UI |
| `Navigator.cs` | 287 | `Helpers.Navigation.Navigator` | Y | Settings page navigation stack — push/pop, HasPendingChanges, save/revert events |
| `MenuItemPageContainerView.cs` | 281 | `Controls.Settings.MenuItemPageContainerView` | N | Settings page container with menu navigation |
| `MenuItemPageContainerViewModel.cs` | 135 | `Controls.Settings.MenuItemPageContainerViewModel` | Y | Settings page container VM |
| `IPage.cs` | 7 | `Controls.Settings.IPage` | Y | Settings page interface |

### Data Store / Persistence

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `LocalDataStore.cs` | 271 | `RootApp.Client.Domain.Helpers.Store.LocalDataStore` | Y | Settings persistence implementation |
| `LocalDataStoreExtensions.cs` | 232 | `RootApp.Client.Domain.Helpers.Store.LocalDataStoreExtensions` | Y | Extension methods for ILocalDataStore |
| `SecureStorageImplementation.cs` | 86 | `RootApp.Client.Domain.Helpers.Store.SecureStorageImplementation` | Y | Encrypted credential storage |
| `DataStoreKeys.cs` | 69 | `RootApp.Client.Domain.Helpers.Store.DataStoreKeys` | Y | All 68 settings keys |
| `ILocalDataStore.cs` | 13 | `RootApp.Client.Domain.Helpers.Store.ILocalDataStore` | Y | Settings persistence interface |

### Session / Core Domain

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `RootSession.cs` | 505 | `RootApp.Client.CoreDomain.RootSession` | Y | Core session object — all service refs, packet dispatch, reconnect logic |
| `RootSessionAccessor.cs` | 47 | `RootApp.Client.CoreDomain.RootSessionAccessor` | Y | ObservableObject wrapper around Session [ObservableProperty] |
| `RootSessionFactory.cs` | 16 | `RootApp.Client.CoreDomain.RootSessionFactory` | N | DI factory for RootSession |
| `IRootSessionAccessor.cs` | 14 | `RootApp.Client.CoreDomain.IRootSessionAccessor` | Y | Session accessor interface |
| `IViewModelBase.cs` | 12 | `RootApp.Client.Avalonia.IViewModelBase` | Y | ViewModel base interface |

### ViewModel Infrastructure

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `ViewModelBase_T_.cs` | 107 | `ViewModelBase<T>` | N | Generic ViewModelBase with INotifyPropertyChanged, IDisposable |
| `SelectableViewModelBase.cs` | 43 | `SelectableViewModelBase` | N | Base for selectable items (IsSelected, SelectionChanged) |
| `CachedViewModelBase.cs` | 21 | `CachedViewModelBase` | N | Cached VM base (WeakReferenceMessenger) |
| `MemberBadgeDisplay.cs` | 15 | `UI.Community.Members.MemberBadgeDisplay` | N | Badge display data object |
| `ITabViewModel.cs` | 13 | `UI.Home.ITabViewModel` | N | Tab VM interface |

### Theme System

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `ThemesDarkAxaml.cs` | 285 | compiled XAML | Y | Dark theme: 32 color keys + ~220 SVG paths |
| `ThemesLightAxaml.cs` | 284 | compiled XAML | Y | Light theme: 32 color keys + ~220 SVG paths |
| `ThemesPureDarkAxaml.cs` | 283 | compiled XAML | Y | PureDark theme: overrides bg/border keys only (inherits Dark) |
| `ThemeService.cs` | 64 | `Resources.Themes.ThemeService` | Y | SetTheme(), InitializeTheme(), IsDefaultColor(), GetInvertedDefaultColorHex() |
| `ThemeMapper.cs` | 75 | `Resources.Themes.ThemeMapper` | Y | PureDark ThemeVariant, enum/variant conversion |
| `ThemeToBoolConverter.cs` | 24 | `Resources.Converters.ThemeToBoolConverter` | Y | RadioButton binding converter for theme picker |
| `RootThemeEnum.cs` | 9 | `Resources.Themes.RootThemeEnum` | Y | Enum: Default, Dark, Light, PureDark |

### Style Files (compiled XAML — control templates and visual states)

All from `RootApp.Client.Avalonia` / `CompiledAvaloniaXaml.!AvaloniaResources`.

| File | Lines | Analyzed | Controls Styled |
|------|------:|:--------:|-----------------|
| `Style_MessageMarkdown.cs` | 1,087 | Y | RootMarkdown + SimpleMessage: CTextBlock, mention pills, code blocks, blockquotes, headings |
| `Style_RootSplitView.cs` | 626 | partial | RootSplitView: main app shell layout (sidebar + content split) |
| `Style_ComboBox.cs` | 521 | partial | ComboBox: template, dropdown, input background |
| `Style_BorderButton.cs` | 356 | Y | Button classes: BorderButton, ListBorderButton, BasicButton, BasicButtonNeverOpaque |
| `Style_SvgButton.cs` | 341 | Y | RootSvgButton: standard, SvgDimmedButton, Custom variants |
| `Style_ComboBoxItem.cs` | 313 | Y | ComboBoxItem: hover/pressed/selected states |
| `Style_ScrollViewer.cs` | 242 | Y | RootScrollViewer + RootScrollBarThumb: opacity-based show/hide |
| `Style_Slider.cs` | 238 | partial | Slider: BrandPrimary foreground, Border track background |
| `Style_TransparentButton.cs` | 227 | Y | TransparentButton variants: Highlight, Opacity, ClickEffect |
| `Style_CheckBox.cs` | 220 | Y | CheckBox default + `.ToggleSwitch` class (45x25 toggle pill) |
| `Style_MenuItem.cs` | 193 | partial | MenuItem: hover states, DeleteMenuItem class |
| `Style_DropDownButton.cs` | 136 | N | DropDownButton: BackgroundTertiary bg, Border border |
| `Style_ListBoxItem.cs` | 111 | Y | ListBoxItem: all states transparent |
| `Style_RootColorPicker.cs` | 106 | N | RootColorPicker: color picker control |
| `Style_TabItem.cs` | 103 | Y | TabItem: selected pipe, TextPrimary foreground |
| `Style_LinkButton.cs` | 85 | partial | RootLinkButton: username display button |
| `Style_Separator.cs` | 83 | partial | Separator: Border color, 0.5px thickness |
| `Style_ListBox.cs` | 80 | Y | ListBox: transparent background/border |
| `Style_RootImageLoader.cs` | 76 | N | RootImageLoader: image loading/placeholder |
| `Style_BorderlessTextbox.cs` | 75 | N | BorderlessTextbox: no-border text input |
| `Style_TextButton.cs` | 73 | N | TextButton: text-only button variant |
| `Style_ToolTip.cs` | 71 | N | ToolTip: tooltip popup styling |
| `Style_MenuFlyoutPresenter.cs` | 71 | N | MenuFlyoutPresenter: context menu container |
| `Style_FlyoutPresenter.cs` | 55 | N | FlyoutPresenter: popup container |
| `Style_DragTabItem.cs` | 54 | N | DragTabItem: draggable tab styling |
| `Style_TabsTheme.cs` | 53 | N | TabsTheme: tab visual theme |
| `Style_TabsControl.cs` | 48 | N | TabsControl: tab container |

### App Infrastructure

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `ViewFactory.cs` | 1,725 | `RootApp.Client.Avalonia.ViewFactory` | Y | Static VM-to-View registry — 236 ViewModel mappings |
| `App.cs` | 635 | `RootApp.Client.Avalonia.App` | partial | App.Initialize(): XAML trampoline, theme init, menu delay |
| `AppBuilder.cs` | 325 | `Avalonia.AppBuilder` | N | Avalonia AppBuilder fluent API |
| `RootLauncher.cs` | 103 | `RootApp.Client.Avalonia.RootLauncher` | N | App launcher (DI host setup, composition root) |
| `Program.cs` | 65 | `Root.Program` | Y | Entry point: STA thread, Velopack, RootLauncher.Run() |
| `ViewLocator.cs` | 64 | `RootApp.Client.Avalonia.ViewLocator` | N | IDataTemplate — calls ViewFactory.CreateView() |
| `RootAppWindowManager.cs` | 29 | `RootApp.Client.Avalonia.RootAppWindowManager` | N | Window manager |

### Interfaces and Base Types

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `PrivacyBlockedActionType.cs` | 8 | `UI.Home` | N | Enum for privacy blocked action types |
| `IBlocksEscapeKey.cs` | 6 | `RootApp.Client.Avalonia` | N | Interface for Escape key blocking |
| `IRootAppWindowManager.cs` | 6 | `RootApp.Client.Avalonia` | N | Window manager interface |

### Avalonia Framework Internals

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `Application.cs` | 394 | `Avalonia.Application` | partial | Application base: Resources, Styles, ActualThemeVariantChanged, TryGetResource() |
| `SimpleTheme.cs` | 1,022 | `Avalonia.Themes.Simple.SimpleTheme` | partial | SimpleTheme base: ThemeControlHighlightLowBrush and other base keys |
| `ToggleSwitch.cs` | 88 | `Avalonia.Controls.ToggleSwitch` | partial | ToggleSwitch : ToggleButton — template parts, knob transitions |
| `CheckBox.cs` | 14 | `Avalonia.Controls.CheckBox` | Y | CheckBox : ToggleButton (trivial — just AutomationControlType override) |

### AvaloniaEdit Theme Resources

| File | Lines | Namespace | Description |
|------|------:|-----------|-------------|
| `NamespaceInfo_Themes_Base.cs` | 97 | `AvaloniaEdit` `/Themes/Base.xaml` | Base text editor theme |
| `NamespaceInfo_Themes_Fluent.cs` | 100 | `AvaloniaEdit` `/Themes/Fluent/AvaloniaEdit.xaml` | Fluent-themed editor |
| `NamespaceInfo_Themes_Simple.cs` | 100 | `AvaloniaEdit` `/Themes/Simple/AvaloniaEdit.xaml` | Simple-themed editor |

### Resource / Font / Sound Definitions

| File | Lines | Namespace | Description |
|------|------:|-----------|-------------|
| `FontsAxaml.cs` | 26 | compiled XAML | Font family resource definitions |
| `SoundsAxaml.cs` | 39 | compiled XAML | Sound resource URI definitions |

### XAML Compiler Infrastructure

These are generated by Avalonia's XAML-to-IL compiler. Mostly boilerplate, but contain useful type resolution info.

| File | Lines | Description |
|------|------:|-------------|
| `-AvaloniaResources-GREPONLY.cs` | 55,296 | **HUGE.** Master resource population — all views, styles, themes, resources. `-GREPONLY` suffix: use grep, not full read. |
| `XamlIlHelpers-GREPONLY.cs` | 33,645 | **HUGE.** XAML IL helper methods — type resolution, property setters. `-GREPONLY` suffix. |
| `StylesAll-GREPONLY.cs.txt` | 5,228 | All 27 style file registrations aggregated. `.txt` extension prevents linting errors. All `Style_*.cs` files cover this content in indexed form. |
| `XamlDynamicSetters.cs` | 584 | Dynamic property setter delegates for compiled bindings |
| `XamlIlContext.cs` | 274 | XAML IL compilation context — type mapping, namespace URIs |
| `XamlClosure_53.cs` | 233 | XAML closure: control template builder (theme preview) |
| `XamlClosure_54.cs` | 233 | XAML closure: control template builder (theme preview) |
| `XamlClosure_55.cs` | 233 | XAML closure: control template builder (theme preview) |
| `XamlIlTrampolines.cs` | 36 | Command trampolines — reveals RootSettingsContainer, MembersViewModel, CommunityLogViewModel, CommunityTabViewModel |

---

## Not Yet Decompiled

Classes referenced in the dumps but not present as standalone files. Candidates for future decompilation:

### Views
- Channel list view(s) — channel sidebar
- DM list view
- Voice/call UI views
- Community settings views — `CommunityLogViewModel` referenced

### Services
- Navigation service (how Root switches between pages)
- `BrowserService` (DotNetBrowser chain link)

---

## Cross-Reference: Dumps → Docs

| Document | Source Dumps |
|----------|-------------|
| [ROOT_CONTROL_REFERENCE.md](../docs/framework/ROOT_CONTROL_REFERENCE.md) | MessageView, MessageViewModel, ChatView, ChatViewModel, ChangeThemeView, ChangeThemeViewModel, ChangeThemeViewModelFactory, ChannelStartMessageViewModel, RootBorder, ThemeService, ThemeMapper, RootThemeEnum, ThemeToBoolConverter, DataStoreKeys, Program, App, ViewFactory, CheckBox, Style_CheckBox, Style_ComboBoxItem, Style_ListBox, Style_ListBoxItem, Style_SvgButton, Style_BorderButton, Style_TransparentButton, Style_ScrollViewer, Style_TabItem, Style_MessageMarkdown, Style_RootSplitView (partial), MainWindow, MainView, MainViewModel, RootSettingsContainer, SaveChangesView, IPage, MenuItemPageContainerViewModel, RootMessageItemsControl, RootMenuFlyout, ILocalDataStore, LocalDataStore, LocalDataStoreExtensions, SecureStorageImplementation, Navigator, IRootSessionAccessor, RootSessionAccessor, RootSession, IViewModelBase, DirectMessageOpenerService, CInline, CRun, CSpan, CHyperlink, CCode, CImage, CTextBlock, MemberProfileView, MemberProfileViewModel |
| [ROOT_THEME_SYSTEM_FINDINGS.md](ROOT_THEME_SYSTEM_FINDINGS.md) | ThemesDarkAxaml, ThemesLightAxaml, ThemesPureDarkAxaml |
| [THEME_ENGINE_DEEP_DIVE.md](../docs/framework/THEME_ENGINE_DEEP_DIVE.md) | ThemeService, ThemeMapper, SimpleTheme (partial) |

---

*Last updated: 2026-02-19 — 219 files from Root v0.9.92, Avalonia 11.3.12, AvaloniaEdit 11.3.0. Full inventory: Views (31), ViewModels (33), VM Factories (29), Custom Controls (30), Markdown (18), Settings (6), Data Store (5), Session (5), VM Infrastructure (5), Themes (7), Styles (27), App Infrastructure (7), Interfaces (3), Avalonia Framework (4), AvaloniaEdit (3), Resources (2), XAML Infrastructure (9).*
