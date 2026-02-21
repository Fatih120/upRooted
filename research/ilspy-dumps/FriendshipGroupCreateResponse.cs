using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FriendshipGroupCreateResponse : IMessage<FriendshipGroupCreateResponse>, IMessage, IEquatable<FriendshipGroupCreateResponse>, IDeepCloneable<FriendshipGroupCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<FriendshipGroupCreateResponse> _parser = new MessageParser<FriendshipGroupCreateResponse>(() => new FriendshipGroupCreateResponse());

	private UnknownFieldSet _unknownFields;

	private FriendshipGroupUuid id_;

	private bool isDefault_;

	private string name_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipGroupCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[5];

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
	public FriendshipGroupCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupCreateResponse(FriendshipGroupCreateResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		isDefault_ = other.isDefault_;
		name_ = other.name_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupCreateResponse Clone()
	{
		return new FriendshipGroupCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipGroupCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipGroupCreateResponse other)
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
		if (IsDefault != other.IsDefault)
		{
			return false;
		}
		if (Name != other.Name)
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
		if (IsDefault)
		{
			num ^= IsDefault.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
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
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (IsDefault)
		{
			P_0.WriteRawTag(40);
			P_0.WriteBool(IsDefault);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Name);
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
		if (IsDefault)
		{
			num += 2;
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipGroupCreateResponse other)
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
		if (other.IsDefault)
		{
			IsDefault = other.IsDefault;
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
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
			case 34u:
				if (id_ == null)
				{
					Id = new FriendshipGroupUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 40u:
				IsDefault = P_0.ReadBool();
				break;
			case 50u:
				Name = P_0.ReadString();
				break;
			}
		}
	}
}
