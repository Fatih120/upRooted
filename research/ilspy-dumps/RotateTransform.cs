// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.RotateTransform
using Avalonia;
using Avalonia.Media;
using Avalonia.Reactive;

public sealed class RotateTransform : Transform
{
	public static readonly StyledProperty<double> AngleProperty = AvaloniaProperty.Register<RotateTransform, double>("Angle", 0.0);

	public static readonly StyledProperty<double> CenterXProperty = AvaloniaProperty.Register<RotateTransform, double>("CenterX", 0.0);

	public static readonly StyledProperty<double> CenterYProperty = AvaloniaProperty.Register<RotateTransform, double>("CenterY", 0.0);

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

	public double CenterX => GetValue(CenterXProperty);

	public double CenterY => GetValue(CenterYProperty);

	public override Matrix Value => Matrix.CreateTranslation(0.0 - CenterX, 0.0 - CenterY) * Matrix.CreateRotation(Matrix.ToRadians(Angle)) * Matrix.CreateTranslation(CenterX, CenterY);

	public RotateTransform()
	{
		this.GetObservable(AngleProperty).Subscribe(delegate
		{
			RaiseChanged();
		});
	}
}

