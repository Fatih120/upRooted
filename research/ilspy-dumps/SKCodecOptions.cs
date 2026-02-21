// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKCodecOptions
using System;
using System.Runtime.CompilerServices;
using SkiaSharp;

public struct SKCodecOptions : IEquatable<SKCodecOptions>
{
	public static readonly SKCodecOptions Default;

	[CompilerGenerated]
	private SKZeroInitialized _003CZeroInitialized_003Ek__BackingField;

	[CompilerGenerated]
	private SKRectI? _003CSubset_003Ek__BackingField;

	[CompilerGenerated]
	private int _003CFrameIndex_003Ek__BackingField;

	[CompilerGenerated]
	private int _003CPriorFrame_003Ek__BackingField;

	public SKZeroInitialized ZeroInitialized
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CZeroInitialized_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CZeroInitialized_003Ek__BackingField = sKZeroInitialized;
		}
	}

	public SKRectI? Subset
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CSubset_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSubset_003Ek__BackingField = sKRectI;
		}
	}

	public readonly bool HasSubset => Subset.HasValue;

	public int FrameIndex
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CFrameIndex_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFrameIndex_003Ek__BackingField = num;
		}
	}

	public int PriorFrame
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CPriorFrame_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPriorFrame_003Ek__BackingField = num;
		}
	}

	static SKCodecOptions()
	{
		Default = new SKCodecOptions(SKZeroInitialized.No);
	}

	public SKCodecOptions(SKZeroInitialized P_0)
	{
		ZeroInitialized = P_0;
		Subset = null;
		FrameIndex = 0;
		PriorFrame = -1;
	}

	public SKCodecOptions(int P_0, int P_1)
	{
		ZeroInitialized = SKZeroInitialized.No;
		Subset = null;
		FrameIndex = P_0;
		PriorFrame = P_1;
	}

	public readonly bool Equals(SKCodecOptions P_0)
	{
		if (ZeroInitialized == P_0.ZeroInitialized)
		{
			SKRectI? subset = Subset;
			SKRectI? subset2 = P_0.Subset;
			if (subset.HasValue == subset2.HasValue && (!subset.HasValue || subset.GetValueOrDefault() == subset2.GetValueOrDefault()) && FrameIndex == P_0.FrameIndex)
			{
				return PriorFrame == P_0.PriorFrame;
			}
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKCodecOptions sKCodecOptions)
		{
			return Equals(sKCodecOptions);
		}
		return false;
	}

	public static bool operator ==(SKCodecOptions P_0, SKCodecOptions P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKCodecOptions P_0, SKCodecOptions P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(ZeroInitialized);
		hashCode.Add(Subset);
		hashCode.Add(FrameIndex);
		hashCode.Add(PriorFrame);
		return hashCode.ToHashCode();
	}
}

