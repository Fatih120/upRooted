// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.BlockedUserViewModelFactory
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;

public class BlockedUserViewModelFactory(BitmapCache P_0, IGlobalUserCacheService P_1, IRootSessionAccessor P_2)
{
	public BlockedUserViewModel Create(UserGuid userId)
	{
		return new BlockedUserViewModel(userId, P_0, P_1, P_2);
	}
}

