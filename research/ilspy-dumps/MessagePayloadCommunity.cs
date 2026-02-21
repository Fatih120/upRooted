using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Payloads.Message;

public sealed class MessagePayloadCommunity : IMessage, IMessage<MessagePayloadCommunity>, IEquatable<MessagePayloadCommunity>, IDeepCloneable<MessagePayloadCommunity>, IBufferMessage
{
	private static readonly MessageParser<MessagePayloadCommunity> _parser = new MessageParser<MessagePayloadCommunity>(() => new MessagePayloadCommunity());

	private UnknownFieldSet _unknownFields;

	private MessagePayloadAction messagePayloadAction_ = MessagePayloadAction.Unspecified;

	private CommunityUuid id_;

	private MessagePayloadCommunityState original_;

	private MessagePayloadCommunityState current_;

	private FieldMask fieldMask_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePayloadCommunity> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessagesReflection.Descriptor.MessageTypes[2];

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
	public CommunityUuid Id
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
	public MessagePayloadCommunityState Original
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
	public MessagePayloadCommunityState Current
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
			Community = this
		};
	}

	public bool HasChanges()
	{
		if (Current == null || Original == null)
		{
			return true;
		}
		return Current.Name == Original.Name || Current.OwnerUserId == Original.OwnerUserId || Current.DefaultChannelId == Original.DefaultChannelId;
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunity()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunity(MessagePayloadCommunity other)
		: this()
	{
		messagePayloadAction_ = other.messagePayloadAction_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		original_ = ((other.original_ != null) ? other.original_.Clone() : null);
		current_ = ((other.current_ != null) ? other.current_.Clone() : null);
		fieldMask_ = ((other.fieldMask_ != null) ? other.fieldMask_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunity Clone()
	{
		return new MessagePayloadCommunity(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePayloadCommunity);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePayloadCommunity other)
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
		if (original_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(Original);
		}
		if (current_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(Current);
		}
		if (fieldMask_ != null)
		{
			P_0.WriteRawTag(58);
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
	public void MergeFrom(MessagePayloadCommunity other)
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
				Id = new CommunityUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.original_ != null)
		{
			if (original_ == null)
			{
				Original = new MessagePayloadCommunityState();
			}
			Original.MergeFrom(other.Original);
		}
		if (other.current_ != null)
		{
			if (current_ == null)
			{
				Current = new MessagePayloadCommunityState();
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
					Id = new CommunityUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (original_ == null)
				{
					Original = new MessagePayloadCommunityState();
				}
				P_0.ReadMessage(Original);
				break;
			case 50u:
				if (current_ == null)
				{
					Current = new MessagePayloadCommunityState();
				}
				P_0.ReadMessage(Current);
				break;
			case 58u:
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
