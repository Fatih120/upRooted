// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.UnicodeRangeSegment
public readonly record struct UnicodeRangeSegment
{
	public int Start { get; }

	public int End { get; }

	public bool IsInRange(int P_0)
	{
		if (Start <= P_0)
		{
			return P_0 <= End;
		}
		return false;
	}
}

