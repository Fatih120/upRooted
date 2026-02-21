// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ThrowHelper
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

internal static class ThrowHelper
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfNull([NotNull] object? P_0, [CallerArgumentExpression("argument")] string? P_1 = null)
	{
		if (P_0 == null)
		{
			ThrowArgumentNullException(P_1);
		}
		[DoesNotReturn]
		static void ThrowArgumentNullException(string? text)
		{
			throw new ArgumentNullException(text);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfNullOrEmpty([NotNull] string? P_0, [CallerArgumentExpression("argument")] string? P_1 = null)
	{
		if (string.IsNullOrEmpty(P_0))
		{
			ThrowNullOrEmptyException(P_0, P_1);
		}
		[DoesNotReturn]
		static void ThrowNullOrEmptyException(string? text, string? text2)
		{
			ThrowIfNull(text, text2);
			throw new ArgumentException("Empty string", text2);
		}
	}
}

