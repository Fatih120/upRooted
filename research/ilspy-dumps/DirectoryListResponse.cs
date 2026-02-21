using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class DirectoryListResponse : IMessage<DirectoryListResponse>, IMessage, IEquatable<DirectoryListResponse>, IDeepCloneable<DirectoryListResponse>, IBufferMessage
{
	private static readonly MessageParser<DirectoryListResponse> _parser = new MessageParser<DirectoryListResponse>(() => new DirectoryListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<DirectoryResponse> _repeated_directories_codec = FieldCodec.ForMessage(114u, DirectoryResponse.Parser);

	private readonly RepeatedField<DirectoryResponse> directories_ = new RepeatedField<DirectoryResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectoryListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectoryReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<DirectoryResponse> Directories => directories_;

	[GeneratedCode("protoc", null)]
	public DirectoryListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectoryListResponse(DirectoryListResponse other)
		: this()
	{
		directories_ = other.directories_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectoryListResponse Clone()
	{
		return new DirectoryListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectoryListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectoryListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!directories_.Equals(other.directories_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= directories_.GetHashCode();
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
		directories_.WriteTo(ref P_0, _repeated_directories_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += directories_.CalculateSize(_repeated_directories_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectoryListResponse other)
	{
		if (other != null)
		{
			directories_.Add(other.directories_);
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
			if (num3 != 114)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				directories_.AddEntriesFrom(ref P_0, _repeated_directories_codec);
			}
		}
	}
}
