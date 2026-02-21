// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.MonoPInvokeCallbackAttribute
using System;
using System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class MonoPInvokeCallbackAttribute : Attribute
{
	[CompilerGenerated]
	private Type _003CType_003Ek__BackingField;

	private Type Type
	{
		[CompilerGenerated]
		set
		{
			_003CType_003Ek__BackingField = type;
		}
	}

	public MonoPInvokeCallbackAttribute(Type P_0)
	{
		Type = P_0;
	}
}

