using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class SupportGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class SupportGrpcServiceClient : ClientBase<SupportGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public SupportGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public SupportGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected SupportGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual SupportCreateResponse Create(SupportCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual SupportCreateResponse Create(SupportCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<SupportCreateResponse> CreateAsync(SupportCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<SupportCreateResponse> CreateAsync(SupportCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override SupportGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new SupportGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.SupportGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<SupportCreateRequest> __Marshaller_root_SupportCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, SupportCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<SupportCreateResponse> __Marshaller_root_SupportCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, SupportCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<SupportCreateRequest, SupportCreateResponse> __Method_Create = new Method<SupportCreateRequest, SupportCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_SupportCreateRequest, __Marshaller_root_SupportCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static void __Helper_SerializeMessage(IMessage message, SerializationContext context)
	{
		if (message is IBufferMessage)
		{
			context.SetPayloadLength(message.CalculateSize());
			message.WriteTo(context.GetBufferWriter());
			context.Complete();
		}
		else
		{
			context.Complete(message.ToByteArray());
		}
	}

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static T __Helper_DeserializeMessage<T>(DeserializationContext P_0, MessageParser<T> P_1) where T : IMessage<T>
	{
		if (__Helper_MessageCache<T>.IsBufferMessage)
		{
			return P_1.ParseFrom(P_0.PayloadAsReadOnlySequence());
		}
		return P_1.ParseFrom(P_0.PayloadAsNewBuffer());
	}
}
