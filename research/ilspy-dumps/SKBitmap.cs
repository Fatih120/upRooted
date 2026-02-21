// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKBitmap
using System;
using System.IO;
using System.Runtime.InteropServices;
using SkiaSharp;

public class SKBitmap : SKObject, ISKSkipObjectRegistration
{
	public unsafe SKImageInfo Info
	{
		get
		{
			SKImageInfoNative sKImageInfoNative = default(SKImageInfoNative);
			SkiaApi.sk_bitmap_get_info(Handle, &sKImageInfoNative);
			return SKImageInfoNative.ToManaged(ref sKImageInfoNative);
		}
	}

	public int Width => Info.Width;

	public int Height => Info.Height;

	public SKColorType ColorType => Info.ColorType;

	public SKAlphaType AlphaType => Info.AlphaType;

	public int RowBytes => (int)SkiaApi.sk_bitmap_get_row_bytes(Handle);

	internal SKBitmap(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKBitmap()
		: this(SkiaApi.sk_bitmap_new(), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKBitmap instance.");
		}
	}

	public SKBitmap(int P_0, int P_1, bool P_2 = false)
		: this(P_0, P_1, SKImageInfo.PlatformColorType, P_2 ? SKAlphaType.Opaque : SKAlphaType.Premul)
	{
	}

	public SKBitmap(int P_0, int P_1, SKColorType P_2, SKAlphaType P_3)
		: this(new SKImageInfo(P_0, P_1, P_2, P_3))
	{
	}

	public SKBitmap(SKImageInfo P_0)
		: this(P_0, P_0.RowBytes)
	{
	}

	public SKBitmap(SKImageInfo P_0, int P_1)
		: this()
	{
		if (!TryAllocPixels(P_0, P_1))
		{
			throw new Exception("Unable to allocate pixels for the bitmap.");
		}
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_bitmap_destructor(Handle);
	}

	public bool TryAllocPixels(SKImageInfo P_0)
	{
		return TryAllocPixels(P_0, P_0.RowBytes);
	}

	public unsafe bool TryAllocPixels(SKImageInfo P_0, int P_1)
	{
		SKImageInfoNative sKImageInfoNative = SKImageInfoNative.FromManaged(ref P_0);
		return SkiaApi.sk_bitmap_try_alloc_pixels(Handle, &sKImageInfoNative, (IntPtr)P_1);
	}

	public void SetImmutable()
	{
		SkiaApi.sk_bitmap_set_immutable(Handle);
	}

	public void Erase(SKColor P_0)
	{
		SkiaApi.sk_bitmap_erase(Handle, (uint)P_0);
	}

	public bool CanCopyTo(SKColorType P_0)
	{
		if (P_0 == SKColorType.Unknown)
		{
			return false;
		}
		using SKBitmap sKBitmap = new SKBitmap();
		SKImageInfo sKImageInfo = Info.WithColorType(P_0).WithSize(1, 1);
		return sKBitmap.TryAllocPixels(sKImageInfo);
	}

	public SKBitmap Copy()
	{
		return Copy(ColorType);
	}

	public SKBitmap Copy(SKColorType P_0)
	{
		SKBitmap sKBitmap = new SKBitmap();
		if (!CopyTo(sKBitmap, P_0))
		{
			sKBitmap.Dispose();
			sKBitmap = null;
		}
		return sKBitmap;
	}

