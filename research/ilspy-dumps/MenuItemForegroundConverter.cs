// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.Settings.MenuItemForegroundConverter
using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Styling;

public class MenuItemForegroundConverter : IMultiValueConverter
{
	public object Convert(IList<object?> P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0.Count < 2 || !(P_0[0] is bool flag) || !(P_0[1] is ThemeVariant))
		{
			return getResource((P_2 as string) ?? "TextPrimary");
		}
		string text = (flag ? "TextTertiary" : ((P_2 as string) ?? "TextPrimary"));
		return getResource(text);
	}

	private static IBrush getResource(string P_0)
	{
		return (IBrush)Application.Current.FindResource(Application.Current.ActualThemeVariant, P_0);
	}
}

