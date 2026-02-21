using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Exceptions.Payloads;

public sealed class AccessTokenExceptionPayload : IMessage<AccessTokenExceptionPayload>, IMessage, IEquatable<AccessTokenExceptionPayload>, IDeepCloneable<AccessTokenExceptionPayload>, IBufferMessage
{
	private static readonly MessageParser<AccessTokenExceptionPayload> _parser = new MessageParser<AccessTokenExceptionPayload>(() => new AccessTokenExceptionPayload());

	private UnknownFieldSet _unknownFields;

	private string accessToken_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<AccessTokenExceptionPayload> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RootExceptionPayloadReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string AccessToken
	{
		get
		{
			return accessToken_;
		}
		set
		{
			accessToken_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	public static implicit operator RootGrpcExceptionPayload(AccessTokenExceptionPayload payload)
	{
		return new RootGrpcExceptionPayload
		{
			AccessToken = payload
		};
	}

	[GeneratedCode("protoc", null)]
	public AccessTokenExceptionPayload()
	{
	}

	[GeneratedCode("protoc", null)]
	public AccessTokenExceptionPayload(AccessTokenExceptionPayload other)
		: this()
	{
		accessToken_ = other.accessToken_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AccessTokenExceptionPayload Clone()
	{
		return new AccessTokenExceptionPayload(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AccessTokenExceptionPayload);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AccessTokenExceptionPayload other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (AccessToken != other.AccessToken)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (AccessToken.Length != 0)
		{
			num ^= AccessToken.GetHashCode();
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
		if (AccessToken.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(AccessToken);
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
		if (AccessToken.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(AccessToken);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AccessTokenExceptionPayload other)
	{
		if (other != null)
		{
			if (other.AccessToken.Length != 0)
			{
				AccessToken = other.AccessToken;
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
				AccessToken = P_0.ReadString();
			}
		}
	}
}
