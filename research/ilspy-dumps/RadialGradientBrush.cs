// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.RadialGradientBrush
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Metadata;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;

public sealed class RadialGradientBrush : GradientBrush, IRadialGradientBrush, IGradientBrush, IBrush
{
	public static readonly StyledProperty<RelativePoint> CenterProperty = AvaloniaProperty.Register<RadialGradientBrush, RelativePoint>("Center", RelativePoint.Center);

	public static readonly StyledProperty<RelativePoint> GradientOriginProperty = AvaloniaProperty.Register<RadialGradientBrush, RelativePoint>("GradientOrigin", RelativePoint.Center);

	[Obsolete("Use RadiusX/RadiusY, note that those properties use _relative_ values, so Radius=0.55 would become RadiusX=55% RadiusY=55%. Radius property is always relative even if the rest of the brush uses absolute values.")]
	public static readonly StyledProperty<double> RadiusProperty = AvaloniaProperty.Register<RadialGradientBrush, double>("Radius", 0.5);

	public static readonly StyledProperty<RelativeScalar> RadiusXProperty = AvaloniaProperty.Register<RadialGradientBrush, RelativeScalar>("RadiusX", RelativeScalar.Middle);

	public static readonly StyledProperty<RelativeScalar> RadiusYProperty = AvaloniaProperty.Register<RadialGradientBrush, RelativeScalar>("RadiusY", RelativeScalar.Middle);

	public RelativePoint Center
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

	public RelativePoint GradientOrigin
	{
		get
		{
			return GetValue(GradientOriginProperty);
		}
		set
		{
			SetValue(GradientOriginProperty, value2);
		}
	}

	[DependsOn("Radius")]
	public RelativeScalar RadiusX
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

	[DependsOn("Radius")]
	public RelativeScalar RadiusY
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

	[Obsolete("Use RadiusX/RadiusY, note that those properties use _relative_ values, so Radius=0.55 would become RadiusX=55% RadiusY=55%. Radius property is always relative even if the rest of the brush uses absolute values.")]
	public double Radius => GetValue(RadiusProperty);

	internal override Func<Compositor, ServerCompositionSimpleBrush> Factory => (Compositor c) => new ServerCompositionSimpleRadialGradientBrush(c.Server);

	public override IImmutableBrush ToImmutable()
	{
		return new ImmutableRadialGradientBrush(this);
	}

	private protected override void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		base.SerializeChanges(P_0, P_1);
		ServerCompositionSimpleRadialGradientBrush.SerializeAllChanges(P_1, Center, GradientOrigin, RadiusX, RadiusY);
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		if (P_0.IsEffectiveValueChange && P_0.Property == RadiusProperty)
		{
			RelativeScalar value = new RelativeScalar(Radius, RelativeUnit.Relative);
			SetCurrentValue(RadiusXProperty, value);
			SetCurrentValue(RadiusYProperty, value);
		}
		base.OnPropertyChanged(P_0);
	}
}

