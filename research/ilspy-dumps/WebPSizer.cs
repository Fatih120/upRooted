using System;
using System.Buffers.Binary;

namespace RootApp.Utility.ImageSize;

public static class WebPSizer
{
	private static ReadOnlySpan<byte> RIFF => "RIFF"u8;

	private static ReadOnlySpan<byte> WEBP => "WEBP"u8;

	private static ReadOnlySpan<byte> VP8 => "VP8"u8;

	private static ReadOnlySpan<byte> VP8Prefix => new byte[3] { 157, 1, 42 };

	public static (int width, int height) GetSize(int P_0, ReadOnlySpan<byte> P_1)
	{
		if (P_1.Length < 32)
		{
			throw new ArgumentException("Header is too short");
		}
		ReadOnlySpan<byte> rIFF = RIFF;
		if (!rIFF.SequenceEqual(P_1.Slice(0, 4)))
		{
			throw new ArgumentException("Header is not RIFF");
		}
		int length = rIFF.Length;
		ReadOnlySpan<byte> readOnlySpan = P_1.Slice(length, P_1.Length - length);
		uint num = BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan.Slice(0, 4));
		readOnlySpan = readOnlySpan.Slice(4, readOnlySpan.Length - 4);
		if (P_0 > 0 && P_0 != num + 8)
		{
			throw new ArgumentException("Header size does not match actual size");
		}
		ReadOnlySpan<byte> wEBP = WEBP;
		if (!wEBP.SequenceEqual(readOnlySpan.Slice(0, 4)))
		{
			throw new ArgumentException("Header is not WebP");
		}
		length = wEBP.Length;
		readOnlySpan = readOnlySpan.Slice(length, readOnlySpan.Length - length);
		ReadOnlySpan<byte> span = readOnlySpan.Slice(0, 4);
		readOnlySpan = readOnlySpan.Slice(4, readOnlySpan.Length - 4);
		if (!span.StartsWith(VP8))
		{
			throw new ArgumentException("Header is not VP8x");
		}
		ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan.Slice(0, 4);
		readOnlySpan = readOnlySpan.Slice(4, readOnlySpan.Length - 4);
		uint num2 = BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan2);
		if (num2 + 16 > P_0)
		{
			throw new InvalidOperationException("Invalid chunk size");
		}
		switch (span[3])
		{
		case 32:
		{
			ReadOnlySpan<byte> readOnlySpan6 = readOnlySpan.Slice(0, 3);
			readOnlySpan = readOnlySpan.Slice(3, readOnlySpan.Length - 3);
			ReadOnlySpan<byte> readOnlySpan7 = readOnlySpan.Slice(0, 3);
			readOnlySpan = readOnlySpan.Slice(3, readOnlySpan.Length - 3);
			if (!VP8Prefix.SequenceEqual(readOnlySpan7))
			{
				throw new ArgumentException("Data is not 'VP8 '");
			}
			uint num8 = (uint)(BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(0, 2)) & 0x3FFF);
			uint num9 = (uint)(BinaryPrimitives.ReadUInt16LittleEndian(readOnlySpan.Slice(2, 2)) & 0x3FFF);
			return checked((width: (int)num8, height: (int)num9));
		}
		case 76:
		{
			if (readOnlySpan[0] != 47)
			{
				throw new ArgumentException("Header is not VP8L");
			}
			readOnlySpan = readOnlySpan.Slice(1, readOnlySpan.Length - 1);
			uint num5 = BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan.Slice(0, 4));
			uint num6 = (num5 & 0x3FFF) + 1;
			uint num7 = ((num5 >> 14) & 0x3FFF) + 1;
			return checked((width: (int)num6, height: (int)num7));
		}
		case 88:
		{
			ReadOnlySpan<byte> readOnlySpan3 = readOnlySpan.Slice(0, 4);
			readOnlySpan = readOnlySpan.Slice(4, readOnlySpan.Length - 4);
			ReadOnlySpan<byte> readOnlySpan4 = readOnlySpan.Slice(0, 4);
			ReadOnlySpan<byte> readOnlySpan5 = readOnlySpan.Slice(3, 4);
			uint num3 = 1 + (BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan4) & 0xFFFFFF);
			uint num4 = 1 + (BinaryPrimitives.ReadUInt32LittleEndian(readOnlySpan5) & 0xFFFFFF);
			return checked((width: (int)num3, height: (int)num4));
		}
		default:
			throw new ArgumentException("Header is not a known VP8x format");
		}
	}
}
