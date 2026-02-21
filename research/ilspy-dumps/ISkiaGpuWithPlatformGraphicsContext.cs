// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpuWithPlatformGraphicsContext
using System;
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.Skia;

[Unstable]
public interface ISkiaGpuWithPlatformGraphicsContext : ISkiaGpu, IPlatformGraphicsContext, IDisposable, IOptionalFeatureProvider
{
	IPlatformGraphicsContext? PlatformGraphicsContext { get; }
}

