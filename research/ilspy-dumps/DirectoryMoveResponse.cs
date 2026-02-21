using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class DirectoryMoveResponse : IMessage<DirectoryMoveResponse>, IMessage, IEquatable<DirectoryMoveResponse>, IDeepCloneable<DirectoryMoveResponse>, IBufferMessage
{
	private static readonly MessageParser<DirectoryMoveResponse> _parser = new MessageParser<DirectoryMoveResponse>(() => new DirectoryMoveResponse());

	private UnknownFieldSet _unknownFields;

	private DirectoryUuid id_;

	private DirectoryUuid parentDirectoryId_;

	private DirectoryUuid oldParentDirectoryId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectoryMoveResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectoryReflection.Descriptor.MessageTypes[4];

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
	public DirectoryUuid OldParentDirectoryId
	{
		get
		{
			return oldParentDirectoryId_;
		}
		set
		{
			oldParentDirectoryId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectoryMoveResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectoryMoveResponse(DirectoryMoveResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		parentDirectoryId_ = ((other.parentDirectoryId_ != null) ? other.parentDirectoryId_.Clone() : null);
		oldParentDirectoryId_ = ((other.oldParentDirectoryId_ != null) ? other.oldParentDirectoryId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectoryMoveResponse Clone()
	{
		return new DirectoryMoveResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectoryMoveResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectoryMoveResponse other)
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
		if (!object.Equals(OldParentDirectoryId, other.OldParentDirectoryId))
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
		if (oldParentDirectoryId_ != null)
		{
			num ^= OldParentDirectoryId.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (parentDirectoryId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(ParentDirectoryId);
		}
		if (oldParentDirectoryId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(OldParentDirectoryId);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (parentDirectoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ParentDirectoryId);
		}
		if (oldParentDirectoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OldParentDirectoryId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectoryMoveResponse other)
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
		if (other.oldParentDirectoryId_ != null)
		{
			if (oldParentDirectoryId_ == null)
			{
				OldParentDirectoryId = new DirectoryUuid();
			}
			OldParentDirectoryId.MergeFrom(other.OldParentDirectoryId);
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
				if (id_ == null)
				{
					Id = new DirectoryUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (parentDirectoryId_ == null)
				{
					ParentDirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(ParentDirectoryId);
				break;
			case 50u:
				if (oldParentDirectoryId_ == null)
				{
					OldParentDirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(OldParentDirectoryId);
				break;
			}
		}
	}
}
