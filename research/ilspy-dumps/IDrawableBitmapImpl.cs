// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.IDrawableBitmapImpl
using System;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal interface IDrawableBitmapImpl : IBitmapImpl, IDisposable
{
	void Draw(DrawingContextImpl P_0, SKRect P_1, SKRect P_2, SKPaint P_3);
}

