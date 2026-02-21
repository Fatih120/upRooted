using DotNetBrowser.Js;
using RootApp.Client.Domain.Helpers.Store;

namespace RootApp.Browser.WebRtc.Services;

public class DeviceServiceFactory(ILocalDataStore)
{
	public DeviceService Create(IJsObject P_0, bool P_1 = true)
	{
		return new DeviceService(P_0, P_1, P_0);
	}
}
