using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FileContainerResponse : IMessage<FileContainerResponse>, IMessage, IEquatable<FileContainerResponse>, IDeepCloneable<FileContainerResponse>, IBufferMessage
{
	private static readonly MessageParser<FileContainerResponse> _parser = new MessageParser<FileContainerResponse>(() => new FileContainerResponse());

	private UnknownFieldSet _unknownFields;

	private MessageContainerUuid containerId_;

	private static readonly FieldCodec<FileResponse> _repeated_files_codec = FieldCodec.ForMessage(90u, FileResponse.Parser);

	private readonly RepeatedField<FileResponse> files_ = new RepeatedField<FileResponse>();

	private int totalCount_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileContainerResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessageContainerUuid ContainerId
	{
		get
		{
			return containerId_;
		}
		set
		{
			containerId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<FileResponse> Files => files_;

	[GeneratedCode("protoc", null)]
	public int TotalCount
	{
		get
		{
			return totalCount_;
		}
		set
		{
			totalCount_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FileContainerResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileContainerResponse(FileContainerResponse other)
		: this()
	{
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		files_ = other.files_.Clone();
		totalCount_ = other.totalCount_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileContainerResponse Clone()
	{
		return new FileContainerResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileContainerResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileContainerResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!files_.Equals(other.files_))
		{
			return false;
		}
		if (TotalCount != other.TotalCount)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		num ^= files_.GetHashCode();
		if (TotalCount != 0)
		{
			num ^= TotalCount.GetHashCode();
		}
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
		if (containerId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(ContainerId);
		}
		files_.WriteTo(ref P_0, _repeated_files_codec);
		if (TotalCount != 0)
		{
			P_0.WriteRawTag(96);
			P_0.WriteInt32(TotalCount);
		}
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		num += files_.CalculateSize(_repeated_files_codec);
		if (TotalCount != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(TotalCount);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileContainerResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new MessageContainerUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		files_.Add(other.files_);
		if (other.TotalCount != 0)
		{
			TotalCount = other.TotalCount;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 82u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 90u:
				files_.AddEntriesFrom(ref P_0, _repeated_files_codec);
				break;
			case 96u:
				TotalCount = P_0.ReadInt32();
				break;
			}
		}
	}
}
