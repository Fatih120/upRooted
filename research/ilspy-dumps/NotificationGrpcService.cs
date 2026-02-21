using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class NotificationGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class NotificationGrpcServiceClient : ClientBase<NotificationGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public NotificationGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public NotificationGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected NotificationGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationListResponse List(NotificationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationListResponse List(NotificationRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationListResponse> ListAsync(NotificationRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationListResponse> ListAsync(NotificationRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationCountListResponse CountUnviewed(NotificationCountUnviewedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CountUnviewed(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationCountListResponse CountUnviewed(NotificationCountUnviewedRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_CountUnviewed, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationCountListResponse> CountUnviewedAsync(NotificationCountUnviewedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CountUnviewedAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationCountListResponse> CountUnviewedAsync(NotificationCountUnviewedRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_CountUnviewed, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationSetViewedResponse SetViewed(NotificationSetViewedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetViewed(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationSetViewedResponse SetViewed(NotificationSetViewedRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetViewed, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationSetViewedResponse> SetViewedAsync(NotificationSetViewedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetViewedAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationSetViewedResponse> SetViewedAsync(NotificationSetViewedRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetViewed, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationSetContainerViewedResponse SetContainerViewed(NotificationSetContainerViewedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetContainerViewed(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationSetContainerViewedResponse SetContainerViewed(NotificationSetContainerViewedRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetContainerViewed, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationSetContainerViewedResponse> SetContainerViewedAsync(NotificationSetContainerViewedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetContainerViewedAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationSetContainerViewedResponse> SetContainerViewedAsync(NotificationSetContainerViewedRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetContainerViewed, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationSetAllViewedResponse SetAllViewed(NotificationSetAllViewedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetAllViewed(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationSetAllViewedResponse SetAllViewed(NotificationSetAllViewedRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetAllViewed, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationSetAllViewedResponse> SetAllViewedAsync(NotificationSetAllViewedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetAllViewedAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationSetAllViewedResponse> SetAllViewedAsync(NotificationSetAllViewedRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetAllViewed, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationDeleteResponse Delete(NotificationDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationDeleteResponse Delete(NotificationDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationDeleteResponse> DeleteAsync(NotificationDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationDeleteResponse> DeleteAsync(NotificationDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationDeleteAllResponse DeleteAll(NotificationDeleteAllRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAll(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual NotificationDeleteAllResponse DeleteAll(NotificationDeleteAllRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_DeleteAll, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationDeleteAllResponse> DeleteAllAsync(NotificationDeleteAllRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAllAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<NotificationDeleteAllResponse> DeleteAllAsync(NotificationDeleteAllRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_DeleteAll, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override NotificationGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new NotificationGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.NotificationGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationRequest> __Marshaller_root_NotificationRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationListResponse> __Marshaller_root_NotificationListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationCountUnviewedRequest> __Marshaller_root_NotificationCountUnviewedRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationCountUnviewedRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationCountListResponse> __Marshaller_root_NotificationCountListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationCountListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationSetViewedRequest> __Marshaller_root_NotificationSetViewedRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationSetViewedRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationSetViewedResponse> __Marshaller_root_NotificationSetViewedResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationSetViewedResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationSetContainerViewedRequest> __Marshaller_root_NotificationSetContainerViewedRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationSetContainerViewedRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationSetContainerViewedResponse> __Marshaller_root_NotificationSetContainerViewedResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationSetContainerViewedResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationSetAllViewedRequest> __Marshaller_root_NotificationSetAllViewedRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationSetAllViewedRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationSetAllViewedResponse> __Marshaller_root_NotificationSetAllViewedResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationSetAllViewedResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationDeleteRequest> __Marshaller_root_NotificationDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationDeleteResponse> __Marshaller_root_NotificationDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationDeleteAllRequest> __Marshaller_root_NotificationDeleteAllRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationDeleteAllRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<NotificationDeleteAllResponse> __Marshaller_root_NotificationDeleteAllResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, NotificationDeleteAllResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<NotificationRequest, NotificationListResponse> __Method_List = new Method<NotificationRequest, NotificationListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_NotificationRequest, __Marshaller_root_NotificationListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<NotificationCountUnviewedRequest, NotificationCountListResponse> __Method_CountUnviewed = new Method<NotificationCountUnviewedRequest, NotificationCountListResponse>(MethodType.Unary, __ServiceName, "CountUnviewed", __Marshaller_root_NotificationCountUnviewedRequest, __Marshaller_root_NotificationCountListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<NotificationSetViewedRequest, NotificationSetViewedResponse> __Method_SetViewed = new Method<NotificationSetViewedRequest, NotificationSetViewedResponse>(MethodType.Unary, __ServiceName, "SetViewed", __Marshaller_root_NotificationSetViewedRequest, __Marshaller_root_NotificationSetViewedResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<NotificationSetContainerViewedRequest, NotificationSetContainerViewedResponse> __Method_SetContainerViewed = new Method<NotificationSetContainerViewedRequest, NotificationSetContainerViewedResponse>(MethodType.Unary, __ServiceName, "SetContainerViewed", __Marshaller_root_NotificationSetContainerViewedRequest, __Marshaller_root_NotificationSetContainerViewedResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<NotificationSetAllViewedRequest, NotificationSetAllViewedResponse> __Method_SetAllViewed = new Method<NotificationSetAllViewedRequest, NotificationSetAllViewedResponse>(MethodType.Unary, __ServiceName, "SetAllViewed", __Marshaller_root_NotificationSetAllViewedRequest, __Marshaller_root_NotificationSetAllViewedResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<NotificationDeleteRequest, NotificationDeleteResponse> __Method_Delete = new Method<NotificationDeleteRequest, NotificationDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_NotificationDeleteRequest, __Marshaller_root_NotificationDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<NotificationDeleteAllRequest, NotificationDeleteAllResponse> __Method_DeleteAll = new Method<NotificationDeleteAllRequest, NotificationDeleteAllResponse>(MethodType.Unary, __ServiceName, "DeleteAll", __Marshaller_root_NotificationDeleteAllRequest, __Marshaller_root_NotificationDeleteAllResponse);

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
