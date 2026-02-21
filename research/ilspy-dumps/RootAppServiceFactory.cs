using DotNetBrowser.Js;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Helpers.RootApps.Connection;
using RootApp.Client.Avalonia.Helpers.RootApps.Files;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Browser.RootApps.Services;

public class RootAppServiceFactory(ILoggerFactory, IRootSessionAccessor, AppConnectionManager, AppAssetFileUploaderFactory, AppImagePreviewCache)
{
	public RootAppService Create(Community P_0, IJsObject P_1, RootAppBrowser P_2)
	{
		return new RootAppService(P_0, P_1, P_2, P_0, P_1, P_2, P_3, P_4);
	}
}
