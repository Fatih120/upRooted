using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum ChannelType
{
	[OriginalName("CHANNEL_TYPE_UNSPECIFIED")]
	Unspecified = 0,
	[OriginalName("CHANNEL_TYPE_TEXT")]
	Text = 1,
	[OriginalName("CHANNEL_TYPE_THREADED_TEXT")]
	ThreadedText = 2,
	[OriginalName("CHANNEL_TYPE_VOICE")]
	Voice = 4,
	[OriginalName("CHANNEL_TYPE_APP")]
	App = 8
}
