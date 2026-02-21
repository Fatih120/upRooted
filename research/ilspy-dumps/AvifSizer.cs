using System;
using System.Buffers.Binary;

namespace RootApp.Utility.ImageSize;

public static class AvifSizer
{
	private static ReadOnlySpan<byte> Ftyp => "ftyp"u8;

	private static ReadOnlySpan<byte> Avif => "avif"u8;

	private static ReadOnlySpan<byte> Meta => "meta"u8;

	private static ReadOnlySpan<byte> Iprp => "iprp"u8;

	private static ReadOnlySpan<byte> Ipco => "ipco"u8;

	private static ReadOnlySpan<byte> Ispe => "ispe"u8;

	public static (int width, int height) GetSize(int P_0, ReadOnlySpan<byte> P_1)
	{
		if (P_1.Length < 32)
		{
			throw new ArgumentException("Header is too short", "header");
		}
		if (P_0 < P_1.Length)
		{
			throw new ArgumentOutOfRangeException("actualSize", "ActualSize is too low");
		}
		ReadOnlySpan<byte> readOnlySpan = P_1;
		bool flag = false;
		bool flag2 = false;
		int num = 0;
		int num2 = 0;
		while (readOnlySpan.Length >= 8 && (!flag || !flag2))
		{
			int num3 = (int)BinaryPrimitives.ReadUInt32BigEndian(readOnlySpan.Slice(0, 4));
			if (num3 < 8 || num3 > readOnlySpan.Length)
			{
				if (P_0 > 0 && num3 > P_0 - (P_1.Length - readOnlySpan.Length))
				{
					throw new ArgumentException("Invalid box size in AVIF container");
				}
				throw new ArgumentException("The header is too short");
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan.Slice(0, num3);
			ReadOnlySpan<byte> readOnlySpan3 = readOnlySpan2.Slice(4, 4);
			if (readOnlySpan3.SequenceEqual(Ftyp))
			{
				if (readOnlySpan2.Length < 12)
				{
					throw new ArgumentException("Truncated ftyp box");
				}
				ReadOnlySpan<byte> readOnlySpan4 = readOnlySpan2.Slice(8, 4);
				if (!readOnlySpan4.SequenceEqual(Avif))
				{
					throw new ArgumentException("Not a valid AVIF file (invalid brand)");
				}
				flag = true;
			}
			else if (readOnlySpan3.SequenceEqual(Meta))
			{
				ReadOnlySpan<byte> readOnlySpan5 = readOnlySpan2.Slice(12, readOnlySpan2.Length - 12);
				flag2 = ParseMetaBox(readOnlySpan5, ref num, ref num2);
			}
			int num4 = num3;
			readOnlySpan = readOnlySpan.Slice(num4, readOnlySpan.Length - num4);
		}
		if (!flag)
		{
			throw new ArgumentException("No AVIF signature found");
		}
		if (!flag2)
		{
			throw new ArgumentException("No image dimensions found");
		}
		return (width: num, height: num2);
	}

	private static bool ParseMetaBox(ReadOnlySpan<byte> P_0, ref int P_1, ref int P_2)
	{
		ReadOnlySpan<byte> readOnlySpan = P_0;
		while (readOnlySpan.Length >= 8)
		{
			int num = (int)BinaryPrimitives.ReadUInt32BigEndian(readOnlySpan.Slice(0, 4));
			if (num < 8 || num > readOnlySpan.Length)
			{
				return false;
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan.Slice(4, 4);
			if (readOnlySpan2.SequenceEqual(Iprp))
			{
				ReadOnlySpan<byte> readOnlySpan3 = readOnlySpan.Slice(8, num - 8);
				if (ParseIprpBox(readOnlySpan3, ref P_1, ref P_2))
				{
					return true;
				}
			}
			int num2 = num;
			readOnlySpan = readOnlySpan.Slice(num2, readOnlySpan.Length - num2);
		}
		return false;
	}

	private static bool ParseIprpBox(ReadOnlySpan<byte> P_0, ref int P_1, ref int P_2)
	{
		ReadOnlySpan<byte> readOnlySpan = P_0;
		while (readOnlySpan.Length >= 8)
		{
			int num = (int)BinaryPrimitives.ReadUInt32BigEndian(readOnlySpan.Slice(0, 4));
			if (num < 8 || num > readOnlySpan.Length)
			{
				return false;
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan.Slice(4, 4);
			if (readOnlySpan2.SequenceEqual(Ipco))
			{
				ReadOnlySpan<byte> readOnlySpan3 = readOnlySpan.Slice(8, num - 8);
				if (ParseIpcoBox(readOnlySpan3, ref P_1, ref P_2))
				{
					return true;
				}
			}
			int num2 = num;
			readOnlySpan = readOnlySpan.Slice(num2, readOnlySpan.Length - num2);
		}
		return false;
	}

	private static bool ParseIpcoBox(ReadOnlySpan<byte> P_0, ref int P_1, ref int P_2)
	{
		ReadOnlySpan<byte> readOnlySpan = P_0;
		while (readOnlySpan.Length >= 8)
		{
			int num = (int)BinaryPrimitives.ReadUInt32BigEndian(readOnlySpan.Slice(0, 4));
			if (num < 8 || num > readOnlySpan.Length)
			{
				return false;
			}
			ReadOnlySpan<byte> readOnlySpan2 = readOnlySpan.Slice(4, 4);
			if (readOnlySpan2.SequenceEqual(Ispe))
			{
				if (num < 20)
				{
					return false;
				}
				P_1 = (int)BinaryPrimitives.ReadUInt32BigEndian(readOnlySpan.Slice(12, 4));
				P_2 = (int)BinaryPrimitives.ReadUInt32BigEndian(readOnlySpan.Slice(16, 4));
				return P_1 > 0 && P_2 > 0;
			}
			int num2 = num;
			readOnlySpan = readOnlySpan.Slice(num2, readOnlySpan.Length - num2);
		}
		return false;
	}
}
