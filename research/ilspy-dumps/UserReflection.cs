using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.Core.Validate;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class UserReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static UserReflection()
	{
		_003C_003Ey__InlineArray77<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray77<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Chp3ZWJhcGkvcmVxdWVzdHMvdXNlci5wcm90bxIEcm9vdBoVY29yZS9yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 1) = "X3V1aWRzLnByb3RvGh5nb29nbGUvcHJvdG9idWYvd3JhcHBlcnMucHJvdG8a";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 2) = "FHdlYmFwaS9jb250ZXh0LnByb3RvGhJ3ZWJhcGkvZW51bXMucHJvdG8aE2Nv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 3) = "cmUvdmFsaWRhdGUucHJvdG8iFAoSVXNlckdldFNlbGZSZXF1ZXN0IhwKGlVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 4) = "ZXJHZXROZXdIdWJzZXJ2ZXJSZXF1ZXN0IhQKElVzZXJTaWduVXBSZXNwb25z";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 5) = "ZSIZChdVc2VyU2V0UGFzc3dvcmRSZXNwb25zZSIZChdVc2VyU2V0VXNlcm5h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 6) = "bWVSZXNwb25zZSIWChRVc2VyU2V0RW1haWxSZXNwb25zZSIVChNVc2VyU2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 7) = "Tm90ZVJlc3BvbnNlIo0BChJVc2VyU2V0Tm90ZVJlcXVlc3QSIgoHY29udGV4";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 8) = "dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSJwoHdXNlcl9pZBgKIAEoCzIO";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 9) = "LnJvb3QuVXNlclV1aWRCBrpIA8gBARIqCgRub3RlGAsgASgLMhwuZ29vZ2xl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 10) = "LnByb3RvYnVmLlN0cmluZ1ZhbHVlIk4KFlVzZXJTZXRVc2VybmFtZVJlcXVl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 11) = "c3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSEAoIdXNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 12) = "cm5hbWUYCiABKAkiZwoZVXNlckZvcmdvdFBhc3N3b3JkUmVxdWVzdBIiCgdj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 13) = "b250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBINCgVlbWFpbBgKIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 14) = "CRIXCg90dXJuc3RpbGVfdG9rZW4YCyABKAkiZwoZVXNlckZvcmdvdFVzZXJu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 15) = "YW1lUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 16) = "dBINCgVlbWFpbBgKIAEoCRIXCg90dXJuc3RpbGVfdG9rZW4YCyABKAkikQEK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 17) = "GFVzZXJSZXNldFBhc3N3b3JkUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 18) = "cm9vdC5Sb290Q29udGV4dBINCgVlbWFpbBgKIAEoCRITCgtyZXNldF90b2tl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 19) = "bhgLIAEoCRIUCgxuZXdfcGFzc3dvcmQYDCABKAkSFwoPdHVybnN0aWxlX3Rv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 20) = "a2VuGA0gASgJImgKFlVzZXJTZXRQYXNzd29yZFJlcXVlc3QSIgoHY29udGV4";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 21) = "dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSFAoMb2xkX3Bhc3N3b3JkGAog";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 22) = "ASgJEhQKDG5ld19wYXNzd29yZBgLIAEoCSJaChNVc2VyU2V0RW1haWxSZXF1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 23) = "ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0Eg0KBWVt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 24) = "YWlsGAogASgJEhAKCHBhc3N3b3JkGAsgASgJImQKI1VzZXJTZXRFbWFpbFZl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 25) = "cmlmaWNhdGlvbkNvZGVSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 26) = "LlJvb3RDb250ZXh0EhkKEXZlcmlmaWNhdGlvbl9jb2RlGAogASgJImUKJlVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 27) = "ZXJSZXNlbmRFbWFpbFZlcmlmaWNhdGlvbkNvZGVSZXF1ZXN0EiIKB2NvbnRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 28) = "eHQYASABKAsyES5yb290LlJvb3RDb250ZXh0EhcKD3R1cm5zdGlsZV90b2tl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 29) = "bhgKIAEoCSJvCh1Vc2VyU2V0TWF4T25saW5lU3RhdHVzUmVxdWVzdBIiCgdj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 30) = "b250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIqCgptYXhfc3RhdHVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 31) = "GAogASgOMhYucm9vdC5Vc2VyT25saW5lU3RhdHVzInQKIFVzZXJTZXREZXZp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 32) = "Y2VPbmxpbmVTdGF0dXNSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 33) = "LlJvb3RDb250ZXh0EiwKBnN0YXR1cxgKIAEoDjIcLnJvb3QuVXNlckRldmlj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 34) = "ZU9ubGluZVN0YXR1cyJ7ChxVc2VyU2V0UHJvZmlsZVBpY3R1cmVSZXF1ZXN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 35) = "EiIKB2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0EjcKEXByb2Zp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 36) = "bGVfdG9rZW5fdXJpGAogASgLMhwuZ29vZ2xlLnByb3RvYnVmLlN0cmluZ1Zh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 37) = "bHVlIpMBChFVc2VyU2lnblVwUmVxdWVzdBIQCgh1c2VybmFtZRgKIAEoCRIQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 38) = "CghwYXNzd29yZBgLIAEoCRINCgVlbWFpbBgMIAEoCRIyCgxhY2Nlc3NfdG9r";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 39) = "ZW4YDSABKAsyHC5nb29nbGUucHJvdG9idWYuU3RyaW5nVmFsdWUSFwoPdHVy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 40) = "bnN0aWxlX3Rva2VuGA4gASgJIhQKElVzZXJTaWduT3V0UmVxdWVzdCJfChVV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 41) = "c2VyRGV2aWNlRGVzY3JpcHRpb24SCgoCb3MYCiABKAkSEgoKb3NfdmVyc2lv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 42) = "bhgLIAEoCRITCgtkZXZpY2VfbmFtZRgMIAEoCRIRCglpc19tb2JpbGUYDSAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 43) = "KAgiswEKEVVzZXJTaWduSW5SZXF1ZXN0EhAKCHVzZXJuYW1lGAogASgJEhAK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 44) = "CHBhc3N3b3JkGAsgASgJEiMKCWRldmljZV9pZBgMIAEoCzIQLnJvb3QuRGV2";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 45) = "aWNlVXVpZBI8Chd1c2VyX2RldmljZV9kZXNjcmlwdGlvbhgNIAEoCzIbLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 46) = "b3QuVXNlckRldmljZURlc2NyaXB0aW9uEhcKD3R1cm5zdGlsZV90b2tlbhgO";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 47) = "IAEoCSI6CiVVc2VyR2V0RXh0ZW5kZWRVc2Vyc0J5VXNlcm5hbWVSZXF1ZXN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 48) = "EhEKCXVzZXJuYW1lcxgKIAMoCSJDCh9Vc2VyR2V0RXh0ZW5kZWRVc2Vyc0J5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 49) = "SWRSZXF1ZXN0EiAKCHVzZXJfaWRzGAogAygLMg4ucm9vdC5Vc2VyVXVpZCIt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 50) = "ChlVc2VyRmluZEJ5VXNlcm5hbWVSZXF1ZXN0EhAKCHVzZXJuYW1lGAogASgJ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 51) = "IjUKElVzZXJHZXROb3RlUmVxdWVzdBIfCgd1c2VyX2lkGAUgASgLMg4ucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 52) = "dC5Vc2VyVXVpZCI/ChxVc2VyQ29ubmVjdGlvbkJldHdlZW5SZXF1ZXN0Eh8K";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 53) = "B3VzZXJfaWQYBSABKAsyDi5yb290LlVzZXJVdWlkIloKJVVzZXJTZXRNb2Jp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 54) = "bGVOb3RpZmljYXRpb25Ub2tlblJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIR";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 55) = "LnJvb3QuUm9vdENvbnRleHQSDQoFdG9rZW4YCiABKAkiSQoRVXNlckRlbGV0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 56) = "ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 57) = "EAoIcGFzc3dvcmQYCyABKAkiFgoUVXNlckJsb2NrTGlzdFJlcXVlc3QiYwoW";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 58) = "VXNlckJsb2NrQ3JlYXRlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 59) = "dC5Sb290Q29udGV4dBIlCg1ibG9ja191c2VyX2lkGAUgASgLMg4ucm9vdC5V";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 60) = "c2VyVXVpZCJjChZVc2VyQmxvY2tEZWxldGVSZXF1ZXN0EiIKB2NvbnRleHQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 61) = "ASABKAsyES5yb290LlJvb3RDb250ZXh0EiUKDWJsb2NrX3VzZXJfaWQYBSAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 62) = "KAsyDi5yb290LlVzZXJVdWlkIscBCg9Vc2VyRmxhZ1JlcXVlc3QSIgoHY29u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 63) = "dGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSJgoOcmVwb3J0X3VzZXJf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 64) = "aWQYBSABKAsyDi5yb290LlVzZXJVdWlkEjQKE2NvbnRlbnRfZmxhZ19yZWFz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 65) = "b24YBiABKA4yFy5yb290LkNvbnRlbnRGbGFnUmVhc29uEjIKEnVzZXJfZmxh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 66) = "Z19wcm9wZXJ0eRgHIAEoDjIWLnJvb3QuVXNlckZsYWdQcm9wZXJ0eSI9ChdV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 67) = "c2VyUmVmcmVzaFRva2VuUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 68) = "dC5Sb290Q29udGV4dCKqAQosVXNlclNldERpcmVjdE1lc3NhZ2VJbnZpdGVS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 69) = "ZXF1aXJlbWVudFJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 70) = "dENvbnRleHQSOwoKY29ubmVjdGlvbhgFIAEoDjInLnJvb3QuVXNlckRpcmVj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 71) = "dE1lc3NhZ2VJbnZpdGVDb25uZWN0aW9uEhkKEWlzX2VtYWlsX3ZlcmlmaWVk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 72) = "GAYgASgIIqQBCilVc2VyU2V0RnJpZW5kc2hpcEludml0ZVJlcXVpcmVtZW50";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 73) = "UmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBI4";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 74) = "Cgpjb25uZWN0aW9uGAUgASgOMiQucm9vdC5Vc2VyRnJpZW5kc2hpcEludml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 75) = "ZUNvbm5lY3Rpb24SGQoRaXNfZW1haWxfdmVyaWZpZWQYBiABKAhCJqoCI1Jv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray77<string>, string>(ref _003C_003Ey__InlineArray, 76) = "b3RBcHAuV2ViQXBpLlNoYXJlZC5HcnBjLlJlcXVlc3RzYgZwcm90bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray77<string>, string>(in _003C_003Ey__InlineArray, 77)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[5]
		{
			RootUuidsReflection.Descriptor,
			WrappersReflection.Descriptor,
			ContextReflection.Descriptor,
			EnumsReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[37]
		{
			new GeneratedClrTypeInfo(typeof(UserGetSelfRequest), UserGetSelfRequest.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserGetNewHubserverRequest), UserGetNewHubserverRequest.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSignUpResponse), UserSignUpResponse.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetPasswordResponse), UserSetPasswordResponse.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetUsernameResponse), UserSetUsernameResponse.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetEmailResponse), UserSetEmailResponse.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetNoteResponse), UserSetNoteResponse.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetNoteRequest), UserSetNoteRequest.Parser, new string[3] { "Context", "UserId", "Note" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetUsernameRequest), UserSetUsernameRequest.Parser, new string[2] { "Context", "Username" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserForgotPasswordRequest), UserForgotPasswordRequest.Parser, new string[3] { "Context", "Email", "TurnstileToken" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserForgotUsernameRequest), UserForgotUsernameRequest.Parser, new string[3] { "Context", "Email", "TurnstileToken" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserResetPasswordRequest), UserResetPasswordRequest.Parser, new string[5] { "Context", "Email", "ResetToken", "NewPassword", "TurnstileToken" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetPasswordRequest), UserSetPasswordRequest.Parser, new string[3] { "Context", "OldPassword", "NewPassword" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetEmailRequest), UserSetEmailRequest.Parser, new string[3] { "Context", "Email", "Password" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetEmailVerificationCodeRequest), UserSetEmailVerificationCodeRequest.Parser, new string[2] { "Context", "VerificationCode" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserResendEmailVerificationCodeRequest), UserResendEmailVerificationCodeRequest.Parser, new string[2] { "Context", "TurnstileToken" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetMaxOnlineStatusRequest), UserSetMaxOnlineStatusRequest.Parser, new string[2] { "Context", "MaxStatus" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetDeviceOnlineStatusRequest), UserSetDeviceOnlineStatusRequest.Parser, new string[2] { "Context", "Status" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetProfilePictureRequest), UserSetProfilePictureRequest.Parser, new string[2] { "Context", "ProfileTokenUri" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSignUpRequest), UserSignUpRequest.Parser, new string[5] { "Username", "Password", "Email", "AccessToken", "TurnstileToken" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSignOutRequest), UserSignOutRequest.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserDeviceDescription), UserDeviceDescription.Parser, new string[4] { "Os", "OsVersion", "DeviceName", "IsMobile" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSignInRequest), UserSignInRequest.Parser, new string[5] { "Username", "Password", "DeviceId", "UserDeviceDescription", "TurnstileToken" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserGetExtendedUsersByUsernameRequest), UserGetExtendedUsersByUsernameRequest.Parser, new string[1] { "Usernames" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserGetExtendedUsersByIdRequest), UserGetExtendedUsersByIdRequest.Parser, new string[1] { "UserIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserFindByUsernameRequest), UserFindByUsernameRequest.Parser, new string[1] { "Username" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserGetNoteRequest), UserGetNoteRequest.Parser, new string[1] { "UserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserConnectionBetweenRequest), UserConnectionBetweenRequest.Parser, new string[1] { "UserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetMobileNotificationTokenRequest), UserSetMobileNotificationTokenRequest.Parser, new string[2] { "Context", "Token" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserDeleteRequest), UserDeleteRequest.Parser, new string[2] { "Context", "Password" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserBlockListRequest), UserBlockListRequest.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserBlockCreateRequest), UserBlockCreateRequest.Parser, new string[2] { "Context", "BlockUserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserBlockDeleteRequest), UserBlockDeleteRequest.Parser, new string[2] { "Context", "BlockUserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserFlagRequest), UserFlagRequest.Parser, new string[4] { "Context", "ReportUserId", "ContentFlagReason", "UserFlagProperty" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserRefreshTokenRequest), UserRefreshTokenRequest.Parser, new string[1] { "Context" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetDirectMessageInviteRequirementRequest), UserSetDirectMessageInviteRequirementRequest.Parser, new string[3] { "Context", "Connection", "IsEmailVerified" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserSetFriendshipInviteRequirementRequest), UserSetFriendshipInviteRequirementRequest.Parser, new string[3] { "Context", "Connection", "IsEmailVerified" }, null, null, null, null)
		}));
	}
}
