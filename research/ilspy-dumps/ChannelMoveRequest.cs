using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class ChannelMoveRequest : IMessage<ChannelMoveRequest>, IMessage, IEquatable<ChannelMoveRequest>, IDeepCloneable<ChannelMoveRequest>, IBufferMessage
{
	private static readonly MessageParser<ChannelMoveRequest> _parser = new MessageParser<ChannelMoveRequest>(() => new ChannelMoveRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private ChannelUuid id_;

	private ChannelGroupUuid oldChannelGroupId_;

	private ChannelGroupUuid newChannelGroupId_;

	private ChannelUuid beforeChannelId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelMoveRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelReflection.Descriptor.MessageTypes[4];

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
	public ChannelUuid Id
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
	public ChannelGroupUuid OldChannelGroupId
	{
		get
		{
			return oldChannelGroupId_;
		}
		set
		{
			oldChannelGroupId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupUuid NewChannelGroupId
	{
		get
		{
			return newChannelGroupId_;
		}
		set
		{
			newChannelGroupId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelUuid BeforeChannelId
	{
		get
		{
			return beforeChannelId_;
		}
		set
		{
			beforeChannelId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelMoveRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelMoveRequest(ChannelMoveRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		oldChannelGroupId_ = ((other.oldChannelGroupId_ != null) ? other.oldChannelGroupId_.Clone() : null);
		newChannelGroupId_ = ((other.newChannelGroupId_ != null) ? other.newChannelGroupId_.Clone() : null);
		beforeChannelId_ = ((other.beforeChannelId_ != null) ? other.beforeChannelId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelMoveRequest Clone()
	{
		return new ChannelMoveRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelMoveRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelMoveRequest other)
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
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(OldChannelGroupId, other.OldChannelGroupId))
		{
			return false;
		}
		if (!object.Equals(NewChannelGroupId, other.NewChannelGroupId))
		{
			return false;
		}
		if (!object.Equals(BeforeChannelId, other.BeforeChannelId))
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
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (oldChannelGroupId_ != null)
		{
			num ^= OldChannelGroupId.GetHashCode();
		}
		if (newChannelGroupId_ != null)
		{
			num ^= NewChannelGroupId.GetHashCode();
		}
		if (beforeChannelId_ != null)
		{
			num ^= BeforeChannelId.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(Id);
		}
		if (oldChannelGroupId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(OldChannelGroupId);
		}
		if (newChannelGroupId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(NewChannelGroupId);
		}
		if (beforeChannelId_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(BeforeChannelId);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (oldChannelGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OldChannelGroupId);
		}
		if (newChannelGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(NewChannelGroupId);
		}
		if (beforeChannelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeforeChannelId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelMoveRequest other)
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
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new ChannelUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.oldChannelGroupId_ != null)
		{
			if (oldChannelGroupId_ == null)
			{
				OldChannelGroupId = new ChannelGroupUuid();
			}
			OldChannelGroupId.MergeFrom(other.OldChannelGroupId);
		}
		if (other.newChannelGroupId_ != null)
		{
			if (newChannelGroupId_ == null)
			{
				NewChannelGroupId = new ChannelGroupUuid();
			}
			NewChannelGroupId.MergeFrom(other.NewChannelGroupId);
		}
		if (other.beforeChannelId_ != null)
		{
			if (beforeChannelId_ == null)
			{
				BeforeChannelId = new ChannelUuid();
			}
			BeforeChannelId.MergeFrom(other.BeforeChannelId);
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
				if (id_ == null)
				{
					Id = new ChannelUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 98u:
				if (oldChannelGroupId_ == null)
				{
					OldChannelGroupId = new ChannelGroupUuid();
				}
				P_0.ReadMessage(OldChannelGroupId);
				break;
			case 106u:
				if (newChannelGroupId_ == null)
				{
					NewChannelGroupId = new ChannelGroupUuid();
				}
				P_0.ReadMessage(NewChannelGroupId);
				break;
			case 114u:
				if (beforeChannelId_ == null)
				{
					BeforeChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(BeforeChannelId);
				break;
			}
		}
	}
}
