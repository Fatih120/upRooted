using System.CodeDom.Compiler;

namespace System.Text.RegularExpressions.Generated;

[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
internal sealed class _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_7 : Regex
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
				int num8 = num6;
				num2 = num;
				int num9 = span.IndexOf('\n');
				if (num9 < 0)
				{
					num9 = span.Length;
				}
				span = span.Slice(num9);
				num += num9;
				num3 = num;
				while (true)
				{
					if (!span.StartsWith("--".AsSpan()))
					{
						if (_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_hasTimeout)
						{
							CheckTimeout();
						}
						if (num2 >= num3 || (num3 = P_0.Slice(num2, Math.Min(P_0.Length, num3 + 1) - num2).LastIndexOf("--".AsSpan())) < 0)
						{
							break;
						}
						num3 += num2;
						num = num3;
						span = P_0.Slice(num);
						continue;
					}
					num6 = num8;
					return false;
				}
				num = num7;
				span = P_0.Slice(num);
				span = P_0.Slice(num);
				int num10 = num;
				if (_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_hasTimeout)
				{
					CheckTimeout();
				}
				if (!span.IsEmpty && span[0] == '-')
				{
					return false;
				}
				num = num10;
				span = P_0.Slice(num);
				num4 = num;
				int num11 = span.IndexOfAnyExcept(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_asciiLettersAndDigitsAndDashKelvinSign);
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
					if ((uint)(num - 1) < P_0.Length && P_0[num - 1] == '-')
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

	internal static readonly _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_7 Instance = new _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_7();

	private _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_7()
	{
		pattern = "^(?!.*--)(?!-)[A-Za-z0-9-]+(?<!-)$";
		roptions = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
		Regex.ValidateMatchTimeout(_003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout);
		internalMatchTimeout = _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__Utilities.s_defaultTimeout;
		factory = new RunnerFactory();
		capsize = 1;
	}
}
