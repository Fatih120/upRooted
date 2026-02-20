// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.ProfileViewModelFactory
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Controls.Support;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Installation;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Services;

public class ProfileViewModelFactory(IRootSessionAccessor P_0, BitmapCache P_1, ILoggerFactory P_2, ProfileSettingsViewModelFactory P_3, SignOutConfirmationViewModelFactory P_4, SupportViewModelFactory P_5, IRootService P_6, RootInstallationManager P_7)
{
	public ProfileViewModel Create()
	{
		return new ProfileViewModel(P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7);
	}
}

