using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class AccessRuleGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class AccessRuleGrpcServiceClient : ClientBase<AccessRuleGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public AccessRuleGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public AccessRuleGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected AccessRuleGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleCreateResponse Create(AccessRuleCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleCreateResponse Create(AccessRuleCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleCreateResponse> CreateAsync(AccessRuleCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleCreateResponse> CreateAsync(AccessRuleCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleEditResponse Edit(AccessRuleEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Edit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleEditResponse Edit(AccessRuleEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleEditResponse> EditAsync(AccessRuleEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return EditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleEditResponse> EditAsync(AccessRuleEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleUpdateResponse Update(AccessRuleUpdateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Update(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleUpdateResponse Update(AccessRuleUpdateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Update, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleUpdateResponse> UpdateAsync(AccessRuleUpdateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return UpdateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleUpdateResponse> UpdateAsync(AccessRuleUpdateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Update, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleListResponse ListByChannelOrChannelGroup(AccessRuleListByChannelOrChannelGroupRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListByChannelOrChannelGroup(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleListResponse ListByChannelOrChannelGroup(AccessRuleListByChannelOrChannelGroupRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListByChannelOrChannelGroup, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleListResponse> ListByChannelOrChannelGroupAsync(AccessRuleListByChannelOrChannelGroupRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListByChannelOrChannelGroupAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleListResponse> ListByChannelOrChannelGroupAsync(AccessRuleListByChannelOrChannelGroupRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListByChannelOrChannelGroup, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleListResponse ListByRoleOrMember(AccessRuleListByRoleOrMemberRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListByRoleOrMember(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleListResponse ListByRoleOrMember(AccessRuleListByRoleOrMemberRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListByRoleOrMember, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleListResponse> ListByRoleOrMemberAsync(AccessRuleListByRoleOrMemberRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListByRoleOrMemberAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleListResponse> ListByRoleOrMemberAsync(AccessRuleListByRoleOrMemberRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListByRoleOrMember, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleResponse Get(AccessRuleGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleResponse Get(AccessRuleGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleResponse> GetAsync(AccessRuleGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleResponse> GetAsync(AccessRuleGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleDeleteResponse Delete(AccessRuleDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AccessRuleDeleteResponse Delete(AccessRuleDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleDeleteResponse> DeleteAsync(AccessRuleDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<AccessRuleDeleteResponse> DeleteAsync(AccessRuleDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override AccessRuleGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new AccessRuleGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.AccessRuleGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleCreateRequest> __Marshaller_root_AccessRuleCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleCreateResponse> __Marshaller_root_AccessRuleCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleEditRequest> __Marshaller_root_AccessRuleEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleEditResponse> __Marshaller_root_AccessRuleEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleUpdateRequest> __Marshaller_root_AccessRuleUpdateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleUpdateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleUpdateResponse> __Marshaller_root_AccessRuleUpdateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleUpdateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleListByChannelOrChannelGroupRequest> __Marshaller_root_AccessRuleListByChannelOrChannelGroupRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleListByChannelOrChannelGroupRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleListResponse> __Marshaller_root_AccessRuleListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleListByRoleOrMemberRequest> __Marshaller_root_AccessRuleListByRoleOrMemberRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleListByRoleOrMemberRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleGetRequest> __Marshaller_root_AccessRuleGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleResponse> __Marshaller_root_AccessRuleResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleDeleteRequest> __Marshaller_root_AccessRuleDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<AccessRuleDeleteResponse> __Marshaller_root_AccessRuleDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, AccessRuleDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AccessRuleCreateRequest, AccessRuleCreateResponse> __Method_Create = new Method<AccessRuleCreateRequest, AccessRuleCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_AccessRuleCreateRequest, __Marshaller_root_AccessRuleCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AccessRuleEditRequest, AccessRuleEditResponse> __Method_Edit = new Method<AccessRuleEditRequest, AccessRuleEditResponse>(MethodType.Unary, __ServiceName, "Edit", __Marshaller_root_AccessRuleEditRequest, __Marshaller_root_AccessRuleEditResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AccessRuleUpdateRequest, AccessRuleUpdateResponse> __Method_Update = new Method<AccessRuleUpdateRequest, AccessRuleUpdateResponse>(MethodType.Unary, __ServiceName, "Update", __Marshaller_root_AccessRuleUpdateRequest, __Marshaller_root_AccessRuleUpdateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AccessRuleListByChannelOrChannelGroupRequest, AccessRuleListResponse> __Method_ListByChannelOrChannelGroup = new Method<AccessRuleListByChannelOrChannelGroupRequest, AccessRuleListResponse>(MethodType.Unary, __ServiceName, "ListByChannelOrChannelGroup", __Marshaller_root_AccessRuleListByChannelOrChannelGroupRequest, __Marshaller_root_AccessRuleListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AccessRuleListByRoleOrMemberRequest, AccessRuleListResponse> __Method_ListByRoleOrMember = new Method<AccessRuleListByRoleOrMemberRequest, AccessRuleListResponse>(MethodType.Unary, __ServiceName, "ListByRoleOrMember", __Marshaller_root_AccessRuleListByRoleOrMemberRequest, __Marshaller_root_AccessRuleListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AccessRuleGetRequest, AccessRuleResponse> __Method_Get = new Method<AccessRuleGetRequest, AccessRuleResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_AccessRuleGetRequest, __Marshaller_root_AccessRuleResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<AccessRuleDeleteRequest, AccessRuleDeleteResponse> __Method_Delete = new Method<AccessRuleDeleteRequest, AccessRuleDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_AccessRuleDeleteRequest, __Marshaller_root_AccessRuleDeleteResponse);

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
