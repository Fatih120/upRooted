// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IImage
using Avalonia;
using Avalonia.Media;

public interface IImage
{
	Size Size { get; }

	void Draw(DrawingContext P_0, Rect P_1, Rect P_2);
}

