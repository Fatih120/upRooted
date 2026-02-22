// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.EffectExtensions
using System;
using Avalonia;
using Avalonia.Media;

public static class EffectExtensions
{
	private static double AdjustPaddingRadius(double P_0)
	{
		if (P_0 <= 0.0)
		{
			return 0.0;
		}
		return Math.Ceiling(P_0) + 1.0;
	}

	internal static Thickness GetEffectOutputPadding(this IEffect? P_0)
	{
		if (P_0 == null)
		{
			return default(Thickness);
		}
		if (P_0 is IBlurEffect blurEffect)
		{
			return new Thickness(AdjustPaddingRadius(blurEffect.Radius));
		}
		if (P_0 is IDropShadowEffect dropShadowEffect)
		{
			double num = AdjustPaddingRadius(dropShadowEffect.BlurRadius);
			Rect rect = new Rect(0.0 - num, 0.0 - num, num * 2.0, num * 2.0).Translate(new Vector(dropShadowEffect.OffsetX, dropShadowEffect.OffsetY));
			return new Thickness(Math.Max(0.0, 0.0 - rect.X), Math.Max(0.0, 0.0 - rect.Y), Math.Max(0.0, rect.Right), Math.Max(0.0, rect.Bottom));
		}
		throw new ArgumentException("Unknown effect type: " + P_0.GetType());
	}

	public static IImmutableEffect ToImmutable(this IEffect P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("effect");
		}
		return (P_0 as IMutableEffect)?.ToImmutable() ?? ((IImmutableEffect)P_0);
	}

	internal static bool EffectEquals(this IImmutableEffect? P_0, IEffect? P_1)
	{
		if (P_0 == null && P_1 == null)
		{
			return true;
		}
		if (P_0 != null && P_1 != null)
		{
			return P_0.Equals(P_1);
		}
		return false;
	}
}

