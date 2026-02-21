using System;
using DotNetBrowser.Net.Handlers;
using RootApp.Browser.Handlers.Utilities;
using RootApp.Browser.WebRtc;

namespace RootApp.Browser.Handlers.Specialized;

public class WebRtcSuppressionInterceptHandler : IUrlSchemeHandler
{
	private readonly NoiseSuppressionBundleExtractor _noiseSuppressionBundleExtractor;

	public WebRtcSuppressionInterceptHandler(NoiseSuppressionBundleExtractor P_0)
	{
		_noiseSuppressionBundleExtractor = P_0;
	}

	public bool CanHandle(string P_0)
	{
		return P_0.StartsWith("rootapp://webrtc/suppresion/", StringComparison.OrdinalIgnoreCase);
	}

	public InterceptRequestResponse Handle(InterceptRequestParameters P_0)
	{
		string text = P_0.UrlRequest.Url.Substring("rootapp://webrtc/suppresion/".Length);
		if (text.StartsWith("/") || text.StartsWith("\\"))
		{
			text = text.Substring(1);
		}
		if (_noiseSuppressionBundleExtractor.CachedTempFolder != null)
		{
			return FileLoadingHelper.LoadFile(P_0, _noiseSuppressionBundleExtractor.CachedTempFolder, text, true);
		}
		return InterceptRequestResponse.Proceed();
	}
}
