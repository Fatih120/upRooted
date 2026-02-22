// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.UnicodeRange
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Avalonia.Media;

public readonly record struct UnicodeRange
{
	private readonly UnicodeRangeSegment _single;

	private readonly IReadOnlyList<UnicodeRangeSegment>? _segments;

	public bool IsInRange(int P_0)
	{
		if (_segments == null)
		{
			return _single.IsInRange(P_0);
		}
		foreach (UnicodeRangeSegment segment in _segments)
		{
			if (segment.IsInRange(P_0))
			{
				return true;
			}
		}
		return false;
	}

	[CompilerGenerated]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("UnicodeRange");
		stringBuilder.Append(" { ");
		if (PrintMembers(stringBuilder))
		{
		}
		stringBuilder.Append('}');
		return stringBuilder.ToString();
	}
}

