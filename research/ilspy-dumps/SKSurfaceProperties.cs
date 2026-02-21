// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKSurfaceProperties
using System;
using SkiaSharp;

public class SKSurfaceProperties : SKObject
{
	internal SKSurfaceProperties(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKSurfaceProperties(SKPixelGeometry P_0)
		: this(0u, P_0)
	{
	}

	public SKSurfaceProperties(uint P_0, SKPixelGeometry P_1)
		: this(SkiaApi.sk_surfaceprops_new(P_0, P_1), true)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_surfaceprops_delete(Handle);
	}
}

