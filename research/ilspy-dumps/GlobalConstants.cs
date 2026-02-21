using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace RootApp.WebApi.Shared;

public static class GlobalConstants
{
	public static class UserAuthentication
	{
		[GeneratedRegex("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant)]
		[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
		public static Regex USERNAME_REGEX()
		{
			return _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__USERNAME_REGEX_4.Instance;
		}
	}

	public static class ChannelGroup
	{
		[GeneratedRegex("^[a-zA-Z0-9]+'?[a-zA-Z0-9]?\\s*[a-zA-Z0-9]+$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
		[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
		public static Regex NAME_REGEX()
		{
			return _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_6.Instance;
		}
	}

	public static class Channel
	{
		[GeneratedRegex("^(?!.*--)(?!-)[A-Za-z0-9-]+(?<!-)$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
		[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
		public static Regex NAME_REGEX()
		{
			return _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_7.Instance;
		}

		[GeneratedRegex("^[a-zA-Z0-9 !@#$%^&*(),.?'\"‘’“”‚:{}|<>_+=\\-\\[\\]\\\\/]*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
		[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
		public static Regex DESCRIPTION_REGEX()
		{
			return _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__DESCRIPTION_REGEX_8.Instance;
		}
	}

	public static class CreateCommunity
	{
		[GeneratedRegex("^[A-Za-z0-9']+(?: [A-Za-z0-9']+)*\\z", RegexOptions.CultureInvariant)]
		[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
		public static Regex NAME_REGEX()
		{
			return _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_9.Instance;
		}
	}

	public static class Friendship
	{
		[GeneratedRegex("^[a-zA-Z0-9]+'?[a-zA-Z0-9]?\\s*[a-zA-Z0-9]+$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
		[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
		public static Regex NAME_REGEX()
		{
			return _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_6.Instance;
		}
	}

	public static class CommunityRole
	{
		[GeneratedRegex("^(?!.*--)(?!-)[A-Za-z0-9-]+(?<!-)$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
		[GeneratedCode("System.Text.RegularExpressions.Generator", "10.0.14.7603")]
		public static Regex NAME_REGEX()
		{
			return _003CRegexGenerator_g_003EFBD7F94909F2E9B48936F95FA90F1F2602A1529075A867C21490886FAAAC72648__NAME_REGEX_7.Instance;
		}
	}
}
