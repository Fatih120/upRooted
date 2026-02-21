// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPointI
using System;
using SkiaSharp;

public struct SKPointI(int P_0, int P_1) : IEquatable<SKPointI>
{
	private int x = P_0;

	private int y = P_1;

	public readonly int X => x;

	public readonly int Y => y;

	public override readonly string ToString()
	{
		return $"{{X={x},Y={y}}}";
	}

	public static SKPointI operator +(SKPointI P_0, SKSizeI P_1)
	{
		return new SKPointI(P_0.X + P_1.Width, P_0.Y + P_1.Height);
	}

	public static SKPointI operator +(SKPointI P_0, SKPointI P_1)
	{
		return new SKPointI(P_0.X + P_1.X, P_0.Y + P_1.Y);
	}

	public static SKPointI operator -(SKPointI P_0, SKSizeI P_1)
	{
		return new SKPointI(P_0.X - P_1.Width, P_0.Y - P_1.Height);
	}

	public static SKPointI operator -(SKPointI P_0, SKPointI P_1)
	{
		return new SKPointI(P_0.X - P_1.X, P_0.Y - P_1.Y);
	}

	public static explicit operator SKSizeI(SKPointI P_0)
	{
		return new SKSizeI(P_0.X, P_0.Y);
	}

	public static implicit operator SKPoint(SKPointI P_0)
	{
		return new SKPoint(P_0.X, P_0.Y);
	}

	public readonly bool Equals(SKPointI P_0)
	{
		if (x == P_0.x)
		{
			return y == P_0.y;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKPointI sKPointI)
		{
			return Equals(sKPointI);
		}
		return false;
	}

	public static bool operator ==(SKPointI P_0, SKPointI P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKPointI P_0, SKPointI P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(x);
		hashCode.Add(y);
		return hashCode.ToHashCode();
	}
}

