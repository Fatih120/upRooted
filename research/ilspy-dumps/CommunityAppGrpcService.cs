using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class CommunityAppGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class CommunityAppGrpcServiceClient : ClientBase<CommunityAppGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityAppGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityAppGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected CommunityAppGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppGetResponse Get(CommunityAppGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppGetResponse Get(CommunityAppGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppGetResponse> GetAsync(CommunityAppGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppGetResponse> GetAsync(CommunityAppGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppListResponse List(CommunityAppListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppListResponse List(CommunityAppListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppListResponse> ListAsync(CommunityAppListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppListResponse> ListAsync(CommunityAppListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppAddResponse Add(CommunityAppAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Add(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppAddResponse Add(CommunityAppAddRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Add, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppAddResponse> AddAsync(CommunityAppAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AddAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppAddResponse> AddAsync(CommunityAppAddRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Add, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppRemoveResponse Remove(CommunityAppRemoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Remove(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppRemoveResponse Remove(CommunityAppRemoveRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Remove, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppRemoveResponse> RemoveAsync(CommunityAppRemoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RemoveAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppRemoveResponse> RemoveAsync(CommunityAppRemoveRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Remove, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppUpdateVersionResponse UpdateVersion(CommunityAppUpdateVersionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UpdateVersion(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppUpdateVersionResponse UpdateVersion(CommunityAppUpdateVersionRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_UpdateVersion, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppUpdateVersionResponse> UpdateVersionAsync(CommunityAppUpdateVersionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UpdateVersionAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppUpdateVersionResponse> UpdateVersionAsync(CommunityAppUpdateVersionRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_UpdateVersion, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetSettingsResponse SetSettings(CommunityAppSetSettingsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetSettings(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetSettingsResponse SetSettings(CommunityAppSetSettingsRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetSettings, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetSettingsResponse> SetSettingsAsync(CommunityAppSetSettingsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetSettingsAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetSettingsResponse> SetSettingsAsync(CommunityAppSetSettingsRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetSettings, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetButtonResponse SetButton(CommunityAppSetButtonRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetButton(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetButtonResponse SetButton(CommunityAppSetButtonRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetButton, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetButtonResponse> SetButtonAsync(CommunityAppSetButtonRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetButtonAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetButtonResponse> SetButtonAsync(CommunityAppSetButtonRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetButton, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetAppOrganizationAccessResponse SetAppOrganizationAccess(CommunityAppSetAppOrganizationAccessRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetAppOrganizationAccess(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetAppOrganizationAccessResponse SetAppOrganizationAccess(CommunityAppSetAppOrganizationAccessRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetAppOrganizationAccess, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetAppOrganizationAccessResponse> SetAppOrganizationAccessAsync(CommunityAppSetAppOrganizationAccessRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetAppOrganizationAccessAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetAppOrganizationAccessResponse> SetAppOrganizationAccessAsync(CommunityAppSetAppOrganizationAccessRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetAppOrganizationAccess, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetRatingResponse SetRating(CommunityAppSetRatingRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetRating(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetRatingResponse SetRating(CommunityAppSetRatingRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetRating, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetRatingResponse> SetRatingAsync(CommunityAppSetRatingRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetRatingAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetRatingResponse> SetRatingAsync(CommunityAppSetRatingRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetRating, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppGetSettingsResponse GetSettings(CommunityAppGetSettingsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetSettings(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppGetSettingsResponse GetSettings(CommunityAppGetSettingsRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetSettings, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppGetSettingsResponse> GetSettingsAsync(CommunityAppGetSettingsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetSettingsAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppGetSettingsResponse> GetSettingsAsync(CommunityAppGetSettingsRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetSettings, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppInitializeResponse Initialize(CommunityAppInitializeRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Initialize(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppInitializeResponse Initialize(CommunityAppInitializeRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Initialize, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppInitializeResponse> InitializeAsync(CommunityAppInitializeRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return InitializeAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppInitializeResponse> InitializeAsync(CommunityAppInitializeRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Initialize, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppListChannelGroupsResponse ListChannelGroups(CommunityAppListChannelGroupsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListChannelGroups(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppListChannelGroupsResponse ListChannelGroups(CommunityAppListChannelGroupsRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListChannelGroups, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppListChannelGroupsResponse> ListChannelGroupsAsync(CommunityAppListChannelGroupsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListChannelGroupsAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppListChannelGroupsResponse> ListChannelGroupsAsync(CommunityAppListChannelGroupsRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListChannelGroups, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppListChannelGroupsForAddResponse ListChannelGroupsForAdd(CommunityAppListChannelGroupsForAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListChannelGroupsForAdd(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppListChannelGroupsForAddResponse ListChannelGroupsForAdd(CommunityAppListChannelGroupsForAddRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListChannelGroupsForAdd, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppListChannelGroupsForAddResponse> ListChannelGroupsForAddAsync(CommunityAppListChannelGroupsForAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListChannelGroupsForAddAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppListChannelGroupsForAddResponse> ListChannelGroupsForAddAsync(CommunityAppListChannelGroupsForAddRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListChannelGroupsForAdd, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetDevSettingsResponse SetDevSettings(CommunityAppSetDevSettingsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetDevSettings(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAppSetDevSettingsResponse SetDevSettings(CommunityAppSetDevSettingsRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetDevSettings, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetDevSettingsResponse> SetDevSettingsAsync(CommunityAppSetDevSettingsRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetDevSettingsAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAppSetDevSettingsResponse> SetDevSettingsAsync(CommunityAppSetDevSettingsRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetDevSettings, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override CommunityAppGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new CommunityAppGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.CommunityAppGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppGetRequest> __Marshaller_root_CommunityAppGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppGetResponse> __Marshaller_root_CommunityAppGetResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppGetResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppListRequest> __Marshaller_root_CommunityAppListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppListResponse> __Marshaller_root_CommunityAppListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppAddRequest> __Marshaller_root_CommunityAppAddRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppAddRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppAddResponse> __Marshaller_root_CommunityAppAddResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppAddResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppRemoveRequest> __Marshaller_root_CommunityAppRemoveRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppRemoveRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppRemoveResponse> __Marshaller_root_CommunityAppRemoveResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppRemoveResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppUpdateVersionRequest> __Marshaller_root_CommunityAppUpdateVersionRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppUpdateVersionRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppUpdateVersionResponse> __Marshaller_root_CommunityAppUpdateVersionResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppUpdateVersionResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetSettingsRequest> __Marshaller_root_CommunityAppSetSettingsRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetSettingsRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetSettingsResponse> __Marshaller_root_CommunityAppSetSettingsResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetSettingsResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetButtonRequest> __Marshaller_root_CommunityAppSetButtonRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetButtonRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetButtonResponse> __Marshaller_root_CommunityAppSetButtonResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetButtonResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetAppOrganizationAccessRequest> __Marshaller_root_CommunityAppSetAppOrganizationAccessRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetAppOrganizationAccessRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetAppOrganizationAccessResponse> __Marshaller_root_CommunityAppSetAppOrganizationAccessResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetAppOrganizationAccessResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetRatingRequest> __Marshaller_root_CommunityAppSetRatingRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetRatingRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetRatingResponse> __Marshaller_root_CommunityAppSetRatingResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetRatingResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppGetSettingsRequest> __Marshaller_root_CommunityAppGetSettingsRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppGetSettingsRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppGetSettingsResponse> __Marshaller_root_CommunityAppGetSettingsResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppGetSettingsResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppInitializeRequest> __Marshaller_root_CommunityAppInitializeRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppInitializeRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppInitializeResponse> __Marshaller_root_CommunityAppInitializeResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppInitializeResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppListChannelGroupsRequest> __Marshaller_root_CommunityAppListChannelGroupsRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppListChannelGroupsRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppListChannelGroupsResponse> __Marshaller_root_CommunityAppListChannelGroupsResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppListChannelGroupsResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppListChannelGroupsForAddRequest> __Marshaller_root_CommunityAppListChannelGroupsForAddRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppListChannelGroupsForAddRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppListChannelGroupsForAddResponse> __Marshaller_root_CommunityAppListChannelGroupsForAddResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppListChannelGroupsForAddResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetDevSettingsRequest> __Marshaller_root_CommunityAppSetDevSettingsRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetDevSettingsRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAppSetDevSettingsResponse> __Marshaller_root_CommunityAppSetDevSettingsResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAppSetDevSettingsResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppGetRequest, CommunityAppGetResponse> __Method_Get = new Method<CommunityAppGetRequest, CommunityAppGetResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_CommunityAppGetRequest, __Marshaller_root_CommunityAppGetResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppListRequest, CommunityAppListResponse> __Method_List = new Method<CommunityAppListRequest, CommunityAppListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_CommunityAppListRequest, __Marshaller_root_CommunityAppListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppAddRequest, CommunityAppAddResponse> __Method_Add = new Method<CommunityAppAddRequest, CommunityAppAddResponse>(MethodType.Unary, __ServiceName, "Add", __Marshaller_root_CommunityAppAddRequest, __Marshaller_root_CommunityAppAddResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppRemoveRequest, CommunityAppRemoveResponse> __Method_Remove = new Method<CommunityAppRemoveRequest, CommunityAppRemoveResponse>(MethodType.Unary, __ServiceName, "Remove", __Marshaller_root_CommunityAppRemoveRequest, __Marshaller_root_CommunityAppRemoveResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppUpdateVersionRequest, CommunityAppUpdateVersionResponse> __Method_UpdateVersion = new Method<CommunityAppUpdateVersionRequest, CommunityAppUpdateVersionResponse>(MethodType.Unary, __ServiceName, "UpdateVersion", __Marshaller_root_CommunityAppUpdateVersionRequest, __Marshaller_root_CommunityAppUpdateVersionResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppSetSettingsRequest, CommunityAppSetSettingsResponse> __Method_SetSettings = new Method<CommunityAppSetSettingsRequest, CommunityAppSetSettingsResponse>(MethodType.Unary, __ServiceName, "SetSettings", __Marshaller_root_CommunityAppSetSettingsRequest, __Marshaller_root_CommunityAppSetSettingsResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppSetButtonRequest, CommunityAppSetButtonResponse> __Method_SetButton = new Method<CommunityAppSetButtonRequest, CommunityAppSetButtonResponse>(MethodType.Unary, __ServiceName, "SetButton", __Marshaller_root_CommunityAppSetButtonRequest, __Marshaller_root_CommunityAppSetButtonResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppSetAppOrganizationAccessRequest, CommunityAppSetAppOrganizationAccessResponse> __Method_SetAppOrganizationAccess = new Method<CommunityAppSetAppOrganizationAccessRequest, CommunityAppSetAppOrganizationAccessResponse>(MethodType.Unary, __ServiceName, "SetAppOrganizationAccess", __Marshaller_root_CommunityAppSetAppOrganizationAccessRequest, __Marshaller_root_CommunityAppSetAppOrganizationAccessResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppSetRatingRequest, CommunityAppSetRatingResponse> __Method_SetRating = new Method<CommunityAppSetRatingRequest, CommunityAppSetRatingResponse>(MethodType.Unary, __ServiceName, "SetRating", __Marshaller_root_CommunityAppSetRatingRequest, __Marshaller_root_CommunityAppSetRatingResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppGetSettingsRequest, CommunityAppGetSettingsResponse> __Method_GetSettings = new Method<CommunityAppGetSettingsRequest, CommunityAppGetSettingsResponse>(MethodType.Unary, __ServiceName, "GetSettings", __Marshaller_root_CommunityAppGetSettingsRequest, __Marshaller_root_CommunityAppGetSettingsResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppInitializeRequest, CommunityAppInitializeResponse> __Method_Initialize = new Method<CommunityAppInitializeRequest, CommunityAppInitializeResponse>(MethodType.Unary, __ServiceName, "Initialize", __Marshaller_root_CommunityAppInitializeRequest, __Marshaller_root_CommunityAppInitializeResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppListChannelGroupsRequest, CommunityAppListChannelGroupsResponse> __Method_ListChannelGroups = new Method<CommunityAppListChannelGroupsRequest, CommunityAppListChannelGroupsResponse>(MethodType.Unary, __ServiceName, "ListChannelGroups", __Marshaller_root_CommunityAppListChannelGroupsRequest, __Marshaller_root_CommunityAppListChannelGroupsResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppListChannelGroupsForAddRequest, CommunityAppListChannelGroupsForAddResponse> __Method_ListChannelGroupsForAdd = new Method<CommunityAppListChannelGroupsForAddRequest, CommunityAppListChannelGroupsForAddResponse>(MethodType.Unary, __ServiceName, "ListChannelGroupsForAdd", __Marshaller_root_CommunityAppListChannelGroupsForAddRequest, __Marshaller_root_CommunityAppListChannelGroupsForAddResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAppSetDevSettingsRequest, CommunityAppSetDevSettingsResponse> __Method_SetDevSettings = new Method<CommunityAppSetDevSettingsRequest, CommunityAppSetDevSettingsResponse>(MethodType.Unary, __ServiceName, "SetDevSettings", __Marshaller_root_CommunityAppSetDevSettingsRequest, __Marshaller_root_CommunityAppSetDevSettingsResponse);

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
