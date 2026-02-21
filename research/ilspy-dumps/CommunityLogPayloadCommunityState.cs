using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadCommunityState : IMessage, IMessage<CommunityLogPayloadCommunityState>, IEquatable<CommunityLogPayloadCommunityState>, IDeepCloneable<CommunityLogPayloadCommunityState>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadCommunityState> _parser = new MessageParser<CommunityLogPayloadCommunityState>(() => new CommunityLogPayloadCommunityState());

	private UnknownFieldSet _unknownFields;

	private string name_ = "";

	private string pictureAssetUri_ = "";

	private string pictureHex_ = "";

	private ChannelUuid defaultChannelId_;

	private UserUuid ownerUserId_;

	private string defaultChannelName_ = "";

	private string ownerUsername_ = "";

	private bool rejectUnverifiedEmail_;

	private CommunityLogPayloadJoinThrottle joinThrottle_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadCommunityState> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[18];

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
	public string PictureAssetUri
	{
		get
		{
			return pictureAssetUri_;
		}
		set
		{
			pictureAssetUri_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public CommunityLogPayloadJoinThrottle JoinThrottle
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
	public CommunityLogPayloadCommunityState()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadCommunityState(CommunityLogPayloadCommunityState other)
		: this()
	{
		name_ = other.name_;
		pictureAssetUri_ = other.pictureAssetUri_;
		pictureHex_ = other.pictureHex_;
		defaultChannelId_ = ((other.defaultChannelId_ != null) ? other.defaultChannelId_.Clone() : null);
		ownerUserId_ = ((other.ownerUserId_ != null) ? other.ownerUserId_.Clone() : null);
		defaultChannelName_ = other.defaultChannelName_;
		ownerUsername_ = other.ownerUsername_;
		rejectUnverifiedEmail_ = other.rejectUnverifiedEmail_;
		joinThrottle_ = ((other.joinThrottle_ != null) ? other.joinThrottle_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadCommunityState Clone()
	{
		return new CommunityLogPayloadCommunityState(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadCommunityState);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadCommunityState other)
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
		if (PictureAssetUri != other.PictureAssetUri)
		{
			return false;
		}
		if (PictureHex != other.PictureHex)
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
		if (PictureAssetUri.Length != 0)
		{
			num ^= PictureAssetUri.GetHashCode();
		}
		if (PictureHex.Length != 0)
		{
			num ^= PictureHex.GetHashCode();
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
		if (PictureAssetUri.Length != 0)
		{
			P_0.WriteRawTag(18);
			P_0.WriteString(PictureAssetUri);
		}
		if (PictureHex.Length != 0)
		{
			P_0.WriteRawTag(26);
			P_0.WriteString(PictureHex);
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
		if (RejectUnverifiedEmail)
		{
			P_0.WriteRawTag(64);
			P_0.WriteBool(RejectUnverifiedEmail);
		}
		if (joinThrottle_ != null)
		{
			P_0.WriteRawTag(74);
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
		if (PictureAssetUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PictureAssetUri);
		}
		if (PictureHex.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PictureHex);
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
	public void MergeFrom(CommunityLogPayloadCommunityState other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.PictureAssetUri.Length != 0)
		{
			PictureAssetUri = other.PictureAssetUri;
		}
		if (other.PictureHex.Length != 0)
		{
			PictureHex = other.PictureHex;
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
		if (other.RejectUnverifiedEmail)
		{
			RejectUnverifiedEmail = other.RejectUnverifiedEmail;
		}
		if (other.joinThrottle_ != null)
		{
			if (joinThrottle_ == null)
			{
				JoinThrottle = new CommunityLogPayloadJoinThrottle();
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
			case 18u:
				PictureAssetUri = P_0.ReadString();
				break;
			case 26u:
				PictureHex = P_0.ReadString();
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
			case 64u:
				RejectUnverifiedEmail = P_0.ReadBool();
				break;
			case 74u:
				if (joinThrottle_ == null)
				{
					JoinThrottle = new CommunityLogPayloadJoinThrottle();
				}
				P_0.ReadMessage(JoinThrottle);
				break;
			}
		}
	}
}
