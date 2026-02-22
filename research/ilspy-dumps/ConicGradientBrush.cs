// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ConicGradientBrush
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;

public sealed class ConicGradientBrush : GradientBrush, IConicGradientBrush, IGradientBrush, IBrush
{
	public static readonly StyledProperty<RelativePoint> CenterProperty = AvaloniaProperty.Register<ConicGradientBrush, RelativePoint>("Center", RelativePoint.Center);

	public static readonly StyledProperty<double> AngleProperty = AvaloniaProperty.Register<ConicGradientBrush, double>("Angle", 0.0);

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

	public double Angle
	{
		get
		{
			return GetValue(AngleProperty);
		}
		set
		{
			SetValue(AngleProperty, value2);
		}
	}

	internal override Func<Compositor, ServerCompositionSimpleBrush> Factory => (Compositor c) => new ServerCompositionSimpleConicGradientBrush(c.Server);

	public override IImmutableBrush ToImmutable()
	{
		return new ImmutableConicGradientBrush(this);
	}

	private protected override void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		base.SerializeChanges(P_0, P_1);
		ServerCompositionSimpleConicGradientBrush.SerializeAllChanges(P_1, Angle, Center);
	}
}

