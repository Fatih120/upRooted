using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class CommunityRoleGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class CommunityRoleGrpcServiceClient : ClientBase<CommunityRoleGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityRoleGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityRoleGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected CommunityRoleGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleCreateResponse Create(CommunityRoleCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleCreateResponse Create(CommunityRoleCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleCreateResponse> CreateAsync(CommunityRoleCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleCreateResponse> CreateAsync(CommunityRoleCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleEditResponse Edit(CommunityRoleEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Edit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleEditResponse Edit(CommunityRoleEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleEditResponse> EditAsync(CommunityRoleEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return EditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleEditResponse> EditAsync(CommunityRoleEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleMoveResponse Move(CommunityRoleMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Move(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleMoveResponse Move(CommunityRoleMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleMoveResponse> MoveAsync(CommunityRoleMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return MoveAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleMoveResponse> MoveAsync(CommunityRoleMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleDeleteResponse Delete(CommunityRoleDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleDeleteResponse Delete(CommunityRoleDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleDeleteResponse> DeleteAsync(CommunityRoleDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleDeleteResponse> DeleteAsync(CommunityRoleDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleListResponse List(CommunityRoleListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleListResponse List(CommunityRoleListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleListResponse> ListAsync(CommunityRoleListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleListResponse> ListAsync(CommunityRoleListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleResponse Get(CommunityRoleGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityRoleResponse Get(CommunityRoleGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleResponse> GetAsync(CommunityRoleGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityRoleResponse> GetAsync(CommunityRoleGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override CommunityRoleGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new CommunityRoleGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.CommunityRoleGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleCreateRequest> __Marshaller_root_CommunityRoleCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleCreateResponse> __Marshaller_root_CommunityRoleCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleEditRequest> __Marshaller_root_CommunityRoleEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleEditResponse> __Marshaller_root_CommunityRoleEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleMoveRequest> __Marshaller_root_CommunityRoleMoveRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleMoveRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleMoveResponse> __Marshaller_root_CommunityRoleMoveResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleMoveResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleDeleteRequest> __Marshaller_root_CommunityRoleDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleDeleteResponse> __Marshaller_root_CommunityRoleDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleListRequest> __Marshaller_root_CommunityRoleListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleListResponse> __Marshaller_root_CommunityRoleListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleGetRequest> __Marshaller_root_CommunityRoleGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityRoleResponse> __Marshaller_root_CommunityRoleResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityRoleResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityRoleCreateRequest, CommunityRoleCreateResponse> __Method_Create = new Method<CommunityRoleCreateRequest, CommunityRoleCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_CommunityRoleCreateRequest, __Marshaller_root_CommunityRoleCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityRoleEditRequest, CommunityRoleEditResponse> __Method_Edit = new Method<CommunityRoleEditRequest, CommunityRoleEditResponse>(MethodType.Unary, __ServiceName, "Edit", __Marshaller_root_CommunityRoleEditRequest, __Marshaller_root_CommunityRoleEditResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityRoleMoveRequest, CommunityRoleMoveResponse> __Method_Move = new Method<CommunityRoleMoveRequest, CommunityRoleMoveResponse>(MethodType.Unary, __ServiceName, "Move", __Marshaller_root_CommunityRoleMoveRequest, __Marshaller_root_CommunityRoleMoveResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityRoleDeleteRequest, CommunityRoleDeleteResponse> __Method_Delete = new Method<CommunityRoleDeleteRequest, CommunityRoleDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_CommunityRoleDeleteRequest, __Marshaller_root_CommunityRoleDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityRoleListRequest, CommunityRoleListResponse> __Method_List = new Method<CommunityRoleListRequest, CommunityRoleListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_CommunityRoleListRequest, __Marshaller_root_CommunityRoleListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityRoleGetRequest, CommunityRoleResponse> __Method_Get = new Method<CommunityRoleGetRequest, CommunityRoleResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_CommunityRoleGetRequest, __Marshaller_root_CommunityRoleResponse);

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
