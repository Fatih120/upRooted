// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.DrawingGroup
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;

public sealed class DrawingGroup : Drawing
{
	public static readonly StyledProperty<double> OpacityProperty = AvaloniaProperty.Register<DrawingGroup, double>("Opacity", 1.0);

	public static readonly StyledProperty<Transform?> TransformProperty = AvaloniaProperty.Register<DrawingGroup, Transform>("Transform");

	public static readonly StyledProperty<Geometry?> ClipGeometryProperty = AvaloniaProperty.Register<DrawingGroup, Geometry>("ClipGeometry");

	public static readonly StyledProperty<IBrush?> OpacityMaskProperty = AvaloniaProperty.Register<DrawingGroup, IBrush>("OpacityMask");

	public static readonly DirectProperty<DrawingGroup, DrawingCollection> ChildrenProperty = AvaloniaProperty.RegisterDirect("Children", (DrawingGroup o) => o.Children, delegate(DrawingGroup o, DrawingCollection v)
	{
		o.Children = v;
	});

	private DrawingCollection _children = new DrawingCollection();

	public double Opacity => GetValue(OpacityProperty);

	public Transform? Transform => GetValue(TransformProperty);

	public Geometry? ClipGeometry
	{
		get
		{
			return GetValue(ClipGeometryProperty);
		}
		set
		{
			SetValue(ClipGeometryProperty, value2);
		}
	}

	public IBrush? OpacityMask => GetValue(OpacityMaskProperty);

	internal RenderOptions? RenderOptions { get; }

	[Content]
	public DrawingCollection Children
	{
		get
		{
			return _children;
		}
		set
		{
			SetAndRaise(ChildrenProperty, ref _children, value2);
		}
	}

	internal override void DrawCore(DrawingContext P_0)
	{
		Rect bounds = GetBounds();
		using (P_0.PushTransform(Transform?.Value ?? Matrix.Identity))
		{
			using (P_0.PushOpacity(Opacity))
			{
				using ((ClipGeometry != null) ? P_0.PushGeometryClip(ClipGeometry) : default(DrawingContext.PushedState))
				{
					using ((OpacityMask != null) ? P_0.PushOpacityMask(OpacityMask, bounds) : default(DrawingContext.PushedState))
					{
						using (RenderOptions.HasValue ? P_0.PushRenderOptions(RenderOptions.Value) : default(DrawingContext.PushedState))
						{
							foreach (Drawing child in Children)
							{
								child.Draw(P_0);
							}
						}
					}
				}
			}
		}
	}

	public override Rect GetBounds()
	{
		Rect result = default(Rect);
		foreach (Drawing child in Children)
		{
			result = result.Union(child.GetBounds());
		}
		if (Transform != null)
		{
			result = result.TransformToAABB(Transform.Value);
		}
		return result;
	}
}

