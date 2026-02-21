using System.Buffers;
using System.Threading;
using System.Threading.Tasks;
using RootApp.App.Messaging.Grpc;

namespace RootApp.AppHub.Client;

public interface IAppHubConnectionHandler
{
	int MaxPacketSize { get; }

	AppRpcMessageToHost PingMessage { get; }

	long SequenceNumber { get; set; }

	ValueTask SignalHubServerOnlineAsync(CancellationToken P_0);

	ValueTask ConsumeMessageAsync(ReadOnlySequence<byte> P_0, CancellationToken P_1);
}
