using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FileMoveResponse : IMessage<FileMoveResponse>, IMessage, IEquatable<FileMoveResponse>, IDeepCloneable<FileMoveResponse>, IBufferMessage
{
	private static readonly MessageParser<FileMoveResponse> _parser = new MessageParser<FileMoveResponse>(() => new FileMoveResponse());

	private UnknownFieldSet _unknownFields;

	private FileUuid id_;

	private DirectoryUuid directoryId_;

	private DirectoryUuid oldDirectoryId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileMoveResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public FileMoveResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileMoveResponse(FileMoveResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		directoryId_ = ((other.directoryId_ != null) ? other.directoryId_.Clone() : null);
		oldDirectoryId_ = ((other.oldDirectoryId_ != null) ? other.oldDirectoryId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileMoveResponse Clone()
	{
		return new FileMoveResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileMoveResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileMoveResponse other)
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
		if (!object.Equals(DirectoryId, other.DirectoryId))
		{
			return false;
		}
		if (!object.Equals(OldDirectoryId, other.OldDirectoryId))
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
		if (directoryId_ != null)
		{
			num ^= DirectoryId.GetHashCode();
		}
		if (oldDirectoryId_ != null)
		{
			num ^= OldDirectoryId.GetHashCode();
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
		if (directoryId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(DirectoryId);
		}
		if (oldDirectoryId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(OldDirectoryId);
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
		if (directoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DirectoryId);
		}
		if (oldDirectoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OldDirectoryId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileMoveResponse other)
	{
		if (other == null)
		{
			return;
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
		if (other.oldDirectoryId_ != null)
		{
			if (oldDirectoryId_ == null)
			{
				OldDirectoryId = new DirectoryUuid();
			}
			OldDirectoryId.MergeFrom(other.OldDirectoryId);
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
					Id = new FileUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (directoryId_ == null)
				{
					DirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(DirectoryId);
				break;
			case 50u:
				if (oldDirectoryId_ == null)
				{
					OldDirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(OldDirectoryId);
				break;
			}
		}
	}
}
