using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum MessageType
{
	[OriginalName("MESSAGE_TYPE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("MESSAGE_TYPE_USER_MESSAGE")]
	UserMessage,
	[OriginalName("MESSAGE_TYPE_SYSTEM")]
	System
}
