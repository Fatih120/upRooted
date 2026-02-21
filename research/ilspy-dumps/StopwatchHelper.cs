// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.StopwatchHelper
using System;
using System.Diagnostics;

internal static class StopwatchHelper
{
	private static readonly double s_timestampToTicks = 10000000.0 / (double)Stopwatch.Frequency;

	private static readonly double s_timestampToMs = s_timestampToTicks / 10000.0;

	public static TimeSpan GetElapsedTime(long P_0)
	{
		return GetElapsedTime(P_0, Stopwatch.GetTimestamp());
	}

	public static TimeSpan GetElapsedTime(long P_0, long P_1)
	{
		return new TimeSpan((long)((double)(P_1 - P_0) * s_timestampToTicks));
	}

	public static double GetElapsedTimeMs(long P_0)
	{
		return (double)(Stopwatch.GetTimestamp() - P_0) * s_timestampToMs;
	}
}

