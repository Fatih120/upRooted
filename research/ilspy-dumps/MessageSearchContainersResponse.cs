using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class MessageSearchContainersResponse : IMessage<MessageSearchContainersResponse>, IMessage, IEquatable<MessageSearchContainersResponse>, IDeepCloneable<MessageSearchContainersResponse>, IBufferMessage
{
	private static readonly MessageParser<MessageSearchContainersResponse> _parser = new MessageParser<MessageSearchContainersResponse>(() => new MessageSearchContainersResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<MessageContainerResponse> _repeated_containers_codec = FieldCodec.ForMessage(82u, MessageContainerResponse.Parser);

	private readonly RepeatedField<MessageContainerResponse> containers_ = new RepeatedField<MessageContainerResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageSearchContainersResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[11];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<MessageContainerResponse> Containers => containers_;

	[GeneratedCode("protoc", null)]
	public MessageSearchContainersResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageSearchContainersResponse(MessageSearchContainersResponse other)
		: this()
	{
		containers_ = other.containers_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageSearchContainersResponse Clone()
	{
		return new MessageSearchContainersResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageSearchContainersResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageSearchContainersResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!containers_.Equals(other.containers_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= containers_.GetHashCode();
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
		containers_.WriteTo(ref P_0, _repeated_containers_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += containers_.CalculateSize(_repeated_containers_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageSearchContainersResponse other)
	{
		if (other != null)
		{
			containers_.Add(other.containers_);
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
				containers_.AddEntriesFrom(ref P_0, _repeated_containers_codec);
			}
		}
	}
}
