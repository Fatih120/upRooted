// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.DirectMessageTabViewModelFactory
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Popout;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Tabs;

public class DirectMessageTabViewModelFactory(IRootSessionAccessor P_0, DirectMessageContentViewModelFactory P_1, DirectMessageViewModelFactory P_2, LeaveDirectMessageViewModelFactory P_3, ITabPopoutService P_4, ILoggerFactory P_5, BitmapCache P_6)
{
	public DirectMessageTabViewModel Create(Tab P_0)
	{
		return new DirectMessageTabViewModel(P_0, P_0, P_1, P_2, P_3, P_4, P_5, P_6);
	}
}

