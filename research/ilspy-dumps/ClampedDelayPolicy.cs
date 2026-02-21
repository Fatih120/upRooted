using System;
using System.Collections.Generic;
using System.Linq;

namespace RootApp.Utility.Extensions;

public static class ClampedDelayPolicy
{
	public static IEnumerable<TimeSpan> Clamp(this IEnumerable<TimeSpan> P_0, TimeSpan P_1, TimeSpan? P_2 = null)
	{
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(P_1, TimeSpan.Zero, "maxDelay");
		long randomTicks = RandomTicks(P_1, P_2);
		return P_0.Select((TimeSpan v) => TimeSpan.FromTicks(Math.Min(v.Ticks, P_1.Ticks + Random.Shared.NextInt64(0L, randomTicks))));
	}

	private static long RandomTicks(TimeSpan P_0, TimeSpan? P_1)
	{
		if (P_1.HasValue && P_1 >= TimeSpan.Zero)
		{
			return P_1.Value.Ticks;
		}
		return P_0.Ticks / 10;
	}

	public static IEnumerable<TimeSpan> ClampForever(this IEnumerable<TimeSpan> P_0, TimeSpan P_1, TimeSpan? P_2 = null)
	{
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(P_1, TimeSpan.Zero, "maxDelay");
		foreach (TimeSpan item in P_0.Clamp(P_1, P_2))
		{
			yield return item;
		}
		long randomTicks = RandomTicks(P_1, P_2);
		while (true)
		{
			yield return TimeSpan.FromTicks(P_1.Ticks + Random.Shared.NextInt64(0L, randomTicks));
		}
	}
}
