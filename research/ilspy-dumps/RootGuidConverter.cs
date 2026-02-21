using System;
using System.ComponentModel;
using System.Globalization;
using RootApp.Core.Identifiers;

namespace RootApp.Core.Converters;

public class RootGuidConverter<T> : TypeConverter where T : struct, IRootGuid<T>
{
	public override bool CanConvertFrom(ITypeDescriptorContext? P_0, Type P_1)
	{
		return P_1 == typeof(string) || base.CanConvertFrom(P_0, P_1);
	}

	public override object? ConvertFrom(ITypeDescriptorContext? P_0, CultureInfo? P_1, object P_2)
	{
		T val;
		return (P_2 is string text && RootGuid.TryParse<T>(text, out val)) ? ((object)val) : base.ConvertFrom(P_0, P_1, P_2);
	}
}
