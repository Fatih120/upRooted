// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.AndConverter
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

internal class AndConverter : IMultiValueConverter
{
	public object Convert(IList<object?> P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0.Any((object x) => !(x is bool)))
		{
			return false;
		}
		return P_0.Cast<bool>().All((bool value) => value);
	}
}

