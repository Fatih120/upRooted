using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public enum AssetInvalid
{
	[OriginalName("ASSET_INVALID_UNSPECIFIED")]
	Unspecified,
	[OriginalName("ASSET_INVALID_ERROR")]
	Error,
	[OriginalName("ASSET_INVALID_DELETED")]
	Deleted,
	[OriginalName("ASSET_INVALID_BLOCKED")]
	Blocked
}
