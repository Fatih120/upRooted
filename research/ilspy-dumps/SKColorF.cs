// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKColorF
using System;
using SkiaSharp;

public readonly struct SKColorF(float P_0, float P_1, float P_2, float P_3) : IEquatable<SKColorF>
{
	private readonly float fR = P_0;

	private readonly float fG = P_1;

	private readonly float fB = P_2;

	private readonly float fA = P_3;

	public override string ToString()
	{
		return ((SKColor)this).ToString();
	}

	public unsafe static implicit operator SKColorF(SKColor P_0)
	{
		SKColorF result = default(SKColorF);
		SkiaApi.sk_color4f_from_color((uint)P_0, &result);
		return result;
	}

	public unsafe static explicit operator SKColor(SKColorF P_0)
	{
		return SkiaApi.sk_color4f_to_color(&P_0);
	}

	public bool Equals(SKColorF P_0)
	{
		if (fR == P_0.fR && fG == P_0.fG && fB == P_0.fB)
		{
			return fA == P_0.fA;
		}
		return false;
	}

	public override bool Equals(object P_0)
	{
		if (P_0 is SKColorF sKColorF)
		{
			return Equals(sKColorF);
		}
		return false;
	}

	public static bool operator ==(SKColorF P_0, SKColorF P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKColorF P_0, SKColorF P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fR);
		hashCode.Add(fG);
		hashCode.Add(fB);
		hashCode.Add(fA);
		return hashCode.ToHashCode();
	}
}

