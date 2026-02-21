using System.Diagnostics;

namespace Uprooted;

/// <summary>
/// A structured wide event that accumulates key-value fields during an operation
/// and emits a single log line when disposed. Implements the loggingsucks.com
/// "one wide event per operation" pattern adapted for client-side desktop logging.
///
/// Usage:
///   using var ev = WideEvent.Begin("AutoUpdate", "check");
///   ev.Set("channel", settings.Channel);
///   ev.Set("result", "up_to_date");
///   // auto-emits on Dispose with dur_ms
///
/// Sampled (for high-frequency scan ticks):
///   using var ev = WideEvent.BeginSampled("LinkEmbed", "scan_tick", _sampler);
///   ev.Set("urls_found", count);
///   if (count > 0) ev.MarkNotable();
///   // only emits if sampler says so (error/slow/notable/heartbeat)
/// </summary>
internal sealed class WideEvent : IDisposable
{
    private readonly string _category;
    private readonly string _operation;
    private readonly long _startTicks;
    private readonly TailSampler? _sampler;
    private readonly List<KeyValuePair<string, string>> _fields = new(8);
    private bool _emitted;
    private bool _hasError;
    private bool _notable;

    /// <summary>Opaque ID for parent-child linking. Format: HHmmss.fff (creation timestamp).</summary>
    internal string Id { get; }

    private WideEvent(string category, string operation, TailSampler? sampler, string? parentId)
    {
        _category = category;
        _operation = operation;
        _sampler = sampler;
        _startTicks = Stopwatch.GetTimestamp();
        Id = DateTime.Now.ToString("HHmmss.fff");
        if (parentId != null)
            _fields.Add(new("parent_op", parentId));
    }

    /// <summary>Start a new wide event that always emits.</summary>
    internal static WideEvent Begin(string category, string operation)
        => new(category, operation, null, null);

    /// <summary>Start a wide event linked to a parent operation.</summary>
    internal static WideEvent Begin(string category, string operation, WideEvent parent)
        => new(category, operation, null, parent.Id);

    /// <summary>Start a tail-sampled wide event (may be suppressed).</summary>
    internal static WideEvent BeginSampled(string category, string operation, TailSampler sampler)
        => new(category, operation, sampler, null);

    /// <summary>Set a string field.</summary>
    internal WideEvent Set(string key, string? value)
    {
        if (value != null)
            _fields.Add(new(key, value));
        return this;
    }

    /// <summary>Set an int field.</summary>
    internal WideEvent Set(string key, int value)
    {
        _fields.Add(new(key, value.ToString()));
        return this;
    }

    /// <summary>Set a long field.</summary>
    internal WideEvent Set(string key, long value)
    {
        _fields.Add(new(key, value.ToString()));
        return this;
    }

    /// <summary>Set a bool field.</summary>
    internal WideEvent Set(string key, bool value)
    {
        _fields.Add(new(key, value ? "true" : "false"));
        return this;
    }

    /// <summary>Increment a counter field. Creates it at 0 if not present.</summary>
    internal WideEvent Increment(string key, int delta = 1)
    {
        for (int i = 0; i < _fields.Count; i++)
        {
            if (_fields[i].Key == key)
            {
                if (int.TryParse(_fields[i].Value, out var current))
                {
                    _fields[i] = new(key, (current + delta).ToString());
                    return this;
                }
            }
        }
        _fields.Add(new(key, delta.ToString()));
        return this;
    }

    /// <summary>Record an error. Forces emission for sampled events.</summary>
    internal WideEvent SetError(Exception ex)
    {
        _hasError = true;
        _fields.Add(new("error", ex.GetType().Name));
        _fields.Add(new("error_msg", Truncate(ex.Message, 300)));
        return this;
    }

    /// <summary>Record an error message. Forces emission for sampled events.</summary>
    internal WideEvent SetError(string message)
    {
        _hasError = true;
        _fields.Add(new("error_msg", Truncate(message, 300)));
        return this;
    }

    /// <summary>Force emission for sampled events (e.g., when work was found).</summary>
    internal void MarkNotable() => _notable = true;

    /// <summary>Emit now without waiting for Dispose. Subsequent Dispose is a no-op.</summary>
    internal void Finish() => Emit();

    public void Dispose() => Emit();

    private void Emit()
    {
        if (_emitted) return;
        _emitted = true;

        var elapsedMs = (int)(Stopwatch.GetElapsedTime(_startTicks).TotalMilliseconds);

        if (_sampler != null)
        {
            var reason = _sampler.ShouldEmit(_hasError, _notable, elapsedMs);
            if (reason == null) return;
            _fields.Add(new("sampled", reason));
        }

        Logger.EmitWideEvent(_category, _operation, _fields, elapsedMs);
    }

    private static string Truncate(string s, int max)
        => s.Length <= max ? s : s[..max] + "...";
}
