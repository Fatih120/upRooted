// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKRotationScaleMatrix
using System;
using SkiaSharp;

public struct SKRotationScaleMatrix : IEquatable<SKRotationScaleMatrix>
{
	private float fSCos;

	private float fSSin;

	private float fTX;

	private float fTY;

	public readonly bool Equals(SKRotationScaleMatrix P_0)
	{
		if (fSCos == P_0.fSCos && fSSin == P_0.fSSin && fTX == P_0.fTX)
		{
			return fTY == P_0.fTY;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKRotationScaleMatrix sKRotationScaleMatrix)
		{
			return Equals(sKRotationScaleMatrix);
		}
		return false;
	}

	public static bool operator ==(SKRotationScaleMatrix P_0, SKRotationScaleMatrix P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKRotationScaleMatrix P_0, SKRotationScaleMatrix P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fSCos);
		hashCode.Add(fSSin);
		hashCode.Add(fTX);
		hashCode.Add(fTY);
		return hashCode.ToHashCode();
	}

	public SKRotationScaleMatrix(float P_0, float P_1, float P_2, float P_3)
	{
		fSCos = P_0;
		fSSin = P_1;
		fTX = P_2;
		fTY = P_3;
	}
}

