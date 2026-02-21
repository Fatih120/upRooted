using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Payloads.Message;

public sealed class MessagePayloadChannel : IMessage, IMessage<MessagePayloadChannel>, IEquatable<MessagePayloadChannel>, IDeepCloneable<MessagePayloadChannel>, IBufferMessage
{
	private static readonly MessageParser<MessagePayloadChannel> _parser = new MessageParser<MessagePayloadChannel>(() => new MessagePayloadChannel());

	private UnknownFieldSet _unknownFields;

	private MessagePayloadAction messagePayloadAction_ = MessagePayloadAction.Unspecified;

	private ChannelUuid id_;

	private ChannelType channelType_ = ChannelType.Unspecified;

	private MessagePayloadChannelState original_;

	private MessagePayloadChannelState current_;

	private FieldMask fieldMask_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePayloadChannel> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessagesReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessagePayloadAction MessagePayloadAction
	{
		get
		{
			return messagePayloadAction_;
		}
		set
		{
			messagePayloadAction_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelUuid Id
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
	public ChannelType ChannelType
	{
		get
		{
			return channelType_;
		}
		set
		{
			channelType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadChannelState Original
	{
		get
		{
			return original_;
		}
		set
		{
			original_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadChannelState Current
	{
		get
		{
			return current_;
		}
		set
		{
			current_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FieldMask FieldMask
	{
		get
		{
			return fieldMask_;
		}
		set
		{
			fieldMask_ = value;
		}
	}

	public void CreateFieldMask()
	{
		throw new NotImplementedException("Should be copied from community log");
	}

	public MessagePayloadItem ToPayloadItem()
	{
		return new MessagePayloadItem
		{
			Channel = this
		};
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadChannel()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadChannel(MessagePayloadChannel other)
		: this()
	{
		messagePayloadAction_ = other.messagePayloadAction_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		channelType_ = other.channelType_;
		original_ = ((other.original_ != null) ? other.original_.Clone() : null);
		current_ = ((other.current_ != null) ? other.current_.Clone() : null);
		fieldMask_ = ((other.fieldMask_ != null) ? other.fieldMask_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadChannel Clone()
	{
		return new MessagePayloadChannel(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePayloadChannel);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePayloadChannel other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MessagePayloadAction != other.MessagePayloadAction)
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (ChannelType != other.ChannelType)
		{
			return false;
		}
		if (!object.Equals(Original, other.Original))
		{
			return false;
		}
		if (!object.Equals(Current, other.Current))
		{
			return false;
		}
		if (!object.Equals(FieldMask, other.FieldMask))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (MessagePayloadAction != MessagePayloadAction.Unspecified)
		{
			num ^= MessagePayloadAction.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (ChannelType != ChannelType.Unspecified)
		{
			num ^= ChannelType.GetHashCode();
		}
		if (original_ != null)
		{
			num ^= Original.GetHashCode();
		}
		if (current_ != null)
		{
			num ^= Current.GetHashCode();
		}
		if (fieldMask_ != null)
		{
			num ^= FieldMask.GetHashCode();
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
		if (MessagePayloadAction != MessagePayloadAction.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)MessagePayloadAction);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (ChannelType != ChannelType.Unspecified)
		{
			P_0.WriteRawTag(48);
			P_0.WriteEnum((int)ChannelType);
		}
		if (original_ != null)
		{
			P_0.WriteRawTag(74);
			P_0.WriteMessage(Original);
		}
		if (current_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Current);
		}
		if (fieldMask_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(FieldMask);
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
		if (MessagePayloadAction != MessagePayloadAction.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MessagePayloadAction);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (ChannelType != ChannelType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ChannelType);
		}
		if (original_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Original);
		}
		if (current_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Current);
		}
		if (fieldMask_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FieldMask);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessagePayloadChannel other)
	{
		if (other == null)
		{
			return;
		}
		if (other.MessagePayloadAction != MessagePayloadAction.Unspecified)
		{
			MessagePayloadAction = other.MessagePayloadAction;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new ChannelUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.ChannelType != ChannelType.Unspecified)
		{
			ChannelType = other.ChannelType;
		}
		if (other.original_ != null)
		{
			if (original_ == null)
			{
				Original = new MessagePayloadChannelState();
			}
			Original.MergeFrom(other.Original);
		}
		if (other.current_ != null)
		{
			if (current_ == null)
			{
				Current = new MessagePayloadChannelState();
			}
			Current.MergeFrom(other.Current);
		}
		if (other.fieldMask_ != null)
		{
			if (fieldMask_ == null)
			{
				FieldMask = new FieldMask();
			}
			FieldMask.MergeFrom(other.FieldMask);
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
			case 8u:
				MessagePayloadAction = (MessagePayloadAction)P_0.ReadEnum();
				break;
			case 34u:
				if (id_ == null)
				{
					Id = new ChannelUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 48u:
				ChannelType = (ChannelType)P_0.ReadEnum();
				break;
			case 74u:
				if (original_ == null)
				{
					Original = new MessagePayloadChannelState();
				}
				P_0.ReadMessage(Original);
				break;
			case 82u:
				if (current_ == null)
				{
					Current = new MessagePayloadChannelState();
				}
				P_0.ReadMessage(Current);
				break;
			case 90u:
				if (fieldMask_ == null)
				{
					FieldMask = new FieldMask();
				}
				P_0.ReadMessage(FieldMask);
				break;
			}
		}
	}
}
