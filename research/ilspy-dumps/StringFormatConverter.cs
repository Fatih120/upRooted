// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.StringFormatConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

internal class StringFormatConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_2 == null)
		{
			return string.Empty;
		}
		return string.Format(P_3, P_2.ToString() ?? string.Empty, P_0);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

