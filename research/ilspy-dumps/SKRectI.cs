// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKRectI
using System;
using SkiaSharp;

public struct SKRectI(int P_0, int P_1, int P_2, int P_3) : IEquatable<SKRectI>
{
	private int left = P_0;

	private int top = P_1;

	private int right = P_2;

	private int bottom = P_3;

	public readonly int Width => right - left;

	public readonly int Height => bottom - top;

	public readonly int Left => left;

	public readonly int Top => top;

	public readonly int Right => right;

	public readonly int Bottom => bottom;

	public override readonly string ToString()
	{
		return $"{{Left={Left},Top={Top},Width={Width},Height={Height}}}";
	}

	public readonly bool Equals(SKRectI P_0)
	{
		if (left == P_0.left && top == P_0.top && right == P_0.right)
		{
			return bottom == P_0.bottom;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKRectI sKRectI)
		{
			return Equals(sKRectI);
		}
		return false;
	}

	public static bool operator ==(SKRectI P_0, SKRectI P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKRectI P_0, SKRectI P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(left);
		hashCode.Add(top);
		hashCode.Add(right);
		hashCode.Add(bottom);
		return hashCode.ToHashCode();
	}
}

