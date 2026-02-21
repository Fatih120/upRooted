using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace RootApp.WebApi.Shared.Grpc;

public static class PermissionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static PermissionReflection()
	{
		_003C_003Ey__InlineArray59<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray59<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Chd3ZWJhcGkvcGVybWlzc2lvbi5wcm90bxIEcm9vdBoeZ29vZ2xlL3Byb3Rv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 1) = "YnVmL3dyYXBwZXJzLnByb3RvItMFChFDaGFubmVsUGVybWlzc2lvbhIcChRj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 2) = "aGFubmVsX2Z1bGxfY29udHJvbBgKIAEoCBIUCgxjaGFubmVsX3ZpZXcYDCAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 3) = "KAgSIgoaY2hhbm5lbF91c2VfZXh0ZXJuYWxfZW1vamkYDSABKAgSHgoWY2hh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 4) = "bm5lbF9jcmVhdGVfbWVzc2FnZRgOIAEoCBIkChxjaGFubmVsX2RlbGV0ZV9t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 5) = "ZXNzYWdlX290aGVyGA8gASgIEiYKHmNoYW5uZWxfbWFuYWdlX3Bpbm5lZF9t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 6) = "ZXNzYWdlcxgQIAEoCBIkChxjaGFubmVsX3ZpZXdfbWVzc2FnZV9oaXN0b3J5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 7) = "GBEgASgIEikKIWNoYW5uZWxfY3JlYXRlX21lc3NhZ2VfYXR0YWNobWVudBgS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 8) = "IAEoCBImCh5jaGFubmVsX2NyZWF0ZV9tZXNzYWdlX21lbnRpb24YEyABKAgS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 9) = "JwofY2hhbm5lbF9jcmVhdGVfbWVzc2FnZV9yZWFjdGlvbhgUIAEoCBIjChtj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 10) = "aGFubmVsX21ha2VfbWVzc2FnZV9wdWJsaWMYFSABKAgSHwoXY2hhbm5lbF9t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 11) = "b3ZlX3VzZXJfb3RoZXIYFiABKAgSGgoSY2hhbm5lbF92b2ljZV90YWxrGBcg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 12) = "ASgIEiAKGGNoYW5uZWxfdm9pY2VfbXV0ZV9vdGhlchgYIAEoCBIiChpjaGFu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 13) = "bmVsX3ZvaWNlX2RlYWZlbl9vdGhlchgZIAEoCBIaChJjaGFubmVsX3ZvaWNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 14) = "X2tpY2sYGiABKAgSIgoaY2hhbm5lbF92aWRlb19zdHJlYW1fbWVkaWEYGyAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 15) = "KAgSGwoTY2hhbm5lbF9jcmVhdGVfZmlsZRgcIAEoCBIcChRjaGFubmVsX21h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 16) = "bmFnZV9maWxlcxgdIAEoCBIZChFjaGFubmVsX3ZpZXdfZmlsZRgeIAEoCBIY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 17) = "ChBjaGFubmVsX2FwcF9raWNrGB8gASgIIqYKChhDaGFubmVsT3ZlcmxheVBl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 18) = "cm1pc3Npb24SOAoUY2hhbm5lbF9mdWxsX2NvbnRyb2wYCiABKAsyGi5nb29n";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 19) = "bGUucHJvdG9idWYuQm9vbFZhbHVlEjAKDGNoYW5uZWxfdmlldxgMIAEoCzIa";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 20) = "Lmdvb2dsZS5wcm90b2J1Zi5Cb29sVmFsdWUSPgoaY2hhbm5lbF91c2VfZXh0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 21) = "ZXJuYWxfZW1vamkYDSABKAsyGi5nb29nbGUucHJvdG9idWYuQm9vbFZhbHVl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 22) = "EjoKFmNoYW5uZWxfY3JlYXRlX21lc3NhZ2UYDiABKAsyGi5nb29nbGUucHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 23) = "dG9idWYuQm9vbFZhbHVlEkAKHGNoYW5uZWxfZGVsZXRlX21lc3NhZ2Vfb3Ro";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 24) = "ZXIYDyABKAsyGi5nb29nbGUucHJvdG9idWYuQm9vbFZhbHVlEkIKHmNoYW5u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 25) = "ZWxfbWFuYWdlX3Bpbm5lZF9tZXNzYWdlcxgQIAEoCzIaLmdvb2dsZS5wcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 26) = "b2J1Zi5Cb29sVmFsdWUSQAocY2hhbm5lbF92aWV3X21lc3NhZ2VfaGlzdG9y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 27) = "eRgRIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5Cb29sVmFsdWUSRQohY2hhbm5l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 28) = "bF9jcmVhdGVfbWVzc2FnZV9hdHRhY2htZW50GBIgASgLMhouZ29vZ2xlLnBy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 29) = "b3RvYnVmLkJvb2xWYWx1ZRJCCh5jaGFubmVsX2NyZWF0ZV9tZXNzYWdlX21l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 30) = "bnRpb24YEyABKAsyGi5nb29nbGUucHJvdG9idWYuQm9vbFZhbHVlEkMKH2No";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 31) = "YW5uZWxfY3JlYXRlX21lc3NhZ2VfcmVhY3Rpb24YFCABKAsyGi5nb29nbGUu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 32) = "cHJvdG9idWYuQm9vbFZhbHVlEj8KG2NoYW5uZWxfbWFrZV9tZXNzYWdlX3B1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 33) = "YmxpYxgVIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5Cb29sVmFsdWUSOwoXY2hh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 34) = "bm5lbF9tb3ZlX3VzZXJfb3RoZXIYFiABKAsyGi5nb29nbGUucHJvdG9idWYu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 35) = "Qm9vbFZhbHVlEjYKEmNoYW5uZWxfdm9pY2VfdGFsaxgXIAEoCzIaLmdvb2ds";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 36) = "ZS5wcm90b2J1Zi5Cb29sVmFsdWUSPAoYY2hhbm5lbF92b2ljZV9tdXRlX290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 37) = "aGVyGBggASgLMhouZ29vZ2xlLnByb3RvYnVmLkJvb2xWYWx1ZRI+ChpjaGFu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 38) = "bmVsX3ZvaWNlX2RlYWZlbl9vdGhlchgZIAEoCzIaLmdvb2dsZS5wcm90b2J1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 39) = "Zi5Cb29sVmFsdWUSNgoSY2hhbm5lbF92b2ljZV9raWNrGBogASgLMhouZ29v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 40) = "Z2xlLnByb3RvYnVmLkJvb2xWYWx1ZRI+ChpjaGFubmVsX3ZpZGVvX3N0cmVh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 41) = "bV9tZWRpYRgbIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5Cb29sVmFsdWUSNwoT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 42) = "Y2hhbm5lbF9jcmVhdGVfZmlsZRgcIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5C";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 43) = "b29sVmFsdWUSOAoUY2hhbm5lbF9tYW5hZ2VfZmlsZXMYHSABKAsyGi5nb29n";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 44) = "bGUucHJvdG9idWYuQm9vbFZhbHVlEjUKEWNoYW5uZWxfdmlld19maWxlGB4g";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 45) = "ASgLMhouZ29vZ2xlLnByb3RvYnVmLkJvb2xWYWx1ZRI0ChBjaGFubmVsX2Fw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 46) = "cF9raWNrGB8gASgLMhouZ29vZ2xlLnByb3RvYnVmLkJvb2xWYWx1ZSLsAwoT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 47) = "Q29tbXVuaXR5UGVybWlzc2lvbhIiChpjb21tdW5pdHlfbWFuYWdlX2NvbW11";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 48) = "bml0eRgKIAEoCBIeChZjb21tdW5pdHlfbWFuYWdlX3JvbGVzGAsgASgIEh8K";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 49) = "F2NvbW11bml0eV9tYW5hZ2VfZW1vamlzGAwgASgIEiIKGmNvbW11bml0eV9t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 50) = "YW5hZ2VfYXVkaXRfbG9nGA0gASgIEh8KF2NvbW11bml0eV9jcmVhdGVfaW52";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 51) = "aXRlGA4gASgIEiAKGGNvbW11bml0eV9tYW5hZ2VfaW52aXRlcxgPIAEoCBIc";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 52) = "ChRjb21tdW5pdHlfY3JlYXRlX2JhbhgQIAEoCBIdChVjb21tdW5pdHlfbWFu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 53) = "YWdlX2JhbnMYESABKAgSHgoWY29tbXVuaXR5X2Z1bGxfY29udHJvbBgSIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 54) = "CBIWCg5jb21tdW5pdHlfa2ljaxgTIAEoCBIkChxjb21tdW5pdHlfY2hhbmdl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 55) = "X215X25pY2tuYW1lGBQgASgIEicKH2NvbW11bml0eV9jaGFuZ2Vfb3RoZXJf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 56) = "bmlja25hbWUYFSABKAgSJgoeY29tbXVuaXR5X2NyZWF0ZV9jaGFubmVsX2dy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 57) = "b3VwGBYgASgIEh0KFWNvbW11bml0eV9tYW5hZ2VfYXBwcxgXIAEoCEIdqgIa";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray59<string>, string>(ref _003C_003Ey__InlineArray, 58) = "Um9vdEFwcC5XZWJBcGkuU2hhcmVkLkdycGNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray59<string>, string>(in _003C_003Ey__InlineArray, 59)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[1] { WrappersReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(ChannelPermission), ChannelPermission.Parser, new string[21]
			{
				"ChannelFullControl", "ChannelView", "ChannelUseExternalEmoji", "ChannelCreateMessage", "ChannelDeleteMessageOther", "ChannelManagePinnedMessages", "ChannelViewMessageHistory", "ChannelCreateMessageAttachment", "ChannelCreateMessageMention", "ChannelCreateMessageReaction",
				"ChannelMakeMessagePublic", "ChannelMoveUserOther", "ChannelVoiceTalk", "ChannelVoiceMuteOther", "ChannelVoiceDeafenOther", "ChannelVoiceKick", "ChannelVideoStreamMedia", "ChannelCreateFile", "ChannelManageFiles", "ChannelViewFile",
				"ChannelAppKick"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelOverlayPermission), ChannelOverlayPermission.Parser, new string[21]
			{
				"ChannelFullControl", "ChannelView", "ChannelUseExternalEmoji", "ChannelCreateMessage", "ChannelDeleteMessageOther", "ChannelManagePinnedMessages", "ChannelViewMessageHistory", "ChannelCreateMessageAttachment", "ChannelCreateMessageMention", "ChannelCreateMessageReaction",
				"ChannelMakeMessagePublic", "ChannelMoveUserOther", "ChannelVoiceTalk", "ChannelVoiceMuteOther", "ChannelVoiceDeafenOther", "ChannelVoiceKick", "ChannelVideoStreamMedia", "ChannelCreateFile", "ChannelManageFiles", "ChannelViewFile",
				"ChannelAppKick"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityPermission), CommunityPermission.Parser, new string[14]
			{
				"CommunityManageCommunity", "CommunityManageRoles", "CommunityManageEmojis", "CommunityManageAuditLog", "CommunityCreateInvite", "CommunityManageInvites", "CommunityCreateBan", "CommunityManageBans", "CommunityFullControl", "CommunityKick",
				"CommunityChangeMyNickname", "CommunityChangeOtherNickname", "CommunityCreateChannelGroup", "CommunityManageApps"
			}, null, null, null, null)
		}));
	}
}
