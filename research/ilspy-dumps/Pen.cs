// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.Pen
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Drawing;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;
using Avalonia.Utilities;

public sealed class Pen : AvaloniaObject, IPen, ICompositionRenderResource<IPen>, ICompositionRenderResource, ICompositorSerializable
{
	public static readonly StyledProperty<IBrush?> BrushProperty = AvaloniaProperty.Register<Pen, IBrush>("Brush");

	public static readonly StyledProperty<double> ThicknessProperty = AvaloniaProperty.Register<Pen, double>("Thickness", 1.0);

	public static readonly StyledProperty<IDashStyle?> DashStyleProperty = AvaloniaProperty.Register<Pen, IDashStyle>("DashStyle");

	public static readonly StyledProperty<PenLineCap> LineCapProperty = AvaloniaProperty.Register<Pen, PenLineCap>("LineCap", PenLineCap.Flat);

	public static readonly StyledProperty<PenLineJoin> LineJoinProperty = AvaloniaProperty.Register<Pen, PenLineJoin>("LineJoin", PenLineJoin.Bevel);

	public static readonly StyledProperty<double> MiterLimitProperty = AvaloniaProperty.Register<Pen, double>("MiterLimit", 10.0);

	private DashStyle? _subscribedToDashes;

	private TargetWeakEventSubscriber<Pen, EventArgs>? _weakSubscriber;

	private static readonly WeakEvent<DashStyle, EventArgs> InvalidatedWeakEvent = WeakEvent.Register(delegate(DashStyle s, EventHandler h)
	{
		s.Invalidated += h;
	}, delegate(DashStyle s, EventHandler h)
	{
		s.Invalidated -= h;
	});

	private CompositorResourceHolder<ServerCompositionSimplePen> _resource;

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

	public double Thickness
	{
		get
		{
			return GetValue(ThicknessProperty);
		}
		set
		{
			SetValue(ThicknessProperty, value2);
		}
	}

	public IDashStyle? DashStyle
	{
		get
		{
			return GetValue(DashStyleProperty);
		}
		set
		{
			SetValue(DashStyleProperty, value2);
		}
	}

	public PenLineCap LineCap
	{
		get
		{
			return GetValue(LineCapProperty);
		}
		set
		{
			SetValue(LineCapProperty, value2);
		}
	}

	public PenLineJoin LineJoin
	{
		get
		{
			return GetValue(LineJoinProperty);
		}
		set
		{
			SetValue(LineJoinProperty, value2);
		}
	}

	public double MiterLimit
	{
		get
		{
			return GetValue(MiterLimitProperty);
		}
		set
		{
			SetValue(MiterLimitProperty, value2);
		}
	}

	public Pen()
	{
	}

	public Pen(IBrush? P_0, double P_1 = 1.0, IDashStyle? P_2 = null, PenLineCap P_3 = PenLineCap.Flat, PenLineJoin P_4 = PenLineJoin.Miter, double P_5 = 10.0)
	{
		Brush = P_0;
		Thickness = P_1;
		LineCap = P_3;
		LineJoin = P_4;
		MiterLimit = P_5;
		DashStyle = P_2;
	}

	public ImmutablePen ToImmutable()
	{
		return new ImmutablePen(Brush?.ToImmutable(), Thickness, DashStyle?.ToImmutable(), LineCap, LineJoin, MiterLimit);
	}

