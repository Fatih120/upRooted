// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.Settings.MenuItemMarginConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

public class MenuItemMarginConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is int num && num != 0)
		{
			return new Thickness(12.0, 12.0, 12.0, 0.0);
		}
		return new Thickness(12.0, 8.0, 12.0, 8.0);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

