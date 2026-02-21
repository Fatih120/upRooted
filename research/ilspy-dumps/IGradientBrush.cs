// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IGradientBrush
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia.Metadata;

[NotClientImplementable]
public interface IGradientBrush : IBrush
{
	IReadOnlyList<IGradientStop> GradientStops { get; }

	GradientSpreadMethod SpreadMethod { get; }
}

