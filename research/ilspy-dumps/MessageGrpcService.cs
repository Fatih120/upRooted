using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class MessageGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class MessageGrpcServiceClient : ClientBase<MessageGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public MessageGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public MessageGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected MessageGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageCreateResponse Create(MessageCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageCreateResponse Create(MessageCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageCreateResponse> CreateAsync(MessageCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageCreateResponse> CreateAsync(MessageCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageDeleteResponse Delete(MessageDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageDeleteResponse Delete(MessageDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageDeleteResponse> DeleteAsync(MessageDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageDeleteResponse> DeleteAsync(MessageDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageEditResponse Edit(MessageEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Edit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageEditResponse Edit(MessageEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageEditResponse> EditAsync(MessageEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return EditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageEditResponse> EditAsync(MessageEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageGetResponse Get(MessageGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageGetResponse Get(MessageGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageGetResponse> GetAsync(MessageGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageGetResponse> GetAsync(MessageGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageListResponse List(MessageListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageListResponse List(MessageListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageListResponse> ListAsync(MessageListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageListResponse> ListAsync(MessageListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageFlagResponse Flag(MessageFlagRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Flag(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageFlagResponse Flag(MessageFlagRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Flag, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageFlagResponse> FlagAsync(MessageFlagRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return FlagAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageFlagResponse> FlagAsync(MessageFlagRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Flag, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessagePinCreateResponse PinCreate(MessagePinCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PinCreate(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessagePinCreateResponse PinCreate(MessagePinCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_PinCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessagePinCreateResponse> PinCreateAsync(MessagePinCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PinCreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessagePinCreateResponse> PinCreateAsync(MessagePinCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_PinCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessagePinDeleteResponse PinDelete(MessagePinDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PinDelete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessagePinDeleteResponse PinDelete(MessagePinDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_PinDelete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessagePinDeleteResponse> PinDeleteAsync(MessagePinDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PinDeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessagePinDeleteResponse> PinDeleteAsync(MessagePinDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_PinDelete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessagePinListResponse PinList(MessagePinListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PinList(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessagePinListResponse PinList(MessagePinListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_PinList, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessagePinListResponse> PinListAsync(MessagePinListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PinListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessagePinListResponse> PinListAsync(MessagePinListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_PinList, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageReactionCreateResponse ReactionCreate(MessageReactionCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ReactionCreate(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageReactionCreateResponse ReactionCreate(MessageReactionCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ReactionCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageReactionCreateResponse> ReactionCreateAsync(MessageReactionCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ReactionCreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageReactionCreateResponse> ReactionCreateAsync(MessageReactionCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ReactionCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageReactionDeleteResponse ReactionDelete(MessageReactionDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ReactionDelete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageReactionDeleteResponse ReactionDelete(MessageReactionDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ReactionDelete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageReactionDeleteResponse> ReactionDeleteAsync(MessageReactionDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ReactionDeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageReactionDeleteResponse> ReactionDeleteAsync(MessageReactionDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ReactionDelete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageSearchResponse Search(MessageSearchRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Search(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageSearchResponse Search(MessageSearchRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Search, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageSearchResponse> SearchAsync(MessageSearchRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SearchAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageSearchResponse> SearchAsync(MessageSearchRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Search, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageSearchContainersResponse SearchContainer(MessageSearchContainersRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SearchContainer(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageSearchContainersResponse SearchContainer(MessageSearchContainersRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SearchContainer, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageSearchContainersResponse> SearchContainerAsync(MessageSearchContainersRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SearchContainerAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageSearchContainersResponse> SearchContainerAsync(MessageSearchContainersRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SearchContainer, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageSetTypingIndicatorResponse SetTypingIndicator(MessageSetTypingIndicatorRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetTypingIndicator(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageSetTypingIndicatorResponse SetTypingIndicator(MessageSetTypingIndicatorRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetTypingIndicator, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageSetTypingIndicatorResponse> SetTypingIndicatorAsync(MessageSetTypingIndicatorRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetTypingIndicatorAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageSetTypingIndicatorResponse> SetTypingIndicatorAsync(MessageSetTypingIndicatorRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetTypingIndicator, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageSetViewTimeResponse SetViewTime(MessageSetViewTimeRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetViewTime(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual MessageSetViewTimeResponse SetViewTime(MessageSetViewTimeRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetViewTime, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageSetViewTimeResponse> SetViewTimeAsync(MessageSetViewTimeRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetViewTimeAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<MessageSetViewTimeResponse> SetViewTimeAsync(MessageSetViewTimeRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetViewTime, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override MessageGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new MessageGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.v2.MessageGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageCreateRequest> __Marshaller_root_MessageCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageCreateResponse> __Marshaller_root_MessageCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageDeleteRequest> __Marshaller_root_MessageDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageDeleteResponse> __Marshaller_root_MessageDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageEditRequest> __Marshaller_root_MessageEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageEditResponse> __Marshaller_root_MessageEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageGetRequest> __Marshaller_root_MessageGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageGetResponse> __Marshaller_root_MessageGetResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageGetResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageListRequest> __Marshaller_root_MessageListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageListResponse> __Marshaller_root_MessageListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageFlagRequest> __Marshaller_root_MessageFlagRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageFlagRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageFlagResponse> __Marshaller_root_MessageFlagResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageFlagResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessagePinCreateRequest> __Marshaller_root_MessagePinCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessagePinCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessagePinCreateResponse> __Marshaller_root_MessagePinCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessagePinCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessagePinDeleteRequest> __Marshaller_root_MessagePinDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessagePinDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessagePinDeleteResponse> __Marshaller_root_MessagePinDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessagePinDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessagePinListRequest> __Marshaller_root_MessagePinListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessagePinListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessagePinListResponse> __Marshaller_root_MessagePinListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessagePinListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageReactionCreateRequest> __Marshaller_root_MessageReactionCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageReactionCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageReactionCreateResponse> __Marshaller_root_MessageReactionCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageReactionCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageReactionDeleteRequest> __Marshaller_root_MessageReactionDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageReactionDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageReactionDeleteResponse> __Marshaller_root_MessageReactionDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageReactionDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageSearchRequest> __Marshaller_root_MessageSearchRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageSearchRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageSearchResponse> __Marshaller_root_MessageSearchResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageSearchResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageSearchContainersRequest> __Marshaller_root_MessageSearchContainersRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageSearchContainersRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageSearchContainersResponse> __Marshaller_root_MessageSearchContainersResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageSearchContainersResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageSetTypingIndicatorRequest> __Marshaller_root_MessageSetTypingIndicatorRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageSetTypingIndicatorRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageSetTypingIndicatorResponse> __Marshaller_root_MessageSetTypingIndicatorResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageSetTypingIndicatorResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageSetViewTimeRequest> __Marshaller_root_MessageSetViewTimeRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageSetViewTimeRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<MessageSetViewTimeResponse> __Marshaller_root_MessageSetViewTimeResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, MessageSetViewTimeResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageCreateRequest, MessageCreateResponse> __Method_Create = new Method<MessageCreateRequest, MessageCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_MessageCreateRequest, __Marshaller_root_MessageCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageDeleteRequest, MessageDeleteResponse> __Method_Delete = new Method<MessageDeleteRequest, MessageDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_MessageDeleteRequest, __Marshaller_root_MessageDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageEditRequest, MessageEditResponse> __Method_Edit = new Method<MessageEditRequest, MessageEditResponse>(MethodType.Unary, __ServiceName, "Edit", __Marshaller_root_MessageEditRequest, __Marshaller_root_MessageEditResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageGetRequest, MessageGetResponse> __Method_Get = new Method<MessageGetRequest, MessageGetResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_MessageGetRequest, __Marshaller_root_MessageGetResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageListRequest, MessageListResponse> __Method_List = new Method<MessageListRequest, MessageListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_MessageListRequest, __Marshaller_root_MessageListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageFlagRequest, MessageFlagResponse> __Method_Flag = new Method<MessageFlagRequest, MessageFlagResponse>(MethodType.Unary, __ServiceName, "Flag", __Marshaller_root_MessageFlagRequest, __Marshaller_root_MessageFlagResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessagePinCreateRequest, MessagePinCreateResponse> __Method_PinCreate = new Method<MessagePinCreateRequest, MessagePinCreateResponse>(MethodType.Unary, __ServiceName, "PinCreate", __Marshaller_root_MessagePinCreateRequest, __Marshaller_root_MessagePinCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessagePinDeleteRequest, MessagePinDeleteResponse> __Method_PinDelete = new Method<MessagePinDeleteRequest, MessagePinDeleteResponse>(MethodType.Unary, __ServiceName, "PinDelete", __Marshaller_root_MessagePinDeleteRequest, __Marshaller_root_MessagePinDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessagePinListRequest, MessagePinListResponse> __Method_PinList = new Method<MessagePinListRequest, MessagePinListResponse>(MethodType.Unary, __ServiceName, "PinList", __Marshaller_root_MessagePinListRequest, __Marshaller_root_MessagePinListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageReactionCreateRequest, MessageReactionCreateResponse> __Method_ReactionCreate = new Method<MessageReactionCreateRequest, MessageReactionCreateResponse>(MethodType.Unary, __ServiceName, "ReactionCreate", __Marshaller_root_MessageReactionCreateRequest, __Marshaller_root_MessageReactionCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageReactionDeleteRequest, MessageReactionDeleteResponse> __Method_ReactionDelete = new Method<MessageReactionDeleteRequest, MessageReactionDeleteResponse>(MethodType.Unary, __ServiceName, "ReactionDelete", __Marshaller_root_MessageReactionDeleteRequest, __Marshaller_root_MessageReactionDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageSearchRequest, MessageSearchResponse> __Method_Search = new Method<MessageSearchRequest, MessageSearchResponse>(MethodType.Unary, __ServiceName, "Search", __Marshaller_root_MessageSearchRequest, __Marshaller_root_MessageSearchResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageSearchContainersRequest, MessageSearchContainersResponse> __Method_SearchContainer = new Method<MessageSearchContainersRequest, MessageSearchContainersResponse>(MethodType.Unary, __ServiceName, "SearchContainer", __Marshaller_root_MessageSearchContainersRequest, __Marshaller_root_MessageSearchContainersResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageSetTypingIndicatorRequest, MessageSetTypingIndicatorResponse> __Method_SetTypingIndicator = new Method<MessageSetTypingIndicatorRequest, MessageSetTypingIndicatorResponse>(MethodType.Unary, __ServiceName, "SetTypingIndicator", __Marshaller_root_MessageSetTypingIndicatorRequest, __Marshaller_root_MessageSetTypingIndicatorResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<MessageSetViewTimeRequest, MessageSetViewTimeResponse> __Method_SetViewTime = new Method<MessageSetViewTimeRequest, MessageSetViewTimeResponse>(MethodType.Unary, __ServiceName, "SetViewTime", __Marshaller_root_MessageSetViewTimeRequest, __Marshaller_root_MessageSetViewTimeResponse);

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
