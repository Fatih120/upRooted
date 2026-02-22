// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.SkewTransform
using Avalonia;
using Avalonia.Media;

public sealed class SkewTransform : Transform
{
	public static readonly StyledProperty<double> AngleXProperty = AvaloniaProperty.Register<SkewTransform, double>("AngleX", 0.0);

	public static readonly StyledProperty<double> AngleYProperty = AvaloniaProperty.Register<SkewTransform, double>("AngleY", 0.0);

	public double AngleX => GetValue(AngleXProperty);

	public double AngleY => GetValue(AngleYProperty);

	public override Matrix Value => Matrix.CreateSkew(Matrix.ToRadians(AngleX), Matrix.ToRadians(AngleY));

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == AngleXProperty || P_0.Property == AngleYProperty)
		{
			RaiseChanged();
		}
	}
}

