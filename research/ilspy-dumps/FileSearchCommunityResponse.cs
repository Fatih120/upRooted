using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FileSearchCommunityResponse : IMessage<FileSearchCommunityResponse>, IMessage, IEquatable<FileSearchCommunityResponse>, IDeepCloneable<FileSearchCommunityResponse>, IBufferMessage
{
	private static readonly MessageParser<FileSearchCommunityResponse> _parser = new MessageParser<FileSearchCommunityResponse>(() => new FileSearchCommunityResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<FileContainerResponse> _repeated_results_codec = FieldCodec.ForMessage(82u, FileContainerResponse.Parser);

	private readonly RepeatedField<FileContainerResponse> results_ = new RepeatedField<FileContainerResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileSearchCommunityResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[8];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<FileContainerResponse> Results => results_;

	[GeneratedCode("protoc", null)]
	public FileSearchCommunityResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileSearchCommunityResponse(FileSearchCommunityResponse other)
		: this()
	{
		results_ = other.results_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileSearchCommunityResponse Clone()
	{
		return new FileSearchCommunityResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileSearchCommunityResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileSearchCommunityResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!results_.Equals(other.results_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= results_.GetHashCode();
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
		results_.WriteTo(ref P_0, _repeated_results_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += results_.CalculateSize(_repeated_results_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileSearchCommunityResponse other)
	{
		if (other != null)
		{
			results_.Add(other.results_);
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
				results_.AddEntriesFrom(ref P_0, _repeated_results_codec);
			}
		}
	}
}
