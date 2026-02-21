using System;
using System.Collections.Generic;
using Polly.Contrib.WaitAndRetry;

namespace RootApp.WebApi.Client.Shared.Turnstile;

public static class TurnstileRetryPolicy
{
	public static IEnumerable<TimeSpan> RetryDelays { get; } = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1L), 3, null, fastFirst: true);
}
