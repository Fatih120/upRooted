using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class LinkGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class LinkGrpcServiceClient : ClientBase<LinkGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public LinkGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public LinkGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected LinkGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkCreateResponse CommunityInviteLinkCreate(CommunityInviteLinkCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkCreate(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkCreateResponse CommunityInviteLinkCreate(CommunityInviteLinkCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_CommunityInviteLinkCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkCreateResponse> CommunityInviteLinkCreateAsync(CommunityInviteLinkCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkCreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkCreateResponse> CommunityInviteLinkCreateAsync(CommunityInviteLinkCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_CommunityInviteLinkCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkResponse CommunityInviteLinkGet(CommunityInviteLinkGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkGet(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkResponse CommunityInviteLinkGet(CommunityInviteLinkGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_CommunityInviteLinkGet, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkResponse> CommunityInviteLinkGetAsync(CommunityInviteLinkGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkGetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkResponse> CommunityInviteLinkGetAsync(CommunityInviteLinkGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_CommunityInviteLinkGet, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkGetInfoResponse CommunityInviteLinkGetInfo(CommunityInviteLinkGetInfoRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkGetInfo(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkGetInfoResponse CommunityInviteLinkGetInfo(CommunityInviteLinkGetInfoRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_CommunityInviteLinkGetInfo, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkGetInfoResponse> CommunityInviteLinkGetInfoAsync(CommunityInviteLinkGetInfoRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkGetInfoAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkGetInfoResponse> CommunityInviteLinkGetInfoAsync(CommunityInviteLinkGetInfoRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_CommunityInviteLinkGetInfo, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkDeleteResponse CommunityInviteLinkDelete(CommunityInviteLinkDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkDelete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkDeleteResponse CommunityInviteLinkDelete(CommunityInviteLinkDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_CommunityInviteLinkDelete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkDeleteResponse> CommunityInviteLinkDeleteAsync(CommunityInviteLinkDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkDeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkDeleteResponse> CommunityInviteLinkDeleteAsync(CommunityInviteLinkDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_CommunityInviteLinkDelete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkListResponse CommunityInviteLinkList(CommunityInviteLinkListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkList(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkListResponse CommunityInviteLinkList(CommunityInviteLinkListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_CommunityInviteLinkList, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkListResponse> CommunityInviteLinkListAsync(CommunityInviteLinkListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkListResponse> CommunityInviteLinkListAsync(CommunityInviteLinkListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_CommunityInviteLinkList, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkListMineResponse CommunityInviteLinkListMine(CommunityInviteLinkListMineRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkListMine(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityInviteLinkListMineResponse CommunityInviteLinkListMine(CommunityInviteLinkListMineRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_CommunityInviteLinkListMine, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkListMineResponse> CommunityInviteLinkListMineAsync(CommunityInviteLinkListMineRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CommunityInviteLinkListMineAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityInviteLinkListMineResponse> CommunityInviteLinkListMineAsync(CommunityInviteLinkListMineRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_CommunityInviteLinkListMine, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override LinkGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new LinkGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.LinkGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkCreateRequest> __Marshaller_root_CommunityInviteLinkCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkCreateResponse> __Marshaller_root_CommunityInviteLinkCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkGetRequest> __Marshaller_root_CommunityInviteLinkGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkResponse> __Marshaller_root_CommunityInviteLinkResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkGetInfoRequest> __Marshaller_root_CommunityInviteLinkGetInfoRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkGetInfoRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkGetInfoResponse> __Marshaller_root_CommunityInviteLinkGetInfoResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkGetInfoResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkDeleteRequest> __Marshaller_root_CommunityInviteLinkDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkDeleteResponse> __Marshaller_root_CommunityInviteLinkDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkListRequest> __Marshaller_root_CommunityInviteLinkListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkListResponse> __Marshaller_root_CommunityInviteLinkListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkListMineRequest> __Marshaller_root_CommunityInviteLinkListMineRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkListMineRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityInviteLinkListMineResponse> __Marshaller_root_CommunityInviteLinkListMineResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityInviteLinkListMineResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityInviteLinkCreateRequest, CommunityInviteLinkCreateResponse> __Method_CommunityInviteLinkCreate = new Method<CommunityInviteLinkCreateRequest, CommunityInviteLinkCreateResponse>(MethodType.Unary, __ServiceName, "CommunityInviteLinkCreate", __Marshaller_root_CommunityInviteLinkCreateRequest, __Marshaller_root_CommunityInviteLinkCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityInviteLinkGetRequest, CommunityInviteLinkResponse> __Method_CommunityInviteLinkGet = new Method<CommunityInviteLinkGetRequest, CommunityInviteLinkResponse>(MethodType.Unary, __ServiceName, "CommunityInviteLinkGet", __Marshaller_root_CommunityInviteLinkGetRequest, __Marshaller_root_CommunityInviteLinkResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityInviteLinkGetInfoRequest, CommunityInviteLinkGetInfoResponse> __Method_CommunityInviteLinkGetInfo = new Method<CommunityInviteLinkGetInfoRequest, CommunityInviteLinkGetInfoResponse>(MethodType.Unary, __ServiceName, "CommunityInviteLinkGetInfo", __Marshaller_root_CommunityInviteLinkGetInfoRequest, __Marshaller_root_CommunityInviteLinkGetInfoResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityInviteLinkDeleteRequest, CommunityInviteLinkDeleteResponse> __Method_CommunityInviteLinkDelete = new Method<CommunityInviteLinkDeleteRequest, CommunityInviteLinkDeleteResponse>(MethodType.Unary, __ServiceName, "CommunityInviteLinkDelete", __Marshaller_root_CommunityInviteLinkDeleteRequest, __Marshaller_root_CommunityInviteLinkDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityInviteLinkListRequest, CommunityInviteLinkListResponse> __Method_CommunityInviteLinkList = new Method<CommunityInviteLinkListRequest, CommunityInviteLinkListResponse>(MethodType.Unary, __ServiceName, "CommunityInviteLinkList", __Marshaller_root_CommunityInviteLinkListRequest, __Marshaller_root_CommunityInviteLinkListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityInviteLinkListMineRequest, CommunityInviteLinkListMineResponse> __Method_CommunityInviteLinkListMine = new Method<CommunityInviteLinkListMineRequest, CommunityInviteLinkListMineResponse>(MethodType.Unary, __ServiceName, "CommunityInviteLinkListMine", __Marshaller_root_CommunityInviteLinkListMineRequest, __Marshaller_root_CommunityInviteLinkListMineResponse);

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
