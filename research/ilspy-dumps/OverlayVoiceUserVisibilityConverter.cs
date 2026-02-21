using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace RootApp.Client.Avalonia.UI.Overlay;

public class OverlayVoiceUserVisibilityConverter : IMultiValueConverter
{
	public object? Convert(IList<object?> P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0.Count < 2)
		{
			return true;
		}
		object obj = P_0[0];
		bool flag = default(bool);
		int num;
		if (obj is bool)
		{
			flag = (bool)obj;
			num = 1;
		}
		else
		{
			num = 0;
		}
		bool flag2 = (byte)((uint)num & (flag ? 1u : 0u)) != 0;
		obj = P_0[1];
		bool flag3 = default(bool);
		int num2;
		if (obj is bool)
		{
			flag3 = (bool)obj;
			num2 = 1;
		}
		else
		{
			num2 = 0;
		}
		bool flag4 = (byte)((uint)num2 & (flag3 ? 1u : 0u)) != 0;
		return !flag4 || flag2;
	}
}
