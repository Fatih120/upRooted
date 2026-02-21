using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class UserSetDirectMessageInviteRequirementRequest : IMessage<UserSetDirectMessageInviteRequirementRequest>, IMessage, IEquatable<UserSetDirectMessageInviteRequirementRequest>, IDeepCloneable<UserSetDirectMessageInviteRequirementRequest>, IBufferMessage
{
	private static readonly MessageParser<UserSetDirectMessageInviteRequirementRequest> _parser = new MessageParser<UserSetDirectMessageInviteRequirementRequest>(() => new UserSetDirectMessageInviteRequirementRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private UserDirectMessageInviteConnection connection_ = UserDirectMessageInviteConnection.Unspecified;

	private bool isEmailVerified_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserSetDirectMessageInviteRequirementRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[35];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootContext Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = value;
		}
	}

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
	public UserSetDirectMessageInviteRequirementRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserSetDirectMessageInviteRequirementRequest(UserSetDirectMessageInviteRequirementRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		connection_ = other.connection_;
		isEmailVerified_ = other.isEmailVerified_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserSetDirectMessageInviteRequirementRequest Clone()
	{
		return new UserSetDirectMessageInviteRequirementRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserSetDirectMessageInviteRequirementRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserSetDirectMessageInviteRequirementRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Context, other.Context))
		{
			return false;
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
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
		if (Connection != UserDirectMessageInviteConnection.Unspecified)
		{
			P_0.WriteRawTag(40);
			P_0.WriteEnum((int)Connection);
		}
		if (IsEmailVerified)
		{
			P_0.WriteRawTag(48);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
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
	public void MergeFrom(UserSetDirectMessageInviteRequirementRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.context_ != null)
		{
			if (context_ == null)
			{
				Context = new RootContext();
			}
			Context.MergeFrom(other.Context);
		}
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
			case 10u:
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
				break;
			case 40u:
				Connection = (UserDirectMessageInviteConnection)P_0.ReadEnum();
				break;
			case 48u:
				IsEmailVerified = P_0.ReadBool();
				break;
			}
		}
	}
}
