// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.KeybindingsViewModelFactory
using RootApp.Client.Avalonia.Controls.Keybinds;
using RootApp.Client.Avalonia.Helpers.Keybindings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class KeybindingsViewModelFactory(KeybindingService P_0, KeybindEditorViewModelFactory P_1, KeybindConflictConfirmationViewModelFactory P_2, ResetAllKeybindingsConfirmationViewModelFactory P_3)
{
	public KeybindingsViewModel Create()
	{
		return new KeybindingsViewModel(P_0, P_1, P_2, P_3);
	}
}

