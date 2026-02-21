// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ImmutableBlurEffect
using System;
using Avalonia.Animation.Animators;
using Avalonia.Media;

public class ImmutableBlurEffect : IBlurEffect, IEffect, IImmutableEffect, IEquatable<IEffect>
{
	public double Radius { get; }

	static ImmutableBlurEffect()
	{
		EffectAnimator.EnsureRegistered();
	}

	public ImmutableBlurEffect(double P_0)
	{
		Radius = P_0;
	}

	public bool Equals(IEffect? P_0)
	{
		if (P_0 is IBlurEffect blurEffect)
		{
			return blurEffect.Radius == Radius;
		}
		return false;
	}
}

