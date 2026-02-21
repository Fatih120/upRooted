using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class UserResetPasswordRequest : IMessage<UserResetPasswordRequest>, IMessage, IEquatable<UserResetPasswordRequest>, IDeepCloneable<UserResetPasswordRequest>, IBufferMessage
{
	private static readonly MessageParser<UserResetPasswordRequest> _parser = new MessageParser<UserResetPasswordRequest>(() => new UserResetPasswordRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private string email_ = "";

	private string resetToken_ = "";

	private string newPassword_ = "";

	private string turnstileToken_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserResetPasswordRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[11];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootContext Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = value;
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
	public string ResetToken
	{
		get
		{
			return resetToken_;
		}
		set
		{
			resetToken_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string NewPassword
	{
		get
		{
			return newPassword_;
		}
		set
		{
			newPassword_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public UserResetPasswordRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserResetPasswordRequest(UserResetPasswordRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		email_ = other.email_;
		resetToken_ = other.resetToken_;
		newPassword_ = other.newPassword_;
		turnstileToken_ = other.turnstileToken_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserResetPasswordRequest Clone()
	{
		return new UserResetPasswordRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserResetPasswordRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserResetPasswordRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Context, other.Context))
		{
			return false;
		}
		if (Email != other.Email)
		{
			return false;
		}
		if (ResetToken != other.ResetToken)
		{
			return false;
		}
		if (NewPassword != other.NewPassword)
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
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
		if (Email.Length != 0)
		{
			num ^= Email.GetHashCode();
		}
		if (ResetToken.Length != 0)
		{
			num ^= ResetToken.GetHashCode();
		}
		if (NewPassword.Length != 0)
		{
			num ^= NewPassword.GetHashCode();
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
		if (Email.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(Email);
		}
		if (ResetToken.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(ResetToken);
		}
		if (NewPassword.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(NewPassword);
		}
		if (TurnstileToken.Length != 0)
		{
			P_0.WriteRawTag(106);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
		if (Email.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Email);
		}
		if (ResetToken.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ResetToken);
		}
		if (NewPassword.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(NewPassword);
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
	public void MergeFrom(UserResetPasswordRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.context_ != null)
		{
			if (context_ == null)
			{
				Context = new RootContext();
			}
			Context.MergeFrom(other.Context);
		}
		if (other.Email.Length != 0)
		{
			Email = other.Email;
		}
		if (other.ResetToken.Length != 0)
		{
			ResetToken = other.ResetToken;
		}
		if (other.NewPassword.Length != 0)
		{
			NewPassword = other.NewPassword;
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
			case 10u:
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
				break;
			case 82u:
				Email = P_0.ReadString();
				break;
			case 90u:
				ResetToken = P_0.ReadString();
				break;
			case 98u:
				NewPassword = P_0.ReadString();
				break;
			case 106u:
				TurnstileToken = P_0.ReadString();
				break;
			}
		}
	}
}
