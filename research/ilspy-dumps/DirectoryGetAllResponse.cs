using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class DirectoryGetAllResponse : IMessage<DirectoryGetAllResponse>, IMessage, IEquatable<DirectoryGetAllResponse>, IDeepCloneable<DirectoryGetAllResponse>, IBufferMessage
{
	private static readonly MessageParser<DirectoryGetAllResponse> _parser = new MessageParser<DirectoryGetAllResponse>(() => new DirectoryGetAllResponse());

	private UnknownFieldSet _unknownFields;

	private DirectoryUuid id_;

	private DirectoryUuid parentDirectoryId_;

	private string name_ = "";

	private static readonly FieldCodec<FileResponse> _repeated_files_codec = FieldCodec.ForMessage(106u, FileResponse.Parser);

	private readonly RepeatedField<FileResponse> files_ = new RepeatedField<FileResponse>();

	private static readonly FieldCodec<DirectoryResponse> _repeated_directories_codec = FieldCodec.ForMessage(114u, DirectoryResponse.Parser);

	private readonly RepeatedField<DirectoryResponse> directories_ = new RepeatedField<DirectoryResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectoryGetAllResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectoryReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public DirectoryUuid Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectoryUuid ParentDirectoryId
	{
		get
		{
			return parentDirectoryId_;
		}
		set
		{
			parentDirectoryId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<FileResponse> Files => files_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<DirectoryResponse> Directories => directories_;

	[GeneratedCode("protoc", null)]
	public DirectoryGetAllResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectoryGetAllResponse(DirectoryGetAllResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		parentDirectoryId_ = ((other.parentDirectoryId_ != null) ? other.parentDirectoryId_.Clone() : null);
		name_ = other.name_;
		files_ = other.files_.Clone();
		directories_ = other.directories_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectoryGetAllResponse Clone()
	{
		return new DirectoryGetAllResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectoryGetAllResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectoryGetAllResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(ParentDirectoryId, other.ParentDirectoryId))
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (!files_.Equals(other.files_))
		{
			return false;
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
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (parentDirectoryId_ != null)
		{
			num ^= ParentDirectoryId.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		num ^= files_.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Id);
		}
		if (parentDirectoryId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(ParentDirectoryId);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Name);
		}
		files_.WriteTo(ref P_0, _repeated_files_codec);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (parentDirectoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ParentDirectoryId);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		num += files_.CalculateSize(_repeated_files_codec);
		num += directories_.CalculateSize(_repeated_directories_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectoryGetAllResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new DirectoryUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.parentDirectoryId_ != null)
		{
			if (parentDirectoryId_ == null)
			{
				ParentDirectoryId = new DirectoryUuid();
			}
			ParentDirectoryId.MergeFrom(other.ParentDirectoryId);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		files_.Add(other.files_);
		directories_.Add(other.directories_);
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
				if (id_ == null)
				{
					Id = new DirectoryUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 90u:
				if (parentDirectoryId_ == null)
				{
					ParentDirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(ParentDirectoryId);
				break;
			case 98u:
				Name = P_0.ReadString();
				break;
			case 106u:
				files_.AddEntriesFrom(ref P_0, _repeated_files_codec);
				break;
			case 114u:
				directories_.AddEntriesFrom(ref P_0, _repeated_directories_codec);
				break;
			}
		}
	}
}
