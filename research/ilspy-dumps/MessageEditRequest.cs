using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class MessageEditRequest : IMessage<MessageEditRequest>, IMessage, IEquatable<MessageEditRequest>, IDeepCloneable<MessageEditRequest>, IBufferMessage
{
	private static readonly MessageParser<MessageEditRequest> _parser = new MessageParser<MessageEditRequest>(() => new MessageEditRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private MessageContainerUuid containerId_;

	private CommunityUuid communityId_;

	private MessageUuid id_;

	private string content_ = "";

	private static readonly FieldCodec<string> _repeated_uris_codec = FieldCodec.ForString(114u);

	private readonly RepeatedField<string> uris_ = new RepeatedField<string>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageEditRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[2];

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
	public MessageUuid Id
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
	public string Content
	{
		get
		{
			return content_;
		}
		set
		{
			content_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<string> Uris => uris_;

	[GeneratedCode("protoc", null)]
	public MessageEditRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageEditRequest(MessageEditRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		content_ = other.content_;
		uris_ = other.uris_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageEditRequest Clone()
	{
		return new MessageEditRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageEditRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageEditRequest other)
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
		if (!object.Equals(ContainerId, other.ContainerId))
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
		if (Content != other.Content)
		{
			return false;
		}
		if (!uris_.Equals(other.uris_))
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
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (Content.Length != 0)
		{
			num ^= Content.GetHashCode();
		}
		num ^= uris_.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(Id);
		}
		if (Content.Length != 0)
		{
			P_0.WriteRawTag(106);
			P_0.WriteString(Content);
		}
		uris_.WriteTo(ref P_0, _repeated_uris_codec);
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
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (Content.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Content);
		}
		num += uris_.CalculateSize(_repeated_uris_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageEditRequest other)
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
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new MessageUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.Content.Length != 0)
		{
			Content = other.Content;
		}
		uris_.Add(other.uris_);
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
				if (id_ == null)
				{
					Id = new MessageUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 106u:
				Content = P_0.ReadString();
				break;
			case 114u:
				uris_.AddEntriesFrom(ref P_0, _repeated_uris_codec);
				break;
			}
		}
	}
}
