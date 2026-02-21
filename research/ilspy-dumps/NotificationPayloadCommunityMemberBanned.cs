using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.Notification;

public sealed class NotificationPayloadCommunityMemberBanned : IMessage, IMessage<NotificationPayloadCommunityMemberBanned>, IEquatable<NotificationPayloadCommunityMemberBanned>, IDeepCloneable<NotificationPayloadCommunityMemberBanned>, IBufferMessage
{
	private static readonly MessageParser<NotificationPayloadCommunityMemberBanned> _parser = new MessageParser<NotificationPayloadCommunityMemberBanned>(() => new NotificationPayloadCommunityMemberBanned());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private UserUuid userId_;

	private string communityName_ = "";

	private static readonly FieldCodec<string> _single_reason_codec = FieldCodec.ForClassWrapper<string>(66u);

	private string reason_;

	private Timestamp expiresAt_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationPayloadCommunityMemberBanned> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationsReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
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
	public string CommunityName
	{
		get
		{
			return communityName_;
		}
		set
		{
			communityName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Reason
	{
		get
		{
			return reason_;
		}
		set
		{
			reason_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp ExpiresAt
	{
		get
		{
			return expiresAt_;
		}
		set
		{
			expiresAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadCommunityMemberBanned()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadCommunityMemberBanned(NotificationPayloadCommunityMemberBanned other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		communityName_ = other.communityName_;
		Reason = other.Reason;
		expiresAt_ = ((other.expiresAt_ != null) ? other.expiresAt_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadCommunityMemberBanned Clone()
	{
		return new NotificationPayloadCommunityMemberBanned(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationPayloadCommunityMemberBanned);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationPayloadCommunityMemberBanned other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (CommunityName != other.CommunityName)
		{
			return false;
		}
		if (Reason != other.Reason)
		{
			return false;
		}
		if (!object.Equals(ExpiresAt, other.ExpiresAt))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (CommunityName.Length != 0)
		{
			num ^= CommunityName.GetHashCode();
		}
		if (reason_ != null)
		{
			num ^= Reason.GetHashCode();
		}
		if (expiresAt_ != null)
		{
			num ^= ExpiresAt.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityId);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(UserId);
		}
		if (CommunityName.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(CommunityName);
		}
		if (reason_ != null)
		{
			_single_reason_codec.WriteTagAndValue(ref P_0, Reason);
		}
		if (expiresAt_ != null)
		{
			P_0.WriteRawTag(74);
			P_0.WriteMessage(ExpiresAt);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (CommunityName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CommunityName);
		}
		if (reason_ != null)
		{
			num += _single_reason_codec.CalculateSizeWithTag(Reason);
		}
		if (expiresAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ExpiresAt);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationPayloadCommunityMemberBanned other)
	{
		if (other == null)
		{
			return;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.CommunityName.Length != 0)
		{
			CommunityName = other.CommunityName;
		}
		if (other.reason_ != null && (reason_ == null || other.Reason != ""))
		{
			Reason = other.Reason;
		}
		if (other.expiresAt_ != null)
		{
			if (expiresAt_ == null)
			{
				ExpiresAt = new Timestamp();
			}
			ExpiresAt.MergeFrom(other.ExpiresAt);
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
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 50u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 58u:
				CommunityName = P_0.ReadString();
				break;
			case 66u:
			{
				string text = _single_reason_codec.Read(ref P_0);
				if (reason_ == null || text != "")
				{
					Reason = text;
				}
				break;
			}
			case 74u:
				if (expiresAt_ == null)
				{
					ExpiresAt = new Timestamp();
				}
				P_0.ReadMessage(ExpiresAt);
				break;
			}
		}
	}
}
