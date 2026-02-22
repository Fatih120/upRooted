// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.MenuItemToggleTypeConverter
using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

public class MenuItemToggleTypeConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is MenuItemToggleType menuItemToggleType)
		{
			return menuItemToggleType == MenuItemToggleType.CheckBox;
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

