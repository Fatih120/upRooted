using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class DirectMessageReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static DirectMessageReflection()
	{
		_003C_003Ey__InlineArray18<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray18<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiR3ZWJhcGkvcmVxdWVzdHMvZGlyZWN0X21lc3NhZ2UucHJvdG8SBHJvb3Qa";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 1) = "FWNvcmUvcm9vdF91dWlkcy5wcm90bxoUd2ViYXBpL2NvbnRleHQucHJvdG8a";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 2) = "E2NvcmUvdmFsaWRhdGUucHJvdG8icQoaRGlyZWN0TWVzc2FnZUNyZWF0ZVJl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 3) = "cXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSLwoP";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 4) = "bWVtYmVyX3VzZXJfaWRzGAogAygLMg4ucm9vdC5Vc2VyVXVpZEIGukgDyAEB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 5) = "IpsBCh5EaXJlY3RNZXNzYWdlQWRkTWVtYmVyc1JlcXVlc3QSIgoHY29udGV4";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 6) = "dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSKwoCaWQYCiABKAsyFy5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 7) = "LkRpcmVjdE1lc3NhZ2VVdWlkQga6SAPIAQESKAoIdXNlcl9pZHMYCyADKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 8) = "Di5yb290LlVzZXJVdWlkQga6SAPIAQEicQoeRGlyZWN0TWVzc2FnZURlbGV0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 9) = "ZVVzZXJSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 10) = "ZXh0EisKAmlkGAogASgLMhcucm9vdC5EaXJlY3RNZXNzYWdlVXVpZEIGukgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 11) = "yAEBIksKGERpcmVjdE1lc3NhZ2VGaW5kUmVxdWVzdBIvCg9tZW1iZXJfdXNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 12) = "cl9pZHMYCiADKAsyDi5yb290LlVzZXJVdWlkQga6SAPIAQEiGgoYRGlyZWN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 13) = "TWVzc2FnZUxpc3RSZXF1ZXN0InIKH0RpcmVjdE1lc3NhZ2VSaW5nRGVjbGlu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 14) = "ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 15) = "KwoCaWQYCiABKAsyFy5yb290LkRpcmVjdE1lc3NhZ2VVdWlkQga6SAPIAQFC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 16) = "JqoCI1Jvb3RBcHAuV2ViQXBpLlNoYXJlZC5HcnBjLlJlcXVlc3RzYgZwcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 17) = "bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray18<string>, string>(in _003C_003Ey__InlineArray, 18)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(DirectMessageCreateRequest), DirectMessageCreateRequest.Parser, new string[2] { "Context", "MemberUserIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectMessageAddMembersRequest), DirectMessageAddMembersRequest.Parser, new string[3] { "Context", "Id", "UserIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectMessageDeleteUserRequest), DirectMessageDeleteUserRequest.Parser, new string[2] { "Context", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectMessageFindRequest), DirectMessageFindRequest.Parser, new string[1] { "MemberUserIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectMessageListRequest), DirectMessageListRequest.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectMessageRingDeclineRequest), DirectMessageRingDeclineRequest.Parser, new string[2] { "Context", "Id" }, null, null, null, null)
		}));
	}
}
