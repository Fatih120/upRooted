using System;
using System.IO;
using System.Linq;
using DotNetBrowser.Net.Handlers;
using RootApp.Browser.Handlers.Utilities;
using RootApp.Client.Avalonia.Helpers.RootApps.Core;
using RootApp.Client.Avalonia.Helpers.RootApps.Files;

namespace RootApp.Browser.Handlers.Specialized;

public class RootAppDynamicAppInterceptHandler : IUrlSchemeHandler
{
	private readonly AppManager _appManager;

	private readonly AppImagePreviewCache _previewCache;

	public RootAppDynamicAppInterceptHandler(AppManager P_0, AppImagePreviewCache P_1)
	{
		_appManager = P_0;
		_previewCache = P_1;
	}

	public bool CanHandle(string P_0)
	{
		return P_0.StartsWith("rootapp://previewimage/", StringComparison.OrdinalIgnoreCase) || P_0.StartsWith("rootapp://app", StringComparison.OrdinalIgnoreCase);
	}

	public InterceptRequestResponse Handle(InterceptRequestParameters P_0)
	{
		string url = P_0.UrlRequest.Url;
		if (url.StartsWith("rootapp://previewimage/", StringComparison.OrdinalIgnoreCase))
		{
			string text = url.Split('/').Last();
			if (_previewCache.TryGet(text, out string text2) && File.Exists(text2))
			{
				return FileLoadingHelper.CreateJobFromBytes(MimeTypeProvider.GetMimeType(text2), File.ReadAllBytes(text2), P_0);
			}
			return InterceptRequestResponse.Proceed();
		}
		if (url.StartsWith("rootapp://app/__index.html", StringComparison.OrdinalIgnoreCase))
		{
			global::_003C_003Ey__InlineArray5<string> _003C_003Ey__InlineArray = default(global::_003C_003Ey__InlineArray5<string>);
			global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 0) = AppDomain.CurrentDomain.BaseDirectory;
			global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 1) = "DotNetBrowser";
			global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 2) = "RootApps";
			global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 3) = "Bundle";
			global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray5<string>, string>(ref _003C_003Ey__InlineArray, 4) = "Host";
			string text3 = Path.Combine(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<global::_003C_003Ey__InlineArray5<string>, string>(in _003C_003Ey__InlineArray, 5));
			return FileLoadingHelper.LoadFile(P_0, text3, "index.html", false);
		}
		if (url.StartsWith("rootapp://app", StringComparison.OrdinalIgnoreCase))
		{
			string text4 = url.Substring("rootapp://app/".Length);
			if (text4.StartsWith("/") || text4.StartsWith("\\"))
			{
				text4 = text4.Substring(1);
			}
			if (string.IsNullOrWhiteSpace(text4))
			{
				text4 = "index.html";
			}
			string text5 = null;
			string text6 = null;
			string[] array = P_0.UrlRequest.Browser.UserAgent.Split(' ');
			foreach (string text7 in array)
			{
				if (text7.StartsWith("AppId=", StringComparison.OrdinalIgnoreCase))
				{
					text5 = text7.Substring("AppId=".Length);
				}
				if (text7.StartsWith("AppVersionId=", StringComparison.OrdinalIgnoreCase))
				{
					text6 = text7.Substring("AppVersionId=".Length);
				}
				if (text5 != null && text6 != null)
				{
					break;
				}
			}
			if (text5 == null || text6 == null)
			{
				return InterceptRequestResponse.Proceed();
			}
			AppInfo appInfo = _appManager.GetAppInfo(text5, text6);
			if (appInfo == null)
			{
				return InterceptRequestResponse.Proceed();
			}
			string directoryName = Path.GetDirectoryName(appInfo.IndexHtmlLocation);
			return FileLoadingHelper.LoadFile(P_0, directoryName, text4, true);
		}
		return InterceptRequestResponse.Proceed();
	}
}
