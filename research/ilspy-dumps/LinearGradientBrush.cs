// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.LinearGradientBrush
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;

public sealed class LinearGradientBrush : GradientBrush, ILinearGradientBrush, IGradientBrush, IBrush
{
	public static readonly StyledProperty<RelativePoint> StartPointProperty = AvaloniaProperty.Register<LinearGradientBrush, RelativePoint>("StartPoint", RelativePoint.TopLeft);

	public static readonly StyledProperty<RelativePoint> EndPointProperty = AvaloniaProperty.Register<LinearGradientBrush, RelativePoint>("EndPoint", RelativePoint.BottomRight);

	public RelativePoint StartPoint
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

	public RelativePoint EndPoint
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

	internal override Func<Compositor, ServerCompositionSimpleBrush> Factory => (Compositor c) => new ServerCompositionSimpleLinearGradientBrush(c.Server);

	public override IImmutableBrush ToImmutable()
	{
		return new ImmutableLinearGradientBrush(this);
	}

	private protected override void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		base.SerializeChanges(P_0, P_1);
		ServerCompositionSimpleLinearGradientBrush.SerializeAllChanges(P_1, StartPoint, EndPoint);
	}
}

