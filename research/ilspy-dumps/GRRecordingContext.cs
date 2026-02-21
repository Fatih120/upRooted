// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRRecordingContext
using System;
using SkiaSharp;

public class GRRecordingContext : SKObject, ISKReferenceCounted
{
	internal GRRecordingContext(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public int GetMaxSurfaceSampleCount(SKColorType P_0)
	{
		return SkiaApi.gr_recording_context_get_max_surface_sample_count_for_color_type(Handle, P_0.ToNative());
	}
}

