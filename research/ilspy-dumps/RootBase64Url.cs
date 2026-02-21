using System;
using System.Buffers;
using System.Buffers.Text;

namespace RootApp.Core;

public static class RootBase64Url
{
	private static readonly SearchValues<char> _searchBase64Url = SearchValues.Create(Base64UrlChars);

	private static ReadOnlySpan<char> Base64UrlChars => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_".AsSpan();

	public static bool TrySafeDecodeBase64UrlChars(ReadOnlySpan<char> P_0, Span<byte> P_1, out int P_2)
	{
		P_2 = 0;
		if (!IsBase64Url(P_0))
		{
			return false;
		}
		try
		{
			return Base64Url.TryDecodeFromChars(P_0, P_1, out P_2);
		}
		catch (FormatException)
		{
			return false;
		}
	}

	public static bool IsBase64Url(ReadOnlySpan<char> P_0)
	{
		return -1 == P_0.IndexOfAnyExcept(_searchBase64Url);
	}
}
