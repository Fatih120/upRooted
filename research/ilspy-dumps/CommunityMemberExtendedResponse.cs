using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberExtendedResponse : IMessage<CommunityMemberExtendedResponse>, IMessage, IEquatable<CommunityMemberExtendedResponse>, IDeepCloneable<CommunityMemberExtendedResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberExtendedResponse> _parser = new MessageParser<CommunityMemberExtendedResponse>(() => new CommunityMemberExtendedResponse());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private static readonly FieldCodec<string> _single_profilePictureAssetUri_codec = FieldCodec.ForClassWrapper<string>(90u);

	private string profilePictureAssetUri_;

	private string nickname_ = "";

	private static readonly FieldCodec<CommunityRoleUuid> _repeated_communityRoleIds_codec = FieldCodec.ForMessage(106u, CommunityRoleUuid.Parser);

	private readonly RepeatedField<CommunityRoleUuid> communityRoleIds_ = new RepeatedField<CommunityRoleUuid>();

	private static readonly FieldCodec<string> _single_roleColorHex_codec = FieldCodec.ForClassWrapper<string>(114u);

	private string roleColorHex_;

	private CommunityRoleUuid primaryCommunityRoleId_;

	private static readonly FieldCodec<string> _single_primaryCommunityRoleName_codec = FieldCodec.ForClassWrapper<string>(130u);

	private string primaryCommunityRoleName_;

	private Timestamp subscribedAt_;

	private Timestamp joinedAt_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberExtendedResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public string ProfilePictureAssetUri
	{
		get
		{
			return profilePictureAssetUri_;
		}
		set
		{
			profilePictureAssetUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Nickname
	{
		get
		{
			return nickname_;
		}
		set
		{
			nickname_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityRoleUuid> CommunityRoleIds => communityRoleIds_;

	[GeneratedCode("protoc", null)]
	public string RoleColorHex
	{
		get
		{
			return roleColorHex_;
		}
		set
		{
			roleColorHex_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleUuid PrimaryCommunityRoleId
	{
		get
		{
			return primaryCommunityRoleId_;
		}
		set
		{
			primaryCommunityRoleId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string PrimaryCommunityRoleName
	{
		get
		{
			return primaryCommunityRoleName_;
		}
		set
		{
			primaryCommunityRoleName_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp SubscribedAt
	{
		get
		{
			return subscribedAt_;
		}
		set
		{
			subscribedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp JoinedAt
	{
		get
		{
			return joinedAt_;
		}
		set
		{
			joinedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberExtendedResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberExtendedResponse(CommunityMemberExtendedResponse other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		ProfilePictureAssetUri = other.ProfilePictureAssetUri;
		nickname_ = other.nickname_;
		communityRoleIds_ = other.communityRoleIds_.Clone();
		RoleColorHex = other.RoleColorHex;
		primaryCommunityRoleId_ = ((other.primaryCommunityRoleId_ != null) ? other.primaryCommunityRoleId_.Clone() : null);
		PrimaryCommunityRoleName = other.PrimaryCommunityRoleName;
		subscribedAt_ = ((other.subscribedAt_ != null) ? other.subscribedAt_.Clone() : null);
		joinedAt_ = ((other.joinedAt_ != null) ? other.joinedAt_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberExtendedResponse Clone()
	{
		return new CommunityMemberExtendedResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberExtendedResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberExtendedResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (ProfilePictureAssetUri != other.ProfilePictureAssetUri)
		{
			return false;
		}
		if (Nickname != other.Nickname)
		{
			return false;
		}
		if (!communityRoleIds_.Equals(other.communityRoleIds_))
		{
			return false;
		}
		if (RoleColorHex != other.RoleColorHex)
		{
			return false;
		}
		if (!object.Equals(PrimaryCommunityRoleId, other.PrimaryCommunityRoleId))
		{
			return false;
		}
		if (PrimaryCommunityRoleName != other.PrimaryCommunityRoleName)
		{
			return false;
		}
		if (!object.Equals(SubscribedAt, other.SubscribedAt))
		{
			return false;
		}
		if (!object.Equals(JoinedAt, other.JoinedAt))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (profilePictureAssetUri_ != null)
		{
			num ^= ProfilePictureAssetUri.GetHashCode();
		}
		if (Nickname.Length != 0)
		{
			num ^= Nickname.GetHashCode();
		}
		num ^= communityRoleIds_.GetHashCode();
		if (roleColorHex_ != null)
		{
			num ^= RoleColorHex.GetHashCode();
		}
		if (primaryCommunityRoleId_ != null)
		{
			num ^= PrimaryCommunityRoleId.GetHashCode();
		}
		if (primaryCommunityRoleName_ != null)
		{
			num ^= PrimaryCommunityRoleName.GetHashCode();
		}
		if (subscribedAt_ != null)
		{
			num ^= SubscribedAt.GetHashCode();
		}
		if (joinedAt_ != null)
		{
			num ^= JoinedAt.GetHashCode();
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
		if (userId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(UserId);
		}
		if (profilePictureAssetUri_ != null)
		{
			_single_profilePictureAssetUri_codec.WriteTagAndValue(ref P_0, ProfilePictureAssetUri);
		}
		if (Nickname.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Nickname);
		}
		communityRoleIds_.WriteTo(ref P_0, _repeated_communityRoleIds_codec);
		if (roleColorHex_ != null)
		{
			_single_roleColorHex_codec.WriteTagAndValue(ref P_0, RoleColorHex);
		}
		if (primaryCommunityRoleId_ != null)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(PrimaryCommunityRoleId);
		}
		if (primaryCommunityRoleName_ != null)
		{
			_single_primaryCommunityRoleName_codec.WriteTagAndValue(ref P_0, PrimaryCommunityRoleName);
		}
		if (subscribedAt_ != null)
		{
			P_0.WriteRawTag(138, 1);
			P_0.WriteMessage(SubscribedAt);
		}
		if (joinedAt_ != null)
		{
			P_0.WriteRawTag(146, 1);
			P_0.WriteMessage(JoinedAt);
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
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (profilePictureAssetUri_ != null)
		{
			num += _single_profilePictureAssetUri_codec.CalculateSizeWithTag(ProfilePictureAssetUri);
		}
		if (Nickname.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Nickname);
		}
		num += communityRoleIds_.CalculateSize(_repeated_communityRoleIds_codec);
		if (roleColorHex_ != null)
		{
			num += _single_roleColorHex_codec.CalculateSizeWithTag(RoleColorHex);
		}
		if (primaryCommunityRoleId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(PrimaryCommunityRoleId);
		}
		if (primaryCommunityRoleName_ != null)
		{
			num += _single_primaryCommunityRoleName_codec.CalculateSizeWithTag(PrimaryCommunityRoleName);
		}
		if (subscribedAt_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SubscribedAt);
		}
		if (joinedAt_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(JoinedAt);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberExtendedResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.profilePictureAssetUri_ != null && (profilePictureAssetUri_ == null || other.ProfilePictureAssetUri != ""))
		{
			ProfilePictureAssetUri = other.ProfilePictureAssetUri;
		}
		if (other.Nickname.Length != 0)
		{
			Nickname = other.Nickname;
		}
		communityRoleIds_.Add(other.communityRoleIds_);
		if (other.roleColorHex_ != null && (roleColorHex_ == null || other.RoleColorHex != ""))
		{
			RoleColorHex = other.RoleColorHex;
		}
		if (other.primaryCommunityRoleId_ != null)
		{
			if (primaryCommunityRoleId_ == null)
			{
				PrimaryCommunityRoleId = new CommunityRoleUuid();
			}
			PrimaryCommunityRoleId.MergeFrom(other.PrimaryCommunityRoleId);
		}
		if (other.primaryCommunityRoleName_ != null && (primaryCommunityRoleName_ == null || other.PrimaryCommunityRoleName != ""))
		{
			PrimaryCommunityRoleName = other.PrimaryCommunityRoleName;
		}
		if (other.subscribedAt_ != null)
		{
			if (subscribedAt_ == null)
			{
				SubscribedAt = new Timestamp();
			}
			SubscribedAt.MergeFrom(other.SubscribedAt);
		}
		if (other.joinedAt_ != null)
		{
			if (joinedAt_ == null)
			{
				JoinedAt = new Timestamp();
			}
			JoinedAt.MergeFrom(other.JoinedAt);
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
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 90u:
			{
				string text = _single_profilePictureAssetUri_codec.Read(ref P_0);
				if (profilePictureAssetUri_ == null || text != "")
				{
					ProfilePictureAssetUri = text;
				}
				break;
			}
			case 98u:
				Nickname = P_0.ReadString();
				break;
			case 106u:
				communityRoleIds_.AddEntriesFrom(ref P_0, _repeated_communityRoleIds_codec);
				break;
			case 114u:
			{
				string text2 = _single_roleColorHex_codec.Read(ref P_0);
				if (roleColorHex_ == null || text2 != "")
				{
					RoleColorHex = text2;
				}
				break;
			}
			case 122u:
				if (primaryCommunityRoleId_ == null)
				{
					PrimaryCommunityRoleId = new CommunityRoleUuid();
				}
				P_0.ReadMessage(PrimaryCommunityRoleId);
				break;
			case 130u:
			{
				string text3 = _single_primaryCommunityRoleName_codec.Read(ref P_0);
				if (primaryCommunityRoleName_ == null || text3 != "")
				{
					PrimaryCommunityRoleName = text3;
				}
				break;
			}
			case 138u:
				if (subscribedAt_ == null)
				{
					SubscribedAt = new Timestamp();
				}
				P_0.ReadMessage(SubscribedAt);
				break;
			case 146u:
				if (joinedAt_ == null)
				{
					JoinedAt = new Timestamp();
				}
				P_0.ReadMessage(JoinedAt);
				break;
			}
		}
	}
}
