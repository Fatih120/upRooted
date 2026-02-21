using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc;

namespace RootApp.WebApi.Shared.Packets;

public sealed class CommunityRolePacket : IPacketCommunity, IPacket, IMessage<CommunityRolePacket>, IMessage, IEquatable<CommunityRolePacket>, IDeepCloneable<CommunityRolePacket>, IBufferMessage
{
	private static readonly MessageParser<CommunityRolePacket> _parser = new MessageParser<CommunityRolePacket>(() => new CommunityRolePacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private CommunityRoleUuid id_;

	private ChannelPermission channelPermission_;

	private CommunityPermission communityPermission_;

	private string name_ = "";

	private string colorHex_ = "";

	private CommunityRoleUuid beforeCommunityRoleId_;

	private bool isMentionable_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityRolePacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[14];

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
	public CommunityRoleUuid Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelPermission ChannelPermission
	{
		get
		{
			return channelPermission_;
		}
		set
		{
			channelPermission_ = value;
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
	public string ColorHex
	{
		get
		{
			return colorHex_;
		}
		set
		{
			colorHex_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleUuid BeforeCommunityRoleId
	{
		get
		{
			return beforeCommunityRoleId_;
		}
		set
		{
			beforeCommunityRoleId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsMentionable
	{
		get
		{
			return isMentionable_;
		}
		set
		{
			isMentionable_ = value;
		}
	}

	public static implicit operator PacketContainer(CommunityRolePacket packet)
	{
		return new PacketContainer
		{
			CommunityRole = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityRolePacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityRolePacket(CommunityRolePacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		name_ = other.name_;
		colorHex_ = other.colorHex_;
		beforeCommunityRoleId_ = ((other.beforeCommunityRoleId_ != null) ? other.beforeCommunityRoleId_.Clone() : null);
		isMentionable_ = other.isMentionable_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityRolePacket Clone()
	{
		return new CommunityRolePacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityRolePacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityRolePacket other)
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
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(ChannelPermission, other.ChannelPermission))
		{
			return false;
		}
		if (!object.Equals(CommunityPermission, other.CommunityPermission))
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (ColorHex != other.ColorHex)
		{
			return false;
		}
		if (!object.Equals(BeforeCommunityRoleId, other.BeforeCommunityRoleId))
		{
			return false;
		}
		if (IsMentionable != other.IsMentionable)
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
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (channelPermission_ != null)
		{
			num ^= ChannelPermission.GetHashCode();
		}
		if (communityPermission_ != null)
		{
			num ^= CommunityPermission.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (ColorHex.Length != 0)
		{
			num ^= ColorHex.GetHashCode();
		}
		if (beforeCommunityRoleId_ != null)
		{
			num ^= BeforeCommunityRoleId.GetHashCode();
		}
		if (IsMentionable)
		{
			num ^= IsMentionable.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(ChannelPermission);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(CommunityPermission);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(Name);
		}
		if (ColorHex.Length != 0)
		{
			P_0.WriteRawTag(66);
			P_0.WriteString(ColorHex);
		}
		if (beforeCommunityRoleId_ != null)
		{
			P_0.WriteRawTag(74);
			P_0.WriteMessage(BeforeCommunityRoleId);
		}
		if (IsMentionable)
		{
			P_0.WriteRawTag(80);
			P_0.WriteBool(IsMentionable);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (channelPermission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelPermission);
		}
		if (communityPermission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityPermission);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (ColorHex.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ColorHex);
		}
		if (beforeCommunityRoleId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeforeCommunityRoleId);
		}
		if (IsMentionable)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityRolePacket other)
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
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new CommunityRoleUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.channelPermission_ != null)
		{
			if (channelPermission_ == null)
			{
				ChannelPermission = new ChannelPermission();
			}
			ChannelPermission.MergeFrom(other.ChannelPermission);
		}
		if (other.communityPermission_ != null)
		{
			if (communityPermission_ == null)
			{
				CommunityPermission = new CommunityPermission();
			}
			CommunityPermission.MergeFrom(other.CommunityPermission);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.ColorHex.Length != 0)
		{
			ColorHex = other.ColorHex;
		}
		if (other.beforeCommunityRoleId_ != null)
		{
			if (beforeCommunityRoleId_ == null)
			{
				BeforeCommunityRoleId = new CommunityRoleUuid();
			}
			BeforeCommunityRoleId.MergeFrom(other.BeforeCommunityRoleId);
		}
		if (other.IsMentionable)
		{
			IsMentionable = other.IsMentionable;
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
				if (id_ == null)
				{
					Id = new CommunityRoleUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			case 50u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			case 58u:
				Name = P_0.ReadString();
				break;
			case 66u:
				ColorHex = P_0.ReadString();
				break;
			case 74u:
				if (beforeCommunityRoleId_ == null)
				{
					BeforeCommunityRoleId = new CommunityRoleUuid();
				}
				P_0.ReadMessage(BeforeCommunityRoleId);
				break;
			case 80u:
				IsMentionable = P_0.ReadBool();
				break;
			}
		}
	}
}
