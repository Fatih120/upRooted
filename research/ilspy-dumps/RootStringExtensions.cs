using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Unicode;

namespace RootApp.Utility.Extensions;

public static class RootStringExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToUtf8Base64(this string P_0)
	{
		return P_0.AsSpan().ToUtf8Base64();
	}

	public static string ToUtf8Base64(this ReadOnlySpan<char> P_0)
	{
		if (P_0.Length <= 256)
		{
			Span<byte> span = stackalloc byte[1024];
			if (Utf8.FromUtf16(P_0, span, out var _, out var num2) == OperationStatus.Done)
			{
				return Convert.ToBase64String(span.Slice(0, num2));
			}
		}
		Encoding uTF = Encoding.UTF8;
		int byteCount = uTF.GetByteCount(P_0);
		byte[] array = ArrayPool<byte>.Shared.Rent(byteCount);
		try
		{
			int bytes = uTF.GetBytes(P_0, array);
			return Convert.ToBase64String(array.AsSpan(..bytes));
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(array);
		}
	}
}
