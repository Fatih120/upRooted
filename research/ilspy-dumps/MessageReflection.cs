using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace RootApp.Core;

public static class MessageReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MessageReflection()
	{
		_003C_003Ey__InlineArray7<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray7<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 0) = "ChJjb3JlL21lc3NhZ2UucHJvdG8SBHJvb3QaH2dvb2dsZS9wcm90b2J1Zi90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 1) = "aW1lc3RhbXAucHJvdG8iSQoKTWVzc2FnZVVyaRILCgN1cmkYASABKAkSLgoK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 2) = "YXR0YWNobWVudBgCIAEoCzIaLnJvb3QuTWVzc2FnZVVyaUF0dGFjaG1lbnQi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 3) = "egoUTWVzc2FnZVVyaUF0dGFjaG1lbnQSEQoJZmlsZV9uYW1lGAEgASgJEhEK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 4) = "CW1pbWVfdHlwZRgCIAEoCRIOCgZsZW5ndGgYAyABKAMSLAoIbW9kaWZpZWQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 5) = "BCABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wQg+qAgxSb290QXBw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray7<string>, string>(ref _003C_003Ey__InlineArray, 6) = "LkNvcmViBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray7<string>, string>(in _003C_003Ey__InlineArray, 7)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(MessageUri), MessageUri.Parser, new string[2] { "Uri", "Attachment" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessageUriAttachment), MessageUriAttachment.Parser, new string[4] { "FileName", "MimeType", "Length", "Modified" }, null, null, null, null)
		}));
	}
}
