using DotNetBrowser.Js;

namespace RootApp.Browser.WebRtc.Services;

public class SoundPoolServiceFactory
{
	public SoundPoolService Create(IJsObject P_0)
	{
		return new SoundPoolService(P_0);
	}
}
