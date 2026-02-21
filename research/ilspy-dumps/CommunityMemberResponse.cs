using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberResponse : IMessage<CommunityMemberResponse>, IMessage, IEquatable<CommunityMemberResponse>, IDeepCloneable<CommunityMemberResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberResponse> _parser = new MessageParser<CommunityMemberResponse>(() => new CommunityMemberResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityPacket community_;

	private float position_;

	private bool isFavorite_;

	private CommunityRoleUuid primaryCommunityRoleId_;

	private static readonly FieldCodec<string> _single_primaryCommunityRoleName_codec = FieldCodec.ForClassWrapper<string>(114u);

	private string primaryCommunityRoleName_;

	private int activeCount_;

	private Timestamp subscribedAt_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityPacket Community
	{
		get
		{
			return community_;
		}
		set
		{
			community_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public float Position
	{
		get
		{
			return position_;
		}
		set
		{
			position_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsFavorite
	{
		get
		{
			return isFavorite_;
		}
		set
		{
			isFavorite_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleUuid PrimaryCommunityRoleId
	{
		get
		{
			return primaryCommunityRoleId_;
		}
		set
		{
			primaryCommunityRoleId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string PrimaryCommunityRoleName
	{
		get
		{
			return primaryCommunityRoleName_;
		}
		set
		{
			primaryCommunityRoleName_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int ActiveCount
	{
		get
		{
			return activeCount_;
		}
		set
		{
			activeCount_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp SubscribedAt
	{
		get
		{
			return subscribedAt_;
		}
		set
		{
			subscribedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberResponse(CommunityMemberResponse other)
		: this()
	{
		community_ = ((other.community_ != null) ? other.community_.Clone() : null);
		position_ = other.position_;
		isFavorite_ = other.isFavorite_;
		primaryCommunityRoleId_ = ((other.primaryCommunityRoleId_ != null) ? other.primaryCommunityRoleId_.Clone() : null);
		PrimaryCommunityRoleName = other.PrimaryCommunityRoleName;
		activeCount_ = other.activeCount_;
		subscribedAt_ = ((other.subscribedAt_ != null) ? other.subscribedAt_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberResponse Clone()
	{
		return new CommunityMemberResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Community, other.Community))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Position, other.Position))
		{
			return false;
		}
		if (IsFavorite != other.IsFavorite)
		{
			return false;
		}
		if (!object.Equals(PrimaryCommunityRoleId, other.PrimaryCommunityRoleId))
		{
			return false;
		}
		if (PrimaryCommunityRoleName != other.PrimaryCommunityRoleName)
		{
			return false;
		}
		if (ActiveCount != other.ActiveCount)
		{
			return false;
		}
		if (!object.Equals(SubscribedAt, other.SubscribedAt))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (community_ != null)
		{
			num ^= Community.GetHashCode();
		}
		if (Position != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Position);
		}
		if (IsFavorite)
		{
			num ^= IsFavorite.GetHashCode();
		}
		if (primaryCommunityRoleId_ != null)
		{
			num ^= PrimaryCommunityRoleId.GetHashCode();
		}
		if (primaryCommunityRoleName_ != null)
		{
			num ^= PrimaryCommunityRoleName.GetHashCode();
		}
		if (ActiveCount != 0)
		{
			num ^= ActiveCount.GetHashCode();
		}
		if (subscribedAt_ != null)
		{
			num ^= SubscribedAt.GetHashCode();
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
		if (community_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Community);
		}
		if (Position != 0f)
		{
			P_0.WriteRawTag(93);
			P_0.WriteFloat(Position);
		}
		if (IsFavorite)
		{
			P_0.WriteRawTag(96);
			P_0.WriteBool(IsFavorite);
		}
		if (primaryCommunityRoleId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(PrimaryCommunityRoleId);
		}
		if (primaryCommunityRoleName_ != null)
		{
			_single_primaryCommunityRoleName_codec.WriteTagAndValue(ref P_0, PrimaryCommunityRoleName);
		}
		if (ActiveCount != 0)
		{
			P_0.WriteRawTag(120);
			P_0.WriteInt32(ActiveCount);
		}
		if (subscribedAt_ != null)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(SubscribedAt);
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
		if (community_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Community);
		}
		if (Position != 0f)
		{
			num += 5;
		}
		if (IsFavorite)
		{
			num += 2;
		}
		if (primaryCommunityRoleId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(PrimaryCommunityRoleId);
		}
		if (primaryCommunityRoleName_ != null)
		{
			num += _single_primaryCommunityRoleName_codec.CalculateSizeWithTag(PrimaryCommunityRoleName);
		}
		if (ActiveCount != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(ActiveCount);
		}
		if (subscribedAt_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SubscribedAt);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.community_ != null)
		{
			if (community_ == null)
			{
				Community = new CommunityPacket();
			}
			Community.MergeFrom(other.Community);
		}
		if (other.Position != 0f)
		{
			Position = other.Position;
		}
		if (other.IsFavorite)
		{
			IsFavorite = other.IsFavorite;
		}
		if (other.primaryCommunityRoleId_ != null)
		{
			if (primaryCommunityRoleId_ == null)
			{
				PrimaryCommunityRoleId = new CommunityRoleUuid();
			}
			PrimaryCommunityRoleId.MergeFrom(other.PrimaryCommunityRoleId);
		}
		if (other.primaryCommunityRoleName_ != null && (primaryCommunityRoleName_ == null || other.PrimaryCommunityRoleName != ""))
		{
			PrimaryCommunityRoleName = other.PrimaryCommunityRoleName;
		}
		if (other.ActiveCount != 0)
		{
			ActiveCount = other.ActiveCount;
		}
		if (other.subscribedAt_ != null)
		{
			if (subscribedAt_ == null)
			{
				SubscribedAt = new Timestamp();
			}
			SubscribedAt.MergeFrom(other.SubscribedAt);
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
				if (community_ == null)
				{
					Community = new CommunityPacket();
				}
				P_0.ReadMessage(Community);
				break;
			case 93u:
				Position = P_0.ReadFloat();
				break;
			case 96u:
				IsFavorite = P_0.ReadBool();
				break;
			case 106u:
				if (primaryCommunityRoleId_ == null)
				{
					PrimaryCommunityRoleId = new CommunityRoleUuid();
				}
				P_0.ReadMessage(PrimaryCommunityRoleId);
				break;
			case 114u:
			{
				string text = _single_primaryCommunityRoleName_codec.Read(ref P_0);
				if (primaryCommunityRoleName_ == null || text != "")
				{
					PrimaryCommunityRoleName = text;
				}
				break;
			}
			case 120u:
				ActiveCount = P_0.ReadInt32();
				break;
			case 130u:
				if (subscribedAt_ == null)
				{
					SubscribedAt = new Timestamp();
				}
				P_0.ReadMessage(SubscribedAt);
				break;
			}
		}
	}
}
