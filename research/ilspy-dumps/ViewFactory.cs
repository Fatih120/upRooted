// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.ViewFactory
using System;
using Avalonia.Controls;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Emojis;
using RootApp.Client.Avalonia.Controls.Gifs;
using RootApp.Client.Avalonia.Controls.ImageUpload;
using RootApp.Client.Avalonia.Controls.Keybinds;
using RootApp.Client.Avalonia.Controls.Keybinds.Global;
using RootApp.Client.Avalonia.Controls.Messaging;
using RootApp.Client.Avalonia.Controls.Messaging.Attachments;
using RootApp.Client.Avalonia.Controls.Messaging.AutoCompleteItems;
using RootApp.Client.Avalonia.Controls.Messaging.Replies;
using RootApp.Client.Avalonia.Controls.ReorderableList;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Controls.Support;
using RootApp.Client.Avalonia.Controls.TitleBars;
using RootApp.Client.Avalonia.UI.Community;
using RootApp.Client.Avalonia.UI.Community.Apps.AppStore;
using RootApp.Client.Avalonia.UI.Community.Apps.FileUpload;
using RootApp.Client.Avalonia.UI.Community.Apps.GlobalSettings;
using RootApp.Client.Avalonia.UI.Community.Apps.GlobalSettings.ItemTypes;
using RootApp.Client.Avalonia.UI.Community.Channels;
using RootApp.Client.Avalonia.UI.Community.Channels.Permissions;
using RootApp.Client.Avalonia.UI.Community.Channels.Pickers;
using RootApp.Client.Avalonia.UI.Community.Content;
using RootApp.Client.Avalonia.UI.Community.LinkJoining;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Panes.Directories;
using RootApp.Client.Avalonia.UI.Community.Panes.Pinned;
using RootApp.Client.Avalonia.UI.Community.Panes.Search;
using RootApp.Client.Avalonia.UI.Community.Roles;
using RootApp.Client.Avalonia.UI.Community.Roles.Pickers;
using RootApp.Client.Avalonia.UI.Community.Settings;
using RootApp.Client.Avalonia.UI.Community.Settings.AppsBots;
using RootApp.Client.Avalonia.UI.Community.Settings.AppsBots.State;
using RootApp.Client.Avalonia.UI.Community.Settings.Roles;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Home.EmailValidation;
using RootApp.Client.Avalonia.UI.Home.KeyboardShortcuts;
using RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Friends;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Notifications;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Notifications.NotificationTypes;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.Avalonia.UI.Home.VoiceBar;
using RootApp.Client.Avalonia.UI.LocalNotifications;
using RootApp.Client.Avalonia.UI.Login;
using RootApp.Client.Avalonia.UI.Main;
using RootApp.Client.Avalonia.UI.MediaViewer;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Client.Avalonia.UI.NewTab;
using RootApp.Client.Avalonia.UI.Register;
using RootApp.Client.Avalonia.UI.Turnstile;

