using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadMemberInvited : IMessage, IMessage<CommunityLogPayloadMemberInvited>, IEquatable<CommunityLogPayloadMemberInvited>, IDeepCloneable<CommunityLogPayloadMemberInvited>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadMemberInvited> _parser = new MessageParser<CommunityLogPayloadMemberInvited>(() => new CommunityLogPayloadMemberInvited());

	private UnknownFieldSet _unknownFields;

	private CommunityLogAction communityLogAction_ = CommunityLogAction.Unspecified;

	private UserUuid userId_;

	private string username_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadMemberInvited> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[14];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityLogAction CommunityLogAction
	{
		get
		{
			return communityLogAction_;
		}
		set
		{
			communityLogAction_ = value;
		}
	}

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

	public CommunityLogPayloadItem ToPayloadItem()
	{
		return new CommunityLogPayloadItem
		{
			MemberInvited = this
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberInvited()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberInvited(CommunityLogPayloadMemberInvited other)
		: this()
	{
		communityLogAction_ = other.communityLogAction_;
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		username_ = other.username_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberInvited Clone()
	{
		return new CommunityLogPayloadMemberInvited(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadMemberInvited);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadMemberInvited other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (CommunityLogAction != other.CommunityLogAction)
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (Username != other.Username)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (CommunityLogAction != CommunityLogAction.Unspecified)
		{
			num ^= CommunityLogAction.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (Username.Length != 0)
		{
			num ^= Username.GetHashCode();
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
		if (CommunityLogAction != CommunityLogAction.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)CommunityLogAction);
		}
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
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (CommunityLogAction != CommunityLogAction.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)CommunityLogAction);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (Username.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Username);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadMemberInvited other)
	{
		if (other == null)
		{
			return;
		}
		if (other.CommunityLogAction != CommunityLogAction.Unspecified)
		{
			CommunityLogAction = other.CommunityLogAction;
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
			case 8u:
				CommunityLogAction = (CommunityLogAction)P_0.ReadEnum();
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
			}
		}
	}
}
