using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Exceptions.Payloads;

namespace RootApp.WebApi.Shared.Exceptions;

public sealed class RootGrpcExceptionPayload : IMessage<RootGrpcExceptionPayload>, IMessage, IEquatable<RootGrpcExceptionPayload>, IDeepCloneable<RootGrpcExceptionPayload>, IBufferMessage
{
	private static readonly MessageParser<RootGrpcExceptionPayload> _parser = new MessageParser<RootGrpcExceptionPayload>(() => new RootGrpcExceptionPayload());

	private UnknownFieldSet _unknownFields;

	private UsernameExceptionPayload username_;

	private EmailExceptionPayload email_;

	private AccessTokenExceptionPayload accessToken_;

	private FriendUserExceptionPayload friendUser_;

	private UploadStatusListExceptionPayload uploadStatusList_;

	private RequestValidatorListExceptionPayload requestValidatorList_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<RootGrpcExceptionPayload> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RootGrpcExceptionReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public UsernameExceptionPayload Username
	{
		get
		{
			return username_;
		}
		set
		{
			username_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public EmailExceptionPayload Email
	{
		get
		{
			return email_;
		}
		set
		{
			email_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AccessTokenExceptionPayload AccessToken
	{
		get
		{
			return accessToken_;
		}
		set
		{
			accessToken_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendUserExceptionPayload FriendUser
	{
		get
		{
			return friendUser_;
		}
		set
		{
			friendUser_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UploadStatusListExceptionPayload UploadStatusList
	{
		get
		{
			return uploadStatusList_;
		}
		set
		{
			uploadStatusList_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RequestValidatorListExceptionPayload RequestValidatorList
	{
		get
		{
			return requestValidatorList_;
		}
		set
		{
			requestValidatorList_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootGrpcExceptionPayload()
	{
	}

	[GeneratedCode("protoc", null)]
	public RootGrpcExceptionPayload(RootGrpcExceptionPayload other)
		: this()
	{
		username_ = ((other.username_ != null) ? other.username_.Clone() : null);
		email_ = ((other.email_ != null) ? other.email_.Clone() : null);
		accessToken_ = ((other.accessToken_ != null) ? other.accessToken_.Clone() : null);
		friendUser_ = ((other.friendUser_ != null) ? other.friendUser_.Clone() : null);
		uploadStatusList_ = ((other.uploadStatusList_ != null) ? other.uploadStatusList_.Clone() : null);
		requestValidatorList_ = ((other.requestValidatorList_ != null) ? other.requestValidatorList_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public RootGrpcExceptionPayload Clone()
	{
		return new RootGrpcExceptionPayload(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RootGrpcExceptionPayload);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(RootGrpcExceptionPayload other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Username, other.Username))
		{
			return false;
		}
		if (!object.Equals(Email, other.Email))
		{
			return false;
		}
		if (!object.Equals(AccessToken, other.AccessToken))
		{
			return false;
		}
		if (!object.Equals(FriendUser, other.FriendUser))
		{
			return false;
		}
		if (!object.Equals(UploadStatusList, other.UploadStatusList))
		{
			return false;
		}
		if (!object.Equals(RequestValidatorList, other.RequestValidatorList))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (username_ != null)
		{
			num ^= Username.GetHashCode();
		}
		if (email_ != null)
		{
			num ^= Email.GetHashCode();
		}
		if (accessToken_ != null)
		{
			num ^= AccessToken.GetHashCode();
		}
		if (friendUser_ != null)
		{
			num ^= FriendUser.GetHashCode();
		}
		if (uploadStatusList_ != null)
		{
			num ^= UploadStatusList.GetHashCode();
		}
		if (requestValidatorList_ != null)
		{
			num ^= RequestValidatorList.GetHashCode();
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
		if (username_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Username);
		}
		if (email_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(Email);
		}
		if (accessToken_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(AccessToken);
		}
		if (friendUser_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(FriendUser);
		}
		if (uploadStatusList_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(UploadStatusList);
		}
		if (requestValidatorList_ != null)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(RequestValidatorList);
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
		if (username_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Username);
		}
		if (email_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Email);
		}
		if (accessToken_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AccessToken);
		}
		if (friendUser_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendUser);
		}
		if (uploadStatusList_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UploadStatusList);
		}
		if (requestValidatorList_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(RequestValidatorList);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(RootGrpcExceptionPayload other)
	{
		if (other == null)
		{
			return;
		}
		if (other.username_ != null)
		{
			if (username_ == null)
			{
				Username = new UsernameExceptionPayload();
			}
			Username.MergeFrom(other.Username);
		}
		if (other.email_ != null)
		{
			if (email_ == null)
			{
				Email = new EmailExceptionPayload();
			}
			Email.MergeFrom(other.Email);
		}
		if (other.accessToken_ != null)
		{
			if (accessToken_ == null)
			{
				AccessToken = new AccessTokenExceptionPayload();
			}
			AccessToken.MergeFrom(other.AccessToken);
		}
		if (other.friendUser_ != null)
		{
			if (friendUser_ == null)
			{
				FriendUser = new FriendUserExceptionPayload();
			}
			FriendUser.MergeFrom(other.FriendUser);
		}
		if (other.uploadStatusList_ != null)
		{
			if (uploadStatusList_ == null)
			{
				UploadStatusList = new UploadStatusListExceptionPayload();
			}
			UploadStatusList.MergeFrom(other.UploadStatusList);
		}
		if (other.requestValidatorList_ != null)
		{
			if (requestValidatorList_ == null)
			{
				RequestValidatorList = new RequestValidatorListExceptionPayload();
			}
			RequestValidatorList.MergeFrom(other.RequestValidatorList);
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
				if (username_ == null)
				{
					Username = new UsernameExceptionPayload();
				}
				P_0.ReadMessage(Username);
				break;
			case 90u:
				if (email_ == null)
				{
					Email = new EmailExceptionPayload();
				}
				P_0.ReadMessage(Email);
				break;
			case 98u:
				if (accessToken_ == null)
				{
					AccessToken = new AccessTokenExceptionPayload();
				}
				P_0.ReadMessage(AccessToken);
				break;
			case 106u:
				if (friendUser_ == null)
				{
					FriendUser = new FriendUserExceptionPayload();
				}
				P_0.ReadMessage(FriendUser);
				break;
			case 114u:
				if (uploadStatusList_ == null)
				{
					UploadStatusList = new UploadStatusListExceptionPayload();
				}
				P_0.ReadMessage(UploadStatusList);
				break;
			case 122u:
				if (requestValidatorList_ == null)
				{
					RequestValidatorList = new RequestValidatorListExceptionPayload();
				}
				P_0.ReadMessage(RequestValidatorList);
				break;
			}
		}
	}
}
