// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.ToolTips.PlacementToBoolConverter
using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace RootApp.Client.Avalonia.Resources.Converters.ToolTips;

public class PlacementToBoolConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is PlacementMode placementMode && P_2 is string b)
		{
			return string.Equals(placementMode.ToString(), b, StringComparison.OrdinalIgnoreCase);
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}
