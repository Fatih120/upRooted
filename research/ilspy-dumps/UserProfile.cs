using System.Runtime.CompilerServices;
using System.Text.Json;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;
using RootApp.Browser.Models;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Browser.RootApps.Models;

public class UserProfile
{
	[CompilerGenerated]
	private string _003Cid_003Ek__BackingField;

	[CompilerGenerated]
	private string _003Cnickname_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CprofilePictureUri_003Ek__BackingField;

	[CompilerGenerated]
	private UserOnlineStatus _003ConlineStatus_003Ek__BackingField;

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

	public string nickname
	{
		[CompilerGenerated]
		get
		{
			return _003Cnickname_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cnickname_003Ek__BackingField = text;
		}
	}

	public string profilePictureUri
	{
		[CompilerGenerated]
		get
		{
			return _003CprofilePictureUri_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CprofilePictureUri_003Ek__BackingField = text;
		}
	}

	public UserOnlineStatus onlineStatus
	{
		[CompilerGenerated]
		get
		{
			return _003ConlineStatus_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003ConlineStatus_003Ek__BackingField = userOnlineStatus;
		}
	}

	public UserProfile(Member member)
	{
		id = member.GlobalUser.Id.ToString("s");
		nickname = member.GlobalUser.UserName;
		profilePictureUri = member.GlobalUser.ProfilePictureUri;
		CommunityMemberOnlineStatus communityOnlineStatus = member.CommunityOnlineStatus;
		if (1 == 0)
		{
		}
		UserOnlineStatus userOnlineStatus;
		switch (communityOnlineStatus)
		{
		case CommunityMemberOnlineStatus.OnlineAndAttached:
		case CommunityMemberOnlineStatus.Online:
			userOnlineStatus = UserOnlineStatus.Active;
			break;
		case CommunityMemberOnlineStatus.AwayAndAttached:
		case CommunityMemberOnlineStatus.Away:
			userOnlineStatus = UserOnlineStatus.Inactive;
			break;
		case CommunityMemberOnlineStatus.Offline:
			userOnlineStatus = UserOnlineStatus.Disconnected;
			break;
		default:
			userOnlineStatus = onlineStatus;
			break;
		}
		if (1 == 0)
		{
		}
		onlineStatus = userOnlineStatus;
	}

	public IJsObject ToJsObject(IFrame P_0)
	{
		string jsonString = JsonSerializer.Serialize(this, UserProfileJsonContext.Default.UserProfile);
		return P_0.ParseJsonString<IJsObject>(jsonString);
	}
}
