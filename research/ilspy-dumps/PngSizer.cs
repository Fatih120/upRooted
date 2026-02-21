using System;
using System.Buffers.Binary;

namespace RootApp.Utility.ImageSize;

public static class PngSizer
{
	private static ReadOnlySpan<byte> Signature => new byte[8] { 137, 80, 78, 71, 13, 10, 26, 10 };

	private static ReadOnlySpan<byte> IHDR => "IHDR"u8;

	public static (int width, int height) GetSize(ReadOnlySpan<byte> P_0)
	{
		if (P_0.Length < 24)
		{
			throw new ArgumentException("Header is too short");
		}
		if (!Signature.SequenceEqual(P_0.Slice(0, 8)))
		{
			throw new ArgumentException("Not a valid PNG file (invalid signature)");
		}
		ReadOnlySpan<byte> readOnlySpan = P_0.Slice(8, P_0.Length - 8);
		uint num = BinaryPrimitives.ReadUInt32BigEndian(readOnlySpan.Slice(0, 4));
		if (num != 13)
		{
			throw new ArgumentException("Invalid IHDR chunk length");
		}
		readOnlySpan = readOnlySpan.Slice(4, readOnlySpan.Length - 4);
		if (!IHDR.SequenceEqual(readOnlySpan.Slice(0, 4)))
		{
			throw new ArgumentException("First chunk is not IHDR");
		}
		readOnlySpan = readOnlySpan.Slice(4, readOnlySpan.Length - 4);
		int num2 = BinaryPrimitives.ReadInt32BigEndian(readOnlySpan.Slice(0, 4));
		int num3 = BinaryPrimitives.ReadInt32BigEndian(readOnlySpan.Slice(4, 4));
		if (num2 <= 0 || num3 <= 0)
		{
			throw new ArgumentException("Invalid image dimensions");
		}
		return (width: num2, height: num3);
	}
}
