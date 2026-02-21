namespace RootApp.WebApi.Client.Shared.Implementations;

public class TokenResponse
{
	public required ClientToken ClientToken { get; init; }

	public required string HubServerInfo { get; init; }
}
