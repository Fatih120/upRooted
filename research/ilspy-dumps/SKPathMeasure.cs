// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPathMeasure
using System;
using SkiaSharp;

public class SKPathMeasure : SKObject, ISKSkipObjectRegistration
{
	public float Length => SkiaApi.sk_pathmeasure_get_length(Handle);

	internal SKPathMeasure(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKPathMeasure(SKPath P_0, bool P_1 = false, float P_2 = 1f)
		: this(IntPtr.Zero, true)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("path");
		}
		Handle = SkiaApi.sk_pathmeasure_new_with_path(P_0.Handle, P_1, P_2);
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKPathMeasure instance.");
		}
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_pathmeasure_destroy(Handle);
	}

	public unsafe bool GetPositionAndTangent(float P_0, out SKPoint P_1, out SKPoint P_2)
	{
		fixed (SKPoint* ptr = &P_1)
		{
			fixed (SKPoint* ptr2 = &P_2)
			{
				return SkiaApi.sk_pathmeasure_get_pos_tan(Handle, P_0, ptr, ptr2);
			}
		}
	}
}

