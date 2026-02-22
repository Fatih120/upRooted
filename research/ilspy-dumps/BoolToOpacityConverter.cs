// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.BoolToOpacityConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class BoolToOpacityConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is bool flag)
		{
			return flag ? 0.5 : 1.0;
		}
		return 1.0;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

