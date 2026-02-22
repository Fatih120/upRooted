// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.MultiBoolInverterConverter
using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

internal class MultiBoolInverterConverter : IMultiValueConverter
{
	public object Convert(IList<object?> P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 != null && P_0.Count == 2 && P_0[0] is bool flag && P_0[1] is bool flag2)
		{
			return !flag && !flag2;
		}
		return false;
	}
}

