using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Exceptions.Payloads;

namespace RootApp.WebApi.Shared.Exceptions;

public static class RootGrpcExceptionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static RootGrpcExceptionReflection()
	{
		_003C_003Ey__InlineArray19<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray19<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiB3ZWJhcGkvcm9vdF9ncnBjX2V4Y2VwdGlvbi5wcm90bxIEcm9vdBoVY29y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 1) = "ZS9yb290X3V1aWRzLnByb3RvGix3ZWJhcGkvcGF5bG9hZHMvcm9vdF9leGNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 2) = "cHRpb25fcGF5bG9hZC5wcm90bxoSd2ViYXBpL2VudW1zLnByb3RvIo8CChFS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 3) = "b290R3JwY0V4Y2VwdGlvbhInCgplcnJvcl9jb2RlGAogASgOMhMucm9vdC5F";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 4) = "cnJvckNvZGVUeXBlEhoKAmlkGAsgASgLMg4ucm9vdC5Sb290VXVpZBIeCgZ3";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 5) = "aG9faWQYDCABKAsyDi5yb290LlVzZXJVdWlkEh8KB3doYXRfaWQYDSABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 6) = "Di5yb290LlJvb3RVdWlkEiAKCHdoZXJlX2lkGA4gASgLMg4ucm9vdC5Sb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 7) = "VXVpZBIhCglwYXJlbnRfaWQYDyABKAsyDi5yb290LlJvb3RVdWlkEi8KB3Bh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 8) = "eWxvYWQYECABKAsyHi5yb290LlJvb3RHcnBjRXhjZXB0aW9uUGF5bG9hZCL4";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 9) = "AgoYUm9vdEdycGNFeGNlcHRpb25QYXlsb2FkEjAKCHVzZXJuYW1lGAogASgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 10) = "Mh4ucm9vdC5Vc2VybmFtZUV4Y2VwdGlvblBheWxvYWQSKgoFZW1haWwYCyAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 11) = "KAsyGy5yb290LkVtYWlsRXhjZXB0aW9uUGF5bG9hZBI3CgxhY2Nlc3NfdG9r";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 12) = "ZW4YDCABKAsyIS5yb290LkFjY2Vzc1Rva2VuRXhjZXB0aW9uUGF5bG9hZBI1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 13) = "CgtmcmllbmRfdXNlchgNIAEoCzIgLnJvb3QuRnJpZW5kVXNlckV4Y2VwdGlv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 14) = "blBheWxvYWQSQgoSdXBsb2FkX3N0YXR1c19saXN0GA4gASgLMiYucm9vdC5V";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 15) = "cGxvYWRTdGF0dXNMaXN0RXhjZXB0aW9uUGF5bG9hZBJKChZyZXF1ZXN0X3Zh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 16) = "bGlkYXRvcl9saXN0GA8gASgLMioucm9vdC5SZXF1ZXN0VmFsaWRhdG9yTGlz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 17) = "dEV4Y2VwdGlvblBheWxvYWRCI6oCIFJvb3RBcHAuV2ViQXBpLlNoYXJlZC5F";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray19<string>, string>(ref _003C_003Ey__InlineArray, 18) = "eGNlcHRpb25zYgZwcm90bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray19<string>, string>(in _003C_003Ey__InlineArray, 19)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			RootExceptionPayloadReflection.Descriptor,
			EnumsReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(RootGrpcException), RootGrpcException.Parser, new string[7] { "ErrorCode", "Id", "WhoId", "WhatId", "WhereId", "ParentId", "Payload" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RootGrpcExceptionPayload), RootGrpcExceptionPayload.Parser, new string[6] { "Username", "Email", "AccessToken", "FriendUser", "UploadStatusList", "RequestValidatorList" }, null, null, null, null)
		}));
	}
}
