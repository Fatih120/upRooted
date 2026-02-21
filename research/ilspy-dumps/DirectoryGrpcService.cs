using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class DirectoryGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class DirectoryGrpcServiceClient : ClientBase<DirectoryGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public DirectoryGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public DirectoryGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected DirectoryGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryCreateResponse Create(DirectoryCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryCreateResponse Create(DirectoryCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryCreateResponse> CreateAsync(DirectoryCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryCreateResponse> CreateAsync(DirectoryCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryEditResponse Edit(DirectoryEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Edit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryEditResponse Edit(DirectoryEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryEditResponse> EditAsync(DirectoryEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return EditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryEditResponse> EditAsync(DirectoryEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryMoveResponse Move(DirectoryMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Move(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryMoveResponse Move(DirectoryMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryMoveResponse> MoveAsync(DirectoryMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return MoveAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryMoveResponse> MoveAsync(DirectoryMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryDeleteResponse Delete(DirectoryDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryDeleteResponse Delete(DirectoryDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryDeleteResponse> DeleteAsync(DirectoryDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryDeleteResponse> DeleteAsync(DirectoryDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryGetResponse Get(DirectoryGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryGetResponse Get(DirectoryGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryGetResponse> GetAsync(DirectoryGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryGetResponse> GetAsync(DirectoryGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryListResponse List(DirectoryListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryListResponse List(DirectoryListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryListResponse> ListAsync(DirectoryListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryListResponse> ListAsync(DirectoryListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryGetAllResponse GetAll(DirectoryGetAllRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAll(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryGetAllResponse GetAll(DirectoryGetAllRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetAll, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryGetAllResponse> GetAllAsync(DirectoryGetAllRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAllAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryGetAllResponse> GetAllAsync(DirectoryGetAllRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetAll, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryListAllResponse ListAll(DirectoryListAllRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAll(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual DirectoryListAllResponse ListAll(DirectoryListAllRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListAll, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryListAllResponse> ListAllAsync(DirectoryListAllRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAllAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<DirectoryListAllResponse> ListAllAsync(DirectoryListAllRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListAll, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override DirectoryGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new DirectoryGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.DirectoryGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryCreateRequest> __Marshaller_root_DirectoryCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryCreateResponse> __Marshaller_root_DirectoryCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryEditRequest> __Marshaller_root_DirectoryEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryEditResponse> __Marshaller_root_DirectoryEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryMoveRequest> __Marshaller_root_DirectoryMoveRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryMoveRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryMoveResponse> __Marshaller_root_DirectoryMoveResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryMoveResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryDeleteRequest> __Marshaller_root_DirectoryDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryDeleteResponse> __Marshaller_root_DirectoryDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryGetRequest> __Marshaller_root_DirectoryGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryGetResponse> __Marshaller_root_DirectoryGetResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryGetResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryListRequest> __Marshaller_root_DirectoryListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryListResponse> __Marshaller_root_DirectoryListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryGetAllRequest> __Marshaller_root_DirectoryGetAllRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryGetAllRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryGetAllResponse> __Marshaller_root_DirectoryGetAllResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryGetAllResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryListAllRequest> __Marshaller_root_DirectoryListAllRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryListAllRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<DirectoryListAllResponse> __Marshaller_root_DirectoryListAllResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, DirectoryListAllResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectoryCreateRequest, DirectoryCreateResponse> __Method_Create = new Method<DirectoryCreateRequest, DirectoryCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_DirectoryCreateRequest, __Marshaller_root_DirectoryCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectoryEditRequest, DirectoryEditResponse> __Method_Edit = new Method<DirectoryEditRequest, DirectoryEditResponse>(MethodType.Unary, __ServiceName, "Edit", __Marshaller_root_DirectoryEditRequest, __Marshaller_root_DirectoryEditResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectoryMoveRequest, DirectoryMoveResponse> __Method_Move = new Method<DirectoryMoveRequest, DirectoryMoveResponse>(MethodType.Unary, __ServiceName, "Move", __Marshaller_root_DirectoryMoveRequest, __Marshaller_root_DirectoryMoveResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectoryDeleteRequest, DirectoryDeleteResponse> __Method_Delete = new Method<DirectoryDeleteRequest, DirectoryDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_DirectoryDeleteRequest, __Marshaller_root_DirectoryDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectoryGetRequest, DirectoryGetResponse> __Method_Get = new Method<DirectoryGetRequest, DirectoryGetResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_DirectoryGetRequest, __Marshaller_root_DirectoryGetResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectoryListRequest, DirectoryListResponse> __Method_List = new Method<DirectoryListRequest, DirectoryListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_DirectoryListRequest, __Marshaller_root_DirectoryListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectoryGetAllRequest, DirectoryGetAllResponse> __Method_GetAll = new Method<DirectoryGetAllRequest, DirectoryGetAllResponse>(MethodType.Unary, __ServiceName, "GetAll", __Marshaller_root_DirectoryGetAllRequest, __Marshaller_root_DirectoryGetAllResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<DirectoryListAllRequest, DirectoryListAllResponse> __Method_ListAll = new Method<DirectoryListAllRequest, DirectoryListAllResponse>(MethodType.Unary, __ServiceName, "ListAll", __Marshaller_root_DirectoryListAllRequest, __Marshaller_root_DirectoryListAllResponse);

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
