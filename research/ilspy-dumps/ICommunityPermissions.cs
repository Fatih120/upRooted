namespace RootApp.WebApi.Shared.Permissions;

public interface ICommunityPermissions
{
	bool CommunityManageCommunity { get; set; }

	bool CommunityManageRoles { get; set; }

	bool CommunityManageEmojis { get; set; }

	bool CommunityManageAuditLog { get; set; }

	bool CommunityManageInvites { get; set; }

	bool CommunityCreateInvite { get; set; }

	bool CommunityManageBans { get; set; }

	bool CommunityCreateBan { get; set; }

	bool CommunityFullControl { get; set; }

	bool CommunityKick { get; set; }

	bool CommunityChangeMyNickname { get; set; }

	bool CommunityChangeOtherNickname { get; set; }

	bool CommunityCreateChannelGroup { get; set; }

	bool CommunityManageApps { get; set; }
}
