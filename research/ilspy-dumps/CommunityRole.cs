using System.Runtime.CompilerServices;
using System.Text.Json;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Browser.RootApps.Models;

public class CommunityRole
{
	[CompilerGenerated]
	private string _003Cid_003Ek__BackingField;

	[CompilerGenerated]
	private string _003Cname_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CcolorHex_003Ek__BackingField;

	[CompilerGenerated]
	private CommunityRoleType _003CcommunityRoleType_003Ek__BackingField;

	public string id
	{
		[CompilerGenerated]
		get
		{
			return _003Cid_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cid_003Ek__BackingField = text;
		}
	}

	public string name
	{
		[CompilerGenerated]
		get
		{
			return _003Cname_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cname_003Ek__BackingField = text;
		}
	}

	public string colorHex
	{
		[CompilerGenerated]
		get
		{
			return _003CcolorHex_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CcolorHex_003Ek__BackingField = text;
		}
	}

	public CommunityRoleType communityRoleType
	{
		[CompilerGenerated]
		get
		{
			return _003CcommunityRoleType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CcommunityRoleType_003Ek__BackingField = communityRoleType;
		}
	}

	public CommunityRole(Role role)
	{
		id = role.Id.ToString("s");
		name = role.RoleName;
		colorHex = role.RoleColorHex;
		communityRoleType = ((!role.IsDefault) ? CommunityRoleType.Standard : CommunityRoleType.Everyone);
	}

	public IJsObject ToJsObject(IFrame P_0)
	{
		string jsonString = JsonSerializer.Serialize(this, CommunityRoleJsonContext.Default.CommunityRole);
		return P_0.ParseJsonString<IJsObject>(jsonString);
	}
}
