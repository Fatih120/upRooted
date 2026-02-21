using System;
using System.Buffers.Binary;
using System.Buffers.Text;
using RootApp.Core.Enums;

namespace RootApp.Core.Identifiers;

public static class RootGuidExtensions
{
	public static DateTime ToDateTime<T>(this IRootGuid<T> P_0) where T : struct, IRootGuid<T>
	{
		long num = P_0.ToEpoch();
		return DateTime.UnixEpoch.AddMilliseconds(num).ToUniversalTime();
	}

	public static DateTimeOffset ToDateTimeOffset<T>(this IRootGuid<T> P_0) where T : struct, IRootGuid<T>
	{
		return DateTimeOffset.FromUnixTimeMilliseconds(P_0.ToEpoch()).ToUniversalTime();
	}

	public static RootGuidType ToRootGuidType<T>(this IRootGuid<T> P_0) where T : struct, IRootGuid<T>
	{
		ulong item = P_0.GetValue().High64;
		return RootGuidInternals.ConvertToRootGuidType(item);
	}

	public static long ToEpoch<T>(this IRootGuid<T> P_0) where T : struct, IRootGuid<T>
	{
		ulong num = P_0.GetValue().High64 >> 16;
		return (long)(num + RootGuidInternals.ROOT_EPOCH);
	}

	public static void ToBytes<T>(this IRootGuid<T> P_0, Span<byte> P_1) where T : struct, IRootGuid<T>
	{
		if (P_1.Length != 16)
		{
			throw new ArgumentException($"RootGuids are {16} bytes.");
		}
		var (num, num2) = P_0.GetValue();
		BinaryPrimitives.WriteUInt64BigEndian(P_1, num);
		BinaryPrimitives.WriteUInt64BigEndian(P_1.Slice(8, P_1.Length - 8), num2);
	}

	public static string ToStringFormat<T>(this IRootGuid<T> P_0, string? P_1) where T : struct, IRootGuid<T>
	{
		if (P_1 == null)
		{
			return P_0.ToString();
		}
		Span<byte> span = stackalloc byte[16];
		Span<char> span2 = stackalloc char[32];
		P_0.ToBytes(span);
		if (!Base64Url.TryEncodeToChars(span, span2, out var num))
		{
			throw new InvalidOperationException("Unable to convert to base64url");
		}
		return new string(span2.Slice(0, num));
	}

	public static Guid ToGuid<T>(this T P_0) where T : struct, IRootGuid<T>
	{
		return RootGuidInternals.ConvertToGuid(P_0);
	}

	public static RootGuid ToRootGuid<T>(this T P_0) where T : struct, IRootGuid<T>
	{
		return RootGuid.Create(P_0.GetValue());
	}
}
