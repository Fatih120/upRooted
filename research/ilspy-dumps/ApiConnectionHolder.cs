using System.Threading;
using System.Threading.Tasks;
using RootApp.WebApi.Client.Shared;

namespace RootApp.WebApi.Client;

public sealed class ApiConnectionHolder : IRootDefaultApiConnection
{
	private IApiConnection? _connection;

	public IApiConnection Connection => _connection ?? NullApiConnection.Instance;

	public async ValueTask SetConnectionAsync(IApiConnection? P_0)
	{
		IApiConnection previous = Interlocked.Exchange(ref _connection, P_0);
		if (previous != null)
		{
			await previous.DisposeAsync().ConfigureAwait(false);
		}
	}

	public async ValueTask ClearConnectionAsync()
	{
		await SetConnectionAsync(null).ConfigureAwait(false);
	}
}
