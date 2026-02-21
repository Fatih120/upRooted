using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.App;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class AppStoreReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AppStoreReflection()
	{
		_003C_003Ey__InlineArray18<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray18<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Ch93ZWJhcGkvcmVxdWVzdHMvYXBwX3N0b3JlLnByb3RvEgRyb290GiVhcHBf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 1) = "c2VydmljZXMvc2VydmljZXMvYXBwX2VudW1zLnByb3RvGhVjb3JlL3Jvb3Rf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 2) = "dXVpZHMucHJvdG8aHmdvb2dsZS9wcm90b2J1Zi93cmFwcGVycy5wcm90bxoT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 3) = "Y29yZS92YWxpZGF0ZS5wcm90byJlChJBcHBTdG9yZUdldFJlcXVlc3QSIQoC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 4) = "aWQYBCABKAsyDS5yb290LkFwcFV1aWRCBrpIA8gBARIsCg5hcHBfdmVyc2lv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 5) = "bl9pZBgFIAEoCzIULnJvb3QuQXBwVmVyc2lvblV1aWQiOgoTQXBwU3RvcmVM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 6) = "aXN0UmVxdWVzdBIjCghhcHBfdHlwZRgEIAEoDjIRLnJvb3QuYXBwLkFwcFR5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 7) = "cGUiPwoaQXBwU3RvcmVMaXN0VmVyc2lvblJlcXVlc3QSIQoCaWQYBCABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 8) = "DS5yb290LkFwcFV1aWRCBrpIA8gBASLZAQoVQXBwU3RvcmVTZWFyY2hSZXF1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 9) = "ZXN0EisKCGFwcF90eXBlGAQgASgOMhEucm9vdC5hcHAuQXBwVHlwZUIGukgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 10) = "yAEBEjYKE2FwcF9vcmdhbml6YXRpb25faWQYBSABKAsyGS5yb290LkFwcE9y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 11) = "Z2FuaXphdGlvblV1aWQSKwoMYXBwX2NhdGVnb3J5GAYgASgOMhUucm9vdC5h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 12) = "cHAuQXBwQ2F0ZWdvcnkSLgoIa2V5d29yZHMYByABKAsyHC5nb29nbGUucHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 13) = "dG9idWYuU3RyaW5nVmFsdWUiRQogQXBwU3RvcmVHZXRHbG9iYWxTZXR0aW5n";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 14) = "c1JlcXVlc3QSIQoCaWQYBCABKAsyDS5yb290LkFwcFV1aWRCBrpIA8gBASI2";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 15) = "ChhBcHBTdG9yZUxpc3RTaG9ydFJlcXVlc3QSGgoDaWRzGAQgAygLMg0ucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 16) = "dC5BcHBVdWlkQiaqAiNSb290QXBwLldlYkFwaS5TaGFyZWQuR3JwYy5SZXF1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 17) = "ZXN0c2IGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray18<string>, string>(in _003C_003Ey__InlineArray, 18)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[4]
		{
			AppEnumsReflection.Descriptor,
			RootUuidsReflection.Descriptor,
			WrappersReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(AppStoreGetRequest), AppStoreGetRequest.Parser, new string[2] { "Id", "AppVersionId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppStoreListRequest), AppStoreListRequest.Parser, new string[1] { "AppType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppStoreListVersionRequest), AppStoreListVersionRequest.Parser, new string[1] { "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppStoreSearchRequest), AppStoreSearchRequest.Parser, new string[4] { "AppType", "AppOrganizationId", "AppCategory", "Keywords" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppStoreGetGlobalSettingsRequest), AppStoreGetGlobalSettingsRequest.Parser, new string[1] { "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppStoreListShortRequest), AppStoreListShortRequest.Parser, new string[1] { "Ids" }, null, null, null, null)
		}));
	}
}
