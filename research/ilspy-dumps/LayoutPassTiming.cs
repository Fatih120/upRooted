// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.LayoutPassTiming
using System;

internal readonly record struct LayoutPassTiming
{
	public int PassCounter { get; }

	public TimeSpan Elapsed { get; }

	public LayoutPassTiming(int P_0, TimeSpan P_1)
	{
		PassCounter = P_0;
		Elapsed = P_1;
	}
}

