using System;
using System.IO;
using System.Threading.Tasks;
using DotNetBrowser.Net;
using DotNetBrowser.Net.Handlers;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.Avalonia.Helpers.Caching;

namespace RootApp.Browser.Handlers.Specialized;

public class ImageCacheInterceptHandler : IUrlSchemeHandler
{
	private readonly BitmapCache _bitmapCache;

	public ImageCacheInterceptHandler(BitmapCache P_0)
	{
		_bitmapCache = P_0;
	}

	public bool CanHandle(string P_0)
	{
		return P_0.StartsWith("root://asset/", StringComparison.OrdinalIgnoreCase) || P_0.StartsWith("root://image/", StringComparison.OrdinalIgnoreCase);
	}

	public InterceptRequestResponse Handle(InterceptRequestParameters P_0)
	{
		string[] array = P_0.UrlRequest.Url.Split("?imageOptions=");
		string uriString = array[0];
		string text = ((array.Length > 1) ? array[1] : "original");
		Uri uri = new Uri(uriString);
		UrlRequestJob job = P_0.Network.CreateUrlRequestJob(P_0.UrlRequest);
		Task.Run(async delegate
		{
			BitmapWrapper bitmapWrapper = await _bitmapCache.GetBitmapAsync(uri.ToString());
			if (bitmapWrapper == null)
			{
				return;
			}
			using MemoryStream stream = new MemoryStream();
			bitmapWrapper.Bitmap.Save(stream);
			byte[] fileBytes = stream.ToArray();
			job.Write(fileBytes);
			job.Complete();
		}).Forget();
		return InterceptRequestResponse.Intercept(job);
	}
}
