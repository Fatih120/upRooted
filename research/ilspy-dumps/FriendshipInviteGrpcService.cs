using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class FriendshipInviteGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class FriendshipInviteGrpcServiceClient : ClientBase<FriendshipInviteGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public FriendshipInviteGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public FriendshipInviteGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected FriendshipInviteGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipInviteCreateResponse Create(FriendshipInviteCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipInviteCreateResponse Create(FriendshipInviteCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipInviteCreateResponse> CreateAsync(FriendshipInviteCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipInviteCreateResponse> CreateAsync(FriendshipInviteCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipInviteRespondResponse Respond(FriendshipInviteRespondRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Respond(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipInviteRespondResponse Respond(FriendshipInviteRespondRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Respond, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipInviteRespondResponse> RespondAsync(FriendshipInviteRespondRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RespondAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipInviteRespondResponse> RespondAsync(FriendshipInviteRespondRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Respond, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override FriendshipInviteGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new FriendshipInviteGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.FriendshipInviteGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipInviteCreateRequest> __Marshaller_root_FriendshipInviteCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipInviteCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipInviteCreateResponse> __Marshaller_root_FriendshipInviteCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipInviteCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipInviteRespondRequest> __Marshaller_root_FriendshipInviteRespondRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipInviteRespondRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipInviteRespondResponse> __Marshaller_root_FriendshipInviteRespondResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipInviteRespondResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FriendshipInviteCreateRequest, FriendshipInviteCreateResponse> __Method_Create = new Method<FriendshipInviteCreateRequest, FriendshipInviteCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_FriendshipInviteCreateRequest, __Marshaller_root_FriendshipInviteCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FriendshipInviteRespondRequest, FriendshipInviteRespondResponse> __Method_Respond = new Method<FriendshipInviteRespondRequest, FriendshipInviteRespondResponse>(MethodType.Unary, __ServiceName, "Respond", __Marshaller_root_FriendshipInviteRespondRequest, __Marshaller_root_FriendshipInviteRespondResponse);

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
