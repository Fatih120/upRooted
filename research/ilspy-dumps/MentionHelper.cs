using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Helpers;

public static class MentionHelper
{
	public static string FormatUserMention(string P_0, MentionType P_1, RootGuid P_2)
	{
		if (1 == 0)
		{
		}
		string result = P_1 switch
		{
			MentionType.All => "[@All](root://role/All)", 
			MentionType.Here => "[@Here](root://role/Here)", 
			MentionType.Role => $"[@{P_0}](root://role/{P_2})", 
			MentionType.User => $"[@{P_0}](root://user/{P_2})", 
			_ => "[Invalid](root://invalid/)", 
		};
		if (1 == 0)
		{
		}
		return result;
	}

	public static string FormatMessageMention(string P_0, MessageContainerGuid P_1, MessageGuid P_2, CommunityGuid? P_3)
	{
		if (!(P_3 != null))
		{
			return $"[{P_0}](root://message/{P_1}/{P_2})";
		}
		return $"[{P_0}](root://message/{P_3}/{P_1}/{P_2})";
	}
}
