// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKColor
using System;
using SkiaSharp;

public readonly struct SKColor : IEquatable<SKColor>
{
	public static readonly SKColor Empty;

	private readonly uint color;

	public byte Alpha => (byte)((color >> 24) & 0xFF);

	public byte Red => (byte)((color >> 16) & 0xFF);

	public byte Green => (byte)((color >> 8) & 0xFF);

	public byte Blue => (byte)(color & 0xFF);

	public SKColor(uint P_0)
	{
		color = P_0;
	}

	public SKColor(byte P_0, byte P_1, byte P_2, byte P_3)
	{
		color = (uint)((P_3 << 24) | (P_0 << 16) | (P_1 << 8) | P_2);
	}

	public override string ToString()
	{
		return $"#{Alpha:x2}{Red:x2}{Green:x2}{Blue:x2}";
	}

	public bool Equals(SKColor P_0)
	{
		return P_0.color == color;
	}

	public override bool Equals(object P_0)
	{
		if (P_0 is SKColor sKColor)
		{
			return Equals(sKColor);
		}
		return false;
	}

	public static bool operator ==(SKColor P_0, SKColor P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKColor P_0, SKColor P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override int GetHashCode()
	{
		return color.GetHashCode();
	}

	public static implicit operator SKColor(uint P_0)
	{
		return new SKColor(P_0);
	}

	public static explicit operator uint(SKColor P_0)
	{
		return P_0.color;
	}
}

