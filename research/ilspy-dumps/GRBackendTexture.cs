// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRBackendTexture
using System;
using SkiaSharp;

public class GRBackendTexture : SKObject, ISKSkipObjectRegistration
{
	internal GRBackendTexture(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public GRBackendTexture(int P_0, int P_1, bool P_2, GRGlTextureInfo P_3)
		: this(IntPtr.Zero, true)
	{
		CreateGl(P_0, P_1, P_2, P_3);
	}

	public GRBackendTexture(int P_0, int P_1, GRVkImageInfo P_2)
		: this(IntPtr.Zero, true)
	{
		CreateVulkan(P_0, P_1, P_2);
	}

	private unsafe void CreateGl(int P_0, int P_1, bool P_2, GRGlTextureInfo P_3)
	{
		Handle = SkiaApi.gr_backendtexture_new_gl(P_0, P_1, P_2, &P_3);
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new GRBackendTexture instance.");
		}
	}

	private unsafe void CreateVulkan(int P_0, int P_1, GRVkImageInfo P_2)
	{
		Handle = SkiaApi.gr_backendtexture_new_vulkan(P_0, P_1, &P_2);
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new GRBackendTexture instance.");
		}
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.gr_backendtexture_delete(Handle);
	}
}

