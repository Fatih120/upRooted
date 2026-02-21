using System;
using System.Collections.Generic;

namespace RootApp.Utility;

public interface IMimeSignatureDetector
{
	(string mimeType, string extension) Detect(ReadOnlySpan<byte> P_0, string? P_1 = null);

	IReadOnlyList<string> GetExtension(string P_0, string? P_1 = null);
}
