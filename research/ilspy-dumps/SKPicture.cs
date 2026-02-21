// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPicture
using System;
using SkiaSharp;

public class SKPicture : SKObject, ISKReferenceCounted
{
	public unsafe SKRect CullRect
	{
		get
		{
			SKRect result = default(SKRect);
			SkiaApi.sk_picture_get_cull_rect(Handle, &result);
			return result;
		}
	}

	internal SKPicture(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public unsafe SKShader ToShader(SKShaderTileMode P_0, SKShaderTileMode P_1, SKMatrix P_2, SKRect P_3)
	{
		return SKShader.GetObject(SkiaApi.sk_picture_make_shader(Handle, P_0, P_1, &P_2, &P_3));
	}

	internal static SKPicture GetObject(IntPtr P_0, bool P_1 = true, bool P_2 = true)
	{
		return SKObject.GetOrAddObject(P_0, P_1, P_2, (IntPtr h, bool o) => new SKPicture(h, o));
	}
}