	public bool CopyTo(SKBitmap P_0, SKColorType P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("destination");
		}
		if (P_1 == SKColorType.Unknown)
		{
			return false;
		}
		using SKPixmap sKPixmap = PeekPixels();
		if (sKPixmap == null)
		{
			return false;
		}
		using SKBitmap sKBitmap = new SKBitmap();
		SKImageInfo sKImageInfo = sKPixmap.Info.WithColorType(P_1);
		if (!sKBitmap.TryAllocPixels(sKImageInfo))
		{
			return false;
		}
		using SKCanvas sKCanvas = new SKCanvas(sKBitmap);
		using SKPaint sKPaint = new SKPaint
		{
			Shader = ToShader(),
			BlendMode = SKBlendMode.Src
		};
		sKCanvas.DrawPaint(sKPaint);
		P_0.Swap(sKBitmap);
		return true;
	}

	public IntPtr GetPixels()
	{
		IntPtr intPtr;
		return GetPixels(out intPtr);
	}

	public unsafe IntPtr GetPixels(out IntPtr P_0)
	{
		fixed (IntPtr* ptr = &P_0)
		{
			return (IntPtr)SkiaApi.sk_bitmap_get_pixels(Handle, ptr);
		}
	}

	public static SKBitmap Decode(SKCodec P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("codec");
		}
		SKImageInfo info = P_0.Info;
		if (info.AlphaType == SKAlphaType.Unpremul)
		{
			info.AlphaType = SKAlphaType.Premul;
		}
		info.ColorSpace = null;
		return Decode(P_0, info);
	}

	public static SKBitmap Decode(SKCodec P_0, SKImageInfo P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("codec");
		}
		SKBitmap sKBitmap = new SKBitmap(P_1);
		IntPtr intPtr;
		SKCodecResult pixels = P_0.GetPixels(P_1, sKBitmap.GetPixels(out intPtr));
		if (pixels != SKCodecResult.Success && pixels != SKCodecResult.IncompleteInput)
		{
			sKBitmap.Dispose();
			sKBitmap = null;
		}
		return sKBitmap;
	}

	public static SKBitmap Decode(Stream P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("stream");
		}
		using SKCodec sKCodec = SKCodec.Create(P_0);
		if (sKCodec == null)
		{
			return null;
		}
		return Decode(sKCodec);
	}

	public static SKBitmap Decode(SKData P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("data");
		}
		using SKCodec sKCodec = SKCodec.Create(P_0);
		if (sKCodec == null)
		{
			return null;
		}
		return Decode(sKCodec);
	}

	public bool InstallPixels(SKImageInfo P_0, IntPtr P_1, int P_2)
	{
		return InstallPixels(P_0, P_1, P_2, null, null);
	}

	public bool InstallPixels(SKImageInfo P_0, IntPtr P_1, int P_2, SKBitmapReleaseDelegate P_3)
	{
		return InstallPixels(P_0, P_1, P_2, P_3, null);
	}

	public unsafe bool InstallPixels(SKImageInfo P_0, IntPtr P_1, int P_2, SKBitmapReleaseDelegate P_3, object P_4)
	{
		SKImageInfoNative sKImageInfoNative = SKImageInfoNative.FromManaged(ref P_0);
		SKBitmapReleaseDelegate sKBitmapReleaseDelegate = ((P_3 != null && P_4 != null) ? ((SKBitmapReleaseDelegate)delegate(IntPtr addr, object _)
		{
			P_3(addr, P_4);
		}) : P_3);
		GCHandle gCHandle;
		IntPtr intPtr;
		SKBitmapReleaseProxyDelegate sKBitmapReleaseProxyDelegate = DelegateProxies.Create(sKBitmapReleaseDelegate, DelegateProxies.SKBitmapReleaseDelegateProxy, out gCHandle, out intPtr);
		return SkiaApi.sk_bitmap_install_pixels(Handle, &sKImageInfoNative, (void*)P_1, (IntPtr)P_2, sKBitmapReleaseProxyDelegate, (void*)intPtr);
	}

	public void NotifyPixelsChanged()
	{
		SkiaApi.sk_bitmap_notify_pixels_changed(Handle);
	}

	public SKPixmap PeekPixels()
	{
		SKPixmap sKPixmap = new SKPixmap();
		if (PeekPixels(sKPixmap))
		{
			return sKPixmap;
		}
		sKPixmap.Dispose();
		return null;
	}

	public bool PeekPixels(SKPixmap P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("pixmap");
		}
		bool flag = SkiaApi.sk_bitmap_peek_pixels(Handle, P_0.Handle);
		if (flag)
		{
			P_0.pixelSource = this;
		}
		return flag;
	}

	public SKBitmap Resize(SKImageInfo P_0, SKFilterQuality P_1)
	{
		if (P_0.IsEmpty)
		{
			return null;
		}
		SKBitmap sKBitmap = new SKBitmap(P_0);
		if (ScalePixels(sKBitmap, P_1))
		{
			return sKBitmap;
		}
		sKBitmap.Dispose();
		return null;
	}

	public bool ScalePixels(SKBitmap P_0, SKFilterQuality P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("destination");
		}
		using SKPixmap sKPixmap = P_0.PeekPixels();
		return ScalePixels(sKPixmap, P_1);
	}

	public bool ScalePixels(SKPixmap P_0, SKFilterQuality P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("destination");
		}
		using SKPixmap sKPixmap = PeekPixels();
		return sKPixmap.ScalePixels(P_0, P_1);
	}

	private void Swap(SKBitmap P_0)
	{
		SkiaApi.sk_bitmap_swap(Handle, P_0.Handle);
	}

	public SKShader ToShader()
	{
		return ToShader(SKShaderTileMode.Clamp, SKShaderTileMode.Clamp);
	}

	public unsafe SKShader ToShader(SKShaderTileMode P_0, SKShaderTileMode P_1)
	{
		return SKShader.GetObject(SkiaApi.sk_bitmap_make_shader(Handle, P_0, P_1, null));
	}
}

