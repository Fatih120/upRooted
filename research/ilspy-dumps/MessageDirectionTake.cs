using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum MessageDirectionTake
{
	[OriginalName("MESSAGE_DIRECTION_TAKE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("MESSAGE_DIRECTION_TAKE_NEWER")]
	Newer,
	[OriginalName("MESSAGE_DIRECTION_TAKE_OLDER")]
	Older,
	[OriginalName("MESSAGE_DIRECTION_TAKE_BOTH")]
	Both
}
