// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKFontMetrics
using System;
using SkiaSharp;

public struct SKFontMetrics : IEquatable<SKFontMetrics>
{
	private uint fFlags;

	private float fTop;

	private float fAscent;

	private float fDescent;

	private float fBottom;

	private float fLeading;

	private float fAvgCharWidth;

	private float fMaxCharWidth;

	private float fXMin;

	private float fXMax;

	private float fXHeight;

	private float fCapHeight;

	private float fUnderlineThickness;

	private float fUnderlinePosition;

	private float fStrikeoutThickness;

	private float fStrikeoutPosition;

	public readonly float Top => fTop;

	public readonly float Ascent => fAscent;

	public readonly float Descent => fDescent;

	public readonly float Bottom => fBottom;

	public readonly float Leading => fLeading;

	public readonly bool Equals(SKFontMetrics P_0)
	{
		if (fFlags == P_0.fFlags && fTop == P_0.fTop && fAscent == P_0.fAscent && fDescent == P_0.fDescent && fBottom == P_0.fBottom && fLeading == P_0.fLeading && fAvgCharWidth == P_0.fAvgCharWidth && fMaxCharWidth == P_0.fMaxCharWidth && fXMin == P_0.fXMin && fXMax == P_0.fXMax && fXHeight == P_0.fXHeight && fCapHeight == P_0.fCapHeight && fUnderlineThickness == P_0.fUnderlineThickness && fUnderlinePosition == P_0.fUnderlinePosition && fStrikeoutThickness == P_0.fStrikeoutThickness)
		{
			return fStrikeoutPosition == P_0.fStrikeoutPosition;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKFontMetrics sKFontMetrics)
		{
			return Equals(sKFontMetrics);
		}
		return false;
	}

	public static bool operator ==(SKFontMetrics P_0, SKFontMetrics P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKFontMetrics P_0, SKFontMetrics P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fFlags);
		hashCode.Add(fTop);
		hashCode.Add(fAscent);
		hashCode.Add(fDescent);
		hashCode.Add(fBottom);
		hashCode.Add(fLeading);
		hashCode.Add(fAvgCharWidth);
		hashCode.Add(fMaxCharWidth);
		hashCode.Add(fXMin);
		hashCode.Add(fXMax);
		hashCode.Add(fXHeight);
		hashCode.Add(fCapHeight);
		hashCode.Add(fUnderlineThickness);
		hashCode.Add(fUnderlinePosition);
		hashCode.Add(fStrikeoutThickness);
		hashCode.Add(fStrikeoutPosition);
		return hashCode.ToHashCode();
	}
}

