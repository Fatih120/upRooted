namespace Uprooted;

/// <summary>
/// Controls when sampled wide events are actually emitted.
/// Designed for high-frequency recurring operations (scan timer ticks)
/// that produce noise when nothing interesting happens.
///
/// Emission rules (OR — emit if ANY is true):
///   1. The event has an error
///   2. Duration exceeds the slow threshold
///   3. The event was marked notable (e.g., found work to do)
///   4. N ticks have elapsed since last emission (periodic heartbeat)
/// </summary>
internal sealed class TailSampler
{
    private readonly int _heartbeatTicks;
    private readonly int _slowThresholdMs;
    private int _ticksSinceEmit;
    private int _suppressedCount;

    /// <param name="heartbeatTicks">Emit every Nth tick regardless (e.g., 60 = every 60th tick).</param>
    /// <param name="slowThresholdMs">Emit if tick duration exceeds this (e.g., 50ms).</param>
    internal TailSampler(int heartbeatTicks = 60, int slowThresholdMs = 50)
    {
        _heartbeatTicks = heartbeatTicks;
        _slowThresholdMs = slowThresholdMs;
    }

    /// <summary>
    /// Called by WideEvent.Dispose to determine whether to emit.
    /// Returns the reason string if should emit, null to suppress.
    /// </summary>
    internal string? ShouldEmit(bool hasError, bool isNotable, int durationMs)
    {
        var tick = Interlocked.Increment(ref _ticksSinceEmit);

        if (hasError)
        {
            Reset();
            return "error";
        }

        if (durationMs > _slowThresholdMs)
        {
            Reset();
            return $"slow:{durationMs}ms";
        }

        if (isNotable)
        {
            Reset();
            return "notable";
        }

        if (tick >= _heartbeatTicks)
        {
            var suppressed = _suppressedCount;
            Reset();
            return $"tick:{tick}+{suppressed}skip";
        }

        Interlocked.Increment(ref _suppressedCount);
        return null;
    }

    private void Reset()
    {
        Interlocked.Exchange(ref _ticksSinceEmit, 0);
        Interlocked.Exchange(ref _suppressedCount, 0);
    }
}