	internal static bool TryModifyOrCreate(ref IPen? P_0, IBrush? P_1, double P_2, IList<double>? P_3 = null, double P_4 = 0.0, PenLineCap P_5 = PenLineCap.Flat, PenLineJoin P_6 = PenLineJoin.Miter, double P_7 = 10.0)
	{
		IPen pen = P_0;
		if (P_1 == null)
		{
			P_0 = null;
			return pen != null;
		}
		IDashStyle dashStyle = null;
		if (P_3 != null && P_3.Count > 0)
		{
			IDashStyle dashStyle3;
			if (!(P_3 is INotifyCollectionChanged))
			{
				IDashStyle dashStyle2 = new ImmutableDashStyle(P_3, P_4);
				dashStyle3 = dashStyle2;
			}
			else
			{
				IDashStyle dashStyle2 = new DashStyle(P_3, P_4);
				dashStyle3 = dashStyle2;
			}
			dashStyle = dashStyle3;
		}
		IImmutableBrush immutableBrush = P_1 as IImmutableBrush;
		bool flag = immutableBrush != null;
		if (flag)
		{
			bool flag2 = ((dashStyle == null || dashStyle is ImmutableDashStyle) ? true : false);
			flag = flag2;
		}
		if (flag)
		{
			P_0 = new ImmutablePen(immutableBrush, P_2, (ImmutableDashStyle)dashStyle, P_5, P_6, P_7);
			return true;
		}
		Pen pen2 = (pen as Pen) ?? new Pen();
		pen2.Brush = P_1;
		pen2.Thickness = P_2;
		pen2.LineCap = P_5;
		pen2.LineJoin = P_6;
		pen2.DashStyle = dashStyle;
		pen2.MiterLimit = P_7;
		P_0 = pen2;
		return !object.Equals(pen, P_0);
	}

	private void RegisterForSerialization()
	{
		_resource.RegisterForInvalidationOnAllCompositors(this);
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		RegisterForSerialization();
		if (P_0.Property == BrushProperty)
		{
			_resource.ProcessPropertyChangeNotification(P_0);
		}
		if (P_0.Property == DashStyleProperty)
		{
			UpdateDashStyleSubscription();
		}
		base.OnPropertyChanged(P_0);
	}

	private void UpdateDashStyleSubscription()
	{
		DashStyle dashStyle = (_resource.IsAttached ? (DashStyle as DashStyle) : null);
		if (_subscribedToDashes == dashStyle)
		{
			return;
		}
		if (_subscribedToDashes != null && _weakSubscriber != null)
		{
			InvalidatedWeakEvent.Unsubscribe(_subscribedToDashes, _weakSubscriber);
			_subscribedToDashes = null;
		}
		if (dashStyle == null)
		{
			return;
		}
		if (_weakSubscriber == null)
		{
			_weakSubscriber = new TargetWeakEventSubscriber<Pen, EventArgs>(this, delegate(Pen target, object? _, WeakEvent ev, EventArgs _)
			{
				if (ev == InvalidatedWeakEvent)
				{
					target.RegisterForSerialization();
				}
			});
		}
		InvalidatedWeakEvent.Subscribe(dashStyle, _weakSubscriber);
		_subscribedToDashes = dashStyle;
	}

	IPen ICompositionRenderResource<IPen>.GetForCompositor(Compositor P_0)
	{
		return _resource.GetForCompositor(P_0);
	}

	void ICompositionRenderResource.AddRefOnCompositor(Compositor P_0)
	{
		if (_resource.CreateOrAddRef(P_0, this, out ServerCompositionSimplePen _, (Compositor c) => new ServerCompositionSimplePen(c.Server)))
		{
			(Brush as ICompositionRenderResource)?.AddRefOnCompositor(P_0);
			UpdateDashStyleSubscription();
		}
	}

	void ICompositionRenderResource.ReleaseOnCompositor(Compositor P_0)
	{
		if (_resource.Release(P_0))
		{
			(Brush as ICompositionRenderResource)?.ReleaseOnCompositor(P_0);
			UpdateDashStyleSubscription();
		}
	}

	SimpleServerObject? ICompositorSerializable.TryGetServer(Compositor P_0)
	{
		return _resource.TryGetForCompositor(P_0);
	}

	void ICompositorSerializable.SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		ServerCompositionSimplePen.SerializeAllChanges(P_1, Brush.GetServer(P_0), DashStyle?.ToImmutable(), LineCap, LineJoin, MiterLimit, Thickness);
	}
}

