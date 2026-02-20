// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.ChangePasswordViewModelFactory
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;

public class ChangePasswordViewModelFactory(IRootSessionAccessor P_0)
{
	public ChangePasswordViewModel Create()
	{
		return new ChangePasswordViewModel(P_0);
	}
}

