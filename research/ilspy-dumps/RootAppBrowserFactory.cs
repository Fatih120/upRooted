using DotNetBrowser.Browser;
using RootApp.Browser.RootApps.Services;
using RootApp.Client.CoreDomain;
using RootApp.Core.Identifiers;

namespace RootApp.Browser;

public class RootAppBrowserFactory(RootAppServiceFactory, IRootSessionAccessor)
{
	public RootAppBrowser Create(AppGuid P_0, string P_1, CommunityAppGuid P_2, CommunityGuid P_3, IBrowser P_4)
	{
		return new RootAppBrowser(P_0, P_1, P_2, P_3, P_4, P_0, P_1);
	}

	public RootAppBrowser CreateFromWarm(AppGuid P_0, string P_1, CommunityAppGuid P_2, CommunityGuid P_3, WarmBrowser P_4)
	{
		return new RootAppBrowser(P_0, P_1, P_2, P_3, P_4, P_0, P_1);
	}
}
