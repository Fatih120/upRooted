// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextDecorationCollection
using System.Collections.Generic;
using Avalonia.Collections;
using Avalonia.Media;

public class TextDecorationCollection : AvaloniaList<TextDecoration>
{
	public TextDecorationCollection()
	{
	}

	public TextDecorationCollection(IEnumerable<TextDecoration> P_0)
		: base(P_0)
	{
	}
}

