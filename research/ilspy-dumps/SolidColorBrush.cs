// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.SolidColorBrush
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;

public sealed class SolidColorBrush : Brush, ISolidColorBrush, IBrush, IMutableBrush
{
	public static readonly StyledProperty<Color> ColorProperty = AvaloniaProperty.Register<SolidColorBrush, Color>("Color");

	public Color Color
	{
		get
		{
			return GetValue(ColorProperty);
		}
		set
		{
			SetValue(ColorProperty, value2);
		}
	}

	internal override Func<Compositor, ServerCompositionSimpleBrush> Factory => (Compositor c) => new ServerCompositionSimpleSolidColorBrush(c.Server);

	public SolidColorBrush()
	{
	}

	public SolidColorBrush(Color P_0, double P_1 = 1.0)
	{
		Color = P_0;
		base.Opacity = P_1;
	}

	public override string ToString()
	{
		return Color.ToString();
	}

	public IImmutableBrush ToImmutable()
	{
		return new ImmutableSolidColorBrush(this);
	}

	private protected override void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		base.SerializeChanges(P_0, P_1);
		ServerCompositionSimpleSolidColorBrush.SerializeAllChanges(P_1, Color);
	}
}

