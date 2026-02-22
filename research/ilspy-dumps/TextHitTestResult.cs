// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextHitTestResult
using Avalonia.Media;

public readonly record struct TextHitTestResult
{
	public CharacterHit CharacterHit { get; }

	public bool IsInside { get; }

	public int TextPosition { get; }

	public bool IsTrailing { get; }

	public TextHitTestResult(CharacterHit P_0, int P_1, bool P_2, bool P_3)
	{
		CharacterHit = P_0;
		TextPosition = P_1;
		IsInside = P_2;
		IsTrailing = P_3;
	}
}

