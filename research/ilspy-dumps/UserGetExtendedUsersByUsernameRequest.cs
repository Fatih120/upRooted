using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class UserGetExtendedUsersByUsernameRequest : IMessage<UserGetExtendedUsersByUsernameRequest>, IMessage, IEquatable<UserGetExtendedUsersByUsernameRequest>, IDeepCloneable<UserGetExtendedUsersByUsernameRequest>, IBufferMessage
{
	private static readonly MessageParser<UserGetExtendedUsersByUsernameRequest> _parser = new MessageParser<UserGetExtendedUsersByUsernameRequest>(() => new UserGetExtendedUsersByUsernameRequest());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<string> _repeated_usernames_codec = FieldCodec.ForString(82u);

	private readonly RepeatedField<string> usernames_ = new RepeatedField<string>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserGetExtendedUsersByUsernameRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[23];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<string> Usernames => usernames_;

	[GeneratedCode("protoc", null)]
	public UserGetExtendedUsersByUsernameRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserGetExtendedUsersByUsernameRequest(UserGetExtendedUsersByUsernameRequest other)
		: this()
	{
		usernames_ = other.usernames_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserGetExtendedUsersByUsernameRequest Clone()
	{
		return new UserGetExtendedUsersByUsernameRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserGetExtendedUsersByUsernameRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserGetExtendedUsersByUsernameRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!usernames_.Equals(other.usernames_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= usernames_.GetHashCode();
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
		usernames_.WriteTo(ref P_0, _repeated_usernames_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += usernames_.CalculateSize(_repeated_usernames_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserGetExtendedUsersByUsernameRequest other)
	{
		if (other != null)
		{
			usernames_.Add(other.usernames_);
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 82)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				usernames_.AddEntriesFrom(ref P_0, _repeated_usernames_codec);
			}
		}
	}
}
