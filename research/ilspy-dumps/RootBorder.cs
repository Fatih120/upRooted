// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootBorder
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using RootApp.Client.Avalonia.Controls;

public class RootBorder : Border
{
	public static readonly StyledProperty<Thickness> DynamicBorderThicknessProperty = AvaloniaProperty.Register<RootBorder, Thickness>("DynamicBorderThickness");

	protected override Type StyleKeyOverride => typeof(Border);

	public Thickness DynamicBorderThickness
	{
		get
		{
			return GetValue(DynamicBorderThicknessProperty);
		}
		set
		{
			SetValue(DynamicBorderThicknessProperty, value2);
		}
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == DynamicBorderThicknessProperty)
		{
			UpdateBorderThickness();
		}
	}

	private void UpdateBorderThickness()
	{
		Thickness dynamicBorderThickness = DynamicBorderThickness;
		double num = 1.0;
		if (Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime { MainWindow: not null } classicDesktopStyleApplicationLifetime)
		{
			num = classicDesktopStyleApplicationLifetime.MainWindow.RenderScaling;
		}
		double num2 = RoundSide(dynamicBorderThickness.Left, num);
		double num3 = RoundSide(dynamicBorderThickness.Top, num);
		double num4 = RoundSide(dynamicBorderThickness.Right, num);
		double num5 = RoundSide(dynamicBorderThickness.Bottom, num);
		base.BorderThickness = new Thickness(num2, num3, num4, num5);
	}

	private static double RoundSide(double P_0, double P_1)
	{
		if (P_0 == 0.0)
		{
			return P_0;
		}
		double num = P_0 * P_1;
		double num2 = Math.Round(num, MidpointRounding.AwayFromZero);
		return num2 / P_1;
	}
}
