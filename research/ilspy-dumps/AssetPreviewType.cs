using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public enum AssetPreviewType
{
	[OriginalName("ASSET_PREVIEW_TYPE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("ASSET_PREVIEW_TYPE_IMAGE")]
	Image,
	[OriginalName("ASSET_PREVIEW_TYPE_VIDEO")]
	Video,
	[OriginalName("ASSET_PREVIEW_TYPE_AUDIO")]
	Audio,
	[OriginalName("ASSET_PREVIEW_TYPE_DOCUMENT")]
	Document,
	[OriginalName("ASSET_PREVIEW_TYPE_TEXT")]
	Text,
	[OriginalName("ASSET_PREVIEW_TYPE_PAGE")]
	Page
}
