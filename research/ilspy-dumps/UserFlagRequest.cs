using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class UserFlagRequest : IMessage<UserFlagRequest>, IMessage, IEquatable<UserFlagRequest>, IDeepCloneable<UserFlagRequest>, IBufferMessage
{
	private static readonly MessageParser<UserFlagRequest> _parser = new MessageParser<UserFlagRequest>(() => new UserFlagRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private UserUuid reportUserId_;

	private ContentFlagReason contentFlagReason_ = ContentFlagReason.Unspecified;

	private UserFlagProperty userFlagProperty_ = UserFlagProperty.Unspecified;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserFlagRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[33];

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
	public UserUuid ReportUserId
	{
		get
		{
			return reportUserId_;
		}
		set
		{
			reportUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ContentFlagReason ContentFlagReason
	{
		get
		{
			return contentFlagReason_;
		}
		set
		{
			contentFlagReason_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserFlagProperty UserFlagProperty
	{
		get
		{
			return userFlagProperty_;
		}
		set
		{
			userFlagProperty_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserFlagRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserFlagRequest(UserFlagRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		reportUserId_ = ((other.reportUserId_ != null) ? other.reportUserId_.Clone() : null);
		contentFlagReason_ = other.contentFlagReason_;
		userFlagProperty_ = other.userFlagProperty_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserFlagRequest Clone()
	{
		return new UserFlagRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserFlagRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserFlagRequest other)
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
		if (!object.Equals(ReportUserId, other.ReportUserId))
		{
			return false;
		}
		if (ContentFlagReason != other.ContentFlagReason)
		{
			return false;
		}
		if (UserFlagProperty != other.UserFlagProperty)
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
		if (reportUserId_ != null)
		{
			num ^= ReportUserId.GetHashCode();
		}
		if (ContentFlagReason != ContentFlagReason.Unspecified)
		{
			num ^= ContentFlagReason.GetHashCode();
		}
		if (UserFlagProperty != UserFlagProperty.Unspecified)
		{
			num ^= UserFlagProperty.GetHashCode();
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
		if (reportUserId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(ReportUserId);
		}
		if (ContentFlagReason != ContentFlagReason.Unspecified)
		{
			P_0.WriteRawTag(48);
			P_0.WriteEnum((int)ContentFlagReason);
		}
		if (UserFlagProperty != UserFlagProperty.Unspecified)
		{
			P_0.WriteRawTag(56);
			P_0.WriteEnum((int)UserFlagProperty);
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
		if (reportUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ReportUserId);
		}
		if (ContentFlagReason != ContentFlagReason.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ContentFlagReason);
		}
		if (UserFlagProperty != UserFlagProperty.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)UserFlagProperty);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserFlagRequest other)
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
		if (other.reportUserId_ != null)
		{
			if (reportUserId_ == null)
			{
				ReportUserId = new UserUuid();
			}
			ReportUserId.MergeFrom(other.ReportUserId);
		}
		if (other.ContentFlagReason != ContentFlagReason.Unspecified)
		{
			ContentFlagReason = other.ContentFlagReason;
		}
		if (other.UserFlagProperty != UserFlagProperty.Unspecified)
		{
			UserFlagProperty = other.UserFlagProperty;
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
			case 42u:
				if (reportUserId_ == null)
				{
					ReportUserId = new UserUuid();
				}
				P_0.ReadMessage(ReportUserId);
				break;
			case 48u:
				ContentFlagReason = (ContentFlagReason)P_0.ReadEnum();
				break;
			case 56u:
				UserFlagProperty = (UserFlagProperty)P_0.ReadEnum();
				break;
			}
		}
	}
}
