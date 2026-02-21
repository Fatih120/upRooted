using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityRoleResponse : IMessage<CommunityRoleResponse>, IMessage, IEquatable<CommunityRoleResponse>, IDeepCloneable<CommunityRoleResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityRoleResponse> _parser = new MessageParser<CommunityRoleResponse>(() => new CommunityRoleResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityRoleUuid id_;

	private ChannelPermission channelPermission_;

	private CommunityPermission communityPermission_;

	private string name_ = "";

	private string colorHex_ = "";

	private float position_;

	private bool isMentionable_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityRoleResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityRoleReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public float Position
	{
		get
		{
			return position_;
		}
		set
		{
			position_ = value;
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

	[GeneratedCode("protoc", null)]
	public CommunityRoleResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleResponse(CommunityRoleResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		name_ = other.name_;
		colorHex_ = other.colorHex_;
		position_ = other.position_;
		isMentionable_ = other.isMentionable_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleResponse Clone()
	{
		return new CommunityRoleResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityRoleResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityRoleResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
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
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Position, other.Position))
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
		if (Position != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Position);
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
		if (id_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Id);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(ChannelPermission);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(CommunityPermission);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(106);
			P_0.WriteString(Name);
		}
		if (ColorHex.Length != 0)
		{
			P_0.WriteRawTag(114);
			P_0.WriteString(ColorHex);
		}
		if (Position != 0f)
		{
			P_0.WriteRawTag(125);
			P_0.WriteFloat(Position);
		}
		if (IsMentionable)
		{
			P_0.WriteRawTag(128, 1);
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
		if (Position != 0f)
		{
			num += 5;
		}
		if (IsMentionable)
		{
			num += 3;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityRoleResponse other)
	{
		if (other == null)
		{
			return;
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
		if (other.Position != 0f)
		{
			Position = other.Position;
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
			case 82u:
				if (id_ == null)
				{
					Id = new CommunityRoleUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 90u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			case 98u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			case 106u:
				Name = P_0.ReadString();
				break;
			case 114u:
				ColorHex = P_0.ReadString();
				break;
			case 125u:
				Position = P_0.ReadFloat();
				break;
			case 128u:
				IsMentionable = P_0.ReadBool();
				break;
			}
		}
	}
}
