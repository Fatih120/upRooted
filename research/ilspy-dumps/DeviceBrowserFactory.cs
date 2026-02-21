using DotNetBrowser.Browser;
using RootApp.Browser.WebRtc;
using RootApp.Browser.WebRtc.Services;
using RootApp.Core.Identifiers;

namespace RootApp.Browser;

public class DeviceBrowserFactory(DeviceServiceFactory, WebRtcBundleExtractor)
{
	public DeviceBrowser Create(RootGuid P_0, IBrowser P_1)
	{
		return new DeviceBrowser(P_0, P_1, P_0, P_1);
	}
}
