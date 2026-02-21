using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppLogListResponse : IMessage<CommunityAppLogListResponse>, IMessage, IEquatable<CommunityAppLogListResponse>, IDeepCloneable<CommunityAppLogListResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppLogListResponse> _parser = new MessageParser<CommunityAppLogListResponse>(() => new CommunityAppLogListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<CommunityAppLogResponse> _repeated_communityAppLogs_codec = FieldCodec.ForMessage(34u, CommunityAppLogResponse.Parser);

	private readonly RepeatedField<CommunityAppLogResponse> communityAppLogs_ = new RepeatedField<CommunityAppLogResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppLogListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppLogReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityAppLogResponse> CommunityAppLogs => communityAppLogs_;

	[GeneratedCode("protoc", null)]
	public CommunityAppLogListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppLogListResponse(CommunityAppLogListResponse other)
		: this()
	{
		communityAppLogs_ = other.communityAppLogs_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppLogListResponse Clone()
	{
		return new CommunityAppLogListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppLogListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppLogListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!communityAppLogs_.Equals(other.communityAppLogs_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= communityAppLogs_.GetHashCode();
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
		communityAppLogs_.WriteTo(ref P_0, _repeated_communityAppLogs_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += communityAppLogs_.CalculateSize(_repeated_communityAppLogs_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppLogListResponse other)
	{
		if (other != null)
		{
			communityAppLogs_.Add(other.communityAppLogs_);
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
				communityAppLogs_.AddEntriesFrom(ref P_0, _repeated_communityAppLogs_codec);
			}
		}
	}
}
