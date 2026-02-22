// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.StringNullOrEmptyToVisibilityConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class StringNullOrEmptyToVisibilityConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		string value = P_0 as string;
		return !string.IsNullOrEmpty(value);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

