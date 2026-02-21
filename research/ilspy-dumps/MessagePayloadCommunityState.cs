using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.Message;

public sealed class MessagePayloadCommunityState : IMessage<MessagePayloadCommunityState>, IMessage, IEquatable<MessagePayloadCommunityState>, IDeepCloneable<MessagePayloadCommunityState>, IBufferMessage
{
	private static readonly MessageParser<MessagePayloadCommunityState> _parser = new MessageParser<MessagePayloadCommunityState>(() => new MessagePayloadCommunityState());

	private UnknownFieldSet _unknownFields;

	private string name_ = "";

	private ChannelUuid defaultChannelId_;

	private UserUuid ownerUserId_;

	private string defaultChannelName_ = "";

	private string ownerUsername_ = "";

	private string pictureHex_ = "";

	private static readonly FieldCodec<string> _single_pictureAssetUri_codec = FieldCodec.ForClassWrapper<string>(74u);

	private string pictureAssetUri_;

	private bool rejectUnverifiedEmail_;

	private MessagePayloadCommunityJoinThrottle joinThrottle_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePayloadCommunityState> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessagesReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public ChannelUuid DefaultChannelId
	{
		get
		{
			return defaultChannelId_;
		}
		set
		{
			defaultChannelId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid OwnerUserId
	{
		get
		{
			return ownerUserId_;
		}
		set
		{
			ownerUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string DefaultChannelName
	{
		get
		{
			return defaultChannelName_;
		}
		set
		{
			defaultChannelName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string OwnerUsername
	{
		get
		{
			return ownerUsername_;
		}
		set
		{
			ownerUsername_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string PictureHex
	{
		get
		{
			return pictureHex_;
		}
		set
		{
			pictureHex_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string PictureAssetUri
	{
		get
		{
			return pictureAssetUri_;
		}
		set
		{
			pictureAssetUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool RejectUnverifiedEmail
	{
		get
		{
			return rejectUnverifiedEmail_;
		}
		set
		{
			rejectUnverifiedEmail_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunityJoinThrottle JoinThrottle
	{
		get
		{
			return joinThrottle_;
		}
		set
		{
			joinThrottle_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunityState()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunityState(MessagePayloadCommunityState other)
		: this()
	{
		name_ = other.name_;
		defaultChannelId_ = ((other.defaultChannelId_ != null) ? other.defaultChannelId_.Clone() : null);
		ownerUserId_ = ((other.ownerUserId_ != null) ? other.ownerUserId_.Clone() : null);
		defaultChannelName_ = other.defaultChannelName_;
		ownerUsername_ = other.ownerUsername_;
		pictureHex_ = other.pictureHex_;
		PictureAssetUri = other.PictureAssetUri;
		rejectUnverifiedEmail_ = other.rejectUnverifiedEmail_;
		joinThrottle_ = ((other.joinThrottle_ != null) ? other.joinThrottle_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunityState Clone()
	{
		return new MessagePayloadCommunityState(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePayloadCommunityState);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePayloadCommunityState other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (!object.Equals(DefaultChannelId, other.DefaultChannelId))
		{
			return false;
		}
		if (!object.Equals(OwnerUserId, other.OwnerUserId))
		{
			return false;
		}
		if (DefaultChannelName != other.DefaultChannelName)
		{
			return false;
		}
		if (OwnerUsername != other.OwnerUsername)
		{
			return false;
		}
		if (PictureHex != other.PictureHex)
		{
			return false;
		}
		if (PictureAssetUri != other.PictureAssetUri)
		{
			return false;
		}
		if (RejectUnverifiedEmail != other.RejectUnverifiedEmail)
		{
			return false;
		}
		if (!object.Equals(JoinThrottle, other.JoinThrottle))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (defaultChannelId_ != null)
		{
			num ^= DefaultChannelId.GetHashCode();
		}
		if (ownerUserId_ != null)
		{
			num ^= OwnerUserId.GetHashCode();
		}
		if (DefaultChannelName.Length != 0)
		{
			num ^= DefaultChannelName.GetHashCode();
		}
		if (OwnerUsername.Length != 0)
		{
			num ^= OwnerUsername.GetHashCode();
		}
		if (PictureHex.Length != 0)
		{
			num ^= PictureHex.GetHashCode();
		}
		if (pictureAssetUri_ != null)
		{
			num ^= PictureAssetUri.GetHashCode();
		}
		if (RejectUnverifiedEmail)
		{
			num ^= RejectUnverifiedEmail.GetHashCode();
		}
		if (joinThrottle_ != null)
		{
			num ^= JoinThrottle.GetHashCode();
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
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(10);
			P_0.WriteString(Name);
		}
		if (defaultChannelId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(DefaultChannelId);
		}
		if (ownerUserId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(OwnerUserId);
		}
		if (DefaultChannelName.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(DefaultChannelName);
		}
		if (OwnerUsername.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(OwnerUsername);
		}
		if (PictureHex.Length != 0)
		{
			P_0.WriteRawTag(66);
			P_0.WriteString(PictureHex);
		}
		if (pictureAssetUri_ != null)
		{
			_single_pictureAssetUri_codec.WriteTagAndValue(ref P_0, PictureAssetUri);
		}
		if (RejectUnverifiedEmail)
		{
			P_0.WriteRawTag(80);
			P_0.WriteBool(RejectUnverifiedEmail);
		}
		if (joinThrottle_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(JoinThrottle);
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
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (defaultChannelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DefaultChannelId);
		}
		if (ownerUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OwnerUserId);
		}
		if (DefaultChannelName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DefaultChannelName);
		}
		if (OwnerUsername.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(OwnerUsername);
		}
		if (PictureHex.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PictureHex);
		}
		if (pictureAssetUri_ != null)
		{
			num += _single_pictureAssetUri_codec.CalculateSizeWithTag(PictureAssetUri);
		}
		if (RejectUnverifiedEmail)
		{
			num += 2;
		}
		if (joinThrottle_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(JoinThrottle);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessagePayloadCommunityState other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.defaultChannelId_ != null)
		{
			if (defaultChannelId_ == null)
			{
				DefaultChannelId = new ChannelUuid();
			}
			DefaultChannelId.MergeFrom(other.DefaultChannelId);
		}
		if (other.ownerUserId_ != null)
		{
			if (ownerUserId_ == null)
			{
				OwnerUserId = new UserUuid();
			}
			OwnerUserId.MergeFrom(other.OwnerUserId);
		}
		if (other.DefaultChannelName.Length != 0)
		{
			DefaultChannelName = other.DefaultChannelName;
		}
		if (other.OwnerUsername.Length != 0)
		{
			OwnerUsername = other.OwnerUsername;
		}
		if (other.PictureHex.Length != 0)
		{
			PictureHex = other.PictureHex;
		}
		if (other.pictureAssetUri_ != null && (pictureAssetUri_ == null || other.PictureAssetUri != ""))
		{
			PictureAssetUri = other.PictureAssetUri;
		}
		if (other.RejectUnverifiedEmail)
		{
			RejectUnverifiedEmail = other.RejectUnverifiedEmail;
		}
		if (other.joinThrottle_ != null)
		{
			if (joinThrottle_ == null)
			{
				JoinThrottle = new MessagePayloadCommunityJoinThrottle();
			}
			JoinThrottle.MergeFrom(other.JoinThrottle);
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
				Name = P_0.ReadString();
				break;
			case 34u:
				if (defaultChannelId_ == null)
				{
					DefaultChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(DefaultChannelId);
				break;
			case 42u:
				if (ownerUserId_ == null)
				{
					OwnerUserId = new UserUuid();
				}
				P_0.ReadMessage(OwnerUserId);
				break;
			case 50u:
				DefaultChannelName = P_0.ReadString();
				break;
			case 58u:
				OwnerUsername = P_0.ReadString();
				break;
			case 66u:
				PictureHex = P_0.ReadString();
				break;
			case 74u:
			{
				string text = _single_pictureAssetUri_codec.Read(ref P_0);
				if (pictureAssetUri_ == null || text != "")
				{
					PictureAssetUri = text;
				}
				break;
			}
			case 80u:
				RejectUnverifiedEmail = P_0.ReadBool();
				break;
			case 90u:
				if (joinThrottle_ == null)
				{
					JoinThrottle = new MessagePayloadCommunityJoinThrottle();
				}
				P_0.ReadMessage(JoinThrottle);
				break;
			}
		}
	}
}
