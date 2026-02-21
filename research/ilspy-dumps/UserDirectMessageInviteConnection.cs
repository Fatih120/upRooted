using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum UserDirectMessageInviteConnection
{
	[OriginalName("USER_DIRECT_MESSAGE_INVITE_CONNECTION_UNSPECIFIED")]
	Unspecified,
	[OriginalName("USER_DIRECT_MESSAGE_INVITE_CONNECTION_ANY")]
	Any,
	[OriginalName("USER_DIRECT_MESSAGE_INVITE_CONNECTION_CONNECTED")]
	Connected,
	[OriginalName("USER_DIRECT_MESSAGE_INVITE_CONNECTION_NONE")]
	None,
	[OriginalName("USER_DIRECT_MESSAGE_INVITE_CONNECTION_FRIEND")]
	Friend
}
