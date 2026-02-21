using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum MessageVoteType
{
	[OriginalName("MESSAGE_VOTE_TYPE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("MESSAGE_VOTE_TYPE_DOWN")]
	Down,
	[OriginalName("MESSAGE_VOTE_TYPE_UP")]
	Up
}
