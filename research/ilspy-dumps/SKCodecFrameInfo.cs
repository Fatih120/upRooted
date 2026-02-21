// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKCodecFrameInfo
using System;
using SkiaSharp;

public struct SKCodecFrameInfo : IEquatable<SKCodecFrameInfo>
{
	private int fRequiredFrame;

	private int fDuration;

	private byte fFullyReceived;

	private SKAlphaType fAlphaType;

	private SKCodecAnimationDisposalMethod fDisposalMethod;

	public readonly int Duration => fDuration;

	public readonly bool Equals(SKCodecFrameInfo P_0)
	{
		if (fRequiredFrame == P_0.fRequiredFrame && fDuration == P_0.fDuration && fFullyReceived == P_0.fFullyReceived && fAlphaType == P_0.fAlphaType)
		{
			return fDisposalMethod == P_0.fDisposalMethod;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKCodecFrameInfo sKCodecFrameInfo)
		{
			return Equals(sKCodecFrameInfo);
		}
		return false;
	}

	public static bool operator ==(SKCodecFrameInfo P_0, SKCodecFrameInfo P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKCodecFrameInfo P_0, SKCodecFrameInfo P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fRequiredFrame);
		hashCode.Add(fDuration);
		hashCode.Add(fFullyReceived);
		hashCode.Add(fAlphaType);
		hashCode.Add(fDisposalMethod);
		return hashCode.ToHashCode();
	}
}

