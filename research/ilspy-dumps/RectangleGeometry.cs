// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.RectangleGeometry
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;

public class RectangleGeometry : Geometry
{
	public static readonly StyledProperty<double> RadiusXProperty;

	public static readonly StyledProperty<double> RadiusYProperty;

	public static readonly StyledProperty<Rect> RectProperty;

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

	static RectangleGeometry()
	{
		RadiusXProperty = AvaloniaProperty.Register<RectangleGeometry, double>("RadiusX", 0.0);
		RadiusYProperty = AvaloniaProperty.Register<RectangleGeometry, double>("RadiusY", 0.0);
		RectProperty = AvaloniaProperty.Register<RectangleGeometry, Rect>("Rect");
		Geometry.AffectsGeometry(RadiusXProperty, RadiusYProperty, RectProperty);
	}

	public RectangleGeometry()
	{
	}

	public RectangleGeometry(Rect P_0)
	{
		Rect = P_0;
	}

	public RectangleGeometry(Rect P_0, double P_1, double P_2)
	{
		Rect = P_0;
		RadiusX = P_1;
		RadiusY = P_2;
	}

	public override Geometry Clone()
	{
		return new RectangleGeometry(Rect, RadiusX, RadiusY);
	}

	private protected sealed override IGeometryImpl? CreateDefiningGeometry()
	{
		double radiusX = RadiusX;
		double radiusY = RadiusY;
		IPlatformRenderInterface requiredService = AvaloniaLocator.Current.GetRequiredService<IPlatformRenderInterface>();
		if (radiusX == 0.0 && radiusY == 0.0)
		{
			return requiredService.CreateRectangleGeometry(Rect);
		}
		IStreamGeometryImpl streamGeometryImpl = requiredService.CreateStreamGeometry();
		using StreamGeometryContext streamGeometryContext = new StreamGeometryContext(streamGeometryImpl.Open());
		GeometryBuilder.DrawRoundedCornersRectangle(streamGeometryContext, Rect, radiusX, radiusY);
		return streamGeometryImpl;
	}
}

