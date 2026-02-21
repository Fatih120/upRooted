// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPoint
using System;
using SkiaSharp;

public struct SKPoint(float P_0, float P_1) : IEquatable<SKPoint>
{
	public static readonly SKPoint Empty;

	private float x = P_0;

	private float y = P_1;

	public float X
	{
		readonly get
		{
			return x;
		}
		set
		{
			x = num;
		}
	}

	public float Y
	{
		readonly get
		{
			return y;
		}
		set
		{
			y = num;
		}
	}

	public override readonly string ToString()
	{
		return $"{{X={x}, Y={y}}}";
	}

	public static SKPoint operator +(SKPoint P_0, SKSizeI P_1)
	{
		return new SKPoint(P_0.x + (float)P_1.Width, P_0.y + (float)P_1.Height);
	}

	public static SKPoint operator +(SKPoint P_0, SKSize P_1)
	{
		return new SKPoint(P_0.x + P_1.Width, P_0.y + P_1.Height);
	}

	public static SKPoint operator +(SKPoint P_0, SKPointI P_1)
	{
		return new SKPoint(P_0.x + (float)P_1.X, P_0.y + (float)P_1.Y);
	}

	public static SKPoint operator +(SKPoint P_0, SKPoint P_1)
	{
		return new SKPoint(P_0.x + P_1.X, P_0.y + P_1.Y);
	}

	public static SKPoint operator -(SKPoint P_0, SKSizeI P_1)
	{
		return new SKPoint(P_0.X - (float)P_1.Width, P_0.Y - (float)P_1.Height);
	}

	public static SKPoint operator -(SKPoint P_0, SKSize P_1)
	{
		return new SKPoint(P_0.X - P_1.Width, P_0.Y - P_1.Height);
	}

	public static SKPoint operator -(SKPoint P_0, SKPointI P_1)
	{
		return new SKPoint(P_0.X - (float)P_1.X, P_0.Y - (float)P_1.Y);
	}

	public static SKPoint operator -(SKPoint P_0, SKPoint P_1)
	{
		return new SKPoint(P_0.X - P_1.X, P_0.Y - P_1.Y);
	}

	public readonly bool Equals(SKPoint P_0)
	{
		if (x == P_0.x)
		{
			return y == P_0.y;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKPoint sKPoint)
		{
			return Equals(sKPoint);
		}
		return false;
	}

	public static bool operator ==(SKPoint P_0, SKPoint P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKPoint P_0, SKPoint P_1)
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

