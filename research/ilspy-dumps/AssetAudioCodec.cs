using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public enum AssetAudioCodec
{
	[OriginalName("ASSET_AUDIO_CODEC_UNSPECIFIED")]
	Unspecified,
	[OriginalName("ASSET_AUDIO_CODEC_MP3")]
	Mp3,
	[OriginalName("ASSET_AUDIO_CODEC_AAC")]
	Aac,
	[OriginalName("ASSET_AUDIO_CODEC_VORBIS")]
	Vorbis,
	[OriginalName("ASSET_AUDIO_CODEC_OPUS")]
	Opus,
	[OriginalName("ASSET_AUDIO_CODEC_FLAC")]
	Flac
}
