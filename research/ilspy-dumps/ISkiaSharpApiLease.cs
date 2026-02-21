// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaSharpApiLease
using System;
using Avalonia.Metadata;
using Avalonia.Skia;
using SkiaSharp;

[Unstable]
public interface ISkiaSharpApiLease : IDisposable
{
	SKCanvas SkCanvas { get; }

	GRContext? GrContext { get; }

	ISkiaSharpPlatformGraphicsApiLease? TryLeasePlatformGraphicsApi();
}

