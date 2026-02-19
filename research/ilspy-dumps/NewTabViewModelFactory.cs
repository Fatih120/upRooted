// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.NewTabViewModelFactory
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.NewTab;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Tabs;

public class NewTabViewModelFactory(NewTabContentViewModelFactory P_0, IRootSessionAccessor P_1)
{
	public NewTabViewModel Create(Tab P_0)
	{
		return new NewTabViewModel(P_0, P_0, P_1);
	}
}

