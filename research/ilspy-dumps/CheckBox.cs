// Avalonia.Controls, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Controls.CheckBox
using Avalonia.Automation;
using Avalonia.Automation.Peers;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

public class CheckBox : ToggleButton
{
	static CheckBox()
	{
		AutomationProperties.ControlTypeOverrideProperty.OverrideDefaultValue<CheckBox>(AutomationControlType.CheckBox);
	}
}
