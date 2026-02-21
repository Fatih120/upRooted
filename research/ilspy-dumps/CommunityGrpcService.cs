using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class CommunityGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class CommunityGrpcServiceClient : ClientBase<CommunityGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected CommunityGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberListResponse ListMine(CommunityListMineRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListMine(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberListResponse ListMine(CommunityListMineRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListMine, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberListResponse> ListMineAsync(CommunityListMineRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListMineAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberListResponse> ListMineAsync(CommunityListMineRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListMine, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityGetExtendedResponse GetExtended(CommunityGetExtendedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetExtended(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityGetExtendedResponse GetExtended(CommunityGetExtendedRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetExtended, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityGetExtendedResponse> GetExtendedAsync(CommunityGetExtendedRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetExtendedAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityGetExtendedResponse> GetExtendedAsync(CommunityGetExtendedRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetExtended, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityGetForAppResponse GetForApp(CommunityGetForAppRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetForApp(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityGetForAppResponse GetForApp(CommunityGetForAppRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetForApp, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityGetForAppResponse> GetForAppAsync(CommunityGetForAppRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetForAppAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityGetForAppResponse> GetForAppAsync(CommunityGetForAppRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetForApp, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityGetResponse Get(CommunityGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityGetResponse Get(CommunityGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityGetResponse> GetAsync(CommunityGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityGetResponse> GetAsync(CommunityGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAttachResponse Attach(CommunityAttachRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Attach(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityAttachResponse Attach(CommunityAttachRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Attach, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAttachResponse> AttachAsync(CommunityAttachRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AttachAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityAttachResponse> AttachAsync(CommunityAttachRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Attach, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityDetachResponse Detach(CommunityDetachRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Detach(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityDetachResponse Detach(CommunityDetachRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Detach, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityDetachResponse> DetachAsync(CommunityDetachRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DetachAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityDetachResponse> DetachAsync(CommunityDetachRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Detach, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityLeaveResponse Leave(CommunityLeaveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Leave(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityLeaveResponse Leave(CommunityLeaveRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Leave, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityLeaveResponse> LeaveAsync(CommunityLeaveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return LeaveAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityLeaveResponse> LeaveAsync(CommunityLeaveRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Leave, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityCreateResponse Create(CommunityCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityCreateResponse Create(CommunityCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityCreateResponse> CreateAsync(CommunityCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityCreateResponse> CreateAsync(CommunityCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityEditResponse Edit(CommunityEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Edit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityEditResponse Edit(CommunityEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityEditResponse> EditAsync(CommunityEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return EditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityEditResponse> EditAsync(CommunityEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityDeleteResponse Delete(CommunityDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityDeleteResponse Delete(CommunityDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityDeleteResponse> DeleteAsync(CommunityDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityDeleteResponse> DeleteAsync(CommunityDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityOwnerEditResponse OwnerEdit(CommunityOwnerEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return OwnerEdit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityOwnerEditResponse OwnerEdit(CommunityOwnerEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_OwnerEdit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityOwnerEditResponse> OwnerEditAsync(CommunityOwnerEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return OwnerEditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityOwnerEditResponse> OwnerEditAsync(CommunityOwnerEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_OwnerEdit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override CommunityGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new CommunityGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.CommunityGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityListMineRequest> __Marshaller_root_CommunityListMineRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityListMineRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberListResponse> __Marshaller_root_CommunityMemberListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityGetExtendedRequest> __Marshaller_root_CommunityGetExtendedRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityGetExtendedRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityGetExtendedResponse> __Marshaller_root_CommunityGetExtendedResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityGetExtendedResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityGetForAppRequest> __Marshaller_root_CommunityGetForAppRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityGetForAppRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityGetForAppResponse> __Marshaller_root_CommunityGetForAppResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityGetForAppResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityGetRequest> __Marshaller_root_CommunityGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityGetResponse> __Marshaller_root_CommunityGetResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityGetResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAttachRequest> __Marshaller_root_CommunityAttachRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAttachRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityAttachResponse> __Marshaller_root_CommunityAttachResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityAttachResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityDetachRequest> __Marshaller_root_CommunityDetachRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityDetachRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityDetachResponse> __Marshaller_root_CommunityDetachResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityDetachResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityLeaveRequest> __Marshaller_root_CommunityLeaveRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityLeaveRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityLeaveResponse> __Marshaller_root_CommunityLeaveResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityLeaveResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityCreateRequest> __Marshaller_root_CommunityCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityCreateResponse> __Marshaller_root_CommunityCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityEditRequest> __Marshaller_root_CommunityEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityEditResponse> __Marshaller_root_CommunityEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityDeleteRequest> __Marshaller_root_CommunityDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityDeleteResponse> __Marshaller_root_CommunityDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityOwnerEditRequest> __Marshaller_root_CommunityOwnerEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityOwnerEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityOwnerEditResponse> __Marshaller_root_CommunityOwnerEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityOwnerEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityListMineRequest, CommunityMemberListResponse> __Method_ListMine = new Method<CommunityListMineRequest, CommunityMemberListResponse>(MethodType.Unary, __ServiceName, "ListMine", __Marshaller_root_CommunityListMineRequest, __Marshaller_root_CommunityMemberListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityGetExtendedRequest, CommunityGetExtendedResponse> __Method_GetExtended = new Method<CommunityGetExtendedRequest, CommunityGetExtendedResponse>(MethodType.Unary, __ServiceName, "GetExtended", __Marshaller_root_CommunityGetExtendedRequest, __Marshaller_root_CommunityGetExtendedResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityGetForAppRequest, CommunityGetForAppResponse> __Method_GetForApp = new Method<CommunityGetForAppRequest, CommunityGetForAppResponse>(MethodType.Unary, __ServiceName, "GetForApp", __Marshaller_root_CommunityGetForAppRequest, __Marshaller_root_CommunityGetForAppResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityGetRequest, CommunityGetResponse> __Method_Get = new Method<CommunityGetRequest, CommunityGetResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_CommunityGetRequest, __Marshaller_root_CommunityGetResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityAttachRequest, CommunityAttachResponse> __Method_Attach = new Method<CommunityAttachRequest, CommunityAttachResponse>(MethodType.Unary, __ServiceName, "Attach", __Marshaller_root_CommunityAttachRequest, __Marshaller_root_CommunityAttachResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityDetachRequest, CommunityDetachResponse> __Method_Detach = new Method<CommunityDetachRequest, CommunityDetachResponse>(MethodType.Unary, __ServiceName, "Detach", __Marshaller_root_CommunityDetachRequest, __Marshaller_root_CommunityDetachResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityLeaveRequest, CommunityLeaveResponse> __Method_Leave = new Method<CommunityLeaveRequest, CommunityLeaveResponse>(MethodType.Unary, __ServiceName, "Leave", __Marshaller_root_CommunityLeaveRequest, __Marshaller_root_CommunityLeaveResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityCreateRequest, CommunityCreateResponse> __Method_Create = new Method<CommunityCreateRequest, CommunityCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_CommunityCreateRequest, __Marshaller_root_CommunityCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityEditRequest, CommunityEditResponse> __Method_Edit = new Method<CommunityEditRequest, CommunityEditResponse>(MethodType.Unary, __ServiceName, "Edit", __Marshaller_root_CommunityEditRequest, __Marshaller_root_CommunityEditResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityDeleteRequest, CommunityDeleteResponse> __Method_Delete = new Method<CommunityDeleteRequest, CommunityDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_CommunityDeleteRequest, __Marshaller_root_CommunityDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityOwnerEditRequest, CommunityOwnerEditResponse> __Method_OwnerEdit = new Method<CommunityOwnerEditRequest, CommunityOwnerEditResponse>(MethodType.Unary, __ServiceName, "OwnerEdit", __Marshaller_root_CommunityOwnerEditRequest, __Marshaller_root_CommunityOwnerEditResponse);

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
