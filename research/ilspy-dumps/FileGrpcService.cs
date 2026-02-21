using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class FileGrpcService
{
	public class FileGrpcServiceClient : ClientBase<FileGrpcServiceClient>
	{
		public static FileGrpcServiceClient Create(GrpcChannel channel)
		{
			return new FileGrpcServiceClient(channel);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public FileGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public FileGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected FileGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileCreateResponse Create(FileCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Create(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileCreateResponse Create(FileCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileCreateResponse> CreateAsync(FileCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileCreateResponse> CreateAsync(FileCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileEditResponse Edit(FileEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Edit(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileEditResponse Edit(FileEditRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileEditResponse> EditAsync(FileEditRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return EditAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileEditResponse> EditAsync(FileEditRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Edit, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileMoveResponse Move(FileMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Move(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileMoveResponse Move(FileMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileMoveResponse> MoveAsync(FileMoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return MoveAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileMoveResponse> MoveAsync(FileMoveRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Move, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileSearchResponse Search(FileSearchRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Search(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileSearchResponse Search(FileSearchRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Search, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileSearchResponse> SearchAsync(FileSearchRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SearchAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileSearchResponse> SearchAsync(FileSearchRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Search, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileSearchCommunityResponse SearchCommunity(FileSearchCommunityRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SearchCommunity(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileSearchCommunityResponse SearchCommunity(FileSearchCommunityRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SearchCommunity, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileSearchCommunityResponse> SearchCommunityAsync(FileSearchCommunityRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SearchCommunityAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileSearchCommunityResponse> SearchCommunityAsync(FileSearchCommunityRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SearchCommunity, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileDeleteResponse Delete(FileDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileDeleteResponse Delete(FileDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileDeleteResponse> DeleteAsync(FileDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileDeleteResponse> DeleteAsync(FileDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileListResponse List(FileListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return List(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileListResponse List(FileListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileListResponse> ListAsync(FileListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileListResponse> ListAsync(FileListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_List, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileResponse Get(FileGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Get(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileResponse Get(FileGetRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileResponse> GetAsync(FileGetRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileResponse> GetAsync(FileGetRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileDownloadResponse Download(FileDownloadRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Download(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual FileDownloadResponse Download(FileDownloadRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Download, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileDownloadResponse> DownloadAsync(FileDownloadRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DownloadAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<FileDownloadResponse> DownloadAsync(FileDownloadRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Download, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override FileGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new FileGrpcServiceClient(P_0);
		}
	}

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	private static readonly string __ServiceName = "root.FileGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileCreateRequest> __Marshaller_root_FileCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileCreateResponse> __Marshaller_root_FileCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileEditRequest> __Marshaller_root_FileEditRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileEditRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileEditResponse> __Marshaller_root_FileEditResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileEditResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileMoveRequest> __Marshaller_root_FileMoveRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileMoveRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileMoveResponse> __Marshaller_root_FileMoveResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileMoveResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileSearchRequest> __Marshaller_root_FileSearchRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileSearchRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileSearchResponse> __Marshaller_root_FileSearchResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileSearchResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileSearchCommunityRequest> __Marshaller_root_FileSearchCommunityRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileSearchCommunityRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileSearchCommunityResponse> __Marshaller_root_FileSearchCommunityResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileSearchCommunityResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileDeleteRequest> __Marshaller_root_FileDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileDeleteResponse> __Marshaller_root_FileDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileListRequest> __Marshaller_root_FileListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileListResponse> __Marshaller_root_FileListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileGetRequest> __Marshaller_root_FileGetRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileGetRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileResponse> __Marshaller_root_FileResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileDownloadRequest> __Marshaller_root_FileDownloadRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileDownloadRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<FileDownloadResponse> __Marshaller_root_FileDownloadResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, FileDownloadResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FileCreateRequest, FileCreateResponse> __Method_Create = new Method<FileCreateRequest, FileCreateResponse>(MethodType.Unary, __ServiceName, "Create", __Marshaller_root_FileCreateRequest, __Marshaller_root_FileCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FileEditRequest, FileEditResponse> __Method_Edit = new Method<FileEditRequest, FileEditResponse>(MethodType.Unary, __ServiceName, "Edit", __Marshaller_root_FileEditRequest, __Marshaller_root_FileEditResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FileMoveRequest, FileMoveResponse> __Method_Move = new Method<FileMoveRequest, FileMoveResponse>(MethodType.Unary, __ServiceName, "Move", __Marshaller_root_FileMoveRequest, __Marshaller_root_FileMoveResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FileSearchRequest, FileSearchResponse> __Method_Search = new Method<FileSearchRequest, FileSearchResponse>(MethodType.Unary, __ServiceName, "Search", __Marshaller_root_FileSearchRequest, __Marshaller_root_FileSearchResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FileSearchCommunityRequest, FileSearchCommunityResponse> __Method_SearchCommunity = new Method<FileSearchCommunityRequest, FileSearchCommunityResponse>(MethodType.Unary, __ServiceName, "SearchCommunity", __Marshaller_root_FileSearchCommunityRequest, __Marshaller_root_FileSearchCommunityResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FileDeleteRequest, FileDeleteResponse> __Method_Delete = new Method<FileDeleteRequest, FileDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_FileDeleteRequest, __Marshaller_root_FileDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FileListRequest, FileListResponse> __Method_List = new Method<FileListRequest, FileListResponse>(MethodType.Unary, __ServiceName, "List", __Marshaller_root_FileListRequest, __Marshaller_root_FileListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FileGetRequest, FileResponse> __Method_Get = new Method<FileGetRequest, FileResponse>(MethodType.Unary, __ServiceName, "Get", __Marshaller_root_FileGetRequest, __Marshaller_root_FileResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<FileDownloadRequest, FileDownloadResponse> __Method_Download = new Method<FileDownloadRequest, FileDownloadResponse>(MethodType.Unary, __ServiceName, "Download", __Marshaller_root_FileDownloadRequest, __Marshaller_root_FileDownloadResponse);

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
