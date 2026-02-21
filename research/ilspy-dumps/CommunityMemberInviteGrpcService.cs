using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class CommunityMemberInviteGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class CommunityMemberInviteGrpcServiceClient : ClientBase<CommunityMemberInviteGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityMemberInviteGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityMemberInviteGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected CommunityMemberInviteGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteResponse Create(CommunityMemberInviteCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteResponse Create(CommunityMemberInviteCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteResponse> CreateAsync(CommunityMemberInviteCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteResponse> CreateAsync(CommunityMemberInviteCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteRespondResponse Respond(CommunityMemberInviteRespondRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Respond(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteRespondResponse Respond(CommunityMemberInviteRespondRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Respond, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteRespondResponse> RespondAsync(CommunityMemberInviteRespondRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RespondAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteRespondResponse> RespondAsync(CommunityMemberInviteRespondRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Respond, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteLinkJoinResponse LinkJoin(CommunityMemberInviteLinkJoinRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return LinkJoin(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteLinkJoinResponse LinkJoin(CommunityMemberInviteLinkJoinRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_LinkJoin, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteLinkJoinResponse> LinkJoinAsync(CommunityMemberInviteLinkJoinRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return LinkJoinAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteLinkJoinResponse> LinkJoinAsync(CommunityMemberInviteLinkJoinRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_LinkJoin, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteDeleteResponse Delete(CommunityMemberInviteDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteDeleteResponse Delete(CommunityMemberInviteDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteDeleteResponse> DeleteAsync(CommunityMemberInviteDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteDeleteResponse> DeleteAsync(CommunityMemberInviteDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteResponse Get(CommunityMemberInviteGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteResponse Get(CommunityMemberInviteGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteResponse> GetAsync(CommunityMemberInviteGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteResponse> GetAsync(CommunityMemberInviteGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteListResponse List(CommunityMemberInviteListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberInviteListResponse List(CommunityMemberInviteListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteListResponse> ListAsync(CommunityMemberInviteListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberInviteListResponse> ListAsync(CommunityMemberInviteListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override CommunityMemberInviteGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new CommunityMemberInviteGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.CommunityMemberInviteGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteCreateRequest> __Marshaller_root_CommunityMemberInviteCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteResponse> __Marshaller_root_CommunityMemberInviteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteRespondRequest> __Marshaller_root_CommunityMemberInviteRespondRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteRespondRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteRespondResponse> __Marshaller_root_CommunityMemberInviteRespondResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteRespondResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteLinkJoinRequest> __Marshaller_root_CommunityMemberInviteLinkJoinRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteLinkJoinRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteLinkJoinResponse> __Marshaller_root_CommunityMemberInviteLinkJoinResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteLinkJoinResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteDeleteRequest> __Marshaller_root_CommunityMemberInviteDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteDeleteResponse> __Marshaller_root_CommunityMemberInviteDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteGetRequest> __Marshaller_root_CommunityMemberInviteGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteListRequest> __Marshaller_root_CommunityMemberInviteListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberInviteListResponse> __Marshaller_root_CommunityMemberInviteListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberInviteListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberInviteCreateRequest, CommunityMemberInviteResponse> __Method_Create = new Method<CommunityMemberInviteCreateRequest, CommunityMemberInviteResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_CommunityMemberInviteCreateRequest, __Marshaller_root_CommunityMemberInviteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberInviteRespondRequest, CommunityMemberInviteRespondResponse> __Method_Respond = new Method<CommunityMemberInviteRespondRequest, CommunityMemberInviteRespondResponse>(MethodType.Unary, __ServiceName, "Respond", __Marshaller_root_CommunityMemberInviteRespondRequest, __Marshaller_root_CommunityMemberInviteRespondResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberInviteLinkJoinRequest, CommunityMemberInviteLinkJoinResponse> __Method_LinkJoin = new Method<CommunityMemberInviteLinkJoinRequest, CommunityMemberInviteLinkJoinResponse>(MethodType.Unary, __ServiceName, "LinkJoin", __Marshaller_root_CommunityMemberInviteLinkJoinRequest, __Marshaller_root_CommunityMemberInviteLinkJoinResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberInviteDeleteRequest, CommunityMemberInviteDeleteResponse> __Method_Delete = new Method<CommunityMemberInviteDeleteRequest, CommunityMemberInviteDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_CommunityMemberInviteDeleteRequest, __Marshaller_root_CommunityMemberInviteDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberInviteGetRequest, CommunityMemberInviteResponse> __Method_Get = new Method<CommunityMemberInviteGetRequest, CommunityMemberInviteResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_CommunityMemberInviteGetRequest, __Marshaller_root_CommunityMemberInviteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberInviteListRequest, CommunityMemberInviteListResponse> __Method_List = new Method<CommunityMemberInviteListRequest, CommunityMemberInviteListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_CommunityMemberInviteListRequest, __Marshaller_root_CommunityMemberInviteListResponse);

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
