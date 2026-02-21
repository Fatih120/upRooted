using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public enum AssetVideoCodec
{
	[OriginalName("ASSET_VIDEO_CODEC_UNSPECIFIED")]
	Unspecified,
	[OriginalName("ASSET_VIDEO_CODEC_H264_AVC")]
	H264Avc,
	[OriginalName("ASSET_VIDEO_CODEC_H265_HEVC")]
	H265Hevc,
	[OriginalName("ASSET_VIDEO_CODEC_VP9")]
	Vp9,
	[OriginalName("ASSET_VIDEO_CODEC_AV1")]
	Av1
}
