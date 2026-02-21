using System;
using System.Collections.Generic;
using MimeDetective;
using RootApp.Utility.MimeDetection;

namespace RootApp.Utility;

public sealed class MimeSignatureDetector : IMimeSignatureDetector
{
	public static IContentInspector Inspector { get; } = new ContentInspectorBuilder
	{
		Definitions = MimeSignatureFactory.Default
	}.Build();

	public static MimeTypeToFileExtensionLookup ToFileExtension { get; } = new MimeTypeToFileExtensionLookupBuilder
	{
		Definitions = MimeSignatureFactory.Default
	}.Build();

	public (string mimeType, string extension) Detect(ReadOnlySpan<byte> P_0, string? P_1 = null)
	{
		return Inspector.Detect(P_0, P_1);
	}

	public IReadOnlyList<string> GetExtension(string P_0, string? P_1 = null)
	{
		return ToFileExtension.GetExtension(P_0, P_1);
	}
}
