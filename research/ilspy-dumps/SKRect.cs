// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKRect
using System;
using SkiaSharp;

public struct SKRect(float P_0, float P_1, float P_2, float P_3) : IEquatable<SKRect>
{
	public static readonly SKRect Empty;

	private float left = P_0;

	private float top = P_1;

	private float right = P_2;

	private float bottom = P_3;

	public readonly float Width => right - left;

	public readonly float Height => bottom - top;

	public readonly float Left => left;

	public readonly float Top => top;

	public readonly float Right => right;

	public readonly float Bottom => bottom;

	public void Inflate(float P_0, float P_1)
	{
		left -= P_0;
		top -= P_1;
		right += P_0;
		bottom += P_1;
	}

	public static SKRect Union(SKRect P_0, SKRect P_1)
	{
		return new SKRect(Math.Min(P_0.left, P_1.left), Math.Min(P_0.top, P_1.top), Math.Max(P_0.right, P_1.right), Math.Max(P_0.bottom, P_1.bottom));
	}

	public void Union(SKRect P_0)
	{
		this = Union(this, P_0);
	}

	public static implicit operator SKRect(SKRectI P_0)
	{
		return new SKRect(P_0.Left, P_0.Top, P_0.Right, P_0.Bottom);
	}

	public void Offset(float P_0, float P_1)
	{
		left += P_0;
		top += P_1;
		right += P_0;
		bottom += P_1;
	}

	public override readonly string ToString()
	{
		return $"{{Left={Left},Top={Top},Width={Width},Height={Height}}}";
	}

	public readonly bool Equals(SKRect P_0)
	{
		if (left == P_0.left && top == P_0.top && right == P_0.right)
		{
			return bottom == P_0.bottom;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKRect sKRect)
		{
			return Equals(sKRect);
		}
		return false;
	}

	public static bool operator ==(SKRect P_0, SKRect P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKRect P_0, SKRect P_1)
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

