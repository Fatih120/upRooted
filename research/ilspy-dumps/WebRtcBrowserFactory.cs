using DotNetBrowser.Browser;
using RootApp.Browser.WebRtc;
using RootApp.Browser.WebRtc.Services;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Core.Identifiers;

namespace RootApp.Browser;

public class WebRtcBrowserFactory(WebRtcServiceFactory, WebRtcBundleExtractor, NoiseSuppressionBundleExtractor)
{
	public WebRtcBrowser Create(RootGuid P_0, MediaRoom P_1, IBrowser P_2, string P_3, BrowserService P_4)
	{
		return new WebRtcBrowser(P_0, P_1, P_2, P_3, P_4, P_0, P_1, P_2);
	}
}
