// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextTrailingTrimming
using Avalonia.Media;
using Avalonia.Media.TextFormatting;

public sealed class TextTrailingTrimming : TextTrimming
{
	private readonly string _ellipsis;

	private readonly bool _isWordBased;

	public TextTrailingTrimming(string P_0, bool P_1)
	{
		_isWordBased = P_1;
		_ellipsis = P_0;
	}

	public override TextCollapsingProperties CreateCollapsingProperties(TextCollapsingCreateInfo P_0)
	{
		if (_isWordBased)
		{
			return new TextTrailingWordEllipsis(_ellipsis, P_0.Width, P_0.TextRunProperties, P_0.FlowDirection);
		}
		return new TextTrailingCharacterEllipsis(_ellipsis, P_0.Width, P_0.TextRunProperties, P_0.FlowDirection);
	}

	public override string ToString()
	{
		if (!_isWordBased)
		{
			return "CharacterEllipsis";
		}
		return "WordEllipsis";
	}
}

