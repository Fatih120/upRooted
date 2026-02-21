using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum PacketErrorCode
{
	[OriginalName("PACKET_ERROR_CODE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("PACKET_ERROR_CODE_SYNC_LOST")]
	SyncLost
}
