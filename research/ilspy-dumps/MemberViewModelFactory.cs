// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberViewModelFactory
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;

public class MemberViewModelFactory(MemberProfileViewModelFactory P_0, UserContextMenuViewModelFactory P_1, BitmapCache P_2, ILoggerFactory P_3)
{
	public MemberViewModel Create(Member P_0, Community P_1, bool P_2)
	{
		return new MemberViewModel(P_0, P_1, P_2, P_0, P_1, P_2, P_3);
	}
}

