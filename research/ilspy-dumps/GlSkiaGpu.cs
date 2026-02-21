// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GlSkiaGpu
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Logging;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Surfaces;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class GlSkiaGpu : ISkiaGpu, IPlatformGraphicsContext, IDisposable, IOptionalFeatureProvider, IOpenGlTextureSharingRenderInterfaceContextFeature, ISkiaGpuWithPlatformGraphicsContext
{
	private class SurfaceWrapper : IGlPlatformSurface
	{
		private readonly object _surface;

		public SurfaceWrapper(object P_0)
		{
			_surface = P_0;
		}

		public IGlPlatformSurfaceRenderTarget CreateGlRenderTarget(IGlContext P_0)
		{
			return P_0.TryGetFeature<IGlPlatformSurfaceRenderTargetFactory>().CreateRenderTarget(P_0, _surface);
		}
	}

	private readonly GRContext _grContext;

	private readonly IGlContext _glContext;

	private readonly List<Action> _postDisposeCallbacks = new List<Action>();

	private bool? _canCreateSurfaces;

	private readonly IExternalObjectsRenderInterfaceContextFeature? _externalObjectsFeature;

	public bool IsLost => _glContext.IsLost;

	public IPlatformGraphicsContext? PlatformGraphicsContext => _glContext;

	public GlSkiaGpu(IGlContext P_0, long? P_1)
	{
		_glContext = P_0;
		using (_glContext.EnsureCurrent())
		{
			IGlSkiaSpecificOptionsFeature glSkiaSpecificOptionsFeature = P_0.TryGetFeature<IGlSkiaSpecificOptionsFeature>();
			GRGlInterface gRGlInterface = ((glSkiaSpecificOptionsFeature == null || !glSkiaSpecificOptionsFeature.UseNativeSkiaGrGlInterface) ? ((P_0.Version.Type == GlProfileType.OpenGL) ? GRGlInterface.CreateOpenGl((string proc) => P_0.GlInterface.GetProcAddress(proc)) : GRGlInterface.CreateGles((string proc) => P_0.GlInterface.GetProcAddress(proc))) : GRGlInterface.Create());
			using (gRGlInterface)
			{
				_grContext = GRContext.CreateGl(gRGlInterface, new GRContextOptions
				{
					AvoidStencilBuffers = true
				});
				if (P_1.HasValue)
				{
					_grContext.SetResourceCacheLimit(P_1.Value);
				}
			}
			P_0.TryGetFeature<IGlContextExternalObjectsFeature>(out var glContextExternalObjectsFeature);
			_externalObjectsFeature = new GlSkiaExternalObjectsFeature(this, glContextExternalObjectsFeature);
		}
	}

	public ISkiaGpuRenderTarget? TryCreateRenderTarget(IEnumerable<object> P_0)
	{
		IGlPlatformSurfaceRenderTargetFactory glPlatformSurfaceRenderTargetFactory = _glContext.TryGetFeature<IGlPlatformSurfaceRenderTargetFactory>();
		foreach (object item in P_0)
		{
			if (glPlatformSurfaceRenderTargetFactory != null && glPlatformSurfaceRenderTargetFactory.CanRenderToSurface(_glContext, item))
			{
				return new GlRenderTarget(_grContext, _glContext, new SurfaceWrapper(item));
			}
			if (item is IGlPlatformSurface glPlatformSurface)
			{
				return new GlRenderTarget(_grContext, _glContext, glPlatformSurface);
			}
		}
		return null;
	}

	public ISkiaSurface? TryCreateSurface(PixelSize P_0, ISkiaGpuRenderSession? P_1)
	{
		if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			return null;
		}
		if (!_glContext.GlInterface.IsBlitFramebufferAvailable)
		{
			return null;
		}
		P_0 = new PixelSize(Math.Max(P_0.Width, 1), Math.Max(P_0.Height, 1));
		if (_canCreateSurfaces == false)
		{
			return null;
		}
		try
		{
			FboSkiaSurface result = new FboSkiaSurface(this, _grContext, _glContext, P_0, P_1?.SurfaceOrigin ?? GRSurfaceOrigin.TopLeft);
			_canCreateSurfaces = true;
			return result;
		}
		catch (Exception)
		{
			Logger.TryGet(LogEventLevel.Error, "OpenGL")?.Log(this, "Unable to create a Skia-compatible FBO manually");
			bool valueOrDefault = _canCreateSurfaces == true;
			if (!_canCreateSurfaces.HasValue)
			{
				valueOrDefault = false;
				_canCreateSurfaces = valueOrDefault;
			}
			return null;
		}
	}

	public void Dispose()
	{
		if (_glContext.IsLost)
		{
			_grContext.AbandonContext();
		}
		else
		{
			_grContext.AbandonContext(true);
		}
		_grContext.Dispose();
		lock (_postDisposeCallbacks)
		{
			foreach (Action postDisposeCallback in _postDisposeCallbacks)
			{
				postDisposeCallback();
			}
		}
	}

	public IDisposable EnsureCurrent()
	{
		return _glContext.EnsureCurrent();
	}

	public object? TryGetFeature(Type P_0)
	{
		if (P_0 == typeof(IOpenGlTextureSharingRenderInterfaceContextFeature))
		{
			return this;
		}
		if (P_0 == typeof(IExternalObjectsRenderInterfaceContextFeature))
		{
			return _externalObjectsFeature;
		}
		if (P_0 == typeof(IExternalObjectsHandleWrapRenderInterfaceContextFeature))
		{
			return _glContext.TryGetFeature(P_0);
		}
		return null;
	}

	public void AddPostDispose(Action P_0)
	{
		lock (_postDisposeCallbacks)
		{
			_postDisposeCallbacks.Add(P_0);
		}
	}
}

