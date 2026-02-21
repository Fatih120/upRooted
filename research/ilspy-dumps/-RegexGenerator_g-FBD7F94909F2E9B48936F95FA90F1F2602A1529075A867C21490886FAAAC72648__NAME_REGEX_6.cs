using System.CodeDom.Compiler;

namespace System.Text.RegularExpressions.Generated;

[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
internal sealed class _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_6 : Regex
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
				int num = runtextpos;
				if (num <= P_0.Length - 2 && num == 0)
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
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				ReadOnlySpan<char> span = P_0.Slice(num);
				if (num != 0)
				{
					return false;
				}
				num2 = num;
				int num6 = span.IndexOfAnyExcept(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_asciiLettersAndDigitsAndKelvinSign);
				if (num6 < 0)
				{
					num6 = span.Length;
				}
				if (num6 == 0)
				{
					return false;
				}
				span = span.Slice(num6);
				num += num6;
				num3 = num;
				num2++;
				while (true)
				{
					if (!span.IsEmpty && span[0] == '\'')
					{
						span = span.Slice(1);
						num++;
					}
					num4 = num;
					char c;
					if (!span.IsEmpty && (((c = span[0]) < '\u0080') ? char.IsAsciiLetterOrDigit(c) : RegexRunner.CharInClass(c, "\0\b\00:A[a{KÅ")))
					{
						span = span.Slice(1);
						num++;
					}
					num5 = num;
					while (true)
					{
						int i;
						for (i = 0; (uint)i < (uint)span.Length && char.IsWhiteSpace(span[i]); i++)
						{
						}
						span = span.Slice(i);
						num += i;
						int num7 = span.IndexOfAnyExcept(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_asciiLettersAndDigitsAndKelvinSign);
						if (num7 < 0)
						{
							num7 = span.Length;
						}
						if (num7 != 0)
						{
							span = span.Slice(num7);
							num += num7;
							if (num >= P_0.Length - 1 && ((uint)num >= (uint)P_0.Length || P_0[num] == '\n'))
							{
								runtextpos = num;
								Capture(0, start, num);
								return true;
							}
						}
						if (_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_hasTimeout)
						{
							CheckTimeout();
						}
						if (num4 >= num5)
						{
							break;
						}
						num = --num5;
						span = P_0.Slice(num);
					}
					if (_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_hasTimeout)
					{
						CheckTimeout();
					}
					if (num2 >= num3)
					{
						break;
					}
					num = --num3;
					span = P_0.Slice(num);
				}
				return false;
			}
		}

		protected override RegexRunner CreateInstance()
		{
			return new Runner();
		}
	}

	internal static readonly _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_6 Instance = new _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_6();

	private _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_6()
	{
		pattern = "^[a-zA-Z0-9]+'?[a-zA-Z0-9]?\\s*[a-zA-Z0-9]+$";
		roptions = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
		Regex.ValidateMatchTimeout(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout);
		internalMatchTimeout = _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout;
		factory = new RunnerFactory();
		capsize = 1;
	}
}
