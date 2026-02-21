using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Packets;

public sealed class UserBadge : IMessage<UserBadge>, IMessage, IEquatable<UserBadge>, IDeepCloneable<UserBadge>, IBufferMessage
{
	private static readonly MessageParser<UserBadge> _parser = new MessageParser<UserBadge>(() => new UserBadge());

	private UnknownFieldSet _unknownFields;

	private BadgeUuid id_;

	private Timestamp createdAt_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserBadge> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public BadgeUuid Id
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
	public Timestamp CreatedAt
	{
		get
		{
			return createdAt_;
		}
		set
		{
			createdAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserBadge()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserBadge(UserBadge other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		createdAt_ = ((other.createdAt_ != null) ? other.createdAt_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserBadge Clone()
	{
		return new UserBadge(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserBadge);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserBadge other)
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
		if (!object.Equals(CreatedAt, other.CreatedAt))
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
		if (createdAt_ != null)
		{
			num ^= CreatedAt.GetHashCode();
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
			P_0.WriteRawTag(26);
			P_0.WriteMessage(Id);
		}
		if (createdAt_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CreatedAt);
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
		if (createdAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CreatedAt);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserBadge other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new BadgeUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.createdAt_ != null)
		{
			if (createdAt_ == null)
			{
				CreatedAt = new Timestamp();
			}
			CreatedAt.MergeFrom(other.CreatedAt);
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
			case 26u:
				if (id_ == null)
				{
					Id = new BadgeUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 34u:
				if (createdAt_ == null)
				{
					CreatedAt = new Timestamp();
				}
				P_0.ReadMessage(CreatedAt);
				break;
			}
		}
	}
}
