using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public sealed class AssetFile : IMessage<AssetFile>, IMessage, IEquatable<AssetFile>, IDeepCloneable<AssetFile>, IBufferMessage
{
	private static readonly MessageParser<AssetFile> _parser = new MessageParser<AssetFile>(() => new AssetFile());

	private UnknownFieldSet _unknownFields;

	private string url_ = "";

	private string mimeType_ = "";

	private string extensions_ = "";

	private long length_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetFile> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetInformationReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Url
	{
		get
		{
			return url_;
		}
		set
		{
			url_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public string Extensions
	{
		get
		{
			return extensions_;
		}
		set
		{
			extensions_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public AssetFile()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetFile(AssetFile other)
		: this()
	{
		url_ = other.url_;
		mimeType_ = other.mimeType_;
		extensions_ = other.extensions_;
		length_ = other.length_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetFile Clone()
	{
		return new AssetFile(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetFile);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetFile other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Url != other.Url)
		{
			return false;
		}
		if (MimeType != other.MimeType)
		{
			return false;
		}
		if (Extensions != other.Extensions)
		{
			return false;
		}
		if (Length != other.Length)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Url.Length != 0)
		{
			num ^= Url.GetHashCode();
		}
		if (MimeType.Length != 0)
		{
			num ^= MimeType.GetHashCode();
		}
		if (Extensions.Length != 0)
		{
			num ^= Extensions.GetHashCode();
		}
		if (Length != 0)
		{
			num ^= Length.GetHashCode();
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
		if (Url.Length != 0)
		{
			P_0.WriteRawTag(10);
			P_0.WriteString(Url);
		}
		if (MimeType.Length != 0)
		{
			P_0.WriteRawTag(18);
			P_0.WriteString(MimeType);
		}
		if (Extensions.Length != 0)
		{
			P_0.WriteRawTag(26);
			P_0.WriteString(Extensions);
		}
		if (Length != 0)
		{
			P_0.WriteRawTag(32);
			P_0.WriteInt64(Length);
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
		if (Url.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Url);
		}
		if (MimeType.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(MimeType);
		}
		if (Extensions.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Extensions);
		}
		if (Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(Length);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetFile other)
	{
		if (other != null)
		{
			if (other.Url.Length != 0)
			{
				Url = other.Url;
			}
			if (other.MimeType.Length != 0)
			{
				MimeType = other.MimeType;
			}
			if (other.Extensions.Length != 0)
			{
				Extensions = other.Extensions;
			}
			if (other.Length != 0)
			{
				Length = other.Length;
			}
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
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
				Url = P_0.ReadString();
				break;
			case 18u:
				MimeType = P_0.ReadString();
				break;
			case 26u:
				Extensions = P_0.ReadString();
				break;
			case 32u:
				Length = P_0.ReadInt64();
				break;
			}
		}
	}
}
