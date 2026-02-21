using System.Buffers;
using System.Threading;
using System.Threading.Tasks;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.HubServer.Client;

public interface IHubConnectionHandler
{
	int MaxPacketSize { get; }

	HubNotification PingMessage { get; }

	long SequenceNumber { get; set; }

	ValueTask SignalHubServerOnlineAsync(CancellationToken P_0);

	ValueTask ConsumeMessageAsync(ReadOnlySequence<byte> P_0, CancellationToken P_1);
}
