using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class CommunityMemberRoleDeletedPacket : IPacketCommunity, IPacket, IMessage<CommunityMemberRoleDeletedPacket>, IMessage, IEquatable<CommunityMemberRoleDeletedPacket>, IDeepCloneable<CommunityMemberRoleDeletedPacket>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberRoleDeletedPacket> _parser = new MessageParser<CommunityMemberRoleDeletedPacket>(() => new CommunityMemberRoleDeletedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private static readonly FieldCodec<UserUuid> _repeated_userIds_codec = FieldCodec.ForMessage(34u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> userIds_ = new RepeatedField<UserUuid>();

	private CommunityRoleUuid communityRoleId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberRoleDeletedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[13];

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
	public RepeatedField<UserUuid> UserIds => userIds_;

	[GeneratedCode("protoc", null)]
	public CommunityRoleUuid CommunityRoleId
	{
		get
		{
			return communityRoleId_;
		}
		set
		{
			communityRoleId_ = value;
		}
	}

	public static implicit operator PacketContainer(CommunityMemberRoleDeletedPacket packet)
	{
		return new PacketContainer
		{
			CommunityMemberRoleDeleted = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberRoleDeletedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberRoleDeletedPacket(CommunityMemberRoleDeletedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		userIds_ = other.userIds_.Clone();
		communityRoleId_ = ((other.communityRoleId_ != null) ? other.communityRoleId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberRoleDeletedPacket Clone()
	{
		return new CommunityMemberRoleDeletedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberRoleDeletedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberRoleDeletedPacket other)
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
		if (!userIds_.Equals(other.userIds_))
		{
			return false;
		}
		if (!object.Equals(CommunityRoleId, other.CommunityRoleId))
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
		num ^= userIds_.GetHashCode();
		if (communityRoleId_ != null)
		{
			num ^= CommunityRoleId.GetHashCode();
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
		userIds_.WriteTo(ref P_0, _repeated_userIds_codec);
		if (communityRoleId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(CommunityRoleId);
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
		if (PacketType != PacketType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PacketType);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		num += userIds_.CalculateSize(_repeated_userIds_codec);
		if (communityRoleId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityRoleId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberRoleDeletedPacket other)
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
		userIds_.Add(other.userIds_);
		if (other.communityRoleId_ != null)
		{
			if (communityRoleId_ == null)
			{
				CommunityRoleId = new CommunityRoleUuid();
			}
			CommunityRoleId.MergeFrom(other.CommunityRoleId);
		}
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
				userIds_.AddEntriesFrom(ref P_0, _repeated_userIds_codec);
				break;
			case 42u:
				if (communityRoleId_ == null)
				{
					CommunityRoleId = new CommunityRoleUuid();
				}
				P_0.ReadMessage(CommunityRoleId);
				break;
			}
		}
	}
}
