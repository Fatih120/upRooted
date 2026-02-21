// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.RendererDebugOverlays
using System;

[Flags]
public enum RendererDebugOverlays
{
	None = 0,
	Fps = 1,
	DirtyRects = 2,
	LayoutTimeGraph = 4,
	RenderTimeGraph = 8
}

