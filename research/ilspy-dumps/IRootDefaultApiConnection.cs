using RootApp.WebApi.Client.Shared;

namespace RootApp.WebApi.Client;

public interface IRootDefaultApiConnection
{
	IApiConnection Connection { get; }
}
