using RootApp.Client.Avalonia.Helpers.Panes;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;

namespace RootApp.Client.Avalonia.UI.NewTab;

public class NewTabContentViewModelFactory(IRootSessionAccessor, NewTabCommunityViewModelFactory, CreateCommunityViewModelFactory, NewTabFavoriteCommunityViewModelFactory, DiscoverVerifiedCommunitiesViewModelFactory, PaneDisplayService)
{
	public NewTabContentViewModel Create()
	{
		return new NewTabContentViewModel(P_0, P_1, P_2, P_3, P_4, P_5);
	}
}
