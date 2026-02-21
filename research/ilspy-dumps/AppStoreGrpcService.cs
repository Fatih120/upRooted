using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class AppStoreGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class AppStoreGrpcServiceClient : ClientBase<AppStoreGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public AppStoreGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public AppStoreGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected AppStoreGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreGetResponse Get(AppStoreGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreGetResponse Get(AppStoreGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreGetResponse> GetAsync(AppStoreGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreGetResponse> GetAsync(AppStoreGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreGetGlobalSettingsResponse GetGlobalSettings(AppStoreGetGlobalSettingsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetGlobalSettings(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreGetGlobalSettingsResponse GetGlobalSettings(AppStoreGetGlobalSettingsRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetGlobalSettings, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreGetGlobalSettingsResponse> GetGlobalSettingsAsync(AppStoreGetGlobalSettingsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetGlobalSettingsAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreGetGlobalSettingsResponse> GetGlobalSettingsAsync(AppStoreGetGlobalSettingsRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetGlobalSettings, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreListResponse List(AppStoreListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreListResponse List(AppStoreListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreListResponse> ListAsync(AppStoreListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreListResponse> ListAsync(AppStoreListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreSearchResponse Search(AppStoreSearchRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Search(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreSearchResponse Search(AppStoreSearchRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Search, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreSearchResponse> SearchAsync(AppStoreSearchRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SearchAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreSearchResponse> SearchAsync(AppStoreSearchRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Search, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreListVersionResponse ListVersion(AppStoreListVersionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListVersion(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreListVersionResponse ListVersion(AppStoreListVersionRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListVersion, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreListVersionResponse> ListVersionAsync(AppStoreListVersionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListVersionAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreListVersionResponse> ListVersionAsync(AppStoreListVersionRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListVersion, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreListShortResponse ListShort(AppStoreListShortRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListShort(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AppStoreListShortResponse ListShort(AppStoreListShortRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListShort, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreListShortResponse> ListShortAsync(AppStoreListShortRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListShortAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AppStoreListShortResponse> ListShortAsync(AppStoreListShortRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListShort, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override AppStoreGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new AppStoreGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.AppStoreGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreGetRequest> __Marshaller_root_AppStoreGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreGetResponse> __Marshaller_root_AppStoreGetResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreGetResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreGetGlobalSettingsRequest> __Marshaller_root_AppStoreGetGlobalSettingsRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreGetGlobalSettingsRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreGetGlobalSettingsResponse> __Marshaller_root_AppStoreGetGlobalSettingsResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreGetGlobalSettingsResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreListRequest> __Marshaller_root_AppStoreListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreListResponse> __Marshaller_root_AppStoreListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreSearchRequest> __Marshaller_root_AppStoreSearchRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreSearchRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreSearchResponse> __Marshaller_root_AppStoreSearchResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreSearchResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreListVersionRequest> __Marshaller_root_AppStoreListVersionRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreListVersionRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreListVersionResponse> __Marshaller_root_AppStoreListVersionResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreListVersionResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreListShortRequest> __Marshaller_root_AppStoreListShortRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreListShortRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AppStoreListShortResponse> __Marshaller_root_AppStoreListShortResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AppStoreListShortResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AppStoreGetRequest, AppStoreGetResponse> __Method_Get = new Method<AppStoreGetRequest, AppStoreGetResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_AppStoreGetRequest, __Marshaller_root_AppStoreGetResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AppStoreGetGlobalSettingsRequest, AppStoreGetGlobalSettingsResponse> __Method_GetGlobalSettings = new Method<AppStoreGetGlobalSettingsRequest, AppStoreGetGlobalSettingsResponse>(MethodType.Unary, __ServiceName, "GetGlobalSettings", __Marshaller_root_AppStoreGetGlobalSettingsRequest, __Marshaller_root_AppStoreGetGlobalSettingsResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AppStoreListRequest, AppStoreListResponse> __Method_List = new Method<AppStoreListRequest, AppStoreListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_AppStoreListRequest, __Marshaller_root_AppStoreListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AppStoreSearchRequest, AppStoreSearchResponse> __Method_Search = new Method<AppStoreSearchRequest, AppStoreSearchResponse>(MethodType.Unary, __ServiceName, "Search", __Marshaller_root_AppStoreSearchRequest, __Marshaller_root_AppStoreSearchResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AppStoreListVersionRequest, AppStoreListVersionResponse> __Method_ListVersion = new Method<AppStoreListVersionRequest, AppStoreListVersionResponse>(MethodType.Unary, __ServiceName, "ListVersion", __Marshaller_root_AppStoreListVersionRequest, __Marshaller_root_AppStoreListVersionResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AppStoreListShortRequest, AppStoreListShortResponse> __Method_ListShort = new Method<AppStoreListShortRequest, AppStoreListShortResponse>(MethodType.Unary, __ServiceName, "ListShort", __Marshaller_root_AppStoreListShortRequest, __Marshaller_root_AppStoreListShortResponse);

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
