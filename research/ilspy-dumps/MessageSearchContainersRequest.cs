using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class MessageSearchContainersRequest : IMessage<MessageSearchContainersRequest>, IMessage, IEquatable<MessageSearchContainersRequest>, IDeepCloneable<MessageSearchContainersRequest>, IBufferMessage
{
	private static readonly MessageParser<MessageSearchContainersRequest> _parser = new MessageParser<MessageSearchContainersRequest>(() => new MessageSearchContainersRequest());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private static readonly FieldCodec<MessageContainerUuid> _repeated_containerIds_codec = FieldCodec.ForMessage(82u, MessageContainerUuid.Parser);

	private readonly RepeatedField<MessageContainerUuid> containerIds_ = new RepeatedField<MessageContainerUuid>();

	private string search_ = "";

	private MessageUuid lastMessageId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageSearchContainersRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[11];

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
	public RepeatedField<MessageContainerUuid> ContainerIds => containerIds_;

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
	public MessageSearchContainersRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageSearchContainersRequest(MessageSearchContainersRequest other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerIds_ = other.containerIds_.Clone();
		search_ = other.search_;
		lastMessageId_ = ((other.lastMessageId_ != null) ? other.lastMessageId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageSearchContainersRequest Clone()
	{
		return new MessageSearchContainersRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageSearchContainersRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageSearchContainersRequest other)
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
		if (!containerIds_.Equals(other.containerIds_))
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
		num ^= containerIds_.GetHashCode();
		if (Search.Length != 0)
		{
			num ^= Search.GetHashCode();
		}
		if (lastMessageId_ != null)
		{
			num ^= LastMessageId.GetHashCode();
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
		containerIds_.WriteTo(ref P_0, _repeated_containerIds_codec);
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
		num += containerIds_.CalculateSize(_repeated_containerIds_codec);
		if (Search.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Search);
		}
		if (lastMessageId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(LastMessageId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageSearchContainersRequest other)
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
		containerIds_.Add(other.containerIds_);
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
				containerIds_.AddEntriesFrom(ref P_0, _repeated_containerIds_codec);
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
			}
		}
	}
}
