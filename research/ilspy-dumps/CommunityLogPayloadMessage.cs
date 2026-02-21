using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadMessage : IMessage, IMessage<CommunityLogPayloadMessage>, IEquatable<CommunityLogPayloadMessage>, IDeepCloneable<CommunityLogPayloadMessage>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadMessage> _parser = new MessageParser<CommunityLogPayloadMessage>(() => new CommunityLogPayloadMessage());

	private UnknownFieldSet _unknownFields;

	private CommunityLogAction communityLogAction_ = CommunityLogAction.Unspecified;

	private MessageUuid id_;

	private ChannelUuid channelId_;

	private string channelName_ = "";

	private UserUuid userId_;

	private string username_ = "";

	private CommunityLogPayloadMessageState original_;

	private CommunityLogPayloadMessageState current_;

	private FieldMask fieldMask_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadMessage> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[9];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityLogAction CommunityLogAction
	{
		get
		{
			return communityLogAction_;
		}
		set
		{
			communityLogAction_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageUuid Id
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
	public string ChannelName
	{
		get
		{
			return channelName_;
		}
		set
		{
			channelName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid UserId
	{
		get
		{
			return userId_;
		}
		set
		{
			userId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Username
	{
		get
		{
			return username_;
		}
		set
		{
			username_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMessageState Original
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
	public CommunityLogPayloadMessageState Current
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

	public CommunityLogPayloadItem ToPayloadItem()
	{
		return new CommunityLogPayloadItem
		{
			Message = this
		};
	}

	public void CreateFieldMask()
	{
		if (Current != null && Original != null)
		{
			FieldMask = new FieldMask();
			if (Original.MessageContent != Current.MessageContent)
			{
				FieldMask.Paths.Add("message_content");
			}
		}
	}

	public bool HasChanges()
	{
		CreateFieldMask();
		return FieldMask == null || FieldMask.Paths.Count > 0;
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMessage()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMessage(CommunityLogPayloadMessage other)
		: this()
	{
		communityLogAction_ = other.communityLogAction_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		channelId_ = ((other.channelId_ != null) ? other.channelId_.Clone() : null);
		channelName_ = other.channelName_;
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		username_ = other.username_;
		original_ = ((other.original_ != null) ? other.original_.Clone() : null);
		current_ = ((other.current_ != null) ? other.current_.Clone() : null);
		fieldMask_ = ((other.fieldMask_ != null) ? other.fieldMask_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMessage Clone()
	{
		return new CommunityLogPayloadMessage(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadMessage);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadMessage other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (CommunityLogAction != other.CommunityLogAction)
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(ChannelId, other.ChannelId))
		{
			return false;
		}
		if (ChannelName != other.ChannelName)
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (Username != other.Username)
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
		if (CommunityLogAction != CommunityLogAction.Unspecified)
		{
			num ^= CommunityLogAction.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (channelId_ != null)
		{
			num ^= ChannelId.GetHashCode();
		}
		if (ChannelName.Length != 0)
		{
			num ^= ChannelName.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (Username.Length != 0)
		{
			num ^= Username.GetHashCode();
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
		if (CommunityLogAction != CommunityLogAction.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)CommunityLogAction);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (channelId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(ChannelId);
		}
		if (ChannelName.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(ChannelName);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(UserId);
		}
		if (Username.Length != 0)
		{
			P_0.WriteRawTag(66);
			P_0.WriteString(Username);
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
		if (CommunityLogAction != CommunityLogAction.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)CommunityLogAction);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (channelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelId);
		}
		if (ChannelName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ChannelName);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (Username.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Username);
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
	public void MergeFrom(CommunityLogPayloadMessage other)
	{
		if (other == null)
		{
			return;
		}
		if (other.CommunityLogAction != CommunityLogAction.Unspecified)
		{
			CommunityLogAction = other.CommunityLogAction;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new MessageUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.channelId_ != null)
		{
			if (channelId_ == null)
			{
				ChannelId = new ChannelUuid();
			}
			ChannelId.MergeFrom(other.ChannelId);
		}
		if (other.ChannelName.Length != 0)
		{
			ChannelName = other.ChannelName;
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.Username.Length != 0)
		{
			Username = other.Username;
		}
		if (other.original_ != null)
		{
			if (original_ == null)
			{
				Original = new CommunityLogPayloadMessageState();
			}
			Original.MergeFrom(other.Original);
		}
		if (other.current_ != null)
		{
			if (current_ == null)
			{
				Current = new CommunityLogPayloadMessageState();
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
				CommunityLogAction = (CommunityLogAction)P_0.ReadEnum();
				break;
			case 34u:
				if (id_ == null)
				{
					Id = new MessageUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (channelId_ == null)
				{
					ChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(ChannelId);
				break;
			case 50u:
				ChannelName = P_0.ReadString();
				break;
			case 58u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 66u:
				Username = P_0.ReadString();
				break;
			case 74u:
				if (original_ == null)
				{
					Original = new CommunityLogPayloadMessageState();
				}
				P_0.ReadMessage(Original);
				break;
			case 82u:
				if (current_ == null)
				{
					Current = new CommunityLogPayloadMessageState();
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
