using System;

namespace RootApp.WebApi.Client.Shared.Turnstile;

public class TurnstileRequiredException : Exception
{
	public string ChallengeUrl { get; }

	public string Action { get; }

	public TurnstileRequiredException(string challengeUrl, string action)
		: base("Turnstile verification required")
	{
		ChallengeUrl = challengeUrl;
		Action = action;
	}

	public TurnstileRequiredException(string challengeUrl, string action, Exception innerException)
		: base("Turnstile verification required", innerException)
	{
		ChallengeUrl = challengeUrl;
		Action = action;
	}
}
