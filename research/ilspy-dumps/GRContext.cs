// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRContext
using System;
using SkiaSharp;

public class GRContext : GRRecordingContext
{
	internal GRContext(IntPtr h, bool owns)
		: base(h, owns)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		AbandonContext();
		base.DisposeNative();
	}

	public unsafe static GRContext CreateGl(GRGlInterface P_0, GRContextOptions P_1)
	{
		IntPtr intPtr = P_0?.Handle ?? IntPtr.Zero;
		if (P_1 == null)
		{
			return GetObject(SkiaApi.gr_direct_context_make_gl(intPtr));
		}
		GRContextOptionsNative gRContextOptionsNative = P_1.ToNative();
		return GetObject(SkiaApi.gr_direct_context_make_gl_with_options(intPtr, &gRContextOptionsNative));
	}

	public static GRContext CreateVulkan(GRVkBackendContext P_0)
	{
		return CreateVulkan(P_0, null);
	}

	public unsafe static GRContext CreateVulkan(GRVkBackendContext P_0, GRContextOptions P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("backendContext");
		}
		if (P_1 == null)
		{
			return GetObject(SkiaApi.gr_direct_context_make_vulkan(P_0.ToNative()));
		}
		GRContextOptionsNative gRContextOptionsNative = P_1.ToNative();
		return GetObject(SkiaApi.gr_direct_context_make_vulkan_with_options(P_0.ToNative(), &gRContextOptionsNative));
	}

	public void AbandonContext(bool P_0 = false)
	{
		if (P_0)
		{
			SkiaApi.gr_direct_context_release_resources_and_abandon_context(Handle);
		}
		else
		{
			SkiaApi.gr_direct_context_abandon_context(Handle);
		}
	}

	public void SetResourceCacheLimit(long P_0)
	{
		SkiaApi.gr_direct_context_set_resource_cache_limit(Handle, (IntPtr)P_0);
	}

	public void ResetContext(GRBackendState P_0 = GRBackendState.All)
	{
		ResetContext((uint)P_0);
	}

	public void ResetContext(uint P_0)
	{
		SkiaApi.gr_direct_context_reset_context(Handle, P_0);
	}

	public void Flush()
	{
		Flush(true);
	}

	public void Flush(bool P_0, bool P_1 = false)
	{
		if (P_0)
		{
			SkiaApi.gr_direct_context_flush_and_submit(Handle, P_1);
		}
		else
		{
			SkiaApi.gr_direct_context_flush(Handle);
		}
	}

	public new int GetMaxSurfaceSampleCount(SKColorType P_0)
	{
		return base.GetMaxSurfaceSampleCount(P_0);
	}

	internal static GRContext GetObject(IntPtr P_0, bool P_1 = true)
	{
		return SKObject.GetOrAddObject(P_0, P_1, (IntPtr h, bool o) => new GRContext(h, o));
	}
}

