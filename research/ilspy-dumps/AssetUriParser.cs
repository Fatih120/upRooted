using System;
using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using RootApp.Core.Enums;
using RootApp.Core.Identifiers;

namespace RootApp.Core.Assets;

public static class AssetUriParser
{
	public static SearchValues<char> ValidPathCharacters { get; } = SearchValues.Create("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~/".AsSpan());

	public static bool TryParse(string P_0, [NotNullWhen(true)] out Uri? P_1, out AssetGuid P_2)
	{
		P_2 = default(AssetGuid);
		P_1 = null;
		if (!TryParse(P_0, out Uri uri))
		{
			return false;
		}
		if (!TryParseAssetGuidInternal(uri, ref P_2))
		{
			return false;
		}
		P_1 = uri;
		return true;
	}

	public static bool TryParse(string? P_0, [NotNullWhen(true)] out Uri? P_1)
	{
		P_1 = null;
		bool flag = string.IsNullOrEmpty(P_0);
		bool flag2 = flag;
		if (!flag2)
		{
			int length = P_0.Length;
			bool flag3 = ((length < 35 || length > 2048) ? true : false);
			flag2 = flag3;
		}
		if (flag2)
		{
			return false;
		}
		if (!P_0.StartsWith("root://asset/", StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		ReadOnlySpan<char> readOnlySpan = P_0.AsSpan(13);
		if (readOnlySpan.ContainsAnyExcept(ValidPathCharacters))
		{
			return false;
		}
		if (!Uri.TryCreate(P_0, UriKind.Absolute, out P_1))
		{
			return false;
		}
		return true;
	}

	private static bool TryParseAssetGuidInternal(Uri P_0, ref AssetGuid P_1)
	{
		ReadOnlySpan<char> readOnlySpan = P_0.AbsolutePath.AsSpan();
		int length = readOnlySpan.Length;
		bool flag = ((length < 23 || length > 2048) ? true : false);
		if (!flag && readOnlySpan[0] == '/')
		{
			if (readOnlySpan[readOnlySpan.Length - 1] != '/')
			{
				readOnlySpan = readOnlySpan.Slice(1, readOnlySpan.Length - 1);
				if (readOnlySpan.ContainsAnyExcept(ValidPathCharacters))
				{
					return false;
				}
				int num = readOnlySpan.IndexOf('/');
				return (num >= 0) ? TryParseSegmentedFormat(readOnlySpan, num, ref P_1) : TryParseBinaryFormat(readOnlySpan, ref P_1);
			}
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool TryParseBinaryFormat(ReadOnlySpan<char> P_0, ref AssetGuid P_1)
	{
		if (P_0.Length < 22)
		{
			return false;
		}
		if (P_0.Length == 36 && P_0[8] == '-' && P_0[13] == '-' && P_0[18] == '-' && P_0[23] == '-')
		{
			return TryParseUuidFormat(P_0, ref P_1);
		}
		if (P_0.Length == 22)
		{
			if (!RootGuid.TryParse<RootGuid>(P_0, out var rootGuid))
			{
				return false;
			}
			if (rootGuid.ToRootGuidType() != RootGuidType.Asset)
			{
				return false;
			}
			P_1 = (AssetGuid)rootGuid;
			return P_1.HasValue();
		}
		int num = ((P_0.Length >= 24) ? 24 : P_0.Length);
		Span<byte> span = stackalloc byte[Base64Url.GetMaxDecodedLength(num)];
		if (!RootBase64Url.TrySafeDecodeBase64UrlChars(P_0.Slice(0, num), span, out var num2))
		{
			return false;
		}
		if (num2 < 16)
		{
			return false;
		}
		RootGuid rootGuid2 = RootGuid.Create(span.Slice(0, 16));
		if (rootGuid2.ToRootGuidType() != RootGuidType.Asset)
		{
			return false;
		}
		P_1 = (AssetGuid)rootGuid2;
		return P_1.HasValue();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static bool TryParseSegmentedFormat(ReadOnlySpan<char> P_0, int P_1, ref AssetGuid P_2)
	{
		ReadOnlySpan<char> readOnlySpan = P_0.Slice(0, P_1);
		if (readOnlySpan.Length < 22)
		{
			if (!readOnlySpan.SequenceEqual("image".AsSpan()) && !readOnlySpan.SequenceEqual("video".AsSpan()))
			{
				return false;
			}
			int num = P_1 + 1;
			ReadOnlySpan<char> readOnlySpan2 = P_0.Slice(num, P_0.Length - num);
			if (readOnlySpan2.Contains('/'))
			{
				return false;
			}
			if (readOnlySpan2.Length < 22)
			{
				return false;
			}
			if (!RootGuid.TryParse<RootGuid>(readOnlySpan2, out var rootGuid))
			{
				return false;
			}
			if (rootGuid.ToRootGuidType() != RootGuidType.Asset)
			{
				return false;
			}
			P_2 = (AssetGuid)rootGuid;
			return P_2.HasValue();
		}
		if (readOnlySpan.Length != 22)
		{
			return false;
		}
		if (!RootGuid.TryParse<RootGuid>(readOnlySpan, out var rootGuid2))
		{
			return false;
		}
		if (rootGuid2.ToRootGuidType() != RootGuidType.Asset)
		{
			return false;
		}
		P_2 = (AssetGuid)rootGuid2;
		return P_2.HasValue();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static bool TryParseUuidFormat(ReadOnlySpan<char> P_0, ref AssetGuid P_1)
	{
		if (!Guid.TryParse(P_0, out var result))
		{
			return false;
		}
		Span<byte> span = stackalloc byte[16];
		if (!result.TryWriteBytes(span))
		{
			return false;
		}
		RootGuid rootGuid = RootGuid.Create(span);
		if (rootGuid.ToRootGuidType() != RootGuidType.Asset)
		{
			return false;
		}
		P_1 = (AssetGuid)rootGuid;
		return P_1.HasValue();
	}
}
