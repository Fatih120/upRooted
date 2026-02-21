// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.PathFigures
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Visuals.Platform;

public sealed class PathFigures : AvaloniaList<PathFigure>
{
	public static PathFigures Parse(string P_0)
	{
		PathGeometry pathGeometry = new PathGeometry();
		using (PathGeometryContext pathGeometryContext = new PathGeometryContext(pathGeometry))
		{
			using PathMarkupParser pathMarkupParser = new PathMarkupParser(pathGeometryContext);
			pathMarkupParser.Parse(P_0);
		}
		return pathGeometry.Figures;
	}
}

