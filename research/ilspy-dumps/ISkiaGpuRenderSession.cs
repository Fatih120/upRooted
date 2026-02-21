// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpuRenderSession
using System;
using SkiaSharp;

public interface ISkiaGpuRenderSession : IDisposable
{
	GRContext GrContext { get; }

	SKSurface SkSurface { get; }

	double ScaleFactor { get; }

	GRSurfaceOrigin SurfaceOrigin { get; }
}

