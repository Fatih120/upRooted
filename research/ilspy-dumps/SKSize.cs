// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKSize
using System;
using SkiaSharp;

public struct SKSize(float P_0, float P_1) : IEquatable<SKSize>
{
	private float w = P_0;

	private float h = P_1;

	public readonly float Width => w;

	public readonly float Height => h;

	public override readonly string ToString()
	{
		return $"{{Width={w}, Height={h}}}";
	}

	public static SKSize operator +(SKSize P_0, SKSize P_1)
	{
		return new SKSize(P_0.Width + P_1.Width, P_0.Height + P_1.Height);
	}

	public static SKSize operator -(SKSize P_0, SKSize P_1)
	{
		return new SKSize(P_0.Width - P_1.Width, P_0.Height - P_1.Height);
	}

	public static explicit operator SKPoint(SKSize P_0)
	{
		return new SKPoint(P_0.Width, P_0.Height);
	}

	public static implicit operator SKSize(SKSizeI P_0)
	{
		return new SKSize(P_0.Width, P_0.Height);
	}

	public readonly bool Equals(SKSize P_0)
	{
		if (w == P_0.w)
		{
			return h == P_0.h;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKSize sKSize)
		{
			return Equals(sKSize);
		}
		return false;
	}

	public static bool operator ==(SKSize P_0, SKSize P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKSize P_0, SKSize P_1)
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

