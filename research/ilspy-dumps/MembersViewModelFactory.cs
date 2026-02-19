// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MembersViewModelFactory
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.Domain.Helpers.Store;

public class MembersViewModelFactory(CommunitySettingsViewModelFactory P_0, InviteMembersViewModelFactory P_1, DeleteCommunityViewModelFactory P_2, LeaveCommunityViewModelFactory P_3, MemberViewModelFactory P_4, MemberGroupViewModelFactory P_5, BitmapCache P_6, ILocalDataStore P_7, IRootSessionAccessor P_8, IDeveloperModeService P_9, ClipboardService P_10)
{
	public MembersViewModel Create(Community P_0)
	{
		return new MembersViewModel(P_0, P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8, P_9, P_10);
	}
}

