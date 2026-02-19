// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Main.MainViewModelFactory
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Controls.TitleBars;
using RootApp.Client.Avalonia.Helpers.Focus;
using RootApp.Client.Avalonia.Helpers.Installation;
using RootApp.Client.Avalonia.Helpers.Windows;
using RootApp.Client.Avalonia.Helpers.Zoom;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Login;
using RootApp.Client.Avalonia.UI.Main;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Services;
using RootApp.Client.CoreDomain.Utils.Badges;

public class MainViewModelFactory(LoginViewModelFactory P_0, HomeViewModelFactory P_1, IRootService P_2, ILoggerFactory P_3, TitleBarViewModelFactory P_4, MemberProfileViewModelFactory P_5, IRootSessionAccessor P_6, FocusService P_7, ActivityTrackerService P_8, RootInstallationManager P_9, IAppBadgeService P_10, ZoomService P_11, IWindowRegistry P_12, OverlayStackService P_13)
{
	public MainViewModel Create()
	{
		return new MainViewModel(P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8, P_9, P_10, P_11, P_12, P_13);
	}
}

