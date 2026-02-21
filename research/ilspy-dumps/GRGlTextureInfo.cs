// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRGlTextureInfo
using System;
using SkiaSharp;

public struct GRGlTextureInfo : IEquatable<GRGlTextureInfo>
{
	private uint fTarget;

	private uint fID;

	private uint fFormat;

	public GRGlTextureInfo(uint P_0, uint P_1)
	{
		fTarget = P_0;
		fID = P_1;
		fFormat = 0u;
	}

	public GRGlTextureInfo(uint P_0, uint P_1, uint P_2)
	{
		fTarget = P_0;
		fID = P_1;
		fFormat = P_2;
	}

	public readonly bool Equals(GRGlTextureInfo P_0)
	{
		if (fTarget == P_0.fTarget && fID == P_0.fID)
		{
			return fFormat == P_0.fFormat;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is GRGlTextureInfo gRGlTextureInfo)
		{
			return Equals(gRGlTextureInfo);
		}
		return false;
	}

	public static bool operator ==(GRGlTextureInfo P_0, GRGlTextureInfo P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(GRGlTextureInfo P_0, GRGlTextureInfo P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fTarget);
		hashCode.Add(fID);
		hashCode.Add(fFormat);
		return hashCode.ToHashCode();
	}
}

