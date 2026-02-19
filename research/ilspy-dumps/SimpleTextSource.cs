// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.SimpleTextSource
using System;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using RootApp.Client.Avalonia.Markdown.Components;

internal readonly struct SimpleTextSource : ITextSource
{
	private readonly ReadOnlyMemory<char> _text;

	private readonly TextRunProperties _props;

	public TextRunProperties RunProperties => _props;

	public SimpleTextSource(ReadOnlyMemory<char> P_0, TextRunProperties P_1)
	{
		_text = P_0;
		_props = P_1;
	}

	public TextRun? GetTextRun(int P_0)
	{
		return new TextCharacters(_text.Slice(P_0), _props);
	}

	public string Substring(int P_0, int P_1)
	{
		return _text.Slice(P_0, P_1).ToString();
	}

	public SimpleTextSource ChangeForeground(IBrush? P_0)
	{
		GenericTextRunProperties genericTextRunProperties = new GenericTextRunProperties(_props.Typeface, _props.FontRenderingEmSize, null, P_0);
		return new SimpleTextSource(_text, genericTextRunProperties);
	}

	public override string ToString()
	{
		return _text.ToString();
	}
}

