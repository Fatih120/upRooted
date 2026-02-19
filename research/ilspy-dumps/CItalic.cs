// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CItalic
using System.Collections.Generic;
using Avalonia.Media;
using RootApp.Client.Avalonia.Markdown.Components;

public class CItalic : CSpan
{
	public CItalic(IEnumerable<CInline> P_0)
		: base(P_0)
	{
		base.FontStyle = FontStyle.Italic;
	}
}

