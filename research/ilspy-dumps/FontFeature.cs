// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.FontFeature
using System;
using System.Runtime.CompilerServices;
using Avalonia.Media;

public class FontFeature : IEquatable<FontFeature>
{
	public string Tag
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[CompilerGenerated]
		get
		{
			throw new NotSupportedException("Linked away");
		}
	}

	public int Value
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[CompilerGenerated]
		get
		{
			throw new NotSupportedException("Linked away");
		}
	}

	public int Start
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[CompilerGenerated]
		get
		{
			throw new NotSupportedException("Linked away");
		}
	}

	public int End
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[CompilerGenerated]
		get
		{
			throw new NotSupportedException("Linked away");
		}
	}

	[CompilerGenerated]
	public static bool operator !=(FontFeature? P_0, FontFeature? P_1)
	{
		return !(P_0 == P_1);
	}

	[CompilerGenerated]
	public static bool operator ==(FontFeature? P_0, FontFeature? P_1)
	{
		if ((object)P_0 != P_1)
		{
			return P_0?.Equals(P_1) ?? false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[CompilerGenerated]
	public virtual bool Equals(FontFeature? P_0)
	{
		throw new NotSupportedException("Linked away");
	}
}

