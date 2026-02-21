// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPoint3
using System;
using SkiaSharp;

public struct SKPoint3(float P_0, float P_1, float P_2) : IEquatable<SKPoint3>
{
	private float x = P_0;

	private float y = P_1;

	private float z = P_2;

	public readonly float X => x;

	public readonly float Y => y;

	public readonly float Z => z;

	public override readonly string ToString()
	{
		return $"{{X={x}, Y={y}, Z={z}}}";
	}

	public static SKPoint3 operator +(SKPoint3 P_0, SKPoint3 P_1)
	{
		return new SKPoint3(P_0.X + P_1.X, P_0.Y + P_1.Y, P_0.Z + P_1.Z);
	}

	public static SKPoint3 operator -(SKPoint3 P_0, SKPoint3 P_1)
	{
		return new SKPoint3(P_0.X - P_1.X, P_0.Y - P_1.Y, P_0.Z - P_1.Z);
	}

	public readonly bool Equals(SKPoint3 P_0)
	{
		if (x == P_0.x && y == P_0.y)
		{
			return z == P_0.z;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKPoint3 sKPoint)
		{
			return Equals(sKPoint);
		}
		return false;
	}

	public static bool operator ==(SKPoint3 P_0, SKPoint3 P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKPoint3 P_0, SKPoint3 P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(x);
		hashCode.Add(y);
		hashCode.Add(z);
		return hashCode.ToHashCode();
	}
}

