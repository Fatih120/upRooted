using System.Threading;
using System.Threading.Tasks;
using RootApp.WebApi.Client.Shared;

namespace RootApp.WebApi.Client;

public sealed class ApiConnectionManager : IRootDefaultApiConnectionManagement
{
	private readonly IApiConnectionFactory _connectionFactory;

	private readonly ApiConnectionHolder _holder;

	public ApiConnectionManager(IRootDefaultApiConnection P_0, IApiConnectionFactory P_1)
	{
		_connectionFactory = P_1;
		_holder = (ApiConnectionHolder)P_0;
		base._002Ector();
	}

	public async Task<bool> ReconnectAsync(CancellationToken P_0)
	{
		IApiConnection connection = await _connectionFactory.GetConnectionAsync(P_0).ConfigureAwait(continueOnCapturedContext: false);
		if (connection == null)
		{
			return false;
		}
		await _holder.SetConnectionAsync(connection).ConfigureAwait(false);
		return true;
	}

	public async Task<bool> ConnectAsync(string P_0, string P_1, CancellationToken P_2)
	{
		IApiConnection connection = await _connectionFactory.SignInAsync(P_0, P_1, persistLogin: true, P_2).ConfigureAwait(continueOnCapturedContext: false);
		if (connection == null)
		{
			return false;
		}
		await _holder.SetConnectionAsync(connection).ConfigureAwait(false);
		return true;
	}

	public async ValueTask DisconnectAsync(CancellationToken P_0)
	{
		await _holder.ClearConnectionAsync().ConfigureAwait(false);
	}
}
