using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class MessageCreateRequest : IMessage<MessageCreateRequest>, IMessage, IEquatable<MessageCreateRequest>, IDeepCloneable<MessageCreateRequest>, IBufferMessage
{
	private static readonly MessageParser<MessageCreateRequest> _parser = new MessageParser<MessageCreateRequest>(() => new MessageCreateRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private MessageContainerUuid containerId_;

	private CommunityUuid communityId_;

	private static readonly FieldCodec<string> _single_content_codec = FieldCodec.ForClassWrapper<string>(98u);

	private string content_;

	private static readonly FieldCodec<string> _repeated_attachmentTokenUris_codec = FieldCodec.ForString(106u);

	private readonly RepeatedField<string> attachmentTokenUris_ = new RepeatedField<string>();

	private static readonly FieldCodec<MessageUuid> _repeated_parentMessageIds_codec = FieldCodec.ForMessage(114u, MessageUuid.Parser);

	private readonly RepeatedField<MessageUuid> parentMessageIds_ = new RepeatedField<MessageUuid>();

	private bool needsParentMessageNotification_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageCreateRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[0];

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
	public string Content
	{
		get
		{
			return content_;
		}
		set
		{
			content_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<string> AttachmentTokenUris => attachmentTokenUris_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<MessageUuid> ParentMessageIds => parentMessageIds_;

	[GeneratedCode("protoc", null)]
	public bool NeedsParentMessageNotification
	{
		get
		{
			return needsParentMessageNotification_;
		}
		set
		{
			needsParentMessageNotification_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageCreateRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageCreateRequest(MessageCreateRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		Content = other.Content;
		attachmentTokenUris_ = other.attachmentTokenUris_.Clone();
		parentMessageIds_ = other.parentMessageIds_.Clone();
		needsParentMessageNotification_ = other.needsParentMessageNotification_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageCreateRequest Clone()
	{
		return new MessageCreateRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageCreateRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageCreateRequest other)
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
		if (Content != other.Content)
		{
			return false;
		}
		if (!attachmentTokenUris_.Equals(other.attachmentTokenUris_))
		{
			return false;
		}
		if (!parentMessageIds_.Equals(other.parentMessageIds_))
		{
			return false;
		}
		if (NeedsParentMessageNotification != other.NeedsParentMessageNotification)
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
		if (content_ != null)
		{
			num ^= Content.GetHashCode();
		}
		num ^= attachmentTokenUris_.GetHashCode();
		num ^= parentMessageIds_.GetHashCode();
		if (NeedsParentMessageNotification)
		{
			num ^= NeedsParentMessageNotification.GetHashCode();
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
		if (content_ != null)
		{
			_single_content_codec.WriteTagAndValue(ref P_0, Content);
		}
		attachmentTokenUris_.WriteTo(ref P_0, _repeated_attachmentTokenUris_codec);
		parentMessageIds_.WriteTo(ref P_0, _repeated_parentMessageIds_codec);
		if (NeedsParentMessageNotification)
		{
			P_0.WriteRawTag(120);
			P_0.WriteBool(NeedsParentMessageNotification);
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
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (content_ != null)
		{
			num += _single_content_codec.CalculateSizeWithTag(Content);
		}
		num += attachmentTokenUris_.CalculateSize(_repeated_attachmentTokenUris_codec);
		num += parentMessageIds_.CalculateSize(_repeated_parentMessageIds_codec);
		if (NeedsParentMessageNotification)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageCreateRequest other)
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
		if (other.content_ != null && (content_ == null || other.Content != ""))
		{
			Content = other.Content;
		}
		attachmentTokenUris_.Add(other.attachmentTokenUris_);
		parentMessageIds_.Add(other.parentMessageIds_);
		if (other.NeedsParentMessageNotification)
		{
			NeedsParentMessageNotification = other.NeedsParentMessageNotification;
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
			{
				string text = _single_content_codec.Read(ref P_0);
				if (content_ == null || text != "")
				{
					Content = text;
				}
				break;
			}
			case 106u:
				attachmentTokenUris_.AddEntriesFrom(ref P_0, _repeated_attachmentTokenUris_codec);
				break;
			case 114u:
				parentMessageIds_.AddEntriesFrom(ref P_0, _repeated_parentMessageIds_codec);
				break;
			case 120u:
				NeedsParentMessageNotification = P_0.ReadBool();
				break;
			}
		}
	}
}
