// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.HexInputBorder
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using RootApp.Client.Avalonia.Controls;

public class HexInputBorder : Border
{
	private TextBox? _textBox;

	public static readonly StyledProperty<IBrush?> FocusedBorderBrushProperty = AvaloniaProperty.Register<HexInputBorder, IBrush>("FocusedBorderBrush");

	public static readonly StyledProperty<IBrush?> UnfocusedBorderBrushProperty = AvaloniaProperty.Register<HexInputBorder, IBrush>("UnfocusedBorderBrush");

	public static readonly StyledProperty<IBrush?> ErrorBorderBrushProperty = AvaloniaProperty.Register<HexInputBorder, IBrush>("ErrorBorderBrush");

	public static readonly StyledProperty<Thickness> FocusedBorderThicknessProperty = AvaloniaProperty.Register<HexInputBorder, Thickness>("FocusedBorderThickness", new Thickness(1.0));

	public static readonly StyledProperty<Thickness> UnfocusedBorderThicknessProperty = AvaloniaProperty.Register<HexInputBorder, Thickness>("UnfocusedBorderThickness", new Thickness(0.5));

	public static readonly StyledProperty<bool> HasErrorProperty = AvaloniaProperty.Register<HexInputBorder, bool>("HasError", false);

	public IBrush? FocusedBorderBrush => GetValue(FocusedBorderBrushProperty);

	public IBrush? UnfocusedBorderBrush => GetValue(UnfocusedBorderBrushProperty);

	public IBrush? ErrorBorderBrush => GetValue(ErrorBorderBrushProperty);

	public bool HasError => GetValue(HasErrorProperty);

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		FindAndSubscribeToTextBox();
		UpdateBorderAppearance(false);
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		UnsubscribeFromTextBox();
	}

	private void FindAndSubscribeToTextBox()
	{
		_textBox = FindTextBox(this);
		if (_textBox != null)
		{
			_textBox.GotFocus += OnTextBoxGotFocus;
			_textBox.LostFocus += OnTextBoxLostFocus;
		}
	}

	private void UnsubscribeFromTextBox()
	{
		if (_textBox != null)
		{
			_textBox.GotFocus -= OnTextBoxGotFocus;
			_textBox.LostFocus -= OnTextBoxLostFocus;
			_textBox = null;
		}
	}

	private TextBox? FindTextBox(Control P_0)
	{
		if (P_0 is TextBox result)
		{
			return result;
		}
		foreach (ILogical logicalChild in P_0.GetLogicalChildren())
		{
			if (logicalChild is Control control)
			{
				TextBox textBox = FindTextBox(control);
				if (textBox != null)
				{
					return textBox;
				}
			}
		}
		return null;
	}

	private void OnTextBoxGotFocus(object? sender, GotFocusEventArgs e)
	{
		UpdateBorderAppearance(true);
	}

	private void OnTextBoxLostFocus(object? sender, RoutedEventArgs e)
	{
		UpdateBorderAppearance(false);
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == HasErrorProperty)
		{
			UpdateBorderAppearance(_textBox?.IsFocused ?? false);
		}
	}

	private void UpdateBorderAppearance(bool P_0)
	{
		if (HasError && ErrorBorderBrush != null)
		{
			base.BorderBrush = ErrorBorderBrush;
		}
		else if (P_0 && FocusedBorderBrush != null)
		{
			base.BorderBrush = FocusedBorderBrush;
		}
		else
		{
			base.BorderBrush = UnfocusedBorderBrush;
		}
	}
}

