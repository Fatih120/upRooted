// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CInlineUIContainer
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;

public class CInlineUIContainer : CInline
{
	public Control? Content
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[CompilerGenerated]
		get
		{
			throw new NotSupportedException("Linked away");
		}
	}

	internal DummyGeometryForControl? Indicator
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		[CompilerGenerated]
		get
		{
			throw new NotSupportedException("Linked away");
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	protected override IEnumerable<CGeometry> MeasureOverride(double P_0, double P_1)
	{
		throw new NotSupportedException("Linked away");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	public override string AsString()
	{
		throw new NotSupportedException("Linked away");
	}
}

