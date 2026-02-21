using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Packets;

public sealed class MessageReferenceMapCommunityRole : IMessage<MessageReferenceMapCommunityRole>, IMessage, IEquatable<MessageReferenceMapCommunityRole>, IDeepCloneable<MessageReferenceMapCommunityRole>, IBufferMessage
{
	private static readonly MessageParser<MessageReferenceMapCommunityRole> _parser = new MessageParser<MessageReferenceMapCommunityRole>(() => new MessageReferenceMapCommunityRole());

	private UnknownFieldSet _unknownFields;

	private CommunityRoleUuid communityRoleId_;

	private string name_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageReferenceMapCommunityRole> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReferenceMapsReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityRoleUuid CommunityRoleId
	{
		get
		{
			return communityRoleId_;
		}
		set
		{
			communityRoleId_ = value;
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
	public MessageReferenceMapCommunityRole()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageReferenceMapCommunityRole(MessageReferenceMapCommunityRole other)
		: this()
	{
		communityRoleId_ = ((other.communityRoleId_ != null) ? other.communityRoleId_.Clone() : null);
		name_ = other.name_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageReferenceMapCommunityRole Clone()
	{
		return new MessageReferenceMapCommunityRole(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageReferenceMapCommunityRole);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageReferenceMapCommunityRole other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityRoleId, other.CommunityRoleId))
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
		if (communityRoleId_ != null)
		{
			num ^= CommunityRoleId.GetHashCode();
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
		if (communityRoleId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityRoleId);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(90);
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
		if (communityRoleId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityRoleId);
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
	public void MergeFrom(MessageReferenceMapCommunityRole other)
	{
		if (other == null)
		{
			return;
		}
		if (other.communityRoleId_ != null)
		{
			if (communityRoleId_ == null)
			{
				CommunityRoleId = new CommunityRoleUuid();
			}
			CommunityRoleId.MergeFrom(other.CommunityRoleId);
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
			case 82u:
				if (communityRoleId_ == null)
				{
					CommunityRoleId = new CommunityRoleUuid();
				}
				P_0.ReadMessage(CommunityRoleId);
				break;
			case 90u:
				Name = P_0.ReadString();
				break;
			}
		}
	}
}
