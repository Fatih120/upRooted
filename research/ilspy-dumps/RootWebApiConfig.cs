using System;
using System.Runtime.CompilerServices;

namespace RootApp.Core;

public class RootWebApiConfig
{
	[CompilerGenerated]
	private Uri _003CWebApiUrl_003Ek__BackingField;

	public static Uri DefaultUrl { get; } = new Uri("https://api.rootapp.com/");

	public Uri WebApiUrl
	{
		[CompilerGenerated]
		get
		{
			return _003CWebApiUrl_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CWebApiUrl_003Ek__BackingField = uri;
		}
	} = DefaultUrl;

	public bool IsProduction => DefaultUrl == WebApiUrl;
}
