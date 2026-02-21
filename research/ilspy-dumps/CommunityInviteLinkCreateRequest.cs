using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityInviteLinkCreateRequest : IMessage<CommunityInviteLinkCreateRequest>, IMessage, IEquatable<CommunityInviteLinkCreateRequest>, IDeepCloneable<CommunityInviteLinkCreateRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityInviteLinkCreateRequest> _parser = new MessageParser<CommunityInviteLinkCreateRequest>(() => new CommunityInviteLinkCreateRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private static readonly FieldCodec<int?> _single_maxUses_codec = FieldCodec.ForStructWrapper<int>(90u);

	private int? maxUses_;

	private Timestamp expiresAt_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityInviteLinkCreateRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => LinkReflection.Descriptor.MessageTypes[4];

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
	public int? MaxUses
	{
		get
		{
			return maxUses_;
		}
		set
		{
			maxUses_ = value;
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
	public CommunityInviteLinkCreateRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkCreateRequest(CommunityInviteLinkCreateRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		MaxUses = other.MaxUses;
		expiresAt_ = ((other.expiresAt_ != null) ? other.expiresAt_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkCreateRequest Clone()
	{
		return new CommunityInviteLinkCreateRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityInviteLinkCreateRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityInviteLinkCreateRequest other)
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
		if (MaxUses != other.MaxUses)
		{
			return false;
		}
		if (!object.Equals(ExpiresAt, other.ExpiresAt))
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
		if (maxUses_.HasValue)
		{
			num ^= MaxUses.GetHashCode();
		}
		if (expiresAt_ != null)
		{
			num ^= ExpiresAt.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
		}
		if (maxUses_.HasValue)
		{
			_single_maxUses_codec.WriteTagAndValue(ref P_0, MaxUses);
		}
		if (expiresAt_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(ExpiresAt);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (maxUses_.HasValue)
		{
			num += _single_maxUses_codec.CalculateSizeWithTag(MaxUses);
		}
		if (expiresAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ExpiresAt);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityInviteLinkCreateRequest other)
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
		if (other.maxUses_.HasValue && (!maxUses_.HasValue || other.MaxUses != 0))
		{
			MaxUses = other.MaxUses;
		}
		if (other.expiresAt_ != null)
		{
			if (expiresAt_ == null)
			{
				ExpiresAt = new Timestamp();
			}
			ExpiresAt.MergeFrom(other.ExpiresAt);
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
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
			{
				int? num2 = _single_maxUses_codec.Read(ref P_0);
				if (!maxUses_.HasValue || num2 != 0)
				{
					MaxUses = num2;
				}
				break;
			}
			case 98u:
				if (expiresAt_ == null)
				{
					ExpiresAt = new Timestamp();
				}
				P_0.ReadMessage(ExpiresAt);
				break;
			}
		}
	}
}
