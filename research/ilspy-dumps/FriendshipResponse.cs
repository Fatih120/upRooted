using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FriendshipResponse : IMessage<FriendshipResponse>, IMessage, IEquatable<FriendshipResponse>, IDeepCloneable<FriendshipResponse>, IBufferMessage
{
	private static readonly MessageParser<FriendshipResponse> _parser = new MessageParser<FriendshipResponse>(() => new FriendshipResponse());

	private UnknownFieldSet _unknownFields;

	private FriendshipUuid id_;

	private UserUuid friendUserId_;

	private float position_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public FriendshipUuid Id
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
	public UserUuid FriendUserId
	{
		get
		{
			return friendUserId_;
		}
		set
		{
			friendUserId_ = value;
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
	public FriendshipResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipResponse(FriendshipResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		friendUserId_ = ((other.friendUserId_ != null) ? other.friendUserId_.Clone() : null);
		position_ = other.position_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipResponse Clone()
	{
		return new FriendshipResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipResponse other)
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
		if (!object.Equals(FriendUserId, other.FriendUserId))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Position, other.Position))
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
		if (friendUserId_ != null)
		{
			num ^= FriendUserId.GetHashCode();
		}
		if (Position != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Position);
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
		if (friendUserId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(FriendUserId);
		}
		if (Position != 0f)
		{
			P_0.WriteRawTag(101);
			P_0.WriteFloat(Position);
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
		if (friendUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendUserId);
		}
		if (Position != 0f)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new FriendshipUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.friendUserId_ != null)
		{
			if (friendUserId_ == null)
			{
				FriendUserId = new UserUuid();
			}
			FriendUserId.MergeFrom(other.FriendUserId);
		}
		if (other.Position != 0f)
		{
			Position = other.Position;
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
					Id = new FriendshipUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 90u:
				if (friendUserId_ == null)
				{
					FriendUserId = new UserUuid();
				}
				P_0.ReadMessage(FriendUserId);
				break;
			case 101u:
				Position = P_0.ReadFloat();
				break;
			}
		}
	}
}
