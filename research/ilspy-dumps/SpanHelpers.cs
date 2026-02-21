// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanHelpers
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

public static class SpanHelpers
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseUInt(this ReadOnlySpan<char> P_0, NumberStyles P_1, IFormatProvider P_2, out uint P_3)
	{
		return uint.TryParse(P_0, P_1, P_2, out P_3);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInt(this ReadOnlySpan<char> P_0, NumberStyles P_1, IFormatProvider P_2, out int P_3)
	{
		return int.TryParse(P_0, P_1, P_2, out P_3);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseDouble(this ReadOnlySpan<char> P_0, NumberStyles P_1, IFormatProvider P_2, out double P_3)
	{
		return double.TryParse(P_0, P_1, P_2, out P_3);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double ParseDouble(this ReadOnlySpan<char> P_0, IFormatProvider P_1)
	{
		return double.Parse(P_0, P_1);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseByte(this ReadOnlySpan<char> P_0, NumberStyles P_1, IFormatProvider P_2, out byte P_3)
	{
		return byte.TryParse(P_0, P_1, P_2, out P_3);
	}
}

