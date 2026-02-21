using System;
using System.Buffers;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using MimeDetective;
using MimeDetective.Engine;
using MimeDetective.Storage;

namespace RootApp.Utility.MimeDetection;

public static class RootContentInspectorExtensions
{
	private static readonly FrozenSet<string>.AlternateLookup<ReadOnlySpan<char>> _textTypes;

	public static IReadOnlyList<string> GetExtension(this MimeTypeToFileExtensionLookup P_0, string P_1, string? P_2 = null)
	{
		ReadOnlyMemory<char> fileExtension = GetFileExtension(P_2);
		ImmutableArray<FileExtensionMatch> immutableArray = P_0.TryGetValues(P_1);
		return (from m in immutableArray
			orderby MemoryExtensions.Equals(fileExtension.Span, m.Extension.AsSpan(), StringComparison.OrdinalIgnoreCase) ? 1 : 0 descending, m.Points descending, m.Extension.Length descending
			select m.Extension.ToLowerInvariant()).ToArray();
	}

	public static (string mimeType, string extension) Detect(this IContentInspector P_0, ReadOnlySpan<byte> P_1, string? P_2 = null)
	{
		if (P_1.IsEmpty)
		{
			return (mimeType: "application/octet-stream", extension: string.Empty);
		}
		ImmutableArray<DefinitionMatch> immutableArray = P_0.Inspect(P_1);
		ReadOnlyMemory<char> fileExtension = GetFileExtension(P_2);
		FileType fileType = null;
		if (immutableArray.Length > 0)
		{
			if (!string.IsNullOrEmpty(P_2) && P_2.Length < 64)
			{
				fileType = immutableArray.Where((DefinitionMatch r) => r.Definition.File.Extensions.Any((string e) => MemoryExtensions.Equals(fileExtension.Span, e.AsSpan(), StringComparison.OrdinalIgnoreCase))).MaxBy((DefinitionMatch r) => r.Points)?.Definition.File;
			}
			if ((object)fileType == null)
			{
				fileType = immutableArray.MaxBy((DefinitionMatch r) => r.Points)?.Definition.File;
			}
		}
		if ((object)fileType == null || string.IsNullOrEmpty(fileType.MimeType))
		{
			return TextDetection(P_1, fileExtension);
		}
		string text = fileType.Extensions.MaxBy((string e) => e.Length);
		if (string.IsNullOrEmpty(text) || text.Equals("txt", StringComparison.OrdinalIgnoreCase))
		{
			return TextDetection(P_1, fileExtension);
		}
		return (mimeType: fileType.MimeType.ToLowerInvariant(), extension: text.ToLowerInvariant());
	}

	private static (string mimeType, string extension) TextDetection(ReadOnlySpan<byte> P_0, ReadOnlyMemory<char> P_1)
	{
		switch (P_0[0])
		{
		case byte.MaxValue:
			if (P_0.StartsWith(Encoding.Unicode.Preamble) && IsLikelyTruncatedUtf16(P_0))
			{
				return (mimeType: "text/plain", extension: GetTextExtension(P_1.Span) ?? "txt");
			}
			break;
		case 254:
			if (P_0.StartsWith(Encoding.BigEndianUnicode.Preamble))
			{
				return (mimeType: "text/plain", extension: GetTextExtension(P_1.Span) ?? string.Empty);
			}
			break;
		default:
			if (IsLikelyTruncatedUtf8(P_0))
			{
				if (P_0.StartsWith(Encoding.UTF8.Preamble))
				{
					return (mimeType: "text/plain", extension: GetTextExtension(P_1.Span) ?? "txt");
				}
				return (mimeType: "text/plain", extension: GetTextExtension(P_1.Span) ?? (Ascii.IsValid(P_0) ? "txt" : string.Empty));
			}
			break;
		}
		return (mimeType: "application/octet-stream", extension: string.Empty);
	}

	private static ReadOnlyMemory<char> GetFileExtension(string? P_0)
	{
		if (string.IsNullOrEmpty(P_0))
		{
			return ReadOnlyMemory<char>.Empty;
		}
		ReadOnlyMemory<char> readOnlyMemory = P_0.AsMemory();
		ReadOnlyMemory<char> result;
		if (readOnlyMemory.Span[0] != '.')
		{
			result = readOnlyMemory;
		}
		else
		{
			result = readOnlyMemory.Slice(1, readOnlyMemory.Length - 1);
		}
		return result;
	}

	private static string? GetTextExtension(ReadOnlySpan<char> P_0)
	{
		if (!P_0.IsEmpty && _textTypes.TryGetValue(P_0, out string result))
		{
			return result;
		}
		return null;
	}

	private static bool IsLikelyTruncatedUtf8(ReadOnlySpan<byte> P_0)
	{
		if (Utf8.IsValid(P_0))
		{
			return true;
		}
		char[] array = ArrayPool<char>.Shared.Rent(P_0.Length);
		try
		{
			int num;
			int num2;
			OperationStatus operationStatus = Utf8.ToUtf16(P_0, array, out num, out num2, false, false);
			return operationStatus != OperationStatus.InvalidData;
		}
		finally
		{
			ArrayPool<char>.Shared.Return(array);
		}
	}

	private static bool IsLikelyTruncatedUtf16(ReadOnlySpan<byte> P_0)
	{
		ReadOnlySpan<char> readOnlySpan = MemoryMarshal.Cast<byte, char>(P_0);
		byte[] array = ArrayPool<byte>.Shared.Rent(3 * readOnlySpan.Length);
		try
		{
			int num;
			int num2;
			OperationStatus operationStatus = Utf8.FromUtf16(readOnlySpan, array, out num, out num2, false, false);
			return operationStatus != OperationStatus.InvalidData;
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(array);
		}
	}

	static RootContentInspectorExtensions()
	{
		StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
		_003C_003Ey__InlineArray29<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray29<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 0) = "txt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 1) = "csv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 2) = "tsv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 3) = "log";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 4) = "md";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 5) = "markdown";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 6) = "xml";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 7) = "json";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 8) = "html";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 9) = "htm";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 10) = "ini";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 11) = "config";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 12) = "cfg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 13) = "css";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 14) = "cs";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 15) = "js";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 16) = "go";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 17) = "ts";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 18) = "jsx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 19) = "tsx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 20) = "php";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 21) = "ps1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 22) = "py";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 23) = "rb";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 24) = "java";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 25) = "c";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 26) = "cpp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 27) = "h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray29<string>, string>(ref _003C_003Ey__InlineArray, 28) = "hpp";
		_textTypes = FrozenSet.Create(ordinalIgnoreCase, global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray29<string>, string>(in _003C_003Ey__InlineArray, 29)).GetAlternateLookup<ReadOnlySpan<char>>();
	}
}
