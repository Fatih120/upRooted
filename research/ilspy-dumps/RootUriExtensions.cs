using System;

namespace RootApp.Core;

public static class RootUriExtensions
{
	public static bool IsUploadToken(this Uri P_0)
	{
		return P_0.IsRootAuthority("upload");
	}

	public static bool IsRootAuthority(this Uri P_0, string P_1)
	{
		return P_0.IsRootScheme() && P_0.IsAuthority(P_1);
	}

	public static bool IsRootScheme(this Uri P_0)
	{
		return P_0.IsScheme("root");
	}

	public static bool IsScheme(this Uri P_0, string P_1)
	{
		return string.Equals(P_0.Scheme, P_1, StringComparison.OrdinalIgnoreCase);
	}

	public static bool IsAuthority(this Uri P_0, string P_1)
	{
		return string.Equals(P_0.Authority, P_1, StringComparison.OrdinalIgnoreCase);
	}

	public static bool IsRootCloudflareAsset(this Uri P_0)
	{
		if (!P_0.IsScheme("https"))
		{
			return false;
		}
		if (!string.Equals(P_0.Host, "imagedelivery.net", StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		string[] array = P_0.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
		if (array.Length < 2)
		{
			return false;
		}
		return string.Equals(array[0], "o8EZDxDw4ZU5noWzBAUDKw", StringComparison.OrdinalIgnoreCase);
	}
}
