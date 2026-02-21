// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRenderRoot
using Avalonia;
using Avalonia.Metadata;
using Avalonia.Rendering;

[NotClientImplementable]
public interface IRenderRoot
{
	Size ClientSize { get; }

	IRenderer Renderer { get; }

	IHitTester HitTester { get; }

	double RenderScaling { get; }

	Point PointToClient(PixelPoint P_0);

	PixelPoint PointToScreen(Point P_0);
}

