// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPixmap
using System;
using SkiaSharp;

public class SKPixmap : SKObject
{
	internal SKObject pixelSource;

	public unsafe SKImageInfo Info
	{
		get
		{
			SKImageInfoNative sKImageInfoNative = default(SKImageInfoNative);
			SkiaApi.sk_pixmap_get_info(Handle, &sKImageInfoNative);
			return SKImageInfoNative.ToManaged(ref sKImageInfoNative);
		}
	}

	internal SKPixmap(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKPixmap()
		: this(SkiaApi.sk_pixmap_new(), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKPixmap instance.");
		}
	}

	public unsafe SKPixmap(SKImageInfo P_0, IntPtr P_1, int P_2)
		: this(IntPtr.Zero, true)
	{
		SKImageInfoNative sKImageInfoNative = SKImageInfoNative.FromManaged(ref P_0);
		Handle = SkiaApi.sk_pixmap_new_with_params(&sKImageInfoNative, (void*)P_1, (IntPtr)P_2);
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKPixmap instance.");
		}
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_pixmap_destructor(Handle);
	}

	protected override void DisposeManaged()
	{
		base.DisposeManaged();
		pixelSource = null;
	}

	public bool ScalePixels(SKPixmap P_0, SKFilterQuality P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("destination");
		}
		return SkiaApi.sk_pixmap_scale_pixels(Handle, P_0.Handle, P_1);
	}
}

