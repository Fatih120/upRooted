using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.Assets;

public static class AssetInformationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AssetInformationReflection()
	{
		_003C_003Ey__InlineArray42<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray42<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Chxjb3JlL2Fzc2V0X2luZm9ybWF0aW9uLnByb3RvEgRyb290Gh5nb29nbGUv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 1) = "cHJvdG9idWYvZHVyYXRpb24ucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 2) = "c3RhbXAucHJvdG8aImNvcmUvYXNzZXRfaW5mb3JtYXRpb25fZW51bXMucHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 3) = "dG8aFWNvcmUvcm9vdF91dWlkcy5wcm90bxoTY29yZS92YWxpZGF0ZS5wcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 4) = "byJ3Cg5Bc3NldEltYWdlTGluaxITCgN1cmwYCiABKAlCBrpIA8gBARIfChds";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 5) = "YXJnZXN0X2RpbWVuc2lvbl9saW1pdBgLIAEoBRIWCg53aWR0aF9lc3RpbWF0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 6) = "ZRgMIAEoBRIXCg9oZWlnaHRfZXN0aW1hdGUYDSABKAUiOAoQQXNzZXRBc3Bl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 7) = "Y3RSYXRpbxISCgpob3Jpem9udGFsGAEgASgFEhAKCHZlcnRpY2FsGAIgASgF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 8) = "InwKCkFzc2V0SW1hZ2USMQoLYXNzZXRfbGlua3MYCiADKAsyFC5yb290LkFz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 9) = "c2V0SW1hZ2VMaW5rQga6SAPIAQESDQoFd2ViX3AYCyABKAwSLAoMYXNwZWN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 10) = "X3JhdGlvGAwgASgLMhYucm9vdC5Bc3NldEFzcGVjdFJhdGlvIswBCgpBc3Nl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 11) = "dFZpZGVvEisKCGR1cmF0aW9uGAEgASgLMhkuZ29vZ2xlLnByb3RvYnVmLkR1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 12) = "cmF0aW9uEg8KB2hsc191cmwYAiABKAkSEAoIZGFzaF91cmwYAyABKAkSFQoN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 13) = "dGh1bWJuYWlsX3VybBgEIAEoCRITCgtwcmV2aWV3X3VybBgFIAEoCRIUCgxk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 14) = "b3dubG9hZF91cmwYBiABKAkSLAoMYXNwZWN0X3JhdGlvGAcgASgLMhYucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 15) = "dC5Bc3NldEFzcGVjdFJhdGlvIlcKCUFzc2V0RmlsZRITCgN1cmwYASABKAlC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 16) = "BrpIA8gBARIRCgltaW1lX3R5cGUYAiABKAkSEgoKZXh0ZW5zaW9ucxgDIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 17) = "CRIOCgZsZW5ndGgYBCABKAMiPwoRQXNzZXRQcmV2aWV3SW1hZ2USDQoFd2lk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 18) = "dGgYASABKAUSDgoGaGVpZ2h0GAIgASgFEgsKA3VybBgDIAEoCSLLAQoRQXNz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 19) = "ZXRQcmV2aWV3VmlkZW8SDQoFd2lkdGgYASABKAUSDgoGaGVpZ2h0GAIgASgF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 20) = "Eg8KB2JpdHJhdGUYAyABKAUSCwoDZnBzGAQgASgFEisKCGR1cmF0aW9uGAUg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 21) = "ASgLMhkuZ29vZ2xlLnByb3RvYnVmLkR1cmF0aW9uEiYKBmZvcm1hdBgGIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 22) = "DjIWLnJvb3QuQXNzZXRWaWRlb0Zvcm1hdBIkCgVjb2RlYxgHIAEoDjIVLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 23) = "b3QuQXNzZXRWaWRlb0NvZGVjIrQBChFBc3NldFByZXZpZXdBdWRpbxIPCgdi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 24) = "aXRyYXRlGAEgASgFEhMKC3NhbXBsZV9yYXRlGAIgASgFEisKCGR1cmF0aW9u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 25) = "GAMgASgLMhkuZ29vZ2xlLnByb3RvYnVmLkR1cmF0aW9uEiYKBmZvcm1hdBgE";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 26) = "IAEoDjIWLnJvb3QuQXNzZXRBdWRpb0Zvcm1hdBIkCgVjb2RlYxgFIAEoDjIV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 27) = "LnJvb3QuQXNzZXRBdWRpb0NvZGVjIpICCgxBc3NldFByZXZpZXcSJAoEdHlw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 28) = "ZRgBIAEoDjIWLnJvb3QuQXNzZXRQcmV2aWV3VHlwZRIuCgp1cGRhdGVkX2F0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 29) = "GAIgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBINCgV0aXRsZRgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 30) = "IAEoCRITCgtkZXNjcmlwdGlvbhgEIAEoCRIpCghwcmV2aWV3cxgFIAMoCzIX";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 31) = "LnJvb3QuQXNzZXRQcmV2aWV3SW1hZ2USKAoFYXVkaW8YCiABKAsyFy5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 32) = "LkFzc2V0UHJldmlld0F1ZGlvSAASKAoFdmlkZW8YCyABKAsyFy5yb290LkFz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 33) = "c2V0UHJldmlld1ZpZGVvSABCCQoHZGV0YWlscyK8AgoQQXNzZXRJbmZvcm1h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 34) = "dGlvbhINCgN1cmwYASABKAlIABIhCgVpbWFnZRgCIAEoCzIQLnJvb3QuQXNz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 35) = "ZXRJbWFnZUgAEiEKBXZpZGVvGAMgASgLMhAucm9vdC5Bc3NldFZpZGVvSAAS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 36) = "JQoHaW52YWxpZBgEIAEoDjISLnJvb3QuQXNzZXRJbnZhbGlkSAASHwoEZmls";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 37) = "ZRgFIAEoCzIPLnJvb3QuQXNzZXRGaWxlSAASMwoPbGlua19leHBpcmVzX2F0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 38) = "GAogASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBIpCghhc3NldF9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 39) = "ZBgLIAEoCzIPLnJvb3QuQXNzZXRVdWlkQga6SAPIAQESIwoHcHJldmlldxgM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 40) = "IAEoCzISLnJvb3QuQXNzZXRQcmV2aWV3QgYKBGxpbmtCEaoCDlJvb3RBcHAu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray42<string>, string>(ref _003C_003Ey__InlineArray, 41) = "QXNzZXRzYgZwcm90bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray42<string>, string>(in _003C_003Ey__InlineArray, 42)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[5]
		{
			DurationReflection.Descriptor,
			TimestampReflection.Descriptor,
			AssetInformationEnumsReflection.Descriptor,
			RootUuidsReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[10]
		{
			new GeneratedClrTypeInfo(typeof(AssetImageLink), AssetImageLink.Parser, new string[4] { "Url", "LargestDimensionLimit", "WidthEstimate", "HeightEstimate" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetAspectRatio), AssetAspectRatio.Parser, new string[2] { "Horizontal", "Vertical" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetImage), AssetImage.Parser, new string[3] { "AssetLinks", "WebP", "AspectRatio" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetVideo), AssetVideo.Parser, new string[7] { "Duration", "HlsUrl", "DashUrl", "ThumbnailUrl", "PreviewUrl", "DownloadUrl", "AspectRatio" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetFile), AssetFile.Parser, new string[4] { "Url", "MimeType", "Extensions", "Length" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetPreviewImage), AssetPreviewImage.Parser, new string[3] { "Width", "Height", "Url" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetPreviewVideo), AssetPreviewVideo.Parser, new string[7] { "Width", "Height", "Bitrate", "Fps", "Duration", "Format", "Codec" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetPreviewAudio), AssetPreviewAudio.Parser, new string[5] { "Bitrate", "SampleRate", "Duration", "Format", "Codec" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetPreview), AssetPreview.Parser, new string[7] { "Type", "UpdatedAt", "Title", "Description", "Previews", "Audio", "Video" }, new string[1] { "Details" }, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetInformation), AssetInformation.Parser, new string[8] { "Url", "Image", "Video", "Invalid", "File", "LinkExpiresAt", "AssetId", "Preview" }, new string[1] { "Link" }, null, null, null)
		}));
	}
}
