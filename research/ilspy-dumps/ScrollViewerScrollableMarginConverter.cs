// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.ScrollViewerScrollableMarginConverter
using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

internal class ScrollViewerScrollableMarginConverter : IMultiValueConverter
{
	public object Convert(IList<object?> P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 == null || P_0.Count != 2 || !(P_0[0] is double num) || !(P_0[1] is double num2))
		{
			return new Thickness(0.0);
		}
		return (num > num2) ? new Thickness(0.0, 0.0, 8.0, 0.0) : new Thickness(0.0);
	}
}

