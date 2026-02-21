// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpu
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;

public interface ISkiaGpu : IPlatformGraphicsContext, IDisposable, IOptionalFeatureProvider
{
	ISkiaGpuRenderTarget? TryCreateRenderTarget(IEnumerable<object> P_0);

	ISkiaSurface? TryCreateSurface(PixelSize P_0, ISkiaGpuRenderSession? P_1);
}

