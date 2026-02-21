using System.Linq;
using Grpc.Core;
using RootApp.WebApi.Shared.Exceptions;

namespace RootApp.WebApi.Shared.Payloads.WebApiException;

public static class RootExceptionPayloadFactory
{
	public static RootGrpcException? AsRootGrpcException(this RpcException P_0)
	{
		Metadata.Entry entry = P_0.Trailers.FirstOrDefault((Metadata.Entry x) => x.Key == "root-exception-bin");
		if (entry == null)
		{
			return null;
		}
		return RootGrpcException.Parser.ParseFrom(entry.ValueBytes);
	}
}
