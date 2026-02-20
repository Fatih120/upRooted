// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.GameOverlaySettingsViewModelFactory
using RootApp.Client.Avalonia.Helpers.Overlay;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.Domain.Helpers.Store;

public class GameOverlaySettingsViewModelFactory(ILocalDataStore P_0, OverlayService P_1)
{
	public GameOverlaySettingsViewModel Create()
	{
		return new GameOverlaySettingsViewModel(P_0, P_1);
	}
}

