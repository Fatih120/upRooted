using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class WebRtcGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class WebRtcGrpcServiceClient : ClientBase<WebRtcGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public WebRtcGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public WebRtcGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected WebRtcGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcSetMuteAndDeafenResponse SetMuteAndDeafen(WebRtcSetMuteAndDeafenRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetMuteAndDeafen(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcSetMuteAndDeafenResponse SetMuteAndDeafen(WebRtcSetMuteAndDeafenRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetMuteAndDeafen, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcSetMuteAndDeafenResponse> SetMuteAndDeafenAsync(WebRtcSetMuteAndDeafenRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetMuteAndDeafenAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcSetMuteAndDeafenResponse> SetMuteAndDeafenAsync(WebRtcSetMuteAndDeafenRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetMuteAndDeafen, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcSetMuteAndDeafenOtherResponse SetMuteAndDeafenOther(WebRtcSetMuteAndDeafenOtherRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetMuteAndDeafenOther(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcSetMuteAndDeafenOtherResponse SetMuteAndDeafenOther(WebRtcSetMuteAndDeafenOtherRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetMuteAndDeafenOther, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcSetMuteAndDeafenOtherResponse> SetMuteAndDeafenOtherAsync(WebRtcSetMuteAndDeafenOtherRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetMuteAndDeafenOtherAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcSetMuteAndDeafenOtherResponse> SetMuteAndDeafenOtherAsync(WebRtcSetMuteAndDeafenOtherRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetMuteAndDeafenOther, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcGetIceInfoResponse GetIceInfo(WebRtcGetIceInfoRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetIceInfo(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcGetIceInfoResponse GetIceInfo(WebRtcGetIceInfoRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetIceInfo, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcGetIceInfoResponse> GetIceInfoAsync(WebRtcGetIceInfoRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetIceInfoAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcGetIceInfoResponse> GetIceInfoAsync(WebRtcGetIceInfoRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetIceInfo, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcSessionCreateResponse SessionCreate(WebRtcSessionCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SessionCreate(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcSessionCreateResponse SessionCreate(WebRtcSessionCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SessionCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcSessionCreateResponse> SessionCreateAsync(WebRtcSessionCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SessionCreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcSessionCreateResponse> SessionCreateAsync(WebRtcSessionCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SessionCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcRenegotiateResponse Renegotiate(WebRtcRenegotiateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Renegotiate(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcRenegotiateResponse Renegotiate(WebRtcRenegotiateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Renegotiate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcRenegotiateResponse> RenegotiateAsync(WebRtcRenegotiateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RenegotiateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcRenegotiateResponse> RenegotiateAsync(WebRtcRenegotiateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Renegotiate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcTracksCreateResponse TracksCreate(WebRtcTracksCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return TracksCreate(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcTracksCreateResponse TracksCreate(WebRtcTracksCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_TracksCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcTracksCreateResponse> TracksCreateAsync(WebRtcTracksCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return TracksCreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcTracksCreateResponse> TracksCreateAsync(WebRtcTracksCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_TracksCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcDataChannelsCreateResponse DataChannelsCreate(WebRtcDataChannelsCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DataChannelsCreate(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcDataChannelsCreateResponse DataChannelsCreate(WebRtcDataChannelsCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_DataChannelsCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcDataChannelsCreateResponse> DataChannelsCreateAsync(WebRtcDataChannelsCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DataChannelsCreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcDataChannelsCreateResponse> DataChannelsCreateAsync(WebRtcDataChannelsCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_DataChannelsCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcTracksCloseResponse TracksClose(WebRtcTracksCloseRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return TracksClose(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcTracksCloseResponse TracksClose(WebRtcTracksCloseRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_TracksClose, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcTracksCloseResponse> TracksCloseAsync(WebRtcTracksCloseRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return TracksCloseAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcTracksCloseResponse> TracksCloseAsync(WebRtcTracksCloseRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_TracksClose, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcDetachResponse Detach(WebRtcDetachRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Detach(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcDetachResponse Detach(WebRtcDetachRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Detach, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcDetachResponse> DetachAsync(WebRtcDetachRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DetachAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcDetachResponse> DetachAsync(WebRtcDetachRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Detach, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcKickResponse Kick(WebRtcKickRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Kick(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcKickResponse Kick(WebRtcKickRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Kick, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcKickResponse> KickAsync(WebRtcKickRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return KickAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcKickResponse> KickAsync(WebRtcKickRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Kick, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcListResponse List(WebRtcListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcListResponse List(WebRtcListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcListResponse> ListAsync(WebRtcListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcListResponse> ListAsync(WebRtcListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcListTracksForSessionResponse ListTracksForSession(WebRtcListTracksForSessionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListTracksForSession(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual WebRtcListTracksForSessionResponse ListTracksForSession(WebRtcListTracksForSessionRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ListTracksForSession, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcListTracksForSessionResponse> ListTracksForSessionAsync(WebRtcListTracksForSessionRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListTracksForSessionAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<WebRtcListTracksForSessionResponse> ListTracksForSessionAsync(WebRtcListTracksForSessionRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ListTracksForSession, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override WebRtcGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new WebRtcGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.WebRtcGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcSetMuteAndDeafenRequest> __Marshaller_root_WebRtcSetMuteAndDeafenRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcSetMuteAndDeafenRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcSetMuteAndDeafenResponse> __Marshaller_root_WebRtcSetMuteAndDeafenResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcSetMuteAndDeafenResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcSetMuteAndDeafenOtherRequest> __Marshaller_root_WebRtcSetMuteAndDeafenOtherRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcSetMuteAndDeafenOtherRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcSetMuteAndDeafenOtherResponse> __Marshaller_root_WebRtcSetMuteAndDeafenOtherResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcSetMuteAndDeafenOtherResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcGetIceInfoRequest> __Marshaller_root_WebRtcGetIceInfoRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcGetIceInfoRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcGetIceInfoResponse> __Marshaller_root_WebRtcGetIceInfoResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcGetIceInfoResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcSessionCreateRequest> __Marshaller_root_WebRtcSessionCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcSessionCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcSessionCreateResponse> __Marshaller_root_WebRtcSessionCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcSessionCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcRenegotiateRequest> __Marshaller_root_WebRtcRenegotiateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcRenegotiateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcRenegotiateResponse> __Marshaller_root_WebRtcRenegotiateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcRenegotiateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcTracksCreateRequest> __Marshaller_root_WebRtcTracksCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcTracksCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcTracksCreateResponse> __Marshaller_root_WebRtcTracksCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcTracksCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcDataChannelsCreateRequest> __Marshaller_root_WebRtcDataChannelsCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcDataChannelsCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcDataChannelsCreateResponse> __Marshaller_root_WebRtcDataChannelsCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcDataChannelsCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcTracksCloseRequest> __Marshaller_root_WebRtcTracksCloseRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcTracksCloseRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcTracksCloseResponse> __Marshaller_root_WebRtcTracksCloseResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcTracksCloseResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcDetachRequest> __Marshaller_root_WebRtcDetachRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcDetachRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcDetachResponse> __Marshaller_root_WebRtcDetachResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcDetachResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcKickRequest> __Marshaller_root_WebRtcKickRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcKickRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcKickResponse> __Marshaller_root_WebRtcKickResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcKickResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcListRequest> __Marshaller_root_WebRtcListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcListResponse> __Marshaller_root_WebRtcListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcListTracksForSessionRequest> __Marshaller_root_WebRtcListTracksForSessionRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcListTracksForSessionRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<WebRtcListTracksForSessionResponse> __Marshaller_root_WebRtcListTracksForSessionResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, WebRtcListTracksForSessionResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcSetMuteAndDeafenRequest, WebRtcSetMuteAndDeafenResponse> __Method_SetMuteAndDeafen = new Method<WebRtcSetMuteAndDeafenRequest, WebRtcSetMuteAndDeafenResponse>(MethodType.Unary, __ServiceName, "SetMuteAndDeafen", __Marshaller_root_WebRtcSetMuteAndDeafenRequest, __Marshaller_root_WebRtcSetMuteAndDeafenResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcSetMuteAndDeafenOtherRequest, WebRtcSetMuteAndDeafenOtherResponse> __Method_SetMuteAndDeafenOther = new Method<WebRtcSetMuteAndDeafenOtherRequest, WebRtcSetMuteAndDeafenOtherResponse>(MethodType.Unary, __ServiceName, "SetMuteAndDeafenOther", __Marshaller_root_WebRtcSetMuteAndDeafenOtherRequest, __Marshaller_root_WebRtcSetMuteAndDeafenOtherResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcGetIceInfoRequest, WebRtcGetIceInfoResponse> __Method_GetIceInfo = new Method<WebRtcGetIceInfoRequest, WebRtcGetIceInfoResponse>(MethodType.Unary, __ServiceName, "GetIceInfo", __Marshaller_root_WebRtcGetIceInfoRequest, __Marshaller_root_WebRtcGetIceInfoResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcSessionCreateRequest, WebRtcSessionCreateResponse> __Method_SessionCreate = new Method<WebRtcSessionCreateRequest, WebRtcSessionCreateResponse>(MethodType.Unary, __ServiceName, "SessionCreate", __Marshaller_root_WebRtcSessionCreateRequest, __Marshaller_root_WebRtcSessionCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcRenegotiateRequest, WebRtcRenegotiateResponse> __Method_Renegotiate = new Method<WebRtcRenegotiateRequest, WebRtcRenegotiateResponse>(MethodType.Unary, __ServiceName, "Renegotiate", __Marshaller_root_WebRtcRenegotiateRequest, __Marshaller_root_WebRtcRenegotiateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcTracksCreateRequest, WebRtcTracksCreateResponse> __Method_TracksCreate = new Method<WebRtcTracksCreateRequest, WebRtcTracksCreateResponse>(MethodType.Unary, __ServiceName, "TracksCreate", __Marshaller_root_WebRtcTracksCreateRequest, __Marshaller_root_WebRtcTracksCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcDataChannelsCreateRequest, WebRtcDataChannelsCreateResponse> __Method_DataChannelsCreate = new Method<WebRtcDataChannelsCreateRequest, WebRtcDataChannelsCreateResponse>(MethodType.Unary, __ServiceName, "DataChannelsCreate", __Marshaller_root_WebRtcDataChannelsCreateRequest, __Marshaller_root_WebRtcDataChannelsCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcTracksCloseRequest, WebRtcTracksCloseResponse> __Method_TracksClose = new Method<WebRtcTracksCloseRequest, WebRtcTracksCloseResponse>(MethodType.Unary, __ServiceName, "TracksClose", __Marshaller_root_WebRtcTracksCloseRequest, __Marshaller_root_WebRtcTracksCloseResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcDetachRequest, WebRtcDetachResponse> __Method_Detach = new Method<WebRtcDetachRequest, WebRtcDetachResponse>(MethodType.Unary, __ServiceName, "Detach", __Marshaller_root_WebRtcDetachRequest, __Marshaller_root_WebRtcDetachResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcKickRequest, WebRtcKickResponse> __Method_Kick = new Method<WebRtcKickRequest, WebRtcKickResponse>(MethodType.Unary, __ServiceName, "Kick", __Marshaller_root_WebRtcKickRequest, __Marshaller_root_WebRtcKickResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcListRequest, WebRtcListResponse> __Method_List = new Method<WebRtcListRequest, WebRtcListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_WebRtcListRequest, __Marshaller_root_WebRtcListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<WebRtcListTracksForSessionRequest, WebRtcListTracksForSessionResponse> __Method_ListTracksForSession = new Method<WebRtcListTracksForSessionRequest, WebRtcListTracksForSessionResponse>(MethodType.Unary, __ServiceName, "ListTracksForSession", __Marshaller_root_WebRtcListTracksForSessionRequest, __Marshaller_root_WebRtcListTracksForSessionResponse);

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
