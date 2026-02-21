// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ISceneBrushContent
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Platform;

[NotClientImplementable]
public interface ISceneBrushContent : IImmutableBrush, IBrush, IDisposable
{
	ITileBrush Brush { get; }

	Rect Rect { get; }

	internal bool UseScalableRasterization { get; }

	void Render(IDrawingContextImpl P_0, Matrix? P_1);
}

