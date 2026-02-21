using System;
using System.Buffers.Binary;

namespace RootApp.Utility.ImageSize;

public static class GifSizer
{
	private static ReadOnlySpan<byte> Gif8 => "GIF8"u8;

	public static (int width, int height) GetSize(ReadOnlySpan<byte> P_0)
	{
		if (P_0.Length < 10)
		{
			throw new ArgumentException("Header is too short");
		}
		if (!P_0.StartsWith(Gif8) || P_0[5] != 97)
		{
			throw new ArgumentException("Not a valid GIF file (invalid signature)");
		}
		byte b = P_0[4];
		if (b != 55 && b != 57)
		{
			throw new ArgumentException("Not a valid GIF file (invalid signature)");
		}
		ReadOnlySpan<byte> readOnlySpan = P_0.Slice(6, P_0.Length - 6);
		ushort num = BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(0, 2));
		ushort num2 = BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(2, 2));
		if (num == 0 || num2 == 0)
		{
			throw new ArgumentException("Invalid image dimensions");
		}
		return (width: num, height: num2);
	}
}
