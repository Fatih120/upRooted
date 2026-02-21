using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum UserFriendshipInviteConnection
{
	[OriginalName("USER_FRIENDSHIP_INVITE_CONNECTION_UNSPECIFIED")]
	Unspecified,
	[OriginalName("USER_FRIENDSHIP_INVITE_CONNECTION_ANY")]
	Any,
	[OriginalName("USER_FRIENDSHIP_INVITE_CONNECTION_CONNECTED")]
	Connected,
	[OriginalName("USER_FRIENDSHIP_INVITE_CONNECTION_NONE")]
	None
}
