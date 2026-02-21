using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class FriendshipGroupMoveRequest : IMessage<FriendshipGroupMoveRequest>, IMessage, IEquatable<FriendshipGroupMoveRequest>, IDeepCloneable<FriendshipGroupMoveRequest>, IBufferMessage
{
	private static readonly MessageParser<FriendshipGroupMoveRequest> _parser = new MessageParser<FriendshipGroupMoveRequest>(() => new FriendshipGroupMoveRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private FriendshipGroupUuid id_;

	private FriendshipGroupUuid beforeFriendshipGroupId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipGroupMoveRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[3];

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
	public FriendshipGroupUuid Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupUuid BeforeFriendshipGroupId
	{
		get
		{
			return beforeFriendshipGroupId_;
		}
		set
		{
			beforeFriendshipGroupId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupMoveRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupMoveRequest(FriendshipGroupMoveRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		beforeFriendshipGroupId_ = ((other.beforeFriendshipGroupId_ != null) ? other.beforeFriendshipGroupId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupMoveRequest Clone()
	{
		return new FriendshipGroupMoveRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipGroupMoveRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipGroupMoveRequest other)
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
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(BeforeFriendshipGroupId, other.BeforeFriendshipGroupId))
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
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (beforeFriendshipGroupId_ != null)
		{
			num ^= BeforeFriendshipGroupId.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Id);
		}
		if (beforeFriendshipGroupId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(BeforeFriendshipGroupId);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (beforeFriendshipGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeforeFriendshipGroupId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipGroupMoveRequest other)
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
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new FriendshipGroupUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.beforeFriendshipGroupId_ != null)
		{
			if (beforeFriendshipGroupId_ == null)
			{
				BeforeFriendshipGroupId = new FriendshipGroupUuid();
			}
			BeforeFriendshipGroupId.MergeFrom(other.BeforeFriendshipGroupId);
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
				if (id_ == null)
				{
					Id = new FriendshipGroupUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 98u:
				if (beforeFriendshipGroupId_ == null)
				{
					BeforeFriendshipGroupId = new FriendshipGroupUuid();
				}
				P_0.ReadMessage(BeforeFriendshipGroupId);
				break;
			}
		}
	}
}
