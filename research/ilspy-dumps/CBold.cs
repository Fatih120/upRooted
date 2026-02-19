// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CBold
using System.Collections.Generic;
using Avalonia.Media;
using RootApp.Client.Avalonia.Markdown.Components;

public class CBold : CSpan
{
	public CBold(IEnumerable<CInline> P_0)
		: base(P_0)
	{
		base.FontWeight = FontWeight.Bold;
	}
}

