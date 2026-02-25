using RootApp.Client.Avalonia.Controls.Support;
using RootApp.Client.Avalonia.Helpers.Installation;
using RootApp.Client.Avalonia.UI.Main;
using RootApp.Core;
using RootApp.WebApi.Client.Shared;

namespace RootApp.Client.Avalonia.Controls.TitleBars;

public sealed class WindowsTitleBarViewModel : TitleBarViewModelBase<WindowsTitleBarViewModel>
{
	public WindowsTitleBarViewModel(string P_0, RootWebApiConfig P_1, IConnectionStatusService P_2, ConnectionBlockingViewModelFactory P_3, SupportViewModelFactory P_4, RootInstallationManager P_5)
		: base(P_0, P_1, P_2, P_3, P_4, P_5)
	{
	}
}
