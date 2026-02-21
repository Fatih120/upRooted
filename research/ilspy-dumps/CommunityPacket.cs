using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class CommunityPacket : IPacketCommunity, IPacket, IMessage<CommunityPacket>, IMessage, IEquatable<CommunityPacket>, IDeepCloneable<CommunityPacket>, IBufferMessage
{
	private static readonly MessageParser<CommunityPacket> _parser = new MessageParser<CommunityPacket>(() => new CommunityPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private UserUuid ownerUserId_;

	private ChannelUuid defaultChannelId_;

	private string name_ = "";

	private string pictureHex_ = "";

	private static readonly FieldCodec<string> _single_pictureAssetUri_codec = FieldCodec.ForClassWrapper<string>(66u);

	private string pictureAssetUri_;

	private bool rejectUnverifiedEmail_;

	private CommunityJoinThrottle joinThrottle_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[0];

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
	public UserUuid OwnerUserId
	{
		get
		{
			return ownerUserId_;
		}
		set
		{
			ownerUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelUuid DefaultChannelId
	{
		get
		{
			return defaultChannelId_;
		}
		set
		{
			defaultChannelId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string PictureHex
	{
		get
		{
			return pictureHex_;
		}
		set
		{
			pictureHex_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string PictureAssetUri
	{
		get
		{
			return pictureAssetUri_;
		}
		set
		{
			pictureAssetUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool RejectUnverifiedEmail
	{
		get
		{
			return rejectUnverifiedEmail_;
		}
		set
		{
			rejectUnverifiedEmail_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityJoinThrottle JoinThrottle
	{
		get
		{
			return joinThrottle_;
		}
		set
		{
			joinThrottle_ = value;
		}
	}

	public static implicit operator PacketContainer(CommunityPacket packet)
	{
		return new PacketContainer
		{
			Community = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityPacket(CommunityPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		ownerUserId_ = ((other.ownerUserId_ != null) ? other.ownerUserId_.Clone() : null);
		defaultChannelId_ = ((other.defaultChannelId_ != null) ? other.defaultChannelId_.Clone() : null);
		name_ = other.name_;
		pictureHex_ = other.pictureHex_;
		PictureAssetUri = other.PictureAssetUri;
		rejectUnverifiedEmail_ = other.rejectUnverifiedEmail_;
		joinThrottle_ = ((other.joinThrottle_ != null) ? other.joinThrottle_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityPacket Clone()
	{
		return new CommunityPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityPacket other)
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
		if (!object.Equals(OwnerUserId, other.OwnerUserId))
		{
			return false;
		}
		if (!object.Equals(DefaultChannelId, other.DefaultChannelId))
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (PictureHex != other.PictureHex)
		{
			return false;
		}
		if (PictureAssetUri != other.PictureAssetUri)
		{
			return false;
		}
		if (RejectUnverifiedEmail != other.RejectUnverifiedEmail)
		{
			return false;
		}
		if (!object.Equals(JoinThrottle, other.JoinThrottle))
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
		if (ownerUserId_ != null)
		{
			num ^= OwnerUserId.GetHashCode();
		}
		if (defaultChannelId_ != null)
		{
			num ^= DefaultChannelId.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (PictureHex.Length != 0)
		{
			num ^= PictureHex.GetHashCode();
		}
		if (pictureAssetUri_ != null)
		{
			num ^= PictureAssetUri.GetHashCode();
		}
		if (RejectUnverifiedEmail)
		{
			num ^= RejectUnverifiedEmail.GetHashCode();
		}
		if (joinThrottle_ != null)
		{
			num ^= JoinThrottle.GetHashCode();
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
		if (ownerUserId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(OwnerUserId);
		}
		if (defaultChannelId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(DefaultChannelId);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Name);
		}
		if (PictureHex.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(PictureHex);
		}
		if (pictureAssetUri_ != null)
		{
			_single_pictureAssetUri_codec.WriteTagAndValue(ref P_0, PictureAssetUri);
		}
		if (RejectUnverifiedEmail)
		{
			P_0.WriteRawTag(72);
			P_0.WriteBool(RejectUnverifiedEmail);
		}
		if (joinThrottle_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(JoinThrottle);
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
		if (ownerUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OwnerUserId);
		}
		if (defaultChannelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DefaultChannelId);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (PictureHex.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PictureHex);
		}
		if (pictureAssetUri_ != null)
		{
			num += _single_pictureAssetUri_codec.CalculateSizeWithTag(PictureAssetUri);
		}
		if (RejectUnverifiedEmail)
		{
			num += 2;
		}
		if (joinThrottle_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(JoinThrottle);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityPacket other)
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
		if (other.ownerUserId_ != null)
		{
			if (ownerUserId_ == null)
			{
				OwnerUserId = new UserUuid();
			}
			OwnerUserId.MergeFrom(other.OwnerUserId);
		}
		if (other.defaultChannelId_ != null)
		{
			if (defaultChannelId_ == null)
			{
				DefaultChannelId = new ChannelUuid();
			}
			DefaultChannelId.MergeFrom(other.DefaultChannelId);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.PictureHex.Length != 0)
		{
			PictureHex = other.PictureHex;
		}
		if (other.pictureAssetUri_ != null && (pictureAssetUri_ == null || other.PictureAssetUri != ""))
		{
			PictureAssetUri = other.PictureAssetUri;
		}
		if (other.RejectUnverifiedEmail)
		{
			RejectUnverifiedEmail = other.RejectUnverifiedEmail;
		}
		if (other.joinThrottle_ != null)
		{
			if (joinThrottle_ == null)
			{
				JoinThrottle = new CommunityJoinThrottle();
			}
			JoinThrottle.MergeFrom(other.JoinThrottle);
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
				if (ownerUserId_ == null)
				{
					OwnerUserId = new UserUuid();
				}
				P_0.ReadMessage(OwnerUserId);
				break;
			case 42u:
				if (defaultChannelId_ == null)
				{
					DefaultChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(DefaultChannelId);
				break;
			case 50u:
				Name = P_0.ReadString();
				break;
			case 58u:
				PictureHex = P_0.ReadString();
				break;
			case 66u:
			{
				string text = _single_pictureAssetUri_codec.Read(ref P_0);
				if (pictureAssetUri_ == null || text != "")
				{
					PictureAssetUri = text;
				}
				break;
			}
			case 72u:
				RejectUnverifiedEmail = P_0.ReadBool();
				break;
			case 82u:
				if (joinThrottle_ == null)
				{
					JoinThrottle = new CommunityJoinThrottle();
				}
				P_0.ReadMessage(JoinThrottle);
				break;
			}
		}
	}
}
