using System;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.LinkJoining;
using RootApp.Client.CoreDomain;

namespace RootApp.Client.Avalonia.UI.NewTab;

public class VerifiedCommunitiesViewModelFactory(LinkJoiningService, IRootSessionAccessor, BitmapCache)
{
	public VerifiedCommunitiesViewModel Create(Action? P_0 = null)
	{
		return new VerifiedCommunitiesViewModel(P_0, P_1, P_2, P_0);
	}
}
