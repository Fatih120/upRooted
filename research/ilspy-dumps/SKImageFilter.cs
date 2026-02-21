// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKImageFilter
using System;
using SkiaSharp;

public class SKImageFilter : SKObject, ISKReferenceCounted
{
	public class CropRect : SKObject, ISKSkipObjectRegistration
	{
		internal CropRect(IntPtr P_0, bool P_1)
			: base(P_0, P_1)
		{
		}

		public unsafe CropRect(SKRect P_0, SKCropRectFlags P_1 = SKCropRectFlags.HasAll)
			: this(SkiaApi.sk_imagefilter_croprect_new_with_rect(&P_0, (uint)P_1), true)
		{
		}

		protected override void Dispose(bool P_0)
		{
			base.Dispose(P_0);
		}

		protected override void DisposeNative()
		{
			SkiaApi.sk_imagefilter_croprect_destructor(Handle);
		}
	}

	internal SKImageFilter(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public static SKImageFilter CreateBlur(float P_0, float P_1)
	{
		return CreateBlur(P_0, P_1, SKShaderTileMode.Decal, null, null);
	}

	public static SKImageFilter CreateBlur(float P_0, float P_1, SKImageFilter P_2)
	{
		return CreateBlur(P_0, P_1, SKShaderTileMode.Decal, P_2, null);
	}

	public static SKImageFilter CreateBlur(float P_0, float P_1, SKImageFilter P_2, SKRect P_3)
	{
		return CreateBlur(P_0, P_1, SKShaderTileMode.Decal, P_2, new CropRect(P_3));
	}

	public static SKImageFilter CreateBlur(float P_0, float P_1, SKShaderTileMode P_2, SKImageFilter P_3, CropRect P_4)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_blur(P_0, P_1, P_2, P_3?.Handle ?? IntPtr.Zero, P_4?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateColorFilter(SKColorFilter P_0, SKImageFilter P_1)
	{
		return CreateColorFilter(P_0, P_1, null);
	}

	public static SKImageFilter CreateColorFilter(SKColorFilter P_0, SKImageFilter P_1, SKRect P_2)
	{
		return CreateColorFilter(P_0, P_1, new CropRect(P_2));
	}

	public static SKImageFilter CreateColorFilter(SKColorFilter P_0, SKImageFilter P_1, CropRect P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("cf");
		}
		return GetObject(SkiaApi.sk_imagefilter_new_color_filter(P_0.Handle, P_1?.Handle ?? IntPtr.Zero, P_2?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateDisplacementMapEffect(SKColorChannel P_0, SKColorChannel P_1, float P_2, SKImageFilter P_3, SKImageFilter P_4)
	{
		return CreateDisplacementMapEffect(P_0, P_1, P_2, P_3, P_4, null);
	}

	public static SKImageFilter CreateDisplacementMapEffect(SKColorChannel P_0, SKColorChannel P_1, float P_2, SKImageFilter P_3, SKImageFilter P_4, SKRect P_5)
	{
		return CreateDisplacementMapEffect(P_0, P_1, P_2, P_3, P_4, new CropRect(P_5));
	}

	public static SKImageFilter CreateDisplacementMapEffect(SKColorChannel P_0, SKColorChannel P_1, float P_2, SKImageFilter P_3, SKImageFilter P_4, CropRect P_5)
	{
		if (P_3 == null)
		{
			throw new ArgumentNullException("displacement");
		}
		return GetObject(SkiaApi.sk_imagefilter_new_displacement_map_effect(P_0, P_1, P_2, P_3.Handle, P_4?.Handle ?? IntPtr.Zero, P_5?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateDropShadow(float P_0, float P_1, float P_2, float P_3, SKColor P_4)
	{
		return CreateDropShadow(P_0, P_1, P_2, P_3, P_4, null, null);
	}

	public static SKImageFilter CreateDropShadow(float P_0, float P_1, float P_2, float P_3, SKColor P_4, SKImageFilter P_5, CropRect P_6)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_drop_shadow(P_0, P_1, P_2, P_3, (uint)P_4, P_5?.Handle ?? IntPtr.Zero, P_6?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateDistantLitDiffuse(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, SKImageFilter P_4)
	{
		return CreateDistantLitDiffuse(P_0, P_1, P_2, P_3, P_4, null);
	}

	public static SKImageFilter CreateDistantLitDiffuse(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, SKImageFilter P_4, SKRect P_5)
	{
		return CreateDistantLitDiffuse(P_0, P_1, P_2, P_3, P_4, new CropRect(P_5));
	}

	public unsafe static SKImageFilter CreateDistantLitDiffuse(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, SKImageFilter P_4, CropRect P_5)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_distant_lit_diffuse(&P_0, (uint)P_1, P_2, P_3, P_4?.Handle ?? IntPtr.Zero, P_5?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreatePointLitDiffuse(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, SKImageFilter P_4)
	{
		return CreatePointLitDiffuse(P_0, P_1, P_2, P_3, P_4, null);
	}

	public static SKImageFilter CreatePointLitDiffuse(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, SKImageFilter P_4, SKRect P_5)
	{
		return CreatePointLitDiffuse(P_0, P_1, P_2, P_3, P_4, new CropRect(P_5));
	}

	public unsafe static SKImageFilter CreatePointLitDiffuse(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, SKImageFilter P_4, CropRect P_5)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_point_lit_diffuse(&P_0, (uint)P_1, P_2, P_3, P_4?.Handle ?? IntPtr.Zero, P_5?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateSpotLitDiffuse(SKPoint3 P_0, SKPoint3 P_1, float P_2, float P_3, SKColor P_4, float P_5, float P_6, SKImageFilter P_7)
	{
		return CreateSpotLitDiffuse(P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, null);
	}

	public static SKImageFilter CreateSpotLitDiffuse(SKPoint3 P_0, SKPoint3 P_1, float P_2, float P_3, SKColor P_4, float P_5, float P_6, SKImageFilter P_7, SKRect P_8)
	{
		return CreateSpotLitDiffuse(P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, new CropRect(P_8));
	}

	public unsafe static SKImageFilter CreateSpotLitDiffuse(SKPoint3 P_0, SKPoint3 P_1, float P_2, float P_3, SKColor P_4, float P_5, float P_6, SKImageFilter P_7, CropRect P_8)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_spot_lit_diffuse(&P_0, &P_1, P_2, P_3, (uint)P_4, P_5, P_6, P_7?.Handle ?? IntPtr.Zero, P_8?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateDistantLitSpecular(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, float P_4, SKImageFilter P_5)
	{
		return CreateDistantLitSpecular(P_0, P_1, P_2, P_3, P_4, P_5, null);
	}

	public static SKImageFilter CreateDistantLitSpecular(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, float P_4, SKImageFilter P_5, SKRect P_6)
	{
		return CreateDistantLitSpecular(P_0, P_1, P_2, P_3, P_4, P_5, new CropRect(P_6));
	}

	public unsafe static SKImageFilter CreateDistantLitSpecular(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, float P_4, SKImageFilter P_5, CropRect P_6)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_distant_lit_specular(&P_0, (uint)P_1, P_2, P_3, P_4, P_5?.Handle ?? IntPtr.Zero, P_6?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreatePointLitSpecular(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, float P_4, SKImageFilter P_5)
	{
		return CreatePointLitSpecular(P_0, P_1, P_2, P_3, P_4, P_5, null);
	}

	public static SKImageFilter CreatePointLitSpecular(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, float P_4, SKImageFilter P_5, SKRect P_6)
	{
		return CreatePointLitSpecular(P_0, P_1, P_2, P_3, P_4, P_5, new CropRect(P_6));
	}

	public unsafe static SKImageFilter CreatePointLitSpecular(SKPoint3 P_0, SKColor P_1, float P_2, float P_3, float P_4, SKImageFilter P_5, CropRect P_6)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_point_lit_specular(&P_0, (uint)P_1, P_2, P_3, P_4, P_5?.Handle ?? IntPtr.Zero, P_6?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateSpotLitSpecular(SKPoint3 P_0, SKPoint3 P_1, float P_2, float P_3, SKColor P_4, float P_5, float P_6, float P_7, SKImageFilter P_8)
	{
		return CreateSpotLitSpecular(P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8, null);
	}

	public static SKImageFilter CreateSpotLitSpecular(SKPoint3 P_0, SKPoint3 P_1, float P_2, float P_3, SKColor P_4, float P_5, float P_6, float P_7, SKImageFilter P_8, SKRect P_9)
	{
		return CreateSpotLitSpecular(P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8, new CropRect(P_9));
	}

	public unsafe static SKImageFilter CreateSpotLitSpecular(SKPoint3 P_0, SKPoint3 P_1, float P_2, float P_3, SKColor P_4, float P_5, float P_6, float P_7, SKImageFilter P_8, CropRect P_9)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_spot_lit_specular(&P_0, &P_1, P_2, P_3, (uint)P_4, P_5, P_6, P_7, P_8?.Handle ?? IntPtr.Zero, P_9?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateMatrixConvolution(SKSizeI P_0, ReadOnlySpan<float> P_1, float P_2, float P_3, SKPointI P_4, SKShaderTileMode P_5, bool P_6, SKImageFilter P_7)
	{
		return CreateMatrixConvolution(P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, null);
	}

	public static SKImageFilter CreateMatrixConvolution(SKSizeI P_0, ReadOnlySpan<float> P_1, float P_2, float P_3, SKPointI P_4, SKShaderTileMode P_5, bool P_6, SKImageFilter P_7, SKRect P_8)
	{
		return CreateMatrixConvolution(P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, new CropRect(P_8));
	}

	public unsafe static SKImageFilter CreateMatrixConvolution(SKSizeI P_0, ReadOnlySpan<float> P_1, float P_2, float P_3, SKPointI P_4, SKShaderTileMode P_5, bool P_6, SKImageFilter P_7, CropRect P_8)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("kernel");
		}
		if (P_1.Length != P_0.Width * P_0.Height)
		{
			throw new ArgumentException("Kernel length must match the dimensions of the kernel size (Width * Height).", "kernel");
		}
		fixed (float* ptr = P_1)
		{
			return GetObject(SkiaApi.sk_imagefilter_new_matrix_convolution(&P_0, ptr, P_2, P_3, &P_4, P_5, P_6, P_7?.Handle ?? IntPtr.Zero, P_8?.Handle ?? IntPtr.Zero));
		}
	}

	public static SKImageFilter CreateMerge(ReadOnlySpan<SKImageFilter> P_0)
	{
		return CreateMerge(P_0, null);
	}

	public static SKImageFilter CreateMerge(ReadOnlySpan<SKImageFilter> P_0, SKRect P_1)
	{
		return CreateMerge(P_0, new CropRect(P_1));
	}

	public unsafe static SKImageFilter CreateMerge(ReadOnlySpan<SKImageFilter> P_0, CropRect P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("filters");
		}
		IntPtr[] array = new IntPtr[P_0.Length];
		for (int i = 0; i < P_0.Length; i++)
		{
			array[i] = P_0[i]?.Handle ?? IntPtr.Zero;
		}
		fixed (IntPtr* ptr = array)
		{
			return GetObject(SkiaApi.sk_imagefilter_new_merge(ptr, P_0.Length, P_1?.Handle ?? IntPtr.Zero));
		}
	}

	public static SKImageFilter CreateDilate(float P_0, float P_1, SKImageFilter P_2)
	{
		return CreateDilate(P_0, P_1, P_2, null);
	}

	public static SKImageFilter CreateDilate(float P_0, float P_1, SKImageFilter P_2, SKRect P_3)
	{
		return CreateDilate(P_0, P_1, P_2, new CropRect(P_3));
	}

	public static SKImageFilter CreateDilate(float P_0, float P_1, SKImageFilter P_2, CropRect P_3)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_dilate(P_0, P_1, P_2?.Handle ?? IntPtr.Zero, P_3?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateErode(float P_0, float P_1, SKImageFilter P_2)
	{
		return CreateErode(P_0, P_1, P_2, null);
	}

	public static SKImageFilter CreateErode(float P_0, float P_1, SKImageFilter P_2, SKRect P_3)
	{
		return CreateErode(P_0, P_1, P_2, new CropRect(P_3));
	}

	public static SKImageFilter CreateErode(float P_0, float P_1, SKImageFilter P_2, CropRect P_3)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_erode(P_0, P_1, P_2?.Handle ?? IntPtr.Zero, P_3?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateOffset(float P_0, float P_1, SKImageFilter P_2)
	{
		return CreateOffset(P_0, P_1, P_2, null);
	}

	public static SKImageFilter CreateOffset(float P_0, float P_1, SKImageFilter P_2, SKRect P_3)
	{
		return CreateOffset(P_0, P_1, P_2, new CropRect(P_3));
	}

	public static SKImageFilter CreateOffset(float P_0, float P_1, SKImageFilter P_2, CropRect P_3)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_offset(P_0, P_1, P_2?.Handle ?? IntPtr.Zero, P_3?.Handle ?? IntPtr.Zero));
	}

	public unsafe static SKImageFilter CreatePicture(SKPicture P_0, SKRect P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("picture");
		}
		return GetObject(SkiaApi.sk_imagefilter_new_picture_with_croprect(P_0.Handle, &P_1));
	}

	public unsafe static SKImageFilter CreateTile(SKRect P_0, SKRect P_1, SKImageFilter P_2)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_tile(&P_0, &P_1, P_2?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateBlendMode(SKBlendMode P_0, SKImageFilter P_1, SKImageFilter P_2)
	{
		return CreateBlendMode(P_0, P_1, P_2, null);
	}

	public static SKImageFilter CreateBlendMode(SKBlendMode P_0, SKImageFilter P_1, SKImageFilter P_2, SKRect P_3)
	{
		return CreateBlendMode(P_0, P_1, P_2, new CropRect(P_3));
	}

	public static SKImageFilter CreateBlendMode(SKBlendMode P_0, SKImageFilter P_1, SKImageFilter P_2, CropRect P_3)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_xfermode(P_0, P_1?.Handle ?? IntPtr.Zero, P_2?.Handle ?? IntPtr.Zero, P_3?.Handle ?? IntPtr.Zero));
	}

	public static SKImageFilter CreateArithmetic(float P_0, float P_1, float P_2, float P_3, bool P_4, SKImageFilter P_5, SKImageFilter P_6)
	{
		return CreateArithmetic(P_0, P_1, P_2, P_3, P_4, P_5, P_6, null);
	}

	public static SKImageFilter CreateArithmetic(float P_0, float P_1, float P_2, float P_3, bool P_4, SKImageFilter P_5, SKImageFilter P_6, SKRect P_7)
	{
		return CreateArithmetic(P_0, P_1, P_2, P_3, P_4, P_5, P_6, new CropRect(P_7));
	}

	public static SKImageFilter CreateArithmetic(float P_0, float P_1, float P_2, float P_3, bool P_4, SKImageFilter P_5, SKImageFilter P_6, CropRect P_7)
	{
		return GetObject(SkiaApi.sk_imagefilter_new_arithmetic(P_0, P_1, P_2, P_3, P_4, P_5?.Handle ?? IntPtr.Zero, P_6?.Handle ?? IntPtr.Zero, P_7?.Handle ?? IntPtr.Zero));
	}

	public unsafe static SKImageFilter CreateImage(SKImage P_0, SKRect P_1, SKRect P_2, SKFilterQuality P_3)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("image");
		}
		return GetObject(SkiaApi.sk_imagefilter_new_image_source(P_0.Handle, &P_1, &P_2, P_3));
	}

	public static SKImageFilter CreatePaint(SKPaint P_0, CropRect P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("paint");
		}
		return GetObject(SkiaApi.sk_imagefilter_new_paint(P_0.Handle, P_1?.Handle ?? IntPtr.Zero));
	}

	internal static SKImageFilter GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (IntPtr h, bool o) => new SKImageFilter(h, o));
	}

	public static SKImageFilter CreateShader(SKShader P_0, bool P_1)
	{
		return CreateShader(P_0, P_1, null);
	}

	public static SKImageFilter CreateShader(SKShader P_0, bool P_1, SKRect P_2)
	{
		return CreateShader(P_0, P_1, new CropRect(P_2));
	}

	public static SKImageFilter CreateShader(SKShader P_0, bool P_1, CropRect P_2)
	{
		SKPaint sKPaint = new SKPaint();
		sKPaint.Shader = P_0;
		sKPaint.IsDither = P_1;
		return CreatePaint(sKPaint, P_2);
	}
}

