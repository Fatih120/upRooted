using System;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public static class PingReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static PingReflection()
	{
		_003C_003Ey__InlineArray6<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray6<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Chl3ZWJhcGkvcGFja2V0cy9waW5nLnByb3RvEgRyb290GhJ3ZWJhcGkvZW51";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 1) = "bXMucHJvdG8iTAoKUGluZ1BhY2tldBIlCgtwYWNrZXRfdHlwZRgBIAEoDjIQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 2) = "LnJvb3QuUGFja2V0VHlwZRIXCg9zZXF1ZW5jZV9udW1iZXIYAiABKAMiPAoT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 3) = "SHViU2VydmVyTW92ZVBhY2tldBIlCgtwYWNrZXRfdHlwZRgBIAEoDjIQLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 4) = "b3QuUGFja2V0VHlwZUIgqgIdUm9vdEFwcC5XZWJBcGkuU2hhcmVkLlBhY2tl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray6<string>, string>(ref _003C_003Ey__InlineArray, 5) = "dHNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray6<string>, string>(in _003C_003Ey__InlineArray, 6)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[1] { EnumsReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(PingPacket), PingPacket.Parser, new string[2] { "PacketType", "SequenceNumber" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(HubServerMovePacket), HubServerMovePacket.Parser, new string[1] { "PacketType" }, null, null, null, null)
		}));
	}
}
