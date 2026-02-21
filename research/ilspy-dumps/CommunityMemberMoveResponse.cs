using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberMoveResponse : IMessage<CommunityMemberMoveResponse>, IMessage, IEquatable<CommunityMemberMoveResponse>, IDeepCloneable<CommunityMemberMoveResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberMoveResponse> _parser = new MessageParser<CommunityMemberMoveResponse>(() => new CommunityMemberMoveResponse());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private bool isFavorite_;

	private CommunityUuid beforeCommunityId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberMoveResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberReflection.Descriptor.MessageTypes[2];

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
	public CommunityUuid BeforeCommunityId
	{
		get
		{
			return beforeCommunityId_;
		}
		set
		{
			beforeCommunityId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberMoveResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberMoveResponse(CommunityMemberMoveResponse other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		isFavorite_ = other.isFavorite_;
		beforeCommunityId_ = ((other.beforeCommunityId_ != null) ? other.beforeCommunityId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberMoveResponse Clone()
	{
		return new CommunityMemberMoveResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberMoveResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberMoveResponse other)
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
		if (IsFavorite != other.IsFavorite)
		{
			return false;
		}
		if (!object.Equals(BeforeCommunityId, other.BeforeCommunityId))
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
		if (IsFavorite)
		{
			num ^= IsFavorite.GetHashCode();
		}
		if (beforeCommunityId_ != null)
		{
			num ^= BeforeCommunityId.GetHashCode();
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
		if (IsFavorite)
		{
			P_0.WriteRawTag(40);
			P_0.WriteBool(IsFavorite);
		}
		if (beforeCommunityId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(BeforeCommunityId);
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
		if (IsFavorite)
		{
			num += 2;
		}
		if (beforeCommunityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeforeCommunityId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberMoveResponse other)
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
		if (other.IsFavorite)
		{
			IsFavorite = other.IsFavorite;
		}
		if (other.beforeCommunityId_ != null)
		{
			if (beforeCommunityId_ == null)
			{
				BeforeCommunityId = new CommunityUuid();
			}
			BeforeCommunityId.MergeFrom(other.BeforeCommunityId);
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
			case 40u:
				IsFavorite = P_0.ReadBool();
				break;
			case 50u:
				if (beforeCommunityId_ == null)
				{
					BeforeCommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(BeforeCommunityId);
				break;
			}
		}
	}
}
