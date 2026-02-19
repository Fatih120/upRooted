// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.ThemeToBoolConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Styling;

internal class ThemeToBoolConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is ThemeVariant themeVariant && P_2 is ThemeVariant themeVariant2)
		{
			return themeVariant.Equals(themeVariant2);
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		bool flag = default(bool);
		int num;
		if (P_0 is bool)
		{
			flag = (bool)P_0;
			num = 1;
		}
		else
		{
			num = 0;
		}
		if (((uint)num & (flag ? 1u : 0u)) != 0 && P_2 is ThemeVariant result)
		{
			return result;
		}
		return Application.Current.RequestedThemeVariant;
	}
}
