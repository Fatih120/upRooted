// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.DeleteCommunityViewModelFactory
using System;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Core.Identifiers;

public class DeleteCommunityViewModelFactory(IRootSessionAccessor P_0, RootMarkdownEngineManager P_1)
{
	public DeleteCommunityViewModel Create(string P_0, CommunityGuid P_1, Action? P_2 = null)
	{
		return new DeleteCommunityViewModel(P_0, P_1, P_0, P_1, P_2);
	}
}

