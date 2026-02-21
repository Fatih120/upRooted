using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public enum AssetVideoFormat
{
	[OriginalName("ASSET_VIDEO_FORMAT_UNSPECIFIED")]
	Unspecified = 0,
	[OriginalName("ASSET_VIDEO_FORMAT_MP4")]
	Mp4 = 1,
	[OriginalName("ASSET_VIDEO_FORMAT_MKV")]
	Mkv = 2,
	[OriginalName("ASSET_VIDEO_FORMAT_MOV")]
	Mov = 3,
	[OriginalName("ASSET_VIDEO_FORMAT_AVI")]
	Avi = 4,
	[OriginalName("ASSET_VIDEO_FORMAT_FLV")]
	Flv = 5,
	[OriginalName("ASSET_VIDEO_FORMAT_MPEG2_TS")]
	Mpeg2Ts = 6,
	[OriginalName("ASSET_VIDEO_FORMAT_MPEG2_PS")]
	Mpeg2Ps = 7,
	[OriginalName("ASSET_VIDEO_FORMAT_MXF")]
	Mxf = 8,
	[OriginalName("ASSET_VIDEO_FORMAT_LXF")]
	Lxf = 9,
	[OriginalName("ASSET_VIDEO_FORMAT_GXF")]
	Gxf = 10,
	[OriginalName("ASSET_VIDEO_FORMAT_WEB_M")]
	WebM = 12,
	[OriginalName("ASSET_VIDEO_FORMAT_MPG")]
	Mpg = 13,
	[OriginalName("ASSET_VIDEO_FORMAT_QUICK_TIME")]
	QuickTime = 14
}
