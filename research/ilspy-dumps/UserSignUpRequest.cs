using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class UserSignUpRequest : IMessage<UserSignUpRequest>, IMessage, IEquatable<UserSignUpRequest>, IDeepCloneable<UserSignUpRequest>, IBufferMessage
{
	private static readonly MessageParser<UserSignUpRequest> _parser = new MessageParser<UserSignUpRequest>(() => new UserSignUpRequest());

	private UnknownFieldSet _unknownFields;

	private string username_ = "";

	private string password_ = "";

	private string email_ = "";

	private static readonly FieldCodec<string> _single_accessToken_codec = FieldCodec.ForClassWrapper<string>(106u);

	private string accessToken_;

	private string turnstileToken_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserSignUpRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[19];

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
	public string AccessToken
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
	public UserSignUpRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserSignUpRequest(UserSignUpRequest other)
		: this()
	{
		username_ = other.username_;
		password_ = other.password_;
		email_ = other.email_;
		AccessToken = other.AccessToken;
		turnstileToken_ = other.turnstileToken_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserSignUpRequest Clone()
	{
		return new UserSignUpRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserSignUpRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserSignUpRequest other)
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
		if (Email != other.Email)
		{
			return false;
		}
		if (AccessToken != other.AccessToken)
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
		if (Email.Length != 0)
		{
			num ^= Email.GetHashCode();
		}
		if (accessToken_ != null)
		{
			num ^= AccessToken.GetHashCode();
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
		if (Email.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Email);
		}
		if (accessToken_ != null)
		{
			_single_accessToken_codec.WriteTagAndValue(ref P_0, AccessToken);
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
		if (Email.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Email);
		}
		if (accessToken_ != null)
		{
			num += _single_accessToken_codec.CalculateSizeWithTag(AccessToken);
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
	public void MergeFrom(UserSignUpRequest other)
	{
		if (other != null)
		{
			if (other.Username.Length != 0)
			{
				Username = other.Username;
			}
			if (other.Password.Length != 0)
			{
				Password = other.Password;
			}
			if (other.Email.Length != 0)
			{
				Email = other.Email;
			}
			if (other.accessToken_ != null && (accessToken_ == null || other.AccessToken != ""))
			{
				AccessToken = other.AccessToken;
			}
			if (other.TurnstileToken.Length != 0)
			{
				TurnstileToken = other.TurnstileToken;
			}
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
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
				Email = P_0.ReadString();
				break;
			case 106u:
			{
				string text = _single_accessToken_codec.Read(ref P_0);
				if (accessToken_ == null || text != "")
				{
					AccessToken = text;
				}
				break;
			}
			case 114u:
				TurnstileToken = P_0.ReadString();
				break;
			}
		}
	}
}
