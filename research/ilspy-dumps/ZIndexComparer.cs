// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.ZIndexComparer
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Rendering;

internal class ZIndexComparer : IComparer<Visual>
{
	public static readonly ZIndexComparer Instance = new ZIndexComparer();

	public static readonly Comparison<Visual> ComparisonInstance = Instance.Compare;

	public int Compare(Visual? x, Visual? y)
	{
		return (x?.ZIndex ?? 0).CompareTo(y?.ZIndex ?? 0);
	}
}
