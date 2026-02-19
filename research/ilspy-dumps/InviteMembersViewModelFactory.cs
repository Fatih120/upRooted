// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.InviteMembersViewModelFactory
using System;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;

public class InviteMembersViewModelFactory(IRootSessionAccessor P_0, MemberInviteViewModelFactory P_1, InviteMembersLinkSettingsViewModelFactory P_2, ClipboardService P_3, IStreamerModeService P_4)
{
	public InviteMembersViewModel Create(Community P_0, Action? P_1 = null)
	{
		return new InviteMembersViewModel(P_0, P_0, P_1, P_2, P_3, P_4, P_1);
	}
}

