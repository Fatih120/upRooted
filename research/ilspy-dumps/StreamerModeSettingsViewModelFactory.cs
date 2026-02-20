// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.StreamerModeSettingsViewModelFactory
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class StreamerModeSettingsViewModelFactory(IStreamerModeService P_0)
{
	public StreamerModeSettingsViewModel Create()
	{
		return new StreamerModeSettingsViewModel(P_0);
	}
}

