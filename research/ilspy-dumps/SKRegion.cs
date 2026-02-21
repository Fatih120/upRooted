// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKRegion
using System;
using SkiaSharp;

public class SKRegion : SKObject, ISKSkipObjectRegistration
{
	public bool IsEmpty => SkiaApi.sk_region_is_empty(Handle);

	public unsafe SKRectI Bounds
	{
		get
		{
			SKRectI result = default(SKRectI);
			SkiaApi.sk_region_get_bounds(Handle, &result);
			return result;
		}
	}

	internal SKRegion(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKRegion()
		: this(SkiaApi.sk_region_new(), true)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_region_delete(Handle);
	}

	public unsafe bool Intersects(SKRectI P_0)
	{
		return SkiaApi.sk_region_intersects_rect(Handle, &P_0);
	}

	public void SetEmpty()
	{
		SkiaApi.sk_region_set_empty(Handle);
	}

	public unsafe bool Op(SKRectI P_0, SKRegionOperation P_1)
	{
		return SkiaApi.sk_region_op_rect(Handle, &P_0, P_1);
	}

	public bool Op(int P_0, int P_1, int P_2, int P_3, SKRegionOperation P_4)
	{
		return Op(new SKRectI(P_0, P_1, P_2, P_3), P_4);
	}
}

