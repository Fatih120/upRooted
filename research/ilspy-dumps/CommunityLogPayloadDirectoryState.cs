using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadDirectoryState : IMessage<CommunityLogPayloadDirectoryState>, IMessage, IEquatable<CommunityLogPayloadDirectoryState>, IDeepCloneable<CommunityLogPayloadDirectoryState>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadDirectoryState> _parser = new MessageParser<CommunityLogPayloadDirectoryState>(() => new CommunityLogPayloadDirectoryState());

	private UnknownFieldSet _unknownFields;

	private DirectoryUuid directoryParentId_;

	private string name_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadDirectoryState> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[19];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public DirectoryUuid DirectoryParentId
	{
		get
		{
			return directoryParentId_;
		}
		set
		{
			directoryParentId_ = value;
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
	public CommunityLogPayloadDirectoryState()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadDirectoryState(CommunityLogPayloadDirectoryState other)
		: this()
	{
		directoryParentId_ = ((other.directoryParentId_ != null) ? other.directoryParentId_.Clone() : null);
		name_ = other.name_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadDirectoryState Clone()
	{
		return new CommunityLogPayloadDirectoryState(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadDirectoryState);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadDirectoryState other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(DirectoryParentId, other.DirectoryParentId))
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (directoryParentId_ != null)
		{
			num ^= DirectoryParentId.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
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
		if (directoryParentId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(DirectoryParentId);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(Name);
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
		if (directoryParentId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DirectoryParentId);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadDirectoryState other)
	{
		if (other == null)
		{
			return;
		}
		if (other.directoryParentId_ != null)
		{
			if (directoryParentId_ == null)
			{
				DirectoryParentId = new DirectoryUuid();
			}
			DirectoryParentId.MergeFrom(other.DirectoryParentId);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
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
				if (directoryParentId_ == null)
				{
					DirectoryParentId = new DirectoryUuid();
				}
				P_0.ReadMessage(DirectoryParentId);
				break;
			case 42u:
				Name = P_0.ReadString();
				break;
			}
		}
	}
}
