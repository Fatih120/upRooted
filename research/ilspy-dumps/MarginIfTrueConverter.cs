// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.MarginIfTrueConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

public class MarginIfTrueConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		Thickness thickness = Thickness.Parse(P_2.ToString());
		if (P_0 is bool flag)
		{
			return flag ? thickness : new Thickness(0.0);
		}
		return new Thickness(0.0);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

