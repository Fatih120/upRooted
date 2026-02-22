// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.Rotate3DTransform
using System;
using System.Numerics;
using Avalonia;
using Avalonia.Media;

public sealed class Rotate3DTransform : Transform
{
	private readonly bool _isInitializing;

	public static readonly StyledProperty<double> AngleXProperty = AvaloniaProperty.Register<Rotate3DTransform, double>("AngleX", 0.0);

	public static readonly StyledProperty<double> AngleYProperty = AvaloniaProperty.Register<Rotate3DTransform, double>("AngleY", 0.0);

	public static readonly StyledProperty<double> AngleZProperty = AvaloniaProperty.Register<Rotate3DTransform, double>("AngleZ", 0.0);

	public static readonly StyledProperty<double> CenterXProperty = AvaloniaProperty.Register<Rotate3DTransform, double>("CenterX", 0.0);

	public static readonly StyledProperty<double> CenterYProperty = AvaloniaProperty.Register<Rotate3DTransform, double>("CenterY", 0.0);

	public static readonly StyledProperty<double> CenterZProperty = AvaloniaProperty.Register<Rotate3DTransform, double>("CenterZ", 0.0);

	public static readonly StyledProperty<double> DepthProperty = AvaloniaProperty.Register<Rotate3DTransform, double>("Depth", 0.0);

	public double AngleX => GetValue(AngleXProperty);

	public double AngleY => GetValue(AngleYProperty);

	public double AngleZ => GetValue(AngleZProperty);

	public double CenterX => GetValue(CenterXProperty);

	public double CenterY => GetValue(CenterYProperty);

	public double CenterZ => GetValue(CenterZProperty);

	public double Depth => GetValue(DepthProperty);

	public override Matrix Value
	{
		get
		{
			Matrix4x4 identity = Matrix4x4.Identity;
			double centerX = CenterX;
			double centerY = CenterY;
			double centerZ = CenterZ;
			double angleX = AngleX;
			double angleY = AngleY;
			double angleZ = AngleZ;
			double depth = Depth;
			double num = angleZ;
			double num2 = angleY;
			double num3 = angleX;
			double num4 = centerZ;
			double num5 = centerY;
			double num6 = centerX;
			double num7 = num6 + num5 + num4;
			if (Math.Abs(num7) > double.Epsilon)
			{
				identity *= Matrix4x4.CreateTranslation(0f - (float)num6, 0f - (float)num5, 0f - (float)num4);
			}
			if (num3 != 0.0)
			{
				identity *= Matrix4x4.CreateRotationX((float)Matrix.ToRadians(num3));
			}
			if (num2 != 0.0)
			{
				identity *= Matrix4x4.CreateRotationY((float)Matrix.ToRadians(num2));
			}
			if (num != 0.0)
			{
				identity *= Matrix4x4.CreateRotationZ((float)Matrix.ToRadians(num));
			}
			if (Math.Abs(num7) > double.Epsilon)
			{
				identity *= Matrix4x4.CreateTranslation((float)num6, (float)num5, (float)num4);
			}
			if (depth != 0.0)
			{
				Matrix4x4 identity2 = Matrix4x4.Identity;
				identity2.M34 = -1f / (float)depth;
				identity *= identity2;
			}
			return new Matrix(identity.M11, identity.M12, identity.M14, identity.M21, identity.M22, identity.M24, identity.M41, identity.M42, identity.M44);
		}
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (!_isInitializing)
		{
			RaiseChanged();
		}
	}
}

