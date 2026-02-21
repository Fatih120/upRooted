// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKCodecOptionsInternal
using System;
using SkiaSharp;

internal struct SKCodecOptionsInternal : IEquatable<SKCodecOptionsInternal>
{
	public SKZeroInitialized fZeroInitialized;

	public unsafe SKRectI* fSubset;

	public int fFrameIndex;

	public int fPriorFrame;

	public unsafe readonly bool Equals(SKCodecOptionsInternal P_0)
	{
		if (fZeroInitialized == P_0.fZeroInitialized && fSubset == P_0.fSubset && fFrameIndex == P_0.fFrameIndex)
		{
			return fPriorFrame == P_0.fPriorFrame;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKCodecOptionsInternal sKCodecOptionsInternal)
		{
			return Equals(sKCodecOptionsInternal);
		}
		return false;
	}

	public static bool operator ==(SKCodecOptionsInternal P_0, SKCodecOptionsInternal P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKCodecOptionsInternal P_0, SKCodecOptionsInternal P_1)
	{
		return !P_0.Equals(P_1);
	}

	public unsafe override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fZeroInitialized);
		hashCode.Add((void*)fSubset);
		hashCode.Add(fFrameIndex);
		hashCode.Add(fPriorFrame);
		return hashCode.ToHashCode();
	}
}

