using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class DirectoryListAllResponse : IMessage<DirectoryListAllResponse>, IMessage, IEquatable<DirectoryListAllResponse>, IDeepCloneable<DirectoryListAllResponse>, IBufferMessage
{
	private static readonly MessageParser<DirectoryListAllResponse> _parser = new MessageParser<DirectoryListAllResponse>(() => new DirectoryListAllResponse());

	private UnknownFieldSet _unknownFields;

	private MessageContainerUuid containerId_;

	private static readonly FieldCodec<DirectoryResponse> _repeated_directories_codec = FieldCodec.ForMessage(90u, DirectoryResponse.Parser);

	private readonly RepeatedField<DirectoryResponse> directories_ = new RepeatedField<DirectoryResponse>();

	private static readonly FieldCodec<FileResponse> _repeated_files_codec = FieldCodec.ForMessage(98u, FileResponse.Parser);

	private readonly RepeatedField<FileResponse> files_ = new RepeatedField<FileResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectoryListAllResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectoryReflection.Descriptor.MessageTypes[7];

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
	public RepeatedField<DirectoryResponse> Directories => directories_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<FileResponse> Files => files_;

	[GeneratedCode("protoc", null)]
	public DirectoryListAllResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectoryListAllResponse(DirectoryListAllResponse other)
		: this()
	{
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		directories_ = other.directories_.Clone();
		files_ = other.files_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectoryListAllResponse Clone()
	{
		return new DirectoryListAllResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectoryListAllResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectoryListAllResponse other)
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
		if (!directories_.Equals(other.directories_))
		{
			return false;
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
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		num ^= directories_.GetHashCode();
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
		if (containerId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(ContainerId);
		}
		directories_.WriteTo(ref P_0, _repeated_directories_codec);
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
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		num += directories_.CalculateSize(_repeated_directories_codec);
		num += files_.CalculateSize(_repeated_files_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectoryListAllResponse other)
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
		directories_.Add(other.directories_);
		files_.Add(other.files_);
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
				directories_.AddEntriesFrom(ref P_0, _repeated_directories_codec);
				break;
			case 98u:
				files_.AddEntriesFrom(ref P_0, _repeated_files_codec);
				break;
			}
		}
	}
}
