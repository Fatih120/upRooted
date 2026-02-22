// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.GeometryTypeConverter
using System;
using System.ComponentModel;
using System.Globalization;
using Avalonia.Media;

public class GeometryTypeConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext? P_0, Type P_1)
	{
		if (P_1 == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(P_0, P_1);
	}

	public override object? ConvertFrom(ITypeDescriptorContext? P_0, CultureInfo? P_1, object P_2)
	{
		if (P_2 == null)
		{
			throw GetConvertFromException(P_2);
		}
		if (P_2 is string text)
		{
			return Geometry.Parse(text);
		}
		return base.ConvertFrom(P_0, P_1, P_2);
	}
}

