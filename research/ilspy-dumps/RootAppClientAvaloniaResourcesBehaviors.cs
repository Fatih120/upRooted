// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Behaviors.TextBoxBehaviors
using Avalonia;
using Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Behaviors;

public static class TextBoxBehaviors
{
	public static readonly AttachedProperty<bool> IsUpperCaseProperty;

	static TextBoxBehaviors()
	{
		IsUpperCaseProperty = AvaloniaProperty.RegisterAttached<TextBox, bool>("IsUpperCase", typeof(TextBoxBehaviors), false);
		IsUpperCaseProperty.Changed.AddClassHandler<TextBox>(OnIsUpperCaseChanged);
	}

	private static void OnIsUpperCaseChanged(TextBox textBox, AvaloniaPropertyChangedEventArgs e)
	{
		if (e.NewValue is int num && num != 0)
		{
			textBox.TextChanged += OnTextChanged;
		}
		else
		{
			textBox.TextChanged -= OnTextChanged;
		}
	}

	private static void OnTextChanged(object? sender, TextChangedEventArgs e)
	{
		if (sender is TextBox { Text: not null } textBox)
		{
			string text = textBox.Text.ToUpperInvariant();
			if (textBox.Text != text)
			{
				int caretIndex = textBox.CaretIndex;
				textBox.Text = text;
				textBox.CaretIndex = caretIndex;
			}
		}
	}
}
