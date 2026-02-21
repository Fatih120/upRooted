using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FriendshipGroupResponse : IMessage<FriendshipGroupResponse>, IMessage, IEquatable<FriendshipGroupResponse>, IDeepCloneable<FriendshipGroupResponse>, IBufferMessage
{
	private static readonly MessageParser<FriendshipGroupResponse> _parser = new MessageParser<FriendshipGroupResponse>(() => new FriendshipGroupResponse());

	private UnknownFieldSet _unknownFields;

	private FriendshipGroupUuid id_;

	private string name_ = "";

	private float position_;

	private bool isDefault_;

	private static readonly FieldCodec<FriendshipResponse> _repeated_friendships_codec = FieldCodec.ForMessage(114u, FriendshipResponse.Parser);

	private readonly RepeatedField<FriendshipResponse> friendships_ = new RepeatedField<FriendshipResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipGroupResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public FriendshipGroupUuid Id
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
	public bool IsDefault
	{
		get
		{
			return isDefault_;
		}
		set
		{
			isDefault_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<FriendshipResponse> Friendships => friendships_;

	[GeneratedCode("protoc", null)]
	public FriendshipGroupResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupResponse(FriendshipGroupResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		name_ = other.name_;
		position_ = other.position_;
		isDefault_ = other.isDefault_;
		friendships_ = other.friendships_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupResponse Clone()
	{
		return new FriendshipGroupResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipGroupResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipGroupResponse other)
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
		if (Name != other.Name)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Position, other.Position))
		{
			return false;
		}
		if (IsDefault != other.IsDefault)
		{
			return false;
		}
		if (!friendships_.Equals(other.friendships_))
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
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (Position != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Position);
		}
		if (IsDefault)
		{
			num ^= IsDefault.GetHashCode();
		}
		num ^= friendships_.GetHashCode();
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
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(Name);
		}
		if (Position != 0f)
		{
			P_0.WriteRawTag(101);
			P_0.WriteFloat(Position);
		}
		if (IsDefault)
		{
			P_0.WriteRawTag(104);
			P_0.WriteBool(IsDefault);
		}
		friendships_.WriteTo(ref P_0, _repeated_friendships_codec);
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
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (Position != 0f)
		{
			num += 5;
		}
		if (IsDefault)
		{
			num += 2;
		}
		num += friendships_.CalculateSize(_repeated_friendships_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipGroupResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new FriendshipGroupUuid();
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
		if (other.IsDefault)
		{
			IsDefault = other.IsDefault;
		}
		friendships_.Add(other.friendships_);
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
					Id = new FriendshipGroupUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 90u:
				Name = P_0.ReadString();
				break;
			case 101u:
				Position = P_0.ReadFloat();
				break;
			case 104u:
				IsDefault = P_0.ReadBool();
				break;
			case 114u:
				friendships_.AddEntriesFrom(ref P_0, _repeated_friendships_codec);
				break;
			}
		}
	}
}
