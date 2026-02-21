// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKShader
using System;
using SkiaSharp;

public class SKShader : SKObject, ISKReferenceCounted
{
	internal SKShader(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public static SKShader CreateColor(SKColor P_0)
	{
		return GetObject(SkiaApi.sk_shader_new_color((uint)P_0));
	}

	public unsafe static SKShader CreateColor(SKColorF P_0, SKColorSpace P_1)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("colorspace");
		}
		return GetObject(SkiaApi.sk_shader_new_color4f(&P_0, P_1.Handle));
	}

	public static SKShader CreatePicture(SKPicture P_0, SKShaderTileMode P_1, SKShaderTileMode P_2, SKMatrix P_3, SKRect P_4)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("src");
		}
		return P_0.ToShader(P_1, P_2, P_3, P_4);
	}

	public unsafe static SKShader CreateLinearGradient(SKPoint P_0, SKPoint P_1, SKColor[] P_2, float[] P_3, SKShaderTileMode P_4)
	{
		if (P_2 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_3 != null && P_2.Length != P_3.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		SKPoint* ptr = stackalloc SKPoint[2] { P_0, P_1 };
		fixed (SKColor* ptr2 = P_2)
		{
			fixed (float* ptr3 = P_3)
			{
				return GetObject(SkiaApi.sk_shader_new_linear_gradient(ptr, (uint*)ptr2, ptr3, P_2.Length, P_4, null));
			}
		}
	}

	public unsafe static SKShader CreateLinearGradient(SKPoint P_0, SKPoint P_1, SKColor[] P_2, float[] P_3, SKShaderTileMode P_4, SKMatrix P_5)
	{
		if (P_2 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_3 != null && P_2.Length != P_3.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		SKPoint* ptr = stackalloc SKPoint[2] { P_0, P_1 };
		fixed (SKColor* ptr2 = P_2)
		{
			fixed (float* ptr3 = P_3)
			{
				return GetObject(SkiaApi.sk_shader_new_linear_gradient(ptr, (uint*)ptr2, ptr3, P_2.Length, P_4, &P_5));
			}
		}
	}

	public unsafe static SKShader CreateLinearGradient(SKPoint P_0, SKPoint P_1, SKColorF[] P_2, SKColorSpace P_3, float[] P_4, SKShaderTileMode P_5)
	{
		if (P_2 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_4 != null && P_2.Length != P_4.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		SKPoint* ptr = stackalloc SKPoint[2] { P_0, P_1 };
		fixed (SKColorF* ptr2 = P_2)
		{
			fixed (float* ptr3 = P_4)
			{
				return GetObject(SkiaApi.sk_shader_new_linear_gradient_color4f(ptr, ptr2, P_3?.Handle ?? IntPtr.Zero, ptr3, P_2.Length, P_5, null));
			}
		}
	}

	public unsafe static SKShader CreateLinearGradient(SKPoint P_0, SKPoint P_1, SKColorF[] P_2, SKColorSpace P_3, float[] P_4, SKShaderTileMode P_5, SKMatrix P_6)
	{
		if (P_2 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_4 != null && P_2.Length != P_4.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		SKPoint* ptr = stackalloc SKPoint[2] { P_0, P_1 };
		fixed (SKColorF* ptr2 = P_2)
		{
			fixed (float* ptr3 = P_4)
			{
				return GetObject(SkiaApi.sk_shader_new_linear_gradient_color4f(ptr, ptr2, P_3?.Handle ?? IntPtr.Zero, ptr3, P_2.Length, P_5, &P_6));
			}
		}
	}

	public unsafe static SKShader CreateRadialGradient(SKPoint P_0, float P_1, SKColor[] P_2, float[] P_3, SKShaderTileMode P_4)
	{
		if (P_2 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_3 != null && P_2.Length != P_3.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		fixed (SKColor* ptr = P_2)
		{
			fixed (float* ptr2 = P_3)
			{
				return GetObject(SkiaApi.sk_shader_new_radial_gradient(&P_0, P_1, (uint*)ptr, ptr2, P_2.Length, P_4, null));
			}
		}
	}

	public unsafe static SKShader CreateRadialGradient(SKPoint P_0, float P_1, SKColor[] P_2, float[] P_3, SKShaderTileMode P_4, SKMatrix P_5)
	{
		if (P_2 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_3 != null && P_2.Length != P_3.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		fixed (SKColor* ptr = P_2)
		{
			fixed (float* ptr2 = P_3)
			{
				return GetObject(SkiaApi.sk_shader_new_radial_gradient(&P_0, P_1, (uint*)ptr, ptr2, P_2.Length, P_4, &P_5));
			}
		}
	}

	public unsafe static SKShader CreateRadialGradient(SKPoint P_0, float P_1, SKColorF[] P_2, SKColorSpace P_3, float[] P_4, SKShaderTileMode P_5)
	{
		if (P_2 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_4 != null && P_2.Length != P_4.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		fixed (SKColorF* ptr = P_2)
		{
			fixed (float* ptr2 = P_4)
			{
				return GetObject(SkiaApi.sk_shader_new_radial_gradient_color4f(&P_0, P_1, ptr, P_3?.Handle ?? IntPtr.Zero, ptr2, P_2.Length, P_5, null));
			}
		}
	}

	public unsafe static SKShader CreateRadialGradient(SKPoint P_0, float P_1, SKColorF[] P_2, SKColorSpace P_3, float[] P_4, SKShaderTileMode P_5, SKMatrix P_6)
	{
		if (P_2 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_4 != null && P_2.Length != P_4.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		fixed (SKColorF* ptr = P_2)
		{
			fixed (float* ptr2 = P_4)
			{
				return GetObject(SkiaApi.sk_shader_new_radial_gradient_color4f(&P_0, P_1, ptr, P_3?.Handle ?? IntPtr.Zero, ptr2, P_2.Length, P_5, &P_6));
			}
		}
	}

	public static SKShader CreateSweepGradient(SKPoint P_0, SKColor[] P_1, float[] P_2, SKMatrix P_3)
	{
		return CreateSweepGradient(P_0, P_1, P_2, SKShaderTileMode.Clamp, 0f, 360f, P_3);
	}

	public unsafe static SKShader CreateSweepGradient(SKPoint P_0, SKColor[] P_1, float[] P_2, SKShaderTileMode P_3, float P_4, float P_5, SKMatrix P_6)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_2 != null && P_1.Length != P_2.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		fixed (SKColor* ptr = P_1)
		{
			fixed (float* ptr2 = P_2)
			{
				return GetObject(SkiaApi.sk_shader_new_sweep_gradient(&P_0, (uint*)ptr, ptr2, P_1.Length, P_3, P_4, P_5, &P_6));
			}
		}
	}

	public unsafe static SKShader CreateTwoPointConicalGradient(SKPoint P_0, float P_1, SKPoint P_2, float P_3, SKColor[] P_4, float[] P_5, SKShaderTileMode P_6)
	{
		if (P_4 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_5 != null && P_4.Length != P_5.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		fixed (SKColor* ptr = P_4)
		{
			fixed (float* ptr2 = P_5)
			{
				return GetObject(SkiaApi.sk_shader_new_two_point_conical_gradient(&P_0, P_1, &P_2, P_3, (uint*)ptr, ptr2, P_4.Length, P_6, null));
			}
		}
	}

	public unsafe static SKShader CreateTwoPointConicalGradient(SKPoint P_0, float P_1, SKPoint P_2, float P_3, SKColor[] P_4, float[] P_5, SKShaderTileMode P_6, SKMatrix P_7)
	{
		if (P_4 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_5 != null && P_4.Length != P_5.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		fixed (SKColor* ptr = P_4)
		{
			fixed (float* ptr2 = P_5)
			{
				return GetObject(SkiaApi.sk_shader_new_two_point_conical_gradient(&P_0, P_1, &P_2, P_3, (uint*)ptr, ptr2, P_4.Length, P_6, &P_7));
			}
		}
	}

	public unsafe static SKShader CreateTwoPointConicalGradient(SKPoint P_0, float P_1, SKPoint P_2, float P_3, SKColorF[] P_4, SKColorSpace P_5, float[] P_6, SKShaderTileMode P_7)
	{
		if (P_4 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_6 != null && P_4.Length != P_6.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		fixed (SKColorF* ptr = P_4)
		{
			fixed (float* ptr2 = P_6)
			{
				return GetObject(SkiaApi.sk_shader_new_two_point_conical_gradient_color4f(&P_0, P_1, &P_2, P_3, ptr, P_5?.Handle ?? IntPtr.Zero, ptr2, P_4.Length, P_7, null));
			}
		}
	}

	public unsafe static SKShader CreateTwoPointConicalGradient(SKPoint P_0, float P_1, SKPoint P_2, float P_3, SKColorF[] P_4, SKColorSpace P_5, float[] P_6, SKShaderTileMode P_7, SKMatrix P_8)
	{
		if (P_4 == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (P_6 != null && P_4.Length != P_6.Length)
		{
			throw new ArgumentException("The number of colors must match the number of color positions.");
		}
		fixed (SKColorF* ptr = P_4)
		{
			fixed (float* ptr2 = P_6)
			{
				return GetObject(SkiaApi.sk_shader_new_two_point_conical_gradient_color4f(&P_0, P_1, &P_2, P_3, ptr, P_5?.Handle ?? IntPtr.Zero, ptr2, P_4.Length, P_7, &P_8));
			}
		}
	}

	public static SKShader CreatePerlinNoiseFractalNoise(float P_0, float P_1, int P_2, float P_3, SKPointI P_4)
	{
		return CreatePerlinNoiseFractalNoise(P_0, P_1, P_2, P_3, (SKSizeI)P_4);
	}

	public unsafe static SKShader CreatePerlinNoiseFractalNoise(float P_0, float P_1, int P_2, float P_3, SKSizeI P_4)
	{
		return GetObject(SkiaApi.sk_shader_new_perlin_noise_fractal_noise(P_0, P_1, P_2, P_3, &P_4));
	}

	public static SKShader CreatePerlinNoiseTurbulence(float P_0, float P_1, int P_2, float P_3, SKPointI P_4)
	{
		return CreatePerlinNoiseTurbulence(P_0, P_1, P_2, P_3, (SKSizeI)P_4);
	}

	public unsafe static SKShader CreatePerlinNoiseTurbulence(float P_0, float P_1, int P_2, float P_3, SKSizeI P_4)
	{
		return GetObject(SkiaApi.sk_shader_new_perlin_noise_turbulence(P_0, P_1, P_2, P_3, &P_4));
	}

	public static SKShader CreateCompose(SKShader P_0, SKShader P_1)
	{
		return CreateCompose(P_0, P_1, SKBlendMode.SrcOver);
	}

	public static SKShader CreateCompose(SKShader P_0, SKShader P_1, SKBlendMode P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("shaderA");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("shaderB");
		}
		return GetObject(SkiaApi.sk_shader_new_blend(P_2, P_0.Handle, P_1.Handle));
	}

	internal static SKShader GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (IntPtr h, bool o) => new SKShader(h, o));
	}
}

