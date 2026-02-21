// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GrVkYcbcrConversionInfo
using System;
using SkiaSharp;

public struct GrVkYcbcrConversionInfo : IEquatable<GrVkYcbcrConversionInfo>
{
	private uint fFormat;

	private ulong fExternalFormat;

	private uint fYcbcrModel;

	private uint fYcbcrRange;

	private uint fXChromaOffset;

	private uint fYChromaOffset;

	private uint fChromaFilter;

	private uint fForceExplicitReconstruction;

	private uint fFormatFeatures;

	public readonly bool Equals(GrVkYcbcrConversionInfo P_0)
	{
		if (fFormat == P_0.fFormat && fExternalFormat == P_0.fExternalFormat && fYcbcrModel == P_0.fYcbcrModel && fYcbcrRange == P_0.fYcbcrRange && fXChromaOffset == P_0.fXChromaOffset && fYChromaOffset == P_0.fYChromaOffset && fChromaFilter == P_0.fChromaFilter && fForceExplicitReconstruction == P_0.fForceExplicitReconstruction)
		{
			return fFormatFeatures == P_0.fFormatFeatures;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is GrVkYcbcrConversionInfo grVkYcbcrConversionInfo)
		{
			return Equals(grVkYcbcrConversionInfo);
		}
		return false;
	}

	public static bool operator ==(GrVkYcbcrConversionInfo P_0, GrVkYcbcrConversionInfo P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(GrVkYcbcrConversionInfo P_0, GrVkYcbcrConversionInfo P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fFormat);
		hashCode.Add(fExternalFormat);
		hashCode.Add(fYcbcrModel);
		hashCode.Add(fYcbcrRange);
		hashCode.Add(fXChromaOffset);
		hashCode.Add(fYChromaOffset);
		hashCode.Add(fChromaFilter);
		hashCode.Add(fForceExplicitReconstruction);
		hashCode.Add(fFormatFeatures);
		return hashCode.ToHashCode();
	}
}

