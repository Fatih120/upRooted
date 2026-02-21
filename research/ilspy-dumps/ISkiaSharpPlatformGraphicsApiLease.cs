// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaSharpPlatformGraphicsApiLease
using System;
using Avalonia.Metadata;
using Avalonia.Platform;

[Unstable]
public interface ISkiaSharpPlatformGraphicsApiLease : IDisposable
{
	IPlatformGraphicsContext Context { get; }
}

