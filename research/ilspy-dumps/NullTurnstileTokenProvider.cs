using System.Threading;
using System.Threading.Tasks;

namespace RootApp.WebApi.Client.Shared.Implementations;

public sealed class NullTurnstileTokenProvider : ITurnstileTokenProvider
{
	public Task<string?> GetTokenAsync(string challengeUrl, string action, CancellationToken cancellationToken)
	{
		return Task.FromResult<string>(null);
	}
}
