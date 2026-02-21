using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Helpers;

public static class PacketTypeHelper
{
	public static bool IsExternalCommunityPacket(this PacketType P_0)
	{
		return P_0 > PacketType.ExternalCommunityBoundaryMinValue && P_0 <= PacketType.ExternalCommunityBoundaryMaxValue;
	}

	public static bool IsCommunityPacket(this PacketType P_0)
	{
		return P_0 > PacketType.CommunityBoundaryMinValue && P_0 <= PacketType.CommunityBoundaryMaxValue;
	}

	public static bool IsDirectMessagePacket(this PacketType P_0)
	{
		return P_0 > PacketType.DirectMessageBoundaryMinValue && P_0 <= PacketType.DirectMessageBoundaryMaxValue;
	}

	public static bool IsNotificationPacket(this PacketType P_0)
	{
		return P_0 > PacketType.NotificationBoundaryMinValue && P_0 <= PacketType.NotificationBoundaryMaxValue;
	}

	public static bool IsDirectMessageWebRtcPacket(this PacketType P_0)
	{
		return P_0 > PacketType.DirectMessageWebRtcBoundaryMinValue && P_0 <= PacketType.DirectMessageWebRtcBoundaryMaxValue;
	}

	public static bool IsFriendshipPacket(this PacketType P_0)
	{
		return P_0 > PacketType.FriendshipBoundaryMinValue && P_0 <= PacketType.FriendshipBoundaryMaxValue;
	}

	public static bool IsFriendshipGroupPacket(this PacketType P_0)
	{
		return P_0 > PacketType.FriendshipGroupBoundaryMinValue && P_0 <= PacketType.FriendshipGroupBoundaryMaxValue;
	}

	public static bool IsUserStatusPacket(this PacketType P_0)
	{
		return P_0 > PacketType.UserStatusBoundaryMinValue && P_0 <= PacketType.UserStatusBoundaryMaxValue;
	}

	public static bool IsUserBlockPacket(this PacketType P_0)
	{
		return P_0 > PacketType.UserBlockBoundaryMinValue && P_0 <= PacketType.UserBlockBoundaryMaxValue;
	}

	public static bool IsChannelPacket(this PacketType P_0)
	{
		return P_0 > PacketType.ChannelBoundaryMinValue && P_0 <= PacketType.ChannelBoundaryMaxValue;
	}

	public static bool IsChannelGroupPacket(this PacketType P_0)
	{
		return P_0 > PacketType.ChannelGroupBoundaryMinValue && P_0 <= PacketType.ChannelGroupBoundaryMaxValue;
	}

	public static bool IsCommunityInfoPacket(this PacketType P_0)
	{
		return P_0 > PacketType.CommunityInfoBoundaryMinValue && P_0 <= PacketType.CommunityInfoBoundaryMaxValue;
	}

	public static bool IsRolePacket(this PacketType P_0)
	{
		return P_0 > PacketType.CommunityRoleBoundaryMinValue && P_0 <= PacketType.CommunityRoleBoundaryMaxValue;
	}

	public static bool IsCommunityMemberPacket(this PacketType P_0)
	{
		return P_0 > PacketType.CommunityMemberBoundaryMinValue && P_0 <= PacketType.CommunityMemberBoundaryMaxValue;
	}

	public static bool IsCommunityMemberRolePacket(this PacketType P_0)
	{
		return P_0 > PacketType.CommunityMemberRoleBoundaryMinValue && P_0 <= PacketType.CommunityMemberRoleBoundaryMaxValue;
	}

	public static bool IsChannelDirectoryPacket(this PacketType P_0)
	{
		return P_0 > PacketType.ChannelDirectoryBoundaryMinValue && P_0 <= PacketType.ChannelDirectoryBoundaryMaxValue;
	}

	public static bool IsChannelFilePacket(this PacketType P_0)
	{
		return P_0 > PacketType.ChannelFileBoundaryMinValue && P_0 <= PacketType.ChannelFileBoundaryMaxValue;
	}

	public static bool IsChannelMessagePacket(this PacketType P_0)
	{
		return P_0 > PacketType.ChannelMessageBoundaryMinValue && P_0 <= PacketType.ChannelMessageBoundaryMaxValue;
	}

	public static bool IsChannelWebRtcPacket(this PacketType P_0)
	{
		return P_0 > PacketType.ChannelWebRtcBoundaryMinValue && P_0 <= PacketType.ChannelWebRtcBoundaryMaxValue;
	}

	public static bool IsCommunityAppPacket(this PacketType P_0)
	{
		return P_0 > PacketType.CommunityAppBoundaryMinValue && P_0 <= PacketType.CommunityAppBoundaryMaxValue;
	}

	public static bool IsAssetPacket(this PacketType P_0)
	{
		return P_0 > PacketType.AssetBoundaryMinValue && P_0 <= PacketType.AssetBoundaryMaxValue;
	}
}
