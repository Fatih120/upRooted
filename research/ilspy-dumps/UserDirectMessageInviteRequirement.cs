using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class UserDirectMessageInviteRequirement : IMessage<UserDirectMessageInviteRequirement>, IMessage, IEquatable<UserDirectMessageInviteRequirement>, IDeepCloneable<UserDirectMessageInviteRequirement>, IBufferMessage
{
	private static readonly MessageParser<UserDirectMessageInviteRequirement> _parser = new MessageParser<UserDirectMessageInviteRequirement>(() => new UserDirectMessageInviteRequirement());

	private UnknownFieldSet _unknownFields;

	private UserDirectMessageInviteConnection connection_ = UserDirectMessageInviteConnection.Unspecified;

	private bool isEmailVerified_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserDirectMessageInviteRequirement> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public UserDirectMessageInviteConnection Connection
	{
		get
		{
			return connection_;
		}
		set
		{
			connection_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsEmailVerified
	{
		get
		{
			return isEmailVerified_;
		}
		set
		{
			isEmailVerified_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserDirectMessageInviteRequirement()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserDirectMessageInviteRequirement(UserDirectMessageInviteRequirement other)
		: this()
	{
		connection_ = other.connection_;
		isEmailVerified_ = other.isEmailVerified_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserDirectMessageInviteRequirement Clone()
	{
		return new UserDirectMessageInviteRequirement(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserDirectMessageInviteRequirement);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserDirectMessageInviteRequirement other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Connection != other.Connection)
		{
			return false;
		}
		if (IsEmailVerified != other.IsEmailVerified)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Connection != UserDirectMessageInviteConnection.Unspecified)
		{
			num ^= Connection.GetHashCode();
		}
		if (IsEmailVerified)
		{
			num ^= IsEmailVerified.GetHashCode();
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
		if (Connection != UserDirectMessageInviteConnection.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)Connection);
		}
		if (IsEmailVerified)
		{
			P_0.WriteRawTag(16);
			P_0.WriteBool(IsEmailVerified);
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
		if (Connection != UserDirectMessageInviteConnection.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Connection);
		}
		if (IsEmailVerified)
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
	public void MergeFrom(UserDirectMessageInviteRequirement other)
	{
		if (other != null)
		{
			if (other.Connection != UserDirectMessageInviteConnection.Unspecified)
			{
				Connection = other.Connection;
			}
			if (other.IsEmailVerified)
			{
				IsEmailVerified = other.IsEmailVerified;
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
				Connection = (UserDirectMessageInviteConnection)P_0.ReadEnum();
				break;
			case 16u:
				IsEmailVerified = P_0.ReadBool();
				break;
			}
		}
	}
}
