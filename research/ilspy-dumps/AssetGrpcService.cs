using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class AssetGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class AssetGrpcServiceClient : ClientBase<AssetGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public AssetGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public AssetGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected AssetGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AssetGetResponse Get(AssetGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AssetGetResponse Get(AssetGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AssetGetResponse> GetAsync(AssetGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AssetGetResponse> GetAsync(AssetGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AssetUploadTokenStatusResponse GetUploadTokenStatus(AssetUploadTokenStatusRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetUploadTokenStatus(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AssetUploadTokenStatusResponse GetUploadTokenStatus(AssetUploadTokenStatusRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetUploadTokenStatus, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AssetUploadTokenStatusResponse> GetUploadTokenStatusAsync(AssetUploadTokenStatusRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetUploadTokenStatusAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AssetUploadTokenStatusResponse> GetUploadTokenStatusAsync(AssetUploadTokenStatusRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetUploadTokenStatus, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AssetAppCreateResponse AppCreate(AssetAppCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AppCreate(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AssetAppCreateResponse AppCreate(AssetAppCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_AppCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AssetAppCreateResponse> AppCreateAsync(AssetAppCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AppCreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AssetAppCreateResponse> AppCreateAsync(AssetAppCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_AppCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override AssetGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new AssetGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.AssetGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AssetGetRequest> __Marshaller_root_AssetGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AssetGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AssetGetResponse> __Marshaller_root_AssetGetResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AssetGetResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AssetUploadTokenStatusRequest> __Marshaller_root_AssetUploadTokenStatusRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AssetUploadTokenStatusRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AssetUploadTokenStatusResponse> __Marshaller_root_AssetUploadTokenStatusResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AssetUploadTokenStatusResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AssetAppCreateRequest> __Marshaller_root_AssetAppCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AssetAppCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AssetAppCreateResponse> __Marshaller_root_AssetAppCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AssetAppCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AssetGetRequest, AssetGetResponse> __Method_Get = new Method<AssetGetRequest, AssetGetResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_AssetGetRequest, __Marshaller_root_AssetGetResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AssetUploadTokenStatusRequest, AssetUploadTokenStatusResponse> __Method_GetUploadTokenStatus = new Method<AssetUploadTokenStatusRequest, AssetUploadTokenStatusResponse>(MethodType.Unary, __ServiceName, "GetUploadTokenStatus", __Marshaller_root_AssetUploadTokenStatusRequest, __Marshaller_root_AssetUploadTokenStatusResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AssetAppCreateRequest, AssetAppCreateResponse> __Method_AppCreate = new Method<AssetAppCreateRequest, AssetAppCreateResponse>(MethodType.Unary, __ServiceName, "AppCreate", __Marshaller_root_AssetAppCreateRequest, __Marshaller_root_AssetAppCreateResponse);

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
