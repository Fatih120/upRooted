using System;
using RootApp.Core;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Authentication;

namespace RootApp.WebApi.Client.Shared;

public sealed record ClientToken(UserGuid UserId, DeviceGuid DeviceId, CommunityGuid? CommunityId, string Token) : IAuthenticated
{
	public static ClientToken? Parse(string value)
	{
		int length = value.Length;
		if ((length < 16 || length > 512) ? true : false)
		{
			return null;
		}
		Span<byte> span = stackalloc byte[value.Length];
		if (!RootBase64Url.TrySafeDecodeBase64UrlChars(value.AsSpan(), span, out var num))
		{
			return null;
		}
		if (num < 32)
		{
			return null;
		}
		return new ClientToken(UserGuid.Create(span.Slice(0, 16)), DeviceGuid.Create(span.Slice(16, 16)), null, value);
	}

	public override string ToString()
	{
		return Token;
	}
}
