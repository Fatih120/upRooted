using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class DirectoryReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static DirectoryReflection()
	{
		_003C_003Ey__InlineArray40<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray40<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Ch93ZWJhcGkvcmVxdWVzdHMvZGlyZWN0b3J5LnByb3RvEgRyb290GhVjb3Jl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 1) = "L3Jvb3RfdXVpZHMucHJvdG8aFHdlYmFwaS9jb250ZXh0LnByb3RvGhNjb3Jl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 2) = "L3ZhbGlkYXRlLnByb3RvIukBChZEaXJlY3RvcnlDcmVhdGVSZXF1ZXN0EiIK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 3) = "B2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0EjEKDGNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 4) = "eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEjgKDGNv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 5) = "bnRhaW5lcl9pZBgLIAEoCzIaLnJvb3QuTWVzc2FnZUNvbnRhaW5lclV1aWRC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 6) = "BrpIA8gBARIMCgRuYW1lGAwgASgJEjAKE3BhcmVudF9kaXJlY3RvcnlfaWQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 7) = "DSABKAsyEy5yb290LkRpcmVjdG9yeVV1aWQi5gEKFERpcmVjdG9yeUVkaXRS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 8) = "ZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0EicK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 9) = "AmlkGAogASgLMhMucm9vdC5EaXJlY3RvcnlVdWlkQga6SAPIAQESMQoMY29t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 10) = "bXVuaXR5X2lkGAsgASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQES";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 11) = "OAoMY29udGFpbmVyX2lkGAwgASgLMhoucm9vdC5NZXNzYWdlQ29udGFpbmVy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 12) = "VXVpZEIGukgDyAEBEhQKBG5hbWUYDSABKAlCBrpIA8gBASLMAgoURGlyZWN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 13) = "b3J5TW92ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 14) = "bnRleHQSJwoCaWQYCiABKAsyEy5yb290LkRpcmVjdG9yeVV1aWRCBrpIA8gB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 15) = "ARIxCgxjb21tdW5pdHlfaWQYCyABKAsyEy5yb290LkNvbW11bml0eVV1aWRC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 16) = "BrpIA8gBARI4Cgxjb250YWluZXJfaWQYDCABKAsyGi5yb290Lk1lc3NhZ2VD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 17) = "b250YWluZXJVdWlkQga6SAPIAQESPAoXb2xkX3BhcmVudF9kaXJlY3Rvcnlf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 18) = "aWQYDSABKAsyEy5yb290LkRpcmVjdG9yeVV1aWRCBrpIA8gBARI8ChduZXdf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 19) = "cGFyZW50X2RpcmVjdG9yeV9pZBgOIAEoCzITLnJvb3QuRGlyZWN0b3J5VXVp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 20) = "ZEIGukgDyAEBIqsBChNEaXJlY3RvcnlHZXRSZXF1ZXN0EjEKDGNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 21) = "eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEjgKDGNv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 22) = "bnRhaW5lcl9pZBgLIAEoCzIaLnJvb3QuTWVzc2FnZUNvbnRhaW5lclV1aWRC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 23) = "BrpIA8gBARInCgJpZBgMIAEoCzITLnJvb3QuRGlyZWN0b3J5VXVpZEIGukgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 24) = "yAEBIq4BChZEaXJlY3RvcnlHZXRBbGxSZXF1ZXN0EjEKDGNvbW11bml0eV9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 25) = "ZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEjgKDGNvbnRh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 26) = "aW5lcl9pZBgLIAEoCzIaLnJvb3QuTWVzc2FnZUNvbnRhaW5lclV1aWRCBrpI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 27) = "A8gBARInCgJpZBgMIAEoCzITLnJvb3QuRGlyZWN0b3J5VXVpZEIGukgDyAEB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 28) = "Iq4BChZEaXJlY3RvcnlEZWxldGVSZXF1ZXN0EjEKDGNvbW11bml0eV9pZBgK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 29) = "IAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEjgKDGNvbnRhaW5l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 30) = "cl9pZBgLIAEoCzIaLnJvb3QuTWVzc2FnZUNvbnRhaW5lclV1aWRCBrpIA8gB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 31) = "ARInCgJpZBgMIAEoCzITLnJvb3QuRGlyZWN0b3J5VXVpZEIGukgDyAEBIoMB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 32) = "ChREaXJlY3RvcnlMaXN0UmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 33) = "Ey5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARI4Cgxjb250YWluZXJfaWQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 34) = "CyABKAsyGi5yb290Lk1lc3NhZ2VDb250YWluZXJVdWlkQga6SAPIAQEihgEK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 35) = "F0RpcmVjdG9yeUxpc3RBbGxSZXF1ZXN0EjEKDGNvbW11bml0eV9pZBgKIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 36) = "CzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEjgKDGNvbnRhaW5lcl9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 37) = "ZBgLIAEoCzIaLnJvb3QuTWVzc2FnZUNvbnRhaW5lclV1aWRCBrpIA8gBAUIm";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 38) = "qgIjUm9vdEFwcC5XZWJBcGkuU2hhcmVkLkdycGMuUmVxdWVzdHNiBnByb3Rv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 39) = "Mw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray40<string>, string>(in _003C_003Ey__InlineArray, 40)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[8]
		{
			new GeneratedClrTypeInfo(typeof(DirectoryCreateRequest), DirectoryCreateRequest.Parser, new string[5] { "Context", "CommunityId", "ContainerId", "Name", "ParentDirectoryId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectoryEditRequest), DirectoryEditRequest.Parser, new string[5] { "Context", "Id", "CommunityId", "ContainerId", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectoryMoveRequest), DirectoryMoveRequest.Parser, new string[6] { "Context", "Id", "CommunityId", "ContainerId", "OldParentDirectoryId", "NewParentDirectoryId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectoryGetRequest), DirectoryGetRequest.Parser, new string[3] { "CommunityId", "ContainerId", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectoryGetAllRequest), DirectoryGetAllRequest.Parser, new string[3] { "CommunityId", "ContainerId", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectoryDeleteRequest), DirectoryDeleteRequest.Parser, new string[3] { "CommunityId", "ContainerId", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectoryListRequest), DirectoryListRequest.Parser, new string[2] { "CommunityId", "ContainerId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectoryListAllRequest), DirectoryListAllRequest.Parser, new string[2] { "CommunityId", "ContainerId" }, null, null, null, null)
		}));
	}
}
