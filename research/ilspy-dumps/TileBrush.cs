// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TileBrush
using Avalonia;
using Avalonia.Media;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;

public abstract class TileBrush : Brush, ITileBrush, IBrush
{
	public static readonly StyledProperty<AlignmentX> AlignmentXProperty = AvaloniaProperty.Register<TileBrush, AlignmentX>("AlignmentX", AlignmentX.Center);

	public static readonly StyledProperty<AlignmentY> AlignmentYProperty = AvaloniaProperty.Register<TileBrush, AlignmentY>("AlignmentY", AlignmentY.Center);

	public static readonly StyledProperty<RelativeRect> DestinationRectProperty = AvaloniaProperty.Register<TileBrush, RelativeRect>("DestinationRect", RelativeRect.Fill);

	public static readonly StyledProperty<RelativeRect> SourceRectProperty = AvaloniaProperty.Register<TileBrush, RelativeRect>("SourceRect", RelativeRect.Fill);

	public static readonly StyledProperty<Stretch> StretchProperty = AvaloniaProperty.Register<TileBrush, Stretch>("Stretch", Stretch.Uniform);

	public static readonly StyledProperty<TileMode> TileModeProperty = AvaloniaProperty.Register<TileBrush, TileMode>("TileMode", TileMode.None);

	public AlignmentX AlignmentX
	{
		get
		{
			return GetValue(AlignmentXProperty);
		}
		set
		{
			SetValue(AlignmentXProperty, value2);
		}
	}

	public AlignmentY AlignmentY
	{
		get
		{
			return GetValue(AlignmentYProperty);
		}
		set
		{
			SetValue(AlignmentYProperty, value2);
		}
	}

	public RelativeRect DestinationRect
	{
		get
		{
			return GetValue(DestinationRectProperty);
		}
		set
		{
			SetValue(DestinationRectProperty, value2);
		}
	}

	public RelativeRect SourceRect
	{
		get
		{
			return GetValue(SourceRectProperty);
		}
		set
		{
			SetValue(SourceRectProperty, value2);
		}
	}

	public Stretch Stretch
	{
		get
		{
			return GetValue(StretchProperty);
		}
		set
		{
			SetValue(StretchProperty, value2);
		}
	}

	public TileMode TileMode
	{
		get
		{
			return GetValue(TileModeProperty);
		}
		set
		{
			SetValue(TileModeProperty, value2);
		}
	}

	internal TileBrush()
	{
	}

	private protected override void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		base.SerializeChanges(P_0, P_1);
		ServerCompositionSimpleTileBrush.SerializeAllChanges(P_1, AlignmentX, AlignmentY, DestinationRect, SourceRect, Stretch, TileMode);
	}
}

