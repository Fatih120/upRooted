// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.PrependStringConverter
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

internal class PrependStringConverter : IMultiValueConverter
{
	public object Convert(IList<object?> P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0.Count > 1 && P_0[0] is string text && P_0[1] is string text2)
		{
			return text2 + text;
		}
		return P_0.FirstOrDefault() ?? string.Empty;
	}
}

