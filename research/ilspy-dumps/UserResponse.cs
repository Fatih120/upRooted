using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class UserResponse : IMessage<UserResponse>, IMessage, IEquatable<UserResponse>, IDeepCloneable<UserResponse>, IBufferMessage
{
	private static readonly MessageParser<UserResponse> _parser = new MessageParser<UserResponse>(() => new UserResponse());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private static readonly FieldCodec<string> _single_profilePictureAssetUri_codec = FieldCodec.ForClassWrapper<string>(90u);

	private string profilePictureAssetUri_;

	private string username_ = "";

	private UserOnlineStatus onlineStatus_ = UserOnlineStatus.Unspecified;

	private bool isDeleted_;

	private static readonly FieldCodec<UserBadge> _repeated_badges_codec = FieldCodec.ForMessage(122u, UserBadge.Parser);

	private readonly RepeatedField<UserBadge> badges_ = new RepeatedField<UserBadge>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[15];

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
	public UserOnlineStatus OnlineStatus
	{
		get
		{
			return onlineStatus_;
		}
		set
		{
			onlineStatus_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsDeleted
	{
		get
		{
			return isDeleted_;
		}
		set
		{
			isDeleted_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserBadge> Badges => badges_;

	[GeneratedCode("protoc", null)]
	public UserResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserResponse(UserResponse other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		ProfilePictureAssetUri = other.ProfilePictureAssetUri;
		username_ = other.username_;
		onlineStatus_ = other.onlineStatus_;
		isDeleted_ = other.isDeleted_;
		badges_ = other.badges_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserResponse Clone()
	{
		return new UserResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserResponse other)
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
		if (Username != other.Username)
		{
			return false;
		}
		if (OnlineStatus != other.OnlineStatus)
		{
			return false;
		}
		if (IsDeleted != other.IsDeleted)
		{
			return false;
		}
		if (!badges_.Equals(other.badges_))
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
		if (Username.Length != 0)
		{
			num ^= Username.GetHashCode();
		}
		if (OnlineStatus != UserOnlineStatus.Unspecified)
		{
			num ^= OnlineStatus.GetHashCode();
		}
		if (IsDeleted)
		{
			num ^= IsDeleted.GetHashCode();
		}
		num ^= badges_.GetHashCode();
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
		if (Username.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Username);
		}
		if (OnlineStatus != UserOnlineStatus.Unspecified)
		{
			P_0.WriteRawTag(104);
			P_0.WriteEnum((int)OnlineStatus);
		}
		if (IsDeleted)
		{
			P_0.WriteRawTag(112);
			P_0.WriteBool(IsDeleted);
		}
		badges_.WriteTo(ref P_0, _repeated_badges_codec);
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
		if (Username.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Username);
		}
		if (OnlineStatus != UserOnlineStatus.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)OnlineStatus);
		}
		if (IsDeleted)
		{
			num += 2;
		}
		num += badges_.CalculateSize(_repeated_badges_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserResponse other)
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
		if (other.Username.Length != 0)
		{
			Username = other.Username;
		}
		if (other.OnlineStatus != UserOnlineStatus.Unspecified)
		{
			OnlineStatus = other.OnlineStatus;
		}
		if (other.IsDeleted)
		{
			IsDeleted = other.IsDeleted;
		}
		badges_.Add(other.badges_);
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
				Username = P_0.ReadString();
				break;
			case 104u:
				OnlineStatus = (UserOnlineStatus)P_0.ReadEnum();
				break;
			case 112u:
				IsDeleted = P_0.ReadBool();
				break;
			case 122u:
				badges_.AddEntriesFrom(ref P_0, _repeated_badges_codec);
				break;
			}
		}
	}
}
