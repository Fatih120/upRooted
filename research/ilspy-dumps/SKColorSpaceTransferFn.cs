// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKColorSpaceTransferFn
using System;
using SkiaSharp;

public struct SKColorSpaceTransferFn : IEquatable<SKColorSpaceTransferFn>
{
	private float fG;

	private float fA;

	private float fB;

	private float fC;

	private float fD;

	private float fE;

	private float fF;

	public unsafe static SKColorSpaceTransferFn Srgb
	{
		get
		{
			SKColorSpaceTransferFn result = default(SKColorSpaceTransferFn);
			SkiaApi.sk_colorspace_transfer_fn_named_srgb(&result);
			return result;
		}
	}

	public unsafe static SKColorSpaceTransferFn Linear
	{
		get
		{
			SKColorSpaceTransferFn result = default(SKColorSpaceTransferFn);
			SkiaApi.sk_colorspace_transfer_fn_named_linear(&result);
			return result;
		}
	}

	public readonly bool Equals(SKColorSpaceTransferFn P_0)
	{
		if (fG == P_0.fG && fA == P_0.fA && fB == P_0.fB && fC == P_0.fC && fD == P_0.fD && fE == P_0.fE)
		{
			return fF == P_0.fF;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKColorSpaceTransferFn sKColorSpaceTransferFn)
		{
			return Equals(sKColorSpaceTransferFn);
		}
		return false;
	}

	public static bool operator ==(SKColorSpaceTransferFn P_0, SKColorSpaceTransferFn P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKColorSpaceTransferFn P_0, SKColorSpaceTransferFn P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fG);
		hashCode.Add(fA);
		hashCode.Add(fB);
		hashCode.Add(fC);
		hashCode.Add(fD);
		hashCode.Add(fE);
		hashCode.Add(fF);
		return hashCode.ToHashCode();
	}
}

