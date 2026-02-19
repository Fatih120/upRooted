// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.LeaveCommunityViewModelFactory
using System;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;

public class LeaveCommunityViewModelFactory(RootMarkdownEngineManager P_0, IRootSessionAccessor P_1)
{
	public LeaveCommunityViewModel Create(Community P_0, Action? P_1 = null)
	{
		return new LeaveCommunityViewModel(P_0, P_0, P_1, P_1);
	}
}

