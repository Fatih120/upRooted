// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.BrushExtensions
using System;
using Avalonia.Media;
using Avalonia.Media.Immutable;

public static class BrushExtensions
{
	public static IImmutableBrush ToImmutable(this IBrush P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("brush");
		}
		return (P_0 as IMutableBrush)?.ToImmutable() ?? ((IImmutableBrush)P_0);
	}

	public static ImmutableDashStyle ToImmutable(this IDashStyle P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("style");
		}
		return (P_0 as ImmutableDashStyle) ?? ((DashStyle)P_0).ToImmutable();
	}
}

