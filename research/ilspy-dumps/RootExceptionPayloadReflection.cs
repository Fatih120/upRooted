using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Exceptions.Payloads;

public static class RootExceptionPayloadReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static RootExceptionPayloadReflection()
	{
		_003C_003Ey__InlineArray17<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray17<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Cix3ZWJhcGkvcGF5bG9hZHMvcm9vdF9leGNlcHRpb25fcGF5bG9hZC5wcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 1) = "bxIEcm9vdBoVY29yZS9yb290X3V1aWRzLnByb3RvGh5nb29nbGUvcHJvdG9i";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 2) = "dWYvd3JhcHBlcnMucHJvdG8iLAoYVXNlcm5hbWVFeGNlcHRpb25QYXlsb2Fk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 3) = "EhAKCHVzZXJuYW1lGAogASgJIiYKFUVtYWlsRXhjZXB0aW9uUGF5bG9hZBIN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 4) = "CgVlbWFpbBgKIAEoCSIzChtBY2Nlc3NUb2tlbkV4Y2VwdGlvblBheWxvYWQS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 5) = "FAoMYWNjZXNzX3Rva2VuGAogASgJIkQKGkZyaWVuZFVzZXJFeGNlcHRpb25Q";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 6) = "YXlsb2FkEiYKDmZyaWVuZF91c2VyX2lkGAogASgLMg4ucm9vdC5Vc2VyVXVp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 7) = "ZCJdChxVcGxvYWRTdGF0dXNFeGNlcHRpb25QYXlsb2FkEhEKCXRva2VuX3Vy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 8) = "aRgKIAEoCRIqCgZyZXN1bHQYCyABKAsyGi5nb29nbGUucHJvdG9idWYuQm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 9) = "bFZhbHVlIl8KIFVwbG9hZFN0YXR1c0xpc3RFeGNlcHRpb25QYXlsb2FkEjsK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 10) = "D3VwbG9hZF9zdGF0dXNlcxgKIAMoCzIiLnJvb3QuVXBsb2FkU3RhdHVzRXhj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 11) = "ZXB0aW9uUGF5bG9hZCJkCiBSZXF1ZXN0VmFsaWRhdG9yRXhjZXB0aW9uUGF5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 12) = "bG9hZBIVCg1wcm9wZXJ0eV9uYW1lGAogASgJEhUKDWVycm9yX21lc3NhZ2UY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 13) = "CyABKAkSEgoKZXJyb3JfY29kZRgMIAEoCSJeCiRSZXF1ZXN0VmFsaWRhdG9y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 14) = "TGlzdEV4Y2VwdGlvblBheWxvYWQSNgoGZXJyb3JzGAogAygLMiYucm9vdC5S";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 15) = "ZXF1ZXN0VmFsaWRhdG9yRXhjZXB0aW9uUGF5bG9hZEIsqgIpUm9vdEFwcC5X";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 16) = "ZWJBcGkuU2hhcmVkLkV4Y2VwdGlvbnMuUGF5bG9hZHNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray17<string>, string>(in _003C_003Ey__InlineArray, 17)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[2]
		{
			RootUuidsReflection.Descriptor,
			WrappersReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[8]
		{
			new GeneratedClrTypeInfo(typeof(UsernameExceptionPayload), UsernameExceptionPayload.Parser, new string[1] { "Username" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(EmailExceptionPayload), EmailExceptionPayload.Parser, new string[1] { "Email" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AccessTokenExceptionPayload), AccessTokenExceptionPayload.Parser, new string[1] { "AccessToken" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendUserExceptionPayload), FriendUserExceptionPayload.Parser, new string[1] { "FriendUserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UploadStatusExceptionPayload), UploadStatusExceptionPayload.Parser, new string[2] { "TokenUri", "Result" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UploadStatusListExceptionPayload), UploadStatusListExceptionPayload.Parser, new string[1] { "UploadStatuses" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RequestValidatorExceptionPayload), RequestValidatorExceptionPayload.Parser, new string[3] { "PropertyName", "ErrorMessage", "ErrorCode" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RequestValidatorListExceptionPayload), RequestValidatorListExceptionPayload.Parser, new string[1] { "Errors" }, null, null, null, null)
		}));
	}
}
