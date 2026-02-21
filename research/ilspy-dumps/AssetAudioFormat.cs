using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public enum AssetAudioFormat
{
	[OriginalName("ASSET_AUDIO_FORMAT_UNSPECIFIED")]
	Unspecified,
	[OriginalName("ASSET_AUDIO_FORMAT_MP3")]
	Mp3,
	[OriginalName("ASSET_AUDIO_FORMAT_AAC")]
	Aac,
	[OriginalName("ASSET_AUDIO_FORMAT_M4A")]
	M4A,
	[OriginalName("ASSET_AUDIO_FORMAT_OGG")]
	Ogg,
	[OriginalName("ASSET_AUDIO_FORMAT_WAV")]
	Wav
}
