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

