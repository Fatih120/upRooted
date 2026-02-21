using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityMemberRoleAddRequest : IMessage<CommunityMemberRoleAddRequest>, IMessage, IEquatable<CommunityMemberRoleAddRequest>, IDeepCloneable<CommunityMemberRoleAddRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberRoleAddRequest> _parser = new MessageParser<CommunityMemberRoleAddRequest>(() => new CommunityMemberRoleAddRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private CommunityRoleUuid communityRoleId_;

	private static readonly FieldCodec<UserUuid> _repeated_userIds_codec = FieldCodec.ForMessage(98u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> userIds_ = new RepeatedField<UserUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberRoleAddRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberRoleReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<UserUuid> UserIds => userIds_;

	[GeneratedCode("protoc", null)]
	public CommunityMemberRoleAddRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberRoleAddRequest(CommunityMemberRoleAddRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		communityRoleId_ = ((other.communityRoleId_ != null) ? other.communityRoleId_.Clone() : null);
		userIds_ = other.userIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberRoleAddRequest Clone()
	{
		return new CommunityMemberRoleAddRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberRoleAddRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberRoleAddRequest other)
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
		if (!object.Equals(CommunityRoleId, other.CommunityRoleId))
		{
			return false;
		}
		if (!userIds_.Equals(other.userIds_))
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
		if (communityRoleId_ != null)
		{
			num ^= CommunityRoleId.GetHashCode();
		}
		num ^= userIds_.GetHashCode();
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
		if (communityRoleId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(CommunityRoleId);
		}
		userIds_.WriteTo(ref P_0, _repeated_userIds_codec);
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
		if (communityRoleId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityRoleId);
		}
		num += userIds_.CalculateSize(_repeated_userIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberRoleAddRequest other)
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
		if (other.communityRoleId_ != null)
		{
			if (communityRoleId_ == null)
			{
				CommunityRoleId = new CommunityRoleUuid();
			}
			CommunityRoleId.MergeFrom(other.CommunityRoleId);
		}
		userIds_.Add(other.userIds_);
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
				if (communityRoleId_ == null)
				{
					CommunityRoleId = new CommunityRoleUuid();
				}
				P_0.ReadMessage(CommunityRoleId);
				break;
			case 98u:
				userIds_.AddEntriesFrom(ref P_0, _repeated_userIds_codec);
				break;
			}
		}
	}
}
