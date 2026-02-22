// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.ScrollToVisibilityConverter
using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

internal class ScrollToVisibilityConverter : IMultiValueConverter
{
	public object Convert(IList<object?> P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0[0] is double num && P_0[1] is bool flag && true && flag && num > 0.0)
		{
			return true;
		}
		return false;
	}
}

