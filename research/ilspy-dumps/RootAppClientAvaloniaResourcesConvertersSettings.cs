// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.Settings.MenuItemFontSizeConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class MenuItemFontSizeConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is int num && num != 0)
		{
			return 12;
		}
		return 14;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.Settings.MenuItemFontWeightConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

public class MenuItemFontWeightConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is int num && num != 0)
		{
			return FontWeight.Medium;
		}
		return 450;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

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

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.Settings.MenuItemMarginConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

public class MenuItemMarginConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is int num && num != 0)
		{
			return new Thickness(12.0, 12.0, 12.0, 0.0);
		}
		return new Thickness(12.0, 8.0, 12.0, 8.0);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.Settings.SaveChangesMarginConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

public class SaveChangesMarginConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		bool flag = default(bool);
		int num;
		if (P_0 is bool)
		{
			flag = (bool)P_0;
			num = 1;
		}
		else
		{
			num = 0;
		}
		if (((uint)num & (flag ? 1u : 0u)) != 0)
		{
			return new Thickness(0.0, 0.0, 0.0, 80.0);
		}
		return new Thickness(0.0);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}
