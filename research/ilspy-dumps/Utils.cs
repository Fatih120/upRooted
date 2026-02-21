// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.Utils
using System;
using System.Buffers;
using System.Reflection;
using System.Text;
using SkiaSharp;

internal static class Utils
{
	[DefaultMember("Item")]
	internal readonly ref struct RentedArray<T>(int P_0)
	{
		public readonly T[] Array = ArrayPool<T>.Shared.Rent(P_0);

		public readonly Span<T> Span = new Span<T>(Array, 0, P_0);

		public int Length => Span.Length;

		public void Dispose()
		{
			if (Array != null)
			{
				ArrayPool<T>.Shared.Return(Array);
			}
		}

		public static explicit operator T[](RentedArray<T> P_0)
		{
			return P_0.Array;
		}

		public static implicit operator Span<T>(RentedArray<T> P_0)
		{
			return P_0.Span;
		}

		public static implicit operator ReadOnlySpan<T>(RentedArray<T> P_0)
		{
			return P_0.Span;
		}
	}

	internal static int GetPreambleSize(SKData P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("data");
		}
		ReadOnlySpan<byte> readOnlySpan = P_0.AsSpan();
		int length = readOnlySpan.Length;
		if (length >= 2 && readOnlySpan[0] == 254 && readOnlySpan[1] == byte.MaxValue)
		{
			return 2;
		}
		if (length >= 3 && readOnlySpan[0] == 239 && readOnlySpan[1] == 187 && readOnlySpan[2] == 191)
		{
			return 3;
		}
		if (length >= 3 && readOnlySpan[0] == 43 && readOnlySpan[1] == 47 && readOnlySpan[2] == 118)
		{
			return 3;
		}
		if (length >= 4 && readOnlySpan[0] == 0 && readOnlySpan[1] == 0 && readOnlySpan[2] == 254 && readOnlySpan[3] == byte.MaxValue)
		{
			return 4;
		}
		return 0;
	}

	internal unsafe static Span<byte> AsSpan(this IntPtr P_0, int P_1)
	{
		return new Span<byte>((void*)P_0, P_1);
	}

	internal unsafe static ReadOnlySpan<byte> AsReadOnlySpan(this IntPtr P_0, int P_1)
	{
		return new ReadOnlySpan<byte>((void*)P_0, P_1);
	}

	internal unsafe static byte[] GetBytes(this Encoding P_0, ReadOnlySpan<char> P_1)
	{
		if (P_1.Length == 0)
		{
			return new byte[0];
		}
		fixed (char* ptr = P_1)
		{
			int byteCount = P_0.GetByteCount(ptr, P_1.Length);
			if (byteCount == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[byteCount];
			fixed (byte* ptr2 = array)
			{
				P_0.GetBytes(ptr, P_1.Length, ptr2, byteCount);
			}
			return array;
		}
	}

	public static RentedArray<T> RentArray<T>(int P_0, bool P_1 = false)
	{
		if (!P_1 || P_0 > 0)
		{
			return new RentedArray<T>(P_0);
		}
		return default(RentedArray<T>);
	}
}
