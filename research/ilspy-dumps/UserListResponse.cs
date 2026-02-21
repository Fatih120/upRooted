using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class UserListResponse : IMessage<UserListResponse>, IMessage, IEquatable<UserListResponse>, IDeepCloneable<UserListResponse>, IBufferMessage
{
	private static readonly MessageParser<UserListResponse> _parser = new MessageParser<UserListResponse>(() => new UserListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<UserResponse> _repeated_users_codec = FieldCodec.ForMessage(82u, UserResponse.Parser);

	private readonly RepeatedField<UserResponse> users_ = new RepeatedField<UserResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[16];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserResponse> Users => users_;

	[GeneratedCode("protoc", null)]
	public UserListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserListResponse(UserListResponse other)
		: this()
	{
		users_ = other.users_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserListResponse Clone()
	{
		return new UserListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!users_.Equals(other.users_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= users_.GetHashCode();
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
		users_.WriteTo(ref P_0, _repeated_users_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += users_.CalculateSize(_repeated_users_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserListResponse other)
	{
		if (other != null)
		{
			users_.Add(other.users_);
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
				users_.AddEntriesFrom(ref P_0, _repeated_users_codec);
			}
		}
	}
}
