using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class FileMoveRequest : IMessage<FileMoveRequest>, IMessage, IEquatable<FileMoveRequest>, IDeepCloneable<FileMoveRequest>, IBufferMessage
{
	private static readonly MessageParser<FileMoveRequest> _parser = new MessageParser<FileMoveRequest>(() => new FileMoveRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private FileUuid id_;

	private DirectoryUuid oldDirectoryId_;

	private DirectoryUuid newDirectoryId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileMoveRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootContext Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = value;
		}
	}

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
	public DirectoryUuid OldDirectoryId
	{
		get
		{
			return oldDirectoryId_;
		}
		set
		{
			oldDirectoryId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectoryUuid NewDirectoryId
	{
		get
		{
			return newDirectoryId_;
		}
		set
		{
			newDirectoryId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FileMoveRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileMoveRequest(FileMoveRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		oldDirectoryId_ = ((other.oldDirectoryId_ != null) ? other.oldDirectoryId_.Clone() : null);
		newDirectoryId_ = ((other.newDirectoryId_ != null) ? other.newDirectoryId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileMoveRequest Clone()
	{
		return new FileMoveRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileMoveRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileMoveRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Context, other.Context))
		{
			return false;
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
		if (!object.Equals(OldDirectoryId, other.OldDirectoryId))
		{
			return false;
		}
		if (!object.Equals(NewDirectoryId, other.NewDirectoryId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
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
		if (oldDirectoryId_ != null)
		{
			num ^= OldDirectoryId.GetHashCode();
		}
		if (newDirectoryId_ != null)
		{
			num ^= NewDirectoryId.GetHashCode();
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
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
		if (oldDirectoryId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(OldDirectoryId);
		}
		if (newDirectoryId_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(NewDirectoryId);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
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
		if (oldDirectoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OldDirectoryId);
		}
		if (newDirectoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(NewDirectoryId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileMoveRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.context_ != null)
		{
			if (context_ == null)
			{
				Context = new RootContext();
			}
			Context.MergeFrom(other.Context);
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
		if (other.oldDirectoryId_ != null)
		{
			if (oldDirectoryId_ == null)
			{
				OldDirectoryId = new DirectoryUuid();
			}
			OldDirectoryId.MergeFrom(other.OldDirectoryId);
		}
		if (other.newDirectoryId_ != null)
		{
			if (newDirectoryId_ == null)
			{
				NewDirectoryId = new DirectoryUuid();
			}
			NewDirectoryId.MergeFrom(other.NewDirectoryId);
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
			case 10u:
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
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
				if (oldDirectoryId_ == null)
				{
					OldDirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(OldDirectoryId);
				break;
			case 114u:
				if (newDirectoryId_ == null)
				{
					NewDirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(NewDirectoryId);
				break;
			}
		}
	}
}
