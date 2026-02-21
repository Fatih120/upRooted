// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.TypeUtilities
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Utilities;

public static class TypeUtilities
{
	[Flags]
	internal enum OperatorType
	{
		Implicit = 1,
		Explicit = 2
	}

	private static readonly int[] Conversions = new int[15]
	{
		24573, 17406, 24575, 24575, 24575, 24575, 24575, 24575, 24575, 24575,
		24573, 24573, 24573, 24576, 32767
	};

	private static readonly int[] ImplicitConversions = new int[15]
	{
		1, 7650, 7508, 8184, 7504, 8160, 7488, 8064, 7424, 7680,
		3072, 2048, 4096, 8192, 16384
	};

	private static readonly Type[] InbuiltTypes = new Type[15]
	{
		typeof(bool),
		typeof(char),
		typeof(sbyte),
		typeof(byte),
		typeof(short),
		typeof(ushort),
		typeof(int),
		typeof(uint),
		typeof(long),
		typeof(ulong),
		typeof(float),
		typeof(double),
		typeof(decimal),
		typeof(DateTime),
		typeof(string)
	};

	private static readonly Type[] NumericTypes = new Type[11]
	{
		typeof(byte),
		typeof(decimal),
		typeof(double),
		typeof(short),
		typeof(int),
		typeof(long),
		typeof(sbyte),
		typeof(float),
		typeof(ushort),
		typeof(uint),
		typeof(ulong)
	};

	public static bool AcceptsNull(Type P_0)
	{
		if (P_0.IsValueType)
		{
			return IsNullableType(P_0);
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AcceptsNull<T>()
	{
		return default(T) == null;
	}

	public static bool CanCast<T>(object? P_0)
	{
		if (!(P_0 is T))
		{
			if (P_0 == null)
			{
				return AcceptsNull<T>();
			}
			return false;
		}
		return true;
	}

	[RequiresUnreferencedCode("Conversion methods are required for type conversion, including op_Implicit, op_Explicit, Parse and TypeConverter.")]
	public static bool TryConvert(Type to, object? value, CultureInfo? culture, out object? result)
	{
		if (to == typeof(object))
		{
			result = value;
			return true;
		}
		if (value == null)
		{
			result = null;
			return AcceptsNull(to);
		}
		if (value == AvaloniaProperty.UnsetValue)
		{
			result = value;
			return true;
		}
		Type type = Nullable.GetUnderlyingType(to) ?? to;
		Type type2 = value.GetType();
		if (type.IsAssignableFrom(type2))
		{
			result = value;
			return true;
		}
		if (type == typeof(string))
		{
			result = Convert.ToString(value, culture);
			return true;
		}
		if (type.IsEnum && type2 == typeof(string) && Enum.IsDefined(type, (string)value))
		{
			result = Enum.Parse(type, (string)value);
			return true;
		}
		if (!type2.IsEnum && type.IsEnum)
		{
			result = null;
			if (TryConvert(Enum.GetUnderlyingType(type), value, culture, out object result2))
			{
				result = Enum.ToObject(type, result2);
				return true;
			}
		}
		if (type2.IsEnum && IsNumeric(type))
		{
			try
			{
				result = Convert.ChangeType((int)value, type, culture);
				return true;
			}
			catch
			{
				result = null;
				return false;
			}
		}
		int num = Array.IndexOf(InbuiltTypes, type2);
		int num2 = Array.IndexOf(InbuiltTypes, type);
		if (num != -1 && num2 != -1 && (Conversions[num] & (1 << num2)) != 0)
		{
			try
			{
				result = Convert.ChangeType(value, type, culture);
				return true;
			}
			catch
			{
				result = null;
				return false;
			}
		}
		TypeConverter converter = TypeDescriptor.GetConverter(type);
		if (converter.CanConvertFrom(type2))
		{
			result = converter.ConvertFrom(null, culture, value);
			return true;
		}
		TypeConverter converter2 = TypeDescriptor.GetConverter(type2);
		if (converter2.CanConvertTo(type))
		{
			result = converter2.ConvertTo(null, culture, value, type);
			return true;
		}
		MethodInfo methodInfo = FindTypeConversionOperatorMethod(type2, type, OperatorType.Implicit | OperatorType.Explicit);
		if (methodInfo != null)
		{
			result = methodInfo.Invoke(null, new object[1] { value });
			return true;
		}
		result = null;
		return false;
	}

	[RequiresUnreferencedCode("Implicit conversion methods are required for type conversion.")]
	public static bool TryConvertImplicit(Type P_0, object? P_1, out object? P_2)
	{
		if (P_1 == null)
		{
			P_2 = null;
			return AcceptsNull(P_0);
		}
		if (P_1 == AvaloniaProperty.UnsetValue)
		{
			P_2 = P_1;
			return true;
		}
		Type type = P_1.GetType();
		if (P_0.IsAssignableFrom(type))
		{
			P_2 = P_1;
			return true;
		}
		int num = Array.IndexOf(InbuiltTypes, type);
		int num2 = Array.IndexOf(InbuiltTypes, P_0);
		if (num != -1 && num2 != -1 && (ImplicitConversions[num] & (1 << num2)) != 0)
		{
			try
			{
				P_2 = Convert.ChangeType(P_1, P_0, CultureInfo.InvariantCulture);
				return true;
			}
			catch
			{
				P_2 = null;
				return false;
			}
		}
		MethodInfo methodInfo = FindTypeConversionOperatorMethod(type, P_0, OperatorType.Implicit);
		if (methodInfo != null)
		{
			P_2 = methodInfo.Invoke(null, new object[1] { P_1 });
			return true;
		}
		P_2 = null;
		return false;
	}

	public static bool IsNumeric(Type P_0)
	{
		Type underlyingType = Nullable.GetUnderlyingType(P_0);
		if (underlyingType != null)
		{
			return IsNumeric(underlyingType);
		}
		return Enumerable.Contains(NumericTypes, P_0);
	}

	private static bool IsNullableType(Type P_0)
	{
		if (P_0.IsGenericType)
		{
			return P_0.GetGenericTypeDefinition() == typeof(Nullable<>);
		}
		return false;
	}

	internal static MethodInfo? FindTypeConversionOperatorMethod([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] Type P_0, Type P_1, OperatorType P_2)
	{
		bool flag = P_2.HasAllFlags(OperatorType.Implicit);
		bool flag2 = P_2.HasAllFlags(OperatorType.Explicit);
		MethodInfo[] methods = P_0.GetMethods();
		foreach (MethodInfo methodInfo in methods)
		{
			if (methodInfo.IsSpecialName && !(methodInfo.ReturnType != P_1))
			{
				if (flag && methodInfo.Name == "op_Implicit")
				{
					return methodInfo;
				}
				if (flag2 && methodInfo.Name == "op_Explicit")
				{
					return methodInfo;
				}
			}
		}
		return null;
	}

	internal static bool IdentityEquals(object? P_0, object? P_1, Type P_2)
	{
		if (P_2.IsValueType || P_2 == typeof(string))
		{
			return object.Equals(P_0, P_1);
		}
		return P_0 == P_1;
	}
}

