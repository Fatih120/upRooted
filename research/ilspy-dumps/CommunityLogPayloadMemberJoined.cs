using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadMemberJoined : IMessage, IMessage<CommunityLogPayloadMemberJoined>, IEquatable<CommunityLogPayloadMemberJoined>, IDeepCloneable<CommunityLogPayloadMemberJoined>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadMemberJoined> _parser = new MessageParser<CommunityLogPayloadMemberJoined>(() => new CommunityLogPayloadMemberJoined());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private string username_ = "";

	private UserUuid senderUserId_;

	private static readonly FieldCodec<string> _single_senderUsername_codec = FieldCodec.ForClassWrapper<string>(58u);

	private string senderUsername_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadMemberJoined> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[15];

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
	public UserUuid SenderUserId
	{
		get
		{
			return senderUserId_;
		}
		set
		{
			senderUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string SenderUsername
	{
		get
		{
			return senderUsername_;
		}
		set
		{
			senderUsername_ = value;
		}
	}

	public CommunityLogPayloadItem ToPayloadItem()
	{
		return new CommunityLogPayloadItem
		{
			MemberJoined = this
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberJoined()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberJoined(CommunityLogPayloadMemberJoined other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		username_ = other.username_;
		senderUserId_ = ((other.senderUserId_ != null) ? other.senderUserId_.Clone() : null);
		SenderUsername = other.SenderUsername;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberJoined Clone()
	{
		return new CommunityLogPayloadMemberJoined(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadMemberJoined);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadMemberJoined other)
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
		if (!object.Equals(SenderUserId, other.SenderUserId))
		{
			return false;
		}
		if (SenderUsername != other.SenderUsername)
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
		if (senderUserId_ != null)
		{
			num ^= SenderUserId.GetHashCode();
		}
		if (senderUsername_ != null)
		{
			num ^= SenderUsername.GetHashCode();
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
		if (senderUserId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(SenderUserId);
		}
		if (senderUsername_ != null)
		{
			_single_senderUsername_codec.WriteTagAndValue(ref P_0, SenderUsername);
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
		if (senderUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SenderUserId);
		}
		if (senderUsername_ != null)
		{
			num += _single_senderUsername_codec.CalculateSizeWithTag(SenderUsername);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadMemberJoined other)
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
		if (other.senderUserId_ != null)
		{
			if (senderUserId_ == null)
			{
				SenderUserId = new UserUuid();
			}
			SenderUserId.MergeFrom(other.SenderUserId);
		}
		if (other.senderUsername_ != null && (senderUsername_ == null || other.SenderUsername != ""))
		{
			SenderUsername = other.SenderUsername;
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
				if (senderUserId_ == null)
				{
					SenderUserId = new UserUuid();
				}
				P_0.ReadMessage(SenderUserId);
				break;
			case 58u:
			{
				string text = _single_senderUsername_codec.Read(ref P_0);
				if (senderUsername_ == null || text != "")
				{
					SenderUsername = text;
				}
				break;
			}
			}
		}
	}
}
