// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpuRenderTarget2
using System;
using Avalonia;
using Avalonia.Metadata;
using Avalonia.Skia;

[PrivateApi]
public interface ISkiaGpuRenderTarget2 : ISkiaGpuRenderTarget, IDisposable
{
	ISkiaGpuRenderSession BeginRenderingSession(PixelSize P_0);
}

