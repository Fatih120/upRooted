using System;

namespace RootApp.Utility;

public static class RootTimeSpanExtensions
{
	public static TimeSpan AddRandomFraction(this TimeSpan P_0, int P_1 = 2)
	{
		long ticks = P_0.Ticks;
		return TimeSpan.FromTicks(AddRandom(ticks, ticks / P_1));
	}

	private static long AddRandom(long P_0, long P_1)
	{
		long num = Random.Shared.NextInt64(P_1);
		return P_0 + num;
	}
}
