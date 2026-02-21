// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRGlFramebufferInfo
using System;
using SkiaSharp;

public struct GRGlFramebufferInfo(uint P_0, uint P_1) : IEquatable<GRGlFramebufferInfo>
{
	private uint fFBOID = P_0;

	private uint fFormat = P_1;

	public readonly bool Equals(GRGlFramebufferInfo P_0)
	{
		if (fFBOID == P_0.fFBOID)
		{
			return fFormat == P_0.fFormat;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is GRGlFramebufferInfo gRGlFramebufferInfo)
		{
			return Equals(gRGlFramebufferInfo);
		}
		return false;
	}

	public static bool operator ==(GRGlFramebufferInfo P_0, GRGlFramebufferInfo P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(GRGlFramebufferInfo P_0, GRGlFramebufferInfo P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fFBOID);
		hashCode.Add(fFormat);
		return hashCode.ToHashCode();
	}
}

