// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKImage
using System;
using System.Runtime.InteropServices;
using SkiaSharp;

public class SKImage : SKObject, ISKReferenceCounted
{
	public int Width => SkiaApi.sk_image_get_width(Handle);

	public int Height => SkiaApi.sk_image_get_height(Handle);

	internal SKImage(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public static SKImage FromPixels(SKImageInfo P_0, IntPtr P_1, int P_2)
	{
		using SKPixmap sKPixmap = new SKPixmap(P_0, P_1, P_2);
		return FromPixels(sKPixmap, null, null);
	}

	public static SKImage FromPixels(SKPixmap P_0, SKImageRasterReleaseDelegate P_1)
	{
		return FromPixels(P_0, P_1, null);
	}

	public unsafe static SKImage FromPixels(SKPixmap P_0, SKImageRasterReleaseDelegate P_1, object P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("pixmap");
		}
		SKImageRasterReleaseDelegate sKImageRasterReleaseDelegate = ((P_1 != null && P_2 != null) ? ((SKImageRasterReleaseDelegate)delegate(IntPtr addr, object _)
		{
			P_1(addr, P_2);
		}) : P_1);
		GCHandle gCHandle;
		IntPtr intPtr;
		SKImageRasterReleaseProxyDelegate sKImageRasterReleaseProxyDelegate = DelegateProxies.Create(sKImageRasterReleaseDelegate, DelegateProxies.SKImageRasterReleaseDelegateProxy, out gCHandle, out intPtr);
		return GetObject(SkiaApi.sk_image_new_raster(P_0.Handle, sKImageRasterReleaseProxyDelegate, (void*)intPtr));
	}

	public static SKImage FromEncodedData(SKData P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("data");
		}
		IntPtr intPtr = SkiaApi.sk_image_new_from_encoded(P_0.Handle);
		return GetObject(intPtr);
	}

	public static SKImage FromEncodedData(byte[] P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("data");
		}
		if (P_0.Length == 0)
		{
			throw new ArgumentException("The data buffer was empty.");
		}
		using SKData sKData = SKData.CreateCopy(P_0);
		return FromEncodedData(sKData);
	}

	public static SKImage FromBitmap(SKBitmap P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("bitmap");
		}
		SKImage result = GetObject(SkiaApi.sk_image_new_from_bitmap(P_0.Handle));
		GC.KeepAlive(P_0);
		return result;
	}

	public static SKImage FromTexture(GRContext P_0, GRBackendTexture P_1, GRSurfaceOrigin P_2, SKColorType P_3)
	{
		return FromTexture((GRRecordingContext)P_0, P_1, P_2, P_3);
	}

	public static SKImage FromTexture(GRRecordingContext P_0, GRBackendTexture P_1, GRSurfaceOrigin P_2, SKColorType P_3)
	{
		return FromTexture(P_0, P_1, P_2, P_3, SKAlphaType.Premul, null, null, null);
	}

	public unsafe static SKImage FromTexture(GRRecordingContext P_0, GRBackendTexture P_1, GRSurfaceOrigin P_2, SKColorType P_3, SKAlphaType P_4, SKColorSpace P_5, SKImageTextureReleaseDelegate P_6, object P_7)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("context");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("texture");
		}
		IntPtr intPtr = P_5?.Handle ?? IntPtr.Zero;
		SKImageTextureReleaseDelegate sKImageTextureReleaseDelegate = ((P_6 != null && P_7 != null) ? ((SKImageTextureReleaseDelegate)delegate
		{
			P_6(P_7);
		}) : P_6);
		GCHandle gCHandle;
		IntPtr intPtr2;
		SKImageTextureReleaseProxyDelegate sKImageTextureReleaseProxyDelegate = DelegateProxies.Create(sKImageTextureReleaseDelegate, DelegateProxies.SKImageTextureReleaseDelegateProxy, out gCHandle, out intPtr2);
		return GetObject(SkiaApi.sk_image_new_from_texture(P_0.Handle, P_1.Handle, P_2, P_3.ToNative(), P_4, intPtr, sKImageTextureReleaseProxyDelegate, (void*)intPtr2));
	}

	public SKData Encode()
	{
		return SKData.GetObject(SkiaApi.sk_image_encode(Handle));
	}

	public SKData Encode(SKEncodedImageFormat P_0, int P_1)
	{
		return SKData.GetObject(SkiaApi.sk_image_encode_specific(Handle, P_0, P_1));
	}

	public unsafe SKShader ToShader(SKShaderTileMode P_0, SKShaderTileMode P_1, SKMatrix P_2)
	{
		return SKShader.GetObject(SkiaApi.sk_image_make_shader(Handle, P_0, P_1, &P_2));
	}

	public unsafe bool ReadPixels(SKImageInfo P_0, IntPtr P_1, int P_2, int P_3, int P_4, SKImageCachingHint P_5)
	{
		SKImageInfoNative sKImageInfoNative = SKImageInfoNative.FromManaged(ref P_0);
		bool result = SkiaApi.sk_image_read_pixels(Handle, &sKImageInfoNative, (void*)P_1, (IntPtr)P_2, P_3, P_4, P_5);
		GC.KeepAlive(this);
		return result;
	}

	public bool ScalePixels(SKPixmap P_0, SKFilterQuality P_1)
	{
		return ScalePixels(P_0, P_1, SKImageCachingHint.Allow);
	}

	public bool ScalePixels(SKPixmap P_0, SKFilterQuality P_1, SKImageCachingHint P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("dst");
		}
		return SkiaApi.sk_image_scale_pixels(Handle, P_0.Handle, P_1, P_2);
	}

	internal static SKImage GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (IntPtr h, bool o) => new SKImage(h, o));
	}
}

