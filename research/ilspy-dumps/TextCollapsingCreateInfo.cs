// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextCollapsingCreateInfo
using Avalonia.Media;
using Avalonia.Media.TextFormatting;

public readonly record struct TextCollapsingCreateInfo
{
	public readonly double Width;

	public readonly TextRunProperties TextRunProperties;

	public readonly FlowDirection FlowDirection;

	public TextCollapsingCreateInfo(double P_0, TextRunProperties P_1, FlowDirection P_2)
	{
		Width = P_0;
		TextRunProperties = P_1;
		FlowDirection = P_2;
	}
}

