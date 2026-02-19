// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.InviteMembersLinkSettingsViewModelFactory
using System;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.WebApi.Shared.Grpc.Responses;

public class InviteMembersLinkSettingsViewModelFactory(IRootSessionAccessor P_0)
{
	public InviteMembersLinkSettingsViewModel Create(Community P_0, Action<CommunityInviteLinkCreateResponse> P_1)
	{
		return new InviteMembersLinkSettingsViewModel(P_0, P_1, P_0);
	}
}

