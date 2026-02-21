using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using MimeDetective.Storage;

namespace RootApp.Utility.MimeDetection;

public static class MimeSignatureFilter
{
	private static readonly FrozenSet<string> _extensions;

	private static readonly FrozenSet<string> _excludeDescription;

	public static IEnumerable<Definition> Filter(IEnumerable<Definition> P_0)
	{
		return P_0.Where(delegate(Definition d)
		{
			FileType file = d.File;
			return !string.IsNullOrEmpty(file.Description) && !_excludeDescription.Contains(file.Description) && !string.IsNullOrEmpty(file.MimeType) && !string.Equals(file.MimeType, "APPLICATION/OCTET-STREAM", StringComparison.OrdinalIgnoreCase) && file.Extensions.Any(_extensions.Contains);
		}).TrimMeta().TrimDescription();
	}

	static MimeSignatureFilter()
	{
		StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
		_003C_003Ey__InlineArray111<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray111<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 0) = "aif";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 1) = "aiff";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 2) = "aac";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 3) = "flac";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 4) = "m4a";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 5) = "mid";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 6) = "midi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 7) = "mp3";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 8) = "mpa";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 9) = "ogg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 10) = "wav";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 11) = "wma";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 12) = "wpl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 13) = "7z";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 14) = "arj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 15) = "bz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 16) = "bz2";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 17) = "deb";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 18) = "pkg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 19) = "rar";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 20) = "rpm";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 21) = "tar";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 22) = "tar.gz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 23) = "xz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 24) = "z";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 25) = "zip";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 26) = "bin";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 27) = "dmg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 28) = "iso";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 29) = "toast";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 30) = "vcd";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 31) = "css";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 32) = "html";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 33) = "js";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 34) = "json";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 35) = "mdb";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 36) = "odb";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 37) = "xml";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 38) = "eml";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 39) = "emlx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 40) = "msg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 41) = "oft";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 42) = "ost";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 43) = "pst";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 44) = "vcf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 45) = "apk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 46) = "appx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 47) = "com";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 48) = "exe";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 49) = "dll";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 50) = "jar";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 51) = "msi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 52) = "msix";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 53) = "fnt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 54) = "fon";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 55) = "otf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 56) = "ttf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 57) = "ai";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 58) = "avif";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 59) = "bmp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 60) = "gif";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 61) = "heic";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 62) = "heif";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 63) = "ico";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 64) = "jpg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 65) = "jpeg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 66) = "png";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 67) = "ps";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 68) = "psd";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 69) = "svg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 70) = "tif";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 71) = "tiff";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 72) = "webp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 73) = "key";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 74) = "odp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 75) = "pps";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 76) = "ppt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 77) = "pptx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 78) = "ods";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 79) = "xls";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 80) = "xlsm";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 81) = "xlsx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 82) = "cab";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 83) = "cur";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 84) = "icns";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 85) = "ico";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 86) = "lnk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 87) = "3g2";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 88) = "3gp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 89) = "avi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 90) = "flv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 91) = "h264";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 92) = "m4v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 93) = "mkv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 94) = "mov";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 95) = "mp4";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 96) = "mpg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 97) = "mpeg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 98) = "rm";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 99) = "swf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 100) = "vob";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 101) = "webm";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 102) = "wmv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 103) = "doc";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 104) = "docx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 105) = "odt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 106) = "pdf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 107) = "rtf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 108) = "txt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 109) = "tex";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray111<string>, string>(ref _003C_003Ey__InlineArray, 110) = "wpd";
		_extensions = FrozenSet.Create(ordinalIgnoreCase, global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray111<string>, string>(in _003C_003Ey__InlineArray, 111));
		StringComparer ordinalIgnoreCase2 = StringComparer.OrdinalIgnoreCase;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray2 = default(global::_003C_003Ey__InlineArray2<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray2<string>, string>(ref _003C_003Ey__InlineArray2, 0) = "Fireworks PNG bitmap";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray2<string>, string>(ref _003C_003Ey__InlineArray2, 1) = "PNG Plus";
		_excludeDescription = FrozenSet.Create(ordinalIgnoreCase2, global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<global::_003C_003Ey__InlineArray2<string>, string>(in _003C_003Ey__InlineArray2, 2));
	}
}
