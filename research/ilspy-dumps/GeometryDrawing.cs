// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.GeometryDrawing
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Metadata;

public sealed class GeometryDrawing : Drawing
{
	private static readonly IPen s_boundsPen = new ImmutablePen(Colors.Black.ToUInt32(), 0.0);

	public static readonly StyledProperty<Geometry?> GeometryProperty = AvaloniaProperty.Register<GeometryDrawing, Geometry>("Geometry");

	public static readonly StyledProperty<IBrush?> BrushProperty = AvaloniaProperty.Register<GeometryDrawing, IBrush>("Brush", Brushes.Transparent);

	public static readonly StyledProperty<IPen?> PenProperty = AvaloniaProperty.Register<GeometryDrawing, IPen>("Pen");

	[Content]
	public Geometry? Geometry
	{
		get
		{
			return GetValue(GeometryProperty);
		}
		set
		{
			SetValue(GeometryProperty, value2);
		}
	}

	public IBrush? Brush
	{
		get
		{
			return GetValue(BrushProperty);
		}
		set
		{
			SetValue(BrushProperty, value2);
		}
	}

	public IPen? Pen => GetValue(PenProperty);

	internal override void DrawCore(DrawingContext P_0)
	{
		if (Geometry != null)
		{
			P_0.DrawGeometry(Brush, Pen, Geometry);
		}
	}

	public override Rect GetBounds()
	{
		IPen pen = Pen ?? s_boundsPen;
		return Geometry?.GetRenderBounds(pen) ?? default(Rect);
	}
}

