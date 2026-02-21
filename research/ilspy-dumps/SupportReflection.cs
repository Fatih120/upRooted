using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class SupportReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SupportReflection()
	{
		_003C_003Ey__InlineArray14<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray14<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Ch13ZWJhcGkvcmVxdWVzdHMvc3VwcG9ydC5wcm90bxIEcm9vdBoVY29yZS9y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 1) = "b290X3V1aWRzLnByb3RvGh5nb29nbGUvcHJvdG9idWYvd3JhcHBlcnMucHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 2) = "dG8aFHdlYmFwaS9jb250ZXh0LnByb3RvIsgDChRTdXBwb3J0Q3JlYXRlUmVx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 3) = "dWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIpCgxj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 4) = "b21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1aWQSFQoNYWxs";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 5) = "b3dfY29udGFjdBgLIAEoCBIPCgdzdWJqZWN0GBQgASgJEg8KB2NvbW1lbnQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 6) = "FSABKAkSLgoIY2F0ZWdvcnkYHiABKAsyHC5nb29nbGUucHJvdG9idWYuU3Ry";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 7) = "aW5nVmFsdWUSLgoIcGxhdGZvcm0YHyABKAsyHC5nb29nbGUucHJvdG9idWYu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 8) = "U3RyaW5nVmFsdWUSMgoMYXJjaGl0ZWN0dXJlGCAgASgLMhwuZ29vZ2xlLnBy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 9) = "b3RvYnVmLlN0cmluZ1ZhbHVlEjAKCm9zX3ZlcnNpb24YISABKAsyHC5nb29n";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 10) = "bGUucHJvdG9idWYuU3RyaW5nVmFsdWUSNAoOY2xpZW50X3ZlcnNpb24YIiAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 11) = "KAsyHC5nb29nbGUucHJvdG9idWYuU3RyaW5nVmFsdWUSLAoGc291cmNlGCMg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 12) = "ASgLMhwuZ29vZ2xlLnByb3RvYnVmLlN0cmluZ1ZhbHVlQiaqAiNSb290QXBw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray14<string>, string>(ref _003C_003Ey__InlineArray, 13) = "LldlYkFwaS5TaGFyZWQuR3JwYy5SZXF1ZXN0c2IGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray14<string>, string>(in _003C_003Ey__InlineArray, 14)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			WrappersReflection.Descriptor,
			ContextReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[1]
		{
			new GeneratedClrTypeInfo(typeof(SupportCreateRequest), SupportCreateRequest.Parser, new string[11]
			{
				"Context", "CommunityId", "AllowContact", "Subject", "Comment", "Category", "Platform", "Architecture", "OsVersion", "ClientVersion",
				"Source"
			}, null, null, null, null)
		}));
	}
}
