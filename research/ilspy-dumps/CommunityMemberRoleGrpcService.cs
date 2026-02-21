using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class CommunityMemberRoleGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class CommunityMemberRoleGrpcServiceClient : ClientBase<CommunityMemberRoleGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityMemberRoleGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public CommunityMemberRoleGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected CommunityMemberRoleGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberRoleAddResponse Add(CommunityMemberRoleAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Add(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberRoleAddResponse Add(CommunityMemberRoleAddRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Add, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberRoleAddResponse> AddAsync(CommunityMemberRoleAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AddAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberRoleAddResponse> AddAsync(CommunityMemberRoleAddRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Add, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberRoleListResponse List(CommunityMemberRoleListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberRoleListResponse List(CommunityMemberRoleListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberRoleListResponse> ListAsync(CommunityMemberRoleListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberRoleListResponse> ListAsync(CommunityMemberRoleListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberRoleRemoveResponse Remove(CommunityMemberRoleRemoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Remove(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberRoleRemoveResponse Remove(CommunityMemberRoleRemoveRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Remove, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberRoleRemoveResponse> RemoveAsync(CommunityMemberRoleRemoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RemoveAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberRoleRemoveResponse> RemoveAsync(CommunityMemberRoleRemoveRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Remove, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberRoleSetPrimaryResponse SetPrimary(CommunityMemberRoleSetPrimaryRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetPrimary(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual CommunityMemberRoleSetPrimaryResponse SetPrimary(CommunityMemberRoleSetPrimaryRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetPrimary, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberRoleSetPrimaryResponse> SetPrimaryAsync(CommunityMemberRoleSetPrimaryRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetPrimaryAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<CommunityMemberRoleSetPrimaryResponse> SetPrimaryAsync(CommunityMemberRoleSetPrimaryRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetPrimary, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override CommunityMemberRoleGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new CommunityMemberRoleGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.CommunityMemberRoleGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberRoleAddRequest> __Marshaller_root_CommunityMemberRoleAddRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberRoleAddRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberRoleAddResponse> __Marshaller_root_CommunityMemberRoleAddResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberRoleAddResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberRoleListRequest> __Marshaller_root_CommunityMemberRoleListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberRoleListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberRoleListResponse> __Marshaller_root_CommunityMemberRoleListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberRoleListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberRoleRemoveRequest> __Marshaller_root_CommunityMemberRoleRemoveRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberRoleRemoveRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberRoleRemoveResponse> __Marshaller_root_CommunityMemberRoleRemoveResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberRoleRemoveResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberRoleSetPrimaryRequest> __Marshaller_root_CommunityMemberRoleSetPrimaryRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberRoleSetPrimaryRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<CommunityMemberRoleSetPrimaryResponse> __Marshaller_root_CommunityMemberRoleSetPrimaryResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, CommunityMemberRoleSetPrimaryResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberRoleAddRequest, CommunityMemberRoleAddResponse> __Method_Add = new Method<CommunityMemberRoleAddRequest, CommunityMemberRoleAddResponse>(MethodType.Unary, __ServiceName, "Add", __Marshaller_root_CommunityMemberRoleAddRequest, __Marshaller_root_CommunityMemberRoleAddResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberRoleListRequest, CommunityMemberRoleListResponse> __Method_List = new Method<CommunityMemberRoleListRequest, CommunityMemberRoleListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_CommunityMemberRoleListRequest, __Marshaller_root_CommunityMemberRoleListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberRoleRemoveRequest, CommunityMemberRoleRemoveResponse> __Method_Remove = new Method<CommunityMemberRoleRemoveRequest, CommunityMemberRoleRemoveResponse>(MethodType.Unary, __ServiceName, "Remove", __Marshaller_root_CommunityMemberRoleRemoveRequest, __Marshaller_root_CommunityMemberRoleRemoveResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<CommunityMemberRoleSetPrimaryRequest, CommunityMemberRoleSetPrimaryResponse> __Method_SetPrimary = new Method<CommunityMemberRoleSetPrimaryRequest, CommunityMemberRoleSetPrimaryResponse>(MethodType.Unary, __ServiceName, "SetPrimary", __Marshaller_root_CommunityMemberRoleSetPrimaryRequest, __Marshaller_root_CommunityMemberRoleSetPrimaryResponse);

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
