using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class ChannelGroupListResponse : IMessage<ChannelGroupListResponse>, IMessage, IEquatable<ChannelGroupListResponse>, IDeepCloneable<ChannelGroupListResponse>, IBufferMessage
{
	private static readonly MessageParser<ChannelGroupListResponse> _parser = new MessageParser<ChannelGroupListResponse>(() => new ChannelGroupListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<ChannelGroupResponse> _repeated_channelGroups_codec = FieldCodec.ForMessage(82u, ChannelGroupResponse.Parser);

	private readonly RepeatedField<ChannelGroupResponse> channelGroups_ = new RepeatedField<ChannelGroupResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelGroupListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelGroupReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelGroupResponse> ChannelGroups => channelGroups_;

	[GeneratedCode("protoc", null)]
	public ChannelGroupListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupListResponse(ChannelGroupListResponse other)
		: this()
	{
		channelGroups_ = other.channelGroups_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupListResponse Clone()
	{
		return new ChannelGroupListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelGroupListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelGroupListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!channelGroups_.Equals(other.channelGroups_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= channelGroups_.GetHashCode();
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
		channelGroups_.WriteTo(ref P_0, _repeated_channelGroups_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += channelGroups_.CalculateSize(_repeated_channelGroups_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelGroupListResponse other)
	{
		if (other != null)
		{
			channelGroups_.Add(other.channelGroups_);
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
				channelGroups_.AddEntriesFrom(ref P_0, _repeated_channelGroups_codec);
			}
		}
	}
}
