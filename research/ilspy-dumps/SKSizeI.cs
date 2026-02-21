// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKSizeI
using System;
using SkiaSharp;

public struct SKSizeI(int P_0, int P_1) : IEquatable<SKSizeI>
{
	private int w = P_0;

	private int h = P_1;

	public readonly int Width => w;

	public readonly int Height => h;

	public override readonly string ToString()
	{
		return $"{{Width={w}, Height={h}}}";
	}

	public static SKSizeI operator +(SKSizeI P_0, SKSizeI P_1)
	{
		return new SKSizeI(P_0.Width + P_1.Width, P_0.Height + P_1.Height);
	}

	public static SKSizeI operator -(SKSizeI P_0, SKSizeI P_1)
	{
		return new SKSizeI(P_0.Width - P_1.Width, P_0.Height - P_1.Height);
	}

	public static explicit operator SKPointI(SKSizeI P_0)
	{
		return new SKPointI(P_0.Width, P_0.Height);
	}

	public readonly bool Equals(SKSizeI P_0)
	{
		if (w == P_0.w)
		{
			return h == P_0.h;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKSizeI sKSizeI)
		{
			return Equals(sKSizeI);
		}
		return false;
	}

	public static bool operator ==(SKSizeI P_0, SKSizeI P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKSizeI P_0, SKSizeI P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(w);
		hashCode.Add(h);
		return hashCode.ToHashCode();
	}
}

