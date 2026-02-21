using System.Buffers;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated;

[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
internal static class _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities
{
	internal static readonly TimeSpan s_defaultTimeout = ((AppContext.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") is TimeSpan timeSpan) ? timeSpan : Regex.InfiniteMatchTimeout);

	internal static readonly bool s_hasTimeout = s_defaultTimeout != Regex.InfiniteMatchTimeout;

	internal static readonly SearchValues<char> s_asciiHexDigits = SearchValues.Create("0123456789ABCDEFabcdef".AsSpan());

	internal static readonly SearchValues<char> s_asciiLettersAndDigitsAndDashDotKelvinSign = SearchValues.Create("-.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzK".AsSpan());

	internal static readonly SearchValues<char> s_asciiLettersAndDigitsAndDashKelvinSign = SearchValues.Create("-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzK".AsSpan());

	internal static readonly SearchValues<char> s_asciiLettersAndDigitsAndDashUnderscoreKelvinSign = SearchValues.Create("-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyzK".AsSpan());

	internal static readonly SearchValues<char> s_asciiLettersAndDigitsAndKelvinSign = SearchValues.Create("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzK".AsSpan());

	internal static readonly SearchValues<char> s_asciiLettersAndKelvinSign = SearchValues.Create("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzK".AsSpan());

	internal static readonly SearchValues<char> s_ascii_8000FF03FEFFFF07FEFFFF07 = SearchValues.Create("'0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".AsSpan());

	internal static readonly SearchValues<char> s_nonAscii_99C76F9500B216ED73C303AF21F3BBD7DA8233090354DDBB971F08135AEC66F4 = SearchValues.Create(" -.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyzK".AsSpan());

	internal static readonly SearchValues<char> s_nonAscii_9D887F42D4125FF97C50D1A7C38CA6681E5E3C194771F207692B4B4A87144C7A = SearchValues.Create("%+-.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyzK".AsSpan());

	internal static readonly SearchValues<char> s_nonAscii_A3F525343EF68C6C07959846D77D91E5D8776D965A0659E1AD39CCD035C19A11 = SearchValues.Create(" !\"#$%&'()*+,-./0123456789:<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_abcdefghijklmnopqrstuvwxyz{|}‘’‚“”K".AsSpan());

	internal static readonly SearchValues<char> s_nonAscii_E1831D2F38B7938E40EAA4F0639C52261E3DFAC5A4A5ED2E4C384476DF69F948 = SearchValues.Create(".0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyzK".AsSpan());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void StackPush(ref int[] P_0, ref int P_1, int P_2)
	{
		int[] array = P_0;
		int num = P_1;
		if ((uint)num < (uint)array.Length)
		{
			array[num] = P_2;
			P_1++;
		}
		else
		{
			WithResize(ref P_0, ref P_1, P_2);
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
		static void WithResize(ref int[] reference, ref int reference2, int num2)
		{
			Array.Resize(ref reference, reference2 * 2);
			StackPush(ref reference, ref reference2, num2);
		}
	}
}
