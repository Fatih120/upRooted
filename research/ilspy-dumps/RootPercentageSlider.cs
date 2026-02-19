// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootPercentageSlider
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

public class RootPercentageSlider : Slider
{
	private TextBlock? _percentageText;

	protected override void OnApplyTemplate(TemplateAppliedEventArgs P_0)
	{
		base.OnApplyTemplate(P_0);
		_percentageText = P_0.NameScope.Find<TextBlock>("PART_PercentageText");
		UpdatePercentageText();
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == RangeBase.ValueProperty)
		{
			UpdatePercentageText();
		}
	}

	private void UpdatePercentageText()
	{
		if (_percentageText != null)
		{
			_percentageText.Text = $"{base.Value:0}%";
		}
	}
}

