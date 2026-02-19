// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.BanMemberViewModelFactory
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;

public class BanMemberViewModelFactory(RootMarkdownEngineManager P_0)
{
	public BanMemberViewModel Create(Member P_0, Community P_1)
	{
		return new BanMemberViewModel(P_0, P_1, P_0);
	}
}

