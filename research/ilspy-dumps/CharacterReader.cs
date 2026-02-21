// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.CharacterReader
using System;
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

public ref struct CharacterReader
{
	private ReadOnlySpan<char> _s;

	[CompilerGenerated]
	private int _003CPosition_003Ek__BackingField;

	public bool End => _s.IsEmpty;

	public char Peek => _s[0];

	public int Position
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CPosition_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CPosition_003Ek__BackingField = num;
		}
	}

	public CharacterReader(ReadOnlySpan<char> P_0)
	{
		this = default(CharacterReader);
		_s = P_0;
	}

	public char Take()
	{
		Position++;
		char result = _s[0];
		_s = _s.Slice(1);
		return result;
	}

	public void SkipWhitespace()
	{
		ReadOnlySpan<char> s = _s.TrimStart();
		Position += _s.Length - s.Length;
		_s = s;
	}

	public bool TakeIf(char P_0)
	{
		if (Peek == P_0)
		{
			Take();
			return true;
		}
		return false;
	}

	internal bool TakeIf(string P_0)
	{
		if (TryPeek(P_0.Length).SequenceEqual(P_0.AsSpan()))
		{
			_s = _s.Slice(P_0.Length);
			Position += P_0.Length;
			return true;
		}
		return false;
	}

	public ReadOnlySpan<char> TakeWhile(Func<char, bool> P_0)
	{
		int i;
		for (i = 0; i < _s.Length && P_0(_s[i]); i++)
		{
		}
		ReadOnlySpan<char> result = _s.Slice(0, i);
		_s = _s.Slice(i);
		Position += i;
		return result;
	}

	public ReadOnlySpan<char> TryPeek(int P_0)
	{
		if (_s.Length < P_0)
		{
			return ReadOnlySpan<char>.Empty;
		}
		return _s.Slice(0, P_0);
	}
}

