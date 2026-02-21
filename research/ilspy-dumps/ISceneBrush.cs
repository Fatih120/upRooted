// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ISceneBrush
using Avalonia.Media;
using Avalonia.Metadata;

[NotClientImplementable]
public interface ISceneBrush : ITileBrush, IBrush
{
	ISceneBrushContent? CreateContent();
}

