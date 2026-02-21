using Google.Protobuf.Reflection;

namespace RootApp.App.Messaging.Grpc;

public enum AppRpcMessageType
{
	[OriginalName("APP_RPC_MESSAGE_TYPE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("APP_RPC_MESSAGE_TYPE_APP_MESSAGE")]
	AppMessage,
	[OriginalName("APP_RPC_MESSAGE_TYPE_CLIENT_ATTACH")]
	ClientAttach,
	[OriginalName("APP_RPC_MESSAGE_TYPE_CLIENT_DETACH")]
	ClientDetach,
	[OriginalName("APP_RPC_MESSAGE_TYPE_PING")]
	Ping,
	[OriginalName("APP_RPC_MESSAGE_TYPE_APP_MESSAGE_EXCEPTION")]
	AppMessageException,
	[OriginalName("APP_RPC_MESSAGE_TYPE_HUB_DISCONNECTING")]
	HubDisconnecting,
	[OriginalName("APP_RPC_MESSAGE_TYPE_COMMUNITY_ADD")]
	CommunityAdd,
	[OriginalName("APP_RPC_MESSAGE_TYPE_COMMUNITY_DELETE")]
	CommunityDelete,
	[OriginalName("APP_RPC_MESSAGE_TYPE_SEQUENCE_REQUEST")]
	SequenceRequest
}
