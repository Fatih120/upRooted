using System;
using System.IO;
using Avalonia.Platform;
using DotNetBrowser.Net.Handlers;
using RootApp.Browser.Handlers.Utilities;

namespace RootApp.Browser.Handlers.Specialized;

public class SoundEffectInterceptHandler : IUrlSchemeHandler
{
	public bool CanHandle(string P_0)
	{
		return P_0.StartsWith("avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds", StringComparison.OrdinalIgnoreCase);
	}

	public InterceptRequestResponse Handle(InterceptRequestParameters P_0)
	{
		using Stream stream = AssetLoader.Open(new Uri(P_0.UrlRequest.Url));
		using MemoryStream memoryStream = new MemoryStream();
		stream.CopyTo(memoryStream);
		byte[] array = memoryStream.ToArray();
		return FileLoadingHelper.CreateJobFromBytes(MimeTypeProvider.GetMimeType(P_0.UrlRequest.Url), array, P_0);
	}
}
