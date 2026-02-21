using System;
using System.Runtime.CompilerServices;

namespace RootApp.WebApi.Shared.Permissions;

public sealed class ChannelPermission : IChannelPermissions, IEquatable<ChannelPermission>
{
	[CompilerGenerated]
	private bool _003CChannelFullControl_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelView_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelUseExternalEmoji_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelCreateMessage_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelDeleteMessageOther_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelManagePinnedMessages_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelViewMessageHistory_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelCreateMessageAttachment_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelCreateMessageMention_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelCreateMessageReaction_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelMakeMessagePublic_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelMoveUserOther_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelVoiceTalk_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelVoiceMuteOther_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelVoiceDeafenOther_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelVoiceKick_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelVideoStreamMedia_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelManageFiles_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelCreateFile_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelViewFile_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChannelAppKick_003Ek__BackingField;

	public bool ChannelFullControl
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelFullControl_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelFullControl_003Ek__BackingField = flag;
		}
	}

	public bool ChannelView
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelView_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelView_003Ek__BackingField = flag;
		}
	}

	public bool ChannelUseExternalEmoji
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelUseExternalEmoji_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelUseExternalEmoji_003Ek__BackingField = flag;
		}
	}

	public bool ChannelCreateMessage
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelCreateMessage_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelCreateMessage_003Ek__BackingField = flag;
		}
	}

	public bool ChannelDeleteMessageOther
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelDeleteMessageOther_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelDeleteMessageOther_003Ek__BackingField = flag;
		}
	}

	public bool ChannelManagePinnedMessages
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelManagePinnedMessages_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelManagePinnedMessages_003Ek__BackingField = flag;
		}
	}

	public bool ChannelViewMessageHistory
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelViewMessageHistory_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelViewMessageHistory_003Ek__BackingField = flag;
		}
	}

	public bool ChannelCreateMessageAttachment
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelCreateMessageAttachment_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelCreateMessageAttachment_003Ek__BackingField = flag;
		}
	}

	public bool ChannelCreateMessageMention
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelCreateMessageMention_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelCreateMessageMention_003Ek__BackingField = flag;
		}
	}

	public bool ChannelCreateMessageReaction
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelCreateMessageReaction_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelCreateMessageReaction_003Ek__BackingField = flag;
		}
	}

	public bool ChannelMakeMessagePublic
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelMakeMessagePublic_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelMakeMessagePublic_003Ek__BackingField = flag;
		}
	}

	public bool ChannelMoveUserOther
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelMoveUserOther_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelMoveUserOther_003Ek__BackingField = flag;
		}
	}

	public bool ChannelVoiceTalk
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelVoiceTalk_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelVoiceTalk_003Ek__BackingField = flag;
		}
	}

	public bool ChannelVoiceMuteOther
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelVoiceMuteOther_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelVoiceMuteOther_003Ek__BackingField = flag;
		}
	}

	public bool ChannelVoiceDeafenOther
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelVoiceDeafenOther_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelVoiceDeafenOther_003Ek__BackingField = flag;
		}
	}

	public bool ChannelVoiceKick
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelVoiceKick_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelVoiceKick_003Ek__BackingField = flag;
		}
	}

	public bool ChannelVideoStreamMedia
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelVideoStreamMedia_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelVideoStreamMedia_003Ek__BackingField = flag;
		}
	}

	public bool ChannelManageFiles
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelManageFiles_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelManageFiles_003Ek__BackingField = flag;
		}
	}

	public bool ChannelCreateFile
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelCreateFile_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelCreateFile_003Ek__BackingField = flag;
		}
	}

	public bool ChannelViewFile
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelViewFile_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelViewFile_003Ek__BackingField = flag;
		}
	}

	public bool ChannelAppKick
	{
		[CompilerGenerated]
		get
		{
			return _003CChannelAppKick_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CChannelAppKick_003Ek__BackingField = flag;
		}
	}

	public bool Equals(ChannelPermission? P_0)
	{
		if ((object)P_0 == null)
		{
			return false;
		}
		return ChannelPermissionsExtensions.Equals(this, P_0);
	}

	public override bool Equals(object? P_0)
	{
		return P_0 is ChannelPermission channelPermission && Equals(channelPermission);
	}

	public static bool operator ==(ChannelPermission? P_0, ChannelPermission? P_1)
	{
		if ((object)P_0 == P_1)
		{
			return true;
		}
		return (object)P_0 != null && (object)P_1 != null && P_0.Equals(P_1);
	}

	public static bool operator !=(ChannelPermission? P_0, ChannelPermission? P_1)
	{
		return !(P_0 == P_1);
	}

	public void SetAll(bool P_0)
	{
		ChannelPermissionsExtensions.SetAll(this, P_0);
	}

	public override int GetHashCode()
	{
		HashCode hashCode = default(HashCode);
		hashCode.Add(ChannelFullControl);
		hashCode.Add(ChannelView);
		hashCode.Add(ChannelUseExternalEmoji);
		hashCode.Add(ChannelCreateMessage);
		hashCode.Add(ChannelDeleteMessageOther);
		hashCode.Add(ChannelManagePinnedMessages);
		hashCode.Add(ChannelViewMessageHistory);
		hashCode.Add(ChannelCreateMessageAttachment);
		hashCode.Add(ChannelCreateMessageMention);
		hashCode.Add(ChannelCreateMessageReaction);
		hashCode.Add(ChannelMakeMessagePublic);
		hashCode.Add(ChannelMoveUserOther);
		hashCode.Add(ChannelVoiceTalk);
		hashCode.Add(ChannelVoiceMuteOther);
		hashCode.Add(ChannelVoiceDeafenOther);
		hashCode.Add(ChannelVoiceKick);
		hashCode.Add(ChannelVideoStreamMedia);
		hashCode.Add(ChannelManageFiles);
		hashCode.Add(ChannelCreateFile);
		hashCode.Add(ChannelViewFile);
		hashCode.Add(ChannelAppKick);
		return hashCode.ToHashCode();
	}
}
