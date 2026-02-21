using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FileListResponse : IMessage<FileListResponse>, IMessage, IEquatable<FileListResponse>, IDeepCloneable<FileListResponse>, IBufferMessage
{
	private static readonly MessageParser<FileListResponse> _parser = new MessageParser<FileListResponse>(() => new FileListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<FileResponse> _repeated_files_codec = FieldCodec.ForMessage(90u, FileResponse.Parser);

	private readonly RepeatedField<FileResponse> files_ = new RepeatedField<FileResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[7];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<FileResponse> Files => files_;

	[GeneratedCode("protoc", null)]
	public FileListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileListResponse(FileListResponse other)
		: this()
	{
		files_ = other.files_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileListResponse Clone()
	{
		return new FileListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!files_.Equals(other.files_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= files_.GetHashCode();
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
		files_.WriteTo(ref P_0, _repeated_files_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += files_.CalculateSize(_repeated_files_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileListResponse other)
	{
		if (other != null)
		{
			files_.Add(other.files_);
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
			if (num3 != 90)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				files_.AddEntriesFrom(ref P_0, _repeated_files_codec);
			}
		}
	}
}
