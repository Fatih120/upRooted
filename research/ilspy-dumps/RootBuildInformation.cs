using System;
using System.Globalization;

namespace RootApp.Information;

public sealed record RootBuildInformation(string Name, string CommitHash, string CommitDateTimeString, string Describe, string Branch, string GitHubRefName, string GitHubSha, string GitHubWorkflow, long GitRunId, int GitRunAttempt, int GitRunNumber, bool GitHubBuild, RootBuildInformation? GitWorkspace)
{
	public DateTimeOffset? CommitDateTimeOffset { get; } = DateTimeOffset.TryParse(CommitDateTimeString, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out var value) ? new DateTimeOffset?(value) : ((DateTimeOffset?)null);
}
