// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaSurface
using System;
using SkiaSharp;

public interface ISkiaSurface : IDisposable
{
	SKSurface Surface { get; }

	bool CanBlit { get; }

	void Blit(SKCanvas P_0);
}

