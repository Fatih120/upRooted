using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityMemberListRequest : IMessage<CommunityMemberListRequest>, IMessage, IEquatable<CommunityMemberListRequest>, IDeepCloneable<CommunityMemberListRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberListRequest> _parser = new MessageParser<CommunityMemberListRequest>(() => new CommunityMemberListRequest());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private static readonly FieldCodec<UserUuid> _repeated_userIds_codec = FieldCodec.ForMessage(90u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> userIds_ = new RepeatedField<UserUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberListRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public RepeatedField<UserUuid> UserIds => userIds_;

	[GeneratedCode("protoc", null)]
	public CommunityMemberListRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberListRequest(CommunityMemberListRequest other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		userIds_ = other.userIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberListRequest Clone()
	{
		return new CommunityMemberListRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberListRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberListRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
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
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		num += userIds_.CalculateSize(_repeated_userIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberListRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
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
			case 82u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
				userIds_.AddEntriesFrom(ref P_0, _repeated_userIds_codec);
				break;
			}
		}
	}
}
