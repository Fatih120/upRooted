using System;
using System.Runtime.CompilerServices;

namespace RootApp.WebApi.Shared.Permissions;

public sealed class CommunityPermission : ICommunityPermissions
{
	[CompilerGenerated]
	private bool _003CCommunityManageCommunity_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityManageRoles_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityManageEmojis_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityManageAuditLog_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityCreateInvite_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityManageInvites_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityCreateBan_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityManageBans_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityFullControl_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityKick_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityChangeMyNickname_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityChangeOtherNickname_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityCreateChannelGroup_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCommunityManageApps_003Ek__BackingField;

	public bool CommunityManageCommunity
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityManageCommunity_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityManageCommunity_003Ek__BackingField = flag;
		}
	}

	public bool CommunityManageRoles
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityManageRoles_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityManageRoles_003Ek__BackingField = flag;
		}
	}

	public bool CommunityManageEmojis
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityManageEmojis_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityManageEmojis_003Ek__BackingField = flag;
		}
	}

	public bool CommunityManageAuditLog
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityManageAuditLog_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityManageAuditLog_003Ek__BackingField = flag;
		}
	}

	public bool CommunityCreateInvite
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityCreateInvite_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityCreateInvite_003Ek__BackingField = flag;
		}
	}

	public bool CommunityManageInvites
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityManageInvites_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityManageInvites_003Ek__BackingField = flag;
		}
	}

	public bool CommunityCreateBan
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityCreateBan_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityCreateBan_003Ek__BackingField = flag;
		}
	}

	public bool CommunityManageBans
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityManageBans_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityManageBans_003Ek__BackingField = flag;
		}
	}

	public bool CommunityFullControl
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityFullControl_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityFullControl_003Ek__BackingField = flag;
		}
	}

	public bool CommunityKick
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityKick_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityKick_003Ek__BackingField = flag;
		}
	}

	public bool CommunityChangeMyNickname
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityChangeMyNickname_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityChangeMyNickname_003Ek__BackingField = flag;
		}
	}

	public bool CommunityChangeOtherNickname
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityChangeOtherNickname_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityChangeOtherNickname_003Ek__BackingField = flag;
		}
	}

	public bool CommunityCreateChannelGroup
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityCreateChannelGroup_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityCreateChannelGroup_003Ek__BackingField = flag;
		}
	}

	public bool CommunityManageApps
	{
		[CompilerGenerated]
		get
		{
			return _003CCommunityManageApps_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommunityManageApps_003Ek__BackingField = flag;
		}
	}

	public bool Equals(CommunityPermission? P_0)
	{
		if ((object)P_0 == null)
		{
			return false;
		}
		return CommunityPermissionsExtensions.Equals(this, P_0);
	}

	public override bool Equals(object? P_0)
	{
		if (!(P_0 is CommunityPermission communityPermission))
		{
			return false;
		}
		return Equals(communityPermission);
	}

	public override int GetHashCode()
	{
		HashCode hashCode = default(HashCode);
		hashCode.Add(CommunityManageCommunity);
		hashCode.Add(CommunityManageRoles);
		hashCode.Add(CommunityManageEmojis);
		hashCode.Add(CommunityManageAuditLog);
		hashCode.Add(CommunityCreateInvite);
		hashCode.Add(CommunityManageInvites);
		hashCode.Add(CommunityCreateBan);
		hashCode.Add(CommunityManageBans);
		hashCode.Add(CommunityFullControl);
		hashCode.Add(CommunityKick);
		hashCode.Add(CommunityChangeMyNickname);
		hashCode.Add(CommunityChangeOtherNickname);
		hashCode.Add(CommunityCreateChannelGroup);
		hashCode.Add(CommunityManageApps);
		return hashCode.ToHashCode();
	}

	public static bool operator ==(CommunityPermission? P_0, CommunityPermission? P_1)
	{
		if ((object)P_0 == P_1)
		{
			return true;
		}
		if ((object)P_0 == null || (object)P_1 == null)
		{
			return false;
		}
		return P_0.Equals(P_1);
	}

	public static bool operator !=(CommunityPermission? P_0, CommunityPermission? P_1)
	{
		return !(P_0 == P_1);
	}
}
