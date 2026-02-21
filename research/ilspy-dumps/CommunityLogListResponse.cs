using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityLogListResponse : IMessage<CommunityLogListResponse>, IMessage, IEquatable<CommunityLogListResponse>, IDeepCloneable<CommunityLogListResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogListResponse> _parser = new MessageParser<CommunityLogListResponse>(() => new CommunityLogListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<CommunityLogResponse> _repeated_communityLogs_codec = FieldCodec.ForMessage(82u, CommunityLogResponse.Parser);

	private readonly RepeatedField<CommunityLogResponse> communityLogs_ = new RepeatedField<CommunityLogResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityLogResponse> CommunityLogs => communityLogs_;

	[GeneratedCode("protoc", null)]
	public CommunityLogListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogListResponse(CommunityLogListResponse other)
		: this()
	{
		communityLogs_ = other.communityLogs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogListResponse Clone()
	{
		return new CommunityLogListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!communityLogs_.Equals(other.communityLogs_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= communityLogs_.GetHashCode();
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
		communityLogs_.WriteTo(ref P_0, _repeated_communityLogs_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += communityLogs_.CalculateSize(_repeated_communityLogs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogListResponse other)
	{
		if (other != null)
		{
			communityLogs_.Add(other.communityLogs_);
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
				communityLogs_.AddEntriesFrom(ref P_0, _repeated_communityLogs_codec);
			}
		}
	}
}
