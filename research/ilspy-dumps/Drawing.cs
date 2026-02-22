// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.Drawing
using Avalonia;
using Avalonia.Media;

public abstract class Drawing : AvaloniaObject
{
	internal Drawing()
	{
	}

	public void Draw(DrawingContext P_0)
	{
		DrawCore(P_0);
	}

	internal abstract void DrawCore(DrawingContext P_0);

	public abstract Rect GetBounds();
}

