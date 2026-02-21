using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class DirectMessageGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class DirectMessageGrpcServiceClient : ClientBase<DirectMessageGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public DirectMessageGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public DirectMessageGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected DirectMessageGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageListResponse List(DirectMessageListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageListResponse List(DirectMessageListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageListResponse> ListAsync(DirectMessageListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageListResponse> ListAsync(DirectMessageListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageCreateResponse Create(DirectMessageCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageCreateResponse Create(DirectMessageCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageCreateResponse> CreateAsync(DirectMessageCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageCreateResponse> CreateAsync(DirectMessageCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageAddMembersResponse AddMembers(DirectMessageAddMembersRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AddMembers(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageAddMembersResponse AddMembers(DirectMessageAddMembersRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_AddMembers, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageAddMembersResponse> AddMembersAsync(DirectMessageAddMembersRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AddMembersAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageAddMembersResponse> AddMembersAsync(DirectMessageAddMembersRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_AddMembers, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageFindResponse Find(DirectMessageFindRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Find(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageFindResponse Find(DirectMessageFindRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Find, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageFindResponse> FindAsync(DirectMessageFindRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return FindAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageFindResponse> FindAsync(DirectMessageFindRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Find, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageDeleteUserResponse RemoveUser(DirectMessageDeleteUserRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RemoveUser(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageDeleteUserResponse RemoveUser(DirectMessageDeleteUserRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_RemoveUser, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageDeleteUserResponse> RemoveUserAsync(DirectMessageDeleteUserRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RemoveUserAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageDeleteUserResponse> RemoveUserAsync(DirectMessageDeleteUserRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_RemoveUser, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageRingDeclineResponse RingDecline(DirectMessageRingDeclineRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RingDecline(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectMessageRingDeclineResponse RingDecline(DirectMessageRingDeclineRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_RingDecline, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageRingDeclineResponse> RingDeclineAsync(DirectMessageRingDeclineRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RingDeclineAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectMessageRingDeclineResponse> RingDeclineAsync(DirectMessageRingDeclineRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_RingDecline, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override DirectMessageGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new DirectMessageGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.DirectMessageGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageListRequest> __Marshaller_root_DirectMessageListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageListResponse> __Marshaller_root_DirectMessageListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageCreateRequest> __Marshaller_root_DirectMessageCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageCreateResponse> __Marshaller_root_DirectMessageCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageAddMembersRequest> __Marshaller_root_DirectMessageAddMembersRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageAddMembersRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageAddMembersResponse> __Marshaller_root_DirectMessageAddMembersResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageAddMembersResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageFindRequest> __Marshaller_root_DirectMessageFindRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageFindRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageFindResponse> __Marshaller_root_DirectMessageFindResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageFindResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageDeleteUserRequest> __Marshaller_root_DirectMessageDeleteUserRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageDeleteUserRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageDeleteUserResponse> __Marshaller_root_DirectMessageDeleteUserResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageDeleteUserResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageRingDeclineRequest> __Marshaller_root_DirectMessageRingDeclineRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageRingDeclineRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectMessageRingDeclineResponse> __Marshaller_root_DirectMessageRingDeclineResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectMessageRingDeclineResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectMessageListRequest, DirectMessageListResponse> __Method_List = new Method<DirectMessageListRequest, DirectMessageListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_DirectMessageListRequest, __Marshaller_root_DirectMessageListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectMessageCreateRequest, DirectMessageCreateResponse> __Method_Create = new Method<DirectMessageCreateRequest, DirectMessageCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_DirectMessageCreateRequest, __Marshaller_root_DirectMessageCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectMessageAddMembersRequest, DirectMessageAddMembersResponse> __Method_AddMembers = new Method<DirectMessageAddMembersRequest, DirectMessageAddMembersResponse>(MethodType.Unary, __ServiceName, "AddMembers", __Marshaller_root_DirectMessageAddMembersRequest, __Marshaller_root_DirectMessageAddMembersResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectMessageFindRequest, DirectMessageFindResponse> __Method_Find = new Method<DirectMessageFindRequest, DirectMessageFindResponse>(MethodType.Unary, __ServiceName, "Find", __Marshaller_root_DirectMessageFindRequest, __Marshaller_root_DirectMessageFindResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectMessageDeleteUserRequest, DirectMessageDeleteUserResponse> __Method_RemoveUser = new Method<DirectMessageDeleteUserRequest, DirectMessageDeleteUserResponse>(MethodType.Unary, __ServiceName, "RemoveUser", __Marshaller_root_DirectMessageDeleteUserRequest, __Marshaller_root_DirectMessageDeleteUserResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectMessageRingDeclineRequest, DirectMessageRingDeclineResponse> __Method_RingDecline = new Method<DirectMessageRingDeclineRequest, DirectMessageRingDeclineResponse>(MethodType.Unary, __ServiceName, "RingDecline", __Marshaller_root_DirectMessageRingDeclineRequest, __Marshaller_root_DirectMessageRingDeclineResponse);

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
