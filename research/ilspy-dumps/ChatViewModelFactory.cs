// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.ChatViewModelFactory
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.Domain.Helpers.Store;

public class ChatViewModelFactory(ILocalDataStore P_0, IRootSessionAccessor P_1)
{
	public ChatViewModel Create()
	{
		return new ChatViewModel(P_0, P_1);
	}
}

