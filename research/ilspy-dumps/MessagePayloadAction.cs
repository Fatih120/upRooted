using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum MessagePayloadAction
{
	[OriginalName("MESSAGE_PAYLOAD_ACTION_UNSPECIFIED")]
	Unspecified,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_CREATED")]
	Created,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_UPDATED")]
	Updated,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_DELETED")]
	Deleted,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_KICKED")]
	Kicked,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_BANNED")]
	Banned,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_JOINED")]
	Joined,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_LEFT")]
	Left,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_PINNED")]
	Pinned,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_UN_PINNED")]
	UnPinned,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_CALL_STARTED")]
	CallStarted,
	[OriginalName("MESSAGE_PAYLOAD_ACTION_CALL_ENDED")]
	CallEnded
}
