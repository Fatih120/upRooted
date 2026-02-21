using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class MessageListRequest : IMessage<MessageListRequest>, IMessage, IEquatable<MessageListRequest>, IDeepCloneable<MessageListRequest>, IBufferMessage
{
	private static readonly MessageParser<MessageListRequest> _parser = new MessageParser<MessageListRequest>(() => new MessageListRequest());

	private UnknownFieldSet _unknownFields;

	private MessageContainerUuid containerId_;

	private CommunityUuid communityId_;

	private MessageDirectionTake messageDirectionTake_ = MessageDirectionTake.Unspecified;

	private Timestamp dateAt_;

	private static readonly FieldCodec<int?> _single_limit_codec = FieldCodec.ForStructWrapper<int>(122u);

	private int? limit_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageListRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessageContainerUuid ContainerId
	{
		get
		{
			return containerId_;
		}
		set
		{
			containerId_ = value;
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
	public MessageDirectionTake MessageDirectionTake
	{
		get
		{
			return messageDirectionTake_;
		}
		set
		{
			messageDirectionTake_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp DateAt
	{
		get
		{
			return dateAt_;
		}
		set
		{
			dateAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int? Limit
	{
		get
		{
			return limit_;
		}
		set
		{
			limit_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageListRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageListRequest(MessageListRequest other)
		: this()
	{
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		messageDirectionTake_ = other.messageDirectionTake_;
		dateAt_ = ((other.dateAt_ != null) ? other.dateAt_.Clone() : null);
		Limit = other.Limit;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageListRequest Clone()
	{
		return new MessageListRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageListRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageListRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (MessageDirectionTake != other.MessageDirectionTake)
		{
			return false;
		}
		if (!object.Equals(DateAt, other.DateAt))
		{
			return false;
		}
		if (Limit != other.Limit)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (MessageDirectionTake != MessageDirectionTake.Unspecified)
		{
			num ^= MessageDirectionTake.GetHashCode();
		}
		if (dateAt_ != null)
		{
			num ^= DateAt.GetHashCode();
		}
		if (limit_.HasValue)
		{
			num ^= Limit.GetHashCode();
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
		if (containerId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(ContainerId);
		}
		if (communityId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(CommunityId);
		}
		if (MessageDirectionTake != MessageDirectionTake.Unspecified)
		{
			P_0.WriteRawTag(96);
			P_0.WriteEnum((int)MessageDirectionTake);
		}
		if (dateAt_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(DateAt);
		}
		if (limit_.HasValue)
		{
			_single_limit_codec.WriteTagAndValue(ref P_0, Limit);
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
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (MessageDirectionTake != MessageDirectionTake.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MessageDirectionTake);
		}
		if (dateAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DateAt);
		}
		if (limit_.HasValue)
		{
			num += _single_limit_codec.CalculateSizeWithTag(Limit);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageListRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new MessageContainerUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.MessageDirectionTake != MessageDirectionTake.Unspecified)
		{
			MessageDirectionTake = other.MessageDirectionTake;
		}
		if (other.dateAt_ != null)
		{
			if (dateAt_ == null)
			{
				DateAt = new Timestamp();
			}
			DateAt.MergeFrom(other.DateAt);
		}
		if (other.limit_.HasValue && (!limit_.HasValue || other.Limit != 0))
		{
			Limit = other.Limit;
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
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 90u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 96u:
				MessageDirectionTake = (MessageDirectionTake)P_0.ReadEnum();
				break;
			case 106u:
				if (dateAt_ == null)
				{
					DateAt = new Timestamp();
				}
				P_0.ReadMessage(DateAt);
				break;
			case 122u:
			{
				int? num2 = _single_limit_codec.Read(ref P_0);
				if (!limit_.HasValue || num2 != 0)
				{
					Limit = num2;
				}
				break;
			}
			}
		}
	}
}
