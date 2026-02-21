// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRBackendRenderTarget
using System;
using System.ComponentModel;
using SkiaSharp;

public class GRBackendRenderTarget : SKObject, ISKSkipObjectRegistration
{
	internal GRBackendRenderTarget(IntPtr handle, bool owns)
		: base(handle, owns)
	{
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use GRBackendRenderTarget(int, int, int, int, GRGlFramebufferInfo) instead.")]
	public GRBackendRenderTarget(GRBackend backend, GRBackendRenderTargetDesc desc)
		: this(IntPtr.Zero, owns: true)
	{
		switch (backend)
		{
		case GRBackend.Metal:
			throw new NotSupportedException();
		case GRBackend.OpenGL:
		{
			GRGlFramebufferInfo gRGlFramebufferInfo = new GRGlFramebufferInfo((uint)(int)desc.RenderTargetHandle, desc.Config.ToGlSizedFormat());
			CreateGl(desc.Width, desc.Height, desc.SampleCount, desc.StencilBits, gRGlFramebufferInfo);
			break;
		}
		case GRBackend.Vulkan:
			throw new NotSupportedException();
		case GRBackend.Dawn:
			throw new NotSupportedException();
		default:
			throw new ArgumentOutOfRangeException("backend");
		}
	}

	public GRBackendRenderTarget(int width, int height, int sampleCount, int stencilBits, GRGlFramebufferInfo glInfo)
		: this(IntPtr.Zero, owns: true)
	{
		CreateGl(width, height, sampleCount, stencilBits, glInfo);
	}

	public GRBackendRenderTarget(int width, int height, int sampleCount, GRVkImageInfo vkImageInfo)
		: this(IntPtr.Zero, owns: true)
	{
		CreateVulkan(width, height, sampleCount, vkImageInfo);
	}

	private unsafe void CreateGl(int P_0, int P_1, int P_2, int P_3, GRGlFramebufferInfo P_4)
	{
		Handle = SkiaApi.gr_backendrendertarget_new_gl(P_0, P_1, P_2, P_3, &P_4);
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new GRBackendRenderTarget instance.");
		}
	}

	private unsafe void CreateVulkan(int P_0, int P_1, int P_2, GRVkImageInfo P_3)
	{
		Handle = SkiaApi.gr_backendrendertarget_new_vulkan(P_0, P_1, P_2, &P_3);
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new GRBackendRenderTarget instance.");
		}
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.gr_backendrendertarget_delete(Handle);
	}
}

