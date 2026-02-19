// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.CommunityTabViewModelFactory
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.Helpers.Popout;
using RootApp.Client.Avalonia.UI.Community;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Settings;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Tabs;

public class CommunityTabViewModelFactory(IRootSessionAccessor P_0, CommunityViewModelFactory P_1, CommunitySettingsViewModelFactory P_2, InviteMembersViewModelFactory P_3, DeleteCommunityViewModelFactory P_4, LeaveCommunityViewModelFactory P_5, VoiceCallMemberAvatarViewModelFactory P_6, ITabPopoutService P_7, ILoggerFactory P_8, BitmapCache P_9, IDeveloperModeService P_10, ClipboardService P_11)
{
	public CommunityTabViewModel Create(Tab P_0)
	{
		return new CommunityTabViewModel(P_0, P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8, P_9, P_10, P_11);
	}
}

