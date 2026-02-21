using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc;

namespace RootApp.WebApi.Shared.Packets;

public sealed class CommunityPermissionUpdatePacket : IPacketCommunity, IPacket, IMessage<CommunityPermissionUpdatePacket>, IMessage, IEquatable<CommunityPermissionUpdatePacket>, IDeepCloneable<CommunityPermissionUpdatePacket>, IBufferMessage
{
	private static readonly MessageParser<CommunityPermissionUpdatePacket> _parser = new MessageParser<CommunityPermissionUpdatePacket>(() => new CommunityPermissionUpdatePacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private CommunityPermission communityPermission_;

	private static readonly FieldCodec<ChannelGroupCreatedPacket> _repeated_channelGroupsCreated_codec = FieldCodec.ForMessage(42u, ChannelGroupCreatedPacket.Parser);

	private readonly RepeatedField<ChannelGroupCreatedPacket> channelGroupsCreated_ = new RepeatedField<ChannelGroupCreatedPacket>();

	private static readonly FieldCodec<ChannelGroupEditedPacket> _repeated_channelGroupsEdited_codec = FieldCodec.ForMessage(50u, ChannelGroupEditedPacket.Parser);

	private readonly RepeatedField<ChannelGroupEditedPacket> channelGroupsEdited_ = new RepeatedField<ChannelGroupEditedPacket>();

	private static readonly FieldCodec<ChannelGroupMovedPacket> _repeated_channelGroupsMoved_codec = FieldCodec.ForMessage(58u, ChannelGroupMovedPacket.Parser);

	private readonly RepeatedField<ChannelGroupMovedPacket> channelGroupsMoved_ = new RepeatedField<ChannelGroupMovedPacket>();

	private static readonly FieldCodec<ChannelGroupDeletedPacket> _repeated_channelGroupsDeleted_codec = FieldCodec.ForMessage(66u, ChannelGroupDeletedPacket.Parser);

	private readonly RepeatedField<ChannelGroupDeletedPacket> channelGroupsDeleted_ = new RepeatedField<ChannelGroupDeletedPacket>();

	private static readonly FieldCodec<ChannelCreatedPacket> _repeated_channelsCreated_codec = FieldCodec.ForMessage(74u, ChannelCreatedPacket.Parser);

	private readonly RepeatedField<ChannelCreatedPacket> channelsCreated_ = new RepeatedField<ChannelCreatedPacket>();

	private static readonly FieldCodec<ChannelEditedPacket> _repeated_channelsEdited_codec = FieldCodec.ForMessage(82u, ChannelEditedPacket.Parser);

	private readonly RepeatedField<ChannelEditedPacket> channelsEdited_ = new RepeatedField<ChannelEditedPacket>();

	private static readonly FieldCodec<ChannelMovedPacket> _repeated_channelsMoved_codec = FieldCodec.ForMessage(90u, ChannelMovedPacket.Parser);

	private readonly RepeatedField<ChannelMovedPacket> channelsMoved_ = new RepeatedField<ChannelMovedPacket>();

	private static readonly FieldCodec<ChannelDeletedPacket> _repeated_channelsDeleted_codec = FieldCodec.ForMessage(98u, ChannelDeletedPacket.Parser);

	private readonly RepeatedField<ChannelDeletedPacket> channelsDeleted_ = new RepeatedField<ChannelDeletedPacket>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityPermissionUpdatePacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[17];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public PacketType PacketType
	{
		get
		{
			return packetType_;
		}
		set
		{
			packetType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityPermission CommunityPermission
	{
		get
		{
			return communityPermission_;
		}
		set
		{
			communityPermission_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelGroupCreatedPacket> ChannelGroupsCreated => channelGroupsCreated_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelGroupEditedPacket> ChannelGroupsEdited => channelGroupsEdited_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelGroupMovedPacket> ChannelGroupsMoved => channelGroupsMoved_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelGroupDeletedPacket> ChannelGroupsDeleted => channelGroupsDeleted_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelCreatedPacket> ChannelsCreated => channelsCreated_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelEditedPacket> ChannelsEdited => channelsEdited_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelMovedPacket> ChannelsMoved => channelsMoved_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelDeletedPacket> ChannelsDeleted => channelsDeleted_;

	public bool IsEmpty()
	{
		return CommunityPermission == null && (ChannelGroupsCreated == null || ChannelGroupsCreated.Count == 0) && (ChannelGroupsEdited == null || ChannelGroupsEdited.Count == 0) && (ChannelGroupsMoved == null || ChannelGroupsMoved.Count == 0) && (ChannelGroupsDeleted == null || ChannelGroupsDeleted.Count == 0) && (ChannelsCreated == null || ChannelsCreated.Count == 0) && (ChannelsEdited == null || ChannelsEdited.Count == 0) && (ChannelsMoved == null || ChannelsMoved.Count == 0) && (ChannelsDeleted == null || ChannelsDeleted.Count == 0);
	}

	public static implicit operator PacketContainer(CommunityPermissionUpdatePacket packet)
	{
		return new PacketContainer
		{
			CommunityPermissionUpdate = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityPermissionUpdatePacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityPermissionUpdatePacket(CommunityPermissionUpdatePacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		channelGroupsCreated_ = other.channelGroupsCreated_.Clone();
		channelGroupsEdited_ = other.channelGroupsEdited_.Clone();
		channelGroupsMoved_ = other.channelGroupsMoved_.Clone();
		channelGroupsDeleted_ = other.channelGroupsDeleted_.Clone();
		channelsCreated_ = other.channelsCreated_.Clone();
		channelsEdited_ = other.channelsEdited_.Clone();
		channelsMoved_ = other.channelsMoved_.Clone();
		channelsDeleted_ = other.channelsDeleted_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityPermissionUpdatePacket Clone()
	{
		return new CommunityPermissionUpdatePacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityPermissionUpdatePacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityPermissionUpdatePacket other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PacketType != other.PacketType)
		{
			return false;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(CommunityPermission, other.CommunityPermission))
		{
			return false;
		}
		if (!channelGroupsCreated_.Equals(other.channelGroupsCreated_))
		{
			return false;
		}
		if (!channelGroupsEdited_.Equals(other.channelGroupsEdited_))
		{
			return false;
		}
		if (!channelGroupsMoved_.Equals(other.channelGroupsMoved_))
		{
			return false;
		}
		if (!channelGroupsDeleted_.Equals(other.channelGroupsDeleted_))
		{
			return false;
		}
		if (!channelsCreated_.Equals(other.channelsCreated_))
		{
			return false;
		}
		if (!channelsEdited_.Equals(other.channelsEdited_))
		{
			return false;
		}
		if (!channelsMoved_.Equals(other.channelsMoved_))
		{
			return false;
		}
		if (!channelsDeleted_.Equals(other.channelsDeleted_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (PacketType != PacketType.Unspecified)
		{
			num ^= PacketType.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (communityPermission_ != null)
		{
			num ^= CommunityPermission.GetHashCode();
		}
		num ^= channelGroupsCreated_.GetHashCode();
		num ^= channelGroupsEdited_.GetHashCode();
		num ^= channelGroupsMoved_.GetHashCode();
		num ^= channelGroupsDeleted_.GetHashCode();
		num ^= channelsCreated_.GetHashCode();
		num ^= channelsEdited_.GetHashCode();
		num ^= channelsMoved_.GetHashCode();
		num ^= channelsDeleted_.GetHashCode();
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
		if (PacketType != PacketType.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)PacketType);
		}
		if (communityId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(CommunityId);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityPermission);
		}
		channelGroupsCreated_.WriteTo(ref P_0, _repeated_channelGroupsCreated_codec);
		channelGroupsEdited_.WriteTo(ref P_0, _repeated_channelGroupsEdited_codec);
		channelGroupsMoved_.WriteTo(ref P_0, _repeated_channelGroupsMoved_codec);
		channelGroupsDeleted_.WriteTo(ref P_0, _repeated_channelGroupsDeleted_codec);
		channelsCreated_.WriteTo(ref P_0, _repeated_channelsCreated_codec);
		channelsEdited_.WriteTo(ref P_0, _repeated_channelsEdited_codec);
		channelsMoved_.WriteTo(ref P_0, _repeated_channelsMoved_codec);
		channelsDeleted_.WriteTo(ref P_0, _repeated_channelsDeleted_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (PacketType != PacketType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PacketType);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (communityPermission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityPermission);
		}
		num += channelGroupsCreated_.CalculateSize(_repeated_channelGroupsCreated_codec);
		num += channelGroupsEdited_.CalculateSize(_repeated_channelGroupsEdited_codec);
		num += channelGroupsMoved_.CalculateSize(_repeated_channelGroupsMoved_codec);
		num += channelGroupsDeleted_.CalculateSize(_repeated_channelGroupsDeleted_codec);
		num += channelsCreated_.CalculateSize(_repeated_channelsCreated_codec);
		num += channelsEdited_.CalculateSize(_repeated_channelsEdited_codec);
		num += channelsMoved_.CalculateSize(_repeated_channelsMoved_codec);
		num += channelsDeleted_.CalculateSize(_repeated_channelsDeleted_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityPermissionUpdatePacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.communityPermission_ != null)
		{
			if (communityPermission_ == null)
			{
				CommunityPermission = new CommunityPermission();
			}
			CommunityPermission.MergeFrom(other.CommunityPermission);
		}
		channelGroupsCreated_.Add(other.channelGroupsCreated_);
		channelGroupsEdited_.Add(other.channelGroupsEdited_);
		channelGroupsMoved_.Add(other.channelGroupsMoved_);
		channelGroupsDeleted_.Add(other.channelGroupsDeleted_);
		channelsCreated_.Add(other.channelsCreated_);
		channelsEdited_.Add(other.channelsEdited_);
		channelsMoved_.Add(other.channelsMoved_);
		channelsDeleted_.Add(other.channelsDeleted_);
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			case 8u:
				PacketType = (PacketType)P_0.ReadEnum();
				break;
			case 26u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 34u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			case 42u:
				channelGroupsCreated_.AddEntriesFrom(ref P_0, _repeated_channelGroupsCreated_codec);
				break;
			case 50u:
				channelGroupsEdited_.AddEntriesFrom(ref P_0, _repeated_channelGroupsEdited_codec);
				break;
			case 58u:
				channelGroupsMoved_.AddEntriesFrom(ref P_0, _repeated_channelGroupsMoved_codec);
				break;
			case 66u:
				channelGroupsDeleted_.AddEntriesFrom(ref P_0, _repeated_channelGroupsDeleted_codec);
				break;
			case 74u:
				channelsCreated_.AddEntriesFrom(ref P_0, _repeated_channelsCreated_codec);
				break;
			case 82u:
				channelsEdited_.AddEntriesFrom(ref P_0, _repeated_channelsEdited_codec);
				break;
			case 90u:
				channelsMoved_.AddEntriesFrom(ref P_0, _repeated_channelsMoved_codec);
				break;
			case 98u:
				channelsDeleted_.AddEntriesFrom(ref P_0, _repeated_channelsDeleted_codec);
				break;
			}
		}
	}
}
