// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.CharacterHit
using System;
using Avalonia.Media;

public readonly struct CharacterHit(int P_0, int P_1 = 0) : IEquatable<CharacterHit>
{
	public int FirstCharacterIndex { get; } = P_0;

	public int TrailingLength { get; } = P_1;

	public bool Equals(CharacterHit P_0)
	{
		if (FirstCharacterIndex == P_0.FirstCharacterIndex)
		{
			return TrailingLength == P_0.TrailingLength;
		}
		return false;
	}

	public override bool Equals(object? P_0)
	{
		if (P_0 is CharacterHit characterHit)
		{
			return Equals(characterHit);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (FirstCharacterIndex * 397) ^ TrailingLength;
	}

	public static bool operator ==(CharacterHit P_0, CharacterHit P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(CharacterHit P_0, CharacterHit P_1)
	{
		return !P_0.Equals(P_1);
	}
}

