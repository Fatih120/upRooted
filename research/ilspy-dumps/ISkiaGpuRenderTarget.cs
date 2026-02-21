// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpuRenderTarget
using System;
using Avalonia.Skia;

public interface ISkiaGpuRenderTarget : IDisposable
{
	bool IsCorrupted { get; }

	ISkiaGpuRenderSession BeginRenderingSession();
}

