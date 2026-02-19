// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.KickMemberViewModelFactory
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;

public class KickMemberViewModelFactory(RootMarkdownEngineManager P_0)
{
	public KickMemberViewModel Create(Member P_0, Community P_1)
	{
		return new KickMemberViewModel(P_0, P_1, P_0);
	}
}

