// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ScaleTransform
using Avalonia;
using Avalonia.Media;

public sealed class ScaleTransform : Transform
{
	public static readonly StyledProperty<double> ScaleXProperty = AvaloniaProperty.Register<ScaleTransform, double>("ScaleX", 1.0);

	public static readonly StyledProperty<double> ScaleYProperty = AvaloniaProperty.Register<ScaleTransform, double>("ScaleY", 1.0);

	public double ScaleX
	{
		get
		{
			return GetValue(ScaleXProperty);
		}
		set
		{
			SetValue(ScaleXProperty, value2);
		}
	}

	public double ScaleY
	{
		get
		{
			return GetValue(ScaleYProperty);
		}
		set
		{
			SetValue(ScaleYProperty, value2);
		}
	}

	public override Matrix Value => Matrix.CreateScale(ScaleX, ScaleY);

	public ScaleTransform()
	{
	}

	public ScaleTransform(double P_0, double P_1)
		: this()
	{
		ScaleX = P_0;
		ScaleY = P_1;
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == ScaleXProperty || P_0.Property == ScaleYProperty)
		{
			RaiseChanged();
		}
	}
}

