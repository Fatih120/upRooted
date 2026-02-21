// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SkiaSharpVersion
using System;
using SkiaSharp;

public static class SkiaSharpVersion
{
	private static readonly Version Zero = new Version(0, 0);

	private static Version nativeMinimum;

	private static Version nativeVersion;

	public static Version NativeMinimum => nativeMinimum ?? (nativeMinimum = new Version(88, 1));

	public static Version Native
	{
		get
		{
			try
			{
				return nativeVersion ?? (nativeVersion = new Version(SkiaApi.sk_version_get_milestone(), SkiaApi.sk_version_get_increment()));
			}
			catch (EntryPointNotFoundException)
			{
				return nativeVersion ?? (nativeVersion = Zero);
			}
		}
	}

	public static bool CheckNativeLibraryCompatible(bool P_0 = false)
	{
		return CheckNativeLibraryCompatible(NativeMinimum, Native, P_0);
	}

	internal static bool CheckNativeLibraryCompatible(Version P_0, Version P_1, bool P_2 = false)
	{
		if ((object)P_0 == null)
		{
			P_0 = Zero;
		}
		if ((object)P_1 == null)
		{
			P_1 = Zero;
		}
		if (P_0 <= Zero)
		{
			return true;
		}
		Version version = new Version(P_0.Major + 1, 0);
		if (P_1 <= Zero)
		{
			if (P_2)
			{
				throw new InvalidOperationException($"The version of the native libSkiaSharp library is incompatible with this version of SkiaSharp. Supported versions of the native libSkiaSharp library are in the range [{P_0.ToString(2)}, {version.ToString(2)}).");
			}
			return false;
		}
		bool flag = P_1 < P_0 || P_1 >= version;
		if (flag && P_2)
		{
			throw new InvalidOperationException($"The version of the native libSkiaSharp library ({P_1.ToString(2)}) is incompatible with this version of SkiaSharp. Supported versions of the native libSkiaSharp library are in the range [{P_0.ToString(2)}, {version.ToString(2)}).");
		}
		return !flag;
	}
}

