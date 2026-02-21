using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class DirectMessageListResponse : IMessage<DirectMessageListResponse>, IMessage, IEquatable<DirectMessageListResponse>, IDeepCloneable<DirectMessageListResponse>, IBufferMessage
{
	private static readonly MessageParser<DirectMessageListResponse> _parser = new MessageParser<DirectMessageListResponse>(() => new DirectMessageListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<DirectMessageResponse> _repeated_directMessages_codec = FieldCodec.ForMessage(82u, DirectMessageResponse.Parser);

	private readonly RepeatedField<DirectMessageResponse> directMessages_ = new RepeatedField<DirectMessageResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectMessageListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectMessageReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<DirectMessageResponse> DirectMessages => directMessages_;

	[GeneratedCode("protoc", null)]
	public DirectMessageListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageListResponse(DirectMessageListResponse other)
		: this()
	{
		directMessages_ = other.directMessages_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageListResponse Clone()
	{
		return new DirectMessageListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectMessageListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectMessageListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!directMessages_.Equals(other.directMessages_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= directMessages_.GetHashCode();
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
		directMessages_.WriteTo(ref P_0, _repeated_directMessages_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += directMessages_.CalculateSize(_repeated_directMessages_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectMessageListResponse other)
	{
		if (other != null)
		{
			directMessages_.Add(other.directMessages_);
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
				directMessages_.AddEntriesFrom(ref P_0, _repeated_directMessages_codec);
			}
		}
	}
}
