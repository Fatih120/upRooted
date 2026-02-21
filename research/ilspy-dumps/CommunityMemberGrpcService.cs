using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class CommunityMemberGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class CommunityMemberGrpcServiceClient : ClientBase<CommunityMemberGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityMemberGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityMemberGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected CommunityMemberGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberExtendedResponse Get(CommunityMemberGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberExtendedResponse Get(CommunityMemberGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberExtendedResponse> GetAsync(CommunityMemberGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberExtendedResponse> GetAsync(CommunityMemberGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberExtendedListResponse List(CommunityMemberListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberExtendedListResponse List(CommunityMemberListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberExtendedListResponse> ListAsync(CommunityMemberListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberExtendedListResponse> ListAsync(CommunityMemberListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberExtendedListResponse ListAll(CommunityMemberListAllRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAll(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberExtendedListResponse ListAll(CommunityMemberListAllRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListAll, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberExtendedListResponse> ListAllAsync(CommunityMemberListAllRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAllAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberExtendedListResponse> ListAllAsync(CommunityMemberListAllRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListAll, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberEditResponse Edit(CommunityMemberEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Edit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberEditResponse Edit(CommunityMemberEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberEditResponse> EditAsync(CommunityMemberEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return EditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberEditResponse> EditAsync(CommunityMemberEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberMoveResponse Move(CommunityMemberMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Move(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberMoveResponse Move(CommunityMemberMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberMoveResponse> MoveAsync(CommunityMemberMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return MoveAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberMoveResponse> MoveAsync(CommunityMemberMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberSetFavoriteResponse SetFavorite(CommunityMemberSetFavoriteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetFavorite(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberSetFavoriteResponse SetFavorite(CommunityMemberSetFavoriteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetFavorite, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberSetFavoriteResponse> SetFavoriteAsync(CommunityMemberSetFavoriteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetFavoriteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberSetFavoriteResponse> SetFavoriteAsync(CommunityMemberSetFavoriteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetFavorite, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override CommunityMemberGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new CommunityMemberGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.CommunityMemberGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberGetRequest> __Marshaller_root_CommunityMemberGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberExtendedResponse> __Marshaller_root_CommunityMemberExtendedResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberExtendedResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberListRequest> __Marshaller_root_CommunityMemberListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberExtendedListResponse> __Marshaller_root_CommunityMemberExtendedListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberExtendedListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberListAllRequest> __Marshaller_root_CommunityMemberListAllRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberListAllRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberEditRequest> __Marshaller_root_CommunityMemberEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberEditResponse> __Marshaller_root_CommunityMemberEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberMoveRequest> __Marshaller_root_CommunityMemberMoveRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberMoveRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberMoveResponse> __Marshaller_root_CommunityMemberMoveResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberMoveResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberSetFavoriteRequest> __Marshaller_root_CommunityMemberSetFavoriteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberSetFavoriteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberSetFavoriteResponse> __Marshaller_root_CommunityMemberSetFavoriteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberSetFavoriteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberGetRequest, CommunityMemberExtendedResponse> __Method_Get = new Method<CommunityMemberGetRequest, CommunityMemberExtendedResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_CommunityMemberGetRequest, __Marshaller_root_CommunityMemberExtendedResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberListRequest, CommunityMemberExtendedListResponse> __Method_List = new Method<CommunityMemberListRequest, CommunityMemberExtendedListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_CommunityMemberListRequest, __Marshaller_root_CommunityMemberExtendedListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberListAllRequest, CommunityMemberExtendedListResponse> __Method_ListAll = new Method<CommunityMemberListAllRequest, CommunityMemberExtendedListResponse>(MethodType.Unary, __ServiceName, "ListAll", __Marshaller_root_CommunityMemberListAllRequest, __Marshaller_root_CommunityMemberExtendedListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberEditRequest, CommunityMemberEditResponse> __Method_Edit = new Method<CommunityMemberEditRequest, CommunityMemberEditResponse>(MethodType.Unary, __ServiceName, "Edit", __Marshaller_root_CommunityMemberEditRequest, __Marshaller_root_CommunityMemberEditResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberMoveRequest, CommunityMemberMoveResponse> __Method_Move = new Method<CommunityMemberMoveRequest, CommunityMemberMoveResponse>(MethodType.Unary, __ServiceName, "Move", __Marshaller_root_CommunityMemberMoveRequest, __Marshaller_root_CommunityMemberMoveResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberSetFavoriteRequest, CommunityMemberSetFavoriteResponse> __Method_SetFavorite = new Method<CommunityMemberSetFavoriteRequest, CommunityMemberSetFavoriteResponse>(MethodType.Unary, __ServiceName, "SetFavorite", __Marshaller_root_CommunityMemberSetFavoriteRequest, __Marshaller_root_CommunityMemberSetFavoriteResponse);

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
