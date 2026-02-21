using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppListChannelGroupsResponse : IMessage<CommunityAppListChannelGroupsResponse>, IMessage, IEquatable<CommunityAppListChannelGroupsResponse>, IDeepCloneable<CommunityAppListChannelGroupsResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppListChannelGroupsResponse> _parser = new MessageParser<CommunityAppListChannelGroupsResponse>(() => new CommunityAppListChannelGroupsResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<CommunityAppChannelGroup> _repeated_channelGroups_codec = FieldCodec.ForMessage(34u, CommunityAppChannelGroup.Parser);

	private readonly RepeatedField<CommunityAppChannelGroup> channelGroups_ = new RepeatedField<CommunityAppChannelGroup>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppListChannelGroupsResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[15];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityAppChannelGroup> ChannelGroups => channelGroups_;

	[GeneratedCode("protoc", null)]
	public CommunityAppListChannelGroupsResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppListChannelGroupsResponse(CommunityAppListChannelGroupsResponse other)
		: this()
	{
		channelGroups_ = other.channelGroups_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppListChannelGroupsResponse Clone()
	{
		return new CommunityAppListChannelGroupsResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppListChannelGroupsResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppListChannelGroupsResponse other)
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
	public void MergeFrom(CommunityAppListChannelGroupsResponse other)
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
			if (num3 != 34)
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
