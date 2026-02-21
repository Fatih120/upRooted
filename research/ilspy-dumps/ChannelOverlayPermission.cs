using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace RootApp.WebApi.Shared.Grpc;

public sealed class ChannelOverlayPermission : IMessage<ChannelOverlayPermission>, IMessage, IEquatable<ChannelOverlayPermission>, IDeepCloneable<ChannelOverlayPermission>, IBufferMessage
{
	private static readonly MessageParser<ChannelOverlayPermission> _parser = new MessageParser<ChannelOverlayPermission>(() => new ChannelOverlayPermission());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<bool?> _single_channelFullControl_codec = FieldCodec.ForStructWrapper<bool>(82u);

	private bool? channelFullControl_;

	private static readonly FieldCodec<bool?> _single_channelView_codec = FieldCodec.ForStructWrapper<bool>(98u);

	private bool? channelView_;

	private static readonly FieldCodec<bool?> _single_channelUseExternalEmoji_codec = FieldCodec.ForStructWrapper<bool>(106u);

	private bool? channelUseExternalEmoji_;

	private static readonly FieldCodec<bool?> _single_channelCreateMessage_codec = FieldCodec.ForStructWrapper<bool>(114u);

	private bool? channelCreateMessage_;

	private static readonly FieldCodec<bool?> _single_channelDeleteMessageOther_codec = FieldCodec.ForStructWrapper<bool>(122u);

	private bool? channelDeleteMessageOther_;

	private static readonly FieldCodec<bool?> _single_channelManagePinnedMessages_codec = FieldCodec.ForStructWrapper<bool>(130u);

	private bool? channelManagePinnedMessages_;

	private static readonly FieldCodec<bool?> _single_channelViewMessageHistory_codec = FieldCodec.ForStructWrapper<bool>(138u);

	private bool? channelViewMessageHistory_;

	private static readonly FieldCodec<bool?> _single_channelCreateMessageAttachment_codec = FieldCodec.ForStructWrapper<bool>(146u);

	private bool? channelCreateMessageAttachment_;

	private static readonly FieldCodec<bool?> _single_channelCreateMessageMention_codec = FieldCodec.ForStructWrapper<bool>(154u);

	private bool? channelCreateMessageMention_;

	private static readonly FieldCodec<bool?> _single_channelCreateMessageReaction_codec = FieldCodec.ForStructWrapper<bool>(162u);

	private bool? channelCreateMessageReaction_;

	private static readonly FieldCodec<bool?> _single_channelMakeMessagePublic_codec = FieldCodec.ForStructWrapper<bool>(170u);

	private bool? channelMakeMessagePublic_;

	private static readonly FieldCodec<bool?> _single_channelMoveUserOther_codec = FieldCodec.ForStructWrapper<bool>(178u);

	private bool? channelMoveUserOther_;

	private static readonly FieldCodec<bool?> _single_channelVoiceTalk_codec = FieldCodec.ForStructWrapper<bool>(186u);

	private bool? channelVoiceTalk_;

	private static readonly FieldCodec<bool?> _single_channelVoiceMuteOther_codec = FieldCodec.ForStructWrapper<bool>(194u);

	private bool? channelVoiceMuteOther_;

	private static readonly FieldCodec<bool?> _single_channelVoiceDeafenOther_codec = FieldCodec.ForStructWrapper<bool>(202u);

	private bool? channelVoiceDeafenOther_;

	private static readonly FieldCodec<bool?> _single_channelVoiceKick_codec = FieldCodec.ForStructWrapper<bool>(210u);

	private bool? channelVoiceKick_;

	private static readonly FieldCodec<bool?> _single_channelVideoStreamMedia_codec = FieldCodec.ForStructWrapper<bool>(218u);

	private bool? channelVideoStreamMedia_;

	private static readonly FieldCodec<bool?> _single_channelCreateFile_codec = FieldCodec.ForStructWrapper<bool>(226u);

	private bool? channelCreateFile_;

	private static readonly FieldCodec<bool?> _single_channelManageFiles_codec = FieldCodec.ForStructWrapper<bool>(234u);

	private bool? channelManageFiles_;

	private static readonly FieldCodec<bool?> _single_channelViewFile_codec = FieldCodec.ForStructWrapper<bool>(242u);

	private bool? channelViewFile_;

	private static readonly FieldCodec<bool?> _single_channelAppKick_codec = FieldCodec.ForStructWrapper<bool>(250u);

