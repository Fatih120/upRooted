// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.Settings.SaveChangesMarginConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

public class SaveChangesMarginConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
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
		if (((uint)num & (flag ? 1u : 0u)) != 0)
		{
			return new Thickness(0.0, 0.0, 0.0, 80.0);
		}
		return new Thickness(0.0);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}
