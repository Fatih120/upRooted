// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CStrikethrough
using System.Collections.Generic;
using RootApp.Client.Avalonia.Markdown.Components;

public class CStrikethrough : CSpan
{
	public CStrikethrough(IEnumerable<CInline> P_0)
		: base(P_0)
	{
		base.IsStrikethrough = true;
	}
}

