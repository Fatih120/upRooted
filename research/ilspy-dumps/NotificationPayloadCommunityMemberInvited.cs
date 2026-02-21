using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.Notification;

public sealed class NotificationPayloadCommunityMemberInvited : IMessage, IMessage<NotificationPayloadCommunityMemberInvited>, IEquatable<NotificationPayloadCommunityMemberInvited>, IDeepCloneable<NotificationPayloadCommunityMemberInvited>, IBufferMessage
{
	private static readonly MessageParser<NotificationPayloadCommunityMemberInvited> _parser = new MessageParser<NotificationPayloadCommunityMemberInvited>(() => new NotificationPayloadCommunityMemberInvited());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private UserUuid userId_;

	private static readonly FieldCodec<CommunityRoleUuid> _repeated_communityRoleIds_codec = FieldCodec.ForMessage(50u, CommunityRoleUuid.Parser);

	private readonly RepeatedField<CommunityRoleUuid> communityRoleIds_ = new RepeatedField<CommunityRoleUuid>();

	private string communityName_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationPayloadCommunityMemberInvited> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationsReflection.Descriptor.MessageTypes[1];

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
	public RepeatedField<CommunityRoleUuid> CommunityRoleIds => communityRoleIds_;

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
	public NotificationPayloadCommunityMemberInvited()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadCommunityMemberInvited(NotificationPayloadCommunityMemberInvited other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		communityRoleIds_ = other.communityRoleIds_.Clone();
		communityName_ = other.communityName_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadCommunityMemberInvited Clone()
	{
		return new NotificationPayloadCommunityMemberInvited(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationPayloadCommunityMemberInvited);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationPayloadCommunityMemberInvited other)
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
		if (!communityRoleIds_.Equals(other.communityRoleIds_))
		{
			return false;
		}
		if (CommunityName != other.CommunityName)
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
		num ^= communityRoleIds_.GetHashCode();
		if (CommunityName.Length != 0)
		{
			num ^= CommunityName.GetHashCode();
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
			P_0.WriteRawTag(42);
			P_0.WriteMessage(UserId);
		}
		communityRoleIds_.WriteTo(ref P_0, _repeated_communityRoleIds_codec);
		if (CommunityName.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(CommunityName);
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
		num += communityRoleIds_.CalculateSize(_repeated_communityRoleIds_codec);
		if (CommunityName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(CommunityName);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationPayloadCommunityMemberInvited other)
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
		communityRoleIds_.Add(other.communityRoleIds_);
		if (other.CommunityName.Length != 0)
		{
			CommunityName = other.CommunityName;
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
			case 42u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 50u:
				communityRoleIds_.AddEntriesFrom(ref P_0, _repeated_communityRoleIds_codec);
				break;
			case 58u:
				CommunityName = P_0.ReadString();
				break;
			}
		}
	}
}
