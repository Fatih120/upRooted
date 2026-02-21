using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class MessageSearchRequest : IMessage<MessageSearchRequest>, IMessage, IEquatable<MessageSearchRequest>, IDeepCloneable<MessageSearchRequest>, IBufferMessage
{
	private static readonly MessageParser<MessageSearchRequest> _parser = new MessageParser<MessageSearchRequest>(() => new MessageSearchRequest());

	private UnknownFieldSet _unknownFields;

	private MessageContainerUuid containerId_;

	private CommunityUuid communityId_;

	private string search_ = "";

	private MessageUuid lastMessageId_;

	private static readonly FieldCodec<int?> _single_limit_codec = FieldCodec.ForStructWrapper<int>(114u);

	private int? limit_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageSearchRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[10];

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
	public string Search
	{
		get
		{
			return search_;
		}
		set
		{
			search_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageUuid LastMessageId
	{
		get
		{
			return lastMessageId_;
		}
		set
		{
			lastMessageId_ = value;
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
	public MessageSearchRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageSearchRequest(MessageSearchRequest other)
		: this()
	{
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		search_ = other.search_;
		lastMessageId_ = ((other.lastMessageId_ != null) ? other.lastMessageId_.Clone() : null);
		Limit = other.Limit;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageSearchRequest Clone()
	{
		return new MessageSearchRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageSearchRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageSearchRequest other)
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
		if (Search != other.Search)
		{
			return false;
		}
		if (!object.Equals(LastMessageId, other.LastMessageId))
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
		if (Search.Length != 0)
		{
			num ^= Search.GetHashCode();
		}
		if (lastMessageId_ != null)
		{
			num ^= LastMessageId.GetHashCode();
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
		if (Search.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Search);
		}
		if (lastMessageId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(LastMessageId);
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
		if (Search.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Search);
		}
		if (lastMessageId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(LastMessageId);
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
	public void MergeFrom(MessageSearchRequest other)
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
		if (other.Search.Length != 0)
		{
			Search = other.Search;
		}
		if (other.lastMessageId_ != null)
		{
			if (lastMessageId_ == null)
			{
				LastMessageId = new MessageUuid();
			}
			LastMessageId.MergeFrom(other.LastMessageId);
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
			case 98u:
				Search = P_0.ReadString();
				break;
			case 106u:
				if (lastMessageId_ == null)
				{
					LastMessageId = new MessageUuid();
				}
				P_0.ReadMessage(LastMessageId);
				break;
			case 114u:
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
