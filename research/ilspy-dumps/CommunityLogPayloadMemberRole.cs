using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadMemberRole : IMessage, IMessage<CommunityLogPayloadMemberRole>, IEquatable<CommunityLogPayloadMemberRole>, IDeepCloneable<CommunityLogPayloadMemberRole>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadMemberRole> _parser = new MessageParser<CommunityLogPayloadMemberRole>(() => new CommunityLogPayloadMemberRole());

	private UnknownFieldSet _unknownFields;

	private CommunityLogAction communityLogAction_ = CommunityLogAction.Unspecified;

	private CommunityRoleUuid communityRoleId_;

	private string roleName_ = "";

	private static readonly FieldCodec<CommunityLogMemberInfo> _repeated_members_codec = FieldCodec.ForMessage(50u, CommunityLogMemberInfo.Parser);

	private readonly RepeatedField<CommunityLogMemberInfo> members_ = new RepeatedField<CommunityLogMemberInfo>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadMemberRole> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[3];

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
	public CommunityRoleUuid CommunityRoleId
	{
		get
		{
			return communityRoleId_;
		}
		set
		{
			communityRoleId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string RoleName
	{
		get
		{
			return roleName_;
		}
		set
		{
			roleName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityLogMemberInfo> Members => members_;

	public CommunityLogPayloadItem ToPayloadItem()
	{
		return new CommunityLogPayloadItem
		{
			MemberRole = this
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberRole()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberRole(CommunityLogPayloadMemberRole other)
		: this()
	{
		communityLogAction_ = other.communityLogAction_;
		communityRoleId_ = ((other.communityRoleId_ != null) ? other.communityRoleId_.Clone() : null);
		roleName_ = other.roleName_;
		members_ = other.members_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberRole Clone()
	{
		return new CommunityLogPayloadMemberRole(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadMemberRole);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadMemberRole other)
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
		if (!object.Equals(CommunityRoleId, other.CommunityRoleId))
		{
			return false;
		}
		if (RoleName != other.RoleName)
		{
			return false;
		}
		if (!members_.Equals(other.members_))
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
		if (communityRoleId_ != null)
		{
			num ^= CommunityRoleId.GetHashCode();
		}
		if (RoleName.Length != 0)
		{
			num ^= RoleName.GetHashCode();
		}
		num ^= members_.GetHashCode();
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
		if (communityRoleId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityRoleId);
		}
		if (RoleName.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(RoleName);
		}
		members_.WriteTo(ref P_0, _repeated_members_codec);
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
		if (communityRoleId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityRoleId);
		}
		if (RoleName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(RoleName);
		}
		num += members_.CalculateSize(_repeated_members_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadMemberRole other)
	{
		if (other == null)
		{
			return;
		}
		if (other.CommunityLogAction != CommunityLogAction.Unspecified)
		{
			CommunityLogAction = other.CommunityLogAction;
		}
		if (other.communityRoleId_ != null)
		{
			if (communityRoleId_ == null)
			{
				CommunityRoleId = new CommunityRoleUuid();
			}
			CommunityRoleId.MergeFrom(other.CommunityRoleId);
		}
		if (other.RoleName.Length != 0)
		{
			RoleName = other.RoleName;
		}
		members_.Add(other.members_);
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
				if (communityRoleId_ == null)
				{
					CommunityRoleId = new CommunityRoleUuid();
				}
				P_0.ReadMessage(CommunityRoleId);
				break;
			case 42u:
				RoleName = P_0.ReadString();
				break;
			case 50u:
				members_.AddEntriesFrom(ref P_0, _repeated_members_codec);
				break;
			}
		}
	}
}
