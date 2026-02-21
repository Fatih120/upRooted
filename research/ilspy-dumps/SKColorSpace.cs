// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKColorSpace
using System;
using SkiaSharp;

public class SKColorSpace : SKObject, ISKNonVirtualReferenceCounted, ISKReferenceCounted
{
	private sealed class SKColorSpaceStatic : SKColorSpace
	{
		internal SKColorSpaceStatic(IntPtr P_0)
			: base(P_0, false)
		{
		}

		protected override void Dispose(bool P_0)
		{
		}
	}

	private static readonly SKColorSpace srgb;

	private static readonly SKColorSpace srgbLinear;

	static SKColorSpace()
	{
		srgb = new SKColorSpaceStatic(SkiaApi.sk_colorspace_new_srgb());
		srgbLinear = new SKColorSpaceStatic(SkiaApi.sk_colorspace_new_srgb_linear());
	}

	internal static void EnsureStaticInstanceAreInitialized()
	{
	}

	internal SKColorSpace(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	void ISKNonVirtualReferenceCounted.UnreferenceNative()
	{
		SkiaApi.sk_colorspace_unref(Handle);
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public static SKColorSpace CreateSrgb()
	{
		return srgb;
	}

	public unsafe static SKColorSpace CreateRgb(SKColorSpaceTransferFn P_0, SKColorSpaceXyz P_1)
	{
		return GetObject(SkiaApi.sk_colorspace_new_rgb(&P_0, &P_1));
	}

	internal static SKColorSpace GetObject(IntPtr P_0, bool P_1 = true, bool P_2 = true)
	{
		return SKObject.GetOrAddObject(P_0, P_1, P_2, (IntPtr h, bool o) => new SKColorSpace(h, o));
	}
}

