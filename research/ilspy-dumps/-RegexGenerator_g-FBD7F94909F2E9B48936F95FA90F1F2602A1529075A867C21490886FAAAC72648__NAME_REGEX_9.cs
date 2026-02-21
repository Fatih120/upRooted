using System.CodeDom.Compiler;

namespace System.Text.RegularExpressions.Generated;

[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
internal sealed class _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_9 : Regex
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
				if ((uint)num < (uint)P_0.Length && num == 0)
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
				int num6 = span.IndexOfAnyExcept(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_ascii_8000FF03FEFFFF07FEFFFF07);
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
					num4 = 0;
					while (true)
					{
						_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.StackPush(ref runstack, ref num5, num);
						num4++;
						if (span.IsEmpty || span[0] != ' ')
						{
							break;
						}
						int num7 = span.Slice(1).IndexOfAnyExcept(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_ascii_8000FF03FEFFFF07FEFFFF07);
						if (num7 < 0)
						{
							num7 = span.Length - 1;
						}
						if (num7 == 0)
						{
							break;
						}
						span = span.Slice(num7);
						num += num7;
						num++;
						span = P_0.Slice(num);
					}
					while (--num4 >= 0)
					{
						num = runstack[--num5];
						span = P_0.Slice(num);
						if ((uint)num < (uint)P_0.Length)
						{
							continue;
						}
						runtextpos = num;
						Capture(0, start, num);
						return true;
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

	internal static readonly _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_9 Instance = new _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_9();

	private _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_9()
	{
		pattern = "^[A-Za-z0-9']+(?: [A-Za-z0-9']+)*\\z";
		roptions = RegexOptions.CultureInvariant;
		Regex.ValidateMatchTimeout(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout);
		internalMatchTimeout = _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout;
		factory = new RunnerFactory();
		capsize = 1;
	}
}
