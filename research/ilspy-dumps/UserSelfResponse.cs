using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class UserSelfResponse : IMessage<UserSelfResponse>, IMessage, IEquatable<UserSelfResponse>, IDeepCloneable<UserSelfResponse>, IBufferMessage
{
	private static readonly MessageParser<UserSelfResponse> _parser = new MessageParser<UserSelfResponse>(() => new UserSelfResponse());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private string username_ = "";

	private string email_ = "";

	private static readonly FieldCodec<string> _single_profilePictureAssetUri_codec = FieldCodec.ForClassWrapper<string>(106u);

	private string profilePictureAssetUri_;

	private bool isEmailVerified_;

	private UserOnlineStatus maxOnlineStatus_ = UserOnlineStatus.Unspecified;

	private static readonly FieldCodec<UserUuid> _repeated_blockUserIds_codec = FieldCodec.ForMessage(130u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> blockUserIds_ = new RepeatedField<UserUuid>();

	private static readonly FieldCodec<UserBadge> _repeated_userBadges_codec = FieldCodec.ForMessage(138u, UserBadge.Parser);

	private readonly RepeatedField<UserBadge> userBadges_ = new RepeatedField<UserBadge>();

	private UserDirectMessageInviteRequirement directMessageInviteRequirement_;

	private UserFriendshipInviteRequirement friendshipInviteRequirement_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserSelfResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[0];

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
	public string Email
	{
		get
		{
			return email_;
		}
		set
		{
			email_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public bool IsEmailVerified
	{
		get
		{
			return isEmailVerified_;
		}
		set
		{
			isEmailVerified_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserOnlineStatus MaxOnlineStatus
	{
		get
		{
			return maxOnlineStatus_;
		}
		set
		{
			maxOnlineStatus_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserUuid> BlockUserIds => blockUserIds_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserBadge> UserBadges => userBadges_;

	[GeneratedCode("protoc", null)]
	public UserDirectMessageInviteRequirement DirectMessageInviteRequirement
	{
		get
		{
			return directMessageInviteRequirement_;
		}
		set
		{
			directMessageInviteRequirement_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserFriendshipInviteRequirement FriendshipInviteRequirement
	{
		get
		{
			return friendshipInviteRequirement_;
		}
		set
		{
			friendshipInviteRequirement_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSelfResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserSelfResponse(UserSelfResponse other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		username_ = other.username_;
		email_ = other.email_;
		ProfilePictureAssetUri = other.ProfilePictureAssetUri;
		isEmailVerified_ = other.isEmailVerified_;
		maxOnlineStatus_ = other.maxOnlineStatus_;
		blockUserIds_ = other.blockUserIds_.Clone();
		userBadges_ = other.userBadges_.Clone();
		directMessageInviteRequirement_ = ((other.directMessageInviteRequirement_ != null) ? other.directMessageInviteRequirement_.Clone() : null);
		friendshipInviteRequirement_ = ((other.friendshipInviteRequirement_ != null) ? other.friendshipInviteRequirement_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserSelfResponse Clone()
	{
		return new UserSelfResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserSelfResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserSelfResponse other)
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
		if (Username != other.Username)
		{
			return false;
		}
		if (Email != other.Email)
		{
			return false;
		}
		if (ProfilePictureAssetUri != other.ProfilePictureAssetUri)
		{
			return false;
		}
		if (IsEmailVerified != other.IsEmailVerified)
		{
			return false;
		}
		if (MaxOnlineStatus != other.MaxOnlineStatus)
		{
			return false;
		}
		if (!blockUserIds_.Equals(other.blockUserIds_))
		{
			return false;
		}
		if (!userBadges_.Equals(other.userBadges_))
		{
			return false;
		}
		if (!object.Equals(DirectMessageInviteRequirement, other.DirectMessageInviteRequirement))
		{
			return false;
		}
		if (!object.Equals(FriendshipInviteRequirement, other.FriendshipInviteRequirement))
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
		if (Username.Length != 0)
		{
			num ^= Username.GetHashCode();
		}
		if (Email.Length != 0)
		{
			num ^= Email.GetHashCode();
		}
		if (profilePictureAssetUri_ != null)
		{
			num ^= ProfilePictureAssetUri.GetHashCode();
		}
		if (IsEmailVerified)
		{
			num ^= IsEmailVerified.GetHashCode();
		}
		if (MaxOnlineStatus != UserOnlineStatus.Unspecified)
		{
			num ^= MaxOnlineStatus.GetHashCode();
		}
		num ^= blockUserIds_.GetHashCode();
		num ^= userBadges_.GetHashCode();
		if (directMessageInviteRequirement_ != null)
		{
			num ^= DirectMessageInviteRequirement.GetHashCode();
		}
		if (friendshipInviteRequirement_ != null)
		{
			num ^= FriendshipInviteRequirement.GetHashCode();
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
		if (Username.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(Username);
		}
		if (Email.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Email);
		}
		if (profilePictureAssetUri_ != null)
		{
			_single_profilePictureAssetUri_codec.WriteTagAndValue(ref P_0, ProfilePictureAssetUri);
		}
		if (IsEmailVerified)
		{
			P_0.WriteRawTag(112);
			P_0.WriteBool(IsEmailVerified);
		}
		if (MaxOnlineStatus != UserOnlineStatus.Unspecified)
		{
			P_0.WriteRawTag(120);
			P_0.WriteEnum((int)MaxOnlineStatus);
		}
		blockUserIds_.WriteTo(ref P_0, _repeated_blockUserIds_codec);
		userBadges_.WriteTo(ref P_0, _repeated_userBadges_codec);
		if (directMessageInviteRequirement_ != null)
		{
			P_0.WriteRawTag(146, 1);
			P_0.WriteMessage(DirectMessageInviteRequirement);
		}
		if (friendshipInviteRequirement_ != null)
		{
			P_0.WriteRawTag(154, 1);
			P_0.WriteMessage(FriendshipInviteRequirement);
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
		if (Username.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Username);
		}
		if (Email.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Email);
		}
		if (profilePictureAssetUri_ != null)
		{
			num += _single_profilePictureAssetUri_codec.CalculateSizeWithTag(ProfilePictureAssetUri);
		}
		if (IsEmailVerified)
		{
			num += 2;
		}
		if (MaxOnlineStatus != UserOnlineStatus.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MaxOnlineStatus);
		}
		num += blockUserIds_.CalculateSize(_repeated_blockUserIds_codec);
		num += userBadges_.CalculateSize(_repeated_userBadges_codec);
		if (directMessageInviteRequirement_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectMessageInviteRequirement);
		}
		if (friendshipInviteRequirement_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendshipInviteRequirement);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserSelfResponse other)
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
		if (other.Username.Length != 0)
		{
			Username = other.Username;
		}
		if (other.Email.Length != 0)
		{
			Email = other.Email;
		}
		if (other.profilePictureAssetUri_ != null && (profilePictureAssetUri_ == null || other.ProfilePictureAssetUri != ""))
		{
			ProfilePictureAssetUri = other.ProfilePictureAssetUri;
		}
		if (other.IsEmailVerified)
		{
			IsEmailVerified = other.IsEmailVerified;
		}
		if (other.MaxOnlineStatus != UserOnlineStatus.Unspecified)
		{
			MaxOnlineStatus = other.MaxOnlineStatus;
		}
		blockUserIds_.Add(other.blockUserIds_);
		userBadges_.Add(other.userBadges_);
		if (other.directMessageInviteRequirement_ != null)
		{
			if (directMessageInviteRequirement_ == null)
			{
				DirectMessageInviteRequirement = new UserDirectMessageInviteRequirement();
			}
			DirectMessageInviteRequirement.MergeFrom(other.DirectMessageInviteRequirement);
		}
		if (other.friendshipInviteRequirement_ != null)
		{
			if (friendshipInviteRequirement_ == null)
			{
				FriendshipInviteRequirement = new UserFriendshipInviteRequirement();
			}
			FriendshipInviteRequirement.MergeFrom(other.FriendshipInviteRequirement);
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
				Username = P_0.ReadString();
				break;
			case 98u:
				Email = P_0.ReadString();
				break;
			case 106u:
			{
				string text = _single_profilePictureAssetUri_codec.Read(ref P_0);
				if (profilePictureAssetUri_ == null || text != "")
				{
					ProfilePictureAssetUri = text;
				}
				break;
			}
			case 112u:
				IsEmailVerified = P_0.ReadBool();
				break;
			case 120u:
				MaxOnlineStatus = (UserOnlineStatus)P_0.ReadEnum();
				break;
			case 130u:
				blockUserIds_.AddEntriesFrom(ref P_0, _repeated_blockUserIds_codec);
				break;
			case 138u:
				userBadges_.AddEntriesFrom(ref P_0, _repeated_userBadges_codec);
				break;
			case 146u:
				if (directMessageInviteRequirement_ == null)
				{
					DirectMessageInviteRequirement = new UserDirectMessageInviteRequirement();
				}
				P_0.ReadMessage(DirectMessageInviteRequirement);
				break;
			case 154u:
				if (friendshipInviteRequirement_ == null)
				{
					FriendshipInviteRequirement = new UserFriendshipInviteRequirement();
				}
				P_0.ReadMessage(FriendshipInviteRequirement);
				break;
			}
		}
	}
}
