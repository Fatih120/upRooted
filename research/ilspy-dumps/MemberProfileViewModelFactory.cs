// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberProfileViewModelFactory
using System;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Roles;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Messages;

public class MemberProfileViewModelFactory(RoleTagViewModelFactory P_0, BitmapCache P_1, DirectMessageOpenerService P_2, AddRoleButtonViewModelFactory P_3, IRootSessionAccessor P_4, UserContextMenuViewModelFactory P_5, PrivacyBlockedActionViewModelFactory P_6, IStreamerModeService P_7, IDeveloperModeService P_8)
{
	public MemberProfileViewModel Create(IMessageContainerMember P_0, Action P_1)
	{
		return new MemberProfileViewModel(P_0, P_1, P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8);
	}
}

