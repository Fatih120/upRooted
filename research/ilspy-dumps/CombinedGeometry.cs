// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.CombinedGeometry
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;

public class CombinedGeometry : Geometry
{
	public static readonly StyledProperty<Geometry?> Geometry1Property = AvaloniaProperty.Register<CombinedGeometry, Geometry>("Geometry1");

	public static readonly StyledProperty<Geometry?> Geometry2Property = AvaloniaProperty.Register<CombinedGeometry, Geometry>("Geometry2");

	public static readonly StyledProperty<GeometryCombineMode> GeometryCombineModeProperty = AvaloniaProperty.Register<CombinedGeometry, GeometryCombineMode>("GeometryCombineMode", GeometryCombineMode.Union);

	public Geometry? Geometry1
	{
		get
		{
			return GetValue(Geometry1Property);
		}
		set
		{
			SetValue(Geometry1Property, value2);
		}
	}

	public Geometry? Geometry2
	{
		get
		{
			return GetValue(Geometry2Property);
		}
		set
		{
			SetValue(Geometry2Property, value2);
		}
	}

	public GeometryCombineMode GeometryCombineMode
	{
		get
		{
			return GetValue(GeometryCombineModeProperty);
		}
		set
		{
			SetValue(GeometryCombineModeProperty, value2);
		}
	}

	public CombinedGeometry(GeometryCombineMode P_0, Geometry? P_1, Geometry? P_2)
	{
		Geometry1 = P_1;
		Geometry2 = P_2;
		GeometryCombineMode = P_0;
	}

	public CombinedGeometry(GeometryCombineMode P_0, Geometry? P_1, Geometry? P_2, Transform? P_3)
	{
		Geometry1 = P_1;
		Geometry2 = P_2;
		GeometryCombineMode = P_0;
		base.Transform = P_3;
	}

	public override Geometry Clone()
	{
		return new CombinedGeometry(GeometryCombineMode, Geometry1, Geometry2, base.Transform);
	}

	private protected sealed override IGeometryImpl? CreateDefiningGeometry()
	{
		Geometry geometry = Geometry1;
		Geometry geometry2 = Geometry2;
		if (geometry?.PlatformImpl != null && geometry2?.PlatformImpl != null)
		{
			return AvaloniaLocator.Current.GetRequiredService<IPlatformRenderInterface>().CreateCombinedGeometry(GeometryCombineMode, geometry.PlatformImpl, geometry2.PlatformImpl);
		}
		if (GeometryCombineMode == GeometryCombineMode.Intersect)
		{
			return null;
		}
		object obj = geometry?.PlatformImpl;
		if (obj == null)
		{
			if (geometry2 == null)
			{
				return null;
			}
			obj = geometry2.PlatformImpl;
		}
		return (IGeometryImpl?)obj;
	}
}

