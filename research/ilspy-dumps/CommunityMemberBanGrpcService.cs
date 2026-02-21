using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class CommunityMemberBanGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class CommunityMemberBanGrpcServiceClient : ClientBase<CommunityMemberBanGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityMemberBanGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityMemberBanGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected CommunityMemberBanGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanResponse Create(CommunityMemberBanCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanResponse Create(CommunityMemberBanCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanResponse> CreateAsync(CommunityMemberBanCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanResponse> CreateAsync(CommunityMemberBanCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanBulkResponse CreateBulk(CommunityMemberBanCreateBulkRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateBulk(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanBulkResponse CreateBulk(CommunityMemberBanCreateBulkRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_CreateBulk, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanBulkResponse> CreateBulkAsync(CommunityMemberBanCreateBulkRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateBulkAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanBulkResponse> CreateBulkAsync(CommunityMemberBanCreateBulkRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_CreateBulk, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanDeleteResponse Delete(CommunityMemberBanDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanDeleteResponse Delete(CommunityMemberBanDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanDeleteResponse> DeleteAsync(CommunityMemberBanDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanDeleteResponse> DeleteAsync(CommunityMemberBanDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanKickResponse Kick(CommunityMemberBanKickRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Kick(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanKickResponse Kick(CommunityMemberBanKickRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Kick, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanKickResponse> KickAsync(CommunityMemberBanKickRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return KickAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanKickResponse> KickAsync(CommunityMemberBanKickRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Kick, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanKickBulkResponse KickBulk(CommunityMemberBanKickBulkRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return KickBulk(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanKickBulkResponse KickBulk(CommunityMemberBanKickBulkRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_KickBulk, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanKickBulkResponse> KickBulkAsync(CommunityMemberBanKickBulkRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return KickBulkAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanKickBulkResponse> KickBulkAsync(CommunityMemberBanKickBulkRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_KickBulk, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanListResponse List(CommunityMemberBanListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanListResponse List(CommunityMemberBanListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanListResponse> ListAsync(CommunityMemberBanListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanListResponse> ListAsync(CommunityMemberBanListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanResponse Get(CommunityMemberBanGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberBanResponse Get(CommunityMemberBanGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanResponse> GetAsync(CommunityMemberBanGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberBanResponse> GetAsync(CommunityMemberBanGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override CommunityMemberBanGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new CommunityMemberBanGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.CommunityMemberBanGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanCreateRequest> __Marshaller_root_CommunityMemberBanCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanResponse> __Marshaller_root_CommunityMemberBanResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanCreateBulkRequest> __Marshaller_root_CommunityMemberBanCreateBulkRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanCreateBulkRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanBulkResponse> __Marshaller_root_CommunityMemberBanBulkResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanBulkResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanDeleteRequest> __Marshaller_root_CommunityMemberBanDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanDeleteResponse> __Marshaller_root_CommunityMemberBanDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanKickRequest> __Marshaller_root_CommunityMemberBanKickRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanKickRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanKickResponse> __Marshaller_root_CommunityMemberBanKickResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanKickResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanKickBulkRequest> __Marshaller_root_CommunityMemberBanKickBulkRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanKickBulkRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanKickBulkResponse> __Marshaller_root_CommunityMemberBanKickBulkResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanKickBulkResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanListRequest> __Marshaller_root_CommunityMemberBanListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanListResponse> __Marshaller_root_CommunityMemberBanListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberBanGetRequest> __Marshaller_root_CommunityMemberBanGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberBanGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberBanCreateRequest, CommunityMemberBanResponse> __Method_Create = new Method<CommunityMemberBanCreateRequest, CommunityMemberBanResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_CommunityMemberBanCreateRequest, __Marshaller_root_CommunityMemberBanResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberBanCreateBulkRequest, CommunityMemberBanBulkResponse> __Method_CreateBulk = new Method<CommunityMemberBanCreateBulkRequest, CommunityMemberBanBulkResponse>(MethodType.Unary, __ServiceName, "CreateBulk", __Marshaller_root_CommunityMemberBanCreateBulkRequest, __Marshaller_root_CommunityMemberBanBulkResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberBanDeleteRequest, CommunityMemberBanDeleteResponse> __Method_Delete = new Method<CommunityMemberBanDeleteRequest, CommunityMemberBanDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_CommunityMemberBanDeleteRequest, __Marshaller_root_CommunityMemberBanDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberBanKickRequest, CommunityMemberBanKickResponse> __Method_Kick = new Method<CommunityMemberBanKickRequest, CommunityMemberBanKickResponse>(MethodType.Unary, __ServiceName, "Kick", __Marshaller_root_CommunityMemberBanKickRequest, __Marshaller_root_CommunityMemberBanKickResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberBanKickBulkRequest, CommunityMemberBanKickBulkResponse> __Method_KickBulk = new Method<CommunityMemberBanKickBulkRequest, CommunityMemberBanKickBulkResponse>(MethodType.Unary, __ServiceName, "KickBulk", __Marshaller_root_CommunityMemberBanKickBulkRequest, __Marshaller_root_CommunityMemberBanKickBulkResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberBanListRequest, CommunityMemberBanListResponse> __Method_List = new Method<CommunityMemberBanListRequest, CommunityMemberBanListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_CommunityMemberBanListRequest, __Marshaller_root_CommunityMemberBanListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberBanGetRequest, CommunityMemberBanResponse> __Method_Get = new Method<CommunityMemberBanGetRequest, CommunityMemberBanResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_CommunityMemberBanGetRequest, __Marshaller_root_CommunityMemberBanResponse);

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
