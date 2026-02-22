// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TransformExtensions
using System;
using Avalonia.Media;
using Avalonia.Media.Immutable;

public static class TransformExtensions
{
	public static ImmutableTransform ToImmutable(this ITransform P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("transform");
		}
		return (P_0 as Transform)?.ToImmutable() ?? new ImmutableTransform(P_0.Value);
	}
}

