// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IDashStyle
using System.Collections.Generic;
using Avalonia.Metadata;

[NotClientImplementable]
public interface IDashStyle
{
	IReadOnlyList<double>? Dashes { get; }

	double Offset { get; }
}

