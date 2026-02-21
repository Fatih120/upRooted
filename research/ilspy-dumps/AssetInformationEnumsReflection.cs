using System;
using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public static class AssetInformationEnumsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AssetInformationEnumsReflection()
	{
		_003C_003Ey__InlineArray32<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray32<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiJjb3JlL2Fzc2V0X2luZm9ybWF0aW9uX2VudW1zLnByb3RvEgRyb290KnwK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 1) = "DEFzc2V0SW52YWxpZBIdChlBU1NFVF9JTlZBTElEX1VOU1BFQ0lGSUVEEAAS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 2) = "FwoTQVNTRVRfSU5WQUxJRF9FUlJPUhABEhkKFUFTU0VUX0lOVkFMSURfREVM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 3) = "RVRFRBACEhkKFUFTU0VUX0lOVkFMSURfQkxPQ0tFRBADKusBChBBc3NldFBy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 4) = "ZXZpZXdUeXBlEiIKHkFTU0VUX1BSRVZJRVdfVFlQRV9VTlNQRUNJRklFRBAA";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 5) = "EhwKGEFTU0VUX1BSRVZJRVdfVFlQRV9JTUFHRRABEhwKGEFTU0VUX1BSRVZJ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 6) = "RVdfVFlQRV9WSURFTxACEhwKGEFTU0VUX1BSRVZJRVdfVFlQRV9BVURJTxAD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 7) = "Eh8KG0FTU0VUX1BSRVZJRVdfVFlQRV9ET0NVTUVOVBAEEhsKF0FTU0VUX1BS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 8) = "RVZJRVdfVFlQRV9URVhUEAUSGwoXQVNTRVRfUFJFVklFV19UWVBFX1BBR0UQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 9) = "Biq1AwoQQXNzZXRWaWRlb0Zvcm1hdBIiCh5BU1NFVF9WSURFT19GT1JNQVRf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 10) = "VU5TUEVDSUZJRUQQABIaChZBU1NFVF9WSURFT19GT1JNQVRfTVA0EAESGgoW";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 11) = "QVNTRVRfVklERU9fRk9STUFUX01LVhACEhoKFkFTU0VUX1ZJREVPX0ZPUk1B";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 12) = "VF9NT1YQAxIaChZBU1NFVF9WSURFT19GT1JNQVRfQVZJEAQSGgoWQVNTRVRf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 13) = "VklERU9fRk9STUFUX0ZMVhAFEh8KG0FTU0VUX1ZJREVPX0ZPUk1BVF9NUEVH";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 14) = "Ml9UUxAGEh8KG0FTU0VUX1ZJREVPX0ZPUk1BVF9NUEVHMl9QUxAHEhoKFkFT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 15) = "U0VUX1ZJREVPX0ZPUk1BVF9NWEYQCBIaChZBU1NFVF9WSURFT19GT1JNQVRf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 16) = "TFhGEAkSGgoWQVNTRVRfVklERU9fRk9STUFUX0dYRhAKEhwKGEFTU0VUX1ZJ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 17) = "REVPX0ZPUk1BVF9XRUJfTRAMEhoKFkFTU0VUX1ZJREVPX0ZPUk1BVF9NUEcQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 18) = "DRIhCh1BU1NFVF9WSURFT19GT1JNQVRfUVVJQ0tfVElNRRAOKqsBCg9Bc3Nl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 19) = "dFZpZGVvQ29kZWMSIQodQVNTRVRfVklERU9fQ09ERUNfVU5TUEVDSUZJRUQQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 20) = "ABIeChpBU1NFVF9WSURFT19DT0RFQ19IMjY0X0FWQxABEh8KG0FTU0VUX1ZJ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 21) = "REVPX0NPREVDX0gyNjVfSEVWQxACEhkKFUFTU0VUX1ZJREVPX0NPREVDX1ZQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 22) = "ORADEhkKFUFTU0VUX1ZJREVPX0NPREVDX0FWMRAEKsIBChBBc3NldEF1ZGlv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 23) = "Rm9ybWF0EiIKHkFTU0VUX0FVRElPX0ZPUk1BVF9VTlNQRUNJRklFRBAAEhoK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 24) = "FkFTU0VUX0FVRElPX0ZPUk1BVF9NUDMQARIaChZBU1NFVF9BVURJT19GT1JN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 25) = "QVRfQUFDEAISGgoWQVNTRVRfQVVESU9fRk9STUFUX000QRADEhoKFkFTU0VU";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 26) = "X0FVRElPX0ZPUk1BVF9PR0cQBBIaChZBU1NFVF9BVURJT19GT1JNQVRfV0FW";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 27) = "EAUqwAEKD0Fzc2V0QXVkaW9Db2RlYxIhCh1BU1NFVF9BVURJT19DT0RFQ19V";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 28) = "TlNQRUNJRklFRBAAEhkKFUFTU0VUX0FVRElPX0NPREVDX01QMxABEhkKFUFT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 29) = "U0VUX0FVRElPX0NPREVDX0FBQxACEhwKGEFTU0VUX0FVRElPX0NPREVDX1ZP";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 30) = "UkJJUxADEhoKFkFTU0VUX0FVRElPX0NPREVDX09QVVMQBBIaChZBU1NFVF9B";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 31) = "VURJT19DT0RFQ19GTEFDEAVCEaoCDlJvb3RBcHAuQXNzZXRzYgZwcm90bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray32<string>, string>(in _003C_003Ey__InlineArray, 32)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[0], new GeneratedClrTypeInfo(new Type[6]
		{
			typeof(AssetInvalid),
			typeof(AssetPreviewType),
			typeof(AssetVideoFormat),
			typeof(AssetVideoCodec),
			typeof(AssetAudioFormat),
			typeof(AssetAudioCodec)
		}, null, null));
	}
}
