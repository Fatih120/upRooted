using System;
using Google.Protobuf.Reflection;
using RootApp.App;
using RootApp.App.Settings;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class CommunityAppReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CommunityAppReflection()
	{
		_003C_003Ey__InlineArray73<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray73<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiN3ZWJhcGkvcmVxdWVzdHMvY29tbXVuaXR5X2FwcC5wcm90bxIEcm9vdBol";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 1) = "YXBwX3NlcnZpY2VzL3NlcnZpY2VzL2FwcF9lbnVtcy5wcm90bxofYXBwX3Nl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 2) = "cnZpY2VzL2FwcF9zZXR0aW5ncy5wcm90bxoVY29yZS9yb290X3V1aWRzLnBy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 3) = "b3RvGhR3ZWJhcGkvY29udGV4dC5wcm90bxoXd2ViYXBpL3Blcm1pc3Npb24u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 4) = "cHJvdG8aHXdlYmFwaS9yZXF1ZXN0cy9jaGFubmVsLnByb3RvGhNjb3JlL3Zh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 5) = "bGlkYXRlLnByb3RvIoUBChZDb21tdW5pdHlBcHBHZXRSZXF1ZXN0EjEKDGNv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 6) = "bW11bml0eV9pZBgEIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 7) = "EjgKEGNvbW11bml0eV9hcHBfaWQYBSABKAsyFi5yb290LkNvbW11bml0eUFw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 8) = "cFV1aWRCBrpIA8gBASJxChdDb21tdW5pdHlBcHBMaXN0UmVxdWVzdBIxCgxj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 9) = "b21tdW5pdHlfaWQYBCABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 10) = "ARIjCghhcHBfdHlwZRgFIAEoDjIRLnJvb3QuYXBwLkFwcFR5cGUivQIKFkNv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 11) = "bW11bml0eUFwcEFkZFJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3Qu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 12) = "Um9vdENvbnRleHQSMQoMY29tbXVuaXR5X2lkGAQgASgLMhMucm9vdC5Db21t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 13) = "dW5pdHlVdWlkQga6SAPIAQESJQoGYXBwX2lkGAUgASgLMg0ucm9vdC5BcHBV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 14) = "dWlkQga6SAPIAQESKwoHY2hhbm5lbBgGIAEoCzIaLnJvb3QuQ2hhbm5lbENy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 15) = "ZWF0ZVJlcXVlc3QSMQoPZ2xvYmFsX3NldHRpbmdzGAcgASgLMhgucm9vdC5h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 16) = "cHAuR2xvYmFsU2V0dGluZ3MSLAoOYXBwX3ZlcnNpb25faWQYDyABKAsyFC5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 17) = "b290LkFwcFZlcnNpb25VdWlkEhcKD3R1cm5zdGlsZV90b2tlbhgQIAEoCSLD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 18) = "AgogQ29tbXVuaXR5QXBwVXBkYXRlVmVyc2lvblJlcXVlc3QSIgoHY29udGV4";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 19) = "dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSMQoMY29tbXVuaXR5X2lkGAQg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 20) = "ASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQESJQoGYXBwX2lkGAUg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 21) = "ASgLMg0ucm9vdC5BcHBVdWlkQga6SAPIAQESOAoQY29tbXVuaXR5X2FwcF9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 22) = "ZBgGIAEoCzIWLnJvb3QuQ29tbXVuaXR5QXBwVXVpZEIGukgDyAEBEjEKD2ds";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 23) = "b2JhbF9zZXR0aW5ncxgHIAEoCzIYLnJvb3QuYXBwLkdsb2JhbFNldHRpbmdz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 24) = "EjQKDmFwcF92ZXJzaW9uX2lkGAggASgLMhQucm9vdC5BcHBWZXJzaW9uVXVp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 25) = "ZEIGukgDyAEBItMBChlDb21tdW5pdHlBcHBSZW1vdmVSZXF1ZXN0EiIKB2Nv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 26) = "bnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0EjEKDGNvbW11bml0eV9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 27) = "ZBgEIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEiUKBmFwcF9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 28) = "ZBgFIAEoCzINLnJvb3QuQXBwVXVpZEIGukgDyAEBEjgKEGNvbW11bml0eV9h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 29) = "cHBfaWQYBiABKAsyFi5yb290LkNvbW11bml0eUFwcFV1aWRCBrpIA8gBASL5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 30) = "AQorQ29tbXVuaXR5QXBwU2V0QXBwT3JnYW5pemF0aW9uQWNjZXNzUmVxdWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 31) = "dBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIxCgxjb21t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 32) = "dW5pdHlfaWQYBCABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARIl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 33) = "CgZhcHBfaWQYBSABKAsyDS5yb290LkFwcFV1aWRCBrpIA8gBARI4ChBjb21t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 34) = "dW5pdHlfYXBwX2lkGAYgASgLMhYucm9vdC5Db21tdW5pdHlBcHBVdWlkQga6";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 35) = "SAPIAQESEgoKY2FuX2FjY2VzcxgHIAEoCCKMAgoeQ29tbXVuaXR5QXBwU2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 36) = "U2V0dGluZ3NSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290LlJvb3RD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 37) = "b250ZXh0EjEKDGNvbW11bml0eV9pZBgEIAEoCzITLnJvb3QuQ29tbXVuaXR5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 38) = "VXVpZEIGukgDyAEBEiUKBmFwcF9pZBgFIAEoCzINLnJvb3QuQXBwVXVpZEIG";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 39) = "ukgDyAEBEjgKEGNvbW11bml0eV9hcHBfaWQYBiABKAsyFi5yb290LkNvbW11";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 40) = "bml0eUFwcFV1aWRCBrpIA8gBARIyCghzZXR0aW5ncxgHIAEoCzIYLnJvb3Qu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 41) = "YXBwLkdsb2JhbFNldHRpbmdzQga6SAPIAQEi4wEKHENvbW11bml0eUFwcFNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 42) = "dEJ1dHRvblJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 43) = "bnRleHQSMQoMY29tbXVuaXR5X2lkGAQgASgLMhMucm9vdC5Db21tdW5pdHlV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 44) = "dWlkQga6SAPIAQESJQoGYXBwX2lkGAUgASgLMg0ucm9vdC5BcHBVdWlkQga6";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 45) = "SAPIAQESOAoQY29tbXVuaXR5X2FwcF9pZBgGIAEoCzIWLnJvb3QuQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 46) = "aXR5QXBwVXVpZEIGukgDyAEBEgsKA2tleRgHIAEoCSK0AQoeQ29tbXVuaXR5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 47) = "QXBwR2V0U2V0dGluZ3NSZXF1ZXN0EjEKDGNvbW11bml0eV9pZBgEIAEoCzIT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 48) = "LnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEiUKBmFwcF9pZBgFIAEoCzIN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 49) = "LnJvb3QuQXBwVXVpZEIGukgDyAEBEjgKEGNvbW11bml0eV9hcHBfaWQYBiAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 50) = "KAsyFi5yb290LkNvbW11bml0eUFwcFV1aWRCBrpIA8gBASLmAQocQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 51) = "aXR5QXBwU2V0UmF0aW5nUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 52) = "dC5Sb290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYBCABKAsyEy5yb290LkNv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 53) = "bW11bml0eVV1aWRCBrpIA8gBARIlCgZhcHBfaWQYBSABKAsyDS5yb290LkFw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 54) = "cFV1aWRCBrpIA8gBARI4ChBjb21tdW5pdHlfYXBwX2lkGAYgASgLMhYucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 55) = "dC5Db21tdW5pdHlBcHBVdWlkQga6SAPIAQESDgoGcmF0aW5nGAcgASgFIroB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 56) = "CiRDb21tdW5pdHlBcHBMaXN0Q2hhbm5lbEdyb3Vwc1JlcXVlc3QSMQoMY29t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 57) = "bXVuaXR5X2lkGAQgASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQES";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 58) = "JQoGYXBwX2lkGAUgASgLMg0ucm9vdC5BcHBVdWlkQga6SAPIAQESOAoQY29t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 59) = "bXVuaXR5X2FwcF9pZBgGIAEoCzIWLnJvb3QuQ29tbXVuaXR5QXBwVXVpZEIG";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 60) = "ukgDyAEBIoYBCipDb21tdW5pdHlBcHBMaXN0Q2hhbm5lbEdyb3Vwc0ZvckFk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 61) = "ZFJlcXVlc3QSMQoMY29tbXVuaXR5X2lkGAQgASgLMhMucm9vdC5Db21tdW5p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 62) = "dHlVdWlkQga6SAPIAQESJQoGYXBwX2lkGAUgASgLMg0ucm9vdC5BcHBVdWlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 63) = "Qga6SAPIAQEijAIKIUNvbW11bml0eUFwcFNldERldlNldHRpbmdzUmVxdWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 64) = "dBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIpCgxjb21t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 65) = "dW5pdHlfaWQYBCABKAsyEy5yb290LkNvbW11bml0eVV1aWQSKgoIc2V0dGlu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 66) = "Z3MYBSABKAsyGC5yb290LmFwcC5HbG9iYWxTZXR0aW5ncxI3ChRjb21tdW5p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 67) = "dHlfcGVybWlzc2lvbhgGIAEoCzIZLnJvb3QuQ29tbXVuaXR5UGVybWlzc2lv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 68) = "bhIzChJjaGFubmVsX3Blcm1pc3Npb24YByABKAsyFy5yb290LkNoYW5uZWxQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 69) = "ZXJtaXNzaW9uIm4KHUNvbW11bml0eUFwcEluaXRpYWxpemVSZXF1ZXN0EiIK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 70) = "B2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0EikKDGNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 71) = "eV9pZBgEIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEImqgIjUm9vdEFwcC5X";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 72) = "ZWJBcGkuU2hhcmVkLkdycGMuUmVxdWVzdHNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray73<string>, string>(in _003C_003Ey__InlineArray, 73)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[7]
		{
			AppEnumsReflection.Descriptor,
			AppSettingsReflection.Descriptor,
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			PermissionReflection.Descriptor,
			ChannelReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[14]
		{
			new GeneratedClrTypeInfo(typeof(CommunityAppGetRequest), CommunityAppGetRequest.Parser, new string[2] { "CommunityId", "CommunityAppId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppListRequest), CommunityAppListRequest.Parser, new string[2] { "CommunityId", "AppType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppAddRequest), CommunityAppAddRequest.Parser, new string[7] { "Context", "CommunityId", "AppId", "Channel", "GlobalSettings", "AppVersionId", "TurnstileToken" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppUpdateVersionRequest), CommunityAppUpdateVersionRequest.Parser, new string[6] { "Context", "CommunityId", "AppId", "CommunityAppId", "GlobalSettings", "AppVersionId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppRemoveRequest), CommunityAppRemoveRequest.Parser, new string[4] { "Context", "CommunityId", "AppId", "CommunityAppId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppSetAppOrganizationAccessRequest), CommunityAppSetAppOrganizationAccessRequest.Parser, new string[5] { "Context", "CommunityId", "AppId", "CommunityAppId", "CanAccess" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppSetSettingsRequest), CommunityAppSetSettingsRequest.Parser, new string[5] { "Context", "CommunityId", "AppId", "CommunityAppId", "Settings" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppSetButtonRequest), CommunityAppSetButtonRequest.Parser, new string[5] { "Context", "CommunityId", "AppId", "CommunityAppId", "Key" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppGetSettingsRequest), CommunityAppGetSettingsRequest.Parser, new string[3] { "CommunityId", "AppId", "CommunityAppId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppSetRatingRequest), CommunityAppSetRatingRequest.Parser, new string[5] { "Context", "CommunityId", "AppId", "CommunityAppId", "Rating" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppListChannelGroupsRequest), CommunityAppListChannelGroupsRequest.Parser, new string[3] { "CommunityId", "AppId", "CommunityAppId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppListChannelGroupsForAddRequest), CommunityAppListChannelGroupsForAddRequest.Parser, new string[2] { "CommunityId", "AppId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppSetDevSettingsRequest), CommunityAppSetDevSettingsRequest.Parser, new string[5] { "Context", "CommunityId", "Settings", "CommunityPermission", "ChannelPermission" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppInitializeRequest), CommunityAppInitializeRequest.Parser, new string[2] { "Context", "CommunityId" }, null, null, null, null)
		}));
	}
}
