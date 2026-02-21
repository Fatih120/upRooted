// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.BinarySearchExtension
using System.Collections.Generic;

internal static class BinarySearchExtension
{
	private static int GetMedian(int P_0, int P_1)
	{
		return P_0 + (P_1 - P_0 >> 1);
	}

	public static int BinarySearch<T>(this IReadOnlyList<T> P_0, T P_1, IComparer<T> P_2)
	{
		return BinarySearch(P_0, 0, P_0.Count, P_1, P_2);
	}

	public static int BinarySearch<T>(this IReadOnlyList<T> P_0, int P_1, int P_2, T P_3, IComparer<T> P_4)
	{
		int num = P_1;
		int num2 = P_1 + P_2 - 1;
		while (num <= num2)
		{
			int median = GetMedian(num, num2);
			int num3 = P_4.Compare(P_0[median], P_3);
			if (num3 == 0)
			{
				return median;
			}
			if (num3 < 0)
			{
				num = median + 1;
			}
			else
			{
				num2 = median - 1;
			}
		}
		return ~num;
	}
}

