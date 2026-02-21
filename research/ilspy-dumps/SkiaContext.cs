// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaContext
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls.Platform.Surfaces;
using Avalonia.OpenGL;
using Avalonia.Platform;
using Avalonia.Skia;

internal class SkiaContext : IPlatformRenderInterfaceContext, IOptionalFeatureProvider, IDisposable
{
	private ISkiaGpu? _gpu;

	public IReadOnlyDictionary<Type, object> PublicFeatures { get; }

	public SkiaContext(ISkiaGpu? P_0)
	{
		_gpu = P_0;
		Dictionary<Type, object> features = new Dictionary<Type, object>();
		if (P_0 != null)
		{
			TryFeature<IOpenGlTextureSharingRenderInterfaceContextFeature>();
			TryFeature<IExternalObjectsRenderInterfaceContextFeature>();
		}
		PublicFeatures = features;
		void TryFeature<T>() where T : class
		{
			T val = P_0.TryGetFeature<T>();
			if (val != null)
			{
				features.Add(typeof(T), val);
			}
		}
	}

	public void Dispose()
	{
		_gpu?.Dispose();
		_gpu = null;
	}

	public IRenderTarget CreateRenderTarget(IEnumerable<object> P_0)
	{
		if (!(P_0 is IList))
		{
			P_0 = P_0.ToList();
		}
		ISkiaGpuRenderTarget skiaGpuRenderTarget = _gpu?.TryCreateRenderTarget(P_0);
		if (skiaGpuRenderTarget != null)
		{
			return new SkiaGpuRenderTarget(_gpu, skiaGpuRenderTarget);
		}
		foreach (object item in P_0)
		{
			if (item is IFramebufferPlatformSurface framebufferPlatformSurface)
			{
				return new FramebufferRenderTarget(framebufferPlatformSurface);
			}
		}
		throw new NotSupportedException("Don't know how to create a Skia render target from any of provided surfaces");
	}

	public object? TryGetFeature(Type P_0)
	{
		return _gpu?.TryGetFeature(P_0);
	}
}

