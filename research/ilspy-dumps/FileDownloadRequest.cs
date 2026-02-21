using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class FileDownloadRequest : IMessage<FileDownloadRequest>, IMessage, IEquatable<FileDownloadRequest>, IDeepCloneable<FileDownloadRequest>, IBufferMessage
{
	private static readonly MessageParser<FileDownloadRequest> _parser = new MessageParser<FileDownloadRequest>(() => new FileDownloadRequest());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private FileUuid id_;

	private DirectoryUuid directoryId_;

	private AssetUuid assetId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileDownloadRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[8];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
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
	public FileDownloadRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileDownloadRequest(FileDownloadRequest other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		directoryId_ = ((other.directoryId_ != null) ? other.directoryId_.Clone() : null);
		assetId_ = ((other.assetId_ != null) ? other.assetId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileDownloadRequest Clone()
	{
		return new FileDownloadRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileDownloadRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileDownloadRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
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
		if (!object.Equals(DirectoryId, other.DirectoryId))
		{
			return false;
		}
		if (!object.Equals(AssetId, other.AssetId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (directoryId_ != null)
		{
			num ^= DirectoryId.GetHashCode();
		}
		if (assetId_ != null)
		{
			num ^= AssetId.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
		}
		if (containerId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(ContainerId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(Id);
		}
		if (directoryId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(DirectoryId);
		}
		if (assetId_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(AssetId);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (directoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DirectoryId);
		}
		if (assetId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AssetId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileDownloadRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
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
		if (other.directoryId_ != null)
		{
			if (directoryId_ == null)
			{
				DirectoryId = new DirectoryUuid();
			}
			DirectoryId.MergeFrom(other.DirectoryId);
		}
		if (other.assetId_ != null)
		{
			if (assetId_ == null)
			{
				AssetId = new AssetUuid();
			}
			AssetId.MergeFrom(other.AssetId);
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
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 98u:
				if (id_ == null)
				{
					Id = new FileUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 106u:
				if (directoryId_ == null)
				{
					DirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(DirectoryId);
				break;
			case 114u:
				if (assetId_ == null)
				{
					AssetId = new AssetUuid();
				}
				P_0.ReadMessage(AssetId);
				break;
			}
		}
	}
}
