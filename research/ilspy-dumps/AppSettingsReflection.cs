using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.App.Settings;

public static class AppSettingsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AppSettingsReflection()
	{
		_003C_003Ey__InlineArray65<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray65<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Ch9hcHBfc2VydmljZXMvYXBwX3NldHRpbmdzLnByb3RvEghyb290LmFwcBol";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 1) = "YXBwX3NlcnZpY2VzL3NlcnZpY2VzL2FwcF9lbnVtcy5wcm90bxoVY29yZS9y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 2) = "b290X3V1aWRzLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0YW1wLnBy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 3) = "b3RvGh5nb29nbGUvcHJvdG9idWYvd3JhcHBlcnMucHJvdG8iXQoOR2xvYmFs";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 4) = "U2V0dGluZ3MSHQoGYXBwX2lkGAUgASgLMg0ucm9vdC5BcHBVdWlkEiwKBmdy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 5) = "b3VwcxgHIAMoCzIcLnJvb3QuYXBwLkdsb2JhbFNldHRpbmdHcm91cCJqChJH";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 6) = "bG9iYWxTZXR0aW5nR3JvdXASCwoDa2V5GAUgASgJEg0KBXRpdGxlGAYgASgJ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 7) = "EhAKCG9yZGVyX2J5GAcgASgFEiYKBWl0ZW1zGAggAygLMhcucm9vdC5hcHAu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 8) = "R2xvYmFsU2V0dGluZyKvBgoNR2xvYmFsU2V0dGluZxILCgNrZXkYASABKAkS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 9) = "DQoFdGl0bGUYAiABKAkSMQoLZGVzY3JpcHRpb24YAyABKAsyHC5nb29nbGUu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 10) = "cHJvdG9idWYuU3RyaW5nVmFsdWUSEAoIcmVxdWlyZWQYBCABKAgSEAoIb3Jk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 11) = "ZXJfYnkYBSABKAUSFAoMY29uZmlybWF0aW9uGAYgASgJEisKBHRleHQYCiAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 12) = "KAsyGy5yb290LmFwcC5HbG9iYWxTZXR0aW5nVGV4dEgAEi8KBm51bWJlchgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 13) = "IAEoCzIdLnJvb3QuYXBwLkdsb2JhbFNldHRpbmdOdW1iZXJIABIzCghjaGVj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 14) = "a2JveBgMIAEoCzIfLnJvb3QuYXBwLkdsb2JhbFNldHRpbmdDaGVja2JveEgA";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 15) = "EkMKDnJvbGVfb3JfbWVtYmVyGA0gASgLMikucm9vdC5hcHAuR2xvYmFsU2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 16) = "dGluZ1JvbGVPck1lbWJlclBpY2tlckgAEjcKB2NoYW5uZWwYDiABKAsyJC5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 17) = "b290LmFwcC5HbG9iYWxTZXR0aW5nQ2hhbm5lbFBpY2tlckgAEkIKDWNoYW5u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 18) = "ZWxfZ3JvdXAYDyABKAsyKS5yb290LmFwcC5HbG9iYWxTZXR0aW5nQ2hhbm5l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 19) = "bEdyb3VwUGlja2VySAASLwoGc2VsZWN0GBAgASgLMh0ucm9vdC5hcHAuR2xv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 20) = "YmFsU2V0dGluZ1NlbGVjdEgAEjsKCXRpbWVzdGFtcBgRIAEoCzImLnJvb3Qu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 21) = "YXBwLkdsb2JhbFNldHRpbmdUaW1lc3RhbXBQaWNrZXJIABIxCgR0aW1lGBIg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 22) = "ASgLMiEucm9vdC5hcHAuR2xvYmFsU2V0dGluZ1RpbWVQaWNrZXJIABIxCgRk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 23) = "YXRlGBMgASgLMiEucm9vdC5hcHAuR2xvYmFsU2V0dGluZ0RhdGVQaWNrZXJI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 24) = "ABIzCgVjb2xvchgUIAEoCzIiLnJvb3QuYXBwLkdsb2JhbFNldHRpbmdDb2xv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 25) = "clBpY2tlckgAEi8KBmJ1dHRvbhgVIAEoCzIdLnJvb3QuYXBwLkdsb2JhbFNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 26) = "dHRpbmdCdXR0b25IAEIGCgRpdGVtIqIBChFHbG9iYWxTZXR0aW5nVGV4dBIr";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 27) = "CgV2YWx1ZRgFIAEoCzIcLmdvb2dsZS5wcm90b2J1Zi5TdHJpbmdWYWx1ZRIz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 28) = "Cg1kZWZhdWx0X3ZhbHVlGAYgASgLMhwuZ29vZ2xlLnByb3RvYnVmLlN0cmlu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 29) = "Z1ZhbHVlEisKBXJlZ2V4GAcgASgLMhwuZ29vZ2xlLnByb3RvYnVmLlN0cmlu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 30) = "Z1ZhbHVlIloKGkdsb2JhbFNldHRpbmdDaGFubmVsUGlja2VyEhQKDG11bHRp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 31) = "X3NlbGVjdBgFIAEoCBImCgtjaGFubmVsX2lkcxgGIAMoCzIRLnJvb3QuQ2hh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 32) = "bm5lbFV1aWQiagofR2xvYmFsU2V0dGluZ0NoYW5uZWxHcm91cFBpY2tlchIU";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 33) = "CgxtdWx0aV9zZWxlY3QYBSABKAgSMQoRY2hhbm5lbF9ncm91cF9pZHMYBiAD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 34) = "KAsyFi5yb290LkNoYW5uZWxHcm91cFV1aWQitwEKH0dsb2JhbFNldHRpbmdS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 35) = "b2xlT3JNZW1iZXJQaWNrZXISPQoPc2VsZWN0X2JlaGF2aW9yGAUgASgOMiQu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 36) = "cm9vdC5hcHAuUm9sZU9yTWVtYmVyUGlja2VyQmVoYXZpb3ISIAoIdXNlcl9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 37) = "ZHMYBiADKAsyDi5yb290LlVzZXJVdWlkEjMKEmNvbW11bml0eV9yb2xlX2lk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 38) = "cxgHIAMoCzIXLnJvb3QuQ29tbXVuaXR5Um9sZVV1aWQiSQocR2xvYmFsU2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 39) = "dGluZ1RpbWVzdGFtcFBpY2tlchIpCgV2YWx1ZRgFIAEoCzIaLmdvb2dsZS5w";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 40) = "cm90b2J1Zi5UaW1lc3RhbXAiPQoRR2xvYmFsU2V0dGluZ0RhdGUSDAoEeWVh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 41) = "chgEIAEoBRINCgVtb250aBgFIAEoBRILCgNkYXkYBiABKAUiRQoXR2xvYmFs";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 42) = "U2V0dGluZ0RhdGVQaWNrZXISKgoFdmFsdWUYBCABKAsyGy5yb290LmFwcC5H";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 43) = "bG9iYWxTZXR0aW5nRGF0ZSJJChZHbG9iYWxTZXR0aW5nVGltZU9mRGF5Eg0K";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 44) = "BWhvdXJzGAEgASgFEg8KB21pbnV0ZXMYAiABKAUSDwoHc2Vjb25kcxgDIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 45) = "BSKDAQoXR2xvYmFsU2V0dGluZ1RpbWVQaWNrZXISLwoFdmFsdWUYBSABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 46) = "IC5yb290LmFwcC5HbG9iYWxTZXR0aW5nVGltZU9mRGF5EjcKDWRlZmF1bHRf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 47) = "dmFsdWUYBiABKAsyIC5yb290LmFwcC5HbG9iYWxTZXR0aW5nVGltZU9mRGF5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 48) = "InwKGEdsb2JhbFNldHRpbmdDb2xvclBpY2tlchIrCgV2YWx1ZRgFIAEoCzIc";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 49) = "Lmdvb2dsZS5wcm90b2J1Zi5TdHJpbmdWYWx1ZRIzCg1kZWZhdWx0X3ZhbHVl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 50) = "GAYgASgLMhwuZ29vZ2xlLnByb3RvYnVmLlN0cmluZ1ZhbHVlIoUCChNHbG9i";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 51) = "YWxTZXR0aW5nTnVtYmVyEisKBXZhbHVlGAUgASgLMhwuZ29vZ2xlLnByb3Rv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 52) = "YnVmLkRvdWJsZVZhbHVlEi8KCW1pbl92YWx1ZRgGIAEoCzIcLmdvb2dsZS5w";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 53) = "cm90b2J1Zi5Eb3VibGVWYWx1ZRIvCgltYXhfdmFsdWUYByABKAsyHC5nb29n";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 54) = "bGUucHJvdG9idWYuRG91YmxlVmFsdWUSKgoEc3RlcBgIIAEoCzIcLmdvb2ds";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 55) = "ZS5wcm90b2J1Zi5Eb3VibGVWYWx1ZRIzCg1kZWZhdWx0X3ZhbHVlGAkgASgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 56) = "MhwuZ29vZ2xlLnByb3RvYnVmLkRvdWJsZVZhbHVlIlkKFUdsb2JhbFNldHRp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 57) = "bmdDaGVja2JveBINCgV2YWx1ZRgFIAEoCBIxCg1kZWZhdWx0X3ZhbHVlGAYg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 58) = "ASgLMhouZ29vZ2xlLnByb3RvYnVmLkJvb2xWYWx1ZSJxChNHbG9iYWxTZXR0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 59) = "aW5nU2VsZWN0EhQKDG11bHRpX3NlbGVjdBgFIAEoCBIOCgZ2YWx1ZXMYBiAD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 60) = "KAkSNAoHb3B0aW9ucxgHIAMoCzIjLnJvb3QuYXBwLkdsb2JhbFNldHRpbmdT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 61) = "ZWxlY3RPcHRpb24iNwoZR2xvYmFsU2V0dGluZ1NlbGVjdE9wdGlvbhILCgNr";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 62) = "ZXkYBSABKAkSDQoFdmFsdWUYBiABKAkiJAoTR2xvYmFsU2V0dGluZ0J1dHRv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 63) = "bhINCgV0aXRsZRgFIAEoCUIXqgIUUm9vdEFwcC5BcHAuU2V0dGluZ3NiBnBy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray65<string>, string>(ref _003C_003Ey__InlineArray, 64) = "b3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray65<string>, string>(in _003C_003Ey__InlineArray, 65)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[4]
		{
			AppEnumsReflection.Descriptor,
			RootUuidsReflection.Descriptor,
			TimestampReflection.Descriptor,
			WrappersReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[18]
		{
			new GeneratedClrTypeInfo(typeof(GlobalSettings), GlobalSettings.Parser, new string[2] { "AppId", "Groups" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingGroup), GlobalSettingGroup.Parser, new string[4] { "Key", "Title", "OrderBy", "Items" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSetting), GlobalSetting.Parser, new string[18]
			{
				"Key", "Title", "Description", "Required", "OrderBy", "Confirmation", "Text", "Number", "Checkbox", "RoleOrMember",
				"Channel", "ChannelGroup", "Select", "Timestamp", "Time", "Date", "Color", "Button"
			}, new string[1] { "Item" }, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingText), GlobalSettingText.Parser, new string[3] { "Value", "DefaultValue", "Regex" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingChannelPicker), GlobalSettingChannelPicker.Parser, new string[2] { "MultiSelect", "ChannelIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingChannelGroupPicker), GlobalSettingChannelGroupPicker.Parser, new string[2] { "MultiSelect", "ChannelGroupIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingRoleOrMemberPicker), GlobalSettingRoleOrMemberPicker.Parser, new string[3] { "SelectBehavior", "UserIds", "CommunityRoleIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingTimestampPicker), GlobalSettingTimestampPicker.Parser, new string[1] { "Value" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingDate), GlobalSettingDate.Parser, new string[3] { "Year", "Month", "Day" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingDatePicker), GlobalSettingDatePicker.Parser, new string[1] { "Value" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingTimeOfDay), GlobalSettingTimeOfDay.Parser, new string[3] { "Hours", "Minutes", "Seconds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingTimePicker), GlobalSettingTimePicker.Parser, new string[2] { "Value", "DefaultValue" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingColorPicker), GlobalSettingColorPicker.Parser, new string[2] { "Value", "DefaultValue" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingNumber), GlobalSettingNumber.Parser, new string[5] { "Value", "MinValue", "MaxValue", "Step", "DefaultValue" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingCheckbox), GlobalSettingCheckbox.Parser, new string[2] { "Value", "DefaultValue" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingSelect), GlobalSettingSelect.Parser, new string[3] { "MultiSelect", "Values", "Options" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingSelectOption), GlobalSettingSelectOption.Parser, new string[2] { "Key", "Value" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalSettingButton), GlobalSettingButton.Parser, new string[1] { "Title" }, null, null, null, null)
		}));
	}
}
