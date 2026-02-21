// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ITileBrush
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;

[NotClientImplementable]
public interface ITileBrush : IBrush
{
	AlignmentX AlignmentX { get; }

	AlignmentY AlignmentY { get; }

	RelativeRect DestinationRect { get; }

	RelativeRect SourceRect { get; }

	Stretch Stretch { get; }

	TileMode TileMode { get; }
}

