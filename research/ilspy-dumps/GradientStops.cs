// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.GradientStops
using System.Collections.Generic;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Media.Immutable;

public class GradientStops : AvaloniaList<GradientStop>
{
	public GradientStops()
	{
		base.ResetBehavior = ResetBehavior.Remove;
	}

	public IReadOnlyList<ImmutableGradientStop> ToImmutable()
	{
		int count = base.Count;
		ImmutableGradientStop[] array = new ImmutableGradientStop[count];
		for (int i = 0; i < count; i++)
		{
			GradientStop gradientStop = base[i];
			array[i] = new ImmutableGradientStop(gradientStop.Offset, gradientStop.Color);
		}
		return array;
	}
}

