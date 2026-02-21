using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadMemberBanned : IMessage, IMessage<CommunityLogPayloadMemberBanned>, IEquatable<CommunityLogPayloadMemberBanned>, IDeepCloneable<CommunityLogPayloadMemberBanned>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadMemberBanned> _parser = new MessageParser<CommunityLogPayloadMemberBanned>(() => new CommunityLogPayloadMemberBanned());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private string username_ = "";

	private Timestamp expiresAt_;

	private static readonly FieldCodec<string> _single_reason_codec = FieldCodec.ForClassWrapper<string>(58u);

	private string reason_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadMemberBanned> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[11];

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

	public CommunityLogPayloadItem ToPayloadItem()
	{
		return new CommunityLogPayloadItem
		{
			MemberBanned = this
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberBanned()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberBanned(CommunityLogPayloadMemberBanned other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		username_ = other.username_;
		expiresAt_ = ((other.expiresAt_ != null) ? other.expiresAt_.Clone() : null);
		Reason = other.Reason;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberBanned Clone()
	{
		return new CommunityLogPayloadMemberBanned(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadMemberBanned);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadMemberBanned other)
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
		if (!object.Equals(ExpiresAt, other.ExpiresAt))
		{
			return false;
		}
		if (Reason != other.Reason)
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
		if (expiresAt_ != null)
		{
			num ^= ExpiresAt.GetHashCode();
		}
		if (reason_ != null)
		{
			num ^= Reason.GetHashCode();
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
			P_0.WriteRawTag(34);
			P_0.WriteMessage(UserId);
		}
		if (Username.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(Username);
		}
		if (expiresAt_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(ExpiresAt);
		}
		if (reason_ != null)
		{
			_single_reason_codec.WriteTagAndValue(ref P_0, Reason);
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
		if (expiresAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ExpiresAt);
		}
		if (reason_ != null)
		{
			num += _single_reason_codec.CalculateSizeWithTag(Reason);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadMemberBanned other)
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
		if (other.expiresAt_ != null)
		{
			if (expiresAt_ == null)
			{
				ExpiresAt = new Timestamp();
			}
			ExpiresAt.MergeFrom(other.ExpiresAt);
		}
		if (other.reason_ != null && (reason_ == null || other.Reason != ""))
		{
			Reason = other.Reason;
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
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 42u:
				Username = P_0.ReadString();
				break;
			case 50u:
				if (expiresAt_ == null)
				{
					ExpiresAt = new Timestamp();
				}
				P_0.ReadMessage(ExpiresAt);
				break;
			case 58u:
			{
				string text = _single_reason_codec.Read(ref P_0);
				if (reason_ == null || text != "")
				{
					Reason = text;
				}
				break;
			}
			}
		}
	}
}
