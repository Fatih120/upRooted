using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class ChannelListResponse : IMessage<ChannelListResponse>, IMessage, IEquatable<ChannelListResponse>, IDeepCloneable<ChannelListResponse>, IBufferMessage
{
	private static readonly MessageParser<ChannelListResponse> _parser = new MessageParser<ChannelListResponse>(() => new ChannelListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<ChannelResponse> _repeated_channels_codec = FieldCodec.ForMessage(82u, ChannelResponse.Parser);

	private readonly RepeatedField<ChannelResponse> channels_ = new RepeatedField<ChannelResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelResponse> Channels => channels_;

	[GeneratedCode("protoc", null)]
	public ChannelListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelListResponse(ChannelListResponse other)
		: this()
	{
		channels_ = other.channels_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelListResponse Clone()
	{
		return new ChannelListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!channels_.Equals(other.channels_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= channels_.GetHashCode();
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
		channels_.WriteTo(ref P_0, _repeated_channels_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += channels_.CalculateSize(_repeated_channels_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelListResponse other)
	{
		if (other != null)
		{
			channels_.Add(other.channels_);
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
				channels_.AddEntriesFrom(ref P_0, _repeated_channels_codec);
			}
		}
	}
}
