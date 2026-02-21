// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TranslateTransform
using Avalonia;
using Avalonia.Media;

public sealed class TranslateTransform : Transform
{
	public static readonly StyledProperty<double> XProperty = AvaloniaProperty.Register<TranslateTransform, double>("X", 0.0);

	public static readonly StyledProperty<double> YProperty = AvaloniaProperty.Register<TranslateTransform, double>("Y", 0.0);

	public double X
	{
		get
		{
			return GetValue(XProperty);
		}
		set
		{
			SetValue(XProperty, value2);
		}
	}

	public double Y
	{
		get
		{
			return GetValue(YProperty);
		}
		set
		{
			SetValue(YProperty, value2);
		}
	}

	public override Matrix Value => Matrix.CreateTranslation(X, Y);

	public TranslateTransform()
	{
	}

	public TranslateTransform(double P_0, double P_1)
		: this()
	{
		X = P_0;
		Y = P_1;
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == XProperty || P_0.Property == YProperty)
		{
			RaiseChanged();
		}
	}
}

