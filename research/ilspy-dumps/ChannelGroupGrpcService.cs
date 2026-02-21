using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class ChannelGroupGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class ChannelGroupGrpcServiceClient : ClientBase<ChannelGroupGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public ChannelGroupGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public ChannelGroupGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected ChannelGroupGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupCreateResponse Create(ChannelGroupCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupCreateResponse Create(ChannelGroupCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupCreateResponse> CreateAsync(ChannelGroupCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupCreateResponse> CreateAsync(ChannelGroupCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupResponse Get(ChannelGroupGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupResponse Get(ChannelGroupGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupResponse> GetAsync(ChannelGroupGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupResponse> GetAsync(ChannelGroupGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupListResponse List(ChannelGroupListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupListResponse List(ChannelGroupListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupListResponse> ListAsync(ChannelGroupListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupListResponse> ListAsync(ChannelGroupListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupEditResponse Edit(ChannelGroupEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Edit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupEditResponse Edit(ChannelGroupEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupEditResponse> EditAsync(ChannelGroupEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return EditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupEditResponse> EditAsync(ChannelGroupEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupMoveResponse Move(ChannelGroupMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Move(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupMoveResponse Move(ChannelGroupMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupMoveResponse> MoveAsync(ChannelGroupMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return MoveAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupMoveResponse> MoveAsync(ChannelGroupMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupDeleteResponse Delete(ChannelGroupDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual ChannelGroupDeleteResponse Delete(ChannelGroupDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupDeleteResponse> DeleteAsync(ChannelGroupDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<ChannelGroupDeleteResponse> DeleteAsync(ChannelGroupDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override ChannelGroupGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new ChannelGroupGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.ChannelGroupGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupCreateRequest> __Marshaller_root_ChannelGroupCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupCreateResponse> __Marshaller_root_ChannelGroupCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupGetRequest> __Marshaller_root_ChannelGroupGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupResponse> __Marshaller_root_ChannelGroupResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupListRequest> __Marshaller_root_ChannelGroupListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupListResponse> __Marshaller_root_ChannelGroupListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupEditRequest> __Marshaller_root_ChannelGroupEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupEditResponse> __Marshaller_root_ChannelGroupEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupMoveRequest> __Marshaller_root_ChannelGroupMoveRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupMoveRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupMoveResponse> __Marshaller_root_ChannelGroupMoveResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupMoveResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupDeleteRequest> __Marshaller_root_ChannelGroupDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<ChannelGroupDeleteResponse> __Marshaller_root_ChannelGroupDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, ChannelGroupDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<ChannelGroupCreateRequest, ChannelGroupCreateResponse> __Method_Create = new Method<ChannelGroupCreateRequest, ChannelGroupCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_ChannelGroupCreateRequest, __Marshaller_root_ChannelGroupCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<ChannelGroupGetRequest, ChannelGroupResponse> __Method_Get = new Method<ChannelGroupGetRequest, ChannelGroupResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_ChannelGroupGetRequest, __Marshaller_root_ChannelGroupResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<ChannelGroupListRequest, ChannelGroupListResponse> __Method_List = new Method<ChannelGroupListRequest, ChannelGroupListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_ChannelGroupListRequest, __Marshaller_root_ChannelGroupListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<ChannelGroupEditRequest, ChannelGroupEditResponse> __Method_Edit = new Method<ChannelGroupEditRequest, ChannelGroupEditResponse>(MethodType.Unary, __ServiceName, "Edit", __Marshaller_root_ChannelGroupEditRequest, __Marshaller_root_ChannelGroupEditResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<ChannelGroupMoveRequest, ChannelGroupMoveResponse> __Method_Move = new Method<ChannelGroupMoveRequest, ChannelGroupMoveResponse>(MethodType.Unary, __ServiceName, "Move", __Marshaller_root_ChannelGroupMoveRequest, __Marshaller_root_ChannelGroupMoveResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<ChannelGroupDeleteRequest, ChannelGroupDeleteResponse> __Method_Delete = new Method<ChannelGroupDeleteRequest, ChannelGroupDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_ChannelGroupDeleteRequest, __Marshaller_root_ChannelGroupDeleteResponse);

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
