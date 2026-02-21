namespace RootApp.WebApi.Shared.Permissions;

public interface IChannelPermissions
{
	bool ChannelFullControl { get; set; }

	bool ChannelView { get; set; }

	bool ChannelUseExternalEmoji { get; set; }

	bool ChannelCreateMessage { get; set; }

	bool ChannelDeleteMessageOther { get; set; }

	bool ChannelManagePinnedMessages { get; set; }

	bool ChannelViewMessageHistory { get; set; }

	bool ChannelCreateMessageAttachment { get; set; }

	bool ChannelCreateMessageMention { get; set; }

	bool ChannelCreateMessageReaction { get; set; }

	bool ChannelMakeMessagePublic { get; set; }

	bool ChannelMoveUserOther { get; set; }

	bool ChannelVoiceTalk { get; set; }

	bool ChannelVoiceMuteOther { get; set; }

	bool ChannelVoiceDeafenOther { get; set; }

	bool ChannelVoiceKick { get; set; }

	bool ChannelVideoStreamMedia { get; set; }

	bool ChannelManageFiles { get; set; }

	bool ChannelCreateFile { get; set; }

	bool ChannelViewFile { get; set; }

	bool ChannelAppKick { get; set; }
}
