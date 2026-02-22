// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.GeometryGroup
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Platform;

public class GeometryGroup : Geometry
{
	public static readonly DirectProperty<GeometryGroup, GeometryCollection> ChildrenProperty = AvaloniaProperty.RegisterDirect("Children", (GeometryGroup o) => o.Children, delegate(GeometryGroup o, GeometryCollection v)
	{
		o.Children = v;
	});

	public static readonly StyledProperty<FillRule> FillRuleProperty = AvaloniaProperty.Register<GeometryGroup, FillRule>("FillRule", FillRule.EvenOdd);

	private GeometryCollection _children;

	[Content]
	public GeometryCollection Children
	{
		get
		{
			return _children;
		}
		set
		{
			OnChildrenChanged(_children, geometryCollection);
			SetAndRaise(ChildrenProperty, ref _children, geometryCollection);
		}
	}

	public FillRule FillRule
	{
		get
		{
			return GetValue(FillRuleProperty);
		}
		set
		{
			SetValue(FillRuleProperty, value2);
		}
	}

	public GeometryGroup()
	{
		_children = new GeometryCollection
		{
			Parent = this
		};
	}

	public override Geometry Clone()
	{
		GeometryGroup geometryGroup = new GeometryGroup
		{
			FillRule = FillRule,
			Transform = base.Transform
		};
		if (_children.Count > 0)
		{
			geometryGroup.Children = new GeometryCollection(_children);
		}
		return geometryGroup;
	}

	protected void OnChildrenChanged(GeometryCollection P_0, GeometryCollection P_1)
	{
		P_0.Parent = null;
		P_1.Parent = this;
	}

	private protected sealed override IGeometryImpl? CreateDefiningGeometry()
	{
		if (_children.Count > 0)
		{
			IPlatformRenderInterface requiredService = AvaloniaLocator.Current.GetRequiredService<IPlatformRenderInterface>();
			IGeometryImpl[] array = new IGeometryImpl[_children.Count];
			for (int i = 0; i < _children.Count; i++)
			{
				array[i] = _children[i].PlatformImpl;
			}
			return requiredService.CreateGeometryGroup(FillRule, array);
		}
		return null;
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		string name = P_0.Property.Name;
		if (name == "FillRule" || name == "Children")
		{
			InvalidateGeometry();
		}
	}

	internal void Invalidate()
	{
		InvalidateGeometry();
	}
}

