using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.Core;

public sealed class MessageUri : IMessage<MessageUri>, IMessage, IEquatable<MessageUri>, IDeepCloneable<MessageUri>, IBufferMessage
{
	private static readonly MessageParser<MessageUri> _parser = new MessageParser<MessageUri>(() => new MessageUri());

	private UnknownFieldSet _unknownFields;

	private string uri_ = "";

	private MessageUriAttachment attachment_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageUri> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Uri
	{
		get
		{
			return uri_;
		}
		set
		{
			uri_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageUriAttachment Attachment
	{
		get
		{
			return attachment_;
		}
		set
		{
			attachment_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageUri()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageUri(MessageUri other)
		: this()
	{
		uri_ = other.uri_;
		attachment_ = ((other.attachment_ != null) ? other.attachment_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageUri Clone()
	{
		return new MessageUri(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageUri);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageUri other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Uri != other.Uri)
		{
			return false;
		}
		if (!object.Equals(Attachment, other.Attachment))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Uri.Length != 0)
		{
			num ^= Uri.GetHashCode();
		}
		if (attachment_ != null)
		{
			num ^= Attachment.GetHashCode();
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
		if (Uri.Length != 0)
		{
			P_0.WriteRawTag(10);
			P_0.WriteString(Uri);
		}
		if (attachment_ != null)
		{
			P_0.WriteRawTag(18);
			P_0.WriteMessage(Attachment);
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
		if (Uri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Uri);
		}
		if (attachment_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Attachment);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageUri other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Uri.Length != 0)
		{
			Uri = other.Uri;
		}
		if (other.attachment_ != null)
		{
			if (attachment_ == null)
			{
				Attachment = new MessageUriAttachment();
			}
			Attachment.MergeFrom(other.Attachment);
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
				Uri = P_0.ReadString();
				break;
			case 18u:
				if (attachment_ == null)
				{
					Attachment = new MessageUriAttachment();
				}
				P_0.ReadMessage(Attachment);
				break;
			}
		}
	}
}
