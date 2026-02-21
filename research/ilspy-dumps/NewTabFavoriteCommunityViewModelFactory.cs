using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Client.Avalonia.UI.NewTab;

public class NewTabFavoriteCommunityViewModelFactory(BitmapCache, IRootSessionAccessor, CommunityOpenerService, LeaveCommunityViewModelFactory)
{
	public NewTabFavoriteCommunityViewModel Create(RootApp.Client.CoreDomain.Models.Community.Community community)
	{
		return new NewTabFavoriteCommunityViewModel(community, P_0, P_1, P_2, P_3);
	}
}