	private bool? channelAppKick_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelOverlayPermission> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PermissionReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public bool? ChannelFullControl
	{
		get
		{
			return channelFullControl_;
		}
		set
		{
			channelFullControl_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelView
	{
		get
		{
			return channelView_;
		}
		set
		{
			channelView_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelUseExternalEmoji
	{
		get
		{
			return channelUseExternalEmoji_;
		}
		set
		{
			channelUseExternalEmoji_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelCreateMessage
	{
		get
		{
			return channelCreateMessage_;
		}
		set
		{
			channelCreateMessage_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelDeleteMessageOther
	{
		get
		{
			return channelDeleteMessageOther_;
		}
		set
		{
			channelDeleteMessageOther_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelManagePinnedMessages
	{
		get
		{
			return channelManagePinnedMessages_;
		}
		set
		{
			channelManagePinnedMessages_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelViewMessageHistory
	{
		get
		{
			return channelViewMessageHistory_;
		}
		set
		{
			channelViewMessageHistory_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelCreateMessageAttachment
	{
		get
		{
			return channelCreateMessageAttachment_;
		}
		set
		{
			channelCreateMessageAttachment_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelCreateMessageMention
	{
		get
		{
			return channelCreateMessageMention_;
		}
		set
		{
			channelCreateMessageMention_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelCreateMessageReaction
	{
		get
		{
			return channelCreateMessageReaction_;
		}
		set
		{
			channelCreateMessageReaction_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelMakeMessagePublic
	{
		get
		{
			return channelMakeMessagePublic_;
		}
		set
		{
			channelMakeMessagePublic_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelMoveUserOther
	{
		get
		{
			return channelMoveUserOther_;
		}
		set
		{
			channelMoveUserOther_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelVoiceTalk
	{
		get
		{
			return channelVoiceTalk_;
		}
		set
		{
			channelVoiceTalk_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelVoiceMuteOther
	{
		get
		{
			return channelVoiceMuteOther_;
		}
		set
		{
			channelVoiceMuteOther_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelVoiceDeafenOther
	{
		get
		{
			return channelVoiceDeafenOther_;
		}
		set
		{
			channelVoiceDeafenOther_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelVoiceKick
	{
		get
		{
			return channelVoiceKick_;
		}
		set
		{
			channelVoiceKick_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelVideoStreamMedia
	{
		get
		{
			return channelVideoStreamMedia_;
		}
		set
		{
			channelVideoStreamMedia_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelCreateFile
	{
		get
		{
			return channelCreateFile_;
		}
		set
		{
			channelCreateFile_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelManageFiles
	{
		get
		{
			return channelManageFiles_;
		}
		set
		{
			channelManageFiles_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelViewFile
	{
		get
		{
			return channelViewFile_;
		}
		set
		{
			channelViewFile_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? ChannelAppKick
	{
		get
		{
			return channelAppKick_;
		}
		set
		{
			channelAppKick_ = value;
		}
	}

	public void MergeFieldMask(ChannelOverlayPermission other, FieldMask fieldMask, string startingPath)
	{
		if (other != this)
		{
			if (ChannelAppKick != other.ChannelAppKick)
			{
				fieldMask.Paths.Add(startingPath + "channel_app_kick");
			}
			if (ChannelCreateFile != other.ChannelCreateFile)
			{
				fieldMask.Paths.Add(startingPath + "channel_create_file");
			}
			if (ChannelCreateMessage != other.ChannelCreateMessage)
			{
				fieldMask.Paths.Add(startingPath + "channel_create_message");
			}
			if (ChannelCreateMessageAttachment != other.ChannelCreateMessageAttachment)
			{
				fieldMask.Paths.Add(startingPath + "channel_create_message_attachment");
			}
			if (ChannelCreateMessageMention != other.ChannelCreateMessageMention)
			{
				fieldMask.Paths.Add(startingPath + "channel_create_message_mention");
			}
			if (ChannelCreateMessageReaction != other.ChannelCreateMessageReaction)
			{
				fieldMask.Paths.Add(startingPath + "channel_create_message_reaction");
			}
			if (ChannelMakeMessagePublic != other.ChannelMakeMessagePublic)
			{
				fieldMask.Paths.Add(startingPath + "channel_make_message_public");
			}
			if (ChannelFullControl != other.ChannelFullControl)
			{
				fieldMask.Paths.Add(startingPath + "channel_full_control");
			}
			if (ChannelManageFiles != other.ChannelManageFiles)
			{
				fieldMask.Paths.Add(startingPath + "channel_manage_files");
			}
			if (ChannelDeleteMessageOther != other.ChannelDeleteMessageOther)
			{
				fieldMask.Paths.Add(startingPath + "channel_delete_message_other");
			}
			if (ChannelManagePinnedMessages != other.ChannelManagePinnedMessages)
			{
				fieldMask.Paths.Add(startingPath + "channel_manage_pinned_messages");
			}
			if (ChannelMoveUserOther != other.ChannelMoveUserOther)
			{
				fieldMask.Paths.Add(startingPath + "channel_move_user_other");
			}
			if (ChannelUseExternalEmoji != other.ChannelUseExternalEmoji)
			{
				fieldMask.Paths.Add(startingPath + "channel_use_external_emoji");
			}
			if (ChannelVideoStreamMedia != other.ChannelVideoStreamMedia)
			{
				fieldMask.Paths.Add(startingPath + "channel_video_stream_media");
			}
			if (ChannelView != other.ChannelView)
			{
				fieldMask.Paths.Add(startingPath + "channel_view");
			}
			if (ChannelViewFile != other.ChannelViewFile)
			{
				fieldMask.Paths.Add(startingPath + "channel_view_file");
			}
			if (ChannelViewMessageHistory != other.ChannelViewMessageHistory)
			{
				fieldMask.Paths.Add(startingPath + "channel_view_message_history");
			}
			if (ChannelVoiceDeafenOther != other.ChannelVoiceDeafenOther)
			{
				fieldMask.Paths.Add(startingPath + "channel_voice_deafen_other");
			}
			if (ChannelVoiceKick != other.ChannelVoiceKick)
			{
				fieldMask.Paths.Add(startingPath + "channel_voice_kick");
			}
			if (ChannelVoiceMuteOther != other.ChannelVoiceMuteOther)
			{
				fieldMask.Paths.Add(startingPath + "channel_voice_mute_other");
			}
			if (ChannelVoiceTalk != other.ChannelVoiceTalk)
			{
				fieldMask.Paths.Add(startingPath + "channel_voice_talk");
			}
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelOverlayPermission()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelOverlayPermission(ChannelOverlayPermission other)
		: this()
	{
		ChannelFullControl = other.ChannelFullControl;
		ChannelView = other.ChannelView;
		ChannelUseExternalEmoji = other.ChannelUseExternalEmoji;
		ChannelCreateMessage = other.ChannelCreateMessage;
		ChannelDeleteMessageOther = other.ChannelDeleteMessageOther;
		ChannelManagePinnedMessages = other.ChannelManagePinnedMessages;
		ChannelViewMessageHistory = other.ChannelViewMessageHistory;
		ChannelCreateMessageAttachment = other.ChannelCreateMessageAttachment;
		ChannelCreateMessageMention = other.ChannelCreateMessageMention;
		ChannelCreateMessageReaction = other.ChannelCreateMessageReaction;
		ChannelMakeMessagePublic = other.ChannelMakeMessagePublic;
		ChannelMoveUserOther = other.ChannelMoveUserOther;
		ChannelVoiceTalk = other.ChannelVoiceTalk;
		ChannelVoiceMuteOther = other.ChannelVoiceMuteOther;
		ChannelVoiceDeafenOther = other.ChannelVoiceDeafenOther;
		ChannelVoiceKick = other.ChannelVoiceKick;
		ChannelVideoStreamMedia = other.ChannelVideoStreamMedia;
		ChannelCreateFile = other.ChannelCreateFile;
		ChannelManageFiles = other.ChannelManageFiles;
		ChannelViewFile = other.ChannelViewFile;
		ChannelAppKick = other.ChannelAppKick;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelOverlayPermission Clone()
	{
		return new ChannelOverlayPermission(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelOverlayPermission);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelOverlayPermission other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ChannelFullControl != other.ChannelFullControl)
		{
			return false;
		}
		if (ChannelView != other.ChannelView)
		{
			return false;
		}
		if (ChannelUseExternalEmoji != other.ChannelUseExternalEmoji)
		{
			return false;
		}
		if (ChannelCreateMessage != other.ChannelCreateMessage)
		{
			return false;
		}
		if (ChannelDeleteMessageOther != other.ChannelDeleteMessageOther)
		{
			return false;
		}
		if (ChannelManagePinnedMessages != other.ChannelManagePinnedMessages)
		{
			return false;
		}
		if (ChannelViewMessageHistory != other.ChannelViewMessageHistory)
		{
			return false;
		}
		if (ChannelCreateMessageAttachment != other.ChannelCreateMessageAttachment)
		{
			return false;
		}
		if (ChannelCreateMessageMention != other.ChannelCreateMessageMention)
		{
			return false;
		}
		if (ChannelCreateMessageReaction != other.ChannelCreateMessageReaction)
		{
			return false;
		}
		if (ChannelMakeMessagePublic != other.ChannelMakeMessagePublic)
		{
			return false;
		}
		if (ChannelMoveUserOther != other.ChannelMoveUserOther)
		{
			return false;
		}
		if (ChannelVoiceTalk != other.ChannelVoiceTalk)
		{
			return false;
		}
		if (ChannelVoiceMuteOther != other.ChannelVoiceMuteOther)
		{
			return false;
		}
		if (ChannelVoiceDeafenOther != other.ChannelVoiceDeafenOther)
		{
			return false;
		}
		if (ChannelVoiceKick != other.ChannelVoiceKick)
		{
			return false;
		}
		if (ChannelVideoStreamMedia != other.ChannelVideoStreamMedia)
		{
			return false;
		}
		if (ChannelCreateFile != other.ChannelCreateFile)
		{
			return false;
		}
		if (ChannelManageFiles != other.ChannelManageFiles)
		{
			return false;
		}
		if (ChannelViewFile != other.ChannelViewFile)
		{
			return false;
		}
		if (ChannelAppKick != other.ChannelAppKick)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (channelFullControl_.HasValue)
		{
			num ^= ChannelFullControl.GetHashCode();
		}
		if (channelView_.HasValue)
		{
			num ^= ChannelView.GetHashCode();
		}
		if (channelUseExternalEmoji_.HasValue)
		{
			num ^= ChannelUseExternalEmoji.GetHashCode();
		}
		if (channelCreateMessage_.HasValue)
		{
			num ^= ChannelCreateMessage.GetHashCode();
		}
		if (channelDeleteMessageOther_.HasValue)
		{
			num ^= ChannelDeleteMessageOther.GetHashCode();
		}
		if (channelManagePinnedMessages_.HasValue)
		{
			num ^= ChannelManagePinnedMessages.GetHashCode();
		}
		if (channelViewMessageHistory_.HasValue)
		{
			num ^= ChannelViewMessageHistory.GetHashCode();
		}
		if (channelCreateMessageAttachment_.HasValue)
		{
			num ^= ChannelCreateMessageAttachment.GetHashCode();
		}
		if (channelCreateMessageMention_.HasValue)
		{
			num ^= ChannelCreateMessageMention.GetHashCode();
		}
		if (channelCreateMessageReaction_.HasValue)
		{
			num ^= ChannelCreateMessageReaction.GetHashCode();
		}
		if (channelMakeMessagePublic_.HasValue)
		{
			num ^= ChannelMakeMessagePublic.GetHashCode();
		}
		if (channelMoveUserOther_.HasValue)
		{
			num ^= ChannelMoveUserOther.GetHashCode();
		}
		if (channelVoiceTalk_.HasValue)
		{
			num ^= ChannelVoiceTalk.GetHashCode();
		}
		if (channelVoiceMuteOther_.HasValue)
		{
			num ^= ChannelVoiceMuteOther.GetHashCode();
		}
		if (channelVoiceDeafenOther_.HasValue)
		{
			num ^= ChannelVoiceDeafenOther.GetHashCode();
		}
		if (channelVoiceKick_.HasValue)
		{
			num ^= ChannelVoiceKick.GetHashCode();
		}
		if (channelVideoStreamMedia_.HasValue)
		{
			num ^= ChannelVideoStreamMedia.GetHashCode();
		}
		if (channelCreateFile_.HasValue)
		{
			num ^= ChannelCreateFile.GetHashCode();
		}
		if (channelManageFiles_.HasValue)
		{
			num ^= ChannelManageFiles.GetHashCode();
		}
		if (channelViewFile_.HasValue)
		{
			num ^= ChannelViewFile.GetHashCode();
		}
		if (channelAppKick_.HasValue)
		{
			num ^= ChannelAppKick.GetHashCode();
		}
		if (_unknownFields != null)
		{
			num ^= _unknownFields.GetHashCode();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public override string ToString()
	{
		return JsonFormatter.ToDiagnosticString(this);
	}

	[GeneratedCode("protoc", null)]
	public void WriteTo(CodedOutputStream output)
	{
		output.WriteRawMessage(this);
	}

	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalWriteTo(ref WriteContext P_0)
	{
		if (channelFullControl_.HasValue)
		{
			_single_channelFullControl_codec.WriteTagAndValue(ref P_0, ChannelFullControl);
		}
		if (channelView_.HasValue)
		{
			_single_channelView_codec.WriteTagAndValue(ref P_0, ChannelView);
		}
		if (channelUseExternalEmoji_.HasValue)
		{
			_single_channelUseExternalEmoji_codec.WriteTagAndValue(ref P_0, ChannelUseExternalEmoji);
		}
		if (channelCreateMessage_.HasValue)
		{
			_single_channelCreateMessage_codec.WriteTagAndValue(ref P_0, ChannelCreateMessage);
		}
		if (channelDeleteMessageOther_.HasValue)
		{
			_single_channelDeleteMessageOther_codec.WriteTagAndValue(ref P_0, ChannelDeleteMessageOther);
		}
		if (channelManagePinnedMessages_.HasValue)
		{
			_single_channelManagePinnedMessages_codec.WriteTagAndValue(ref P_0, ChannelManagePinnedMessages);
		}
		if (channelViewMessageHistory_.HasValue)
		{
			_single_channelViewMessageHistory_codec.WriteTagAndValue(ref P_0, ChannelViewMessageHistory);
		}
		if (channelCreateMessageAttachment_.HasValue)
		{
			_single_channelCreateMessageAttachment_codec.WriteTagAndValue(ref P_0, ChannelCreateMessageAttachment);
		}
		if (channelCreateMessageMention_.HasValue)
		{
			_single_channelCreateMessageMention_codec.WriteTagAndValue(ref P_0, ChannelCreateMessageMention);
		}
		if (channelCreateMessageReaction_.HasValue)
		{
			_single_channelCreateMessageReaction_codec.WriteTagAndValue(ref P_0, ChannelCreateMessageReaction);
		}
		if (channelMakeMessagePublic_.HasValue)
		{
			_single_channelMakeMessagePublic_codec.WriteTagAndValue(ref P_0, ChannelMakeMessagePublic);
		}
		if (channelMoveUserOther_.HasValue)
		{
			_single_channelMoveUserOther_codec.WriteTagAndValue(ref P_0, ChannelMoveUserOther);
		}
		if (channelVoiceTalk_.HasValue)
		{
			_single_channelVoiceTalk_codec.WriteTagAndValue(ref P_0, ChannelVoiceTalk);
		}
		if (channelVoiceMuteOther_.HasValue)
		{
			_single_channelVoiceMuteOther_codec.WriteTagAndValue(ref P_0, ChannelVoiceMuteOther);
		}
		if (channelVoiceDeafenOther_.HasValue)
		{
			_single_channelVoiceDeafenOther_codec.WriteTagAndValue(ref P_0, ChannelVoiceDeafenOther);
		}
		if (channelVoiceKick_.HasValue)
		{
			_single_channelVoiceKick_codec.WriteTagAndValue(ref P_0, ChannelVoiceKick);
		}
		if (channelVideoStreamMedia_.HasValue)
		{
			_single_channelVideoStreamMedia_codec.WriteTagAndValue(ref P_0, ChannelVideoStreamMedia);
		}
		if (channelCreateFile_.HasValue)
		{
			_single_channelCreateFile_codec.WriteTagAndValue(ref P_0, ChannelCreateFile);
		}
		if (channelManageFiles_.HasValue)
		{
			_single_channelManageFiles_codec.WriteTagAndValue(ref P_0, ChannelManageFiles);
		}
		if (channelViewFile_.HasValue)
		{
			_single_channelViewFile_codec.WriteTagAndValue(ref P_0, ChannelViewFile);
		}
		if (channelAppKick_.HasValue)
		{
			_single_channelAppKick_codec.WriteTagAndValue(ref P_0, ChannelAppKick);
		}
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (channelFullControl_.HasValue)
		{
			num += _single_channelFullControl_codec.CalculateSizeWithTag(ChannelFullControl);
		}
		if (channelView_.HasValue)
		{
			num += _single_channelView_codec.CalculateSizeWithTag(ChannelView);
		}
		if (channelUseExternalEmoji_.HasValue)
		{
			num += _single_channelUseExternalEmoji_codec.CalculateSizeWithTag(ChannelUseExternalEmoji);
		}
		if (channelCreateMessage_.HasValue)
		{
			num += _single_channelCreateMessage_codec.CalculateSizeWithTag(ChannelCreateMessage);
		}
		if (channelDeleteMessageOther_.HasValue)
		{
			num += _single_channelDeleteMessageOther_codec.CalculateSizeWithTag(ChannelDeleteMessageOther);
		}
		if (channelManagePinnedMessages_.HasValue)
		{
			num += _single_channelManagePinnedMessages_codec.CalculateSizeWithTag(ChannelManagePinnedMessages);
		}
		if (channelViewMessageHistory_.HasValue)
		{
			num += _single_channelViewMessageHistory_codec.CalculateSizeWithTag(ChannelViewMessageHistory);
		}
		if (channelCreateMessageAttachment_.HasValue)
		{
			num += _single_channelCreateMessageAttachment_codec.CalculateSizeWithTag(ChannelCreateMessageAttachment);
		}
		if (channelCreateMessageMention_.HasValue)
		{
			num += _single_channelCreateMessageMention_codec.CalculateSizeWithTag(ChannelCreateMessageMention);
		}
		if (channelCreateMessageReaction_.HasValue)
		{
			num += _single_channelCreateMessageReaction_codec.CalculateSizeWithTag(ChannelCreateMessageReaction);
		}
		if (channelMakeMessagePublic_.HasValue)
		{
			num += _single_channelMakeMessagePublic_codec.CalculateSizeWithTag(ChannelMakeMessagePublic);
		}
		if (channelMoveUserOther_.HasValue)
		{
			num += _single_channelMoveUserOther_codec.CalculateSizeWithTag(ChannelMoveUserOther);
		}
		if (channelVoiceTalk_.HasValue)
		{
			num += _single_channelVoiceTalk_codec.CalculateSizeWithTag(ChannelVoiceTalk);
		}
		if (channelVoiceMuteOther_.HasValue)
		{
			num += _single_channelVoiceMuteOther_codec.CalculateSizeWithTag(ChannelVoiceMuteOther);
		}
		if (channelVoiceDeafenOther_.HasValue)
		{
			num += _single_channelVoiceDeafenOther_codec.CalculateSizeWithTag(ChannelVoiceDeafenOther);
		}
		if (channelVoiceKick_.HasValue)
		{
			num += _single_channelVoiceKick_codec.CalculateSizeWithTag(ChannelVoiceKick);
		}
		if (channelVideoStreamMedia_.HasValue)
		{
			num += _single_channelVideoStreamMedia_codec.CalculateSizeWithTag(ChannelVideoStreamMedia);
		}
		if (channelCreateFile_.HasValue)
		{
			num += _single_channelCreateFile_codec.CalculateSizeWithTag(ChannelCreateFile);
		}
		if (channelManageFiles_.HasValue)
		{
			num += _single_channelManageFiles_codec.CalculateSizeWithTag(ChannelManageFiles);
		}
		if (channelViewFile_.HasValue)
		{
			num += _single_channelViewFile_codec.CalculateSizeWithTag(ChannelViewFile);
		}
		if (channelAppKick_.HasValue)
		{
			num += _single_channelAppKick_codec.CalculateSizeWithTag(ChannelAppKick);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelOverlayPermission other)
	{
		if (other != null)
		{
			if (other.channelFullControl_.HasValue && (!channelFullControl_.HasValue || other.ChannelFullControl != false))
			{
				ChannelFullControl = other.ChannelFullControl;
			}
			if (other.channelView_.HasValue && (!channelView_.HasValue || other.ChannelView != false))
			{
				ChannelView = other.ChannelView;
			}
			if (other.channelUseExternalEmoji_.HasValue && (!channelUseExternalEmoji_.HasValue || other.ChannelUseExternalEmoji != false))
			{
				ChannelUseExternalEmoji = other.ChannelUseExternalEmoji;
			}
			if (other.channelCreateMessage_.HasValue && (!channelCreateMessage_.HasValue || other.ChannelCreateMessage != false))
			{
				ChannelCreateMessage = other.ChannelCreateMessage;
			}
			if (other.channelDeleteMessageOther_.HasValue && (!channelDeleteMessageOther_.HasValue || other.ChannelDeleteMessageOther != false))
			{
				ChannelDeleteMessageOther = other.ChannelDeleteMessageOther;
			}
			if (other.channelManagePinnedMessages_.HasValue && (!channelManagePinnedMessages_.HasValue || other.ChannelManagePinnedMessages != false))
			{
				ChannelManagePinnedMessages = other.ChannelManagePinnedMessages;
			}
			if (other.channelViewMessageHistory_.HasValue && (!channelViewMessageHistory_.HasValue || other.ChannelViewMessageHistory != false))
			{
				ChannelViewMessageHistory = other.ChannelViewMessageHistory;
			}
			if (other.channelCreateMessageAttachment_.HasValue && (!channelCreateMessageAttachment_.HasValue || other.ChannelCreateMessageAttachment != false))
			{
				ChannelCreateMessageAttachment = other.ChannelCreateMessageAttachment;
			}
			if (other.channelCreateMessageMention_.HasValue && (!channelCreateMessageMention_.HasValue || other.ChannelCreateMessageMention != false))
			{
				ChannelCreateMessageMention = other.ChannelCreateMessageMention;
			}
			if (other.channelCreateMessageReaction_.HasValue && (!channelCreateMessageReaction_.HasValue || other.ChannelCreateMessageReaction != false))
			{
				ChannelCreateMessageReaction = other.ChannelCreateMessageReaction;
			}
			if (other.channelMakeMessagePublic_.HasValue && (!channelMakeMessagePublic_.HasValue || other.ChannelMakeMessagePublic != false))
			{
				ChannelMakeMessagePublic = other.ChannelMakeMessagePublic;
			}
			if (other.channelMoveUserOther_.HasValue && (!channelMoveUserOther_.HasValue || other.ChannelMoveUserOther != false))
			{
				ChannelMoveUserOther = other.ChannelMoveUserOther;
			}
			if (other.channelVoiceTalk_.HasValue && (!channelVoiceTalk_.HasValue || other.ChannelVoiceTalk != false))
			{
				ChannelVoiceTalk = other.ChannelVoiceTalk;
			}
			if (other.channelVoiceMuteOther_.HasValue && (!channelVoiceMuteOther_.HasValue || other.ChannelVoiceMuteOther != false))
			{
				ChannelVoiceMuteOther = other.ChannelVoiceMuteOther;
			}
			if (other.channelVoiceDeafenOther_.HasValue && (!channelVoiceDeafenOther_.HasValue || other.ChannelVoiceDeafenOther != false))
			{
				ChannelVoiceDeafenOther = other.ChannelVoiceDeafenOther;
			}
			if (other.channelVoiceKick_.HasValue && (!channelVoiceKick_.HasValue || other.ChannelVoiceKick != false))
			{
				ChannelVoiceKick = other.ChannelVoiceKick;
			}
			if (other.channelVideoStreamMedia_.HasValue && (!channelVideoStreamMedia_.HasValue || other.ChannelVideoStreamMedia != false))
			{
				ChannelVideoStreamMedia = other.ChannelVideoStreamMedia;
			}
			if (other.channelCreateFile_.HasValue && (!channelCreateFile_.HasValue || other.ChannelCreateFile != false))
			{
				ChannelCreateFile = other.ChannelCreateFile;
			}
			if (other.channelManageFiles_.HasValue && (!channelManageFiles_.HasValue || other.ChannelManageFiles != false))
			{
				ChannelManageFiles = other.ChannelManageFiles;
			}
			if (other.channelViewFile_.HasValue && (!channelViewFile_.HasValue || other.ChannelViewFile != false))
			{
				ChannelViewFile = other.ChannelViewFile;
			}
			if (other.channelAppKick_.HasValue && (!channelAppKick_.HasValue || other.ChannelAppKick != false))
			{
				ChannelAppKick = other.ChannelAppKick;
			}
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CodedInputStream input)
	{
		input.ReadRawMessage(this);
	}

	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalMergeFrom(ref ParseContext P_0)
	{
		uint num;
		while ((num = P_0.ReadTag()) != 0 && (num & 7) != 4)
		{
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 82u:
			{
				bool? flag10 = _single_channelFullControl_codec.Read(ref P_0);
				if (!channelFullControl_.HasValue || flag10 != false)
				{
					ChannelFullControl = flag10;
				}
				break;
			}
			case 98u:
			{
				bool? flag3 = _single_channelView_codec.Read(ref P_0);
				if (!channelView_.HasValue || flag3 != false)
				{
					ChannelView = flag3;
				}
				break;
			}
			case 106u:
			{
				bool? flag19 = _single_channelUseExternalEmoji_codec.Read(ref P_0);
				if (!channelUseExternalEmoji_.HasValue || flag19 != false)
				{
					ChannelUseExternalEmoji = flag19;
				}
				break;
			}
			case 114u:
			{
				bool? flag13 = _single_channelCreateMessage_codec.Read(ref P_0);
				if (!channelCreateMessage_.HasValue || flag13 != false)
				{
					ChannelCreateMessage = flag13;
				}
				break;
			}
			case 122u:
			{
				bool? flag11 = _single_channelDeleteMessageOther_codec.Read(ref P_0);
				if (!channelDeleteMessageOther_.HasValue || flag11 != false)
				{
					ChannelDeleteMessageOther = flag11;
				}
				break;
			}
			case 130u:
			{
				bool? flag20 = _single_channelManagePinnedMessages_codec.Read(ref P_0);
				if (!channelManagePinnedMessages_.HasValue || flag20 != false)
				{
					ChannelManagePinnedMessages = flag20;
				}
				break;
			}
			case 138u:
			{
				bool? flag17 = _single_channelViewMessageHistory_codec.Read(ref P_0);
				if (!channelViewMessageHistory_.HasValue || flag17 != false)
				{
					ChannelViewMessageHistory = flag17;
				}
				break;
			}
			case 146u:
			{
				bool? flag5 = _single_channelCreateMessageAttachment_codec.Read(ref P_0);
				if (!channelCreateMessageAttachment_.HasValue || flag5 != false)
				{
					ChannelCreateMessageAttachment = flag5;
				}
				break;
			}
			case 154u:
			{
				bool? flag2 = _single_channelCreateMessageMention_codec.Read(ref P_0);
				if (!channelCreateMessageMention_.HasValue || flag2 != false)
				{
					ChannelCreateMessageMention = flag2;
				}
				break;
			}
			case 162u:
			{
				bool? flag16 = _single_channelCreateMessageReaction_codec.Read(ref P_0);
				if (!channelCreateMessageReaction_.HasValue || flag16 != false)
				{
					ChannelCreateMessageReaction = flag16;
				}
				break;
			}
			case 170u:
			{
				bool? flag14 = _single_channelMakeMessagePublic_codec.Read(ref P_0);
				if (!channelMakeMessagePublic_.HasValue || flag14 != false)
				{
					ChannelMakeMessagePublic = flag14;
				}
				break;
			}
			case 178u:
			{
				bool? flag8 = _single_channelMoveUserOther_codec.Read(ref P_0);
				if (!channelMoveUserOther_.HasValue || flag8 != false)
				{
					ChannelMoveUserOther = flag8;
				}
				break;
			}
			case 186u:
			{
				bool? flag6 = _single_channelVoiceTalk_codec.Read(ref P_0);
				if (!channelVoiceTalk_.HasValue || flag6 != false)
				{
					ChannelVoiceTalk = flag6;
				}
				break;
			}
			case 194u:
			{
				bool? flag21 = _single_channelVoiceMuteOther_codec.Read(ref P_0);
				if (!channelVoiceMuteOther_.HasValue || flag21 != false)
				{
					ChannelVoiceMuteOther = flag21;
				}
				break;
			}
			case 202u:
			{
				bool? flag18 = _single_channelVoiceDeafenOther_codec.Read(ref P_0);
				if (!channelVoiceDeafenOther_.HasValue || flag18 != false)
				{
					ChannelVoiceDeafenOther = flag18;
				}
				break;
			}
			case 210u:
			{
				bool? flag15 = _single_channelVoiceKick_codec.Read(ref P_0);
				if (!channelVoiceKick_.HasValue || flag15 != false)
				{
					ChannelVoiceKick = flag15;
				}
				break;
			}
			case 218u:
			{
				bool? flag12 = _single_channelVideoStreamMedia_codec.Read(ref P_0);
				if (!channelVideoStreamMedia_.HasValue || flag12 != false)
				{
					ChannelVideoStreamMedia = flag12;
				}
				break;
			}
			case 226u:
			{
				bool? flag9 = _single_channelCreateFile_codec.Read(ref P_0);
				if (!channelCreateFile_.HasValue || flag9 != false)
				{
					ChannelCreateFile = flag9;
				}
				break;
			}
			case 234u:
			{
				bool? flag7 = _single_channelManageFiles_codec.Read(ref P_0);
				if (!channelManageFiles_.HasValue || flag7 != false)
				{
					ChannelManageFiles = flag7;
				}
				break;
			}
			case 242u:
			{
				bool? flag4 = _single_channelViewFile_codec.Read(ref P_0);
				if (!channelViewFile_.HasValue || flag4 != false)
				{
					ChannelViewFile = flag4;
				}
				break;
			}
			case 250u:
			{
				bool? flag = _single_channelAppKick_codec.Read(ref P_0);
				if (!channelAppKick_.HasValue || flag != false)
				{
					ChannelAppKick = flag;
				}
				break;
			}
			}
		}
	}
}
