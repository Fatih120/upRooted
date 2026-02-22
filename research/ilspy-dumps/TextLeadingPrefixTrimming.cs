// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextLeadingPrefixTrimming
using Avalonia.Media;
using Avalonia.Media.TextFormatting;

public sealed class TextLeadingPrefixTrimming : TextTrimming
{
	private readonly string _ellipsis;

	private readonly int _prefixLength;

	public TextLeadingPrefixTrimming(string P_0, int P_1)
	{
		_prefixLength = P_1;
		_ellipsis = P_0;
	}

	public override TextCollapsingProperties CreateCollapsingProperties(TextCollapsingCreateInfo P_0)
	{
		return new TextLeadingPrefixCharacterEllipsis(_ellipsis, _prefixLength, P_0.Width, P_0.TextRunProperties, P_0.FlowDirection);
	}

	public override string ToString()
	{
		return "PrefixCharacterEllipsis";
	}
}

