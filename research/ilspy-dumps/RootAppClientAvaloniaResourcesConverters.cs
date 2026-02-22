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

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.BoolInverterConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

internal class BoolInverterConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is bool flag)
		{
			return !flag;
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.BoolToOpacityConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class BoolToOpacityConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is bool flag)
		{
			return flag ? 0.5 : 1.0;
		}
		return 1.0;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.BoolToTopBorderThicknessConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

public class BoolToTopBorderThicknessConverter : IValueConverter
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
			return new Thickness(0.0, 0.5, 0.0, 0.0);
		}
		return new Thickness(0.0);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.CollapsedToAngleConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class CollapsedToAngleConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is bool flag)
		{
			return flag ? 270 : 0;
		}
		return 0;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

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

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.EnumToBoolConverter
using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

internal class EnumToBoolConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is Enum obj && P_2 is Enum obj2)
		{
			return obj.Equals(obj2);
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
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
		if (((uint)num & (flag ? 1u : 0u)) != 0 && P_2 is Enum result)
		{
			return result;
		}
		return BindingOperations.DoNothing;
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.GreaterThanZeroToTrueConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class GreaterThanZeroToTrueConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is int num)
		{
			return num > 0;
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.ItemsControlHasItemsConverter
using System;
using System.Globalization;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Data.Converters;

public class ItemsControlHasItemsConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is ItemsControl itemsControl)
		{
			return itemsControl.Items.Any();
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.MarginIfFalseConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

public class MarginIfFalseConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		Thickness thickness = Thickness.Parse(P_2.ToString());
		if (P_0 is bool flag)
		{
			return (!flag) ? thickness : new Thickness(0.0);
		}
		return new Thickness(0.0);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.MarginIfTrueConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

public class MarginIfTrueConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		Thickness thickness = Thickness.Parse(P_2.ToString());
		if (P_0 is bool flag)
		{
			return flag ? thickness : new Thickness(0.0);
		}
		return new Thickness(0.0);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.MediaRoomConnectionStatusToVisibleConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using RootApp.Client.CoreDomain.Models.Media;

public class MediaRoomConnectionStatusToVisibleConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is MediaRoomConnectionStatus mediaRoomConnectionStatus)
		{
			switch (mediaRoomConnectionStatus)
			{
			case MediaRoomConnectionStatus.Disconnecting:
			case MediaRoomConnectionStatus.Connecting:
			case MediaRoomConnectionStatus.Connected:
			case MediaRoomConnectionStatus.Error:
				return true;
			default:
				return false;
			}
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.MenuItemToggleTypeConverter
using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data.Converters;

public class MenuItemToggleTypeConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is MenuItemToggleType menuItemToggleType)
		{
			return menuItemToggleType == MenuItemToggleType.CheckBox;
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

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

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.OnlineStatusToIndexConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using RootApp.WebApi.Shared.Enums;

internal class OnlineStatusToIndexConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is UserOnlineStatus userOnlineStatus)
		{
			return userOnlineStatus switch
			{
				UserOnlineStatus.Active => 0, 
				UserOnlineStatus.Inactive => 1, 
				_ => 2, 
			};
		}
		return 2;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is int num)
		{
			return num switch
			{
				0 => UserOnlineStatus.Active, 
				1 => UserOnlineStatus.Inactive, 
				_ => UserOnlineStatus.Disconnected, 
			};
		}
		return UserOnlineStatus.Disconnected;
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.OnlineTextStatusConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using RootApp.WebApi.Shared.Enums;

internal class OnlineTextStatusConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is UserOnlineStatus userOnlineStatus)
		{
			return userOnlineStatus switch
			{
				UserOnlineStatus.Active => "Online", 
				UserOnlineStatus.Disconnected => "Offline", 
				UserOnlineStatus.Inactive => "Away", 
				_ => "Unknown status", 
			};
		}
		return "Invalid status";
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.OrConverter
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

internal class OrConverter : IMultiValueConverter
{
	public object Convert(IList<object?> P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0.Any((object x) => !(x is bool)))
		{
			return false;
		}
		return P_0.Cast<bool>().Any((bool value) => value);
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.PathToBitmapConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

public class PathToBitmapConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is string text)
		{
			return new Bitmap(text);
		}
		return null;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.PercentageToVisibilityConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class PercentageToVisibilityConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is int num && true && num > 0 && num <= 100)
		{
			return true;
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

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

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.StringNullOrEmptyToVisibilityConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class StringNullOrEmptyToVisibilityConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		string value = P_0 as string;
		return !string.IsNullOrEmpty(value);
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.ThemeToBoolConverter
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Styling;

internal class ThemeToBoolConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is ThemeVariant themeVariant && P_2 is ThemeVariant themeVariant2)
		{
			return themeVariant.Equals(themeVariant2);
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
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
		if (((uint)num & (flag ? 1u : 0u)) != 0 && P_2 is ThemeVariant result)
		{
			return result;
		}
		return Application.Current.RequestedThemeVariant;
	}
}

// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.VisibilityToOpacityConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;

public class VisibilityToOpacityConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is bool flag && true && flag)
		{
			return 1;
		}
		return 0;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}
