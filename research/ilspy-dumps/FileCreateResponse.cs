using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FileCreateResponse : IMessage<FileCreateResponse>, IMessage, IEquatable<FileCreateResponse>, IDeepCloneable<FileCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<FileCreateResponse> _parser = new MessageParser<FileCreateResponse>(() => new FileCreateResponse());

	private UnknownFieldSet _unknownFields;

	private DirectoryUuid directoryId_;

	private MessageContainerUuid containerId_;

	private FileUuid id_;

	private string name_ = "";

	private long length_;

	private string mimeType_ = "";

	private AssetUuid assetId_;

	private Timestamp modifiedAt_;

	private ByteString sha256_ = ByteString.Empty;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public DirectoryUuid DirectoryId
	{
		get
		{
			return directoryId_;
		}
		set
		{
			directoryId_ = value;
		}
	}

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
	public FileUuid Id
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
	public long Length
	{
		get
		{
			return length_;
		}
		set
		{
			length_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string MimeType
	{
		get
		{
			return mimeType_;
		}
		set
		{
			mimeType_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetUuid AssetId
	{
		get
		{
			return assetId_;
		}
		set
		{
			assetId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp ModifiedAt
	{
		get
		{
			return modifiedAt_;
		}
		set
		{
			modifiedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ByteString Sha256
	{
		get
		{
			return sha256_;
		}
		set
		{
			sha256_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public FileCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileCreateResponse(FileCreateResponse other)
		: this()
	{
		directoryId_ = ((other.directoryId_ != null) ? other.directoryId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		name_ = other.name_;
		length_ = other.length_;
		mimeType_ = other.mimeType_;
		assetId_ = ((other.assetId_ != null) ? other.assetId_.Clone() : null);
		modifiedAt_ = ((other.modifiedAt_ != null) ? other.modifiedAt_.Clone() : null);
		sha256_ = other.sha256_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileCreateResponse Clone()
	{
		return new FileCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileCreateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(DirectoryId, other.DirectoryId))
		{
			return false;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (Length != other.Length)
		{
			return false;
		}
		if (MimeType != other.MimeType)
		{
			return false;
		}
		if (!object.Equals(AssetId, other.AssetId))
		{
			return false;
		}
		if (!object.Equals(ModifiedAt, other.ModifiedAt))
		{
			return false;
		}
		if (Sha256 != other.Sha256)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (directoryId_ != null)
		{
			num ^= DirectoryId.GetHashCode();
		}
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (Length != 0)
		{
			num ^= Length.GetHashCode();
		}
		if (MimeType.Length != 0)
		{
			num ^= MimeType.GetHashCode();
		}
		if (assetId_ != null)
		{
			num ^= AssetId.GetHashCode();
		}
		if (modifiedAt_ != null)
		{
			num ^= ModifiedAt.GetHashCode();
		}
		if (Sha256.Length != 0)
		{
			num ^= Sha256.GetHashCode();
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
		if (directoryId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(DirectoryId);
		}
		if (containerId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(ContainerId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(Id);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(Name);
		}
		if (Length != 0)
		{
			P_0.WriteRawTag(64);
			P_0.WriteInt64(Length);
		}
		if (MimeType.Length != 0)
		{
			P_0.WriteRawTag(74);
			P_0.WriteString(MimeType);
		}
		if (assetId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(AssetId);
		}
		if (modifiedAt_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(ModifiedAt);
		}
		if (Sha256.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteBytes(Sha256);
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
		if (directoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DirectoryId);
		}
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(Length);
		}
		if (MimeType.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(MimeType);
		}
		if (assetId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AssetId);
		}
		if (modifiedAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ModifiedAt);
		}
		if (Sha256.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeBytesSize(Sha256);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileCreateResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.directoryId_ != null)
		{
			if (directoryId_ == null)
			{
				DirectoryId = new DirectoryUuid();
			}
			DirectoryId.MergeFrom(other.DirectoryId);
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new MessageContainerUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new FileUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.Length != 0)
		{
			Length = other.Length;
		}
		if (other.MimeType.Length != 0)
		{
			MimeType = other.MimeType;
		}
		if (other.assetId_ != null)
		{
			if (assetId_ == null)
			{
				AssetId = new AssetUuid();
			}
			AssetId.MergeFrom(other.AssetId);
		}
		if (other.modifiedAt_ != null)
		{
			if (modifiedAt_ == null)
			{
				ModifiedAt = new Timestamp();
			}
			ModifiedAt.MergeFrom(other.ModifiedAt);
		}
		if (other.Sha256.Length != 0)
		{
			Sha256 = other.Sha256;
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
			case 34u:
				if (directoryId_ == null)
				{
					DirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(DirectoryId);
				break;
			case 42u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 50u:
				if (id_ == null)
				{
					Id = new FileUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 58u:
				Name = P_0.ReadString();
				break;
			case 64u:
				Length = P_0.ReadInt64();
				break;
			case 74u:
				MimeType = P_0.ReadString();
				break;
			case 82u:
				if (assetId_ == null)
				{
					AssetId = new AssetUuid();
				}
				P_0.ReadMessage(AssetId);
				break;
			case 90u:
				if (modifiedAt_ == null)
				{
					ModifiedAt = new Timestamp();
				}
				P_0.ReadMessage(ModifiedAt);
				break;
			case 98u:
				Sha256 = P_0.ReadBytes();
				break;
			}
		}
	}
}
