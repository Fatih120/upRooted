// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextTrimming
using System.Runtime.CompilerServices;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;

public abstract class TextTrimming
{
	[CompilerGenerated]
	private static readonly TextTrimming _003CPrefixCharacterEllipsis_003Ek__BackingField = new TextLeadingPrefixTrimming("…", 8);

	[CompilerGenerated]
	private static readonly TextTrimming _003CLeadingCharacterEllipsis_003Ek__BackingField = new TextLeadingPrefixTrimming("…", 0);

	[CompilerGenerated]
	private static readonly TextTrimming _003CPathSegmentEllipsis_003Ek__BackingField = new TextPathSegmentTrimming("…");

	public static TextTrimming None { get; } = new TextNoneTrimming();

	public static TextTrimming CharacterEllipsis { get; } = new TextTrailingTrimming("…", false);

	public static TextTrimming WordEllipsis { get; } = new TextTrailingTrimming("…", true);

	public abstract TextCollapsingProperties CreateCollapsingProperties(TextCollapsingCreateInfo P_0);
}

