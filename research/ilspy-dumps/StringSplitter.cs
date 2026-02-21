// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.StringSplitter
using System;
using System.Collections.Generic;

internal static class StringSplitter
{
	public static string[] SplitRespectingBrackets(string P_0, char P_1, char P_2 = '(', char P_3 = ')', StringSplitOptions P_4 = StringSplitOptions.None)
	{
		return SplitRespectingBrackets(P_0, new ReadOnlySpan<char>((char)P_1), P_2, P_3, P_4);
	}

	public static string[] SplitRespectingBrackets(string P_0, ReadOnlySpan<char> P_1, char P_2 = '(', char P_3 = ')', StringSplitOptions P_4 = StringSplitOptions.None)
	{
		if (P_2 == P_3)
		{
			throw new ArgumentException($"Opening bracket and closing bracket cannot be the same character '{P_2}'.", "closingBracket");
		}
		if (P_0 == null)
		{
			return Array.Empty<string>();
		}
		ReadOnlySpan<char> readOnlySpan = P_0.AsSpan();
		List<(int start, int length)> ranges = new List<(int, int)>();
		int num = 0;
		int num2 = 0;
		bool removeEmptyEntries = P_4.HasFlag(StringSplitOptions.RemoveEmptyEntries);
		bool trimEntries = P_4.HasFlag(StringSplitOptions.TrimEntries);
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			char c = readOnlySpan[i];
			if (c == P_2)
			{
				num++;
			}
			else if (c == P_3)
			{
				if (num <= 0)
				{
					throw new FormatException($"Unmatched closing bracket '{P_3}' at position {i}.");
				}
				num--;
			}
			else if (P_1.IndexOf(c) >= 0 && num == 0)
			{
				ProcessSegment(num2, i - 1);
				num2 = i + 1;
			}
		}
		if (num != 0)
		{
			throw new FormatException($"Unmatched opening bracket '{P_2}' in input string.");
		}
		ProcessSegment(num2, readOnlySpan.Length - 1);
		if (ranges.Count == 0)
		{
			return Array.Empty<string>();
		}
		string[] array = new string[ranges.Count];
		for (int j = 0; j < ranges.Count; j++)
		{
			(int, int) tuple = ranges[j];
			array[j] = new string(readOnlySpan.Slice(tuple.Item1, tuple.Item2));
		}
		return array;
		void ProcessSegment(int num3, int num4)
		{
			if (trimEntries)
			{
				while (num3 <= num4 && char.IsWhiteSpace(P_0[num3]))
				{
					num3++;
				}
				while (num4 >= num3 && char.IsWhiteSpace(P_0[num4]))
				{
					num4--;
				}
			}
			int num5 = num4 - num3 + 1;
			if (num5 > 0 || !removeEmptyEntries)
			{
				ranges.Add((num3, num5));
			}
		}
	}
}

