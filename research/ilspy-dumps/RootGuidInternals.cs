using System;
using System.Buffers.Binary;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using RootApp.Core.Enums;

namespace RootApp.Core.Identifiers;

public static class RootGuidInternals
{
	public static readonly ulong ROOT_EPOCH = 1577836800000uL;

	public static readonly DateTime START_DATE = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public static (ulong High64, ulong Low64) FromDateTime(DateTime P_0)
	{
		ulong num = (ulong)P_0.ToUniversalTime().Subtract(START_DATE).TotalMilliseconds;
		return (High64: (num << 16) + 32768, Low64: 9223372036854775808uL);
	}

	public static (ulong High64, ulong Low64) FromReadOnlySpan(ReadOnlySpan<byte> P_0)
	{
		if (P_0.Length != 16)
		{
			throw new ArgumentException($"RootGuids are {16} bytes long. This one is not.");
		}
		return (High64: BinaryPrimitives.ReadUInt64BigEndian(P_0), Low64: BinaryPrimitives.ReadUInt64BigEndian(P_0.Slice(8, 8)));
	}

	public static (ulong High64, ulong Low64) FromRootGuidType(RootGuidType P_0, DateTime? P_1 = null)
	{
		Span<byte> span = stackalloc byte[10];
		RandomNumberGenerator.Fill(span);
		ulong num = (P_1.HasValue ? ((ulong)P_1.Value.ToUniversalTime().Subtract(START_DATE).TotalMilliseconds) : ((ulong)DateTime.UtcNow.Subtract(START_DATE).TotalMilliseconds));
		span[1] = (byte)P_0;
		span[2] = (byte)(0x80 | (span[2] >> 2));
		return (High64: (num << 16) + 32768 + (uint)((0xF & span[0]) << 8) + span[1], Low64: BinaryPrimitives.ReadUInt64BigEndian(span.Slice(2, 8)));
	}

	public static (ulong High64, ulong Low64) ConvertFromGuid(Guid P_0)
	{
		Span<byte> destination = stackalloc byte[16];
		if (!P_0.TryWriteBytes(destination))
		{
			throw new InvalidOperationException("Invalid buffer size");
		}
		ulong num = (ulong)BinaryPrimitives.ReadUInt32LittleEndian(destination.Slice(0, 4)) << 32;
		num += (ulong)BinaryPrimitives.ReadUInt16LittleEndian(destination.Slice(4, 2)) << 16;
		num += BinaryPrimitives.ReadUInt16LittleEndian(destination.Slice(6, 2));
		ulong num2 = BinaryPrimitives.ReadUInt64BigEndian(destination.Slice(8, 8));
		return (High64: num, Low64: num2);
	}

	public static Guid ConvertToGuid<T>(IRootGuid<T> P_0) where T : struct, IRootGuid<T>
	{
		(ulong High64, ulong Low64) value = P_0.GetValue();
		ulong item = value.High64;
		ulong item2 = value.Low64;
		Span<byte> span = stackalloc byte[16];
		BinaryPrimitives.WriteUInt32LittleEndian(span.Slice(0, 4), (uint)(item >> 32));
		BinaryPrimitives.WriteUInt16LittleEndian(span.Slice(4, 2), (ushort)((item >> 16) & 0xFFFF));
		BinaryPrimitives.WriteUInt16LittleEndian(span.Slice(6, 2), (ushort)(item & 0xFFFF));
		BinaryPrimitives.WriteUInt64BigEndian(span.Slice(8, 8), item2);
		return new Guid(span);
	}

