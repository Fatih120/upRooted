using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace RootApp.Core;

public sealed class MessageUriAttachment : IMessage<MessageUriAttachment>, IMessage, IEquatable<MessageUriAttachment>, IDeepCloneable<MessageUriAttachment>, IBufferMessage
{
	private static readonly MessageParser<MessageUriAttachment> _parser = new MessageParser<MessageUriAttachment>(() => new MessageUriAttachment());

	private UnknownFieldSet _unknownFields;

	private string fileName_ = "";

	private string mimeType_ = "";

	private long length_;

	private Timestamp modified_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageUriAttachment> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string FileName
	{
		get
		{
			return fileName_;
		}
		set
		{
			fileName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string MimeType
	{
		get
		{
			return mimeType_;
		}
		set
		{
			mimeType_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public long Length
	{
		get
		{
			return length_;
		}
		set
		{
			length_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp Modified
	{
		get
		{
			return modified_;
		}
		set
		{
			modified_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageUriAttachment()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageUriAttachment(MessageUriAttachment other)
		: this()
	{
		fileName_ = other.fileName_;
		mimeType_ = other.mimeType_;
		length_ = other.length_;
		modified_ = ((other.modified_ != null) ? other.modified_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageUriAttachment Clone()
	{
		return new MessageUriAttachment(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageUriAttachment);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageUriAttachment other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (FileName != other.FileName)
		{
			return false;
		}
		if (MimeType != other.MimeType)
		{
			return false;
		}
		if (Length != other.Length)
		{
			return false;
		}
		if (!object.Equals(Modified, other.Modified))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (FileName.Length != 0)
		{
			num ^= FileName.GetHashCode();
		}
		if (MimeType.Length != 0)
		{
			num ^= MimeType.GetHashCode();
		}
		if (Length != 0)
		{
			num ^= Length.GetHashCode();
		}
		if (modified_ != null)
		{
			num ^= Modified.GetHashCode();
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
		if (FileName.Length != 0)
		{
			P_0.WriteRawTag(10);
			P_0.WriteString(FileName);
		}
		if (MimeType.Length != 0)
		{
			P_0.WriteRawTag(18);
			P_0.WriteString(MimeType);
		}
		if (Length != 0)
		{
			P_0.WriteRawTag(24);
			P_0.WriteInt64(Length);
		}
		if (modified_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Modified);
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
		if (FileName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(FileName);
		}
		if (MimeType.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(MimeType);
		}
		if (Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(Length);
		}
		if (modified_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Modified);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageUriAttachment other)
	{
		if (other == null)
		{
			return;
		}
		if (other.FileName.Length != 0)
		{
			FileName = other.FileName;
		}
		if (other.MimeType.Length != 0)
		{
			MimeType = other.MimeType;
		}
		if (other.Length != 0)
		{
			Length = other.Length;
		}
		if (other.modified_ != null)
		{
			if (modified_ == null)
			{
				Modified = new Timestamp();
			}
			Modified.MergeFrom(other.Modified);
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
				FileName = P_0.ReadString();
				break;
			case 18u:
				MimeType = P_0.ReadString();
				break;
			case 24u:
				Length = P_0.ReadInt64();
				break;
			case 34u:
				if (modified_ == null)
				{
					Modified = new Timestamp();
				}
				P_0.ReadMessage(Modified);
				break;
			}
		}
	}
}
