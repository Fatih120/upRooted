// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.LineGeometry
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;

public class LineGeometry : Geometry
{
	public static readonly StyledProperty<Point> StartPointProperty;

	public static readonly StyledProperty<Point> EndPointProperty;

	public Point StartPoint
	{
		get
		{
			return GetValue(StartPointProperty);
		}
		set
		{
			SetValue(StartPointProperty, value2);
		}
	}

	public Point EndPoint
	{
		get
		{
			return GetValue(EndPointProperty);
		}
		set
		{
			SetValue(EndPointProperty, value2);
		}
	}

	static LineGeometry()
	{
		StartPointProperty = AvaloniaProperty.Register<LineGeometry, Point>("StartPoint");
		EndPointProperty = AvaloniaProperty.Register<LineGeometry, Point>("EndPoint");
		Geometry.AffectsGeometry(StartPointProperty, EndPointProperty);
	}

	public LineGeometry()
	{
	}

	public LineGeometry(Point P_0, Point P_1)
		: this()
	{
		StartPoint = P_0;
		EndPoint = P_1;
	}

	public override Geometry Clone()
	{
		return new LineGeometry(StartPoint, EndPoint);
	}

	private protected sealed override IGeometryImpl? CreateDefiningGeometry()
	{
		return AvaloniaLocator.Current.GetRequiredService<IPlatformRenderInterface>().CreateLineGeometry(StartPoint, EndPoint);
	}
}

