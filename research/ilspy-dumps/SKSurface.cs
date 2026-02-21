// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKSurface
using System;
using System.Runtime.InteropServices;
using SkiaSharp;

public class SKSurface : SKObject, ISKReferenceCounted, ISKSkipObjectRegistration
{
	public SKCanvas Canvas => SKObject.OwnedBy(SKCanvas.GetObject(SkiaApi.sk_surface_get_canvas(Handle), false, false), this);

	internal SKSurface(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public static SKSurface Create(SKImageInfo P_0)
	{
		return Create(P_0, 0, null);
	}

	public static SKSurface Create(SKImageInfo P_0, SKSurfaceProperties P_1)
	{
		return Create(P_0, 0, P_1);
	}

	public unsafe static SKSurface Create(SKImageInfo P_0, int P_1, SKSurfaceProperties P_2)
	{
		SKImageInfoNative sKImageInfoNative = SKImageInfoNative.FromManaged(ref P_0);
		return GetObject(SkiaApi.sk_surface_new_raster(&sKImageInfoNative, (IntPtr)P_1, P_2?.Handle ?? IntPtr.Zero));
	}

	public static SKSurface Create(SKImageInfo P_0, IntPtr P_1, int P_2, SKSurfaceProperties P_3)
	{
		return Create(P_0, P_1, P_2, null, null, P_3);
	}

	public unsafe static SKSurface Create(SKImageInfo P_0, IntPtr P_1, int P_2, SKSurfaceReleaseDelegate P_3, object P_4, SKSurfaceProperties P_5)
	{
		SKImageInfoNative sKImageInfoNative = SKImageInfoNative.FromManaged(ref P_0);
		SKSurfaceReleaseDelegate sKSurfaceReleaseDelegate = ((P_3 != null && P_4 != null) ? ((SKSurfaceReleaseDelegate)delegate(IntPtr addr, object _)
		{
			P_3(addr, P_4);
		}) : P_3);
		GCHandle gCHandle;
		IntPtr intPtr;
		SKSurfaceRasterReleaseProxyDelegate sKSurfaceRasterReleaseProxyDelegate = DelegateProxies.Create(sKSurfaceReleaseDelegate, DelegateProxies.SKSurfaceReleaseDelegateProxy, out gCHandle, out intPtr);
		return GetObject(SkiaApi.sk_surface_new_raster_direct(&sKImageInfoNative, (void*)P_1, (IntPtr)P_2, sKSurfaceRasterReleaseProxyDelegate, (void*)intPtr, P_5?.Handle ?? IntPtr.Zero));
	}

	public static SKSurface Create(GRContext P_0, GRBackendRenderTarget P_1, GRSurfaceOrigin P_2, SKColorType P_3)
	{
		return Create((GRRecordingContext)P_0, P_1, P_2, P_3);
	}

	public static SKSurface Create(GRContext P_0, GRBackendRenderTarget P_1, GRSurfaceOrigin P_2, SKColorType P_3, SKColorSpace P_4)
	{
		return Create((GRRecordingContext)P_0, P_1, P_2, P_3, P_4);
	}

	public static SKSurface Create(GRContext P_0, GRBackendRenderTarget P_1, GRSurfaceOrigin P_2, SKColorType P_3, SKSurfaceProperties P_4)
	{
		return Create((GRRecordingContext)P_0, P_1, P_2, P_3, P_4);
	}

	public static SKSurface Create(GRRecordingContext P_0, GRBackendRenderTarget P_1, GRSurfaceOrigin P_2, SKColorType P_3)
	{
		return Create(P_0, P_1, P_2, P_3, null, null);
	}

	public static SKSurface Create(GRRecordingContext P_0, GRBackendRenderTarget P_1, GRSurfaceOrigin P_2, SKColorType P_3, SKColorSpace P_4)
	{
		return Create(P_0, P_1, P_2, P_3, P_4, null);
	}

	public static SKSurface Create(GRRecordingContext P_0, GRBackendRenderTarget P_1, GRSurfaceOrigin P_2, SKColorType P_3, SKSurfaceProperties P_4)
	{
		return Create(P_0, P_1, P_2, P_3, null, P_4);
	}

	public static SKSurface Create(GRRecordingContext P_0, GRBackendRenderTarget P_1, GRSurfaceOrigin P_2, SKColorType P_3, SKColorSpace P_4, SKSurfaceProperties P_5)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("context");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("renderTarget");
		}
		return GetObject(SkiaApi.sk_surface_new_backend_render_target(P_0.Handle, P_1.Handle, P_2, P_3.ToNative(), P_4?.Handle ?? IntPtr.Zero, P_5?.Handle ?? IntPtr.Zero));
	}

	public static SKSurface Create(GRContext P_0, GRBackendTexture P_1, GRSurfaceOrigin P_2, SKColorType P_3)
	{
		return Create((GRRecordingContext)P_0, P_1, P_2, P_3);
	}

	public static SKSurface Create(GRRecordingContext P_0, GRBackendTexture P_1, GRSurfaceOrigin P_2, SKColorType P_3)
	{
		return Create(P_0, P_1, P_2, 0, P_3, null, null);
	}

	public static SKSurface Create(GRRecordingContext P_0, GRBackendTexture P_1, GRSurfaceOrigin P_2, int P_3, SKColorType P_4, SKColorSpace P_5, SKSurfaceProperties P_6)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("context");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("texture");
		}
		return GetObject(SkiaApi.sk_surface_new_backend_texture(P_0.Handle, P_1.Handle, P_2, P_3, P_4.ToNative(), P_5?.Handle ?? IntPtr.Zero, P_6?.Handle ?? IntPtr.Zero));
	}

	public static SKSurface Create(GRContext P_0, bool P_1, SKImageInfo P_2, SKSurfaceProperties P_3)
	{
		return Create((GRRecordingContext)P_0, P_1, P_2, P_3);
	}

	public static SKSurface Create(GRRecordingContext P_0, bool P_1, SKImageInfo P_2, SKSurfaceProperties P_3)
	{
		return Create(P_0, P_1, P_2, 0, GRSurfaceOrigin.BottomLeft, P_3, false);
	}

	public unsafe static SKSurface Create(GRRecordingContext P_0, bool P_1, SKImageInfo P_2, int P_3, GRSurfaceOrigin P_4, SKSurfaceProperties P_5, bool P_6)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("context");
		}
		SKImageInfoNative sKImageInfoNative = SKImageInfoNative.FromManaged(ref P_2);
		return GetObject(SkiaApi.sk_surface_new_render_target(P_0.Handle, P_1, &sKImageInfoNative, P_3, P_4, P_5?.Handle ?? IntPtr.Zero, P_6));
	}

	public SKImage Snapshot()
	{
		return SKImage.GetObject(SkiaApi.sk_surface_new_image_snapshot(Handle));
	}

	public void Draw(SKCanvas P_0, float P_1, float P_2, SKPaint P_3)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("canvas");
		}
		SkiaApi.sk_surface_draw(Handle, P_0.Handle, P_1, P_2, P_3?.Handle ?? IntPtr.Zero);
	}

	public void Flush()
	{
		Flush(true);
	}

	public void Flush(bool P_0, bool P_1 = false)
	{
		if (P_0)
		{
			SkiaApi.sk_surface_flush_and_submit(Handle, P_1);
		}
		else
		{
			SkiaApi.sk_surface_flush(Handle);
		}
	}

	internal static SKSurface GetObject(IntPtr P_0)
	{
		if (!(P_0 == IntPtr.Zero))
		{
			return new SKSurface(P_0, true);
		}
		return null;
	}
}

