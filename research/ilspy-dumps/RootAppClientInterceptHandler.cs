using System;
using System.IO;
using DotNetBrowser.Net.Handlers;
using RootApp.Browser.Handlers.Utilities;

namespace RootApp.Browser.Handlers.Specialized;

public class RootAppClientInterceptHandler : IUrlSchemeHandler
{
	public bool CanHandle(string P_0)
	{
		return P_0.StartsWith("rootapp://client/", StringComparison.OrdinalIgnoreCase);
	}

	public InterceptRequestResponse Handle(InterceptRequestParameters P_0)
	{
		string text = P_0.UrlRequest.Url.Substring("rootapp://client/".Length);
		if (text.StartsWith("/") || text.StartsWith("\\"))
		{
			text = text.Substring(1);
		}
		if (text.Length == 0)
		{
			text = "index.html";
		}
		global::_003C_003Ey__InlineArray5<string> _003C_003Ey__InlineArray = default(global::_003C_003Ey__InlineArray5<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 0) = AppDomain.CurrentDomain.BaseDirectory;
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 1) = "DotNetBrowser";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 2) = "RootApps";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 3) = "Bundle";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 4) = "App";
		string text2 = Path.Combine(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<global::_003C_003Ey__InlineArray5<string>, string>(in _003C_003Ey__InlineArray, 5));
		return FileLoadingHelper.LoadFile(P_0, text2, text, true);
	}
}