	public static RootGuidType ConvertToRootGuidType(ulong P_0)
	{
		return (RootGuidType)(P_0 & 0xFF);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetResult(ulong P_0, ulong P_1)
	{
		return (P_0 >= P_1) ? 1 : (-1);
	}

	public static string ToString<T>(IRootGuid<T> P_0) where T : struct, IRootGuid<T>
	{
		Span<char> destination = stackalloc char[36];
		var (num, num2) = P_0.GetValue();
		destination[8] = (destination[13] = (destination[18] = (destination[23] = '-')));
		((uint)(num >> 32)).TryFormat(destination, out var charsWritten, "x8".AsSpan(), CultureInfo.InvariantCulture);
		ushort num3 = (ushort)(num >> 16);
		num3.TryFormat(destination.Slice(9, destination.Length - 9), out charsWritten, "x4".AsSpan(), CultureInfo.InvariantCulture);
		num3 = (ushort)num;
		num3.TryFormat(destination.Slice(14, destination.Length - 14), out charsWritten, "x4".AsSpan(), CultureInfo.InvariantCulture);
		num3 = (ushort)(num2 >> 48);
		num3.TryFormat(destination.Slice(19, destination.Length - 19), out charsWritten, "x4".AsSpan(), CultureInfo.InvariantCulture);
		num3 = (ushort)(num2 >> 32);
		num3.TryFormat(destination.Slice(24, destination.Length - 24), out charsWritten, "x4".AsSpan(), CultureInfo.InvariantCulture);
		uint num4 = (uint)num2;
		num4.TryFormat(destination.Slice(28, destination.Length - 28), out charsWritten, "x8".AsSpan(), CultureInfo.InvariantCulture);
		return destination.ToString();
	}

	public static bool TryParse(ReadOnlySpan<char> P_0, out (ulong High64, ulong Low64) P_1)
	{
		P_1 = default((ulong, ulong));
		P_0 = P_0.Trim();
		bool flag;
		switch (P_0.Length)
		{
		default:
			flag = true;
			break;
		case 22:
		case 36:
		case 37:
		case 38:
			flag = false;
			break;
		}
		if (flag)
		{
			return false;
		}
		if (P_0.Length == 22)
		{
			Span<byte> span = stackalloc byte[16];
			if (!RootBase64Url.TrySafeDecodeBase64UrlChars(P_0, span, out var num) || span.Length != num)
			{
				return false;
			}
			P_1 = FromReadOnlySpan(span);
			return true;
		}
		int num2 = 0;
		if (P_0[0] == '{')
		{
			if (P_0.Length != 38 || P_0[37] != '}')
			{
				return false;
			}
			num2 = 1;
		}
		else if (P_0[0] == '(')
		{
			if (P_0.Length != 38 || P_0[37] != ')')
			{
				return false;
			}
			num2 = 1;
		}
		else if (P_0.Length != 36)
		{
			return false;
		}
		if (P_0[8 + num2] != '-' || P_0[13 + num2] != '-' || P_0[18 + num2] != '-' || P_0[23 + num2] != '-')
		{
			return false;
		}
		int num3 = num2;
		ReadOnlySpan<char> readOnlySpan = P_0.Slice(num3, P_0.Length - num3);
		ulong num4 = (ulong)uint.Parse(readOnlySpan.Slice(0, 8), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture) << 32;
		readOnlySpan = readOnlySpan.Slice(9, readOnlySpan.Length - 9);
		num4 += (uint)(ushort.Parse(readOnlySpan.Slice(0, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture) << 16);
		readOnlySpan = readOnlySpan.Slice(5, readOnlySpan.Length - 5);
		num4 += ushort.Parse(readOnlySpan.Slice(0, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
		readOnlySpan = readOnlySpan.Slice(5, readOnlySpan.Length - 5);
		ulong num5 = (ulong)ushort.Parse(readOnlySpan.Slice(0, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture) << 48;
		readOnlySpan = readOnlySpan.Slice(5, readOnlySpan.Length - 5);
		num5 += (ulong)ushort.Parse(readOnlySpan.Slice(0, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture) << 32;
		readOnlySpan = readOnlySpan.Slice(4, readOnlySpan.Length - 4);
		num5 += uint.Parse(readOnlySpan.Slice(0, 8), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
		P_1 = (High64: num4, Low64: num5);
		return true;
	}

	public static int CompareTo<T>(IRootGuid<T> P_0, object? P_1) where T : struct, IRootGuid<T>
	{
		if (P_1 == null)
		{
			return 1;
		}
		if (!(P_1 is T val) || 1 == 0)
		{
			throw new ArgumentException("Not a RootGuid");
		}
		return CompareToGuid(P_0, val);
	}

	public static int CompareToGuid(IRootGuid P_0, IRootGuid? P_1)
	{
		ArgumentNullException.ThrowIfNull(P_1, "value");
		var (num, num2) = P_0.GetValue();
		var (num3, num4) = P_1.GetValue();
		if (num3 != num)
		{
			return GetResult(num, num3);
		}
		if (num4 != num2)
		{
			return GetResult(num2, num4);
		}
		return 0;
	}
}
