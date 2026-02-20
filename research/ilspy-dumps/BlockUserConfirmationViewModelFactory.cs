// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.BlockUserConfirmationViewModelFactory
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.User;

public class BlockUserConfirmationViewModelFactory(IRootSessionAccessor P_0, RootMarkdownEngineManager P_1)
{
	public BlockUserConfirmationViewModel Create(GlobalUser P_0)
	{
		return new BlockUserConfirmationViewModel(P_0, P_0, P_1);
	}
}

