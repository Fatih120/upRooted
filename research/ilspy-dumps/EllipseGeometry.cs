// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.EllipseGeometry
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;

public class EllipseGeometry : Geometry
{
	public static readonly StyledProperty<Rect> RectProperty;

	public static readonly StyledProperty<double> RadiusXProperty;

	public static readonly StyledProperty<double> RadiusYProperty;

	public static readonly StyledProperty<Point> CenterProperty;

	public Rect Rect
	{
		get
		{
			return GetValue(RectProperty);
		}
		set
		{
			SetValue(RectProperty, value2);
		}
	}

	public double RadiusX
	{
		get
		{
			return GetValue(RadiusXProperty);
		}
		set
		{
			SetValue(RadiusXProperty, value2);
		}
	}

	public double RadiusY
	{
		get
		{
			return GetValue(RadiusYProperty);
		}
		set
		{
			SetValue(RadiusYProperty, value2);
		}
	}

	public Point Center
	{
		get
		{
			return GetValue(CenterProperty);
		}
		set
		{
			SetValue(CenterProperty, value2);
		}
	}

	static EllipseGeometry()
	{
		RectProperty = AvaloniaProperty.Register<EllipseGeometry, Rect>("Rect");
		RadiusXProperty = AvaloniaProperty.Register<EllipseGeometry, double>("RadiusX", 0.0);
		RadiusYProperty = AvaloniaProperty.Register<EllipseGeometry, double>("RadiusY", 0.0);
		CenterProperty = AvaloniaProperty.Register<EllipseGeometry, Point>("Center");
		Geometry.AffectsGeometry(RectProperty, RadiusXProperty, RadiusYProperty, CenterProperty);
	}

	public EllipseGeometry()
	{
	}

	public EllipseGeometry(Rect P_0)
		: this()
	{
		Rect = P_0;
	}

	public override Geometry Clone()
	{
		return new EllipseGeometry
		{
			Rect = Rect,
			RadiusX = RadiusX,
			RadiusY = RadiusY,
			Center = Center
		};
	}

	private protected sealed override IGeometryImpl? CreateDefiningGeometry()
	{
		IPlatformRenderInterface requiredService = AvaloniaLocator.Current.GetRequiredService<IPlatformRenderInterface>();
		if (Rect != default(Rect))
		{
			return requiredService.CreateEllipseGeometry(Rect);
		}
		double num = Center.X - RadiusX;
		double num2 = Center.Y - RadiusY;
		double num3 = RadiusX * 2.0;
		double num4 = RadiusY * 2.0;
		return requiredService.CreateEllipseGeometry(new Rect(num, num2, num3, num4));
	}
}

