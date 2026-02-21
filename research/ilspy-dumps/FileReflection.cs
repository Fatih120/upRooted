using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class FileReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FileReflection()
	{
		_003C_003Ey__InlineArray49<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray49<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Chp3ZWJhcGkvcmVxdWVzdHMvZmlsZS5wcm90bxIEcm9vdBoVY29yZS9yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 1) = "X3V1aWRzLnByb3RvGhR3ZWJhcGkvY29udGV4dC5wcm90bxoTY29yZS92YWxp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 2) = "ZGF0ZS5wcm90byL5AQoRRmlsZUNyZWF0ZVJlcXVlc3QSIgoHY29udGV4dBgB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 3) = "IAEoCzIRLnJvb3QuUm9vdENvbnRleHQSMQoMY29tbXVuaXR5X2lkGAogASgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 4) = "MhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQESOAoMY29udGFpbmVyX2lk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 5) = "GAsgASgLMhoucm9vdC5NZXNzYWdlQ29udGFpbmVyVXVpZEIGukgDyAEBEjEK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 6) = "DGRpcmVjdG9yeV9pZBgNIAEoCzITLnJvb3QuRGlyZWN0b3J5VXVpZEIGukgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 7) = "yAEBEiAKEHVwbG9hZF90b2tlbl91cmkYDiABKAlCBrpIA8gBASKPAgoPRmls";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 8) = "ZUVkaXRSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 9) = "ZXh0EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 10) = "ZEIGukgDyAEBEjgKDGNvbnRhaW5lcl9pZBgLIAEoCzIaLnJvb3QuTWVzc2Fn";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 11) = "ZUNvbnRhaW5lclV1aWRCBrpIA8gBARIiCgJpZBgMIAEoCzIOLnJvb3QuRmls";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 12) = "ZVV1aWRCBrpIA8gBARIxCgxkaXJlY3RvcnlfaWQYDSABKAsyEy5yb290LkRp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 13) = "cmVjdG9yeVV1aWRCBrpIA8gBARIUCgRuYW1lGA4gASgJQga6SAPIAQEitAIK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 14) = "D0ZpbGVNb3ZlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 15) = "Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 16) = "eVV1aWRCBrpIA8gBARI4Cgxjb250YWluZXJfaWQYCyABKAsyGi5yb290Lk1l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 17) = "c3NhZ2VDb250YWluZXJVdWlkQga6SAPIAQESIgoCaWQYDCABKAsyDi5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 18) = "LkZpbGVVdWlkQga6SAPIAQESNQoQb2xkX2RpcmVjdG9yeV9pZBgNIAEoCzIT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 19) = "LnJvb3QuRGlyZWN0b3J5VXVpZEIGukgDyAEBEjUKEG5ld19kaXJlY3Rvcnlf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 20) = "aWQYDiABKAsyEy5yb290LkRpcmVjdG9yeVV1aWRCBrpIA8gBASLXAQoRRmls";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 21) = "ZURlbGV0ZVJlcXVlc3QSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5D";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 22) = "b21tdW5pdHlVdWlkQga6SAPIAQESOAoMY29udGFpbmVyX2lkGAsgASgLMhou";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 23) = "cm9vdC5NZXNzYWdlQ29udGFpbmVyVXVpZEIGukgDyAEBEiIKAmlkGAwgASgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 24) = "Mg4ucm9vdC5GaWxlVXVpZEIGukgDyAEBEjEKDGRpcmVjdG9yeV9pZBgNIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 25) = "CzITLnJvb3QuRGlyZWN0b3J5VXVpZEIGukgDyAEBItQBCg5GaWxlR2V0UmVx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 26) = "dWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 27) = "aWRCBrpIA8gBARI4Cgxjb250YWluZXJfaWQYCyABKAsyGi5yb290Lk1lc3Nh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 28) = "Z2VDb250YWluZXJVdWlkQga6SAPIAQESIgoCaWQYDCABKAsyDi5yb290LkZp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 29) = "bGVVdWlkQga6SAPIAQESMQoMZGlyZWN0b3J5X2lkGA0gASgLMhMucm9vdC5E";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 30) = "aXJlY3RvcnlVdWlkQga6SAPIAQEimgEKGkZpbGVTZWFyY2hDb21tdW5pdHlS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 31) = "ZXF1ZXN0EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 32) = "VXVpZEIGukgDyAEBEjkKDWNvbnRhaW5lcl9pZHMYCyADKAsyGi5yb290Lk1l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 33) = "c3NhZ2VDb250YWluZXJVdWlkQga6SAPIAQESDgoGc2VhcmNoGAwgASgJIrYB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 34) = "ChFGaWxlU2VhcmNoUmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 35) = "b290LkNvbW11bml0eVV1aWRCBrpIA8gBARI4Cgxjb250YWluZXJfaWQYCyAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 36) = "KAsyGi5yb290Lk1lc3NhZ2VDb250YWluZXJVdWlkQga6SAPIAQESDgoGc2Vh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 37) = "cmNoGAwgASgJEiQKDGxhc3RfZmlsZV9pZBgNIAEoCzIOLnJvb3QuRmlsZVV1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 38) = "aWQisQEKD0ZpbGVMaXN0UmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 39) = "Ey5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARI4Cgxjb250YWluZXJfaWQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 40) = "CyABKAsyGi5yb290Lk1lc3NhZ2VDb250YWluZXJVdWlkQga6SAPIAQESMQoM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 41) = "ZGlyZWN0b3J5X2lkGAwgASgLMhMucm9vdC5EaXJlY3RvcnlVdWlkQga6SAPI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 42) = "AQEihAIKE0ZpbGVEb3dubG9hZFJlcXVlc3QSMQoMY29tbXVuaXR5X2lkGAog";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 43) = "ASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQESOAoMY29udGFpbmVy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 44) = "X2lkGAsgASgLMhoucm9vdC5NZXNzYWdlQ29udGFpbmVyVXVpZEIGukgDyAEB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 45) = "EiIKAmlkGAwgASgLMg4ucm9vdC5GaWxlVXVpZEIGukgDyAEBEjEKDGRpcmVj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 46) = "dG9yeV9pZBgNIAEoCzITLnJvb3QuRGlyZWN0b3J5VXVpZEIGukgDyAEBEikK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 47) = "CGFzc2V0X2lkGA4gASgLMg8ucm9vdC5Bc3NldFV1aWRCBrpIA8gBAUImqgIj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray49<string>, string>(ref _003C_003Ey__InlineArray, 48) = "Um9vdEFwcC5XZWJBcGkuU2hhcmVkLkdycGMuUmVxdWVzdHNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray49<string>, string>(in _003C_003Ey__InlineArray, 49)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[9]
		{
			new GeneratedClrTypeInfo(typeof(FileCreateRequest), FileCreateRequest.Parser, new string[5] { "Context", "CommunityId", "ContainerId", "DirectoryId", "UploadTokenUri" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FileEditRequest), FileEditRequest.Parser, new string[6] { "Context", "CommunityId", "ContainerId", "Id", "DirectoryId", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FileMoveRequest), FileMoveRequest.Parser, new string[6] { "Context", "CommunityId", "ContainerId", "Id", "OldDirectoryId", "NewDirectoryId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FileDeleteRequest), FileDeleteRequest.Parser, new string[4] { "CommunityId", "ContainerId", "Id", "DirectoryId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FileGetRequest), FileGetRequest.Parser, new string[4] { "CommunityId", "ContainerId", "Id", "DirectoryId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FileSearchCommunityRequest), FileSearchCommunityRequest.Parser, new string[3] { "CommunityId", "ContainerIds", "Search" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FileSearchRequest), FileSearchRequest.Parser, new string[4] { "CommunityId", "ContainerId", "Search", "LastFileId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FileListRequest), FileListRequest.Parser, new string[3] { "CommunityId", "ContainerId", "DirectoryId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FileDownloadRequest), FileDownloadRequest.Parser, new string[5] { "CommunityId", "ContainerId", "Id", "DirectoryId", "AssetId" }, null, null, null, null)
		}));
	}
}
