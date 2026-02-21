using System.CodeDom.Compiler;

namespace System.Text.RegularExpressions.Generated;

[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
internal sealed class _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__DESCRIPTION_REGEX_8 : Regex
{
	private sealed class RunnerFactory : RegexRunnerFactory
	{
		private sealed class Runner : RegexRunner
		{
			protected override void Scan(ReadOnlySpan<char> P_0)
			{
				if (TryFindNextPossibleStartingPosition(P_0) && !TryMatchAtCurrentPosition(P_0))
				{
					runtextpos = P_0.Length;
				}
			}

			private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> P_0)
			{
				if (runtextpos == 0)
				{
					return true;
				}
				runtextpos = P_0.Length;
				return false;
			}

			private bool TryMatchAtCurrentPosition(ReadOnlySpan<char> P_0)
			{
				int num = runtextpos;
				int start = num;
				ReadOnlySpan<char> span = P_0.Slice(num);
				if (num != 0)
				{
					return false;
				}
				int num2 = span.IndexOfAnyExcept(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_nonAscii_A3F525343EF68C6C07959846D77D91E5D8776D965A0659E1AD39CCD035C19A11);
				if (num2 < 0)
				{
					num2 = span.Length;
				}
				span = span.Slice(num2);
				num += num2;
				if (num < P_0.Length - 1 || ((uint)num < (uint)P_0.Length && P_0[num] != '\n'))
				{
					return false;
				}
				runtextpos = num;
				Capture(0, start, num);
				return true;
			}
		}

		protected override RegexRunner CreateInstance()
		{
			return new Runner();
		}
	}

	internal static readonly _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__DESCRIPTION_REGEX_8 Instance = new _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__DESCRIPTION_REGEX_8();

	private _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__DESCRIPTION_REGEX_8()
	{
		pattern = "^[a-zA-Z0-9 !@#$%^&*(),.?'\"‘’“”‚:{}|<>_+=\\-\\[\\]\\\\/]*$";
		roptions = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
		Regex.ValidateMatchTimeout(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout);
		internalMatchTimeout = _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout;
		factory = new RunnerFactory();
		capsize = 1;
	}
}
