using System.Threading;
using System.Threading.Tasks;

namespace RootApp.WebApi.Client;

public interface IRootDefaultApiConnectionManagement
{
	Task<bool> ReconnectAsync(CancellationToken P_0);

	Task<bool> ConnectAsync(string P_0, string P_1, CancellationToken P_2);

	ValueTask DisconnectAsync(CancellationToken P_0);
}
