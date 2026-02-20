// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.BlockedUsersViewModelFactory
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;

public class BlockedUsersViewModelFactory(BlockedUserViewModelFactory P_0, IRootSessionAccessor P_1)
{
	public BlockedUsersViewModel Create()
	{
		return new BlockedUsersViewModel(P_0, P_1);
	}
}

