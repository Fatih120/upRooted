using System.Threading;
using System.Threading.Tasks;

namespace RootApp.WebApi.Client.Shared;

public interface ITurnstileTokenProvider
{
	Task<string?> GetTokenAsync(string challengeUrl, string action, CancellationToken cancellationToken);
}
