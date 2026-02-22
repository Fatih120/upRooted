// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.EmptyStringFallbackConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class EmptyStringFallbackConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is string text && !string.IsNullOrWhiteSpace(text))
		{
			return text;
		}
		return P_2 ?? "Default Group Name";
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

