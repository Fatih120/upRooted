// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.WindowsSettingsViewModelFactory
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.Domain.Helpers.Store;

public class WindowsSettingsViewModelFactory(ILocalDataStore P_0)
{
	public WindowsSettingsViewModel Create()
	{
		return new WindowsSettingsViewModel(P_0);
	}
}
