using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class FileSearchRequest : IMessage<FileSearchRequest>, IMessage, IEquatable<FileSearchRequest>, IDeepCloneable<FileSearchRequest>, IBufferMessage
{
	private static readonly MessageParser<FileSearchRequest> _parser = new MessageParser<FileSearchRequest>(() => new FileSearchRequest());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private string search_ = "";

	private FileUuid lastFileId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileSearchRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[6];

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
	public string Search
	{
		get
		{
			return search_;
		}
		set
		{
			search_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public FileUuid LastFileId
	{
		get
		{
			return lastFileId_;
		}
		set
		{
			lastFileId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FileSearchRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileSearchRequest(FileSearchRequest other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		search_ = other.search_;
		lastFileId_ = ((other.lastFileId_ != null) ? other.lastFileId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileSearchRequest Clone()
	{
		return new FileSearchRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileSearchRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileSearchRequest other)
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
		if (Search != other.Search)
		{
			return false;
		}
		if (!object.Equals(LastFileId, other.LastFileId))
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
		if (Search.Length != 0)
		{
			num ^= Search.GetHashCode();
		}
		if (lastFileId_ != null)
		{
			num ^= LastFileId.GetHashCode();
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
		if (Search.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Search);
		}
		if (lastFileId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(LastFileId);
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
		if (Search.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Search);
		}
		if (lastFileId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(LastFileId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileSearchRequest other)
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
		if (other.Search.Length != 0)
		{
			Search = other.Search;
		}
		if (other.lastFileId_ != null)
		{
			if (lastFileId_ == null)
			{
				LastFileId = new FileUuid();
			}
			LastFileId.MergeFrom(other.LastFileId);
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
				Search = P_0.ReadString();
				break;
			case 106u:
				if (lastFileId_ == null)
				{
					LastFileId = new FileUuid();
				}
				P_0.ReadMessage(LastFileId);
				break;
			}
		}
	}
}
