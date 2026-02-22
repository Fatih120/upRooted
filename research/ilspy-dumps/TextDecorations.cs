// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextDecorations
using System.Runtime.CompilerServices;
using Avalonia.Media;

public static class TextDecorations
{
	[CompilerGenerated]
	private static readonly TextDecorationCollection _003COverline_003Ek__BackingField;

	[CompilerGenerated]
	private static readonly TextDecorationCollection _003CBaseline_003Ek__BackingField;

	public static TextDecorationCollection Underline { get; }

	public static TextDecorationCollection Strikethrough { get; }

	static TextDecorations()
	{
		Underline = new TextDecorationCollection
		{
			new TextDecoration
			{
				Location = TextDecorationLocation.Underline
			}
		};
		Strikethrough = new TextDecorationCollection
		{
			new TextDecoration
			{
				Location = TextDecorationLocation.Strikethrough
			}
		};
		_003COverline_003Ek__BackingField = new TextDecorationCollection
		{
			new TextDecoration
			{
				Location = TextDecorationLocation.Overline
			}
		};
		_003CBaseline_003Ek__BackingField = new TextDecorationCollection
		{
			new TextDecoration
			{
				Location = TextDecorationLocation.Baseline
			}
		};
	}
}

