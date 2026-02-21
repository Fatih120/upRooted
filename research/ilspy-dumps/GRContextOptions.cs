// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRContextOptions
using System;
using System.Runtime.CompilerServices;
using SkiaSharp;

public class GRContextOptions
{
	[CompilerGenerated]
	private bool _003CAvoidStencilBuffers_003Ek__BackingField;

	public bool AvoidStencilBuffers
	{
		[CompilerGenerated]
		get
		{
			return _003CAvoidStencilBuffers_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CAvoidStencilBuffers_003Ek__BackingField = flag;
		}
	}

	public int RuntimeProgramCacheSize { get; } = 256;

	public int GlyphCacheTextureMaximumBytes { get; } = 8388608;

	public bool AllowPathMaskCaching { get; } = true;

	public bool DoManualMipmapping { get; }

	public int BufferMapThreshold { get; } = -1;

	internal GRContextOptionsNative ToNative()
	{
		return new GRContextOptionsNative
		{
			fAllowPathMaskCaching = (AllowPathMaskCaching ? ((byte)1) : ((byte)0)),
			fAvoidStencilBuffers = (AvoidStencilBuffers ? ((byte)1) : ((byte)0)),
			fBufferMapThreshold = BufferMapThreshold,
			fDoManualMipmapping = (DoManualMipmapping ? ((byte)1) : ((byte)0)),
			fGlyphCacheTextureMaximumBytes = (IntPtr)GlyphCacheTextureMaximumBytes,
			fRuntimeProgramCacheSize = RuntimeProgramCacheSize
		};
	}
}

