// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.ChangeThemeViewModelFactory
using RootApp.Client.Avalonia.Resources.Themes;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class ChangeThemeViewModelFactory(ThemeService P_0)
{
	public ChangeThemeViewModel Create()
	{
		return new ChangeThemeViewModel(P_0);
	}
}
