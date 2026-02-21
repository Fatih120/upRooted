using System.Collections.Generic;
using DotNetBrowser.Handlers;
using DotNetBrowser.Net.Handlers;
using RootApp.Browser.Handlers.Specialized;

namespace RootApp.Browser.Handlers;

public class CompositeInterceptHandler : IHandler<InterceptRequestParameters, InterceptRequestResponse>
{
	private readonly List<IUrlSchemeHandler> _handlers;

	public CompositeInterceptHandler(SoundEffectInterceptHandler P_0, ImageCacheInterceptHandler P_1, RootAppDynamicAppInterceptHandler P_2, RootAppHostInterceptHandler P_3, RootAppClientInterceptHandler P_4, WebRtcSuppressionInterceptHandler P_5)
	{
		_handlers = new List<IUrlSchemeHandler> { P_0, P_1, P_2, P_3, P_4, P_5 };
	}

	public InterceptRequestResponse Handle(InterceptRequestParameters P_0)
	{
		foreach (IUrlSchemeHandler handler in _handlers)
		{
			if (handler.CanHandle(P_0.UrlRequest.Url))
			{
				return handler.Handle(P_0);
			}
		}
		return InterceptRequestResponse.Proceed();
	}
}
