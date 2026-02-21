using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Packets;

public sealed class MessageReferenceMapChannel : IMessage<MessageReferenceMapChannel>, IMessage, IEquatable<MessageReferenceMapChannel>, IDeepCloneable<MessageReferenceMapChannel>, IBufferMessage
{
	private static readonly MessageParser<MessageReferenceMapChannel> _parser = new MessageParser<MessageReferenceMapChannel>(() => new MessageReferenceMapChannel());

	private UnknownFieldSet _unknownFields;

	private ChannelUuid channelId_;

	private string name_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageReferenceMapChannel> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReferenceMapsReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public ChannelUuid ChannelId
	{
		get
		{
			return channelId_;
		}
		set
		{
			channelId_ = value;
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
	public MessageReferenceMapChannel()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageReferenceMapChannel(MessageReferenceMapChannel other)
		: this()
	{
		channelId_ = ((other.channelId_ != null) ? other.channelId_.Clone() : null);
		name_ = other.name_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageReferenceMapChannel Clone()
	{
		return new MessageReferenceMapChannel(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageReferenceMapChannel);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageReferenceMapChannel other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(ChannelId, other.ChannelId))
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
		if (channelId_ != null)
		{
			num ^= ChannelId.GetHashCode();
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
		if (channelId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(ChannelId);
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
		if (channelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelId);
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
	public void MergeFrom(MessageReferenceMapChannel other)
	{
		if (other == null)
		{
			return;
		}
		if (other.channelId_ != null)
		{
			if (channelId_ == null)
			{
				ChannelId = new ChannelUuid();
			}
			ChannelId.MergeFrom(other.ChannelId);
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
				if (channelId_ == null)
				{
					ChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(ChannelId);
				break;
			case 90u:
				Name = P_0.ReadString();
				break;
			}
		}
	}
}
