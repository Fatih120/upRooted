using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class FriendshipMoveRequest : IMessage<FriendshipMoveRequest>, IMessage, IEquatable<FriendshipMoveRequest>, IDeepCloneable<FriendshipMoveRequest>, IBufferMessage
{
	private static readonly MessageParser<FriendshipMoveRequest> _parser = new MessageParser<FriendshipMoveRequest>(() => new FriendshipMoveRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private UserUuid friendUserId_;

	private FriendshipGroupUuid oldFriendshipGroupId_;

	private FriendshipGroupUuid newFriendshipGroupId_;

	private FriendshipUuid beforeFriendshipId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipMoveRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[5];

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
	public UserUuid FriendUserId
	{
		get
		{
			return friendUserId_;
		}
		set
		{
			friendUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupUuid OldFriendshipGroupId
	{
		get
		{
			return oldFriendshipGroupId_;
		}
		set
		{
			oldFriendshipGroupId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupUuid NewFriendshipGroupId
	{
		get
		{
			return newFriendshipGroupId_;
		}
		set
		{
			newFriendshipGroupId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipUuid BeforeFriendshipId
	{
		get
		{
			return beforeFriendshipId_;
		}
		set
		{
			beforeFriendshipId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipMoveRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipMoveRequest(FriendshipMoveRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		friendUserId_ = ((other.friendUserId_ != null) ? other.friendUserId_.Clone() : null);
		oldFriendshipGroupId_ = ((other.oldFriendshipGroupId_ != null) ? other.oldFriendshipGroupId_.Clone() : null);
		newFriendshipGroupId_ = ((other.newFriendshipGroupId_ != null) ? other.newFriendshipGroupId_.Clone() : null);
		beforeFriendshipId_ = ((other.beforeFriendshipId_ != null) ? other.beforeFriendshipId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipMoveRequest Clone()
	{
		return new FriendshipMoveRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipMoveRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipMoveRequest other)
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
		if (!object.Equals(FriendUserId, other.FriendUserId))
		{
			return false;
		}
		if (!object.Equals(OldFriendshipGroupId, other.OldFriendshipGroupId))
		{
			return false;
		}
		if (!object.Equals(NewFriendshipGroupId, other.NewFriendshipGroupId))
		{
			return false;
		}
		if (!object.Equals(BeforeFriendshipId, other.BeforeFriendshipId))
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
		if (friendUserId_ != null)
		{
			num ^= FriendUserId.GetHashCode();
		}
		if (oldFriendshipGroupId_ != null)
		{
			num ^= OldFriendshipGroupId.GetHashCode();
		}
		if (newFriendshipGroupId_ != null)
		{
			num ^= NewFriendshipGroupId.GetHashCode();
		}
		if (beforeFriendshipId_ != null)
		{
			num ^= BeforeFriendshipId.GetHashCode();
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
		if (friendUserId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(FriendUserId);
		}
		if (oldFriendshipGroupId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(OldFriendshipGroupId);
		}
		if (newFriendshipGroupId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(NewFriendshipGroupId);
		}
		if (beforeFriendshipId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(BeforeFriendshipId);
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
		if (friendUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendUserId);
		}
		if (oldFriendshipGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OldFriendshipGroupId);
		}
		if (newFriendshipGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(NewFriendshipGroupId);
		}
		if (beforeFriendshipId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeforeFriendshipId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipMoveRequest other)
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
		if (other.friendUserId_ != null)
		{
			if (friendUserId_ == null)
			{
				FriendUserId = new UserUuid();
			}
			FriendUserId.MergeFrom(other.FriendUserId);
		}
		if (other.oldFriendshipGroupId_ != null)
		{
			if (oldFriendshipGroupId_ == null)
			{
				OldFriendshipGroupId = new FriendshipGroupUuid();
			}
			OldFriendshipGroupId.MergeFrom(other.OldFriendshipGroupId);
		}
		if (other.newFriendshipGroupId_ != null)
		{
			if (newFriendshipGroupId_ == null)
			{
				NewFriendshipGroupId = new FriendshipGroupUuid();
			}
			NewFriendshipGroupId.MergeFrom(other.NewFriendshipGroupId);
		}
		if (other.beforeFriendshipId_ != null)
		{
			if (beforeFriendshipId_ == null)
			{
				BeforeFriendshipId = new FriendshipUuid();
			}
			BeforeFriendshipId.MergeFrom(other.BeforeFriendshipId);
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
				if (friendUserId_ == null)
				{
					FriendUserId = new UserUuid();
				}
				P_0.ReadMessage(FriendUserId);
				break;
			case 90u:
				if (oldFriendshipGroupId_ == null)
				{
					OldFriendshipGroupId = new FriendshipGroupUuid();
				}
				P_0.ReadMessage(OldFriendshipGroupId);
				break;
			case 98u:
				if (newFriendshipGroupId_ == null)
				{
					NewFriendshipGroupId = new FriendshipGroupUuid();
				}
				P_0.ReadMessage(NewFriendshipGroupId);
				break;
			case 106u:
				if (beforeFriendshipId_ == null)
				{
					BeforeFriendshipId = new FriendshipUuid();
				}
				P_0.ReadMessage(BeforeFriendshipId);
				break;
			}
		}
	}
}
