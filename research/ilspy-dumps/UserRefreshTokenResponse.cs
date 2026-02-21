using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class UserRefreshTokenResponse : IMessage<UserRefreshTokenResponse>, IMessage, IEquatable<UserRefreshTokenResponse>, IDeepCloneable<UserRefreshTokenResponse>, IBufferMessage
{
	private static readonly MessageParser<UserRefreshTokenResponse> _parser = new MessageParser<UserRefreshTokenResponse>(() => new UserRefreshTokenResponse());

	private UnknownFieldSet _unknownFields;

	private string clientToken_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserRefreshTokenResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[28];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string ClientToken
	{
		get
		{
			return clientToken_;
		}
		set
		{
			clientToken_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public UserRefreshTokenResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserRefreshTokenResponse(UserRefreshTokenResponse other)
		: this()
	{
		clientToken_ = other.clientToken_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserRefreshTokenResponse Clone()
	{
		return new UserRefreshTokenResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserRefreshTokenResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserRefreshTokenResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ClientToken != other.ClientToken)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (ClientToken.Length != 0)
		{
			num ^= ClientToken.GetHashCode();
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
		if (ClientToken.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(ClientToken);
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
		if (ClientToken.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ClientToken);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserRefreshTokenResponse other)
	{
		if (other != null)
		{
			if (other.ClientToken.Length != 0)
			{
				ClientToken = other.ClientToken;
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 82)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				ClientToken = P_0.ReadString();
			}
		}
	}
}
