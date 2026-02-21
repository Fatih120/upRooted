using System;
using System.Buffers.Binary;

namespace RootApp.Utility.ImageSize;

public static class JpegSizer
{
	public static (int width, int height) GetSize(ReadOnlySpan<byte> P_0)
	{
		if (P_0.Length < 11)
		{
			throw new ArgumentException("Header is too short");
		}
		if (P_0[0] != byte.MaxValue || P_0[1] != 216)
		{
			throw new ArgumentException("Not a valid JPEG file (missing SOI marker)");
		}
		int num = 2;
		while (num < P_0.Length - 1)
		{
			if (P_0[num++] != byte.MaxValue)
			{
				throw new ArgumentException("Invalid JPEG marker structure");
			}
			byte b = P_0[num++];
			if (b == byte.MaxValue)
			{
				continue;
			}
			if ((b == 192 || b == 194) ? true : false)
			{
				if (num + 7 > P_0.Length)
				{
					throw new ArgumentException("Header truncated in SOF segment");
				}
				ushort num2 = BinaryPrimitives.ReadUInt16BigEndian(P_0.Slice(num, 2));
				if (num2 < 7)
				{
					throw new ArgumentException("Invalid SOF segment length");
				}
				num += 3;
				ushort num3 = BinaryPrimitives.ReadUInt16BigEndian(P_0.Slice(num, 2));
				ushort num4 = BinaryPrimitives.ReadUInt16BigEndian(P_0.Slice(num + 2, 2));
				return (width: num4, height: num3);
			}
			if (num + 2 > P_0.Length)
			{
				throw new ArgumentException("Header truncated in segment length");
			}
			ushort num5 = BinaryPrimitives.ReadUInt16BigEndian(P_0.Slice(num, 2));
			num += num5;
		}
		throw new ArgumentException("No SOF marker found in JPEG header");
	}
}
