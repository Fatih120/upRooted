using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core.Enums;

namespace RootApp.WebApi.Shared.Grpc;

public sealed class UserCommandTarget : IMessage<UserCommandTarget>, IMessage, IEquatable<UserCommandTarget>, IDeepCloneable<UserCommandTarget>, IBufferMessage
{
	private static readonly MessageParser<UserCommandTarget> _parser = new MessageParser<UserCommandTarget>(() => new UserCommandTarget());

	private UnknownFieldSet _unknownFields;

	private UserCommandVerb verb_ = UserCommandVerb.Unspecified;

	private RootGuidType type_ = RootGuidType.Unknown;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserCommandTarget> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ContextReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public UserCommandVerb Verb
	{
		get
		{
			return verb_;
		}
		set
		{
			verb_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootGuidType Type
	{
		get
		{
			return type_;
		}
		set
		{
			type_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserCommandTarget()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserCommandTarget(UserCommandTarget other)
		: this()
	{
		verb_ = other.verb_;
		type_ = other.type_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserCommandTarget Clone()
	{
		return new UserCommandTarget(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserCommandTarget);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserCommandTarget other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Verb != other.Verb)
		{
			return false;
		}
		if (Type != other.Type)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Verb != UserCommandVerb.Unspecified)
		{
			num ^= Verb.GetHashCode();
		}
		if (Type != RootGuidType.Unknown)
		{
			num ^= Type.GetHashCode();
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
		if (Verb != UserCommandVerb.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)Verb);
		}
		if (Type != RootGuidType.Unknown)
		{
			P_0.WriteRawTag(16);
			P_0.WriteEnum((int)Type);
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
		if (Verb != UserCommandVerb.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Verb);
		}
		if (Type != RootGuidType.Unknown)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Type);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserCommandTarget other)
	{
		if (other != null)
		{
			if (other.Verb != UserCommandVerb.Unspecified)
			{
				Verb = other.Verb;
			}
			if (other.Type != RootGuidType.Unknown)
			{
				Type = other.Type;
			}
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
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
				Verb = (UserCommandVerb)P_0.ReadEnum();
				break;
			case 16u:
				Type = (RootGuidType)P_0.ReadEnum();
				break;
			}
		}
	}
}