public static class ViewFactory
{
	public static Control CreateView(IViewModelBase P_0)
	{
		if (!(P_0 is EmojiCategoryViewModel dataContext))
		{
			if (!(P_0 is EmojiGroupViewModel dataContext2))
			{
				if (!(P_0 is EmojiPickerViewModel dataContext3))
				{
					if (!(P_0 is EmojiRowViewModel dataContext4))
					{
						if (!(P_0 is EmojiViewModel dataContext5))
						{
							if (!(P_0 is GifCategoryViewModel dataContext6))
							{
								if (!(P_0 is GifMediaViewModel dataContext7))
								{
									if (!(P_0 is GifPickerViewModel dataContext8))
									{
										if (!(P_0 is FileTooLargeConfirmationViewModel dataContext9))
										{
											if (!(P_0 is ImageUploaderViewModel dataContext10))
											{
												if (!(P_0 is GlobalHookKeybindViewModel dataContext11))
												{
													if (!(P_0 is KeybindConflictConfirmationViewModel dataContext12))
													{
														if (!(P_0 is KeybindEditorViewModel dataContext13))
														{
															if (!(P_0 is ResetAllKeybindingsConfirmationViewModel dataContext14))
															{
																if (!(P_0 is FileAttachmentViewModel dataContext15))
																{
																	if (!(P_0 is ChannelMentionAutoCompleteItemViewModel dataContext16))
																	{
																		if (!(P_0 is DividerAutoCompleteItemViewModel dataContext17))
																		{
																			if (!(P_0 is EmojiAutoCompleteItemViewModel dataContext18))
																			{
																				if (!(P_0 is GlobalMentionAutoCompleteItemViewModel dataContext19))
																				{
																					if (!(P_0 is RoleMentionAutoCompleteItemViewModel dataContext20))
																					{
																						if (!(P_0 is UserMentionAutoCompleteItemViewModel dataContext21))
																						{
																							if (!(P_0 is ReplyUserTagViewModel dataContext22))
																							{
																								if (!(P_0 is RootMessageTextboxViewModel dataContext23))
																								{
																									if (!(P_0 is ReorderablePlaceholderViewModel dataContext24))
																									{
																										if (!(P_0 is MenuItemPageContainerViewModel dataContext25))
																										{
																											if (!(P_0 is SupportViewModel dataContext26))
																											{
																												MacosTitleBarViewModel macosTitleBarViewModel = null;
																												if (macosTitleBarViewModel == null)
																												{
																													if (!(P_0 is WindowsTitleBarViewModel dataContext27))
																													{
																														if (!(P_0 is AppAlreadyInCommunityViewModel dataContext28))
																														{
																															if (!(P_0 is AppDetailsViewModel dataContext29))
																															{
																																if (!(P_0 is AppStoreViewModel dataContext30))
																																{
																																	if (!(P_0 is AppViewModel dataContext31))
																																	{
																																		if (!(P_0 is ScreenshotPreviewViewModel dataContext32))
																																		{
																																			if (!(P_0 is AppFileUploadItemViewModel dataContext33))
																																			{
																																				if (!(P_0 is GlobalSettingsGroupViewModel dataContext34))
																																				{
																																					if (!(P_0 is GlobalSettingsItemViewModel dataContext35))
																																					{
																																						if (!(P_0 is GlobalSettingsSelectorViewModel dataContext36))
																																						{
																																							if (!(P_0 is ItemRoleOrMemberViewModel dataContext37))
																																							{
																																								if (!(P_0 is ItemSelectViewModel dataContext38))
																																								{
																																									if (!(P_0 is ItemTextViewModel dataContext39))
																																									{
																																										if (!(P_0 is ChannelGroupContainsAppViewModel dataContext40))
																																										{
																																											if (!(P_0 is ChannelGroupViewModel dataContext41))
																																											{
																																												if (!(P_0 is ChannelMediaMemberViewModel dataContext42))
																																												{
																																													if (!(P_0 is ChannelsViewModel dataContext43))
																																													{
																																														if (!(P_0 is ChannelViewModel dataContext44))
																																														{
																																															if (!(P_0 is CreateChannelGroupViewModel dataContext45))
																																															{
																																																if (!(P_0 is CreateChannelViewModel dataContext46))
																																																{
																																																	if (!(P_0 is DeleteChannelGroupViewModel dataContext47))
																																																	{
																																																		if (!(P_0 is DeleteChannelViewModel dataContext48))
																																																		{
																																																			if (!(P_0 is EditChannelGroupViewModel dataContext49))
																																																			{
																																																				if (!(P_0 is EditChannelViewModel dataContext50))
																																																				{
																																																					if (!(P_0 is AccessRuleSelectorViewModel dataContext51))
																																																					{
																																																						if (!(P_0 is AccessRulesViewModel dataContext52))
																																																						{
																																																							if (!(P_0 is AccessRuleViewModel dataContext53))
																																																							{
																																																								if (!(P_0 is AppAccessRuleTagViewModel dataContext54))
																																																								{
																																																									if (!(P_0 is MemberAccessRuleTagViewModel dataContext55))
																																																									{
																																																										if (!(P_0 is RoleAccessRuleTagViewModel dataContext56))
																																																										{
																																																											if (!(P_0 is ChannelCheckBoxViewModel dataContext57))
																																																											{
																																																												if (!(P_0 is ChannelPickerViewModel dataContext58))
																																																												{
																																																													if (!(P_0 is SwitchVoiceCallConfirmationViewModel dataContext59))
																																																													{
																																																														if (!(P_0 is CommunityViewModel dataContext60))
																																																														{
																																																															if (!(P_0 is AppChannelContentViewModel dataContext61))
																																																															{
																																																																if (!(P_0 is TextChannelContentViewModel dataContext62))
																																																																{
																																																																	if (!(P_0 is TextChannelFileUploadViewModel dataContext63))
																																																																	{
																																																																		if (!(P_0 is VoiceChannelContentViewModel dataContext64))
																																																																		{
																																																																			if (!(P_0 is CommunityJoinFailedViewModel dataContext65))
																																																																			{
																																																																				if (!(P_0 is CommunityLinkJoinViewModel dataContext66))
																																																																				{
																																																																					if (!(P_0 is CommunityUnauthenticatedViewModel dataContext67))
																																																																					{
																																																																						if (!(P_0 is BanMemberViewModel dataContext68))
																																																																						{
																																																																							if (!(P_0 is DeleteCommunityViewModel dataContext69))
																																																																							{
																																																																								if (!(P_0 is InviteMembersLinkSettingsViewModel dataContext70))
																																																																								{
																																																																									if (!(P_0 is InviteMembersViewModel dataContext71))
																																																																									{
																																																																										if (!(P_0 is KickMemberViewModel dataContext72))
																																																																										{
																																																																											if (!(P_0 is LeaveCommunityViewModel dataContext73))
																																																																											{
																																																																												if (!(P_0 is MemberCheckBoxViewModel dataContext74))
																																																																												{
																																																																													if (!(P_0 is MemberGroupViewModel dataContext75))
																																																																													{
																																																																														if (!(P_0 is RootApp.Client.Avalonia.UI.Community.Members.MemberInviteViewModel dataContext76))
																																																																														{
																																																																															if (!(P_0 is MemberPickerViewModel dataContext77))
																																																																															{
																																																																																if (!(P_0 is MemberProfileViewModel dataContext78))
																																																																																{
																																																																																	if (!(P_0 is MembersViewModel dataContext79))
																																																																																	{
																																																																																		if (!(P_0 is MemberViewModel dataContext80))
																																																																																		{
																																																																																			if (!(P_0 is TransferOwnershipViewModel dataContext81))
																																																																																			{
																																																																																				if (!(P_0 is DirectoriesViewModel dataContext82))
																																																																																				{
																																																																																					if (!(P_0 is DirectoryFileUploadItemViewModel dataContext83))
																																																																																					{
																																																																																						if (!(P_0 is DirectoryFileUploadStatusViewModel dataContext84))
																																																																																						{
																																																																																							if (!(P_0 is DirectoryFileUploadViewModel dataContext85))
																																																																																							{
																																																																																								if (!(P_0 is DirectoryFileViewModel dataContext86))
																																																																																								{
																																																																																									if (!(P_0 is DirectoryNavigationItemViewModel dataContext87))
																																																																																									{
																																																																																										if (!(P_0 is DirectoryViewModel dataContext88))
																																																																																										{
																																																																																											if (!(P_0 is PinnedMessagesViewModel dataContext89))
																																																																																											{
																																																																																												if (!(P_0 is PinnedMessageViewModel dataContext90))
																																																																																												{
																																																																																													if (!(P_0 is SearchResultDirectoryFileViewModel dataContext91))
																																																																																													{
																																																																																														if (!(P_0 is SearchResultMemberViewModel dataContext92))
																																																																																														{
																																																																																															if (!(P_0 is SearchResultMessageViewModel dataContext93))
																																																																																															{
																																																																																																if (!(P_0 is SearchViewModel dataContext94))
																																																																																																{
																																																																																																	if (!(P_0 is AddRoleButtonViewModel dataContext95))
																																																																																																	{
																																																																																																		if (!(P_0 is RoleOrMemberPickerViewModel dataContext96))
																																																																																																		{
																																																																																																			if (!(P_0 is RolePickerViewModel dataContext97))
																																																																																																			{
																																																																																																				if (!(P_0 is RoleCheckBoxViewModel dataContext98))
																																																																																																				{
																																																																																																					if (!(P_0 is RoleTagViewModel dataContext99))
																																																																																																					{
																																																																																																						if (!(P_0 is AppBotChannelAssignmentsTabViewModel dataContext100))
																																																																																																						{
																																																																																																							if (!(P_0 is AppBotDetailsViewModel dataContext101))
																																																																																																							{
																																																																																																								if (!(P_0 is AppBotGeneralViewModel dataContext102))
																																																																																																								{
																																																																																																									if (!(P_0 is AppBotGlobalSettingsViewModel dataContext103))
																																																																																																									{
																																																																																																										if (!(P_0 is AppBotViewModel dataContext104))
																																																																																																										{
																																																																																																											if (!(P_0 is AppsBotsViewModel dataContext105))
																																																																																																											{
																																																																																																												if (!(P_0 is RemoveAppViewModel dataContext106))
																																																																																																												{
																																																																																																													if (!(P_0 is ChannelAppAssignmentViewModel dataContext107))
																																																																																																													{
																																																																																																														if (!(P_0 is ChannelGroupAppAssignmentViewModel dataContext108))
																																																																																																														{
																																																																																																															if (!(P_0 is UpdateAppViewModel dataContext109))
																																																																																																															{
																																																																																																																if (!(P_0 is BannedMemberDetailsViewModel dataContext110))
																																																																																																																{
																																																																																																																	if (!(P_0 is BannedMembersViewModel dataContext111))
																																																																																																																	{
																																																																																																																		if (!(P_0 is BannedMemberViewModel dataContext112))
																																																																																																																		{
																																																																																																																			if (!(P_0 is ComboBoxChannelItemViewModel dataContext113))
																																																																																																																			{
																																																																																																																				if (!(P_0 is CommunityLogDateBreakViewModel dataContext114))
																																																																																																																				{
																																																																																																																					if (!(P_0 is CommunityLogDetailsViewModel dataContext115))
																																																																																																																					{
																																																																																																																						if (!(P_0 is CommunityLogsViewModel dataContext116))
																																																																																																																						{
																																																																																																																							if (!(P_0 is CommunityLogViewModel dataContext117))
																																																																																																																							{
																																																																																																																								if (!(P_0 is CommunitySettingsViewModel dataContext118))
																																																																																																																								{
																																																																																																																									if (!(P_0 is DeleteCommunityLinkViewModel dataContext119))
																																																																																																																									{
																																																																																																																										if (!(P_0 is DiscardChangesViewModel dataContext120))
																																																																																																																										{
																																																																																																																											if (!(P_0 is MemberDetailsViewModel dataContext121))
																																																																																																																											{
																																																																																																																												if (!(P_0 is MemberInvitesViewModel dataContext122))
																																																																																																																												{
																																																																																																																													if (!(P_0 is RootApp.Client.Avalonia.UI.Community.Settings.MemberInviteViewModel dataContext123))
																																																																																																																													{
																																																																																																																														if (!(P_0 is MembersDetailsViewModel dataContext124))
																																																																																																																														{
																																																																																																																															if (!(P_0 is OverviewViewModel dataContext125))
																																																																																																																															{
																																																																																																																																if (!(P_0 is ProtectionSettingsViewModel dataContext126))
																																																																																																																																{
																																																																																																																																	if (!(P_0 is ChannelGroupRoleAssignmentViewModel dataContext127))
																																																																																																																																	{
																																																																																																																																		if (!(P_0 is ChannelRoleAssignmentViewModel dataContext128))
																																																																																																																																		{
																																																																																																																																			if (!(P_0 is DeleteRoleViewModel dataContext129))
																																																																																																																																			{
																																																																																																																																				if (!(P_0 is DiscardRoleChangesViewModel dataContext130))
																																																																																																																																				{
																																																																																																																																					if (!(P_0 is EditRoleChannelAssignmentsTabViewModel dataContext131))
																																																																																																																																					{
																																																																																																																																						if (!(P_0 is EditRoleContainerViewModel dataContext132))
																																																																																																																																						{
																																																																																																																																							if (!(P_0 is EditRoleDetailsTabViewModel dataContext133))
																																																																																																																																							{
																																																																																																																																								if (!(P_0 is EditRoleMembersTabViewModel dataContext134))
																																																																																																																																								{
																																																																																																																																									if (!(P_0 is EditRolePermissionsTabViewModel dataContext135))
																																																																																																																																									{
																																																																																																																																										if (!(P_0 is RoleMemberCheckBoxViewModel dataContext136))
																																																																																																																																										{
																																																																																																																																											if (!(P_0 is RolesViewModel dataContext137))
																																																																																																																																											{
																																																																																																																																												if (!(P_0 is RoleViewModel dataContext138))
																																																																																																																																												{
																																																																																																																																													if (!(P_0 is CommunityTabViewModel dataContext139))
																																																																																																																																													{
																																																																																																																																														if (!(P_0 is CreateCommunityViewModel dataContext140))
																																																																																																																																														{
																																																																																																																																															if (!(P_0 is DirectMessageTabViewModel dataContext141))
																																																																																																																																															{
																																																																																																																																																if (!(P_0 is VerificationCodeCooldownViewModel dataContext142))
																																																																																																																																																{
																																																																																																																																																	if (!(P_0 is EnterVerificationCodeViewModel dataContext143))
																																																																																																																																																	{
																																																																																																																																																		if (!(P_0 is HomeViewModel dataContext144))
																																																																																																																																																		{
																																																																																																																																																			if (!(P_0 is KeyboardShortcutsViewModel dataContext145))
																																																																																																																																																			{
																																																																																																																																																				if (!(P_0 is NewTabViewModel dataContext146))
																																																																																																																																																				{
																																																																																																																																																					if (!(P_0 is PrivacyBlockedActionViewModel dataContext147))
																																																																																																																																																					{
																																																																																																																																																						if (!(P_0 is AddDirectMessageMemberViewModel dataContext148))
																																																																																																																																																						{
																																																																																																																																																							if (!(P_0 is CreateDirectMessageViewModel dataContext149))
																																																																																																																																																							{
																																																																																																																																																								if (!(P_0 is DirectMessageCallContentViewModel dataContext150))
																																																																																																																																																								{
																																																																																																																																																									if (!(P_0 is DirectMessageContentViewModel dataContext151))
																																																																																																																																																									{
																																																																																																																																																										if (!(P_0 is DirectMessageDetailsViewModel dataContext152))
																																																																																																																																																										{
																																																																																																																																																											if (!(P_0 is DirectMessageFriendPickerViewModel dataContext153))
																																																																																																																																																											{
																																																																																																																																																												if (!(P_0 is DirectMessageFriendViewModel dataContext154))
																																																																																																																																																												{
																																																																																																																																																													if (!(P_0 is DirectMessageMemberViewModel dataContext155))
																																																																																																																																																													{
																																																																																																																																																														if (!(P_0 is DirectMessagesViewModel dataContext156))
																																																																																																																																																														{
																																																																																																																																																															if (!(P_0 is DirectMessageViewModel dataContext157))
																																																																																																																																																															{
																																																																																																																																																																if (!(P_0 is FriendTagViewModel dataContext158))
																																																																																																																																																																{
																																																																																																																																																																	if (!(P_0 is LeaveDirectMessageViewModel dataContext159))
																																																																																																																																																																	{
																																																																																																																																																																		if (!(P_0 is MemberBadgeViewModel dataContext160))
																																																																																																																																																																		{
																																																																																																																																																																			if (!(P_0 is AddFriendSearchResultViewModel dataContext161))
																																																																																																																																																																			{
																																																																																																																																																																				if (!(P_0 is AddFriendViewModel dataContext162))
																																																																																																																																																																				{
																																																																																																																																																																					if (!(P_0 is DeleteFriendGroupViewModel dataContext163))
																																																																																																																																																																					{
																																																																																																																																																																						if (!(P_0 is FriendGroupViewModel dataContext164))
																																																																																																																																																																						{
																																																																																																																																																																							if (!(P_0 is FriendsViewModel dataContext165))
																																																																																																																																																																							{
																																																																																																																																																																								if (!(P_0 is FriendViewModel dataContext166))
																																																																																																																																																																								{
																																																																																																																																																																									if (!(P_0 is RemoveFriendViewModel dataContext167))
																																																																																																																																																																									{
																																																																																																																																																																										if (!(P_0 is DeleteNotificationsViewModel dataContext168))
																																																																																																																																																																										{
																																																																																																																																																																											if (!(P_0 is NotificationsViewModel dataContext169))
																																																																																																																																																																											{
																																																																																																																																																																												if (!(P_0 is CommunityMemberBannedNotificationViewModel dataContext170))
																																																																																																																																																																												{
																																																																																																																																																																													if (!(P_0 is CommunityMemberInviteNotificationViewModel dataContext171))
																																																																																																																																																																													{
																																																																																																																																																																														if (!(P_0 is CommunityKickedNotificationViewModel dataContext172))
																																																																																																																																																																														{
																																																																																																																																																																															if (!(P_0 is FriendRequestNotificationViewModel dataContext173))
																																																																																																																																																																															{
																																																																																																																																																																																if (!(P_0 is FriendResponseNotificationViewModel dataContext174))
																																																																																																																																																																																{
																																																																																																																																																																																	if (!(P_0 is LocalMessageNotificationViewModel dataContext175))
																																																																																																																																																																																	{
																																																																																																																																																																																		if (!(P_0 is MentionNotificationViewModel dataContext176))
																																																																																																																																																																																		{
																																																																																																																																																																																			if (!(P_0 is BlockUserConfirmationViewModel dataContext177))
																																																																																																																																																																																			{
																																																																																																																																																																																				if (!(P_0 is ProfileViewModel dataContext178))
																																																																																																																																																																																				{
																																																																																																																																																																																					if (!(P_0 is AdvancedSettingsViewModel dataContext179))
																																																																																																																																																																																					{
																																																																																																																																																																																						if (!(P_0 is AudioVideoViewModel dataContext180))
																																																																																																																																																																																						{
																																																																																																																																																																																							if (!(P_0 is BlockedUsersViewModel dataContext181))
																																																																																																																																																																																							{
																																																																																																																																																																																								if (!(P_0 is BlockedUserViewModel dataContext182))
																																																																																																																																																																																								{
																																																																																																																																																																																									if (!(P_0 is ChangePasswordViewModel dataContext183))
																																																																																																																																																																																									{
																																																																																																																																																																																										if (!(P_0 is ChangeThemeViewModel dataContext184))
																																																																																																																																																																																										{
																																																																																																																																																																																											if (!(P_0 is ChatViewModel dataContext185))
																																																																																																																																																																																											{
																																																																																																																																																																																												if (!(P_0 is EditProfileViewModel dataContext186))
																																																																																																																																																																																												{
																																																																																																																																																																																													if (!(P_0 is GameOverlaySettingsViewModel dataContext187))
																																																																																																																																																																																													{
																																																																																																																																																																																														if (!(P_0 is KeybindingsViewModel dataContext188))
																																																																																																																																																																																														{
																																																																																																																																																																																															if (!(P_0 is NotificationSettingsViewModel dataContext189))
																																																																																																																																																																																															{
																																																																																																																																																																																																if (!(P_0 is PrivacySettingsViewModel dataContext190))
																																																																																																																																																																																																{
																																																																																																																																																																																																	if (!(P_0 is ProfileSettingsViewModel dataContext191))
																																																																																																																																																																																																	{
																																																																																																																																																																																																		if (!(P_0 is StreamerModeSettingsViewModel dataContext192))
																																																																																																																																																																																																		{
																																																																																																																																																																																																			if (!(P_0 is WindowsSettingsViewModel dataContext193))
																																																																																																																																																																																																			{
																																																																																																																																																																																																				if (!(P_0 is SignOutConfirmationViewModel dataContext194))
																																																																																																																																																																																																				{
																																																																																																																																																																																																					if (!(P_0 is ScreenshareAudioFailedViewModel dataContext195))
																																																																																																																																																																																																					{
																																																																																																																																																																																																						if (!(P_0 is ScreensharePickerViewModel dataContext196))
																																																																																																																																																																																																						{
																																																																																																																																																																																																							if (!(P_0 is ScreenshareViewModel dataContext197))
																																																																																																																																																																																																							{
																																																																																																																																																																																																								if (!(P_0 is VoiceBarViewModel dataContext198))
																																																																																																																																																																																																								{
																																																																																																																																																																																																									if (!(P_0 is LocalNotificationShellViewModel dataContext199))
																																																																																																																																																																																																									{
																																																																																																																																																																																																										if (!(P_0 is ForgotPasswordViewModel dataContext200))
																																																																																																																																																																																																										{
																																																																																																																																																																																																											if (!(P_0 is ForgotUsernameOrPasswordPickerViewModel dataContext201))
																																																																																																																																																																																																											{
																																																																																																																																																																																																												if (!(P_0 is ForgotUsernameViewModel dataContext202))
																																																																																																																																																																																																												{
																																																																																																																																																																																																													if (!(P_0 is LoginViewModel dataContext203))
																																																																																																																																																																																																													{
																																																																																																																																																																																																														if (!(P_0 is ResetPasswordViewModel dataContext204))
																																																																																																																																																																																																														{
																																																																																																																																																																																																															if (!(P_0 is ConnectionBlockingViewModel dataContext205))
																																																																																																																																																																																																															{
																																																																																																																																																																																																																if (!(P_0 is MainViewModel dataContext206))
																																																																																																																																																																																																																{
																																																																																																																																																																																																																	if (!(P_0 is LocalMediaViewerViewModel dataContext207))
																																																																																																																																																																																																																	{
																																																																																																																																																																																																																		if (!(P_0 is MediaViewerGifViewModel dataContext208))
																																																																																																																																																																																																																		{
																																																																																																																																																																																																																			if (!(P_0 is MediaViewerImageViewModel dataContext209))
																																																																																																																																																																																																																			{
																																																																																																																																																																																																																				if (!(P_0 is MediaViewerVideoViewModel dataContext210))
																																																																																																																																																																																																																				{
																																																																																																																																																																																																																					if (!(P_0 is MediaViewerViewModel dataContext211))
																																																																																																																																																																																																																					{
																																																																																																																																																																																																																						if (!(P_0 is AddReactionViewModel dataContext212))
																																																																																																																																																																																																																						{
																																																																																																																																																																																																																							if (!(P_0 is ChannelStartMessageViewModel dataContext213))
																																																																																																																																																																																																																							{
																																																																																																																																																																																																																								if (!(P_0 is CommunityMessageViewModel dataContext214))
																																																																																																																																																																																																																								{
																																																																																																																																																																																																																									if (!(P_0 is DeleteMessageViewModel dataContext215))
																																																																																																																																																																																																																									{
																																																																																																																																																																																																																										if (!(P_0 is DirectMessageStartMessageViewModel dataContext216))
																																																																																																																																																																																																																										{
																																																																																																																																																																																																																											if (!(P_0 is ExternalLinkConfirmationViewModel dataContext217))
																																																																																																																																																																																																																											{
																																																																																																																																																																																																																												if (!(P_0 is FileMessageViewModel dataContext218))
																																																																																																																																																																																																																												{
																																																																																																																																																																																																																													if (!(P_0 is GifMessageViewModel dataContext219))
																																																																																																																																																																																																																													{
																																																																																																																																																																																																																														if (!(P_0 is ImageMessageViewModel dataContext220))
																																																																																																																																																																																																																														{
																																																																																																																																																																																																																															if (!(P_0 is LinkMessageViewModel dataContext221))
																																																																																																																																																																																																																															{
																																																																																																																																																																																																																																if (!(P_0 is MessageRepliesViewModel dataContext222))
																																																																																																																																																																																																																																{
																																																																																																																																																																																																																																	if (!(P_0 is MessageReplyViewModel dataContext223))
																																																																																																																																																																																																																																	{
																																																																																																																																																																																																																																		if (!(P_0 is MessageViewModel dataContext224))
																																																																																																																																																																																																																																		{
																																																																																																																																																																																																																																			if (!(P_0 is PendingFileMessageViewModel dataContext225))
																																																																																																																																																																																																																																			{
																																																																																																																																																																																																																																				if (!(P_0 is PendingMediaMessageViewModel dataContext226))
																																																																																																																																																																																																																																				{
																																																																																																																																																																																																																																					if (!(P_0 is ReactionViewModel dataContext227))
																																																																																																																																																																																																																																					{
																																																																																																																																																																																																																																						if (!(P_0 is TypingIndicatorViewModel dataContext228))
																																																																																																																																																																																																																																						{
																																																																																																																																																																																																																																							if (!(P_0 is VideoMessageViewModel dataContext229))
																																																																																																																																																																																																																																							{
																																																																																																																																																																																																																																								if (!(P_0 is DiscoverVerifiedCommunitiesViewModel dataContext230))
																																																																																																																																																																																																																																								{
																																																																																																																																																																																																																																									if (!(P_0 is NewTabCommunityViewModel dataContext231))
																																																																																																																																																																																																																																									{
																																																																																																																																																																																																																																										if (!(P_0 is NewTabContentViewModel dataContext232))
																																																																																																																																																																																																																																										{
																																																																																																																																																																																																																																											if (!(P_0 is NewTabFavoriteCommunityViewModel dataContext233))
																																																																																																																																																																																																																																											{
																																																																																																																																																																																																																																												if (!(P_0 is VerifiedCommunitiesViewModel dataContext234))
																																																																																																																																																																																																																																												{
																																																																																																																																																																																																																																													if (!(P_0 is RegisterViewModel dataContext235))
																																																																																																																																																																																																																																													{
																																																																																																																																																																																																																																														if (P_0 is TurnstileVerificationViewModel dataContext236)
																																																																																																																																																																																																																																														{
																																																																																																																																																																																																																																															return new TurnstileVerificationView
																																																																																																																																																																																																																																															{
																																																																																																																																																																																																																																																DataContext = dataContext236
																																																																																																																																																																																																																																															};
																																																																																																																																																																																																																																														}
																																																																																																																																																																																																																																														throw new InvalidOperationException("No view found for {viewModel.GetType()}");
																																																																																																																																																																																																																																													}
																																																																																																																																																																																																																																													return new RegisterView
																																																																																																																																																																																																																																													{
																																																																																																																																																																																																																																														DataContext = dataContext235
																																																																																																																																																																																																																																													};
																																																																																																																																																																																																																																												}
																																																																																																																																																																																																																																												return new VerifiedCommunitiesView
																																																																																																																																																																																																																																												{
																																																																																																																																																																																																																																													DataContext = dataContext234
																																																																																																																																																																																																																																												};
																																																																																																																																																																																																																																											}
																																																																																																																																																																																																																																											return new NewTabFavoriteCommunityView
																																																																																																																																																																																																																																											{
																																																																																																																																																																																																																																												DataContext = dataContext233
																																																																																																																																																																																																																																											};
																																																																																																																																																																																																																																										}
																																																																																																																																																																																																																																										return new NewTabContentView
																																																																																																																																																																																																																																										{
																																																																																																																																																																																																																																											DataContext = dataContext232
																																																																																																																																																																																																																																										};
																																																																																																																																																																																																																																									}
																																																																																																																																																																																																																																									return new NewTabCommunityView
																																																																																																																																																																																																																																									{
																																																																																																																																																																																																																																										DataContext = dataContext231
																																																																																																																																																																																																																																									};
																																																																																																																																																																																																																																								}
																																																																																																																																																																																																																																								return new DiscoverVerifiedCommunitiesView
																																																																																																																																																																																																																																								{
																																																																																																																																																																																																																																									DataContext = dataContext230
																																																																																																																																																																																																																																								};
																																																																																																																																																																																																																																							}
																																																																																																																																																																																																																																							return new VideoMessageView
																																																																																																																																																																																																																																							{
																																																																																																																																																																																																																																								DataContext = dataContext229
																																																																																																																																																																																																																																							};
																																																																																																																																																																																																																																						}
																																																																																																																																																																																																																																						return new TypingIndicatorView
																																																																																																																																																																																																																																						{
																																																																																																																																																																																																																																							DataContext = dataContext228
																																																																																																																																																																																																																																						};
																																																																																																																																																																																																																																					}
																																																																																																																																																																																																																																					return new ReactionView
																																																																																																																																																																																																																																					{
																																																																																																																																																																																																																																						DataContext = dataContext227
																																																																																																																																																																																																																																					};
																																																																																																																																																																																																																																				}
																																																																																																																																																																																																																																				return new PendingMediaMessageView
																																																																																																																																																																																																																																				{
																																																																																																																																																																																																																																					DataContext = dataContext226
																																																																																																																																																																																																																																				};
																																																																																																																																																																																																																																			}
																																																																																																																																																																																																																																			return new PendingFileMessageView
																																																																																																																																																																																																																																			{
																																																																																																																																																																																																																																				DataContext = dataContext225
																																																																																																																																																																																																																																			};
																																																																																																																																																																																																																																		}
																																																																																																																																																																																																																																		return new MessageView
																																																																																																																																																																																																																																		{
																																																																																																																																																																																																																																			DataContext = dataContext224
																																																																																																																																																																																																																																		};
																																																																																																																																																																																																																																	}
																																																																																																																																																																																																																																	return new MessageReplyView
																																																																																																																																																																																																																																	{
																																																																																																																																																																																																																																		DataContext = dataContext223
																																																																																																																																																																																																																																	};
																																																																																																																																																																																																																																}
																																																																																																																																																																																																																																return new MessageRepliesView
																																																																																																																																																																																																																																{
																																																																																																																																																																																																																																	DataContext = dataContext222
																																																																																																																																																																																																																																};
																																																																																																																																																																																																																															}
																																																																																																																																																																																																																															return new LinkMessageView
																																																																																																																																																																																																																															{
																																																																																																																																																																																																																																DataContext = dataContext221
																																																																																																																																																																																																																															};
																																																																																																																																																																																																																														}
																																																																																																																																																																																																																														return new ImageMessageView
																																																																																																																																																																																																																														{
																																																																																																																																																																																																																															DataContext = dataContext220
																																																																																																																																																																																																																														};
																																																																																																																																																																																																																													}
																																																																																																																																																																																																																													return new GifMessageView
																																																																																																																																																																																																																													{
																																																																																																																																																																																																																														DataContext = dataContext219
																																																																																																																																																																																																																													};
																																																																																																																																																																																																																												}
																																																																																																																																																																																																																												return new FileMessageView
																																																																																																																																																																																																																												{
																																																																																																																																																																																																																													DataContext = dataContext218
																																																																																																																																																																																																																												};
																																																																																																																																																																																																																											}
																																																																																																																																																																																																																											return new ExternalLinkConfirmationView
																																																																																																																																																																																																																											{
																																																																																																																																																																																																																												DataContext = dataContext217
																																																																																																																																																																																																																											};
																																																																																																																																																																																																																										}
																																																																																																																																																																																																																										return new DirectMessageStartMessageView
																																																																																																																																																																																																																										{
																																																																																																																																																																																																																											DataContext = dataContext216
																																																																																																																																																																																																																										};
																																																																																																																																																																																																																									}
																																																																																																																																																																																																																									return new DeleteMessageView
																																																																																																																																																																																																																									{
																																																																																																																																																																																																																										DataContext = dataContext215
																																																																																																																																																																																																																									};
																																																																																																																																																																																																																								}
																																																																																																																																																																																																																								return new CommunityMessageView
																																																																																																																																																																																																																								{
																																																																																																																																																																																																																									DataContext = dataContext214
																																																																																																																																																																																																																								};
																																																																																																																																																																																																																							}
																																																																																																																																																																																																																							return new ChannelStartMessageView
																																																																																																																																																																																																																							{
																																																																																																																																																																																																																								DataContext = dataContext213
																																																																																																																																																																																																																							};
																																																																																																																																																																																																																						}
																																																																																																																																																																																																																						return new AddReactionView
																																																																																																																																																																																																																						{
																																																																																																																																																																																																																							DataContext = dataContext212
																																																																																																																																																																																																																						};
																																																																																																																																																																																																																					}
																																																																																																																																																																																																																					return new MediaViewerView
																																																																																																																																																																																																																					{
																																																																																																																																																																																																																						DataContext = dataContext211
																																																																																																																																																																																																																					};
																																																																																																																																																																																																																				}
																																																																																																																																																																																																																				return new MediaViewerVideoView
																																																																																																																																																																																																																				{
																																																																																																																																																																																																																					DataContext = dataContext210
																																																																																																																																																																																																																				};
																																																																																																																																																																																																																			}
																																																																																																																																																																																																																			return new MediaViewerImageView
																																																																																																																																																																																																																			{
																																																																																																																																																																																																																				DataContext = dataContext209
																																																																																																																																																																																																																			};
																																																																																																																																																																																																																		}
																																																																																																																																																																																																																		return new MediaViewerGifView
																																																																																																																																																																																																																		{
																																																																																																																																																																																																																			DataContext = dataContext208
																																																																																																																																																																																																																		};
																																																																																																																																																																																																																	}
																																																																																																																																																																																																																	return new LocalMediaViewerView
																																																																																																																																																																																																																	{
																																																																																																																																																																																																																		DataContext = dataContext207
																																																																																																																																																																																																																	};
																																																																																																																																																																																																																}
																																																																																																																																																																																																																return new MainView
																																																																																																																																																																																																																{
																																																																																																																																																																																																																	DataContext = dataContext206
																																																																																																																																																																																																																};
																																																																																																																																																																																																															}
																																																																																																																																																																																																															return new ConnectionBlockingView
																																																																																																																																																																																																															{
																																																																																																																																																																																																																DataContext = dataContext205
																																																																																																																																																																																																															};
																																																																																																																																																																																																														}
																																																																																																																																																																																																														return new ResetPasswordView
																																																																																																																																																																																																														{
																																																																																																																																																																																																															DataContext = dataContext204
																																																																																																																																																																																																														};
																																																																																																																																																																																																													}
																																																																																																																																																																																																													return new LoginView
																																																																																																																																																																																																													{
																																																																																																																																																																																																														DataContext = dataContext203
																																																																																																																																																																																																													};
																																																																																																																																																																																																												}
																																																																																																																																																																																																												return new ForgotUsernameView
																																																																																																																																																																																																												{
																																																																																																																																																																																																													DataContext = dataContext202
																																																																																																																																																																																																												};
																																																																																																																																																																																																											}
																																																																																																																																																																																																											return new ForgotUsernameOrPasswordPickerView
																																																																																																																																																																																																											{
																																																																																																																																																																																																												DataContext = dataContext201
																																																																																																																																																																																																											};
																																																																																																																																																																																																										}
																																																																																																																																																																																																										return new ForgotPasswordView
																																																																																																																																																																																																										{
																																																																																																																																																																																																											DataContext = dataContext200
																																																																																																																																																																																																										};
																																																																																																																																																																																																									}
																																																																																																																																																																																																									return new LocalNotificationShellView
																																																																																																																																																																																																									{
																																																																																																																																																																																																										DataContext = dataContext199
																																																																																																																																																																																																									};
																																																																																																																																																																																																								}
																																																																																																																																																																																																								return new VoiceBarView
																																																																																																																																																																																																								{
																																																																																																																																																																																																									DataContext = dataContext198
																																																																																																																																																																																																								};
																																																																																																																																																																																																							}
																																																																																																																																																																																																							return new ScreenshareView
																																																																																																																																																																																																							{
																																																																																																																																																																																																								DataContext = dataContext197
																																																																																																																																																																																																							};
																																																																																																																																																																																																						}
																																																																																																																																																																																																						return new ScreensharePickerView
																																																																																																																																																																																																						{
																																																																																																																																																																																																							DataContext = dataContext196
																																																																																																																																																																																																						};
																																																																																																																																																																																																					}
																																																																																																																																																																																																					return new ScreenshareAudioFailedView
																																																																																																																																																																																																					{
																																																																																																																																																																																																						DataContext = dataContext195
																																																																																																																																																																																																					};
																																																																																																																																																																																																				}
																																																																																																																																																																																																				return new SignOutConfirmationView
																																																																																																																																																																																																				{
																																																																																																																																																																																																					DataContext = dataContext194
																																																																																																																																																																																																				};
																																																																																																																																																																																																			}
																																																																																																																																																																																																			return new WindowsSettingsView
																																																																																																																																																																																																			{
																																																																																																																																																																																																				DataContext = dataContext193
																																																																																																																																																																																																			};
																																																																																																																																																																																																		}
																																																																																																																																																																																																		return new StreamerModeSettingsView
																																																																																																																																																																																																		{
																																																																																																																																																																																																			DataContext = dataContext192
																																																																																																																																																																																																		};
																																																																																																																																																																																																	}
																																																																																																																																																																																																	return new ProfileSettingsView
																																																																																																																																																																																																	{
																																																																																																																																																																																																		DataContext = dataContext191
																																																																																																																																																																																																	};
																																																																																																																																																																																																}
																																																																																																																																																																																																return new PrivacySettingsView
																																																																																																																																																																																																{
																																																																																																																																																																																																	DataContext = dataContext190
																																																																																																																																																																																																};
																																																																																																																																																																																															}
																																																																																																																																																																																															return new NotificationSettingsView
																																																																																																																																																																																															{
																																																																																																																																																																																																DataContext = dataContext189
																																																																																																																																																																																															};
																																																																																																																																																																																														}
																																																																																																																																																																																														return new KeybindingsView
																																																																																																																																																																																														{
																																																																																																																																																																																															DataContext = dataContext188
																																																																																																																																																																																														};
																																																																																																																																																																																													}
																																																																																																																																																																																													return new GameOverlaySettingsView
																																																																																																																																																																																													{
																																																																																																																																																																																														DataContext = dataContext187
																																																																																																																																																																																													};
																																																																																																																																																																																												}
																																																																																																																																																																																												return new EditProfileView
																																																																																																																																																																																												{
																																																																																																																																																																																													DataContext = dataContext186
																																																																																																																																																																																												};
																																																																																																																																																																																											}
																																																																																																																																																																																											return new ChatView
																																																																																																																																																																																											{
																																																																																																																																																																																												DataContext = dataContext185
																																																																																																																																																																																											};
																																																																																																																																																																																										}
																																																																																																																																																																																										return new ChangeThemeView
																																																																																																																																																																																										{
																																																																																																																																																																																											DataContext = dataContext184
																																																																																																																																																																																										};
																																																																																																																																																																																									}
																																																																																																																																																																																									return new ChangePasswordView
																																																																																																																																																																																									{
																																																																																																																																																																																										DataContext = dataContext183
																																																																																																																																																																																									};
																																																																																																																																																																																								}
																																																																																																																																																																																								return new BlockedUserView
																																																																																																																																																																																								{
																																																																																																																																																																																									DataContext = dataContext182
																																																																																																																																																																																								};
																																																																																																																																																																																							}
																																																																																																																																																																																							return new BlockedUsersView
																																																																																																																																																																																							{
																																																																																																																																																																																								DataContext = dataContext181
																																																																																																																																																																																							};
																																																																																																																																																																																						}
																																																																																																																																																																																						return new AudioVideoView
																																																																																																																																																																																						{
																																																																																																																																																																																							DataContext = dataContext180
																																																																																																																																																																																						};
																																																																																																																																																																																					}
																																																																																																																																																																																					return new AdvancedSettingsView
																																																																																																																																																																																					{
																																																																																																																																																																																						DataContext = dataContext179
																																																																																																																																																																																					};
																																																																																																																																																																																				}
																																																																																																																																																																																				return new ProfileView
																																																																																																																																																																																				{
																																																																																																																																																																																					DataContext = dataContext178
																																																																																																																																																																																				};
																																																																																																																																																																																			}
																																																																																																																																																																																			return new BlockUserConfirmationView
																																																																																																																																																																																			{
																																																																																																																																																																																				DataContext = dataContext177
																																																																																																																																																																																			};
																																																																																																																																																																																		}
																																																																																																																																																																																		return new MentionNotificationView
																																																																																																																																																																																		{
																																																																																																																																																																																			DataContext = dataContext176
																																																																																																																																																																																		};
																																																																																																																																																																																	}
																																																																																																																																																																																	return new LocalMessageNotificationView
																																																																																																																																																																																	{
																																																																																																																																																																																		DataContext = dataContext175
																																																																																																																																																																																	};
																																																																																																																																																																																}
																																																																																																																																																																																return new FriendResponseNotificationView
																																																																																																																																																																																{
																																																																																																																																																																																	DataContext = dataContext174
																																																																																																																																																																																};
																																																																																																																																																																															}
																																																																																																																																																																															return new FriendRequestNotificationView
																																																																																																																																																																															{
																																																																																																																																																																																DataContext = dataContext173
																																																																																																																																																																															};
																																																																																																																																																																														}
																																																																																																																																																																														return new CommunityKickedNotificationView
																																																																																																																																																																														{
																																																																																																																																																																															DataContext = dataContext172
																																																																																																																																																																														};
																																																																																																																																																																													}
																																																																																																																																																																													return new CommunityMemberInviteNotificationView
																																																																																																																																																																													{
																																																																																																																																																																														DataContext = dataContext171
																																																																																																																																																																													};
																																																																																																																																																																												}
																																																																																																																																																																												return new CommunityMemberBannedNotificationView
																																																																																																																																																																												{
																																																																																																																																																																													DataContext = dataContext170
																																																																																																																																																																												};
																																																																																																																																																																											}
																																																																																																																																																																											return new NotificationsView
																																																																																																																																																																											{
																																																																																																																																																																												DataContext = dataContext169
																																																																																																																																																																											};
																																																																																																																																																																										}
																																																																																																																																																																										return new DeleteNotificationsView
																																																																																																																																																																										{
																																																																																																																																																																											DataContext = dataContext168
																																																																																																																																																																										};
																																																																																																																																																																									}
																																																																																																																																																																									return new RemoveFriendView
																																																																																																																																																																									{
																																																																																																																																																																										DataContext = dataContext167
																																																																																																																																																																									};
																																																																																																																																																																								}
																																																																																																																																																																								return new FriendView
																																																																																																																																																																								{
																																																																																																																																																																									DataContext = dataContext166
																																																																																																																																																																								};
																																																																																																																																																																							}
																																																																																																																																																																							return new FriendsView
																																																																																																																																																																							{
																																																																																																																																																																								DataContext = dataContext165
																																																																																																																																																																							};
																																																																																																																																																																						}
																																																																																																																																																																						return new FriendGroupView
																																																																																																																																																																						{
																																																																																																																																																																							DataContext = dataContext164
																																																																																																																																																																						};
																																																																																																																																																																					}
																																																																																																																																																																					return new DeleteFriendGroupView
																																																																																																																																																																					{
																																																																																																																																																																						DataContext = dataContext163
																																																																																																																																																																					};
																																																																																																																																																																				}
																																																																																																																																																																				return new AddFriendView
																																																																																																																																																																				{
																																																																																																																																																																					DataContext = dataContext162
																																																																																																																																																																				};
																																																																																																																																																																			}
																																																																																																																																																																			return new AddFriendSearchResultView
																																																																																																																																																																			{
																																																																																																																																																																				DataContext = dataContext161
																																																																																																																																																																			};
																																																																																																																																																																		}
																																																																																																																																																																		return new MemberBadgeView
																																																																																																																																																																		{
																																																																																																																																																																			DataContext = dataContext160
																																																																																																																																																																		};
																																																																																																																																																																	}
																																																																																																																																																																	return new LeaveDirectMessageView
																																																																																																																																																																	{
																																																																																																																																																																		DataContext = dataContext159
																																																																																																																																																																	};
																																																																																																																																																																}
																																																																																																																																																																return new FriendTagView
																																																																																																																																																																{
																																																																																																																																																																	DataContext = dataContext158
																																																																																																																																																																};
																																																																																																																																																															}
																																																																																																																																																															return new DirectMessageView
																																																																																																																																																															{
																																																																																																																																																																DataContext = dataContext157
																																																																																																																																																															};
																																																																																																																																																														}
																																																																																																																																																														return new DirectMessagesView
																																																																																																																																																														{
																																																																																																																																																															DataContext = dataContext156
																																																																																																																																																														};
																																																																																																																																																													}
																																																																																																																																																													return new DirectMessageMemberView
																																																																																																																																																													{
																																																																																																																																																														DataContext = dataContext155
																																																																																																																																																													};
																																																																																																																																																												}
																																																																																																																																																												return new DirectMessageFriendView
																																																																																																																																																												{
																																																																																																																																																													DataContext = dataContext154
																																																																																																																																																												};
																																																																																																																																																											}
																																																																																																																																																											return new DirectMessageFriendPickerView
																																																																																																																																																											{
																																																																																																																																																												DataContext = dataContext153
																																																																																																																																																											};
																																																																																																																																																										}
																																																																																																																																																										return new DirectMessageDetailsView
																																																																																																																																																										{
																																																																																																																																																											DataContext = dataContext152
																																																																																																																																																										};
																																																																																																																																																									}
																																																																																																																																																									return new DirectMessageContentView
																																																																																																																																																									{
																																																																																																																																																										DataContext = dataContext151
																																																																																																																																																									};
																																																																																																																																																								}
																																																																																																																																																								return new DirectMessageCallContentView
																																																																																																																																																								{
																																																																																																																																																									DataContext = dataContext150
																																																																																																																																																								};
																																																																																																																																																							}
																																																																																																																																																							return new CreateDirectMessageView
																																																																																																																																																							{
																																																																																																																																																								DataContext = dataContext149
																																																																																																																																																							};
																																																																																																																																																						}
																																																																																																																																																						return new AddDirectMessageMemberView
																																																																																																																																																						{
																																																																																																																																																							DataContext = dataContext148
																																																																																																																																																						};
																																																																																																																																																					}
																																																																																																																																																					return new PrivacyBlockedActionView
																																																																																																																																																					{
																																																																																																																																																						DataContext = dataContext147
																																																																																																																																																					};
																																																																																																																																																				}
																																																																																																																																																				return new NewTabView
																																																																																																																																																				{
																																																																																																																																																					DataContext = dataContext146
																																																																																																																																																				};
																																																																																																																																																			}
																																																																																																																																																			return new KeyboardShortcutsView
																																																																																																																																																			{
																																																																																																																																																				DataContext = dataContext145
																																																																																																																																																			};
																																																																																																																																																		}
																																																																																																																																																		return new HomeView
																																																																																																																																																		{
																																																																																																																																																			DataContext = dataContext144
																																																																																																																																																		};
																																																																																																																																																	}
																																																																																																																																																	return new EnterVerificationCodeView
																																																																																																																																																	{
																																																																																																																																																		DataContext = dataContext143
																																																																																																																																																	};
																																																																																																																																																}
																																																																																																																																																return new VerificationCodeCooldownView
																																																																																																																																																{
																																																																																																																																																	DataContext = dataContext142
																																																																																																																																																};
																																																																																																																																															}
																																																																																																																																															return new DirectMessageTabView
																																																																																																																																															{
																																																																																																																																																DataContext = dataContext141
																																																																																																																																															};
																																																																																																																																														}
																																																																																																																																														return new CreateCommunityView
																																																																																																																																														{
																																																																																																																																															DataContext = dataContext140
																																																																																																																																														};
																																																																																																																																													}
																																																																																																																																													return new CommunityTabView
																																																																																																																																													{
																																																																																																																																														DataContext = dataContext139
																																																																																																																																													};
																																																																																																																																												}
																																																																																																																																												return new RoleView
																																																																																																																																												{
																																																																																																																																													DataContext = dataContext138
																																																																																																																																												};
																																																																																																																																											}
																																																																																																																																											return new RolesView
																																																																																																																																											{
																																																																																																																																												DataContext = dataContext137
																																																																																																																																											};
																																																																																																																																										}
																																																																																																																																										return new RoleMemberCheckBoxView
																																																																																																																																										{
																																																																																																																																											DataContext = dataContext136
																																																																																																																																										};
																																																																																																																																									}
																																																																																																																																									return new EditRolePermissionsTabView
																																																																																																																																									{
																																																																																																																																										DataContext = dataContext135
																																																																																																																																									};
																																																																																																																																								}
																																																																																																																																								return new EditRoleMembersTabView
																																																																																																																																								{
																																																																																																																																									DataContext = dataContext134
																																																																																																																																								};
																																																																																																																																							}
																																																																																																																																							return new EditRoleDetailsTabView
																																																																																																																																							{
																																																																																																																																								DataContext = dataContext133
																																																																																																																																							};
																																																																																																																																						}
																																																																																																																																						return new EditRoleContainerView
																																																																																																																																						{
																																																																																																																																							DataContext = dataContext132
																																																																																																																																						};
																																																																																																																																					}
																																																																																																																																					return new EditRoleChannelAssignmentsTabView
																																																																																																																																					{
																																																																																																																																						DataContext = dataContext131
																																																																																																																																					};
																																																																																																																																				}
																																																																																																																																				return new DiscardRoleChangesView
																																																																																																																																				{
																																																																																																																																					DataContext = dataContext130
																																																																																																																																				};
																																																																																																																																			}
																																																																																																																																			return new DeleteRoleView
																																																																																																																																			{
																																																																																																																																				DataContext = dataContext129
																																																																																																																																			};
																																																																																																																																		}
																																																																																																																																		return new ChannelRoleAssignmentView
																																																																																																																																		{
																																																																																																																																			DataContext = dataContext128
																																																																																																																																		};
																																																																																																																																	}
																																																																																																																																	return new ChannelGroupRoleAssignmentView
																																																																																																																																	{
																																																																																																																																		DataContext = dataContext127
																																																																																																																																	};
																																																																																																																																}
																																																																																																																																return new ProtectionSettingsView
																																																																																																																																{
																																																																																																																																	DataContext = dataContext126
																																																																																																																																};
																																																																																																																															}
																																																																																																																															return new OverviewView
																																																																																																																															{
																																																																																																																																DataContext = dataContext125
																																																																																																																															};
																																																																																																																														}
																																																																																																																														return new MembersDetailsView
																																																																																																																														{
																																																																																																																															DataContext = dataContext124
																																																																																																																														};
																																																																																																																													}
																																																																																																																													return new RootApp.Client.Avalonia.UI.Community.Settings.MemberInviteView
																																																																																																																													{
																																																																																																																														DataContext = dataContext123
																																																																																																																													};
																																																																																																																												}
																																																																																																																												return new MemberInvitesView
																																																																																																																												{
																																																																																																																													DataContext = dataContext122
																																																																																																																												};
																																																																																																																											}
																																																																																																																											return new MemberDetailsView
																																																																																																																											{
																																																																																																																												DataContext = dataContext121
																																																																																																																											};
																																																																																																																										}
																																																																																																																										return new DiscardChangesView
																																																																																																																										{
																																																																																																																											DataContext = dataContext120
																																																																																																																										};
																																																																																																																									}
																																																																																																																									return new DeleteCommunityLinkView
																																																																																																																									{
																																																																																																																										DataContext = dataContext119
																																																																																																																									};
																																																																																																																								}
																																																																																																																								return new CommunitySettingsView
																																																																																																																								{
																																																																																																																									DataContext = dataContext118
																																																																																																																								};
																																																																																																																							}
																																																																																																																							return new CommunityLogView
																																																																																																																							{
																																																																																																																								DataContext = dataContext117
																																																																																																																							};
																																																																																																																						}
																																																																																																																						return new CommunityLogsView
																																																																																																																						{
																																																																																																																							DataContext = dataContext116
																																																																																																																						};
																																																																																																																					}
																																																																																																																					return new CommunityLogDetailsView
																																																																																																																					{
																																																																																																																						DataContext = dataContext115
																																																																																																																					};
																																																																																																																				}
																																																																																																																				return new CommunityLogDateBreakView
																																																																																																																				{
																																																																																																																					DataContext = dataContext114
																																																																																																																				};
																																																																																																																			}
																																																																																																																			return new ComboBoxChannelItemView
																																																																																																																			{
																																																																																																																				DataContext = dataContext113
																																																																																																																			};
																																																																																																																		}
																																																																																																																		return new BannedMemberView
																																																																																																																		{
																																																																																																																			DataContext = dataContext112
																																																																																																																		};
																																																																																																																	}
																																																																																																																	return new BannedMembersView
																																																																																																																	{
																																																																																																																		DataContext = dataContext111
																																																																																																																	};
																																																																																																																}
																																																																																																																return new BannedMemberDetailsView
																																																																																																																{
																																																																																																																	DataContext = dataContext110
																																																																																																																};
																																																																																																															}
																																																																																																															return new UpdateAppView
																																																																																																															{
																																																																																																																DataContext = dataContext109
																																																																																																															};
																																																																																																														}
																																																																																																														return new ChannelGroupAppAssignmentView
																																																																																																														{
																																																																																																															DataContext = dataContext108
																																																																																																														};
																																																																																																													}
																																																																																																													return new ChannelAppAssignmentView
																																																																																																													{
																																																																																																														DataContext = dataContext107
																																																																																																													};
																																																																																																												}
																																																																																																												return new RemoveAppView
																																																																																																												{
																																																																																																													DataContext = dataContext106
																																																																																																												};
																																																																																																											}
																																																																																																											return new AppsBotsView
																																																																																																											{
																																																																																																												DataContext = dataContext105
																																																																																																											};
																																																																																																										}
																																																																																																										return new AppBotView
																																																																																																										{
																																																																																																											DataContext = dataContext104
																																																																																																										};
																																																																																																									}
																																																																																																									return new AppBotGlobalSettingsView
																																																																																																									{
																																																																																																										DataContext = dataContext103
																																																																																																									};
																																																																																																								}
																																																																																																								return new AppBotGeneralView
																																																																																																								{
																																																																																																									DataContext = dataContext102
																																																																																																								};
																																																																																																							}
																																																																																																							return new AppBotDetailsView
																																																																																																							{
																																																																																																								DataContext = dataContext101
																																																																																																							};
																																																																																																						}
																																																																																																						return new AppBotChannelAssignmentsTabView
																																																																																																						{
																																																																																																							DataContext = dataContext100
																																																																																																						};
																																																																																																					}
																																																																																																					return new RoleTagView
																																																																																																					{
																																																																																																						DataContext = dataContext99
																																																																																																					};
																																																																																																				}
																																																																																																				return new RoleCheckBoxView
																																																																																																				{
																																																																																																					DataContext = dataContext98
																																																																																																				};
																																																																																																			}
																																																																																																			return new RolePickerView
																																																																																																			{
																																																																																																				DataContext = dataContext97
																																																																																																			};
																																																																																																		}
																																																																																																		return new RoleOrMemberPickerView
																																																																																																		{
																																																																																																			DataContext = dataContext96
																																																																																																		};
																																																																																																	}
																																																																																																	return new AddRoleButtonView
																																																																																																	{
																																																																																																		DataContext = dataContext95
																																																																																																	};
																																																																																																}
																																																																																																return new SearchView
																																																																																																{
																																																																																																	DataContext = dataContext94
																																																																																																};
																																																																																															}
																																																																																															return new SearchResultMessageView
																																																																																															{
																																																																																																DataContext = dataContext93
																																																																																															};
																																																																																														}
																																																																																														return new SearchResultMemberView
																																																																																														{
																																																																																															DataContext = dataContext92
																																																																																														};
																																																																																													}
																																																																																													return new SearchResultDirectoryFileView
																																																																																													{
																																																																																														DataContext = dataContext91
																																																																																													};
																																																																																												}
																																																																																												return new PinnedMessageView
																																																																																												{
																																																																																													DataContext = dataContext90
																																																																																												};
																																																																																											}
																																																																																											return new PinnedMessagesView
																																																																																											{
																																																																																												DataContext = dataContext89
																																																																																											};
																																																																																										}
																																																																																										return new DirectoryView
																																																																																										{
																																																																																											DataContext = dataContext88
																																																																																										};
																																																																																									}
																																																																																									return new DirectoryNavigationItemView
																																																																																									{
																																																																																										DataContext = dataContext87
																																																																																									};
																																																																																								}
																																																																																								return new DirectoryFileView
																																																																																								{
																																																																																									DataContext = dataContext86
																																																																																								};
																																																																																							}
																																																																																							return new DirectoryFileUploadView
																																																																																							{
																																																																																								DataContext = dataContext85
																																																																																							};
																																																																																						}
																																																																																						return new DirectoryFileUploadStatusView
																																																																																						{
																																																																																							DataContext = dataContext84
																																																																																						};
																																																																																					}
																																																																																					return new DirectoryFileUploadItemView
																																																																																					{
																																																																																						DataContext = dataContext83
																																																																																					};
																																																																																				}
																																																																																				return new DirectoriesView
																																																																																				{
																																																																																					DataContext = dataContext82
																																																																																				};
																																																																																			}
																																																																																			return new TransferOwnershipView
																																																																																			{
																																																																																				DataContext = dataContext81
																																																																																			};
																																																																																		}
																																																																																		return new MemberView
																																																																																		{
																																																																																			DataContext = dataContext80
																																																																																		};
																																																																																	}
																																																																																	return new MembersView
																																																																																	{
																																																																																		DataContext = dataContext79
																																																																																	};
																																																																																}
																																																																																return new MemberProfileView
																																																																																{
																																																																																	DataContext = dataContext78
																																																																																};
																																																																															}
																																																																															return new MemberPickerView
																																																																															{
																																																																																DataContext = dataContext77
																																																																															};
																																																																														}
																																																																														return new RootApp.Client.Avalonia.UI.Community.Members.MemberInviteView
																																																																														{
																																																																															DataContext = dataContext76
																																																																														};
																																																																													}
																																																																													return new MemberGroupView
																																																																													{
																																																																														DataContext = dataContext75
																																																																													};
																																																																												}
																																																																												return new MemberCheckBoxView
																																																																												{
																																																																													DataContext = dataContext74
																																																																												};
																																																																											}
																																																																											return new LeaveCommunityView
																																																																											{
																																																																												DataContext = dataContext73
																																																																											};
																																																																										}
																																																																										return new KickMemberView
																																																																										{
																																																																											DataContext = dataContext72
																																																																										};
																																																																									}
																																																																									return new InviteMembersView
																																																																									{
																																																																										DataContext = dataContext71
																																																																									};
																																																																								}
																																																																								return new InviteMembersLinkSettingsView
																																																																								{
																																																																									DataContext = dataContext70
																																																																								};
																																																																							}
																																																																							return new DeleteCommunityView
																																																																							{
																																																																								DataContext = dataContext69
																																																																							};
																																																																						}
																																																																						return new BanMemberView
																																																																						{
																																																																							DataContext = dataContext68
																																																																						};
																																																																					}
																																																																					return new CommunityUnauthenticatedView
																																																																					{
																																																																						DataContext = dataContext67
																																																																					};
																																																																				}
																																																																				return new CommunityLinkJoinView
																																																																				{
																																																																					DataContext = dataContext66
																																																																				};
																																																																			}
																																																																			return new CommunityJoinFailedView
																																																																			{
																																																																				DataContext = dataContext65
																																																																			};
																																																																		}
																																																																		return new VoiceChannelContentView
																																																																		{
																																																																			DataContext = dataContext64
																																																																		};
																																																																	}
																																																																	return new TextChannelFileUploadView
																																																																	{
																																																																		DataContext = dataContext63
																																																																	};
																																																																}
																																																																return new TextChannelContentView
																																																																{
																																																																	DataContext = dataContext62
																																																																};
																																																															}
																																																															return new AppChannelContentView
																																																															{
																																																																DataContext = dataContext61
																																																															};
																																																														}
																																																														return new CommunityView
																																																														{
																																																															DataContext = dataContext60
																																																														};
																																																													}
																																																													return new SwitchVoiceCallConfirmationView
																																																													{
																																																														DataContext = dataContext59
																																																													};
																																																												}
																																																												return new ChannelPickerView
																																																												{
																																																													DataContext = dataContext58
																																																												};
																																																											}
																																																											return new ChannelCheckBoxView
																																																											{
																																																												DataContext = dataContext57
																																																											};
																																																										}
																																																										return new RoleAccessRuleTagView
																																																										{
																																																											DataContext = dataContext56
																																																										};
																																																									}
																																																									return new MemberAccessRuleTagView
																																																									{
																																																										DataContext = dataContext55
																																																									};
																																																								}
																																																								return new AppAccessRuleTagView
																																																								{
																																																									DataContext = dataContext54
																																																								};
																																																							}
																																																							return new AccessRuleView
																																																							{
																																																								DataContext = dataContext53
																																																							};
																																																						}
																																																						return new AccessRulesView
																																																						{
																																																							DataContext = dataContext52
																																																						};
																																																					}
																																																					return new AccessRuleSelectorView
																																																					{
																																																						DataContext = dataContext51
																																																					};
																																																				}
																																																				return new EditChannelView
																																																				{
																																																					DataContext = dataContext50
																																																				};
																																																			}
																																																			return new EditChannelGroupView
																																																			{
																																																				DataContext = dataContext49
																																																			};
																																																		}
																																																		return new DeleteChannelView
																																																		{
																																																			DataContext = dataContext48
																																																		};
																																																	}
																																																	return new DeleteChannelGroupView
																																																	{
																																																		DataContext = dataContext47
																																																	};
																																																}
																																																return new CreateChannelView
																																																{
																																																	DataContext = dataContext46
																																																};
																																															}
																																															return new CreateChannelGroupView
																																															{
																																																DataContext = dataContext45
																																															};
																																														}
																																														return new ChannelView
																																														{
																																															DataContext = dataContext44
																																														};
																																													}
																																													return new ChannelsView
																																													{
																																														DataContext = dataContext43
																																													};
																																												}
																																												return new ChannelMediaMemberView
																																												{
																																													DataContext = dataContext42
																																												};
																																											}
																																											return new ChannelGroupView
																																											{
																																												DataContext = dataContext41
																																											};
																																										}
																																										return new ChannelGroupContainsAppView
																																										{
																																											DataContext = dataContext40
																																										};
																																									}
																																									return new ItemTextView
																																									{
																																										DataContext = dataContext39
																																									};
																																								}
																																								return new ItemSelectView
																																								{
																																									DataContext = dataContext38
																																								};
																																							}
																																							return new ItemRoleOrMemberView
																																							{
																																								DataContext = dataContext37
																																							};
																																						}
																																						return new GlobalSettingsSelectorView
																																						{
																																							DataContext = dataContext36
																																						};
																																					}
																																					return new GlobalSettingsItemView
																																					{
																																						DataContext = dataContext35
																																					};
																																				}
																																				return new GlobalSettingsGroupView
																																				{
																																					DataContext = dataContext34
																																				};
																																			}
																																			return new AppFileUploadItemView
																																			{
																																				DataContext = dataContext33
																																			};
																																		}
																																		return new ScreenshotPreviewView
																																		{
																																			DataContext = dataContext32
																																		};
																																	}
																																	return new AppView
																																	{
																																		DataContext = dataContext31
																																	};
																																}
																																return new AppStoreView
																																{
																																	DataContext = dataContext30
																																};
																															}
																															return new AppDetailsView
																															{
																																DataContext = dataContext29
																															};
																														}
																														return new AppAlreadyInCommunityView
																														{
																															DataContext = dataContext28
																														};
																													}
																													return new WindowsTitleBarView
																													{
																														DataContext = dataContext27
																													};
																												}
																												return new MacosTitleBarView
																												{
																													DataContext = macosTitleBarViewModel
																												};
																											}
																											return new SupportView
																											{
																												DataContext = dataContext26
																											};
																										}
																										return new MenuItemPageContainerView
																										{
																											DataContext = dataContext25
																										};
																									}
																									return new ReorderablePlaceholderView
																									{
																										DataContext = dataContext24
																									};
																								}
																								return new RootMessageTextboxView
																								{
																									DataContext = dataContext23
																								};
																							}
																							return new ReplyUserTagView
																							{
																								DataContext = dataContext22
																							};
																						}
																						return new UserMentionAutoCompleteItemView
																						{
																							DataContext = dataContext21
																						};
																					}
																					return new RoleMentionAutoCompleteItemView
																					{
																						DataContext = dataContext20
																					};
																				}
																				return new GlobalMentionAutoCompleteItemView
																				{
																					DataContext = dataContext19
																				};
																			}
																			return new EmojiAutoCompleteItemView
																			{
																				DataContext = dataContext18
																			};
																		}
																		return new DividerAutoCompleteItemView
																		{
																			DataContext = dataContext17
																		};
																	}
																	return new ChannelMentionAutoCompleteItemView
																	{
																		DataContext = dataContext16
																	};
																}
																return new FileAttachmentView
																{
																	DataContext = dataContext15
																};
															}
															return new ResetAllKeybindingsConfirmationView
															{
																DataContext = dataContext14
															};
														}
														return new KeybindEditorView
														{
															DataContext = dataContext13
														};
													}
													return new KeybindConflictConfirmationView
													{
														DataContext = dataContext12
													};
												}
												return new GlobalHookKeybindView
												{
													DataContext = dataContext11
												};
											}
											return new ImageUploaderView
											{
												DataContext = dataContext10
											};
										}
										return new FileTooLargeConfirmationView
										{
											DataContext = dataContext9
										};
									}
									return new GifPickerView
									{
										DataContext = dataContext8
									};
								}
								return new GifMediaView
								{
									DataContext = dataContext7
								};
							}
							return new GifCategoryView
							{
								DataContext = dataContext6
							};
						}
						return new EmojiView
						{
							DataContext = dataContext5
						};
					}
					return new EmojiRowView
					{
						DataContext = dataContext4
					};
				}
				return new EmojiPickerView
				{
					DataContext = dataContext3
				};
			}
			return new EmojiGroupView
			{
				DataContext = dataContext2
			};
		}
		return new EmojiCategoryView
		{
			DataContext = dataContext
		};
	}
}

