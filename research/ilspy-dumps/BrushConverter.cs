// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.BrushConverter
using System;
using System.ComponentModel;
using System.Globalization;
using Avalonia.Media;

public class BrushConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext? P_0, Type P_1)
	{
		return P_1 == typeof(string);
	}

	public override object? ConvertFrom(ITypeDescriptorContext? P_0, CultureInfo? P_1, object? P_2)
	{
		if (!(P_2 is string text))
		{
			return null;
		}
		return Brush.Parse(text);
	}
}

