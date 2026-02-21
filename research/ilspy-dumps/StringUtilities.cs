// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.StringUtilities
using System;
using System.Text;
using SkiaSharp;

public static class StringUtilities
{
	internal static byte[] GetEncodedText(string P_0, SKTextEncoding P_1, bool P_2)
	{
		if (!string.IsNullOrEmpty(P_0) && P_2)
		{
			P_0 += "\0";
		}
		return GetEncodedText(P_0.AsSpan(), P_1);
	}

	public static byte[] GetEncodedText(ReadOnlySpan<char> P_0, SKTextEncoding P_1)
	{
		return P_1 switch
		{
			SKTextEncoding.Utf8 => Encoding.UTF8.GetBytes(P_0), 
			SKTextEncoding.Utf16 => Encoding.Unicode.GetBytes(P_0), 
			SKTextEncoding.Utf32 => Encoding.UTF32.GetBytes(P_0), 
			_ => throw new ArgumentOutOfRangeException("encoding", $"Encoding {P_1} is not supported."), 
		};
	}

	public static string GetString(IntPtr P_0, int P_1, SKTextEncoding P_2)
	{
		return GetString(P_0.AsReadOnlySpan(P_1), 0, P_1, P_2);
	}

	public unsafe static string GetString(ReadOnlySpan<byte> P_0, int P_1, int P_2, SKTextEncoding P_3)
	{
		P_0 = P_0.Slice(P_1, P_2);
		if (P_0.Length == 0)
		{
			return string.Empty;
		}
		fixed (byte* ptr = P_0)
		{
			return P_3 switch
			{
				SKTextEncoding.Utf8 => Encoding.UTF8.GetString(ptr, P_0.Length), 
				SKTextEncoding.Utf16 => Encoding.Unicode.GetString(ptr, P_0.Length), 
				SKTextEncoding.Utf32 => Encoding.UTF32.GetString(ptr, P_0.Length), 
				_ => throw new ArgumentOutOfRangeException("encoding", $"Encoding {P_3} is not supported."), 
			};
		}
	}
}

