// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ImmutableDropShadowDirectionEffect
using System;
using Avalonia.Animation.Animators;
using Avalonia.Media;

public class ImmutableDropShadowDirectionEffect : IDirectionDropShadowEffect, IDropShadowEffect, IEffect, IImmutableEffect, IEquatable<IEffect>
{
	public double OffsetX => Math.Cos(Direction * Math.PI / 180.0) * ShadowDepth;

	public double OffsetY => Math.Sin(Direction * Math.PI / 180.0) * ShadowDepth;

	public double Direction { get; }

	public double ShadowDepth { get; }

	public double BlurRadius { get; }

	public Color Color { get; }

	public double Opacity { get; }

	static ImmutableDropShadowDirectionEffect()
	{
		EffectAnimator.EnsureRegistered();
	}

	public ImmutableDropShadowDirectionEffect(double P_0, double P_1, double P_2, Color P_3, double P_4)
	{
		Direction = P_0;
		ShadowDepth = P_1;
		BlurRadius = P_2;
		Color = P_3;
		Opacity = P_4;
	}

	public bool Equals(IEffect? P_0)
	{
		if (P_0 is IDropShadowEffect dropShadowEffect && dropShadowEffect.OffsetX == OffsetX && dropShadowEffect.OffsetY == OffsetY && dropShadowEffect.BlurRadius == BlurRadius && dropShadowEffect.Color == Color)
		{
			return dropShadowEffect.Opacity == Opacity;
		}
		return false;
	}
}

