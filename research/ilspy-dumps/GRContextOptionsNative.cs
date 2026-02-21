// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRContextOptionsNative
using System;
using SkiaSharp;

internal struct GRContextOptionsNative : IEquatable<GRContextOptionsNative>
{
	public byte fAvoidStencilBuffers;

	public int fRuntimeProgramCacheSize;

	public IntPtr fGlyphCacheTextureMaximumBytes;

	public byte fAllowPathMaskCaching;

	public byte fDoManualMipmapping;

	public int fBufferMapThreshold;

	public readonly bool Equals(GRContextOptionsNative obj)
	{
		if (fAvoidStencilBuffers == obj.fAvoidStencilBuffers && fRuntimeProgramCacheSize == obj.fRuntimeProgramCacheSize && fGlyphCacheTextureMaximumBytes == obj.fGlyphCacheTextureMaximumBytes && fAllowPathMaskCaching == obj.fAllowPathMaskCaching && fDoManualMipmapping == obj.fDoManualMipmapping)
		{
			return fBufferMapThreshold == obj.fBufferMapThreshold;
		}
		return false;
	}

	public override readonly bool Equals(object obj)
	{
		if (obj is GRContextOptionsNative obj2)
		{
			return Equals(obj2);
		}
		return false;
	}

	public static bool operator ==(GRContextOptionsNative left, GRContextOptionsNative right)
	{
		return left.Equals(right);
	}

	public static bool operator !=(GRContextOptionsNative left, GRContextOptionsNative right)
	{
		return !left.Equals(right);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fAvoidStencilBuffers);
		hashCode.Add(fRuntimeProgramCacheSize);
		hashCode.Add(fGlyphCacheTextureMaximumBytes);
		hashCode.Add(fAllowPathMaskCaching);
		hashCode.Add(fDoManualMipmapping);
		hashCode.Add(fBufferMapThreshold);
		return hashCode.ToHashCode();
	}
}

