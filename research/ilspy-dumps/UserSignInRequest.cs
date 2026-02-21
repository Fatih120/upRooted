using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class UserSignInRequest : IMessage<UserSignInRequest>, IMessage, IEquatable<UserSignInRequest>, IDeepCloneable<UserSignInRequest>, IBufferMessage
{
	private static readonly MessageParser<UserSignInRequest> _parser = new MessageParser<UserSignInRequest>(() => new UserSignInRequest());

	private UnknownFieldSet _unknownFields;

	private string username_ = "";

	private string password_ = "";

	private DeviceUuid deviceId_;

	private UserDeviceDescription userDeviceDescription_;

	private string turnstileToken_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserSignInRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[22];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public string Password
	{
		get
		{
			return password_;
		}
		set
		{
			password_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public DeviceUuid DeviceId
	{
		get
		{
			return deviceId_;
		}
		set
		{
			deviceId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserDeviceDescription UserDeviceDescription
	{
		get
		{
			return userDeviceDescription_;
		}
		set
		{
			userDeviceDescription_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string TurnstileToken
	{
		get
		{
			return turnstileToken_;
		}
		set
		{
			turnstileToken_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSignInRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserSignInRequest(UserSignInRequest other)
		: this()
	{
		username_ = other.username_;
		password_ = other.password_;
		deviceId_ = ((other.deviceId_ != null) ? other.deviceId_.Clone() : null);
		userDeviceDescription_ = ((other.userDeviceDescription_ != null) ? other.userDeviceDescription_.Clone() : null);
		turnstileToken_ = other.turnstileToken_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserSignInRequest Clone()
	{
		return new UserSignInRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserSignInRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserSignInRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Username != other.Username)
		{
			return false;
		}
		if (Password != other.Password)
		{
			return false;
		}
		if (!object.Equals(DeviceId, other.DeviceId))
		{
			return false;
		}
		if (!object.Equals(UserDeviceDescription, other.UserDeviceDescription))
		{
			return false;
		}
		if (TurnstileToken != other.TurnstileToken)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Username.Length != 0)
		{
			num ^= Username.GetHashCode();
		}
		if (Password.Length != 0)
		{
			num ^= Password.GetHashCode();
		}
		if (deviceId_ != null)
		{
			num ^= DeviceId.GetHashCode();
		}
		if (userDeviceDescription_ != null)
		{
			num ^= UserDeviceDescription.GetHashCode();
		}
		if (TurnstileToken.Length != 0)
		{
			num ^= TurnstileToken.GetHashCode();
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
		if (Username.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(Username);
		}
		if (Password.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(Password);
		}
		if (deviceId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(DeviceId);
		}
		if (userDeviceDescription_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(UserDeviceDescription);
		}
		if (TurnstileToken.Length != 0)
		{
			P_0.WriteRawTag(114);
			P_0.WriteString(TurnstileToken);
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
		if (Username.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Username);
		}
		if (Password.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Password);
		}
		if (deviceId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DeviceId);
		}
		if (userDeviceDescription_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserDeviceDescription);
		}
		if (TurnstileToken.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(TurnstileToken);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserSignInRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Username.Length != 0)
		{
			Username = other.Username;
		}
		if (other.Password.Length != 0)
		{
			Password = other.Password;
		}
		if (other.deviceId_ != null)
		{
			if (deviceId_ == null)
			{
				DeviceId = new DeviceUuid();
			}
			DeviceId.MergeFrom(other.DeviceId);
		}
		if (other.userDeviceDescription_ != null)
		{
			if (userDeviceDescription_ == null)
			{
				UserDeviceDescription = new UserDeviceDescription();
			}
			UserDeviceDescription.MergeFrom(other.UserDeviceDescription);
		}
		if (other.TurnstileToken.Length != 0)
		{
			TurnstileToken = other.TurnstileToken;
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
				Username = P_0.ReadString();
				break;
			case 90u:
				Password = P_0.ReadString();
				break;
			case 98u:
				if (deviceId_ == null)
				{
					DeviceId = new DeviceUuid();
				}
				P_0.ReadMessage(DeviceId);
				break;
			case 106u:
				if (userDeviceDescription_ == null)
				{
					UserDeviceDescription = new UserDeviceDescription();
				}
				P_0.ReadMessage(UserDeviceDescription);
				break;
			case 114u:
				TurnstileToken = P_0.ReadString();
				break;
			}
		}
	}
}
