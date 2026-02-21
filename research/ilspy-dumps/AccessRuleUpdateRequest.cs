using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Identifiers;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class AccessRuleUpdateRequest : IMessage<AccessRuleUpdateRequest>, IMessage, IEquatable<AccessRuleUpdateRequest>, IDeepCloneable<AccessRuleUpdateRequest>, IBufferMessage
{
	private static readonly MessageParser<AccessRuleUpdateRequest> _parser = new MessageParser<AccessRuleUpdateRequest>(() => new AccessRuleUpdateRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private static readonly FieldCodec<AccessRuleCreateRequest> _repeated_creates_codec = FieldCodec.ForMessage(90u, AccessRuleCreateRequest.Parser);

	private readonly RepeatedField<AccessRuleCreateRequest> creates_ = new RepeatedField<AccessRuleCreateRequest>();

	private static readonly FieldCodec<AccessRuleEditRequest> _repeated_edits_codec = FieldCodec.ForMessage(98u, AccessRuleEditRequest.Parser);

	private readonly RepeatedField<AccessRuleEditRequest> edits_ = new RepeatedField<AccessRuleEditRequest>();

	private static readonly FieldCodec<AccessRuleDeleteRequest> _repeated_deletes_codec = FieldCodec.ForMessage(106u, AccessRuleDeleteRequest.Parser);

	private readonly RepeatedField<AccessRuleDeleteRequest> deletes_ = new RepeatedField<AccessRuleDeleteRequest>();

	public RootGuid? PrimaryId
	{
		get
		{
			switch ((Creates?.Count ?? 0) + (Edits?.Count ?? 0) + (Deletes?.Count ?? 0))
			{
			case 0:
				return null;
			case 1:
			{
				RepeatedField<AccessRuleCreateRequest> creates = Creates;
				if (creates != null && creates.Count > 0)
				{
					return Creates[0].ChannelOrChannelGroupId;
				}
				RepeatedField<AccessRuleEditRequest> edits = Edits;
				if (edits != null && edits.Count > 0)
				{
					return Edits[0].ChannelOrChannelGroupId;
				}
				RepeatedField<AccessRuleDeleteRequest> deletes = Deletes;
				if (deletes != null && deletes.Count > 0)
				{
					return Deletes[0].ChannelOrChannelGroupId;
				}
				break;
			}
			}
			ChannelOrChannelGroupGuid target0 = default(ChannelOrChannelGroupGuid);
			RoleOrMemberGuid attachment0 = default(RoleOrMemberGuid);
			bool hasFirst = false;
			bool allSameTarget = true;
			bool allSameAttachment = true;
			if (Creates != null)
			{
				foreach (AccessRuleCreateRequest create in Creates)
				{
					if (Consume(create.ChannelOrChannelGroupId, create.RoleOrMemberId))
					{
						return null;
					}
				}
			}
			if (Edits != null)
			{
				foreach (AccessRuleEditRequest edit in Edits)
				{
					if (Consume(edit.ChannelOrChannelGroupId, edit.RoleOrMemberId))
					{
						return null;
					}
				}
			}
			if (Deletes != null)
			{
				foreach (AccessRuleDeleteRequest delete in Deletes)
				{
					if (Consume(delete.ChannelOrChannelGroupId, delete.RoleOrMemberId))
					{
						return null;
					}
				}
			}
			if (allSameTarget)
			{
				return target0;
			}
			if (allSameAttachment)
			{
				return attachment0;
			}
			return null;
			bool Consume(ChannelOrChannelGroupUuid P_0, RoleOrMemberUuid P_1)
			{
				if (!hasFirst)
				{
					target0 = P_0;
					attachment0 = P_1;
					hasFirst = true;
				}
				else
				{
					if (allSameTarget && P_0 != (ChannelOrChannelGroupUuid)target0)
					{
						allSameTarget = false;
					}
					if (allSameAttachment && P_1 != (RoleOrMemberUuid)attachment0)
					{
						allSameAttachment = false;
					}
				}
				return !allSameTarget && !allSameAttachment;
			}
		}
	}

	[GeneratedCode("protoc", null)]
	public static MessageParser<AccessRuleUpdateRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AccessRuleReflection.Descriptor.MessageTypes[4];

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
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<AccessRuleCreateRequest> Creates => creates_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<AccessRuleEditRequest> Edits => edits_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<AccessRuleDeleteRequest> Deletes => deletes_;

	[GeneratedCode("protoc", null)]
	public AccessRuleUpdateRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleUpdateRequest(AccessRuleUpdateRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		creates_ = other.creates_.Clone();
		edits_ = other.edits_.Clone();
		deletes_ = other.deletes_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleUpdateRequest Clone()
	{
		return new AccessRuleUpdateRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AccessRuleUpdateRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AccessRuleUpdateRequest other)
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
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!creates_.Equals(other.creates_))
		{
			return false;
		}
		if (!edits_.Equals(other.edits_))
		{
			return false;
		}
		if (!deletes_.Equals(other.deletes_))
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
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		num ^= creates_.GetHashCode();
		num ^= edits_.GetHashCode();
		num ^= deletes_.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
		}
		creates_.WriteTo(ref P_0, _repeated_creates_codec);
		edits_.WriteTo(ref P_0, _repeated_edits_codec);
		deletes_.WriteTo(ref P_0, _repeated_deletes_codec);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		num += creates_.CalculateSize(_repeated_creates_codec);
		num += edits_.CalculateSize(_repeated_edits_codec);
		num += deletes_.CalculateSize(_repeated_deletes_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AccessRuleUpdateRequest other)
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
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		creates_.Add(other.creates_);
		edits_.Add(other.edits_);
		deletes_.Add(other.deletes_);
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
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
				creates_.AddEntriesFrom(ref P_0, _repeated_creates_codec);
				break;
			case 98u:
				edits_.AddEntriesFrom(ref P_0, _repeated_edits_codec);
				break;
			case 106u:
				deletes_.AddEntriesFrom(ref P_0, _repeated_deletes_codec);
				break;
			}
		}
	}
}
