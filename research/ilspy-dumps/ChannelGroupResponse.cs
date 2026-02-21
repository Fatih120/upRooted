using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class ChannelGroupResponse : IMessage<ChannelGroupResponse>, IMessage, IEquatable<ChannelGroupResponse>, IDeepCloneable<ChannelGroupResponse>, IBufferMessage
{
	private static readonly MessageParser<ChannelGroupResponse> _parser = new MessageParser<ChannelGroupResponse>(() => new ChannelGroupResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private ChannelGroupUuid id_;

	private string name_ = "";

	private float position_;

	private ChannelPermission channelGroupPermission_;

	private static readonly FieldCodec<ChannelResponse> _repeated_channels_codec = FieldCodec.ForMessage(122u, ChannelResponse.Parser);

	private readonly RepeatedField<ChannelResponse> channels_ = new RepeatedField<ChannelResponse>();

	private static readonly FieldCodec<RoleOrMemberUuid> _repeated_roleOrMemberIds_codec = FieldCodec.ForMessage(130u, RoleOrMemberUuid.Parser);

	private readonly RepeatedField<RoleOrMemberUuid> roleOrMemberIds_ = new RepeatedField<RoleOrMemberUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelGroupResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelGroupReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public ChannelGroupUuid Id
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
	public ChannelPermission ChannelGroupPermission
	{
		get
		{
			return channelGroupPermission_;
		}
		set
		{
			channelGroupPermission_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelResponse> Channels => channels_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<RoleOrMemberUuid> RoleOrMemberIds => roleOrMemberIds_;

	[GeneratedCode("protoc", null)]
	public ChannelGroupResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupResponse(ChannelGroupResponse other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		name_ = other.name_;
		position_ = other.position_;
		channelGroupPermission_ = ((other.channelGroupPermission_ != null) ? other.channelGroupPermission_.Clone() : null);
		channels_ = other.channels_.Clone();
		roleOrMemberIds_ = other.roleOrMemberIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupResponse Clone()
	{
		return new ChannelGroupResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelGroupResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelGroupResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Position, other.Position))
		{
			return false;
		}
		if (!object.Equals(ChannelGroupPermission, other.ChannelGroupPermission))
		{
			return false;
		}
		if (!channels_.Equals(other.channels_))
		{
			return false;
		}
		if (!roleOrMemberIds_.Equals(other.roleOrMemberIds_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (Position != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Position);
		}
		if (channelGroupPermission_ != null)
		{
			num ^= ChannelGroupPermission.GetHashCode();
		}
		num ^= channels_.GetHashCode();
		num ^= roleOrMemberIds_.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(Id);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Name);
		}
		if (Position != 0f)
		{
			P_0.WriteRawTag(109);
			P_0.WriteFloat(Position);
		}
		if (channelGroupPermission_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(ChannelGroupPermission);
		}
		channels_.WriteTo(ref P_0, _repeated_channels_codec);
		roleOrMemberIds_.WriteTo(ref P_0, _repeated_roleOrMemberIds_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (Position != 0f)
		{
			num += 5;
		}
		if (channelGroupPermission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelGroupPermission);
		}
		num += channels_.CalculateSize(_repeated_channels_codec);
		num += roleOrMemberIds_.CalculateSize(_repeated_roleOrMemberIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelGroupResponse other)
	{
		if (other == null)
		{
			return;
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
				Id = new ChannelGroupUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.Position != 0f)
		{
			Position = other.Position;
		}
		if (other.channelGroupPermission_ != null)
		{
			if (channelGroupPermission_ == null)
			{
				ChannelGroupPermission = new ChannelPermission();
			}
			ChannelGroupPermission.MergeFrom(other.ChannelGroupPermission);
		}
		channels_.Add(other.channels_);
		roleOrMemberIds_.Add(other.roleOrMemberIds_);
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
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
				if (id_ == null)
				{
					Id = new ChannelGroupUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 98u:
				Name = P_0.ReadString();
				break;
			case 109u:
				Position = P_0.ReadFloat();
				break;
			case 114u:
				if (channelGroupPermission_ == null)
				{
					ChannelGroupPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelGroupPermission);
				break;
			case 122u:
				channels_.AddEntriesFrom(ref P_0, _repeated_channels_codec);
				break;
			case 130u:
				roleOrMemberIds_.AddEntriesFrom(ref P_0, _repeated_roleOrMemberIds_codec);
				break;
			}
		}
	}
}
