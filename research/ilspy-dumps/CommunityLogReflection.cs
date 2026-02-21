using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class CommunityLogReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CommunityLogReflection()
	{
		_003C_003Ey__InlineArray6<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray6<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiN3ZWJhcGkvcmVxdWVzdHMvY29tbXVuaXR5X2xvZy5wcm90bxIEcm9vdBoV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 1) = "Y29yZS9yb290X3V1aWRzLnByb3RvGhNjb3JlL3ZhbGlkYXRlLnByb3RvInsK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 2) = "F0NvbW11bml0eUxvZ0xpc3RSZXF1ZXN0EjEKDGNvbW11bml0eV9pZBgKIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 3) = "CzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEi0KFWxhc3RfY29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 4) = "aXR5X2xvZ19pZBgLIAEoCzIOLnJvb3QuUm9vdFV1aWRCJqoCI1Jvb3RBcHAu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 5) = "V2ViQXBpLlNoYXJlZC5HcnBjLlJlcXVlc3RzYgZwcm90bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray6<string>, string>(in _003C_003Ey__InlineArray, 6)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[2]
		{
			RootUuidsReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[1]
		{
			new GeneratedClrTypeInfo(typeof(CommunityLogListRequest), CommunityLogListRequest.Parser, new string[2] { "CommunityId", "LastCommunityLogId" }, null, null, null, null)
		}));
	}
}
