// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.SignOutConfirmationViewModelFactory
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.Client.CoreDomain.Services;

public class SignOutConfirmationViewModelFactory(IRootService P_0, RootMarkdownEngineManager P_1)
{
	public SignOutConfirmationViewModel Create()
	{
		return new SignOutConfirmationViewModel(P_0, P_1);
	}
}
