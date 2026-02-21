// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IImageBrushSource
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.Utilities;

[NotClientImplementable]
public interface IImageBrushSource
{
	internal IRef<IBitmapImpl>? Bitmap { get; }
}

