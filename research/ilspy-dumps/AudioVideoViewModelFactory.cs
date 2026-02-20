// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.AudioVideoViewModelFactory
using RootApp.Browser;
using RootApp.Client.Avalonia.Controls.Keybinds.Global;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.Volume;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class AudioVideoViewModelFactory(BrowserService P_0, GlobalHookKeybindViewModelFactory P_1, PushToTalkService P_2, UserVolumeService P_3)
{
	public AudioVideoViewModel Create()
	{
		return new AudioVideoViewModel(P_0, P_1, P_2, P_3);
	}
}

