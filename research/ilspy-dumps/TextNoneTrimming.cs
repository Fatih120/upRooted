// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextNoneTrimming
using System;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;

internal sealed class TextNoneTrimming : TextTrimming
{
	public override TextCollapsingProperties CreateCollapsingProperties(TextCollapsingCreateInfo P_0)
	{
		throw new NotSupportedException();
	}

	public override string ToString()
	{
		return "None";
	}
}

