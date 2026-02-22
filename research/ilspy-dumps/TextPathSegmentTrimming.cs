// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextPathSegmentTrimming
using Avalonia.Media;
using Avalonia.Media.TextFormatting;

public sealed class TextPathSegmentTrimming : TextTrimming
{
	private readonly string _ellipsis;

	public TextPathSegmentTrimming(string P_0)
	{
		_ellipsis = P_0;
	}

	public override TextCollapsingProperties CreateCollapsingProperties(TextCollapsingCreateInfo P_0)
	{
		return new TextPathSegmentEllipsis(_ellipsis, P_0.Width, P_0.TextRunProperties, P_0.FlowDirection);
	}

	public override string ToString()
	{
		return "PathSegmentEllipsis";
	}
}

