// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.Effect
using System;
using Avalonia.Animation;
using Avalonia.Animation.Animators;
using Avalonia.Media;
using Avalonia.Rendering.Composition.Expressions;

public class Effect : Animatable
{
	private static Exception ParseError(string P_0)
	{
		throw new ArgumentException("Unable to parse effect: " + P_0);
	}

	public static IEffect Parse(string P_0)
	{
		ReadOnlySpan<char> readOnlySpan = P_0.AsSpan();
		TokenParser tokenParser = new TokenParser(readOnlySpan);
		if (tokenParser.TryConsume("blur"))
		{
			if (!tokenParser.TryConsume('(') || !tokenParser.TryParseDouble(out var num) || !tokenParser.TryConsume(')') || !tokenParser.IsEofWithWhitespace())
			{
				throw ParseError(P_0);
			}
			return new ImmutableBlurEffect(num);
		}
		if (tokenParser.TryConsume("drop-shadow"))
		{
			if (!tokenParser.TryConsume('(') || !tokenParser.TryParseDouble(out var num2) || !tokenParser.TryParseDouble(out var num3))
			{
				throw ParseError(P_0);
			}
			double num4 = 0.0;
			Color black = Colors.Black;
			if (!tokenParser.TryConsume(')'))
			{
				if (!tokenParser.TryParseDouble(out num4) || num4 < 0.0)
				{
					throw ParseError(P_0);
				}
				if (!tokenParser.TryConsume(')'))
				{
					int num5 = P_0.LastIndexOf(")", StringComparison.Ordinal);
					if (num5 == -1)
					{
						throw ParseError(P_0);
					}
					if (!new TokenParser(readOnlySpan.Slice(num5 + 1)).IsEofWithWhitespace())
					{
						throw ParseError(P_0);
					}
					if (!Color.TryParse(readOnlySpan.Slice(tokenParser.Position, num5 - tokenParser.Position).TrimEnd(), out black))
					{
						throw ParseError(P_0);
					}
					return new ImmutableDropShadowEffect(num2, num3, num4, black, 1.0);
				}
			}
			if (!tokenParser.IsEofWithWhitespace())
			{
				throw ParseError(P_0);
			}
			return new ImmutableDropShadowEffect(num2, num3, num4, black, 1.0);
		}
		throw ParseError(P_0);
	}

	static Effect()
	{
		EffectAnimator.EnsureRegistered();
	}
}

