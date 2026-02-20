// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.AdvancedSettingsViewModelFactory
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;

public class AdvancedSettingsViewModelFactory(IDeveloperModeService P_0, IRootSessionAccessor P_1, ClipboardService P_2)
{
	public AdvancedSettingsViewModel Create()
	{
		return new AdvancedSettingsViewModel(P_0, P_1, P_2);
	}
}

