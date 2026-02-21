using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.Core.Validate;

public static class ValidateReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ValidateReflection()
	{
		_003C_003Ey__InlineArray7<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray7<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 0) = "ChNjb3JlL3ZhbGlkYXRlLnByb3RvEgh2YWxpZGF0ZRogZ29vZ2xlL3Byb3Rv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 1) = "YnVmL2Rlc2NyaXB0b3IucHJvdG8iHgoKRmllbGRSdWxlcxIQCghyZXF1aXJl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 2) = "ZBgZIAEoCCJQCgtTdHJpbmdSdWxlcxIPCgdtaW5fbGVuGAIgASgEEg8KB21h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 3) = "eF9sZW4YAyABKAQSDwoHcGF0dGVybhgEIAEoCRIOCgZjdXN0b20YBSABKAk6";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 4) = "RgoFcnVsZXMSHS5nb29nbGUucHJvdG9idWYuRmllbGRPcHRpb25zGIcJIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 5) = "CzIULnZhbGlkYXRlLkZpZWxkUnVsZXOIAQFCGKoCFVJvb3RBcHAuQ29yZS5W";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 6) = "YWxpZGF0ZWIGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray7<string>, string>(in _003C_003Ey__InlineArray, 7)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[1] { DescriptorReflection.Descriptor }, new GeneratedClrTypeInfo(null, new Extension[1] { ValidateExtensions.Rules }, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FieldRules), FieldRules.Parser, new string[1] { "Required" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(StringRules), StringRules.Parser, new string[4] { "MinLen", "MaxLen", "Pattern", "Custom" }, null, null, null, null)
		}));
	}
}
