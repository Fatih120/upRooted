using System.CodeDom.Compiler;

namespace System.Text.RegularExpressions.Generated;

[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
internal sealed class _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__USERNAME_REGEX_4 : Regex
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
				int num6 = 0;
				ReadOnlySpan<char> span = P_0.Slice(num);
				if (num != 0)
				{
					return false;
				}
				span = P_0.Slice(num);
				int num7 = num;
				if (_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_hasTimeout)
				{
					CheckTimeout();
				}
				char c;
				if (!span.IsEmpty && !((c = span[0]) != '.' && c != '_'))
				{
					return false;
				}
				num = num7;
				span = P_0.Slice(num);
				span = P_0.Slice(num);
				int num8 = num;
				if (_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_hasTimeout)
				{
					CheckTimeout();
				}
				int num9 = num6;
				num2 = num;
				int num10 = span.IndexOf('\n');
				if (num10 < 0)
				{
					num10 = span.Length;
				}
				span = span.Slice(num10);
				num += num10;
				num3 = num;
				while (true)
				{
					if ((uint)span.Length < 2u || ((c = span[0]) != '.' && c != '_') || ((c = span[1]) != '.' && c != '_'))
					{
						if (_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_hasTimeout)
						{
							CheckTimeout();
						}
						if (num2 >= num3 || (num3 = P_0.Slice(num2, num3 - num2).LastIndexOfAny('.', '_')) < 0)
						{
							break;
						}
						num3 += num2;
						num = num3;
						span = P_0.Slice(num);
						continue;
					}
					num6 = num9;
					return false;
				}
				num = num8;
				span = P_0.Slice(num);
				num4 = num;
				int num11 = span.IndexOfAnyExcept(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_nonAscii_E1831D2F38B7938E40EAA4F0639C52261E3DFAC5A4A5ED2E4C384476DF69F948);
				if (num11 < 0)
				{
					num11 = span.Length;
				}
				if (num11 == 0)
				{
					return false;
				}
				span = span.Slice(num11);
				num += num11;
				num5 = num;
				num4++;
				while (true)
				{
					span = P_0.Slice(num);
					int num12 = num;
					if (_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_hasTimeout)
					{
						CheckTimeout();
					}
					if ((uint)(num - 1) < P_0.Length && !((c = P_0[num - 1]) != '.' && c != '_'))
					{
						num--;
					}
					else
					{
						num = num12;
						span = P_0.Slice(num);
						if (num >= P_0.Length - 1 && ((uint)num >= (uint)P_0.Length || P_0[num] == '\n'))
						{
							break;
						}
					}
					if (_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_hasTimeout)
					{
						CheckTimeout();
					}
					if (num4 >= num5)
					{
						return false;
					}
					num = --num5;
					span = P_0.Slice(num);
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

	internal static readonly _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__USERNAME_REGEX_4 Instance = new _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__USERNAME_REGEX_4();

	private _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__USERNAME_REGEX_4()
	{
		pattern = "^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";
		roptions = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant;
		Regex.ValidateMatchTimeout(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout);
		internalMatchTimeout = _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout;
		factory = new RunnerFactory();
		capsize = 1;
	}
}
