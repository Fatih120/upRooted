using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.Core.Validate;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class CommunityReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CommunityReflection()
	{
		_003C_003Ey__InlineArray38<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray38<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Ch93ZWJhcGkvcmVxdWVzdHMvY29tbXVuaXR5LnByb3RvEgRyb290GhVjb3Jl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 1) = "L3Jvb3RfdXVpZHMucHJvdG8aE2NvcmUvdmFsaWRhdGUucHJvdG8aHmdvb2ds";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 2) = "ZS9wcm90b2J1Zi93cmFwcGVycy5wcm90bxoUd2ViYXBpL2NvbnRleHQucHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 3) = "dG8aHndlYmFwaS9wYWNrZXRzL2NvbW11bml0eS5wcm90byKIAgoWQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 4) = "aXR5Q3JlYXRlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 5) = "Q29udGV4dBIMCgRuYW1lGAogASgJEhMKC3BpY3R1cmVfaGV4GAsgASgJEhUK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 6) = "DXRlbXBsYXRlX3R5cGUYDCABKAkSOwoVaWNvbl91cGxvYWRfdG9rZW5fdXJp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 7) = "GA0gASgLMhwuZ29vZ2xlLnByb3RvYnVmLlN0cmluZ1ZhbHVlEh8KF3JlamVj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 8) = "dF91bnZlcmlmaWVkX2VtYWlsGA4gASgIEjIKDWpvaW5fdGhyb3R0bGUYDyAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 9) = "KAsyGy5yb290LkNvbW11bml0eUpvaW5UaHJvdHRsZSLlAgoUQ29tbXVuaXR5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 10) = "RWRpdFJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 11) = "eHQSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 12) = "Qga6SAPIAQESDAoEbmFtZRgLIAEoCRITCgtwaWN0dXJlX2hleBgMIAEoCRIW";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 13) = "Cg51cGRhdGVfcGljdHVyZRgNIAEoCBI3ChFwaWN0dXJlX3Rva2VuX3VyaRgO";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 14) = "IAEoCzIcLmdvb2dsZS5wcm90b2J1Zi5TdHJpbmdWYWx1ZRItChJkZWZhdWx0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 15) = "X2NoYW5uZWxfaWQYDyABKAsyES5yb290LkNoYW5uZWxVdWlkEh8KF3JlamVj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 16) = "dF91bnZlcmlmaWVkX2VtYWlsGBAgASgIEjIKDWpvaW5fdGhyb3R0bGUYESAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 17) = "KAsyGy5yb290LkNvbW11bml0eUpvaW5UaHJvdHRsZSJvChZDb21tdW5pdHlE";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 18) = "ZWxldGVSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 19) = "ZXh0EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 20) = "ZEIGukgDyAEBIhoKGENvbW11bml0eUxpc3RNaW5lUmVxdWVzdCJIChNDb21t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 21) = "dW5pdHlHZXRSZXF1ZXN0EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJvb3Qu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 22) = "Q29tbXVuaXR5VXVpZEIGukgDyAEBIlAKG0NvbW11bml0eUdldEV4dGVuZGVk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 23) = "UmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 24) = "eVV1aWRCBrpIA8gBASJOChlDb21tdW5pdHlHZXRGb3JBcHBSZXF1ZXN0EjEK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 25) = "DGNvbW11bml0eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 26) = "yAEBIksKFkNvbW11bml0eUF0dGFjaFJlcXVlc3QSMQoMY29tbXVuaXR5X2lk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 27) = "GAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQEiSwoWQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 28) = "aXR5RGV0YWNoUmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 29) = "LkNvbW11bml0eVV1aWRCBrpIA8gBASJuChVDb21tdW5pdHlMZWF2ZVJlcXVl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 30) = "c3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSMQoMY29t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 31) = "bXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQEi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 32) = "swEKGUNvbW11bml0eU93bmVyRWRpdFJlcXVlc3QSIgoHY29udGV4dBgBIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 33) = "CzIRLnJvb3QuUm9vdENvbnRleHQSMQoMY29tbXVuaXR5X2lkGAogASgLMhMu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 34) = "cm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQESLQoNb3duZXJfdXNlcl9pZBgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 35) = "IAEoCzIOLnJvb3QuVXNlclV1aWRCBrpIA8gBARIQCghwYXNzd29yZBgMIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 36) = "CUImqgIjUm9vdEFwcC5XZWJBcGkuU2hhcmVkLkdycGMuUmVxdWVzdHNiBnBy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray38<string>, string>(ref _003C_003Ey__InlineArray, 37) = "b3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray38<string>, string>(in _003C_003Ey__InlineArray, 38)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[5]
		{
			RootUuidsReflection.Descriptor,
			ValidateReflection.Descriptor,
			WrappersReflection.Descriptor,
			ContextReflection.Descriptor,
			RootApp.WebApi.Shared.Packets.CommunityReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[11]
		{
			new GeneratedClrTypeInfo(typeof(CommunityCreateRequest), CommunityCreateRequest.Parser, new string[7] { "Context", "Name", "PictureHex", "TemplateType", "IconUploadTokenUri", "RejectUnverifiedEmail", "JoinThrottle" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityEditRequest), CommunityEditRequest.Parser, new string[9] { "Context", "CommunityId", "Name", "PictureHex", "UpdatePicture", "PictureTokenUri", "DefaultChannelId", "RejectUnverifiedEmail", "JoinThrottle" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityDeleteRequest), CommunityDeleteRequest.Parser, new string[2] { "Context", "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityListMineRequest), CommunityListMineRequest.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityGetRequest), CommunityGetRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityGetExtendedRequest), CommunityGetExtendedRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityGetForAppRequest), CommunityGetForAppRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAttachRequest), CommunityAttachRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityDetachRequest), CommunityDetachRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityLeaveRequest), CommunityLeaveRequest.Parser, new string[2] { "Context", "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityOwnerEditRequest), CommunityOwnerEditRequest.Parser, new string[4] { "Context", "CommunityId", "OwnerUserId", "Password" }, null, null, null, null)
		}));
	}
}
