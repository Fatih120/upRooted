using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class FriendshipGroupGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class FriendshipGroupGrpcServiceClient : ClientBase<FriendshipGroupGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public FriendshipGroupGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public FriendshipGroupGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected FriendshipGroupGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupCreateResponse Create(FriendshipGroupCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupCreateResponse Create(FriendshipGroupCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupCreateResponse> CreateAsync(FriendshipGroupCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupCreateResponse> CreateAsync(FriendshipGroupCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupDeleteResponse Delete(FriendshipGroupDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupDeleteResponse Delete(FriendshipGroupDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupDeleteResponse> DeleteAsync(FriendshipGroupDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupDeleteResponse> DeleteAsync(FriendshipGroupDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupListResponse List(FriendshipGroupListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupListResponse List(FriendshipGroupListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupListResponse> ListAsync(FriendshipGroupListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupListResponse> ListAsync(FriendshipGroupListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupEditResponse Edit(FriendshipGroupEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Edit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupEditResponse Edit(FriendshipGroupEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupEditResponse> EditAsync(FriendshipGroupEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return EditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupEditResponse> EditAsync(FriendshipGroupEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupMoveResponse Move(FriendshipGroupMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Move(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FriendshipGroupMoveResponse Move(FriendshipGroupMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupMoveResponse> MoveAsync(FriendshipGroupMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return MoveAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FriendshipGroupMoveResponse> MoveAsync(FriendshipGroupMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override FriendshipGroupGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new FriendshipGroupGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.FriendshipGroupGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupCreateRequest> __Marshaller_root_FriendshipGroupCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupCreateResponse> __Marshaller_root_FriendshipGroupCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupDeleteRequest> __Marshaller_root_FriendshipGroupDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupDeleteResponse> __Marshaller_root_FriendshipGroupDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupListRequest> __Marshaller_root_FriendshipGroupListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupListResponse> __Marshaller_root_FriendshipGroupListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupEditRequest> __Marshaller_root_FriendshipGroupEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupEditResponse> __Marshaller_root_FriendshipGroupEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupMoveRequest> __Marshaller_root_FriendshipGroupMoveRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupMoveRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FriendshipGroupMoveResponse> __Marshaller_root_FriendshipGroupMoveResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FriendshipGroupMoveResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FriendshipGroupCreateRequest, FriendshipGroupCreateResponse> __Method_Create = new Method<FriendshipGroupCreateRequest, FriendshipGroupCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_FriendshipGroupCreateRequest, __Marshaller_root_FriendshipGroupCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FriendshipGroupDeleteRequest, FriendshipGroupDeleteResponse> __Method_Delete = new Method<FriendshipGroupDeleteRequest, FriendshipGroupDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_FriendshipGroupDeleteRequest, __Marshaller_root_FriendshipGroupDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FriendshipGroupListRequest, FriendshipGroupListResponse> __Method_List = new Method<FriendshipGroupListRequest, FriendshipGroupListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_FriendshipGroupListRequest, __Marshaller_root_FriendshipGroupListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FriendshipGroupEditRequest, FriendshipGroupEditResponse> __Method_Edit = new Method<FriendshipGroupEditRequest, FriendshipGroupEditResponse>(MethodType.Unary, __ServiceName, "Edit", __Marshaller_root_FriendshipGroupEditRequest, __Marshaller_root_FriendshipGroupEditResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FriendshipGroupMoveRequest, FriendshipGroupMoveResponse> __Method_Move = new Method<FriendshipGroupMoveRequest, FriendshipGroupMoveResponse>(MethodType.Unary, __ServiceName, "Move", __Marshaller_root_FriendshipGroupMoveRequest, __Marshaller_root_FriendshipGroupMoveResponse);

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
