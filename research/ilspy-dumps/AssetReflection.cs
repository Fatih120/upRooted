using System;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class AssetReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AssetReflection()
	{
		global::_003C_003Ey__InlineArray7<string> _003C_003Ey__InlineArray = default(global::_003C_003Ey__InlineArray7<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Cht3ZWJhcGkvcmVxdWVzdHMvYXNzZXQucHJvdG8SBHJvb3QaFWNvcmUvcm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 1) = "dF91dWlkcy5wcm90byIfCg9Bc3NldEdldFJlcXVlc3QSDAoEdXJpcxgBIAMo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 2) = "CSIvCh1Bc3NldFVwbG9hZFRva2VuU3RhdHVzUmVxdWVzdBIOCgZ0b2tlbnMY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 3) = "BSADKAkiUgoVQXNzZXRBcHBDcmVhdGVSZXF1ZXN0EikKDGNvbW11bml0eV9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 4) = "ZBgEIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZBIOCgZ0b2tlbnMYBSADKAlC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 5) = "JqoCI1Jvb3RBcHAuV2ViQXBpLlNoYXJlZC5HcnBjLlJlcXVlc3RzYgZwcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 6) = "bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<global::_003C_003Ey__InlineArray7<string>, string>(in _003C_003Ey__InlineArray, 7)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[1] { RootUuidsReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(AssetGetRequest), AssetGetRequest.Parser, new string[1] { "Uris" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetUploadTokenStatusRequest), AssetUploadTokenStatusRequest.Parser, new string[1] { "Tokens" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetAppCreateRequest), AssetAppCreateRequest.Parser, new string[2] { "CommunityId", "Tokens" }, null, null, null, null)
		}));
	}
}
